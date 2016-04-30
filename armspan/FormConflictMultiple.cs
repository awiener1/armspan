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
    public partial class FormConflictMultiple : ThinDialog
    {
        public FormConflictMultiple()
        {
            InitializeComponent();
        }

        private void FormConflictMultiple_Load(object sender, EventArgs e)
        {
            rtbConflict.Text = "";
            Font rtbBold = new Font(rtbConflict.Font, FontStyle.Bold);
            rtbConflict.SelectionAlignment = HorizontalAlignment.Center;
            rtbConflict.SelectionFont = rtbBold;
            rtbConflict.AppendText(NewEvent.Name);
            rtbConflict.SelectionFont = rtbConflict.Font;
            rtbConflict.AppendText(" conflicts with the following events:");
            List<string> conflicts = new List<string>();
            foreach (string s in ConflictingEvents)
            {
                Event p = Event.All[s];
                string name = p.Name;
                string cats = string.Join(", ", NewEvent.Categories.Intersect(p.Categories).Select(x => Category.All[x].Name));
                conflicts.Add(name + " (" + cats + ")");
            }
            lbConflicts.DataSource = conflicts;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<string> m_conflicting;
        private Event m_newEv;

        public List<string> ConflictingEvents
        {
            get { return m_conflicting; }
            set { m_conflicting = value; }
        }

        public Event NewEvent
        {
            get { return m_newEv; }
            set { m_newEv = value; }
        }
    }
}
