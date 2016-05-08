/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The TaskOccurrence class defines one single task Event
 * occurring on the timeline.
 * 
 * @deprecated This class has been deprecated, 
 * as tasks have been removed from this version of the program.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Span
{
    class TaskEvent : Event
    {

        /**
         * Creates a new TaskEvent from the specified information.
         * 
         * @param a_name a name or title for the event. The name
         * can be changed at any time, as the event is internally
         * identified with an Id. This string must be single-line.
         * 
         * @param a_manualOccurrences a list of Occurrences defined
         * manually. These will occur only once on the timeline.
         * 
         * @param a_allRules a list of Period objects specifying
         * periodic Occurrences for the Event, which may occur
         * multiple times on the timeline.
         * 
         * @param a_loc a place or location for the event. This
         * parameter is mandatory, but will be ignored if it is
         * set to a blank string. This string must be single-line.
         * 
         * @param a_categories a list of ids for Categories to which
         * this Event belongs. Index 0 is the PrimaryCategory and
         * the rest are SecondaryCategories.
         * 
         * @param a_alarms an AlarmSettings object indicating when
         * alarms should go off for each Occurrence of this Event.
         * Note: the alarm settings are the same for every Occurrence,
         * but they can be changed at any time.
         * 
         * @param a_desc a description for the Event. This string may
         * be multi-line.
         * 
         * @param a_times the minimum number of times the task may
         * occur during each Occurrence.
         * 
         * @date March 15, 2016
         */
        public TaskEvent(string a_name, List<Occurrence> a_manualOccurrences, List<Period> a_allRules, 
            string a_loc, List<string> a_categories, AlarmSettings a_alarms, string a_desc, uint a_times)
            : base(true, a_name, a_manualOccurrences, a_allRules, a_loc, a_categories, a_alarms, a_desc)
        {
            Times = a_times;
        }

        /**
         * Gets or sets the minimum number of times the task may
         * occur during each Occurrence. Must be greater than or equal
         * to 1.
         */
        public uint Times
        {
            get { return m_times; }
            set { m_times = Math.Max(value, 1); }
        }

        /**
         * The minimum number of times the task may occur. See also Occurrence.
         */
        private uint m_times;



    }
}
