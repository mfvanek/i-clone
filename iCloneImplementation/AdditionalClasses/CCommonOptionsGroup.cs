using IClone.ICloneImplementation.GUIControls;

namespace IClone.ICloneImplementation.AdditionalClasses
{
    public class CCommonOptionsGroup : CICloneOptionsGroup
    {
        public CCommonOptionsGroup()
            : base(new CCommonOptionsUserControl())
        {
        }

        public CCommonOptionsGroup(DelegateLanguageChange CallBack)
            : base(new CCommonOptionsUserControl(CallBack))
        {
        }

        /// <summary>
        /// Получить название группы параметров
        /// </summary>
        /// <returns></returns>
        public override string GetName()
        {
            return ICloneLocalization.ICOP_ICloneCommonOptionsName;
        }
    }
}
