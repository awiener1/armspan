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
    public partial class FormConflictTwo : ThinDialog
    {
        public FormConflictTwo()
        {
            InitializeComponent();
        }

        private void FormConflictTwo_Load(object sender, EventArgs e)
        {
            Event newEv = m_newOcc.Parent();
            Event oldEv = m_oldOcc.Parent();
            rtbConflict.Text = "";
            Font rtbBold = new Font(rtbConflict.Font, FontStyle.Bold);
            rtbConflict.SelectionAlignment = HorizontalAlignment.Center;
            rtbConflict.SelectionFont = rtbBold;
            rtbConflict.AppendText(newEv.Name);
            rtbConflict.SelectionFont = rtbConflict.Font;
            rtbConflict.AppendText(" conflicts with ");
            rtbConflict.SelectionFont = rtbBold;
            rtbConflict.AppendText(oldEv.Name);
            rtbConflict.SelectionFont = rtbConflict.Font;
            rtbConflict.AppendText(".");
            //in case font changes
            rtbBold = new Font(rtbNew.Font, FontStyle.Bold);
            rtbNew.Text = "";
            rtbNew.SelectionFont = rtbBold;
            rtbNew.AppendText(newEv.Name);
            rtbNew.SelectionFont = rtbNew.Font;
            rtbNew.AppendText("\n");
            rtbNew.AppendText(NewOccurrence.StartActual.ToShortDateString() + " " + NewOccurrence.StartActual.ToShortTimeString() + " to ");
            rtbNew.AppendText(NewOccurrence.EndActual.ToShortDateString() + " " + NewOccurrence.EndActual.ToShortTimeString());
            if (!newEv.Location.Equals(""))
            {
                rtbNew.AppendText(" at " + newEv.Location);
            }
            rtbNew.AppendText("\n" + string.Join(", ", newEv.Categories.Select(x => Category.All[x].Name)) + "\n");
            rtbNew.AppendText(newEv.Description);
            Color newColor = Category.All[newEv.PrimaryCategory].Color;
            rtbNew.BackColor = ControlPaint.LightLight(newColor);
            pnlNew.BackColor = newColor;

            //in case font changes
            rtbBold = new Font(rtbOld.Font, FontStyle.Bold);
            rtbOld.Text = "";
            rtbOld.SelectionFont = rtbBold;
            rtbOld.AppendText(oldEv.Name);
            rtbOld.SelectionFont = rtbOld.Font;
            rtbOld.AppendText("\n");
            rtbOld.AppendText(OldOccurrence.StartActual.ToShortDateString() + " " + OldOccurrence.StartActual.ToShortTimeString() + " to ");
            rtbOld.AppendText(OldOccurrence.EndActual.ToShortDateString() + " " + OldOccurrence.EndActual.ToShortTimeString());
            if (!oldEv.Location.Equals(""))
            {
                rtbOld.AppendText(" at " + oldEv.Location);
            }
            rtbOld.AppendText("\n" + string.Join(", ", oldEv.Categories.Select(x => Category.All[x].Name)) + "\n");
            rtbOld.AppendText(oldEv.Description);
            Color oldColor = Category.All[oldEv.PrimaryCategory].Color;
            rtbOld.BackColor = ControlPaint.LightLight(oldColor);
            pnlOld.BackColor = oldColor;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelNew_Click(object sender, EventArgs e)
        {
            
            if (!FormMain.DeChainIfChained(NewOccurrence))
            {
                return;
            }
            NewOccurrence.Cancel();
            this.Close();
        }

        private void btnIgnoreNew_Click(object sender, EventArgs e)
        {
            if (!FormMain.DeChainIfChained(NewOccurrence))
            {
                return;
            }
            NewOccurrence.Ignore();
            this.Close();
        }

        private void btnDeleteNew_Click(object sender, EventArgs e)
        {
            if (!FormMain.DeChainIfChained(NewOccurrence))
            {
                return;
            }
            DialogResult d = MessageBox.Show("Are you sure you want to delete?\nThis cannot be undone.", "", MessageBoxButtons.YesNo);
            if (!d.Equals(DialogResult.Yes))
            {
                return;
            }
            NewOccurrence.Delete();
            this.Close();
        }

        private void btnCancelOld_Click(object sender, EventArgs e)
        {
            if (!FormMain.DeChainIfChained(OldOccurrence))
            {
                return;
            }
            OldOccurrence.Cancel();
            this.Close();
        }

        private void btnIgnoreOld_Click(object sender, EventArgs e)
        {
            if (!FormMain.DeChainIfChained(OldOccurrence))
            {
                return;
            }
            OldOccurrence.Ignore();
            this.Close();
        }

        private void btnDeleteOld_Click(object sender, EventArgs e)
        {
            if (!FormMain.DeChainIfChained(OldOccurrence))
            {
                return;
            }
            DialogResult d = MessageBox.Show("Are you sure you want to delete?\nThis cannot be undone.", "", MessageBoxButtons.YesNo);
            if (!d.Equals(DialogResult.Yes))
            {
                return;
            }
            OldOccurrence.Delete();
            this.Close();
        }

        private void btnReschedNew_Click(object sender, EventArgs e)
        {
            FormOccurrenceScheduler popup = new FormOccurrenceScheduler();
            popup.Single = NewOccurrence;
            popup.ShowDialog();
            this.Close();
        }

        private void btnReschedOld_Click(object sender, EventArgs e)
        {
            FormOccurrenceScheduler popup = new FormOccurrenceScheduler();
            popup.Single = OldOccurrence;
            popup.ShowDialog();
            this.Close();
        }

        public Occurrence OldOccurrence
        {
            get
            {
                return m_oldOcc;
            }

            set
            {
                m_oldOcc = value;
            }
        }

        public Occurrence NewOccurrence
        {
            get { return m_newOcc; }

            set { m_newOcc = value; }
        }

        private Occurrence m_oldOcc;
        private Occurrence m_newOcc;


        

    

    
    }
}
