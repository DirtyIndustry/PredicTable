using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 海洋牧场-海温
    /// </summary>
    public class OceanRanchTemp
    {
        public DateTime PUBLISHDATE { get; set; }
        public DateTime FORECASTDATE { get; set; }

        public string OCEANRANCHNAME { get; set; }

        public string TEMP24H { get; set; }
        public string TEMP48H { get; set; }
        public string TEMP72H { get; set; }

        public string OCEANRANCHSHORTNAME { get; set; }
        public string SN { get; set; }
    }
}