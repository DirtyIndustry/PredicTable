using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 海阳万米海滩海水浴场风、浪预报
    /// </summary>
    public class YT_YC
    {
        public DateTime PUBLISHDATE { get; set; }
        public DateTime FORECASTDATE { get; set; }
        public string WEATERSTATE { get; set; }
        public string TEMPERATURE { get; set; }
        public string WINDSPEED { get; set; }
        public string WINDDIRECTION { get; set; }
        public string WAVEHEIGHT { get; set; }
    }
}