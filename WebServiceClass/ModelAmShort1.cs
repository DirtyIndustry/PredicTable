using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort1
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public string REPORTAREA { get; set; } = "";
        public DateTime FORECASTDATE { get; set; } = DateTime.Now;
        public string YRBHWWFWAVEHEIGHT { get; set; } = "";
        public string YRBHWWFWAVEDIR { get; set; } = "";
        public string YRBHWWFFLOWDIR { get; set; } = "";
        public string YRBHWWFFLOWLEVEL { get; set; } = "";
        public string YRBHWWFWATERTEMPERATURE { get; set; } = "";

        public ModelAmShort1()
        {
            
        }

        public ModelAmShort1(DateTime publishdate, DateTime forecastdate, string reportarea)
        {
            this.PUBLISHDATE = publishdate;
            this.FORECASTDATE = forecastdate;
            this.REPORTAREA = reportarea;
        }
    }
}