using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort10
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public DateTime FORECASTDATE { get; set; } = DateTime.Now;
        public string WAVELEVELONE { get; set; } = "";
        public string WAVELEVELTYPE { get; set; } = "";
        public string WAVEDIRECTION { get; set; } = "";
        public string WATERTEMPERATURE { get; set; } = "";

        public ModelAmShort10() { }
        public ModelAmShort10(DateTime publishdate, DateTime forecastdate)
        {
            this.PUBLISHDATE = publishdate;
            this.FORECASTDATE = forecastdate;
        }
    }
}