using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model.NewMediumAndLong
{
    /// <summary>
    /// 中长期参数类
    /// </summary>
    public class ZCQHBParamModel
    {
        public string ForcastArea { get; set; }
        public string FilePath { get; set; }
        public string FileMessage { get; set; }
    }

    public class HBParamStaticList {
        public static List<ZCQHBParamModel> HBParamList = null;
        //public static Dictionary<string,string> obj;

    }
}