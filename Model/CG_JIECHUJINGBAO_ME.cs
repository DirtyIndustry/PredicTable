using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class CG_JIECHUJINGBAO_ME
    {
        /// <summary>
        /// 解除警报文件名
        /// </summary>
        public string JCWENJIANMING
        {
            get;
            set;
        }
        /// <summary>
        /// 解除警报区域
        /// </summary>
        public string JCQUYU
        {
            get;
            set;
        }
        /// <summary>
        /// 解除警报内容
        /// </summary>
        public string JCNEIRONG
        {
            get;
            set;
        }
        /// <summary>
        /// 解除警报编号
        /// </summary>
        public string JCBIANHAO
        {
            get;
            set;
        }
        /// <summary>
        /// 解除警报级别
        /// </summary>
        public DateTime JCSHIJIAN
        {
            get;
            set;
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string JCJIBIE
        {
            get;
            set;
        }
        /// <summary>
        /// 发布单位
        /// </summary>
        public string JCDANWEI
        {
            get;
            set;
        }
        
    }
}