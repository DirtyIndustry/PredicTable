using System.Collections.Generic;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShort6Comparer : Comparer<ModelAmShort6>
    {
        public override int Compare(ModelAmShort6 x, ModelAmShort6 y)
        {
            int valuex = 0;
            int valuey = 0;
            switch (x.TLFORECASTSTANCE)
            {
                case "小岛河":
                    valuex = 1;
                    break;
                case "孤东":
                    valuex = 2;
                    break;
                case "东营港":
                    valuex = 3;
                    break;
                case "桩西":
                    valuex = 4;
                    break;
                case "飞雁滩":
                    valuex = 5;
                    break;
                case "新户":
                    valuex = 6;
                    break;
                default:
                    break;
            }
            switch (y.TLFORECASTSTANCE)
            {
                case "小岛河":
                    valuey = 1;
                    break;
                case "孤东":
                    valuey = 2;
                    break;
                case "东营港":
                    valuey = 3;
                    break;
                case "桩西":
                    valuey = 4;
                    break;
                case "飞雁滩":
                    valuey = 5;
                    break;
                case "新户":
                    valuey = 6;
                    break;
                default: break;
            }
            return valuex.CompareTo(valuey);
        }
    }
}