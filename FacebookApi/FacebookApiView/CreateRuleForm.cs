using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FacebookApiModel;


namespace FacebookApiView
{
    // TODO: бизнес-логика плотно перемешана с прямым управлением контролов - по-хорошему надо разделить
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

            // TODO: аналогичные имена есть в FiltersResult. Нельзя их инициализизовать в одном месте и использовать в форме?
            names.Add("Цена за результат", "cost_per");
            names.Add("Результаты", "results");
            names.Add("Расходы", "spent");
            names.Add("Цена за установку", "cost_per_mobile_app_install");
            names.Add("Показы", "impressions"); 
            names.Add("Охват", "reach");
            names.Add("Клики по ссылке", "link_click");
            names.Add("CPM", "cpm");
            names.Add("CPC", "cpc");
            names.Add("CTR", "ctr");
            names.Add(">", "GREATER_THAN");
            names.Add("<", "LESS_THAN");
            names.Add("=", "EQUAL");
            names.Add("Группа объявлений", "ADSET");
            names.Add("Компания", "CAMPAIGN");
            names.Add("Объявление", "AD");
            names.Add("Весь срок действия", "LIFETIME");
            names.Add("Сегодня", "TODAY");
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
            // TODO: лучше инвартировать условие, чтобы не городить такую вложенность
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
                    values = (TableLayoutPanel.Controls["ConditionValueTextBox" + i.ToString()] as NumericUpDown).Text;

            
                    if ((fields == "Цена за результат") || (fields == "Расходы") || (fields == "Цена за установку"))
                    {                      
                            string clearv = values.Replace(",", "");
                            values = clearv.Replace(".", "");
                   
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

                try
                {
                    rc.UploadAsync(_acc, trigger, NameTextBox.Text, _entityType, filters, names[TimeRangeComboBox.Text]);
                }
                catch(Exception ex)
                {
                    // TODO: обработка базового класса исключений - плохо. Вообще, можно было бы в окне выводить какую-то информацию из самого исключения, чтобы как-то конкретезировать для пользователя тип ошибки
                    MessageBox.Show("Ошибка отправки запроса создания нового правила", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // TODO: такие месседжбоксы бесят. Если выполнение операции не занимает минуты, то показывать месседжбокс смысла нет - пользователь и так сразу видит успешный результат
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
            if (TableLayoutPanel.RowCount < 6)
            {
                FiltersLabel.Visible = true;
                textboxcounter++;
                ComboBox field = new ComboBox();
                ComboBox op = new ComboBox();
                NumericUpDown value = new NumericUpDown();

                field.Name = "ConditionFieldComboBox" + textboxcounter;
                op.Name = "ConditionOperatorComboBox" + textboxcounter;
                value.Name = "ConditionValueTextBox" + textboxcounter;

                string[] fields = { "Цена за результат", "Результаты", "Расходы", "Цена за установку", "Показы", "Охват" , "Клики по ссылке", "CPM", "CPC", "CTR" };
                string[] ops = { ">", "<", "=" };

                field.Items.AddRange(fields);
                op.Items.AddRange(ops);

                // TODO: магические числа в именованные константы
                field.Size = new Size(151, 24);
                field.DropDownStyle = ComboBoxStyle.DropDownList;
                op.Size = new Size(37, 24);
                op.DropDownStyle = ComboBoxStyle.DropDownList;
                value.Size = new Size(69, 22);
                value.Text = "0";

                field.Leave += ConditionFieldComboBox1_Leave;                
                TableLayoutPanel.RowCount = ++TableLayoutPanel.RowCount;
                //Для корректного отображения добавления фильтров
                if (textboxcounter == 2)
                {
                    TableLayoutPanel.Size = new Size(282, TableLayoutPanel.Size.Height + 50);
                    this.Height += 50;
                }
                else
                {
                    TableLayoutPanel.Size = new Size(282, TableLayoutPanel.Size.Height + 30);
                    this.Height += 30;
                }
                TableLayoutPanel.RowStyles.Add(new RowStyle());
                TableLayoutPanel.Controls.Add(field, 0, TableLayoutPanel.RowCount - 1);
                TableLayoutPanel.Controls.Add(op, 1, TableLayoutPanel.RowCount - 1);
                TableLayoutPanel.Controls.Add(value, 2, TableLayoutPanel.RowCount - 1);               
            }
            else
            {
                MessageBox.Show("Количество дополнительных правил должно быть меньше 4-х", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (row > 1)
            {
                for (int i = 0; i < TableLayoutPanel.ColumnCount; i++)
                {
                    Control c = TableLayoutPanel.GetControlFromPosition(i, row);
                    TableLayoutPanel.Controls.Remove(c);
                    c.Dispose();
                }

                TableLayoutPanel.RowStyles.RemoveAt(row);
                TableLayoutPanel.RowCount--;
                //Для корректного отображения добавления фильтров
                if (textboxcounter == 2)
                {
                    TableLayoutPanel.Size = new Size(282, TableLayoutPanel.Size.Height - 50);
                    this.Height -= 50;
                }
                else
                {
                    TableLayoutPanel.Size = new Size(282, TableLayoutPanel.Size.Height - 30);
                    this.Height -= 30;
                }

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
            if(EntityTypeComboBox.Text != "")
            _entityType = names[EntityTypeComboBox.Text];
            else MessageBox.Show("Имя не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
       
        /// <summary>
        /// Событие, изменяющее формат ввода значения при измененении условия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionFieldComboBox1_Leave(object sender, EventArgs e)
        {
            var textbox = sender as ComboBox;

            string counter = textbox.Name.Substring(textbox.Name.Length - 1);
            var valuetextbox = TableLayoutPanel.Controls["ConditionValueTextBox" + counter.ToString()] as NumericUpDown;
            // если введеное число может содержать , или .
            if ((textbox.Text == "Цена за результат") || (textbox.Text == "Цена за установку") || (textbox.Text == "Расходы"))
            {
                valuetextbox.DecimalPlaces = 2;
            }
            else
            {
                valuetextbox.DecimalPlaces = 0;
            }
        }
    }
}
