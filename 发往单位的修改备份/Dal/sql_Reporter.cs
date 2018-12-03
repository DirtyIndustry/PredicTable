using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;


namespace PredicTable.Dal
{
    /// <summary>
    /// 预报员Dal
    /// </summary>
    public class sql_Reporter
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_Reporter() {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 获取预报员信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagerow"></param>
        /// <returns></returns>
        public DataTable GetReporter(int pagenum, int pagerow)
        {
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;



            string sql = "select * from(select t.*,rownum rn from(" +
                    " SELECT a.id,a.reportername, a.reportercode, b.reportertype, b.reportertypeid,a.reportertel FROM ht_report_reporterinfo A, ht_report_reporter B WHERE a.reporterid = b.reportertypeid ORDER BY a.id" + " ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                return null;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        public DataTable GetReporter(int pagenum, int pagerow,string reportid)
        {
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;



            // string sql = "select * from(select t.*,rownum rn from(" +" SELECT a.id,a.reportername, a.reportercode, b.reportertype, b.reportertypeid,a.reportertel FROM ht_report_reporterinfo A, ht_report_reporter B WHERE a.reporterid = b.reportertypeid ORDER BY a.id" + " ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";

            string sql = "select * from(select t.*,rownum rn from(" + " SELECT a.id,a.reportername, a.reportercode, b.reportertype, b.reportertypeid,a.reportertel FROM ht_report_reporterinfo A, ht_report_reporter B WHERE a.reporterid = b.reportertypeid and a.reporterid = '"+reportid+"' ORDER BY a.id" + " ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";


            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                return null;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取预报员总数
        /// </summary>
        /// <returns></returns>
        public int GetReportCount()
        {
            try
            {
                return Convert.ToInt32(DataExe.GetObjectExeData("SELECT COUNT(*) from ht_report_reporterinfo A, ht_report_reporter B WHERE a.reporterid = b.reportertypeid"));
            }
            catch (Exception error)
            {
                WriteLog.Write("获取预报员总数出现异常！" + error.Message + "\r\n" + error.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 添加预报员信息
        /// </summary>
        /// <param name="reporterModel">预报员信息Model</param>
        /// REPORTER
        /// <returns></returns>
        public int SubmitReporter(ReporterModel reporterModel)
        {
            string sql = "INSERT INTO  HT_REPORT_REPORTERINFO (ID,REPORTERNAME,REPORTERCODE,REPORTERID,REPORTERTEL) VALUES (REPORTER.Nextval,@REPORTERNAME,@REPORTERCODE,@REPORTERID,@REPORTERTEL)";

            var REPORTERNAME = DataExe.GetDbParameter();
            var REPORTERCODE = DataExe.GetDbParameter();
            var REPORTERID = DataExe.GetDbParameter();
            var REPORTERTEL = DataExe.GetDbParameter();

            REPORTERNAME.ParameterName = "@REPORTERNAME";
            REPORTERCODE.ParameterName = "@REPORTERCODE";
            REPORTERID.ParameterName = "@REPORTERID";
            REPORTERTEL.ParameterName = "@REPORTERTEL";

            REPORTERNAME.Value = reporterModel.ReporterName;
            REPORTERCODE.Value = reporterModel.ReporterCode;
            REPORTERID.Value = reporterModel.ReporterType;
            REPORTERTEL.Value = reporterModel.ReporterTel;

            DbParameter[] parameters = { REPORTERNAME, REPORTERCODE, REPORTERID, REPORTERTEL };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加预报员信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改预报员信息
        /// </summary>
        /// <param name="reporterModel">预报员信息Model</param>
        /// <returns></returns>
        public int EditReporter(ReporterModel reporterModel)
        {
            string sql = "UPDATE  HT_REPORT_REPORTERINFO SET  REPORTERNAME = @REPORTERNAME,REPORTERTEL = @REPORTERTEL, REPORTERCODE = @REPORTERCODE, REPORTERID = @REPORTERID WHERE ID = @ID";

            var ID = DataExe.GetDbParameter();
            var REPORTERNAME = DataExe.GetDbParameter();
            var REPORTERTEL = DataExe.GetDbParameter();
            var REPORTERCODE = DataExe.GetDbParameter();
            var REPORTERID = DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            REPORTERNAME.ParameterName = "@REPORTERNAME";
            REPORTERTEL.ParameterName = "@REPORTERTEL";
            REPORTERCODE.ParameterName = "@REPORTERCODE";
            REPORTERID.ParameterName = "@REPORTERID";

            ID.Value = reporterModel.ID;
            REPORTERNAME.Value = reporterModel.ReporterName;
            REPORTERTEL.Value = reporterModel.ReporterTel;
            REPORTERCODE.Value = reporterModel.ReporterCode;
            REPORTERID.Value = reporterModel.ReporterType;

            DbParameter[] parameters = { ID, REPORTERNAME, REPORTERTEL, REPORTERCODE, REPORTERID };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改预报员信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 删除预报员信息
        /// </summary>
        /// <param name="id">预报员id</param>
        /// <returns></returns>
        public int DeleteReporter(int id)
        {
            string sql = "DELETE FROM HT_REPORT_REPORTERINFO WHERE ID = " + id;

            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除预报员信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}