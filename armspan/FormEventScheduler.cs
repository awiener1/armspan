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
            m_manual = new List<Occurrence>();
            m_rules = new List<Period>();

            //assume pre-existing schedule
            lbSchedule.Items.AddRange(m_manual.ToArray());
            lbSchedule.Items.AddRange(m_rules.ToArray());
        
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateOptions()
        {
            gbPeriodic.Enabled = !rbManual.Checked;
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
            DateTime start = dtpFrom.Value;
            DateTime end = dtpTo.Value;
            if (rbPeriodic.Checked)
            {
                uint freq = (uint)nudFrequency.Value;
                Span.Frequency timeunit = (Span.Frequency)Enum.Parse(typeof(Span.Frequency), (string)cbFrequency.SelectedItem);
                TimeSpan length = dtpOccEnd.Value - start;
                length = length.TotalMinutes > 0 ? length : TimeSpan.FromMinutes(1);
                Period rule = new Period(freq, timeunit, start, end, start, end, length, ParentId);
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
    }
}
