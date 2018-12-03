using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 东营埕岛-未来三天高/低潮预报
    /// </summary>
    public class HT_DYTIDEFORECAST
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
        /// 预报日期
        /// </summary>	
        public DateTime FORECASTDATE
        {
            get;
            set;
        }
        /// <summary>
        /// 第一次高潮潮时
        /// </summary>		
        public string NOTFFIRSTHIGHWAVETIME
        {
            get;
            set;
        }
        /// <summary>
        /// 第一次高潮潮位
        /// </summary>	
        public string NOTFFIRSTHIGHWAVEHEIGHT
        {
            get;
            set;
        }
        /// <summary>
        /// 第一次低潮潮时
        /// </summary>	
        public string NOTFFIRSTLOWWAVETIME
        {
            get;
            set;
        }
        /// <summary>
        /// 第一次低潮潮位
        /// </summary>	
        public string NOTFFIRSTLOWWAVEHEIGHT
        {
            get;
            set;
        }
        /// <summary>
        /// 第二次高潮潮时
        /// </summary>		
        public string NOTFSECONDHIGHWAVETIME
        {
            get;
            set;
        }
        /// <summary>
        /// 第二次高潮潮位
        /// </summary>		
        public string NOTFSECONDHIGHWAVEHEIGHT
        {
            get;
            set;
        }
        /// <summary>
        /// 第二次低潮潮时
        /// </summary>		
        public string NOTFSECONDLOWWAVETIME
        {
            get;
            set;
        }
        /// <summary>
        /// 第二次低潮潮位
        /// </summary>	
        public string NOTFSECONDLOWWAVEHEIGHT
        {
            get;
            set;
        }
    }
}