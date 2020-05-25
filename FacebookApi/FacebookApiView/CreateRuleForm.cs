using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FacebookApiModel;


namespace FacebookApiView
{
    public partial class CreateRuleForm : Form
    {
        /// <summary>
        /// Объект класса отправления запроса.
        /// </summary>
        public RulesCreator rc;
        /// <summary>
        /// id аккаунта.
        /// </summary>
        private string _acc;
        /// <summary>
        /// Область примененеия правила
        /// </summary>
        private string _entityType;
        /// <summary>
        /// Действие над правилом
        /// </summary>
        private string _action;
        /// <summary>
        /// Счетчик текстбоксов.
        /// </summary>
        private int textboxcounter = 1;
        /// <summary>
        /// Словарь для перевода русского текста в поля объекта
        /// </summary>
        Dictionary<string, string> names = new Dictionary<string, string>();
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="acc">аккаунт</param>
        /// <param name="re">запрос</param>
        public CreateRuleForm(string acc, RequestExecutor re)
        {
            InitializeComponent();

            names.Add("Цена за результат", "cost_per");
            names.Add("Результаты", "results");
            names.Add("Расходы", "spent");
            names.Add(">", "GREATER_THAN");
            names.Add("<", "LESS_THAN");
            names.Add("=", "EQUAL");
            names.Add("Группа объявлений", "ADSET");
            names.Add("Компания", "CAMPAIGN");
            names.Add("Объявление", "AD");

            _acc = acc;
            rc = new RulesCreator(re);
            _action = "PAUSE";
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку создать.
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (NameTextBox.Text != "" && EntityTypeComboBox.SelectedItem != null && ConditionFieldComboBox1.SelectedItem != null && ConditionValueTextBox1.Text != "")
            {
                string trigger = null;
                string filters = null;
                string fields;
                string operators;
                string values;
                for (int i = 1; i <= textboxcounter; i++)
                {
                    fields = (TableLayoutPanel.Controls["ConditionFieldComboBox" + i.ToString()] as ComboBox).Text;
                    operators = (TableLayoutPanel.Controls["ConditionOperatorComboBox" + i.ToString()] as ComboBox).Text;
                    values = (TableLayoutPanel.Controls["ConditionValueTextBox" + i.ToString()] as TextBox).Text;

                    // т.к фб читает цену 13$ как 1300$
                    if ((fields == "Цена за результат") || (fields == "Расходы"))
                    {
                        if ((values.Contains(',') == true) || (values.Contains('.')))
                        {
                            string clearv = values.Replace(",", "");
                            values = clearv.Replace(".", "");
                        }
                        else
                        {
                            int v = Int32.Parse(values) * 100;
                            values = v.ToString();
                        }
                    }
                    //Если поле - фильтр
                    if (i > 1)
                    {
                        filters = filters + "{"
                        + "\"field\": \"" + names[fields] + "\","
                        + "\"value\": \"" + values + "\","
                        + "\"operator\" : \"" + names[operators] + "\"},";
                    }
                    //Если поле - триггер
                    else
                    {
                        trigger = "{\"evaluation_type\":\"TRIGGER\","
                                                         + "\"trigger\" : {"
                                                         + "\"type\" : \"STATS_CHANGE\","
                                                         + "\"field\": \"" + names[fields] + "\","
                                                         + "\"value\": \"" + values + "\","
                                                         + "\"operator\": \"" + names[operators] + "\"},";
                    }
                }

                if (ActionOffRadioButton.Checked = true)
                {
                    _action = "PAUSE";
                }
                else if (ActionOnRadioButton.Checked == true)
                {
                    _action = "UNPAUSE";
                }

                rc.UploadAsync(_acc, trigger, NameTextBox.Text, _entityType, _action, filters);
                NameTextBox.Text = "";
                MessageBox.Show("Правило добавлено! Можете добавить еще");
            }
            else
            {
                MessageBox.Show("Заполнены не все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Обработчик события добавления нового условия.
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private void AddCondition_Click(object sender, EventArgs e)
        {
            if (TableLayoutPanel.RowCount < 5)
            {
                textboxcounter++;
                ComboBox field = new ComboBox();
                ComboBox op = new ComboBox();
                TextBox value = new TextBox();

                field.Name = "ConditionFieldComboBox" + textboxcounter;
                op.Name = "ConditionOperatorComboBox" + textboxcounter;
                value.Name = "ConditionValueTextBox" + textboxcounter;

                string[] fields = { "Цена за результат", "Результаты", "Расходы" };
                string[] ops = { ">", "<", "=" };

                field.Items.AddRange(fields);
                op.Items.AddRange(ops);

                field.Size = new Size(151, 24);
                field.DropDownStyle = ComboBoxStyle.DropDownList;
                op.Size = new Size(37, 24);
                op.DropDownStyle = ComboBoxStyle.DropDownList;
                value.Size = new Size(69, 22);
                value.Text = "0";

                value.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);

                TableLayoutPanel.RowCount = ++TableLayoutPanel.RowCount;
                TableLayoutPanel.Size = new Size(282, TableLayoutPanel.Size.Height + 30);
                TableLayoutPanel.RowStyles.Add(new RowStyle());
                TableLayoutPanel.Controls.Add(field, 0, TableLayoutPanel.RowCount - 1);
                TableLayoutPanel.Controls.Add(op, 1, TableLayoutPanel.RowCount - 1);
                TableLayoutPanel.Controls.Add(value, 2, TableLayoutPanel.RowCount - 1);
                this.Height += 30;
            }
            else
            {
                MessageBox.Show("Количество дополнительных правил должно быть меньше 5-ти", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик удаления дополнительного условия
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private void DeleteCondition_Click(object sender, EventArgs e)
        {
            int row = TableLayoutPanel.RowCount - 1;
            if (row > 0)
            {
                for (int i = 0; i < TableLayoutPanel.ColumnCount; i++)
                {
                    Control c = TableLayoutPanel.GetControlFromPosition(i, row);
                    TableLayoutPanel.Controls.Remove(c);
                    c.Dispose();
                }

                TableLayoutPanel.RowStyles.RemoveAt(row);
                TableLayoutPanel.RowCount--;
                TableLayoutPanel.Size = new Size(282, TableLayoutPanel.Size.Height - 30);
                this.Height -= 30;
                textboxcounter--;
            }
        }
        /// <summary>
        /// Обработчик изменения поля области действия.
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private void EntityTypeComboBox_Leave(object sender, EventArgs e)
        {
            _entityType = names[EntityTypeComboBox.Text];
        }
        /// <summary>
        /// Валидация ввода чисел.
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 46)
                e.Handled = true;
        }
    }
}
