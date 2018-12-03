using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 海阳近岸海域潮汐预报
    /// </summary>
    public class Sql_YT_TideForecast
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_YT_TideForecast()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        public object GetTideDataBy_S(YT_TideForecast tideForecast)
        {
            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + tideForecast.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海阳近岸海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        public object GetTideDataBy_T(YT_TideForecast tideForecast)
        {
            try
            {
                string sql = "SELECT * FROM TBLYTTIDE WHERE PUBLISHDATE = to_date('" + tideForecast.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海阳近岸海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        public int AddTideData(YT_TideForecast tideForecast)
        {
            try
            {
                string sql = "INSERT INTO TBLYTTIDE"
                           + " (PUBLISHDATE,FORECASTDATE,FIRSTHIGHTIME,FIRSTHIGHLEVEL,FIRSTLOWTIME,FIRSTLOWLEVEL,SECONDHIGHTIME,SECONDHIGHLEVEL,SECONDLOWTIME,SECONDLOWLEVEL)"
                           + " VALUES"
                           + " (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@FIRSTHIGHTIME,@FIRSTHIGHLEVEL,@FIRSTLOWTIME,@FIRSTLOWLEVEL,@SECONDHIGHTIME,@SECONDHIGHLEVEL,@SECONDLOWTIME,@SECONDLOWLEVEL)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var FIRSTHIGHTIME = DataExe.GetDbParameter();
                var FIRSTHIGHLEVEL = DataExe.GetDbParameter();
                var FIRSTLOWTIME = DataExe.GetDbParameter();
                var FIRSTLOWLEVEL = DataExe.GetDbParameter();
                var SECONDHIGHTIME = DataExe.GetDbParameter();
                var SECONDHIGHLEVEL = DataExe.GetDbParameter();
                var SECONDLOWTIME = DataExe.GetDbParameter();
                var SECONDLOWLEVEL = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                FIRSTHIGHTIME.ParameterName = "@FIRSTHIGHTIME";
                FIRSTHIGHLEVEL.ParameterName = "@FIRSTHIGHLEVEL";
                FIRSTLOWTIME.ParameterName = "@FIRSTLOWTIME";
                FIRSTLOWLEVEL.ParameterName = "@FIRSTLOWLEVEL";
                SECONDHIGHTIME.ParameterName = "@SECONDHIGHTIME";
                SECONDHIGHLEVEL.ParameterName = "@SECONDHIGHLEVEL";
                SECONDLOWTIME.ParameterName = "@SECONDLOWTIME";
                SECONDLOWLEVEL.ParameterName = "@SECONDLOWLEVEL";

                PUBLISHDATE.Value = tideForecast.PUBLISHDATE.ToString();
                FORECASTDATE.Value = tideForecast.FORECASTDATE.ToString();
                FIRSTHIGHTIME.Value = tideForecast.FIRSTHIGHTIME;
                FIRSTHIGHLEVEL.Value = tideForecast.FIRSTHIGHLEVEL;
                FIRSTLOWTIME.Value = tideForecast.FIRSTLOWTIME;
                FIRSTLOWLEVEL.Value = tideForecast.FIRSTLOWLEVEL;
                SECONDHIGHTIME.Value = tideForecast.SECONDHIGHTIME;
                SECONDHIGHLEVEL.Value = tideForecast.SECONDHIGHLEVEL;
                SECONDLOWTIME.Value = tideForecast.SECONDLOWTIME;
                SECONDLOWLEVEL.Value = tideForecast.SECONDLOWLEVEL;

                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, FIRSTHIGHTIME, FIRSTHIGHLEVEL, FIRSTLOWTIME, FIRSTLOWLEVEL, SECONDHIGHTIME, SECONDHIGHLEVEL, SECONDLOWTIME, SECONDLOWLEVEL };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("插入海阳近岸海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        public int EditTideData(YT_TideForecast tideForecast)
        {
            try
            {
                string sql = "UPDATE TBLYTTIDE"
                           + " SET FIRSTHIGHTIME=@FIRSTHIGHTIME, FIRSTHIGHLEVEL=@FIRSTHIGHLEVEL, FIRSTLOWTIME=@FIRSTLOWTIME, FIRSTLOWLEVEL=@FIRSTLOWLEVEL, SECONDHIGHTIME=@SECONDHIGHTIME, SECONDHIGHLEVEL=@SECONDHIGHLEVEL, SECONDLOWTIME=@SECONDLOWTIME, SECONDLOWLEVEL=@SECONDLOWLEVEL WHERE  PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FIRSTHIGHTIME = DataExe.GetDbParameter();
                var FIRSTHIGHLEVEL = DataExe.GetDbParameter();
                var FIRSTLOWTIME = DataExe.GetDbParameter();
                var FIRSTLOWLEVEL = DataExe.GetDbParameter();
                var SECONDHIGHTIME = DataExe.GetDbParameter();
                var SECONDHIGHLEVEL = DataExe.GetDbParameter();
                var SECONDLOWTIME = DataExe.GetDbParameter();
                var SECONDLOWLEVEL = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FIRSTHIGHTIME.ParameterName = "@FIRSTHIGHTIME";
                FIRSTHIGHLEVEL.ParameterName = "@FIRSTHIGHLEVEL";
                FIRSTLOWTIME.ParameterName = "@FIRSTLOWTIME";
                FIRSTLOWLEVEL.ParameterName = "@FIRSTLOWLEVEL";
                SECONDHIGHTIME.ParameterName = "@SECONDHIGHTIME";
                SECONDHIGHLEVEL.ParameterName = "@SECONDHIGHLEVEL";
                SECONDLOWTIME.ParameterName = "@SECONDLOWTIME";
                SECONDLOWLEVEL.ParameterName = "@SECONDLOWLEVEL";

                PUBLISHDATE.Value = tideForecast.PUBLISHDATE.ToString();
                FIRSTHIGHTIME.Value = tideForecast.FIRSTHIGHTIME;
                FIRSTHIGHLEVEL.Value = tideForecast.FIRSTHIGHLEVEL;
                FIRSTLOWTIME.Value = tideForecast.FIRSTLOWTIME;
                FIRSTLOWLEVEL.Value = tideForecast.FIRSTLOWLEVEL;
                SECONDHIGHTIME.Value = tideForecast.SECONDHIGHTIME;
                SECONDHIGHLEVEL.Value = tideForecast.SECONDHIGHLEVEL;
                SECONDLOWTIME.Value = tideForecast.SECONDLOWTIME;
                SECONDLOWLEVEL.Value = tideForecast.SECONDLOWLEVEL;

                DbParameter[] parameters = { PUBLISHDATE, FIRSTHIGHTIME, FIRSTHIGHLEVEL, FIRSTLOWTIME, FIRSTLOWLEVEL, SECONDHIGHTIME, SECONDHIGHLEVEL, SECONDLOWTIME, SECONDLOWLEVEL };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改海阳近岸海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}