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
        private readonly RequestExecutor _re;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="re">Объект класса отправки запроса</param>
        public RulesCreator(RequestExecutor re)
        {
            _re = re;
        }
        /// <summary>
        /// Загрузка нового правила
        /// </summary>
        /// <param name="acc">id аккаунта</param>
        /// <param name="name">Название правила</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        public async Task UploadAsync(string acc, string name, string value, string entityType, string conditionOperator, string conditionField, string action)
        {
            try
            {
                var accSplit = acc.Split(',');
                foreach (var a in accSplit)
                {
                    string evalutionSpecString = "{\"evaluation_type\":\"TRIGGER\","
                                                 + "\"trigger\" : {"
                                                 + "\"type\" : \"STATS_CHANGE\","
                                                 + "\"field\": \""+ conditionField + "\","
                                                 + "\"value\": \""+value+"\","
                                                 + "\"operator\": \"" + conditionOperator + "\"},"
                                                 + "\"filters\": ["
                                                 + "{"
                                                 + "\"field\": \"entity_type\","
                                                 + "\"value\": \"" + entityType + "\","
                                                 + "\"operator\" : \"EQUAL\"},"
                                                 + "{"
                                                 + "\"field\": \"time_preset\","
                                                 + "\"value\": \"LIFETIME\","
                                                 + "\"operator\": \"EQUAL\" },]}";
                    string executionSpecString = "{\"execution_type\":\""+action+"\"}";
                    var execution_spec = JObject.Parse(executionSpecString);
                    var evaluation_spec = JObject.Parse(evalutionSpecString);


                    var req = new RestRequest($"act_{a}/adrules_library", Method.POST);
                    req.AddParameter("evaluation_spec", evaluation_spec);
                    req.AddParameter("execution_spec", execution_spec);
                    req.AddParameter("name", name);
                    


                    var js = await _re.ExecuteRequestAsync(req);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

      
    }
}
    