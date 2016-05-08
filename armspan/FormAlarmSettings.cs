/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The FormAlarmSettings class allows the user
 * to set the AlarmSettings for an Event.
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
    public partial class FormAlarmSettings : ThinDialog
    {
        public FormAlarmSettings()
        {
            InitializeComponent();  
        }

        /**
         * Initializes the controls to be grouped in lists.
         * 
         * @date April 21, 2016
         */
        private void InitializeControlLists()
        {
            //add all before objects to before lists
            cbBeforeList = new List<CheckBox>();
            cbBeforeList.AddRange(tlpBefore.Controls.OfType<CheckBox>());
            nudBeforeList = new List<NumericUpDown>();
            nudBeforeList.AddRange(tlpBefore.Controls.OfType<NumericUpDown>());
            ddlBeforeList = new List<ComboBox>();
            ddlBeforeList.AddRange(tlpBefore.Controls.OfType<ComboBox>());
            //add all during objects to during lists
            cbDuringList = new List<CheckBox>();
            cbDuringList.AddRange(tlpDuring.Controls.OfType<CheckBox>());
            nudDuringList = new List<NumericUpDown>();
            nudDuringList.AddRange(tlpDuring.Controls.OfType<NumericUpDown>());
            ddlDuringList = new List<ComboBox>();
            ddlDuringList.AddRange(tlpDuring.Controls.OfType<ComboBox>());
            //add all after objects to after lists
            cbAfterList = new List<CheckBox>();
            cbAfterList.AddRange(tlpAfter.Controls.OfType<CheckBox>());
            nudAfterList = new List<NumericUpDown>();
            nudAfterList.AddRange(tlpAfter.Controls.OfType<NumericUpDown>());
            ddlAfterList = new List<ComboBox>();
            ddlAfterList.AddRange(tlpAfter.Controls.OfType<ComboBox>());
            //add all objects to a master list
            ddlAllList = new List<ComboBox>();
            ddlAllList.AddRange(ddlBeforeList);
            ddlAllList.AddRange(ddlDuringList);
            ddlAllList.AddRange(ddlAfterList);
            //initialize controls for new event
            foreach (ComboBox ddl in ddlAllList)
            {
                ddl.SelectedIndex = ddl.FindStringExact("Minutes");
            }
            //initialize controls for edited event
            List<Alarm> oldbefore = m_settings.Alarms.Where(x => x.m_relativePlace == When.Before).ToList();
            List<Alarm> oldduring = m_settings.Alarms.Where(x => x.m_relativePlace == When.During).ToList();
            List<Alarm> oldafter = m_settings.Alarms.Where(x => x.m_relativePlace == When.After).ToList();
            for (int i = 0; i < oldbefore.Count; i++)
            {
                cbBeforeList[i].Checked = true;
                nudBeforeList[i].Value = oldbefore[i].m_timeLength;
                ddlBeforeList[i].SelectedIndex = ddlBeforeList[i].FindStringExact(oldbefore[i].m_timeUnit.ToString());
            }
            for (int i = 0; i < oldduring.Count; i++)
            {
                cbDuringList[i].Checked = true;
                nudDuringList[i].Value = oldduring[i].m_timeLength;
                ddlDuringList[i].SelectedIndex = ddlDuringList[i].FindStringExact(oldduring[i].m_timeUnit.ToString());
            }
            for (int i = 0; i < oldafter.Count; i++)
            {
                cbAfterList[i].Checked = true;
                nudAfterList[i].Value = oldafter[i].m_timeLength;
                ddlAfterList[i].SelectedIndex = ddlAfterList[i].FindStringExact(oldafter[i].m_timeUnit.ToString());
            }
        }

        private void FormAlarmSettings_Load(object sender, EventArgs e)
        {
            InitializeControlLists();
            cbBeforeGroup.Checked = true;
            cbDuringGroup.Checked = true;
            cbAfterGroup.Checked = true;

        }

        /**
         * Sets the AlarmSettings to reflect the user's choices.
         * 
         * @date April 22, 2016
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            m_settings.Alarms.Clear();
            //before
            for (int i = 0; i < cbBeforeList.Count; i++)
            {
                if (!cbBeforeList[i].Checked)
                {
                    continue;
                }
                Alarm thisAlarm = new Alarm(When.Before, (uint)nudBeforeList[i].Value, (Length)Enum.Parse(typeof(Length), (string)ddlBeforeList[i].SelectedItem));
                m_settings.Alarms.Add(thisAlarm);
            }
            //during
            for (int i = 0; i < cbDuringList.Count; i++)
            {
                if (!cbDuringList[i].Checked)
                {
                    continue;
                }
                Alarm thisAlarm = new Alarm(When.During, (uint)nudDuringList[i].Value, (Length)Enum.Parse(typeof(Length), (string)ddlDuringList[i].SelectedItem));
                m_settings.Alarms.Add(thisAlarm);
            }
            //after
            for (int i = 0; i < cbAfterList.Count; i++)
            {
                if (!cbAfterList[i].Checked)
                {
                    continue;
                }
                Alarm thisAlarm = new Alarm(When.After, (uint)nudAfterList[i].Value, (Length)Enum.Parse(typeof(Length), (string)ddlAfterList[i].SelectedItem));
                m_settings.Alarms.Add(thisAlarm);
            }
            this.Close();
        }

        /**
         * The list of CheckBoxes for Before alarms.
         */
        private List<CheckBox> cbBeforeList;
        /**
         * The list of NumericUpDowns for Before alarms.
         */
        private List<NumericUpDown> nudBeforeList;
        /**
         * The list of ComboBoxes for Before alarms.
         */
        private List<ComboBox> ddlBeforeList;
        /**
         * The list of CheckBoxes for During alarms.
         */
        private List<CheckBox> cbDuringList;
        /**
         * The list of NumericUpDowns for During alarms.
         */
        private List<NumericUpDown> nudDuringList;
        /**
         * The list of ComboBoxes for During alarms.
         */
        private List<ComboBox> ddlDuringList;
        /**
         * The list of CheckBoxes for After alarms.
         */
        private List<CheckBox> cbAfterList;
        /**
         * The list of NumericUpDowns for After alarms.
         */
        private List<NumericUpDown> nudAfterList;
        /**
         * The list of ComboBoxes for After alarms.
         */
        private List<ComboBox> ddlAfterList;
        /**
         * The list of ComboBoxes for all alarms.
         */
        private List<ComboBox> ddlAllList;
        /**
         * The AlarmSettings to modify. See also Settings.
         */
        private AlarmSettings m_settings;

        /**
         * The AlarmSettings to modify.
         */
        public AlarmSettings Settings
        {
            get { return m_settings; }
            set { m_settings = value; }
        }

        private void cbBeforeGroup_CheckedChanged(object sender, EventArgs e)
        {
            gbBefore.Enabled = cbBeforeGroup.Checked;
        }

        private void cbDuringGroup_CheckedChanged(object sender, EventArgs e)
        {
            gbDuring.Enabled = cbDuringGroup.Checked;
        }

        private void cbAfterGroup_CheckedChanged(object sender, EventArgs e)
        {
            gbAfter.Enabled = cbAfterGroup.Checked;
        }
    }
}
