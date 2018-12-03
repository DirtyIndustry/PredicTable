using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 7天渤海海区及黄河海港风、浪预报
    /// </summary>
    public class sql_TBLYRBHWINDWAVE7DAYFORECASTTWO
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLYRBHWINDWAVE7DAYFORECASTTWO()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 获取7天渤海海区及黄河海港风、浪预报
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public object GETTBLYRBHWINDWAVE7DAYFORECASTTWO(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo, string searchType = "p")
        {
            try
            {
                string sql = "select * from TBLYRBHWINDWAVE7DAYFORECASTTWO where PUBLISHDATE=to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by reportarea, forecastdate asc";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取所有72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 添加7天渤海海区及黄河海港风、浪预报
        /// 1127  贾 ------ 添加quanxian参数
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <returns></returns>
        public int AddTBLYRBHWINDWAVE7DAYFORECASTTWO(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo, string quanxian)
        {
            string sql = null;
            DbParameter[] parameters = null;
            if (quanxian.ToLower() == "fl")
            {
                sql = "INSERT INTO  TBLYRBHWINDWAVE7DAYFORECASTTWO (PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@REPORTAREA,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@YRBHWWFWAVEHEIGHT,@YRBHWWFWAVEDIR,@YRBHWWFFLOWDIR,@YRBHWWFFLOWLEVEL)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var REPORTAREA = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var YRBHWWFWAVEHEIGHT = DataExe.GetDbParameter();
                var YRBHWWFWAVEDIR = DataExe.GetDbParameter();
                var YRBHWWFFLOWDIR = DataExe.GetDbParameter();
                var YRBHWWFFLOWLEVEL = DataExe.GetDbParameter();
                //var YRBHWWFWATERTEMPERATURE = DataExe.GetDbParameter();
                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                REPORTAREA.ParameterName = "@REPORTAREA";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                YRBHWWFWAVEHEIGHT.ParameterName = "@YRBHWWFWAVEHEIGHT";
                YRBHWWFWAVEDIR.ParameterName = "@YRBHWWFWAVEDIR";
                YRBHWWFFLOWDIR.ParameterName = "@YRBHWWFFLOWDIR";
                YRBHWWFFLOWLEVEL.ParameterName = "@YRBHWWFFLOWLEVEL";
                //YRBHWWFWATERTEMPERATURE.ParameterName = "@YRBHWWFWATERTEMPERATURE";
                PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
                REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
                FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
                YRBHWWFWAVEHEIGHT.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEHEIGHT;
                YRBHWWFWAVEDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEDIR.ToString();
                YRBHWWFFLOWDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWDIR.ToString();
                YRBHWWFFLOWLEVEL.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWLEVEL;
                //YRBHWWFWATERTEMPERATURE.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWATERTEMPERATURE;
                parameters = new DbParameter[] { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL };
            }
            else if (quanxian.ToLower() == "sw")
            {
                sql = "INSERT INTO  TBLYRBHWINDWAVE7DAYFORECASTTWO (PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWATERTEMPERATURE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@REPORTAREA,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@YRBHWWFWATERTEMPERATURE)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var REPORTAREA = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var YRBHWWFWATERTEMPERATURE = DataExe.GetDbParameter();
                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                REPORTAREA.ParameterName = "@REPORTAREA";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                YRBHWWFWATERTEMPERATURE.ParameterName = "@YRBHWWFWATERTEMPERATURE";
                PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
                REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
                FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
                YRBHWWFWATERTEMPERATURE.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWATERTEMPERATURE;
                parameters = new DbParameter[] { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWATERTEMPERATURE };

            }

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改7天渤海海区及黄河海港风、浪预报
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <returns></returns>
        public int UPDATETBLYRBHWINDWAVE7DAYFORECASTTWO(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo, string quanxian)
        {
            string sql = null;
            DbParameter[] parameters = null;
            if (quanxian.ToLower() == "fl")
            {
                sql = "UPDATE   TBLYRBHWINDWAVE7DAYFORECASTTWO set  YRBHWWFWAVEHEIGHT=@YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR=@YRBHWWFWAVEDIR, YRBHWWFFLOWDIR=@YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL=@YRBHWWFFLOWLEVEL where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and REPORTAREA=@REPORTAREA and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var REPORTAREA = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var YRBHWWFWAVEHEIGHT = DataExe.GetDbParameter();
                var YRBHWWFWAVEDIR = DataExe.GetDbParameter();
                var YRBHWWFFLOWDIR = DataExe.GetDbParameter();
                var YRBHWWFFLOWLEVEL = DataExe.GetDbParameter();
                //var YRBHWWFWATERTEMPERATURE = DataExe.GetDbParameter();
                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                REPORTAREA.ParameterName = "@REPORTAREA";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                YRBHWWFWAVEHEIGHT.ParameterName = "@YRBHWWFWAVEHEIGHT";
                YRBHWWFWAVEDIR.ParameterName = "@YRBHWWFWAVEDIR";
                YRBHWWFFLOWDIR.ParameterName = "@YRBHWWFFLOWDIR";
                YRBHWWFFLOWLEVEL.ParameterName = "@YRBHWWFFLOWLEVEL";
                //YRBHWWFWATERTEMPERATURE.ParameterName = "@YRBHWWFWATERTEMPERATURE";
                PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
                REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
                FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
                YRBHWWFWAVEHEIGHT.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEHEIGHT;
                YRBHWWFWAVEDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEDIR.ToString();
                YRBHWWFFLOWDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWDIR.ToString();
                YRBHWWFFLOWLEVEL.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWLEVEL;
                //YRBHWWFWATERTEMPERATURE.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWATERTEMPERATURE;
                parameters = new DbParameter[] { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL };

            }
            else if (quanxian.ToLower() == "sw")
            {
                sql = "UPDATE   TBLYRBHWINDWAVE7DAYFORECASTTWO set  YRBHWWFWATERTEMPERATURE=@YRBHWWFWATERTEMPERATURE where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and REPORTAREA=@REPORTAREA and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var REPORTAREA = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var YRBHWWFWATERTEMPERATURE = DataExe.GetDbParameter();
                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                REPORTAREA.ParameterName = "@REPORTAREA";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                YRBHWWFWATERTEMPERATURE.ParameterName = "@YRBHWWFWATERTEMPERATURE";
                PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
                REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
                FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
                YRBHWWFWATERTEMPERATURE.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWATERTEMPERATURE;
                parameters = new DbParameter[] { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWATERTEMPERATURE };
            }
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

    }
}