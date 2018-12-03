using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    ///1129 sl
    /// <summary>
    /// 胜利油田周报
    /// </summary>
    public class sql_TBLSLYTWEEKFORECAST
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLSLYTWEEKFORECAST()
        {
            //
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
                                          //
        }
        /// <summary>
        /// 获取胜利油田水温周报
        /// </summary>
        /// <returns></returns>
        public object GETTBLSLYTWEEKFORECAST(TBLSDOFFSHORESEVENCITY24HWAVE TBLSDOFFSHORESEVENCITY24HWAVE)
        {

            try
            {
                return DataExe.GetTableExeData("select * from HT_TBLSLYTWEEKFORECAST where PUBLISHDATE=to_date('" + TBLSDOFFSHORESEVENCITY24HWAVE.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取胜利油田水温周报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 新增胜利油田水温周报
        /// </summary>
        /// <returns></returns>
        public int AddTBLSLYTWEEKFORECAST(TBLSDOFFSHORESEVENCITY24HWAVE TBLSDOFFSHORESEVENCITY24HWAVE)
        {

            string sql = "INSERT INTO  HT_TBLSLYTWEEKFORECAST (PUBLISHDATE,SDOSCWAREA,SDOSCWLOWESTWAVEHEIGHT,SDOSCWHIGHTESTWAVEHEIGHT,SDOSCWSURFACETEMPERATURE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@SDOSCWAREA,@SDOSCWLOWESTWAVEHEIGHT,@SDOSCWHIGHTESTWAVEHEIGHT,@SDOSCWSURFACETEMPERATURE)";



            var PUBLISHDATE = DataExe.GetDbParameter();
            var SDOSCWAREA = DataExe.GetDbParameter();
            var SDOSCWLOWESTWAVEHEIGHT = DataExe.GetDbParameter();
            var SDOSCWHIGHTESTWAVEHEIGHT = DataExe.GetDbParameter();
            var SDOSCWSURFACETEMPERATURE = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            SDOSCWAREA.ParameterName = "@SDOSCWAREA";
            SDOSCWLOWESTWAVEHEIGHT.ParameterName = "@SDOSCWLOWESTWAVEHEIGHT";
            SDOSCWHIGHTESTWAVEHEIGHT.ParameterName = "@SDOSCWHIGHTESTWAVEHEIGHT";
            SDOSCWSURFACETEMPERATURE.ParameterName = "@SDOSCWSURFACETEMPERATURE";




            PUBLISHDATE.Value = TBLSDOFFSHORESEVENCITY24HWAVE.PUBLISHDATE.ToString();
            SDOSCWAREA.Value = TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWAREA;
            SDOSCWLOWESTWAVEHEIGHT.Value = TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWLOWESTWAVEHEIGHT;
            SDOSCWHIGHTESTWAVEHEIGHT.Value = TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWHIGHTESTWAVEHEIGHT;
            SDOSCWSURFACETEMPERATURE.Value = TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWSURFACETEMPERATURE;


            DbParameter[] parameters = { PUBLISHDATE, SDOSCWAREA, SDOSCWLOWESTWAVEHEIGHT, SDOSCWHIGHTESTWAVEHEIGHT, SDOSCWSURFACETEMPERATURE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增胜利油田水温周报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改胜利油田水温周报
        /// </summary>
        public int EditTBLSLYTWEEKFORECAST(TBLSDOFFSHORESEVENCITY24HWAVE TBLSDOFFSHORESEVENCITY24HWAVE)
        {
            string sql = "UPDATE   HT_TBLSLYTWEEKFORECAST set	SDOSCWLOWESTWAVEHEIGHT=@SDOSCWLOWESTWAVEHEIGHT,SDOSCWHIGHTESTWAVEHEIGHT=@SDOSCWHIGHTESTWAVEHEIGHT,SDOSCWSURFACETEMPERATURE=@SDOSCWSURFACETEMPERATURE where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and SDOSCWAREA=@SDOSCWAREA";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var SDOSCWAREA = DataExe.GetDbParameter();
            var SDOSCWLOWESTWAVEHEIGHT = DataExe.GetDbParameter();
            var SDOSCWHIGHTESTWAVEHEIGHT = DataExe.GetDbParameter();
            var SDOSCWSURFACETEMPERATURE = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            SDOSCWAREA.ParameterName = "@SDOSCWAREA";
            SDOSCWLOWESTWAVEHEIGHT.ParameterName = "@SDOSCWLOWESTWAVEHEIGHT";
            SDOSCWHIGHTESTWAVEHEIGHT.ParameterName = "@SDOSCWHIGHTESTWAVEHEIGHT";
            SDOSCWSURFACETEMPERATURE.ParameterName = "@SDOSCWSURFACETEMPERATURE";




            PUBLISHDATE.Value = TBLSDOFFSHORESEVENCITY24HWAVE.PUBLISHDATE.ToString();
            SDOSCWAREA.Value = TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWAREA;
            SDOSCWLOWESTWAVEHEIGHT.Value = TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWLOWESTWAVEHEIGHT;
            SDOSCWHIGHTESTWAVEHEIGHT.Value = TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWHIGHTESTWAVEHEIGHT;
            SDOSCWSURFACETEMPERATURE.Value = TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWSURFACETEMPERATURE;


            DbParameter[] parameters = { PUBLISHDATE, SDOSCWAREA, SDOSCWLOWESTWAVEHEIGHT, SDOSCWHIGHTESTWAVEHEIGHT, SDOSCWSURFACETEMPERATURE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改胜利油田水温周报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }
    }
}