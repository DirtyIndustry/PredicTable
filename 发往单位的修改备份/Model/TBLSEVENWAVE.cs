using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 七地市海浪
    /// </summary>
    public class TBLSEVENWAVE
    {
        public DateTime PUBLISHDATE { get; set; }
        public DateTime FORECASTDATE { get; set; }
        public string WINDDIRECTION { get; set; }
        public string WINDFORCE { get; set; }
        public string WAVEHEIGHT { get; set; }
        public string FORECASTAREA { get; set; }
    }
}