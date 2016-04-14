namespace Span.GUI
{
    partial class FormConflictMultiple
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
            this.rtbConflict = new System.Windows.Forms.RichTextBox();
            this.lbConflicts = new System.Windows.Forms.ListBox();
            this.gbNew = new System.Windows.Forms.GroupBox();
            this.tlpNew = new System.Windows.Forms.TableLayoutPanel();
            this.tlpDecline = new System.Windows.Forms.TableLayoutPanel();
            this.btnDeleteNew = new System.Windows.Forms.Button();
            this.btnCancelNew = new System.Windows.Forms.Button();
            this.btnIgnoreNew = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnReschedNew = new System.Windows.Forms.Button();
            this.gbNew.SuspendLayout();
            this.tlpNew.SuspendLayout();
            this.tlpDecline.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbConflict
            // 
            this.rtbConflict.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbConflict.BackColor = System.Drawing.SystemColors.Control;
            this.rtbConflict.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbConflict.DetectUrls = false;
            this.rtbConflict.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rtbConflict.Location = new System.Drawing.Point(12, 41);
            this.rtbConflict.Name = "rtbConflict";
            this.rtbConflict.ReadOnly = true;
            this.rtbConflict.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbConflict.Size = new System.Drawing.Size(469, 26);
            this.rtbConflict.TabIndex = 9;
            this.rtbConflict.Text = "";
            // 
            // lbConflicts
            // 
            this.lbConflicts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbConflicts.FormattingEnabled = true;
            this.lbConflicts.Location = new System.Drawing.Point(12, 85);
            this.lbConflicts.Name = "lbConflicts";
            this.lbConflicts.ScrollAlwaysVisible = true;
            this.lbConflicts.Size = new System.Drawing.Size(468, 95);
            this.lbConflicts.TabIndex = 10;
            // 
            // gbNew
            // 
            this.gbNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNew.Controls.Add(this.tlpNew);
            this.gbNew.Location = new System.Drawing.Point(76, 203);
            this.gbNew.Name = "gbNew";
            this.gbNew.Size = new System.Drawing.Size(346, 69);
            this.gbNew.TabIndex = 11;
            this.gbNew.TabStop = false;
            this.gbNew.Text = "New Event";
            // 
            // tlpNew
            // 
            this.tlpNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpNew.ColumnCount = 3;
            this.tlpNew.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpNew.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpNew.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpNew.Controls.Add(this.tlpDecline, 1, 0);
            this.tlpNew.Controls.Add(this.btnOK, 0, 0);
            this.tlpNew.Controls.Add(this.btnReschedNew, 2, 0);
            this.tlpNew.Location = new System.Drawing.Point(6, 14);
            this.tlpNew.Name = "tlpNew";
            this.tlpNew.RowCount = 1;
            this.tlpNew.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNew.Size = new System.Drawing.Size(334, 51);
            this.tlpNew.TabIndex = 12;
            // 
            // tlpDecline
            // 
            this.tlpDecline.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tlpDecline.ColumnCount = 3;
            this.tlpDecline.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDecline.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDecline.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDecline.Controls.Add(this.btnDeleteNew, 2, 0);
            this.tlpDecline.Controls.Add(this.btnCancelNew, 0, 0);
            this.tlpDecline.Controls.Add(this.btnIgnoreNew, 1, 0);
            this.tlpDecline.Location = new System.Drawing.Point(97, 0);
            this.tlpDecline.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDecline.Name = "tlpDecline";
            this.tlpDecline.RowCount = 1;
            this.tlpDecline.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDecline.Size = new System.Drawing.Size(137, 46);
            this.tlpDecline.TabIndex = 0;
            // 
            // btnDeleteNew
            // 
            this.btnDeleteNew.Image = global::armspan.Properties.Resources.trash_svg;
            this.btnDeleteNew.Location = new System.Drawing.Point(90, 0);
            this.btnDeleteNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteNew.Name = "btnDeleteNew";
            this.btnDeleteNew.Size = new System.Drawing.Size(45, 45);
            this.btnDeleteNew.TabIndex = 20;
            this.ttThin.SetToolTip(this.btnDeleteNew, "Delete New Event");
            this.btnDeleteNew.UseVisualStyleBackColor = true;
            this.btnDeleteNew.Click += new System.EventHandler(this.btnDeleteNew_Click);
            // 
            // btnCancelNew
            // 
            this.btnCancelNew.Image = global::armspan.Properties.Resources.cancel_svg;
            this.btnCancelNew.Location = new System.Drawing.Point(0, 0);
            this.btnCancelNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancelNew.Name = "btnCancelNew";
            this.btnCancelNew.Size = new System.Drawing.Size(45, 45);
            this.btnCancelNew.TabIndex = 18;
            this.ttThin.SetToolTip(this.btnCancelNew, "Cancel New Event");
            this.btnCancelNew.UseVisualStyleBackColor = true;
            this.btnCancelNew.Click += new System.EventHandler(this.btnCancelNew_Click);
            // 
            // btnIgnoreNew
            // 
            this.btnIgnoreNew.Image = global::armspan.Properties.Resources.ignore_svg;
            this.btnIgnoreNew.Location = new System.Drawing.Point(45, 0);
            this.btnIgnoreNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnIgnoreNew.Name = "btnIgnoreNew";
            this.btnIgnoreNew.Size = new System.Drawing.Size(45, 45);
            this.btnIgnoreNew.TabIndex = 19;
            this.ttThin.SetToolTip(this.btnIgnoreNew, "Ignore New Event");
            this.btnIgnoreNew.UseVisualStyleBackColor = true;
            this.btnIgnoreNew.Click += new System.EventHandler(this.btnIgnoreNew_Click);
            // 
            // btnOK
            // 
            this.btnOK.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnOK.Location = new System.Drawing.Point(0, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 45);
            this.btnOK.TabIndex = 23;
            this.ttThin.SetToolTip(this.btnOK, "Accept Conflict");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnReschedNew
            // 
            this.btnReschedNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReschedNew.Image = global::armspan.Properties.Resources.schedule_svg;
            this.btnReschedNew.Location = new System.Drawing.Point(289, 0);
            this.btnReschedNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnReschedNew.Name = "btnReschedNew";
            this.btnReschedNew.Size = new System.Drawing.Size(45, 45);
            this.btnReschedNew.TabIndex = 21;
            this.ttThin.SetToolTip(this.btnReschedNew, "Reschedule New Event");
            this.btnReschedNew.UseVisualStyleBackColor = true;
            this.btnReschedNew.Click += new System.EventHandler(this.btnReschedNew_Click);
            // 
            // FormConflictMultiple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 284);
            this.Controls.Add(this.gbNew);
            this.Controls.Add(this.lbConflicts);
            this.Controls.Add(this.rtbConflict);
            this.MinimumSize = new System.Drawing.Size(426, 246);
            this.Name = "FormConflictMultiple";
            this.Text = "FormConflictMultiple";
            this.Load += new System.EventHandler(this.FormConflictMultiple_Load);
            this.Controls.SetChildIndex(this.rtbConflict, 0);
            this.Controls.SetChildIndex(this.lbConflicts, 0);
            this.Controls.SetChildIndex(this.gbNew, 0);
            this.gbNew.ResumeLayout(false);
            this.tlpNew.ResumeLayout(false);
            this.tlpDecline.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbConflict;
        private System.Windows.Forms.ListBox lbConflicts;
        private System.Windows.Forms.GroupBox gbNew;
        private System.Windows.Forms.Button btnReschedNew;
        private System.Windows.Forms.TableLayoutPanel tlpNew;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancelNew;
        private System.Windows.Forms.Button btnIgnoreNew;
        private System.Windows.Forms.Button btnDeleteNew;
        private System.Windows.Forms.TableLayoutPanel tlpDecline;
    }
}