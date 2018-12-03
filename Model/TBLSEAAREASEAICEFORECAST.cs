using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class TBLSEAAREASEAICEFORECAST
    {
        /// <summary>
        /// 填报日期
        /// </summary>		
        private DateTime _publishdate;
        public DateTime PUBLISHDATE
        {
            get { return _publishdate; }
            set { _publishdate = value; }
        }
        /// <summary>
        /// 海区
        /// </summary>		
        private string _SEAAREA;
        public string SEAAREA
        {
            get { return _SEAAREA; }
            set { _SEAAREA = value; }
        }
        /// <summary>
        /// 最大结冰范围
        /// </summary>		
        private string _MAXICEAREA;
        public string MAXICEAREA
        {
            get { return _MAXICEAREA; }
            set { _MAXICEAREA = value; }
        }
        /// <summary>
        /// 一般冰厚
        /// </summary>		
        private string _COMMONTHICKNESS;
        public string COMMONTHICKNESS
        {
            get { return _COMMONTHICKNESS; }
            set { _COMMONTHICKNESS = value; }
        }
        /// <summary>
        /// 最大冰厚
        /// </summary>		
        private string _MAXTHICKNESS;
        public string MAXTHICKNESS
        {
            get { return _MAXTHICKNESS; }
            set { _MAXTHICKNESS = value; }
        }

    }
}