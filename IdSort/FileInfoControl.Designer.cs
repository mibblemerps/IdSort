
namespace IdSort
{
    partial class FileInfoControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tagValuesLabel = new System.Windows.Forms.Label();
            this.tagNamesLabels = new System.Windows.Forms.Label();
            this.noTagDataLabel = new System.Windows.Forms.Label();
            this.fileDescriptionLabel = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.fileNameLabel.Location = new System.Drawing.Point(0, 0);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(99, 25);
            this.fileNameLabel.TabIndex = 0;
            this.fileNameLabel.Text = "File Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tagValuesLabel);
            this.groupBox1.Controls.Add(this.tagNamesLabels);
            this.groupBox1.Controls.Add(this.noTagDataLabel);
            this.groupBox1.Location = new System.Drawing.Point(0, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 277);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tags";
            // 
            // tagValuesLabel
            // 
            this.tagValuesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagValuesLabel.AutoEllipsis = true;
            this.tagValuesLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.tagValuesLabel.Location = new System.Drawing.Point(129, 16);
            this.tagValuesLabel.Name = "tagValuesLabel";
            this.tagValuesLabel.Size = new System.Drawing.Size(464, 258);
            this.tagValuesLabel.TabIndex = 3;
            this.tagValuesLabel.Text = "Tag Values";
            // 
            // tagNamesLabels
            // 
            this.tagNamesLabels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tagNamesLabels.AutoEllipsis = true;
            this.tagNamesLabels.Location = new System.Drawing.Point(6, 16);
            this.tagNamesLabels.Name = "tagNamesLabels";
            this.tagNamesLabels.Size = new System.Drawing.Size(117, 258);
            this.tagNamesLabels.TabIndex = 0;
            this.tagNamesLabels.Text = "Tag Data";
            // 
            // noTagDataLabel
            // 
            this.noTagDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noTagDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.noTagDataLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.noTagDataLabel.Location = new System.Drawing.Point(3, 16);
            this.noTagDataLabel.Name = "noTagDataLabel";
            this.noTagDataLabel.Size = new System.Drawing.Size(593, 151);
            this.noTagDataLabel.TabIndex = 1;
            this.noTagDataLabel.Text = "No Tag Data";
            this.noTagDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileDescriptionLabel
            // 
            this.fileDescriptionLabel.AutoSize = true;
            this.fileDescriptionLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fileDescriptionLabel.Location = new System.Drawing.Point(3, 27);
            this.fileDescriptionLabel.Name = "fileDescriptionLabel";
            this.fileDescriptionLabel.Size = new System.Drawing.Size(23, 13);
            this.fileDescriptionLabel.TabIndex = 2;
            this.fileDescriptionLabel.Text = "File";
            // 
            // playButton
            // 
            this.playButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playButton.Location = new System.Drawing.Point(516, 4);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(77, 23);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // FileInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.fileDescriptionLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fileNameLabel);
            this.Name = "FileInfoControl";
            this.Size = new System.Drawing.Size(599, 330);
            this.Load += new System.EventHandler(this.FileInfoControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label fileDescriptionLabel;
        private System.Windows.Forms.Label tagNamesLabels;
        private System.Windows.Forms.Label noTagDataLabel;
        private System.Windows.Forms.Label tagValuesLabel;
        private System.Windows.Forms.Button playButton;
    }
}
