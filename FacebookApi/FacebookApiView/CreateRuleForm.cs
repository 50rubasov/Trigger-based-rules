using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookApiModel;

namespace FacebookApiView
{
    public partial class CreateRuleForm : Form
    {
        private int HeightGroupBox = 81;
        
        public RulesCreator rc;
        private string _acc;
        private string _entityType;
        private string _conditionOperator;
        private string _conditionField;
        private string _action;
        

        public CreateRuleForm (string acc, RequestExecutor re)
        {
            InitializeComponent();
            _acc = acc;
            rc = new RulesCreator(re);
            _entityType = "ADSET";
            _conditionOperator = "GREATER_THAN";
            _conditionField = "cost_per";
            _action = "PAUSE";
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            rc.UploadAsync(_acc, NameTextBox.Text, ConditionValueTextBox.Text, _entityType, _conditionOperator, _conditionField, _action);
            MessageBox.Show("Правило успешно добавлено!");
        }

        private void AddCondition_Click(object sender, EventArgs e)
        {
            int i = 1;
            
            ComboBox field = new ComboBox();
            ComboBox op = new ComboBox();
            NumericUpDown value = new NumericUpDown();

            field.Name = "ConditionFieldComboBox" + i;
            op.Name = "ConditionOperatorComboBox" + i;
            value.Name = "ConditionValueComboBox" + i;

            string[] fields = { "Цена за результат", "Результаты", "Расходы" };
            string[] ops = { "Больше, чем", "Меньше, чем", "Равно" };

            field.Items.AddRange(fields);
            op.Items.AddRange(ops);

            field.Size = new Size(158, 24); // размер
            field.Text = "Цена за результат"; //текст
            op.Size = new Size(106, 24); // размер
            op.Text = "Больше, чем"; //текст
            value.Size = new Size(75, 22); // размер

            TableLayoutPanel.RowCount = ++TableLayoutPanel.RowCount;
            TableLayoutPanel.Size = new Size(361, TableLayoutPanel.Size.Height + 30);
            TableLayoutPanel.RowStyles.Add(new RowStyle());
            TableLayoutPanel.Controls.Add(field, 0, TableLayoutPanel.RowCount -1 );
            TableLayoutPanel.Controls.Add(op, 1, TableLayoutPanel.RowCount -1);
            TableLayoutPanel.Controls.Add(value, 2, TableLayoutPanel.RowCount -1 );

            this.Height += 30;
            i++;
        }

      
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EntityTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(EntityTypeComboBox.Text == "Группа объявлений")
            {
                _entityType = "ADSET";
            }
            if (EntityTypeComboBox.Text == "Компания")
            {
                _entityType = "CAMPAIGN";
            }
            if (EntityTypeComboBox.Text == "Объявление")
            {
                _entityType = "AD";
            }
        }

        private void ConditionOperatorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConditionOperatorComboBox.Text == "Больше, чем")
            {
                _conditionOperator = "GREATER_THAN";
            }
            if (ConditionOperatorComboBox.Text == "Меньше, чем")
            {
                _conditionOperator = "LESS_THAN";
            }
            if (ConditionOperatorComboBox.Text == "Равно")
            {
                _conditionOperator = "EQUAL";
            }
        }

        private void ConditionFieldComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConditionFieldComboBox.Text == "Расходы")
            {
                _conditionField = "spent";
            }
            if (ConditionFieldComboBox.Text == "Результаты")
            {
                _conditionField = "results";
            }
            if (ConditionFieldComboBox.Text == "Цена за результат")
            {
                _conditionField = "cost_per";
            }
        }

        private void ActionOffRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _action = "PAUSE";
        }

        private void ActionOnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _action = "UNPAUSE";
        }

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
                TableLayoutPanel.Size = new Size(361, TableLayoutPanel.Size.Height - 30);
                this.Height -= 30;
            }            
        }
    }
}
