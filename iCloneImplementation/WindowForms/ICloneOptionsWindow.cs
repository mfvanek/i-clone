using System;
using System.Windows.Forms;
using IClone.ICloneImplementation.AdditionalClasses;
using IClone.ICloneImplementation.GUIControls;

namespace IClone.ICloneImplementation.WindowForms
{
    public partial class ICloneOptionsWindow : Form, ICloneUILocalization
    {
        #region // Реализация интерфейса ICloneUILocalization

        /// <summary>
        /// Инициализация пользовательского интерфейса
        /// </summary>
        public void InitUI()
        {
            ICloneOpOKButton.Text = ICloneLocalization.ICOP_ICloneOpOKButtonText;
            ICloneOpCancelButton.Text = ICloneLocalization.ICOP_ICloneOpCancelButtonText;
            ICloneOpSaveButton.Text = ICloneLocalization.ICOP_ICloneOpSaveButtonText;
            CWindowNamer.SetWindowName(this, ICloneLocalization.ICOP_WindowHeader);
        }

        #endregion

        private void InitOptionsTreeView()
        {
            OptionsTreeView.Nodes.Clear();
            OptionsTreeView.Nodes.AddRange(CICloneOptionsManager.CreateOptionsTreeNodes(new DelegateLanguageChange(LanguageChange)));
            OptionsTreeView.ExpandAll();
        }

        public ICloneOptionsWindow()
        {
            InitializeComponent();
            InitUI();
            CurrentGroupNameLabel.Text = string.Empty;
            InitOptionsTreeView();
        }

        private void UpdateTreeOptions_Recurse(TreeNodeCollection Nodes)
        {
            foreach (TreeNode Node in Nodes)
            {
                UpdateTreeOptions_Recurse(Node.Nodes);
                CICloneOptionsGroup OptionsGroup = Node.Tag as CICloneOptionsGroup;
                OptionsGroup.UpdateUI();
                Node.Text = OptionsGroup.GetName();
            }
        }

        private void LanguageChange()
        {
            InitUI();
            UpdateTreeOptions_Recurse(OptionsTreeView.Nodes);
        }

        private void OptionsTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode CurrentNode = e.Node;

            if (CurrentNode.Tag != null)
            {
                try
                {
                    CurrentGroupNameLabel.Text = CurrentNode.Tag.ToString();
                    CICloneOptionsGroup OptionsGroup = CurrentNode.Tag as CICloneOptionsGroup;
                    OptionsGroup.ShowOptions(this.ICloneOpParentPanel);
                }
                catch
                {
                }
            }
            else
            {
                CurrentGroupNameLabel.Text = string.Empty;
            }
        }

        private void ICloneOpCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ICloneOpSaveButton_Click(object sender, EventArgs e)
        {
            SaveTreeOptions_Recurse(OptionsTreeView.Nodes);
        }

        private void ICloneOpOKButton_Click(object sender, EventArgs e)
        {
            ICloneOpSaveButton_Click(sender, e);
            ICloneOpCancelButton_Click(sender, e);
        }

        private void SaveTreeOptions_Recurse(TreeNodeCollection Nodes)
        {
            foreach (TreeNode Node in Nodes)
            {
                SaveTreeOptions_Recurse(Node.Nodes);
                CICloneOptionsGroup OptionsGroup = Node.Tag as CICloneOptionsGroup;
                OptionsGroup.SaveSettings();
            }
        }
    }
}
