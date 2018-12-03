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
    /// 处理预报单字段
    /// 保存入库
    /// </summary>
    public class sql_UpLoadModel
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_UpLoadModel()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 获取主预报员
        /// </summary>
        /// <returns></returns>
        public DataTable GetHeaderReporter()
        {
            string sql = "SELECT A.ID, A.REPORTERNAME, A.REPORTERCODE FROM HT_REPORT_REPORTERINFO A, HT_REPORT_REPORTER B WHERE A.REPORTERID = B.REPORTERTYPEID AND B.REPORTERTYPEID='FBHBYB'";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                return dt;
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取副预报员
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeputyRepoter()
        {
            string sql = "SELECT A.ID, A.REPORTERNAME, A.REPORTERCODE FROM HT_REPORT_REPORTERINFO A, HT_REPORT_REPORTER B WHERE A.REPORTERID = B.REPORTERTYPEID AND B.REPORTERTYPEID='FBHBYB'";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                return dt;
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }


        #region  操 作 中 文 表 单


        /// <summary>
        /// 获取中文预报单主键
        /// </summary>
        /// <returns></returns>
        public int GetCNID(Project_CN projectCN)
        {
            string sql = "SELECT REPORT.Nextval FROM DUAL";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if(dt != null && dt.Rows.Count > 0)
                {
                    projectCN.ID = Convert.ToInt32(dt.Rows[0]["NEXTVAL"]);
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
        /// 判断当前是否存在该发布单位预报单信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCNInfo(string publishTime, string publishCompanyName)
        {
           
            string sql = "SELECT A.*, "
                        + "(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.DEPUTYREPORTER = b.reportercode)AS DEPUTYREPORTERNAME,"
                        + "(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.HEADREPORTER = b.reportercode)AS HEADREPORTERNAME FROM HT_REPORT_CN  A "
                        + "WHERE publishtime='{0}' AND PUBLISHCOMPANYNAME='{1}'";
            sql = string.Format(sql, publishTime, publishCompanyName);
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
                return null;
            }
        }

        /// <summary>
        /// 根据北海中心旬预报
        /// 获取南堡油田预报单数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetCNNBInfo(string publishTime,string publishEffect,string publishCompanyName)
        {
            string sql = "SELECT A.*, "
                        + "(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.DEPUTYREPORTER = b.reportercode)AS DEPUTYREPORTERNAME,"
                        + "(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.HEADREPORTER = b.reportercode)AS HEADREPORTERNAME FROM HT_REPORT_EN  A "
                        + "WHERE PUBLISHTIME='{0}' AND PUBLISHEFFECT = '{1}' AND PUBLISHCOMPANY='{2}'";
            sql = string.Format(sql, publishTime, publishEffect, publishCompanyName);
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
                return null;
            }
        }

        /// <summary>
        /// 添加中文预报
        /// </summary>
        /// <param name="publishTime">发布时间</param>
        /// <param name="reportName">预报名称</param>
        /// <param name="reportContent">预报内容</param>
        /// <param name="headReporter">主预报员</param>
        /// <param name="deputyReporter">副预报员</param>
        /// <returns></returns>
        public int AddCNModel(string publishTime,string  reportName,string reportContent,string headReporter,string deputyReporter,DateTime createTime,string sendDepartment, Project_CN projectCN,string publishCompanyName)
        {
            string sql = "INSERT INTO  HT_REPORT_CN (ID,PUBLISHTIME,REPORTNAME,REPORTCONTENT,HEADREPORTER,DEPUTYREPORTER,CREATETIME,SENDDEPARTMENT,PUBLISHCOMPANYNAME)" +
                "VALUES (@CNID,@PUBLISHTIME,@REPORTNAME,@REPORTCONTENT,@HEADREPORTER,@DEPUTYREPORTER,to_char(sysdate,'yyyy-mm-dd'),@SENDDEPARTMENT,@PUBLISHCOMPANYNAME)";

            var CNID = DataExe.GetDbParameter();
            var PUBLISHTIME = DataExe.GetDbParameter();
            var REPORTNAME = DataExe.GetDbParameter();
            var REPORTCONTENT = DataExe.GetDbParameter();
            var HEADREPORTER = DataExe.GetDbParameter();
            var DEPUTYREPORTER = DataExe.GetDbParameter();
            var SENDDEPARTMENT = DataExe.GetDbParameter();
            var PUBLISHCOMPANYNAME = DataExe.GetDbParameter();

            CNID.ParameterName = "@CNID";
            PUBLISHTIME.ParameterName = "@PUBLISHTIME";
            REPORTNAME.ParameterName = "@REPORTNAME";
            REPORTCONTENT.ParameterName = "@REPORTCONTENT";
            HEADREPORTER.ParameterName = "@HEADREPORTER";
            DEPUTYREPORTER.ParameterName = "@DEPUTYREPORTER";
            SENDDEPARTMENT.ParameterName = "@SENDDEPARTMENT";
            PUBLISHCOMPANYNAME.ParameterName = "@PUBLISHCOMPANYNAME";
            
            CNID.Value = projectCN.ID;
            PUBLISHTIME.Value = publishTime;
            REPORTNAME.Value = reportName;
            REPORTCONTENT.Value = reportContent;
            HEADREPORTER.Value = headReporter;
            DEPUTYREPORTER.Value = deputyReporter;
            SENDDEPARTMENT.Value = sendDepartment;
            PUBLISHCOMPANYNAME.Value = publishCompanyName;


            DbParameter[] parameters = { CNID, PUBLISHTIME, REPORTNAME, REPORTCONTENT ,HEADREPORTER , DEPUTYREPORTER, SENDDEPARTMENT, PUBLISHCOMPANYNAME };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("插入中文预报单失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 更新中文数据
        /// </summary>
        /// <param name="publishTime"></param>
        /// <param name="reportName"></param>
        /// <param name="reportContent"></param>
        /// <param name="headReporter"></param>
        /// <param name="deputyReporter"></param>
        /// <param name="createTime"></param>
        /// <param name="sendDepartment"></param>
        /// <param name="projectCN"></param>
        /// <returns></returns>
        public int UpdateCNModel(string publishTime, string reportName, string reportContent, string headReporter, string deputyReporter,string sendDepartment, Project_CN projectCN,string publishCompanyName)
        {
            string sql = "UPDATE  HT_REPORT_CN SET "
                + " REPORTNAME = @REPORTNAME,REPORTCONTENT = @REPORTCONTENT,HEADREPORTER = @HEADREPORTER,DEPUTYREPORTER = @DEPUTYREPORTER,SENDDEPARTMENT = @SENDDEPARTMENT"
                +" WHERE PUBLISHTIME=@PUBLISHTIME AND PUBLISHCOMPANYNAME = @PUBLISHCOMPANYNAME";
            
            var PUBLISHTIME = DataExe.GetDbParameter();
            var REPORTNAME = DataExe.GetDbParameter();
            var REPORTCONTENT = DataExe.GetDbParameter();
            var HEADREPORTER = DataExe.GetDbParameter();
            var DEPUTYREPORTER = DataExe.GetDbParameter();
            var SENDDEPARTMENT = DataExe.GetDbParameter();
            var PUBLISHCOMPANYNAME = DataExe.GetDbParameter();

            PUBLISHTIME.ParameterName = "@PUBLISHTIME";
            REPORTNAME.ParameterName = "@REPORTNAME";
            REPORTCONTENT.ParameterName = "@REPORTCONTENT";
            HEADREPORTER.ParameterName = "@HEADREPORTER";
            DEPUTYREPORTER.ParameterName = "@DEPUTYREPORTER";
            SENDDEPARTMENT.ParameterName = "@SENDDEPARTMENT";
            PUBLISHCOMPANYNAME.ParameterName = "@PUBLISHCOMPANYNAME";

            PUBLISHTIME.Value = publishTime;
            REPORTNAME.Value = reportName;
            REPORTCONTENT.Value = reportContent;
            HEADREPORTER.Value = headReporter;
            DEPUTYREPORTER.Value = deputyReporter;
            SENDDEPARTMENT.Value = sendDepartment;
            PUBLISHCOMPANYNAME.Value = publishCompanyName;


            DbParameter[] parameters = { PUBLISHTIME, REPORTNAME, REPORTCONTENT, HEADREPORTER, DEPUTYREPORTER, SENDDEPARTMENT, PUBLISHCOMPANYNAME };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("插入中文预报单失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        #endregion


        #region 操 作 英 文 旬 、 月 预 报 单 标 签 属 性


        /**********************操作英文旬、月预报单标签属性***************************************/

        /// <summary>
        /// 山东预报台预报
        /// 获取海洋局北海预报中心数据
        /// </summary>
        /// <param name="publishTime">发布时间</param>
        /// <param name="publishEffect">预报时效</param>
        /// <param name="publishCompany">预报发布单位</param>
        /// <returns></returns>
        public DataTable ENGetForecastInfo(string publishTime, string publishEffect, string publishCompany)
        {
            string sql = "SELECT A.*, "
                        +"(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.DEPUTYREPORTER = b.reportercode)AS DEPUTYREPORTERNAME,"
                        +"(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.HEADREPORTER = b.reportercode)AS HEADREPORTERNAME FROM ht_report_en  A "
                        + "WHERE publishtime='{0}' AND publisheffect='{1}' AND publishcompany='{2}'";
            sql = string.Format(sql, publishTime, publishEffect, publishCompany);
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
                return null;
            }
        }

        /// <summary>
        /// 判断是否存在数据
        /// </summary>
        /// <returns></returns>
        public DataTable ENGetInfo(Project_ENDay projectDay)
        {
            string sql = "SELECT * FROM ht_report_en "
                        + " WHERE publishtime='{0}' AND publisheffect='{1}' AND publishcompany='{2}'";
            sql = string.Format(sql, projectDay.publishTime, projectDay.publishEffect, projectDay.publishCompany);
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
                return null;
            }
        }
        /// <summary>
        /// 获取英文预报单主键
        /// </summary>
        /// <returns></returns>
        public int GetENDayID(Project_ENDay projectDay)
        {
            string sql = "SELECT REPORT_EN.Nextval FROM DUAL";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    projectDay.ID = Convert.ToInt32(dt.Rows[0]["NEXTVAL"]);
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
        /// 将英文预报单属性值保存到数据库
        /// </summary>
        /// <param name="projectDay"></param>
        /// <returns></returns>
        public int AddENDayModel(Project_ENDay projectDay)
        {
            string sql = "INSERT INTO  HT_REPORT_EN (ID,REPORTNO,PUBLISHTIME,REPORTTITLE,REPORTTIME,REPORTNORTH,REPORTSOUTH,HEADREPORTER,DEPUTYREPORTER,CREATETIME,SENDDEPARTMENT,PUBLISHCOMPANY,PUBLISHEFFECT)" +
                "VALUES (@ENDayID,@REPORTNO,@PUBLISHTIME,@REPORTTITLE,@REPORTTIME,@REPORTNORTH,@REPORTSOUTH,@HEADREPORTER,@DEPUTYREPORTER,to_char(sysdate,'yyyy-mm-dd'),@SENDDEPARTMENT,@PUBLISHCOMPANY,@PUBLISHEFFECT)";

            var ENDayID = DataExe.GetDbParameter();
            var REPORTNO = DataExe.GetDbParameter();
            var PUBLISHTIME = DataExe.GetDbParameter();
            var REPORTTITLE = DataExe.GetDbParameter();
            var REPORTTIME = DataExe.GetDbParameter();
            var REPORTNORTH = DataExe.GetDbParameter();
            var REPORTSOUTH = DataExe.GetDbParameter();
            var HEADREPORTER = DataExe.GetDbParameter();
            var DEPUTYREPORTER = DataExe.GetDbParameter();
            var SENDDEPARTMENT = DataExe.GetDbParameter();
            var PUBLISHCOMPANY = DataExe.GetDbParameter();
            var PUBLISHEFFECT = DataExe.GetDbParameter();

            ENDayID.ParameterName = "@ENDayID";
            REPORTNO.ParameterName = "@REPORTNO";
            PUBLISHTIME.ParameterName = "@PUBLISHTIME";
            REPORTTITLE.ParameterName = "@REPORTTITLE";
            REPORTTIME.ParameterName = "@REPORTTIME";
            REPORTNORTH.ParameterName = "@REPORTNORTH";
            REPORTSOUTH.ParameterName = "@REPORTSOUTH";
            HEADREPORTER.ParameterName = "@HEADREPORTER";
            DEPUTYREPORTER.ParameterName = "@DEPUTYREPORTER";
            SENDDEPARTMENT.ParameterName = "@SENDDEPARTMENT";
            PUBLISHCOMPANY.ParameterName = "@PUBLISHCOMPANY";
            PUBLISHEFFECT.ParameterName = "@PUBLISHEFFECT";

            ENDayID.Value = projectDay.ID;
            REPORTNO.Value = projectDay.reportNo;
            PUBLISHTIME.Value = projectDay.publishTime;
            REPORTTITLE.Value = projectDay.reportTitle;
            REPORTTIME.Value = projectDay.reportTime;
            REPORTNORTH.Value = projectDay.reportNorth;
            REPORTSOUTH.Value = projectDay.reportSouth;
            HEADREPORTER.Value = projectDay.headReporter;
            DEPUTYREPORTER.Value = projectDay.deputyReporter;
            SENDDEPARTMENT.Value = projectDay.sendDepartment;
            PUBLISHCOMPANY.Value = projectDay.publishCompany;
            PUBLISHEFFECT.Value = projectDay.publishEffect;

            DbParameter[] parameters = { ENDayID, REPORTNO, PUBLISHTIME, REPORTTITLE, REPORTTIME, REPORTNORTH, REPORTSOUTH, HEADREPORTER, DEPUTYREPORTER, SENDDEPARTMENT, PUBLISHCOMPANY, PUBLISHEFFECT };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("插入英文预报单失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改英文预报单属性值
        /// </summary>
        /// <param name="projectDay"></param>
        /// <returns></returns>
        public int UpdateENDayModel(Project_ENDay projectDay)
        {
            string sql = "UPDATE  HT_REPORT_EN SET "
                        + " REPORTNO = @REPORTNO,REPORTTITLE = @REPORTTITLE,REPORTTIME = @REPORTTIME,REPORTNORTH = @REPORTNORTH,"
                        + " REPORTSOUTH = @REPORTSOUTH,HEADREPORTER = @HEADREPORTER,DEPUTYREPORTER = @DEPUTYREPORTER,"
                        + " SENDDEPARTMENT = @SENDDEPARTMENT,PUBLISHEFFECT = @PUBLISHEFFECT"
                        + " WHERE PUBLISHCOMPANY = @PUBLISHCOMPANY AND PUBLISHEFFECT = @PUBLISHEFFECT AND PUBLISHTIME = @PUBLISHTIME";

            //var ENDayID = DataExe.GetDbParameter();
            var REPORTNO = DataExe.GetDbParameter();
            var PUBLISHTIME = DataExe.GetDbParameter();
            var REPORTTITLE = DataExe.GetDbParameter();
            var REPORTTIME = DataExe.GetDbParameter();
            var REPORTNORTH = DataExe.GetDbParameter();
            var REPORTSOUTH = DataExe.GetDbParameter();
            var HEADREPORTER = DataExe.GetDbParameter();
            var DEPUTYREPORTER = DataExe.GetDbParameter();
            var SENDDEPARTMENT = DataExe.GetDbParameter();
            var PUBLISHCOMPANY = DataExe.GetDbParameter();
            var PUBLISHEFFECT = DataExe.GetDbParameter();

            //ENDayID.ParameterName = "@ENDayID";
            REPORTNO.ParameterName = "@REPORTNO";
            PUBLISHTIME.ParameterName = "@PUBLISHTIME";
            REPORTTITLE.ParameterName = "@REPORTTITLE";
            REPORTTIME.ParameterName = "@REPORTTIME";
            REPORTNORTH.ParameterName = "@REPORTNORTH";
            REPORTSOUTH.ParameterName = "@REPORTSOUTH";
            HEADREPORTER.ParameterName = "@HEADREPORTER";
            DEPUTYREPORTER.ParameterName = "@DEPUTYREPORTER";
            SENDDEPARTMENT.ParameterName = "@SENDDEPARTMENT";
            PUBLISHCOMPANY.ParameterName = "@PUBLISHCOMPANY";
            PUBLISHEFFECT.ParameterName = "@PUBLISHEFFECT";

            //ENDayID.Value = projectDay.ID;
            REPORTNO.Value = projectDay.reportNo;
            PUBLISHTIME.Value = projectDay.publishTime;
            REPORTTITLE.Value = projectDay.reportTitle;
            REPORTTIME.Value = projectDay.reportTime;
            REPORTNORTH.Value = projectDay.reportNorth;
            REPORTSOUTH.Value = projectDay.reportSouth;
            HEADREPORTER.Value = projectDay.headReporter;
            DEPUTYREPORTER.Value = projectDay.deputyReporter;
            SENDDEPARTMENT.Value = projectDay.sendDepartment;
            PUBLISHCOMPANY.Value = projectDay.publishCompany;
            PUBLISHEFFECT.Value = projectDay.publishEffect;

            DbParameter[] parameters = { REPORTNO, PUBLISHTIME, REPORTTITLE, REPORTTIME, REPORTNORTH, REPORTSOUTH, HEADREPORTER, DEPUTYREPORTER, SENDDEPARTMENT, PUBLISHCOMPANY, PUBLISHEFFECT };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("插入英文预报单失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region 操 作 英 文 年 预 报 单 标 签 属 性


        /**********************操作英文年预报单标签属性***************************************/

        /// <summary>
        /// 山东预报台预报
        /// 获取海洋局北海预报中心数据
        /// </summary>
        /// <param name="publishTime">发布时间</param>
        /// <param name="publishCompany">预报发布单位</param>
        /// <returns></returns>
        public DataTable ENYearGetForecastInfo(string publishTime, string publishCompany)
        {
            string sql = "SELECT A.*, "
                        + "(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.DEPUTYREPORTER = b.reportercode)AS DEPUTYREPORTERNAME,"
                        + "(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.HEADREPORTER = b.reportercode)AS HEADREPORTERNAME FROM ht_report_en_Year  A"
                        + "  WHERE publishtime='{0}' AND publishcompany='NMFC'";
            sql = string.Format(sql, publishTime);
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
                return null;
            }
        }
        /// <summary>
        /// 山东预报台预报
        /// 获取山东预报台数据
        /// </summary>
        /// <param name="publishTime"></param>
        /// <param name="publishCompany"></param>
        /// <returns></returns>
        public DataTable ENYearGetForecastInfoSDMF(string publishTime, string publishCompany)
        {
            string sql = "SELECT A.*, "
                        + "(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.DEPUTYREPORTER = b.reportercode)AS DEPUTYREPORTERNAME,"
                        + "(SELECT B.REPORTERNAME FROM ht_report_reporterinfo B WHERE a.HEADREPORTER = b.reportercode)AS HEADREPORTERNAME FROM ht_report_en_Year  A"
                        + "  WHERE publishtime='{0}' AND publishcompany='SDMF'";
            sql = string.Format(sql, publishTime);
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
                return null;
            }
        }
        /// <summary>
        /// 获取英文预报单主键
        /// </summary>
        /// <returns></returns>
        public int GetENYearID(Project_ENYear projectYear)
        {
            string sql = "SELECT REPORT_ENYEAR.Nextval FROM DUAL";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    projectYear.ID = Convert.ToInt32(dt.Rows[0]["NEXTVAL"]);
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
        /// 判读是否存在当前年预报单
        /// </summary>
        /// <param name="projectYear"></param>
        /// <returns></returns>
        public DataTable GetYearModel(Project_ENYear projectYear)
        {
            string sql = "select * from HT_REPORT_EN_YEAR  WHERE publishtime = '{0}' AND publishcompany = '{1}'";
            sql = string.Format(sql, projectYear.publishTime, projectYear.publishCompany);
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
        /// 将英文预报单属性值保存到数据库
        /// </summary>
        /// <param name="projectDay"></param>
        /// <returns></returns>
        public int AddENYearModel(Project_ENYear projectYear)
        {
            string sql = "INSERT INTO  HT_REPORT_EN_YEAR (ID,REPORTNO,PUBLISHTIME,REPORTTITLE,STORMSURGE, SEAWAVE, REDTIDE, GREENTIDE, TROPICALCYCLONE,HEADREPORTER,DEPUTYREPORTER,CREATETIME,SENDDEPARTMENT,PUBLISHCOMPANY)" +
                "VALUES (@ENYearID,@REPORTNO,@PUBLISHTIME,@REPORTTITLE,@STORMSURGE, @SEAWAVE, @REDTIDE, @GREENTIDE, @TROPICALCYCLONE,@HEADREPORTER,@DEPUTYREPORTER,to_char(sysdate,'yyyy-mm-dd'),@SENDDEPARTMENT,@PUBLISHCOMPANY)";

            var ENYearID = DataExe.GetDbParameter();
            var REPORTNO = DataExe.GetDbParameter();
            var PUBLISHTIME = DataExe.GetDbParameter();
            var REPORTTITLE = DataExe.GetDbParameter();
            var STORMSURGE = DataExe.GetDbParameter();
            var SEAWAVE = DataExe.GetDbParameter();
            var REDTIDE = DataExe.GetDbParameter();
            var GREENTIDE = DataExe.GetDbParameter();
            var TROPICALCYCLONE = DataExe.GetDbParameter();
            var HEADREPORTER = DataExe.GetDbParameter();
            var DEPUTYREPORTER = DataExe.GetDbParameter();
            var SENDDEPARTMENT = DataExe.GetDbParameter();
            var PUBLISHCOMPANY = DataExe.GetDbParameter();

            ENYearID.ParameterName = "@ENYearID";
            REPORTNO.ParameterName = "@REPORTNO";
            PUBLISHTIME.ParameterName = "@PUBLISHTIME";
            REPORTTITLE.ParameterName = "@REPORTTITLE";
            STORMSURGE.ParameterName = "@STORMSURGE";
            SEAWAVE.ParameterName = "@SEAWAVE";
            REDTIDE.ParameterName = "@REDTIDE";
            GREENTIDE.ParameterName = "@GREENTIDE";
            TROPICALCYCLONE.ParameterName = "@TROPICALCYCLONE";
            HEADREPORTER.ParameterName = "@HEADREPORTER";
            DEPUTYREPORTER.ParameterName = "@DEPUTYREPORTER";
            SENDDEPARTMENT.ParameterName = "@SENDDEPARTMENT";
            PUBLISHCOMPANY.ParameterName = "@PUBLISHCOMPANY";

            ENYearID.Value = projectYear.ID;
            REPORTNO.Value = projectYear.reportNo;
            PUBLISHTIME.Value = projectYear.publishTime;
            REPORTTITLE.Value = projectYear.reportTitle;
            STORMSURGE.Value = projectYear.stormSurge;
            SEAWAVE.Value = projectYear.seaWave;
            REDTIDE.Value = projectYear.redTide;
            GREENTIDE.Value = projectYear.greebTide;
            TROPICALCYCLONE.Value = projectYear.tropicalCyclone;
            HEADREPORTER.Value = projectYear.headReporter;
            DEPUTYREPORTER.Value = projectYear.deputyReporter;
            SENDDEPARTMENT.Value = projectYear.sendDepartment;
            PUBLISHCOMPANY.Value = projectYear.publishCompany;

            DbParameter[] parameters = { ENYearID, REPORTNO, PUBLISHTIME, REPORTTITLE, STORMSURGE, SEAWAVE, REDTIDE, GREENTIDE, TROPICALCYCLONE,HEADREPORTER, DEPUTYREPORTER, SENDDEPARTMENT, PUBLISHCOMPANY };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("插入英文年预报单失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 更新年预报单属性值
        /// </summary>
        /// <param name="projectYear"></param>
        /// <returns></returns>
        public int UpdateENYearModel(Project_ENYear projectYear) {
            string sql =
                "UPDATE HT_REPORT_EN_YEAR " +
                "SET REPORTNO=@REPORTNO,REPORTTITLE=@REPORTTITLE,STORMSURGE=@STORMSURGE, SEAWAVE=@SEAWAVE,REDTIDE=@REDTIDE, GREENTIDE=@GREENTIDE, "+
                "TROPICALCYCLONE= @TROPICALCYCLONE,HEADREPORTER=@HEADREPORTER,DEPUTYREPORTER=@DEPUTYREPORTER, " +
                "SENDDEPARTMENT=@SENDDEPARTMENT  "+
                "WHERE PUBLISHCOMPANY=@PUBLISHCOMPANY AND PUBLISHTIME=@PUBLISHTIME";

            //var ENYearID = DataExe.GetDbParameter();
            var REPORTNO = DataExe.GetDbParameter();
            var PUBLISHTIME = DataExe.GetDbParameter();
            var REPORTTITLE = DataExe.GetDbParameter();
            var STORMSURGE = DataExe.GetDbParameter();
            var SEAWAVE = DataExe.GetDbParameter();
            var REDTIDE = DataExe.GetDbParameter();
            var GREENTIDE = DataExe.GetDbParameter();
            var TROPICALCYCLONE = DataExe.GetDbParameter();
            var HEADREPORTER = DataExe.GetDbParameter();
            var DEPUTYREPORTER = DataExe.GetDbParameter();
            var SENDDEPARTMENT = DataExe.GetDbParameter();
            var PUBLISHCOMPANY = DataExe.GetDbParameter();

            //ENYearID.ParameterName = "@ENYearID";
            REPORTNO.ParameterName = "@REPORTNO";
            PUBLISHTIME.ParameterName = "@PUBLISHTIME";
            REPORTTITLE.ParameterName = "@REPORTTITLE";
            STORMSURGE.ParameterName = "@STORMSURGE";
            SEAWAVE.ParameterName = "@SEAWAVE";
            REDTIDE.ParameterName = "@REDTIDE";
            GREENTIDE.ParameterName = "@GREENTIDE";
            TROPICALCYCLONE.ParameterName = "@TROPICALCYCLONE";
            HEADREPORTER.ParameterName = "@HEADREPORTER";
            DEPUTYREPORTER.ParameterName = "@DEPUTYREPORTER";
            SENDDEPARTMENT.ParameterName = "@SENDDEPARTMENT";
            PUBLISHCOMPANY.ParameterName = "@PUBLISHCOMPANY";

            //ENYearID.Value = projectYear.ID;
            REPORTNO.Value = projectYear.reportNo;
            PUBLISHTIME.Value = projectYear.publishTime;
            REPORTTITLE.Value = projectYear.reportTitle;
            STORMSURGE.Value = projectYear.stormSurge;
            SEAWAVE.Value = projectYear.seaWave;
            REDTIDE.Value = projectYear.redTide;
            GREENTIDE.Value = projectYear.greebTide;
            TROPICALCYCLONE.Value = projectYear.tropicalCyclone;
            HEADREPORTER.Value = projectYear.headReporter;
            DEPUTYREPORTER.Value = projectYear.deputyReporter;
            SENDDEPARTMENT.Value = projectYear.sendDepartment;
            PUBLISHCOMPANY.Value = projectYear.publishCompany;

            DbParameter[] parameters = { REPORTNO, PUBLISHTIME, REPORTTITLE, STORMSURGE, SEAWAVE, REDTIDE, GREENTIDE, TROPICALCYCLONE, HEADREPORTER, DEPUTYREPORTER, SENDDEPARTMENT, PUBLISHCOMPANY };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改英文年预报单失败！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        #endregion
    }
}