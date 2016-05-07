/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The Period class defines a periodically reoccurring
 * set of Occurrences.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace Span
{

    /**
     * Denotes the unit of time in which to specify the
     * frequency of the Period.
     */
    public enum Frequency
    {
        /**
         * The Occurrence will reoccur in the specified
         * number of minutes.
         */
        Minutes,
        /**
         * The Occurrence will reoccur in the specified
         * number of hours.
         */
        Hours,
        /**
         * The Occurrence will reoccur in the specified
         * number of days.
         */
        Days,
        /**
         * The Occurrence will reoccur in the specified
         * number of weeks.
         */
        Weeks,
        /**
         * The Occurrence will reoccur in the specified
         * number of months.
         */
        Months,
        /**
         * The Occurrence will reoccur in the specified
         * number of years.
         */
        Years
    }

   
    public class Period : JSONCapable
    {

        /**
         * Creates a new Period from the specified times, length, and parent id.
         * 
         * @param a_frequency specifies how often the Event will occur.
         * 
         * @param a_timeUnit denotes the time unit used for a_frequency.
         * 
         * @param a_startDate a DateTime struct specifying the day on
         * which the Occurrences will start.
         * 
         * @param a_endDate a DateTime struct specifying the day on
         * which the Occurrences will end, inclusively (that is, the final day
         * on which they will occur).
         * 
         * @param a_startTime a DateTime struct specifying the earliest
         * time the Occurrences may occur every day. This is also the time
         * at which the first Occurrence will start.
         * 
         * @param a_endTime a DateTime struct specifying the latest
         * time the Occurrences may occur every day.
         * 
         * @param a_length a TimeSpan struct specifying the length of 
         * each Occurrence.
         * 
         * @param a_parent the id of the parent Event to which this
         * Period belongs.
         * 
         * @date March 10, 2016
         */
        public Period(uint a_frequency, Frequency a_timeUnit, DateTime a_startTime, DateTime a_endTime, TimeSpan a_length, string a_parent)
        {
            m_numId = num++;
            m_id = "p" + m_numId.ToString("x8");
            PeriodicFrequency = a_frequency;
            TimeUnit = a_timeUnit;
           
            StartTime = a_startTime;
           
            EndTime = a_endTime;
            OccurrenceLength = a_length;
            m_parent = a_parent;
            m_needUpdate = true;
            m_occurrences = null;
        }

        /**
         * Creates a new Period object without
         * any initialized data.
         * 
         * Please only use this constructor for
         * deserialization.
         * 
         * @date March 19, 2016
         */
        protected Period(){}

        /**
         * Generates a Period object from the
         * specified JSON-serialized Period
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
        public static Period FromJSON(string json)
        {
            Dictionary<string, object> jsd = JSONDictionary(Period.FromString(json));
            uint freq = (uint)(int)jsd["PeriodicFrequency"];
            uint thisnum = (uint)(int)jsd["Number"];
            string id = (string)jsd["Id"];
            Frequency timeunit = (Frequency)jsd["TimeUnit"];
            bool exclude = (bool)jsd["Excluded"];
            DateTime starttime = ((DateTime)jsd["StartTime"]).ToLocalTime();
            DateTime endtime = ((DateTime)jsd["EndTime"]).ToLocalTime();
            string parentid = (string)jsd["ParentId"];
            object olength = JSONDictionary(jsd["OccurrenceLength"])["Ticks"];
            long llength = 1;
            if (olength is long){
                llength = (long)olength;
            }
            else if (olength is int)
            {
                llength = (long)(int)olength;
            }
            else
            {
                throw new ArgumentException("type of " + olength.GetType().ToString());
            }
            TimeSpan length = new TimeSpan(llength);
           
            Period loaded = new Period();
            
            loaded.m_id = id;
            loaded.m_frequency = freq;
            loaded.m_timeUnit = timeunit;
            loaded.m_exclude = exclude;
            loaded.m_startTime = starttime;
            
            loaded.m_endTime = endtime;
            loaded.m_length = length;
            loaded.m_parent = parentid;
           
            loaded.m_numId = thisnum;
            if (thisnum >= num)
            {
                num = thisnum + 1;
            }
            loaded.m_needUpdate = true;
            loaded.m_occurrences = null;
            return loaded;
        }

        /**
         * Updates the list of Occurrences specified by this Period
         * based on the current settings.
         * 
         * @date March 10, 2016
         */
        private void updateOccurrences()
        {
            if (!m_needUpdate) return;
            //delete all chained occurrences. non-chained ones will remain elsewhere.
            if (m_occurrences != null)
            {
                foreach (Occurrence ocr in m_occurrences)
                {
                    if (ocr.IsChained())
                    {
                        ocr.Status = OccurrenceStatus.Deleted;
                    }
                }

            }
            m_occurrences = new List<Occurrence>();
            //set up start/end dates
            DateTime eachStartTime = StartTime;
            DateTime finalEndTime = EndTime;

            IEnumerable<Period> excludeRules = new List<Period>();
            IEnumerable<Occurrence> excludes = new List<Occurrence>();

            if (!Excluded)
            {
                excludeRules = Event.All[ParentId].Rules.Where(x => x.Excluded);
                excludes = excludeRules.SelectMany(x => x.Occurrences());
            }
            
            while (eachStartTime < finalEndTime)
            {
                DateTime eachEndTime = eachStartTime.Add(OccurrenceLength);
                //check for overlap with excluded Occurrences
                bool canCreate = true;
                foreach (Occurrence exOcc in excludes)
                {
                    if (eachStartTime == exOcc.StartActual || eachEndTime == exOcc.EndActual)
                    {
                        canCreate = false;
                        break;
                    }
                    if (eachStartTime > exOcc.EndActual || eachEndTime < exOcc.StartActual) continue;
                    if (exOcc.StartActual <= eachStartTime && eachStartTime <= exOcc.EndActual)
                    {
                        canCreate = false;
                        break;
                    }
                    if (exOcc.StartActual <= eachEndTime && eachEndTime <= exOcc.EndActual)
                    {
                        canCreate = false;
                        break;
                    }
                    if (eachStartTime <= exOcc.StartActual && exOcc.StartActual <= eachEndTime)
                    {
                        canCreate = false;
                        break;
                    }
                    if (eachStartTime <= exOcc.EndActual && exOcc.EndActual <= eachEndTime)
                    {
                        canCreate = false;
                        break;
                    }
                }
                if (canCreate)
                {
                    Occurrence newOcc = new Occurrence(false, eachStartTime, eachStartTime.Add(OccurrenceLength), m_parent);
                    if (Excluded)
                    {
                        newOcc.Status = OccurrenceStatus.Excluded;
                    }
                    newOcc.ChainId = Id;
                    m_occurrences.Add(newOcc);
                }
                //increment by time unit (esp. for months and years)
                switch (TimeUnit)
                {
                    case Frequency.Minutes:
                        eachStartTime = eachStartTime.AddMinutes(PeriodicFrequency);
                        break;
                    case Frequency.Hours:
                        eachStartTime = eachStartTime.AddHours(PeriodicFrequency);
                        break;
                    case Frequency.Days:
                        eachStartTime = eachStartTime.AddDays(PeriodicFrequency);
                        break;
                    case Frequency.Weeks:
                        eachStartTime = eachStartTime.AddDays(PeriodicFrequency * 7);
                        break;
                    case Frequency.Months:
                        eachStartTime = eachStartTime.AddMonths((int)PeriodicFrequency);
                        break;
                    case Frequency.Years:
                        eachStartTime = eachStartTime.AddYears((int)PeriodicFrequency);
                        break;
                }
               
            }
            m_needUpdate = false;
        }

        //TODO: document
        public void WipeOut()
        {
            //delete all chained occurrences. non-chained ones will remain elsewhere.
            if (m_occurrences != null)
            {
                foreach (Occurrence ocr in m_occurrences)
                {
                    if (ocr.IsChained())
                    {
                        ocr.Status = OccurrenceStatus.Deleted;
                    }
                }

            }
            Event.All[ParentId].Rules.Remove(this);
        }

        [ScriptIgnore]
        public string Describe
        {
            get
            {
                string outputter = "FROM: " + StartTime.ToShortDateString() + " at " + StartTime.ToShortTimeString()
                    + " TO: " + EndTime.ToShortDateString() + " at " + EndTime.ToShortTimeString()
                    + " EVERY: " + PeriodicFrequency + " " + TimeUnit.ToString().ToLower() 
                    + " for " + OccurrenceLength.TotalMinutes + " minutes";

                return outputter;
            }
        }

        /**
         * Removes the specified Occurrence from the Period.
         * 
         * @param ocr the Occurrence to remove. It will not be deleted,
         * but rather moved to the parent Event's list of manual
         * occurrences.
         * 
         * @throws ArgumentException if the Occurrence is not chained,
         * or if it is chained to a different Period.
         * 
         * @throws KeyNotFoundException if the Occurrence is chained
         * to the Period, but is somehow not found in its list of
         * Occurrences.
         * 
         * @date March 16, 2016
         */
        //TODO: make nextOcr only defined when there is one, and add prevOcr to handle days better
        public void DeChain(Occurrence ocr)
        {
            if (!(ocr.ChainId != null) || ocr.ChainId != Id) throw new ArgumentException("Occurrence not part of this Period chain");
            updateOccurrences();
            m_occurrences.Sort((x, y) => x.StartActual.CompareTo(y.StartActual));
            int ocrPos = m_occurrences.IndexOf(ocr);
            if (ocrPos == -1) throw new KeyNotFoundException("Occurrence not found");
            Occurrence nextOcr = null;
            if (ocrPos < m_occurrences.Count - 1)
            {
                nextOcr = m_occurrences[ocrPos + 1];
            }

            //officially dechain the occurrence
            ocr.ChainId = null;
            Event.All[ParentId].ManualOccurrences.Add(ocr);

            //adjust Period accordingly

           

            //single occurrence, rule is not needed
            if (ocrPos == 0 && nextOcr == null)
            {
                Event.All[ParentId].Rules.Remove(this);
            }

            //first occurrence, just start at the next one
            else if (ocrPos == 0)
            {
                StartTime = nextOcr.StartActual;
            }
            //occurrences before and after; split this Period into two
            else if (ocrPos < m_occurrences.Count - 1)
            {
                Event.All[ParentId].Rules.Add(new Period
                    (PeriodicFrequency, TimeUnit, nextOcr.StartActual, 
                    EndTime, OccurrenceLength, ParentId));
            }
            //not first occurrence, end before this one
            if (ocrPos > 0)
            {
                EndTime = ocr.StartActual;
               
            }
            m_needUpdate = true;
            //updateOccurrences();
            //update?
            Event.All[ParentId].Occurrences();
        }

        /**
         * Gets the list of Occurrences specified by this Period.
         */
        public ReadOnlyCollection<Occurrence> Occurrences()
        {
            
            updateOccurrences();
            return new ReadOnlyCollection<Occurrence>(m_occurrences);
            
        }

        /**
         * Gets or sets the frequency at which the Event will
         * occur. Must be greater than or equal to 1.
         */
        public uint PeriodicFrequency
        {
            get { return m_frequency; }

            set 
            { 
                m_frequency = Math.Max(value, 1);
                m_needUpdate = true;
            }
        }

        /**
         * Gets or sets the time unit used to describe the
         * frequency.
         */
        public Frequency TimeUnit
        {
            get { return m_timeUnit; }

            set  
            { 
                m_timeUnit = value;
                m_needUpdate = true;
            }
        }

       

        /**
         * Gets or sets a DateTime struct specifying the earliest
         * time the Occurrences may occur every day. This is also the time
         * at which the first Occurrence will start.
         */
        public DateTime StartTime
        {
            get { return m_startTime; }

            set 
            { 
                m_startTime = value;
                m_needUpdate = true;
            }
        }


        public void ForceUpdate()
        {
            m_needUpdate = true;
        }


        /**
         * Gets or sets a DateTime struct specifying the latest
         * time the Occurrences may occur every day.
         */
        public DateTime EndTime
        {
            get { return m_endTime; }

            set 
            { 
                m_endTime = value;
                m_needUpdate = true;
            }
        }

        /**
         * Gets or sets a TimeSpan struct specifying the length of 
         * each Occurrence.
         */
        public TimeSpan OccurrenceLength
        {
            get { return m_length; }

            set 
            { 
                m_length = value;
                m_needUpdate = true;
            }
        }

        /**
         * Gets the id of the parent Event to which
         * the occurrence belongs.
         */
        public string ParentId { get { return m_parent; } }


        /**
         * Gets the id of the Period.
         * 
         * The id is written in the form of a string starting
         * with the letter 'p', followed by a 32-bit unsigned
         * integer in hexadecimal (all lowercase). This allows
         * the period to be looked up easily.
         * 
         * Currently, the integer portion of the id is
         * simply equal to the period's number.
         */
        public string Id { get { return m_id; } }

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

        //TODO: document
        public bool Excluded
        {
            get { return m_exclude; }

            set { m_exclude = value; }
        }

        protected string m_id;
        protected static uint num = 1;
        protected uint m_numId;
        private uint m_frequency;
        private Frequency m_timeUnit;
        
        private DateTime m_startTime;
        private DateTime m_endTime;
        private TimeSpan m_length;
        private string m_parent;
        private List<Occurrence> m_occurrences;
        private bool m_needUpdate;
        private bool m_exclude;

       
    }
}
