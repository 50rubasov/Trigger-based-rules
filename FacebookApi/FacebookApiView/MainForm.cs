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
        // TODO: слишком много полей/логики в главном окне. Создать класс менеджера, в который перенести все поля и логику из обработчиков формы.
        // Форма должна создать экземпляр менеджера и потом только вызывать его методы
        /// <summary>
        /// Подключение к Facebook Api.
        /// </summary>
        // TODO: именование
        private const string apiAddress = "https://graph.facebook.com/v6.0/";
        /// <summary>
        /// Создание объекта класса для отправки запросов к API
        /// </summary>
        /// // TODO: именование
        /// // TODO: правильнее было бы сделать синглтон с поддержкой многопоточности
        private static RequestExecutor re;
        /// <summary>
        /// Создание объекта класса для создания новых правил. 
        /// </summary>
        /// // TODO: именование
        public RulesCreator rc = new RulesCreator(re);
        /// <summary>
        /// Создание объекта класса для работы с API.
        /// </summary>
        /// // TODO: именование
        private Navigator navigator = new Navigator(re);
        /// <summary>
        /// Токен доступа.
        /// </summary>
        // TODO: именование
        public static string token;
        /// <summary>
        /// Список БМов.
        /// </summary>
        // TODO: именование
        public List<JToken> bms;
        /// <summary>
        /// Список рекламных акаунтов.
        /// </summary>
        /// // TODO: именование
        public List<JToken> acs;
        /// <summary>
        /// ID БМа
        /// </summary>
        /// // TODO: именование
        private string bmid;
        /// <summary>
        /// ID РК.
        /// </summary>
        /// // TODO: именование
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
            // TODO: именование контролов
            // TODO: перемножение трёх условий лучше вынести в отдельный метод IsAccountSelected() - выше читаемость. Вывод месседжбокса можно туда же.
            if (TokenTextBox.Text != "" && BmComboBox.SelectedItem != null && RkComboBox.SelectedItem != null)
            {
                CreateRuleForm createRule = new CreateRuleForm(accid, re);

                createRule.ShowDialog();
                // обновление окна датагрида
                // TODO: метод называется GetRules, но ничего не возвращает. Именование
                GetRules(accid);
            }
            else
            {
                MessageBox.Show("Необходимо выбрать аккаунт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Событие удаления правила
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            // TODO: проверка условия повторяется - вынести в метод
            if (TokenTextBox.Text != "" && BmComboBox.SelectedItem != null && RkComboBox.SelectedItem != null)
            {
                // TODO: эту логику в отдельный менеджер
                var request = new RestRequest($"act_{accid}/adrules_library", Method.GET);
                request.AddQueryParameter("fields", "name");
                var json = await re.ExecuteRequestAsync(request);
                string selected = DataGridView.SelectedCells[0].Value.ToString();
                foreach (var rule in json["data"])
                {
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
            else
            {
                // TODO: было бы правильнее сделать, чтобы кнопки были заблокированы, если аккаунт не выбран
                MessageBox.Show("Необходимо выбрать аккаунт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, возникающее при вводе токена.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Действие</param>
        private async void TokenTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                // TODO: все в метод менеджера
                if (TokenTextBox.Text != "")
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
                else
                {
                    MessageBox.Show("Поле токена не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BmComboBox.Items.Clear();
                    RkComboBox.Items.Clear();
                    DataGridView.Rows.Clear();
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
        /// // TODO: в менеджер
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
                bmid = bms[BmComboBox.SelectedIndex]["id"].ToString();
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
                accid = acs[RkComboBox.SelectedIndex]["id"].ToString().TrimStart(new[] { 'a', 'c', 't', '_' });
                GetRules(accid);       
        }

        // TODO: в менеджер
        /// <summary>
        /// Добавление правил в таблицу DataGrid.
        /// </summary>
        /// <param name="acc">id рк.</param>
        public async void GetRules(string acc)
        {
            DataGridView.Rows.Clear();

            // TODO: все строки вынести в отдельный класс, где каждая строка будет константой. Здесь вместо прямого написания строк использовать вызов констант - меньше вероятность ошибки.
            var request = new RestRequest($"act_{acc}/adrules_library", Method.GET);
            request.AddQueryParameter("fields", "entity_type,evaluation_spec,execution_spec,name,schedule_spec,execution_type");
            var json = await re.ExecuteRequestAsync(request);
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
                // TODO: вот эта часть остается в форме, а менеджер просто возвращает данные для строки таблицы
                DataGridView.Rows.Add(rule["name"], entitytype, timepresent, triggerResult, filtercondition);
              
                
                
            }

        }

    }
}
