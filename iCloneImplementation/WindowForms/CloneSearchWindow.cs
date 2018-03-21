using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using IClone.ICloneImplementation.AdditionalClasses;
using ICloneBaseLibrary.Classes;
using ICloneExtensions;
using ICloneExtensions.Interfaces;
using ICloneSearchLibrary.Classes;
using ICloneServices.Classes;
using ISourceFilesLibrary.Classes;
using ISourceFilesLibrary.Classes.Languages;
using CodeLoadingLibrary.Classes;
using ISourceFilesLibrary.Classes.CodeFragment;
using ICloneCodeCharacteristicsLibrary.Classes;
using IClone.IDetectCodeSize;

namespace IClone.ICloneImplementation.WindowForms
{
   public partial class CloneSearchWindow : Form, ICloneUILocalization, IDisposable
   {
      private const int INVALID_COMBOBOX_SelectedIndex = -1;

      #region // Поля класса

      private CBaseCloneSearchExecutor m_CloneSearchExecutor;
      /// <summary>
      /// Вспомогательный объект для измерения временных интервалов
      /// </summary>
      private Stopwatch m_StopWatch;

      #endregion

      #region // Реализация интерфейса ICloneUILocalization

      /// <summary>
      /// Инициализация пользовательского интерфейса
      /// </summary>
      public void InitUI()
      {
         CWindowNamer.SetWindowName(this, ICloneLocalization.CSW_WindowHeader);
         CloneSearchTabPage1.Text = ICloneLocalization.CSW_CloneSearchTabPage1Text;
         CloneSearchTabPage2.Text = ICloneLocalization.CSW_CloneSearchTabPage2Text;
         CodeLocationGroupBox.Text = ICloneLocalization.CSW_CodeLocationGroupBoxText;
         CodeLocationLabel.Text = ICloneLocalization.CSW_CodeLocationLabelText;
         ChooseCodeLocationButton.Text = ICloneLocalization.CSW_ChooseCodeLocationButtonText;
         CodeEncodingLabel.Text = ICloneLocalization.CSW_CodeEncodingLabelText;
         CodeLanguageLabel.Text = ICloneLocalization.CSW_CodeLanguageLabelText;
         ChoosenFilesExtensionsLabel.Text = ICloneLocalization.DCS_ChoosenFileExtLabelText;
         StartCloneSearchButton.Text = ICloneLocalization.CSW_StartCloneSearchButtonText;
         StopCloneSearchButton.Text = ICloneLocalization.CSW_StopCloneSearchButtonText;
         StatusLabel.Text = ICloneLocalization.CMNMESS_WaitingForUserActions;
         CodeProcessingResultsGroupBox.Text = ICloneLocalization.CSW_CodeProcessingResultsGroupBoxText;
         TimeElapsedNameLabel.Text = ICloneLocalization.CMN_TimeElapsedNameLabelText;
         FileProcessedNameLabel.Text = ICloneLocalization.CMN_FileProcessedNameLabelText;
         RowsProcessedNameLabel.Text = ICloneLocalization.CMN_RowsProcessedNameLabelText;
         RowsInOriginalFilesNameLabel.Text = ICloneLocalization.CSW_RowsInOriginalFilesNameLabelText;
         RowsInModifiedFilesNameLabel.Text = ICloneLocalization.CSW_RowsInModifiedFilesNameLabelText;
      }

      #endregion

      #region // Конструкторы

      #region // Вспомогательные функции для инициализации компонентов

      private void ChooseLangComboBox_DropDownClosed(object sender, EventArgs e)
      {
         ComboBox lang_combobox = sender as ComboBox;

         if (lang_combobox.SelectedIndex > INVALID_COMBOBOX_SelectedIndex)
         {
            ICloneExtension extension = lang_combobox.SelectedItem as ICloneExtension;
            ChoosenFilesExtensionsTextBox.Text = extension.GetSourceFileExtentions();
            ChooseLangComboBox_Validating(ChooseLangComboBox, new CancelEventArgs());
            ChoosenFilesExtensionsTextBox_Validating(ChoosenFilesExtensionsTextBox, new CancelEventArgs());
         }
      }

      #endregion

      #region // Загрузка и сохранение настроек

      private void LoadSettings()
      {
         CodeLocationTextBox.Text = Properties.Settings.Default.CSWLastChoosenFolder;
         var LangComboBoxInitializer = new CComboBoxInitializer<ICloneExtension>(ref ChooseLangComboBox, new EventHandler(ChooseLangComboBox_DropDownClosed),
             CAvailableExtentions.GetExtentionsList().Values, CAvailableExtentions.GetExtention((LANGUAGES)Properties.Settings.Default.CSWLastChoosenLanguage));
      }

