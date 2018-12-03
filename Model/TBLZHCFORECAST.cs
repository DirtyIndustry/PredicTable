using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class TBLZHCFORECAST
    {
        /// <summary>
        /// 填报日期
        /// </summary>		
        private DateTime _PUBLISHDATE;
        public DateTime PUBLISHDATE
        {
            get { return _PUBLISHDATE; }
            set { _PUBLISHDATE = value; }
        }
        /// <summary>
        /// 预报日期
        /// </summary>		
        private DateTime _FORECASTDATE;
        public DateTime FORECASTDATE
        {
            get { return _FORECASTDATE; }
            set { _FORECASTDATE = value; }
        }


        /// <summary>
        /// 海区
        /// </summary>		
        private string _SEAAREA;
        public string SEAAREA
        {
            get { return _SEAAREA; }
            set { _SEAAREA = value; }
        }
        /// <summary>
        /// 天气现象
        /// </summary>		
        private string _WEATHERAPPEARANCE;
        public string WEATHERAPPEARANCE
        {
            get { return _WEATHERAPPEARANCE; }
            set { _WEATHERAPPEARANCE = value; }
        }
        /// <summary>
        /// 风向
        /// </summary>		
        private string _WINDDIRECTION;
        public string WINDDIRECTION
        {
            get { return _WINDDIRECTION; }
            set { _WINDDIRECTION = value; }
        }
        /// <summary>
        /// 风力
        /// </summary>		
        private string _WINDFORCE;
        public string WINDFORCE
        {
            get { return _WINDFORCE; }
            set { _WINDFORCE = value; }
        }
        /// <summary>
        /// 波高
        /// </summary>		
        private string _WAVEHEIGHT;
        public string WAVEHEIGHT
        {
            get { return _WAVEHEIGHT; }
            set { _WAVEHEIGHT = value; }
        }
        /// <summary>
        /// 波向
        /// </summary>		
        private string _WAVEDIRECTION;
        public string WAVEDIRECTION
        {
            get { return _WAVEDIRECTION; }
            set { _WAVEDIRECTION = value; }
        }

    }
}