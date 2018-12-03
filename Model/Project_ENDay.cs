using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 英文模板
    /// 旬、月模板
    /// </summary>
    public class Project_ENDay
    {
        //ID
        public int ID;
        //预报单编号
        public string reportNo;
        //发布时间
        public string publishTime;
        //预报标题
        public string reportTitle;
        //预报时间
        public string reportTime;
        //渤海、黄海北部预告
        public string reportNorth;
        //黄海中部、黄海南部
        public string reportSouth;
        //主预报员
        public string headReporter;
        //副预报员
        public string deputyReporter;
        //主、抄送机关
        public string sendDepartment;
        //发布预报时效
        public string publishEffect;
        //发布单位
        public string publishCompany;
    }
}