using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//预报单综合信息
		public class TBLREPORTSCOMMONMESS
	{
   		     
      	/// <summary>
		/// 发布单位
        /// </summary>		
		private string _rcmpublishsector;
        public string RCMPUBLISHSECTOR
        {
            get{ return _rcmpublishsector; }
            set{ _rcmpublishsector = value; }
        }        
		/// <summary>
		/// 电话
        /// </summary>		
		private string _rcmtellphone;
        public string RCMTELLPHONE
        {
            get{ return _rcmtellphone; }
            set{ _rcmtellphone = value; }
        }        
		/// <summary>
		/// 传真
        /// </summary>		
		private string _rcmfax;
        public string RCMFAX
        {
            get{ return _rcmfax; }
            set{ _rcmfax = value; }
        }        
		   
	}


