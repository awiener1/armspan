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
            rtbConflict.Rtf = @"{\rtf0\ansi\pard\qc \b Something called x \b0 conflicts with the following events: \par}";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelNew_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIgnoreNew_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnDeleteNew_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReschedNew_Click(object sender, EventArgs e)
        {
            FormEventScheduler popup = new FormEventScheduler();
            popup.ShowDialog();
            this.Close();
        }
    }
}
