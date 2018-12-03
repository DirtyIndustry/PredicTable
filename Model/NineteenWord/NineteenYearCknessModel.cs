using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model.NineteenWord
{
    /// <summary>
    /// 19号年-严重冰期沿岸主要港口与海岛平整冰厚度预测
    /// </summary>
    public class NineteenYearCknessModel
    {
        //主键
        public string ID { get; set; }
        //外键
        public string WID { get; set; }
      
        //区域
        public string NAME { get; set; }
        //一般冰厚
        public string GENERALICETHICKNESS { get; set; }
        //最大冰厚  
        public string MAXICETHICKNESS { get; set; }
    }
}