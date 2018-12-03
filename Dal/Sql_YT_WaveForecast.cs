using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 烟台南部海浪、水温预报
    /// </summary>
    public class Sql_YT_WaveForecast
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_YT_WaveForecast()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 从解析文件数据库中获取数据
        /// </summary>
        /// <returns></returns>
        public object GetWaveDataBy_S(YT_WaveForecast waveForecast)
        {
            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + waveForecast.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND FORECASTAREA = '乳山近海'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取烟台南部海浪、水温预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取海阳万米海滩海水浴场风、浪预报
        /// </summary>
        /// <param name="ytYc"></param>
        /// <returns></returns>
        public object GetWindDataBy_S(YT_WaveForecast waveForecast)
        {
            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + waveForecast.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND FORECASTAREA = '乳山近海'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海阳万米海滩海水浴场风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 从数据库表中获取数据
        /// </summary>
        /// <returns></returns>
        public object GetWaveDataBy_T(YT_WaveForecast waveForecast)
        {
            try
            {
                string sql = "SELECT * FROM TBLYTWAVE WHERE PUBLISHDATE = to_date('" + waveForecast.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取烟台南部海浪、水温预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 插入烟台南部海浪、水温预报
        /// </summary>
        /// <param name="waveForecast"></param>
        /// <returns></returns>
        public int AddWaveData(YT_WaveForecast waveForecast, string quanxian)
        {
            try
            {
                string sql = "";
                DbParameter[] parameters = null;
                sql = "INSERT INTO TBLYTWAVE"
                           + " (PUBLISHDATE,FORECASTDATE,WAVELEVELONE,WAVELEVELTYPE,WAVEDIRECTION,WATERTEMPERATURE)"
                           + " VALUES"
                           + " (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@WAVELEVELONE,@WAVELEVELTYPE,@WAVEDIRECTION,@WATERTEMPERATURE)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var WAVELEVELONE = DataExe.GetDbParameter();
                var WAVELEVELTYPE = DataExe.GetDbParameter();
                var WAVEDIRECTION = DataExe.GetDbParameter();
                var WATERTEMPERATURE = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                WAVELEVELONE.ParameterName = "@WAVELEVELONE";
                WAVELEVELTYPE.ParameterName = "@WAVELEVELTYPE";
                WAVEDIRECTION.ParameterName = "@WAVEDIRECTION";
                WATERTEMPERATURE.ParameterName = "@WATERTEMPERATURE";

                PUBLISHDATE.Value = waveForecast.PUBLISHDATE.ToString();
                FORECASTDATE.Value = waveForecast.FORECASTDATE.ToString();
                WAVELEVELONE.Value = waveForecast.WAVELEVELONE;
                WAVELEVELTYPE.Value = waveForecast.WAVELEVELTYPE;
                WAVEDIRECTION.Value = waveForecast.WAVEDIRECTION;
                WATERTEMPERATURE.Value = waveForecast.WATERTEMPERATURE;

                parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, WAVELEVELONE, WAVELEVELTYPE, WAVEDIRECTION, WATERTEMPERATURE };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("插入烟台南部海浪、水温预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改烟台南部海浪、水温预报
        /// </summary>
        /// <param name="waveForecast"></param>
        /// <returns></returns>
        public int EditWaveData(YT_WaveForecast waveForecast, string quanxian)
        {
            try
            {
                string sql = "";
                DbParameter[] parameters = null;
                if (quanxian == "fl")
                {
                   sql = "UPDATE TBLYTWAVE"
                           + " SET WAVELEVELONE=@WAVELEVELONE,WAVELEVELTYPE=@WAVELEVELTYPE,WAVEDIRECTION=@WAVEDIRECTION WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";

                    var PUBLISHDATE = DataExe.GetDbParameter();
                    var WAVELEVELONE = DataExe.GetDbParameter();
                    var WAVELEVELTYPE = DataExe.GetDbParameter();
                    var WAVEDIRECTION = DataExe.GetDbParameter();

                    PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                    WAVELEVELONE.ParameterName = "@WAVELEVELONE";
                    WAVELEVELTYPE.ParameterName = "@WAVELEVELTYPE";
                    WAVEDIRECTION.ParameterName = "@WAVEDIRECTION";

                    PUBLISHDATE.Value = waveForecast.PUBLISHDATE.ToString();
                    WAVELEVELONE.Value = waveForecast.WAVELEVELONE;
                    WAVELEVELTYPE.Value = waveForecast.WAVELEVELTYPE;
                    WAVEDIRECTION.Value = waveForecast.WAVEDIRECTION;

                    parameters = new DbParameter[]{ PUBLISHDATE, WAVELEVELONE, WAVELEVELTYPE, WAVEDIRECTION };
                }
                else if (quanxian == "sw")
                {
                   sql = "UPDATE TBLYTWAVE"
                           + " SET WATERTEMPERATURE=@WATERTEMPERATURE WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";

                    var PUBLISHDATE = DataExe.GetDbParameter();
                    var WATERTEMPERATURE = DataExe.GetDbParameter();

                    PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                    WATERTEMPERATURE.ParameterName = "@WATERTEMPERATURE";

                    PUBLISHDATE.Value = waveForecast.PUBLISHDATE.ToString();
                    WATERTEMPERATURE.Value = waveForecast.WATERTEMPERATURE;

                    parameters = new DbParameter[] { PUBLISHDATE, WATERTEMPERATURE };
                }
                
                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改烟台南部海浪、水温预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}