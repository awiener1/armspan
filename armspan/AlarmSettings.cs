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
            m_hasSavedTime = "";
            
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
            HasNextAlarm = a_settings.HasNextAlarm;
            if (HasNextAlarm.Length > 0)
            {
                NextAlarmTimeToSave = a_settings.NextAlarmTimeToSave;
            }
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
            m_hasSavedTime = "";
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
            //json to object types
            Dictionary<string, object> jsd = JSONDictionary(AlarmSettings.FromString(json));
            List<Alarm> alarms = jss.ConvertToType<List<Alarm>>(jsd["Alarms"]);
            string parentid = (string)jsd["ParentId"];
            //loose objects to AlarmSettings
            AlarmSettings loaded = new AlarmSettings(parentid, alarms);
            bool hsexists = jsd.ContainsKey("HasNextAlarm");
            if (hsexists)
            {
                string hasSaved = (string)jsd["HasNextAlarm"];
                loaded.m_hasSavedTime = hasSaved;
            }
            else
            {
                loaded.m_hasSavedTime = "";
            }
            if (loaded.m_hasSavedTime.Length > 0)
            {
                DateTime serializeNext = ((DateTime)jsd["NextAlarmTimeToSave"]);
                loaded.NextAlarmTimeToSave = serializeNext;
            }
            
            return loaded;
        }

        /**
         * Updates the ParentId of the specified AlarmSettings.
         * 
         * Uses the NextAlarmToSave property to find
         * the correct Occurrence, which
         * may not be the saved ParentId. Once this has
         * been found, the function does nothing.
         * 
         * @date May 7, 2016
         */
        private void updateParent()
        {
            if (m_hasSavedTime.Length > 0)
            {
                Event gparent = Event.All[m_hasSavedTime];
                DateTime timeadjust = NextAlarmTimeToSave.ToLocalTime();
                //find occurrence owned by this event with this time as one of its alarms
                Occurrence newparent = Occurrence.All.Values.FirstOrDefault(x => x.StartActual.Equals(timeadjust));
                ParentId = newparent.Id;
                m_hasSavedTime = "";
            }
        }

        /**
         * Updates the AlarmTimes list based on the current Alarms
         * list and the Occurrence's time settings.
         * 
         * @date March 6, 2016
         */
        private void updateAlarms()
        {
            updateParent();
            Occurrence parent = Occurrence.All[ParentId];
            m_alarmTimes = new List<DateTime>();
            foreach (Alarm alarm in Alarms){
                //if it's already gone off, it shouldn't again.
                if (alarm.m_dealtWith)
                {
                    continue;
                }
                //if the occurrence has ended, only after-alarms should go off.
                if (alarm.m_relativePlace != When.After && parent.EndActual < TimeKeeper.Now.ToLocalTime())
                {
                    continue;
                }
                
                DateTime target = SingleAlarmTime(alarm);
                m_alarmTimes.Add(target);
            }
            m_alarmTimes.Sort();
        }
        


        /**
         * Gets or sets the list of alarm settings to use.
         */
        public List<Alarm> Alarms
        {
            get { return m_alarms; }
            set { m_alarms = value; }
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
        public DateTime SingleAlarmTime(Alarm single){
            updateParent();
            Occurrence parent = Occurrence.All[ParentId];
            DateTime target = new DateTime();
            TimeSpan offset = new TimeSpan();

            //convert Length units into TimeSpan units
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
                    //should happen **before** start time, so go backwards in time
                    offset = offset.Negate();
                    goto case When.During;
                case When.During:
                    target = parent.StartActual;
                    break;
                case When.After:
                    target = parent.EndActual;
                    break;
            }
            //convert relative time to absolute time
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
         * 
         * Note: this is the parent Occurrence, not the parent
         * Event.
         */
        public string ParentId { 
            get { return m_parent; }
            set { m_parent = value; }
        }

        /**
         * Gets or sets the time, in universal time,
         * when the Occurrence specified in ParentId
         * is set to start. 
         * 
         * This value is only used
         * during serialization and deserialization.
         */
        public DateTime NextAlarmTimeToSave
        {
            get { return m_serializedNext; }
            set { m_serializedNext = value; }
        }

        /**
         * Gets or sets the id of the Event
         * to which these alarm settings apply.
         * 
         * This value is only used during
         * serialization and deserialization. If
         * it is set to an empty string, the
         * NextAlarmTimeToSave property should be
         * ignored.
         */
        public string HasNextAlarm
        {
            get { return m_hasSavedTime; }
            set { m_hasSavedTime = value; }
        }

        /**
         * The list of Alarm structs. See also Alarms.
         */
        private List<Alarm> m_alarms;
        /**
         * The list of alarm times in DateTime structs. See also AlarmTimes().
         */
        private List<DateTime> m_alarmTimes;
        /**
         * The id of the parent Occurrence. See also ParentId.
         */
        private string m_parent;
        /**
         * The DateTime when the parent Occurrence will start. See also NextAlarmTimeToSave.
         */
        private DateTime m_serializedNext;
        /**
         * The id of the parent Event. See also HasNextAlarm.
         */
        private string m_hasSavedTime; 
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
