using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class CG_JIECHUJINGBAO_FILE
    {
        /// <summary>
        /// 解除文件名
        /// </summary>
        public string JCWENJIANMING
        {
            get;
            set;
        }
        /// <summary>
        /// 解除警报文件内容
        /// </summary>
        public byte[] JCNEIRONG
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