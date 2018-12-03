using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//各海区24小时海浪
		public class TBLEACHSEAAREA24HSEAWAVE
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
		/// 地区
        /// </summary>		
		private string _esaswarea;
        public string ESASWAREA
        {
            get{ return _esaswarea; }
            set{ _esaswarea = value; }
        }        
		/// <summary>
		/// 最低浪高
        /// </summary>		
		private string _esaswlowestwaveheight;
        public string ESASWLOWESTWAVEHEIGHT
        {
            get{ return _esaswlowestwaveheight; }
            set{ _esaswlowestwaveheight = value; }
        }        
		/// <summary>
		/// 最高浪高
        /// </summary>		
		private string _esaswhightestwaveheight;
        public string ESASWHIGHTESTWAVEHEIGHT
        {
            get{ return _esaswhightestwaveheight; }
            set{ _esaswhightestwaveheight = value; }
        }        
		/// <summary>
		/// 浪高类型
        /// </summary>		
		private string _esaswwavetype;
        public string ESASWWAVETYPE
        {
            get{ return _esaswwavetype; }
            set{ _esaswwavetype = value; }
        }        
		   
	}


