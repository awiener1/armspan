/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The FormAddCategories class allows the user
 * to manage the Category objects used for Events.
 * Alternatively, it can be used to select categories
 * for an Event or for the FormSummaryWindow.
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
    public partial class FormAddCategories : ThinDialog
    {
        /**
         * The list of checked indices in the Category list.
         */
        public List<int> Checked
        {
            get { return m_checked; }
            set { m_checked = value; }
        }

        /**
         * The index of the disabled Category, which will
         * appear greyed out and will not be clickable.
         * If there is no disabled Category, use -1.
         */
        public int Disabled 
        {
            get { return m_disabled; }
            set { m_disabled = value; }
        }

        public FormAddCategories()
        {
            InitializeComponent();
        }

        /**
         * Creates a new FormAddCategories to either
         * select or edit Category objects.
         * 
         * @param a_canEdit If true, allows the user
         * to add or edit categories. If false,
         * allows the user to select categories.
         * 
         * @date April 20, 2016
         */
        public FormAddCategories(bool a_canEdit)
        {
            InitializeComponent();
            lvCategories.CheckBoxes = !a_canEdit;
            btnAddCat.Enabled = a_canEdit;
            btnEditCat.Enabled = false;
            btnSelectAll.Enabled = !a_canEdit;
            btnSelectNone.Enabled = !a_canEdit;
            m_canEdit = a_canEdit;
        }

        /**
         * Refreshes the list of categories.
         * 
         * @date April 20, 2016
         */
        private void RefreshCategories()
        {
            //clear list and repopulate
            lvCategories.SmallImageList.Images.Clear();
            lvCategories.Items.Clear();
            foreach (Category cat in Category.All.Values)
            {
                ListViewItem lvcat = new ListViewItem();
                lvcat.Text = cat.Name;
                lvcat.ImageIndex = (int)cat.Number - 1;
                lvcat.SubItems.Add(cat.Number.ToString());
                //add "image" of Category's color
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
            //make disabled (eg, primary) category stand out
            if (Disabled >= 0)
            {
                ListViewItem dis = lvCategories.Items[Disabled];
                dis.Checked = true;
                dis.ForeColor = System.Drawing.SystemColors.ControlDark;
                dis.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        private void FormAddCategories_Load(object sender, EventArgs e)
        {
            RefreshCategories();
        }

        /**
         * Allows the user to add a new Category.
         * 
         * @date May 1, 2016
         */
        private void btnAddCat_Click(object sender, EventArgs e)
        {
            FormEditCategory popup = new FormEditCategory();
            popup.Color = SystemColors.ControlDark;
            popup.Number = (uint)Category.All.Count() + 1;
            popup.ShowDialog();
            RefreshCategories();
        }

        /**
         * Closes the form.
         * 
         * If there are no categories, prevents the user from
         * closing the form. If the user is selecting categories,
         * makes the selection available in Checked.
         * 
         * @date April 20, 2016
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Category.All.Count() < 1)
            {
                MessageBox.Show("Please add at least one category before using armspan.");
                return;
            }
            if (m_canEdit)
            {
                this.Close();
                return;
            }
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

        private void lvCategories_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.ItemIndex == Disabled){
                e.Item.Selected = false;
            }
            if (m_canEdit)
            {
                btnEditCat.Enabled = e.IsSelected;
            }
        }

        private void lvCategories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == Disabled)
            {
                e.NewValue = CheckState.Checked;
            }

        }

        /**
         * Selects all categories.
         * 
         * @date May 1, 2016
         */
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvCategories.Items)
            {
                item.Checked = true;
            }
        }

        /**
         * Selects no categories, except for the primary Category.
         * 
         * @date May 1, 2016
         */
        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvCategories.Items)
            {
                item.Checked = false;
            }
        }

        /**
         * Allows the user to edit an existing Category.
         * 
         * @date May 1, 2016
         */
        private void btnEditCat_Click(object sender, EventArgs e)
        {
            FormEditCategory popup = new FormEditCategory();
            int c = lvCategories.SelectedIndices[0];
            Category toEdit = Category.All.Values.ElementAt(c);
            popup.NameCategory = toEdit.Name;
            popup.Color = toEdit.Color;
            popup.Number = toEdit.Number;
            popup.ShowDialog();
            RefreshCategories();
        }

        /**
         * The list of checked indices in the Category list. See also Checked.
         */
        private List<int> m_checked;
        /**
         * The index of the disabled Category. See also Disabled.
         */
        private int m_disabled = -1;
        /**
         * Denotes if the user can add and edit categories.
         */
        private bool m_canEdit;
    }
}
