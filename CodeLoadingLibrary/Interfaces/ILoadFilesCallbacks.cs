using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeLoadingLibrary.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoadFilesCallbacks
    {
        #region // Реализация интерфейса ILoadFilesCallbacks

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportLoadFilesStart"></param>
        /// <param name="ReportLoadFilesProgress"></param>
        /// <param name="ReportLoadFilesEnd"></param>
        void InitLoadFilesCallbacks(EventHandler ReportLoadFilesStart, EventHandler ReportLoadFilesProgress, EventHandler ReportLoadFilesEnd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportFilesListBuildingStart"></param>
        /// <param name="ReportFilesListBuildingProgress"></param>
        /// <param name="ReportFilesListBuildingEnd"></param>
        void InitFilesListBuildingCallbacks(EventHandler ReportFilesListBuildingStart, EventHandler ReportFilesListBuildingProgress, EventHandler ReportFilesListBuildingEnd);

        #endregion
    }
}
