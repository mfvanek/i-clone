using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CodeLoadingLibrary.Classes;
using IClone.ICloneImplementation.AdditionalClasses;
using IClone.IDetectCodeSize;
using ICloneExtensions;
using ICloneExtensions.Interfaces;
using ISourceFilesLibrary.Classes.Languages;
using ICloneCodeCharacteristicsLibrary.Classes;

namespace IClone.ICloneImplementation.WindowForms
{
   public partial class DetectCodeSizeWindow : Form, ICloneUILocalization
   {
      private BackgroundWorker m_BackgroundThread;
      private CCodeSizeDetector m_Counter;
      private Stopwatch m_StopWatch;
      private CSyntacticInfo m_Info;

      // Стратегия уведомлений. Слишком часто - бессмысленно для быстрых алгоритмов
      private int m_ReportingCounter;
      private int REPORTING_FRECUENCY = 100;

      #region // Реализация интерфейса ICloneUILocalization

      /// <summary>
      /// Инициализация пользовательского интерфейса
      /// </summary>
      public void InitUI()
      {
         StatusLabel.Text = ICloneLocalization.CMNMESS_WaitingForUserActions;
         CWindowNamer.SetWindowName(this, ICloneLocalization.DCS_WindowHeader);
         ParamsGroupBox.Text = ICloneLocalization.GUI_CMN_GroupBoxText;
         ChoosenFolderLabel.Text = ICloneLocalization.DCS_ChooseFolderLabelText;
         ChoosenFilesExtensionsLabel.Text = ICloneLocalization.DCS_ChoosenFileExtLabelText;
         TimeElapsedNameLabel.Text = ICloneLocalization.CMN_TimeElapsedNameLabelText;
         FileProcessedNameLabel.Text = ICloneLocalization.CMN_FileProcessedNameLabelText;
         RowsProcessedNameLabel.Text = ICloneLocalization.CMN_RowsProcessedNameLabelText;
         TokensProcessedNameLabel.Text = ICloneLocalization.CMN_TokensProcessedNameLabelText;
      }

      #endregion

      #region // Закрытые вспомогательные функции

      private void InitCodeCounter()
      {
         m_Info = new CSyntacticInfo();
         m_StopWatch = new Stopwatch();
         LANGUAGES LangID = (ChooseLangComboBox.SelectedItem as ICloneExtension).LanguageID();
         m_Counter = new CCodeSizeDetector(LangID, new CLoadFilesOptions(CodeLocationTextBox.Text, ChoosenFilesExtensionsTextBox.Text));
         m_BackgroundThread = new BackgroundWorker();
         m_BackgroundThread.WorkerReportsProgress = true;
         m_BackgroundThread.WorkerSupportsCancellation = true;

         m_BackgroundThread.DoWork += (o, e) =>
         {
            m_Info = m_Counter.Calculate();
         };

         m_BackgroundThread.ProgressChanged += (o, e) =>
         {
            // ВНИМАНИЕ! ЗДЕСЬ НЕ ДОЛЖНО БЫТЬ НИКАКИХ Application.DoEvents();
            ShowResults();
         };

         m_BackgroundThread.RunWorkerCompleted += (o, e) =>
         {
            ShowResults();

            if (m_Counter.GetCancelOperationFlag())
            {
               StatusLabel.Text = ICloneLocalization.CMNMESS_OperationCancelled;
            }
            else
            {
               if (e.Error != null)
               {
                  StatusLabel.Text = ICloneLocalization.CMNMESS_OperationtDoneWithErrors;
               }
               else
               {
                  ShowSyntacticInfo();
                  StatusLabel.Text = ICloneLocalization.CMNMESS_OperationDoneSuccesfully;
               }
            }

            m_StopWatch.Stop();
            CancelOpButton.Enabled = false;
         };
      }

      private void ReportProgress(object sender, EventArgs e)
      {
         --m_ReportingCounter;
         if (m_ReportingCounter <= 0)
         {
            m_ReportingCounter = REPORTING_FRECUENCY;
            m_BackgroundThread.ReportProgress(1);
         }
      }

