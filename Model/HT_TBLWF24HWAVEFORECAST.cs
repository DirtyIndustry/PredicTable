using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class HT_TBLWF24HWAVEFORECAST
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
        /// 渤海浪高
        /// </summary>
        public string SA24HWFBOHAIWAVEHEIGHT
        {
            get;
            set;
        }
        /// <summary>
        /// 黄海北部浪高
        /// </summary>
        public string SA24HWFNORTHOFYSWAVEHEIGHT
        {
            get;
            set;
        }
        /// <summary>
        /// 黄海中部浪高
        /// </summary>
        public string SA24HWFMIDDLEOFYSWAVEHEIGHT
        {
            get;
            set;

        }

        /// <summary>
        /// 黄海南部浪高
        /// </summary>
        public string SA24HWFSOUTHOFYSWAVEHEIGHT
        {
            get;
            set;

        }
        /// <summary>
        ///  潍坊近岸浪高
        /// </summary>
        public string SA24HWFOFFSHOREWAVEHEIGHT
        {
            get;
            set;
        }
        /// <summary>
        ///  潍坊近岸水温
        /// </summary>
        public string SA24HWFOFFSHORESW
        {
            get;
            set;
        }
    }
}