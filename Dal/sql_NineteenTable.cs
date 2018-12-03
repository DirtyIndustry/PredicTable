using PredicTable.Model;
using PredicTable.Model.NineteenWord;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 保存19号预报单字段入库
    /// </summary>
    public class sql_NineteenTable
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_NineteenTable()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        #region  周、旬、月 预 报 单

        /// <summary>
        /// 获取19号表单周、旬、月主键
        /// </summary>
        public int GetNomalTableKey(NineteenNomalModel nomalModel)
        {
            string sql = "SELECT BHHBZHOUXUNYUE.Nextval FROM DUAL";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                   // nomalModel.ID = Convert.ToInt32(dt.Rows[0]["NEXTVAL"]).ToString();
                    return 1;
                }
                return 0;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取周、旬、月数据是否存在
        /// </summary>
        public int GetNomalKey(NineteenNomalModel nomalModel)
        {
            string sql = "SELECT * FROM HT_KJ_BHHBZHOUXUNYUE where ID=@ID";

            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = nomalModel.ID;
            DbParameter[] parameters = { ID };
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql, parameters);

                return dt.Rows.Count;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取周、旬、月数据是否存在
        /// </summary>
        public int GetNomalKey1(NineteenNomalModel nomalModel)
        {
        //    '%' || @DAIMA || '%' "
            string sql = "SELECT * FROM HT_KJ_BHHBZHOUXUNYUE where ID =@ID";

            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = nomalModel.ID;
            DbParameter[] parameters = { ID };
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql, parameters);

                return dt.Rows.Count;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 保存周旬月数据库主表
        /// HT_KJ_BHHBZHOUXUNYUE
        /// </summary>
        public int SaveNomalTable(NineteenNomalModel nomalModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHBZHOUXUNYUE (ID,PUBLISHDATE,ICESITUATION,PREDICT,PREDICTAGING,DESCRIPTION,CHUANZHEN,IPHONE,LINKMAN,FASONGDANWEI,SENDUNIT)" +
                "VALUES (@ID,to_date(@PUBLISHDATE,'yyyy-mm-dd'),@ICESITUATION,@PREDICT,@PREDICTAGING,@DESCRIPTION,@CHUANZHEN,@IPHONE,@LINKMAN,@FASONGDANWEI,@SENDUNIT)";

            var ID = DataExe.GetDbParameter();
            var PUBLISHDATE = DataExe.GetDbParameter();
            var ICESITUATION = DataExe.GetDbParameter();
            var PREDICT = DataExe.GetDbParameter();
            var PREDICTAGING = DataExe.GetDbParameter();
            var DESCRIPTION = DataExe.GetDbParameter();
            var CHUANZHEN = DataExe.GetDbParameter();
            var IPHONE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var FASONGDANWEI = DataExe.GetDbParameter();
            var SENDUNIT = DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            ICESITUATION.ParameterName = "@ICESITUATION";
            PREDICT.ParameterName = "@PREDICT";
            PREDICTAGING.ParameterName = "@PREDICTAGING";
            DESCRIPTION.ParameterName = "@DESCRIPTION";
            CHUANZHEN.ParameterName = "@CHUANZHEN";
            IPHONE.ParameterName = "@IPHONE";
            LINKMAN.ParameterName = "@LINKMAN";
            FASONGDANWEI.ParameterName = "@FASONGDANWEI";
            SENDUNIT.ParameterName = "@SENDUNIT";

            ID.Value = nomalModel.ID;
            PUBLISHDATE.Value = nomalModel.PUBLISHDATE.ToString("yyyy-MM-dd");
            ICESITUATION.Value = nomalModel.ICESITUATION==null ? ICESITUATION.Value= DBNull.Value : ICESITUATION.Value= nomalModel.ICESITUATION;
            PREDICT.Value = nomalModel.PREDICT == null ? PREDICT.Value = DBNull.Value : PREDICT.Value = nomalModel.PREDICT;
            PREDICTAGING.Value = nomalModel.PREDICTAGING == null ? PREDICTAGING.Value = DBNull.Value : PREDICTAGING.Value = nomalModel.PREDICTAGING;
            DESCRIPTION.Value = nomalModel.DESCRIPTION == null ? DESCRIPTION.Value = DBNull.Value : DESCRIPTION.Value = nomalModel.DESCRIPTION;
            CHUANZHEN.Value = nomalModel.CHUANZHEN == null ? CHUANZHEN.Value = DBNull.Value : CHUANZHEN.Value = nomalModel.CHUANZHEN;
            IPHONE.Value = nomalModel.IPHONE == null ? IPHONE.Value= DBNull.Value : IPHONE.Value=nomalModel.IPHONE;
            LINKMAN.Value = nomalModel.LINKMAN == null ? LINKMAN.Value = DBNull.Value : LINKMAN.Value = nomalModel.LINKMAN;
            FASONGDANWEI.Value = nomalModel.FASONGDANWEI == null ? FASONGDANWEI.Value = DBNull.Value : FASONGDANWEI.Value = nomalModel.FASONGDANWEI;
            SENDUNIT.Value = nomalModel.SENDUNIT==null? SENDUNIT.Value = DBNull.Value: SENDUNIT.Value = nomalModel.SENDUNIT;



            DbParameter[] parameters = { ID, PUBLISHDATE, ICESITUATION, PREDICT, PREDICTAGING, DESCRIPTION, CHUANZHEN, IPHONE, LINKMAN, FASONGDANWEI, SENDUNIT };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("周旬月数据插入失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改周旬月数据库主表
        /// HT_KJ_BHHBZHOUXUNYUE
        /// </summary>
        public int UpdateNomalTable(NineteenNomalModel nomalModel)
        {
            string sql = "Update HT_KJ_BHHBZHOUXUNYUE set PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd'),ICESITUATION=@ICESITUATION,PREDICT=@PREDICT,PREDICTAGING=@PREDICTAGING,DESCRIPTION=@DESCRIPTION,CHUANZHEN=@CHUANZHEN,IPHONE=@IPHONE,LINKMAN=@LINKMAN,FASONGDANWEI=@FASONGDANWEI,SENDUNIT=@SENDUNIT where ID = @ID";

            var ID = DataExe.GetDbParameter();
            var PUBLISHDATE = DataExe.GetDbParameter();
            var ICESITUATION = DataExe.GetDbParameter();
            var PREDICT = DataExe.GetDbParameter();
            var PREDICTAGING = DataExe.GetDbParameter();
            var DESCRIPTION = DataExe.GetDbParameter();
            var CHUANZHEN = DataExe.GetDbParameter();
            var IPHONE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var FASONGDANWEI = DataExe.GetDbParameter();
            var SENDUNIT = DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            ICESITUATION.ParameterName = "@ICESITUATION";
            PREDICT.ParameterName = "@PREDICT";
            PREDICTAGING.ParameterName = "@PREDICTAGING";
            DESCRIPTION.ParameterName = "@DESCRIPTION";
            CHUANZHEN.ParameterName = "@CHUANZHEN";
            IPHONE.ParameterName = "@IPHONE";
            LINKMAN.ParameterName = "@LINKMAN";
            FASONGDANWEI.ParameterName = "@FASONGDANWEI";
            SENDUNIT.ParameterName = "@SENDUNIT";

            ID.Value = nomalModel.ID;
            PUBLISHDATE.Value = nomalModel.PUBLISHDATE.ToString("yyyy-MM-dd");
            ICESITUATION.Value = nomalModel.ICESITUATION == null ? ICESITUATION.Value = DBNull.Value : ICESITUATION.Value = nomalModel.ICESITUATION;
            PREDICT.Value = nomalModel.PREDICT == null ? PREDICT.Value = DBNull.Value : PREDICT.Value = nomalModel.PREDICT;
            PREDICTAGING.Value = nomalModel.PREDICTAGING == null ? PREDICTAGING.Value = DBNull.Value : PREDICTAGING.Value = nomalModel.PREDICTAGING;
            DESCRIPTION.Value = nomalModel.DESCRIPTION == null ? DESCRIPTION.Value = DBNull.Value : DESCRIPTION.Value = nomalModel.DESCRIPTION;
            CHUANZHEN.Value = nomalModel.CHUANZHEN == null ? CHUANZHEN.Value = DBNull.Value : CHUANZHEN.Value = nomalModel.CHUANZHEN;
            IPHONE.Value = nomalModel.IPHONE == null ? IPHONE.Value = DBNull.Value : IPHONE.Value = nomalModel.IPHONE;
            LINKMAN.Value = nomalModel.LINKMAN == null ? LINKMAN.Value = DBNull.Value : LINKMAN.Value = nomalModel.LINKMAN;
            FASONGDANWEI.Value = nomalModel.FASONGDANWEI == null ? FASONGDANWEI.Value = DBNull.Value : FASONGDANWEI.Value = nomalModel.FASONGDANWEI;
            SENDUNIT.Value = nomalModel.SENDUNIT == null ? SENDUNIT.Value = DBNull.Value : SENDUNIT.Value = nomalModel.SENDUNIT;


            DbParameter[] parameters = { ID,PUBLISHDATE, ICESITUATION, PREDICT, PREDICTAGING, DESCRIPTION, CHUANZHEN, IPHONE, LINKMAN, FASONGDANWEI, SENDUNIT };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("周旬月数据修改失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改周旬月图片数据库主表
        /// HT_KJ_BHHBZHOUXUNYUE
        /// </summary>
        public int UpdateImgNomalTable(NineteenNomalModel nomalModel)
        {
            string sql = "Update HT_KJ_BHHBZHOUXUNYUE set BMP=@BMP where ID = @ID";
          
            var BMP = DataExe.GetDbParameter();
            var ID = DataExe.GetDbParameter();


            BMP.ParameterName = "@BMP";
            ID.ParameterName = "@ID";


            BMP.Value = nomalModel.BMP;
            ID.Value = nomalModel.ID;


            DbParameter[] parameters = { ID, BMP };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("周旬月数据修改失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 保存周旬月图片数据库主表
        /// HT_KJ_BHHBZHOUXUNYUE
        /// </summary>
        public int SaveImgNomalTable(NineteenNomalModel nomalModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHBZHOUXUNYUE (ID,BMP)" +
                "VALUES (@ID,@BMP)";

            var ID = DataExe.GetDbParameter();
            var BMP = DataExe.GetDbParameter();
           
            ID.ParameterName = "@ID";
            BMP.ParameterName = "@BMP";
           
            ID.Value = nomalModel.ID;
            BMP.Value = nomalModel.BMP;
           
            DbParameter[] parameters = { ID, BMP};
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("周旬月图片数据插入失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 保存周旬月数据库子表
        /// HT_KJ_BHHBZHOUXUNYUE_LINE
        /// </summary>
        public int SaveNomalTableLine(NineteenNomalLineModel nineteenNomalLineModel, NineteenNomalModel nomalModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHBZHOUXUNYUE_LINE (ID,WID,NAME,TERMINALLINE,GENERALICETHICKNESS,MAXICETHICKNESS)" +
                "VALUES (to_char(BHHBZHOUXUNYUE_LINE.Nextval),@WID,@NAME,@TERMINALLINE,@GENERALICETHICKNESS,@MAXICETHICKNESS)";
            //var ID = DataExe.GetDbParameter();
            var WID = DataExe.GetDbParameter();
            var NAME = DataExe.GetDbParameter();
            var TERMINALLINE = DataExe.GetDbParameter();
            var GENERALICETHICKNESS = DataExe.GetDbParameter();
            var MAXICETHICKNESS = DataExe.GetDbParameter();

            //ID.ParameterName = "@ID";
            WID.ParameterName = "@WID";
            NAME.ParameterName = "@NAME";
            TERMINALLINE.ParameterName = "@TERMINALLINE";
            GENERALICETHICKNESS.ParameterName = "@GENERALICETHICKNESS";
            MAXICETHICKNESS.ParameterName = "@MAXICETHICKNESS";

         
            WID.Value = nomalModel.ID;
            NAME.Value = nineteenNomalLineModel.NAME == null ? NAME.Value = "" : NAME.Value = nineteenNomalLineModel.NAME;
            TERMINALLINE.Value = nineteenNomalLineModel.TERMINALLINE == null ? TERMINALLINE.Value = "" : TERMINALLINE.Value = nineteenNomalLineModel.TERMINALLINE;
            GENERALICETHICKNESS.Value = nineteenNomalLineModel.GENERALICETHICKNESS == null ? GENERALICETHICKNESS.Value = "" : GENERALICETHICKNESS.Value = nineteenNomalLineModel.GENERALICETHICKNESS;
            MAXICETHICKNESS.Value = nineteenNomalLineModel.MAXICETHICKNESS == null ? MAXICETHICKNESS.Value = " " : MAXICETHICKNESS.Value = nineteenNomalLineModel.MAXICETHICKNESS;



            DbParameter[] parameters = { WID, NAME, TERMINALLINE, GENERALICETHICKNESS, MAXICETHICKNESS };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("周旬月表格数据插入失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 删除渤海及黄海北部冰日预测
        /// HT_KJ_BHHBNIAN_ICE
        /// </summary>
        public int DelNomalTableLine(NineteenNomalModel nomalModel)
        {
            string sql = "delete from HT_KJ_BHHBZHOUXUNYUE_LINE where WID = @ID";
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = nomalModel.ID;
            DbParameter[] parameters = { ID };
            int a = DataExe.GetIntExeData(sql, parameters);
            return a;
        }
        #endregion

        #region 年 预 报 单

        /// <summary>
        /// 获取海冰表年主键
        /// </summary>
        public int GetNomalYearTableKey(NineteenYearModel yearModel)
        {
            string sql = "SELECT BHHBNIAN.Nextval FROM DUAL";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    yearModel.ID = Convert.ToInt32(dt.Rows[0]["NEXTVAL"]).ToString();
                    return 1;
                }
                return 0;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取年数据是否存在
        /// </summary>
        public int GetYearKey(NineteenYearModel yearModel)
        {
            string sql = "SELECT * FROM HT_KJ_BHHBNIAN where ID=@ID";

            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = yearModel.ID;
            DbParameter[] parameters = { ID };
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql, parameters);
               
                return dt.Rows.Count;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        
        /// <summary>
        /// 添加年数据库主表
        /// HT_KJ_BHHBNIAN
        /// </summary>
        public int SaveYearTable(NineteenYearModel yearModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHBNIAN (ID,PUBLISHDATE,ICESITUATION,PREDICT,DESCRIPTION,CHUANZHEN,IPHONE,LINKMAN,FASONGDANWEI,SENDUNIT)" +
                "VALUES (@ID,to_date(@PUBLISHDATE,'yyyy-mm-dd'),@ICESITUATION,@PREDICT,@DESCRIPTION,@CHUANZHEN,@IPHONE,@LINKMAN,@FASONGDANWEI,@SENDUNIT)";

            var ID = DataExe.GetDbParameter();
            var PUBLISHDATE = DataExe.GetDbParameter();
            var ICESITUATION = DataExe.GetDbParameter();
            var PREDICT = DataExe.GetDbParameter();
            var DESCRIPTION = DataExe.GetDbParameter();
            var CHUANZHEN = DataExe.GetDbParameter();
            var IPHONE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var FASONGDANWEI = DataExe.GetDbParameter();
            var SENDUNIT = DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            ICESITUATION.ParameterName = "@ICESITUATION";
            PREDICT.ParameterName = "@PREDICT";
            DESCRIPTION.ParameterName = "@DESCRIPTION";
            CHUANZHEN.ParameterName = "@CHUANZHEN";
            IPHONE.ParameterName = "@IPHONE";
            LINKMAN.ParameterName = "@LINKMAN";
            FASONGDANWEI.ParameterName = "@FASONGDANWEI";
            SENDUNIT.ParameterName = "@SENDUNIT";

            ID.Value = yearModel.ID;
            PUBLISHDATE.Value = yearModel.PUBLISHDATE.ToString("yyyy-MM-dd");
            ICESITUATION.Value = yearModel.ICESITUATION == null ? ICESITUATION.Value = DBNull.Value : ICESITUATION.Value = yearModel.ICESITUATION;
            PREDICT.Value = yearModel.PREDICT == null ? PREDICT.Value = DBNull.Value : PREDICT.Value = yearModel.PREDICT;
            DESCRIPTION.Value = yearModel.DESCRIPTION == null ? DESCRIPTION.Value = DBNull.Value : DESCRIPTION.Value = yearModel.DESCRIPTION;
            CHUANZHEN.Value = yearModel.CHUANZHEN == null ? CHUANZHEN.Value = DBNull.Value : CHUANZHEN.Value = yearModel.CHUANZHEN;
            IPHONE.Value = yearModel.IPHONE == null ? IPHONE.Value = DBNull.Value : IPHONE.Value = yearModel.IPHONE;
            LINKMAN.Value = yearModel.LINKMAN == null ? LINKMAN.Value = DBNull.Value : LINKMAN.Value = yearModel.LINKMAN;
            FASONGDANWEI.Value = yearModel.FASONGDANWEI == null ? FASONGDANWEI.Value = DBNull.Value : FASONGDANWEI.Value = yearModel.FASONGDANWEI;
            SENDUNIT.Value = yearModel.SENDUNIT == null ? SENDUNIT.Value = DBNull.Value : SENDUNIT.Value = yearModel.SENDUNIT;


            DbParameter[] parameters = { ID, PUBLISHDATE, ICESITUATION, PREDICT, DESCRIPTION, CHUANZHEN, IPHONE, LINKMAN, FASONGDANWEI,SENDUNIT };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("年数据插入失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改周旬月图片数据库主表
        /// HT_KJ_BHHBZHOUXUNYUE
        /// </summary>
        public int UpdateImgYearTable(NineteenYearModel yearModel)
        {
            string sql = "Update HT_KJ_BHHBNIAN set BMP=@BMP where ID = @ID";

            var BMP = DataExe.GetDbParameter();
            var ID = DataExe.GetDbParameter();


            BMP.ParameterName = "@BMP";
            ID.ParameterName = "@ID";


            BMP.Value = yearModel.BMP;
            ID.Value = yearModel.ID;


            DbParameter[] parameters = { ID, BMP };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("年图片数据修改失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 保存年图片数据库主表
        /// HT_KJ_BHHBZHOUXUNYUE
        /// </summary>
        public int SaveImgYearTable(NineteenYearModel yearModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHBNIAN (ID,BMP)" +
                "VALUES (@ID,@BMP)";

            var ID = DataExe.GetDbParameter();
            var BMP = DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            BMP.ParameterName = "@BMP";

            ID.Value = yearModel.ID;
            BMP.Value = yearModel.BMP;

            DbParameter[] parameters = { ID, BMP };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("年图片数据插入失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除年数据库主表
        /// HT_KJ_BHHBNIAN
        /// </summary>
        public int UpdateYearTable(NineteenYearModel yearModel)
        {
            string sql = "Update HT_KJ_BHHBNIAN set PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd') ,ICESITUATION=@ICESITUATION,PREDICT=@PREDICT,DESCRIPTION=@DESCRIPTION,CHUANZHEN=@CHUANZHEN,IPHONE=@IPHONE,LINKMAN=@LINKMAN,FASONGDANWEI=@FASONGDANWEI,SENDUNIT=@SENDUNIT where ID=@ID";

            var ID = DataExe.GetDbParameter();
            var PUBLISHDATE = DataExe.GetDbParameter();
            var ICESITUATION = DataExe.GetDbParameter();
            var PREDICT = DataExe.GetDbParameter();
            var DESCRIPTION = DataExe.GetDbParameter();
            var CHUANZHEN = DataExe.GetDbParameter();
            var IPHONE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var FASONGDANWEI = DataExe.GetDbParameter();
            var SENDUNIT = DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            ICESITUATION.ParameterName = "@ICESITUATION";
            PREDICT.ParameterName = "@PREDICT";
            DESCRIPTION.ParameterName = "@DESCRIPTION";
            CHUANZHEN.ParameterName = "@CHUANZHEN";
            IPHONE.ParameterName = "@IPHONE";
            LINKMAN.ParameterName = "@LINKMAN";
            FASONGDANWEI.ParameterName = "@FASONGDANWEI";
            SENDUNIT.ParameterName = "@SENDUNIT";

            ID.Value = yearModel.ID;
            PUBLISHDATE.Value = yearModel.PUBLISHDATE.ToString("yyyy-MM-dd");
            ICESITUATION.Value = yearModel.ICESITUATION == null ? ICESITUATION.Value = DBNull.Value : ICESITUATION.Value = yearModel.ICESITUATION;
            PREDICT.Value = yearModel.PREDICT == null ? PREDICT.Value = DBNull.Value : PREDICT.Value = yearModel.PREDICT;
            DESCRIPTION.Value = yearModel.DESCRIPTION == null ? DESCRIPTION.Value = DBNull.Value : DESCRIPTION.Value = yearModel.DESCRIPTION;
            CHUANZHEN.Value = yearModel.CHUANZHEN == null ? CHUANZHEN.Value = DBNull.Value : CHUANZHEN.Value = yearModel.CHUANZHEN;
            IPHONE.Value = yearModel.IPHONE == null ? IPHONE.Value = DBNull.Value : IPHONE.Value = yearModel.IPHONE;
            LINKMAN.Value = yearModel.LINKMAN == null ? LINKMAN.Value = DBNull.Value : LINKMAN.Value = yearModel.LINKMAN;
            FASONGDANWEI.Value = yearModel.FASONGDANWEI == null ? FASONGDANWEI.Value = DBNull.Value : FASONGDANWEI.Value = yearModel.FASONGDANWEI;
            SENDUNIT.Value = yearModel.SENDUNIT == null ? SENDUNIT.Value = DBNull.Value : SENDUNIT.Value = yearModel.SENDUNIT;

            DbParameter[] parameters = { ID, PUBLISHDATE, ICESITUATION, PREDICT, DESCRIPTION, CHUANZHEN, IPHONE, LINKMAN, FASONGDANWEI, SENDUNIT };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("年数据修改失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除渤海及黄海北部冰日预测
        /// HT_KJ_BHHBNIAN_ICE
        /// </summary>
        public int DelYearTableIce(NineteenYearModel yearModel)
        {
            string sql = "delete from HT_KJ_BHHBNIAN_ICE where WID = @ID";
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = yearModel.ID;
            DbParameter[] parameters = {ID};
            int a = DataExe.GetIntExeData(sql, parameters);
            return a;
        }
        /// <summary>
        /// 保存渤海及黄海北部冰日预测
        /// HT_KJ_BHHBNIAN_ICE
        /// </summary>
        public int SaveYearTableIce(NineteenYearICEModel nineteenYearICEModel, NineteenYearModel yearModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHBNIAN_ICE (ID,WID,NAME,FIRSTFROZENDAY,SERIOUSICE,ICEMELTINGDAY,ICEDISAPPDAY)" +
                " VALUES (to_char(BHHBNIAN_ICE.Nextval),@WID,@NAME,@FIRSTFROZENDAY,@SERIOUSICE,@ICEMELTINGDAY,@ICEDISAPPDAY)";

            //var ID = DataExe.GetDbParameter();
            var WID = DataExe.GetDbParameter();
            var NAME = DataExe.GetDbParameter();
            var FIRSTFROZENDAY = DataExe.GetDbParameter();
            var SERIOUSICE = DataExe.GetDbParameter();
            var ICEMELTINGDAY = DataExe.GetDbParameter();
            var ICEDISAPPDAY = DataExe.GetDbParameter();

           
            WID.ParameterName = "@WID";
            NAME.ParameterName = "@NAME";
            FIRSTFROZENDAY.ParameterName = "@FIRSTFROZENDAY";
            SERIOUSICE.ParameterName = "@SERIOUSICE";
            ICEMELTINGDAY.ParameterName = "@ICEMELTINGDAY";
            ICEDISAPPDAY.ParameterName = "@ICEDISAPPDAY";

            //ID.Value = nomalModel.ID;
            WID.Value = yearModel.ID;
            NAME.Value = nineteenYearICEModel.NAME == null ? NAME.Value = "" : NAME.Value = nineteenYearICEModel.NAME;
            FIRSTFROZENDAY.Value = nineteenYearICEModel.FIRSTFROZENDAY == null ? FIRSTFROZENDAY.Value = "" : FIRSTFROZENDAY.Value = nineteenYearICEModel.FIRSTFROZENDAY;
            SERIOUSICE.Value = nineteenYearICEModel.SERIOUSICE == null ? SERIOUSICE.Value = "" : SERIOUSICE.Value = nineteenYearICEModel.SERIOUSICE;
            ICEMELTINGDAY.Value = nineteenYearICEModel.ICEMELTINGDAY == null ? ICEMELTINGDAY.Value = " " : ICEMELTINGDAY.Value = nineteenYearICEModel.ICEMELTINGDAY;
            ICEDISAPPDAY.Value = nineteenYearICEModel.ICEDISAPPDAY == null ? ICEDISAPPDAY.Value = " " : ICEDISAPPDAY.Value = nineteenYearICEModel.ICEDISAPPDAY;


            DbParameter[] parameters = { WID, NAME, FIRSTFROZENDAY, SERIOUSICE, ICEMELTINGDAY, ICEDISAPPDAY };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("年表格渤海及黄海北部冰日预测数据插入失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除浮冰外缘线离岸最大距离及平整冰厚度预测
        /// HT_KJ_BHHBNIAN_LINE
        /// </summary>
        public int DelYearTableLine(NineteenYearModel yearModel)
        {
            string sql = "delete from HT_KJ_BHHBNIAN_LINE where WID = @ID";
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = yearModel.ID;
            DbParameter[] parameters = { ID };
            int a = DataExe.GetIntExeData(sql, parameters);
            return a;
        }
        /// <summary>
        /// 浮冰外缘线离岸最大距离及平整冰厚度预测
        /// HT_KJ_BHHBNIAN_LINE
        /// </summary>
        public int SaveYearTableLine(NineteenYearLineModel nineteenYearLineModel, NineteenYearModel yearModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHBNIAN_LINE (ID,WID,NAME,TERMINALLINE,GENERALICETHICKNESS,MAXICETHICKNESS)" +
                " VALUES (to_char(BHHBNIAN_LINE.Nextval),@WID,@NAME,@TERMINALLINE,@GENERALICETHICKNESS,@MAXICETHICKNESS)";

          
            var WID = DataExe.GetDbParameter();
            var NAME = DataExe.GetDbParameter();
            var TERMINALLINE = DataExe.GetDbParameter();
            var GENERALICETHICKNESS = DataExe.GetDbParameter();
            var MAXICETHICKNESS = DataExe.GetDbParameter();

            
            WID.ParameterName = "@WID";
            NAME.ParameterName = "@NAME";
            TERMINALLINE.ParameterName = "@TERMINALLINE";
            GENERALICETHICKNESS.ParameterName = "@GENERALICETHICKNESS";
            MAXICETHICKNESS.ParameterName = "@MAXICETHICKNESS";

            
            WID.Value = yearModel.ID;
            NAME.Value = nineteenYearLineModel.NAME == null ? NAME.Value = "" : NAME.Value = nineteenYearLineModel.NAME;
            TERMINALLINE.Value = nineteenYearLineModel.TERMINALLINE == null ? TERMINALLINE.Value = "" : TERMINALLINE.Value = nineteenYearLineModel.TERMINALLINE;
            GENERALICETHICKNESS.Value = nineteenYearLineModel.GENERALICETHICKNESS == null ? GENERALICETHICKNESS.Value = "" : GENERALICETHICKNESS.Value = nineteenYearLineModel.GENERALICETHICKNESS;
            MAXICETHICKNESS.Value = nineteenYearLineModel.MAXICETHICKNESS == null ? MAXICETHICKNESS.Value = " " : MAXICETHICKNESS.Value = nineteenYearLineModel.MAXICETHICKNESS;



            DbParameter[] parameters = { WID, NAME, TERMINALLINE, GENERALICETHICKNESS, MAXICETHICKNESS };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("年表格浮冰外缘线离岸最大距离及平整冰厚度预测数据插入失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 严重冰期沿岸主要港口与海岛平整冰厚度预测
        /// HT_KJ_BHHBNIAN_LINE
        /// </summary>
        public int DelYearTableCkness(NineteenYearModel yearModel)
        {
            string sql = "delete from HT_KJ_BHHBNIAN_CKNESS where WID = @ID";
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = yearModel.ID;
            DbParameter[] parameters = { ID };
            int a = DataExe.GetIntExeData(sql, parameters);
            return a;
        }
        /// <summary>
        /// 严重冰期沿岸主要港口与海岛平整冰厚度预测
        /// HT_KJ_BHHBNIAN_CKNESS
        /// </summary>
        public int SaveYearTableCkness(NineteenYearCknessModel nineteenYearCknessModel, NineteenYearModel yearModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHBNIAN_CKNESS (ID,WID,NAME,GENERALICETHICKNESS,MAXICETHICKNESS)" +
                " VALUES (to_char(BHHBNIAN_CKNESS.Nextval),@WID,@NAME,@GENERALICETHICKNESS,@MAXICETHICKNESS)";

            
            var WID = DataExe.GetDbParameter();
            var NAME = DataExe.GetDbParameter();
            var GENERALICETHICKNESS = DataExe.GetDbParameter();
            var MAXICETHICKNESS = DataExe.GetDbParameter();

            
            WID.ParameterName = "@WID";
            NAME.ParameterName = "@NAME";
            GENERALICETHICKNESS.ParameterName = "@GENERALICETHICKNESS";
            MAXICETHICKNESS.ParameterName = "@MAXICETHICKNESS";

            
            WID.Value = yearModel.ID;
            NAME.Value = nineteenYearCknessModel.NAME == null ? NAME.Value = "" : NAME.Value = nineteenYearCknessModel.NAME;
            GENERALICETHICKNESS.Value = nineteenYearCknessModel.GENERALICETHICKNESS == null ? GENERALICETHICKNESS.Value = "" : GENERALICETHICKNESS.Value = nineteenYearCknessModel.GENERALICETHICKNESS;
            MAXICETHICKNESS.Value = nineteenYearCknessModel.MAXICETHICKNESS == null ? MAXICETHICKNESS.Value = " " : MAXICETHICKNESS.Value = nineteenYearCknessModel.MAXICETHICKNESS;



            DbParameter[] parameters = { WID, NAME, GENERALICETHICKNESS, MAXICETHICKNESS };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("周旬月表格数据插入失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        #endregion

        /// <summary>
        /// 获取文档主键
        /// </summary>
        /// <param name="nomalFileModel"></param>
        /// <returns></returns>
        public int GetNomalTableFileKey(NineteenNomalFileModel nomalFileModel)
        {
            string sql = "SELECT BHHB_FILE.Nextval FROM DUAL";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    nomalFileModel.ID = Convert.ToInt32(dt.Rows[0]["NEXTVAL"]);
                    return 1;
                }
                return 0;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 保存19号表单入库
        /// HT_KJ_BHHB_FILE
        /// </summary>
        /// <param name="docname">表单名称</param>
        /// <param name="b">表单文件流</param>
        /// <param name="PUBLISHDATE">发布日期</param>
        /// <param name="PREDICTAGING">表单类型（周、旬、月）</param>
        /// <returns></returns>
        public int SaveFile(NineteenNomalFileModel nomalFileModel)
        {
            string sql = "INSERT INTO  HT_KJ_BHHB_FILE (ID,FILENAME,FILEFLOW,FUBLISHDATE,FILETYPE)" +
               " VALUES (@ID,@FILENAME,@FILEFLOW,@PUBLISHDATE,@FILETYPE)";

            var ID = DataExe.GetDbParameter();
            var FILENAME = DataExe.GetDbParameter();
            var FILEFLOW = DataExe.GetDbParameter();
            var PUBLISHDATE = DataExe.GetDbParameter();
            var FILETYPE = DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            FILENAME.ParameterName = "@FILENAME";
            FILEFLOW.ParameterName = "@FILEFLOW";
            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FILETYPE.ParameterName = "@FILETYPE";

            ID.Value = nomalFileModel.ID;
            FILENAME.Value = nomalFileModel.FILENAME == null ? FILENAME.Value = "" : FILENAME.Value = nomalFileModel.FILENAME;
            FILEFLOW.Value = nomalFileModel.FILEFLOW == null ? FILEFLOW.Value = DBNull.Value : FILEFLOW.Value = nomalFileModel.FILEFLOW;
            PUBLISHDATE.Value = nomalFileModel.PUBLISHDATE.ToString("yyyy-MM-dd");
            FILETYPE.Value = nomalFileModel.FILETYPE == null ? FILETYPE.Value = "" : FILETYPE.Value = nomalFileModel.FILETYPE;

            DbParameter[] parameters = { ID, FILENAME, FILEFLOW, PUBLISHDATE, FILETYPE};
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("19号表单入库失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改19号表单入库
        /// HT_KJ_BHHB_FILE
        /// </summary>
        /// <param name="docname">表单名称</param>
        /// <param name="b">表单文件流</param>
        /// <param name="PUBLISHDATE">发布日期</param>
        /// <param name="PREDICTAGING">表单类型（周、旬、月）</param>
        /// <returns></returns>
        public int DeleteFile(NineteenNomalFileModel nomalFileModel)
        {
            string sql = "Delete HT_KJ_BHHB_FILE where FILENAME=@FILENAME ";

            var FILENAME = DataExe.GetDbParameter();

            FILENAME.ParameterName = "@FILENAME";

            FILENAME.Value = nomalFileModel.FILENAME;
          
            DbParameter[] parameters = { FILENAME };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("海冰入库失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取文件是否存在
        /// </summary>
        public int GetFile(NineteenNomalFileModel nomalFileModel)
        {
            string sql = "SELECT * FROM HT_KJ_BHHB_FILE where FILENAME=@FILENAME";

            var FILENAME = DataExe.GetDbParameter();
            FILENAME.ParameterName = "@FILENAME";
            FILENAME.Value = nomalFileModel.FILENAME;
            DbParameter[] parameters = { FILENAME };
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql, parameters);

                return dt.Rows.Count;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

    }
}