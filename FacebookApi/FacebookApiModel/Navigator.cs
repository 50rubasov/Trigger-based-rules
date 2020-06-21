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
        // TODO: именование
        /// <summary>
        /// Объект класса отправки запроса
        /// </summary>
        private readonly RequestExecutor _re;
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="re">Объект класса отправки запроса</param>
        public Navigator(RequestExecutor re)
        {
            _re = re;
        }

        // TODO: именование
        /// <summary>
        /// Получение всех БМов.
        /// </summary>
        /// <returns>название БМа</returns>
        public async Task<List<JToken>> GetAllBmsAsync()
        {
            var request = new RestRequest($"me/businesses", Method.GET);
            var json = await _re.ExecuteRequestAsync(request);
            var bms = json["data"].ToList();
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
            // TODO: опять куча строк - вынести в отдельный класс-словарь с именованными константами
            var request = new RestRequest($"{bmid}/client_ad_accounts", Method.GET);
            request.AddQueryParameter("fields", "name,account_status");
            var json = await _re.ExecuteRequestAsync(request);
            var accounts = json["data"].ToList();
            request = new RestRequest($"{bmid}/owned_ad_accounts", Method.GET);
            request.AddQueryParameter("fields", "name,account_status");
            json = await _re.ExecuteRequestAsync(request);
            accounts.AddRange(json["data"].ToList());
            //Исключаем забаненные
            if (!includeBanned)
                accounts = accounts.Where(acc => acc["account_status"].ToString() != "2").ToList();
            return accounts;
        }

       
    }
}
