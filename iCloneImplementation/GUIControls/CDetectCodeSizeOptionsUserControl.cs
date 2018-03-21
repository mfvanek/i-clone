
namespace IClone.ICloneImplementation.GUIControls
{
    public partial class CDetectCodeSizeOptionsUserControl : CICloneBaseUIControl, ICloneOptionsCommonGUI
    {
        #region // Реализация интерфейса ICloneUILocalization

        /// <summary>
        /// Инициализация пользовательского интерфейса
        /// </summary>
        public override void InitUI()
        {
            DetectCodeSizeOpGroupBox.Text = ICloneLocalization.GUI_CMN_GroupBoxText;
            MemorizeLastRunParamsCheckBox.Text = ICloneLocalization.GUI_CMN_MemorizeLastRunParamsCheckBoxText;
            ChooseFolderLabel.Text = ICloneLocalization.DCS_ChooseFolderLabelText;
            ChoosenFileExtLabel.Text = ICloneLocalization.DCS_ChoosenFileExtLabelText;
        }

        #endregion

        public CDetectCodeSizeOptionsUserControl()
        {
            InitializeComponent();
            InitUI();
            LoadSettings();
        }

        #region // Реализация ICloneOptionsCommonGUI

        /// <summary>
        /// Загрузить параметры
        /// </summary>
        public override void LoadSettings()
        {
            MemorizeLastRunParamsCheckBox.Checked = Properties.Settings.Default.DCSMemorizeLastRunParams;
            ChoosenFolderTextBox.Text = Properties.Settings.Default.DCSLastChoosenFolder;
            ChoosenFilesExtensionsTextBox.Text = Properties.Settings.Default.DCSLastChoosenFilesExtensions;
        }

        /// <summary>
        /// Сохранить параметры
        /// </summary>
        public override void SaveSettings()
        {
            Properties.Settings.Default.DCSMemorizeLastRunParams = MemorizeLastRunParamsCheckBox.Checked;
            Properties.Settings.Default.DCSLastChoosenFolder = ChoosenFolderTextBox.Text;
            Properties.Settings.Default.DCSLastChoosenFilesExtensions = ChoosenFilesExtensionsTextBox.Text;
            Properties.Settings.Default.Save();
        }

        #endregion
    }
}
