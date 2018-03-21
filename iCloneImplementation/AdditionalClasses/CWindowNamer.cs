using System;
using System.Reflection;

namespace IClone.ICloneImplementation.AdditionalClasses
{
    public static class CWindowNamer
    {
        #region Assembly Attribute Accessors

        /// <summary>
        /// Установить заголовок окна по шаблону "Программа | версия | Расширение заголовка"
        /// </summary>
        /// <param name="Window"></param>
        /// <param name="TitleExt">Расширение заголовка</param>
        public static void SetWindowName(System.Windows.Forms.Form Window, string TitleExt)
        {
            if (Window != null)
            {
                Window.Text = BuildWindowName(TitleExt);
            }
        }

        /// <summary>
        /// Построить строку для заголовка окна
        /// </summary>
        /// <param name="TitleExt"></param>
        /// <returns></returns>
        public static string BuildWindowName(string TitleExt)
        {
            string retval = AssemblyTitle + " v." + AssemblyVersion;

            if (!String.IsNullOrEmpty(TitleExt))
            {
                retval = retval + " - " + TitleExt;
            }

            return retval;
        }

        /// <summary>
        /// Построить строку для заголовка окна
        /// </summary>
        /// <returns></returns>
        public static string BuildWindowName()
        {
            return BuildWindowName(string.Empty);
        }

        /// <summary>
        /// Название программы
        /// </summary>
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!String.IsNullOrEmpty(titleAttribute.Title))
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Версия программы
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Описание программы
        /// </summary>
        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Авторские права
        /// </summary>
        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion
    }
}
