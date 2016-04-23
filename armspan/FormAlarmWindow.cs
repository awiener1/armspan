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
            this.Close();
        }

        

        private void FormAlarmWindow_Load(object sender, EventArgs e)
        {
            UpdateList();
            lbNext.SelectedIndex = 0;
            DisplayAlarm(0);
        }

        private void UpdateList()
        {
            lbNext.Items.Clear();
            foreach (Occurrence occ in Alarms.Values)
            {
                lbNext.Items.Add(occ.Parent().Name);
            }
        }

        private void DisplayAlarm(uint index)
        {
            Occurrence thisOcc = Alarms.ElementAt((int)index).Value;
            DateTime thisTime = Alarms.ElementAt((int)index).Key;
            string richtext = @"{\rtf0\ansi ";
            Alarm thisAlarm = thisOcc.Parent().Alarms.Alarms.Where(x => thisOcc.Parent().Alarms.SingleAlarmTime(x).Equals(thisTime)).ElementAt(0);
            switch (thisAlarm.m_relativePlace)
            {
                case When.Before:
                    richtext += @"Are you getting ready for \b <EVNM>\b0 ?";
                    break;
                case When.During:
                    richtext += @"Is \b <EVNM>\b0  occurring?";
                    break;
                case When.After:
                    richtext += @"Did \b <EVNM>\b0  occur on time?";
                    break;
            }
            richtext += "}";

            //CITE: http://stackoverflow.com/a/4795785
            //how to convert text to rich text automatically
            RichTextBox temp = new RichTextBox();
            temp.Text = thisOcc.Parent().Name;
            string richname = temp.Rtf;
            int start = richname.IndexOf(@"\fs17") + 6;
            int length = richname.LastIndexOf(@"\par") - start;
            richname = richname.Substring(start, length);
            Color thisColor = Category.All[thisOcc.Parent().PrimaryCategory].Color;
            pnlCurrent.BackColor = thisColor;
            rtbAlarm.BackColor = ControlPaint.LightLight(thisColor);

            richtext = richtext.Replace("<EVNM>", richname);

            //MessageBox.Show(richtext);
            rtbAlarm.Rtf = richtext;
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
            toConfirm.Value.StartNow();
            toConfirm.Value.Confirm();
            RemoveAlarm(toConfirm);
        }

        private Dictionary<DateTime, Occurrence> m_alarms;


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
        }

        
    }
}
