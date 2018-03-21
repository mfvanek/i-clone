namespace IClone.ICloneImplementation.GUIControls
{
    partial class CCommonOptionsUserControl
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
            this.ChooseUILangLabel = new System.Windows.Forms.Label();
            this.LangGroupBox = new System.Windows.Forms.GroupBox();
            this.CultureComboBox = new System.Windows.Forms.ComboBox();
            this.LangGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChooseUILangLabel
            // 
            this.ChooseUILangLabel.AutoSize = true;
            this.ChooseUILangLabel.Location = new System.Drawing.Point(25, 19);
            this.ChooseUILangLabel.Name = "ChooseUILangLabel";
            this.ChooseUILangLabel.Size = new System.Drawing.Size(41, 13);
            this.ChooseUILangLabel.TabIndex = 0;
            this.ChooseUILangLabel.Text = "_Язык";
            // 
            // LangGroupBox
            // 
            this.LangGroupBox.Controls.Add(this.CultureComboBox);
            this.LangGroupBox.Controls.Add(this.ChooseUILangLabel);
            this.LangGroupBox.Location = new System.Drawing.Point(9, 15);
            this.LangGroupBox.Name = "LangGroupBox";
            this.LangGroupBox.Size = new System.Drawing.Size(394, 50);
            this.LangGroupBox.TabIndex = 2;
            this.LangGroupBox.TabStop = false;
            this.LangGroupBox.Text = "groupBox1";
            // 
            // CultureComboBox
            // 
            this.CultureComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CultureComboBox.FormattingEnabled = true;
            this.CultureComboBox.Location = new System.Drawing.Point(103, 15);
            this.CultureComboBox.Name = "CultureComboBox";
            this.CultureComboBox.Size = new System.Drawing.Size(121, 21);
            this.CultureComboBox.TabIndex = 1;
            this.CultureComboBox.SelectedIndexChanged += new System.EventHandler(this.CultureComboBox_SelectedIndexChanged);
            // 
            // CCommonOptionsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LangGroupBox);
            this.Name = "CCommonOptionsUserControl";
            this.LangGroupBox.ResumeLayout(false);
            this.LangGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ChooseUILangLabel;
        private System.Windows.Forms.GroupBox LangGroupBox;
        private System.Windows.Forms.ComboBox CultureComboBox;
    }
}
