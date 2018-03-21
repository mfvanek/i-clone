namespace IClone.ICloneImplementation.WindowForms
{
    partial class CloneReportWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloneReportWindow));
            this.CloneReportBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // CloneReportBrowser
            // 
            this.CloneReportBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CloneReportBrowser.Location = new System.Drawing.Point(0, 0);
            this.CloneReportBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.CloneReportBrowser.Name = "CloneReportBrowser";
            this.CloneReportBrowser.Size = new System.Drawing.Size(604, 524);
            this.CloneReportBrowser.TabIndex = 0;
            this.CloneReportBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.CloneReportBrowser_Navigating);
            // 
            // CloneReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 524);
            this.Controls.Add(this.CloneReportBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CloneReportWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CloneReportWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloneReportWindow_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser CloneReportBrowser;
    }
}