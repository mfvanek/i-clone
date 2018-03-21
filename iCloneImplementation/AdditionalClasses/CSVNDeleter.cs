using System;

namespace IClone.ICloneImplementation.AdditionalClasses
{

    public class CSVNDeleter
    {
        long m_FoldersCount;
        string m_Folder;
        //bool m_IsRunning;
        private object SyncRoot = new object();

        public CSVNDeleter()
        {
            ClearCounter();
            lock (SyncRoot)
            {
                m_Folder = "";
                //m_IsRunning = false;
            }
        }

        public void ClearCounter()
        {
            lock (SyncRoot)
            {
                m_FoldersCount = 0;
            }
        }

        public CSVNDeleter(string _Folder)
        {
            ClearCounter();
            lock (SyncRoot)
            {
                //m_IsRunning = false;
                if (System.IO.Directory.Exists(_Folder))
                {
                    m_Folder = _Folder;
                }
                else
                {
                    throw new Exception("Каталог не существует!");
                }
            }
        }

        public void StartDeleting()
        {
            ClearCounter();
            DeleteSVNSubFolders(m_Folder);
        }

        public void StartDeletingSVNFiles(string RootFolder, bool IsCheckSVNFolder)
        {
            bool IsValid = IsCheckSVNFolder ? false : true;

            if (IsCheckSVNFolder)
            {
                System.IO.DirectoryInfo DInfo = new System.IO.DirectoryInfo(RootFolder);

                if (DInfo.Name == ".svn")
                {
                    IsValid = true;
                }
                else
                {
                    throw new ArgumentException("Это не .svn каталог!");
                }
            }

            if (IsValid)
            {
                string[] SubFolders = System.IO.Directory.GetDirectories(RootFolder);

                foreach (string SubFolderName in SubFolders)
                {
                    DeleteSVNFiles(SubFolderName);
                }

                DeleteSVNFiles(RootFolder);
            }
        }

        private void DeleteSVNFiles(string FolderName)
        {
            string[] Files = System.IO.Directory.GetFiles(FolderName);

            if (Files != null && Files.Length > 0)
            {
                foreach (string FileName in Files)
                {
                    System.IO.File.SetAttributes(FileName, System.IO.FileAttributes.Normal);
                    System.IO.File.Delete(FileName);
                }
            }
        }

        public long FoldersCount
        {
            get
            {
                lock (SyncRoot)
                {
                    return m_FoldersCount;
                }
            }
        }

        private void DeleteSVNSubFolders(string FolderName)
        {
            string[] SubFolders = System.IO.Directory.GetDirectories(FolderName);

            foreach (string SubFolderName in SubFolders)
            {
                System.IO.DirectoryInfo DInfo = new System.IO.DirectoryInfo(SubFolderName);

                if (DInfo.Name == ".svn")
                {
                    try
                    {
                        StartDeletingSVNFiles(SubFolderName, false);
                        DInfo.Delete(true);

                        lock (SyncRoot)
                        {
                            m_FoldersCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    DeleteSVNSubFolders(SubFolderName);
                }
            }
        }
    }
}
