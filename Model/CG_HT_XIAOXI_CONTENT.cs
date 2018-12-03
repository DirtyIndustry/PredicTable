using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class CG_HT_XIAOXI_CONTENT
    {
        public string XXWENJIANMING
        {
            get;
            set;
        }
        public string CONTENT
        {
            get;
            set;
        }
        public string SENTTO
        {
            get;
            set;
        }
            
        public byte[] ISSUEPICTURE
        {
            get;
            set;
        }
        public string LINKMAN
        {
            get;
            set;
        }
        public string XXTITLE
        {
            get;
            set;
        }
        public DateTime DATETIME
        {
            get;
            set;
        }
        //冰情概况
        public string ICESITUATION { get; set; }
        //预计
        public string PREDICT { get; set; }
        //说明
        public string DESCRIPTION { get; set; }
        //电话
        public string IPHONE { get; set; }
        //传真
        public string CHUANZHEN { get; set; }
    }
}