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
        private const string apiAddress = "https://graph.facebook.com/v6.0/";
        /// <summary>
        /// Создание объекта класса для отправки запросов к API
        /// </summary>
        private static RequestExecutor re;
        /// <summary>
        /// Создание объекта класса для создания новых правил. 
        /// </summary>
        public RulesCreator rc = new RulesCreator(re);
        /// <summary>
        /// Создание объекта класса для работы с API.
        /// </summary>
        private Navigator navigator = new Navigator(re);
        /// <summary>
        /// Токен доступа.
        /// </summary>
        public static string token;
        /// <summary>
        /// Список БМов.
        /// </summary>
        public List<JToken> bms;
        /// <summary>
        /// Список рекламных акаунтов.
        /// </summary>
        public List<JToken> acs;
        /// <summary>
        /// ID БМа
        /// </summary>
        private string bmid;
        /// <summary>
        /// ID РК.
        /// </summary>
        public string accid;

        /// <summary>
        /// Конструктор класса MainForm.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Событие создания нового правила.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private void CreateRuleButton_Click(object sender, EventArgs e)
        {
            CreateRuleForm createRule = new CreateRuleForm(accid, re);

            createRule.ShowDialog();
            // обновление окна датагрида
            GetRules(accid);
        }
        /// <summary>
        /// Событие удаления правила
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            
            var request = new RestRequest($"act_{accid}/adrules_library", Method.GET);
            request.AddQueryParameter("fields", "name");
            var json = await re.ExecuteRequestAsync(request);
            foreach (var rule in json["data"])
            {
                string selected = DataGridView.SelectedCells[0].Value.ToString();
                string name = rule["name"].ToString();
                if (name == selected)
                {
                    request = new RestRequest($"{rule["id"]}", Method.DELETE);
                    var js = await re.ExecuteRequestAsync(request);
                    
                    DataGridView.Rows.RemoveAt(DataGridView.SelectedCells[0].RowIndex);
                }
            }
            
            GetRules(accid); 
        }

        /// <summary>
        /// Событие, возникающее при вводе токена.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private async void TokenTextBox_Leave(object sender, EventArgs e)
        {
            token = TokenTextBox.Text;
            re = GetConfiguredRequestExecutor(apiAddress);
            navigator = new Navigator(re);
            
            bms = await navigator.GetAllBmsAsync();
            for (int i = 0; i < bms.Count; i++)
            {
                var bm = bms[i];
                BmComboBox.Items.Add($"{bm["name"]}");
            }

        }
        /// <summary>
        /// Формирование запроса к Facebook API
        /// </summary>
        /// <param name="apiAddress">Facebook Api</param>
        /// <returns>Запрос</returns>
        private static RequestExecutor GetConfiguredRequestExecutor(string apiAddress)
        {
            return new RequestExecutor(apiAddress, token);
        }

        /// <summary>
        /// Событие возникающее при выборе БМа
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private async void BmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridView.Rows.Clear();
            RkComboBox.Items.Clear();
            RkComboBox.Text = null;
            bmid =bms[BmComboBox.SelectedIndex]["id"].ToString();
            acs = await navigator.GetBmsAdAccountsAsync(bmid, true);

            for (int i = 0; i < acs.Count; i++)
            {
                var acc = acs[i];
                RkComboBox.Items.Add($"{acc["name"]}");
            }
        }
        /// <summary>
        /// Событие возникающее при выборе РК
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private void RkComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridView.Rows.Clear();
            accid =acs[RkComboBox.SelectedIndex]["id"].ToString().TrimStart(new[] { 'a', 'c', 't', '_' });
            GetRules(accid);
        }

        /// <summary>
        /// Добавление правил в таблицу DataGrid.
        /// </summary>
        /// <param name="acc">id рк.</param>
        public async void GetRules(string acc)
        {
            DataGridView.Rows.Clear();
            var request = new RestRequest($"act_{acc}/adrules_library", Method.GET);
            request.AddQueryParameter("fields", "entity_type,evaluation_spec,execution_spec,name,schedule_spec,execution_type");
            var json = await re.ExecuteRequestAsync(request);
            

            foreach (var rule in json["data"])
                {
                    var filter = rule["evaluation_spec"]["filters"];
                    var trigger = rule["evaluation_spec"]["trigger"];
                    // get JSON result objects into a list
                    List<JToken> filters = filter.Children().ToList();
                    List<JToken> triggers = trigger.Parent.ToList();
                    filters.AddRange(triggers);
                // serialize JSON results into .NET objects
                    List<FiltersResult> searchResults = new List<FiltersResult>();
                    foreach (JToken result in filters)
                    {
                        // JToken.ToObject is a helper method that uses JsonSerializer internally
                        if ((result.ToString().Contains("entity_type") == false)&&(result.ToString().Contains("time_preset") == false))
                        {
                            FiltersResult searchResult = result.ToObject<FiltersResult>();
                            searchResults.Add(searchResult);
                        }
                    }
                    string condition = null;
                    foreach (var s in searchResults)
                    {                       
                        condition = condition+s;
                    }
                    DataGridView.Rows.Add(rule["name"], rule["entity_type"], rule["execution_spec"]["execution_type"], condition);

                }

        }

    }
}
