namespace IClone.ICloneImplementation.WindowForms
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxDescription = new System.Windows.Forms.RichTextBox();
            this.labelCompanyName = new System.Windows.Forms.TextBox();
            this.labelCopyright = new System.Windows.Forms.TextBox();
            this.VersionLabel = new System.Windows.Forms.TextBox();
            this.labelProductName = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0391F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.96091F));
            this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 2, 4);
            this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.VersionLabel, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.okButton, 2, 5);
            this.tableLayoutPanel.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(417, 265);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Location = new System.Drawing.Point(155, 107);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.textBoxDescription.Size = new System.Drawing.Size(259, 126);
            this.textBoxDescription.TabIndex = 25;
            this.textBoxDescription.Text = "textBoxDescription";
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCompanyName.Location = new System.Drawing.Point(155, 81);
            this.labelCompanyName.Multiline = true;
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.ReadOnly = true;
            this.labelCompanyName.Size = new System.Drawing.Size(259, 20);
            this.labelCompanyName.TabIndex = 26;
            this.labelCompanyName.Text = "labelCompanyName";
            // 
            // labelCopyright
            // 
            this.labelCopyright.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Location = new System.Drawing.Point(155, 55);
            this.labelCopyright.Multiline = true;
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.ReadOnly = true;
            this.labelCopyright.Size = new System.Drawing.Size(259, 20);
            this.labelCopyright.TabIndex = 27;
            this.labelCopyright.Text = "labelCopyright";
            // 
            // VersionLabel
            // 
            this.VersionLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionLabel.Location = new System.Drawing.Point(155, 29);
            this.VersionLabel.Multiline = true;
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.ReadOnly = true;
            this.VersionLabel.Size = new System.Drawing.Size(259, 20);
            this.VersionLabel.TabIndex = 28;
            this.VersionLabel.Text = "VersionLabel";
            // 
            // labelProductName
            // 
            this.labelProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Location = new System.Drawing.Point(155, 3);
            this.labelProductName.Multiline = true;
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.ReadOnly = true;
            this.labelProductName.Size = new System.Drawing.Size(259, 20);
            this.labelProductName.TabIndex = 30;
            this.labelProductName.Text = "labelProductName";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(339, 239);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::IClone.ICloneImplementation.Properties.Resources.BlankBlue;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel.SetRowSpan(this.pictureBox1, 6);
            this.pictureBox1.Size = new System.Drawing.Size(136, 259);
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // AboutBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 283);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBox1";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.RichTextBox textBoxDescription;
        private System.Windows.Forms.TextBox labelCompanyName;
        private System.Windows.Forms.TextBox labelCopyright;
        private System.Windows.Forms.TextBox VersionLabel;
        private System.Windows.Forms.TextBox labelProductName;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
