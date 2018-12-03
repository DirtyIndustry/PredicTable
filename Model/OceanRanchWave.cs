using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 海洋牧场-海浪
    /// </summary>
    public class OceanRanchWave
    {
        public DateTime PUBLISHDATE { get; set; }
        public DateTime FORECASTDATE { get; set; }

        public string OCEANRANCHNAME { get; set; }

        public string WAVE24HDAY { get; set; }
        public string WAVE24HNEIGHT { get; set; }
        public string WAVE48HDAY { get; set; }
        public string WAVE48HNEIGHT { get; set; }
        public string WAVE72HDAY { get; set; }
        public string WAVE72HNEIGHT { get; set; }

        public string OCEANRANCHSHORTNAME { get; set; }
        public string SN { get; set; }
    }
}