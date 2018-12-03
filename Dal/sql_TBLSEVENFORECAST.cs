using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_TBLSEVENFORECAST
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLSEVENFORECAST()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        #region 查询

        /// <summary>
        /// 获取潮汐数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public object GetTideData(string forecastArea, TBLSEVENTIDE model)
        {
            try
            {
                string sql = "SELECT * FROM  TBLREFINETIDE  WHERE PUBLISHDATE = to_date('" + model.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA = '" + forecastArea + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取海浪数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public object GetWaveData(string forecastArea, TBLSEVENWAVE model)
        {
            try
            {
                string sql = "SELECT * FROM TBLREFINEWAVE WHERE PUBLISHDATE = to_date('" + model.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')  AND FORECASTAREA = '" + forecastArea + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取海温数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public object GetTemperatureData(string forecastArea, TBLSEVENTEMPERATURE model)
        {
            try
            {
                string sql = "SELECT * FROM TBLREFINEWATERTEMPERATURE WHERE PUBLISHDATE = to_date('" + model.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')  AND FORECASTAREA = '" + forecastArea + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        ///从海洋牧场解析数据中获取海温数据
        /// </summary>
        /// <returns></returns>
        public object GetTemperatureDataFormOCEAN(string forecastArea, TBLSEVENTEMPERATURE model)
        {
            try
            {
                //string sql = "SELECT A.FORECASTDATE AS PUBLISHDATE, A.temp48h AS Max48,A.temp72h AS Max72, A.temp96h AS Max96, B.temp48h AS Min48, B.temp72h AS Min72, B.temp96h as Min96 FROM OCEANRANCH72HTEMPERATURE_S A join OCEANRANCH72HTEMPERATURE_S B  on A.SN= B.SN WHERE A.FORECASTDATE = to_date('" + model.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')  AND A.oceanranchname='"+ forecastArea + "最大值' and B.oceanranchname='"+ forecastArea + "最小值'";
                string sql = "SELECT A.FORECASTDATE AS PUBLISHDATE, A.temp48h AS Max48,A.temp72h AS Max72, A.temp96h AS Max96, B.temp48h AS Min48, B.temp72h AS Min72, B.temp96h as Min96 FROM OCEANRANCH72HTEMPERATURE_S A join (select * from OCEANRANCH72HTEMPERATURE_S WHERE  oceanranchname='" + forecastArea + "最小值' and FORECASTDATE = to_date('" + model.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')) B  on A.SN= B.SN WHERE A.FORECASTDATE = to_date('" + model.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')  AND A.oceanranchname='" + forecastArea + "最大值'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region  插入

        /// <summary>
        /// 插入潮汐数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddTideData(TBLSEVENTIDE model)
        {
            try
            {
                string sql = "INSERT INTO TBLREFINETIDE "
                           + " (PUBLISHDATE,FORECASTDATE,FIRSTHIGHTIME,FIRSTHIGHLEVEL,FIRSTLOWTIME,FIRSTLOWLEVEL,SECONDHIGHTIME,SECONDHIGHLEVEL,SECONDLOWTIME,SECONDLOWLEVEL,FORECASTAREA)"
                           + " VALUES"
                           + " (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@FIRSTHIGHTIME,@FIRSTHIGHLEVEL,@FIRSTLOWTIME,@FIRSTLOWLEVEL,@SECONDHIGHTIME,@SECONDHIGHLEVEL,@SECONDLOWTIME,@SECONDLOWLEVEL,@FORECASTAREA)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var FIRSTHIGHTIME = DataExe.GetDbParameter();
                var FIRSTHIGHLEVEL = DataExe.GetDbParameter();
                var FIRSTLOWTIME = DataExe.GetDbParameter();
                var FIRSTLOWLEVEL = DataExe.GetDbParameter();
                var SECONDHIGHTIME = DataExe.GetDbParameter();
                var SECONDHIGHLEVEL = DataExe.GetDbParameter();
                var SECONDLOWTIME = DataExe.GetDbParameter();
                var SECONDLOWLEVEL = DataExe.GetDbParameter();
                var FORECASTAREA = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                FIRSTHIGHTIME.ParameterName = "@FIRSTHIGHTIME";
                FIRSTHIGHLEVEL.ParameterName = "@FIRSTHIGHLEVEL";
                FIRSTLOWTIME.ParameterName = "@FIRSTLOWTIME";
                FIRSTLOWLEVEL.ParameterName = "@FIRSTLOWLEVEL";
                SECONDHIGHTIME.ParameterName = "@SECONDHIGHTIME";
                SECONDHIGHLEVEL.ParameterName = "@SECONDHIGHLEVEL";
                SECONDLOWTIME.ParameterName = "@SECONDLOWTIME";
                SECONDLOWLEVEL.ParameterName = "@SECONDLOWLEVEL";
                FORECASTAREA.ParameterName = "@FORECASTAREA";

                PUBLISHDATE.Value = model.PUBLISHDATE.ToString();
                FORECASTDATE.Value = model.FORECASTDATE.ToString();
                FIRSTHIGHTIME.Value = model.FIRSTHIGHTIME;
                FIRSTHIGHLEVEL.Value = model.FIRSTHIGHLEVEL;
                FIRSTLOWTIME.Value = model.FIRSTLOWTIME;
                FIRSTLOWLEVEL.Value = model.FIRSTLOWLEVEL;
                SECONDHIGHTIME.Value = model.SECONDHIGHTIME;
                SECONDHIGHLEVEL.Value = model.SECONDHIGHLEVEL;
                SECONDLOWTIME.Value = model.SECONDLOWTIME;
                SECONDLOWLEVEL.Value = model.SECONDLOWLEVEL;
                FORECASTAREA.Value = model.FORECASTAREA;

                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, FIRSTHIGHTIME, FIRSTHIGHLEVEL, FIRSTLOWTIME, FIRSTLOWLEVEL, SECONDHIGHTIME, SECONDHIGHLEVEL, SECONDLOWTIME, SECONDLOWLEVEL, FORECASTAREA };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("插入" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 插入海浪数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddWaveData(TBLSEVENWAVE model)
        {
            try
            {
                string sql = "INSERT INTO TBLREFINEWAVE "
                           + " (PUBLISHDATE,FORECASTDATE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT,FORECASTAREA)"
                           + " VALUES"
                           + " (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@WINDDIRECTION,@WINDFORCE,@WAVEHEIGHT,@FORECASTAREA)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var WINDDIRECTION = DataExe.GetDbParameter();
                var WINDFORCE = DataExe.GetDbParameter();
                var WAVEHEIGHT = DataExe.GetDbParameter();
                var FORECASTAREA = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                WINDDIRECTION.ParameterName = "@WINDDIRECTION";
                WINDFORCE.ParameterName = "@WINDFORCE";
                WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";
                FORECASTAREA.ParameterName = "@FORECASTAREA";

                PUBLISHDATE.Value = model.PUBLISHDATE.ToString();
                FORECASTDATE.Value = model.FORECASTDATE.ToString();
                WINDDIRECTION.Value = model.WINDDIRECTION;
                WINDFORCE.Value = model.WINDFORCE;
                WAVEHEIGHT.Value = model.WAVEHEIGHT;
                FORECASTAREA.Value = model.FORECASTAREA;

                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, WINDDIRECTION, WINDFORCE, WAVEHEIGHT, FORECASTAREA };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("插入" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 插入海温数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddTemperatureData(TBLSEVENTEMPERATURE model)
        {
            try
            {
                string sql = "INSERT INTO TBLREFINEWATERTEMPERATURE "
                           + " (PUBLISHDATE,FORECASTDATE,WATERTEMPERATURE,FORECASTAREA)"
                           + " VALUES"
                           + " (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@TEMPERATURE,@FORECASTAREA)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var TEMPERATURE = DataExe.GetDbParameter();
                var FORECASTAREA = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                TEMPERATURE.ParameterName = "@TEMPERATURE";
                FORECASTAREA.ParameterName = "@FORECASTAREA";

                PUBLISHDATE.Value = model.PUBLISHDATE.ToString();
                FORECASTDATE.Value = model.FORECASTDATE.ToString();
                TEMPERATURE.Value = model.TEMPERATURE;
                FORECASTAREA.Value = model.FORECASTAREA;

                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, TEMPERATURE, FORECASTAREA };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("插入" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        #endregion

        #region  修改

        /// <summary>
        /// 修改潮汐数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditTideData(TBLSEVENTIDE model)
        {
            try
            {
                string sql = "UPDATE TBLREFINETIDE" +
                             " SET FIRSTHIGHTIME=@FIRSTHIGHTIME, FIRSTHIGHLEVEL=@FIRSTHIGHLEVEL, FIRSTLOWTIME=@FIRSTLOWTIME, FIRSTLOWLEVEL=@FIRSTLOWLEVEL, SECONDHIGHTIME=@SECONDHIGHTIME, SECONDHIGHLEVEL=@SECONDHIGHLEVEL, SECONDLOWTIME=@SECONDLOWTIME, SECONDLOWLEVEL=@SECONDLOWLEVEL WHERE  PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') AND FORECASTDATE = to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA = @FORECASTAREA";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var FIRSTHIGHTIME = DataExe.GetDbParameter();
                var FIRSTHIGHLEVEL = DataExe.GetDbParameter();
                var FIRSTLOWTIME = DataExe.GetDbParameter();
                var FIRSTLOWLEVEL = DataExe.GetDbParameter();
                var SECONDHIGHTIME = DataExe.GetDbParameter();
                var SECONDHIGHLEVEL = DataExe.GetDbParameter();
                var SECONDLOWTIME = DataExe.GetDbParameter();
                var SECONDLOWLEVEL = DataExe.GetDbParameter();
                var FORECASTAREA = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                FIRSTHIGHTIME.ParameterName = "@FIRSTHIGHTIME";
                FIRSTHIGHLEVEL.ParameterName = "@FIRSTHIGHLEVEL";
                FIRSTLOWTIME.ParameterName = "@FIRSTLOWTIME";
                FIRSTLOWLEVEL.ParameterName = "@FIRSTLOWLEVEL";
                SECONDHIGHTIME.ParameterName = "@SECONDHIGHTIME";
                SECONDHIGHLEVEL.ParameterName = "@SECONDHIGHLEVEL";
                SECONDLOWTIME.ParameterName = "@SECONDLOWTIME";
                SECONDLOWLEVEL.ParameterName = "@SECONDLOWLEVEL";
                FORECASTAREA.ParameterName = "@FORECASTAREA";

                PUBLISHDATE.Value = model.PUBLISHDATE.ToString();
                FORECASTDATE.Value = model.FORECASTDATE.ToString();
                FIRSTHIGHTIME.Value = model.FIRSTHIGHTIME;
                FIRSTHIGHLEVEL.Value = model.FIRSTHIGHLEVEL;
                FIRSTLOWTIME.Value = model.FIRSTLOWTIME;
                FIRSTLOWLEVEL.Value = model.FIRSTLOWLEVEL;
                SECONDHIGHTIME.Value = model.SECONDHIGHTIME;
                SECONDHIGHLEVEL.Value = model.SECONDHIGHLEVEL;
                SECONDLOWTIME.Value = model.SECONDLOWTIME;
                SECONDLOWLEVEL.Value = model.SECONDLOWLEVEL;
                FORECASTAREA.Value = model.FORECASTAREA;

                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, FIRSTHIGHTIME, FIRSTHIGHLEVEL, FIRSTLOWTIME, FIRSTLOWLEVEL, SECONDHIGHTIME, SECONDHIGHLEVEL, SECONDLOWTIME, SECONDLOWLEVEL, FORECASTAREA };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改海浪数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditWaveData(TBLSEVENWAVE model)
        {
            try
            {
                string sql = "UPDATE TBLREFINEWAVE"
                           + " SET WINDDIRECTION=@WINDDIRECTION, WINDFORCE=@WINDFORCE, WAVEHEIGHT=@WAVEHEIGHT WHERE  PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') AND FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')  AND FORECASTAREA = @FORECASTAREA";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var WINDDIRECTION = DataExe.GetDbParameter();
                var WINDFORCE = DataExe.GetDbParameter();
                var WAVEHEIGHT = DataExe.GetDbParameter();
                var FORECASTAREA = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                WINDDIRECTION.ParameterName = "@WINDDIRECTION";
                WINDFORCE.ParameterName = "@WINDFORCE";
                WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";
                FORECASTAREA.ParameterName = "@FORECASTAREA";

                PUBLISHDATE.Value = model.PUBLISHDATE.ToString();
                FORECASTDATE.Value = model.FORECASTDATE.ToString();
                WINDDIRECTION.Value = model.WINDDIRECTION;
                WINDFORCE.Value = model.WINDFORCE;
                WAVEHEIGHT.Value = model.WAVEHEIGHT;
                FORECASTAREA.Value = model.FORECASTAREA;

                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, WINDDIRECTION, WINDFORCE, WAVEHEIGHT, FORECASTAREA };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改海温数据
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditTemperatureData(TBLSEVENTEMPERATURE model)
        {
            try
            {
                string sql = "UPDATE TBLREFINEWATERTEMPERATURE "
                           + " SET WATERTEMPERATURE=@TEMPERATURE WHERE  PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') AND FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')  AND FORECASTAREA = @FORECASTAREA";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var TEMPERATURE = DataExe.GetDbParameter();
                var FORECASTAREA = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                TEMPERATURE.ParameterName = "@TEMPERATURE";
                FORECASTAREA.ParameterName = "@FORECASTAREA";

                PUBLISHDATE.Value = model.PUBLISHDATE.ToString();
                FORECASTDATE.Value = model.FORECASTDATE.ToString();
                TEMPERATURE.Value = model.TEMPERATURE;
                FORECASTAREA.Value = model.FORECASTAREA;

                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, TEMPERATURE, FORECASTAREA };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改" + model.FORECASTAREA + "潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        #endregion
    }
}