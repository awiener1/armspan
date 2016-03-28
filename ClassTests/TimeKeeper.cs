using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Span
{
    static class TimeKeeper
    {

        public static void Update()
        {
            m_now = Now;
            m_begin = m_now.AddDays(-1);
            m_end = m_now.AddDays(1);
            IEnumerable<KeyValuePair<string, Occurrence>> partial = Occurrence.All.Where
                (x => x.Value.StartActual >= m_begin && x.Value.StartActual <= m_end ||
                x.Value.EndActual >= m_begin && x.Value.EndActual <= m_end);
            m_occurrences = partial.ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
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
        }

        public static DateTime Now
        {
            get
            {
                DateTime precise = DateTime.Now;
                m_now = new DateTime(precise.Year, precise.Month, precise.Day, precise.Hour, precise.Minute, 0);
                return m_now;
            }
        }

        private static DateTime m_begin;
        private static DateTime m_end;
        private static DateTime m_now;
        private static Dictionary<DateTime, Occurrence> m_alarms = new Dictionary<DateTime, Occurrence>();
        private static List<string> m_occurrences = new List<string>();
        
    }

    
}
