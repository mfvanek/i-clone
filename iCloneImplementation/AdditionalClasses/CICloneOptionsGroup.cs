using IClone.ICloneImplementation.GUIControls;

namespace IClone.ICloneImplementation.AdditionalClasses
{
    /// <summary>
    /// Абстрактный базовый класс для представления группы параметров
    /// </summary>
    public abstract class CICloneOptionsGroup
    {
        /// <summary>
        /// UI Control для отображения
        /// </summary>
        protected CICloneBaseUIControl m_BaseUIControl;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CICloneOptionsGroup()
        {
            m_BaseUIControl = new CICloneBaseUIControl();
        }

        public CICloneOptionsGroup(CICloneBaseUIControl UIControl)
        {
            m_BaseUIControl = UIControl;
        }

        /// <summary>
        /// Получить название группы параметров
        /// </summary>
        /// <returns></returns>
        public abstract string GetName();

        /// <summary>
        /// Показать группу параметров
        /// </summary>
        /// <param name="ParentPanel">Родительская панель, на которой будут размещаться элементы</param>
        public virtual void ShowOptions(System.Windows.Forms.Panel ParentPanel)
        {
            ParentPanel.Controls.Clear();
            ParentPanel.Controls.Add(m_BaseUIControl);
        }

        /// <summary>
        /// Сохранить параметры
        /// </summary>
        public virtual void SaveSettings()
        {
            m_BaseUIControl.SaveSettings();
        }

        /// <summary>
        /// Обновить пользовательский интерфейс
        /// </summary>
        /// <remarks>
        /// Вызывается после смены языка интерфейса
        /// </remarks>
        public virtual void UpdateUI()
        {
            m_BaseUIControl.InitUI();
        }

        /// <summary>
        /// Переопределяем метод для отображения на форме
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetName();
        }
    }
}
