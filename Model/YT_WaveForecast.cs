using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 烟台南部海浪、水温预报
    /// </summary>
    public class YT_WaveForecast
    {
        public DateTime PUBLISHDATE { get; set; }
        public DateTime FORECASTDATE { get; set; }
        public string WAVELEVELONE { get; set; }
        public string WAVELEVELTWO { get; set; }
        public string WAVELEVELTYPE { get; set; }
        public string WAVEDIRECTION { get; set; }
        public string WATERTEMPERATURE { get; set; }
    }
}