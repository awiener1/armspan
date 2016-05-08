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
    public partial class FormAddEvent : ThinDialog
    {
        public FormAddEvent()
        {
            InitializeComponent();
            ExtraInitNew(false);
        }

        public FormAddEvent(bool a_isTask)
        {
            InitializeComponent();
            ExtraInitNew(a_isTask);
        }

        public FormAddEvent(Event a_event)
        {
            InitializeComponent();
            ExtraInitEdit(a_event);
        }
        private void ExtraInitNew(bool a_isTask)
        {
            m_isNew = true;
            m_newevent = new Event(false, "", new List<Occurrence>(), new List<Period>(), "", new List<string>(), new AlarmSettings(""), "");
            m_newevent.Exists = false;

            ExtraInitAll(a_isTask);
            m_secondcats = new List<string>();
            m_manualocc = new List<Occurrence>();
            m_rules = new List<Period>();
            m_settings = new AlarmSettings("");
            
        }

        private void ExtraInitEdit(Event a_event)
        {
            m_isNew = false;
            m_newevent = a_event;
            ExtraInitAll(m_newevent.IsTask);
            //don't make any changes unless ok is pressed
            m_secondcats = new List<string>(m_newevent.SecondaryCategories);
            m_manualocc = new List<Occurrence>(m_newevent.ManualOccurrences);
            m_rules = new List<Period>(m_newevent.Rules);
            m_settings = new AlarmSettings(m_newevent.Alarms.ParentId, m_newevent.Alarms.Alarms);
            cbCatPrim.SelectedItem = Category.All[m_newevent.PrimaryCategory];
            tbName.Text = m_newevent.Name;
            tbLocation.Text = m_newevent.Location;
            tbDesc.Text = m_newevent.Description;
        }

        private void ExtraInitAll(bool a_isTask)
        {
            cbType.SelectedIndex = cbType.FindStringExact(a_isTask ? "Task" : "Appointment");
            cbCatPrim.DataSource = Category.All.Values.ToList();
            cbCatPrim.DisplayMember = "Name";
            cbCatPrim.ValueMember = "Id";
        }

        public bool IsTask
        {
            get
            {
                return m_isTask;
            }
            set
            {
                m_isTask = value;
                lblTaskNum.Enabled = m_isTask;
                nudTaskNum.Enabled = m_isTask;
            }
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            FormAlarmSettings popup = new FormAlarmSettings();
            popup.Settings = m_settings;
            popup.ShowDialog();
            m_settings = popup.Settings;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (m_isNew)
            {
                m_newevent.Exists = false;
            }
            
            this.Close();
        }

        private void btnAddOcc_Click(object sender, EventArgs e)
        {
            bool canClose = true;
            if (tbName.Text.Trim().Equals(""))
            {
                canClose = false;
            }
            if (m_manualocc.Count == 0 && m_rules.Count == 0)
            {
                canClose = false;
            }
            if (m_settings == null)
            {
                canClose = false;
            }
            if (canClose)
            {
                m_newevent.Name = tbName.Text.Trim();
                m_newevent.PrimaryCategory = m_primarycat;
                m_newevent.SecondaryCategories = m_secondcats;
                m_newevent.ManualOccurrences = m_manualocc;
                m_newevent.Rules = m_rules;
                m_newevent.Alarms = new AlarmSettings(m_settings, m_newevent.FirstOccurrence().Id);
               
                m_newevent.Location = tbLocation.Text.Trim();
                m_newevent.Description = tbDesc.Text.Trim();
                m_newevent.Exists = true;
                TimeKeeper.Update();
                FormMain.CheckOverlapping(m_newevent);
                
                this.Close();
                //check overlapping here
            }
            else
            {
                MessageBox.Show("One or more fields are missing");
            }
        }

        private void FormAddEvent_Load(object sender, EventArgs e)
        {

        }

        private bool m_isTask;
        private bool m_isNew;

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            IsTask = (cbType.SelectedItem.ToString().Equals("Task"));
        }

        private void btnCatSec_Click(object sender, EventArgs e)
        {
            FormAddCategories popup = new FormAddCategories(false);
            List<int> incats = new List<int>();
            foreach (string id in m_secondcats){
                int index = (int)Category.All[id].Number - 1;
                incats.Add(index);
               
            }
            popup.Checked = incats;
            popup.Disabled = (int)Category.All[m_primarycat].Number - 1;
            popup.ShowDialog();
            
            
            m_secondcats.Clear();
            foreach (int index in popup.Checked)
            {
                m_secondcats.Add(Category.All.First(x => x.Value.Number == index + 1).Key);
            }
        }

        private List<string> m_secondcats;
        private string m_primarycat;
        private Event m_newevent;
        private List<Occurrence> m_manualocc;
        private List<Period> m_rules;
        private AlarmSettings m_settings;
        

        private void cbCatPrim_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_primarycat = (string)cbCatPrim.SelectedValue;
        }

        private void btnTime_Click(object sender, EventArgs e)
        {
            FormEventScheduler popup = new FormEventScheduler();
            popup.ManualOccurrences = m_manualocc;
            popup.Rules = m_rules;
            popup.ParentId = m_newevent.Id;
            popup.EventExists = !m_isNew;
            popup.ShowDialog();

            m_manualocc = popup.ManualOccurrences;
            m_rules = popup.Rules;
        }
    }
}
