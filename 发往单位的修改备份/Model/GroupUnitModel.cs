using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 发送单位分组Model
    /// </summary>
    public class GroupUnitModel
    {
        //主键
        public string ID { get; set; }
        //分组名称
        public string GROUPNAME { get; set; }
        //发送单位名称
        public string UNITNAME { get; set; }
        //创建时间
        public DateTime CREATETIME { get; set; }
    }
}