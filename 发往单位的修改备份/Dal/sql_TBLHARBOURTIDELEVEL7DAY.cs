using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 7天港口潮位预报
    /// </summary>
    public class sql_TBLHARBOURTIDELEVEL7DAY
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLHARBOURTIDELEVEL7DAY()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        /// 获取所有潮汐数据
        /// </summary>
        /// <param name="TBLHARBOURTIDELEVEL"></param>
        /// <returns></returns>
        public object GETTBLHARBOURTIDELEVEL7DAY(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {

            try
            {
                //string temp = "select * from TBLHARBOURTIDELEVEL7DAY where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData("select * from TBLHARBOURTIDELEVEL7DAY where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取港口潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取所有潮汐数据add by xp 2018-9-17
        /// </summary>
        /// <param name="TBLHARBOURTIDELEVEL"></param>
        /// <returns></returns>
        public object GETTBLHARBOURTIDELEVEL7DAY_Week(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {

            try
            {
                string Temp = "select * from TBLHARBOURTIDELEVEL7DAY where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') "+
                    "and forecastdate >to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')" +
                "and forecastdate< to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.AddDays(5).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by htlharbour desc, forecastdate asc";
                //return DataExe.GetTableExeData("select * from TBLHARBOURTIDELEVEL7DAY where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");
                return DataExe.GetTableExeData(Temp);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取港口潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取所有港口潮位预报add by xp 2018-9-7 取周日上午二的数据
        /// </summary>
        /// <returns></returns>
        public object get_TBLHARBOURTIDELEVEL_AllData(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {

            try
            {
                string temp = "select * from TBLHARBOURTIDELEVEL where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') " +
                    "and forecastdate >to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by htlharbour DESC,forecastdate asc";
                //return DataExe.GetTableExeData("select * from TBLHARBOURTIDELEVEL where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");
                return DataExe.GetTableExeData(temp);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取港口潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
      
        /// <summary>
        /// 获取所有潮汐数据，并排序生成表单
        /// </summary>
        /// <param name="TBLHARBOURTIDELEVEL"></param>
        /// <returns></returns>
        public object GETTBLHARBOURTIDELEVEL7DAYWORD(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {

            try
            {
                string sql = "select * from TBLHARBOURTIDELEVEL7DAY where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by htlharbour desc, forecastdate ";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取港口潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 新增7天港口潮位预报
        /// </summary>
        /// <returns></returns>
        public int AddTBLHARBOURTIDELEVEL7DAY(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {

            string sql = "INSERT INTO  TBLHARBOURTIDELEVEL7DAY (PUBLISHDATE,HTLSECONDTIMELOWTIDE,HTLLOWTIDELEVELFORTHESECONDTIM,HTLHARBOUR,FORECASTDATE,HTLFIRSTWAVEOFTIME,HTLFIRSTWAVETIDELEVEL,HTLFIRSTTIMELOWTIDE,HTLLOWTIDELEVELFORTHEFIRSTTIME,HTLSECONDWAVEOFTIME,HTLSECONDWAVETIDELEVEL) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@HTLSECONDTIMELOWTIDE,@HTLLOWTIDELEVELFORTHESECONDTIM,@HTLHARBOUR,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@HTLFIRSTWAVEOFTIME,@HTLFIRSTWAVETIDELEVEL,@HTLFIRSTTIMELOWTIDE,@HTLLOWTIDELEVELFORTHEFIRSTTIME,@HTLSECONDWAVEOFTIME,@HTLSECONDWAVETIDELEVEL)";



            var PUBLISHDATE = DataExe.GetDbParameter();
            var HTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();
            var HTLLOWTIDELEVELFORTHESECONDTIM = DataExe.GetDbParameter();
            var HTLHARBOUR = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var HTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
            var HTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
            var HTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
            var HTLLOWTIDELEVELFORTHEFIRSTTIME = DataExe.GetDbParameter();
            var HTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
            var HTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            HTLSECONDTIMELOWTIDE.ParameterName = "@HTLSECONDTIMELOWTIDE";
            HTLLOWTIDELEVELFORTHESECONDTIM.ParameterName = "@HTLLOWTIDELEVELFORTHESECONDTIM";
            HTLHARBOUR.ParameterName = "@HTLHARBOUR";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            HTLFIRSTWAVEOFTIME.ParameterName = "@HTLFIRSTWAVEOFTIME";
            HTLFIRSTWAVETIDELEVEL.ParameterName = "@HTLFIRSTWAVETIDELEVEL";
            HTLFIRSTTIMELOWTIDE.ParameterName = "@HTLFIRSTTIMELOWTIDE";
            HTLLOWTIDELEVELFORTHEFIRSTTIME.ParameterName = "@HTLLOWTIDELEVELFORTHEFIRSTTIME";
            HTLSECONDWAVEOFTIME.ParameterName = "@HTLSECONDWAVEOFTIME";
            HTLSECONDWAVETIDELEVEL.ParameterName = "@HTLSECONDWAVETIDELEVEL";




            PUBLISHDATE.Value = TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString();
            HTLSECONDTIMELOWTIDE.Value = TBLHARBOURTIDELEVEL.HTLSECONDTIMELOWTIDE;
            HTLLOWTIDELEVELFORTHESECONDTIM.Value = TBLHARBOURTIDELEVEL.HTLLOWTIDELEVELFORTHESECONDTIM;
            HTLHARBOUR.Value = TBLHARBOURTIDELEVEL.HTLHARBOUR;
            FORECASTDATE.Value = TBLHARBOURTIDELEVEL.FORECASTDATE.ToString();
            HTLFIRSTWAVEOFTIME.Value = TBLHARBOURTIDELEVEL.HTLFIRSTWAVEOFTIME;
            HTLFIRSTWAVETIDELEVEL.Value = TBLHARBOURTIDELEVEL.HTLFIRSTWAVETIDELEVEL;
            HTLFIRSTTIMELOWTIDE.Value = TBLHARBOURTIDELEVEL.HTLFIRSTTIMELOWTIDE;
            HTLLOWTIDELEVELFORTHEFIRSTTIME.Value = TBLHARBOURTIDELEVEL.HTLLOWTIDELEVELFORTHEFIRSTTIME;
            HTLSECONDWAVEOFTIME.Value = TBLHARBOURTIDELEVEL.HTLSECONDWAVEOFTIME;
            HTLSECONDWAVETIDELEVEL.Value = TBLHARBOURTIDELEVEL.HTLSECONDWAVETIDELEVEL;


            DbParameter[] parameters = { PUBLISHDATE, HTLSECONDTIMELOWTIDE, HTLLOWTIDELEVELFORTHESECONDTIM, HTLHARBOUR, FORECASTDATE, HTLFIRSTWAVEOFTIME, HTLFIRSTWAVETIDELEVEL, HTLFIRSTTIMELOWTIDE, HTLLOWTIDELEVELFORTHEFIRSTTIME, HTLSECONDWAVEOFTIME, HTLSECONDWAVETIDELEVEL };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增港口潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改7天港口潮位预报
        /// </summary>
        public int UPDATETBLHARBOURTIDELEVEL7DAY(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {
            string sql = "UPDATE   TBLHARBOURTIDELEVEL7DAY set	HTLSECONDTIMELOWTIDE=@HTLSECONDTIMELOWTIDE,HTLLOWTIDELEVELFORTHESECONDTIM=@HTLLOWTIDELEVELFORTHESECONDTIM,HTLFIRSTWAVEOFTIME=@HTLFIRSTWAVEOFTIME,HTLFIRSTWAVETIDELEVEL=@HTLFIRSTWAVETIDELEVEL,HTLFIRSTTIMELOWTIDE=@HTLFIRSTTIMELOWTIDE,HTLLOWTIDELEVELFORTHEFIRSTTIME=@HTLLOWTIDELEVELFORTHEFIRSTTIME,HTLSECONDWAVEOFTIME=@HTLSECONDWAVEOFTIME,HTLSECONDWAVETIDELEVEL=@HTLSECONDWAVETIDELEVEL where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and HTLHARBOUR=@HTLHARBOUR and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var HTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();
            var HTLLOWTIDELEVELFORTHESECONDTIM = DataExe.GetDbParameter();
            var HTLHARBOUR = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var HTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
            var HTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
            var HTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
            var HTLLOWTIDELEVELFORTHEFIRSTTIME = DataExe.GetDbParameter();
            var HTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
            var HTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            HTLSECONDTIMELOWTIDE.ParameterName = "@HTLSECONDTIMELOWTIDE";
            HTLLOWTIDELEVELFORTHESECONDTIM.ParameterName = "@HTLLOWTIDELEVELFORTHESECONDTIM";
            HTLHARBOUR.ParameterName = "@HTLHARBOUR";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            HTLFIRSTWAVEOFTIME.ParameterName = "@HTLFIRSTWAVEOFTIME";
            HTLFIRSTWAVETIDELEVEL.ParameterName = "@HTLFIRSTWAVETIDELEVEL";
            HTLFIRSTTIMELOWTIDE.ParameterName = "@HTLFIRSTTIMELOWTIDE";
            HTLLOWTIDELEVELFORTHEFIRSTTIME.ParameterName = "@HTLLOWTIDELEVELFORTHEFIRSTTIME";
            HTLSECONDWAVEOFTIME.ParameterName = "@HTLSECONDWAVEOFTIME";
            HTLSECONDWAVETIDELEVEL.ParameterName = "@HTLSECONDWAVETIDELEVEL";




            PUBLISHDATE.Value = TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString();
            HTLSECONDTIMELOWTIDE.Value = TBLHARBOURTIDELEVEL.HTLSECONDTIMELOWTIDE;
            HTLLOWTIDELEVELFORTHESECONDTIM.Value = TBLHARBOURTIDELEVEL.HTLLOWTIDELEVELFORTHESECONDTIM;
            HTLHARBOUR.Value = TBLHARBOURTIDELEVEL.HTLHARBOUR;
            FORECASTDATE.Value = TBLHARBOURTIDELEVEL.FORECASTDATE.ToString();
            HTLFIRSTWAVEOFTIME.Value = TBLHARBOURTIDELEVEL.HTLFIRSTWAVEOFTIME;
            HTLFIRSTWAVETIDELEVEL.Value = TBLHARBOURTIDELEVEL.HTLFIRSTWAVETIDELEVEL;
            HTLFIRSTTIMELOWTIDE.Value = TBLHARBOURTIDELEVEL.HTLFIRSTTIMELOWTIDE;
            HTLLOWTIDELEVELFORTHEFIRSTTIME.Value = TBLHARBOURTIDELEVEL.HTLLOWTIDELEVELFORTHEFIRSTTIME;
            HTLSECONDWAVEOFTIME.Value = TBLHARBOURTIDELEVEL.HTLSECONDWAVEOFTIME;
            HTLSECONDWAVETIDELEVEL.Value = TBLHARBOURTIDELEVEL.HTLSECONDWAVETIDELEVEL;


            DbParameter[] parameters = { PUBLISHDATE, HTLSECONDTIMELOWTIDE, HTLLOWTIDELEVELFORTHESECONDTIM, HTLHARBOUR, FORECASTDATE, HTLFIRSTWAVEOFTIME, HTLFIRSTWAVETIDELEVEL, HTLFIRSTTIMELOWTIDE, HTLLOWTIDELEVELFORTHEFIRSTTIME, HTLSECONDWAVEOFTIME, HTLSECONDWAVETIDELEVEL };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改港口潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}