using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Linq;

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
        /// <summary>
        /// Строковые значения объектов JSON.
        /// </summary>
        private RequestCommands _strDict = new RequestCommands();
        /// <summary>
        /// Объект класса перевода объектов JSON на русский язык
        /// </summary>
        public FiltersResult FilterRes = new FiltersResult();
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


                var req = new RestRequest($"act_{a}/{_strDict.AdrulesLibrary}", Method.POST);
                req.AddParameter(_strDict.EvaluationSpec, evaluation_spec);
                req.AddParameter(_strDict.ExecutionSpec, execution_spec);
                req.AddParameter(_strDict.Name, name);

                var js = await _reqEx.ExecuteRequestAsync(req);
                var error = js["error"]?["message"].ToString();
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
            }
        }
        /// <summary>
        /// Метод формирования условия триггера/фильтра
        /// </summary>
        /// <param name="fields">поле</param>
        /// <param name="values">значение</param>
        /// <param name="operators">оператор сравнения</param>
        /// <param name="trigger">триггер</param>
        /// <param name="filters">фильтры</param>
        /// <param name="i">итератор количества дополнительных фильтров</param>
        /// <returns></returns>
        public Tuple<string, string> CreateMainCondition(string fields, string values, string operators, string filters, string trigger, int i)
        {
            //string trigger = null;
            if ((fields == "Цена за результат") || (fields == "Расходы") || (fields == "Цена за установку"))
            {
                string clearv = values.Replace(",", "");
                values = clearv.Replace(".", "");

            }
            //Если поле - фильтр
            if (i > 1)
            {
                filters = filters + "{"
                + "\"field\": \"" + FilterRes.Names.FirstOrDefault(x => x.Value == fields).Key + "\","
                + "\"value\": \"" + values + "\","
                + "\"operator\" : \"" + FilterRes.Names.FirstOrDefault(x => x.Value == operators).Key + "\"},";
            }
            //Если поле - триггер
            else
            {
                trigger = "{\"evaluation_type\":\"TRIGGER\","
                                                 + "\"trigger\" : {"
                                                 + "\"type\" : \"STATS_CHANGE\","
                                                 + "\"field\": \"" + FilterRes.Names.FirstOrDefault(x => x.Value == fields).Key + "\","
                                                 + "\"value\": \"" + values + "\","
                                                 + "\"operator\": \"" + FilterRes.Names.FirstOrDefault(x => x.Value == operators).Key + "\"},";
            }
            return Tuple.Create<string, string>(
                trigger,
                filters                              
                );
        }       
    }
}
