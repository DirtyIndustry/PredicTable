using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//明泽闸潮位
		public class TBLMZZTIDELEVEL
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
		private string _mzztlfirstwaveoftime;
        public string MZZTLFIRSTWAVEOFTIME
        {
            get{ return _mzztlfirstwaveoftime; }
            set{ _mzztlfirstwaveoftime = value; }
        }        
		/// <summary>
		/// 第一次高潮潮位
        /// </summary>		
		private string _mzztlfirstwavetidelevel;
        public string MZZTLFIRSTWAVETIDELEVEL
        {
            get{ return _mzztlfirstwavetidelevel; }
            set{ _mzztlfirstwavetidelevel = value; }
        }        
		/// <summary>
		/// 第一次低潮时间
        /// </summary>		
		private string _mzztlfirsttimelowtide;
        public string MZZTLFIRSTTIMELOWTIDE
        {
            get{ return _mzztlfirsttimelowtide; }
            set{ _mzztlfirsttimelowtide = value; }
        }        
		/// <summary>
		/// 第一次低潮潮位
        /// </summary>		
		private string _mzztllowtidelevelforthefirstti;
        public string MZZTLLOWTIDELEVELFORTHEFIRSTTI
        {
            get{ return _mzztllowtidelevelforthefirstti; }
            set{ _mzztllowtidelevelforthefirstti = value; }
        }        
		/// <summary>
		/// 第二次高潮时间
        /// </summary>		
		private string _mzztlsecondwaveoftime;
        public string MZZTLSECONDWAVEOFTIME
        {
            get{ return _mzztlsecondwaveoftime; }
            set{ _mzztlsecondwaveoftime = value; }
        }        
		/// <summary>
		/// 第二次高潮潮位
        /// </summary>		
		private string _mzztlsecondwavetidelevel;
        public string MZZTLSECONDWAVETIDELEVEL
        {
            get{ return _mzztlsecondwavetidelevel; }
            set{ _mzztlsecondwavetidelevel = value; }
        }        
		/// <summary>
		/// 第二次低潮时间
        /// </summary>		
		private string _mzztlsecondtimelowtide;
        public string MZZTLSECONDTIMELOWTIDE
        {
            get{ return _mzztlsecondtimelowtide; }
            set{ _mzztlsecondtimelowtide = value; }
        }        
		/// <summary>
		/// 第二次低潮潮位
        /// </summary>		
		private string _mzztllowtidelevelforthesecondt;
        public string MZZTLLOWTIDELEVELFORTHESECONDT
        {
            get{ return _mzztllowtidelevelforthesecondt; }
            set{ _mzztllowtidelevelforthesecondt = value; }
        }        
		   
	}


