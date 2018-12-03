using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//黄河南海堤附近海域24小时风浪
		public class TBLYRSOUTHSEAWALL24WINDWAVE
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
		private string _yrsswwwaveheight;
        public string YRSSWWWAVEHEIGHT
        {
            get{ return _yrsswwwaveheight; }
            set{ _yrsswwwaveheight = value; }
        }        
		/// <summary>
		/// 波向
        /// </summary>		
		private string _yrsswwwavedirection;
        public string YRSSWWWAVEDIRECTION
        {
            get{ return _yrsswwwavedirection; }
            set{ _yrsswwwavedirection = value; }
        }        
		/// <summary>
		/// 风向
        /// </summary>		
		private string _yrsswwwinddirection;
        public string YRSSWWWINDDIRECTION
        {
            get{ return _yrsswwwinddirection; }
            set{ _yrsswwwinddirection = value; }
        }        
		/// <summary>
		/// 风力
        /// </summary>		
		private string _yrsswwwindforce;
        public string YRSSWWWINDFORCE
        {
            get{ return _yrsswwwindforce; }
            set{ _yrsswwwindforce = value; }
        }        
		   
	}


