using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class CG_HT_JIECHUJINGBAO_CONTENT
    {
        /// <summary>
        /// 解除警报文件名
        /// </summary>
        public string JCJBWENJIANMING
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string CONTENT
        {
            get;
            set;
        }
        /// <summary>
        /// 发往
        /// </summary>
        public string SENTTO
        {
            get;
            set;
        }
        /// <summary>
        /// 图片
        /// </summary>
        public byte[] ISSUEPICTURE
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LINKMAN
        {
            get;
            set;
        }
        /// <summary>
        /// 解除警报标题
        /// </summary>
        public string JCTITLE
        {
            get;
            set;
        }

        public DateTime DATETIME
        {
            get;
            set;
        }
        public string CONTENTTABLE
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
        public string JCREMARKS { get; set; }
    }
}