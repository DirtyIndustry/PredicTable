using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    //下午二、山东省近海七市3天海浪、水温预报
    public class TBLSDOFFSHORESEVENCITY3DWAVE
    {
        /// <summary>
		/// 填报日期
        /// </summary>		
        public DateTime PUBLISHDATE
        {
            get;set;
        }
        /// <summary>
        /// 地区
        /// </summary>	
        public string SDOSCWAREA
        {
            get; set;
        }
        /// <summary>
        /// 时效
        /// </summary>	
        public string EFFECTIVE
        {
            get; set;
        }
        /// <summary>
        /// 浪高24小时
        /// </summary>		
        public string SDOSCWHIGHTESTWAVEHEIGHT24H
        {
            get; set;
        }
        /// <summary>
        /// 浪高48小时
        /// </summary>		
        public string SDOSCWHIGHTESTWAVEHEIGHT48H
        {
            get; set;
        }
        /// <summary>
        /// 浪高72小时
        /// </summary>		
        public string SDOSCWHIGHTESTWAVEHEIGHT72H
        {
            get; set;
        }
        /// <summary>
        /// 表层水温24小时
        /// </summary>
        public string SDOSCWSURFACETEMPERATURE24H
        {
            get; set;
        }
        /// <summary>
        /// 表层水温48小时
        /// </summary>
        public string SDOSCWSURFACETEMPERATURE48H
        {
            get; set;
        }
        /// <summary>
        /// 表层水温72小时
        /// </summary>
        public string SDOSCWSURFACETEMPERATURE72H
        {
            get; set;
        }
    }
}