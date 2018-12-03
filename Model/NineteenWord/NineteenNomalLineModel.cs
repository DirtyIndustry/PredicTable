using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model.NineteenWord
{
    public class NineteenNomalLineModel
    {
        public int ID { get; set; }
        public string WID { get; set; }
       
        //区域
        public string NAME { get; set; }
        //伏冰外缘线
        public string TERMINALLINE { get; set; }
        //一般冰厚
        public string GENERALICETHICKNESS { get; set; }
        //最大冰厚
        public string MAXICETHICKNESS { get; set; }
    }
}