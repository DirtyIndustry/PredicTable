using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//72小时渤海海区及黄河海港风、浪预报
		public class TBLYRBHWINDWAVE72HFORECASTTWO
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
		/// 区域
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
		/// 波高
        /// </summary>		
		private string _yrbhwwfwaveheight;
        public string YRBHWWFWAVEHEIGHT
        {
            get{ return _yrbhwwfwaveheight; }
            set{ _yrbhwwfwaveheight = value; }
        }        
		/// <summary>
		/// 波向
        /// </summary>		
		private string _yrbhwwfwavedir;
        public string YRBHWWFWAVEDIR
        {
            get{ return _yrbhwwfwavedir; }
            set{ _yrbhwwfwavedir = value; }
        }        
		/// <summary>
		/// 风向
        /// </summary>		
		private string _yrbhwwfflowdir;
        public string YRBHWWFFLOWDIR
        {
            get{ return _yrbhwwfflowdir; }
            set{ _yrbhwwfflowdir = value; }
        }        
		/// <summary>
		/// 风力
        /// </summary>		
		private string _yrbhwwfflowlevel;
        public string YRBHWWFFLOWLEVEL
        {
            get{ return _yrbhwwfflowlevel; }
            set{ _yrbhwwfflowlevel = value; }
        }        
		/// <summary>
		/// 水温
        /// </summary>		
		private string _yrbhwwfwatertemperature;
        public string YRBHWWFWATERTEMPERATURE
        {
            get{ return _yrbhwwfwatertemperature; }
            set{ _yrbhwwfwatertemperature = value; }
        }        
		   
	}


