using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 海阳近岸海域潮汐预报
    /// </summary>
    public class YT_TideForecast
    {
        public DateTime PUBLISHDATE { get; set; }
        public DateTime FORECASTDATE { get; set; }
        public string FIRSTHIGHTIME { get; set; }
        public string FIRSTHIGHLEVEL { get; set; }
        public string FIRSTLOWTIME { get; set; }
        public string FIRSTLOWLEVEL { get; set; }
        public string SECONDHIGHTIME { get; set; }
        public string SECONDHIGHLEVEL { get; set; }
        public string SECONDLOWTIME { get; set; }
        public string SECONDLOWLEVEL { get; set; }
    }
}