using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//用户权限表
		public class TBLUSERRIGHT
	{
   		     
      	/// <summary>
		/// 用户权限编码
        /// </summary>		
		private decimal _userrightid;
        public decimal USERRIGHTID
        {
            get{ return _userrightid; }
            set{ _userrightid = value; }
        }        
		/// <summary>
		/// 权限名称
        /// </summary>		
		private string _userrightname;
        public string USERRIGHTNAME
        {
            get{ return _userrightname; }
            set{ _userrightname = value; }
        }        
		   
	}


