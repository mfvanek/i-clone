namespace IClone.ICloneImplementation.WindowForms
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ICloneMainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ICloneFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenCloneReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ICloneParamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ICloneActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CustomOpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSVNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetCodeSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ICloneHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.OpenCloneReportDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.CloneSearchMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutICloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ICloneMainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ICloneMainMenuStrip
            // 
            this.ICloneMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ICloneFileToolStripMenuItem,
            this.ICloneParamsToolStripMenuItem,
            this.ICloneActionsToolStripMenuItem,
            this.ICloneHelpToolStripMenuItem});
            resources.ApplyResources(this.ICloneMainMenuStrip, "ICloneMainMenuStrip");
            this.ICloneMainMenuStrip.Name = "ICloneMainMenuStrip";
            // 
            // ICloneFileToolStripMenuItem
            // 
            this.ICloneFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloneSearchMenuItem3,
            this.OpenCloneReportToolStripMenuItem,
            this.toolStripMenuItem2,
            this.ExitToolStripMenuItem});
            this.ICloneFileToolStripMenuItem.Name = "ICloneFileToolStripMenuItem";
            resources.ApplyResources(this.ICloneFileToolStripMenuItem, "ICloneFileToolStripMenuItem");
            // 
            // OpenCloneReportToolStripMenuItem
            // 
            this.OpenCloneReportToolStripMenuItem.Name = "OpenCloneReportToolStripMenuItem";
            resources.ApplyResources(this.OpenCloneReportToolStripMenuItem, "OpenCloneReportToolStripMenuItem");
            this.OpenCloneReportToolStripMenuItem.Click += new System.EventHandler(this.OpenCloneReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // ICloneParamsToolStripMenuItem
            // 
            this.ICloneParamsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsToolStripMenuItem});
            this.ICloneParamsToolStripMenuItem.Name = "ICloneParamsToolStripMenuItem";
            resources.ApplyResources(this.ICloneParamsToolStripMenuItem, "ICloneParamsToolStripMenuItem");
            // 
            // ICloneActionsToolStripMenuItem
            // 
            this.ICloneActionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.CustomOpsToolStripMenuItem});
            this.ICloneActionsToolStripMenuItem.Name = "ICloneActionsToolStripMenuItem";
            resources.ApplyResources(this.ICloneActionsToolStripMenuItem, "ICloneActionsToolStripMenuItem");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // CustomOpsToolStripMenuItem
            // 
            this.CustomOpsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteSVNToolStripMenuItem,
            this.GetCodeSizeToolStripMenuItem});
            this.CustomOpsToolStripMenuItem.Name = "CustomOpsToolStripMenuItem";
            resources.ApplyResources(this.CustomOpsToolStripMenuItem, "CustomOpsToolStripMenuItem");
            // 
            // DeleteSVNToolStripMenuItem
            // 
            this.DeleteSVNToolStripMenuItem.Name = "DeleteSVNToolStripMenuItem";
            resources.ApplyResources(this.DeleteSVNToolStripMenuItem, "DeleteSVNToolStripMenuItem");
            this.DeleteSVNToolStripMenuItem.Click += new System.EventHandler(this.DeleteSVNToolStripMenuItem_Click);
            // 
            // GetCodeSizeToolStripMenuItem
            // 
            this.GetCodeSizeToolStripMenuItem.Name = "GetCodeSizeToolStripMenuItem";
            resources.ApplyResources(this.GetCodeSizeToolStripMenuItem, "GetCodeSizeToolStripMenuItem");
            this.GetCodeSizeToolStripMenuItem.Click += new System.EventHandler(this.GetCodeSizeToolStripMenuItem_Click);
            // 
            // ICloneHelpToolStripMenuItem
            // 
            this.ICloneHelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenuItem,
            this.toolStripSeparator1,
            this.AboutICloneToolStripMenuItem});
            this.ICloneHelpToolStripMenuItem.Name = "ICloneHelpToolStripMenuItem";
            resources.ApplyResources(this.ICloneHelpToolStripMenuItem, "ICloneHelpToolStripMenuItem");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // OpenCloneReportDialog
            // 
            resources.ApplyResources(this.OpenCloneReportDialog, "OpenCloneReportDialog");
            this.OpenCloneReportDialog.RestoreDirectory = true;
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // CloneSearchMenuItem3
            // 
            this.CloneSearchMenuItem3.Name = "CloneSearchMenuItem3";
            resources.ApplyResources(this.CloneSearchMenuItem3, "CloneSearchMenuItem3");
            this.CloneSearchMenuItem3.Click += new System.EventHandler(this.CloneSearchMenuItem3_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            resources.ApplyResources(this.ExitToolStripMenuItem, "ExitToolStripMenuItem");
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // OptionsToolStripMenuItem
            // 
            this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            resources.ApplyResources(this.OptionsToolStripMenuItem, "OptionsToolStripMenuItem");
            this.OptionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            resources.ApplyResources(this.HelpMenuItem, "HelpMenuItem");
            // 
            // AboutICloneToolStripMenuItem
            // 
            this.AboutICloneToolStripMenuItem.Name = "AboutICloneToolStripMenuItem";
            resources.ApplyResources(this.AboutICloneToolStripMenuItem, "AboutICloneToolStripMenuItem");
            this.AboutICloneToolStripMenuItem.Click += new System.EventHandler(this.AboutICloneToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ICloneMainMenuStrip);
            this.MainMenuStrip = this.ICloneMainMenuStrip;
            this.Name = "MainWindow";
            this.ICloneMainMenuStrip.ResumeLayout(false);
            this.ICloneMainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ICloneMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ICloneFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenCloneReportToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenCloneReportDialog;
        private System.Windows.Forms.ToolStripMenuItem ICloneActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CustomOpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSVNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GetCodeSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem ICloneParamsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ICloneHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutICloneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloneSearchMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

