﻿/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The Occurrence class defines one single occurrence
 * of a (potentially reoccurring) Event.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace Span
{   
    public class Occurrence : JSONCapable
    {
       
        /**
         * Gets the id of the occurrence.
         * 
         * The id is written in the form of a string starting
         * with the letter 'o', followed by a 32-bit unsigned
         * integer in hexadecimal (all lowercase). This allows
         * the occurrence to be looked up easily.
         * 
         * Currently, the integer portion of the id is
         * simply equal to the occurrence's number.
         */
        public string Id { get { return m_id; } }

        /**
         * Specifies if the Occurrence is a task.
         * 
         * If false, the Occurrence is an appointment.
         * This should only be true if the Occurrence is
         * also a TaskOccurrence object.
         */
        public bool IsTask { get { return m_isTask; } }

        /**
         * Gets or sets the time at which the Occurrence
         * has actually started.
         */
        public DateTime StartActual 
        {
            get { return m_actualStart; }
            set { m_actualStart = value; }
        }

        /**
         * Gets or sets the time at which the Occurrence
         * has actually ended.
         */
        public DateTime EndActual
        {
            get { return m_actualEnd; }
            set { m_actualEnd = value; }
        }

        /**
         * Gets or sets the time at which the Occurrence
         * is intended to start.
         */
        public DateTime StartIntended
        {
            get { return m_createStart; }
            set { m_createStart = value; }
        }

        /**
         * Gets or sets the time at which the Occurrence
         * is intended to end.
         */
        public DateTime EndIntended
        {
            get { return m_createEnd; }
            set { m_createEnd = value; }
        }

        /**
         * Gets or sets the status of the Occurrence
         * as an OccurrenceStatus value.
         */
        public OccurrenceStatus Status
        {
            get { return m_status; }
            set { m_status = value; }
        }       
        
        /**
         * Gets the id of the parent Event to which
         * the occurrence belongs.
         */
        public string ParentId { get { return m_parent; } }

        /**
         * Gets a Dictionary containing all Occurrences by Id.
         */
        public static Dictionary<string, Occurrence> All { get { return all; } }

        /**
         * Gets a written, intelligible description of the Occurrence as a string.
         */
        [ScriptIgnore]
        public string Describe
        {
            get
            {
                string outputter = "Once, FROM: " + StartActual.ToShortDateString() + " at " + StartActual.ToShortTimeString()
                    + " TO: " + EndActual.ToShortDateString() + " at " + EndActual.ToShortTimeString();

                return outputter;
            }
        }

        /**
         * Gets or sets the id of the Period to which
         * this Occurrence is chained, or null if it
         * is not chained to anything.
         */
        public string ChainId
        {
            get { return m_chainId; }
            set { m_chainId = value; }
        }

        /**
         * Gets the number of the occurrence.
         * 
         * Note: please use this property sparingly,
         * as the Id property is better-suited to
         * keeping track of the object. This property
         * exists in order to allow proper serialization
         * of the object.
         */
        public uint Number { get { return m_numId; } }

        /**
         * Denotes if the program should be run in debug mode.
         * 
         * This bool is found in the Occurrence class because it
         * would be present whether the GUI was implemented or not.
         * However, it is presently only used in the GUI.
         */
        public static bool DebugMode { get { return m_debug; } }

        /**
         * Creates a new Occurrence from the specified times and parent id.
         * 
         * @param a_isTask specifies if the Occurrence is a task.
         * However, if it is a task, please use a TaskOccurrence object
         * instead.
         * 
         * @param a_createStart a DateTime struct specifying the starting
         * time of the Occurrence.
         * 
         * @param a_createEnd a DateTime struct specifying the ending time
         * of the Occurrence.
         * 
         * @param a_parent the id of the parent Event to which this
         * Occurrence belongs.
         * 
         * @date March 4, 2016
         */
        public Occurrence(bool a_isTask, DateTime a_createStart, DateTime a_createEnd, string a_parent)
        {
            m_numId = num++;
            m_id = "o" + m_numId.ToString("x8");
            m_isTask = a_isTask;
            StartIntended = a_createStart;
            EndIntended = a_createEnd;
            StartActual = StartIntended;
            EndActual = EndIntended;
            Status = (DateTime.Now.CompareTo(StartActual) > 0) ? OccurrenceStatus.On_Time : OccurrenceStatus.Future;
            m_parent = a_parent;
            All.Add(m_id, this);
        }

        /**
         * Creates a new Occurrence object without
         * any initialized data.
         * 
         * Please only use this constructor for
         * deserialization.
         * 
         * @date March 17, 2016
         */
        protected Occurrence(){}

        /**
         * Generates an Occurrence object from the
         * specified JSON-serialized Occurrence
         * string.
         * 
         * @param json the serialized string
         * representing the object.
         * 
         * @return the object, properly
         * deserialized and initialized.
         * 
         * @date March 17, 2016
         */
        public static Occurrence FromJSON(string json)
        {
            //json to object types
            Dictionary<string, object> jsd = JSONDictionary(Occurrence.FromString(json));
            OccurrenceStatus status = (OccurrenceStatus)jsd["Status"];
            //no deleted objects
            if (status == OccurrenceStatus.Deleted) 
                return null; 
            string id = (string)jsd["Id"];
            bool istask = (bool)jsd["IsTask"];
            DateTime startactual = ((DateTime)jsd["StartActual"]).ToLocalTime();
            DateTime endactual = ((DateTime)jsd["EndActual"]).ToLocalTime();
            DateTime startintended = ((DateTime)jsd["StartIntended"]).ToLocalTime();
            DateTime endintended = ((DateTime)jsd["EndIntended"]).ToLocalTime();
            string parentid = (string)jsd["ParentId"];
            string chainid = (string)jsd["ChainId"];
            uint thisnum = (uint)(int)jsd["Number"];
            //loose objects to Occurrence
            Occurrence loaded = new Occurrence();
            loaded.m_id = id;
            loaded.m_isTask = istask;
            loaded.m_createStart = startintended;
            loaded.m_createEnd = endintended;
            loaded.m_actualStart = startactual;
            loaded.m_actualEnd = endactual;
            loaded.m_chainId = chainid;
            loaded.m_parent = parentid;
            loaded.m_status = status;
            loaded.m_numId = thisnum;
            if (thisnum >= num){
                num = thisnum + 1;
            }
            if (istask)
            {
                loaded = (TaskOccurrence)loaded;
                uint times = (uint)(int)jsd["Times"];
                ((TaskOccurrence) loaded).Times = times;
            }
            All.Add(id, loaded);
            return loaded;
        }

        /**
         * Determines if this Occurrence overlaps with another.
         * 
         * @param other the other Occurrence.
         * 
         * @return true if there is any time at which both Occurrences are
         * happening simultaneously. Returns false otherwise.
         * The exception is that if one Occurrence ends just as the other
         * starts, the function returns false.
         * 
         * @date March 4, 2016
         */
        public bool Overlaps(Occurrence other) 
        {
            //if both start or end at the same time, they overlap.
            if (StartActual.Equals(other.StartActual)) return true;
            if (EndActual.Equals(other.EndActual)) return true;

            //if one starts when the other ends, no overlap.
            //this only makes sense in practice.
            if (StartActual.Equals(other.EndActual)) return false;
            if (EndActual.Equals(other.StartActual)) return false;

            //if the start or end time of one is "enclosed" by the
            //other, they overlap.
            if (other.StartActual <= StartActual && StartActual <= other.EndActual)
            {
                return true;
            }
            
            if (other.StartActual <= EndActual && EndActual <= other.EndActual)
            {
                return true;
            }

            if (StartActual <= other.StartActual && other.StartActual <= EndActual)
            {
                return true;
            }

            if (StartActual <= other.EndActual && other.EndActual <= EndActual)
            {
                return true;
            }
            return false;
        }

        /**
         * Deletes the Occurrence from the timeline.
         * 
         * @date March 16, 2016
         */
        public void Delete() { Status = OccurrenceStatus.Deleted; }

        /**
         * Sets the Occurrence to be ignored.
         * 
         * @date March 16, 2016
         */
        public void Ignore() { Status = OccurrenceStatus.Ignored; }

        /**
         * Cancels the Occurrence.
         * 
         * @date March 16, 2016
         */
        public void Cancel() { Status = OccurrenceStatus.Canceled; }

        /**
         * Postpones the Occurrence by the specified number of minutes.
         * 
         * @param minutes the number of minutes to postpone the Occurrence;
         * equal to 5 by default.
         * 
         * @date March 16, 2016
         */
        public void Postpone(uint minutes = 5) 
        { 
            Status = OccurrenceStatus.Postponed;
            StartActual = StartActual.AddMinutes(minutes);
            EndActual = EndActual.AddMinutes(minutes);
        }

        /**
         * Starts the Occurrence now, and sets the end time so
         * that the length is unchanged.
         * 
         * @date March 16, 2016
         */
        public void StartNow() 
        { 
            DateTime now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            TimeSpan diff = now.Subtract(StartActual);
            StartActual = now;
            EndActual = EndActual.Add(diff);
        }

        /**
         * Stops the Occurrence now, if possible.
         * 
         * @throws ArgumentOutOfRangeException if the Occurrence
         * has not started yet.
         * 
         * @date March 16, 2016
         */
        public void StopNow()
        {
            DateTime now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            if (StartActual > now) throw new ArgumentOutOfRangeException("Occurrence hasn't started yet");
            //can't have occurrence with length 0
            if (StartActual == now)
            {
                StartActual = StartActual.AddMinutes(-1);
            }
            EndActual = now;
        }

        /**
         * Marks the first possible alarm for
         * this Occurrence as having been dealt
         * with.
         * 
         * @date March 30, 2016
         */
        public void Confirm()
        {
            if (Parent().Alarms.ParentId == Id)
            {
                if (Status == OccurrenceStatus.Future)
                {
                    Status = OccurrenceStatus.On_Time;
                }
                List<Alarm> alarmtemp = Parent().Alarms.Alarms;
                if (Parent().Alarms.AlarmTimes().Count == 0)
                {
                    return;
                }
                if (IsChained())
                {
                    DeChain();
                }
                //find index of first (as in queue) alarm entry based on its time, and set it as done.
                OccurrenceStatus[] noInclude = {OccurrenceStatus.Canceled, OccurrenceStatus.Deleted, OccurrenceStatus.Excluded, OccurrenceStatus.Ignored};
                DateTime firstTime = Parent().Alarms.AlarmTimes()[0];
                int alarmIndex = alarmtemp.FindIndex(x => Parent().Alarms.SingleAlarmTime(x) == firstTime);
                alarmtemp[alarmIndex] = new Alarm(alarmtemp[alarmIndex].m_relativePlace, alarmtemp[alarmIndex].m_timeLength, alarmtemp[alarmIndex].m_timeUnit, true);
                //now AlarmTimes will not include it anymore, so the next one is first.
                //but if that was the last one in the occurrence, there are no more alarm times.
                if (Parent().Alarms.AlarmTimes().Count == 0)
                {
                    //move to next occurrence
                    Parent().Occurrences().Sort((x, y) => x.StartActual.CompareTo(y.StartActual));
                    //(unless there is no next occurrence)
                    if (Parent().Occurrences().IndexOf(this) == Parent().Occurrences().Count - 1)
                    {
                        Parent().Occurrences().Sort((x, y) => x.Number.CompareTo(y.Number));
                        return;
                    }
                    int nextNum = Parent().Occurrences().IndexOf(this) + 1;
                    //don't take excluded/deleted/ignored/canceled occurrences
                    while (noInclude.Contains(Parent().Occurrences()[nextNum].Status))
                    {
                        nextNum++;
                        if (nextNum == Parent().Occurrences().Count)
                        {
                            Parent().Occurrences().Sort((x, y) => x.Number.CompareTo(y.Number));
                            return;
                        }
                    }
                    Parent().Alarms.ParentId = Parent().Occurrences()[nextNum].Id;
                    Parent().Occurrences().Sort((x, y) => x.Number.CompareTo(y.Number));
                    //set all alarms to false
                    for (int i = 0; i < alarmtemp.Count(); i++)
                    {
                        alarmtemp[i] = new Alarm(alarmtemp[i].m_relativePlace, alarmtemp[i].m_timeLength, alarmtemp[i].m_timeUnit, false);

                    }
                }
            }
        }

        /**
         * Gets the parent Event to which the
         * occurrence belongs.
         * 
         * @return the parent Event to which the
         * occurrence belongs.
         * 
         * @date March 16, 2016
         */
        public Event Parent() { return Event.All[ParentId]; }

        /**
         * Specifies whether the Occurrence is part of
         * a periodic set of occurrences (a Period) or is
         * manually defined. If the Occurrence has been
         * periodically defined, it can be de-chained to
         * become manually defined.
         * 
         * @date March 15, 2016
         */
        public bool IsChained()
        {
           return m_chainId != null; 
        }

        /**
         * Removes this Occurrence from its Period.
         * 
         * @throws ArgumentException if this Occurrence is not chained,
         * or if it is chained to more than one Period at once. (This
         * should not normally be possible.)
         * 
         * @throws KeyNotFoundException if this Occurrence is chained
         * to the Period, but is somehow not found in its list of
         * Occurrences.
         * 
         * @date March 16, 2016
         */
        public void DeChain()
        {
            Event.All[ParentId].Rules.Find(x => x.Id == ChainId).DeChain(this);
        }

        /**
         * Gets the list of times when the alarms for this
         * Occurrence will go off.
         * 
         * @date March 17, 2016
         */
        public ReadOnlyCollection<DateTime> AlarmTimes()
        {
            AlarmSettings tempSettings = new AlarmSettings(Parent().Alarms, Id);
            return tempSettings.AlarmTimes();
        }

        /**
         * This counter is used to give each new Occurrence a distinct Number.
         */
        private static uint num = 1;
        /**
         * The number of the Occurrence. See also Number.
         */
        private uint m_numId;
        /**
         * The id of the Occurrence. See also Id.
         */
        private string m_id;
        /**
         * Denotes if the Occurrence is a TaskOccurrence. See also IsTask.
         */
        private bool m_isTask;
        /**
         * The actual and intended start and end times of the Occurrence. See also StartActual, EndActual, StartIntended, and EndIntended.
         */
        protected DateTime m_actualStart, m_actualEnd, m_createStart, m_createEnd;
        /**
         * The status of the Occurrence. See also Status.
         */
        protected OccurrenceStatus m_status;
        /**
         * The id of the parent Event of this Occurrence. See also ParentId.
         */
        private string m_parent;
        /**
         * The id of the parent Period of this Occurrence, if it is chained. See also ChainId.
         */
        protected string m_chainId = null;
        /**
         * Contains all Occurrence objects as values, with their Id strings as keys. See also All.
         */
        protected static Dictionary<string, Occurrence> all = new Dictionary<string, Occurrence>();
        /**
         * Denotes if the program should be run in debug mode. See also DebugMode.
         */
        private const bool m_debug = false;
    }

    /**
     * Specifies the status of the Occurrence
     * in terms of time management.
     */
    public enum OccurrenceStatus
    {
        /**
         * The Event has not occurred yet.
         */
        Future, 
        /**
         * The Event is / was on time.
         */
        On_Time, 
        /**
         * The Event has been canceled.
         * 
         * This means that the event is not
         * occurring at all.
         */
        Canceled, 
        /**
         * The Event has been / is being ignored.
         * 
         * This means that the user is choosing not
         * to partake in the event, though it is not
         * necessarily canceled.
         */
        Ignored, 
        /**
         * The Event has been postponed.
         * 
         * This is distinct from the idea of being
         * rescheduled, in that a rescheduled event can
         * still occur on time. Postponing an event means
         * either the event is starting late (preferably
         * if the user is in charge of the event), or the user
         * is late to the event.
         */
        Postponed, 
        /**
         * The Event has been deleted from the Timeline.
         * 
         * Unlike canceling or ignoring an event, deleting
         * it removes the event from the timeline
         * completely (from the user's perspective). 
         * This cannot be undone. However, an
         * event will have the status of Deleted before the
         * program closes to prevent any possible errors
         * caused by actually removing the data. Instead,
         * deleted events will not be deserialized from the
         * save file.
         */
        Deleted,
        /**
         * This Occurrence specifies when the Event cannot happen.
         * 
         * Setting an Occurrence to Excluded means that it will not
         * be counted as an Occurrence at all; instead, it requires
         * that no chained Occurrence must exist at that time for this
         * particular Event. This allows the user to specify
         * times when the Event should not occur, regardless of
         * any Period that already exists there. If any other
         * chained Occurrence is scheduled to take place during this
         * Occurrence, it will not be created.
         * 
         * Occurrences should only be given this status if they
         * are chained.
         */
        Excluded
      
    };


}
