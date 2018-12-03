using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class CG_XIAOXI_FILE
    {
        /// <summary>
        /// 消息文件名
        /// </summary>
        public string XXWENJIANMING
        {
            get;
            set;
        }
        /// <summary>
        /// 消息文件内容
        /// </summary>
        public byte[] XXNEIRONG
        {
            get;
            set;
        }
        /// <summary>
        /// 图片文件
        /// </summary>
        public byte[] PICFILE
        {
            get;
            set;
        }
    }
}