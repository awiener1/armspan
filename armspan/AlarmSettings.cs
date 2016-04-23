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

    public class AlarmSettings : JSONCapable
    {
        

        /**
         * Creates an AlarmSettings object from a parent Occurrence
         * id and a list of Alarms as alarm settings.
         * 
         * @param a_parent the id of the parent Occurrence to which
         * these alarm settings belong.
         * 
         * @param a_alarms a list of alarm settings in the form of
         * Alarm objects. 
         * 
         * @date March 4, 2016
         */
        public AlarmSettings(string a_parent, List<Alarm> a_alarms)
        {
            Alarms = a_alarms;
            m_parent = a_parent;
        }

        /**
         * Creates an AlarmSettings object from an existing 
         * AlarmSettings object with a new parent.
         * 
         * @param a_settings the existing AlarmSettings object
         * to copy.
         * 
         * @param a_newParent the id of the parent Occurrence to which
         * these alarm settings will now belong.
         * 
         * @date March 4, 2016
         */
        public AlarmSettings(AlarmSettings a_settings, string a_newParent)
        {
            Alarms = a_settings.Alarms;
            m_parent = a_newParent;
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

        /**
         * Generates an AlarmSettings object from the
         * specified JSON-serialized AlarmSettings
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
        public static AlarmSettings FromJSON(string json)
        {
            Dictionary<string, object> jsd = JSONDictionary(AlarmSettings.FromString(json));
            List<Alarm> alarms = jss.ConvertToType<List<Alarm>>(jsd["Alarms"]);
            string parentid = (string)jsd["ParentId"];
            AlarmSettings loaded = new AlarmSettings(parentid, alarms);
            
            return loaded;
        }

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
                //if it's already gone off, it shouldn't again.
                if (alarm.m_dealtWith)
                {
                    continue;
                }
                /*COMMENT OUT THE IF STATEMENT BELOW TO CHECK IF ALARMS ARE BEING DEALT WITH PROPERLY.*/
                //if the occurrence has ended, only after-alarms should go off.
                if (alarm.m_relativePlace != When.After && parent.EndActual < TimeKeeper.Now.ToLocalTime())
                {
                    continue;
                }


                DateTime target = new DateTime();
                TimeSpan offset = new TimeSpan();
                //convert Length units into TimeSpan units

                //TODO: change this TimeSpan code to work with DST
                //(the TimeSpan class doesn't pay attention to
                //DST, only the DateTime class does. Likely removing
                //the offset object altogether will help.
                switch(alarm.m_timeUnit)
                {
                    case Length.Minutes:
                        offset = new TimeSpan(0, (int)alarm.m_timeLength, 0);
                        break;
                    case Length.Hours:
                        offset = new TimeSpan((int)alarm.m_timeLength, 0, 0);
                        break;
                    case Length.Days:
                        offset = new TimeSpan((int)alarm.m_timeLength, 0, 0, 0);
                        break;
                    case Length.Weeks:
                        offset = new TimeSpan((int)alarm.m_timeLength * 7, 0, 0, 0);
                        break;
                }
                //convert When into start or end time
                switch (alarm.m_relativePlace)
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
         * Gets the absolute time that the specified Alarm will occur.
         * 
         * @param single the Alarm struct specified in relative time.
         * 
         * @return a DateTime struct specified in absolute time.
         * 
         * @date April 2, 2016
         */
        //TODO: change updateAlarms() to use this function instead
        public DateTime SingleAlarmTime(Alarm single){
            Occurrence parent = Occurrence.All[ParentId];
            DateTime target = new DateTime();
            TimeSpan offset = new TimeSpan();
            //convert Length units into TimeSpan units

            //TODO: change this TimeSpan code to work with DST
            //(the TimeSpan class doesn't pay attention to
            //DST, only the DateTime class does. Likely removing
            //the offset object altogether will help.
            switch (single.m_timeUnit)
            {
                case Length.Minutes:
                    offset = new TimeSpan(0, (int)single.m_timeLength, 0);
                    break;
                case Length.Hours:
                    offset = new TimeSpan((int)single.m_timeLength, 0, 0);
                    break;
                case Length.Days:
                    offset = new TimeSpan((int)single.m_timeLength, 0, 0, 0);
                    break;
                case Length.Weeks:
                    offset = new TimeSpan((int)single.m_timeLength * 7, 0, 0, 0);
                    break;
            }
            //convert When into start or end time
            switch (single.m_relativePlace)
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
            return target;
        }

        /**
         * Gets the list of alarm settings to use, in
         * the form of a read-only, sorted list of DateTime
         * structs. 
         * 
         * @date March 6, 2016
         */
        public ReadOnlyCollection<DateTime> AlarmTimes()
        {
         
            updateAlarms();
            return new ReadOnlyCollection<DateTime>(m_alarmTimes);
            
        }

        /**
         * Gets or sets the id of the parent Occurrence to which
         * the alarm settings currently apply.
         */
        public string ParentId { 
            get { return m_parent; }
            set { m_parent = value; }
        }


        private List<Alarm> m_alarms;
        private List<DateTime> m_alarmTimes;
        private string m_parent;
    }

    /**
     * Represents one alarm that is displayed to the user in
     * some fashion.
     * 
     * Each Alarm struct contains a When parameter (m_relativePlace),
     * an unsigned integer value (m_timeLength), and a Length parameter
     * (m_timeUnit). Together, they specify the time that one alarm goes
     * off, as in the following statement:
     * "The alarm will go off (uint) (Length) (When) the Occurrence".
     * For example, if When = After, uint = 20 and Length = Minutes,
     * the alarm should go off 20 minutes after the Occurrence
     * has ended.
     * 
     */
    public struct Alarm
    {
        /**
         * Specifies if the alarm should go off before, during, or
         * after the Occurrence.
         */
        public When m_relativePlace;

        /**
         * Specifies the quantity of time units before/during/after 
         * the Occurrence when the alarm goes off.
         */
        public uint m_timeLength;

        /**
         * Specifies the time unit for m_timeLength, which can be
         * Minutes, Hours, Days or Weeks. This program is not
         * designed to track smaller units of time than minutes.
         */
        public Length m_timeUnit;

        /**
         * In general, denotes if the alarm has gone off yet. False
         * by default.
         * 
         * More precisely, denotes if the alarm should be
         * ignored in the future. For example, if the user
         * sees an alarm five minutes before an event and
         * postpones the event, the same alarm should
         * go off again, five minutes before the event's rescheduled
         * time. Similarly, the alarm should go off if the
         * program has been closed and is reopened far after
         * the alarm is due. However, the alarm should not go off
         * if it previously went off, the user properly dealt with it
         * and closed the program, and then reopened the program
         * later.
         */
        public bool m_dealtWith;

        /**
         * Creates an Alarm at the specified relative time.
         * 
         * @param a_relativePlace specifies if the alarm should 
         * go off before, during, or after the Occurrence.
         * 
         * @param a_timeLength specifies the quantity of time 
         * units before/during/after the Occurrence when the 
         * alarm goes off.
         * 
         * @param a_timeUnit specifies the time unit to use.
         * 
         * @param a_dealtWith denotes if the alarm has gone
         * off yet. False by default.
         * 
         * @date March 9, 2016
         */
        public Alarm(When a_relativePlace, uint a_timeLength, Length a_timeUnit, bool a_dealtWith = false)
        {
            m_relativePlace = a_relativePlace;
            m_timeLength = a_timeLength;
            m_timeUnit = a_timeUnit;
            m_dealtWith = a_dealtWith;
        }
    }
}
