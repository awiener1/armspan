namespace Span.GUI
{
    partial class ThinDialog
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
            this.dragBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dragBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dragBox
            // 
            this.dragBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dragBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.dragBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dragBox.Location = new System.Drawing.Point(12, 12);
            this.dragBox.Name = "dragBox";
            this.dragBox.Size = new System.Drawing.Size(268, 14);
            this.dragBox.TabIndex = 0;
            this.dragBox.TabStop = false;
            this.dragBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragBox_MouseDown);
            this.dragBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dragBox_MouseMove);
            // 
            // ThinDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.dragBox);
            this.Name = "ThinDialog";
            this.Text = "ThinDialog";
            this.Load += new System.EventHandler(this.ThinDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dragBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox dragBox;
    }
}