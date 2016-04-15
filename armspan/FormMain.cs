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

        private void FormMain_Load(object sender, EventArgs e)
        {
            rtbTask.Rtf = @"{\rtf0\ansi Some \b text \b0 is now bold.}";
            rtbOccurrence.Rtf = @"{\rtf0\ansi But this is \b different \b0 text, somehow.}";
            TimeKeeper.Update();

            //temp draw timeline
            Bitmap tl = new Bitmap(pbTimeline.Width, pbTimeline.Height);
            Graphics tlg = Graphics.FromImage(tl);
            tlg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            TimeSpan bte = TimeKeeper.End - TimeKeeper.Begin;
            Pen dayDasher = new Pen(Color.Black);
            dayDasher.DashPattern = new float[]{8.0f, 2.0f};
            Pen hourDasher = new Pen(Color.Gray);
            hourDasher.DashPattern = new float[] {2.0f, 2.0f};
            Pen nowDasher = new Pen(Color.Red, 2.0f);
            nowDasher.DashPattern = new float[] {1.0f, 1.0f };
            for (DateTime i = TimeKeeper.Begin.Date; i < TimeKeeper.End; i = i.AddHours(1))
            {
                TimeSpan ist = i - TimeKeeper.Begin;
                float ipt = (float)(ist.TotalMinutes / bte.TotalMinutes);
                ipt *= pbTimeline.Width;
                if (i.ToLocalTime() == i.ToLocalTime().Date)
                {
                    tlg.DrawLine(dayDasher, ipt, 0, ipt, pbTimeline.Height);
                }
                else
                {
                    tlg.DrawLine(hourDasher, ipt, 0, ipt, pbTimeline.Height);
                }
                
            }
            TimeSpan nowst = TimeKeeper.Now - TimeKeeper.Begin;
            float nowpt = (float)(nowst.TotalMinutes / bte.TotalMinutes);
            nowpt *= pbTimeline.Width;
            tlg.DrawLine(nowDasher, nowpt, 0, nowpt, pbTimeline.Height);
            tlg.FillPolygon(new SolidBrush(Color.Black), new PointF[] {new PointF(nowpt - 10.5f, -0.5f), new PointF(nowpt + 10.5f, -0.5f), new PointF(nowpt, 10.5f) });
            foreach (string s in TimeKeeper.InDate)
            {
                Occurrence o = Occurrence.All[s];
                Event p = o.Parent();
                Category prim = Category.All[p.PrimaryCategory];
                
                TimeSpan starttime = o.StartActual - TimeKeeper.Begin;
                TimeSpan endtime = o.EndActual - TimeKeeper.Begin;
                float startpt = (float)(starttime.TotalMinutes / bte.TotalMinutes);
                float endpt = (float)(endtime.TotalMinutes / bte.TotalMinutes);
                startpt *= pbTimeline.Width;
                endpt *= pbTimeline.Width;
                tlg.FillRectangle(new SolidBrush(Color.White), startpt, 20f, endpt - startpt, 40f);
                tlg.FillRectangle(new SolidBrush(Color.FromArgb(128,prim.Color)), startpt, 20f, endpt - startpt, 40f);
                tlg.DrawRectangle(new Pen(new SolidBrush(prim.Color)), startpt, 20f, endpt - startpt, 40f);
            }
            
            pbTimeline.Image = tl;
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
            FormAddEvent popup = new FormAddEvent();
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
            FormAddCategories popup = new FormAddCategories();
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
          
        }

        

       
    }
}
