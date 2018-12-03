using System.Collections.Generic;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort2Comparer: Comparer<ModelAmShort2>
    {
        public override int Compare(ModelAmShort2 x, ModelAmShort2 y)
        {
            if (x.HTLHARBOUR == y.HTLHARBOUR && x.FORECASTDATE == y.FORECASTDATE)
            {
                return 0;
            }
            else if (x.HTLHARBOUR == "龙口港" && y.HTLHARBOUR == "黄河海港")
            {
                return -1;
            }
            else if (x.HTLHARBOUR == "黄河海港" && y.HTLHARBOUR == "龙口港")
            {
                return 1;
            }
            else
            {
                return x.FORECASTDATE.CompareTo(y.FORECASTDATE);
            }
        }
    }
}