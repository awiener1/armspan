﻿using System;
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

        private void button1_Click(object sender, EventArgs e)
        {
            ThinDialog popup = new ThinDialog();
            popup.Show();
            //popup.Activate();
        }

       
    }
}