using System;
using System.Threading;
using System.Windows.Forms;
using IClone.ICloneImplementation.AdditionalClasses;

namespace IClone.ICloneImplementation.WindowForms
{
    /// <summary>
    /// Главное окно программы
    /// </summary>
    public partial class MainWindow : Form, ICloneUILocalization
    {
        private Thread m_PrimaryThread;
        //private ICloneOptionsWindow m_ICloneOptions;

        #region // Реализация интерфейса ICloneUILocalization

        /// <summary>
        /// Инициализация пользовательского интерфейса
        /// </summary>
        public void InitUI()
        {
            DeleteSVNToolStripMenuItem.Text = ICloneLocalization.MW_DSSFMenuItemName;
            GetCodeSizeToolStripMenuItem.Text = ICloneLocalization.MW_DCSMenuItemName;
            CWindowNamer.SetWindowName(this, ICloneLocalization.MW_WindowHeader);
            HelpMenuItem.Text = ICloneLocalization.MW_HelpMenuItemName;
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            InitUI();

            // Получаем активный поток
            m_PrimaryThread = Thread.CurrentThread;
            m_PrimaryThread.Name = this.Text;
        }

        private void ShowCloneReport()
        {
            CloneReportWindow CloneReport = new CloneReportWindow(this.OpenCloneReportDialog.FileName);
            CloneReport.ShowDialog();
        }

        private void OpenCloneReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.OpenCloneReportDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(this.OpenCloneReportDialog.FileName))
                {
                    ShowCloneReport();
                }
            }
        }

        private void DeleteSVNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSVNSubFoldersForm SVNDel = new DeleteSVNSubFoldersForm();
            SVNDel.ShowDialog();
        }

        private void GetCodeSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectCodeSizeWindow DetectCodeSize = new DetectCodeSizeWindow();
            DetectCodeSize.ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string PreviosLang = Properties.Settings.Default.UILanguage;

            ICloneOptionsWindow ICloneOptions = new ICloneOptionsWindow();
            ICloneOptions.ShowDialog();

            // Если изменился язык, то обновим интерфейс
            if (PreviosLang != Properties.Settings.Default.UILanguage)
            {
                InitUI();
            }

            ICloneOptions.Dispose();
        }

        private void AboutICloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox AboutIClone = new AboutBox();
            AboutIClone.ShowDialog();
        }

        private void CloneSearchMenuItem3_Click(object sender, EventArgs e)
        {
            CloneSearchWindow СloneSearch = new CloneSearchWindow();
            СloneSearch.ShowDialog();
            СloneSearch.Dispose();
            GC.Collect();
        }
    }
}
