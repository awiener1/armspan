namespace Span.GUI
{
    partial class FormConflictTwo
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
            this.tlpConflicts = new System.Windows.Forms.TableLayoutPanel();
            this.gbOld = new System.Windows.Forms.GroupBox();
            this.btnReschedOld = new System.Windows.Forms.Button();
            this.btnDeleteOld = new System.Windows.Forms.Button();
            this.btnIgnoreOld = new System.Windows.Forms.Button();
            this.btnCancelOld = new System.Windows.Forms.Button();
            this.pnlOld = new System.Windows.Forms.Panel();
            this.rtbOld = new System.Windows.Forms.RichTextBox();
            this.gbNew = new System.Windows.Forms.GroupBox();
            this.btnReschedNew = new System.Windows.Forms.Button();
            this.btnDeleteNew = new System.Windows.Forms.Button();
            this.btnIgnoreNew = new System.Windows.Forms.Button();
            this.btnCancelNew = new System.Windows.Forms.Button();
            this.pnlNew = new System.Windows.Forms.Panel();
            this.rtbNew = new System.Windows.Forms.RichTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.rtbConflict = new System.Windows.Forms.RichTextBox();
            this.tlpOK = new System.Windows.Forms.TableLayoutPanel();
            this.tlpConflicts.SuspendLayout();
            this.gbOld.SuspendLayout();
            this.pnlOld.SuspendLayout();
            this.gbNew.SuspendLayout();
            this.pnlNew.SuspendLayout();
            this.tlpOK.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpConflicts
            // 
            this.tlpConflicts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpConflicts.ColumnCount = 3;
            this.tlpConflicts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpConflicts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConflicts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpConflicts.Controls.Add(this.gbOld, 2, 0);
            this.tlpConflicts.Controls.Add(this.gbNew, 0, 0);
            this.tlpConflicts.Location = new System.Drawing.Point(14, 79);
            this.tlpConflicts.Name = "tlpConflicts";
            this.tlpConflicts.RowCount = 1;
            this.tlpConflicts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpConflicts.Size = new System.Drawing.Size(724, 166);
            this.tlpConflicts.TabIndex = 1;
            // 
            // gbOld
            // 
            this.gbOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOld.Controls.Add(this.btnReschedOld);
            this.gbOld.Controls.Add(this.btnDeleteOld);
            this.gbOld.Controls.Add(this.btnIgnoreOld);
            this.gbOld.Controls.Add(this.btnCancelOld);
            this.gbOld.Controls.Add(this.pnlOld);
            this.gbOld.Location = new System.Drawing.Point(375, 3);
            this.gbOld.Name = "gbOld";
            this.gbOld.Size = new System.Drawing.Size(346, 160);
            this.gbOld.TabIndex = 2;
            this.gbOld.TabStop = false;
            this.gbOld.Text = "Old Event";
            // 
            // btnReschedOld
            // 
            this.btnReschedOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReschedOld.Image = global::armspan.Properties.Resources.schedule_svg;
            this.btnReschedOld.Location = new System.Drawing.Point(295, 109);
            this.btnReschedOld.Name = "btnReschedOld";
            this.btnReschedOld.Size = new System.Drawing.Size(45, 45);
            this.btnReschedOld.TabIndex = 21;
            this.ttThin.SetToolTip(this.btnReschedOld, "Reschedule Old Event");
            this.btnReschedOld.UseVisualStyleBackColor = true;
            this.btnReschedOld.Click += new System.EventHandler(this.btnReschedOld_Click);
            // 
            // btnDeleteOld
            // 
            this.btnDeleteOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteOld.Image = global::armspan.Properties.Resources.trash_svg;
            this.btnDeleteOld.Location = new System.Drawing.Point(96, 109);
            this.btnDeleteOld.Name = "btnDeleteOld";
            this.btnDeleteOld.Size = new System.Drawing.Size(45, 45);
            this.btnDeleteOld.TabIndex = 20;
            this.ttThin.SetToolTip(this.btnDeleteOld, "Delete Old Event");
            this.btnDeleteOld.UseVisualStyleBackColor = true;
            this.btnDeleteOld.Click += new System.EventHandler(this.btnDeleteOld_Click);
            // 
            // btnIgnoreOld
            // 
            this.btnIgnoreOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIgnoreOld.Image = global::armspan.Properties.Resources.ignore_svg;
            this.btnIgnoreOld.Location = new System.Drawing.Point(51, 109);
            this.btnIgnoreOld.Name = "btnIgnoreOld";
            this.btnIgnoreOld.Size = new System.Drawing.Size(45, 45);
            this.btnIgnoreOld.TabIndex = 19;
            this.ttThin.SetToolTip(this.btnIgnoreOld, "Ignore Old Event");
            this.btnIgnoreOld.UseVisualStyleBackColor = true;
            this.btnIgnoreOld.Click += new System.EventHandler(this.btnIgnoreOld_Click);
            // 
            // btnCancelOld
            // 
            this.btnCancelOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelOld.Image = global::armspan.Properties.Resources.cancel_svg;
            this.btnCancelOld.Location = new System.Drawing.Point(6, 109);
            this.btnCancelOld.Name = "btnCancelOld";
            this.btnCancelOld.Size = new System.Drawing.Size(45, 45);
            this.btnCancelOld.TabIndex = 18;
            this.ttThin.SetToolTip(this.btnCancelOld, "Cancel Old Event");
            this.btnCancelOld.UseVisualStyleBackColor = true;
            this.btnCancelOld.Click += new System.EventHandler(this.btnCancelOld_Click);
            // 
            // pnlOld
            // 
            this.pnlOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOld.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlOld.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOld.Controls.Add(this.rtbOld);
            this.pnlOld.Location = new System.Drawing.Point(6, 17);
            this.pnlOld.Name = "pnlOld";
            this.pnlOld.Size = new System.Drawing.Size(333, 86);
            this.pnlOld.TabIndex = 0;
            // 
            // rtbOld
            // 
            this.rtbOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOld.BackColor = System.Drawing.SystemColors.Window;
            this.rtbOld.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbOld.DetectUrls = false;
            this.rtbOld.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbOld.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rtbOld.Location = new System.Drawing.Point(9, 7);
            this.rtbOld.Name = "rtbOld";
            this.rtbOld.ReadOnly = true;
            this.rtbOld.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbOld.Size = new System.Drawing.Size(309, 65);
            this.rtbOld.TabIndex = 6;
            this.rtbOld.Text = "";
            // 
            // gbNew
            // 
            this.gbNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNew.Controls.Add(this.btnReschedNew);
            this.gbNew.Controls.Add(this.btnDeleteNew);
            this.gbNew.Controls.Add(this.btnIgnoreNew);
            this.gbNew.Controls.Add(this.btnCancelNew);
            this.gbNew.Controls.Add(this.pnlNew);
            this.gbNew.Location = new System.Drawing.Point(3, 3);
            this.gbNew.Name = "gbNew";
            this.gbNew.Size = new System.Drawing.Size(346, 160);
            this.gbNew.TabIndex = 0;
            this.gbNew.TabStop = false;
            this.gbNew.Text = "New Event";
            // 
            // btnReschedNew
            // 
            this.btnReschedNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReschedNew.Image = global::armspan.Properties.Resources.schedule_svg;
            this.btnReschedNew.Location = new System.Drawing.Point(295, 109);
            this.btnReschedNew.Name = "btnReschedNew";
            this.btnReschedNew.Size = new System.Drawing.Size(45, 45);
            this.btnReschedNew.TabIndex = 21;
            this.ttThin.SetToolTip(this.btnReschedNew, "Reschedule New Event");
            this.btnReschedNew.UseVisualStyleBackColor = true;
            this.btnReschedNew.Click += new System.EventHandler(this.btnReschedNew_Click);
            // 
            // btnDeleteNew
            // 
            this.btnDeleteNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteNew.Image = global::armspan.Properties.Resources.trash_svg;
            this.btnDeleteNew.Location = new System.Drawing.Point(96, 109);
            this.btnDeleteNew.Name = "btnDeleteNew";
            this.btnDeleteNew.Size = new System.Drawing.Size(45, 45);
            this.btnDeleteNew.TabIndex = 20;
            this.ttThin.SetToolTip(this.btnDeleteNew, "Delete New Event");
            this.btnDeleteNew.UseVisualStyleBackColor = true;
            this.btnDeleteNew.Click += new System.EventHandler(this.btnDeleteNew_Click);
            // 
            // btnIgnoreNew
            // 
            this.btnIgnoreNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIgnoreNew.Image = global::armspan.Properties.Resources.ignore_svg;
            this.btnIgnoreNew.Location = new System.Drawing.Point(51, 109);
            this.btnIgnoreNew.Name = "btnIgnoreNew";
            this.btnIgnoreNew.Size = new System.Drawing.Size(45, 45);
            this.btnIgnoreNew.TabIndex = 19;
            this.ttThin.SetToolTip(this.btnIgnoreNew, "Ignore New Event");
            this.btnIgnoreNew.UseVisualStyleBackColor = true;
            this.btnIgnoreNew.Click += new System.EventHandler(this.btnIgnoreNew_Click);
            // 
            // btnCancelNew
            // 
            this.btnCancelNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelNew.Image = global::armspan.Properties.Resources.cancel_svg;
            this.btnCancelNew.Location = new System.Drawing.Point(6, 109);
            this.btnCancelNew.Name = "btnCancelNew";
            this.btnCancelNew.Size = new System.Drawing.Size(45, 45);
            this.btnCancelNew.TabIndex = 18;
            this.ttThin.SetToolTip(this.btnCancelNew, "Cancel New Event");
            this.btnCancelNew.UseVisualStyleBackColor = true;
            this.btnCancelNew.Click += new System.EventHandler(this.btnCancelNew_Click);
            // 
            // pnlNew
            // 
            this.pnlNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNew.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlNew.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlNew.Controls.Add(this.rtbNew);
            this.pnlNew.Location = new System.Drawing.Point(6, 17);
            this.pnlNew.Name = "pnlNew";
            this.pnlNew.Size = new System.Drawing.Size(333, 86);
            this.pnlNew.TabIndex = 0;
            // 
            // rtbNew
            // 
            this.rtbNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNew.BackColor = System.Drawing.SystemColors.Window;
            this.rtbNew.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNew.DetectUrls = false;
            this.rtbNew.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rtbNew.Location = new System.Drawing.Point(9, 7);
            this.rtbNew.Name = "rtbNew";
            this.rtbNew.ReadOnly = true;
            this.rtbNew.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbNew.Size = new System.Drawing.Size(309, 65);
            this.rtbNew.TabIndex = 6;
            this.rtbNew.Text = "";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnOK.Location = new System.Drawing.Point(339, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 45);
            this.btnOK.TabIndex = 6;
            this.ttThin.SetToolTip(this.btnOK, "Accept Conflict");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rtbConflict
            // 
            this.rtbConflict.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbConflict.BackColor = System.Drawing.SystemColors.Control;
            this.rtbConflict.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbConflict.DetectUrls = false;
            this.rtbConflict.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rtbConflict.Location = new System.Drawing.Point(12, 43);
            this.rtbConflict.Name = "rtbConflict";
            this.rtbConflict.ReadOnly = true;
            this.rtbConflict.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbConflict.Size = new System.Drawing.Size(727, 26);
            this.rtbConflict.TabIndex = 8;
            this.rtbConflict.Text = "";
           
            // 
            // tlpOK
            // 
            this.tlpOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpOK.ColumnCount = 1;
            this.tlpOK.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOK.Controls.Add(this.btnOK, 0, 0);
            this.tlpOK.Location = new System.Drawing.Point(14, 248);
            this.tlpOK.Name = "tlpOK";
            this.tlpOK.RowCount = 1;
            this.tlpOK.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOK.Size = new System.Drawing.Size(723, 60);
            this.tlpOK.TabIndex = 9;
            // 
            // FormConflictTwo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 319);
            this.Controls.Add(this.tlpOK);
            this.Controls.Add(this.rtbConflict);
            this.Controls.Add(this.tlpConflicts);
            this.MinimumSize = new System.Drawing.Size(460, 314);
            this.Name = "FormConflictTwo";
            this.Text = "FormConflictTwo";
            this.Load += new System.EventHandler(this.FormConflictTwo_Load);
            this.Controls.SetChildIndex(this.tlpConflicts, 0);
            this.Controls.SetChildIndex(this.rtbConflict, 0);
            this.Controls.SetChildIndex(this.tlpOK, 0);
            this.tlpConflicts.ResumeLayout(false);
            this.gbOld.ResumeLayout(false);
            this.pnlOld.ResumeLayout(false);
            this.gbNew.ResumeLayout(false);
            this.pnlNew.ResumeLayout(false);
            this.tlpOK.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpConflicts;
        private System.Windows.Forms.GroupBox gbNew;
        private System.Windows.Forms.Panel pnlNew;
        private System.Windows.Forms.RichTextBox rtbNew;
        private System.Windows.Forms.Button btnReschedNew;
        private System.Windows.Forms.Button btnDeleteNew;
        private System.Windows.Forms.Button btnIgnoreNew;
        private System.Windows.Forms.Button btnCancelNew;
        private System.Windows.Forms.GroupBox gbOld;
        private System.Windows.Forms.Button btnReschedOld;
        private System.Windows.Forms.Button btnDeleteOld;
        private System.Windows.Forms.Button btnIgnoreOld;
        private System.Windows.Forms.Button btnCancelOld;
        private System.Windows.Forms.Panel pnlOld;
        private System.Windows.Forms.RichTextBox rtbOld;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RichTextBox rtbConflict;
        private System.Windows.Forms.TableLayoutPanel tlpOK;
    }
}