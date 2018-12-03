//变更记录实体类  ：Add  by yy in 2018-04-17
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 变更记录实体类
    /// </summary>
    public class ChangeRecordModel
    {
        //变更记录ID
        public string ID { get; set; }
        //变更记录内容
        public string ChangeContent { get; set; }
        //变更人
        public string ChangePerson { get; set; }
        //变更记录添加时间
        public DateTime ChangeTime { get; set; }

    }
}