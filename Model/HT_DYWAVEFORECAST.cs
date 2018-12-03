using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
    /// </summary>
    public class HT_DYWAVEFORECAST
    {
        /// <summary>
		/// 填报日期
        /// </summary>		
        public DateTime PUBLISHDATE
        {
            get;
            set;
        }
        /// <summary>
        /// 预报时效（12、24、48、72小时）
        /// </summary>	
        public string TIMEEFFECTIVE
        {
            get;
            set;
        }
        /// <summary>
        /// 风向
        /// </summary>
        public string WINDDIRECTION
        {
            get;
            set;
        }
        /// <summary>
        /// 风力
        /// </summary>
        public string WINDFORCE
        {
            get;
            set;
        }
        /// <summary>
        /// 浪高
        /// </summary>
        public string WAVEHEIGHT
        {
            get;
            set;
        }
    }
}