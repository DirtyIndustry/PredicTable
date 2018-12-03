using System;

namespace PredicTable.WebServiceClass
{
    public class ModelPublishMetaInfo
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public string PUBLISHHOUR { get; set; } = ""; //"16";
        public string FRELEASEUNIT { get; set; } = ""; //"国家海洋局北海预报中心";
        public string FTELEPHONE { get; set; } = ""; // "0532-58750688";
        public string FFAX { get; set; } = ""; // "0532-58750682";
        public string FWAVEFORECASTER { get; set; } = ""; // "HBY2006007";
        public string FTIDALFORECASTER { get; set; } = ""; // "HBY2006030";
        public string FWATERTEMPERATUREFORECASTER { get; set; } = ""; // "HBY2006009";
        public string FWAVEFORECASTERTEL { get; set; } = ""; // "13864864761";
        public string FTIDALFORECASTERTEL { get; set; } = ""; // "053258750619";
        public string FWATERTEMPERATUREFORECASTERTEL { get; set; } = ""; // "053258750619";
        public string ZHIBANTEL { get; set; } = ""; // "0532-58750688";
        public string SENDTEL { get; set; } = ""; // "0532-58750626";

        public ModelPublishMetaInfo() { }
        public ModelPublishMetaInfo(DateTime publishdate)
        {
            this.PUBLISHDATE = publishdate;
        }
    }
}