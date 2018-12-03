using System;
using System.Web;

namespace PredicTable.Model
{
    public class CG_JINGBAO_FILE
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
        /// 警报文件内容
        /// </summary>
        public byte[] JBNEIRONG
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
