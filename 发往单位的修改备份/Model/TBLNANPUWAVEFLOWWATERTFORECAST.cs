using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//南堡油田海域波浪、风、水温预报
		public class TBLNANPUWAVEFLOWWATERTFORECAST
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
		/// 波高
        /// </summary>		
		private string _nwfwtfwaveheight;
        public string NWFWTFWAVEHEIGHT
        {
            get{ return _nwfwtfwaveheight; }
            set{ _nwfwtfwaveheight = value; }
        }        
		/// <summary>
		/// 波向
        /// </summary>		
		private string _nwfwtfwavedir;
        public string NWFWTFWAVEDIR
        {
            get{ return _nwfwtfwavedir; }
            set{ _nwfwtfwavedir = value; }
        }        
		/// <summary>
		/// 风向
        /// </summary>		
		private string _nwfwtfflowdir;
        public string NWFWTFFLOWDIR
        {
            get{ return _nwfwtfflowdir; }
            set{ _nwfwtfflowdir = value; }
        }        
		/// <summary>
		/// 风力
        /// </summary>		
		private string _nwfwtfflowlevel;
        public string NWFWTFFLOWLEVEL
        {
            get{ return _nwfwtfflowlevel; }
            set{ _nwfwtfflowlevel = value; }
        }        
		/// <summary>
		/// 水温
        /// </summary>		
		private string _nwfwtfwatertemperature;
        public string NWFWTFWATERTEMPERATURE
        {
            get{ return _nwfwtfwatertemperature; }
            set{ _nwfwtfwatertemperature = value; }
        }        
		/// <summary>
		/// 天气
        /// </summary>		
		private string _nwfwtfweather;
        public string NWFWTFWEATHER
        {
            get{ return _nwfwtfweather; }
            set{ _nwfwtfweather = value; }
        }        
		   
	}


