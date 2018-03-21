using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using IClone.ICloneImplementation.AdditionalClasses;

namespace IClone.ICloneImplementation.WindowForms
{
    public partial class DeleteSVNSubFoldersForm : Form
    {
        Thread backgroundThread;
        CSVNDeleter Deleter;

        public DeleteSVNSubFoldersForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = this.ChooseFolderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.ChoosenFolderBox.Text = ChooseFolderDialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Deleter = new CSVNDeleter(this.ChoosenFolderBox.Text);

            if (backgroundThread == null)
            {
                // Создаём фоновый поток, который будет обрабатывать файлы
                backgroundThread = new Thread(new ThreadStart(Deleter.StartDeleting));
                backgroundThread.Name = "Вторичный";
                backgroundThread.IsBackground = true; // Фоновый поток!
                backgroundThread.Start();

                while (backgroundThread.IsAlive)
                {
                    Application.DoEvents();
                }
                MessageBox.Show("Закончено! Удалено <" + Deleter.FoldersCount.ToString() + "> каталогов.");
                //CThreadMarshalling.KillingBackgroundThread(ref backgroundThread);
            }
        }
    }

    public class CFileDeleterToRecycleBin
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        private struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string wFrom;
            public string pTo;
            public short pFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [DllImport("shel32.dll", CharSet = CharSet.Auto)]
        private static extern int SHFileOperation(ref SHFILEOPSTRUCT fileOp);

        // Константы для SHFileOperation
    }
}