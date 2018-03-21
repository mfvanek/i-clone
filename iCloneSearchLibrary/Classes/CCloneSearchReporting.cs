using System;
using ICloneBaseLibrary.Classes;
using ICloneSearchLibrary.Interfaces;

namespace ICloneSearchLibrary.Classes
{
    public abstract class CCloneSearchReporting : CBreakableActions, ICloneSearchReporting, ICloneSearchCallbacks
    {
        #region // События

        /// <summary>
        /// Событие, возникающее при начале поиска клонов
        /// </summary>
        public event EventHandler CloneSearchStart;
        /// <summary>
        /// Событие, возникающее при завершении поиска клонов
        /// </summary>
        public event EventHandler CloneSearchEnd;
        /// <summary>
        /// Событие, возникающее при выполнении некоторого этапа во время поиска клонов
        /// </summary>
        public event EventHandler CloneSearchProgress;

        #endregion

        #region // Реализация интерфейса ICloneSearchCallbacks

        public void InitCloneSearchCallbacks(EventHandler ReportCloneSearchStart, EventHandler ReportCloneSearchProgress, EventHandler ReportCloneSearchEnd)
        {
            CloneSearchStart += ReportCloneSearchStart;
            CloneSearchProgress += ReportCloneSearchProgress;
            CloneSearchEnd += ReportCloneSearchEnd;
        }

        #endregion

        #region // ICloneSearchReporting

        #region // OnCloneSearch

        /// <summary>
        /// Уведомить о начале поиска клонов
        /// </summary>
        public virtual void OnCloneSearchStart()
        {
            if (CloneSearchStart != null)
            {
                CloneSearchStart(this, new EventArgs());
            }
        }

        /// <summary>
        /// Уведомить о прогрессе поиска клонов
        /// </summary>
        public virtual void OnCloneSearchProgress()
        {
            if (CloneSearchProgress != null)
            {
                CloneSearchProgress(this, new EventArgs());
            }
        }

        /// <summary>
        /// Уведомить о завершении поиска клонов
        /// </summary>
        public virtual void OnCloneSearchEnd()
        {
            if (CloneSearchEnd != null)
            {
                CloneSearchEnd(this, new EventArgs());
            }
        }

        #endregion

        #endregion
    }
}