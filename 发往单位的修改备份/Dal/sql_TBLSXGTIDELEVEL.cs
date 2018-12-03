using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 72小时东营神仙沟挡潮闸专项预报
    /// </summary>
    public class sql_TBLSXGTIDELEVEL
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLSXGTIDELEVEL()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        /// 查询72小时东营神仙沟挡潮闸专项预报
        /// </summary>
        /// <param name="TBLMZZTIDELEVEL"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public object GETTBLSXGTIDELEVEL(TBLMZZTIDELEVEL TBLMZZTIDELEVEL, string searchType)
        {

            try
            {
                string sql = "";
                if (searchType == "p")
                {
                    sql = "select * from HT_TBLSXGTIDELEVEL where PUBLISHDATE=to_date('" + TBLMZZTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                }
                else
                {
                    sql = "select * from HT_TBLSXGTIDELEVEL "
                        + " where FORECASTDATE > to_date('" + TBLMZZTIDELEVEL.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and FORECASTDATE < to_date('" + TBLMZZTIDELEVEL.FORECASTDATE.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and PUBLISHDATE=to_date('" + TBLMZZTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";

                }
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取72小时东营神仙沟挡潮闸专项预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 新增72小时东营神仙沟挡潮闸专项预报
        /// </summary>
        /// <returns></returns>
        public int AddTBLSXGTIDELEVEL(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
        {

            string sql = "INSERT INTO  HT_TBLSXGTIDELEVEL (PUBLISHDATE,MZZTLLOWTIDELEVELFORTHESECONDT,FORECASTDATE,MZZTLFIRSTWAVEOFTIME,MZZTLFIRSTWAVETIDELEVEL,MZZTLFIRSTTIMELOWTIDE,MZZTLLOWTIDELEVELFORTHEFIRSTTI,MZZTLSECONDWAVEOFTIME,MZZTLSECONDWAVETIDELEVEL,MZZTLSECONDTIMELOWTIDE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@MZZTLLOWTIDELEVELFORTHESECONDT,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@MZZTLFIRSTWAVEOFTIME,@MZZTLFIRSTWAVETIDELEVEL,@MZZTLFIRSTTIMELOWTIDE,@MZZTLLOWTIDELEVELFORTHEFIRSTTI,@MZZTLSECONDWAVEOFTIME,@MZZTLSECONDWAVETIDELEVEL,@MZZTLSECONDTIMELOWTIDE)";



            var PUBLISHDATE = DataExe.GetDbParameter();
            var MZZTLLOWTIDELEVELFORTHESECONDT = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var MZZTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
            var MZZTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
            var MZZTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
            var MZZTLLOWTIDELEVELFORTHEFIRSTTI = DataExe.GetDbParameter();
            var MZZTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
            var MZZTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();
            var MZZTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            MZZTLLOWTIDELEVELFORTHESECONDT.ParameterName = "@MZZTLLOWTIDELEVELFORTHESECONDT";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            MZZTLFIRSTWAVEOFTIME.ParameterName = "@MZZTLFIRSTWAVEOFTIME";
            MZZTLFIRSTWAVETIDELEVEL.ParameterName = "@MZZTLFIRSTWAVETIDELEVEL";
            MZZTLFIRSTTIMELOWTIDE.ParameterName = "@MZZTLFIRSTTIMELOWTIDE";
            MZZTLLOWTIDELEVELFORTHEFIRSTTI.ParameterName = "@MZZTLLOWTIDELEVELFORTHEFIRSTTI";
            MZZTLSECONDWAVEOFTIME.ParameterName = "@MZZTLSECONDWAVEOFTIME";
            MZZTLSECONDWAVETIDELEVEL.ParameterName = "@MZZTLSECONDWAVETIDELEVEL";
            MZZTLSECONDTIMELOWTIDE.ParameterName = "@MZZTLSECONDTIMELOWTIDE";




            PUBLISHDATE.Value = TBLMZZTIDELEVEL.PUBLISHDATE.ToString();
            MZZTLLOWTIDELEVELFORTHESECONDT.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHESECONDT;
            FORECASTDATE.Value = TBLMZZTIDELEVEL.FORECASTDATE.ToString();
            MZZTLFIRSTWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVEOFTIME;
            MZZTLFIRSTWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVETIDELEVEL;
            MZZTLFIRSTTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLFIRSTTIMELOWTIDE;
            MZZTLLOWTIDELEVELFORTHEFIRSTTI.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHEFIRSTTI;
            MZZTLSECONDWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVEOFTIME;
            MZZTLSECONDWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVETIDELEVEL;
            MZZTLSECONDTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLSECONDTIMELOWTIDE;


            DbParameter[] parameters = { PUBLISHDATE, MZZTLLOWTIDELEVELFORTHESECONDT, FORECASTDATE, MZZTLFIRSTWAVEOFTIME, MZZTLFIRSTWAVETIDELEVEL, MZZTLFIRSTTIMELOWTIDE, MZZTLLOWTIDELEVELFORTHEFIRSTTI, MZZTLSECONDWAVEOFTIME, MZZTLSECONDWAVETIDELEVEL, MZZTLSECONDTIMELOWTIDE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增72小时东营神仙沟挡潮闸专项预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改72小时东营神仙沟挡潮闸专项预报
        /// </summary>
        public int EditTBLSXGTIDELEVEL(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
        {
            string sql = "UPDATE   HT_TBLSXGTIDELEVEL set	MZZTLLOWTIDELEVELFORTHESECONDT=@MZZTLLOWTIDELEVELFORTHESECONDT,MZZTLFIRSTWAVEOFTIME=@MZZTLFIRSTWAVEOFTIME,MZZTLFIRSTWAVETIDELEVEL=@MZZTLFIRSTWAVETIDELEVEL,MZZTLFIRSTTIMELOWTIDE=@MZZTLFIRSTTIMELOWTIDE,MZZTLLOWTIDELEVELFORTHEFIRSTTI=@MZZTLLOWTIDELEVELFORTHEFIRSTTI,MZZTLSECONDWAVEOFTIME=@MZZTLSECONDWAVEOFTIME,MZZTLSECONDWAVETIDELEVEL=@MZZTLSECONDWAVETIDELEVEL,MZZTLSECONDTIMELOWTIDE=@MZZTLSECONDTIMELOWTIDE where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var MZZTLLOWTIDELEVELFORTHESECONDT = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var MZZTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
            var MZZTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
            var MZZTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
            var MZZTLLOWTIDELEVELFORTHEFIRSTTI = DataExe.GetDbParameter();
            var MZZTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
            var MZZTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();
            var MZZTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            MZZTLLOWTIDELEVELFORTHESECONDT.ParameterName = "@MZZTLLOWTIDELEVELFORTHESECONDT";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            MZZTLFIRSTWAVEOFTIME.ParameterName = "@MZZTLFIRSTWAVEOFTIME";
            MZZTLFIRSTWAVETIDELEVEL.ParameterName = "@MZZTLFIRSTWAVETIDELEVEL";
            MZZTLFIRSTTIMELOWTIDE.ParameterName = "@MZZTLFIRSTTIMELOWTIDE";
            MZZTLLOWTIDELEVELFORTHEFIRSTTI.ParameterName = "@MZZTLLOWTIDELEVELFORTHEFIRSTTI";
            MZZTLSECONDWAVEOFTIME.ParameterName = "@MZZTLSECONDWAVEOFTIME";
            MZZTLSECONDWAVETIDELEVEL.ParameterName = "@MZZTLSECONDWAVETIDELEVEL";
            MZZTLSECONDTIMELOWTIDE.ParameterName = "@MZZTLSECONDTIMELOWTIDE";




            PUBLISHDATE.Value = TBLMZZTIDELEVEL.PUBLISHDATE.ToString();
            MZZTLLOWTIDELEVELFORTHESECONDT.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHESECONDT;
            FORECASTDATE.Value = TBLMZZTIDELEVEL.FORECASTDATE.ToString();
            MZZTLFIRSTWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVEOFTIME;
            MZZTLFIRSTWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVETIDELEVEL;
            MZZTLFIRSTTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLFIRSTTIMELOWTIDE;
            MZZTLLOWTIDELEVELFORTHEFIRSTTI.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHEFIRSTTI;
            MZZTLSECONDWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVEOFTIME;
            MZZTLSECONDWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVETIDELEVEL;
            MZZTLSECONDTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLSECONDTIMELOWTIDE;


            DbParameter[] parameters = { PUBLISHDATE, MZZTLLOWTIDELEVELFORTHESECONDT, FORECASTDATE, MZZTLFIRSTWAVEOFTIME, MZZTLFIRSTWAVETIDELEVEL, MZZTLFIRSTTIMELOWTIDE, MZZTLLOWTIDELEVELFORTHEFIRSTTI, MZZTLSECONDWAVEOFTIME, MZZTLSECONDWAVETIDELEVEL, MZZTLSECONDTIMELOWTIDE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改72小时东营神仙沟挡潮闸专项预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }
        
        public object GETTBLSXGDATA(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
        {
            try
            {
                string sql = "";
                sql = "select * from HT_TBLSXGTIDELEVEL "
                       + " where FORECASTDATE > to_date('" + TBLMZZTIDELEVEL.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                       + " and FORECASTDATE < to_date('" + TBLMZZTIDELEVEL.FORECASTDATE.AddDays(3).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                       + " and PUBLISHDATE=to_date('" + TBLMZZTIDELEVEL.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取72小时东营神仙沟挡潮闸专项预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
            
        }
    }
}