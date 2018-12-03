using PredicTable.Model.NewMediumAndLong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_ReportMonthOrDays
    {
        DataExecution DataExe = new DataExecution();//声明一个数据执行类

        /// <summary>
        /// 获取当天旬、月信息
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetReportMonthOrDays(ReportMonthModel month,string reportType)
        {
            try
            {
                var sql = "SELECT * FROM HT_REPORTMONTH WHERE PUBLISHTIME='" + month.PUBLISHTIME + "' AND PUBLISHCOMPANY='" + month.PUBLISHCOMPANY + "' AND DOCNAME='" + month.DOCNAME + "' AND REPORTTYPE = '"+ reportType + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询当天全部数据
        /// </summary>
        /// <param name="ptime"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetReportMonthOrDaysAll(string ptime, string type)
        {
            try
            {
                string types = "\""+type+"\"";
                var sql = "SELECT * FROM HT_REPORTMONTH WHERE PUBLISHTIME='" + ptime +"' AND REPORTTYPE = '" + types + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据type，X or M查询所有的数据
        /// </summary>
        /// <param name="ptime"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetReportMonthOrDaysByType(string type)
        {
            try
            {
                string types = "\"" + type + "\"";
                var sql = "SELECT * FROM HT_REPORTMONTH WHERE REPORTTYPE='" + types + "'  order by PUBLISHTIME desc";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        /// <summary>
        /// 添加旬、月预报
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int InsertReportMonthOrDays(ReportMonthModel month, string reportType)
        {
            try
            {
                var sql = "INSERT INTO HT_REPORTMONTH(PUBLISHTIME,PUBLISHCOMPANY,REPORTNO,REPORTTITLE,REPORTNORTH,REPORTSOUTH,REPORTCONTENT,HEADREPORTER,DEPUTYREPORTER,DOCNAME,SENDDEPARTMENT,REPORTTYPE) "
                    + " VALUES "
                    + " ('" + month.PUBLISHTIME + "','" + month.PUBLISHCOMPANY + "','" + month.REPORTNO + "','" + month.REPORTTITLE + "','" + month.REPORTNORTH + "','" + month.REPORTSOUTH + "','" + month.REPORTCONTENT + "',"
                    + " '" + month.HEADREPORTER + "','" + month.DEPUTYREPORTER + "','" + month.DOCNAME + "','" + month.SENDDEPARTMENT + "','"+ reportType + "')";
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改旬、月预报
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int UpdateReportMonthOrDays(ReportMonthModel month, string reportType)
        {
            try
            {
                var sql = "UPDATE HT_REPORTMONTH SET "
                    + " REPORTNO='" + month.REPORTNO + "',REPORTTITLE='" + month.REPORTTITLE + "',REPORTNORTH='" + month.REPORTNORTH + "',REPORTSOUTH='" + month.REPORTSOUTH + "',"
                    + " REPORTCONTENT='" + month.REPORTCONTENT + "',HEADREPORTER='" + month.HEADREPORTER + "',DEPUTYREPORTER='" + month.DEPUTYREPORTER + "',"
                    + " SENDDEPARTMENT='" + month.SENDDEPARTMENT + "'"
                    + " WHERE PUBLISHTIME = '" + month.PUBLISHTIME + "' AND PUBLISHCOMPANY='" + month.PUBLISHCOMPANY + "' AND DOCNAME='" + month.DOCNAME + "'  AND REPORTTYPE='" + reportType + "'";
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 插入文件流
        /// </summary>
        /// <param name="docName"></param>
        /// <param name="b"></param>
        /// <param name="btImg"></param>
        /// <returns></returns>
        public int InsertFlow_ME(string docName, byte[] b, byte[] btImg, string YBQUYU, string YBNEIRONGS, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            string sql = "INSERT INTO CG_YUBAO_ME(YBWENJIANMING,YBQUYU,YBNEIRONG,YBSHIXIAO,YBSHIJIAN,YBDANWEI) VALUES ('" + docName + "','" + YBQUYU + "','" + YBNEIRONGS + "','" + YBSHIXIAO + "',to_date('" + YBSHIJIAN + "','yyyy-mm-dd hh24@mi@ss'),'" + YBDANWEI + "')";
            //string sql2 = "INSERT INTO CG_YUBAO_FILE(YBWENJIANMING, YBNEIRONG, PICFILE) VALUES(@YBWENJIANMING,@YBNEIRONG , @PICFILE)";

            //var YBWENJIANMING = DataExe.GetDbParameter();
            //var YBNEIRONG = DataExe.GetDbParameter();
            //var PICFILE = DataExe.GetDbParameter();

            //YBWENJIANMING.ParameterName = "@YBWENJIANMING";
            //YBNEIRONG.ParameterName = "@YBNEIRONG";
            //PICFILE.ParameterName = "@PICFILE";

            //YBWENJIANMING.Value = docName;
            //YBNEIRONG.Value = b;
            //PICFILE.Value = btImg;

            //DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
            try
            {
                DataExe.GetIntExeData(sql);
                //DataExe.GetIntExeData(sql2, parameters);
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        /// <summary>
        /// 插入文件流 modify by xp 2018-8-27
        /// </summary>
        /// <param name="docName"></param>
        /// <param name="b"></param>
        /// <param name="btImg"></param>
        /// <returns></returns>
        public int InsertFlow_FILE(string docName, byte[] b, byte[] btImg, string YBQUYU, string YBNEIRONGS, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            //string sql = "INSERT INTO CG_YUBAO_ME(YBWENJIANMING,YBQUYU,YBNEIRONG,YBSHIXIAO,YBSHIJIAN,YBDANWEI) VALUES ('" + docName + "','" + YBQUYU + "','" + YBNEIRONGS + "','" + YBSHIXIAO + "',to_date('" + YBSHIJIAN + "','yyyy-mm-dd hh24@mi@ss'),'" + YBDANWEI + "')";
            string sql2 = "INSERT INTO CG_YUBAO_FILE(YBWENJIANMING, YBNEIRONG, PICFILE) VALUES(@YBWENJIANMING,@YBNEIRONG , @PICFILE)";

            var YBWENJIANMING = DataExe.GetDbParameter();
            var YBNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            YBWENJIANMING.ParameterName = "@YBWENJIANMING";
            YBNEIRONG.ParameterName = "@YBNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            YBWENJIANMING.Value = docName;
            YBNEIRONG.Value = b;
            PICFILE.Value = btImg;

            DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
            try
            {
                //DataExe.GetIntExeData(sql);
                DataExe.GetIntExeData(sql2, parameters);
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        /// <summary>
        /// 修改文件流
        /// </summary>
        /// <param name="docName"></param>
        /// <param name="b"></param>
        /// <param name="btImg"></param>
        /// <returns></returns>
        public int UploadFlow_ME(string docName, byte[] b, byte[] btImg, string YBQUYU, string YBNEIRONGS, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            //string sql = "";
            string sql1 = "UPDATE CG_YUBAO_ME SET YBQUYU = '" + YBQUYU + "',YBNEIRONG = '" + YBNEIRONGS + "',YBSHIXIAO = '" + YBSHIXIAO + "',YBSHIJIAN = to_date('" + YBSHIJIAN + "','yyyy-mm-dd hh24@mi@ss'),YBDANWEI = '" + YBDANWEI + "' where YBWENJIANMING = '" + docName + "'";
            //sql = "UPDATE CG_YUBAO_FILE SET YBNEIRONG=@YBNEIRONG, PICFILE = @PICFILE WHERE YBWENJIANMING = @YBWENJIANMING";
            //var YBWENJIANMING = DataExe.GetDbParameter();
            //var YBNEIRONG = DataExe.GetDbParameter();
            //var PICFILE = DataExe.GetDbParameter();

            //YBWENJIANMING.ParameterName = "@YBWENJIANMING";
            //YBNEIRONG.ParameterName = "@YBNEIRONG";
            //PICFILE.ParameterName = "@PICFILE";

            //YBWENJIANMING.Value = docName;
            //YBNEIRONG.Value = b;
            //PICFILE.Value = btImg;

            //DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
            try
            {
                DataExe.GetIntExeData(sql1);
                //DataExe.GetIntExeData(sql, parameters);
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        /// <summary>
        /// 修改文件流 modify by xp 2018-8-27
        /// </summary>
        /// <param name="docName"></param>
        /// <param name="b"></param>
        /// <param name="btImg"></param>
        /// <returns></returns>
        public int UploadFlow_FILE(string docName, byte[] b, byte[] btImg, string YBQUYU, string YBNEIRONGS, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            string sql = "";
            //string sql1 = "UPDATE CG_YUBAO_ME SET YBQUYU = '" + YBQUYU + "',YBNEIRONG = '" + YBNEIRONGS + "',YBSHIXIAO = '" + YBSHIXIAO + "',YBSHIJIAN = to_date('" + YBSHIJIAN + "','yyyy-mm-dd hh24@mi@ss'),YBDANWEI = '" + YBDANWEI + "' where YBWENJIANMING = '" + docName + "'";
            sql = "UPDATE CG_YUBAO_FILE SET YBNEIRONG=@YBNEIRONG, PICFILE = @PICFILE WHERE YBWENJIANMING = @YBWENJIANMING";
            var YBWENJIANMING = DataExe.GetDbParameter();
            var YBNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            YBWENJIANMING.ParameterName = "@YBWENJIANMING";
            YBNEIRONG.ParameterName = "@YBNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            YBWENJIANMING.Value = docName;
            YBNEIRONG.Value = b;
            PICFILE.Value = btImg;

            DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
            try
            {
                //DataExe.GetIntExeData(sql1);
                DataExe.GetIntExeData(sql, parameters);
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        //add by xp 2018-8-27
        public DataTable GetYUBAO_ME(string docName)
        {
            try
            {
                //string sql = "SELECT * FROM CG_HT_YUBAO_CONTENT WHERE YBWENJIANMING = '" + docName + "'";
                string sql = "SELECT * FROM  CG_YUBAO_ME  WHERE YBWENJIANMING = @YBWENJIANMING";


                var YBWENJIANMING = DataExe.GetDbParameter();


                YBWENJIANMING.ParameterName = "@YBWENJIANMING";


                YBWENJIANMING.Value = docName;

                DbParameter[] parameters = { YBWENJIANMING };
                return DataExe.GetTableExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发送单位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }
        //add by xp 2018-8-27
        public DataTable GetYUBAO_FILE(string docName)
        {
            try
            {
                //string sql = "SELECT * FROM CG_HT_YUBAO_CONTENT WHERE YBWENJIANMING = '" + docName + "'";
                string sql = "SELECT * FROM  CG_YUBAO_FILE  WHERE YBWENJIANMING = @YBWENJIANMING";


                var YBWENJIANMING = DataExe.GetDbParameter();


                YBWENJIANMING.ParameterName = "@YBWENJIANMING";


                YBWENJIANMING.Value = docName;

                DbParameter[] parameters = { YBWENJIANMING };
                return DataExe.GetTableExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发送单位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }
    }
}