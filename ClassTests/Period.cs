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

namespace Span
{

    /**
     * Denotes the unit of time in which to specify the
     * frequency of the Period.
     */
    enum Frequency
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

   
    class Period
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
        public Period(uint a_frequency, Frequency a_timeUnit, DateTime a_startDate, DateTime a_endDate, DateTime a_startTime, DateTime a_endTime, TimeSpan a_length, string a_parent)
        {
            m_numId = num++;
            m_id = "p" + m_numId.ToString("x8");
            PeriodicFrequency = a_frequency;
            TimeUnit = a_timeUnit;
            StartDate = a_startDate;
            StartTime = a_startTime;
            EndDate = a_endDate;
            EndTime = a_endTime;
            OccurrenceLength = a_length;
            m_parent = a_parent;
            m_needUpdate = true;
            m_occurrences = null;
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
                    if (ocr.IsChained)
                    {
                        ocr.Status = OccurrenceStatus.Deleted;
                    }
                }

            }
            m_occurrences = new List<Occurrence>();
            //set up start/end dates
            DateTime eachStartTime = StartDate.Date.Add(StartTime.TimeOfDay);
            DateTime finalEndTime = EndDate.Date.Add(EndTime.TimeOfDay);
            while (eachStartTime < finalEndTime)
            {
                DateTime eachEndTime = eachStartTime.Add(OccurrenceLength);
                //check for overlap with non-working time
                if (eachStartTime.TimeOfDay < EndTime.TimeOfDay
                    && eachStartTime.TimeOfDay >= StartTime.TimeOfDay
                    && eachEndTime.TimeOfDay <= EndTime.TimeOfDay
                    && eachEndTime.TimeOfDay > StartTime.TimeOfDay)
                {
                    Occurrence newOcc = new Occurrence(false, eachStartTime, eachStartTime.Add(OccurrenceLength), m_parent);
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
        public void DeChain(Occurrence ocr)
        {
            if (!(ocr.ChainId != null) || ocr.ChainId != Id) throw new ArgumentException("Occurrence not part of this Period chain");
            updateOccurrences();
            m_occurrences.Sort();
            int ocrPos = m_occurrences.IndexOf(ocr);
            if (ocrPos == -1) throw new KeyNotFoundException("Occurrence not found");
            
            Occurrence nextOcr = m_occurrences[ocrPos + 1];

            //officially dechain the occurrence
            ocr.ChainId = null;
            Event.All[ParentId].ManualOccurrences.Add(ocr);

            //adjust Period accordingly

            //TODO: make this work for smaller units of time between occurrences than days.
            //seriously, it doesn't retain some occurrences if they happen multiple times per day,
            //NOR does it retain the offset of time that continues between night and morning.
            //so not only do you lose some occurrences, but some also move earlier or later!

            //TODO: make this work for a single occurrence too.

            //first occurrence, just start at the next one
            if (ocrPos == 0)
            {
                StartDate = nextOcr.StartActual.Date;
            }
            //occurrences before and after; split this Period into two
            else if (ocrPos < m_occurrences.Count - 1)
            {
                Event.All[ParentId].Rules.Add(new Period
                    (PeriodicFrequency, TimeUnit, nextOcr.StartActual.Date, 
                    EndDate, StartTime, EndTime, OccurrenceLength, ParentId));
            }
            //not first occurrence, end before this one
            if (ocrPos > 0)
            {
                EndDate = ocr.EndActual.Date.AddDays(-1);
            }
            
        }

        /**
         * Gets the list of Occurrences specified by this Period.
         */
        public ReadOnlyCollection<Occurrence> Occurrences
        {
            get
            {
                updateOccurrences();
                return new ReadOnlyCollection<Occurrence>(m_occurrences);
            }
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
         * Gets or sets a DateTime struct specifying the day on
         * which the Occurrences will start.
         */
        public DateTime StartDate 
        {
            get { return m_startDate; }

            set 
            { 
                m_startDate = value;
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

        /**
         * Gets or sets a DateTime struct specifying the day on
         * which the Occurrences will end, inclusively (that is, the final day
         * on which they will occur).
         */
        public DateTime EndDate
        {
            get { return m_endDate; }

            set 
            { 
                m_endDate = value;
                m_needUpdate = true;
            }
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

        protected string m_id;
        protected static uint num = 1;
        protected uint m_numId;
        private uint m_frequency;
        private Frequency m_timeUnit;
        private DateTime m_startDate;
        private DateTime m_endDate;
        private DateTime m_startTime;
        private DateTime m_endTime;
        private TimeSpan m_length;
        private string m_parent;
        private List<Occurrence> m_occurrences;
        private bool m_needUpdate;
        //private bool exclude;

       
    }
}
