using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//表尾
		public class TBLFOOTER
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
		/// 填报小时
        /// </summary>		
		private int _publishhour;
        public int PUBLISHHOUR
        {
            get{ return _publishhour; }
            set{ _publishhour = value; }
        }        
		/// <summary>
		/// 发布单位
        /// </summary>		
		private string _freleaseunit;
        public string FRELEASEUNIT
        {
            get{ return _freleaseunit; }
            set{ _freleaseunit = value; }
        }        
		/// <summary>
		/// 电话
        /// </summary>		
		private string _ftelephone;
        public string FTELEPHONE
        {
            get{ return _ftelephone; }
            set{ _ftelephone = value; }
        }        
		/// <summary>
		/// 传真
        /// </summary>		
		private string _ffax;
        public string FFAX
        {
            get{ return _ffax; }
            set{ _ffax = value; }
        }        
		/// <summary>
		/// 海浪预报员
        /// </summary>		
		private string _fwaveforecaster;
        public string FWAVEFORECASTER
        {
            get{ return _fwaveforecaster; }
            set{ _fwaveforecaster = value; }
        }        
		/// <summary>
		/// 潮汐预报员
        /// </summary>		
		private string _ftidalforecaster;
        public string FTIDALFORECASTER
        {
            get{ return _ftidalforecaster; }
            set{ _ftidalforecaster = value; }
        }        
		/// <summary>
		/// 水温预报员
        /// </summary>		
		private string _fwatertemperatureforecaster;
        public string FWATERTEMPERATUREFORECASTER
        {
            get{ return _fwatertemperatureforecaster; }
            set{ _fwatertemperatureforecaster = value; }
        }
        /// <summary>
        /// 海浪预报员电话
        /// </summary>		
        private string _fwaveforecastertel;
        public string FWAVEFORECASTERTEL
        {
            get { return _fwaveforecastertel; }
            set { _fwaveforecastertel = value; }
        }
        /// <summary>
        /// 潮汐预报员电话
        /// </summary>		
        private string _ftidalforecastertel;
        public string FTIDALFORECASTERTEL
        {
            get { return _ftidalforecastertel; }
            set { _ftidalforecastertel = value; }
        }
        /// <summary>
        /// 水温预报员电话
        /// </summary>		
        private string _fwatertemperatureforecastertel;
        public string FWATERTEMPERATUREFORECASTERTEL
        {
            get { return _fwatertemperatureforecastertel; }
            set { _fwatertemperatureforecastertel = value; }
        }
        /// <summary>
        /// 预报值班
        /// </summary>		
        private string _zhibantel;
        public string ZHIBANTEL
        {
            get { return _zhibantel; }
            set { _zhibantel = value; }
        }
        /// <summary>
        /// 预报发送
        /// </summary>		
        private string _sendtel;
        public string SENDTEL
        {
            get { return _sendtel; }
            set { _sendtel = value; }
        }


}


