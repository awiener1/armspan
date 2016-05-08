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
         * 
         * The Alarm structs themselves are not very useful, so
         * the dictionary contains each alarm's time as a key and
         * parent Occurrence as a value. The Alarm struct can be
         * retrieved using both of these components if necessary.
         */
        public static Dictionary<DateTime, Occurrence> Alarms { get { return m_alarms; } }

        /**
         * Gets a read only collection of strings that contains the ids of Occurrences found between Begin and End.
         */
        public static ReadOnlyCollection<string> InDate { get { return new ReadOnlyCollection<string>(m_inDate); } }

        /**
         * Gets a read only collection of strings that contains the ids of Occurrences occurring now.
         */
        public static ReadOnlyCollection<string> Current { get { return new ReadOnlyCollection<string>(m_current); } }

        /**
         * Gets or sets the beginning of the time when the Occurrences being displayed can occur.
         */
        public static DateTime Begin
        {
            get { return m_begin; }
            set { m_begin = value; }
        }

        /**
         * Gets or sets the end of the time when the Occurrences being displayed can occur.
         */
        public static DateTime End
        {
            get { return m_end; }
            set { m_end = value; }
        }

        /**
         * Gets or sets the fraction at which to zoom in on the Occurrences being displayed.
         * 
         * A value of 1 will not zoom in at all, while a value of 2 will
         * zoom everything in to be twice as large, etc. The accepted range is 1 through 60.
         */
        public static int ZoomFactor
        {
            get { return m_zoomFactor; }
            set
            {
                m_zoomFactor = Math.Min(60, Math.Max(1, value));
            }
        }

        /**
         * Denotes if the displayed range of time is centered at Now.
         * 
         * If false, Begin and End can be changed to any value. If true,
         * they remain 24 hours before and after Now, while Now advances in time.
         */
        public static bool ViewingNow
        {
            get { return m_viewingNow; }
            set { m_viewingNow = value; }
        }

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
            //get all occurrences in date range
            IEnumerable<KeyValuePair<string, Occurrence>> partial = Occurrence.All.Where
                (x => x.Value.StartActual >= m_begin.ToLocalTime() && x.Value.StartActual <= m_end.ToLocalTime() ||
                x.Value.EndActual >= m_begin.ToLocalTime() && x.Value.EndActual <= m_end.ToLocalTime());
            m_inDate = partial.ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
            //get all occurrences
            m_occurrences = Occurrence.All.Keys.ToList();
            //get all alarms for the occurrences
            m_alarms = new Dictionary<DateTime, Occurrence>();
            foreach (string occurrence in m_occurrences)
            {
                Occurrence val = Occurrence.All[occurrence];
                Event e = val.Parent();
                //include NextAlarmTimeToSave alarms or the parent id won't be set properly
                if (e.Alarms.ParentId == occurrence || (e.Alarms.HasNextAlarm.Length > 0 && e.Alarms.NextAlarmTimeToSave.ToLocalTime().Equals(val.StartActual)))
                {
                    foreach (DateTime alarmtime in val.AlarmTimes())
                    {
                        m_alarms[alarmtime] = val;
                    }
                }
            }
            //get all occurrences happening now, but not the canceled/deleted/excluded ones
            OccurrenceStatus[] noInclude = { OccurrenceStatus.Canceled, OccurrenceStatus.Deleted, OccurrenceStatus.Excluded };
            partial = Occurrence.All.Where
                (x => x.Value.StartActual <= m_now.ToLocalTime() && x.Value.EndActual > m_now.ToLocalTime());
            Dictionary<string, Occurrence> tempp = partial.ToDictionary(x => x.Key, x => x.Value);
            m_current = partial.Select(x => x.Key).ToList();
            m_current.RemoveAll(x => noInclude.Contains(Occurrence.All[x].Status) || !Occurrence.All[x].Parent().Exists);
        }

        /**
         * The start of the displayed time. See also Begin.
         */
        private static DateTime m_begin;
        /**
         * The end of the displayed time. See also End.
         */
        private static DateTime m_end;
        /**
         * The current time rounded to minutes. See also Now.
         */
        private static DateTime m_now;
        /**
         * A list of all pending "alarms", with their time as keys and their parent Occurrences as values.
         */
        private static Dictionary<DateTime, Occurrence> m_alarms = new Dictionary<DateTime, Occurrence>();
        /**
         * A list of all Occurrence ids.
         */
        private static List<string> m_occurrences = new List<string>();
        /**
         * A list of the ids of Occurrences happening now. See also Current.
         */
        private static List<string> m_current = new List<string>();
        /**
         * A list of the ids of Occurrences within the displayed time. See also InDate.
         */
        private static List<string> m_inDate = new List<string>();
        /**
         * The factor at which to zoom in on the displayed time. See also ZoomFactor.
         */
        private static int m_zoomFactor = 1;
        /**
         * Denotes if the displayed time centers on Now. See also ViewingNow.
         */
        private static bool m_viewingNow = true;
    }  
}
