using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//金沙滩24小时潮汐预报
		public class TBLGOLDBEACH24HTIDALFORECAST
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
		/// 第一次高潮时
        /// </summary>		
		private string _gb24htffirsthighwavehour;
        public string GB24HTFFIRSTHIGHWAVEHOUR
        {
            get{ return _gb24htffirsthighwavehour; }
            set{ _gb24htffirsthighwavehour = value; }
        }        
		/// <summary>
		/// 第一次高潮分
        /// </summary>		
		private string _gb24htffirsthighwaveminute;
        public string GB24HTFFIRSTHIGHWAVEMINUTE
        {
            get{ return _gb24htffirsthighwaveminute; }
            set{ _gb24htffirsthighwaveminute = value; }
        }        
		/// <summary>
		/// 第一次低潮时
        /// </summary>		
		private string _gb24htffirstlowwavehour;
        public string GB24HTFFIRSTLOWWAVEHOUR
        {
            get{ return _gb24htffirstlowwavehour; }
            set{ _gb24htffirstlowwavehour = value; }
        }        
		/// <summary>
		/// 第一次低潮分
        /// </summary>		
		private string _gb24htffirstlowwaveminute;
        public string GB24HTFFIRSTLOWWAVEMINUTE
        {
            get{ return _gb24htffirstlowwaveminute; }
            set{ _gb24htffirstlowwaveminute = value; }
        }        
		/// <summary>
		/// 第二次高潮时
        /// </summary>		
		private string _gb24htfsecondhighwavehour;
        public string GB24HTFSECONDHIGHWAVEHOUR
        {
            get{ return _gb24htfsecondhighwavehour; }
            set{ _gb24htfsecondhighwavehour = value; }
        }        
		/// <summary>
		/// 第二次高潮分
        /// </summary>		
		private string _gb24htfsecondhighwaveminute;
        public string GB24HTFSECONDHIGHWAVEMINUTE
        {
            get{ return _gb24htfsecondhighwaveminute; }
            set{ _gb24htfsecondhighwaveminute = value; }
        }        
		/// <summary>
		/// 第二次低潮时
        /// </summary>		
		private string _gb24htfsecondlowwavehour;
        public string GB24HTFSECONDLOWWAVEHOUR
        {
            get{ return _gb24htfsecondlowwavehour; }
            set{ _gb24htfsecondlowwavehour = value; }
        }        
		/// <summary>
		/// 第二次低潮分
        /// </summary>		
		private string _gb24htfsecondlowwaveminute;
        public string GB24HTFSECONDLOWWAVEMINUTE
        {
            get{ return _gb24htfsecondlowwaveminute; }
            set{ _gb24htfsecondlowwaveminute = value; }
        }
    /// <summary>
    /// 海区
    /// </summary>		
    private string _seabeach;
    public string SEABEACH
    {
        get { return _seabeach; }
        set { _seabeach = value; }
    }
    
}


