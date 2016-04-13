namespace Span.GUI
{
    partial class FormAlarmWindow
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
            this.gbCurrent = new System.Windows.Forms.GroupBox();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.btnNow = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAffirm = new System.Windows.Forms.Button();
            this.btnAlarmSet = new System.Windows.Forms.Button();
            this.nudPostponeOcc = new System.Windows.Forms.NumericUpDown();
            this.btnPostpone = new System.Windows.Forms.Button();
            this.pnlCurrent = new System.Windows.Forms.Panel();
            this.cbMore = new System.Windows.Forms.CheckBox();
            this.rtbAlarm = new System.Windows.Forms.RichTextBox();
            this.btnPostponeFive = new System.Windows.Forms.Button();
            this.gbNext = new System.Windows.Forms.GroupBox();
            this.lbNext = new System.Windows.Forms.ListBox();
            this.gbCurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPostponeOcc)).BeginInit();
            this.pnlCurrent.SuspendLayout();
            this.gbNext.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCurrent
            // 
            this.gbCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCurrent.Controls.Add(this.btnRepeat);
            this.gbCurrent.Controls.Add(this.btnNow);
            this.gbCurrent.Controls.Add(this.btnIgnore);
            this.gbCurrent.Controls.Add(this.btnCancel);
            this.gbCurrent.Controls.Add(this.btnAffirm);
            this.gbCurrent.Controls.Add(this.btnAlarmSet);
            this.gbCurrent.Controls.Add(this.nudPostponeOcc);
            this.gbCurrent.Controls.Add(this.btnPostpone);
            this.gbCurrent.Controls.Add(this.pnlCurrent);
            this.gbCurrent.Location = new System.Drawing.Point(12, 80);
            this.gbCurrent.Name = "gbCurrent";
            this.gbCurrent.Size = new System.Drawing.Size(532, 204);
            this.gbCurrent.TabIndex = 1;
            this.gbCurrent.TabStop = false;
            this.gbCurrent.Text = "Current Alarm";
            // 
            // btnRepeat
            // 
            this.btnRepeat.Enabled = false;
            this.btnRepeat.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnRepeat.Image = global::armspan.Properties.Resources.repeat_svg;
            this.btnRepeat.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRepeat.Location = new System.Drawing.Point(272, 121);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(65, 77);
            this.btnRepeat.TabIndex = 18;
            this.btnRepeat.Text = "Do Again";
            this.btnRepeat.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttThin.SetToolTip(this.btnRepeat, "Do Task Again");
            this.btnRepeat.UseVisualStyleBackColor = true;
            // 
            // btnNow
            // 
            this.btnNow.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnNow.Image = global::armspan.Properties.Resources.now_svg;
            this.btnNow.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNow.Location = new System.Drawing.Point(207, 121);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(65, 77);
            this.btnNow.TabIndex = 17;
            this.btnNow.Text = "Start Now";
            this.btnNow.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttThin.SetToolTip(this.btnNow, "Start / Stop Now");
            this.btnNow.UseVisualStyleBackColor = true;
            // 
            // btnIgnore
            // 
            this.btnIgnore.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnIgnore.Image = global::armspan.Properties.Resources.ignore_svg;
            this.btnIgnore.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnIgnore.Location = new System.Drawing.Point(142, 121);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(65, 77);
            this.btnIgnore.TabIndex = 16;
            this.btnIgnore.Text = "No; Ignore";
            this.btnIgnore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttThin.SetToolTip(this.btnIgnore, "Ignore");
            this.btnIgnore.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnCancel.Image = global::armspan.Properties.Resources.cancel_svg;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancel.Location = new System.Drawing.Point(77, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 77);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "No; Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttThin.SetToolTip(this.btnCancel, "Cancel");
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAffirm
            // 
            this.btnAffirm.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnAffirm.Image = global::armspan.Properties.Resources.ok_svg;
            this.btnAffirm.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAffirm.Location = new System.Drawing.Point(12, 121);
            this.btnAffirm.Name = "btnAffirm";
            this.btnAffirm.Size = new System.Drawing.Size(65, 77);
            this.btnAffirm.TabIndex = 14;
            this.btnAffirm.Text = "Yes";
            this.btnAffirm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttThin.SetToolTip(this.btnAffirm, "Affirm");
            this.btnAffirm.UseVisualStyleBackColor = true;
            // 
            // btnAlarmSet
            // 
            this.btnAlarmSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlarmSet.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnAlarmSet.Image = global::armspan.Properties.Resources.alarm2_svg;
            this.btnAlarmSet.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAlarmSet.Location = new System.Drawing.Point(451, 121);
            this.btnAlarmSet.Name = "btnAlarmSet";
            this.btnAlarmSet.Size = new System.Drawing.Size(65, 77);
            this.btnAlarmSet.TabIndex = 13;
            this.btnAlarmSet.Text = "Alarm Settings";
            this.btnAlarmSet.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttThin.SetToolTip(this.btnAlarmSet, "Change Alarm Settings");
            this.btnAlarmSet.UseVisualStyleBackColor = true;
            // 
            // nudPostponeOcc
            // 
            this.nudPostponeOcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPostponeOcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPostponeOcc.Font = new System.Drawing.Font("Tahoma", 10F);
            this.nudPostponeOcc.Location = new System.Drawing.Point(403, 121);
            this.nudPostponeOcc.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudPostponeOcc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPostponeOcc.Name = "nudPostponeOcc";
            this.nudPostponeOcc.Size = new System.Drawing.Size(42, 24);
            this.nudPostponeOcc.TabIndex = 12;
            this.nudPostponeOcc.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnPostpone
            // 
            this.btnPostpone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPostpone.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnPostpone.Image = global::armspan.Properties.Resources.postpone_svg;
            this.btnPostpone.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPostpone.Location = new System.Drawing.Point(338, 121);
            this.btnPostpone.Name = "btnPostpone";
            this.btnPostpone.Size = new System.Drawing.Size(65, 77);
            this.btnPostpone.TabIndex = 11;
            this.btnPostpone.Text = "Postpone";
            this.btnPostpone.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttThin.SetToolTip(this.btnPostpone, "Postpone");
            this.btnPostpone.UseVisualStyleBackColor = true;
            // 
            // pnlCurrent
            // 
            this.pnlCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCurrent.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlCurrent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCurrent.Controls.Add(this.cbMore);
            this.pnlCurrent.Controls.Add(this.rtbAlarm);
            this.pnlCurrent.Location = new System.Drawing.Point(12, 22);
            this.pnlCurrent.Name = "pnlCurrent";
            this.pnlCurrent.Size = new System.Drawing.Size(503, 86);
            this.pnlCurrent.TabIndex = 0;
            // 
            // cbMore
            // 
            this.cbMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMore.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbMore.BackColor = System.Drawing.SystemColors.Control;
            this.cbMore.Image = global::armspan.Properties.Resources.dropdown;
            this.cbMore.Location = new System.Drawing.Point(461, 47);
            this.cbMore.Name = "cbMore";
            this.cbMore.Size = new System.Drawing.Size(35, 23);
            this.cbMore.TabIndex = 6;
            this.cbMore.UseVisualStyleBackColor = false;
            // 
            // rtbAlarm
            // 
            this.rtbAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbAlarm.BackColor = System.Drawing.SystemColors.Window;
            this.rtbAlarm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbAlarm.DetectUrls = false;
            this.rtbAlarm.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbAlarm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rtbAlarm.Location = new System.Drawing.Point(12, 10);
            this.rtbAlarm.Name = "rtbAlarm";
            this.rtbAlarm.ReadOnly = true;
            this.rtbAlarm.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbAlarm.Size = new System.Drawing.Size(443, 59);
            this.rtbAlarm.TabIndex = 5;
            this.rtbAlarm.Text = "";
            // 
            // btnPostponeFive
            // 
            this.btnPostponeFive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPostponeFive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPostponeFive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnPostponeFive.Image = global::armspan.Properties.Resources.postponesmall_svg;
            this.btnPostponeFive.Location = new System.Drawing.Point(496, 26);
            this.btnPostponeFive.Name = "btnPostponeFive";
            this.btnPostponeFive.Size = new System.Drawing.Size(48, 48);
            this.btnPostponeFive.TabIndex = 2;
            this.btnPostponeFive.Text = "5";
            this.btnPostponeFive.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.ttThin.SetToolTip(this.btnPostponeFive, "Postpone for 5 Minutes");
            this.btnPostponeFive.UseVisualStyleBackColor = true;
            this.btnPostponeFive.Click += new System.EventHandler(this.btnPostponeFive_Click);
            // 
            // gbNext
            // 
            this.gbNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNext.Controls.Add(this.lbNext);
            this.gbNext.Location = new System.Drawing.Point(32, 316);
            this.gbNext.Name = "gbNext";
            this.gbNext.Size = new System.Drawing.Size(477, 107);
            this.gbNext.TabIndex = 3;
            this.gbNext.TabStop = false;
            this.gbNext.Text = "Next Alarms";
            // 
            // lbNext
            // 
            this.lbNext.FormattingEnabled = true;
            this.lbNext.Location = new System.Drawing.Point(14, 17);
            this.lbNext.Name = "lbNext";
            this.lbNext.ScrollAlwaysVisible = true;
            this.lbNext.Size = new System.Drawing.Size(450, 82);
            this.lbNext.TabIndex = 0;
            // 
            // FormAlarmWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 445);
            this.Controls.Add(this.gbNext);
            this.Controls.Add(this.btnPostponeFive);
            this.Controls.Add(this.gbCurrent);
            this.MinimumSize = new System.Drawing.Size(564, 8);
            this.Name = "FormAlarmWindow";
            this.Text = "FormAlarmWindow";
            this.Load += new System.EventHandler(this.FormAlarmWindow_Load);
            this.Controls.SetChildIndex(this.gbCurrent, 0);
            this.Controls.SetChildIndex(this.btnPostponeFive, 0);
            this.Controls.SetChildIndex(this.gbNext, 0);
            this.gbCurrent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudPostponeOcc)).EndInit();
            this.pnlCurrent.ResumeLayout(false);
            this.gbNext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCurrent;
        private System.Windows.Forms.Button btnPostponeFive;
        private System.Windows.Forms.Panel pnlCurrent;
        private System.Windows.Forms.RichTextBox rtbAlarm;
        private System.Windows.Forms.CheckBox cbMore;
        private System.Windows.Forms.Button btnAlarmSet;
        private System.Windows.Forms.NumericUpDown nudPostponeOcc;
        private System.Windows.Forms.Button btnPostpone;
        private System.Windows.Forms.Button btnAffirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.Button btnNow;
        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.GroupBox gbNext;
        private System.Windows.Forms.ListBox lbNext;
    }
}