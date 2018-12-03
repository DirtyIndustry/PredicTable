using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//用户类型和权限对应表
		public class TBLTYPEANDRIGHT
	{
   		     
      	/// <summary>
		/// 用户类型编码
        /// </summary>		
		private decimal _usertypeid;
        public decimal USERTYPEID
        {
            get{ return _usertypeid; }
            set{ _usertypeid = value; }
        }        
		/// <summary>
		/// 用户权限编码
        /// </summary>		
		private decimal _userrightid;
        public decimal USERRIGHTID
        {
            get{ return _userrightid; }
            set{ _userrightid = value; }
        }        
		   
	}


