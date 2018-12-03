using System;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort9
    {
        public DateTime PUBLISHDATE { get; set; } = DateTime.Now;
        public string SA24HWFBOHAIWAVEHEIGHT { get; set; } = "";
        public string SA24HWFNORTHOFYSWAVEHEIGHT { get; set; } = "";
        public string SA24HWFMIDDLEOFYSWAVEHEIGHT { get; set; } = "";
        public string SA24HWFSOUTHOFYSWAVEHEIGHT { get; set; } = "";
        public string SA24HWFOFFSHOREWAVEHEIGHT { get; set; } = "";
        public string SA24HWFOFFSHORESW { get; set; } = "";

        public ModelAmShort9() { }
        public ModelAmShort9(DateTime publishdate)
        {
            this.PUBLISHDATE = publishdate;
        }
    }
}