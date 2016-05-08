/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The FormAlarmWindow class appears
 * to notify the user of the alarms that
 * are currently going off. It also provides
 * controls to allow the user to confirm the
 * alarms or take other actions.
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
using System.Windows.Forms.VisualStyles;

namespace Span.GUI
{
    public partial class FormAlarmWindow : ThinDialog
    {
        /**
         * Gets the list of alarms currently going off.
         * 
         * The Alarm structs themselves are not very useful, so
         * the dictionary contains each alarm's time as a key and
         * parent Occurrence as a value.
         */
        public Dictionary<DateTime, Occurrence> Alarms
        {
            get { return m_alarms; }
            set { m_alarms = value; }
        }

        public FormAlarmWindow()
        {
            InitializeComponent();
        }

        /**
         * Updates the list of alarms going off.
         * 
         * @date April 23, 2016
         */
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

        /**
         * Displays the alarm at the specified index of the visible list of alarms.
         * 
         * @param index The index of the alarm to display.
         * 
         * @date April 23, 2016
         */
        private void DisplayAlarm(uint index)
        {
            rtbAlarm.Text = "";
            Occurrence thisOcc = Alarms.ElementAt((int)index).Value;
            DateTime thisTime = Alarms.ElementAt((int)index).Key;
            //display text on main alarm RichTextBox
            string beforetext = "";
            string aftertext = "";
            Alarm thisAlarm = thisOcc.Parent().Alarms.Alarms.Where(x => thisOcc.Parent().Alarms.SingleAlarmTime(x).Equals(thisTime)).ElementAt(0);
            //determine message based on type of alarm
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
            //set color
            Color thisColor = Category.All[thisOcc.Parent().PrimaryCategory].Color;
            pnlCurrent.BackColor = thisColor;
            rtbAlarm.BackColor = ControlPaint.LightLight(thisColor);
            rtbAlarm.AppendText(beforetext);
            rtbAlarm.SelectionFont = new Font(rtbAlarm.Font, FontStyle.Bold);
            rtbAlarm.AppendText(thisOcc.Parent().Name);
            rtbAlarm.SelectionFont = rtbAlarm.Font;
            rtbAlarm.AppendText(aftertext + "\n");
            //display date/times
            rtbAlarm.AppendText(thisOcc.StartActual.ToShortDateString() + " " + thisOcc.StartActual.ToShortTimeString());
            rtbAlarm.AppendText(" - ");
            rtbAlarm.AppendText(thisOcc.EndActual.ToShortDateString() + " " + thisOcc.EndActual.ToShortTimeString());
            DateTime alarmOff = thisOcc.Parent().Alarms.SingleAlarmTime(thisAlarm);
            rtbAlarm.SelectionFont = new Font(rtbAlarm.Font.Name, rtbAlarm.Font.Size * 0.75f, rtbAlarm.Font.Unit);
            rtbAlarm.AppendText("\nThis alarm was set to go off on " + alarmOff.ToShortDateString() + " at " + alarmOff.ToShortTimeString() + ".");
            //switch startnow button to stopnow if necessary
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

        /**
         * Removes the specified alarm and updates the list of alarms.
         * 
         * @param toConfirm a KeyValuePair, with the alarm's time as
         * the key and the alarm's parent Occurrence as the value, of
         * the alarm to remove.
         * 
         * @date April 23, 2016
         */
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

        /**
         * Postpones all Occurrences mentioned in the alarm window for five minutes.
         * 
         * This allows the user to quickly close the window,
         * but effectively forces it to open again in five minutes.
         * 
         * @date April 23, 2016
         */
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

        /**
         * Cancels the Occurrence associated with the alarm.
         * 
         * @date April 23, 2016
         */
        private void btnCancel_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            toConfirm.Value.Cancel();
            toConfirm.Value.Confirm();
            RemoveAlarm(toConfirm);
        }

        /**
         * Ignores the Occurrence associated with the alarm.
         * 
         * @date April 23, 2016
         */
        private void btnIgnore_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            toConfirm.Value.Ignore();
            toConfirm.Value.Confirm();
            RemoveAlarm(toConfirm);
        }

        /**
         * Starts or stops the Occurrence associated with the alarm.
         * 
         * @date April 23, 2016
         */
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

        private void lbNext_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayAlarm((uint)lbNext.SelectedIndex);
        }

        /**
         * Affirms the alarm by confirming the Occurrence associated with the alarm.
         * 
         * @date April 23, 2016
         */
        private void btnAffirm_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            toConfirm.Value.Confirm();
            RemoveAlarm(toConfirm);
        }

        /**
         * Postpones the Occurrence associated with the alarm.
         * 
         * @date April 23, 2016
         */
        private void btnPostpone_Click(object sender, EventArgs e)
        {
            var toConfirm = Alarms.ElementAt(lbNext.SelectedIndex);
            toConfirm.Value.Postpone((uint)nudPostponeOcc.Value);
            RemoveAlarm(toConfirm);
            //due to alarm changes, close.
            this.Close();
        }

        /**
         * Changes the alarm settings for the Event associated with the alarm.
         * 
         * @date April 23, 2016
         */
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
        
        /**
         * The list of alarms going off. See also Alarms.
         */
        private Dictionary<DateTime, Occurrence> m_alarms;
        /**
         * The settings for the current alarm.
         */
        private AlarmSettings m_settings;
        /**
         * Denotes if the start now button should change to "stop now".
         */
        private bool m_stop;
    }
}
