using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class CG_HT_JINGBAO_TABLE
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
        /// 站位
        /// </summary>
        public string STATION
        {
            get;
            set;
        }
        /// <summary>
        /// 高潮时
        /// </summary>
        public string HIGHTIDETIME
        {
            get;
            set;
        }
        /// <summary>
        /// 预报时间
        /// </summary>
        public DateTime FORECASTDATE
        {
            get;
            set;
        }
        /// <summary>
        /// 高潮值
        /// </summary>
        public string HIGHTIDEVALUE
        {
            get;
            set;
        }
        /// <summary>
        /// 警戒潮位
        /// </summary>
        public string WARNINGVALUE
        {
            get;
            set;
        }
        /// <summary>
        /// 警报级别
        /// </summary>
        public string WARNINGLEVEL
        {
            get;
            set;
        }
        
    }
}