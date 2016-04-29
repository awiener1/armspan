using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Span.GUI
{
    public partial class FormAlarmWindow : ThinDialog
    {
        public FormAlarmWindow()
        {
            InitializeComponent();
        }

        private void btnPostponeFive_Click(object sender, EventArgs e)
        {
            var distinctOccurrences = Alarms.Select(x => x.Value).Distinct();
            foreach (Occurrence occ in distinctOccurrences){
                occ.Postpone(5);
            }
            this.Close();
        }

        

        private void FormAlarmWindow_Load(object sender, EventArgs e)
        {
            UpdateList();
            lbNext.SelectedIndex = 0;
            DisplayAlarm(0);
        }

        public void UpdateList()
        {
            object olditem = lbNext.SelectedItem;
            lbNext.Items.Clear();
            foreach (var occ in Alarms)
            {
                lbNext.Items.Add(occ.Value.Parent().Name + " " + occ.Key.ToShortTimeString());
            }
            lbNext.SelectedItem = olditem;
        }

        private void DisplayAlarm(uint index)
        {
            rtbAlarm.Text = "";
            Occurrence thisOcc = Alarms.ElementAt((int)index).Value;
            DateTime thisTime = Alarms.ElementAt((int)index).Key;
           
            string beforetext = "";
            string aftertext = "";
            Alarm thisAlarm = thisOcc.Parent().Alarms.Alarms.Where(x => thisOcc.Parent().Alarms.SingleAlarmTime(x).Equals(thisTime)).ElementAt(0);
            switch (thisAlarm.m_relativePlace)
            {
                case When.Before:
                    
                    beforetext = "Are you getting ready for ";
                    aftertext = "?";
                    break;
                case When.During:
                    
                    beforetext = "Is ";
                    aftertext = " occurring?";
                    break;
                case When.After:
                   
                    beforetext = "Did ";
                    aftertext = " occur on time?";
                    break;
            }
           

           
            Color thisColor = Category.All[thisOcc.Parent().PrimaryCategory].Color;
            pnlCurrent.BackColor = thisColor;
            rtbAlarm.BackColor = ControlPaint.LightLight(thisColor);

            

            rtbAlarm.AppendText(beforetext);
            rtbAlarm.SelectionFont = new Font(rtbAlarm.Font, FontStyle.Bold);
            rtbAlarm.AppendText(thisOcc.Parent().Name);
            rtbAlarm.SelectionFont = rtbAlarm.Font;
            rtbAlarm.AppendText(aftertext + "\n");
            rtbAlarm.AppendText(thisOcc.StartActual.ToShortDateString() + " " + thisOcc.StartActual.ToShortTimeString());
            rtbAlarm.AppendText(" - ");
            rtbAlarm.AppendText(thisOcc.EndActual.ToShortDateString() + " " + thisOcc.EndActual.ToShortTimeString());
            DateTime alarmOff = thisOcc.Parent().Alarms.SingleAlarmTime(thisAlarm);
            rtbAlarm.SelectionFont = new Font(rtbAlarm.Font.Name, rtbAlarm.Font.Size * 0.75f, rtbAlarm.Font.Unit);
            rtbAlarm.AppendText("\nThis alarm was set to go off on " + alarmOff.ToShortDateString() + " at " + alarmOff.ToShortTimeString() + ".");
            //MessageBox.Show(richtext);

            if (thisOcc.StartActual <= TimeKeeper.Now.ToLocalTime() && thisOcc.EndActual > TimeKeeper.Now.ToLocalTime())
            {
                btnNow.Image = global::armspan.Properties.Resources.stop_svg;
                btnNow.Text = "Stop Now";
                m_stop = true;
            }
            else
            {
                btnNow.Image = global::armspan.Properties.Resources.now_svg;
                btnNow.Text = "Start Now";
                m_stop = false;
            }

            m_settings = thisOcc.Parent().Alarms;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            toConfirm.Value.Cancel();
            toConfirm.Value.Confirm();
            RemoveAlarm(toConfirm);
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            toConfirm.Value.Ignore();
            toConfirm.Value.Confirm();
            RemoveAlarm(toConfirm);
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            if (m_stop)
            {
                toConfirm.Value.StopNow();
            }
            else
            {
                toConfirm.Value.StartNow();
            }
            
            toConfirm.Value.Confirm();
            RemoveAlarm(toConfirm);
        }

        private Dictionary<DateTime, Occurrence> m_alarms;
        private AlarmSettings m_settings;
        private bool m_stop;

        public Dictionary<DateTime, Occurrence> Alarms
        {
            get
            {
                return m_alarms;
            }

            set
            {
                m_alarms = value;
            }
        }

        private void lbNext_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayAlarm((uint)lbNext.SelectedIndex);
        }

        private void btnAffirm_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            toConfirm.Value.Confirm();
            RemoveAlarm(toConfirm);
        }

        private void RemoveAlarm(KeyValuePair<DateTime, Occurrence> toConfirm)
        {
            
            
            Alarms.Remove(toConfirm.Key);
            if (Alarms.Count > 0)
            {
                UpdateList();
                lbNext.SelectedIndex = 0;
                DisplayAlarm(0);
            }
            else
            {
                this.Close();

            }
        }

        private void btnPostpone_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            toConfirm.Value.Postpone((uint)nudPostponeOcc.Value);
            RemoveAlarm(toConfirm);
            //due to alarm changes, close.
            this.Close();
        }

        private void btnAlarmSet_Click(object sender, EventArgs e)
        {
            FormAlarmSettings popup = new FormAlarmSettings();
            popup.Settings = new AlarmSettings(m_settings.ParentId, m_settings.Alarms);
            popup.ShowDialog();
            Alarms.ElementAt(lbNext.SelectedIndex).Value.Parent().Alarms.Alarms = popup.Settings.Alarms;
            //due to alarm changes, close and possibly reopen in ten seconds
            this.Close();
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            Event thisEvent = Alarms.ElementAt(lbNext.SelectedIndex).Value.Parent();
            MessageBox.Show(thisEvent.Description);
        }

        
    }
}
