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
        public CreateRuleForm(string acc, RequestExecutor re)
        {
            InitializeComponent();
            _acc = acc;
            rc = new RulesCreator(re);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            rc.UploadAsync(_acc, NameTextBox.Text, ConditionValueTextBox.Text);
            MessageBox.Show("Правило успешно добавлено!");
        }

        private void AddCondition_Click(object sender, EventArgs e)
        {
            
            
            //ConditionGroupBox.Size = new Size(363, HeightGroupBox=HeightGroupBox+20);
            //var textBox = new TextBox();
            //textBox.Top = ConditionFieldComboBox.Top-100;
            //textBox.Left = ConditionFieldComboBox.Left;
            //Controls.Add(textBox);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
