using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//青岛周边海域24小时预报
		public class TBLQDCIRCUM24HWATERFORECAST
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
		/// 青岛近海浪高
        /// </summary>		
		private string _qdc24hwfqdoffshorewaveheight;
        public string QDC24HWFQDOFFSHOREWAVEHEIGHT
        {
            get{ return _qdc24hwfqdoffshorewaveheight; }
            set{ _qdc24hwfqdoffshorewaveheight = value; }
        }        
		/// <summary>
		/// 青岛近海水温
        /// </summary>		
		private string _qdc24hwfqdoffshorewatertemp;
        public string QDC24HWFQDOFFSHOREWATERTEMP
        {
            get{ return _qdc24hwfqdoffshorewatertemp; }
            set{ _qdc24hwfqdoffshorewatertemp = value; }
        }        
		/// <summary>
		/// 即墨近海浪高
        /// </summary>		
		private string _qdc24hwfjmoffshorewaveheight;
        public string QDC24HWFJMOFFSHOREWAVEHEIGHT
        {
            get{ return _qdc24hwfjmoffshorewaveheight; }
            set{ _qdc24hwfjmoffshorewaveheight = value; }
        }        
		/// <summary>
		/// 即墨近海水温
        /// </summary>		
		private string _qdc24hwfjmoffshorewatertemp;
        public string QDC24HWFJMOFFSHOREWATERTEMP
        {
            get{ return _qdc24hwfjmoffshorewatertemp; }
            set{ _qdc24hwfjmoffshorewatertemp = value; }
        }        
		/// <summary>
		/// 胶州湾浪高
        /// </summary>		
		private string _qdc24hwfjzwoffshorewaveheight;
        public string QDC24HWFJZWOFFSHOREWAVEHEIGHT
        {
            get{ return _qdc24hwfjzwoffshorewaveheight; }
            set{ _qdc24hwfjzwoffshorewaveheight = value; }
        }        
		/// <summary>
		/// 胶州湾水温
        /// </summary>		
		private string _qdc24hwfjzwoffshorewatertemp;
        public string QDC24HWFJZWOFFSHOREWATERTEMP
        {
            get{ return _qdc24hwfjzwoffshorewatertemp; }
            set{ _qdc24hwfjzwoffshorewatertemp = value; }
        }        
		/// <summary>
		/// 胶南近海浪高
        /// </summary>		
		private string _qdc24hwfjnoffshorewaveheight;
        public string QDC24HWFJNOFFSHOREWAVEHEIGHT
        {
            get{ return _qdc24hwfjnoffshorewaveheight; }
            set{ _qdc24hwfjnoffshorewaveheight = value; }
        }        
		/// <summary>
		/// 胶南近海水温
        /// </summary>		
		private string _qdc24hwfjnoffshorewatertemp;
        public string QDC24HWFJNOFFSHOREWATERTEMP
        {
            get{ return _qdc24hwfjnoffshorewatertemp; }
            set{ _qdc24hwfjnoffshorewatertemp = value; }
        }        
		   
	}


