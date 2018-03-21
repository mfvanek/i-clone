using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using IClone.ICloneImplementation.AdditionalClasses;

namespace IClone.ICloneImplementation.GUIControls
{
    public delegate void DelegateLanguageChange();

    public partial class CCommonOptionsUserControl : CICloneBaseUIControl, ICloneOptionsCommonGUI
    {
        public event DelegateLanguageChange OnLanguageChange;

        #region // Реализация интерфейса ICloneUILocalization

        /// <summary>
        /// Инициализация пользовательского интерфейса
        /// </summary>
        public override void InitUI()
        {
            LangGroupBox.Text = ICloneLocalization.GUI_CMNOP_ChooseUILangLabelText;
            ChooseUILangLabel.Text = ICloneLocalization.GUI_CMNOP_ChooseUILangLabelText;
        }

        #endregion

        private void InitControl()
        {
            InitializeComponent();
            InitUI();
            OnLanguageChange += new DelegateLanguageChange(InitUI);
            LoadSettings();
        }

        public CCommonOptionsUserControl()
        {
            InitControl();
        }

        public CCommonOptionsUserControl(DelegateLanguageChange CallBack)
        {
            InitControl();
            OnLanguageChange += CallBack;
        }

        #region // Реализация ICloneOptionsCommonGUI

        /// <summary>
        /// Загрузить параметры
        /// </summary>
        public override void LoadSettings()
        {
            int SelectedIndex = -1;
            CultureComboBox.Items.Clear();
            Dictionary<string, string> AvailableCulture = CAvailableCulture.GetAvailableCultureList();
            foreach (KeyValuePair<string, string> lang_key in AvailableCulture)
            {
                if (lang_key.Value == Properties.Settings.Default.UILanguage)
                {
                    SelectedIndex = CultureComboBox.Items.Count;
                }
                CultureComboBox.Items.Add(lang_key.Key);
            }
            CultureComboBox.SelectedIndex = SelectedIndex;
        }

        /// <summary>
        /// Сохранить параметры
        /// </summary>
        public override void SaveSettings()
        {
            Properties.Settings.Default.UILanguage = CAvailableCulture.GetCultureID(CultureComboBox.SelectedItem as string);
            Properties.Settings.Default.Save();
        }

        #endregion

        private void CultureComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CAvailableCulture.GetCultureID(CultureComboBox.SelectedItem as string));
            OnLanguageChange();
        }
    }
}
