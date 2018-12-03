using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//海区48小时海浪预报
		public class TBLSEAAREA48HWAVEFORECAST
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
		/// 渤海波高
        /// </summary>		
		private string _sa48hwfbohaiwaveheight;
        public string SA48HWFBOHAIWAVEHEIGHT
        {
            get{ return _sa48hwfbohaiwaveheight; }
            set{ _sa48hwfbohaiwaveheight = value; }
        }        
		/// <summary>
		/// 渤海波向
        /// </summary>		
		private string _sa48hwfbohaiwavedir;
        public string SA48HWFBOHAIWAVEDIR
        {
            get{ return _sa48hwfbohaiwavedir; }
            set{ _sa48hwfbohaiwavedir = value; }
        }        
		/// <summary>
		/// 渤海涌向
        /// </summary>		
		private string _sa48hwfbohaisurgedir;
        public string SA48HWFBOHAISURGEDIR
        {
            get{ return _sa48hwfbohaisurgedir; }
            set{ _sa48hwfbohaisurgedir = value; }
        }        
		/// <summary>
		/// 渤海海浪预报备注
        /// </summary>		
		private string _sa48hwfbohaiwavenotes;
        public string SA48HWFBOHAIWAVENOTES
        {
            get{ return _sa48hwfbohaiwavenotes; }
            set{ _sa48hwfbohaiwavenotes = value; }
        }        
		/// <summary>
		/// 黄海北部波高
        /// </summary>		
		private string _sa48hwfnorthofyswaveheight;
        public string SA48HWFNORTHOFYSWAVEHEIGHT
        {
            get{ return _sa48hwfnorthofyswaveheight; }
            set{ _sa48hwfnorthofyswaveheight = value; }
        }        
		/// <summary>
		/// 黄海北部波向
        /// </summary>		
		private string _sa48hwfnorthofyswavedir;
        public string SA48HWFNORTHOFYSWAVEDIR
        {
            get{ return _sa48hwfnorthofyswavedir; }
            set{ _sa48hwfnorthofyswavedir = value; }
        }        
		/// <summary>
		/// 黄海北部涌向
        /// </summary>		
		private string _sa48hwfnorthofyssurgedir;
        public string SA48HWFNORTHOFYSSURGEDIR
        {
            get{ return _sa48hwfnorthofyssurgedir; }
            set{ _sa48hwfnorthofyssurgedir = value; }
        }        
		/// <summary>
		/// 黄海北部预报备注
        /// </summary>		
		private string _sa48hwfnorthofyswavenotes;
        public string SA48HWFNORTHOFYSWAVENOTES
        {
            get{ return _sa48hwfnorthofyswavenotes; }
            set{ _sa48hwfnorthofyswavenotes = value; }
        }        
		/// <summary>
		/// 黄海中部波高
        /// </summary>		
		private string _sa48hwfmiddleofyswaveheight;
        public string SA48HWFMIDDLEOFYSWAVEHEIGHT
        {
            get{ return _sa48hwfmiddleofyswaveheight; }
            set{ _sa48hwfmiddleofyswaveheight = value; }
        }        
		/// <summary>
		/// 黄海中部波向
        /// </summary>		
		private string _sa48hwfmiddleofyswavedir;
        public string SA48HWFMIDDLEOFYSWAVEDIR
        {
            get{ return _sa48hwfmiddleofyswavedir; }
            set{ _sa48hwfmiddleofyswavedir = value; }
        }        
		/// <summary>
		/// 黄海中部涌向
        /// </summary>		
		private string _sa48hwfmiddleofyssurgedir;
        public string SA48HWFMIDDLEOFYSSURGEDIR
        {
            get{ return _sa48hwfmiddleofyssurgedir; }
            set{ _sa48hwfmiddleofyssurgedir = value; }
        }        
		/// <summary>
		/// 黄海中部预报备注
        /// </summary>		
		private string _sa48hwfmiddleofyswavenotes;
        public string SA48HWFMIDDLEOFYSWAVENOTES
        {
            get{ return _sa48hwfmiddleofyswavenotes; }
            set{ _sa48hwfmiddleofyswavenotes = value; }
        }        
		/// <summary>
		/// 黄海南部波高
        /// </summary>		
		private string _sa48hwfsouthofyswaveheight;
        public string SA48HWFSOUTHOFYSWAVEHEIGHT
        {
            get{ return _sa48hwfsouthofyswaveheight; }
            set{ _sa48hwfsouthofyswaveheight = value; }
        }        
		/// <summary>
		/// 黄海南部波向
        /// </summary>		
		private string _sa48hwfsouthofyswavedir;
        public string SA48HWFSOUTHOFYSWAVEDIR
        {
            get{ return _sa48hwfsouthofyswavedir; }
            set{ _sa48hwfsouthofyswavedir = value; }
        }        
		/// <summary>
		/// 黄海南部涌向
        /// </summary>		
		private string _sa48hwfsouthofyssurgedir;
        public string SA48HWFSOUTHOFYSSURGEDIR
        {
            get{ return _sa48hwfsouthofyssurgedir; }
            set{ _sa48hwfsouthofyssurgedir = value; }
        }        
		/// <summary>
		/// 黄海南部预报备份
        /// </summary>		
		private string _sa48hwfsouthofyswavenotes;
        public string SA48HWFSOUTHOFYSWAVENOTES
        {
            get{ return _sa48hwfsouthofyswavenotes; }
            set{ _sa48hwfsouthofyswavenotes = value; }
        }        
		   
	}


