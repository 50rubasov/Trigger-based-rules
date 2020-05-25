using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace FacebookApiModel
{
    /// <summary>
    /// Класс для вывода всех условий в DataGrid
    /// </summary>
    public partial class FiltersResult
    {
        /// <summary>
        /// Словарь для перевода поля на русский язык.
        /// </summary>
        public Dictionary<string, string> revnames = new Dictionary<string, string>();
        public FiltersResult()
        {
            revnames.Add("cost_per", "Цена за результат");
            revnames.Add("results","Результаты");
            // для обычных правил
            revnames.Add("result", "Результаты");
            revnames.Add("spent","Расходы");
            revnames.Add("GREATER_THAN",">");
            revnames.Add("LESS_THAN","<");
            revnames.Add("EQUAL","=");
            revnames.Add("ADSET","Группа объявлений");
            revnames.Add("CAMPAIGN","Компания");
            revnames.Add("AD","Объявление");
        }
        /// <summary>
        /// Поле
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
        /// <summary>
        /// Оператор
        /// </summary>
        [JsonProperty("operator")]
        public string Operator { get; set; }

        /// <summary>
        /// Формирование строки вывода
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if ((Field == "cost_per") || (Field == "spent"))
            {
                double v = Int32.Parse(Value);
                // если значение целое 1300 -> 13
                if (v % 100 == 0)
                {
                    v = v / 100;
                    Value = v.ToString();
                }
                //если значение вещественное 1325 -> 13,25
                else
                {

                    Value = (v/100).ToString() + "," + (v % 100).ToString();
                }
            }
            return $"{revnames[Field]} {revnames[Operator]} {Value}; ";
        }
    }
}
