
namespace IClone.ICloneImplementation.AdditionalClasses
{
    public class CParentOptionsNode : CICloneOptionsGroup
    {
        public CParentOptionsNode()
        {
        }

        /// <summary>
        /// Получить название группы параметров
        /// </summary>
        /// <returns></returns>
        public override string GetName()
        {
            return ICloneLocalization.ICOP_OptionsTreeHeaderTag;
        }
    }
}
