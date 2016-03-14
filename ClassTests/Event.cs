using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Span
{
    class Event
    {

        public Event(bool a_isTask, string a_name, List<Occurrence> a_manualOccurrences, List<Period> a_allRules, string a_loc, List<string> a_categories, AlarmSettings a_alarms, string a_desc)
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

        
        public string Name 
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public bool Exists
        {
            get { return m_exists; }
            set { m_exists = value; }
        }

        public List<Occurrence> ManualOccurrences
        {
            get { return m_manualOccurrences; }
            set { m_manualOccurrences = value; }
        }

        //TODO: make this work with periods that are changed into
        //manual occurrences
        private void updateOccurrences()
        {
            m_allOccurrences = new List<Occurrence>(m_manualOccurrences);
            foreach (Period per in m_allRules)
            {
                foreach (Occurrence ocr in per.Occurrences)
                {
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

        public List<Occurrence> Occurrences
        {
            get 
            {
                updateOccurrences();
                return m_allOccurrences; 
            }
        }

        public List<Period> Rules
        {
            get { return m_allRules; }
            set { m_allRules = value; }
        }

        public string Location
        {
            get { return m_loc; }
            set { m_loc = value; }
        }

        public List<string> Categories
        {
            get { return m_categories; }
            set { m_categories = value; }
        }

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
            }
        }

        public AlarmSettings Alarms
        {
            get { return m_alarms; }
            set { m_alarms = value; }
        }

        public string Description
        {
            get { return m_desc; }
            set { m_desc = value; }
        }

        public string Id { get { return m_id; } }

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
