using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model.NineteenWord
{
    /// <summary>
    /// 19号表单周旬月Word
    /// </summary>
    public class NineteenNomalFileModel
    {
        //主键
        public int ID { get; set; }
        //文件名称
        public string FILENAME { get; set; }
        //文件流
        public byte[] FILEFLOW { get; set; }
        //发布日期
        public DateTime PUBLISHDATE { get; set; }
        //表单类型
        public string FILETYPE { get; set; }
    }
}