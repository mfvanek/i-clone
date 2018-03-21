using IClone.ICloneImplementation.GUIControls;

namespace IClone.ICloneImplementation.AdditionalClasses
{
    /// <summary>
    /// Оценка объёма кода - параметры процедуры
    /// </summary>
    public class CDetectCodeSizeOptionsGroup : CICloneOptionsGroup
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CDetectCodeSizeOptionsGroup()
            : base(new CDetectCodeSizeOptionsUserControl())
        {
        }

        /// <summary>
        /// Получить название группы параметров
        /// </summary>
        /// <returns></returns>
        public override string GetName()
        {
            return ICloneLocalization.MW_DCSMenuItemName;
        }
    }
}
