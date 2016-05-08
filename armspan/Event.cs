/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The Event class defines one single event
 * occurring on the timeline.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Span
{
    public class Event : JSONCapable
    {
        /**
         * Creates a new Event from the specified information.
         * 
         * @param a_isTask specifies if the Event is a task.
         * However, if it is a task, please use a TaskEvent object
         * instead.
         * 
         * @param a_name a name or title for the event. The name
         * can be changed at any time, as the event is internally
         * identified with an Id. This string must be single-line.
         * 
         * @param a_manualOccurrences a list of Occurrences defined
         * manually. These will occur only once on the timeline.
         * 
         * @param a_allRules a list of Period objects specifying
         * periodic Occurrences for the Event, which may occur
         * multiple times on the timeline.
         * 
         * @param a_loc a place or location for the event. This
         * parameter is mandatory, but will be ignored if it is
         * set to a blank string. This string must be single-line.
         * 
         * @param a_categories a list of ids for Categories to which
         * this Event belongs. Index 0 is the PrimaryCategory and
         * the rest are SecondaryCategories.
         * 
         * @param a_alarms an AlarmSettings object indicating when
         * alarms should go off for each Occurrence of this Event.
         * Note: the alarm settings are the same for every Occurrence,
         * but they can be changed at any time.
         * 
         * @param a_desc a description for the Event. This string may
         * be multi-line.
         * 
         * @date March 14, 2016
         */
        public Event(bool a_isTask, string a_name, List<Occurrence> a_manualOccurrences, List<Period> a_allRules, 
            string a_loc, List<string> a_categories, AlarmSettings a_alarms, string a_desc)
        {
            m_numId = num++;
            m_id = "e" + m_numId.ToString("x8");
            m_isTask = a_isTask;
            Name = a_name;
            ManualOccurrences = a_manualOccurrences;
            Rules = a_allRules;
            Location = a_loc;
            Categories = a_categories;
            Alarms = a_alarms;
            Description = a_desc;
            m_exists = true;
            All.Add(m_id, this);
        }

        /**
         * Creates a new Event object without
         * any initialized data.
         * 
         * Please only use this constructor for
         * deserialization.
         * 
         * @date March 19, 2016
         */
        protected Event(){}

        /**
         * Generates an Event object from the
         * specified JSON-serialized Event
         * string.
         * 
         * @param json the serialized string
         * representing the object.
         * 
         * @return the object, properly
         * deserialized and initialized.
         * 
         * @date March 19, 2016
         */
        public static Event FromJSON(string json)
        {
            //json to object types
            Dictionary<string, object> jsd = JSONDictionary(Event.FromString(json));
            bool exists = (bool)jsd["Exists"];
            if (!exists) return null;
            string name = (string)jsd["Name"];
            //manual occurrences
            List<Occurrence> manualoccurrences = new List<Occurrence>();
            List<Dictionary<string, object>> moraw = jss.ConvertToType<List<Dictionary<string, object>>>(jsd["ManualOccurrences"]);
            foreach (Dictionary<string, object> ocrraw in moraw)
            {
                Occurrence ocr = Occurrence.FromJSON(jss.Serialize(ocrraw));
                manualoccurrences.Add(ocr);
            }
            //periodic occurrences
            List<Period> rules = new List<Period>();
            List<Dictionary<string, object>> ruraw = jss.ConvertToType<List<Dictionary<string, object>>>(jsd["Rules"]);
            foreach (Dictionary<string, object> perraw in ruraw)
            {
                Period per = Period.FromJSON(jss.Serialize(perraw));
                rules.Add(per);
            }
            //remaining objects
            string loc = (string)jsd["Location"];
            List<string> cats = jss.ConvertToType<List<string>>(jsd["Categories"]);
            AlarmSettings als = AlarmSettings.FromJSON(jss.Serialize(jsd["Alarms"]));
            string desc = (string)jsd["Description"];
            bool istask = (bool)jsd["IsTask"];
           
            uint thisnum = (uint)(int)jsd["Number"];
            string id = (string)jsd["Id"];
            //loose objects to Event
            Event loaded = new Event();
            loaded.m_id = id;
            loaded.m_exists = true;
            loaded.m_alarms = als;
            loaded.m_manualOccurrences = manualoccurrences;
            loaded.m_allRules = rules;
            loaded.m_categories = cats;
            loaded.m_name = name;
            loaded.m_loc = loc;
            loaded.m_desc = desc;
            loaded.m_numId = thisnum;
            if (thisnum >= num)
            {
                num = thisnum + 1;
            }
            loaded.m_isTask = istask;
            if (istask)
            {
                loaded = (TaskEvent)loaded;
                uint times = (uint)(int)jsd["Times"];
                ((TaskEvent)loaded).Times = times;
            }
            All.Add(id, loaded);
            return loaded;
        }

        /**
         * Gets the earliest Occurrence of this Event.
         * 
         * @return the earliest Occurrence of this Event.
         * 
         * @date April 1, 2016
         */
        public Occurrence FirstOccurrence()
        {
            Occurrences().Sort((x, y) => x.StartActual.CompareTo(y.StartActual));
            Occurrence first = Occurrences()[0];
            Occurrences().Sort((x, y) => x.Number.CompareTo(y.Number));
            return first;
        }

        /**
         * Returns a list of the Ids of all Events overlapping this one
         * on the timeline.
         * 
         * @return a Tuple containing a uint value and a List of
         * strings. The uint value specifies the number of
         * overlapping events. If the uint value is 0, the
         * list should be empty. If the uint value is greater than
         * 1, the list includes that many event Ids. If the uint
         * is equal to 1, the list includes the event Id at index
         * 0, and the ids of the overlapping Occurrences for this
         * Event and the other Event at indices 1 and 2, respectively.
         */
        public Tuple<uint, List<string>> GetOverlapping()
        {
            List<string> overlapping = new List<string>();
            Occurrence myOccurrence = null;
            Occurrence otherOccurrence = null;
            OccurrenceStatus[] noInclude = { OccurrenceStatus.Canceled, OccurrenceStatus.Deleted, OccurrenceStatus.Ignored, OccurrenceStatus.Excluded };
            foreach (Event other in All.Values)
            {
                //can't be itself
                if (other.Id == Id) continue;
                if (!other.Exists) continue;
                //must have overlapping categories
                if (new List<string>(other.Categories.Intersect(Categories)).Count == 0) continue;
                bool noOver = true;
                //every valid occurrence of this event against every valid occurrence of every other event
                foreach (Occurrence ocr in Occurrences())
                {
                    if (noInclude.Contains(ocr.Status)) continue;
                    foreach (Occurrence otherOcr in other.Occurrences())
                    {
                        if (noInclude.Contains(otherOcr.Status)) continue;
                        if (ocr.Overlaps(otherOcr))
                        {
                            noOver = false;
                            if (!(myOccurrence != null))
                            {
                                myOccurrence = ocr;
                                otherOccurrence = otherOcr;
                            }
                            break;
                        }
                    }
                    if (!noOver)
                    {
                        break;
                    }
                }
                if (!noOver)
                {
                    overlapping.Add(other.Id);
                }
            }
            //include first instance of overlap if only two events involved
            if (overlapping.Count() == 1)
            {
                overlapping.Add(myOccurrence.Id);
                overlapping.Add(otherOccurrence.Id);
                return new Tuple<uint, List<string>>(1, overlapping);
            }
            return new Tuple<uint, List<string>>((uint)overlapping.Count, overlapping);
        }

        /**
         * Gets or sets a name or title for the event. 
         * This string must be single-line.
         */
        public string Name 
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /**
         * Specifies or sets whether this Event exists. True by
         * default. If false, the Event is removed from the timeline
         * and is not saved when the program closes, effectively and
         * safely deleting it from existence.
         */
        public bool Exists
        {
            get { return m_exists; }
            set { m_exists = value; }
        }

        /**
         * Gets or sets a list of Occurrences defined
         * manually. These will occur only once on the timeline.
         */
        public List<Occurrence> ManualOccurrences
        {
            get { return m_manualOccurrences; }
            set { m_manualOccurrences = value; }
        }

        /**
         * Updates the list of all Occurrences based on the list of
         * ManualOccurrences and the list of Rules.
         * 
         * @date March 14, 2016
         */
        private void UpdateOccurrences()
        {
            //just copy manual occ's
            m_allOccurrences = new List<Occurrence>(m_manualOccurrences);
            //update periodic occurrences
            foreach (Period per in m_allRules)
            {
                foreach (Occurrence ocr in per.Occurrences())
                {
                    //dechained occurrences have become manual
                    if (!ocr.IsChained())
                    {
                        if (!m_allOccurrences.Contains(ocr))
                        {
                            m_allOccurrences.Add(ocr);
                            m_manualOccurrences.Add(ocr);
                        }
                        continue;

                    }
                    OccurrenceStatus[] noInclude = 
                    {OccurrenceStatus.Canceled, OccurrenceStatus.Deleted, OccurrenceStatus.Ignored, OccurrenceStatus.Excluded};
                    if (noInclude.Contains(ocr.Status)) continue;
                    bool unused = true;
                    //manual occurrences take precedence over periodic
                    foreach (Occurrence manualOcr in m_allOccurrences)
                    {
                        if (noInclude.Contains(manualOcr.Status)) continue;
                        if (ocr.Overlaps(manualOcr))
                        {
                            unused = false;
                            break;
                        }
                    }
                    if (!unused) continue;
                    m_allOccurrences.Add(ocr);
                }
            }
        }

        /**
         * Gets the list of all Occurrences of this Event.
         * 
         * @date March 14, 2016
         */
        public List<Occurrence> Occurrences()
        {
            UpdateOccurrences();
            return m_allOccurrences; 
        }

        /**
         * Gets or sets the list of all Period objects
         * that define the periodic Occurrences of this
         * Event.
         */
        public List<Period> Rules
        {
            get { return m_allRules; }
            set 
            { 
                m_allRules = value;
                foreach (Period p in m_allRules)
                {
                    p.ForceUpdate();
                }
            }
        }

        /**
         * Gets or sets a place or location for the event. This
         * property will be ignored if it is
         * set to a blank string. This string must be single-line.
         */
        public string Location
        {
            get { return m_loc; }
            set { m_loc = value; }
        }

        /**
         * Gets or sets a list of ids for Categories to which
         * this Event belongs. Index 0 is the PrimaryCategory and
         * the rest are SecondaryCategories.
         */
        public List<string> Categories
        {
            get { return m_categories; }
            set { m_categories = value; }
        }

        /**
         * Gets or sets the primary Category to which this Event
         * belongs. If the primary category is set to one that
         * is already a secondary category, the existing primary
         * category and the desired one will switch.
         */
        [ScriptIgnore]
        public string PrimaryCategory
        {
            get { return Categories[0]; }
            set 
            { 
                int index = Categories.IndexOf(value);
                //no change required
                if (index == 0) return;
                //category hasn't been added yet
                if (index < 0)
                {
                    m_categories.Insert(0, value);
                    return;
                }
                //category is secondary
                m_categories[index] = m_categories[0];
                m_categories[0] = value;
            }
        }

        /**
         * Gets or sets the list of secondary Categories to
         * which this Event belongs. The existing primary
         * Category cannot be added to this list, and must be
         * changed first.
         * 
         * @throws ArgumentException if the primary category
         * is included in the list of secondary categories
         * to be set.
         */
        [ScriptIgnore]
        public List<string> SecondaryCategories
        {
            get 
            {
                //just return Categories without 1st element
                List<string> copy = new List<string>(Categories);
                copy.RemoveAt(0);
                return copy;
            }
            set 
            {
                if (value.IndexOf(PrimaryCategory) >= 0)
                {
                    throw new ArgumentException("Secondary categories cannot include primary category");
                }
                //updating Categories list indirectly updates SecondaryCategories
                List<string> copy = new List<string>(value);
                copy.Insert(0, PrimaryCategory);
                Categories = copy;
            }
        }

        /**
         * Gets or sets an AlarmSettings object indicating when
         * alarms should go off for each Occurrence of this Event.
         * Note: the alarm settings are the same for every Occurrence,
         * but they can be changed at any time.
         */
        public AlarmSettings Alarms
        {
            get { return m_alarms; }
            set { m_alarms = value; }
        }

        /**
         * Gets or sets a description for the Event. This string may
         * be multi-line.
         */
        public string Description
        {
            get { return m_desc; }
            set { m_desc = value; }
        }

        /**
         * Gets the id of the Event.
         * 
         * The id is written in the form of a string starting
         * with the letter 'e', followed by a 32-bit unsigned
         * integer in hexadecimal (all lowercase). This allows
         * the event to be looked up easily.
         * 
         * Currently, the integer portion of the id is
         * simply equal to the event's number.
         */
        public string Id { get { return m_id; } }

        /**
         * Gets a Dictionary containing all Events by Id.
         */
        public static Dictionary<string, Event> All { get { return all; } }

        /**
         * Gets the number of the occurrence.
         * 
         * Note: please use this property sparingly,
         * as the Id property is better-suited to
         * keeping track of the object. This property
         * exists in order to allow proper serialization
         * of the object.
         */
        public uint Number
        {
            get { return m_numId; }
        }

        /**
         * Specifies if the Event is a task.
         * 
         * If false, the Event is an appointment.
         * This should only be true if the Event is
         * also a TaskEvent object.
         */
        public bool IsTask { get { return m_isTask; } }

        /**
         * The list of all Occurrences. See also Occurrences().
         */
        protected List<Occurrence> m_allOccurrences;
        /**
         * The list of all manual Occurrences. See also ManualOccurrences.
         */
        protected List<Occurrence> m_manualOccurrences;
        /**
         * The list of all Periods. See also Rules.
         */
        protected List<Period> m_allRules;
        /**
         * The id of this Event. See also Id.
         */
        protected string m_id;
        /**
         * This counter is used to give each new Event a distinct Number.
         */
        protected static uint num = 1;
        /**
         * The number of this Event. See also Number.
         */
        protected uint m_numId;
        /**
         * The list of Category ids this Event belongs to. See also Categories, PrimaryCategory and SecondaryCategories.
         */
        protected List<string> m_categories;
        /**
         * The name of this Event. See also Name.
         */
        protected string m_name;
        /**
         * The description of this Event. See also Description.
         */
        protected string m_desc;
        /**
         * The location of this Event. See also Location.
         */
        protected string m_loc;
        /**
         * The AlarmSettings associated with this Event. See also Alarms.
         */
        protected AlarmSettings m_alarms;
        /**
         * Denotes whether this Event is a TaskEvent. See also IsTask.
         */
        protected bool m_isTask;
        /**
         * Denotes whether this Event exists. See also Exists.
         */
        protected bool m_exists;
        /**
         * Contains all Event objects as values, with their Id strings as keys. See also All.
         */
        protected static Dictionary<string, Event> all = new Dictionary<string, Event>();

    }
}
