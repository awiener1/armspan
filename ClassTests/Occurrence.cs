using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Span
{
    class Occurrence : JSONCapable
    {

        public enum OccurrenceStatus
        {
            Future, On_Time, Canceled, Ignored, Postponed, Deleted
        };

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
        }

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

        public string Id { get { return m_id; } }
        public bool IsTask { get { return m_isTask; } }
        public DateTime StartActual 
        {
            get { return m_actualStart; }
            set { m_actualStart = value; }
        }
        public DateTime EndActual
        {
            get { return m_actualEnd; }
            set { m_actualEnd = value; }
        }
        public DateTime StartIntended
        {
            get { return m_createStart; }
            set { m_createStart = value; }
        }
        public DateTime EndIntended
        {
            get { return m_createEnd; }
            set { m_createEnd = value; }
        }
        public OccurrenceStatus Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public string ParentId { get { return m_parent; } }

        private static uint num = 1;
        private uint m_numId;
        private string m_id;
        private bool m_isTask;
        private DateTime m_actualStart, m_actualEnd, m_createStart, m_createEnd;
        private OccurrenceStatus m_status;
        private string m_parent;
    }
}
