//导出数据的实体类  add by yy on 2018-04-16
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 导出数据的实体类 
    /// </summary>
    public class ExportDataModel
    {
        // 预报时段（上/下午）
        public string YBSD { get; set; }
        //数据类型（订正前/后）
        public string SJLX { get; set; }
        //预报海区
        public string YBHQ { get; set; }
        //预报时效（24/48/72）
        public string YBSX { get; set; }
        //文件格式（txt/excel）
        public string WJGS { get; set; }
        //预报要素(浪高/浪向/..)
        public string[] YBYSARR { get; set; }
        //开始时间
        public DateTime TBSJS { get; set; }
        //结束时间
        public DateTime TBSJE { get; set; }
    }
}