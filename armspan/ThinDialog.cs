/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The ThinDialog class is a custom form class
 * that allows for forms with titlebars
 * that have no buttons or captions on them.
 * They can be dragged or resized just like
 * regular forms, but they have no close
 * buttons, so that must be implemented separately.
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
using System.Windows.Forms.VisualStyles;

namespace Span.GUI
{
    public partial class ThinDialog : Form
    {
        /**
         * Allows form to have button-like edges,
         * and eliminates the title bar while keeping
         * the caption when viewed in the desktop
         * taskbar.
         */
        //taken from http://stackoverflow.com/a/3162167
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                //remove WS_CAPTION
                parms.Style &= -0x00C00000; 
                //add WS_SIZEBOX
                parms.Style |= 0x00040000; 
                return parms;
            }
        }

        public ThinDialog()
        {
            InitializeComponent();
        }

        /**
         * Allows a control (PictureBox here) to
         * be used to drag the form around,
         * since there is no longer a title bar. 
         */
        //taken from http://stackoverflow.com/a/2384459
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

        /**
         * Allows a control (PictureBox here) to
         * be used to drag the form around,
         * since there is no longer a title bar. 
         */
        //taken from http://stackoverflow.com/a/2384459
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

        private void ThinDialog_Load(object sender, EventArgs e)
        {
            this.dragBox.Width = this.ClientSize.Width - 24;
        }

        /**
         * Allows a control (PictureBox here) to
         * be used to drag the form around,
         * since there is no longer a title bar. 
         */
        //taken from http://stackoverflow.com/a/2384459
        Point dragOffset;
    }
}
