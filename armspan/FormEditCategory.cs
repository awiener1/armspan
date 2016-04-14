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
    public partial class FormEditCategory : ThinDialog
    {
        public FormEditCategory()
        {
            InitializeComponent();
        }

        private void FormEditCategory_Load(object sender, EventArgs e)
        {
            MessageBox.Show(this.Size.ToString());
        }
    }
}
