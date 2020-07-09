using NUnit.Framework;
using FacebookApiModel;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

using System.Linq;
namespace IntegrationTests
{
    /// <summary>
    /// Класс тестирования основных методов.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Объект класса отправки запроса
        /// </summary>
        private static RequestExecutor _reqEx;
        /// <summary>
        /// id рекламного аккаунта
        /// </summary>
        private string _acc = "608186249803429";
        /// <summary>
        /// Объект класса для создания нового правила
        /// </summary>
        public RulesCreator rc;
        /// <summary>
        /// API адресс facebook
        /// </summary>
        private static string _apiAddress = "https://graph.facebook.com/v6.0/";
        /// <summary>
        /// Создание объекта класса для работы с API.
        /// </summary>
        private Navigator Navigator;
        /// <summary>
        /// Класс для перевода объектов JSON
        /// </summary>
        private RequestCommands _strDict = new RequestCommands();
        /// <summary>
        /// Токен доступа к тестовому аккаунту
        /// </summary>
        private static string _token = "EAABsbCS1iHgBALRwFLMmFQmwATtlbFhuzZA7DcnPdmeSbRAFBvIWDIAnVkJlA6c3ka9cevCOOZBo0V72ubFtK8FiUr9xFrQaUGC4mgJlTWj1GnRWq9y58ZBRKCtdfjZBUsuehudQ3MHJm2z2ExMONXdpcndCYnBGdla0l0VZCZC2sIv1tDqsZCI";
        /// <summary>
        /// Формирование запроса к Facebook API
        /// </summary>
        /// <param name="apiAddress">api facebook</param>
        /// <returns></returns>
        public static RequestExecutor GetConfiguredRequestExecutor(string apiAddress)
        {
            return new RequestExecutor(apiAddress, _token);
        }
        
        [Test]
        [TestCase(TestName = "Тест установки токена и получение имени БМа")]
        public async Task SetTokenAndGetBMs()
        {
            
            string name = null;
            _reqEx = GetConfiguredRequestExecutor(_apiAddress);
            Navigator = new FacebookApiModel.Navigator(_reqEx);

            var Bms = await Navigator.GetAllBmsAsync();
            for (int i = 0; i < Bms.Count; i++)
            {
                var bm = Bms[i];
            }
            foreach (var bm in Bms)
            {
                name = Convert.ToString(bm[_strDict.Name]);
            }
            Assert.AreEqual("TEst", name);
        }

        [Test]
        [TestCase(TestName = "Тест создания правила на основе триггеров")]
        public async Task UploadAsync()
        {
            FiltersResult FilterRes = new FiltersResult();
            rc = new RulesCreator(_reqEx);
            string trigger = null;
            string filters = null;
            string fields;
            string operators;
            string values;
            bool working;
            var result = rc.CreateMainCondition("Результаты", "3", ">", null, 1);
            trigger = result.Item1;
            filters = result.Item2;
            
            try
            {
                await rc.UploadAsync(_acc, trigger, "test", "ADSET", filters, FilterRes.Names.FirstOrDefault(x => x.Value == "Сегодня").Key);
                working = true;
            }
            catch(Exception ex)
            {
                working = false;
            }

           
           
            Assert.IsTrue(working);
        }
        [Test]
        [TestCase(TestName = "Тест вывода всех правил")]
        public async Task ShowRules()
        {
            string name = null;
            var request = new RestRequest($"act_{_acc}/{_strDict.AdrulesLibrary}", Method.GET);
            request.AddQueryParameter(_strDict.Fields, _strDict.EntityType + "," + _strDict.EvaluationSpec + ","
                + _strDict.ExecutionSpec + "," + _strDict.Name + ","
                + _strDict.ScheduleSpec + "," + _strDict.ExecutionType);
            var json = await _reqEx.ExecuteRequestAsync(request);

            foreach (var rule in json[_strDict.Data])
            {

                JToken filter = rule[_strDict.EvaluationSpec][_strDict.Filters];
                JToken trigger = rule[_strDict.EvaluationSpec][_strDict.Trigger];
                var result = Navigator.TranslateRules(trigger, filter);

                
                name = rule[_strDict.Name].ToString();
            }
            Assert.AreEqual("test", name);
        }

        [Test]
        [TestCase(TestName = "Тест удаления правила")]
        public async Task DeleteRule()
        {
            bool working = true;
            Navigator = new FacebookApiModel.Navigator(_reqEx);
            Navigator.DeleteRules(_acc, "test");
            var request = new RestRequest($"act_{_acc}/{_strDict.AdrulesLibrary}", Method.GET);
            
            request.AddQueryParameter(_strDict.Fields, _strDict.Name);
            var json = await _reqEx.ExecuteRequestAsync(request);
            if (json[_strDict.Name] == null)
            {
                working = true;
            }
            else working = false;
            Assert.IsTrue(working);
        }  
    }
}