namespace IClone.ICloneImplementation.WindowForms
{
    partial class ICloneOptionsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ICloneOptionsWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.CurrentGroupNameLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ICloneOpOKButton = new System.Windows.Forms.Button();
            this.ICloneOpCancelButton = new System.Windows.Forms.Button();
            this.ICloneOpSaveButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.OptionsTreeView = new System.Windows.Forms.TreeView();
            this.ICloneOpParentPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.CurrentGroupNameLabel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // CurrentGroupNameLabel
            // 
            resources.ApplyResources(this.CurrentGroupNameLabel, "CurrentGroupNameLabel");
            this.CurrentGroupNameLabel.Name = "CurrentGroupNameLabel";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.ICloneOpOKButton);
            this.panel2.Controls.Add(this.ICloneOpCancelButton);
            this.panel2.Controls.Add(this.ICloneOpSaveButton);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // ICloneOpOKButton
            // 
            resources.ApplyResources(this.ICloneOpOKButton, "ICloneOpOKButton");
            this.ICloneOpOKButton.Name = "ICloneOpOKButton";
            this.ICloneOpOKButton.UseVisualStyleBackColor = true;
            this.ICloneOpOKButton.Click += new System.EventHandler(this.ICloneOpOKButton_Click);
            // 
            // ICloneOpCancelButton
            // 
            resources.ApplyResources(this.ICloneOpCancelButton, "ICloneOpCancelButton");
            this.ICloneOpCancelButton.Name = "ICloneOpCancelButton";
            this.ICloneOpCancelButton.UseVisualStyleBackColor = true;
            this.ICloneOpCancelButton.Click += new System.EventHandler(this.ICloneOpCancelButton_Click);
            // 
            // ICloneOpSaveButton
            // 
            resources.ApplyResources(this.ICloneOpSaveButton, "ICloneOpSaveButton");
            this.ICloneOpSaveButton.Name = "ICloneOpSaveButton";
            this.ICloneOpSaveButton.UseVisualStyleBackColor = true;
            this.ICloneOpSaveButton.Click += new System.EventHandler(this.ICloneOpSaveButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.OptionsTreeView);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // OptionsTreeView
            // 
            resources.ApplyResources(this.OptionsTreeView, "OptionsTreeView");
            this.OptionsTreeView.Name = "OptionsTreeView";
            this.OptionsTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("OptionsTreeView.Nodes")))});
            this.OptionsTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OptionsTreeView_NodeMouseClick);
            // 
            // ICloneOpParentPanel
            // 
            this.ICloneOpParentPanel.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.ICloneOpParentPanel, "ICloneOpParentPanel");
            this.ICloneOpParentPanel.Name = "ICloneOpParentPanel";
            // 
            // ICloneOptionsWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ICloneOpParentPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ICloneOptionsWindow";
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel ICloneOpParentPanel;
        private System.Windows.Forms.TreeView OptionsTreeView;
        private System.Windows.Forms.Button ICloneOpSaveButton;
        private System.Windows.Forms.Button ICloneOpCancelButton;
        private System.Windows.Forms.Button ICloneOpOKButton;
        private System.Windows.Forms.Label CurrentGroupNameLabel;
    }
}