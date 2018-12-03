using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort5
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public string EFWWBHLOWESTWAVE { get; set; } = "";
        public string EFWWBHHIGHESTWAVE { get; set; } = "";
        public string EFWWBHWAVETYPE { get; set; } = "";
        public string EFWWBHNORTHLOWESTWAVE { get; set; } = "";
        public string EFWWBHNORTHHIGHESTWAVE { get; set; } = "";
        public string EFWWBHNORTHWAVETYPE { get; set; } = "";
        public string EFWWDKSEAAREAWAVEHEIGHT { get; set; } = "";
        public string EFWWDKSEAAREAWATERTEMPE { get; set; } = "";
        public string EFWWHHKSEAAREAWAVEHEIGHT { get; set; } = "";
        public string EFWWHHKSEAAREAWATERTEMP { get; set; } = "";
        public string EFWWGLGSEAAREAWAVEHEIGHT { get; set; } = "";
        public string EFWWGLGSEAAREAWATERTEMP { get; set; } = "";
        public string EFWWDYGWAVEHEIGHT { get; set; } = "";
        public string EFWWDYGWATERTEMPERATURE { get; set; } = "";
        public string EFWWXHWAVEHEIGHT { get; set; } = "";
        public string EFWWXHWATERTEMPERATURE { get; set; } = "";
        public string EFWWCKWAVEHEIGHT { get; set; } = "";
        public string EFWWCKWATERTEMPERATURE { get; set; } = "";

        public ModelAmShort5() { }
        public ModelAmShort5(DateTime publishdate)
        {
            this.PUBLISHDATE = publishdate;
        }
    }
}