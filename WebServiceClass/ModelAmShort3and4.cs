using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort3and4
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public string METEOROLOGICALREVIEW { get; set; } = "";
        public string METEOROLOGICALREVIEW24HOUR { get; set; } = "";
        public string METEOROLOGICALREVIEW7DAYS { get; set; } = "";
        public string METEOROLOGICALREVIEWCX { get; set; } = "";
        public string METEOROLOGICALREVIEW24HOURCX { get; set; } = "";
        public string METEOROLOGICALREVIEW7DAYSCX { get; set; } = "";

        public ModelAmShort3and4() { }
        public ModelAmShort3and4(DateTime publishdate)
        {
            this.PUBLISHDATE = publishdate;
        }
    }
}