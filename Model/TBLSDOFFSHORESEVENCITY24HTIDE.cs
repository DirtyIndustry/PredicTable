using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//山东省近海七市24小时潮汐
    public class TBLSDOFFSHORESEVENCITY24HTIDE
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
            get { return _forecastdate; }
            set { _forecastdate = value; }
        }
        /// <summary>
        /// 地市
        /// </summary>		
        private string _sdosctcity;
        public string SDOSCTCITY
        {
            get{ return _sdosctcity; }
            set{ _sdosctcity = value; }
        }        
		/// <summary>
		/// 第一次高潮时
        /// </summary>		
		private string _sdosctfirsthighwavehour;
        public string SDOSCTFIRSTHIGHWAVEHOUR
        {
            get{ return _sdosctfirsthighwavehour; }
            set{ _sdosctfirsthighwavehour = value; }
        }        
		/// <summary>
		/// 第一次高潮分
        /// </summary>		
		private string _sdosctfirsthighwaveminute;
        public string SDOSCTFIRSTHIGHWAVEMINUTE
        {
            get{ return _sdosctfirsthighwaveminute; }
            set{ _sdosctfirsthighwaveminute = value; }
        }        
		/// <summary>
		/// 第二次高潮时
        /// </summary>		
		private string _sdosctsecondhighwavehour;
        public string SDOSCTSECONDHIGHWAVEHOUR
        {
            get{ return _sdosctsecondhighwavehour; }
            set{ _sdosctsecondhighwavehour = value; }
        }        
		/// <summary>
		/// 第二次高潮分
        /// </summary>		
		private string _sdosctsecondhighwaveminute;
        public string SDOSCTSECONDHIGHWAVEMINUTE
        {
            get{ return _sdosctsecondhighwaveminute; }
            set{ _sdosctsecondhighwaveminute = value; }
        }        
		/// <summary>
		/// 第一次低潮时
        /// </summary>		
		private string _sdosctfirstlowwavehour;
        public string SDOSCTFIRSTLOWWAVEHOUR
        {
            get{ return _sdosctfirstlowwavehour; }
            set{ _sdosctfirstlowwavehour = value; }
        }        
		/// <summary>
		/// 第一次低潮分
        /// </summary>		
		private string _sdosctfirstlowwaveminute;
        public string SDOSCTFIRSTLOWWAVEMINUTE
        {
            get{ return _sdosctfirstlowwaveminute; }
            set{ _sdosctfirstlowwaveminute = value; }
        }        
		/// <summary>
		/// 第二次低潮时
        /// </summary>		
		private string _sdosctsecondlowwavehour;
        public string SDOSCTSECONDLOWWAVEHOUR
        {
            get{ return _sdosctsecondlowwavehour; }
            set{ _sdosctsecondlowwavehour = value; }
        }        
		/// <summary>
		/// 第二次低潮分
        /// </summary>		
		private string _sdosctsecondlowwaveminute;
        public string SDOSCTSECONDLOWWAVEMINUTE
        {
            get{ return _sdosctsecondlowwaveminute; }
            set{ _sdosctsecondlowwaveminute = value; }
        }        
		   
	}


