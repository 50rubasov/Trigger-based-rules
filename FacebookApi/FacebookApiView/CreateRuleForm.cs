using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FacebookApiModel;
using System.Diagnostics;
using System.Text;
using System.IO;
namespace FacebookApiView
{
    public partial class CreateRuleForm : Form
    {
        private const int _tableWidth = 282;
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
        private readonly Dictionary<string, string> Names = new Dictionary<string, string>();
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="acc">аккаунт</param>
        /// <param name="re">запрос</param>
        public CreateRuleForm(string acc, RequestExecutor re)
        {
            InitializeComponent();

            Names.Add("Цена за результат", "cost_per");
            Names.Add("Результаты", "results");
            Names.Add("Расходы", "spent");
            Names.Add("Цена за установку", "cost_per_mobile_app_install");
            Names.Add("Показы", "impressions"); 
            Names.Add("Охват", "reach");
            Names.Add("Клики по ссылке", "link_click");
            Names.Add("CPM", "cpm");
            Names.Add("CPC", "cpc");
            Names.Add("CTR", "ctr");
            Names.Add(">", "GREATER_THAN");
            Names.Add("<", "LESS_THAN");
            Names.Add("=", "EQUAL");
            Names.Add("Группа объявлений", "ADSET");
            Names.Add("Компания", "CAMPAIGN");
            Names.Add("Объявление", "AD");
            Names.Add("Весь срок действия", "LIFETIME");
            Names.Add("Сегодня", "TODAY");
            _acc = acc;
            rc = new RulesCreator(re);
            _action = "PAUSE";
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку создать.
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private async void CreateButtonClick(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "" || EntityTypeComboBox.SelectedItem == null || ConditionFieldComboBox1.SelectedItem == null || ConditionValueTextBox1.Text == "")
            {
                MessageBox.Show("Заполнены не все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
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
                        + "\"field\": \"" + Names[fields] + "\","
                        + "\"value\": \"" + values + "\","
                        + "\"operator\" : \"" + Names[operators] + "\"},";
                    }
                    //Если поле - триггер
                    else
                    {
                        trigger = "{\"evaluation_type\":\"TRIGGER\","
                                                         + "\"trigger\" : {"
                                                         + "\"type\" : \"STATS_CHANGE\","
                                                         + "\"field\": \"" + Names[fields] + "\","
                                                         + "\"value\": \"" + values + "\","
                                                         + "\"operator\": \"" + Names[operators] + "\"},";
                    }
                }

                try
                {
                    await rc.UploadAsync(_acc, trigger, NameTextBox.Text, _entityType, filters, Names[TimeRangeComboBox.Text]);
                    MessageBox.Show("Правило добавлено! Можете добавить еще");
                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                NameTextBox.Text = "";
            }
        }


        /// <summary>
        /// Обработчик события добавления нового условия.
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private void AddConditionClick(object sender, EventArgs e)
        {
            const int fieldWidth = 151;
            const int controlsHeight = 24;
            const int opWidth = 37;
            const int valueWidth = 69;
            
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
                
                field.Size = new Size(fieldWidth, controlsHeight);
                field.DropDownStyle = ComboBoxStyle.DropDownList;
                op.Size = new Size(opWidth, controlsHeight);
                op.DropDownStyle = ComboBoxStyle.DropDownList;
                value.Size = new Size(valueWidth, controlsHeight);
                value.Text = "0";

                field.Leave += ConditionFieldComboBoxLeave;                
                TableLayoutPanel.RowCount = ++TableLayoutPanel.RowCount;
                //Для корректного отображения добавления фильтров
                if (textboxcounter == 2)
                {
                    TableLayoutPanel.Size = new Size(_tableWidth, TableLayoutPanel.Size.Height + 50);
                    this.Height += 50;
                }
                else
                {
                    TableLayoutPanel.Size = new Size(_tableWidth, TableLayoutPanel.Size.Height + 30);
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
        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик удаления дополнительного условия
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">действие</param>
        private void DeleteConditionClick(object sender, EventArgs e)
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
                    TableLayoutPanel.Size = new Size(_tableWidth, TableLayoutPanel.Size.Height - 50);
                    this.Height -= 50;
                }
                else
                {
                    TableLayoutPanel.Size = new Size(_tableWidth, TableLayoutPanel.Size.Height - 30);
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
        private void EntityTypeComboBoxLeave(object sender, EventArgs e)
        {
            if(EntityTypeComboBox.Text != "")
            _entityType = Names[EntityTypeComboBox.Text];
            else MessageBox.Show("Имя не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
       
        /// <summary>
        /// Событие, изменяющее формат ввода значения при измененении условия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionFieldComboBoxLeave(object sender, EventArgs e)
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
