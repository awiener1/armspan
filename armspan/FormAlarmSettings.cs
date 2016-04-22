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
        private void InitializeControlLists()
        {
            cbBeforeList = new List<CheckBox>();
            cbBeforeList.AddRange(tlpBefore.Controls.OfType<CheckBox>());
            nudBeforeList = new List<NumericUpDown>();
            nudBeforeList.AddRange(tlpBefore.Controls.OfType<NumericUpDown>());
            ddlBeforeList = new List<ComboBox>();
            ddlBeforeList.AddRange(tlpBefore.Controls.OfType<ComboBox>());

            cbDuringList = new List<CheckBox>();
            cbDuringList.AddRange(tlpDuring.Controls.OfType<CheckBox>());
            nudDuringList = new List<NumericUpDown>();
            nudDuringList.AddRange(tlpDuring.Controls.OfType<NumericUpDown>());
            ddlDuringList = new List<ComboBox>();
            ddlDuringList.AddRange(tlpDuring.Controls.OfType<ComboBox>());

            cbAfterList = new List<CheckBox>();
            cbAfterList.AddRange(tlpAfter.Controls.OfType<CheckBox>());
            nudAfterList = new List<NumericUpDown>();
            nudAfterList.AddRange(tlpAfter.Controls.OfType<NumericUpDown>());
            ddlAfterList = new List<ComboBox>();
            ddlAfterList.AddRange(tlpAfter.Controls.OfType<ComboBox>());

            ddlAllList = new List<ComboBox>();
            ddlAllList.AddRange(ddlBeforeList);
            ddlAllList.AddRange(ddlDuringList);
            ddlAllList.AddRange(ddlAfterList);

            foreach (ComboBox ddl in ddlAllList)
            {
                ddl.SelectedIndex = ddl.FindStringExact("Minutes");
            }

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

            //MessageBox.Show(string.Join(", ", cbBeforeList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", nudBeforeList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", ddlBeforeList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", cbDuringList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", nudDuringList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", ddlDuringList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", cbAfterList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", nudAfterList.Select(o => o.Name).ToArray()));
            //MessageBox.Show(string.Join(", ", ddlAfterList.Select(o => o.Name).ToArray()));
        }
        private void FormAlarmSettings_Load(object sender, EventArgs e)
        {
            InitializeControlLists();
            cbBeforeGroup.Checked = true;
            cbDuringGroup.Checked = true;
            cbAfterGroup.Checked = true;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_settings.Alarms.Clear();
            for (int i = 0; i < cbBeforeList.Count; i++)
            {
                if (!cbBeforeList[i].Checked)
                {
                    continue;
                }
                Alarm thisAlarm = new Alarm(When.Before, (uint)nudBeforeList[i].Value, (Length)Enum.Parse(typeof(Length), (string)ddlBeforeList[i].SelectedItem));
                m_settings.Alarms.Add(thisAlarm);
            }

            for (int i = 0; i < cbDuringList.Count; i++)
            {
                if (!cbDuringList[i].Checked)
                {
                    continue;
                }
                Alarm thisAlarm = new Alarm(When.During, (uint)nudDuringList[i].Value, (Length)Enum.Parse(typeof(Length), (string)ddlDuringList[i].SelectedItem));
                m_settings.Alarms.Add(thisAlarm);
            }

            for (int i = 0; i < cbAfterList.Count; i++)
            {
                if (!cbAfterList[i].Checked)
                {
                    continue;
                }
                Alarm thisAlarm = new Alarm(When.After, (uint)nudAfterList[i].Value, (Length)Enum.Parse(typeof(Length), (string)ddlAfterList[i].SelectedItem));
                m_settings.Alarms.Add(thisAlarm);
            }

            //REPEAT FOR DURING AND AFTER!!!!!


            this.Close();
        }

        private List<CheckBox> cbBeforeList;
        private List<NumericUpDown> nudBeforeList;
        private List<ComboBox> ddlBeforeList;
        private List<CheckBox> cbDuringList;
        private List<NumericUpDown> nudDuringList;
        private List<ComboBox> ddlDuringList;
        private List<CheckBox> cbAfterList;
        private List<NumericUpDown> nudAfterList;
        private List<ComboBox> ddlAfterList;
        private List<ComboBox> ddlAllList;

        private AlarmSettings m_settings;

        public AlarmSettings Settings
        {
            get
            {
                return m_settings;
            }
            set
            {
                m_settings = value;
            }
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
