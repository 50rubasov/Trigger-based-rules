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
        /// // TODO: именование
        /// // TODO: readonly?
        public Dictionary<string, string> revnames = new Dictionary<string, string>();
        public FiltersResult()
        {
            revnames.Add("cost_per", "Цена за результат");
            revnames.Add("results","Результаты");        
            revnames.Add("result", "Результаты"); // для обычных правил
            revnames.Add("spent","Расходы");
            revnames.Add("GREATER_THAN",">");
            revnames.Add("LESS_THAN","<");
            revnames.Add("EQUAL","=");
            revnames.Add("ADSET","Группа объявлений");
            revnames.Add("CAMPAIGN","Компания");
            revnames.Add("AD","Объявление");
            revnames.Add("LIFETIME", "Весь срок действия");
            revnames.Add("TODAY", "Сегодня");
            revnames.Add("cost_per_mobile_app_install", "Цена за установку");
            revnames.Add("impressions", "Показы");
            revnames.Add("reach", "Охват");
            revnames.Add("link_click", "Клики по ссылке");
            revnames.Add("cpm", "CPM");
            revnames.Add("cpc", "CPC");
            revnames.Add("ctr", "CTR");
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
                if (revnames.ContainsKey(value))
                {
                    _field = revnames[value];
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
                if (revnames.ContainsKey(value))
                {
                    _operator = revnames[value];
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
                return $"{revnames[Value]}";
            }

            return $"{Field} {Operator} {Value}; ";
            
        }
    }
}
