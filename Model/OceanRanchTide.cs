using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 海洋牧场-潮汐
    /// </summary>
    public class OceanRanchTide
    {
        public DateTime PUBLISHDATE { get; set; }
        public DateTime FORECASTDATE { get; set; }

        public string OCEANRANCHNAME { get; set; }
        public string OCEANRANCHSHORTNAME { get; set; }
        public string SN { get; set; }

        public string TIDE24H00 { get; set; }
        public string TIDE24H01 { get; set; }
        public string TIDE24H02 { get; set; }
        public string TIDE24H03 { get; set; }
        public string TIDE24H04 { get; set; }
        public string TIDE24H05 { get; set; }
        public string TIDE24H06 { get; set; }
        public string TIDE24H07 { get; set; }
        public string TIDE24H08 { get; set; }
        public string TIDE24H09 { get; set; }
        public string TIDE24H10 { get; set; }
        public string TIDE24H11 { get; set; }
        public string TIDE24H12 { get; set; }
        public string TIDE24H13 { get; set; }
        public string TIDE24H14 { get; set; }
        public string TIDE24H15 { get; set; }
        public string TIDE24H16 { get; set; }
        public string TIDE24H17 { get; set; }
        public string TIDE24H18 { get; set; }
        public string TIDE24H19 { get; set; }
        public string TIDE24H20 { get; set; }
        public string TIDE24H21 { get; set; }
        public string TIDE24H22 { get; set; }
        public string TIDE24H23 { get; set; }

        public string TIDEFIRSTHTIME { get; set; }
        public string TIDEFIRSTHHEIGHT { get; set; }
        public string TIDESECONDHTIME { get; set; }
        public string TIDESECONDHHEIGHT { get; set; }
        public string TIDEFIRSTLTIME { get; set; }
        public string TIDEFIRSTLHEIGHT { get; set; }
        public string TIDESECONDLTIME { get; set; }
        public string TIDESECONDLHEIGHT { get; set; }
    }
}