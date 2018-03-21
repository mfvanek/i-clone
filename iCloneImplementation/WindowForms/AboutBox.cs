using System;
using System.Windows.Forms;
using IClone.ICloneImplementation.AdditionalClasses;

namespace IClone.ICloneImplementation.WindowForms
{
   /// <summary>
   /// Сведения о программе
   /// </summary>
   partial class AboutBox : Form, ICloneUILocalization
   {
      /// <summary>
      /// Краткая информация о программном продукте
      /// </summary>
      /// <returns></returns>
      public static string GetICloneBriefInfo()
      {
         return String.Format(ICloneLocalization.ABOUT_WindowHeader, CWindowNamer.AssemblyTitle) + Environment.NewLine +
             String.Format(ICloneLocalization.ABOUT_VersionLabelText, CWindowNamer.AssemblyVersion) + Environment.NewLine + CWindowNamer.AssemblyCopyright;
      }

      #region // Реализация интерфейса ICloneUILocalization

      /// <summary>
      /// Инициализация пользовательского интерфейса
      /// </summary>
      public void InitUI()
      {
         CWindowNamer.SetWindowName(this, String.Format(ICloneLocalization.ABOUT_WindowHeader, CWindowNamer.AssemblyTitle));
         VersionLabel.Text = String.Format(ICloneLocalization.ABOUT_VersionLabelText, CWindowNamer.AssemblyVersion);
      }

      #endregion

      /// <summary>
      /// Конструктор
      /// </summary>
      public AboutBox()
      {
         InitializeComponent();
         this.labelProductName.Text = CWindowNamer.AssemblyProduct;
         this.labelCopyright.Text = CWindowNamer.AssemblyCopyright;
         this.labelCompanyName.Text = CWindowNamer.AssemblyCompany;
         this.textBoxDescription.Text = CWindowNamer.AssemblyDescription;

         InitUI();
      }
   }
}