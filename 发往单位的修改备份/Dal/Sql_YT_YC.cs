using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 海阳万米海滩海水浴场风、浪预报
    /// </summary>
    public class Sql_YT_YC
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_YT_YC()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 获取海阳万米海滩海水浴场风、浪预报
        /// </summary>
        /// <param name="ytYc"></param>
        /// <returns></returns>
        public object GetYcDataBy_S(YT_YC ytYc)
        {
            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + ytYc.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND FORECASTAREA = '乳山近海'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海阳万米海滩海水浴场风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取海阳万米海滩海水浴场风、浪预报
        /// </summary>
        /// <param name="ytYc"></param>
        /// <returns></returns>
        public object GetYcWaveDataBy_S(YT_YC ytYc)
        {
            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + ytYc.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND FORECASTAREA = '乳山近海'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海阳万米海滩海水浴场风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 从数据库中检索海阳万米海滩海水浴场风、浪预报
        /// </summary>
        /// <param name="ytYc"></param>
        /// <returns></returns>
        public object GetYcDataBy_T(YT_YC ytYc)
        {
            try
            {
                string sql = "SELECT * FROM TBLYTYC WHERE PUBLISHDATE = to_date('" + ytYc.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海阳万米海滩海水浴场风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 插入海阳万米海滩海水浴场风、浪预报
        /// </summary>
        /// <param name="ytYc"></param>
        /// <returns></returns>
        public int AddYcData(YT_YC ytYc)
        {
            try
            {
                string sql = "INSERT INTO TBLYTYC"
                           + " (PUBLISHDATE,FORECASTDATE,WEATERSTATE,TEMPERATURE,WINDSPEED,WINDDIRECTION,WAVEHEIGHT)"
                           + " VALUES"
                           + " (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@WEATERSTATE,@TEMPERATURE,@WINDSPEED,@WINDDIRECTION,@WAVEHEIGHT)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var WEATERSTATE = DataExe.GetDbParameter();
                var TEMPERATURE = DataExe.GetDbParameter();
                var WINDSPEED = DataExe.GetDbParameter();
                var WINDDIRECTION = DataExe.GetDbParameter();
                var WAVEHEIGHT = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                WEATERSTATE.ParameterName = "@WEATERSTATE";
                TEMPERATURE.ParameterName = "@TEMPERATURE";
                WINDSPEED.ParameterName = "@WINDSPEED";
                WINDDIRECTION.ParameterName = "@WINDDIRECTION";
                WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";

                PUBLISHDATE.Value = ytYc.PUBLISHDATE.ToString();
                FORECASTDATE.Value = ytYc.FORECASTDATE.ToString();
                WEATERSTATE.Value = ytYc.WEATERSTATE;
                TEMPERATURE.Value = ytYc.TEMPERATURE;
                WINDSPEED.Value = ytYc.WINDSPEED;
                WINDDIRECTION.Value = ytYc.WINDDIRECTION;
                WAVEHEIGHT.Value = ytYc.WAVEHEIGHT;

                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, WEATERSTATE, TEMPERATURE, WINDSPEED, WINDDIRECTION, WAVEHEIGHT };
                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("插入海阳万米海滩海水浴场风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改海阳万米海滩海水浴场风、浪预报
        /// </summary>
        /// <param name="ytYc"></param>
        /// <returns></returns>
        public int EditYcData(YT_YC ytYc)
        {
            try
            {
                string sql = "UPDATE TBLYTYC"
                           + " SET  WEATERSTATE=@WEATERSTATE, TEMPERATURE=@TEMPERATURE, WINDSPEED=@WINDSPEED, WINDDIRECTION=@WINDDIRECTION, WAVEHEIGHT=@WAVEHEIGHT WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var WEATERSTATE = DataExe.GetDbParameter();
                var TEMPERATURE = DataExe.GetDbParameter();
                var WINDSPEED = DataExe.GetDbParameter();
                var WINDDIRECTION = DataExe.GetDbParameter();
                var WAVEHEIGHT = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                WEATERSTATE.ParameterName = "@WEATERSTATE";
                TEMPERATURE.ParameterName = "@TEMPERATURE";
                WINDSPEED.ParameterName = "@WINDSPEED";
                WINDDIRECTION.ParameterName = "@WINDDIRECTION";
                WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";

                PUBLISHDATE.Value = ytYc.PUBLISHDATE.ToString();
                WEATERSTATE.Value = ytYc.WEATERSTATE;
                TEMPERATURE.Value = ytYc.TEMPERATURE;
                WINDSPEED.Value = ytYc.WINDSPEED;
                WINDDIRECTION.Value = ytYc.WINDDIRECTION;
                WAVEHEIGHT.Value = ytYc.WAVEHEIGHT;

                DbParameter[] parameters = { PUBLISHDATE, WEATERSTATE, TEMPERATURE, WINDSPEED, WINDDIRECTION, WAVEHEIGHT };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改海阳万米海滩海水浴场风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}