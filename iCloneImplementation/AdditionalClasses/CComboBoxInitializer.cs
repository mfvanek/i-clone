using System;
using System.Collections;
using System.Windows.Forms;

namespace IClone.ICloneImplementation.AdditionalClasses
{
    /// <summary>
    /// Вспомогательный класс для заполнения ComboBox'ов
    /// </summary>
    public class CComboBoxInitializer<T> where T : class
    {
        /// <summary>
        /// Подготовить ComboBox
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="DropDownClosed_EventHandler"></param>
        private void InitComboBox(ref ComboBox combobox, EventHandler DropDownClosed_EventHandler)
        {
            combobox.Items.Clear();
            combobox.DropDownStyle = ComboBoxStyle.DropDownList;
            combobox.DropDownClosed += DropDownClosed_EventHandler;
        }

        /// <summary>
        /// Построить список доступных элементов
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="Values"></param>
        private void FillComboBox(ref ComboBox combobox, IEnumerable Values)
        {
            foreach (T value in Values)
            {
                combobox.Items.Add(value);
            }
        }

        /// <summary>
        /// Установить значение по умолчанию
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="DropDownClosed_EventHandler"></param>
        /// <param name="DefaultValue"></param>
        private void SetDefaultItem(ref ComboBox combobox, EventHandler DropDownClosed_EventHandler, T DefaultValue)
        {
            if (combobox.Items.Count > 0 && DefaultValue != null)
            {
                object DefaultItem = null;
                foreach (object obj in combobox.Items)
                {
                    if (DefaultValue.Equals(obj))
                    {
                        DefaultItem = obj;
                        break;
                    }
                }

                if (DefaultItem != null)
                {
                    combobox.SelectedItem = DefaultItem;
                    DropDownClosed_EventHandler(combobox, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Конструктор, выполняющий инициализацию ComboBox'а
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="DropDownClosed_EventHandler"></param>
        /// <param name="Values"></param>
        /// <param name="DefaultValue"></param>
        public CComboBoxInitializer(ref ComboBox combobox, EventHandler DropDownClosed_EventHandler, IEnumerable Values, T DefaultValue)
        {
            InitComboBox(ref combobox, DropDownClosed_EventHandler);
            FillComboBox(ref combobox, Values);
            SetDefaultItem(ref combobox, DropDownClosed_EventHandler, DefaultValue);
        }
    }
}