/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The FormEditCategory class allows the user
 * to edit one Category.
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

        /**
         * Applies the user's changes to the Category.
         * 
         * @date May 1, 2016
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            NameCategory = tbName.Text.Trim();
            if (NameCategory.Equals(""))
            {
                MessageBox.Show("Please enter a name");
                return;
            }
            
            if (Category.All.Count > Number - 1)
            {
                //edit category
                Category dest = Category.All.Values.ElementAt((int)Number - 1);
                dest.Name = NameCategory;
                dest.Color = Color;
            }
            else
            {
                //make new category instead
                Category dest = new Category(NameCategory, Color);
            }
            this.Close();
        }

        /**
         * The name of the Category.
         * 
         * Not to be confused with the name property
         * of the form itself.
         */
        public string NameCategory
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /**
         * The Color of the Category.
         */
        public Color Color
        {
            get { return m_color; }
            set { m_color = value; }
        }

        /**
         * The Number of the Category.
         */
        public uint Number
        {
            get { return m_num; }
            set { m_num = value; }
        }

        /**
         * The name of the Category. See also NameCategory.
         */
        private string m_name = "";
        /**
         * The Color of the Category. See also Color.
         */
        private Color m_color;
        /**
         * The number of the Category. See also Number.
         */
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
