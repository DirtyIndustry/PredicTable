using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model.NineteenWord
{
    /// <summary>
    /// 19号年-浮冰外缘线离岸最大距离及平整冰厚度预测
    /// </summary>
    public class NineteenYearLineModel
    {
        //主键
        public string ID { get; set; }
        //外键
        public string WID { get; set; }
      
        //区域
        public string NAME { get; set; }
        //浮冰外缘线
        public string TERMINALLINE { get; set; }
        //一般冰厚
        public string GENERALICETHICKNESS { get; set; }
        //最大冰厚  
        public string MAXICETHICKNESS { get; set; }
    }
}