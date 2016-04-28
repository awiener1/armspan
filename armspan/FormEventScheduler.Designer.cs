namespace Span.GUI
{
    partial class FormEventScheduler
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
            this.gbSchedule = new System.Windows.Forms.GroupBox();
            this.btnRemoveOcc = new System.Windows.Forms.Button();
            this.lbSchedule = new System.Windows.Forms.ListBox();
            this.gbNewTime = new System.Windows.Forms.GroupBox();
            this.cbExclude = new System.Windows.Forms.CheckBox();
            this.rbPeriodic = new System.Windows.Forms.RadioButton();
            this.rbManual = new System.Windows.Forms.RadioButton();
            this.btnAddOcc = new System.Windows.Forms.Button();
            this.gbPeriodic = new System.Windows.Forms.GroupBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.dtpOccEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEndAt = new System.Windows.Forms.Label();
            this.cbFrequency = new System.Windows.Forms.ComboBox();
            this.nudFrequency = new System.Windows.Forms.NumericUpDown();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblOften = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbSchedule.SuspendLayout();
            this.gbNewTime.SuspendLayout();
            this.gbPeriodic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSchedule
            // 
            this.gbSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSchedule.Controls.Add(this.btnRemoveOcc);
            this.gbSchedule.Controls.Add(this.lbSchedule);
            this.gbSchedule.Location = new System.Drawing.Point(12, 41);
            this.gbSchedule.Name = "gbSchedule";
            this.gbSchedule.Size = new System.Drawing.Size(588, 126);
            this.gbSchedule.TabIndex = 1;
            this.gbSchedule.TabStop = false;
            this.gbSchedule.Text = "Schedule for this event";
            // 
            // btnRemoveOcc
            // 
            this.btnRemoveOcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveOcc.Image = global::armspan.Properties.Resources.sub_svg;
            this.btnRemoveOcc.Location = new System.Drawing.Point(531, 27);
            this.btnRemoveOcc.Name = "btnRemoveOcc";
            this.btnRemoveOcc.Size = new System.Drawing.Size(45, 45);
            this.btnRemoveOcc.TabIndex = 2;
            this.ttThin.SetToolTip(this.btnRemoveOcc, "Remove Occurrence");
            this.btnRemoveOcc.UseVisualStyleBackColor = true;
            this.btnRemoveOcc.Click += new System.EventHandler(this.btnRemoveOcc_Click);
            // 
            // lbSchedule
            // 
            this.lbSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSchedule.FormattingEnabled = true;
            this.lbSchedule.Location = new System.Drawing.Point(12, 27);
            this.lbSchedule.Name = "lbSchedule";
            this.lbSchedule.ScrollAlwaysVisible = true;
            this.lbSchedule.Size = new System.Drawing.Size(509, 69);
            this.lbSchedule.TabIndex = 0;
            // 
            // gbNewTime
            // 
            this.gbNewTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNewTime.Controls.Add(this.cbExclude);
            this.gbNewTime.Controls.Add(this.rbPeriodic);
            this.gbNewTime.Controls.Add(this.rbManual);
            this.gbNewTime.Controls.Add(this.btnAddOcc);
            this.gbNewTime.Controls.Add(this.gbPeriodic);
            this.gbNewTime.Controls.Add(this.lblTo);
            this.gbNewTime.Controls.Add(this.dtpTo);
            this.gbNewTime.Controls.Add(this.lblFrom);
            this.gbNewTime.Controls.Add(this.dtpFrom);
            this.gbNewTime.Location = new System.Drawing.Point(13, 173);
            this.gbNewTime.Name = "gbNewTime";
            this.gbNewTime.Size = new System.Drawing.Size(586, 160);
            this.gbNewTime.TabIndex = 2;
            this.gbNewTime.TabStop = false;
            this.gbNewTime.Text = "New Time";
            // 
            // cbExclude
            // 
            this.cbExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExclude.AutoSize = true;
            this.cbExclude.Location = new System.Drawing.Point(498, 18);
            this.cbExclude.Name = "cbExclude";
            this.cbExclude.Size = new System.Drawing.Size(63, 17);
            this.cbExclude.TabIndex = 8;
            this.cbExclude.Text = "Exclude";
            this.ttThin.SetToolTip(this.cbExclude, "When checked, the event will NEVER happen\r\nat the specified time(s).");
            this.cbExclude.UseVisualStyleBackColor = true;
            // 
            // rbPeriodic
            // 
            this.rbPeriodic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPeriodic.AutoSize = true;
            this.rbPeriodic.Location = new System.Drawing.Point(227, 18);
            this.rbPeriodic.Name = "rbPeriodic";
            this.rbPeriodic.Size = new System.Drawing.Size(62, 17);
            this.rbPeriodic.TabIndex = 7;
            this.rbPeriodic.TabStop = true;
            this.rbPeriodic.Text = "Periodic";
            this.ttThin.SetToolTip(this.rbPeriodic, "Periodic Repeating Occurrences");
            this.rbPeriodic.UseVisualStyleBackColor = true;
            // 
            // rbManual
            // 
            this.rbManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbManual.AutoSize = true;
            this.rbManual.Checked = true;
            this.rbManual.Location = new System.Drawing.Point(16, 18);
            this.rbManual.Name = "rbManual";
            this.rbManual.Size = new System.Drawing.Size(59, 17);
            this.rbManual.TabIndex = 6;
            this.rbManual.TabStop = true;
            this.rbManual.Text = "Manual";
            this.ttThin.SetToolTip(this.rbManual, "One Manually Defined Occurrence");
            this.rbManual.UseVisualStyleBackColor = true;
            this.rbManual.CheckedChanged += new System.EventHandler(this.rbManual_CheckedChanged);
            // 
            // btnAddOcc
            // 
            this.btnAddOcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddOcc.Image = global::armspan.Properties.Resources.add_svg;
            this.btnAddOcc.Location = new System.Drawing.Point(11, 109);
            this.btnAddOcc.Name = "btnAddOcc";
            this.btnAddOcc.Size = new System.Drawing.Size(45, 45);
            this.btnAddOcc.TabIndex = 3;
            this.ttThin.SetToolTip(this.btnAddOcc, "Add Occurrence");
            this.btnAddOcc.UseVisualStyleBackColor = true;
            this.btnAddOcc.Click += new System.EventHandler(this.btnAddOcc_Click);
            // 
            // gbPeriodic
            // 
            this.gbPeriodic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPeriodic.Controls.Add(this.lblDuration);
            this.gbPeriodic.Controls.Add(this.dtpOccEnd);
            this.gbPeriodic.Controls.Add(this.lblEndAt);
            this.gbPeriodic.Controls.Add(this.cbFrequency);
            this.gbPeriodic.Controls.Add(this.nudFrequency);
            this.gbPeriodic.Controls.Add(this.lblFrequency);
            this.gbPeriodic.Controls.Add(this.lblOften);
            this.gbPeriodic.Location = new System.Drawing.Point(227, 45);
            this.gbPeriodic.Name = "gbPeriodic";
            this.gbPeriodic.Size = new System.Drawing.Size(348, 110);
            this.gbPeriodic.TabIndex = 4;
            this.gbPeriodic.TabStop = false;
            // 
            // lblDuration
            // 
            this.lblDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(7, 64);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(92, 13);
            this.lblDuration.TabIndex = 6;
            this.lblDuration.Text = "Duration of Each?";
            // 
            // dtpOccEnd
            // 
            this.dtpOccEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpOccEnd.CustomFormat = "M/d/yy \'at\' h:mm tt";
            this.dtpOccEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOccEnd.Location = new System.Drawing.Point(179, 83);
            this.dtpOccEnd.Name = "dtpOccEnd";
            this.dtpOccEnd.Size = new System.Drawing.Size(145, 21);
            this.dtpOccEnd.TabIndex = 5;
            // 
            // lblEndAt
            // 
            this.lblEndAt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEndAt.AutoSize = true;
            this.lblEndAt.Location = new System.Drawing.Point(44, 87);
            this.lblEndAt.Name = "lblEndAt";
            this.lblEndAt.Size = new System.Drawing.Size(123, 13);
            this.lblEndAt.TabIndex = 4;
            this.lblEndAt.Text = "First occurrence ends at";
            // 
            // cbFrequency
            // 
            this.cbFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFrequency.DisplayMember = "Minutes";
            this.cbFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrequency.FormattingEnabled = true;
            this.cbFrequency.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days",
            "Weeks",
            "Months",
            "Years"});
            this.cbFrequency.Location = new System.Drawing.Point(203, 31);
            this.cbFrequency.Name = "cbFrequency";
            this.cbFrequency.Size = new System.Drawing.Size(121, 21);
            this.cbFrequency.TabIndex = 3;
            // 
            // nudFrequency
            // 
            this.nudFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFrequency.Location = new System.Drawing.Point(110, 31);
            this.nudFrequency.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFrequency.Name = "nudFrequency";
            this.nudFrequency.Size = new System.Drawing.Size(66, 21);
            this.nudFrequency.TabIndex = 2;
            this.nudFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblFrequency
            // 
            this.lblFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(41, 35);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(63, 13);
            this.lblFrequency.TabIndex = 1;
            this.lblFrequency.Text = "Once every";
            // 
            // lblOften
            // 
            this.lblOften.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOften.AutoSize = true;
            this.lblOften.Location = new System.Drawing.Point(7, 12);
            this.lblOften.Name = "lblOften";
            this.lblOften.Size = new System.Drawing.Size(64, 13);
            this.lblOften.TabIndex = 0;
            this.lblOften.Text = "How Often?";
            // 
            // lblTo
            // 
            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(27, 86);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(19, 13);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "To";
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTo.CustomFormat = "M/d/yy \'at\' h:mm tt";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(53, 83);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(145, 21);
            this.dtpTo.TabIndex = 2;
            // 
            // lblFrom
            // 
            this.lblFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(13, 59);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(31, 13);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "From";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFrom.CustomFormat = "M/d/yy \'at\' h:mm tt";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(53, 56);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(145, 21);
            this.dtpFrom.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnOK.Location = new System.Drawing.Point(554, 340);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 45);
            this.btnOK.TabIndex = 5;
            this.ttThin.SetToolTip(this.btnOK, "OK");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Image = global::armspan.Properties.Resources.cancel_svg;
            this.btnCancel.Location = new System.Drawing.Point(12, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(45, 45);
            this.btnCancel.TabIndex = 9;
            this.ttThin.SetToolTip(this.btnCancel, "Add Occurrence");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormEventScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 392);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbNewTime);
            this.Controls.Add(this.gbSchedule);
            this.MinimumSize = new System.Drawing.Size(614, 400);
            this.Name = "FormEventScheduler";
            this.Text = "FormEventScheduler";
            this.Load += new System.EventHandler(this.FormEventScheduler_Load);
            this.Controls.SetChildIndex(this.gbSchedule, 0);
            this.Controls.SetChildIndex(this.gbNewTime, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.gbSchedule.ResumeLayout(false);
            this.gbNewTime.ResumeLayout(false);
            this.gbNewTime.PerformLayout();
            this.gbPeriodic.ResumeLayout(false);
            this.gbPeriodic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrequency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSchedule;
        private System.Windows.Forms.ListBox lbSchedule;
        private System.Windows.Forms.Button btnRemoveOcc;
        private System.Windows.Forms.GroupBox gbNewTime;
        private System.Windows.Forms.Button btnAddOcc;
        private System.Windows.Forms.GroupBox gbPeriodic;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.DateTimePicker dtpOccEnd;
        private System.Windows.Forms.Label lblEndAt;
        private System.Windows.Forms.ComboBox cbFrequency;
        private System.Windows.Forms.NumericUpDown nudFrequency;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Label lblOften;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox cbExclude;
        private System.Windows.Forms.RadioButton rbPeriodic;
        private System.Windows.Forms.RadioButton rbManual;
        private System.Windows.Forms.Button btnCancel;
    }
}