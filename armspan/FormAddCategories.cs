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

        public FormAddCategories(bool a_canEdit)
        {
            InitializeComponent();
            lvCategories.CheckBoxes = !a_canEdit;
            btnAddCat.Enabled = a_canEdit;
            btnEditCat.Enabled = a_canEdit;
            btnSelectAll.Enabled = !a_canEdit;
            btnSelectNone.Enabled = !a_canEdit;
        }

        private void FormAddCategories_Load(object sender, EventArgs e)
        {
            
            RefreshCategories();
        }

        private void RefreshCategories()
        {
            lvCategories.SmallImageList.Images.Clear();
            foreach (Category cat in Category.All.Values)
            {
                ListViewItem lvcat = new ListViewItem();
                lvcat.Text = cat.Name;
                lvcat.ImageIndex = (int)cat.Number - 1;
                lvcat.SubItems.Add(cat.Number.ToString());
                Bitmap imgcat = new Bitmap(48, 16);
                Graphics imggfx = Graphics.FromImage(imgcat);
                imggfx.Clear(cat.Color);
                lvCategories.SmallImageList.Images.Add(imgcat);
                lvCategories.Items.Add(lvcat);
            }
            if (Checked == null)
            {
                return;
            }
            foreach (int index in Checked)
            {
                lvCategories.Items[index].Checked = true;
            }
            if (Disabled >= 0)
            {
                ListViewItem dis = lvCategories.Items[Disabled];
                dis.Checked = true;
                dis.ForeColor = System.Drawing.SystemColors.ControlDark;
                dis.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            FormEditCategory popup = new FormEditCategory();
            popup.ShowDialog();
        }

        public List<int> Checked
        {
            get
            {
                return m_checked;
            }

            set
            {
                m_checked = value;
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            Checked.Clear();
            foreach (int index in lvCategories.CheckedIndices)
            {
                if (index != Disabled)
                {
                    Checked.Add(index);
                }
            }
            this.Close();
        }


        private List<int> m_checked;
        private int m_disabled = -1;


        public int Disabled 
        {
            get
            {
                return m_disabled;
            }

            set
            {
                m_disabled = value;
            }
        }

        private void lvCategories_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.ItemIndex == Disabled){
                e.Item.Selected = false;
            }
        }

        private void lvCategories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == Disabled)
            {
                e.NewValue = CheckState.Checked;
            }

        }

        
    }
}
