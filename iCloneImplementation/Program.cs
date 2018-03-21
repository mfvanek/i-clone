using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using IClone.ICloneImplementation.WindowForms;
using NDesk.Options;

namespace IClone.ICloneImplementation
{
    static class Program
    {
        /// <summary>
        /// Allocates a new console for the calling process.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        /// <summary>
        /// Detaches the calling process from its console.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        /// <summary>
        /// Attaches the calling process to the console of the specified process.
        /// </summary>
        /// <param name="dwProcessId">The identifier of the process whose console is to be used.</param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            // Определяем язык интерфейса
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.UILanguage);

            bool help = false;
            bool version = false;
            bool IsConsoleMode = false;
            var p = new OptionSet()
            {
            { "h|?|help", "show this message and exit.", v => help = v != null },
            { "c|C|Console", "Запуск программы в консольном режиме", v => IsConsoleMode = v != null },
            { "v|V|version", "output version information and exit.", v => version = v != null },
            //{ "a|alg|algorithm", "алгоритм поиска клонов. см. CloneSearchAlgoritms", v => f},
            //{ "p|P|path", "Путь к каталогу с исходниками"},
            //{ "l|lang|language", "язык программирования"},
            //{ "e|ext|extension", "расширения файлов для обработки"},
            };

            try
            {
                p.Parse(args);
            }
            catch (OptionException)
            {
                //Console.Write("localization: ");
                //Console.WriteLine(e.Message);
                return;
            }

            if (IsConsoleMode)
            {
                // Получить указатель на "активное" окно (на то, которое сейчас на переднем плане)
                // The idea here is that if the user is starting our application from an existing console
                // shell, that shell will be the uppermost window.  We'll get it and attach to it
                IntPtr ForegroundWindowPtr = GetForegroundWindow();
                int ProcessID = -1;
                GetWindowThreadProcessId(ForegroundWindowPtr, out ProcessID);
                Process process = Process.GetProcessById(ProcessID);

                if (process.ProcessName == "cmd")    //Is the uppermost window a cmd process?
                {
                    //we have a console to attach to ..
                    AttachConsole(process.Id);
                }
                else
                {
                    //no console AND we're in console mode ... create a new console.
                    AllocConsole();
                }

                if (help)
                {
                    p.WriteOptionDescriptions(Console.Out);
                    //Console.WriteLine(AboutBox.GetICloneBriefInfo());
                    //Console.WriteLine(ICloneLocalization.CONSOLE_UseGUIModeMessage);
                    //Console.WriteLine(ICloneLocalization.CONSOLE_WaitingMessage);
                    Console.ReadLine();
                }

                if (version)
                    Console.WriteLine(AboutBox.GetICloneBriefInfo());
                else
                {
                    Console.WriteLine(ICloneLocalization.CNMMESS_FunctionalityNotSupported);
                    Console.WriteLine(ICloneLocalization.CONSOLE_WaitingMessage);
                    Console.ReadLine();
                }

                FreeConsole();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
        }
    }
}