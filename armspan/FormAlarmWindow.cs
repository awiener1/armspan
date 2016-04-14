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
            rtbAlarm.Rtf = @"{\rtf0\ansi Are you getting ready for \b An Event\b0 ?}";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
           
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
            
        }
    }
}
