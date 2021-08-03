
namespace IdSort
{
    partial class Importer
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
            this.importProgressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.importingFromLabel = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.currentlyWorkingOnLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // importProgressBar
            // 
            this.importProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importProgressBar.Location = new System.Drawing.Point(12, 59);
            this.importProgressBar.Name = "importProgressBar";
            this.importProgressBar.Size = new System.Drawing.Size(530, 23);
            this.importProgressBar.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(467, 93);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Importing From:";
            // 
            // importingFromLabel
            // 
            this.importingFromLabel.AutoSize = true;
            this.importingFromLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.importingFromLabel.Location = new System.Drawing.Point(91, 9);
            this.importingFromLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.importingFromLabel.Name = "importingFromLabel";
            this.importingFromLabel.Size = new System.Drawing.Size(22, 13);
            this.importingFromLabel.TabIndex = 3;
            this.importingFromLabel.Text = "C:\\";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(12, 33);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(0, 13);
            this.progressLabel.TabIndex = 4;
            // 
            // currentlyWorkingOnLabel
            // 
            this.currentlyWorkingOnLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.currentlyWorkingOnLabel.AutoSize = true;
            this.currentlyWorkingOnLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.currentlyWorkingOnLabel.Location = new System.Drawing.Point(12, 98);
            this.currentlyWorkingOnLabel.Name = "currentlyWorkingOnLabel";
            this.currentlyWorkingOnLabel.Size = new System.Drawing.Size(92, 13);
            this.currentlyWorkingOnLabel.TabIndex = 5;
            this.currentlyWorkingOnLabel.Text = "Preparing import...";
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 123);
            this.Controls.Add(this.currentlyWorkingOnLabel);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.importingFromLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.importProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Importer";
            this.Text = "IdSort - Import";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar importProgressBar;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label importingFromLabel;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label currentlyWorkingOnLabel;
    }
}