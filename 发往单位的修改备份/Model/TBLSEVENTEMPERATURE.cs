using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 七地市海温
    /// </summary>
    public class TBLSEVENTEMPERATURE
    {
        public DateTime PUBLISHDATE { get; set; }
        public DateTime FORECASTDATE { get; set; }
        public string TEMPERATURE { get; set; }
        public string FORECASTAREA { get; set; }
    }
}