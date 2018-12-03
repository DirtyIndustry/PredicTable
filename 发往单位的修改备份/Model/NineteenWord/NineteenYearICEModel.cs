using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model.NineteenWord
{
    /// <summary>
    /// 19号年-渤海及黄海北部冰日预测
    /// </summary>
    public class NineteenYearICEModel
    {
        //主键
        public string ID { get; set; }
        //外键
        public string WID { get; set; }
       
        //区域
        public string NAME { get; set; }
        //初冰日
        public string FIRSTFROZENDAY { get; set; }
        //严重冰日
        public string SERIOUSICE { get; set; }
        //融冰日  
        public string ICEMELTINGDAY { get; set; }
        //终冰日
        public string ICEDISAPPDAY { get; set; }
    }
}