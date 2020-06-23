using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookApiModel;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace FacebookApiView
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Подключение к Facebook Api.
        /// </summary>
        private const string _apiAddress = "https://graph.facebook.com/v6.0/";
        /// <summary>
        /// Создание объекта класса для отправки запросов к API
        /// </summary>
        private static RequestExecutor _ReqEx;
        /// <summary>
        /// Создание объекта класса для создания новых правил. 
        /// </summary>
        public RulesCreator RulesCreator = new RulesCreator(_ReqEx);
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
        /// ID БМа
        /// </summary>
        private string _bmId;
        /// <summary>
        /// ID РК.
        /// </summary>
        public string _accId;

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
                var request = new RestRequest($"act_{_accId}/adrules_library", Method.GET);
                request.AddQueryParameter("fields", "name");
                var json = await _ReqEx.ExecuteRequestAsync(request);
                string selected = DataGridView.SelectedCells[0].Value.ToString();
                foreach (var rule in json["data"])
                {
                    string name = rule["name"].ToString();
                    if (name == selected)
                    {
                        request = new RestRequest($"{rule["id"]}", Method.DELETE);
                        var js = await _ReqEx.ExecuteRequestAsync(request);

                        DataGridView.Rows.RemoveAt(DataGridView.SelectedCells[0].RowIndex);
                    }
                }
                ShowRules(_accId);
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
                        BmComboBox.Items.Add($"{bm["name"]}");
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
                _bmId = Bms[BmComboBox.SelectedIndex]["id"].ToString();
                Acs = await Navigator.GetBmsAdAccountsAsync(_bmId, true);

                for (int i = 0; i < Acs.Count; i++)
                {
                    var acc = Acs[i];
                    RkComboBox.Items.Add($"{acc["name"]}");
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
                _accId = Acs[RkComboBox.SelectedIndex]["id"].ToString().TrimStart(new[] { 'a', 'c', 't', '_' });
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
            
            var request = new RestRequest($"act_{acc}/adrules_library", Method.GET);
            request.AddQueryParameter("fields", "entity_type,evaluation_spec,execution_spec,name,schedule_spec,execution_type");
            var json = await _ReqEx.ExecuteRequestAsync(request);
            string timepresent = null;
            string entitytype = null;
            
            foreach (var rule in json["data"])
            {
                string filtercondition = null;
                string triggerResult = null;
                bool errorfilter = false;
                var filter = rule["evaluation_spec"]["filters"];
                var trigger = rule["evaluation_spec"]["trigger"];

                List<JToken> filters = filter.Children().ToList();
                //если правило не на основе триггеров
                if (trigger != null)
                    {
                    triggerResult = trigger.ToObject<FiltersResult>().ToString();
                    }

                List<FiltersResult> filtersResults = new List<FiltersResult>();
                foreach (JToken result in filters)
                {
                    try
                    {
                        FiltersResult filtersResult = result.ToObject<FiltersResult>();
                        filtersResults.Add(filtersResult);
                    }
                    catch 
                    {                     
                        
                        filtercondition += "(Нечитаемый фильтр)"+result;
                        errorfilter = true;
                    }

                }
                
                foreach (var s in filtersResults)
                {
                    if (s.Field == "entity_type")
                    {
                        entitytype = s.ToString();
                    }
                    if (s.Field == "time_preset")
                    {
                        timepresent = s.ToString();
                    }
                    if ((s.Field == "attribution_window" || s.Field == "entity_type" || s.Field == "time_preset") == false)
                    {
                        filtercondition = filtercondition + s;
                    }
                }
                DataGridView.Rows.Add(rule["name"], entitytype, timepresent, triggerResult, filtercondition);
           
            }

        }

    }
}
