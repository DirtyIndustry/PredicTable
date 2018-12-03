using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 指挥处预报
    /// </summary>
    /// <returns></returns>
    public class sql_TBLZHCFORECAST
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLZHCFORECAST()
        {
            //
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
                                          //
        }


        /// <summary>
        /// 按发布日期获取指挥处预报
        /// </summary>
        /// <returns></returns>
        public object get_TBLZHCFORECAST_AllData(TBLZHCFORECAST TBLZHCFORECAST)
        {

            try
            {
                return DataExe.GetTableExeData("select * from TBLZHCFORECAST where PUBLISHDATE=to_date('" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24@mi@ss')");

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 按发布日期删除预报内容
        /// </summary>
        /// <returns></returns>
        public object del_TBLZHCFORECAST_AllData(TBLZHCFORECAST TBLZHCFORECAST)
        {

            try
            {
                return DataExe.GetObjectExeData("DELETE  from TBLZHCFORECAST where PUBLISHDATE=to_date('" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24@mi@ss')");

            }
            catch (Exception ex)
            {
                WriteLog.Write("删除指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 按照预报时间获取上午指挥处数据
        /// </summary>
        /// <param name="TBLZHCFORECAST"></param>
        /// <returns></returns>
        public object GETTBLZHCFORECASTBYFORCASEDATE(TBLZHCFORECAST TBLZHCFORECAST, string date1, string date2, string date3)
        {

            try
            {
                string sql = "select * from (select PUBLISHDATE,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime,SEAAREA,"
                                + " WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION from TBLZHCFORECAST )a"
                                + " where a.forecasttime = '" + date1 + "'"
                                + " or a.forecasttime = '" + date2 + "'"
                                + " or a.forecasttime = '" + date3 + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// 下午指挥处不固定海区部分（数据库有字段保存固定字符串---bgdhq）
        /// </summary>
        /// <param name="TBLZHCFORECAST"></param>
        /// <returns></returns>
        public object GETTBLZHCFORECASTBYFORCASEDATEALL_bgdhq(TBLZHCFORECAST TBLZHCFORECAST)
        {

            try
            {
                string sql = "select * from (select to_char(PUBLISHDATE,'yyyy-MM-dd HH24:mi:ss') AS publishTime,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime,SEAAREA,"
                                + " WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION from TBLZHCFORECAST )a"
                                + " where A.publishTime = '" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "' and  WEATHERAPPEARANCE='bgdhq'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 7点上午指挥处
        /// </summary>
        /// <param name="TBLZHCFORECAST"></param>
        /// <returns></returns>
        public object GETTBLZHCFORECASTBYFORCASEDATEBYPUBLISH(TBLZHCFORECAST TBLZHCFORECAST)
        {

            try
            {
                string sql = "select * from (select to_char(PUBLISHDATE,'yyyy-MM-dd HH24:mi:ss') AS publishTime,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime,SEAAREA,"
                                + " WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION from TBLZHCFORECAST )a"
                                + " where A.publishTime = '" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 查询指挥处昨天下午不固定海区
        /// </summary>
        /// <param name="TBLZHCFORECAST"></param>
        /// <returns></returns>
        public object GETTBLZHCFORECASTBYFORCASEDATEBYPUBLISH_bgd(TBLZHCFORECAST TBLZHCFORECAST)
        {

            try
            {
                string sql = "select * from (select to_char(PUBLISHDATE,'yyyy-MM-dd HH24:mi:ss') AS publishTime,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime,SEAAREA,"
                                + " WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION from TBLZHCFORECAST )a"
                                + " where A.publishTime = '" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "' ";// and  WEATHERAPPEARANCE='bgdhq'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            { 
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 7点上午指挥处
        /// </summary>
        /// <param name="TBLZHCFORECAST"></param>
        /// <returns></returns>
        public object GETTBLZHCFORECASTBYFORCASEDATEALL(TBLZHCFORECAST TBLZHCFORECAST, string date1, string date2, string date3)
        {

            try
            {
                string sql = "select * from (select to_char(PUBLISHDATE,'yyyy-MM-dd HH24:mi:ss') AS publishTime,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime,SEAAREA,"
                                + " WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION from TBLZHCFORECAST )a"
                                + " where A.publishTime = '" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取责任海区
        /// </summary>
        /// <param name="TBLZHCFORECAST"></param>
        /// <returns></returns>
        public object GetZRArea(TBLZHCFORECAST TBLZHCFORECAST)
        {
            try
            {
                string sql = "select * from (select to_char(PUBLISHDATE,'yyyy-MM-dd HH24:mi:ss') AS publishTime,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime,SEAAREA,"
                                + " WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION from TBLZHCFORECAST )a"
                                + " where A.publishTime = '" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "' AND A.WEATHERAPPEARANCE='bgdhq'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 按照预报时间检索是否存在上午指挥处数据
        /// </summary>
        /// <param name="TBLZHCFORECAST"></param>
        /// <returns></returns>
        public object GETTBLZHCFORECASTBYFORCASEDATE(TBLZHCFORECAST TBLZHCFORECAST, string date)
        {

            try
            {
                string sql = "select * from (select to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime,SEAAREA,"
                                + " WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION from TBLZHCFORECAST )a"
                                + " where a.forecasttime = '" + date + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 按发布日期和海区获取指挥处预报
        /// </summary>
        /// <returns></returns>
        public object get_TBLZHCFORECAST_AllData2(TBLZHCFORECAST TBLZHCFORECAST)
        {

            try
            {
                return DataExe.GetTableExeData("select * from TBLZHCFORECAST where PUBLISHDATE=to_date('" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24@mi@ss') and SEAAREA=" + TBLZHCFORECAST.SEAAREA);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 新增指挥处预报
        /// </summary>
        /// <returns></returns>
        public int Add_TBLZHCFORECAST(TBLZHCFORECAST TBLZHCFORECAST)
        {
            string sql = null;
            DbParameter[] parameters = null;

            sql = "INSERT INTO  TBLZHCFORECAST (PUBLISHDATE,FORECASTDATE,SEAAREA,WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION)"
              + " VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@SEAAREA,@WEATHERAPPEARANCE,@WINDDIRECTION,@WINDFORCE,@WAVEHEIGHT,@WAVEDIRECTION)";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var SEAAREA = DataExe.GetDbParameter();
            var WEATHERAPPEARANCE = DataExe.GetDbParameter();
            var WINDDIRECTION = DataExe.GetDbParameter();
            var WINDFORCE = DataExe.GetDbParameter();
            var WAVEHEIGHT = DataExe.GetDbParameter();
            var WAVEDIRECTION = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            SEAAREA.ParameterName = "@SEAAREA";
            WEATHERAPPEARANCE.ParameterName = "@WEATHERAPPEARANCE";
            WINDDIRECTION.ParameterName = "@WINDDIRECTION";
            WINDFORCE.ParameterName = "@WINDFORCE";
            WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";
            WAVEDIRECTION.ParameterName = "@WAVEDIRECTION";

            PUBLISHDATE.Value = TBLZHCFORECAST.PUBLISHDATE.ToString();
            FORECASTDATE.Value = TBLZHCFORECAST.FORECASTDATE.ToString();
            SEAAREA.Value = TBLZHCFORECAST.SEAAREA;
            WEATHERAPPEARANCE.Value = TBLZHCFORECAST.WEATHERAPPEARANCE;
            WINDDIRECTION.Value = TBLZHCFORECAST.WINDDIRECTION;
            WINDFORCE.Value = TBLZHCFORECAST.WINDFORCE;
            WAVEHEIGHT.Value = TBLZHCFORECAST.WAVEHEIGHT;
            WAVEDIRECTION.Value = TBLZHCFORECAST.WAVEDIRECTION;

            parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, SEAAREA, WEATHERAPPEARANCE, WINDDIRECTION, WINDFORCE, WAVEHEIGHT, WAVEDIRECTION };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        //1127  贾 ------ 添加quanxian参数进行数据判定
        public int Add_TBLZHCFORECAST(TBLZHCFORECAST TBLZHCFORECAST, string quanxian)
        {
            //if (TBLZHCFORECAST.SEAAREA != "青岛市")
            //{
            //    return Add_TBLZHCFORECAST(TBLZHCFORECAST);
            //}
            //else
            //{
                string sql = null;
                DbParameter[] parameters = null;
            //if (quanxian.ToLower() == "sw")
            //{
            //    sql = "INSERT INTO  TBLZHCFORECAST (PUBLISHDATE,FORECASTDATE,SEAAREA,WAVEHEIGHT,WAVEDIRECTION)"
            //      + " VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@SEAAREA,@WAVEHEIGHT,@WAVEDIRECTION)";

            //    var PUBLISHDATE = DataExe.GetDbParameter();
            //    var FORECASTDATE = DataExe.GetDbParameter();
            //    var SEAAREA = DataExe.GetDbParameter();
            //    var WAVEHEIGHT = DataExe.GetDbParameter();
            //    var WAVEDIRECTION = DataExe.GetDbParameter();

            //    PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            //    FORECASTDATE.ParameterName = "@FORECASTDATE";
            //    SEAAREA.ParameterName = "@SEAAREA";
            //    WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";
            //    WAVEDIRECTION.ParameterName = "@WAVEDIRECTION";

            //    PUBLISHDATE.Value = TBLZHCFORECAST.PUBLISHDATE.ToString();
            //    FORECASTDATE.Value = TBLZHCFORECAST.FORECASTDATE.ToString();
            //    SEAAREA.Value = TBLZHCFORECAST.SEAAREA;
            //    WAVEHEIGHT.Value = TBLZHCFORECAST.WAVEHEIGHT;
            //    WAVEDIRECTION.Value = TBLZHCFORECAST.WAVEDIRECTION;

            //    parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, SEAAREA, WAVEHEIGHT, WAVEDIRECTION };
            //}
            //else
            if (quanxian.ToLower() == "fl")
                {
                    sql = "INSERT INTO  TBLZHCFORECAST (PUBLISHDATE,FORECASTDATE,SEAAREA,WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE)"
                      + " VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@SEAAREA,@WEATHERAPPEARANCE,@WINDDIRECTION,@WINDFORCE)";

                    var PUBLISHDATE = DataExe.GetDbParameter();
                    var FORECASTDATE = DataExe.GetDbParameter();
                    var SEAAREA = DataExe.GetDbParameter();
                    var WEATHERAPPEARANCE = DataExe.GetDbParameter();
                    var WINDDIRECTION = DataExe.GetDbParameter();
                    var WINDFORCE = DataExe.GetDbParameter();

                    PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                    FORECASTDATE.ParameterName = "@FORECASTDATE";
                    SEAAREA.ParameterName = "@SEAAREA";
                    WEATHERAPPEARANCE.ParameterName = "@WEATHERAPPEARANCE";
                    WINDDIRECTION.ParameterName = "@WINDDIRECTION";
                    WINDFORCE.ParameterName = "@WINDFORCE";

                    PUBLISHDATE.Value = TBLZHCFORECAST.PUBLISHDATE.ToString();
                    FORECASTDATE.Value = TBLZHCFORECAST.FORECASTDATE.ToString();
                    SEAAREA.Value = TBLZHCFORECAST.SEAAREA;
                    WEATHERAPPEARANCE.Value = TBLZHCFORECAST.WEATHERAPPEARANCE;
                    WINDDIRECTION.Value = TBLZHCFORECAST.WINDDIRECTION;
                    WINDFORCE.Value = TBLZHCFORECAST.WINDFORCE;

                    parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, SEAAREA, WEATHERAPPEARANCE, WINDDIRECTION, WINDFORCE };
                }

                try
                {
                    return DataExe.GetIntExeData(sql, parameters);
                }
                catch (Exception ex)
                {
                    WriteLog.Write("新增指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                    return 0;
                }
           // }
        }

        /// <summary>
        /// 修改指挥处预报
        /// </summary>
        public int Edit_TBLZHCFORECAST(TBLZHCFORECAST TBLZHCFORECAST, string quanxian)
        {
            string sql = null;
            DbParameter[] parameters = null;
            if (quanxian.ToLower() == "fl")
            {

                    sql = "UPDATE  TBLZHCFORECAST set WEATHERAPPEARANCE=@WEATHERAPPEARANCE,WINDDIRECTION=@WINDDIRECTION,WINDFORCE=@WINDFORCE,WAVEHEIGHT =@WAVEHEIGHT,WAVEDIRECTION=@WAVEDIRECTION"
            + " where"
            + " PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')"
            + " and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')"
            + " and SEAAREA=@SEAAREA";

                    var PUBLISHDATE = DataExe.GetDbParameter();
                    var FORECASTDATE = DataExe.GetDbParameter();
                    var SEAAREA = DataExe.GetDbParameter();
                    var WEATHERAPPEARANCE = DataExe.GetDbParameter();
                    var WINDDIRECTION = DataExe.GetDbParameter();
                    var WINDFORCE = DataExe.GetDbParameter();
                    var WAVEHEIGHT = DataExe.GetDbParameter();
                    var WAVEDIRECTION = DataExe.GetDbParameter();


                    PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                    FORECASTDATE.ParameterName = "@FORECASTDATE";
                    SEAAREA.ParameterName = "@SEAAREA";
                    WEATHERAPPEARANCE.ParameterName = "@WEATHERAPPEARANCE";
                    WINDDIRECTION.ParameterName = "@WINDDIRECTION";
                    WINDFORCE.ParameterName = "@WINDFORCE";
                    WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";
                    WAVEDIRECTION.ParameterName = "@WAVEDIRECTION";


                    PUBLISHDATE.Value = TBLZHCFORECAST.PUBLISHDATE.ToString();
                    FORECASTDATE.Value = TBLZHCFORECAST.FORECASTDATE.ToString();
                    SEAAREA.Value = TBLZHCFORECAST.SEAAREA;
                    WEATHERAPPEARANCE.Value = TBLZHCFORECAST.WEATHERAPPEARANCE;
                    WINDDIRECTION.Value = TBLZHCFORECAST.WINDDIRECTION;
                    WINDFORCE.Value = TBLZHCFORECAST.WINDFORCE;
                    WAVEHEIGHT.Value = TBLZHCFORECAST.WAVEHEIGHT;
                    WAVEDIRECTION.Value = TBLZHCFORECAST.WAVEDIRECTION;


                    parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, SEAAREA, WEATHERAPPEARANCE, WINDDIRECTION, WINDFORCE, WAVEHEIGHT, WAVEDIRECTION };
                }
                
                try
                {
                    return DataExe.GetIntExeData(sql, parameters);
                }
                catch (Exception ex)
                {
                    WriteLog.Write("修改指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                    return 0;
                }


            
            
            //try
            //{
            //    return DataExe.GetIntExeData(sql, parameters);
            //}
            //catch (Exception ex)
            //{
            //    WriteLog.Write("修改指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            //    return 0;
            //}

        }

        /// <summary>
        /// 按照预报日期修改上午指挥处
        /// </summary>
        /// <param name="TBLZHCFORECAST"></param>
        /// <returns></returns>
        public int EditTBLZHCFORECAST(TBLZHCFORECAST TBLZHCFORECAST, string quanxian)
        {
            string sql = "";
            //if(quanxian == "sw")
            //{
            //    if(TBLZHCFORECAST.SEAAREA == "青岛市")
            //    {
            //        sql = "UPDATE "
            //        + " (select * from (select SEAAREA,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime , to_char(PUBLISHDATE,'yyyy-MM-dd HH24:mi:ss') as publishtime,WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION"
            //        + " from TBLZHCFORECAST )a "
            //        + " where forecasttime = '" + TBLZHCFORECAST.FORECASTDATE.ToString("yyyy-MM-dd HH:mm:ss") + "'  AND SEAAREA = '" + TBLZHCFORECAST.SEAAREA + "' and publishtime = '" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "') b "
            //        + "set WAVEDIRECTION = '" + TBLZHCFORECAST.WAVEDIRECTION + "'";
            //    }
            //    else
            //    {
            //        return 1;
            //    }
            //}
            // else
            if (quanxian == "fl")
            {
                //if (TBLZHCFORECAST.SEAAREA == "青岛市")
                //{
                //    sql = "UPDATE "
                //        + " (select * from (select SEAAREA,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime ,to_char(PUBLISHDATE,'yyyy-MM-dd HH24:mi:ss') as publishtime,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION,WEATHERAPPEARANCE"
                //        + " from TBLZHCFORECAST )a "
                //        + " where forecasttime = '" + TBLZHCFORECAST.FORECASTDATE.ToString("yyyy-MM-dd HH:mm:ss") + "'  AND SEAAREA = '" + TBLZHCFORECAST.SEAAREA + "' and publishtime = '" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "') b "
                //        + "set WINDDIRECTION = '" + TBLZHCFORECAST.WINDDIRECTION + "',WINDFORCE = '" + TBLZHCFORECAST.WINDFORCE + "',"
                //        + " WEATHERAPPEARANCE='" + TBLZHCFORECAST.WEATHERAPPEARANCE + "'";
                //}
                //else
                //{
                    sql = "UPDATE "
                        + " (select * from (select SEAAREA,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime ,to_char(PUBLISHDATE,'yyyy-MM-dd HH24:mi:ss') as publishtime,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION,WEATHERAPPEARANCE"
                        + " from TBLZHCFORECAST )a "
                        + " where forecasttime = '" + TBLZHCFORECAST.FORECASTDATE.ToString("yyyy-MM-dd HH:mm:ss") + "'  AND SEAAREA = '" + TBLZHCFORECAST.SEAAREA + "' and publishtime = '" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "') b "
                        + "set WINDDIRECTION = '" + TBLZHCFORECAST.WINDDIRECTION + "',WINDFORCE = '" + TBLZHCFORECAST.WINDFORCE + "',"
                        + "WAVEHEIGHT='" + TBLZHCFORECAST.WAVEHEIGHT + "',WAVEDIRECTION='" + TBLZHCFORECAST.WAVEDIRECTION + "',WEATHERAPPEARANCE='" + TBLZHCFORECAST.WEATHERAPPEARANCE + "'";
                //}

            }
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }
        public int EditTBLZHCFORECAST(TBLZHCFORECAST TBLZHCFORECAST)
        {
            string sql = "UPDATE "
                + " (select * from (select SEAAREA,to_char(forecastdate,'yyyy-MM-dd HH24:mi:ss') as forecasttime,WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION"
                + " from TBLZHCFORECAST )a "
                + " where forecasttime = '" + TBLZHCFORECAST.FORECASTDATE.ToString("yyyy-MM-dd HH:mm:ss") + "'  AND SEAAREA = '" + TBLZHCFORECAST.SEAAREA + "') b "
                + "set WEATHERAPPEARANCE = '" + TBLZHCFORECAST.WEATHERAPPEARANCE + "',WINDDIRECTION = '" + TBLZHCFORECAST.WINDDIRECTION + "',WINDFORCE = '" + TBLZHCFORECAST.WINDFORCE + "',"
                + "WAVEHEIGHT='" + TBLZHCFORECAST.WAVEHEIGHT + "',WAVEDIRECTION='" + TBLZHCFORECAST.WAVEDIRECTION + "'";
            //var FORECASTDATE = DataExe.GetDbParameter();
            //var SEAAREA = DataExe.GetDbParameter();
            //var WEATHERAPPEARANCE = DataExe.GetDbParameter();
            //var WINDDIRECTION = DataExe.GetDbParameter();
            //var WINDFORCE = DataExe.GetDbParameter();
            //var WAVEHEIGHT = DataExe.GetDbParameter();
            //var WAVEDIRECTION = DataExe.GetDbParameter();



            //FORECASTDATE.ParameterName = "@FORECASTDATE";
            //SEAAREA.ParameterName = "@SEAAREA";
            //WEATHERAPPEARANCE.ParameterName = "@WEATHERAPPEARANCE";
            //WINDDIRECTION.ParameterName = "@WINDDIRECTION";
            //WINDFORCE.ParameterName = "@WINDFORCE";
            //WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";
            //WAVEDIRECTION.ParameterName = "@WAVEDIRECTION";




            //FORECASTDATE.Value = TBLZHCFORECAST.FORECASTDATE.ToString("yyyy-MM-dd HH:mm:ss");
            //SEAAREA.Value = TBLZHCFORECAST.SEAAREA;
            //WEATHERAPPEARANCE.Value = TBLZHCFORECAST.WEATHERAPPEARANCE;
            //WINDDIRECTION.Value = TBLZHCFORECAST.WINDDIRECTION;
            //WINDFORCE.Value = TBLZHCFORECAST.WINDFORCE;
            //WAVEHEIGHT.Value = TBLZHCFORECAST.WAVEHEIGHT;
            //WAVEDIRECTION.Value = TBLZHCFORECAST.WAVEDIRECTION;


            //DbParameter[] parameters = { FORECASTDATE, SEAAREA, WEATHERAPPEARANCE, WINDDIRECTION, WINDFORCE, WAVEHEIGHT, WAVEDIRECTION };

            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }
        //*********************下午指挥处**********************
        //获取上午指挥处天气现象、风向、风力
        public object GetZHCMorning(TBLZHCFORECAST TBLZHCFORECAST)
        {
            try
            {
                string str = "select PUBLISHDATE,FORECASTDATE,SEAAREA,WEATHERAPPEARANCE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,WAVEDIRECTION from TBLZHCFORECAST where PUBLISHDATE = to_date('" + TBLZHCFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(str);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取指挥处预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }

}