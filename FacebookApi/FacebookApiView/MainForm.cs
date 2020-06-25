using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookApiModel;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace FacebookApiView
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Создание объекта класса для отправки запросов к API
        /// </summary>
        private static RequestExecutor _ReqEx;
        /// <summary>
        /// Создание объекта класса для работы с API.
        /// </summary>
        private Navigator Navigator = new Navigator(_ReqEx);
        /// <summary>
        /// Токен доступа.
        /// </summary>
        public static string _token;
        /// <summary>
        /// Список БМов.
        /// </summary>
        public List<JToken> Bms;
        /// <summary>
        /// Список рекламных акаунтов.
        /// </summary>
        public List<JToken> Acs;
        /// <summary>
        /// ID РК.
        /// </summary>
        public string _accId;
        /// <summary>
        /// Строковые значения объектов JSON.
        /// </summary>
        private StringDictionary _strDict = new StringDictionary();
        /// <summary>
        /// Конструктор класса MainForm.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            CreateRuleButton.Enabled = false;
            DeleteRuleButton.Enabled = false;
        }

        /// <summary>
        /// Метод, проверяющий, выбран ли аккаунт
        /// </summary>
        /// <returns>выбран/не выбран</returns>
        private bool IsAccountSelected()
        {
            if (TokenTextBox.Text == "" || BmComboBox.SelectedItem == null || RkComboBox.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать аккаунт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);              
                return false;       
            }
            else
            {                
                return true;
            }
        }
        /// <summary>
        /// Событие создания нового правила.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private void CreateRuleButtonClick(object sender, EventArgs e)
        {
            if(IsAccountSelected())
            {
                CreateRuleForm createRule = new CreateRuleForm(_accId, _ReqEx);
                createRule.ShowDialog();
                // обновление окна датагрида
                ShowRules(_accId);
            }
        }
        /// <summary>
        /// Событие удаления правила
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private async void DeleteRuleButtonClick(object sender, EventArgs e)
        {
            if (IsAccountSelected())
            {
                if(DataGridView.Rows.Count > 0)
                {
                    Navigator.DeleteRules(_accId, DataGridView.SelectedCells[0].Value.ToString());
                    DataGridView.Rows.RemoveAt(DataGridView.SelectedCells[0].RowIndex);
                }
                
                else
                {
                    MessageBox.Show("Невозможно удалить правило", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }           
        }

        /// <summary>
        /// Событие, возникающее при вводе токена.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private async void TokenTextBoxLeave(object sender, EventArgs e)
        {
            try
            {
                string _apiAddress = "https://graph.facebook.com/v6.0/";
                BmComboBox.Items.Clear();
                RkComboBox.Items.Clear();
                DataGridView.Rows.Clear();
                if (TokenTextBox.Text != "")
                {
                    _token = TokenTextBox.Text;
                    _ReqEx = GetConfiguredRequestExecutor(_apiAddress);
                    Navigator = new Navigator(_ReqEx);

                    Bms = await Navigator.GetAllBmsAsync();
                    for (int i = 0; i < Bms.Count; i++)
                    {
                        var bm = Bms[i];
                        BmComboBox.Items.Add(bm[_strDict.Name]);
                    }
                  
                }
                else
                {
                    CreateRuleButton.Enabled = false;
                    DeleteRuleButton.Enabled = false;
                    MessageBox.Show("Поле токена не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 
                }
                
            }
            catch (Exception ex)
            {
                TokenTextBox.Select();
                MessageBox.Show("Неверный токен доступа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Формирование запроса к Facebook API
        /// </summary>
        /// <param name="apiAddress">Facebook Api</param>
        /// <returns>Запрос</returns>
        private static RequestExecutor GetConfiguredRequestExecutor(string apiAddress)
        {
            return new RequestExecutor(apiAddress, _token);
        }

        /// <summary>
        /// Событие возникающее при выборе БМа
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private async void BmComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
                DataGridView.Rows.Clear();
                RkComboBox.Items.Clear();
                RkComboBox.Text = null;
                string _bmId = Bms[BmComboBox.SelectedIndex][_strDict.Id].ToString();
                Acs = await Navigator.GetBmsAdAccountsAsync(_bmId, true);

                for (int i = 0; i < Acs.Count; i++)
                {
                    var acc = Acs[i];
                    RkComboBox.Items.Add(acc[_strDict.Name]);
                }
            
        }
        /// <summary>
        /// Событие возникающее при выборе РК
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private void RkComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {         
                DataGridView.Rows.Clear();
                _accId = Acs[RkComboBox.SelectedIndex][_strDict.Id].ToString().TrimStart(new[] { 'a', 'c', 't', '_' });
                CreateRuleButton.Enabled = true;
                DeleteRuleButton.Enabled = true;
                ShowRules(_accId);       
        }

        /// <summary>
        /// Добавление правил в таблицу DataGrid.
        /// </summary>
        /// <param name="acc">id рк.</param>
        public async void ShowRules(string acc)
        {
            DataGridView.Rows.Clear();
            
            var request = new RestRequest($"act_{acc}/{_strDict.AdrulesLibrary}", Method.GET);
            request.AddQueryParameter(_strDict.Fields, _strDict.EntityType + "," + _strDict.EvaluationSpec + ","
                + _strDict.ExecutionSpec + "," + _strDict.Name + ","
                + _strDict.ScheduleSpec + "," + _strDict.ExecutionType);
            var json = await _ReqEx.ExecuteRequestAsync(request);            
            
            foreach (var rule in json[_strDict.Data])
            {
                
                JToken filter = rule[_strDict.EvaluationSpec][_strDict.Filters];
                JToken trigger = rule[_strDict.EvaluationSpec][_strDict.Trigger];
                var result = Navigator.TranslateRules(trigger, filter); 
                
                DataGridView.Rows.Add(rule[_strDict.Name], result.Item1, result.Item2, result.Item3, result.Item4);
           
            }

        }

    }
}
