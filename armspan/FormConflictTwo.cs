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
            
            rtbConflict.Rtf = @"{\rtf0\ansi\pard\qc \b Something called x \b0 conflicts with \b Something called y\b0 . \par}";
            rtbNew.Rtf = @"{\rtf0\ansi\b Something called x \b0 is the new event.}";
            rtbOld.Rtf = @"{\rtf0\ansi\b Something called y \b0 is the old event.}";
        }

    

    
    }
}