      private void LoadSettings()
      {
         CodeLocationTextBox.Text = Properties.Settings.Default.DCSLastChoosenFolder;
         ChoosenFilesExtensionsTextBox.Text = Properties.Settings.Default.DCSLastChoosenFilesExtensions;
         var LangComboBoxInitializer = new CComboBoxInitializer<ICloneExtension>(ref ChooseLangComboBox, new EventHandler(ChooseLangComboBox_DropDownClosed),
             CAvailableExtentions.GetExtentionsList().Values, CAvailableExtentions.GetExtention((LANGUAGES)Properties.Settings.Default.CSWLastChoosenLanguage));
      }

      private void SaveSettings()
      {
         if (Properties.Settings.Default.DCSMemorizeLastRunParams)
         {
            Properties.Settings.Default.DCSLastChoosenFolder = CodeLocationTextBox.Text;
            Properties.Settings.Default.DCSLastChoosenFilesExtensions = ChoosenFilesExtensionsTextBox.Text;
            Properties.Settings.Default.Save();
         }
      }

      private void ChooseLangComboBox_DropDownClosed(object sender, EventArgs e)
      {
         ComboBox lang_combobox = sender as ComboBox;

         if (lang_combobox.SelectedIndex > -1)
         {
            ICloneExtension extension = lang_combobox.SelectedItem as ICloneExtension;
            ChoosenFilesExtensionsTextBox.Text = extension.GetSourceFileExtentions();
            ChooseLangComboBox_Validating(ChooseLangComboBox, new CancelEventArgs());
            ChoosenFilesExtensionsTextBox_Validating(ChoosenFilesExtensionsTextBox, new CancelEventArgs());
         }
      }

      #endregion

      public DetectCodeSizeWindow()
      {
         InitializeComponent();
         InitUI();
         LoadSettings();
         InitCodeCounter();
      }

      private void CodeLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (String.IsNullOrEmpty(CodeLocationTextBox.Text))
         {
            DCS_ErrorProvider.SetError(CodeLocationTextBox, ICloneLocalization.DCS_ChooseFolderError_Empty);
            StatusLabel.Text = ICloneLocalization.DCS_ChooseFolderError_Empty;
         }
         else
         {
            if (Directory.Exists(CodeLocationTextBox.Text))
            {
               DCS_ErrorProvider.SetError(CodeLocationTextBox, "");
               StatusLabel.Text = "";
            }
            else
            {
               DCS_ErrorProvider.SetError(CodeLocationTextBox, ICloneLocalization.DCS_ChooseFolderError_NoneExist);
               StatusLabel.Text = ICloneLocalization.DCS_ChooseFolderError_NoneExist;
            }
         }
      }

      private void ChoosenFilesExtensionsTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (String.IsNullOrEmpty(ChoosenFilesExtensionsTextBox.Text))
         {
            DCS_ErrorProvider.SetError(ChoosenFilesExtensionsTextBox, ICloneLocalization.DCS_ChooseFilesExtensions_Empty);
            StatusLabel.Text = ICloneLocalization.DCS_ChooseFilesExtensions_Empty;
         }
         else
         {
            DCS_ErrorProvider.SetError(ChoosenFilesExtensionsTextBox, "");
            StatusLabel.Text = "";
         }
      }

      private void ChooseFolderButton_Click(object sender, EventArgs e)
      {
         if (ChooseFolderDialog.ShowDialog() == DialogResult.OK)
         {
            CodeLocationTextBox.Text = ChooseFolderDialog.SelectedPath;
            CodeLocationTextBox_Validating(sender, new System.ComponentModel.CancelEventArgs());
         }
      }

      private void ShowResults()
      {
         TimeElapsedValueLabel.Text = m_StopWatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");
         CountOfLinesLabel.Text = m_Counter.CountOfLines.ToString("N00");
         FileProcessedValueLabel.Text = m_Counter.CountOfFiles.ToString("N00");
         TokensProcessedValueLabel.Text = m_Counter.CountOfTokens.ToString("N00");
         SyntacticUnitsProcessedValueLabel.Text = m_Counter.CountOfSyntacticUnits.ToString("N00");
      }

      private void ShowSyntacticInfo()
      {
         MinSizeValueLabel.Text = m_Info.MinSyntacticUnitSize.ToString("N00");
         MaxSizeValueLabel.Text = m_Info.MaxSyntacticUnitSize.ToString("N00");
         MediumSizeValueLabel.Text = m_Info.MediumSyntacticUnitSize.ToString();
         MeanSquareDeviationValueLabel.Text = m_Info.MeanSquareDeviation.ToString();
         KminValueLabel.Text = m_Info.Kmin.ToString("N00");
      }

      private void ResetResults()
      {
         m_Info = new CSyntacticInfo();
         LANGUAGES LangID = (ChooseLangComboBox.SelectedItem as ICloneExtension).LanguageID();
         m_Counter = new CCodeSizeDetector(LangID, new CLoadFilesOptions(CodeLocationTextBox.Text, ChoosenFilesExtensionsTextBox.Text));
         m_StopWatch.Reset();
      }

      private void StartButton_Click(object sender, EventArgs e)
      {
         if (!m_BackgroundThread.IsBusy)
         {
            if (!String.IsNullOrEmpty(CodeLocationTextBox.Text))
            {
               if (Directory.Exists(CodeLocationTextBox.Text))
               {
                  if (ChooseLangComboBox.SelectedIndex > -1)
                  {
                     if (!String.IsNullOrEmpty(ChoosenFilesExtensionsTextBox.Text))
                     {
                        ResetResults();
                        ShowResults();
                        ShowSyntacticInfo();
                        CancelOpButton.Enabled = true;
                        StatusLabel.Text = ICloneLocalization.CMNMESS_OperationInProgress;
                        m_Counter.LoadFilesProgress += new EventHandler(ReportProgress);
                        m_StopWatch.Start();
                        m_BackgroundThread.RunWorkerAsync();
                     }
                     else
                     {
                        ChoosenFilesExtensionsTextBox.Focus();
                        ChoosenFilesExtensionsTextBox_Validating(ChoosenFilesExtensionsTextBox, new CancelEventArgs());
                     }
                  }
                  else
                  {
                     ChooseLangComboBox.Focus();
                     ChooseLangComboBox_Validating(ChooseLangComboBox, new CancelEventArgs());
                  }
               }
               else
               {
                  CodeLocationTextBox.Focus();
                  CodeLocationTextBox_Validating(CodeLocationTextBox, new CancelEventArgs());
               }
            }
            else
            {
               CodeLocationTextBox.Focus();
               CodeLocationTextBox_Validating(CodeLocationTextBox, new CancelEventArgs());
            }
         }
      }

      private void DetectCodeSizeWindow_FormClosing(object sender, FormClosingEventArgs e)
      {
         SaveSettings();
      }

      private void CancelOpButton_Click(object sender, EventArgs e)
      {
         StatusLabel.Text = ICloneLocalization.CMNMESS_OperationCancellingInProgress;
         m_Counter.SetCancelOperationFlag(true);
         m_BackgroundThread.CancelAsync();
         StatusLabel.Text = ICloneLocalization.CMNMESS_OperationCancelled;
      }

      private void ChooseLangComboBox_Validating(object sender, CancelEventArgs e)
      {
         if (ChooseLangComboBox.Items.Count == 0)
         {
            DCS_ErrorProvider.SetError(ChooseLangComboBox, ICloneLocalization.CMNMESS_PluginsNotAvailable);
            StatusLabel.Text = ICloneLocalization.CMNMESS_PluginsNotAvailable;
         }
         else
         {
            if (ChooseLangComboBox.SelectedItem == null)
            {
               DCS_ErrorProvider.SetError(ChooseLangComboBox, ICloneLocalization.CMNMESS_NoOnePluginChoosen);
               StatusLabel.Text = ICloneLocalization.CMNMESS_NoOnePluginChoosen;
            }
            else
            {
               DCS_ErrorProvider.SetError(ChooseLangComboBox, "");
               StatusLabel.Text = "";
            }
         }
      }
   }
}