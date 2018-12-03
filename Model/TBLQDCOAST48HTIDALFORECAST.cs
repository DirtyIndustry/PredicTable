using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//青岛沿岸48小时潮汐预报
		public class TBLQDCOAST48HTIDALFORECAST
	{
   		     
      	/// <summary>
		/// 填报时间
        /// </summary>		
		private DateTime _publishdate;
        public DateTime PUBLISHDATE
        {
            get{ return _publishdate; }
            set{ _publishdate = value; }
        }        
		/// <summary>
		/// 第一次高潮时
        /// </summary>		
		private string _qdc48htffirsthighwavehour;
        public string QDC48HTFFIRSTHIGHWAVEHOUR
        {
            get{ return _qdc48htffirsthighwavehour; }
            set{ _qdc48htffirsthighwavehour = value; }
        }        
		/// <summary>
		/// 第一次高潮分
        /// </summary>		
		private string _qdc48htffirsthghwaveminute;
        public string QDC48HTFFIRSTHGHWAVEMINUTE
        {
            get{ return _qdc48htffirsthghwaveminute; }
            set{ _qdc48htffirsthghwaveminute = value; }
        }        
		/// <summary>
		/// 第一次高潮潮高
        /// </summary>		
		private string _qdc48htffirsthighwaveheight;
        public string QDC48HTFFIRSTHIGHWAVEHEIGHT
        {
            get{ return _qdc48htffirsthighwaveheight; }
            set{ _qdc48htffirsthighwaveheight = value; }
        }        
		/// <summary>
		/// 第二次高潮时
        /// </summary>		
		private string _qdc48htfsecondhighwavehour;
        public string QDC48HTFSECONDHIGHWAVEHOUR
        {
            get{ return _qdc48htfsecondhighwavehour; }
            set{ _qdc48htfsecondhighwavehour = value; }
        }        
		/// <summary>
		/// 第二次高潮分
        /// </summary>		
		private string _qdc48htfsecondhighwaveminute;
        public string QDC48HTFSECONDHIGHWAVEMINUTE
        {
            get{ return _qdc48htfsecondhighwaveminute; }
            set{ _qdc48htfsecondhighwaveminute = value; }
        }        
		/// <summary>
		/// 第二次高潮潮高
        /// </summary>		
		private string _qdc48htfsecondhighwaveheight;
        public string QDC48HTFSECONDHIGHWAVEHEIGHT
        {
            get{ return _qdc48htfsecondhighwaveheight; }
            set{ _qdc48htfsecondhighwaveheight = value; }
        }        
		/// <summary>
		/// 第一次低潮时
        /// </summary>		
		private string _qdc48htffirstlowwavehour;
        public string QDC48HTFFIRSTLOWWAVEHOUR
        {
            get{ return _qdc48htffirstlowwavehour; }
            set{ _qdc48htffirstlowwavehour = value; }
        }        
		/// <summary>
		/// 第一次低潮分
        /// </summary>		
		private string _qdc48htffirstlowwaveminute;
        public string QDC48HTFFIRSTLOWWAVEMINUTE
        {
            get{ return _qdc48htffirstlowwaveminute; }
            set{ _qdc48htffirstlowwaveminute = value; }
        }        
		/// <summary>
		/// 第一次低潮潮高
        /// </summary>		
		private string _qdc48htffirstlowwaveheight;
        public string QDC48HTFFIRSTLOWWAVEHEIGHT
        {
            get{ return _qdc48htffirstlowwaveheight; }
            set{ _qdc48htffirstlowwaveheight = value; }
        }        
		/// <summary>
		/// 第二次低潮时
        /// </summary>		
		private string _qdc48htfsecondlowwavehour;
        public string QDC48HTFSECONDLOWWAVEHOUR
        {
            get{ return _qdc48htfsecondlowwavehour; }
            set{ _qdc48htfsecondlowwavehour = value; }
        }        
		/// <summary>
		/// 第二次低潮分
        /// </summary>		
		private string _qdc48htfsecondlowwaveminute;
        public string QDC48HTFSECONDLOWWAVEMINUTE
        {
            get{ return _qdc48htfsecondlowwaveminute; }
            set{ _qdc48htfsecondlowwaveminute = value; }
        }        
		/// <summary>
		/// 第二次低潮潮高
        /// </summary>		
		private string _qdc48htfsecondlowwaveheight;
        public string QDC48HTFSECONDLOWWAVEHEIGHT
        {
            get{ return _qdc48htfsecondlowwaveheight; }
            set{ _qdc48htfsecondlowwaveheight = value; }
        }        
		   
	}


