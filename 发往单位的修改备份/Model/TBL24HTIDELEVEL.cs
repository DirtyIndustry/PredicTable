using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//24小时潮位
		public class TBL24HTIDELEVEL
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
		/// 预报站位
        /// </summary>		
		private string _tlforecaststance;
        public string TLFORECASTSTANCE
        {
            get{ return _tlforecaststance; }
            set{ _tlforecaststance = value; }
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
		private string _tlfirstwaveoftime;
        public string TLFIRSTWAVEOFTIME
        {
            get{ return _tlfirstwaveoftime; }
            set{ _tlfirstwaveoftime = value; }
        }        
		/// <summary>
		/// 第一次高潮潮位
        /// </summary>		
		private string _tlfirstwavetidelevel;
        public string TLFIRSTWAVETIDELEVEL
        {
            get{ return _tlfirstwavetidelevel; }
            set{ _tlfirstwavetidelevel = value; }
        }        
		/// <summary>
		/// 第一次低潮时间
        /// </summary>		
		private string _tlfirsttimelowtide;
        public string TLFIRSTTIMELOWTIDE
        {
            get{ return _tlfirsttimelowtide; }
            set{ _tlfirsttimelowtide = value; }
        }        
		/// <summary>
		/// 第一次低潮潮位
        /// </summary>		
		private string _tllowtidelevelforthefirsttime;
        public string TLLOWTIDELEVELFORTHEFIRSTTIME
        {
            get{ return _tllowtidelevelforthefirsttime; }
            set{ _tllowtidelevelforthefirsttime = value; }
        }        
		/// <summary>
		/// 第二次高潮时间
        /// </summary>		
		private string _tlsecondwaveoftime;
        public string TLSECONDWAVEOFTIME
        {
            get{ return _tlsecondwaveoftime; }
            set{ _tlsecondwaveoftime = value; }
        }        
		/// <summary>
		/// 第二次高潮潮位
        /// </summary>		
		private string _tlsecondwavetidelevel;
        public string TLSECONDWAVETIDELEVEL
        {
            get{ return _tlsecondwavetidelevel; }
            set{ _tlsecondwavetidelevel = value; }
        }        
		/// <summary>
		/// 第二次低潮时间
        /// </summary>		
		private string _tlsecondtimelowtide;
        public string TLSECONDTIMELOWTIDE
        {
            get{ return _tlsecondtimelowtide; }
            set{ _tlsecondtimelowtide = value; }
        }        
		/// <summary>
		/// 第二次低潮潮位
        /// </summary>		
		private string _tllowtidelevelforthesecondtime;
        public string TLLOWTIDELEVELFORTHESECONDTIME
        {
            get{ return _tllowtidelevelforthesecondtime; }
            set{ _tllowtidelevelforthesecondtime = value; }
        }        
		   
	}


