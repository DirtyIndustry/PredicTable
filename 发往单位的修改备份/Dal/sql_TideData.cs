using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 下午三潮高数据
    /// </summary>
    public class sql_TideData
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TideData()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 根据发布日期获取当前地区的潮高数据
        /// add by Lian
        /// </summary>
        /// <param name="tideData"></param>
        /// <returns></returns>
        public object getTideDataByArea(HT_TideData tideData,string areas)
        {
            try
            {
                string sql = "select * from SDCITY_TIDEDATA where PUBLISHDATE=to_date('" + tideData.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and SDOSCTCITY='"+areas+"' ";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取潮汐潮高数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 根据发布日期获取潮高数据
        /// </summary>
        /// <param name="tideData"></param>
        /// <returns></returns>
        public object getTideData(HT_TideData tideData)
        {
            try
            {
                string sql = "select * from SDCITY_TIDEDATA where PUBLISHDATE=to_date('" + tideData.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取潮汐潮高数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        public object get24TideData(HT_TideData tideData)
        {
            try
            {
                string sql = "select * from SDCITY_TIDEDATA where PUBLISHDATE=to_date('" + tideData.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date('" + tideData.PUBLISHDATE.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取潮汐潮高数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 添加潮汐潮高数据
        /// </summary>
        /// <param name="tideData"></param>
        /// <returns></returns>
        public int AddTideData(HT_TideData tideData)
        {
            string sql = "INSERT INTO  SDCITY_TIDEDATA "
                + " (PUBLISHDATE,FORECASTDATE,SDOSCTCITY,FIRSTHIGHWAVETIDEDATA,FIRSTLOWWAVETIDEDATA,SECONDHIGHWAVETIDEDATA,SECONDLOWWAVETIDEDATA)"
                + " VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@SDOSCTCITY,@FIRSTHIGHWAVETIDEDATA,@FIRSTLOWWAVETIDEDATA,@SECONDHIGHWAVETIDEDATA,@SECONDLOWWAVETIDEDATA)";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var SDOSCTCITY = DataExe.GetDbParameter();
            var FIRSTHIGHWAVETIDEDATA = DataExe.GetDbParameter();
            var FIRSTLOWWAVETIDEDATA = DataExe.GetDbParameter();
            var SECONDHIGHWAVETIDEDATA = DataExe.GetDbParameter();
            var SECONDLOWWAVETIDEDATA = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            SDOSCTCITY.ParameterName = "@SDOSCTCITY";
            FIRSTHIGHWAVETIDEDATA.ParameterName = "@FIRSTHIGHWAVETIDEDATA";
            FIRSTLOWWAVETIDEDATA.ParameterName = "@FIRSTLOWWAVETIDEDATA";
            SECONDHIGHWAVETIDEDATA.ParameterName = "@SECONDHIGHWAVETIDEDATA";
            SECONDLOWWAVETIDEDATA.ParameterName = "@SECONDLOWWAVETIDEDATA";

            PUBLISHDATE.Value = tideData.PUBLISHDATE.ToString();
            FORECASTDATE.Value = tideData.FORECASTDATE.ToString();
            SDOSCTCITY.Value = tideData.SDOSCTCITY;
            FIRSTHIGHWAVETIDEDATA.Value = tideData.FIRSTHIGHWAVETIDEDATA;
            FIRSTLOWWAVETIDEDATA.Value = tideData.FIRSTLOWWAVETIDEDATA;
            SECONDHIGHWAVETIDEDATA.Value = tideData.SECONDHIGHWAVETIDEDATA;
            SECONDLOWWAVETIDEDATA.Value = tideData.SECONDLOWWAVETIDEDATA;


            DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, SDOSCTCITY, FIRSTHIGHWAVETIDEDATA, FIRSTLOWWAVETIDEDATA, SECONDHIGHWAVETIDEDATA, SECONDLOWWAVETIDEDATA };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增潮汐潮高数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改潮汐潮高数据
        /// </summary>
        /// <param name="tideData"></param>
        /// <returns></returns>
        public int EditTideDate(HT_TideData tideData)
        {
            string sql = "UPDATE  SDCITY_TIDEDATA "
               + " SET FIRSTHIGHWAVETIDEDATA = @FIRSTHIGHWAVETIDEDATA,FIRSTLOWWAVETIDEDATA = @FIRSTLOWWAVETIDEDATA,SECONDHIGHWAVETIDEDATA = @SECONDHIGHWAVETIDEDATA,SECONDLOWWAVETIDEDATA = @SECONDLOWWAVETIDEDATA"
               + " WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') AND SDOSCTCITY = @SDOSCTCITY AND FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var SDOSCTCITY = DataExe.GetDbParameter();
            var FIRSTHIGHWAVETIDEDATA = DataExe.GetDbParameter();
            var FIRSTLOWWAVETIDEDATA = DataExe.GetDbParameter();
            var SECONDHIGHWAVETIDEDATA = DataExe.GetDbParameter();
            var SECONDLOWWAVETIDEDATA = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            SDOSCTCITY.ParameterName = "@SDOSCTCITY";
            FIRSTHIGHWAVETIDEDATA.ParameterName = "@FIRSTHIGHWAVETIDEDATA";
            FIRSTLOWWAVETIDEDATA.ParameterName = "@FIRSTLOWWAVETIDEDATA";
            SECONDHIGHWAVETIDEDATA.ParameterName = "@SECONDHIGHWAVETIDEDATA";
            SECONDLOWWAVETIDEDATA.ParameterName = "@SECONDLOWWAVETIDEDATA";

            PUBLISHDATE.Value = tideData.PUBLISHDATE.ToString();
            FORECASTDATE.Value = tideData.FORECASTDATE.ToString();
            SDOSCTCITY.Value = tideData.SDOSCTCITY;
            FIRSTHIGHWAVETIDEDATA.Value = tideData.FIRSTHIGHWAVETIDEDATA;
            FIRSTLOWWAVETIDEDATA.Value = tideData.FIRSTLOWWAVETIDEDATA;
            SECONDHIGHWAVETIDEDATA.Value = tideData.SECONDHIGHWAVETIDEDATA;
            SECONDLOWWAVETIDEDATA.Value = tideData.SECONDLOWWAVETIDEDATA;

            DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, SDOSCTCITY, FIRSTHIGHWAVETIDEDATA, FIRSTLOWWAVETIDEDATA, SECONDHIGHWAVETIDEDATA, SECONDLOWWAVETIDEDATA };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改潮汐潮高数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}