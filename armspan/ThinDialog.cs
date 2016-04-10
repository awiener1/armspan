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
    public partial class ThinDialog : Form
    {
        public ThinDialog()
        {
            InitializeComponent();
        }

        //CITE: http://stackoverflow.com/a/3162167
        //Allows form to have button-like edges,
        //and eliminates the title bar while keeping
        //the caption when viewed in the desktop
        //taskbar.
        //cite start
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= -0x00C00000; //remove WS_CAPTION
                parms.Style |= 0x00040000; //add WS_SIZEBOX
                return parms;
            }
        }
        //cite end


        //CITE: http://stackoverflow.com/a/2384459
        //Allows a control (PictureBox here) to
        //be used to drag the form around,
        //since there is no longer a title bar.
        //cite start
        Point dragOffset;
        protected void dragBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragOffset = this.PointToScreen(e.Location);
                Point formLocation = FindForm().Location;
                dragOffset.X -= formLocation.X;
                dragOffset.Y -= formLocation.Y;
            }
        }

        protected void dragBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newLocation = this.PointToScreen(e.Location);
                newLocation.X -= dragOffset.X;
                newLocation.Y -= dragOffset.Y;

                FindForm().Location = newLocation;
            }
        }
        //cite end

        private void ThinDialog_Load(object sender, EventArgs e)
        {

        }

    }
}