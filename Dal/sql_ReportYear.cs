using PredicTable.Model.NewMediumAndLong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 
    /// </summary>
    public class sql_ReportYear
    {
        DataExecution DataExe = new DataExecution();//声明一个数据执行类

        /// <summary>
        /// 获取当天年信息
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetReportYear(ReportYearModel year)
        {
            try
            {
                var sql = "SELECT * FROM ht_reportyear WHERE publishtime='"+ year.PUBLISHTIME + "' AND publishcompany='" + year.PUBLISHCOMPANY + "' AND docname='" + year.DOCNAME + "'";
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
        /// <param name="company"></param>
        /// <returns></returns>
        public DataTable GetReportYearAll(string ptime,string company)
        {
            try
            {
                var sql = "SELECT * FROM ht_reportyear WHERE publishtime='" + ptime + "' AND publishcompany='" + company + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加年预报
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int InsertReportYear(ReportYearModel year)
        {
            try
            {
                //var sql = "INSERT INTO ht_reportyear(PUBLISHTIME,PUBLISHCOMPANY,REPORTNO,REPORTTITLE,STORMSURGE,SEAWAVE,REDTIDE,GREENTIDE,TROPICALCYCLONE,HEADREPORTER,DEPUTYREPORTER,DOCNAME,SENDDEPARTMENT) " + " VALUES " + " ('" + year.PUBLISHTIME + "','" + year.PUBLISHCOMPANY + "','" + year.REPORTNO + "','" + year.REPORTTITLE + "','" + year.STORMSURGE + "','" + year.SEAWAVE + "','" + year.REDTIDE + "',"+ " '" + year.GREENTIDE + "','" + year.TROPICALCYCLONE + "','" + year.HEADREPORTER + "','" + year.DEPUTYREPORTER + "','" + year.DOCNAME + "','" + year.SENDDEPARTMENT + "')";

                var sql = "INSERT INTO ht_reportyear(PUBLISHTIME,PUBLISHCOMPANY,REPORTNO,REPORTTITLE,STORMSURGE,SEAWAVE,REDTIDE,GREENTIDE,TROPICALCYCLONE,HEADREPORTER,DEPUTYREPORTER,DOCNAME,SENDDEPARTMENT,SEAICE) " + " VALUES " + " ('" + year.PUBLISHTIME + "','" + year.PUBLISHCOMPANY + "','" + year.REPORTNO + "','" + year.REPORTTITLE + "','" + year.STORMSURGE + "','" + year.SEAWAVE + "','" + year.REDTIDE + "'," + " '" + year.GREENTIDE + "','" + year.TROPICALCYCLONE + "','" + year.HEADREPORTER + "','" + year.DEPUTYREPORTER + "','" + year.DOCNAME + "','" + year.SENDDEPARTMENT + "','" + year.SEAICE + "')";
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改年预报
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int UpdateReportYear(ReportYearModel year)
        {
            try
            {
                //var sql = "UPDATE ht_reportyear SET "
                //    + " REPORTNO='" + year.REPORTNO + "',REPORTTITLE='" + year.REPORTTITLE + "',STORMSURGE='" + year.STORMSURGE + "',SEAWAVE='" + year.SEAWAVE + "',"
                //    + " REDTIDE='" + year.REDTIDE + "',GREENTIDE='" + year.GREENTIDE + "',TROPICALCYCLONE='" + year.TROPICALCYCLONE + "',HEADREPORTER='" + year.HEADREPORTER + "',DEPUTYREPORTER='" + year.DEPUTYREPORTER + "',"
                //    + " SENDDEPARTMENT='" + year.SENDDEPARTMENT + "'"
                //    + " WHERE PUBLISHTIME = '" + year.PUBLISHTIME + "' AND PUBLISHCOMPANY='" + year.PUBLISHCOMPANY + "' AND DOCNAME='" + year.DOCNAME + "'";

                var sql = "UPDATE ht_reportyear SET "
                    + " REPORTNO='" + year.REPORTNO + "',REPORTTITLE='" + year.REPORTTITLE + "',STORMSURGE='" + year.STORMSURGE + "',SEAWAVE='" + year.SEAWAVE + "',"
                    + " REDTIDE='" + year.REDTIDE + "',GREENTIDE='" + year.GREENTIDE + "',TROPICALCYCLONE='" + year.TROPICALCYCLONE + "',HEADREPORTER='" + year.HEADREPORTER + "',DEPUTYREPORTER='" + year.DEPUTYREPORTER + "',SENDDEPARTMENT='" + year.SENDDEPARTMENT + "',"
                    + " SEAICE='" + year.SEAICE + "'"
                    + " WHERE PUBLISHTIME = '" + year.PUBLISHTIME + "' AND PUBLISHCOMPANY='" + year.PUBLISHCOMPANY + "' AND DOCNAME='" + year.DOCNAME + "'";
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
        public int InsertFlow(string docName,byte[] b,byte[] btImg, string YBQUYU, string YBNEIRONGS, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            string sql = "INSERT INTO CG_YUBAO_ME(YBWENJIANMING,YBQUYU,YBNEIRONG,YBSHIXIAO,YBSHIJIAN,YBDANWEI) VALUES ('" + docName + "','" + YBQUYU + "','" + YBNEIRONGS + "','" + YBSHIXIAO + "',to_date('" + YBSHIJIAN + "','yyyy-mm-dd hh24@mi@ss'),'" + YBDANWEI + "')";
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
                DataExe.GetIntExeData(sql);
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
        public int UploadFlow(string docName, byte[] b, byte[] btImg ,string YBQUYU, string YBNEIRONGS, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            string sql = "";
            string sql1 = "UPDATE CG_YUBAO_ME SET YBQUYU = '" + YBQUYU + "',YBNEIRONG = '" + YBNEIRONGS + "',YBSHIXIAO = '" + YBSHIXIAO + "',YBSHIJIAN = to_date('" + YBSHIJIAN + "','yyyy-mm-dd hh24@mi@ss'),YBDANWEI = '" + YBDANWEI + "' where YBWENJIANMING = '" + docName + "'";
            //sql = "UPDATE CG_YUBAO_ME SET YBWENJIANMING = '" + docName + "'";
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
                DataExe.GetIntExeData(sql1);
                DataExe.GetIntExeData(sql, parameters);
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
    }
}