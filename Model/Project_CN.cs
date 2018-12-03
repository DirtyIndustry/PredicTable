using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 中文模板公共类
    /// </summary>
    public class Project_CN
    {
        //ID
        public int ID;
        //发布时间
        public string pbtime;
        //预报时间
        public string ybtime;
        //预报内容
        public string ybcontent;
        //主预报员
        public string headReporter;
        //副预报员
        public string deputyReporter;
        //主、抄送机关
        public string sendDepartment;
    }
}