namespace Span.GUI
{
    partial class FormSummaryWindow
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
            this.btnOK = new System.Windows.Forms.Button();
            this.gbSummary = new System.Windows.Forms.GroupBox();
            this.tbSummary = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.gbSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnOK.Location = new System.Drawing.Point(389, 363);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 45);
            this.btnOK.TabIndex = 26;
            this.ttThin.SetToolTip(this.btnOK, "Confirm Category");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbSummary
            // 
            this.gbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSummary.Controls.Add(this.tbSummary);
            this.gbSummary.Location = new System.Drawing.Point(12, 46);
            this.gbSummary.Name = "gbSummary";
            this.gbSummary.Size = new System.Drawing.Size(422, 310);
            this.gbSummary.TabIndex = 25;
            this.gbSummary.TabStop = false;
            this.gbSummary.Text = "Summary";
            // 
            // tbSummary
            // 
            this.tbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSummary.BackColor = System.Drawing.SystemColors.Window;
            this.tbSummary.Location = new System.Drawing.Point(9, 17);
            this.tbSummary.Multiline = true;
            this.tbSummary.Name = "tbSummary";
            this.tbSummary.ReadOnly = true;
            this.tbSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSummary.Size = new System.Drawing.Size(403, 284);
            this.tbSummary.TabIndex = 6;
            // 
            // lblTo
            // 
            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(34, 392);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(19, 13);
            this.lblTo.TabIndex = 30;
            this.lblTo.Text = "To";
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTo.CustomFormat = "M/d/yy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(60, 389);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(68, 21);
            this.dtpTo.TabIndex = 29;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(20, 365);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(31, 13);
            this.lblFrom.TabIndex = 28;
            this.lblFrom.Text = "From";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFrom.CustomFormat = "M/d/yy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(60, 362);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(68, 21);
            this.dtpFrom.TabIndex = 27;
            this.dtpFrom.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // FormSummaryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 417);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbSummary);
            this.MinimumSize = new System.Drawing.Size(300, 280);
            this.Name = "FormSummaryWindow";
            this.Text = "FormSummaryWindow";
            this.Load += new System.EventHandler(this.FormSummaryWindow_Load);
            this.Controls.SetChildIndex(this.gbSummary, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.dtpFrom, 0);
            this.Controls.SetChildIndex(this.lblFrom, 0);
            this.Controls.SetChildIndex(this.dtpTo, 0);
            this.Controls.SetChildIndex(this.lblTo, 0);
            this.gbSummary.ResumeLayout(false);
            this.gbSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbSummary;
        private System.Windows.Forms.TextBox tbSummary;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
    }
}