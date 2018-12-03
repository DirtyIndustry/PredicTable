using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 综述表单操做
    /// </summary>
    public class sql_TBLZS
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLZS()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 检索
        /// 3天海洋水文气象预报棕述
        /// 24小时海洋水文气象预报综述
        /// 7天海洋水文气象预报综述
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object get_TBLSWQX_ZS_3DayS_OR_24HourS(DateTime publishTime)
        {
            string sql = "SELECT * FROM HT_ZSVIEW WHERE PUBLISHDATE=to_date('" + publishTime.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss')";
            try
            {
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("检索海洋水文气象预报棕述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        //=========================================3天海洋水文气象预报棕述===========================================
        /// <summary>
        /// 新增3天海洋水文气象预报棕述
        /// </summary>
        /// <returns></returns>
        public int Add_TBLSWQX_ZS_3Days(DateTime publishTime,string meteorologicalreview,string meteorologicalreviewcx)
        {
            string sql = "INSERT INTO HT_ZSVIEW (PUBLISHDATE, METEOROLOGICALREVIEW, METEOROLOGICALREVIEW24HOUR,METEOROLOGICALREVIEW7DAYS,METEOROLOGICALREVIEWCX,METEOROLOGICALREVIEW24HOURCX,METEOROLOGICALREVIEW7DAYSCX) "
                            + " VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@METEOROLOGICALREVIEW, ' ',' ',@METEOROLOGICALREVIEWCX,'','')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEWCX = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEW.ParameterName = "@METEOROLOGICALREVIEW";
            METEOROLOGICALREVIEWCX.ParameterName = "@METEOROLOGICALREVIEWCX";

            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEW.Value = meteorologicalreview;
            METEOROLOGICALREVIEWCX.Value = meteorologicalreviewcx;
            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW, METEOROLOGICALREVIEWCX };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增3天海洋水文气象预报棕述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改3天海洋水文气象预报棕述(海浪)
        /// </summary>
        /// <returns></returns>
        public int Edit_TBLSWQX_ZS_3DayS(DateTime publishTime, string meteorologicalreview)
        {
            string sql = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEW=@METEOROLOGICALREVIEW WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEW.ParameterName = "@METEOROLOGICALREVIEW";

            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEW.Value = meteorologicalreview.ToString();

            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW };
            
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改3天海洋水文气象预报棕述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改3天海洋水文气象预报棕述（潮汐）
        /// </summary>
        /// <returns></returns>
        public int Edit_TBLSWQX_ZS_3DaySCX(DateTime publishTime, string meteorologicalreviewcx)
        {
            string sql = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEWCX=@METEOROLOGICALREVIEWCX WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEWCX = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEWCX.ParameterName = "@METEOROLOGICALREVIEWCX";

            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEWCX.Value = meteorologicalreviewcx.ToString();

            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEWCX };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改3天海洋水文气象预报棕述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        //=========================================24小时海洋水文气象预报综述===========================================
        /// <summary>
        /// 新增24小时水文气象预报综述
        /// </summary>
        /// <returns></returns>
        public int Add_TBLSWQX_ZS_24HOURS(DateTime publishTime, string meteorologicalreview24hour, string meteorologicalreview24hourcx)
        {

            string sql = "INSERT INTO HT_ZSVIEW (PUBLISHDATE, METEOROLOGICALREVIEW, METEOROLOGICALREVIEW24HOUR,METEOROLOGICALREVIEW7DAYS,METEOROLOGICALREVIEWCX,METEOROLOGICALREVIEW24HOURCX,METEOROLOGICALREVIEW7DAYSCX) "
                           + " VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),'', @METEOROLOGICALREVIEW24HOUR ,' ','',@METEOROLOGICALREVIEW24HOURCX,'')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW24HOUR = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW24HOURCX = DataExe.GetDbParameter();


            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEW24HOUR.ParameterName = "@METEOROLOGICALREVIEW24HOUR";
            METEOROLOGICALREVIEW24HOURCX.ParameterName = "@METEOROLOGICALREVIEW24HOURCX";


            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEW24HOUR.Value = meteorologicalreview24hour;
            METEOROLOGICALREVIEW24HOURCX.Value = meteorologicalreview24hourcx;

            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW24HOUR, METEOROLOGICALREVIEW24HOURCX };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增24小时水文气象预报综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改24小时水文气象预报综述(海浪)
        /// </summary>
        /// <returns></returns>
        public int Edit_TBLSWQX_ZS_24HOURS(DateTime publishTime, string meteorologicalreview24hour)
        {
            string sql = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEW24HOUR  = @METEOROLOGICALREVIEW24HOUR WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";
            
            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW24HOUR = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEW24HOUR.ParameterName = "@METEOROLOGICALREVIEW24HOUR";

            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEW24HOUR.Value = meteorologicalreview24hour.ToString();

            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW24HOUR };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改24小时水文气象预报综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改24小时水文气象预报综述(潮汐)
        /// </summary>
        /// <returns></returns>
        public int Edit_TBLSWQX_ZS_24HOURSCX(DateTime publishTime, string meteorologicalreview24hourcx)
        {
            string sql = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEW24HOURCX  = @METEOROLOGICALREVIEW24HOURCX WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW24HOURCX = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEW24HOURCX.ParameterName = "@METEOROLOGICALREVIEW24HOURCX";

            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEW24HOURCX.Value = meteorologicalreview24hourcx.ToString();

            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW24HOURCX };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改24小时水文气象预报综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        //=========================================7天海洋水文气象预报综述===========================================
        /// <summary>
        /// 新增7天海洋水文气象预报综述
        /// </summary>
        /// <returns></returns>
        public int Add_TBLSWQX_ZS_7Days(DateTime publishTime, string meteorologicalreview7Days, string meteorologicalreview7Dayscx)
        {

            string sql = "INSERT INTO HT_ZSVIEW (PUBLISHDATE, METEOROLOGICALREVIEW, METEOROLOGICALREVIEW24HOUR,METEOROLOGICALREVIEW7DAYS,METEOROLOGICALREVIEWCX,METEOROLOGICALREVIEW24HOURCX,METEOROLOGICALREVIEW7DAYSCX) "
                           + " VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),'', '' ,@METEOROLOGICALREVIEW7DAYS,'','',@METEOROLOGICALREVIEW7DAYSCX)";
            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW7DAYS = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW7DAYSCX = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEW7DAYS.ParameterName = "@METEOROLOGICALREVIEW7DAYS";
            METEOROLOGICALREVIEW7DAYSCX.ParameterName = "@METEOROLOGICALREVIEW7DAYSCX";

            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEW7DAYS.Value = meteorologicalreview7Days;
            METEOROLOGICALREVIEW7DAYSCX.Value = meteorologicalreview7Dayscx;

            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW7DAYS, METEOROLOGICALREVIEW7DAYSCX };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增7天海洋水文气象预报综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改7天海洋水文气象预报综述(海浪)
        /// </summary>
        /// <returns></returns>
        public int Edit_TBLSWQX_ZS_7Days(DateTime publishTime, string meteorologicalreview7Days)
        {
            string sql = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEW7DAYS  = @METEOROLOGICALREVIEW7DAYS WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";
            
            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW7DAYS = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEW7DAYS.ParameterName = "@METEOROLOGICALREVIEW7DAYS";

            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEW7DAYS.Value = meteorologicalreview7Days.ToString();

            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW7DAYS };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改7天海洋水文气象预报综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// 修改7天海洋水文气象预报综述（潮汐）
        /// </summary>
        /// <returns></returns>
        public int Edit_TBLSWQX_ZS_7DaysCX(DateTime publishTime, string meteorologicalreview7Dayscx)
        {
            string sql = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEW7DAYSCX  = @METEOROLOGICALREVIEW7DAYSCX WHERE PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var METEOROLOGICALREVIEW7DAYSCX = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            METEOROLOGICALREVIEW7DAYSCX.ParameterName = "@METEOROLOGICALREVIEW7DAYSCX";

            PUBLISHDATE.Value = publishTime.ToString("yyyy-MM-dd");
            METEOROLOGICALREVIEW7DAYSCX.Value = meteorologicalreview7Dayscx.ToString();

            DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW7DAYSCX };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改7天海洋水文气象预报综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}