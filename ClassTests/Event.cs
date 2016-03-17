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

namespace Span
{
    class Event
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
        public Tuple<uint, List<string>> getOverlapping()
        {
            List<string> overlapping = new List<string>();
            Occurrence myOccurrence = null;
            Occurrence otherOccurrence = null;
            foreach (Event other in All.Values)
            {
                //can't be itself
                if (other.Id == Id) continue;
                if (!other.Exists) continue;
                //must have overlapping categories
                if (new List<string>(other.Categories.Intersect(Categories)).Count == 0) continue;
                bool noOver = true;
                foreach (Occurrence ocr in Occurrences)
                {
                    if (ocr.Status == (OccurrenceStatus.Canceled | OccurrenceStatus.Deleted | OccurrenceStatus.Ignored)) continue;
                    foreach (Occurrence otherOcr in other.Occurrences)
                    {
                        if (otherOcr.Status == (OccurrenceStatus.Canceled | OccurrenceStatus.Deleted | OccurrenceStatus.Ignored)) continue;
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
        private void updateOccurrences()
        {
            m_allOccurrences = new List<Occurrence>(m_manualOccurrences);
            foreach (Period per in m_allRules)
            {
                foreach (Occurrence ocr in per.Occurrences)
                {
                    if (!ocr.IsChained())
                    {
                        if (!m_allOccurrences.Contains(ocr))
                        {
                            m_allOccurrences.Add(ocr);
                            m_manualOccurrences.Add(ocr);
                        }
                        continue;

                    }
                    if (ocr.Status == (OccurrenceStatus.Canceled | OccurrenceStatus.Deleted | OccurrenceStatus.Ignored)) continue;
                    bool unused = true;
                    foreach (Occurrence manualOcr in m_allOccurrences)
                    {
                        if (manualOcr.Status == (OccurrenceStatus.Canceled | OccurrenceStatus.Deleted | OccurrenceStatus.Ignored)) continue;
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
         */
        public List<Occurrence> Occurrences
        {
            get 
            {
                updateOccurrences();
                return m_allOccurrences; 
            }
        }

        /**
         * Gets or sets the list of all Period objects
         * that define the periodic Occurrences of this
         * Event.
         */
        public List<Period> Rules
        {
            get { return m_allRules; }
            set { m_allRules = value; }
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
        public List<string> SecondaryCategories
        {
            get 
            {
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


        protected List<Occurrence> m_allOccurrences;
        protected List<Occurrence> m_manualOccurrences;
        protected List<Period> m_allRules;
        protected string m_id;
        protected static uint num = 1;
        protected uint m_numId;
        protected List<string> m_categories;
        protected string m_name;
        protected string m_desc;
        protected string m_loc;
        protected AlarmSettings m_alarms;
        protected bool m_isTask;
        protected bool m_exists;
        protected static Dictionary<string, Event> all = new Dictionary<string, Event>();

    }
}
