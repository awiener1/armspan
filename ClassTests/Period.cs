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
            PeriodicFrequency = a_frequency;
            TimeUnit = a_timeUnit;
            StartDate = a_startDate;
            StartTime = a_startTime;
            EndDate = a_endDate;
            EndTime = a_endTime;
            OccurrenceLength = a_length;
            m_parent = a_parent;
        }

        /**
         * Gets the list of Occurrences specified by this Period.
         */

        //TODO: make this readonly
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

        /**
         * Gets or sets the frequency at which the Event will
         * occur. Must be greater than or equal to 1.
         */
        public uint PeriodicFrequency
        {
            get { return m_frequency; }

            set { m_frequency = Math.Max(value, 1); }
        }

        /**
         * Gets or sets the time unit used to describe the
         * frequency.
         */
        public Frequency TimeUnit
        {
            get { return m_timeUnit; }

            set  { m_timeUnit = value; }
        }

        /**
         * Gets or sets a DateTime struct specifying the day on
         * which the Occurrences will start.
         */
        public DateTime StartDate 
        {
            get { return m_startDate; }

            set { m_startDate = value; } 
        }

        /**
         * Gets or sets a DateTime struct specifying the earliest
         * time the Occurrences may occur every day. This is also the time
         * at which the first Occurrence will start.
         */
        public DateTime StartTime
        {
            get { return m_startTime; }

            set { m_startTime = value; }
        }

        /**
         * Gets or sets a DateTime struct specifying the day on
         * which the Occurrences will end, inclusively (that is, the final day
         * on which they will occur).
         */
        public DateTime EndDate
        {
            get { return m_endDate; }

            set { m_endDate = value; }
        }

        /**
         * Gets or sets a DateTime struct specifying the latest
         * time the Occurrences may occur every day.
         */
        public DateTime EndTime
        {
            get { return m_endTime; }

            set { m_endTime = value; }
        }

        /**
         * Gets or sets a TimeSpan struct specifying the length of 
         * each Occurrence.
         */
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
