/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The AlarmSettings class specifies all alarms associated with
 * a specific Occurrence.
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
        * Specifies if the alarm is set to go off before, during,
        * or after the occurrence.
        */
    public enum When
    {
        /**
            * The alarm will go off before the beginning
            * of the occurrence by the specified amount
            * of time.
            */
        Before,
        /**
            * The alarm will go off after the beginning
            * of the occurrence by the specified amount
            * of time, assuming the occurrence has not
            * yet ended.
            */
        During,
        /**
            * The alarm will go off after the end
            * of the occurrence by the specified amount
            * of time.
            */
        After   
    };

    /**
        * Denotes the unit of time in which to specify the
        * time when the alarm will go off.
        */
    public enum Length
    {
        /**
            * The alarm will go off in the specified
            * number of minutes.
            */
        Minutes, 
        /**
            * The alarm will go off in the specified
            * number of hours.
            */
        Hours,   
        /**
            * The alarm will go off in the specified
            * number of days.
            */
        Days,    
        /**
            * The alarm will go off in the specified
            * number of weeks.
            */
        Weeks    
    };

    class AlarmSettings
    {
        

        /**
         * Creates an AlarmSettings object from a parent Occurrence
         * id and a list of Alarms as alarm settings.
         * 
         * @param a_parent the id of the parent Occurrence to which
         * these alarm settings belong.
         * 
         * @param a_alarms a list of alarm settings in the form of
         * Tuples. 
         * 
         * @date March 4, 2016
         */
        public AlarmSettings(string a_parent, List<Alarm> a_alarms)
        {
            Alarms = a_alarms;
            m_parent = a_parent;
        }

        /**
         * Creates an AlarmSettings object from a parent Occurrence
         * id with no alarms.
         * 
         * @param a_parent the id of the parent Occurrence to which
         * these alarm settings belong.
         * 
         * @date March 4, 2016
         */
        public AlarmSettings(string a_parent)
        {
            Alarms = new List<Alarm>();
            m_parent = a_parent;
        }

        //TODO: change alarms from tuples to their own class
        //of When, uint, Length, and bool m_dealtWith.
        //that way, when the alarm has gone off, it won't go
        //off a second time unless explicitly told to do so.
        //It might also make constructors easier.

        /**
         * Updates the AlarmTimes list based on the current Alarms
         * list and the Occurrence's time settings.
         * 
         * @date March 6, 2016
         */
        private void updateAlarms()
        {

            Occurrence parent = Occurrence.All[ParentId];
            m_alarmTimes = new List<DateTime>();
            foreach (Alarm alarm in Alarms){
                DateTime target = new DateTime();
                TimeSpan offset = new TimeSpan();
                //convert Length units into TimeSpan units
                switch(alarm.m_timeunit)
                {
                    case Length.Minutes:
                        offset = new TimeSpan(0, (int)alarm.m_timelength, 0);
                        break;
                    case Length.Hours:
                        offset = new TimeSpan((int)alarm.m_timelength, 0, 0);
                        break;
                    case Length.Days:
                        offset = new TimeSpan((int)alarm.m_timelength, 0, 0, 0);
                        break;
                    case Length.Weeks:
                        offset = new TimeSpan((int)alarm.m_timelength * 7, 0, 0, 0);
                        break;
                }
                //convert When into start or end time
                switch (alarm.m_relativeplace)
                {
                    case When.Before:
                        //should happen **before** start time
                        offset = offset.Negate();
                        goto case When.During;
                    case When.During:
                        target = parent.StartActual;
                        break;
                    case When.After:
                        target = parent.EndActual;
                        break;
                }
                target = target.Add(offset);
                m_alarmTimes.Add(target);
            }
            m_alarmTimes.Sort();
        }
        


        /**
         * Gets or sets the list of alarm settings to use.
         * 
         * The list is made up of Tuples. Each 
         * contains a When parameter, an unsigned integer value, 
         * and a Length parameter. Together, they specify the time 
         * that one alarm goes off, as in the following statement:
         * "The alarm will go off (uint) (Length) (When) the Occurrence".
         * For example, if When = After, uint = 20 and Length = Minutes,
         * the alarm should go off 20 minutes after the Occurrence
         * has ended.
         * 
         */
        public List<Alarm> Alarms{
            get 
            {
                return m_alarms;
            }

            set
            {
                m_alarms = value;
            }

        }

        /**
         * Gets the list of alarm settings to use, in
         * the form of a read-only, sorted list of DateTime
         * structs. 
         */
        public ReadOnlyCollection<DateTime> AlarmTimes
        {
            get
            {
                updateAlarms();
                return new ReadOnlyCollection<DateTime>(m_alarmTimes);
            }
        }

        /**
         * Gets the id of the parent Occurrence to which
         * the alarm settings belong.
         */
        public string ParentId { get { return m_parent; } }


        private List<Alarm> m_alarms;
        private List<DateTime> m_alarmTimes;
        private string m_parent;
    }

    struct Alarm
    {
        public When m_relativeplace;
        public uint m_timelength;
        public Length m_timeunit;

        public Alarm(When a_relativeplace, uint a_timelength, Length a_timeunit)
        {
            m_relativeplace = a_relativeplace;
            m_timelength = a_timelength;
            m_timeunit = a_timeunit;
        }
    }
}
