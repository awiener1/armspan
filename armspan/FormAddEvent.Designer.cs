namespace Span.GUI
{
    partial class FormAddEvent
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
            this.lblType = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnTime = new System.Windows.Forms.Button();
            this.lblTaskNum = new System.Windows.Forms.Label();
            this.nudTaskNum = new System.Windows.Forms.NumericUpDown();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.gbCategories = new System.Windows.Forms.GroupBox();
            this.btnCatSec = new System.Windows.Forms.Button();
            this.cbCatPrim = new System.Windows.Forms.ComboBox();
            this.lblCatSec = new System.Windows.Forms.Label();
            this.lblCatPrim = new System.Windows.Forms.Label();
            this.lblAlarm = new System.Windows.Forms.Label();
            this.btnAddOcc = new System.Windows.Forms.Button();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskNum)).BeginInit();
            this.gbCategories.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 44);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "Type";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Enabled = false;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Appointment",
            "Task"});
            this.cbType.Location = new System.Drawing.Point(66, 41);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(139, 21);
            this.cbType.TabIndex = 2;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 78);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(34, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(66, 78);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(396, 21);
            this.tbName.TabIndex = 4;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(12, 116);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(29, 13);
            this.lblTime.TabIndex = 5;
            this.lblTime.Text = "Time";
            // 
            // btnTime
            // 
            this.btnTime.Location = new System.Drawing.Point(66, 110);
            this.btnTime.Name = "btnTime";
            this.btnTime.Size = new System.Drawing.Size(31, 25);
            this.btnTime.TabIndex = 6;
            this.btnTime.Text = "...";
            this.btnTime.UseVisualStyleBackColor = true;
            this.btnTime.Click += new System.EventHandler(this.btnTime_Click);
            // 
            // lblTaskNum
            // 
            this.lblTaskNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaskNum.AutoSize = true;
            this.lblTaskNum.Location = new System.Drawing.Point(113, 116);
            this.lblTaskNum.Name = "lblTaskNum";
            this.lblTaskNum.Size = new System.Drawing.Size(276, 13);
            this.lblTaskNum.TabIndex = 7;
            this.lblTaskNum.Text = "Minimum number of times to do the task per occurrence:";
            // 
            // nudTaskNum
            // 
            this.nudTaskNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTaskNum.Location = new System.Drawing.Point(396, 110);
            this.nudTaskNum.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nudTaskNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTaskNum.Name = "nudTaskNum";
            this.nudTaskNum.Size = new System.Drawing.Size(66, 21);
            this.nudTaskNum.TabIndex = 8;
            this.nudTaskNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tbLocation
            // 
            this.tbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocation.Location = new System.Drawing.Point(66, 152);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(396, 21);
            this.tbLocation.TabIndex = 10;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(12, 152);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(32, 13);
            this.lblLocation.TabIndex = 9;
            this.lblLocation.Text = "Place";
            // 
            // gbCategories
            // 
            this.gbCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCategories.Controls.Add(this.btnCatSec);
            this.gbCategories.Controls.Add(this.cbCatPrim);
            this.gbCategories.Controls.Add(this.lblCatSec);
            this.gbCategories.Controls.Add(this.lblCatPrim);
            this.gbCategories.Location = new System.Drawing.Point(15, 190);
            this.gbCategories.Name = "gbCategories";
            this.gbCategories.Size = new System.Drawing.Size(447, 58);
            this.gbCategories.TabIndex = 11;
            this.gbCategories.TabStop = false;
            this.gbCategories.Text = "Categories";
            // 
            // btnCatSec
            // 
            this.btnCatSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCatSec.Location = new System.Drawing.Point(407, 20);
            this.btnCatSec.Name = "btnCatSec";
            this.btnCatSec.Size = new System.Drawing.Size(31, 25);
            this.btnCatSec.TabIndex = 13;
            this.btnCatSec.Text = "...";
            this.btnCatSec.UseVisualStyleBackColor = true;
            this.btnCatSec.Click += new System.EventHandler(this.btnCatSec_Click);
            // 
            // cbCatPrim
            // 
            this.cbCatPrim.DisplayMember = "Name";
            this.cbCatPrim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCatPrim.FormattingEnabled = true;
            this.cbCatPrim.Location = new System.Drawing.Point(65, 23);
            this.cbCatPrim.Name = "cbCatPrim";
            this.cbCatPrim.Size = new System.Drawing.Size(139, 21);
            this.cbCatPrim.TabIndex = 13;
            this.cbCatPrim.ValueMember = "Id";
            this.cbCatPrim.SelectedIndexChanged += new System.EventHandler(this.cbCatPrim_SelectedIndexChanged);
            // 
            // lblCatSec
            // 
            this.lblCatSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCatSec.AutoSize = true;
            this.lblCatSec.Location = new System.Drawing.Point(295, 26);
            this.lblCatSec.Name = "lblCatSec";
            this.lblCatSec.Size = new System.Drawing.Size(100, 13);
            this.lblCatSec.TabIndex = 12;
            this.lblCatSec.Text = "All other categories";
            // 
            // lblCatPrim
            // 
            this.lblCatPrim.AutoSize = true;
            this.lblCatPrim.Location = new System.Drawing.Point(11, 26);
            this.lblCatPrim.Name = "lblCatPrim";
            this.lblCatPrim.Size = new System.Drawing.Size(43, 13);
            this.lblCatPrim.TabIndex = 12;
            this.lblCatPrim.Text = "Primary";
            // 
            // lblAlarm
            // 
            this.lblAlarm.AutoSize = true;
            this.lblAlarm.Location = new System.Drawing.Point(12, 271);
            this.lblAlarm.Name = "lblAlarm";
            this.lblAlarm.Size = new System.Drawing.Size(39, 13);
            this.lblAlarm.TabIndex = 12;
            this.lblAlarm.Text = "Alarms";
            // 
            // btnAddOcc
            // 
            this.btnAddOcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddOcc.Image = global::armspan.Properties.Resources.add_svg;
            this.btnAddOcc.Location = new System.Drawing.Point(417, 395);
            this.btnAddOcc.Name = "btnAddOcc";
            this.btnAddOcc.Size = new System.Drawing.Size(45, 45);
            this.btnAddOcc.TabIndex = 13;
            this.ttThin.SetToolTip(this.btnAddOcc, "Add Occurrence");
            this.btnAddOcc.UseVisualStyleBackColor = true;
            this.btnAddOcc.Click += new System.EventHandler(this.btnAddOcc_Click);
            // 
            // btnAlarm
            // 
            this.btnAlarm.Image = global::armspan.Properties.Resources.alarm2_svg;
            this.btnAlarm.Location = new System.Drawing.Point(57, 255);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(45, 45);
            this.btnAlarm.TabIndex = 14;
            this.ttThin.SetToolTip(this.btnAlarm, "Add Occurrence");
            this.btnAlarm.UseVisualStyleBackColor = true;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(12, 319);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 15;
            this.lblDesc.Text = "Description";
            // 
            // tbDesc
            // 
            this.tbDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDesc.Location = new System.Drawing.Point(13, 338);
            this.tbDesc.Multiline = true;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDesc.Size = new System.Drawing.Size(449, 47);
            this.tbDesc.TabIndex = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Image = global::armspan.Properties.Resources.cancel_svg;
            this.btnCancel.Location = new System.Drawing.Point(13, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(45, 45);
            this.btnCancel.TabIndex = 17;
            this.ttThin.SetToolTip(this.btnCancel, "Add Occurrence");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormAddEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 448);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbDesc);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.btnAlarm);
            this.Controls.Add(this.btnAddOcc);
            this.Controls.Add(this.lblAlarm);
            this.Controls.Add(this.gbCategories);
            this.Controls.Add(this.tbLocation);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.nudTaskNum);
            this.Controls.Add(this.lblTaskNum);
            this.Controls.Add(this.btnTime);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.lblType);
            this.MinimumSize = new System.Drawing.Size(474, 452);
            this.Name = "FormAddEvent";
            this.Text = "FormAddEvent";
         
            this.Controls.SetChildIndex(this.lblType, 0);
            this.Controls.SetChildIndex(this.cbType, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.lblTime, 0);
            this.Controls.SetChildIndex(this.btnTime, 0);
            this.Controls.SetChildIndex(this.lblTaskNum, 0);
            this.Controls.SetChildIndex(this.nudTaskNum, 0);
            this.Controls.SetChildIndex(this.lblLocation, 0);
            this.Controls.SetChildIndex(this.tbLocation, 0);
            this.Controls.SetChildIndex(this.gbCategories, 0);
            this.Controls.SetChildIndex(this.lblAlarm, 0);
            this.Controls.SetChildIndex(this.btnAddOcc, 0);
            this.Controls.SetChildIndex(this.btnAlarm, 0);
            this.Controls.SetChildIndex(this.lblDesc, 0);
            this.Controls.SetChildIndex(this.tbDesc, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskNum)).EndInit();
            this.gbCategories.ResumeLayout(false);
            this.gbCategories.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnTime;
        private System.Windows.Forms.Label lblTaskNum;
        private System.Windows.Forms.NumericUpDown nudTaskNum;
        private System.Windows.Forms.TextBox tbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.GroupBox gbCategories;
        private System.Windows.Forms.ComboBox cbCatPrim;
        private System.Windows.Forms.Label lblCatPrim;
        private System.Windows.Forms.Button btnCatSec;
        private System.Windows.Forms.Label lblCatSec;
        private System.Windows.Forms.Label lblAlarm;
        private System.Windows.Forms.Button btnAddOcc;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.Button btnCancel;
    }
}