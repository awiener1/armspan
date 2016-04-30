using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Span.GUI
{
    public partial class FormSummaryWindow : ThinDialog
    {
        public FormSummaryWindow()
        {
            InitializeComponent();
        }

        private void FormSummaryWindow_Load(object sender, EventArgs e)
        {
            if (m_dateChanged)
            {
                dtpFrom.Value = m_begin;
                dtpTo.Value = m_end;
            }
            UpdateSummary();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static DateTime m_begin;
        private static DateTime m_end;
        private static bool m_dateChanged = false;
        private List<string> m_catsToUse;

        public List<string> Categories
        {
            get
            {
                return m_catsToUse;
            }
            set
            {
                m_catsToUse = value;
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            
            UpdateSummary();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            //prevent string bugs
            string nl = Environment.NewLine;
            m_begin = dtpFrom.Value.ToUniversalTime().Date;
            m_end = dtpTo.Value.ToUniversalTime().Date.AddDays(1);
            m_dateChanged = true;
            tbSummary.Text = "";
            OccurrenceStatus[] noInclude = { OccurrenceStatus.Deleted, OccurrenceStatus.Excluded };
            foreach (string catid in m_catsToUse)
            {
                Category cat = Category.All[catid];
                tbSummary.AppendText(cat.Name + "\n");
                List<string> gottenEvents = Category.getEvents(catid);
                var catevents = gottenEvents.Select(x => Event.All[x]);
                var catoccs = Occurrence.All.Values.Where(x => gottenEvents.Contains(x.ParentId));
                catoccs = catoccs.Where(x => x.StartIntended >= m_begin && x.EndIntended <= m_end && !noInclude.Contains(x.Status)).ToList();
                catevents = catoccs.Select(x => x.Parent()).Distinct();
                tbSummary.AppendText("-Events: " + catevents.Count() + nl);
                if (catevents.Count() == 0)
                {
                    tbSummary.AppendText(nl);
                    continue;
                }
                double occCount = catoccs.Count();
                tbSummary.AppendText("-Occurrences: " + catoccs.Count() + nl);
                double occOK = catoccs.Where(x => x.Status == OccurrenceStatus.Future || x.Status == OccurrenceStatus.On_Time).Count();
                tbSummary.AppendText("-Timely started and ended: " + occOK + " (" + (int)(occOK * 100 / occCount) + "%)" + nl);
                double occIgnored = catoccs.Where(x => x.Status == OccurrenceStatus.Ignored).Count();
                tbSummary.AppendText("-Ignored: " + occIgnored + " (" + (int)(occIgnored * 100 / occCount) + "%)" + nl);
                double occCanceled = catoccs.Where(x => x.Status == OccurrenceStatus.Canceled).Count();
                tbSummary.AppendText("-Canceled: " + occCanceled + " (" + (int)(occCanceled * 100 / occCount) + "%)" + nl);
                double occLate = catoccs.Where(x => x.Status == OccurrenceStatus.Postponed).Count();
                tbSummary.AppendText("-Started Late: " + occLate + " (" + (int)(occLate * 100 / occCount) + "%)" + nl + nl);
            }
        }
    }
}
