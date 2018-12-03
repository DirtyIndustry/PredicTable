using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 预报员信息
    /// </summary>
    public class ReporterModel
    {
        //ID
        public int ID { get; set; }
        //预报员姓名
        public string ReporterName { get; set;}
        //预报员编号
        public string ReporterCode { get; set; }
        //预报员类型
        public string ReporterType { get; set; }
        //预报员电话
        public string ReporterTel { get; set; }
    }
}