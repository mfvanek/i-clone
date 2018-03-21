using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IClone.ICloneReport.Interfaces
{
    public interface ISimilarity
    {
        #region // Реализация интерфейса ISimilarity

        /// <summary>
        /// Схожесть
        /// </summary>
        double Similarity
        {
            get;
            set;
        }

        #endregion
    }
}
