using System.Collections.Generic;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort1Comparer: Comparer<ModelAmShort1>
    {
        public override int Compare(ModelAmShort1 x, ModelAmShort1 y)
        {
            if (x.REPORTAREA == y.REPORTAREA && x.FORECASTDATE == y.FORECASTDATE)
            {
                return 0;
            }
            else if (x.REPORTAREA == "渤海" && y.REPORTAREA == "黄河海港")
            {
                return -1;
            }
            else if (x.REPORTAREA == "黄河海港" && y.REPORTAREA == "渤海")
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