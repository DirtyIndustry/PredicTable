using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


 
    public class ConfigXml
    {
        public string LastDate { get; set; }
        public string LocalPath { get; set; }
    }
  public    class Model1
    {
        ///// <summary>
        ///// 新插入的id
        ///// </summary>		
        //private decimal _newid;
        //public decimal NEWID
        //{
        //    get { return _newid; }
        //    set { _newid = value; }
        //}
        /// <summary>
        /// 文件
        /// </summary>		
        private byte[] _filebyte;
        public byte[] FILEBYTE
        {
            get { return _filebyte; }
            set { _filebyte = value; }
        }
        /// <summary>
        /// 主键编号
        /// </summary>		
        private decimal _contentid;
        public decimal CONTENTID
        {
            get { return _contentid; }
            set { _contentid = value; }
        }
        /// <summary>
        /// 文件名
        /// </summary>		
        private string _filename;
        public string FILENAME
        {
            get { return _filename; }
            set { _filename = value; }
        }
        /// <summary>
        /// 预报发布时间
        /// </summary>		
        private string _publishdate;
        public string PUBLISHDATE
        {
            get { return _publishdate; }
            set { _publishdate = value; }
        }
        /// <summary>
        /// 目标类型
        /// </summary>		
        private string _targettype;
        public string TARGETTYPE
        {
            get { return _targettype; }
            set { _targettype = value; }
        }
        /// <summary>
        /// 目标名称
        /// </summary>		
        private string _targetname;
        public string TARGETNAME
        {
            get { return _targetname; }
            set { _targetname = value; }
        }
        /// <summary>
        /// 预报单编号
        /// </summary>		
        private string _filenumber;
        public string FILENUMBER
        {
            get { return _filenumber; }
            set { _filenumber = value; }
        }
        /// <summary>
        /// 第一天第一次高潮潮时
        /// </summary>		
        private string _fd_gc1_cs;
        public string FD_GC1_CS
        {
            get { return _fd_gc1_cs; }
            set { _fd_gc1_cs = value; }
        }
        /// <summary>
        /// 第一天第一次高潮潮高
        /// </summary>		
        private string _fd_gc1_cg;
        public string FD_GC1_CG
        {
            get { return _fd_gc1_cg; }
            set { _fd_gc1_cg = value; }
        }
        /// <summary>
        /// 第一天第二次高潮潮时
        /// </summary>		
        private string _fd_gc2_cs;
        public string FD_GC2_CS
        {
            get { return _fd_gc2_cs; }
            set { _fd_gc2_cs = value; }
        }
        /// <summary>
        /// 第一天第二次高潮潮高
        /// </summary>		
        private string _fd_gc2_cg;
        public string FD_GC2_CG
        {
            get { return _fd_gc2_cg; }
            set { _fd_gc2_cg = value; }
        }
        /// <summary>
        /// 第一天第三次高潮潮时
        /// </summary>		
        private string _fd_gc3_cs;
        public string FD_GC3_CS
        {
            get { return _fd_gc3_cs; }
            set { _fd_gc3_cs = value; }
        }
        /// <summary>
        /// 第一天第三次高潮潮高
        /// </summary>		
        private string _fd_gc3_cg;
        public string FD_GC3_CG
        {
            get { return _fd_gc3_cg; }
            set { _fd_gc3_cg = value; }
        }
        /// <summary>
        /// 第一天第一次低潮潮时
        /// </summary>		
        private string _fd_dc1_cs;
        public string FD_DC1_CS
        {
            get { return _fd_dc1_cs; }
            set { _fd_dc1_cs = value; }
        }
        /// <summary>
        /// 第一天第一次低潮潮高
        /// </summary>		
        private string _fd_dc1_cg;
        public string FD_DC1_CG
        {
            get { return _fd_dc1_cg; }
            set { _fd_dc1_cg = value; }
        }
        /// <summary>
        /// 第一天第二次低潮潮时
        /// </summary>		
        private string _fd_dc2_cs;
        public string FD_DC2_CS
        {
            get { return _fd_dc2_cs; }
            set { _fd_dc2_cs = value; }
        }
        /// <summary>
        /// 第一天第二次低潮潮高
        /// </summary>		
        private string _fd_dc2_cg;
        public string FD_DC2_CG
        {
            get { return _fd_dc2_cg; }
            set { _fd_dc2_cg = value; }
        }
        /// <summary>
        /// 第一天第三次低潮潮时
        /// </summary>		
        private string _fd_dc3_cs;
        public string FD_DC3_CS
        {
            get { return _fd_dc3_cs; }
            set { _fd_dc3_cs = value; }
        }
        /// <summary>
        /// 第一天第三次低潮潮高
        /// </summary>		
        private string _fd_dc3_cg;
        public string FD_DC3_CG
        {
            get { return _fd_dc3_cg; }
            set { _fd_dc3_cg = value; }
        }
        /// <summary>
        /// 第二天第一次高潮潮时
        /// </summary>		
        private string _sd_gc1_cs;
        public string SD_GC1_CS
        {
            get { return _sd_gc1_cs; }
            set { _sd_gc1_cs = value; }
        }
        /// <summary>
        /// 第二天第一次高潮潮高
        /// </summary>		
        private string _sd_gc1_cg;
        public string SD_GC1_CG
        {
            get { return _sd_gc1_cg; }
            set { _sd_gc1_cg = value; }
        }
        /// <summary>
        /// 第二天第二次高潮潮时
        /// </summary>		
        private string _sd_gc2_cs;
        public string SD_GC2_CS
        {
            get { return _sd_gc2_cs; }
            set { _sd_gc2_cs = value; }
        }
        /// <summary>
        /// 第二天第二次高潮潮高
        /// </summary>		
        private string _sd_gc2_cg;
        public string SD_GC2_CG
        {
            get { return _sd_gc2_cg; }
            set { _sd_gc2_cg = value; }
        }
        /// <summary>
        /// 第二天第三次高潮潮时
        /// </summary>		
        private string _sd_gc3_cs;
        public string SD_GC3_CS
        {
            get { return _sd_gc3_cs; }
            set { _sd_gc3_cs = value; }
        }
        /// <summary>
        /// 第二天第三次高潮潮高
        /// </summary>		
        private string _sd_gc3_cg;
        public string SD_GC3_CG
        {
            get { return _sd_gc3_cg; }
            set { _sd_gc3_cg = value; }
        }
        /// <summary>
        /// 第二天第一次低潮潮时
        /// </summary>		
        private string _sd_dc1_cs;
        public string SD_DC1_CS
        {
            get { return _sd_dc1_cs; }
            set { _sd_dc1_cs = value; }
        }
        /// <summary>
        /// 第二天第一次低潮潮高
        /// </summary>		
        private string _sd_dc1_cg;
        public string SD_DC1_CG
        {
            get { return _sd_dc1_cg; }
            set { _sd_dc1_cg = value; }
        }
        /// <summary>
        /// 第二天第二次低潮潮时
        /// </summary>		
        private string _sd_dc2_cs;
        public string SD_DC2_CS
        {
            get { return _sd_dc2_cs; }
            set { _sd_dc2_cs = value; }
        }
        /// <summary>
        /// 第二天第二次低潮潮高
        /// </summary>		
        private string _sd_dc2_cg;
        public string SD_DC2_CG
        {
            get { return _sd_dc2_cg; }
            set { _sd_dc2_cg = value; }
        }
        /// <summary>
        /// 第二天第三次低潮潮时
        /// </summary>		
        private string _sd_dc3_cs;
        public string SD_DC3_CS
        {
            get { return _sd_dc3_cs; }
            set { _sd_dc3_cs = value; }
        }
        /// <summary>
        /// 第二天第三次低潮潮高
        /// </summary>		
        private string _sd_dc3_cg;
        public string SD_DC3_CG
        {
            get { return _sd_dc3_cg; }
            set { _sd_dc3_cg = value; }
        }
        /// <summary>
        /// 第三天第一次高潮潮时
        /// </summary>		
        private string _td_gc1_cs;
        public string TD_GC1_CS
        {
            get { return _td_gc1_cs; }
            set { _td_gc1_cs = value; }
        }
        /// <summary>
        /// 第三天第一次高潮潮高
        /// </summary>		
        private string _td_gc1_cg;
        public string TD_GC1_CG
        {
            get { return _td_gc1_cg; }
            set { _td_gc1_cg = value; }
        }
        /// <summary>
        /// 第三天第二次高潮潮时
        /// </summary>		
        private string _td_gc2_cs;
        public string TD_GC2_CS
        {
            get { return _td_gc2_cs; }
            set { _td_gc2_cs = value; }
        }
        /// <summary>
        /// 第三天第二次高潮潮高
        /// </summary>		
        private string _td_gc2_cg;
        public string TD_GC2_CG
        {
            get { return _td_gc2_cg; }
            set { _td_gc2_cg = value; }
        }
        /// <summary>
        /// 第三天第三次高潮潮时
        /// </summary>		
        private string _td_gc3_cs;
        public string TD_GC3_CS
        {
            get { return _td_gc3_cs; }
            set { _td_gc3_cs = value; }
        }
        /// <summary>
        /// 第三天第三次高潮潮高
        /// </summary>		
        private string _td_gc3_cg;
        public string TD_GC3_CG
        {
            get { return _td_gc3_cg; }
            set { _td_gc3_cg = value; }
        }
        /// <summary>
        /// 第三天第一次低潮潮时
        /// </summary>		
        private string _td_dc1_cs;
        public string TD_DC1_CS
        {
            get { return _td_dc1_cs; }
            set { _td_dc1_cs = value; }
        }
        /// <summary>
        /// 第三天第一次低潮潮高
        /// </summary>		
        private string _td_dc1_cg;
        public string TD_DC1_CG
        {
            get { return _td_dc1_cg; }
            set { _td_dc1_cg = value; }
        }
        /// <summary>
        /// 第三天第二次低潮潮时
        /// </summary>		
        private string _td_dc2_cs;
        public string TD_DC2_CS
        {
            get { return _td_dc2_cs; }
            set { _td_dc2_cs = value; }
        }
        /// <summary>
        /// 第三天第二次低潮潮高
        /// </summary>		
        private string _td_dc2_cg;
        public string TD_DC2_CG
        {
            get { return _td_dc2_cg; }
            set { _td_dc2_cg = value; }
        }
        /// <summary>
        /// 第三天第三次低潮潮时
        /// </summary>		
        private string _td_dc3_cs;
        public string TD_DC3_CS
        {
            get { return _td_dc3_cs; }
            set { _td_dc3_cs = value; }
        }
        /// <summary>
        /// 第三天第三次低潮潮高
        /// </summary>		
        private string _td_dc3_cg;
        public string TD_DC3_CG
        {
            get { return _td_dc3_cg; }
            set { _td_dc3_cg = value; }
        }
        /// <summary>
        /// 浪高数据1
        /// </summary>		
        private string _lg_data1;
        public string LG_DATA1
        {
            get { return _lg_data1; }
            set { _lg_data1 = value; }
        }
        /// <summary>
        /// 浪高数据2
        /// </summary>		
        private string _lg_data2;
        public string LG_DATA2
        {
            get { return _lg_data2; }
            set { _lg_data2 = value; }
        }
        /// <summary>
        /// 浪高数据3
        /// </summary>		
        private string _lg_data3;
        public string LG_DATA3
        {
            get { return _lg_data3; }
            set { _lg_data3 = value; }
        }
        /// <summary>
        /// 浪高数据4
        /// </summary>		
        private string _lg_data4;
        public string LG_DATA4
        {
            get { return _lg_data4; }
            set { _lg_data4 = value; }
        }
        /// <summary>
        /// 浪高数据5
        /// </summary>		
        private string _lg_data5;
        public string LG_DATA5
        {
            get { return _lg_data5; }
            set { _lg_data5 = value; }
        }
        /// <summary>
        /// 浪高数据6
        /// </summary>		
        private string _lg_data6;
        public string LG_DATA6
        {
            get { return _lg_data6; }
            set { _lg_data6 = value; }
        }
        /// <summary>
        /// 第一列时间
        /// </summary>		
        private string _dt1;
        public string DT1
        {
            get { return _dt1; }
            set { _dt1 = value; }
        }
        /// <summary>
        /// 水温数据
        /// </summary>		
        private string _sw_data1;
        public string SW_DATA1
        {
            get { return _sw_data1; }
            set { _sw_data1 = value; }
        }
        /// <summary>
        /// 第二列时间
        /// </summary>		
        private string _dt2;
        public string DT2
        {
            get { return _dt2; }
            set { _dt2 = value; }
        }
        /// <summary>
        /// 水温数据
        /// </summary>		
        private string _sw_data2;
        public string SW_DATA2
        {
            get { return _sw_data2; }
            set { _sw_data2 = value; }
        }
        /// <summary>
        /// 第二列时间
        /// </summary>		
        private string _dt3;
        public string DT3
        {
            get { return _dt3; }
            set { _dt3 = value; }
        }
        /// <summary>
        /// 水温数据
        /// </summary>		
        private string _sw_data3;
        public string SW_DATA3
        {
            get { return _sw_data3; }
            set { _sw_data3 = value; }
        }
        /// <summary>
        /// 联系人
        /// </summary>		
        private string _linkman;
        public string LINKMAN
        {
            get { return _linkman; }
            set { _linkman = value; }
        }
        /// <summary>
        /// 联系人电话
        /// </summary>		
        private string _linkphone;
        public string LINKPHONE
        {
            get { return _linkphone; }
            set { _linkphone = value; }
        }
    }
 
