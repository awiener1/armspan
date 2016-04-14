namespace Span.GUI
{
    partial class FormAddCategories
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "name1"}, 0, System.Drawing.Color.Black, System.Drawing.Color.Transparent, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "name2"}, 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddCategories));
            this.lvCategories = new System.Windows.Forms.ListView();
            this.chInclude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.btnEditCat = new System.Windows.Forms.Button();
            this.btnAddCat = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tlpControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvCategories
            // 
            this.lvCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCategories.CheckBoxes = true;
            this.lvCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chInclude,
            this.chName});
            this.lvCategories.GridLines = true;
            this.lvCategories.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.lvCategories.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvCategories.Location = new System.Drawing.Point(17, 73);
            this.lvCategories.Name = "lvCategories";
            this.lvCategories.Size = new System.Drawing.Size(501, 124);
            this.lvCategories.SmallImageList = this.imageList1;
            this.lvCategories.TabIndex = 1;
            this.lvCategories.UseCompatibleStateImageBehavior = false;
            this.lvCategories.View = System.Windows.Forms.View.Details;
            // 
            // chInclude
            // 
            this.chInclude.Text = "      Color";
            this.chInclude.Width = 80;
            // 
            // chName
            // 
            this.chName.Text = "Category Name";
            this.chName.Width = 300;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "calendar.svg.png");
            this.imageList1.Images.SetKeyName(1, "postponesmallfive.svg.png");
            this.imageList1.Images.SetKeyName(2, "colors.svg.png");
            // 
            // tlpControls
            // 
            this.tlpControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpControls.ColumnCount = 5;
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.Controls.Add(this.btnEditCat, 0, 0);
            this.tlpControls.Controls.Add(this.btnAddCat, 0, 0);
            this.tlpControls.Controls.Add(this.btnSelectAll, 2, 0);
            this.tlpControls.Controls.Add(this.btnSelectNone, 3, 0);
            this.tlpControls.Controls.Add(this.btnOK, 4, 0);
            this.tlpControls.Location = new System.Drawing.Point(17, 214);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 1;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControls.Size = new System.Drawing.Size(501, 50);
            this.tlpControls.TabIndex = 2;
            // 
            // btnEditCat
            // 
            this.btnEditCat.Image = global::armspan.Properties.Resources.edit_svg;
            this.btnEditCat.Location = new System.Drawing.Point(45, 0);
            this.btnEditCat.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditCat.Name = "btnEditCat";
            this.btnEditCat.Size = new System.Drawing.Size(45, 45);
            this.btnEditCat.TabIndex = 3;
            this.ttThin.SetToolTip(this.btnEditCat, "Edit Category");
            this.btnEditCat.UseVisualStyleBackColor = true;
            // 
            // btnAddCat
            // 
            this.btnAddCat.Image = global::armspan.Properties.Resources.add_svg;
            this.btnAddCat.Location = new System.Drawing.Point(0, 0);
            this.btnAddCat.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddCat.Name = "btnAddCat";
            this.btnAddCat.Size = new System.Drawing.Size(45, 45);
            this.btnAddCat.TabIndex = 2;
            this.ttThin.SetToolTip(this.btnAddCat, "Add Category");
            this.btnAddCat.UseVisualStyleBackColor = true;
            this.btnAddCat.Click += new System.EventHandler(this.btnAddCat_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.Image = global::armspan.Properties.Resources.checkall_svg;
            this.btnSelectAll.Location = new System.Drawing.Point(228, 0);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(45, 45);
            this.btnSelectAll.TabIndex = 5;
            this.ttThin.SetToolTip(this.btnSelectAll, "Select All");
            this.btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Image = global::armspan.Properties.Resources.checknone_svg;
            this.btnSelectNone.Location = new System.Drawing.Point(273, 0);
            this.btnSelectNone.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(45, 45);
            this.btnSelectNone.TabIndex = 4;
            this.ttThin.SetToolTip(this.btnSelectNone, "Select None");
            this.btnSelectNone.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnOK.Location = new System.Drawing.Point(456, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 45);
            this.btnOK.TabIndex = 6;
            this.ttThin.SetToolTip(this.btnOK, "OK");
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FormAddCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 280);
            this.Controls.Add(this.tlpControls);
            this.Controls.Add(this.lvCategories);
            this.MinimumSize = new System.Drawing.Size(270, 242);
            this.Name = "FormAddCategories";
            this.Text = "FormAddCategories";
            this.Load += new System.EventHandler(this.FormAddCategories_Load);
            this.Controls.SetChildIndex(this.lvCategories, 0);
            this.Controls.SetChildIndex(this.tlpControls, 0);
            this.tlpControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvCategories;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader chInclude;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.Button btnAddCat;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnEditCat;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnOK;
    }
}