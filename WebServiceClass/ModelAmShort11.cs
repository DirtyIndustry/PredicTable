using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort11
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public DateTime FORECASTDATE { get; set; } = DateTime.Now;
        public string FIRSTHIGHTIME { get; set; } = "";
        public string FIRSTHIGHLEVEL { get; set; } = "";
        public string FIRSTLOWTIME { get; set; } = "";
        public string FIRSTLOWLEVEL { get; set; } = "";
        public string SECONDHIGHTIME { get; set; } = "";
        public string SECONDHIGHLEVEL { get; set; } = "";
        public string SECONDLOWTIME { get; set; } = "";
        public string SECONDLOWLEVEL { get; set; } = "";

        public ModelAmShort11() { }
        public ModelAmShort11(DateTime publishdate, DateTime forecastdate)
        {
            this.PUBLISHDATE = publishdate;
            this.FORECASTDATE = forecastdate;
        }
    }
}