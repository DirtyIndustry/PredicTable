using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//港口潮位
		public class TBLHARBOURTIDELEVEL
	{
   		     
      	/// <summary>
		/// 填报日期
        /// </summary>		
		private DateTime _publishdate;
        public DateTime PUBLISHDATE
        {
            get{ return _publishdate; }
            set{ _publishdate = value; }
        }        
		/// <summary>
		/// 港口
        /// </summary>		
		private string _htlharbour;
        public string HTLHARBOUR
        {
            get{ return _htlharbour; }
            set{ _htlharbour = value; }
        }        
		/// <summary>
		/// 预报日期
        /// </summary>		
		private DateTime _forecastdate;
        public DateTime FORECASTDATE
        {
            get{ return _forecastdate; }
            set{ _forecastdate = value; }
        }        
		/// <summary>
		/// 第一次高潮时间
        /// </summary>		
		private string _htlfirstwaveoftime;
        public string HTLFIRSTWAVEOFTIME
        {
            get{ return _htlfirstwaveoftime; }
            set{ _htlfirstwaveoftime = value; }
        }        
		/// <summary>
		/// 第一次高潮潮位
        /// </summary>		
		private string _htlfirstwavetidelevel;
        public string HTLFIRSTWAVETIDELEVEL
        {
            get{ return _htlfirstwavetidelevel; }
            set{ _htlfirstwavetidelevel = value; }
        }        
		/// <summary>
		/// 第一次低潮时间
        /// </summary>		
		private string _htlfirsttimelowtide;
        public string HTLFIRSTTIMELOWTIDE
        {
            get{ return _htlfirsttimelowtide; }
            set{ _htlfirsttimelowtide = value; }
        }        
		/// <summary>
		/// 第一次低潮潮位
        /// </summary>		
		private string _htllowtidelevelforthefirsttime;
        public string HTLLOWTIDELEVELFORTHEFIRSTTIME
        {
            get{ return _htllowtidelevelforthefirsttime; }
            set{ _htllowtidelevelforthefirsttime = value; }
        }        
		/// <summary>
		/// 第二次高潮时间
        /// </summary>		
		private string _htlsecondwaveoftime;
        public string HTLSECONDWAVEOFTIME
        {
            get{ return _htlsecondwaveoftime; }
            set{ _htlsecondwaveoftime = value; }
        }        
		/// <summary>
		/// 第二次高潮潮位
        /// </summary>		
		private string _htlsecondwavetidelevel;
        public string HTLSECONDWAVETIDELEVEL
        {
            get{ return _htlsecondwavetidelevel; }
            set{ _htlsecondwavetidelevel = value; }
        }        
		/// <summary>
		/// 第二次低潮时间
        /// </summary>		
		private string _htlsecondtimelowtide;
        public string HTLSECONDTIMELOWTIDE
        {
            get{ return _htlsecondtimelowtide; }
            set{ _htlsecondtimelowtide = value; }
        }        
		/// <summary>
		/// 第二次低潮潮位
        /// </summary>		
		private string _htllowtidelevelforthesecondtim;
        public string HTLLOWTIDELEVELFORTHESECONDTIM
        {
            get{ return _htllowtidelevelforthesecondtim; }
            set{ _htllowtidelevelforthesecondtim = value; }
        }        
		   
	}


