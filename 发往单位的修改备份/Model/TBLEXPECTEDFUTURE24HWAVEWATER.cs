using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//预计未来24小时海浪水温
		public class TBLEXPECTEDFUTURE24HWAVEWATER
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
		/// 渤海最低浪高
        /// </summary>		
		private string _efwwbhlowestwave;
        public string EFWWBHLOWESTWAVE
        {
            get{ return _efwwbhlowestwave; }
            set{ _efwwbhlowestwave = value; }
        }        
		/// <summary>
		/// 渤海最高浪高
        /// </summary>		
		private string _efwwbhhighestwave;
        public string EFWWBHHIGHESTWAVE
        {
            get{ return _efwwbhhighestwave; }
            set{ _efwwbhhighestwave = value; }
        }        
		/// <summary>
		/// 渤海浪高类型
        /// </summary>		
		private string _efwwbhwavetype;
        public string EFWWBHWAVETYPE
        {
            get{ return _efwwbhwavetype; }
            set{ _efwwbhwavetype = value; }
        }        
		/// <summary>
		/// 黄海北部最低浪高
        /// </summary>		
		private string _efwwbhnorthlowestwave;
        public string EFWWBHNORTHLOWESTWAVE
        {
            get{ return _efwwbhnorthlowestwave; }
            set{ _efwwbhnorthlowestwave = value; }
        }        
		/// <summary>
		/// 黄海北部最高浪高
        /// </summary>		
		private string _efwwbhnorthhighestwave;
        public string EFWWBHNORTHHIGHESTWAVE
        {
            get{ return _efwwbhnorthhighestwave; }
            set{ _efwwbhnorthhighestwave = value; }
        }        
		/// <summary>
		/// 黄海北部浪高类型
        /// </summary>		
		private string _efwwbhnorthwavetype;
        public string EFWWBHNORTHWAVETYPE
        {
            get{ return _efwwbhnorthwavetype; }
            set{ _efwwbhnorthwavetype = value; }
        }        
		/// <summary>
		/// 刁口海域浪高
        /// </summary>		
		private string _efwwdkseaareawaveheight;
        public string EFWWDKSEAAREAWAVEHEIGHT
        {
            get{ return _efwwdkseaareawaveheight; }
            set{ _efwwdkseaareawaveheight = value; }
        }        
		/// <summary>
		/// 刁口海域水温
        /// </summary>		
		private string _efwwdkseaareawatertempe;
        public string EFWWDKSEAAREAWATERTEMPE
        {
            get{ return _efwwdkseaareawatertempe; }
            set{ _efwwdkseaareawatertempe = value; }
        }        
		/// <summary>
		/// 黄河口海域浪高
        /// </summary>		
		private string _efwwhhkseaareawaveheight;
        public string EFWWHHKSEAAREAWAVEHEIGHT
        {
            get{ return _efwwhhkseaareawaveheight; }
            set{ _efwwhhkseaareawaveheight = value; }
        }        
		/// <summary>
		/// 黄河口水温
        /// </summary>		
		private string _efwwhhkseaareawatertemp;
        public string EFWWHHKSEAAREAWATERTEMP
        {
            get{ return _efwwhhkseaareawatertemp; }
            set{ _efwwhhkseaareawatertemp = value; }
        }        
		/// <summary>
		/// 广利港海域浪高
        /// </summary>		
		private string _efwwglgseaareawaveheight;
        public string EFWWGLGSEAAREAWAVEHEIGHT
        {
            get{ return _efwwglgseaareawaveheight; }
            set{ _efwwglgseaareawaveheight = value; }
        }        
		/// <summary>
		/// 广利港水温
        /// </summary>		
		private string _efwwglgseaareawatertemp;
        public string EFWWGLGSEAAREAWATERTEMP
        {
            get{ return _efwwglgseaareawatertemp; }
            set{ _efwwglgseaareawatertemp = value; }
        }        
		/// <summary>
		/// 东营港浪高
        /// </summary>		
		private string _efwwdygwaveheight;
        public string EFWWDYGWAVEHEIGHT
        {
            get{ return _efwwdygwaveheight; }
            set{ _efwwdygwaveheight = value; }
        }        
		/// <summary>
		/// 东营港水温
        /// </summary>		
		private string _efwwdygwatertemperature;
        public string EFWWDYGWATERTEMPERATURE
        {
            get{ return _efwwdygwatertemperature; }
            set{ _efwwdygwatertemperature = value; }
        }        
		/// <summary>
		/// 新户海域浪高
        /// </summary>		
		private string _efwwxhwaveheight;
        public string EFWWXHWAVEHEIGHT
        {
            get{ return _efwwxhwaveheight; }
            set{ _efwwxhwaveheight = value; }
        }        
		/// <summary>
		/// 新户海域水温
        /// </summary>		
		private string _efwwxhwatertemperature;
        public string EFWWXHWATERTEMPERATURE
        {
            get{ return _efwwxhwatertemperature; }
            set{ _efwwxhwatertemperature = value; }
        }        
		/// <summary>
		/// 埕口海域浪高
        /// </summary>		
		private string _efwwckwaveheight;
        public string EFWWCKWAVEHEIGHT
        {
            get{ return _efwwckwaveheight; }
            set{ _efwwckwaveheight = value; }
        }        
		/// <summary>
		/// 埕口海域水温
        /// </summary>		
		private string _efwwckwatertemperature;
        public string EFWWCKWATERTEMPERATURE
        {
            get{ return _efwwckwatertemperature; }
            set{ _efwwckwatertemperature = value; }
        }        
		   
	}


