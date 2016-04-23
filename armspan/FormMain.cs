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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public float DateTimeToPixel(DateTime time)
        {
            TimeSpan diff = time - TimeKeeper.Begin;
            float point = (float)(diff.TotalMinutes / (TimeKeeper.End - TimeKeeper.Begin).TotalMinutes);
            point *= pbTimeline.Width;
            return point;
        }

        public void DrawTimeline()
        {
            TimeKeeper.Update();
            int oldScroll = pnlTimeline.HorizontalScroll.Value;
           
            int oldMax = pnlTimeline.HorizontalScroll.Maximum;
            float oldpos = (float)oldScroll / (float)oldMax;
            //temp draw timeline
            Bitmap tl = new Bitmap(pnlTimeline.Width * TimeKeeper.ZoomFactor, pnlTimeline.Height);
            pbTimeline.Width = pnlTimeline.Width * TimeKeeper.ZoomFactor;
            Graphics tlg = Graphics.FromImage(tl);
            tlg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //TimeSpan bte = TimeKeeper.End - TimeKeeper.Begin;
            Pen dayDasher = new Pen(Color.Black);
            dayDasher.DashPattern = new float[] { 8.0f, 2.0f };
            Pen hourDasher = new Pen(Color.Gray);
            hourDasher.DashPattern = new float[] { 2.0f, 2.0f };
            Pen nowDasher = new Pen(Color.Red, 2.0f);
            nowDasher.DashPattern = new float[] { 1.0f, 1.0f };
            for (DateTime i = TimeKeeper.Begin.Date; i < TimeKeeper.End; i = i.AddHours(1))
            {
                //TimeSpan ist = i - TimeKeeper.Begin;
                //float ipt = (float)(ist.TotalMinutes / bte.TotalMinutes);
                //ipt *= pbTimeline.Width;
                float ipt = DateTimeToPixel(i);
                if (i.ToLocalTime() == i.ToLocalTime().Date)
                {
                    tlg.DrawLine(dayDasher, ipt, 0, ipt, pbTimeline.Height);
                }
                else
                {
                    tlg.DrawLine(hourDasher, ipt, 0, ipt, pbTimeline.Height);
                }

            }
            //TimeSpan nowst = TimeKeeper.Now - TimeKeeper.Begin;
            //float nowpt = (float)(nowst.TotalMinutes / bte.TotalMinutes);
            //nowpt *= pbTimeline.Width;
            float nowpt = DateTimeToPixel(TimeKeeper.Now);
            //offset by 2 so it is easier to see next to hours
            tlg.DrawLine(nowDasher, nowpt, 2, nowpt, pbTimeline.Height);
            tlg.FillPolygon(new SolidBrush(Color.Black), new PointF[] { new PointF(nowpt - 10.5f, -0.5f), new PointF(nowpt + 10.5f, -0.5f), new PointF(nowpt, 10.5f) });
            Dictionary<string, int> primCatHeight = new Dictionary<string, int>();
            int offset = 0;
            foreach (string s in TimeKeeper.InDate)
            {
                Occurrence o = Occurrence.All[s];
                if (o.Status == OccurrenceStatus.Deleted || o.Status == OccurrenceStatus.Canceled)
                {
                    continue;
                }
                if (!o.Parent().Exists)
                {
                    continue;
                }
                string newprim = Occurrence.All[s].Parent().PrimaryCategory;
                if (!primCatHeight.ContainsKey(newprim))
                {
                    primCatHeight.Add(newprim, offset);
                    offset++;
                }
            }
            
            float occSpacing = (float)(pbTimeline.Height - 20) / (float)offset;
            float occHeight = occSpacing * 0.75f;
            foreach (string s in TimeKeeper.InDate)
            {
                Occurrence o = Occurrence.All[s];
                if (o.Status == OccurrenceStatus.Deleted || o.Status == OccurrenceStatus.Canceled)
                {
                    continue;
                }
                Event p = o.Parent();
                if (!p.Exists)
                {
                    continue;
                }
                tlg.ResetClip();
                Category prim = Category.All[p.PrimaryCategory];
                float ycoord = primCatHeight[p.PrimaryCategory] * occSpacing + 20f;
                //TimeSpan starttime = o.StartActual - TimeKeeper.Begin;
                //TimeSpan endtime = o.EndActual - TimeKeeper.Begin;
                //float startpt = (float)(starttime.TotalMinutes / bte.TotalMinutes);
                //float endpt = (float)(endtime.TotalMinutes / bte.TotalMinutes);
                //startpt *= pbTimeline.Width;
                //endpt *= pbTimeline.Width;
                float startpt = DateTimeToPixel(o.StartActual.ToUniversalTime());
                float endpt = DateTimeToPixel(o.EndActual.ToUniversalTime());
                RectangleF occRect = new RectangleF(startpt, ycoord, endpt - startpt, occHeight);
                tlg.FillRectangle(new SolidBrush(Color.White), occRect);
                tlg.FillRectangle(new SolidBrush(Color.FromArgb(128, prim.Color)), occRect);
                tlg.DrawRectangle(new Pen(new SolidBrush(prim.Color)), occRect.Left, occRect.Top, occRect.Width, occRect.Height);
                tlg.Clip = new Region(occRect);
                PointF occPt = new PointF(occRect.Left + 3, occRect.Top + 3);
                string occCats = String.Join(", ", p.Categories.Select(x => Category.All[x].Name));
                SizeF firstLine = tlg.MeasureString(occCats, this.Font);
                tlg.DrawString(occCats, this.Font, new SolidBrush(Color.Black), occPt);
                string occTimePlace = o.StartActual.ToShortTimeString() + " - " + o.EndActual.ToShortTimeString();
                if (p.Location != "")
                {
                    occTimePlace += " at " + p.Location;
                }
                tlg.DrawString(occTimePlace, this.Font, new SolidBrush(Color.Black), occPt.X, occPt.Y + firstLine.Height);
                tlg.DrawString(p.Name, new Font(this.Font, FontStyle.Bold), new SolidBrush(Color.Black), occPt.X, occPt.Y + firstLine.Height * 2);
                RectangleF occDescRect = new RectangleF(occPt.X, occPt.Y + firstLine.Height * 3, occRect.Width - 3, occRect.Height - (3 + firstLine.Height * 3));
                tlg.DrawString(p.Description, this.Font, new SolidBrush(Color.Black), occDescRect);
            }

            pbTimeline.Image = tl;

            //double assignment
            //CITE: comment on http://stackoverflow.com/a/263590 (#comment15236167_263590)
            pnlTimeline.HorizontalScroll.Value = pnlTimeline.HorizontalScroll.Value = (int)((pnlTimeline.HorizontalScroll.Maximum) * oldpos);
            
            int newScroll = pnlTimeline.HorizontalScroll.Value;

            int newMax = pnlTimeline.HorizontalScroll.Maximum;
            float newpos = newScroll / newMax;
            //MessageBox.Show(oldMax + " " + newMax);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            rtbTask.Rtf = @"{\rtf0\ansi Some \b text \b0 is now bold.}";
            rtbOccurrence.Rtf = @"{\rtf0\ansi But this is \b different \b0 text, somehow.}";
            DrawTimeline();
        }

        private void btnStartOcc_Click(object sender, EventArgs e)
        {
            FormAlarmWindow popup = new FormAlarmWindow();
            popup.ShowDialog();
            //popup.Activate();
        }

        private void btnRescheduleOcc_Click(object sender, EventArgs e)
        {
            FormEventScheduler popup = new FormEventScheduler();
            popup.ShowDialog();
        }

        private void btnNewAppt_Click(object sender, EventArgs e)
        {
            FormAddEvent popup = new FormAddEvent(false);
            popup.ShowDialog();
        }

        private void btnCancelAppts_Click(object sender, EventArgs e)
        {
            FormConflictTwo popup = new FormConflictTwo();
            popup.ShowDialog();
        }

        private void btnCancelTasks_Click(object sender, EventArgs e)
        {
            FormConflictMultiple popup = new FormConflictMultiple();
            popup.ShowDialog();

        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            FormAddCategories popup = new FormAddCategories(true);
            popup.ShowDialog();
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            FormSummaryWindow popup = new FormSummaryWindow();
            popup.ShowDialog();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            DrawTimeline();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
           
            TimeKeeper.ZoomFactor >>= 1;
            DrawTimeline();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
           
            TimeKeeper.ZoomFactor <<= 1;
            DrawTimeline();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            DrawTimeline();
        }

        private void pbTimeline_Click(object sender, EventArgs e)
        {

        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            FormAddEvent popup = new FormAddEvent(true);
            popup.ShowDialog();
        }

        private void tmWhole_Tick(object sender, EventArgs e)
        {
            DrawTimeline();
            Dictionary<DateTime, Occurrence> alarmOccs = TimeKeeper.Alarms.Where(x => (x.Key <= TimeKeeper.Now.ToLocalTime())).ToDictionary(x => x.Key, x => x.Value);
            if (alarmOccs.Count() > 0)
            {
                FormAlarmWindow popup;
                bool isNewForm = false;

                //CITE: http://stackoverflow.com/q/3861602
                //to only open a form if it isn't already open
                popup = (FormAlarmWindow)Application.OpenForms["FormAlarmWindow"];
                if (popup == null)
                {
                    isNewForm = true;
                    popup = new FormAlarmWindow();
                    
                }

                popup.Alarms = alarmOccs;
                popup.UpdateList();
                if (isNewForm)
                {
                    popup.ShowDialog();
                }
               //MessageBox.Show( alarmOccs.ElementAt(0).ToString());
            }
           
            //if (TimeKeeper.Alarms.Count > 0)
            //{
            //    MessageBox.Show(TimeKeeper.Alarms.Count.ToString());
            //}
        }

       
       
    }
}
