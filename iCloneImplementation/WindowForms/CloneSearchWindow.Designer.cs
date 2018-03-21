
namespace IClone.ICloneImplementation.WindowForms
{
    partial class CloneSearchWindow
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
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloneSearchWindow));
           this.statusStrip1 = new System.Windows.Forms.StatusStrip();
           this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
           this.CloneSearchTabControl = new System.Windows.Forms.TabControl();
           this.CloneSearchTabPage1 = new System.Windows.Forms.TabPage();
           this.CodeProcessingResultsGroupBox = new System.Windows.Forms.GroupBox();
           this.RowsInModifiedFilesValueLabel = new System.Windows.Forms.Label();
           this.RowsInModifiedFilesNameLabel = new System.Windows.Forms.Label();
           this.RowsInOriginalFilesValueLabel = new System.Windows.Forms.Label();
           this.RowsInOriginalFilesNameLabel = new System.Windows.Forms.Label();
           this.RowsProcessedValueLabel = new System.Windows.Forms.Label();
           this.RowsProcessedNameLabel = new System.Windows.Forms.Label();
           this.FileProcessedValueLabel = new System.Windows.Forms.Label();
           this.TimeElapsedValueLabel = new System.Windows.Forms.Label();
           this.FileProcessedNameLabel = new System.Windows.Forms.Label();
           this.TimeElapsedNameLabel = new System.Windows.Forms.Label();
           this.StopCloneSearchButton = new System.Windows.Forms.Button();
           this.StartCloneSearchButton = new System.Windows.Forms.Button();
           this.CodeLocationGroupBox = new System.Windows.Forms.GroupBox();
           this.AutomaticKminCalculationCheckBox = new System.Windows.Forms.CheckBox();
           this.KminNameLabel = new System.Windows.Forms.Label();
           this.KminValue = new System.Windows.Forms.NumericUpDown();
           this.ChoosenFilesExtensionsLabel = new System.Windows.Forms.Label();
           this.ChoosenFilesExtensionsTextBox = new System.Windows.Forms.TextBox();
           this.ChooseLangComboBox = new System.Windows.Forms.ComboBox();
           this.CodeLanguageLabel = new System.Windows.Forms.Label();
           this.CodeEncodingComboBox = new System.Windows.Forms.ComboBox();
           this.CodeEncodingLabel = new System.Windows.Forms.Label();
           this.ChooseCodeLocationButton = new System.Windows.Forms.Button();
           this.CodeLocationTextBox = new System.Windows.Forms.TextBox();
           this.CodeLocationLabel = new System.Windows.Forms.Label();
           this.CloneSearchTabPage2 = new System.Windows.Forms.TabPage();
           this.ChooseCodeLocationDialog = new System.Windows.Forms.FolderBrowserDialog();
           this.CSW_ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
           this.statusStrip1.SuspendLayout();
           this.CloneSearchTabControl.SuspendLayout();
           this.CloneSearchTabPage1.SuspendLayout();
           this.CodeProcessingResultsGroupBox.SuspendLayout();
           this.CodeLocationGroupBox.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.KminValue)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.CSW_ErrorProvider)).BeginInit();
           this.SuspendLayout();
           // 
           // statusStrip1
           // 
           this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
           resources.ApplyResources(this.statusStrip1, "statusStrip1");
           this.statusStrip1.Name = "statusStrip1";
           // 
           // StatusLabel
           // 
           this.StatusLabel.Name = "StatusLabel";
           resources.ApplyResources(this.StatusLabel, "StatusLabel");
           // 
           // CloneSearchTabControl
           // 
           this.CloneSearchTabControl.Controls.Add(this.CloneSearchTabPage1);
           this.CloneSearchTabControl.Controls.Add(this.CloneSearchTabPage2);
           resources.ApplyResources(this.CloneSearchTabControl, "CloneSearchTabControl");
           this.CloneSearchTabControl.Name = "CloneSearchTabControl";
           this.CloneSearchTabControl.SelectedIndex = 0;
           // 
           // CloneSearchTabPage1
           // 
           this.CloneSearchTabPage1.Controls.Add(this.CodeProcessingResultsGroupBox);
           this.CloneSearchTabPage1.Controls.Add(this.StopCloneSearchButton);
           this.CloneSearchTabPage1.Controls.Add(this.StartCloneSearchButton);
           this.CloneSearchTabPage1.Controls.Add(this.CodeLocationGroupBox);
           resources.ApplyResources(this.CloneSearchTabPage1, "CloneSearchTabPage1");
           this.CloneSearchTabPage1.Name = "CloneSearchTabPage1";
           this.CloneSearchTabPage1.UseVisualStyleBackColor = true;
           // 
           // CodeProcessingResultsGroupBox
           // 
           this.CodeProcessingResultsGroupBox.Controls.Add(this.RowsInModifiedFilesValueLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.RowsInModifiedFilesNameLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.RowsInOriginalFilesValueLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.RowsInOriginalFilesNameLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.RowsProcessedValueLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.RowsProcessedNameLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.FileProcessedValueLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.TimeElapsedValueLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.FileProcessedNameLabel);
           this.CodeProcessingResultsGroupBox.Controls.Add(this.TimeElapsedNameLabel);
           resources.ApplyResources(this.CodeProcessingResultsGroupBox, "CodeProcessingResultsGroupBox");
           this.CodeProcessingResultsGroupBox.Name = "CodeProcessingResultsGroupBox";
           this.CodeProcessingResultsGroupBox.TabStop = false;
           // 
           // RowsInModifiedFilesValueLabel
           // 
           resources.ApplyResources(this.RowsInModifiedFilesValueLabel, "RowsInModifiedFilesValueLabel");
           this.RowsInModifiedFilesValueLabel.Name = "RowsInModifiedFilesValueLabel";
           // 
           // RowsInModifiedFilesNameLabel
           // 
           resources.ApplyResources(this.RowsInModifiedFilesNameLabel, "RowsInModifiedFilesNameLabel");
           this.RowsInModifiedFilesNameLabel.Name = "RowsInModifiedFilesNameLabel";
           // 
           // RowsInOriginalFilesValueLabel
           // 
           resources.ApplyResources(this.RowsInOriginalFilesValueLabel, "RowsInOriginalFilesValueLabel");
           this.RowsInOriginalFilesValueLabel.Name = "RowsInOriginalFilesValueLabel";
           // 
           // RowsInOriginalFilesNameLabel
           // 
           resources.ApplyResources(this.RowsInOriginalFilesNameLabel, "RowsInOriginalFilesNameLabel");
           this.RowsInOriginalFilesNameLabel.Name = "RowsInOriginalFilesNameLabel";
           // 
           // RowsProcessedValueLabel
           // 
           resources.ApplyResources(this.RowsProcessedValueLabel, "RowsProcessedValueLabel");
           this.RowsProcessedValueLabel.Name = "RowsProcessedValueLabel";
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
           // TimeElapsedValueLabel
           // 
           resources.ApplyResources(this.TimeElapsedValueLabel, "TimeElapsedValueLabel");
           this.TimeElapsedValueLabel.Name = "TimeElapsedValueLabel";
           // 
           // FileProcessedNameLabel
           // 
           resources.ApplyResources(this.FileProcessedNameLabel, "FileProcessedNameLabel");
           this.FileProcessedNameLabel.Name = "FileProcessedNameLabel";
           // 
           // TimeElapsedNameLabel
           // 
           resources.ApplyResources(this.TimeElapsedNameLabel, "TimeElapsedNameLabel");
           this.TimeElapsedNameLabel.Name = "TimeElapsedNameLabel";
           // 
           // StopCloneSearchButton
           // 
           resources.ApplyResources(this.StopCloneSearchButton, "StopCloneSearchButton");
           this.StopCloneSearchButton.Name = "StopCloneSearchButton";
           this.StopCloneSearchButton.UseVisualStyleBackColor = true;
           this.StopCloneSearchButton.Click += new System.EventHandler(this.StopCloneSearchButton_Click);
           // 
           // StartCloneSearchButton
           // 
           resources.ApplyResources(this.StartCloneSearchButton, "StartCloneSearchButton");
           this.StartCloneSearchButton.Name = "StartCloneSearchButton";
           this.StartCloneSearchButton.UseVisualStyleBackColor = true;
           this.StartCloneSearchButton.Click += new System.EventHandler(this.StartCloneSearchButton_Click);
           // 
           // CodeLocationGroupBox
           // 
           this.CodeLocationGroupBox.Controls.Add(this.AutomaticKminCalculationCheckBox);
           this.CodeLocationGroupBox.Controls.Add(this.KminNameLabel);
           this.CodeLocationGroupBox.Controls.Add(this.KminValue);
           this.CodeLocationGroupBox.Controls.Add(this.ChoosenFilesExtensionsLabel);
           this.CodeLocationGroupBox.Controls.Add(this.ChoosenFilesExtensionsTextBox);
           this.CodeLocationGroupBox.Controls.Add(this.ChooseLangComboBox);
           this.CodeLocationGroupBox.Controls.Add(this.CodeLanguageLabel);
           this.CodeLocationGroupBox.Controls.Add(this.CodeEncodingComboBox);
           this.CodeLocationGroupBox.Controls.Add(this.CodeEncodingLabel);
           this.CodeLocationGroupBox.Controls.Add(this.ChooseCodeLocationButton);
           this.CodeLocationGroupBox.Controls.Add(this.CodeLocationTextBox);
           this.CodeLocationGroupBox.Controls.Add(this.CodeLocationLabel);
           resources.ApplyResources(this.CodeLocationGroupBox, "CodeLocationGroupBox");
           this.CodeLocationGroupBox.Name = "CodeLocationGroupBox";
           this.CodeLocationGroupBox.TabStop = false;
           // 
           // AutomaticKminCalculationCheckBox
           // 
           resources.ApplyResources(this.AutomaticKminCalculationCheckBox, "AutomaticKminCalculationCheckBox");
           this.AutomaticKminCalculationCheckBox.Checked = true;
           this.AutomaticKminCalculationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
           this.AutomaticKminCalculationCheckBox.Name = "AutomaticKminCalculationCheckBox";
           this.AutomaticKminCalculationCheckBox.UseVisualStyleBackColor = true;
           this.AutomaticKminCalculationCheckBox.CheckedChanged += new System.EventHandler(this.AutomaticKminCalculationCheckBox_CheckedChanged);
           // 
           // KminNameLabel
           // 
           resources.ApplyResources(this.KminNameLabel, "KminNameLabel");
           this.KminNameLabel.Name = "KminNameLabel";
           // 
           // KminValue
           // 
           resources.ApplyResources(this.KminValue, "KminValue");
           this.KminValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
           this.KminValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
           this.KminValue.Name = "KminValue";
           this.KminValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
           this.ChoosenFilesExtensionsTextBox.Tag = "";
           this.ChoosenFilesExtensionsTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ChoosenFilesExtensionsTextBox_Validating);
           // 
           // ChooseLangComboBox
           // 
           this.ChooseLangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.ChooseLangComboBox.FormattingEnabled = true;
           resources.ApplyResources(this.ChooseLangComboBox, "ChooseLangComboBox");
           this.ChooseLangComboBox.Name = "ChooseLangComboBox";
           this.ChooseLangComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ChooseLangComboBox_Validating);
           // 
           // CodeLanguageLabel
           // 
           resources.ApplyResources(this.CodeLanguageLabel, "CodeLanguageLabel");
           this.CodeLanguageLabel.Name = "CodeLanguageLabel";
           // 
           // CodeEncodingComboBox
           // 
           this.CodeEncodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.CodeEncodingComboBox.FormattingEnabled = true;
           resources.ApplyResources(this.CodeEncodingComboBox, "CodeEncodingComboBox");
           this.CodeEncodingComboBox.Name = "CodeEncodingComboBox";
           // 
           // CodeEncodingLabel
           // 
           resources.ApplyResources(this.CodeEncodingLabel, "CodeEncodingLabel");
           this.CodeEncodingLabel.Name = "CodeEncodingLabel";
           // 
           // ChooseCodeLocationButton
           // 
           resources.ApplyResources(this.ChooseCodeLocationButton, "ChooseCodeLocationButton");
           this.ChooseCodeLocationButton.Name = "ChooseCodeLocationButton";
           this.ChooseCodeLocationButton.UseVisualStyleBackColor = true;
           this.ChooseCodeLocationButton.Click += new System.EventHandler(this.ChooseCodeLocationButton_Click);
           // 
           // CodeLocationTextBox
           // 
           resources.ApplyResources(this.CodeLocationTextBox, "CodeLocationTextBox");
           this.CodeLocationTextBox.Name = "CodeLocationTextBox";
           this.CodeLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.CodeLocationTextBox_Validating);
           // 
           // CodeLocationLabel
           // 
           resources.ApplyResources(this.CodeLocationLabel, "CodeLocationLabel");
           this.CodeLocationLabel.Name = "CodeLocationLabel";
           // 
           // CloneSearchTabPage2
           // 
           resources.ApplyResources(this.CloneSearchTabPage2, "CloneSearchTabPage2");
           this.CloneSearchTabPage2.Name = "CloneSearchTabPage2";
           this.CloneSearchTabPage2.UseVisualStyleBackColor = true;
           // 
           // CSW_ErrorProvider
           // 
           this.CSW_ErrorProvider.ContainerControl = this;
           // 
           // CloneSearchWindow
           // 
           resources.ApplyResources(this, "$this");
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.Controls.Add(this.CloneSearchTabControl);
           this.Controls.Add(this.statusStrip1);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "CloneSearchWindow";
           this.ShowInTaskbar = false;
           this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloneSearchWindow_FormClosing);
           this.statusStrip1.ResumeLayout(false);
           this.statusStrip1.PerformLayout();
           this.CloneSearchTabControl.ResumeLayout(false);
           this.CloneSearchTabPage1.ResumeLayout(false);
           this.CodeProcessingResultsGroupBox.ResumeLayout(false);
           this.CodeProcessingResultsGroupBox.PerformLayout();
           this.CodeLocationGroupBox.ResumeLayout(false);
           this.CodeLocationGroupBox.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.KminValue)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.CSW_ErrorProvider)).EndInit();
           this.ResumeLayout(false);
           this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl CloneSearchTabControl;
        private System.Windows.Forms.TabPage CloneSearchTabPage1;
        private System.Windows.Forms.TabPage CloneSearchTabPage2;
        private System.Windows.Forms.GroupBox CodeLocationGroupBox;
        private System.Windows.Forms.Label CodeLocationLabel;
        private System.Windows.Forms.TextBox CodeLocationTextBox;
        private System.Windows.Forms.Button ChooseCodeLocationButton;
        private System.Windows.Forms.FolderBrowserDialog ChooseCodeLocationDialog;
        private System.Windows.Forms.Label CodeEncodingLabel;
        private System.Windows.Forms.ComboBox CodeEncodingComboBox;
        private System.Windows.Forms.Label CodeLanguageLabel;
        private System.Windows.Forms.ComboBox ChooseLangComboBox;
        private System.Windows.Forms.TextBox ChoosenFilesExtensionsTextBox;
        private System.Windows.Forms.Label ChoosenFilesExtensionsLabel;
        private System.Windows.Forms.Button StartCloneSearchButton;
        private System.Windows.Forms.Button StopCloneSearchButton;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.GroupBox CodeProcessingResultsGroupBox;
        private System.Windows.Forms.Label TimeElapsedNameLabel;
        private System.Windows.Forms.Label FileProcessedNameLabel;
        private System.Windows.Forms.Label TimeElapsedValueLabel;
        private System.Windows.Forms.Label FileProcessedValueLabel;
        private System.Windows.Forms.ErrorProvider CSW_ErrorProvider;
        private System.Windows.Forms.Label RowsProcessedNameLabel;
        private System.Windows.Forms.Label RowsProcessedValueLabel;
        private System.Windows.Forms.Label RowsInOriginalFilesNameLabel;
        private System.Windows.Forms.Label RowsInOriginalFilesValueLabel;
        private System.Windows.Forms.Label RowsInModifiedFilesNameLabel;
        private System.Windows.Forms.Label RowsInModifiedFilesValueLabel;
        private System.Windows.Forms.NumericUpDown KminValue;
        private System.Windows.Forms.Label KminNameLabel;
        private System.Windows.Forms.CheckBox AutomaticKminCalculationCheckBox;
    }
}