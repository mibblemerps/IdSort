
namespace IdSort.Restructure
{
    partial class RestructureControl
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
            this.fileVarButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.outputFormatString = new System.Windows.Forms.TextBox();
            this.restructureButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.neverUseVariousArtistsCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileVarButtons
            // 
            this.fileVarButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileVarButtons.AutoSize = true;
            this.fileVarButtons.BackColor = System.Drawing.SystemColors.Control;
            this.fileVarButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fileVarButtons.Location = new System.Drawing.Point(9, 129);
            this.fileVarButtons.Name = "fileVarButtons";
            this.fileVarButtons.Size = new System.Drawing.Size(661, 32);
            this.fileVarButtons.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Output Format Strings";
            // 
            // outputFormatString
            // 
            this.outputFormatString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFormatString.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.outputFormatString.Location = new System.Drawing.Point(9, 49);
            this.outputFormatString.Multiline = true;
            this.outputFormatString.Name = "outputFormatString";
            this.outputFormatString.Size = new System.Drawing.Size(661, 74);
            this.outputFormatString.TabIndex = 7;
            this.outputFormatString.Text = "{artist}\\{album}\\{filename}\r\n{album}\\{filename}";
            // 
            // restructureButton
            // 
            this.restructureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.restructureButton.Location = new System.Drawing.Point(502, 363);
            this.restructureButton.Name = "restructureButton";
            this.restructureButton.Size = new System.Drawing.Size(168, 32);
            this.restructureButton.TabIndex = 8;
            this.restructureButton.Text = "Preview Restructure";
            this.restructureButton.UseVisualStyleBackColor = true;
            this.restructureButton.Click += new System.EventHandler(this.restructureButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Restructure won\'t apply until you commit changes to disk.";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(124, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(546, 38);
            this.label3.TabIndex = 10;
            this.label3.Text = "Add output path format strings here. Place them in order of preference. If a vari" +
    "able isn\'t set on one format string, the next one will be tried.";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.neverUseVariousArtistsCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(9, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 53);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(238, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(403, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "This will allow you to sort by artist without having large collab albums making a" +
    " mess.";
            // 
            // neverUseVariousArtistsCheckbox
            // 
            this.neverUseVariousArtistsCheckbox.AutoSize = true;
            this.neverUseVariousArtistsCheckbox.Location = new System.Drawing.Point(6, 19);
            this.neverUseVariousArtistsCheckbox.Name = "neverUseVariousArtistsCheckbox";
            this.neverUseVariousArtistsCheckbox.Size = new System.Drawing.Size(237, 17);
            this.neverUseVariousArtistsCheckbox.TabIndex = 0;
            this.neverUseVariousArtistsCheckbox.Text = "Never use \"Various Artists\" as Artist variable.";
            this.neverUseVariousArtistsCheckbox.UseVisualStyleBackColor = true;
            // 
            // RestructureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.restructureButton);
            this.Controls.Add(this.outputFormatString);
            this.Controls.Add(this.fileVarButtons);
            this.Controls.Add(this.label1);
            this.Name = "RestructureControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(680, 395);
            this.Load += new System.EventHandler(this.RestructureControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fileVarButtons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox outputFormatString;
        private System.Windows.Forms.Button restructureButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox neverUseVariousArtistsCheckbox;
    }
}
