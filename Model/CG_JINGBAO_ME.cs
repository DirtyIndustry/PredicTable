using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class CG_JINGBAO_ME
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
        /// 警报区域
        /// </summary>
        public string JBQUYU
        {
            get;
            set;
        }
        /// <summary>
        /// 警报内容
        /// </summary>
        public string JBNEIRONG
        {
            get;
            set;
        }
        /// <summary>
        /// 警报编号
        /// </summary>
        public string JBBIANHAO
        {
            get;
            set;
        }
        /// <summary>
        /// 警报级别
        /// </summary>
        public DateTime JBSHIJIAN
        {
            get;
            set;
        }
        /// <summary>
        /// 发布单位
        /// </summary>
        public string JBDANWEI
        {
            get;
            set;
        }
        /// <summary>
        /// 警报级别
        /// </summary>
        public string JBJIBIE
        {
            get;
            set;
        }
    }
}