using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 英文模板
    /// 年模板
    /// </summary>
    public class Project_ENYear
    {
        //ID
        public int ID;
        //预报单编号
        public string reportNo;
        //发布时间
        public string publishTime;
        //预报标题
        public string reportTitle;
        //风暴潮
        public string stormSurge;
        //海浪
        public string seaWave;
        //赤潮
        public string redTide;
        //绿潮
        public string greebTide;
        //热带气旋
        public string tropicalCyclone;
        //主预报员
        public string headReporter;
        //副预报员
        public string deputyReporter;
        //主、抄送机关
        public string sendDepartment;
        //发布单位
        public string publishCompany;
    }
}