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
            m_tl = new Bitmap(pnlTimeline.Width * TimeKeeper.ZoomFactor, pnlTimeline.Height);
            //replace the existing timeline image
            //and get rid of it as soon as possible to prevent out-of-memory
            GC.Collect();
            pbTimeline.Width = pnlTimeline.Width * TimeKeeper.ZoomFactor;
            Graphics tlg = Graphics.FromImage(m_tl);
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
            OccurrenceStatus[] noInclude = { OccurrenceStatus.Deleted, OccurrenceStatus.Canceled, OccurrenceStatus.Excluded };
            Dictionary<string, int> primCatHeight = new Dictionary<string, int>();
            int offset = 0;
            foreach (string s in TimeKeeper.InDate)
            {
                Occurrence o = Occurrence.All[s];
                if (noInclude.Contains(o.Status))
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
            float occHeight = occSpacing * 0.9f;
            foreach (string s in TimeKeeper.InDate)
            {
                Occurrence o = Occurrence.All[s];
                if (noInclude.Contains(o.Status))
                {
                    if (m_occurrenceGraphics.ContainsKey(s))
                    {
                        m_occurrenceGraphics.Remove(s);
                    }
                    continue;
                }
                Event p = o.Parent();
                if (!p.Exists)
                {
                    if (m_occurrenceGraphics.ContainsKey(s))
                    {
                        m_occurrenceGraphics.Remove(s);
                    }
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
                m_occurrenceGraphics[o.Id] = occRect;
                
                tlg.FillRectangle(new SolidBrush(Color.FromArgb(o.Status == OccurrenceStatus.Ignored ? 128 : 255, Color.White)), occRect);
                tlg.FillRectangle(new SolidBrush(Color.FromArgb(o.Status == OccurrenceStatus.Ignored ? 32 : 128, prim.Color)), occRect);
                tlg.DrawRectangle(new Pen(new SolidBrush(prim.Color)), occRect.Left, occRect.Top, occRect.Width, occRect.Height);
                tlg.Clip = new Region(occRect);
                PointF occPt = new PointF(occRect.Left + 3, occRect.Top + 3);
                string occCats = String.Join(", ", p.Categories.Select(x => Category.All[x].Name));
                SizeF firstLine = tlg.MeasureString(occCats, this.Font);
                SolidBrush occFntColor = new SolidBrush(Color.FromArgb(o.Status == OccurrenceStatus.Ignored ? 128 : 255, Color.Black));
                tlg.DrawString(occCats, this.Font, occFntColor, occPt);
                string occTimePlace = o.StartActual.ToShortTimeString() + " - " + o.EndActual.ToShortTimeString();
                if (p.Location != "")
                {
                    occTimePlace += " at " + p.Location;
                }
                tlg.DrawString(occTimePlace, this.Font, occFntColor, occPt.X, occPt.Y + firstLine.Height);
                tlg.DrawString(p.Name, new Font(this.Font, FontStyle.Bold), occFntColor, occPt.X, occPt.Y + firstLine.Height * 2);
                RectangleF occDescRect = new RectangleF(occPt.X, occPt.Y + firstLine.Height * 3, occRect.Width - 3, occRect.Height - (3 + firstLine.Height * 3));
                tlg.DrawString(p.Description, this.Font, occFntColor, occDescRect);
            }
            tlg.ResetClip();
            if (m_selected != "")
            {
                RectangleF selRect = m_occurrenceGraphics[m_selected];
                tlg.DrawRectangle(new Pen(new SolidBrush(Color.Red), 4.0f), selRect.Left, selRect.Top, selRect.Width, selRect.Height);
            }
            

            pbTimeline.Image = m_tl;

            //double assignment
            //CITE: comment on http://stackoverflow.com/a/263590 (#comment15236167_263590)
            pnlTimeline.HorizontalScroll.Value = pnlTimeline.HorizontalScroll.Value = (int)((pnlTimeline.HorizontalScroll.Maximum) * oldpos);
            
            int newScroll = pnlTimeline.HorizontalScroll.Value;

            int newMax = pnlTimeline.HorizontalScroll.Maximum;
            float newpos = newScroll / newMax;
            //MessageBox.Show(oldMax + " " + newMax);
            UpdateNow();
            UpdateSides();
        }

        private void UpdateSides()
        {
            if (m_selected == "")
            {
                gbEvent.Enabled = false;
                gbOccurrence.Enabled = false;
                rtbTask.Text = "";
                rtbOccurrence.Text = "";
                pbEventColor.BackColor = SystemColors.ControlDark;
                return;
            }
            Occurrence occ =  Occurrence.All[m_selected];
            Event p = occ.Parent();
            gbEvent.Enabled = true;
            gbOccurrence.Enabled = true;
            rtbTask.Text = "";
            rtbTask.SelectionFont = new Font(rtbTask.Font, FontStyle.Bold);
            rtbTask.AppendText(p.Name);
            rtbTask.SelectionFont = rtbTask.Font;
            rtbTask.AppendText("\n");
            if (!p.Location.Equals(""))
            {
                rtbTask.AppendText("at " + p.Location + "\n");
            }
            rtbTask.AppendText(p.Description);
            pbEventColor.BackColor = Category.All[p.PrimaryCategory].Color;
            rtbOccurrence.Text = "";
            Font boldOcc = new Font(rtbOccurrence.Font, FontStyle.Bold);
            rtbOccurrence.SelectionFont = boldOcc;
            rtbOccurrence.AppendText("Starts: ");
            rtbOccurrence.SelectionFont = rtbOccurrence.Font;
            rtbOccurrence.AppendText(occ.StartActual.ToShortDateString() + ", " + occ.StartActual.ToShortTimeString() + "\n");
            rtbOccurrence.SelectionFont = boldOcc;
            rtbOccurrence.AppendText("Ends: ");
            rtbOccurrence.SelectionFont = rtbOccurrence.Font;
            rtbOccurrence.AppendText(occ.EndActual.ToShortDateString() + ", " + occ.EndActual.ToShortTimeString() + "\n");
        }

        private void UpdateNow()
        {
            
            
            var curnew = TimeKeeper.Current.Where(x => !m_current.Contains(x));
            var curold = m_current.Where(x => !TimeKeeper.Current.Contains(x)).ToList();
            
            //rtbOccurrence.Text = "New:" + curnew.Count() + "\n";
            foreach (string occ in curnew)
            {
                m_currentView.Add(occ, new RichTextBox());
                
                //rtbOccurrence.Text += Occurrence.All[occ].Parent().Name + "\n";
                m_currentView[occ].Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                m_currentView[occ].ReadOnly = true;
                m_currentView[occ].Cursor = this.Cursor;
                

                tlpNow.Controls.Add(m_currentView[occ]);
                m_currentView[occ].Click += new EventHandler(rtbOccurrenceNow_Click);
                
               
                m_current.Add(occ);
            }
            //rtbOccurrence.Text += "Old:" + curold.Count() + "\n";
            foreach (string occ in curold)
            {
                tlpNow.Controls.Remove(m_currentView[occ]);
                m_currentView.Remove(occ);
                m_current.Remove(occ);
                //rtbOccurrence.Text += Occurrence.All[occ].Parent().Name + "\n";
            }
            //rtbOccurrence.Text += "Current:" + m_current.Count + "\n";
            foreach (string occ in m_current)
            {
                Occurrence thisOcc = Occurrence.All[occ];
                Color thisColor = Category.All[thisOcc.Parent().PrimaryCategory].Color;
                m_currentView[occ].BackColor = ControlPaint.LightLight(thisColor);
                m_currentView[occ].ForeColor = ControlPaint.DarkDark(thisColor);
                string occCats = String.Join(", ", thisOcc.Parent().Categories.Select(x => Category.All[x].Name));
                m_currentView[occ].Text = occCats + "\n";
                m_currentView[occ].AppendText(thisOcc.StartActual.ToShortTimeString() + " - " + thisOcc.EndActual.ToShortTimeString());
                if (thisOcc.Parent().Location != "")
                {
                    m_currentView[occ].AppendText(" at " + thisOcc.Parent().Location);
                }
                m_currentView[occ].AppendText("\n");
                m_currentView[occ].SelectionFont = new Font(m_currentView[occ].Font, FontStyle.Bold);
                m_currentView[occ].AppendText(thisOcc.Parent().Name);
                m_currentView[occ].SelectionFont = m_currentView[occ].Font;
                m_currentView[occ].AppendText("\n" + thisOcc.Parent().Description);
                //rtbOccurrence.Text += Occurrence.All[occ].Parent().Name + "\n";
                m_currentView[occ].BorderStyle = occ.Equals(m_selected) ? BorderStyle.Fixed3D : BorderStyle.None;
                m_currentView[occ].SelectionStart = 0;
                m_currentView[occ].Enabled = false;
                m_currentView[occ].Enabled = true;
                
                
            }
            foreach (ColumnStyle tlpcs in tlpNow.ColumnStyles)
            {
                tlpcs.SizeType = SizeType.Percent;
                tlpcs.Width = 100 / tlpNow.ColumnCount;
            }
        }

        private void rtbOccurrenceNow_Click(object sender, System.EventArgs e)
        {
            string selOcc = m_currentView.First(x => x.Value.Equals(sender)).Key;
            m_selected = selOcc;
            DrawTimeline();
        }

        private List<string> m_current;
        private Dictionary<string, RichTextBox> m_currentView;

        private void FormMain_Load(object sender, EventArgs e)
        {
            rtbTask.Rtf = @"{\rtf0\ansi Some \b text \b0 is now bold.}";
            rtbOccurrence.Rtf = @"{\rtf0\ansi But this is \b different \b0 text, somehow.}";
            m_selected = "";
            m_occurrenceGraphics = new Dictionary<string, RectangleF>();
            m_current = new List<string>();
            m_currentView = new Dictionary<string, RichTextBox>();
            DrawTimeline();
        }

        private void btnStartOcc_Click(object sender, EventArgs e)
        {
            Occurrence.All[m_selected].StartNow();
            DrawTimeline();
        }

        private void btnRescheduleOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            if (o.IsChained())
            {
                DialogResult d = MessageBox.Show("This Occurrence appears to be part of a periodic set. "
                     + "If you wish to edit the occurrence's schedule, it will become detached from the rest of the set. Proceed?", "", MessageBoxButtons.YesNo);
                if (!d.Equals(DialogResult.Yes))
                {
                    return;
                }
                o.DeChain();
            }
            FormOccurrenceScheduler popup = new FormOccurrenceScheduler();
            popup.Single = o;
            popup.ShowDialog();
            DrawTimeline();
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

        private Dictionary<string, RectangleF> m_occurrenceGraphics;
        private string m_selected;
        private Bitmap m_tl;

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
            try
            {
                string occ = m_occurrenceGraphics.First(x => Rectangle.Round(x.Value).Contains(pbTimeline.PointToClient(MousePosition))).Key;
                m_selected = occ;
                //MessageBox.Show(Occurrence.All[occ].Parent().Name);
            }
            catch (InvalidOperationException)
            {
                m_selected = "";
            }
            DrawTimeline();
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

        private void btnStopOcc_Click(object sender, EventArgs e)
        {
            Occurrence.All[m_selected].StopNow();
            DrawTimeline();
        }

        private void btnPostponeOcc_Click(object sender, EventArgs e)
        {
            Occurrence.All[m_selected].Postpone((uint)nudPostponeOcc.Value);
            DrawTimeline();
        }

        private void btnIgnoreOcc_Click(object sender, EventArgs e)
        {
            Occurrence.All[m_selected].Ignore();
            DrawTimeline();
        }

        private void btnCancelOcc_Click(object sender, EventArgs e)
        {
            Occurrence.All[m_selected].Cancel();
            m_selected = "";
            DrawTimeline();
        }

        private void btnDeleteOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            DialogResult d = MessageBox.Show("Are you sure you want to delete this occurrence of \"" + o.Parent().Name + "\"?\nThis cannot be undone.", "", MessageBoxButtons.YesNo);
            if (!d.Equals(DialogResult.Yes))
            {
                return;
            }
            o.Delete();
            m_selected = "";
            DrawTimeline();
        }

        private void btnAddOcc_Click(object sender, EventArgs e)
        {

        }

        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            FormAddEvent popup = new FormAddEvent(Occurrence.All[m_selected].Parent());
            popup.ShowDialog();
            DrawTimeline();
        }

       
       
    }

}
