using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//威海电视台未来24h预报
		public class TBLWEIHAITV24HFORECAST
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
		/// 石岛近海24h浪高
        /// </summary>		
		private string _wtv24hsd24hfwaveheight;
        public string WTV24HSD24HFWAVEHEIGHT
        {
            get{ return _wtv24hsd24hfwaveheight; }
            set{ _wtv24hsd24hfwaveheight = value; }
        }        
		/// <summary>
		/// 石岛近海24h水温
        /// </summary>		
		private string _wtv24hsd24hfwatertemp;
        public string WTV24HSD24HFWATERTEMP
        {
            get{ return _wtv24hsd24hfwatertemp; }
            set{ _wtv24hsd24hfwatertemp = value; }
        }        
		/// <summary>
		/// 威海近海48h浪高
        /// </summary>		
		private string _wtv24hwh48hfwaveheight;
        public string WTV24HWH48HFWAVEHEIGHT
        {
            get{ return _wtv24hwh48hfwaveheight; }
            set{ _wtv24hwh48hfwaveheight = value; }
        }        
		/// <summary>
		/// 威海近海48h水温
        /// </summary>		
		private string _wtv24hwh48hfwatertemp;
        public string WTV24HWH48HFWATERTEMP
        {
            get{ return _wtv24hwh48hfwatertemp; }
            set{ _wtv24hwh48hfwatertemp = value; }
        }        
		/// <summary>
		/// 石岛近海48h浪高
        /// </summary>		
		private string _wtv24hsd48hfwaveheight;
        public string WTV24HSD48HFWAVEHEIGHT
        {
            get{ return _wtv24hsd48hfwaveheight; }
            set{ _wtv24hsd48hfwaveheight = value; }
        }        
		/// <summary>
		/// 石岛近海48水温
        /// </summary>		
		private string _wtv24hsd48hfwatertemp;
        public string WTV24HSD48HFWATERTEMP
        {
            get{ return _wtv24hsd48hfwatertemp; }
            set{ _wtv24hsd48hfwatertemp = value; }
        }        
		/// <summary>
		/// 文登区24h浪高
        /// </summary>		
		private string _wtv24hwd24hfwaveheight;
        public string WTV24HWD24HFWAVEHEIGHT
        {
            get{ return _wtv24hwd24hfwaveheight; }
            set{ _wtv24hwd24hfwaveheight = value; }
        }        
		/// <summary>
		/// 文登区24h水温
        /// </summary>		
		private string _wtv24hwd24hfwatertemp;
        public string WTV24HWD24HFWATERTEMP
        {
            get{ return _wtv24hwd24hfwatertemp; }
            set{ _wtv24hwd24hfwatertemp = value; }
        }        
		/// <summary>
		/// 成山头24h浪高
        /// </summary>		
		private string _wtv24hcst24hfwaveheight;
        public string WTV24HCST24HFWAVEHEIGHT
        {
            get{ return _wtv24hcst24hfwaveheight; }
            set{ _wtv24hcst24hfwaveheight = value; }
        }        
		/// <summary>
		/// 成山头24h水温
        /// </summary>		
		private string _wtv24hcst24hfwatertemp;
        public string WTV24HCST24HFWATERTEMP
        {
            get{ return _wtv24hcst24hfwatertemp; }
            set{ _wtv24hcst24hfwatertemp = value; }
        }        
		/// <summary>
		/// 乳山市24h浪高
        /// </summary>		
		private string _wtv24hrs24hfwaveheight;
        public string WTV24HRS24HFWAVEHEIGHT
        {
            get{ return _wtv24hrs24hfwaveheight; }
            set{ _wtv24hrs24hfwaveheight = value; }
        }        
		/// <summary>
		/// 乳山市24h水温
        /// </summary>		
		private string _wtv24hrs24hfwatertemp;
        public string WTV24HRS24HFWATERTEMP
        {
            get{ return _wtv24hrs24hfwatertemp; }
            set{ _wtv24hrs24hfwatertemp = value; }
        }        
		/// <summary>
		/// 文登区48h浪高
        /// </summary>		
		private string _wtv24hwd48hfwaveheight;
        public string WTV24HWD48HFWAVEHEIGHT
        {
            get{ return _wtv24hwd48hfwaveheight; }
            set{ _wtv24hwd48hfwaveheight = value; }
        }        
		/// <summary>
		/// 文登区48h水温
        /// </summary>		
		private string _wtv24hwd48hfwatertemp;
        public string WTV24HWD48HFWATERTEMP
        {
            get{ return _wtv24hwd48hfwatertemp; }
            set{ _wtv24hwd48hfwatertemp = value; }
        }        
		/// <summary>
		/// 成山头48h浪高
        /// </summary>		
		private string _wtv24hcst48hfwaveheight;
        public string WTV24HCST48HFWAVEHEIGHT
        {
            get{ return _wtv24hcst48hfwaveheight; }
            set{ _wtv24hcst48hfwaveheight = value; }
        }        
		/// <summary>
		/// 成山头48h水温
        /// </summary>		
		private string _wtv24hcst48hfwatertemp;
        public string WTV24HCST48HFWATERTEMP
        {
            get{ return _wtv24hcst48hfwatertemp; }
            set{ _wtv24hcst48hfwatertemp = value; }
        }        
		/// <summary>
		/// 乳山市48h浪高
        /// </summary>		
		private string _wtv24hrs48hfwaveheight;
        public string WTV24HRS48HFWAVEHEIGHT
        {
            get{ return _wtv24hrs48hfwaveheight; }
            set{ _wtv24hrs48hfwaveheight = value; }
        }        
		/// <summary>
		/// 乳山市48h水温
        /// </summary>		
		private string _wtv24hrs48hfwatertemp;
        public string WTV24HRS48HFWATERTEMP
        {
            get{ return _wtv24hrs48hfwatertemp; }
            set{ _wtv24hrs48hfwatertemp = value; }
        }        
		   
	}


