using System;
using System.Windows.Forms;
using IClone.ICloneImplementation.AdditionalClasses;
using ICloneBaseLibrary.Classes;
using ICloneExtensions;
using ICloneSearchLibrary.Classes;
using ICloneExtensions.Interfaces;
using ISourceFilesLibrary.Classes.Languages;

namespace IClone.ICloneImplementation.GUIControls
{
   public partial class CCloneSearchOptionsUserControl : CICloneBaseUIControl, ICloneOptionsCommonGUI
   {
      #region // Реализация интерфейса ICloneUILocalization

      /// <summary>
      /// Инициализация пользовательского интерфейса
      /// </summary>
      public override void InitUI()
      {
         DetectCodeSizeOpGroupBox.Text = ICloneLocalization.GUI_CMN_GroupBoxText;
         MemorizeLastRunParamsCheckBox.Text = ICloneLocalization.GUI_CMN_MemorizeLastRunParamsCheckBoxText;
         CodeLocationLabel.Text = ICloneLocalization.CSW_CodeLocationLabelText;
         CodeEncodingLabel.Text = ICloneLocalization.CSW_CodeEncodingLabelText;
         CodeLanguageLabel.Text = ICloneLocalization.CSW_CodeLanguageLabelText;
         SkippingFoldersLabelName.Text = ICloneLocalization.GUI_CSW_SkippingFoldersLabelNameText;
         IsUseParallelExtensionsCheckBox.Text = ICloneLocalization.GUI_CMN_IsUseParallelExtensionsCheckBoxText;
         ChooseAlgorithmLabel.Text = ICloneLocalization.CSW_ChooseAlgorithmLabelText;
      }

      #endregion

      public CCloneSearchOptionsUserControl()
      {
         InitializeComponent();
         InitUI();
         LoadSettings();
         CCloneSearchOptionsGroup.InitCodeEncodingComboBox(ref CodeEncodingComboBox);
      }

      #region // Реализация ICloneOptionsCommonGUI

      /// <summary>
      /// Загрузить параметры
      /// </summary>
      public override void LoadSettings()
      {
         MemorizeLastRunParamsCheckBox.Checked = Properties.Settings.Default.CSWMemorizeLastRunParams;
         CodeLocationTextBox.Text = Properties.Settings.Default.CSWLastChoosenFolder;
         SkippingFoldersTextBox.Text = Properties.Settings.Default.CSWSkippingFolders;
         IsUseParallelExtensionsCheckBox.Checked = Properties.Settings.Default.CSWIsUseParallelExtensions;
         var LangComboBoxInitializer = new CComboBoxInitializer<ICloneExtension>(ref ChooseLangComboBox, new EventHandler(ChooseLangComboBox_DropDownClosed),
             CAvailableExtentions.GetExtentionsList().Values, CAvailableExtentions.GetExtention((LANGUAGES)Properties.Settings.Default.CSWLastChoosenLanguage));
         var AlgorithmComboBoxInitializer = new CComboBoxInitializer<CBaseCloneSearchStrategy>(ref ChooseAlgorithmComboBox, new EventHandler(ChooseAlgorithmComboBox_DropDownClosed),
             CAvailableCloneSearchAlgorithms.GetAlgorithmsList().Values, CAvailableCloneSearchAlgorithms.GetAlgorithm((CloneSearchAlgoritms)Properties.Settings.Default.CSWLastChoosenAlgorithm));
      }

      /// <summary>
      /// Сохранить параметры
      /// </summary>
      public override void SaveSettings()
      {
         Properties.Settings.Default.CSWMemorizeLastRunParams = MemorizeLastRunParamsCheckBox.Checked;
         Properties.Settings.Default.CSWLastChoosenFolder = CodeLocationTextBox.Text;
         Properties.Settings.Default.CSWSkippingFolders = SkippingFoldersTextBox.Text;
         Properties.Settings.Default.CSWIsUseParallelExtensions = IsUseParallelExtensionsCheckBox.Checked;

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
            Properties.Settings.Default.CSWLastChoosenLanguage = -1;
         }

         if (ChooseAlgorithmComboBox.SelectedItem != null)
         {
            Properties.Settings.Default.CSWLastChoosenAlgorithm = (int)(ChooseAlgorithmComboBox.SelectedItem as CBaseCloneSearchStrategy).AlgorithmID();
         }
         else
         {
            Properties.Settings.Default.CSWLastChoosenAlgorithm = (int)CloneSearchAlgoritms.BruteForceAlgorithm;
         }

         Properties.Settings.Default.Save();
      }

      #endregion

      private void ChooseLangComboBox_DropDownClosed(object sender, EventArgs e)
      {
      }

      private void CheckAbilityToUseParallelExtensions()
      {
         if (IsUseParallelExtensionsCheckBox.Checked)
         {
            if (!CParallelExtensionsProvider.IsCanUseParallelExtensions())
            {
               MessageBox.Show(ICloneLocalization.CMNMESS_CannotUseParallelExtensions, CWindowNamer.BuildWindowName(ICloneLocalization.CSW_WindowHeader), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         }
      }

      private void IsUseParallelExtensionsCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         CheckAbilityToUseParallelExtensions();
      }

      private void ChooseAlgorithmComboBox_DropDownClosed(object sender, EventArgs e)
      {
      }
   }
}