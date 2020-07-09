using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace FacebookApiModel
{
    /// <summary>
    /// Класс для вывода всех условий в DataGrid
    /// </summary>
    public partial class FiltersResult
    {
        /// <summary>
        /// Высота.
        /// </summary>
        private string _field;
        /// <summary>
        /// Длинна.
        /// </summary>
        private string _operator;
        /// <summary>
        /// Ширина.
        /// </summary>
        private string _value;
        /// <summary>
        /// Количество отверстий сабвуфера.
        /// </summary>
       
        /// <summary>
        /// Словарь для перевода поля на русский язык.
        /// </summary>
        public readonly Dictionary<string, string> Names = new Dictionary<string, string>();
        public FiltersResult()
        {
            Names.Add("cost_per", "Цена за результат");
            Names.Add("results","Результаты");        
            Names.Add("result", "Результаты"); // для обычных правил
            Names.Add("spent","Расходы");
            Names.Add("GREATER_THAN",">");
            Names.Add("LESS_THAN","<");
            Names.Add("EQUAL","=");
            Names.Add("ADSET","Группа объявлений");
            Names.Add("CAMPAIGN","Кампания");
            Names.Add("AD","Объявление");
            Names.Add("LIFETIME", "Весь срок действия");
            Names.Add("TODAY", "Сегодня");
            Names.Add("cost_per_mobile_app_install", "Цена за установку");
            Names.Add("lifetime_impressions", "Показы за все время");
            Names.Add("impressions", "Показы");
            Names.Add("reach", "Охват");
            Names.Add("link_click", "Клики по ссылке");
            Names.Add("cpm", "CPM");
            Names.Add("cpc", "CPC");
            Names.Add("ctr", "CTR");
        }
        /// <summary>
        /// Поле
        /// </summary>
        [JsonProperty("field")]
        public string Field
        {
            get
            {
                return _field;

            }
            set
            {
                if (Names.ContainsKey(value))
                {
                    _field = Names[value];
                }
                else _field = value;

            }
        }
        /// <summary>
        /// Значение
        /// </summary>
        [JsonProperty("value")]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if ((Field == "Расходы") || (Field == "Цена за результат") || (Field == "Цена за установку"))
                {
                    double v = Int32.Parse(value);               
                    value = (v / 100).ToString();
                }
                _value = value;
            }
        }

        /// <summary>
        /// Оператор
        /// </summary>
        [JsonProperty("operator")]
        public string Operator {
            get
            {

                return _operator;
            }
            set
            {
                if (Names.ContainsKey(value))
                {
                    _operator = Names[value];
                }
                else _operator = value;
            }
        }

        /// <summary>
        /// Формирование строки вывода
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            
            if (Field == "entity_type" || Field == "time_preset")
            {
                return $"{Names[Value]}";
            }

            return $"{Field} {Operator} {Value}; ";
            
        }
    }
}
