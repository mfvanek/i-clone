using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IClone.ICloneImplementation.WindowForms
{
    public partial class CloneReportWindow : Form
    {
        string m_CloneReportName;
        bool m_FirstShow = true;

        public CloneReportWindow(string _CloneReportName)
        {
            InitializeComponent();
            m_CloneReportName = _CloneReportName;
            IClone.ICloneImplementation.AdditionalClasses.CWindowNamer.SetWindowName(this, "Клон-отчёт: " + _CloneReportName);
            LoadCloneReport();
        }

        private void LoadCloneReport()
        {
            try
            {
                this.CloneReportBrowser.Navigate(m_CloneReportName);
            }
            catch (Exception)
            {
            }
        }

        private void CloneReportWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.CloneReportBrowser.Dispose();
        }

        private void CloneReportBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!m_FirstShow)
            {
                e.Cancel = true;
            }
            else
            {
                m_FirstShow = false;
            }
        }
    }
}
