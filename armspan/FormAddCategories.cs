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
    public partial class FormAddCategories : ThinDialog
    {
        public FormAddCategories()
        {
            InitializeComponent();
        }

        private void FormAddCategories_Load(object sender, EventArgs e)
        {

        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            FormEditCategory popup = new FormEditCategory();
            popup.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
