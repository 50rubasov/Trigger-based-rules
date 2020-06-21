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
        // TODO: именование
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
        /// // TODO: xml-комментарий содержит неправильный список параметров
        public async Task UploadAsync(string acc, string trigger, string name,  string entityType,string filters, string time)
        {
            try
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
                    
                    var js = await _re.ExecuteRequestAsync(req);
                }
            }
            catch (Exception ex)
            {
                // TODO: зачем? Создавая новое исключение, ты теряешь всю полезную информацию из исключения, вылетевшего со стороны API
                throw new Exception();
            }
        }
      
    }
}
    