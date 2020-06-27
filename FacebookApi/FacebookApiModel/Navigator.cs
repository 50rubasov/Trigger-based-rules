using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;


namespace FacebookApiModel
{
    /// <summary>
    /// Класс, содержащий методы для работы с API
    /// </summary>
    public class Navigator
    {
        /// <summary>
        /// Объект класса отправки запроса
        /// </summary>
        private readonly RequestExecutor _reqEx;
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="re">Объект класса отправки запроса</param>
        public Navigator(RequestExecutor re)
        {
            _reqEx = re;
        }

        /// <summary>
        /// Получение всех БМов.
        /// </summary>
        /// <returns>название БМа</returns>
        private StringDictionary _strDict = new StringDictionary();
        /// <summary>
        /// Объект класса перевода объектов JSON на русский язык
        /// </summary>
        public async Task<List<JToken>> GetAllBmsAsync()
        {
            var request = new RestRequest($"me/businesses", Method.GET);
            var json = await _reqEx.ExecuteRequestAsync(request);
            var bms = json[_strDict.Data].ToList();
            return bms;
        }

        /// <summary>
        /// Получение всех рекламных кабинетов
        /// </summary>
        /// <param name="bmid">ид бма</param>
        /// <param name="includeBanned">забаненные рекламные кабинеты</param>
        /// <returns>название рк</returns>
        public async Task<List<JToken>> GetBmsAdAccountsAsync(string bmid, bool includeBanned = false)
        {
            var request = new RestRequest($"{bmid}/{_strDict.ClientAdAccounts}", Method.GET);
            request.AddQueryParameter(_strDict.Fields, _strDict.Name + ", "+ _strDict.AccountStatus);
            var json = await _reqEx.ExecuteRequestAsync(request);
            var accounts = json[_strDict.Data].ToList();
            request = new RestRequest($"{bmid}/{_strDict.OwnedAdAccounts}", Method.GET);
            request.AddQueryParameter(_strDict.Fields, _strDict.Name + ", " + _strDict.AccountStatus);
            json = await _reqEx.ExecuteRequestAsync(request);
            accounts.AddRange(json[_strDict.Data].ToList());
            //Исключаем забаненные
            if (!includeBanned)
                accounts = accounts.Where(acc => acc[_strDict.AccountStatus].ToString() != "2").ToList();
            return accounts;
        }
        /// <summary>
        /// Получение личного рекламного кабинета
        /// </summary>
        /// <param name="includeBanned">забаненные рекламные кабинеты</param>
        /// <returns>название рк</returns>
        public async Task<List<JToken>> GetAdAccountsAsync(bool includeBanned = false)
        {
            var request = new RestRequest($"me/personal_ad_accounts", Method.GET);
            request.AddQueryParameter(_strDict.Fields, _strDict.Name+", "+ _strDict.Id);
            var json = await _reqEx.ExecuteRequestAsync(request);
            var accounts = json[_strDict.Data].ToList();

            //Исключаем забаненные
            if (!includeBanned)
                accounts = accounts.Where(acc => acc[_strDict.AccountStatus].ToString() != "2").ToList();
            return accounts;
        }
        /// <summary>
        /// Метод удаления правил
        /// </summary>
        /// <param name="accId">ID аккаунта</param>
        /// <param name="selected">название выбраного правила</param>
        public async void DeleteRules(string accId, string selected)
        {
            var request = new RestRequest($"act_{accId}/{_strDict.AdrulesLibrary}", Method.GET);
            request.AddQueryParameter(_strDict.Fields, _strDict.Name);
            var json = await _reqEx.ExecuteRequestAsync(request);

            foreach (var rule in json[_strDict.Data])
            {
                string name = rule[_strDict.Name].ToString();
                if (name == selected)
                {
                    try
                    {
                        request = new RestRequest($"{rule[_strDict.Id]}", Method.DELETE);
                        var js = _reqEx.ExecuteRequestAsync(request);
                    }
                    catch(Exception ex)
                    {
                        throw new Exception();
                    }
                }
            }
        }
        /// <summary>
        /// Метод, переводящий объекты JSON для DataGridView
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Tuple<string, string,string,string> TranslateRules(JToken trigger, JToken filter)
        {
            string timepresent = null;
            string entitytype = null;
            string filtercondition = null;
            string triggerResult = null;
            bool errorfilter = false;
            

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

                    filtercondition += "(Нечитаемый фильтр)" + result;
                    errorfilter = true;
                }

            }

            foreach (var s in filtersResults)
            {
                if (s.Field == _strDict.EntityType)
                {
                    entitytype = s.ToString();
                }
                if (s.Field == _strDict.TimePreset)
                {
                    timepresent = s.ToString();
                }
                if ((s.Field == _strDict.AttributionWindow || s.Field == _strDict.EntityType || s.Field == _strDict.TimePreset) == false)
                {
                    filtercondition = filtercondition + s;
                }
            }
            return Tuple.Create<string, string, string, string>(
                entitytype,
                timepresent,
                triggerResult,
                filtercondition
                );
        }

    }
}
