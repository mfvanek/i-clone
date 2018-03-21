namespace IClone.ICloneImplementation.WindowForms
{
    partial class DetectCodeSizeWindow
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
           this.components = new System.ComponentModel.Container();
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetectCodeSizeWindow));
           this.ChooseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
           this.ChooseFolderButton = new System.Windows.Forms.Button();
           this.CodeLocationTextBox = new System.Windows.Forms.TextBox();
           this.ChoosenFolderLabel = new System.Windows.Forms.Label();
           this.StartButton = new System.Windows.Forms.Button();
           this.ChoosenFilesExtensionsLabel = new System.Windows.Forms.Label();
           this.ChoosenFilesExtensionsTextBox = new System.Windows.Forms.TextBox();
           this.DCS_ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
           this.TimeElapsedNameLabel = new System.Windows.Forms.Label();
           this.TimeElapsedValueLabel = new System.Windows.Forms.Label();
           this.ChooseLangComboBox = new System.Windows.Forms.ComboBox();
           this.ChooseLangNameLabel = new System.Windows.Forms.Label();
           this.FileProcessedNameLabel = new System.Windows.Forms.Label();
           this.RowsProcessedNameLabel = new System.Windows.Forms.Label();
           this.FileProcessedValueLabel = new System.Windows.Forms.Label();
           this.CountOfLinesLabel = new System.Windows.Forms.Label();
           this.ResultsGroupBox = new System.Windows.Forms.GroupBox();
           this.TokensProcessedValueLabel = new System.Windows.Forms.Label();
           this.TokensProcessedNameLabel = new System.Windows.Forms.Label();
           this.ParamsGroupBox = new System.Windows.Forms.GroupBox();
           this.OperationStatusStrip = new System.Windows.Forms.StatusStrip();
           this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
           this.CancelOpButton = new System.Windows.Forms.Button();
           this.SyntacticUnitsProcessedNameLabel = new System.Windows.Forms.Label();
           this.SyntacticUnitsProcessedValueLabel = new System.Windows.Forms.Label();
           this.MinSizeNameLabel = new System.Windows.Forms.Label();
           this.MaxSizeNameLabel = new System.Windows.Forms.Label();
           this.MediumSizeNameLabel = new System.Windows.Forms.Label();
           this.KminNameLabel = new System.Windows.Forms.Label();
           this.MeanSquareDeviationNameLabel = new System.Windows.Forms.Label();
           this.KminValueLabel = new System.Windows.Forms.Label();
           this.MeanSquareDeviationValueLabel = new System.Windows.Forms.Label();
           this.MediumSizeValueLabel = new System.Windows.Forms.Label();
           this.MaxSizeValueLabel = new System.Windows.Forms.Label();
           this.MinSizeValueLabel = new System.Windows.Forms.Label();
           ((System.ComponentModel.ISupportInitialize)(this.DCS_ErrorProvider)).BeginInit();
           this.ResultsGroupBox.SuspendLayout();
           this.ParamsGroupBox.SuspendLayout();
           this.OperationStatusStrip.SuspendLayout();
           this.SuspendLayout();
           // 
           // ChooseFolderDialog
           // 
           this.ChooseFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
           this.ChooseFolderDialog.ShowNewFolderButton = false;
           // 
           // ChooseFolderButton
           // 
           resources.ApplyResources(this.ChooseFolderButton, "ChooseFolderButton");
           this.ChooseFolderButton.Name = "ChooseFolderButton";
           this.ChooseFolderButton.UseVisualStyleBackColor = true;
           this.ChooseFolderButton.Click += new System.EventHandler(this.ChooseFolderButton_Click);
           // 
           // CodeLocationTextBox
           // 
           resources.ApplyResources(this.CodeLocationTextBox, "CodeLocationTextBox");
           this.CodeLocationTextBox.Name = "CodeLocationTextBox";
           this.CodeLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.CodeLocationTextBox_Validating);
           // 
           // ChoosenFolderLabel
           // 
           resources.ApplyResources(this.ChoosenFolderLabel, "ChoosenFolderLabel");
           this.ChoosenFolderLabel.Name = "ChoosenFolderLabel";
           // 
           // StartButton
           // 
           resources.ApplyResources(this.StartButton, "StartButton");
           this.StartButton.Name = "StartButton";
           this.StartButton.UseVisualStyleBackColor = true;
           this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
           // 
           // ChoosenFilesExtensionsLabel
           // 
           resources.ApplyResources(this.ChoosenFilesExtensionsLabel, "ChoosenFilesExtensionsLabel");
           this.ChoosenFilesExtensionsLabel.Name = "ChoosenFilesExtensionsLabel";
           // 
           // ChoosenFilesExtensionsTextBox
           // 
           resources.ApplyResources(this.ChoosenFilesExtensionsTextBox, "ChoosenFilesExtensionsTextBox");
           this.ChoosenFilesExtensionsTextBox.Name = "ChoosenFilesExtensionsTextBox";
           this.ChoosenFilesExtensionsTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ChoosenFilesExtensionsTextBox_Validating);
           // 
           // DCS_ErrorProvider
           // 
           this.DCS_ErrorProvider.ContainerControl = this;
           // 
           // TimeElapsedNameLabel
           // 
           resources.ApplyResources(this.TimeElapsedNameLabel, "TimeElapsedNameLabel");
           this.TimeElapsedNameLabel.Name = "TimeElapsedNameLabel";
           // 
           // TimeElapsedValueLabel
           // 
           resources.ApplyResources(this.TimeElapsedValueLabel, "TimeElapsedValueLabel");
           this.TimeElapsedValueLabel.Name = "TimeElapsedValueLabel";
           // 
           // ChooseLangComboBox
           // 
           this.ChooseLangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.ChooseLangComboBox.FormattingEnabled = true;
           resources.ApplyResources(this.ChooseLangComboBox, "ChooseLangComboBox");
           this.ChooseLangComboBox.Name = "ChooseLangComboBox";
           this.ChooseLangComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ChooseLangComboBox_Validating);
           // 
           // ChooseLangNameLabel
           // 
           resources.ApplyResources(this.ChooseLangNameLabel, "ChooseLangNameLabel");
           this.ChooseLangNameLabel.Name = "ChooseLangNameLabel";
           // 
           // FileProcessedNameLabel
           // 
           resources.ApplyResources(this.FileProcessedNameLabel, "FileProcessedNameLabel");
           this.FileProcessedNameLabel.Name = "FileProcessedNameLabel";
           // 
           // RowsProcessedNameLabel
           // 
           resources.ApplyResources(this.RowsProcessedNameLabel, "RowsProcessedNameLabel");
           this.RowsProcessedNameLabel.Name = "RowsProcessedNameLabel";
           // 
           // FileProcessedValueLabel
           // 
           resources.ApplyResources(this.FileProcessedValueLabel, "FileProcessedValueLabel");
           this.FileProcessedValueLabel.Name = "FileProcessedValueLabel";
           // 
           // CountOfLinesLabel
           // 
           resources.ApplyResources(this.CountOfLinesLabel, "CountOfLinesLabel");
           this.CountOfLinesLabel.Name = "CountOfLinesLabel";
           // 
           // ResultsGroupBox
           // 
           this.ResultsGroupBox.Controls.Add(this.MinSizeValueLabel);
           this.ResultsGroupBox.Controls.Add(this.MaxSizeValueLabel);
           this.ResultsGroupBox.Controls.Add(this.MediumSizeValueLabel);
           this.ResultsGroupBox.Controls.Add(this.MeanSquareDeviationValueLabel);
           this.ResultsGroupBox.Controls.Add(this.KminValueLabel);
           this.ResultsGroupBox.Controls.Add(this.MeanSquareDeviationNameLabel);
           this.ResultsGroupBox.Controls.Add(this.KminNameLabel);
           this.ResultsGroupBox.Controls.Add(this.MediumSizeNameLabel);
           this.ResultsGroupBox.Controls.Add(this.MaxSizeNameLabel);
           this.ResultsGroupBox.Controls.Add(this.MinSizeNameLabel);
           this.ResultsGroupBox.Controls.Add(this.SyntacticUnitsProcessedValueLabel);
           this.ResultsGroupBox.Controls.Add(this.SyntacticUnitsProcessedNameLabel);
           this.ResultsGroupBox.Controls.Add(this.TokensProcessedValueLabel);
           this.ResultsGroupBox.Controls.Add(this.TokensProcessedNameLabel);
           this.ResultsGroupBox.Controls.Add(this.FileProcessedNameLabel);
           this.ResultsGroupBox.Controls.Add(this.CountOfLinesLabel);
           this.ResultsGroupBox.Controls.Add(this.TimeElapsedNameLabel);
           this.ResultsGroupBox.Controls.Add(this.FileProcessedValueLabel);
           this.ResultsGroupBox.Controls.Add(this.TimeElapsedValueLabel);
           this.ResultsGroupBox.Controls.Add(this.RowsProcessedNameLabel);
           resources.ApplyResources(this.ResultsGroupBox, "ResultsGroupBox");
           this.ResultsGroupBox.Name = "ResultsGroupBox";
           this.ResultsGroupBox.TabStop = false;
           // 
           // TokensProcessedValueLabel
           // 
           resources.ApplyResources(this.TokensProcessedValueLabel, "TokensProcessedValueLabel");
           this.TokensProcessedValueLabel.Name = "TokensProcessedValueLabel";
           // 
           // TokensProcessedNameLabel
           // 
           resources.ApplyResources(this.TokensProcessedNameLabel, "TokensProcessedNameLabel");
           this.TokensProcessedNameLabel.Name = "TokensProcessedNameLabel";
           // 
           // ParamsGroupBox
           // 
           this.ParamsGroupBox.Controls.Add(this.ChoosenFolderLabel);
           this.ParamsGroupBox.Controls.Add(this.ChooseFolderButton);
           this.ParamsGroupBox.Controls.Add(this.ChooseLangNameLabel);
           this.ParamsGroupBox.Controls.Add(this.CodeLocationTextBox);
           this.ParamsGroupBox.Controls.Add(this.ChooseLangComboBox);
           this.ParamsGroupBox.Controls.Add(this.ChoosenFilesExtensionsLabel);
           this.ParamsGroupBox.Controls.Add(this.ChoosenFilesExtensionsTextBox);
           resources.ApplyResources(this.ParamsGroupBox, "ParamsGroupBox");
           this.ParamsGroupBox.Name = "ParamsGroupBox";
           this.ParamsGroupBox.TabStop = false;
           // 
           // OperationStatusStrip
           // 
           this.OperationStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
           resources.ApplyResources(this.OperationStatusStrip, "OperationStatusStrip");
           this.OperationStatusStrip.Name = "OperationStatusStrip";
           // 
           // StatusLabel
           // 
           this.StatusLabel.Name = "StatusLabel";
           resources.ApplyResources(this.StatusLabel, "StatusLabel");
           // 
           // CancelOpButton
           // 
           resources.ApplyResources(this.CancelOpButton, "CancelOpButton");
           this.CancelOpButton.Name = "CancelOpButton";
           this.CancelOpButton.UseVisualStyleBackColor = true;
           this.CancelOpButton.Click += new System.EventHandler(this.CancelOpButton_Click);
           // 
           // SyntacticUnitsProcessedNameLabel
           // 
           resources.ApplyResources(this.SyntacticUnitsProcessedNameLabel, "SyntacticUnitsProcessedNameLabel");
           this.SyntacticUnitsProcessedNameLabel.Name = "SyntacticUnitsProcessedNameLabel";
           // 
           // SyntacticUnitsProcessedValueLabel
           // 
           resources.ApplyResources(this.SyntacticUnitsProcessedValueLabel, "SyntacticUnitsProcessedValueLabel");
           this.SyntacticUnitsProcessedValueLabel.Name = "SyntacticUnitsProcessedValueLabel";
           // 
           // MinSizeNameLabel
           // 
           resources.ApplyResources(this.MinSizeNameLabel, "MinSizeNameLabel");
           this.MinSizeNameLabel.Name = "MinSizeNameLabel";
           // 
           // MaxSizeNameLabel
           // 
           resources.ApplyResources(this.MaxSizeNameLabel, "MaxSizeNameLabel");
           this.MaxSizeNameLabel.Name = "MaxSizeNameLabel";
           // 
           // MediumSizeNameLabel
           // 
           resources.ApplyResources(this.MediumSizeNameLabel, "MediumSizeNameLabel");
           this.MediumSizeNameLabel.Name = "MediumSizeNameLabel";
           // 
           // KminNameLabel
           // 
           resources.ApplyResources(this.KminNameLabel, "KminNameLabel");
           this.KminNameLabel.Name = "KminNameLabel";
           // 
           // MeanSquareDeviationNameLabel
           // 
           resources.ApplyResources(this.MeanSquareDeviationNameLabel, "MeanSquareDeviationNameLabel");
           this.MeanSquareDeviationNameLabel.Name = "MeanSquareDeviationNameLabel";
           // 
           // KminValueLabel
           // 
           resources.ApplyResources(this.KminValueLabel, "KminValueLabel");
           this.KminValueLabel.Name = "KminValueLabel";
           // 
           // MeanSquareDeviationValueLabel
           // 
           resources.ApplyResources(this.MeanSquareDeviationValueLabel, "MeanSquareDeviationValueLabel");
           this.MeanSquareDeviationValueLabel.Name = "MeanSquareDeviationValueLabel";
           // 
           // MediumSizeValueLabel
           // 
           resources.ApplyResources(this.MediumSizeValueLabel, "MediumSizeValueLabel");
           this.MediumSizeValueLabel.Name = "MediumSizeValueLabel";
           // 
           // MaxSizeValueLabel
           // 
           resources.ApplyResources(this.MaxSizeValueLabel, "MaxSizeValueLabel");
           this.MaxSizeValueLabel.Name = "MaxSizeValueLabel";
           // 
           // MinSizeValueLabel
           // 
           resources.ApplyResources(this.MinSizeValueLabel, "MinSizeValueLabel");
           this.MinSizeValueLabel.Name = "MinSizeValueLabel";
           // 
           // DetectCodeSizeWindow
           // 
           resources.ApplyResources(this, "$this");
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.Controls.Add(this.CancelOpButton);
           this.Controls.Add(this.OperationStatusStrip);
           this.Controls.Add(this.ParamsGroupBox);
           this.Controls.Add(this.ResultsGroupBox);
           this.Controls.Add(this.StartButton);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "DetectCodeSizeWindow";
           this.ShowInTaskbar = false;
           this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DetectCodeSizeWindow_FormClosing);
           ((System.ComponentModel.ISupportInitialize)(this.DCS_ErrorProvider)).EndInit();
           this.ResultsGroupBox.ResumeLayout(false);
           this.ResultsGroupBox.PerformLayout();
           this.ParamsGroupBox.ResumeLayout(false);
           this.ParamsGroupBox.PerformLayout();
           this.OperationStatusStrip.ResumeLayout(false);
           this.OperationStatusStrip.PerformLayout();
           this.ResumeLayout(false);
           this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog ChooseFolderDialog;
        private System.Windows.Forms.Button ChooseFolderButton;
        private System.Windows.Forms.TextBox CodeLocationTextBox;
        private System.Windows.Forms.Label ChoosenFolderLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label ChoosenFilesExtensionsLabel;
        private System.Windows.Forms.TextBox ChoosenFilesExtensionsTextBox;
        private System.Windows.Forms.ErrorProvider DCS_ErrorProvider;
        private System.Windows.Forms.Label TimeElapsedNameLabel;
        private System.Windows.Forms.Label TimeElapsedValueLabel;
        private System.Windows.Forms.Label ChooseLangNameLabel;
        private System.Windows.Forms.ComboBox ChooseLangComboBox;
        private System.Windows.Forms.Label FileProcessedNameLabel;
        private System.Windows.Forms.Label RowsProcessedNameLabel;
        private System.Windows.Forms.Label FileProcessedValueLabel;
        private System.Windows.Forms.Label CountOfLinesLabel;
        private System.Windows.Forms.GroupBox ResultsGroupBox;
        private System.Windows.Forms.GroupBox ParamsGroupBox;
        private System.Windows.Forms.StatusStrip OperationStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Button CancelOpButton;
        private System.Windows.Forms.Label TokensProcessedNameLabel;
        private System.Windows.Forms.Label TokensProcessedValueLabel;
        private System.Windows.Forms.Label SyntacticUnitsProcessedNameLabel;
        private System.Windows.Forms.Label SyntacticUnitsProcessedValueLabel;
        private System.Windows.Forms.Label KminNameLabel;
        private System.Windows.Forms.Label MediumSizeNameLabel;
        private System.Windows.Forms.Label MaxSizeNameLabel;
        private System.Windows.Forms.Label MinSizeNameLabel;
        private System.Windows.Forms.Label MeanSquareDeviationNameLabel;
        private System.Windows.Forms.Label MinSizeValueLabel;
        private System.Windows.Forms.Label MaxSizeValueLabel;
        private System.Windows.Forms.Label MediumSizeValueLabel;
        private System.Windows.Forms.Label MeanSquareDeviationValueLabel;
        private System.Windows.Forms.Label KminValueLabel;
    }
}