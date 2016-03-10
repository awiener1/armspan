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

namespace Span
{   /**
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
         * deleted events will not be serialized to the
         * save file.
         */
        Deleted
    };

    class Occurrence : JSONCapable
    {
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

        private static uint num = 1;
        private uint m_numId;
        private string m_id;
        private bool m_isTask;
        private DateTime m_actualStart, m_actualEnd, m_createStart, m_createEnd;
        private OccurrenceStatus m_status;
        private string m_parent;
        private static Dictionary<string, Occurrence> all = new Dictionary<string, Occurrence>();
    }
}
