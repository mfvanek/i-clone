using System;

namespace ICloneSearchLibrary.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICloneSearchCallbacks
    {
        #region // Реализация интерфейса ICloneSearchCallbacks

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportCloneSearchStart"></param>
        /// <param name="ReportCloneSearchProgress"></param>
        /// <param name="ReportCloneSearchEnd"></param>
        void InitCloneSearchCallbacks(EventHandler ReportCloneSearchStart, EventHandler ReportCloneSearchProgress, EventHandler ReportCloneSearchEnd);

        #endregion
    }
}
