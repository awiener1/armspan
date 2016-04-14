namespace Span.GUI
{
    partial class FormEditCategory
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
            this.gbCategory = new System.Windows.Forms.GroupBox();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblColor = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCategory
            // 
            this.gbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCategory.Controls.Add(this.pnlColor);
            this.gbCategory.Controls.Add(this.btnColor);
            this.gbCategory.Controls.Add(this.lblColor);
            this.gbCategory.Controls.Add(this.tbName);
            this.gbCategory.Controls.Add(this.lblName);
            this.gbCategory.Location = new System.Drawing.Point(15, 37);
            this.gbCategory.Name = "gbCategory";
            this.gbCategory.Size = new System.Drawing.Size(471, 83);
            this.gbCategory.TabIndex = 1;
            this.gbCategory.TabStop = false;
            this.gbCategory.Text = "Category #";
            // 
            // pnlColor
            // 
            this.pnlColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlColor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlColor.Location = new System.Drawing.Point(94, 48);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(202, 25);
            this.pnlColor.TabIndex = 9;
            this.ttThin.SetToolTip(this.pnlColor, "Color");
            // 
            // btnColor
            // 
            this.btnColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnColor.Location = new System.Drawing.Point(54, 48);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(31, 25);
            this.btnColor.TabIndex = 8;
            this.btnColor.Text = "...";
            this.ttThin.SetToolTip(this.btnColor, "Pick a color for the category");
            this.btnColor.UseVisualStyleBackColor = true;
            // 
            // lblColor
            // 
            this.lblColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(10, 54);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(32, 13);
            this.lblColor.TabIndex = 7;
            this.lblColor.Text = "Color";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(54, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(400, 21);
            this.tbName.TabIndex = 6;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(10, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(34, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnOK.Location = new System.Drawing.Point(441, 127);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 45);
            this.btnOK.TabIndex = 24;
            this.ttThin.SetToolTip(this.btnOK, "Confirm Category");
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FormEditCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 178);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbCategory);
            this.MinimumSize = new System.Drawing.Size(326, 186);
            this.Name = "FormEditCategory";
            this.Text = "FormEditCategory";
            this.Load += new System.EventHandler(this.FormEditCategory_Load);
            this.Controls.SetChildIndex(this.gbCategory, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.gbCategory.ResumeLayout(false);
            this.gbCategory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCategory;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnOK;
    }
}