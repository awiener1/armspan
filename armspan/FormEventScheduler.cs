/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The FormEventScheduler class allows the user
 * to schedule manual Occurrences and Periods
 * for an event.
 * 
 */
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
        /**
         * The id of the Event.
         */
        public string ParentId
        {
            get { return m_parentid; }
            set { m_parentid = value; }
        }

        /**
         * The list of all manual Occurrences.
         */
        public List<Occurrence> ManualOccurrences
        {
            get { return m_manual; }
            set { m_manual = value; }
        }

        /**
         * The list of all Periods.
         */
        public List<Period> Rules
        { 
            get { return m_rules; }
            set { m_rules = value; }
        }

        /**
         * Denotes if the Event already exists.
         * 
         * If it does, removing any Periods or Occurrences
         * requires referencing them.
         */
        public bool EventExists
        {
            get { return m_edit; }
            set { m_edit = value; }
        }

        public FormEventScheduler()
        {
            InitializeComponent();
            ExtraInit();
            
        }

        private void ExtraInit()
        {
            UpdateOptions();
            cbFrequency.SelectedIndex = cbFrequency.FindStringExact("Minutes");
        }

        private void UpdateOptions()
        {
            gbPeriodic.Enabled = !rbManual.Checked;
            cbExclude.Enabled = !rbManual.Checked;
        }

        /**
         * Confirms the schedule and adds it to the Event.
         * 
         * @date April 22, 2016
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_manual.Count == 0 && m_rules.Where(x => !x.Excluded).Count() == 0)
            {
                MessageBox.Show("You must include at least one manual time, or at least one periodic time that is not excluded.");
                return;
            }
            //delete removed periods and occurrences that already existed
            //but not ones that were created and immediately removed
            if (EventExists)
            {
                foreach (string s in m_manualToRemove)
                {
                    if (Occurrence.All.ContainsKey(s))
                    {
                        Occurrence.All[s].Delete();
                    }
                }
                foreach (string s in m_rulesToRemove)
                {
                    Period p =  Event.All[ParentId].Rules.FirstOrDefault(x => x.Id.Equals(s));
                    if (p != null)
                    {
                        p.WipeOut();
                    } 
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        /**
         * Adds an Occurrence or Period to the schedule based on the user's selections.
         * 
         * @date April 20, 2016
         */
        private void btnAddOcc_Click(object sender, EventArgs e)
        {
            DateTime start = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day, dtpFrom.Value.Hour, dtpFrom.Value.Minute, 0, 0);
            DateTime end = new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day, dtpTo.Value.Hour, dtpTo.Value.Minute, 0, 0);
            if (end < start.AddMinutes(1))
            {
                MessageBox.Show("Start time must be at least one minute before end time");
                return;
            }
            
            if (rbPeriodic.Checked)
            {
                //create Period
                uint freq = (uint)nudFrequency.Value;
                Span.Frequency timeunit = (Span.Frequency)Enum.Parse(typeof(Span.Frequency), (string)cbFrequency.SelectedItem);
                TimeSpan length = new DateTime(dtpOccEnd.Value.Year, dtpOccEnd.Value.Month, dtpOccEnd.Value.Day, dtpOccEnd.Value.Hour, dtpOccEnd.Value.Minute, 0, 0) - start;
                length = length.TotalMinutes > 0 ? length : TimeSpan.FromMinutes(1);
                Period rule = new Period(freq, timeunit, start, end, length, ParentId);
                rule.Excluded = cbExclude.Checked;
                m_rules.Add(rule);
                lbSchedule.Items.Add(rule);
               
            }
            else
            {
                //create Occurrence
                Occurrence manual = new Occurrence(Event.All[ParentId].IsTask, start, end, ParentId);
                m_manual.Add(manual);
                lbSchedule.Items.Add(manual);
            }
            lbSchedule.Update();
        }

        /**
         * Removes the selected item from the schedule.
         * 
         * @date April 20, 2016
         */
        private void btnRemoveOcc_Click(object sender, EventArgs e)
        {
            if (lbSchedule.Items.Count == 0)
            {
                return;
            }
            
            if (lbSchedule.SelectedItem is Occurrence)
            {
                //delete occurrence unless it exists already
                //(and if so, mark it for deletion)
                Occurrence thisitem = (Occurrence)lbSchedule.SelectedItem;
                lbSchedule.Items.Remove(thisitem);
                m_manual.Remove(thisitem);
                if (EventExists)
                {
                    m_manualToRemove.Add(thisitem.Id);
                }
                else
                {
                    thisitem.Delete();
                }
            }
            else if (lbSchedule.SelectedItem is Period)
            {
                //delete period unless it exists already
                //(and if so, mark it for deletion)
                Period thisitem = (Period)lbSchedule.SelectedItem;
                lbSchedule.Items.Remove(thisitem);
                m_rules.Remove(thisitem);
                if (EventExists)
                {
                    m_rulesToRemove.Add(thisitem.Id);
                }
            }
            //if it's neither of those (eg, null), don't do anything.
        }

        private void FormEventScheduler_Load(object sender, EventArgs e)
        {
            //assume pre-existing schedule
            lbSchedule.DisplayMember = "Describe";
            lbSchedule.Items.AddRange(m_manual.ToArray());
            lbSchedule.Items.AddRange(m_rules.ToArray());
            if (EventExists)
            {
                m_manualToRemove = new List<string>();
                m_rulesToRemove = new List<string>();
            }
        }

        /**
         * The list of all manual Occurrences. See also ManualOccurrences.
         */
        private List<Occurrence> m_manual;
        /**
         * The list of all Periods. See also Rules.
         */
        private List<Period> m_rules;
        /**
         * The list of all manual Occurrences to remove that already exist.
         */
        private List<string> m_manualToRemove;
        /**
         * The list of all Periods to remove that already exist.
         */
        private List<string> m_rulesToRemove;
        /**
         * Denotes if the Event already exists. See also EventExists.
         */
        private bool m_edit;
        /**
         * The id of the Event. See also ParentId.
         */
        private string m_parentid;
    }
}
