using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{

    /// <summary>
    /// 渤海及黄海北部冰情预报
    /// </summary>
    /// <returns></returns>
    public class sql_TBLSEAAREASEAICEFORECAST
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLSEAAREASEAICEFORECAST()
        {
            //
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
                                          //
        }


        /// <summary>
        /// 按发布日期获取渤海及黄海北部冰情预报
        /// </summary>
        /// <returns></returns>
        public object get_TBLSEAAREASEAICEFORECAST_AllData(TBLSEAAREASEAICEFORECAST TBLSEAAREASEAICEFORECAST)
        {

            try
            {
                return DataExe.GetTableExeData("select * from TBLSEAAREASEAICEFORECAST where PUBLISHDATE=to_date('" + TBLSEAAREASEAICEFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取渤海及黄海北部冰情预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 新增渤海及黄海北部冰情预报
        /// </summary>
        /// <returns></returns>
        public int Add_TBLSEAAREASEAICEFORECAST(TBLSEAAREASEAICEFORECAST TBLSEAAREASEAICEFORECAST)
        {

            string sql = "INSERT INTO  TBLSEAAREASEAICEFORECAST (PUBLISHDATE,SEAAREA,MAXICEAREA,COMMONTHICKNESS,MAXTHICKNESS) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@SEAAREA,@MAXICEAREA,@COMMONTHICKNESS,@MAXTHICKNESS)";



            var PUBLISHDATE = DataExe.GetDbParameter();
            var SEAAREA = DataExe.GetDbParameter();
            var MAXICEAREA = DataExe.GetDbParameter();
            var COMMONTHICKNESS = DataExe.GetDbParameter();
            var MAXTHICKNESS = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            SEAAREA.ParameterName = "@SEAAREA";
            MAXICEAREA.ParameterName = "@MAXICEAREA";
            COMMONTHICKNESS.ParameterName = "@COMMONTHICKNESS";
            MAXTHICKNESS.ParameterName = "@MAXTHICKNESS";




            PUBLISHDATE.Value = TBLSEAAREASEAICEFORECAST.PUBLISHDATE.ToString();
            SEAAREA.Value = TBLSEAAREASEAICEFORECAST.SEAAREA;
            MAXICEAREA.Value = TBLSEAAREASEAICEFORECAST.MAXICEAREA;
            COMMONTHICKNESS.Value = TBLSEAAREASEAICEFORECAST.COMMONTHICKNESS;
            MAXTHICKNESS.Value = TBLSEAAREASEAICEFORECAST.MAXTHICKNESS;


            DbParameter[] parameters = { PUBLISHDATE, SEAAREA, MAXICEAREA, COMMONTHICKNESS, MAXTHICKNESS };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增渤海及黄海北部冰情预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改渤海及黄海北部冰情预报
        /// </summary>
        public int Edit_TBLSEAAREASEAICEFORECAST(TBLSEAAREASEAICEFORECAST TBLSEAAREASEAICEFORECAST)
        {
            string sql = "UPDATE  TBLSEAAREASEAICEFORECAST set MAXICEAREA=@MAXICEAREA,COMMONTHICKNESS=@COMMONTHICKNESS,MAXTHICKNESS=@MAXTHICKNESS where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and SEAAREA=@SEAAREA";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var SEAAREA = DataExe.GetDbParameter();
            var MAXICEAREA = DataExe.GetDbParameter();
            var COMMONTHICKNESS = DataExe.GetDbParameter();
            var MAXTHICKNESS = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            SEAAREA.ParameterName = "@SEAAREA";
            MAXICEAREA.ParameterName = "@MAXICEAREA";
            COMMONTHICKNESS.ParameterName = "@COMMONTHICKNESS";
            MAXTHICKNESS.ParameterName = "@MAXTHICKNESS";




            PUBLISHDATE.Value = TBLSEAAREASEAICEFORECAST.PUBLISHDATE.ToString();
            SEAAREA.Value = TBLSEAAREASEAICEFORECAST.SEAAREA;
            MAXICEAREA.Value = TBLSEAAREASEAICEFORECAST.MAXICEAREA;
            COMMONTHICKNESS.Value = TBLSEAAREASEAICEFORECAST.COMMONTHICKNESS;
            MAXTHICKNESS.Value = TBLSEAAREASEAICEFORECAST.MAXTHICKNESS;


            DbParameter[] parameters = { PUBLISHDATE, SEAAREA, MAXICEAREA, COMMONTHICKNESS, MAXTHICKNESS };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改渤海及黄海北部冰情预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }

    }
}