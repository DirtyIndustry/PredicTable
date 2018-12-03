using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//海洋气象综述
		public class TBLMARINEMETEOROLOGICALREVIEW
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
		/// 综述3天
        /// </summary>		
		private string _meteorologicalreview;
        public string METEOROLOGICALREVIEW
        {
            get{ return _meteorologicalreview; }
            set{ _meteorologicalreview = value; }
        }        
		/// <summary>
		/// 综述24小时
        /// </summary>		
		private string _meteorologicalreview24hour;
        public string METEOROLOGICALREVIEW24HOUR
        {
            get{ return _meteorologicalreview24hour; }
            set{ _meteorologicalreview24hour = value; }
        }        
		   
	}


