using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassTests
{
    class AlarmSettings
    {
        public enum When
        {
            Before,
            During,
            After
        };
        public enum Length
        {
            Minutes,
            Hours,
            Days,
            Weeks
        };

        public AlarmSettings(string a_parent, List<Tuple<When, uint, Length>> a_alarms)
        {
            Alarms = a_alarms;
            m_parent = a_parent;
        }

        public AlarmSettings(string a_parent)
        {
            Alarms = new List<Tuple<When, uint, Length>>();
            m_parent = a_parent;
        }

        //private void updateAlarms() //takes Alarms and turns it into AlarmTimes by checking the parent
        //TODO: set up a static list of occurrences

        public List<Tuple<When, uint, Length>> Alarms{
            get 
            {
                return m_alarms;
            }

            set
            {
                m_alarms = value;
            }

        }
        public string ParentId { get { return m_parent; } }


        private List<Tuple<When, uint, Length>> m_alarms;
        private List<DateTime> m_alarmTimes;
        private string m_parent;
    }
}
