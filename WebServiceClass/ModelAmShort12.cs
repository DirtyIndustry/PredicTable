using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort12
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public DateTime FORECASTDATE { get; set; } = DateTime.Now;
        public string WEATERSTATE { get; set; } = "";
        public string TEMPERATURE { get; set; } = "";
        public string WINDSPEED { get; set; } = "";
        public string WINDDIRECTION { get; set; } = "";
        public string WAVEHEIGHT { get; set; } = "";

        public ModelAmShort12() { }
        public ModelAmShort12(DateTime publishdate, DateTime forecastdate)
        {
            this.PUBLISHDATE = publishdate;
            this.FORECASTDATE = forecastdate;
        }
    }
}