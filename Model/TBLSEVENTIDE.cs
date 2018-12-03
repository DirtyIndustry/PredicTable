using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 七地市潮汐
    /// </summary>
    public class TBLSEVENTIDE
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
        public string FORECASTAREA { get; set; }
    }
}