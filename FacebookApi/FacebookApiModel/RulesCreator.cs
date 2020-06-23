using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace FacebookApiModel
{
    /// <summary>
    /// Класс создания новых правил.
    /// </summary>
    public class RulesCreator
    {
        /// <summary>
        /// Объект класса отправки запроса
        /// </summary>
        private readonly RequestExecutor _reqEx;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="re">Объект класса отправки запроса</param>
        public RulesCreator(RequestExecutor re)
        {
            _reqEx = re;
        }
        public string msg;

        /// <summary>
        /// Загрузка нового правила
        /// </summary>
        /// <param name="acc">ID Аккаунта</param>
        /// <param name="trigger">Параметры Триггера</param>
        /// <param name="name">Название правила</param>
        /// <param name="entityType">Область применения</param>
        /// <param name="filters">Параметры фильтров</param>
        /// <param name="time">Время выполнения правила</param>
        /// <returns></returns>
        public async Task UploadAsync(string acc, string trigger, string name, string entityType, string filters, string time)
        {
            var accSplit = acc.Split(',');
            foreach (var a in accSplit)
            {

                string evalutionSpecString = trigger
                                             + "\"filters\": ["
                                             + "{"
                                             + "\"field\": \"entity_type\","
                                             + "\"value\": \"" + entityType + "\","
                                             + "\"operator\" : \"EQUAL\"},"
                                             + "{"
                                             + "\"field\": \"time_preset\","
                                             + "\"value\": \"" + time + "\","
                                             + "\"operator\": \"EQUAL\" }," + filters + "]}";


                string executionSpecString = "{\"execution_type\":\"PAUSE\"}";
                var execution_spec = JObject.Parse(executionSpecString);
                var evaluation_spec = JObject.Parse(evalutionSpecString);


                var req = new RestRequest($"act_{a}/adrules_library", Method.POST);
                req.AddParameter("evaluation_spec", evaluation_spec);
                req.AddParameter("execution_spec", execution_spec);
                req.AddParameter("name", name);

                var js = await _reqEx.ExecuteRequestAsync(req);
                var error = js["error"]?["message"].ToString();
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
            }
        }
    }
}
