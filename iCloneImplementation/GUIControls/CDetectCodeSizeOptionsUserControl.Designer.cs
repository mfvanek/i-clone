namespace IClone.ICloneImplementation.GUIControls
{
    partial class CDetectCodeSizeOptionsUserControl
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
            this.DetectCodeSizeOpGroupBox = new System.Windows.Forms.GroupBox();
            this.ChoosenFileExtLabel = new System.Windows.Forms.Label();
            this.ChoosenFilesExtensionsTextBox = new System.Windows.Forms.TextBox();
            this.ChooseFolderLabel = new System.Windows.Forms.Label();
            this.ChoosenFolderTextBox = new System.Windows.Forms.TextBox();
            this.MemorizeLastRunParamsCheckBox = new System.Windows.Forms.CheckBox();
            this.DetectCodeSizeOpGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DetectCodeSizeOpGroupBox
            // 
            this.DetectCodeSizeOpGroupBox.Controls.Add(this.ChoosenFileExtLabel);
            this.DetectCodeSizeOpGroupBox.Controls.Add(this.ChoosenFilesExtensionsTextBox);
            this.DetectCodeSizeOpGroupBox.Controls.Add(this.ChooseFolderLabel);
            this.DetectCodeSizeOpGroupBox.Controls.Add(this.ChoosenFolderTextBox);
            this.DetectCodeSizeOpGroupBox.Controls.Add(this.MemorizeLastRunParamsCheckBox);
            this.DetectCodeSizeOpGroupBox.Location = new System.Drawing.Point(9, 15);
            this.DetectCodeSizeOpGroupBox.Name = "DetectCodeSizeOpGroupBox";
            this.DetectCodeSizeOpGroupBox.Size = new System.Drawing.Size(394, 166);
            this.DetectCodeSizeOpGroupBox.TabIndex = 0;
            this.DetectCodeSizeOpGroupBox.TabStop = false;
            this.DetectCodeSizeOpGroupBox.Text = "groupBox1";
            // 
            // ChoosenFileExtLabel
            // 
            this.ChoosenFileExtLabel.AutoSize = true;
            this.ChoosenFileExtLabel.Location = new System.Drawing.Point(9, 105);
            this.ChoosenFileExtLabel.Name = "ChoosenFileExtLabel";
            this.ChoosenFileExtLabel.Size = new System.Drawing.Size(35, 13);
            this.ChoosenFileExtLabel.TabIndex = 4;
            this.ChoosenFileExtLabel.Text = "label1";
            // 
            // ChoosenFilesExtensionsTextBox
            // 
            this.ChoosenFilesExtensionsTextBox.Location = new System.Drawing.Point(7, 124);
            this.ChoosenFilesExtensionsTextBox.Name = "ChoosenFilesExtensionsTextBox";
            this.ChoosenFilesExtensionsTextBox.Size = new System.Drawing.Size(381, 20);
            this.ChoosenFilesExtensionsTextBox.TabIndex = 3;
            // 
            // ChooseFolderLabel
            // 
            this.ChooseFolderLabel.AutoSize = true;
            this.ChooseFolderLabel.Location = new System.Drawing.Point(6, 54);
            this.ChooseFolderLabel.Name = "ChooseFolderLabel";
            this.ChooseFolderLabel.Size = new System.Drawing.Size(35, 13);
            this.ChooseFolderLabel.TabIndex = 2;
            this.ChooseFolderLabel.Text = "label1";
            // 
            // ChoosenFolderTextBox
            // 
            this.ChoosenFolderTextBox.Location = new System.Drawing.Point(7, 70);
            this.ChoosenFolderTextBox.Name = "ChoosenFolderTextBox";
            this.ChoosenFolderTextBox.Size = new System.Drawing.Size(381, 20);
            this.ChoosenFolderTextBox.TabIndex = 1;
            // 
            // MemorizeLastRunParamsCheckBox
            // 
            this.MemorizeLastRunParamsCheckBox.AutoSize = true;
            this.MemorizeLastRunParamsCheckBox.Location = new System.Drawing.Point(7, 30);
            this.MemorizeLastRunParamsCheckBox.Name = "MemorizeLastRunParamsCheckBox";
            this.MemorizeLastRunParamsCheckBox.Size = new System.Drawing.Size(80, 17);
            this.MemorizeLastRunParamsCheckBox.TabIndex = 0;
            this.MemorizeLastRunParamsCheckBox.Text = "checkBox1";
            this.MemorizeLastRunParamsCheckBox.UseVisualStyleBackColor = true;
            // 
            // CDetectCodeSizeOptionsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DetectCodeSizeOpGroupBox);
            this.Name = "CDetectCodeSizeOptionsUserControl";
            this.DetectCodeSizeOpGroupBox.ResumeLayout(false);
            this.DetectCodeSizeOpGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox DetectCodeSizeOpGroupBox;
        private System.Windows.Forms.CheckBox MemorizeLastRunParamsCheckBox;
        private System.Windows.Forms.TextBox ChoosenFolderTextBox;
        private System.Windows.Forms.Label ChooseFolderLabel;
        private System.Windows.Forms.TextBox ChoosenFilesExtensionsTextBox;
        private System.Windows.Forms.Label ChoosenFileExtLabel;
    }
}
