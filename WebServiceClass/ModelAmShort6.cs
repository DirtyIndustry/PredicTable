using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort6
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public string TLFORECASTSTANCE { get; set; } = "";
        public DateTime FORECASTDATE { get; set; } = DateTime.Now;
        public string TLFIRSTWAVEOFTIME { get; set; } = "";
        public string TLFIRSTWAVETIDELEVEL { get; set; } = "";
        public string TLFIRSTTIMELOWTIDE { get; set; } = "";
        public string TLLOWTIDELEVELFORTHEFIRSTTIME { get; set; } = "";
        public string TLSECONDWAVEOFTIME { get; set; } = "";
        public string TLSECONDWAVETIDELEVEL { get; set; } = "";
        public string TLSECONDTIMELOWTIDE { get; set; } = "";
        public string TLLOWTIDELEVELFORTHESECONDTIME { get; set; } = "";

        public ModelAmShort6() { }
        public ModelAmShort6(DateTime publishdate, DateTime forecastdate, string location)
        {
            this.PUBLISHDATE = publishdate;
            this.FORECASTDATE = forecastdate;
            this.TLFORECASTSTANCE = location;
        }
    }
}