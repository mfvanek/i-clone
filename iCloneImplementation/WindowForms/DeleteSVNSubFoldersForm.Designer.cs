namespace IClone.ICloneImplementation.WindowForms
{
    partial class DeleteSVNSubFoldersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteSVNSubFoldersForm));
            this.ChooseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.ChoosenFolderBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(438, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChoosenFolderBox
            // 
            this.ChoosenFolderBox.Location = new System.Drawing.Point(12, 38);
            this.ChoosenFolderBox.Name = "ChoosenFolderBox";
            this.ChoosenFolderBox.Size = new System.Drawing.Size(420, 20);
            this.ChoosenFolderBox.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(195, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 59);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DeleteSVNSubFoldersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 296);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ChoosenFolderBox);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeleteSVNSubFoldersForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog ChooseFolderDialog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ChoosenFolderBox;
        private System.Windows.Forms.Button button2;
    }
}