      private void SaveSettings()
      {
         if (Properties.Settings.Default.CSWMemorizeLastRunParams)
         {
            Properties.Settings.Default.CSWLastChoosenFolder = CodeLocationTextBox.Text;
            if (CodeEncodingComboBox.SelectedItem != null)
            {
               Properties.Settings.Default.CSWLastChoosenEncoding = (CodeEncodingComboBox.SelectedItem as CICloneEncodingInfo).CodePage;
            }
            else
            {
               Properties.Settings.Default.CSWLastChoosenEncoding = 1251;
            }
            if (ChooseLangComboBox.SelectedItem != null)
            {
               Properties.Settings.Default.CSWLastChoosenLanguage = (int)(ChooseLangComboBox.SelectedItem as ICloneExtension).LanguageID();
            }
            else
            {
               Properties.Settings.Default.CSWLastChoosenLanguage = INVALID_COMBOBOX_SelectedIndex;
            }
            Properties.Settings.Default.Save();
         }
      }

      #endregion

      public CloneSearchWindow()
      {
         InitializeComponent();
         InitUI();
         LoadSettings();
         CCloneSearchOptionsGroup.InitCodeEncodingComboBox(ref CodeEncodingComboBox);
         m_CloneSearchExecutor = new CBaseCloneSearchExecutor(CAvailableCloneSearchAlgorithms.GetAlgorithm((CloneSearchAlgoritms)Properties.Settings.Default.CSWLastChoosenAlgorithm), CAvailableExtentions.GetExtention((LANGUAGES)Properties.Settings.Default.CSWLastChoosenLanguage));

         m_CloneSearchExecutor.CloneSearchExecutingEnd += new MessageEventHandler(ReportCloneSearchExecutingEnd);
         m_CloneSearchExecutor.CloneSearchExecutingStart += new MessageEventHandler(ReportCloneSearchExecutingStart);
         m_CloneSearchExecutor.CloneSearchExecutingProgress += new MessageEventHandler(ReportCloneSearchExecutingProgress);
         m_StopWatch = new Stopwatch();
         KminValue.Value = CCodeFragment.Kmin;
      }

      #endregion

      private void SayTimeElapsed()
      {
         TimeElapsedValueLabel.Invoke(new Action(() => { TimeElapsedValueLabel.Text = m_StopWatch.Elapsed.ToString(@"hh\:mm\:ss\.fff"); }));
      }

      private void ChooseCodeLocationButton_Click(object sender, EventArgs e)
      {
         if (ChooseCodeLocationDialog.ShowDialog() == DialogResult.OK)
         {
            CodeLocationTextBox.Text = ChooseCodeLocationDialog.SelectedPath;
            CodeLocationTextBox_Validating(sender, new System.ComponentModel.CancelEventArgs());
         }
      }

      private void PrepareLoadFilesOptions()
      {
         ICloneExtension Ext = ChooseLangComboBox.SelectedItem as ICloneExtension;
         CICloneEncodingInfo EncodeInfo = CodeEncodingComboBox.SelectedItem as CICloneEncodingInfo;
         CLoadFilesOptions LoadFilesOptions = new CLoadFilesOptions(CodeLocationTextBox.Text, ChoosenFilesExtensionsTextBox.Text, EncodeInfo.GetEncodingInfo().GetEncoding(), new CCodePreProcessingOptions(Ext.GetCommentSymbols()));
         LoadFilesOptions.SetSkippingFolders(Properties.Settings.Default.CSWSkippingFolders);
         LoadFilesOptions.SetIsUseParallelExtensions(Properties.Settings.Default.CSWIsUseParallelExtensions);

         m_CloneSearchExecutor.SetLoadFilesOptions(LoadFilesOptions);
         if (AutomaticKminCalculationCheckBox.Checked)
         {
            CCodeSizeDetector Counter = new CCodeSizeDetector(Ext.LanguageID(), new CLoadFilesOptions(CodeLocationTextBox.Text, ChoosenFilesExtensionsTextBox.Text));
            //Counter.LoadFilesProgress += new EventHandler(ReportProgress);
            CSyntacticInfo info = Counter.Calculate();
            CCodeFragment.SetKmin(info.Kmin);
         }
         else
            CCodeFragment.SetKmin((long)KminValue.Value);
         m_CloneSearchExecutor.Extension = Ext;
      }

      private void SayCodeLocationTextBoxError()
      {
         CodeLocationTextBox.Focus();
         CodeLocationTextBox_Validating(CodeLocationTextBox, new CancelEventArgs());
      }

