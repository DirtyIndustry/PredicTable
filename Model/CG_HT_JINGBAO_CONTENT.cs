using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class CG_HT_JINGBAO_CONTENT
    {
        /// <summary>
        /// 警报文件名
        /// </summary>
        public string JBWENJIANMING
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
        public byte[] PICTURE
        {
            get;
            set;
        }
        /// <summary>
        /// 表格
        /// </summary>
        public string CONTENTTABLE
        {
            get;
            set;
        }
        /// <summary>
        /// 签发图
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
        /// 警报标题
        /// </summary>
        public string JBTITLE
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
        //海浪警报图片
        public byte[] WAVEJBIMG { get; set; }
        //警报备注
        public string JBREMARKS { get; set; }
    }
}