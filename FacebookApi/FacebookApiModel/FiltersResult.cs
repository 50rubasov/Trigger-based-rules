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
            return $"{Field} {Operator} {Value}; ";
        }
    }
}
