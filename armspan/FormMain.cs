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
        }

        private void btnStartOcc_Click(object sender, EventArgs e)
        {
            FormAlarmWindow popup = new FormAlarmWindow();
            popup.Show();
            //popup.Activate();
        }

        private void btnRescheduleOcc_Click(object sender, EventArgs e)
        {
            FormEventScheduler popup = new FormEventScheduler();
            popup.Show();
        }

        private void btnNewAppt_Click(object sender, EventArgs e)
        {
            FormAddEvent popup = new FormAddEvent();
            popup.Show();
        }

        private void btnCancelAppts_Click(object sender, EventArgs e)
        {
            FormConflictTwo popup = new FormConflictTwo();
            popup.Show();
        }

        private void btnCancelTasks_Click(object sender, EventArgs e)
        {
            FormConflictMultiple popup = new FormConflictMultiple();
            popup.Show();

        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            FormAddCategories popup = new FormAddCategories();
            popup.Show();
        }

        

       
    }
}
