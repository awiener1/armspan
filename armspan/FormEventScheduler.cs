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
    public partial class FormEventScheduler : ThinDialog
    {
        public FormEventScheduler()
        {
            InitializeComponent();
            ExtraInit();
            
        }

        private void ExtraInit()
        {
            UpdateOptions();
            cbFrequency.SelectedIndex = cbFrequency.FindStringExact("Minutes");
            ////don't use, as they will be initialized by parent form
            //m_manual = new List<Occurrence>();
            //m_rules = new List<Period>();

            
        
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_manual.Count == 0 && m_rules.Where(x => !x.Excluded).Count() == 0)
            {
                MessageBox.Show("You must include at least one manual time, or at least one periodic time that is not excluded.");
                return;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateOptions()
        {
            gbPeriodic.Enabled = !rbManual.Checked;
            cbExclude.Enabled = !rbManual.Checked;
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private List<Occurrence> m_manual;
        private List<Period> m_rules;
        
        private string m_parentid;

        public string ParentId
        {
            get
            {
                return m_parentid;
            }
            set
            {
                m_parentid = value;
            }
        }

        private void btnAddOcc_Click(object sender, EventArgs e)
        {
            DateTime start = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day, dtpFrom.Value.Hour, dtpFrom.Value.Minute, 0, 0);
            DateTime end = new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day, dtpTo.Value.Hour, dtpTo.Value.Minute, 0, 0);
            if (rbPeriodic.Checked)
            {
                uint freq = (uint)nudFrequency.Value;
                Span.Frequency timeunit = (Span.Frequency)Enum.Parse(typeof(Span.Frequency), (string)cbFrequency.SelectedItem);
                TimeSpan length = new DateTime(dtpOccEnd.Value.Year, dtpOccEnd.Value.Month, dtpOccEnd.Value.Day, dtpOccEnd.Value.Hour, dtpOccEnd.Value.Minute, 0, 0) - start;
                length = length.TotalMinutes > 0 ? length : TimeSpan.FromMinutes(1);
                Period rule = new Period(freq, timeunit, start, end, length, ParentId);
                rule.Excluded = cbExclude.Checked;
                m_rules.Add(rule);
                lbSchedule.Items.Add(rule);
                MessageBox.Show(rule.ToString());
            }
            else
            {
                Occurrence manual = new Occurrence(Event.All[ParentId].IsTask, start, end, ParentId);
                m_manual.Add(manual);
                lbSchedule.Items.Add(manual);
                MessageBox.Show(manual.ToString());
            }
            
            lbSchedule.Update();
        }

        public List<Occurrence> ManualOccurrences
        {
            get
            {
                return m_manual;
            }
            set
            {
                m_manual = value;
            }
        }

        public List<Period> Rules
        {
            get
            {
                return m_rules;
            }
            set
            {
                m_rules = value;
            }
        }

        private void btnRemoveOcc_Click(object sender, EventArgs e)
        {
            if (lbSchedule.Items.Count == 0)
            {
                return;
            }
            if (lbSchedule.SelectedItem is Occurrence)
            {
                Occurrence thisitem = (Occurrence)lbSchedule.SelectedItem;
                lbSchedule.Items.Remove(thisitem);
                m_manual.Remove(thisitem);
                thisitem.Delete();
            }
            else if (lbSchedule.SelectedItem is Period)
            {
                Period thisitem = (Period)lbSchedule.SelectedItem;
                lbSchedule.Items.Remove(thisitem);
                m_rules.Remove(thisitem);
            }
        }

        private void FormEventScheduler_Load(object sender, EventArgs e)
        {
            //assume pre-existing schedule
            lbSchedule.Items.AddRange(m_manual.ToArray());
            lbSchedule.Items.AddRange(m_rules.ToArray());
        }
    }
}
