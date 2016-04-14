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
    public partial class FormAddEvent : ThinDialog
    {
        public FormAddEvent()
        {
            InitializeComponent();
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            FormAlarmSettings popup = new FormAlarmSettings();
            popup.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddOcc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
