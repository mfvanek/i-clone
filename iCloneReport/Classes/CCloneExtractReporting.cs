using System;
using ICloneBaseLibrary.Classes;
using IClone.ICloneReport.Interfaces;

namespace IClone.ICloneReport.Classes
{
    public abstract class CCloneExtractReporting : CBreakableActions, ICloneExtractReporting
    {
        #region // События

        /// <summary>
        /// Событие, возникающее при начале извлечения клон-фрагментов
        /// </summary>
        public virtual event EventHandler CloneExtractStart;
        /// <summary>
        /// Событие, возникающее при завершении извлечения клон-фрагментов
        /// </summary>
        public virtual event EventHandler CloneExtractEnd;
        /// <summary>
        /// Событие, возникающее при выполнении некоторого этапа во время извлечения клон-фрагментов
        /// </summary>
        public virtual event EventHandler CloneExtractProgress;

        #endregion

        #region // Реализация интерфейса ICloneExtractReporting

        #region // OnCloneExtract

        /// <summary>
        /// Уведомить о начале извлечения клон-фрагментов
        /// </summary>
        public virtual void OnCloneExtractStart()
        {
            if (CloneExtractStart != null)
            {
                CloneExtractStart(this, new EventArgs());
            }
        }

        /// <summary>
        /// Уведомить о прогрессе извлечения клон-фрагментов
        /// </summary>
        public virtual void OnCloneExtractProgress()
        {
            if (CloneExtractProgress != null)
            {
                CloneExtractProgress(this, new EventArgs());
            }
        }

        /// <summary>
        /// Уведомить о завершении извлечения клон-фрагментов
        /// </summary>
        public virtual void OnCloneExtractEnd()
        {
            if (CloneExtractEnd != null)
            {
                CloneExtractEnd(this, new EventArgs());
            }
        }

        #endregion

        #endregion
    }
}
