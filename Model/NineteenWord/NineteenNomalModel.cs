using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PredicTable.Model.NineteenWord
{
    /// <summary>
    /// 19号预报单周、旬、月
    /// </summary>
    public class NineteenNomalModel
    {
        //
        public string ID { get; set; }
        //时间
        public DateTime PUBLISHDATE { get; set; }
        //冰情概况
        public string ICESITUATION { get; set; }
        //predict
        public string PREDICT { get; set; }
        //预报时效（周、旬、月）
        public string PREDICTAGING { get; set; }
        //说明
        public string DESCRIPTION { get; set; }
       
        //图片二进制数据
        public byte[] BMP { get; set; }
        //传真
        public string CHUANZHEN { get; set; }
        //电话
        public string IPHONE { get; set; }
        //联系人
        public string LINKMAN { get; set; }

        //发送单位
        public string FASONGDANWEI { get; set; }
        //发往
        public string SENDUNIT { get; set; }
        //表格数据
        public List<NineteenNomalLineModel> NineteenNomalLine { get; set; }
    }
}