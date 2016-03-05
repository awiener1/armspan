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

namespace Span
{
    class AlarmSettings
    {
        /**
         * Specifies if the alarm is set to go off before, during,
         * or after the occurrence.
         */
        public enum When
        {
            Before, /**
                     * The alarm will go off before the beginning
                     * of the occurrence by the specified amount
                     * of time.
                     */
            During, /**
                     * The alarm will go off after the beginning
                     * of the occurrence by the specified amount
                     * of time, assuming the occurrence has not
                     * yet ended.
                     */
            After   /**
                     * The alarm will go off after the end
                     * of the occurrence by the specified amount
                     * of time.
                     */
        };

        /**
         * Denotes the unit of time in which to specify the
         * time when the alarm will go off.
         */
        public enum Length
        {
            Minutes, /**
                      * The alarm will go off in the specified
                      * number of minutes.
                      */
            Hours,   /**
                      * The alarm will go off in the specified
                      * number of hours.
                      */
            Days,    /**
                      * The alarm will go off in the specified
                      * number of days.
                      */
            Weeks    /**
                      * The alarm will go off in the specified
                      * number of weeks.
                      */
        };

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
        public AlarmSettings(string a_parent, List<Tuple<When, uint, Length>> a_alarms)
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
            Alarms = new List<Tuple<When, uint, Length>>();
            m_parent = a_parent;
        }

        //private void updateAlarms() //takes Alarms and turns it into AlarmTimes by checking the parent
        //TODO: set up a static list of occurrences


        /**
         * The list of alarm settings to use.
         * 
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

        /**
         * The id of the parent Occurrence to which
         * the alarm settings belong.
         */
        public string ParentId { get { return m_parent; } }


        private List<Tuple<When, uint, Length>> m_alarms;
        private List<DateTime> m_alarmTimes;
        private string m_parent;
    }
}
