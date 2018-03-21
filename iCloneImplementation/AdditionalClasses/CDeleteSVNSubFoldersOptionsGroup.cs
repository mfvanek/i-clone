
namespace IClone.ICloneImplementation.AdditionalClasses
{
    public class CDeleteSVNSubFoldersOptionsGroup : CICloneOptionsGroup
    {
        public CDeleteSVNSubFoldersOptionsGroup()
        {
        }

        /// <summary>
        /// Получить название группы параметров
        /// </summary>
        /// <returns></returns>
        public override string GetName()
        {
            return ICloneLocalization.MW_DSSFMenuItemName;
        }
    }
}
