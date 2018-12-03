using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class HT_KJ_BHHB_CONTENTS
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string FILENAME
        {
            get;
            set;
        }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string CONTENT
        {
            get;
            set;
        }
    }
}