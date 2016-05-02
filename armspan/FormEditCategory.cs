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
            tbName.Text = NameCategory;
            pnlColor.BackColor = Color;
            gbCategory.Text = "Category #" + Number.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            NameCategory = tbName.Text.Trim();
            if (NameCategory.Equals(""))
            {
                MessageBox.Show("Please enter a name");
                return;
            }
            //TODO: check if color or name is the same as any other category
            if (Category.All.Count > Number - 1)
            {
                Category dest = Category.All.Values.ElementAt((int)Number - 1);
                dest.Name = NameCategory;
                dest.Color = Color;
            }
            else
            {
                Category dest = new Category(NameCategory, Color);
            }
            this.Close();
        }

        public string NameCategory
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public Color Color
        {
            get { return m_color; }
            set { m_color = value; }
        }

        public uint Number
        {
            get { return m_num; }
            set { m_num = value; }
        }

        private string m_name = "";
        private Color m_color;
        private uint m_num;

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (cdColor.ShowDialog() == DialogResult.OK)
            {
                Color = cdColor.Color;
                pnlColor.BackColor = Color;
            }
        }


    }
}
