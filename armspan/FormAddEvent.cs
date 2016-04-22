﻿using System;
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
            ExtraInit(false);
        }

        public FormAddEvent(bool a_isTask)
        {
            InitializeComponent();
            ExtraInit(a_isTask);
        }

        private void ExtraInit(bool a_isTask)
        {
            m_newevent = new Event(false, "", new List<Occurrence>(), new List<Period>(), "", new List<string>(), null, "");
            //make sure alarms are filled
            cbType.SelectedIndex = cbType.FindStringExact(a_isTask ? "Task" : "Appointment");
            cbCatPrim.DataSource = Category.All.Values.ToList();
            cbCatPrim.DisplayMember = "Name";
            cbCatPrim.ValueMember = "Id";
            m_secondcats = new List<string>();
            m_manualocc = new List<Occurrence>();
            m_rules = new List<Period>();
            m_settings = new AlarmSettings("");
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
            //if a new event only
            m_newevent.Exists = false;
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
            MessageBox.Show("end");
            
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
            //TODO: change this to not be dependent on the parentid code here.
            //for example, set up a dummy class that has no id but allows for passing.
            popup.ManualOccurrences = m_manualocc;
            popup.Rules = m_rules;
            popup.ParentId = m_newevent.Id;
            popup.ShowDialog();
            m_manualocc = popup.ManualOccurrences;
            m_rules = popup.Rules;
        }
    }
}
