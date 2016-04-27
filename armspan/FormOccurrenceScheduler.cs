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
    public partial class FormOccurrenceScheduler : ThinDialog
    {
        public FormOccurrenceScheduler()
        {
            InitializeComponent();
        }

        private Occurrence m_single;

        public Occurrence Single
        {
            get
            {
                return m_single;
            }

            set
            {
                m_single = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime start = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day, dtpFrom.Value.Hour, dtpFrom.Value.Minute, 0, 0);
            DateTime end = new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day, dtpTo.Value.Hour, dtpTo.Value.Minute, 0, 0);
            if (end < start.AddMinutes(1))
            {
                MessageBox.Show("Start time must be at least one minute before end time");
                return;
            }
            Single.StartActual = start;
            Single.EndActual = end;
            this.Close();
        }

        private void FormOccurrenceScheduler_Load(object sender, EventArgs e)
        {
            if (Single == null)
            {
                throw new NullReferenceException("No occurrence has been assigned");
            }
            lblOccInfo.Text = Single.Parent().Name + "\nInitially\n"
                + Single.StartActual.ToShortDateString() + " at " + Single.StartActual.ToShortTimeString() + "\nto "
                + Single.EndActual.ToShortDateString() + " at " + Single.EndActual.ToShortTimeString();
            dtpFrom.Value = Single.StartActual;
            dtpTo.Value = Single.EndActual;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
