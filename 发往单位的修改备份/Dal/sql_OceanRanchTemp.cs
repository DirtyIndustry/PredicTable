using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 海洋牧场-海温
    /// </summary>
    public class sql_OceanRanchTemp
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_OceanRanchTemp()
        {
            DataExe = new DataExecution();
        }

        /// <summary>
        /// 获取海洋牧场海温预报
        /// </summary>
        /// <param name="oceanRabchTide"></param>
        /// <returns></returns>
        public object GetOceanRanchTempList(OceanRanchTemp oceanRabchTemp)
        {
            try
            {
                string sql = "SELECT * FROM OCEANRANCH72HTEMPERATURE_T WHERE PUBLISHDATE = to_date('" + oceanRabchTemp.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海洋牧场海温预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取海洋牧场海温预报解析数据
        /// </summary>
        /// <param name="oceanRabchTemp"></param>
        /// <returns></returns>
        public object GetOceanRanchTempListBy_S(OceanRanchTemp oceanRabchTemp)
        {
            try
            {
                string sql = "SELECT * FROM OCEANRANCH72HTEMPERATURE_S WHERE to_char(FORECASTDATE,'yyyy-mm-dd') = '" + oceanRabchTemp.PUBLISHDATE.ToString("yyyy-MM-dd") + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海洋牧场海温预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 添加海洋牧场海温预报
        /// </summary>
        /// <param name="oceanRabchTemp"></param>
        /// <returns></returns>
        public int InsertOceanRanchTempList(OceanRanchTemp oceanRabchTemp)
        {
            try
            {
                string sql = "INSERT INTO OCEANRANCH72HTEMPERATURE_T ( "
                           + " PUBLISHDATE,FORECASTDATE,OCEANRANCHNAME,OCEANRANCHSHORTNAME,SN,TEMP24H,TEMP48H,TEMP72H)"
                           + " VALUES "
                           + " ( to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@OCEANRANCHNAME,@OCEANRANCHSHORTNAME,@SN,@TEMP24H,@TEMP48H,@TEMP72H)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var OCEANRANCHNAME = DataExe.GetDbParameter();
                var OCEANRANCHSHORTNAME = DataExe.GetDbParameter();
                var SN = DataExe.GetDbParameter();
                var TEMP24H = DataExe.GetDbParameter();
                var TEMP48H = DataExe.GetDbParameter();
                var TEMP72H = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                OCEANRANCHNAME.ParameterName = "@OCEANRANCHNAME";
                OCEANRANCHSHORTNAME.ParameterName = "@OCEANRANCHSHORTNAME";
                SN.ParameterName = "@SN";
                TEMP24H.ParameterName = "@TEMP24H";
                TEMP48H.ParameterName = "@TEMP48H";
                TEMP72H.ParameterName = "@TEMP72H";

                PUBLISHDATE.Value = oceanRabchTemp.PUBLISHDATE.ToString();
                FORECASTDATE.Value = oceanRabchTemp.FORECASTDATE.ToString();
                OCEANRANCHNAME.Value = oceanRabchTemp.OCEANRANCHNAME;
                OCEANRANCHSHORTNAME.Value = oceanRabchTemp.OCEANRANCHSHORTNAME;
                SN.Value = oceanRabchTemp.SN;
                TEMP24H.Value = oceanRabchTemp.TEMP24H;
                TEMP48H.Value = oceanRabchTemp.TEMP48H;
                TEMP72H.Value = oceanRabchTemp.TEMP72H;


                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, OCEANRANCHNAME, OCEANRANCHSHORTNAME, SN, TEMP24H, TEMP48H, TEMP72H };
                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("添加海洋牧场海温预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改海洋牧场海温预报
        /// </summary>
        /// <param name="oceanRabchTemp"></param>
        /// <returns></returns>
        public int EditOceanRanchTempList(OceanRanchTemp oceanRabchTemp)
        {
            try
            {
                string sql = "UPDATE OCEANRANCH72HTEMPERATURE_T "
                           + " SET"
                           + " TEMP24H =@TEMP24H,TEMP48H=@TEMP48H,TEMP72H=@TEMP72H"
                           + " WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') AND OCEANRANCHNAME=@OCEANRANCHNAME";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var OCEANRANCHNAME = DataExe.GetDbParameter();
                var TEMP24H = DataExe.GetDbParameter();
                var TEMP48H = DataExe.GetDbParameter();
                var TEMP72H = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                OCEANRANCHNAME.ParameterName = "@OCEANRANCHNAME";
                TEMP24H.ParameterName = "@TEMP24H";
                TEMP48H.ParameterName = "@TEMP48H";
                TEMP72H.ParameterName = "@TEMP72H";

                PUBLISHDATE.Value = oceanRabchTemp.PUBLISHDATE.ToString();
                OCEANRANCHNAME.Value = oceanRabchTemp.OCEANRANCHNAME;
                TEMP24H.Value = oceanRabchTemp.TEMP24H;
                TEMP48H.Value = oceanRabchTemp.TEMP48H;
                TEMP72H.Value = oceanRabchTemp.TEMP72H;


                DbParameter[] parameters = { PUBLISHDATE, OCEANRANCHNAME, TEMP24H, TEMP48H, TEMP72H };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改海洋牧场海温预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}