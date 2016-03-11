﻿/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The TaskOccurrence class defines one single, reoccurring occurrence
 * of a task.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Span
{
    class TaskOccurrence : Occurrence
    {
        /**
         * Creates a new TaskOccurrence from the specified times, parent id,
         * and task quantity.
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
         * @param a_times the number of times the task should be done between
         * a_createStart and a_createEnd.
         * 
         * @date March 10, 2016
         */
        public TaskOccurrence(bool a_isTask, DateTime a_createStart, DateTime a_createEnd, string a_parent, uint a_times = 1)
            : base(a_isTask, a_createStart, a_createEnd, a_parent)
        {
            Times = a_times;
        }

        /**
         * Gets or sets the number of times the task should be done during
         * the Occurrence.
         */
        public uint Times
        {
            get { return m_times; }

            set { m_times = Math.Max(value, 1); }
        }

        /**
         * Sets up the TaskOccurrence to do the task again.
         * 
         * Currently, this only increments the Times property.
         * 
         * @date March 10, 2016
         */
        public void DoAgain()
        {
            Times++;
        }

        private uint m_times;
    }
}
