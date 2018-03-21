using System.Collections.Generic;
using System.Windows.Forms;
using IClone.ICloneImplementation.GUIControls;

namespace IClone.ICloneImplementation.AdditionalClasses
{
    /// <summary>
    /// Вспомогательный класс для управления параметрами приложения
    /// </summary>
    public static class CICloneOptionsManager
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        static CICloneOptionsManager()
        {
        }

        /// <summary>
        /// Создать список объектов для отображения параметров программы
        /// </summary>
        /// <param name="CallBack"></param>
        /// <returns></returns>
        private static List<CICloneOptionsGroup> CreateOptionsObjectsList(DelegateLanguageChange CallBack)
        {
            List<CICloneOptionsGroup> OptionsObjectsList = new List<CICloneOptionsGroup>();
            OptionsObjectsList.Add(new CCommonOptionsGroup(CallBack));
            OptionsObjectsList.Add(new CCloneSearchOptionsGroup());
            OptionsObjectsList.Add(new CDetectCodeSizeOptionsGroup());
            OptionsObjectsList.Add(new CDeleteSVNSubFoldersOptionsGroup());

            return OptionsObjectsList;
        }

        /// <summary>
        /// Создать дочерние узлы
        /// </summary>
        /// <param name="ParentTreeNode"></param>
        /// <param name="CallBack"></param>
        private static void CreateSubNodes(TreeNode ParentTreeNode, DelegateLanguageChange CallBack)
        {
            ParentTreeNode.Nodes.Clear();

            List<CICloneOptionsGroup> OptionsObjectsList = CreateOptionsObjectsList(CallBack);
            foreach (CICloneOptionsGroup OptionsObject in OptionsObjectsList)
            {
                TreeNode SubNode = new TreeNode();

                SubNode.Name = OptionsObject.GetName();
                SubNode.Text = OptionsObject.GetName();
                SubNode.Tag = OptionsObject;

                ParentTreeNode.Nodes.Add(SubNode);
            }
        }

        /// <summary>
        /// Создать набор узлов для дерева параметров программы
        /// </summary>
        /// <param name="CallBack">Функция обратного вызова для обработки смены языка интерфейса</param>
        /// <returns></returns>
        public static System.Windows.Forms.TreeNode[] CreateOptionsTreeNodes(DelegateLanguageChange CallBack)
        {
            List<TreeNode> OptionsTreeNodes = new List<TreeNode>();

            TreeNode ParentTreeNode = new TreeNode();
            CParentOptionsNode ParentTreeNodeObj = new CParentOptionsNode();
            ParentTreeNode.Name = ICloneLocalization.ICOP_OptionsTreeHeader;
            ParentTreeNode.Text = ICloneLocalization.ICOP_OptionsTreeHeaderTag;
            ParentTreeNode.Tag = ParentTreeNodeObj;

            CreateSubNodes(ParentTreeNode, CallBack);
            OptionsTreeNodes.Add(ParentTreeNode);

            return OptionsTreeNodes.ToArray();
        }
    }
}
