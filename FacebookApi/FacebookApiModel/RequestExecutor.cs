﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;
using System;
namespace FacebookApiModel
{
    /// <summary>
    /// Класс создания запроса к Facebook API
    /// </summary>
    public class RequestExecutor
    {
        /// <summary>
        /// токен доступа
        /// </summary>
        private readonly string _accessToken;
        /// <summary>
        /// RestClient для отправки запроса
        /// </summary>
        public RestClient Rc { get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="apiAddress">Api Facebook</param>
        /// <param name="accessToken">Токен доступа</param>
        public RequestExecutor(string apiAddress, string accessToken)
        {
            Rc = new RestClient(apiAddress);
            _accessToken = accessToken;
        }

        /// <summary>
        /// Создание запроса к API
        /// </summary>
        /// <param name="req">запрос</param>
        /// <param name="changeToken">изменение токена</param>
        /// <returns></returns>
        public async Task<JObject> ExecuteRequestAsync(RestRequest req, bool changeToken = true)
        {
            if (changeToken)
            {
                if (req.Method == Method.GET)
                    req.AddQueryParameter("access_token", _accessToken);
                else
                    req.AddParameter("access_token", _accessToken);
            }
            var resp = await Rc.ExecuteTaskAsync(req);
            var respJson = (JObject)JsonConvert.DeserializeObject(resp.Content);           
            return respJson;
        }
    }
    
}
