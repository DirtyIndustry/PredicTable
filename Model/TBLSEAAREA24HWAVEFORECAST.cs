using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//海区24小时海浪预报
		public class TBLSEAAREA24HWAVEFORECAST
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
		private string _sa24hwfbohaiwaveheight;
        public string SA24HWFBOHAIWAVEHEIGHT
        {
            get{ return _sa24hwfbohaiwaveheight; }
            set{ _sa24hwfbohaiwaveheight = value; }
        }
        /// <summary>
        /// 渤海波向
        /// </summary>		
        private string _sa24hwfbohaiwavedir;
        public string SA24HWFBOHAIWAVEDIR
        {
            get { return _sa24hwfbohaiwavedir; }
            set { _sa24hwfbohaiwavedir = value; }
        }
        /// <summary>
        /// 渤海波级
        /// </summary>		
        private string _SA24HWFBOHAIWAVETYPE;
        public string SA24HWFBOHAIWAVETYPE
        {
            get { return _SA24HWFBOHAIWAVETYPE; }
            set { _SA24HWFBOHAIWAVETYPE = value; }
        }


        /// <summary>
        /// 渤海涌向
        /// </summary>		
        private string _sa24hwfbohaisurgedir;
        public string SA24HWFBOHAISURGEDIR
        {
            get{ return _sa24hwfbohaisurgedir; }
            set{ _sa24hwfbohaisurgedir = value; }
        }        
		/// <summary>
		/// 黄海北部波高
        /// </summary>		
		private string _sa24hwfnorthofyswaveheight;
        public string SA24HWFNORTHOFYSWAVEHEIGHT
        {
            get{ return _sa24hwfnorthofyswaveheight; }
            set{ _sa24hwfnorthofyswaveheight = value; }
        }        
		/// <summary>
		/// 黄海北部波向
        /// </summary>		
		private string _sa24hwfnorthofyswavedir;
        public string SA24HWFNORTHOFYSWAVEDIR
        {
            get{ return _sa24hwfnorthofyswavedir; }
            set{ _sa24hwfnorthofyswavedir = value; }
        }
        /// <summary>
        /// 黄海北部波级
        /// </summary>		
        private string _SA24HWFNORTHOFYSWAVETYPE;
        public string SA24HWFNORTHOFYSWAVETYPE
        {
            get { return _SA24HWFNORTHOFYSWAVETYPE; }
            set { _SA24HWFNORTHOFYSWAVETYPE = value; }
        }
        /// <summary>
        /// 黄海北部涌向
        /// </summary>		
        private string _sa24hwfnorthofyssurgedir;
        public string SA24HWFNORTHOFYSSURGEDIR
        {
            get{ return _sa24hwfnorthofyssurgedir; }
            set{ _sa24hwfnorthofyssurgedir = value; }
        }        
		/// <summary>
		/// 黄海中部波高
        /// </summary>		
		private string _sa24hwfmiddleofyswaveheight;
        public string SA24HWFMIDDLEOFYSWAVEHEIGHT
        {
            get{ return _sa24hwfmiddleofyswaveheight; }
            set{ _sa24hwfmiddleofyswaveheight = value; }
        }        
		/// <summary>
		/// 黄海中部波向
        /// </summary>		
		private string _sa24hwfmiddleofyswavedir;
        public string SA24HWFMIDDLEOFYSWAVEDIR
        {
            get{ return _sa24hwfmiddleofyswavedir; }
            set{ _sa24hwfmiddleofyswavedir = value; }
        }
        /// <summary>
        /// 黄海中部波级
        /// </summary>		
        private string _SA24HWFMIDDLEOFYSWAVETYPE;
        public string SA24HWFMIDDLEOFYSWAVETYPE
        {
            get { return _SA24HWFMIDDLEOFYSWAVETYPE; }
            set { _SA24HWFMIDDLEOFYSWAVETYPE = value; }
        }
        /// <summary>
        /// 黄海中部涌向
        /// </summary>		
        private string _sa24hwfmiddleofyssurgedir;
        public string SA24HWFMIDDLEOFYSSURGEDIR
        {
            get{ return _sa24hwfmiddleofyssurgedir; }
            set{ _sa24hwfmiddleofyssurgedir = value; }
        }        
		/// <summary>
		/// 黄海南部波高
        /// </summary>		
		private string _sa24hwfsouthofyswaveheight;
        public string SA24HWFSOUTHOFYSWAVEHEIGHT
        {
            get{ return _sa24hwfsouthofyswaveheight; }
            set{ _sa24hwfsouthofyswaveheight = value; }
        }        
		/// <summary>
		/// 黄海南部波向
        /// </summary>		
		private string _sa24hwfsouthofyswavedir;
        public string SA24HWFSOUTHOFYSWAVEDIR
        {
            get{ return _sa24hwfsouthofyswavedir; }
            set{ _sa24hwfsouthofyswavedir = value; }
        }        
		/// <summary>
		/// 黄海南部涌向
        /// </summary>		
		private string _sa24hwfsouthofyssurgedir;
        public string SA24HWFSOUTHOFYSSURGEDIR
        {
            get{ return _sa24hwfsouthofyssurgedir; }
            set{ _sa24hwfsouthofyssurgedir = value; }
        }        
		/// <summary>
		/// 青岛近岸波高
        /// </summary>		
		private string _sa24hwfqdoffshorewaveheight;
        public string SA24HWFQDOFFSHOREWAVEHEIGHT
        {
            get{ return _sa24hwfqdoffshorewaveheight; }
            set{ _sa24hwfqdoffshorewaveheight = value; }
        }        
		/// <summary>
		/// 青岛近岸波向
        /// </summary>		
		private string _sa24hwfqdoffshorewavedir;
        public string SA24HWFQDOFFSHOREWAVEDIR
        {
            get{ return _sa24hwfqdoffshorewavedir; }
            set{ _sa24hwfqdoffshorewavedir = value; }
        }        
		/// <summary>
		/// 青岛近岸有无涌状况
        /// </summary>		
		private string _sa24hwfqdoffshoresurgestate;
        public string SA24HWFQDOFFSHORESURGESTATE
        {
            get{ return _sa24hwfqdoffshoresurgestate; }
            set{ _sa24hwfqdoffshoresurgestate = value; }
        }        
		/// <summary>
		/// 青岛近岸涌向
        /// </summary>		
		private string _sa24hwfqdoffshoresurgedir;
        public string SA24HWFQDOFFSHORESURGEDIR
        {
            get{ return _sa24hwfqdoffshoresurgedir; }
            set{ _sa24hwfqdoffshoresurgedir = value; }
        }        
		/// <summary>
		/// 渤海海浪备注
        /// </summary>		
		private string _sa24hwfbohaiwavenotes;
        public string SA24HWFBOHAIWAVENOTES
        {
            get{ return _sa24hwfbohaiwavenotes; }
            set{ _sa24hwfbohaiwavenotes = value; }
        }        
		/// <summary>
		/// 黄海北部海浪备注
        /// </summary>		
		private string _sa24hwfnorthofyswavenotes;
        public string SA24HWFNORTHOFYSWAVENOTES
        {
            get{ return _sa24hwfnorthofyswavenotes; }
            set{ _sa24hwfnorthofyswavenotes = value; }
        }        
		/// <summary>
		/// 黄海中部海浪备注
        /// </summary>		
		private string _sa24hwfmiddleofyswavenotes;
        public string SA24HWFMIDDLEOFYSWAVENOTES
        {
            get{ return _sa24hwfmiddleofyswavenotes; }
            set{ _sa24hwfmiddleofyswavenotes = value; }
        }        
		/// <summary>
		/// 黄海南部海浪备注
        /// </summary>		
		private string _sa24hwfsouthofyswavenotes;
        public string SA24HWFSOUTHOFYSWAVENOTES
        {
            get{ return _sa24hwfsouthofyswavenotes; }
            set{ _sa24hwfsouthofyswavenotes = value; }
        }        
		/// <summary>
		/// 青岛近岸海浪备注
        /// </summary>		
		private string _sa24hwfqdoffshorewavenotes;
        public string SA24HWFQDOFFSHOREWAVENOTES
        {
            get{ return _sa24hwfqdoffshorewavenotes; }
            set{ _sa24hwfqdoffshorewavenotes = value; }
        }        
		   
	}


