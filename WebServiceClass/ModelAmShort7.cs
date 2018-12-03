using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort7
    {
        public DateTime PUBLISHDATE { set; get; } = DateTime.Now;
        public string REPORTAREA { get; set; } = "";
        public DateTime FORECASTDATE { set; get; } = DateTime.Now.AddDays(1);
        public string YRBHWWFWAVEHEIGHT { get; set; } = "";
        public string YRBHWWFWAVEDIR { get; set; } = "";
        public string YRBHWWFFLOWDIR { get; set; } = "";
        public string YRBHWWFFLOWLEVEL { get; set; } = "";

        public ModelAmShort7() { }
        public ModelAmShort7(DateTime publishdate, DateTime forecastdate, string location)
        {
            this.PUBLISHDATE = publishdate;
            this.FORECASTDATE = forecastdate;
            this.REPORTAREA = location;
        }
    }
}