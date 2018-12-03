using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

		public class TBLYBDDOCUMENT
	{
   		     
      	/// <summary>
		/// YBDID
        /// </summary>		
		private decimal _ybdid;
        public decimal TYPE
        {
            get{ return _ybdid; }
            set{ _ybdid = value; }
        }        
		/// <summary>
		/// YBDNAME
        /// </summary>		
		private string _ybdname;
        public string YBDNAME
        {
            get{ return _ybdname; }
            set{ _ybdname = value; }
        }        
		/// <summary>
		/// UPLOADDATE
        /// </summary>		
		private DateTime _uploaddate;
        public DateTime UPLOADDATE
        {
            get{ return _uploaddate; }
            set{ _uploaddate = value; }
        }        
		/// <summary>
		/// UPLOADER
        /// </summary>		
		private string _uploader;
        public string UPLOADER
        {
            get{ return _uploader; }
            set{ _uploader = value; }
        }        
		/// <summary>
		/// YBDCONTENT
        /// </summary>		
		private byte[] _ybdcontent;
        public byte[] YBDCONTENT
        {
            get{ return _ybdcontent; }
            set{ _ybdcontent = value; }
        }        
		/// <summary>
		/// YBDSIZE
        /// </summary>		
		private string _ybdsize;
        public string YBDSIZE
        {
            get{ return _ybdsize; }
            set{ _ybdsize = value; }
        }        
		   
	}


