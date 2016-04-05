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
            m_begin = m_now.AddDays(-1);
            m_end = m_now.AddDays(1);
            //get all occurrences
            IEnumerable<KeyValuePair<string, Occurrence>> partial = Occurrence.All.Where
                (x => x.Value.StartActual >= m_begin && x.Value.StartActual <= m_end ||
                x.Value.EndActual >= m_begin && x.Value.EndActual <= m_end);
            ////UNCOMMENT THE FOLLOWING LINE AND CHANGE THE DATA MEMBER NAME
            ////TO GET ALL OCCURRENCES IN A 48-HOUR RADIUS.
            //m_occurrences = partial.ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
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
            partial = Occurrence.All.Where
                (x => x.Value.StartActual <= m_now && x.Value.EndActual > m_now);
            Dictionary<string, Occurrence> tempp = partial.ToDictionary(x => x.Key, x => x.Value);
            m_current = partial.ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
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
                m_now = new DateTime(precise.Year, precise.Month, precise.Day, precise.Hour, precise.Minute, 0);
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

        private static DateTime m_begin;
        private static DateTime m_end;
        private static DateTime m_now;
        private static Dictionary<DateTime, Occurrence> m_alarms = new Dictionary<DateTime, Occurrence>();
        private static List<string> m_occurrences = new List<string>();
        private static List<string> m_current = new List<string>();
    }

    
}
