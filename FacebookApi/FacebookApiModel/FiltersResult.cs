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
        public readonly Dictionary<string, string> RevNames = new Dictionary<string, string>();
        public FiltersResult()
        {
            RevNames.Add("cost_per", "Цена за результат");
            RevNames.Add("results","Результаты");        
            RevNames.Add("result", "Результаты"); // для обычных правил
            RevNames.Add("spent","Расходы");
            RevNames.Add("GREATER_THAN",">");
            RevNames.Add("LESS_THAN","<");
            RevNames.Add("EQUAL","=");
            RevNames.Add("ADSET","Группа объявлений");
            RevNames.Add("CAMPAIGN","Компания");
            RevNames.Add("AD","Объявление");
            RevNames.Add("LIFETIME", "Весь срок действия");
            RevNames.Add("TODAY", "Сегодня");
            RevNames.Add("cost_per_mobile_app_install", "Цена за установку");
            RevNames.Add("impressions", "Показы");
            RevNames.Add("reach", "Охват");
            RevNames.Add("link_click", "Клики по ссылке");
            RevNames.Add("cpm", "CPM");
            RevNames.Add("cpc", "CPC");
            RevNames.Add("ctr", "CTR");
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
                if (RevNames.ContainsKey(value))
                {
                    _field = RevNames[value];
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
                if (RevNames.ContainsKey(value))
                {
                    _operator = RevNames[value];
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
                return $"{RevNames[Value]}";
            }

            return $"{Field} {Operator} {Value}; ";
            
        }
    }
}
