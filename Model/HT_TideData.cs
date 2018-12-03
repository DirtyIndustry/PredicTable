using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 潮高数据
    /// </summary>
    public class HT_TideData
    {
        /// <summary>
		/// 填报日期
        /// </summary>	
        public DateTime PUBLISHDATE
        {
            get;set;
        }
        public DateTime FORECASTDATE
        {
            get;
            set;
        }
        /// <summary>
        /// 地市
        /// </summary>
        public string SDOSCTCITY
        {
            get; set;
        }
        /// <summary>
        /// 第一次高潮潮高
        /// </summary>
        public string FIRSTHIGHWAVETIDEDATA
        {
            get; set;
        }
        /// <summary>
        /// 第一次低潮潮高
        /// </summary>
        public string FIRSTLOWWAVETIDEDATA
        {
            get; set;
        }
        /// <summary>
        /// 第二次高潮潮高
        /// </summary>
        public string SECONDHIGHWAVETIDEDATA
        {
            get; set;
        }
        /// <summary>
        /// 第二次低潮潮高
        /// </summary>
        public string SECONDLOWWAVETIDEDATA
        {
            get; set;
        }
    }
}