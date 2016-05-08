/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The FormMain class is the main form where
 * the user can interact with the Timeline
 * and the Events and Occurrences on it.
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
using System.IO;

namespace Span.GUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        /**
         * Converts the specified DateTime value to a horizontal pixel coordinate for displaying on the Timeline.
         * 
         * pbTimeline must already have a defined width to convert
         * the value.
         * 
         * @param time the DateTime to convert.
         * 
         * @return the horizontal pixel coordinate to display
         * on the Timeline, as a float.
         * 
         * @date April 16, 2016
         */
        public float DateTimeToPixel(DateTime time)
        {
            TimeSpan diff = time - TimeKeeper.Begin;
            float point = (float)(diff.TotalMinutes / (TimeKeeper.End - TimeKeeper.Begin).TotalMinutes);
            point *= pbTimeline.Width;
            return point;
        }

        /**
         * Draws the timeline and updates all form
         * components around it.
         * 
         * @date April 16, 2016
         */
        public void DrawTimeline()
        {
            
            TimeKeeper.Update();
            //minimized window means pbTimeline has no size
            if (WindowState == FormWindowState.Minimized)
            {
                return;
            }
            //get old scrolling value to "transfer" to new value later
            int oldScroll = pnlTimeline.HorizontalScroll.Value;
            int oldMax = pnlTimeline.HorizontalScroll.Maximum;
            float oldpos = (float)oldScroll / (float)oldMax;
            //set up timeline
            m_tl = new Bitmap(pnlTimeline.Width * TimeKeeper.ZoomFactor, pnlTimeline.Height);
            //replace the existing timeline image
            //and get rid of it as soon as possible to prevent out-of-memory
            GC.Collect();
            pbTimeline.Width = pnlTimeline.Width * TimeKeeper.ZoomFactor;
            Graphics tlg = Graphics.FromImage(m_tl);
            tlg.Clear(SystemColors.Window);
            tlg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //draw dashed lines for days, hours, and now
            Pen dayDasher = new Pen(Color.Black);
            dayDasher.DashPattern = new float[] { 8.0f, 2.0f };
            Pen hourDasher = new Pen(Color.Gray);
            hourDasher.DashPattern = new float[] { 2.0f, 2.0f };
            Pen nowDasher = new Pen(Color.Red, 2.0f);
            nowDasher.DashPattern = new float[] { 1.0f, 1.0f };
            //keep track of text regions so none overlap
            Region textAreas = new Region();
            textAreas.MakeEmpty();
            Font boldFont = new Font(this.Font, FontStyle.Bold);
            for (DateTime i = TimeKeeper.Begin.Date; i < TimeKeeper.End; i = i.AddHours(1))
            {
                float ipt = DateTimeToPixel(i);
                //if huge, it can't possibly be on the timeline
                if (float.IsInfinity(ipt))
                {
                    continue;
                }
                
                if (i.ToLocalTime() == i.ToLocalTime().Date)
                {
                    //days
                    tlg.DrawLine(dayDasher, ipt, 0, ipt, pbTimeline.Height);
                    string toPrint = i.ToString("ddd M/d");
                    SizeF tpSize = tlg.MeasureString(toPrint, boldFont);
                    PointF iptText = new PointF(ipt, 3);
                    RectangleF textRect = new RectangleF(iptText, tpSize);
                    tlg.FillRectangle(new SolidBrush(Color.White), textRect);
                    tlg.DrawString(toPrint, boldFont, new SolidBrush(Color.Black), iptText);
                    textAreas.Union(textRect);
                }
                else
                {
                    //hours
                    tlg.DrawLine(hourDasher, ipt, 0, ipt, pbTimeline.Height);
                    string toPrint = i.ToLocalTime().ToString("htt");
                    SizeF tpSize = tlg.MeasureString(toPrint, this.Font);
                    PointF iptText = new PointF(ipt, 3);
                    RectangleF textRect = new RectangleF(iptText, tpSize);
                    if (!textAreas.IsVisible(textRect))
                    {
                        tlg.DrawString(toPrint, this.Font, new SolidBrush(Color.Black), iptText);
                        textAreas.Union(textRect);
                    }
                }
            }
            //now
            float nowpt = DateTimeToPixel(TimeKeeper.Now);
            //again, if huge, it can't possibly be on the timeline
            //so blank it and ask for an actual day
            if (float.IsInfinity(nowpt))
            {
                string badDate = "Please select a single day.";
                SizeF messageSize = tlg.MeasureString(badDate, this.Font);
                tlg.DrawString(badDate, this.Font, new SolidBrush(Color.Black), new PointF((pbTimeline.Width - messageSize.Width) / 2f, 3));
                pbTimeline.Image = m_tl;
                return;
            }
            //offset by 2 pixels vertically so it is easier to see next to hours
            tlg.DrawLine(nowDasher, nowpt, 2, nowpt, pbTimeline.Height);
            //draw black triangle at top
            tlg.FillPolygon(new SolidBrush(Color.Black), new PointF[] { new PointF(nowpt - 10.5f, -0.5f), new PointF(nowpt + 10.5f, -0.5f), new PointF(nowpt, 10.5f) });
            OccurrenceStatus[] noInclude = { OccurrenceStatus.Deleted, OccurrenceStatus.Canceled, OccurrenceStatus.Excluded };
            //define height and position of categories based on how many are on the screen
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
            //draw occurrences
            float occSpacing = (float)(pbTimeline.Height - 20) / (float)offset;
            float occHeight = occSpacing * 0.9f;
            foreach (string s in TimeKeeper.InDate)
            {
                //only draw valid occurrences
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
                //set placement of occurrence
                Category prim = Category.All[p.PrimaryCategory];
                float ycoord = primCatHeight[p.PrimaryCategory] * occSpacing + 20f;
               
                float startpt = DateTimeToPixel(o.StartActual.ToUniversalTime());
                float endpt = DateTimeToPixel(o.EndActual.ToUniversalTime());
                RectangleF occRect = new RectangleF(startpt, ycoord, endpt - startpt, occHeight);
                m_occurrenceGraphics[o.Id] = occRect;
                //do painting itself
                tlg.FillRectangle(new SolidBrush(Color.FromArgb(o.Status == OccurrenceStatus.Ignored ? 128 : 255, Color.White)), occRect);
                tlg.FillRectangle(new SolidBrush(Color.FromArgb(o.Status == OccurrenceStatus.Ignored ? 32 : 128, prim.Color)), occRect);
                tlg.DrawRectangle(new Pen(new SolidBrush(prim.Color)), occRect.Left, occRect.Top, occRect.Width, occRect.Height);
                tlg.Clip = new Region(occRect);
                //add text
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
            //draw selected occurrence if it is on screen
            //cannot keep selecting something that isn't visible
            if (m_selected != "" && noInclude.Contains(Occurrence.All[m_selected].Status))
            {
                m_selected = "";
            }
            if (m_selected != "" && m_occurrenceGraphics.ContainsKey(m_selected))
            {
                RectangleF selRect = m_occurrenceGraphics[m_selected];
                tlg.DrawRectangle(new Pen(new SolidBrush(Color.Red), 4.0f), selRect.Left, selRect.Top, selRect.Width, selRect.Height);
            }
            pbTimeline.Image = m_tl;

            //transfer old scrolling value and adjust for new size
            //this is not exact because there is no way to tell
            //the width of the scrollbar "thumb", so it just
            //left-aligns the thumb.
            //Note: this uses double assignment due to a glitch in .NET framework.
            //taken from a comment on http://stackoverflow.com/a/263590 (#comment15236167_263590)
            pnlTimeline.HorizontalScroll.Value = pnlTimeline.HorizontalScroll.Value = (int)((pnlTimeline.HorizontalScroll.Maximum) * oldpos);
            int newScroll = pnlTimeline.HorizontalScroll.Value;
            int newMax = pnlTimeline.HorizontalScroll.Maximum;
            float newpos = newScroll / newMax;
            //everything else
            UpdateNow();
            UpdateSides();
        }

        /**
         * Update the left and right sidebars to reflect the currently-selected Occurrence.
         * 
         * @date April 26, 2016
         */
        private void UpdateSides()
        {
            //disable if nothing is selected
            if (m_selected == "")
            {
                gbEvent.Enabled = false;
                gbOccurrence.Enabled = false;
                rtbTask.Text = "";
                rtbOccurrence.Text = "";
                pbEventColor.BackColor = SystemColors.ControlDark;
                return;
            }
            //enable if occurrence is selected
            Occurrence occ =  Occurrence.All[m_selected];
            Event p = occ.Parent();
            gbEvent.Enabled = true;
            gbOccurrence.Enabled = true;
            //but leave some buttons disabled if ignored
            bool ignored = (occ.Status == OccurrenceStatus.Ignored);
            btnStartOcc.Enabled = !ignored;
            btnStopOcc.Enabled = (!ignored && TimeKeeper.Current.Contains(occ.Id));
            btnPostponeOcc.Enabled = !ignored;
            btnIgnoreOcc.Enabled = !ignored;
            //add task name, description, etc to left pane
            rtbTask.Text = "";
            if (Occurrence.DebugMode)
            {
                rtbTask.AppendText(m_selected);
            }
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
            //add occurrence time info to right pane
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

        /**
         * Check the specified event to see if other events overlap, and alert the user if so.
         * 
         * @param p the Event to check for overlapping.
         * 
         * @date April 29, 2016
         */
        public static void CheckOverlapping(Event p)
        {
            Tuple<uint, List<string>> overlapping = p.GetOverlapping();
            if (overlapping.Item1 > 1)
            {
                FormConflictMultiple popup = new FormConflictMultiple();
                popup.ConflictingEvents = overlapping.Item2;
                popup.NewEvent = p;
                popup.ShowDialog();
            }
            else if (overlapping.Item1 == 1)
            {
                FormConflictTwo popup = new FormConflictTwo();
                popup.NewOccurrence = Occurrence.All[overlapping.Item2[1]];
                popup.OldOccurrence = Occurrence.All[overlapping.Item2[2]];
                popup.ShowDialog();
            }
        }

        /**
         * Update the bottom pane to reflect the Occurrences currently happening.
         * 
         * @date April 24, 2016
         */
        private void UpdateNow()
        {
            //divide into new (to add) occurrences, existing occurrences,
            //and old (to remove) occurrences.
            var curnew = TimeKeeper.Current.Where(x => !m_current.Contains(x));
            var curold = m_current.Where(x => !TimeKeeper.Current.Contains(x)).ToList();
            
            //add new ones on the right
            foreach (string occ in curnew)
            {
                m_currentView.Add(occ, new RichTextBox());
                m_currentView[occ].Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                m_currentView[occ].ReadOnly = true;
                m_currentView[occ].Cursor = this.Cursor;
                tlpNow.Controls.Add(m_currentView[occ]);
                m_currentView[occ].Click += new EventHandler(rtbOccurrenceNow_Click);
                m_current.Add(occ);
            }
            //delete old ones
            foreach (string occ in curold)
            {
                tlpNow.Controls.Remove(m_currentView[occ]);
                m_currentView.Remove(occ);
                m_current.Remove(occ);
               
            }
            //update all
            foreach (string occ in m_current)
            {
                //update color
                Occurrence thisOcc = Occurrence.All[occ];
                Color thisColor = Category.All[thisOcc.Parent().PrimaryCategory].Color;
                m_currentView[occ].BackColor = ControlPaint.LightLight(thisColor);
                m_currentView[occ].ForeColor = ControlPaint.DarkDark(thisColor);
                //update text
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
                //update if selected or not
                m_currentView[occ].BorderStyle = occ.Equals(m_selected) ? BorderStyle.Fixed3D : BorderStyle.None;
                m_currentView[occ].SelectionStart = 0;
                //deselect control itself
                m_currentView[occ].Enabled = false;
                m_currentView[occ].Enabled = true;
            }
            //attempt to keep sizes of controls consistent
            foreach (ColumnStyle tlpcs in tlpNow.ColumnStyles)
            {
                tlpcs.SizeType = SizeType.Percent;
                tlpcs.Width = 100 / tlpNow.ColumnCount;
            }
        }

        /**
         * Prompts the user to dechain the Occurrence, if necessary.
         * 
         * @param the specified Occurrence.
         * 
         * @return true if the Occurrence is not chained
         * (eg, if it was never chained or if it has been
         * dechained) and false otherwise. This means that
         * if false is returned, the user has effectively
         * canceled the action.
         * 
         * @date April 27, 2016
         */
        public static bool DeChainIfChained(Occurrence o)
        {
            if (o.IsChained())
            {
                DialogResult d = MessageBox.Show(o.Parent().Name + " at " + o.StartActual.ToShortTimeString() + " appears to be part of a periodic set. "
                     + "If you wish to edit the occurrence's schedule, it will become detached from the rest of the set. Proceed?", "", MessageBoxButtons.YesNo);
                if (!d.Equals(DialogResult.Yes))
                {
                    return false;
                }
                o.DeChain();
            }
            return true;
        }

        /**
         * Selects the specified Occurrence RichTextBox control in the bottom pane.
         * 
         * @date April 24, 2016
         */
        private void rtbOccurrenceNow_Click(object sender, System.EventArgs e)
        {
            string selOcc = m_currentView.First(x => x.Value.Equals(sender)).Key;
            m_selected = selOcc;
            DrawTimeline();
        }

        /**
         * Initializes the form. If the schedule doesn't exist, prompts the user to add categories.
         * 
         * @date April 16, 2016
         */
        private void FormMain_Load(object sender, EventArgs e)
        {
            m_selected = "";
            m_occurrenceGraphics = new Dictionary<string, RectangleF>();
            m_current = new List<string>();
            m_currentView = new Dictionary<string, RichTextBox>();
            DrawTimeline();
            if (Category.All.Count() < 1)
            {
                MessageBox.Show("It appears you have not used armspan before. Please add at least one category before scheduling any events.");
                FormAddCategories popup = new FormAddCategories(true);
                popup.ShowDialog();
            }
        }

        /**
         * Starts the selected Occurrence now.
         * 
         * @date April 27, 2016
         */
        private void btnStartOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            if (!DeChainIfChained(o))
            {
                return;
            }
            o.StartNow();
            DrawTimeline();
        }

        /**
         * Reschedules the selected Occurrence.
         * 
         * @date April 26, 2016
         */
        private void btnRescheduleOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            if (!DeChainIfChained(o))
            {
                return;
            }
            FormOccurrenceScheduler popup = new FormOccurrenceScheduler();
            popup.Single = o;
            popup.ShowDialog();
            CheckOverlapping(o.Parent());
            DrawTimeline();
        }

        /**
         * Prompts the user to add a new appointment.
         * 
         * @date April 12, 2016
         */
        private void btnNewAppt_Click(object sender, EventArgs e)
        {
            FormAddEvent popup = new FormAddEvent(false);
            popup.ShowDialog();
        }

        /**
         * Cancels all current appointments.
         * 
         * @date April 12, 2016
         */
        private void btnCancelAppts_Click(object sender, EventArgs e)
        {
            foreach (Occurrence occ in TimeKeeper.Current.Select(x => Occurrence.All[x]))
            {
                if (!DeChainIfChained(occ))
                {
                    continue;
                }
                occ.Cancel();
            }
            DrawTimeline();
        }

        /**
         * Lets the user add or edit categories.
         * 
         * @date April 13, 2016
         */
        private void btnCategories_Click(object sender, EventArgs e)
        {
            FormAddCategories popup = new FormAddCategories(true);
            popup.ShowDialog();
        }

        /**
         * Sets the timeline display to now.
         * 
         * @date April 29, 2016
         */
        private void btnNow_Click(object sender, EventArgs e)
        {
            TimeKeeper.ViewingNow = true;
            DrawTimeline();
        }

        /**
         * Prompts the user to pick categories, then gives a summary of events.
         * 
         * @date April 30, 2016
         */
        private void btnSummary_Click(object sender, EventArgs e)
        {
            FormAddCategories popupcat = new FormAddCategories(false);
            popupcat.Checked = new List<int>();
            popupcat.ShowDialog();
            FormSummaryWindow popup = new FormSummaryWindow();
            List<string> cats = new List<string>();
            foreach (int index in popupcat.Checked)
            {
                cats.Add(Category.All.First(x => x.Value.Number == index + 1).Key);
            }
            popup.Categories = cats;
            popup.ShowDialog();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            DrawTimeline();
        }

        /**
         * Zooms the timeline out.
         * 
         * @date April 16, 2016
         */
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            TimeKeeper.ZoomFactor >>= 1;
            DrawTimeline();
        }

        /**
         * Zooms the timeline in.
         * 
         * @date April 16, 2016
         */
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            TimeKeeper.ZoomFactor <<= 1;
            DrawTimeline();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            DrawTimeline();
        }

        /**
         * Selects the Occurrence on the timeline on which the user is clicking.
         * 
         * @date April 23, 2016
         */
        private void pbTimeline_Click(object sender, EventArgs e)
        {
            try
            {
                string occ = m_occurrenceGraphics.First(x => Rectangle.Round(x.Value).Contains(pbTimeline.PointToClient(MousePosition))).Key;
                m_selected = occ;
            }
            catch (InvalidOperationException)
            {
                m_selected = "";
            }
            DrawTimeline();
        }

        /**
         * Prompts the user to add a new task.
         * 
         * @date April 12, 2016
         */
        private void btnNewTask_Click(object sender, EventArgs e)
        {
            FormAddEvent popup = new FormAddEvent(true);
            popup.ShowDialog();
        }

        /**
         * Updates the timeline and displays alarms every ten seconds.
         * 
         * @date April 22, 2016
         */
        private void tmWhole_Tick(object sender, EventArgs e)
        {
            DrawTimeline();
            //open alarm window if necessary
            if (TimeKeeper.Alarms.Count > 0)
            {
                DateTime nextAlarm = TimeKeeper.Alarms.ElementAt(0).Key;
                lblAlarmTime.Text = "Next alarm on: " + nextAlarm.ToShortDateString() + " at " + nextAlarm.ToShortTimeString();
            }
            else
            {
                lblAlarmTime.Text = "No alarms expected";
            }
            Dictionary<DateTime, Occurrence> alarmOccs = TimeKeeper.Alarms.Where(x => (x.Key <= TimeKeeper.Now.ToLocalTime())).ToDictionary(x => x.Key, x => x.Value);
            if (alarmOccs.Count() > 0)
            {
                FormAlarmWindow popup;
                bool isNewForm = false;
                //to only open a form if it isn't already open
                //taken from http://stackoverflow.com/q/3861602
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
            }
        }

        /**
         * Stops the selected Occurrence now.
         * 
         * @date April 26, 2016
         */
        private void btnStopOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            if (!DeChainIfChained(o))
            {
                return;
            }
            o.StopNow();
            DrawTimeline();
        }

        /**
         * Postpones the selected Occurrence.
         * 
         * @date April 26, 2016
         */
        private void btnPostponeOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            if (!DeChainIfChained(o))
            {
                return;
            }
            o.Postpone((uint)nudPostponeOcc.Value);
            DrawTimeline();
        }

        /**
         * Ignores the selected Occurrence.
         * 
         * @date April 26, 2016
         */
        private void btnIgnoreOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            if (!DeChainIfChained(o))
            {
                return;
            }
            o.Ignore();
            DrawTimeline();
        }

        /**
         * Cancels the selected Occurrence.
         * 
         * @date April 26, 2016
         */
        private void btnCancelOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            if (!DeChainIfChained(o))
            {
                return;
            }
            o.Cancel();
            m_selected = "";
            DrawTimeline();
        }

        /**
         * Deletes the selected Occurrence.
         * 
         * @date April 26, 2016
         */
        private void btnDeleteOcc_Click(object sender, EventArgs e)
        {
            Occurrence o = Occurrence.All[m_selected];
            if (!DeChainIfChained(o))
            {
                return;
            }
            DialogResult d = MessageBox.Show("Are you sure you want to delete this occurrence of \"" + o.Parent().Name + "\"?\nThis cannot be undone.", "", MessageBoxButtons.YesNo);
            if (!d.Equals(DialogResult.Yes))
            {
                return;
            }
            o.Delete();
            m_selected = "";
            DrawTimeline();
        }

        /**
         * Prompts the user to edit the selected Event.
         * 
         * @date April 26, 2016
         */
        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            Event p = Occurrence.All[m_selected].Parent();
            FormAddEvent popup = new FormAddEvent(p);
            m_selected = "";
            popup.ShowDialog();
            CheckOverlapping(p);
            DrawTimeline();
        }

        /**
         * Deletes the selected Event.
         * 
         * @date April 27, 2016
         */
        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            Event p = Occurrence.All[m_selected].Parent();
            DialogResult d = MessageBox.Show("Are you sure you want to delete \"" + p.Name + "\"?\nThis cannot be undone.", "", MessageBoxButtons.YesNo);
            if (!d.Equals(DialogResult.Yes))
            {
                return;
            }
            p.Exists = false;
            m_selected = "";
            DrawTimeline();
        }

        /**
         * Changes the date displayed on the timeline.
         * 
         * @date April 29, 2016
         */
        private void mCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (mCalendar.SelectionStart.ToUniversalTime().Date.Equals(TimeKeeper.Now.Date))
            {
                TimeKeeper.ViewingNow = true;
            }
            else
            {
                TimeKeeper.ViewingNow = false;
                TimeKeeper.Begin = mCalendar.SelectionStart.ToUniversalTime();
                TimeKeeper.End = mCalendar.SelectionEnd.ToUniversalTime();
            }
            m_occurrenceGraphics.Clear();
            DrawTimeline();
        }

        /**
         * Saves the schedule.
         * 
         * Note: This can still occur when the program crashes.
         * 
         * @date May 1, 2016
         */
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(Program.SaveFilename, JSONCapable.SaveState());
        }

        /**
         * Stops all current appointments.
         * 
         * @date April 12, 2016
         */
        private void btnStopAppts_Click(object sender, EventArgs e)
        {
            foreach (Occurrence occ in TimeKeeper.Current.Select(x=> Occurrence.All[x]))
            {
                if (!DeChainIfChained(occ))
                {
                    continue;
                }
                occ.StopNow();
            }
            DrawTimeline();
        }

        /**
         * Starts an appointment now, then prompts the user to edit it.
         * 
         * @date May 2, 2016
         */
        private void btnStartAppt_Click(object sender, EventArgs e)
        {
            Event p = new Event(false, "Automated Appointment", new List<Occurrence>(), new List<Period>(), "", new List<string>(), new AlarmSettings(""), "");
            p.Alarms.ParentId = p.Id;
            p.PrimaryCategory = Category.All.Keys.ElementAt(0);
            Occurrence o = new Occurrence(false, TimeKeeper.Now.ToLocalTime(), TimeKeeper.Now.ToLocalTime().AddHours(1), p.Id);
            p.ManualOccurrences.Add(o);
            DrawTimeline();
            FormAddEvent popup = new FormAddEvent(p);
            m_selected = "";
            popup.ShowDialog();
            CheckOverlapping(p);
            DrawTimeline();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("armspan (A Calendar Program)\nTrack your past, present, and future\n\nCreated by Allan Wiener\nfor CMPS 450 - Senior Project\nat Ramapo College of New Jersey\nSpring 2016");
        }

        /**
         * The list of Occurrences happening now, as listed in the bottom pane.
         */
        private List<string> m_current;
        /**
         * The list of Occurrences happening now, encapsulated in RichTextBoxes in the bottom pane.
         */
        private Dictionary<string, RichTextBox> m_currentView;

        /**
         * A dictionary containing Occurrence ids as keys and their corresponding RectangleF on the timeline as values.
         */
        private Dictionary<string, RectangleF> m_occurrenceGraphics;
        /**
         * The id of the selected Occurrence, or an empty string if nothing is selected.
         */
        private string m_selected;
        /**
         * The current timeline image.
         */
        private Bitmap m_tl;
    }
}
