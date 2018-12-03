using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model.NewMediumAndLong
{
    /// <summary>
    /// 中长期旬、月实体
    /// </summary>
    public class ReportMonthModel
    {
        public string PUBLISHTIME { get; set; }
        public string PUBLISHCOMPANY { get; set; }
        public string REPORTNO { get; set; }
        public string REPORTTITLE { get; set; }
        public string REPORTNORTH { get; set; }
        public string REPORTSOUTH { get; set; }
        public string REPORTCONTENT { get; set; }
        public string HEADREPORTER { get; set; }
        public string DEPUTYREPORTER { get; set; }
        public string DOCNAME { get; set; }
        public string SENDDEPARTMENT { get; set; }
        public string REPORTTYPE { get; set; }
    }
}