namespace Span.GUI
{
    partial class FormOccurrenceScheduler
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblOccInfo = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Image = global::armspan.Properties.Resources.cancel_svg;
            this.btnCancel.Location = new System.Drawing.Point(12, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(45, 45);
            this.btnCancel.TabIndex = 11;
            this.ttThin.SetToolTip(this.btnCancel, "Add Occurrence");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnOK.Location = new System.Drawing.Point(248, 159);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 45);
            this.btnOK.TabIndex = 10;
            this.ttThin.SetToolTip(this.btnOK, "OK");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblOccInfo
            // 
            this.lblOccInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOccInfo.Location = new System.Drawing.Point(12, 38);
            this.lblOccInfo.Name = "lblOccInfo";
            this.lblOccInfo.Size = new System.Drawing.Size(281, 52);
            this.lblOccInfo.TabIndex = 12;
            this.lblOccInfo.Text = "Occurrence Event Name\r\nInitially datetime to datetime";
            this.lblOccInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTo
            // 
            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(34, 123);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(19, 13);
            this.lblTo.TabIndex = 16;
            this.lblTo.Text = "To";
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTo.CustomFormat = "M/d/yy \'at\' h:mm tt";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(60, 120);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(220, 21);
            this.dtpTo.TabIndex = 15;
            // 
            // lblFrom
            // 
            this.lblFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(20, 96);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(31, 13);
            this.lblFrom.TabIndex = 14;
            this.lblFrom.Text = "From";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFrom.CustomFormat = "M/d/yy \'at\' h:mm tt";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(60, 93);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(220, 21);
            this.dtpFrom.TabIndex = 13;
            // 
            // FormOccurrenceScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 216);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblOccInfo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MinimumSize = new System.Drawing.Size(246, 224);
            this.Name = "FormOccurrenceScheduler";
            this.Text = "FormOccurrenceScheduler";
            this.Load += new System.EventHandler(this.FormOccurrenceScheduler_Load);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblOccInfo, 0);
            this.Controls.SetChildIndex(this.dtpFrom, 0);
            this.Controls.SetChildIndex(this.lblFrom, 0);
            this.Controls.SetChildIndex(this.dtpTo, 0);
            this.Controls.SetChildIndex(this.lblTo, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblOccInfo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;

    }
}