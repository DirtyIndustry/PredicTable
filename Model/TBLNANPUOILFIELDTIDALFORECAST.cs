using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//南堡油田海域潮汐预报
		public class TBLNANPUOILFIELDTIDALFORECAST
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
		/// 第一次高潮潮时
        /// </summary>		
		private string _notffirsthighwavetime;
        public string NOTFFIRSTHIGHWAVETIME
        {
            get{ return _notffirsthighwavetime; }
            set{ _notffirsthighwavetime = value; }
        }        
		/// <summary>
		/// 第一次高潮潮位
        /// </summary>		
		private string _notffirsthighwaveheight;
        public string NOTFFIRSTHIGHWAVEHEIGHT
        {
            get{ return _notffirsthighwaveheight; }
            set{ _notffirsthighwaveheight = value; }
        }        
		/// <summary>
		/// 第一次低潮潮时
        /// </summary>		
		private string _notffirstlowwavetime;
        public string NOTFFIRSTLOWWAVETIME
        {
            get{ return _notffirstlowwavetime; }
            set{ _notffirstlowwavetime = value; }
        }        
		/// <summary>
		/// 第一次低潮潮位
        /// </summary>		
		private string _notffirstlowwaveheight;
        public string NOTFFIRSTLOWWAVEHEIGHT
        {
            get{ return _notffirstlowwaveheight; }
            set{ _notffirstlowwaveheight = value; }
        }        
		/// <summary>
		/// 第二次高潮潮时
        /// </summary>		
		private string _notfsecondhighwavetime;
        public string NOTFSECONDHIGHWAVETIME
        {
            get{ return _notfsecondhighwavetime; }
            set{ _notfsecondhighwavetime = value; }
        }        
		/// <summary>
		/// 第二次高潮潮位
        /// </summary>		
		private string _notfsecondhighwaveheight;
        public string NOTFSECONDHIGHWAVEHEIGHT
        {
            get{ return _notfsecondhighwaveheight; }
            set{ _notfsecondhighwaveheight = value; }
        }        
		/// <summary>
		/// 第二次低潮潮时
        /// </summary>		
		private string _notfsecondlowwavetime;
        public string NOTFSECONDLOWWAVETIME
        {
            get{ return _notfsecondlowwavetime; }
            set{ _notfsecondlowwavetime = value; }
        }        
		/// <summary>
		/// 第二次低潮潮位
        /// </summary>		
		private string _notfsecondlowwaveheight;
        public string NOTFSECONDLOWWAVEHEIGHT
        {
            get{ return _notfsecondlowwaveheight; }
            set{ _notfsecondlowwaveheight = value; }
        }        
		   
	}


