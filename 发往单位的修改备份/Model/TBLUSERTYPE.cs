using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//用户类型表
		public class TBLUSERTYPE
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
		/// 用户类型
        /// </summary>		
		private string _usertype;
        public string USERTYPE
        {
            get{ return _usertype; }
            set{ _usertype = value; }
        }        
		   
	}


