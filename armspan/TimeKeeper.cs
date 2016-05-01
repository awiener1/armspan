/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The Timekeeper class is a static class
 * that keeps track of all occurrences and
 * all current alarms, based on the current time.
 * 
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Span
{
    static class TimeKeeper
    {

        /**
         * Updates the current time, list of occurrences within
         * 48 hours, list of occurrences currently happening,
         * and pending alarms.
         * 
         * @date March 28, 2016
         */
        public static void Update()
        {
            //update current time and 48-hour radius
            DateTime temp = Now;
            if (ViewingNow)
            {
                m_begin = m_now.AddDays(-1.0);
                m_end = m_now.AddDays(1.0);
            }
            //get all occurrences
            IEnumerable<KeyValuePair<string, Occurrence>> partial = Occurrence.All.Where
                (x => x.Value.StartActual >= m_begin.ToLocalTime() && x.Value.StartActual <= m_end.ToLocalTime() ||
                x.Value.EndActual >= m_begin.ToLocalTime() && x.Value.EndActual <= m_end.ToLocalTime());
            ////UNCOMMENT THE FOLLOWING LINE AND CHANGE THE DATA MEMBER NAME
            ////TO GET ALL OCCURRENCES IN A 48-HOUR RADIUS.
            m_inDate = partial.ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
            ////TODO:POSSIBLY CHANGE THIS BACK TO 48 HOURS
            ////AND THEN USE Occurrence.All for all of them
            ////instead
            m_occurrences = Occurrence.All.Keys.ToList();
            //get all alarms for the occurrences
            m_alarms = new Dictionary<DateTime, Occurrence>();
            foreach (string occurrence in m_occurrences)
            {
                Occurrence val = Occurrence.All[occurrence];
                Event e = val.Parent();
                if (e.Alarms.ParentId == occurrence)
                {
                    foreach (DateTime alarmtime in val.AlarmTimes())
                    {
                        m_alarms.Add(alarmtime, val);
                    }
                }
            }
            //get all occurrences happening now
            OccurrenceStatus[] noInclude = { OccurrenceStatus.Canceled, OccurrenceStatus.Deleted, OccurrenceStatus.Excluded };
            partial = Occurrence.All.Where
                (x => x.Value.StartActual <= m_now.ToLocalTime() && x.Value.EndActual > m_now.ToLocalTime());
            Dictionary<string, Occurrence> tempp = partial.ToDictionary(x => x.Key, x => x.Value);
            m_current = partial.Select(x => x.Key).ToList();

            int removed = m_current.RemoveAll(x => noInclude.Contains(Occurrence.All[x].Status) || !Occurrence.All[x].Parent().Exists);
        }

        /**
         * Gets the current time, rounded to minutes, and in
         * UTC time.
         */
        public static DateTime Now
        {
            get
            {
                DateTime precise = DateTime.Now.ToUniversalTime();
                m_now = new DateTime(precise.Year, precise.Month, precise.Day, precise.Hour, precise.Minute, 0, 0);
                return m_now;
            }
        }

        /**
         * Gets the list of alarms currently pending.
         */
        public static Dictionary<DateTime, Occurrence> Alarms 
        {
            get
            {
                return m_alarms;
            }
        }

        //TODO: document
        public static ReadOnlyCollection<string> InDate
        {
            get
            {
                return new ReadOnlyCollection<string>(m_inDate);
            }
        }

        //TODO: document
        public static ReadOnlyCollection<string> Current
        {
            get
            {
                return new ReadOnlyCollection<string>(m_current);
            }
        }

        public static DateTime Begin
        {
            get
            {
                return m_begin;
            }
            set
            {
                m_begin = value;
            }
        }

        public static DateTime End
        {
            get
            {
                return m_end;
            }
            set
            {
                m_end = value;
            }
        }

        public static int ZoomFactor
        {
            get 
            {
                return m_zoomFactor;
            }

            set
            {
                m_zoomFactor = Math.Min(60, Math.Max(1, value));
            }

        }

        public static bool ViewingNow
        {
            get
            {
                return m_viewingNow;
            }
            set
            {
                m_viewingNow = value;
            }
        }

        private static DateTime m_begin;
        private static DateTime m_end;
        private static DateTime m_now;
        private static Dictionary<DateTime, Occurrence> m_alarms = new Dictionary<DateTime, Occurrence>();
        private static List<string> m_occurrences = new List<string>();
        private static List<string> m_current = new List<string>();
        private static List<string> m_inDate = new List<string>();
        private static int m_zoomFactor = 1;
        private static bool m_viewingNow = true;
    }


    
}
