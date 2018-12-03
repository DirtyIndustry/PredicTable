using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//威海石岛区域潮汐预报
		public class TBLWEIHAISHIDAOTIDALFORECAST
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
		private string _reportarea;
        public string REPORTAREA
        {
            get{ return _reportarea; }
            set{ _reportarea = value; }
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
		private string _firsthighwavetime;
        public string FIRSTHIGHWAVETIME
        {
            get{ return _firsthighwavetime; }
            set{ _firsthighwavetime = value; }
        }        
		/// <summary>
		/// 第一次高潮潮高
        /// </summary>		
		private string _firsthighwaveheight;
        public string FIRSTHIGHWAVEHEIGHT
        {
            get{ return _firsthighwaveheight; }
            set{ _firsthighwaveheight = value; }
        }        
		/// <summary>
		/// 第一次低潮潮时
        /// </summary>		
		private string _firstlowwavetime;
        public string FIRSTLOWWAVETIME
        {
            get{ return _firstlowwavetime; }
            set{ _firstlowwavetime = value; }
        }        
		/// <summary>
		/// 第一次低潮潮高
        /// </summary>		
		private string _firstlowwaveheight;
        public string FIRSTLOWWAVEHEIGHT
        {
            get{ return _firstlowwaveheight; }
            set{ _firstlowwaveheight = value; }
        }        
		/// <summary>
		/// 第二次高潮潮时
        /// </summary>		
		private string _secondhighwavetime;
        public string SECONDHIGHWAVETIME
        {
            get{ return _secondhighwavetime; }
            set{ _secondhighwavetime = value; }
        }        
		/// <summary>
		/// 第二次高潮潮高
        /// </summary>		
		private string _secondhighwaveheight;
        public string SECONDHIGHWAVEHEIGHT
        {
            get{ return _secondhighwaveheight; }
            set{ _secondhighwaveheight = value; }
        }        
		/// <summary>
		/// 第二次低潮潮时
        /// </summary>		
		private string _secondlowwavetime;
        public string SECONDLOWWAVETIME
        {
            get{ return _secondlowwavetime; }
            set{ _secondlowwavetime = value; }
        }        
		/// <summary>
		/// 第二次低潮潮高
        /// </summary>		
		private string _secondlowwaveheight;
        public string SECONDLOWWAVEHEIGHT
        {
            get{ return _secondlowwaveheight; }
            set{ _secondlowwaveheight = value; }
        }        
		   
	}


