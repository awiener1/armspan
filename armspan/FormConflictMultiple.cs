/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The FormConflictMultiple class provides a form
 * asking the user to resolve a conflict between
 * three or more Events.
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

namespace Span.GUI
{
    public partial class FormConflictMultiple : ThinDialog
    {
        /**
         * The list of conflicting Event ids.
         */
        public List<string> ConflictingEvents
        {
            get { return m_conflicting; }
            set { m_conflicting = value; }
        }

        /**
         * The new Event.
         */
        public Event NewEvent
        {
            get { return m_newEv; }
            set { m_newEv = value; }
        }

        public FormConflictMultiple()
        {
            InitializeComponent();
        }

        /**
         * Initializes the display.
         * 
         * @date April 29, 2016
         */
        private void FormConflictMultiple_Load(object sender, EventArgs e)
        {
            //adding title
            rtbConflict.Text = "";
            Font rtbBold = new Font(rtbConflict.Font, FontStyle.Bold);
            rtbConflict.SelectionAlignment = HorizontalAlignment.Center;
            rtbConflict.SelectionFont = rtbBold;
            rtbConflict.AppendText(NewEvent.Name);
            rtbConflict.SelectionFont = rtbConflict.Font;
            rtbConflict.AppendText(" conflicts with the following events:");
            //adding event list
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

        /**
         * Allows the conflict to persist.
         * 
         * Note: There is no alternative to this,
         * but the form notifies the user of the
         * conflicts so they can correct them
         * manually.
         * 
         * @date April 29, 2016
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**
         * The list of conflicting event ids. See also ConflictingEvents.
         */
        private List<string> m_conflicting;

        /**
         * The new Event. See also NewEvent.
         */
        private Event m_newEv;
    }
}
