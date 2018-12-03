using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort8
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public string HTLHARBOUR { get; set; } = "";
        public DateTime FORECASTDATE { get; set; } = DateTime.Now;
        public string HTLFIRSTWAVEOFTIME { get; set; } = "";
        public string HTLFIRSTWAVETIDELEVEL { get; set; } = "";
        public string HTLFIRSTTIMELOWTIDE { get; set; } = "";
        public string HTLLOWTIDELEVELFORTHEFIRSTTIME { get; set; } = "";
        public string HTLSECONDWAVEOFTIME { get; set; } = "";
        public string HTLSECONDWAVETIDELEVEL { get; set; } = "";
        public string HTLSECONDTIMELOWTIDE { get; set; } = "";
        public string HTLLOWTIDELEVELFORTHESECONDTIM { get; set; } = "";
        
        public ModelAmShort8() { }
        public ModelAmShort8(DateTime publishdate, DateTime forecastdate, string location)
        {
            this.PUBLISHDATE = publishdate;
            this.FORECASTDATE = forecastdate;
            this.HTLHARBOUR = location;
        }
    }
}