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
        public async Task<List<JToken>> GetAllBmsAsync()
        {
            var request = new RestRequest($"me/businesses", Method.GET);
            var json = await _reqEx.ExecuteRequestAsync(request);
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
            var request = new RestRequest($"{bmid}/client_ad_accounts", Method.GET);
            request.AddQueryParameter("fields", "name,account_status");
            var json = await _reqEx.ExecuteRequestAsync(request);
            var accounts = json["data"].ToList();
            request = new RestRequest($"{bmid}/owned_ad_accounts", Method.GET);
            request.AddQueryParameter("fields", "name,account_status");
            json = await _reqEx.ExecuteRequestAsync(request);
            accounts.AddRange(json["data"].ToList());
            //Исключаем забаненные
            if (!includeBanned)
                accounts = accounts.Where(acc => acc["account_status"].ToString() != "2").ToList();
            return accounts;
        }

       
    }
}