      private bool Check()
      {
         bool result = false;

         if (!String.IsNullOrEmpty(CodeLocationTextBox.Text))
         {
            if (Directory.Exists(CodeLocationTextBox.Text))
            {
               if (ChooseLangComboBox.SelectedIndex > INVALID_COMBOBOX_SelectedIndex)
               {
                  if (!String.IsNullOrEmpty(ChoosenFilesExtensionsTextBox.Text))
                  {
                     if (CodeEncodingComboBox.SelectedIndex > INVALID_COMBOBOX_SelectedIndex)
                     {
                        result = true;
                     }
                     else
                     {
                        CodeEncodingComboBox.Focus();
                     }
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
               SayCodeLocationTextBoxError();
            }
         }
         else
         {
            SayCodeLocationTextBoxError();
         }

         return result;
      }

      private void StartCloneSearchButton_Click(object sender, EventArgs e)
      {
         if (!m_CloneSearchExecutor.IsBusy() && Check())
         {
            PrepareLoadFilesOptions();
            m_CloneSearchExecutor.Run();
         }
      }

      private void ReportCloneSearchExecutingStart(object sender, MessageEventArgs e)
      {
         m_StopWatch.Reset();
         SayTimeElapsed();
         m_StopWatch.Start();
         StartCloneSearchButton.Invoke(new Action(() => { StartCloneSearchButton.Enabled = false; }));
         StartCloneSearchButton.Invoke(new Action(() => { StopCloneSearchButton.Enabled = true; }));
         StatusLabel.Text = e.Message;
      }

      private void ReportCloneSearchExecutingProgress(object sender, MessageEventArgs e)
      {
         SayTimeElapsed();
         StatusLabel.Text = e.Message;
         FileProcessedValueLabel.Invoke(new Action(() => { FileProcessedValueLabel.Text = (sender as CBaseCloneSearchExecutor).FilesCount().ToString("N00"); }));
         RowsInOriginalFilesValueLabel.Invoke(new Action(() => { RowsInOriginalFilesValueLabel.Text = (sender as CBaseCloneSearchExecutor).GetCountOfLinesInOriginFiles().ToString(); }));
         RowsInModifiedFilesValueLabel.Invoke(new Action(() => { RowsInModifiedFilesValueLabel.Text = (sender as CBaseCloneSearchExecutor).GetCountOfLinesInModifiedFiles().ToString(); }));
         RowsProcessedValueLabel.Invoke(new Action(() => { RowsProcessedValueLabel.Text = (sender as CBaseCloneSearchExecutor).ProcessedClonesCounter.ToString(); }));
      }

      private void ReportCloneSearchExecutingEnd(object sender, MessageEventArgs e)
      {
         m_StopWatch.Stop();
         SayTimeElapsed();
         StartCloneSearchButton.Invoke(new Action(() => { StartCloneSearchButton.Enabled = true; }));
         StartCloneSearchButton.Invoke(new Action(() => { StopCloneSearchButton.Enabled = false; }));
         StatusLabel.Text = e.Message;
      }

      private void CloneSearchWindow_FormClosing(object sender, FormClosingEventArgs e)
      {
         SaveSettings();
      }

      private void StopCloneSearchButton_Click(object sender, EventArgs e)
      {
         m_CloneSearchExecutor.CancelAsync();
      }

      private void CodeLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (String.IsNullOrEmpty(CodeLocationTextBox.Text))
         {
            CSW_ErrorProvider.SetError(CodeLocationTextBox, ICloneLocalization.DCS_ChooseFolderError_Empty);
            StatusLabel.Text = ICloneLocalization.DCS_ChooseFolderError_Empty;
         }
         else
         {
            if (Directory.Exists(CodeLocationTextBox.Text))
            {
               CSW_ErrorProvider.SetError(CodeLocationTextBox, "");
               StatusLabel.Text = "";
            }
            else
            {
               CSW_ErrorProvider.SetError(CodeLocationTextBox, ICloneLocalization.DCS_ChooseFolderError_NoneExist);
               StatusLabel.Text = ICloneLocalization.DCS_ChooseFolderError_NoneExist;
            }
         }
      }

      private void ChoosenFilesExtensionsTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (String.IsNullOrEmpty(ChoosenFilesExtensionsTextBox.Text))
         {
            CSW_ErrorProvider.SetError(ChoosenFilesExtensionsTextBox, ICloneLocalization.DCS_ChooseFilesExtensions_Empty);
            StatusLabel.Text = ICloneLocalization.DCS_ChooseFilesExtensions_Empty;
         }
         else
         {
            CSW_ErrorProvider.SetError(ChoosenFilesExtensionsTextBox, "");
            StatusLabel.Text = "";
         }
      }

      private void ChooseLangComboBox_Validating(object sender, CancelEventArgs e)
      {
         if (ChooseLangComboBox.Items.Count == 0)
         {
            CSW_ErrorProvider.SetError(ChooseLangComboBox, ICloneLocalization.CMNMESS_PluginsNotAvailable);
            StatusLabel.Text = ICloneLocalization.CMNMESS_PluginsNotAvailable;
         }
         else
         {
            if (ChooseLangComboBox.SelectedItem == null)
            {
               CSW_ErrorProvider.SetError(ChooseLangComboBox, ICloneLocalization.CMNMESS_NoOnePluginChoosen);
               StatusLabel.Text = ICloneLocalization.CMNMESS_NoOnePluginChoosen;
            }
            else
            {
               CSW_ErrorProvider.SetError(ChooseLangComboBox, "");
               StatusLabel.Text = "";
            }
         }
      }

      private void AutomaticKminCalculationCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         if (AutomaticKminCalculationCheckBox.Checked)
            KminValue.Enabled = false;
         else
            KminValue.Enabled = true;
      }
   }
}