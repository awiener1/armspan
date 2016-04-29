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
            this.btnOK = new System.Windows.Forms.Button();
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
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnOK.Location = new System.Drawing.Point(435, 230);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 45);
            this.btnOK.TabIndex = 23;
            this.ttThin.SetToolTip(this.btnOK, "Accept Conflict");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormConflictMultiple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 284);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbConflicts);
            this.Controls.Add(this.rtbConflict);
            this.MinimumSize = new System.Drawing.Size(426, 246);
            this.Name = "FormConflictMultiple";
            this.Text = "FormConflictMultiple";
            this.Load += new System.EventHandler(this.FormConflictMultiple_Load);
            this.Controls.SetChildIndex(this.rtbConflict, 0);
            this.Controls.SetChildIndex(this.lbConflicts, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbConflict;
        private System.Windows.Forms.ListBox lbConflicts;
        private System.Windows.Forms.Button btnOK;
    }
}