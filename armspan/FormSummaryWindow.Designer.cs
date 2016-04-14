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
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblTimeframe = new System.Windows.Forms.Label();
            this.cbTimeframe = new System.Windows.Forms.ComboBox();
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
            // 
            // gbSummary
            // 
            this.gbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSummary.Controls.Add(this.tbName);
            this.gbSummary.Location = new System.Drawing.Point(12, 46);
            this.gbSummary.Name = "gbSummary";
            this.gbSummary.Size = new System.Drawing.Size(422, 310);
            this.gbSummary.TabIndex = 25;
            this.gbSummary.TabStop = false;
            this.gbSummary.Text = "Summary";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.BackColor = System.Drawing.SystemColors.Window;
            this.tbName.Location = new System.Drawing.Point(9, 17);
            this.tbName.Multiline = true;
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbName.Size = new System.Drawing.Size(403, 284);
            this.tbName.TabIndex = 6;
            this.tbName.Text = "Here\'s the summary\r\n\r\nIt\'s going to be objective, not condescending.\r\n\r\nI can\'t b" +
                "elieve I need to make that clear.";
            // 
            // lblTimeframe
            // 
            this.lblTimeframe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTimeframe.AutoSize = true;
            this.lblTimeframe.Location = new System.Drawing.Point(12, 379);
            this.lblTimeframe.Name = "lblTimeframe";
            this.lblTimeframe.Size = new System.Drawing.Size(57, 13);
            this.lblTimeframe.TabIndex = 5;
            this.lblTimeframe.Text = "Timeframe";
            // 
            // cbTimeframe
            // 
            this.cbTimeframe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbTimeframe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimeframe.FormattingEnabled = true;
            this.cbTimeframe.Items.AddRange(new object[] {
            "Past day",
            "Past week",
            "Past month",
            "Past year",
            "All events"});
            this.cbTimeframe.Location = new System.Drawing.Point(75, 376);
            this.cbTimeframe.Name = "cbTimeframe";
            this.cbTimeframe.Size = new System.Drawing.Size(103, 21);
            this.cbTimeframe.TabIndex = 27;
            // 
            // FormSummaryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 417);
            this.Controls.Add(this.cbTimeframe);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbSummary);
            this.Controls.Add(this.lblTimeframe);
            this.MinimumSize = new System.Drawing.Size(300, 280);
            this.Name = "FormSummaryWindow";
            this.Text = "FormSummaryWindow";
            this.Load += new System.EventHandler(this.FormSummaryWindow_Load);
            this.Controls.SetChildIndex(this.lblTimeframe, 0);
            this.Controls.SetChildIndex(this.gbSummary, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.cbTimeframe, 0);
            this.gbSummary.ResumeLayout(false);
            this.gbSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbSummary;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblTimeframe;
        private System.Windows.Forms.ComboBox cbTimeframe;
    }
}