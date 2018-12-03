//**********************************************************************************************

//文件名(File Name)：                   StromTableModel

//作者(Author)：                        sl

//日期(Create Date)：                   2017-02-07

//修改记录(Revision History)：
//        R1：
//         修改作者：                   sl  
//         修改日期：                   2017-02-07
//         修改理由：                   添加
//                                      风暴潮Word文件表格属性实体
//                                      
//
//**********************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class StromTableModel
    {
        //主键
        public string ID { get; set; }
        //验潮站
        public string STATION { get; set; }
        //日期
        public string PUBLISHTIME { get; set; }
        //高潮时
        public string HIGHTIME { get; set; }
        //高潮值
        public string HIGHVALUE { get; set; }
        //警戒潮位
        public string WARNINGTIDEVALUE { get; set; }
        //警报级别
        public string WARNINGLEVEL { get; set; }
    }
}