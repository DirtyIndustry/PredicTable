using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 海洋牧场-海浪
    /// </summary>
    public class sql_OceanRanchWave
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_OceanRanchWave()
        {
            DataExe = new DataExecution();
        }

        /// <summary>
        /// 获取海洋牧场海浪预报
        /// </summary>
        /// <param name="oceanRabchWave"></param>
        /// <returns></returns>
        public object GetOceanRanchWaveList(OceanRanchWave oceanRabchWave)
        {
            try
            {
                string sql = "SELECT * FROM OCEANRANCH72HWAVE_T WHERE PUBLISHDATE = to_date('" + oceanRabchWave.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海洋牧场海浪预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取海洋牧场海浪预报解析数据
        /// </summary>
        /// <param name="oceanRabchWave"></param>
        /// <returns></returns>
        public object GetOceanRanchWaveListBy_S(OceanRanchWave oceanRabchWave)
        {
            try
            {
                string sql = "SELECT * FROM OCEANRANCH72HWAVE_S WHERE FORECASTDATE = to_date('" + oceanRabchWave.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海洋牧场海浪预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 添加海洋牧场海浪预报
        /// </summary>
        /// <param name="oceanRabchWave"></param>
        /// <returns></returns>
        public int InsertOceanRanchWaveList(OceanRanchWave oceanRabchWave)
        {
            try
            {
                string sql = "INSERT INTO OCEANRANCH72HWAVE_T ( "
                           + " PUBLISHDATE,FORECASTDATE,OCEANRANCHNAME,OCEANRANCHSHORTNAME,SN,WAVE24HDAY,WAVE24HNEIGHT,WAVE48HDAY,WAVE48HNEIGHT,WAVE72HDAY,WAVE72HNEIGHT )"
                           + " VALUES "
                           + " ( to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@OCEANRANCHNAME,@OCEANRANCHSHORTNAME,@SN,@WAVE24HDAY,@WAVE24HNEIGHT,@WAVE48HDAY,@WAVE48HNEIGHT,@WAVE72HDAY,@WAVE72HNEIGHT)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var OCEANRANCHNAME = DataExe.GetDbParameter();
                var OCEANRANCHSHORTNAME = DataExe.GetDbParameter();
                var SN = DataExe.GetDbParameter();
                var WAVE24HDAY = DataExe.GetDbParameter();
                var WAVE24HNEIGHT = DataExe.GetDbParameter();
                var WAVE48HDAY = DataExe.GetDbParameter();
                var WAVE48HNEIGHT = DataExe.GetDbParameter();
                var WAVE72HDAY = DataExe.GetDbParameter();
                var WAVE72HNEIGHT = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                OCEANRANCHNAME.ParameterName = "@OCEANRANCHNAME";
                OCEANRANCHSHORTNAME.ParameterName = "@OCEANRANCHSHORTNAME";
                SN.ParameterName = "@SN";
                WAVE24HDAY.ParameterName = "@WAVE24HDAY";
                WAVE24HNEIGHT.ParameterName = "@WAVE24HNEIGHT";
                WAVE48HDAY.ParameterName = "@WAVE48HDAY";
                WAVE48HNEIGHT.ParameterName = "@WAVE48HNEIGHT";
                WAVE72HDAY.ParameterName = "@WAVE72HDAY";
                WAVE72HNEIGHT.ParameterName = "@WAVE72HNEIGHT";

                PUBLISHDATE.Value = oceanRabchWave.PUBLISHDATE.ToString();
                FORECASTDATE.Value = oceanRabchWave.FORECASTDATE.ToString();
                OCEANRANCHNAME.Value = oceanRabchWave.OCEANRANCHNAME;
                OCEANRANCHSHORTNAME.Value = oceanRabchWave.OCEANRANCHSHORTNAME;
                SN.Value = oceanRabchWave.SN;
                WAVE24HDAY.Value = oceanRabchWave.WAVE24HDAY;
                WAVE24HNEIGHT.Value = oceanRabchWave.WAVE24HNEIGHT;
                WAVE48HDAY.Value = oceanRabchWave.WAVE48HDAY;
                WAVE48HNEIGHT.Value = oceanRabchWave.WAVE48HNEIGHT;
                WAVE72HDAY.Value = oceanRabchWave.WAVE72HDAY;
                WAVE72HNEIGHT.Value = oceanRabchWave.WAVE72HNEIGHT;


                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, OCEANRANCHNAME, OCEANRANCHSHORTNAME, SN, WAVE24HDAY, WAVE24HNEIGHT, WAVE48HDAY, WAVE48HNEIGHT, WAVE72HDAY, WAVE72HNEIGHT };
                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("添加海洋牧场海浪预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改海洋牧场海浪预报
        /// </summary>
        /// <param name="oceanRabchWave"></param>
        /// <returns></returns>
        public int EditOceanRanchWaveList(OceanRanchWave oceanRabchWave)
        {
            try
            {
                string sql = "UPDATE OCEANRANCH72HWAVE_T "
                           + " SET"
                           + " WAVE24HDAY =@WAVE24HDAY,WAVE24HNEIGHT=@WAVE24HNEIGHT,WAVE48HDAY=@WAVE48HDAY,WAVE48HNEIGHT=@WAVE48HNEIGHT,WAVE72HDAY=@WAVE72HDAY, WAVE72HNEIGHT = @WAVE72HNEIGHT"
                           + " WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') AND OCEANRANCHNAME=@OCEANRANCHNAME";
                var PUBLISHDATE = DataExe.GetDbParameter();
                var OCEANRANCHNAME = DataExe.GetDbParameter();
                var WAVE24HDAY = DataExe.GetDbParameter();
                var WAVE24HNEIGHT = DataExe.GetDbParameter();
                var WAVE48HDAY = DataExe.GetDbParameter();
                var WAVE48HNEIGHT = DataExe.GetDbParameter();
                var WAVE72HDAY = DataExe.GetDbParameter();
                var WAVE72HNEIGHT = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                OCEANRANCHNAME.ParameterName = "@OCEANRANCHNAME";
                WAVE24HDAY.ParameterName = "@WAVE24HDAY";
                WAVE24HNEIGHT.ParameterName = "@WAVE24HNEIGHT";
                WAVE48HDAY.ParameterName = "@WAVE48HDAY";
                WAVE48HNEIGHT.ParameterName = "@WAVE48HNEIGHT";
                WAVE72HDAY.ParameterName = "@WAVE72HDAY";
                WAVE72HNEIGHT.ParameterName = "@WAVE72HNEIGHT";

                PUBLISHDATE.Value = oceanRabchWave.PUBLISHDATE.ToString();
                OCEANRANCHNAME.Value = oceanRabchWave.OCEANRANCHNAME;
                WAVE24HDAY.Value = oceanRabchWave.WAVE24HDAY;
                WAVE24HNEIGHT.Value = oceanRabchWave.WAVE24HNEIGHT;
                WAVE48HDAY.Value = oceanRabchWave.WAVE48HDAY;
                WAVE48HNEIGHT.Value = oceanRabchWave.WAVE48HNEIGHT;
                WAVE72HDAY.Value = oceanRabchWave.WAVE72HDAY;
                WAVE72HNEIGHT.Value = oceanRabchWave.WAVE72HNEIGHT;


                DbParameter[] parameters = { PUBLISHDATE, OCEANRANCHNAME, WAVE24HDAY, WAVE24HNEIGHT, WAVE48HDAY, WAVE48HNEIGHT, WAVE72HDAY, WAVE72HNEIGHT };
                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改海洋牧场海浪预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}