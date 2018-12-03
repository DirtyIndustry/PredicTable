using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort2
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

        public ModelAmShort2() { }
        public ModelAmShort2(DateTime publishdate, DateTime forecastdate, string htlharbour)
        {
            this.PUBLISHDATE = publishdate;
            this.FORECASTDATE = forecastdate;
            this.HTLHARBOUR = htlharbour;
        }
    }
}