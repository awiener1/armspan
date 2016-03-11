using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Span
{
    enum Frequency
    {

        Minutes,

        Hours,

        Days,

        Weeks,
        
        Months,

        Years
    }
    class Period
    {

        public Period(uint a_frequency, Frequency a_timeUnit, DateTime a_startDate, DateTime a_endDate, DateTime a_startTime, DateTime a_endTime, TimeSpan a_length, string a_parent)
        {
            PeriodicFrequency = a_frequency;
            TimeUnit = a_timeUnit;
            StartDate = a_startDate;
            StartTime = a_startTime;
            EndDate = a_endDate;
            EndTime = a_endTime;
            OccurrenceLength = a_length;
            m_parent = a_parent;
        }

        public List<Occurrence> Occurrences
        {
            get
            {
                List<Occurrence> outList = new List<Occurrence>();
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
                        outList.Add(newOcc);
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
                return outList;
            }
        }

        public uint PeriodicFrequency
        {
            get { return m_frequency; }

            set { m_frequency = Math.Max(value, 1); }
        }

        public Frequency TimeUnit
        {
            get { return m_timeUnit; }

            set  { m_timeUnit = value; }
        }

        public DateTime StartDate 
        {
            get { return m_startDate; }

            set { m_startDate = value; } 
        }

        public DateTime StartTime
        {
            get { return m_startTime; }

            set { m_startTime = value; }
        }

        public DateTime EndDate
        {
            get { return m_endDate; }

            set { m_endDate = value; }
        }

        public DateTime EndTime
        {
            get { return m_endTime; }

            set { m_endTime = value; }
        }

        public TimeSpan OccurrenceLength
        {
            get { return m_length; }

            set { m_length = value; }
        }

        /**
         * Gets the id of the parent Event to which
         * the occurrence belongs.
         */
        public string ParentId { get { return m_parent; } }

        private uint m_frequency;
        private Frequency m_timeUnit;
        private DateTime m_startDate;
        private DateTime m_endDate;
        private DateTime m_startTime;
        private DateTime m_endTime;
        private TimeSpan m_length;
        private string m_parent;
        //private bool exclude;

       
    }
}
