using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using PredicTable.Model;
using System.Text;

namespace PredicTable.Dal
{
    /// <summary>
    /// 上合峰会专项预报单风浪水温 修改提交后
    /// </summary>
    public class sql_SCOTableList
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_SCOTableList()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        #region 近/外海风浪数据

        #region 近/外海风浪数据获取
        /// <summary>
        /// 获取近海风数据（源数据）
        /// </summary>
        /// <returns></returns>
        public object GetTableOffShoreWind_S(DateTime date)
        {

            try
            {
                string sql = "select * from TBLWINDFORECAST_SH where PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='近海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取近海风源信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取近海风数据（预报修改后）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOffShoreWind(DateTime date)
        {
            try
            {
                string sql = "select * from TBL_WINDFORECAST_SH where PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='近海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取近海风信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取外海风数据（源数据）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOffOnShoreWind_S(DateTime date)
        {
            try
            {
                string sql = "select * from TBLWINDFORECAST_SH where PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='外海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取外海风浪信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取外海风数据（预报修改后）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOffOnShoreWind(DateTime date)
        {
            try
            {
                string sql = "select * from TBL_WINDFORECAST_SH where PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='外海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取外海风信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取近海浪（源数据）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOffShoreWave_S(DateTime date)
        {
            try
            {
                string sql = "SELECT * FROM TBLWAVEFORECAST_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='近海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取近海浪源信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取外海浪（源数据）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOpenShoreWave_S(DateTime date)
        {
            try
            {
                string sql = "SELECT * FROM TBLWAVEFORECAST_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='外海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取外海浪源信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        ///获取近海浪（预报修改后）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOffShoreWave(DateTime date)
        {
            try
            {
                string sql = "SELECT * FROM TBL_WAVEFORECAST_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='近海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取近海浪信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取外海浪（预报修改后）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOpenShoreWave(DateTime date)
        {
            try
            {
                string sql = "SELECT * FROM TBL_WAVEFORECAST_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='外海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取外海浪信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region 近/外海风浪数据添加、修改
        /// <summary>
        /// 添加风数据到数据库
        /// </summary>
        /// <param name="date"></param>
        /// <param name="ScoModel"></param>
        /// <returns></returns>
        public int AddTableOffShoreWind(DateTime date, SCOFLModel ScoModel)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("INSERT INTO TBL_WINDFORECAST_SH(PUBLISHDATE,FORECASTAREA,WINDFORCE00H,WINDFORCE01H,WINDFORCE02H,WINDFORCE03H,WINDFORCE04H,WINDFORCE05H,WINDFORCE06H,WINDFORCE07H,WINDFORCE08H,WINDFORCE09H,WINDFORCE10H,WINDFORCE11H,WINDFORCE12H,WINDFORCE13H,WINDFORCE14H,WINDFORCE15H,WINDFORCE16H,WINDFORCE17H,WINDFORCE18H,WINDFORCE19H,WINDFORCE20H,WINDFORCE21H,WINDFORCE22H,WINDFORCE23H,WINDFORCE24H,WINDFORCE25H,WINDFORCE26H,WINDFORCE27H,WINDFORCE28H,WINDFORCE29H,WINDFORCE30H,WINDFORCE31H,");
                sql.Append("WINDDIRECTION00H,WINDDIRECTION01H,WINDDIRECTION02H,WINDDIRECTION03H,WINDDIRECTION04H,WINDDIRECTION05H,WINDDIRECTION06H,WINDDIRECTION07H,WINDDIRECTION08H,WINDDIRECTION09H,WINDDIRECTION10H,WINDDIRECTION11H,WINDDIRECTION12H,WINDDIRECTION13H,WINDDIRECTION14H,WINDDIRECTION15H,WINDDIRECTION16H,WINDDIRECTION17H,WINDDIRECTION18H,WINDDIRECTION19H,WINDDIRECTION20H,WINDDIRECTION21H,WINDDIRECTION22H,WINDDIRECTION23H,WINDDIRECTION24H,WINDDIRECTION25H,WINDDIRECTION26H,WINDDIRECTION27H,WINDDIRECTION28H,WINDDIRECTION29H,WINDDIRECTION30H,WINDDIRECTION31H,WEATHER00D00H,WEATHER00D01H,WEATHER01D00H,WEATHER01D01H,WEATHER02D00H,WEATHER02D01H)");
                sql.Append(" VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@FORECASTAREA,@WINDFORCE00H,@WINDFORCE01H,@WINDFORCE02H,@WINDFORCE03H,@WINDFORCE04H,@WINDFORCE05H,@WINDFORCE06H,@WINDFORCE07H,@WINDFORCE08H,@WINDFORCE09H,@WINDFORCE10H,@WINDFORCE11H,@WINDFORCE12H,@WINDFORCE13H,@WINDFORCE14H,@WINDFORCE15H,@WINDFORCE16H,@WINDFORCE17H,@WINDFORCE18H,@WINDFORCE19H,@WINDFORCE20H,@WINDFORCE21H,@WINDFORCE22H,@WINDFORCE23H,@WINDFORCE24H,@WINDFORCE25H,@WINDFORCE26H,@WINDFORCE27H,@WINDFORCE28H,@WINDFORCE29H,@WINDFORCE30H,@WINDFORCE31H,");
                sql.Append("@WINDDIRECTION00H,@WINDDIRECTION01H,@WINDDIRECTION02H,@WINDDIRECTION03H,@WINDDIRECTION04H,@WINDDIRECTION05H,@WINDDIRECTION06H,@WINDDIRECTION07H,@WINDDIRECTION08H,@WINDDIRECTION09H,@WINDDIRECTION10H,@WINDDIRECTION11H,@WINDDIRECTION12H,@WINDDIRECTION13H,@WINDDIRECTION14H,@WINDDIRECTION15H,@WINDDIRECTION16H,@WINDDIRECTION17H,@WINDDIRECTION18H,@WINDDIRECTION19H,@WINDDIRECTION20H,@WINDDIRECTION21H,@WINDDIRECTION22H,@WINDDIRECTION23H,@WINDDIRECTION24H,@WINDDIRECTION25H,@WINDDIRECTION26H,@WINDDIRECTION27H,@WINDDIRECTION28H,@WINDDIRECTION29H,@WINDDIRECTION30H,@WINDDIRECTION31H,@WEATHER00D00H,@WEATHER00D01H,@WEATHER01D00H,@WEATHER01D01H,@WEATHER02D00H,@WEATHER02D01H)");
                DbParameter[] parameters = SetOffShoreWindParam(date,ScoModel);
                return DataExe.GetIntExeData(sql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加"+ ScoModel .FORECASTAREA+ "风数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 编辑风数据到数据库
        /// </summary>
        /// <param name="date"></param>
        /// <param name="ScoModel"></param>
        /// <returns></returns>
        public int EditTableOffShoreWind(DateTime date, SCOFLModel ScoModel) {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE TBL_WINDFORECAST_SH SET WINDFORCE00H=@WINDFORCE00H,WINDFORCE01H=@WINDFORCE01H，WINDFORCE02H=@WINDFORCE02H,WINDFORCE03H=@WINDFORCE03H,WINDFORCE04H=@WINDFORCE04H,WINDFORCE05H=@WINDFORCE05H,WINDFORCE06H=@WINDFORCE06H,WINDFORCE07H=@WINDFORCE07H,WINDFORCE08H=@WINDFORCE08H,WINDFORCE09H=@WINDFORCE09H,WINDFORCE10H=@WINDFORCE10H,WINDFORCE11H=@WINDFORCE11H,WINDFORCE12H=@WINDFORCE12H,WINDFORCE13H=@WINDFORCE13H,WINDFORCE14H=@WINDFORCE14H,WINDFORCE15H=@WINDFORCE15H,WINDFORCE16H=@WINDFORCE16H,WINDFORCE17H=@WINDFORCE17H,WINDFORCE18H=@WINDFORCE18H,WINDFORCE19H=@WINDFORCE19H,WINDFORCE20H=@WINDFORCE20H,WINDFORCE21H=@WINDFORCE21H,WINDFORCE22H=@WINDFORCE22H,WINDFORCE23H=@WINDFORCE23H,WINDFORCE24H=@WINDFORCE24H,WINDFORCE25H=@WINDFORCE25H,WINDFORCE26H=@WINDFORCE26H,WINDFORCE27H=@WINDFORCE27H,WINDFORCE28H=@WINDFORCE28H,WINDFORCE29H=@WINDFORCE29H,WINDFORCE30H=@WINDFORCE30H,WINDFORCE31H=@WINDFORCE31H,");
                sql.Append("WINDDIRECTION00H=@WINDDIRECTION00H,WINDDIRECTION01H=@WINDDIRECTION01H,WINDDIRECTION02H=@WINDDIRECTION02H,WINDDIRECTION03H=@WINDDIRECTION03H,WINDDIRECTION04H=@WINDDIRECTION04H,WINDDIRECTION05H=@WINDDIRECTION05H,WINDDIRECTION06H=@WINDDIRECTION06H,WINDDIRECTION07H=@WINDDIRECTION07H,WINDDIRECTION08H=@WINDDIRECTION08H,WINDDIRECTION09H=@WINDDIRECTION09H,WINDDIRECTION10H=@WINDDIRECTION10H,WINDDIRECTION11H=@WINDDIRECTION11H,WINDDIRECTION12H=@WINDDIRECTION12H,WINDDIRECTION13H=@WINDDIRECTION13H,WINDDIRECTION14H=@WINDDIRECTION14H,WINDDIRECTION15H=@WINDDIRECTION15H,WINDDIRECTION16H=@WINDDIRECTION16H,WINDDIRECTION17H=@WINDDIRECTION17H,WINDDIRECTION18H=@WINDDIRECTION18H,WINDDIRECTION19H=@WINDDIRECTION19H,WINDDIRECTION20H=@WINDDIRECTION20H,WINDDIRECTION21H=@WINDDIRECTION21H,WINDDIRECTION22H=@WINDDIRECTION22H,WINDDIRECTION23H=@WINDDIRECTION23H,WINDDIRECTION24H=@WINDDIRECTION24H,WINDDIRECTION25H=@WINDDIRECTION25H,WINDDIRECTION26H=@WINDDIRECTION26H,WINDDIRECTION27H=@WINDDIRECTION27H,WINDDIRECTION28H=@WINDDIRECTION28H,WINDDIRECTION29H=@WINDDIRECTION29H,WINDDIRECTION30H=@WINDDIRECTION30H,WINDDIRECTION31H=@WINDDIRECTION31H,WEATHER00D00H=@WEATHER00D00H,WEATHER00D01H=@WEATHER00D01H,WEATHER01D00H=@WEATHER01D00H,WEATHER01D01H=@WEATHER01D01H,WEATHER02D00H=@WEATHER02D00H,WEATHER02D01H=@WEATHER02D01H");
                sql.Append(" WHERE PUBLISHDATE = to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA=@FORECASTAREA");
                DbParameter[] parameters = SetOffShoreWindParam(date, ScoModel);
                return DataExe.GetIntExeData(sql.ToString(), parameters);
            }
            catch (Exception ex) {
                WriteLog.Write("编辑"+ ScoModel.FORECASTAREA+ "风数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 添加海浪数据（近、外）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="WaveModel"></param>
        /// <returns></returns>
        public int AddTableOffAndOnWave(DateTime date,SCOWaveModel WaveModel)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("INSERT INTO TBL_WAVEFORECAST_SH(PUBLISHDATE,FORECASTAREA,WAVEFORCE00H,WAVEFORCE01H,WAVEFORCE02H,WAVEFORCE03H,WAVEFORCE04H,WAVEFORCE05H,WAVEFORCE06H,WAVEFORCE07H,WAVEFORCE08H,WAVEFORCE09H,WAVEFORCE10H,WAVEFORCE11H,WAVEFORCE12H,WAVEFORCE13H,WAVEFORCE14H,WAVEFORCE15H,WAVEFORCE16H,WAVEFORCE17H,WAVEFORCE18H,WAVEFORCE19H,WAVEFORCE20H,WAVEFORCE21H,WAVEFORCE22H,WAVEFORCE23H,WAVEFORCE24H,WAVEFORCE25H,WAVEFORCE26H,WAVEFORCE27H,WAVEFORCE28H,WAVEFORCE29H,WAVEFORCE30H,WAVEFORCE31H,");
                sql.Append("WAVEDIRECTION00H,WAVEDIRECTION01H,WAVEDIRECTION02H,WAVEDIRECTION03H,WAVEDIRECTION04H,WAVEDIRECTION05H,WAVEDIRECTION06H,WAVEDIRECTION07H,WAVEDIRECTION08H,WAVEDIRECTION09H,WAVEDIRECTION10H,WAVEDIRECTION11H,WAVEDIRECTION12H,WAVEDIRECTION13H,WAVEDIRECTION14H,WAVEDIRECTION15H,WAVEDIRECTION16H,WAVEDIRECTION17H,WAVEDIRECTION18H,WAVEDIRECTION19H,WAVEDIRECTION20H,WAVEDIRECTION21H,WAVEDIRECTION22H,WAVEDIRECTION23H,WAVEDIRECTION24H,WAVEDIRECTION25H,WAVEDIRECTION26H,WAVEDIRECTION27H,WAVEDIRECTION28H,WAVEDIRECTION29H,WAVEDIRECTION30H,WAVEDIRECTION31H)");
                sql.Append(" VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@FORECASTAREA,@WAVEFORCE00H,@WAVEFORCE01H,@WAVEFORCE02H,@WAVEFORCE03H,@WAVEFORCE04H,@WAVEFORCE05H,@WAVEFORCE06H,@WAVEFORCE07H,@WAVEFORCE08H,@WAVEFORCE09H,@WAVEFORCE10H,@WAVEFORCE11H,@WAVEFORCE12H,@WAVEFORCE13H,@WAVEFORCE14H,@WAVEFORCE15H,@WAVEFORCE16H,@WAVEFORCE17H,@WAVEFORCE18H,@WAVEFORCE19H,@WAVEFORCE20H,@WAVEFORCE21H,@WAVEFORCE22H,@WAVEFORCE23H,@WAVEFORCE24H,@WAVEFORCE25H,@WAVEFORCE26H,@WAVEFORCE27H,@WAVEFORCE28H,@WAVEFORCE29H,@WAVEFORCE30H,@WAVEFORCE31H,");
                sql.Append("@WAVEDIRECTION00H,@WAVEDIRECTION01H,@WAVEDIRECTION02H,@WAVEDIRECTION03H,@WAVEDIRECTION04H,@WAVEDIRECTION05H,@WAVEDIRECTION06H,@WAVEDIRECTION07H,@WAVEDIRECTION08H,@WAVEDIRECTION09H,@WAVEDIRECTION10H,@WAVEDIRECTION11H,@WAVEDIRECTION12H,@WAVEDIRECTION13H,@WAVEDIRECTION14H,@WAVEDIRECTION15H,@WAVEDIRECTION16H,@WAVEDIRECTION17H,@WAVEDIRECTION18H,@WAVEDIRECTION19H,@WAVEDIRECTION20H,@WAVEDIRECTION21H,@WAVEDIRECTION22H,@WAVEDIRECTION23H,@WAVEDIRECTION24H,@WAVEDIRECTION25H,@WAVEDIRECTION26H,@WAVEDIRECTION27H,@WAVEDIRECTION28H,@WAVEDIRECTION29H,@WAVEDIRECTION30H,@WAVEDIRECTION31H)");
                DbParameter[] parameters = SetWaveParam(date,WaveModel);
                return DataExe.GetIntExeData(sql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加"+ WaveModel .FORECASTAREA+ "浪数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改海浪数据（近、外）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="WaveModel"></param>
        /// <returns></returns>
        public int EditTableOffAndOnWave(DateTime date, SCOWaveModel WaveModel) {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE TBL_WAVEFORECAST_SH SET WAVEFORCE00H=@WAVEFORCE00H,WAVEFORCE01H=@WAVEFORCE01H，WAVEFORCE02H=@WAVEFORCE02H,WAVEFORCE03H=@WAVEFORCE03H,WAVEFORCE04H=@WAVEFORCE04H,WAVEFORCE05H=@WAVEFORCE05H,WAVEFORCE06H=@WAVEFORCE06H,WAVEFORCE07H=@WAVEFORCE07H,WAVEFORCE08H=@WAVEFORCE08H,WAVEFORCE09H=@WAVEFORCE09H,WAVEFORCE10H=@WAVEFORCE10H,WAVEFORCE11H=@WAVEFORCE11H,WAVEFORCE12H=@WAVEFORCE12H,WAVEFORCE13H=@WAVEFORCE13H,WAVEFORCE14H=@WAVEFORCE14H,WAVEFORCE15H=@WAVEFORCE15H,WAVEFORCE16H=@WAVEFORCE16H,WAVEFORCE17H=@WAVEFORCE17H,WAVEFORCE18H=@WAVEFORCE18H,WAVEFORCE19H=@WAVEFORCE19H,WAVEFORCE20H=@WAVEFORCE20H,WAVEFORCE21H=@WAVEFORCE21H,WAVEFORCE22H=@WAVEFORCE22H,WAVEFORCE23H=@WAVEFORCE23H,WAVEFORCE24H=@WAVEFORCE24H,WAVEFORCE25H=@WAVEFORCE25H,WAVEFORCE26H=@WAVEFORCE26H,WAVEFORCE27H=@WAVEFORCE27H,WAVEFORCE28H=@WAVEFORCE28H,WAVEFORCE29H=@WAVEFORCE29H,WAVEFORCE30H=@WAVEFORCE30H,WAVEFORCE31H=@WAVEFORCE31H,");
                sql.Append("WAVEDIRECTION00H=@WAVEDIRECTION00H,WAVEDIRECTION01H=@WAVEDIRECTION01H,WAVEDIRECTION02H=@WAVEDIRECTION02H,WAVEDIRECTION03H=@WAVEDIRECTION03H,WAVEDIRECTION04H=@WAVEDIRECTION04H,WAVEDIRECTION05H=@WAVEDIRECTION05H,WAVEDIRECTION06H=@WAVEDIRECTION06H,WAVEDIRECTION07H=@WAVEDIRECTION07H,WAVEDIRECTION08H=@WAVEDIRECTION08H,WAVEDIRECTION09H=@WAVEDIRECTION09H,WAVEDIRECTION10H=@WAVEDIRECTION10H,WAVEDIRECTION11H=@WAVEDIRECTION11H,WAVEDIRECTION12H=@WAVEDIRECTION12H,WAVEDIRECTION13H=@WAVEDIRECTION13H,WAVEDIRECTION14H=@WAVEDIRECTION14H,WAVEDIRECTION15H=@WAVEDIRECTION15H,WAVEDIRECTION16H=@WAVEDIRECTION16H,WAVEDIRECTION17H=@WAVEDIRECTION17H,WAVEDIRECTION18H=@WAVEDIRECTION18H,WAVEDIRECTION19H=@WAVEDIRECTION19H,WAVEDIRECTION20H=@WAVEDIRECTION20H,WAVEDIRECTION21H=@WAVEDIRECTION21H,WAVEDIRECTION22H=@WAVEDIRECTION22H,WAVEDIRECTION23H=@WAVEDIRECTION23H,WAVEDIRECTION24H=@WAVEDIRECTION24H,WAVEDIRECTION25H=@WAVEDIRECTION25H,WAVEDIRECTION26H=@WAVEDIRECTION26H,WAVEDIRECTION27H=@WAVEDIRECTION27H,WAVEDIRECTION28H=@WAVEDIRECTION28H,WAVEDIRECTION29H=@WAVEDIRECTION29H,WAVEDIRECTION30H=@WAVEDIRECTION30H,WAVEDIRECTION31H=@WAVEDIRECTION31H");
                sql.Append(" WHERE PUBLISHDATE=to_date(@PUBLISHDATE, 'yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA=@FORECASTAREA");
                DbParameter[] parameters = SetWaveParam(date,WaveModel);
                return DataExe.GetIntExeData(sql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("编辑" + WaveModel.FORECASTAREA + "风数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 生成风的参数数组
        /// </summary>
        /// <param name="ScoModel"></param>
        /// <returns></returns>
        private DbParameter[] SetOffShoreWindParam(DateTime date,SCOFLModel ScoModel)
        {
            #region 
            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTAREA = DataExe.GetDbParameter();
            var WINDFORCE00H = DataExe.GetDbParameter();
            var WINDFORCE01H = DataExe.GetDbParameter();
            var WINDFORCE02H = DataExe.GetDbParameter();
            var WINDFORCE03H = DataExe.GetDbParameter();
            var WINDFORCE04H = DataExe.GetDbParameter();
            var WINDFORCE05H = DataExe.GetDbParameter();
            var WINDFORCE06H = DataExe.GetDbParameter();
            var WINDFORCE07H = DataExe.GetDbParameter();
            var WINDFORCE08H = DataExe.GetDbParameter();
            var WINDFORCE09H = DataExe.GetDbParameter();
            var WINDFORCE10H = DataExe.GetDbParameter();
            var WINDFORCE11H = DataExe.GetDbParameter();
            var WINDFORCE12H = DataExe.GetDbParameter();
            var WINDFORCE13H = DataExe.GetDbParameter();
            var WINDFORCE14H = DataExe.GetDbParameter();
            var WINDFORCE15H = DataExe.GetDbParameter();
            var WINDFORCE16H = DataExe.GetDbParameter();
            var WINDFORCE17H = DataExe.GetDbParameter();
            var WINDFORCE18H = DataExe.GetDbParameter();
            var WINDFORCE19H = DataExe.GetDbParameter();
            var WINDFORCE20H = DataExe.GetDbParameter();
            var WINDFORCE21H = DataExe.GetDbParameter();
            var WINDFORCE22H = DataExe.GetDbParameter();
            var WINDFORCE23H = DataExe.GetDbParameter();
            var WINDFORCE24H = DataExe.GetDbParameter();
            var WINDFORCE25H = DataExe.GetDbParameter();
            var WINDFORCE26H = DataExe.GetDbParameter();
            var WINDFORCE27H = DataExe.GetDbParameter();
            var WINDFORCE28H = DataExe.GetDbParameter();
            var WINDFORCE29H = DataExe.GetDbParameter();
            var WINDFORCE30H = DataExe.GetDbParameter();
            var WINDFORCE31H = DataExe.GetDbParameter();

            var WINDDIRECTION00H = DataExe.GetDbParameter();
            var WINDDIRECTION01H = DataExe.GetDbParameter();
            var WINDDIRECTION02H = DataExe.GetDbParameter();
            var WINDDIRECTION03H = DataExe.GetDbParameter();
            var WINDDIRECTION04H = DataExe.GetDbParameter();
            var WINDDIRECTION05H = DataExe.GetDbParameter();
            var WINDDIRECTION06H = DataExe.GetDbParameter();
            var WINDDIRECTION07H = DataExe.GetDbParameter();
            var WINDDIRECTION08H = DataExe.GetDbParameter();
            var WINDDIRECTION09H = DataExe.GetDbParameter();
            var WINDDIRECTION10H = DataExe.GetDbParameter();
            var WINDDIRECTION11H = DataExe.GetDbParameter();
            var WINDDIRECTION12H = DataExe.GetDbParameter();
            var WINDDIRECTION13H = DataExe.GetDbParameter();
            var WINDDIRECTION14H = DataExe.GetDbParameter();
            var WINDDIRECTION15H = DataExe.GetDbParameter();
            var WINDDIRECTION16H = DataExe.GetDbParameter();
            var WINDDIRECTION17H = DataExe.GetDbParameter();
            var WINDDIRECTION18H = DataExe.GetDbParameter();
            var WINDDIRECTION19H = DataExe.GetDbParameter();
            var WINDDIRECTION20H = DataExe.GetDbParameter();
            var WINDDIRECTION21H = DataExe.GetDbParameter();
            var WINDDIRECTION22H = DataExe.GetDbParameter();
            var WINDDIRECTION23H = DataExe.GetDbParameter();
            var WINDDIRECTION24H = DataExe.GetDbParameter();
            var WINDDIRECTION25H = DataExe.GetDbParameter();
            var WINDDIRECTION26H = DataExe.GetDbParameter();
            var WINDDIRECTION27H = DataExe.GetDbParameter();
            var WINDDIRECTION28H = DataExe.GetDbParameter();
            var WINDDIRECTION29H = DataExe.GetDbParameter();
            var WINDDIRECTION30H = DataExe.GetDbParameter();
            var WINDDIRECTION31H = DataExe.GetDbParameter();

            var WEATHER00D00H = DataExe.GetDbParameter();
            var WEATHER00D01H = DataExe.GetDbParameter();
            var WEATHER01D00H = DataExe.GetDbParameter();
            var WEATHER01D01H = DataExe.GetDbParameter();
            var WEATHER02D00H = DataExe.GetDbParameter();
            var WEATHER02D01H = DataExe.GetDbParameter();
            #endregion
            #region
            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTAREA.ParameterName = "@FORECASTAREA";
            WINDFORCE00H.ParameterName = "@WINDFORCE00H";
            WINDFORCE01H.ParameterName = "@WINDFORCE01H";
            WINDFORCE02H.ParameterName = "@WINDFORCE02H";
            WINDFORCE03H.ParameterName = "@WINDFORCE03H";
            WINDFORCE04H.ParameterName = "@WINDFORCE04H";
            WINDFORCE05H.ParameterName = "@WINDFORCE05H";
            WINDFORCE06H.ParameterName = "@WINDFORCE06H";
            WINDFORCE07H.ParameterName = "@WINDFORCE07H";
            WINDFORCE08H.ParameterName = "@WINDFORCE08H";
            WINDFORCE09H.ParameterName = "@WINDFORCE09H";
            WINDFORCE10H.ParameterName = "@WINDFORCE10H";
            WINDFORCE11H.ParameterName = "@WINDFORCE11H";
            WINDFORCE12H.ParameterName = "@WINDFORCE12H";
            WINDFORCE13H.ParameterName = "@WINDFORCE13H";
            WINDFORCE14H.ParameterName = "@WINDFORCE14H";
            WINDFORCE15H.ParameterName = "@WINDFORCE15H";
            WINDFORCE16H.ParameterName = "@WINDFORCE16H";
            WINDFORCE17H.ParameterName = "@WINDFORCE17H";
            WINDFORCE18H.ParameterName = "@WINDFORCE18H";
            WINDFORCE19H.ParameterName = "@WINDFORCE19H";
            WINDFORCE20H.ParameterName = "@WINDFORCE20H";
            WINDFORCE21H.ParameterName = "@WINDFORCE21H";
            WINDFORCE22H.ParameterName = "@WINDFORCE22H";
            WINDFORCE23H.ParameterName = "@WINDFORCE23H";
            WINDFORCE24H.ParameterName = "@WINDFORCE24H";
            WINDFORCE25H.ParameterName = "@WINDFORCE25H";
            WINDFORCE26H.ParameterName = "@WINDFORCE26H";
            WINDFORCE27H.ParameterName = "@WINDFORCE27H";
            WINDFORCE28H.ParameterName = "@WINDFORCE28H";
            WINDFORCE29H.ParameterName = "@WINDFORCE29H";
            WINDFORCE30H.ParameterName = "@WINDFORCE30H";
            WINDFORCE31H.ParameterName = "@WINDFORCE31H";

            WINDDIRECTION00H.ParameterName = "@WINDDIRECTION00H";
            WINDDIRECTION01H.ParameterName = "@WINDDIRECTION01H";
            WINDDIRECTION02H.ParameterName = "@WINDDIRECTION02H";
            WINDDIRECTION03H.ParameterName = "@WINDDIRECTION03H";
            WINDDIRECTION04H.ParameterName = "@WINDDIRECTION04H";
            WINDDIRECTION05H.ParameterName = "@WINDDIRECTION05H";
            WINDDIRECTION06H.ParameterName = "@WINDDIRECTION06H";
            WINDDIRECTION07H.ParameterName = "@WINDDIRECTION07H";
            WINDDIRECTION08H.ParameterName = "@WINDDIRECTION08H";
            WINDDIRECTION09H.ParameterName = "@WINDDIRECTION09H";
            WINDDIRECTION10H.ParameterName = "@WINDDIRECTION10H";
            WINDDIRECTION11H.ParameterName = "@WINDDIRECTION11H";
            WINDDIRECTION12H.ParameterName = "@WINDDIRECTION12H";
            WINDDIRECTION13H.ParameterName = "@WINDDIRECTION13H";
            WINDDIRECTION14H.ParameterName = "@WINDDIRECTION14H";
            WINDDIRECTION15H.ParameterName = "@WINDDIRECTION15H";
            WINDDIRECTION16H.ParameterName = "@WINDDIRECTION16H";
            WINDDIRECTION17H.ParameterName = "@WINDDIRECTION17H";
            WINDDIRECTION18H.ParameterName = "@WINDDIRECTION18H";
            WINDDIRECTION19H.ParameterName = "@WINDDIRECTION19H";
            WINDDIRECTION20H.ParameterName = "@WINDDIRECTION20H";
            WINDDIRECTION21H.ParameterName = "@WINDDIRECTION21H";
            WINDDIRECTION22H.ParameterName = "@WINDDIRECTION22H";
            WINDDIRECTION23H.ParameterName = "@WINDDIRECTION23H";
            WINDDIRECTION24H.ParameterName = "@WINDDIRECTION24H";
            WINDDIRECTION25H.ParameterName = "@WINDDIRECTION25H";
            WINDDIRECTION26H.ParameterName = "@WINDDIRECTION26H";
            WINDDIRECTION27H.ParameterName = "@WINDDIRECTION27H";
            WINDDIRECTION28H.ParameterName = "@WINDDIRECTION28H";
            WINDDIRECTION29H.ParameterName = "@WINDDIRECTION29H";
            WINDDIRECTION30H.ParameterName = "@WINDDIRECTION30H";
            WINDDIRECTION31H.ParameterName = "@WINDDIRECTION31H";

            WEATHER00D00H.ParameterName = "@WEATHER00D00H";
            WEATHER00D01H.ParameterName = "@WEATHER00D01H";
            WEATHER01D00H.ParameterName = "@WEATHER01D00H";
            WEATHER01D01H.ParameterName = "@WEATHER01D01H";
            WEATHER02D00H.ParameterName = "@WEATHER02D00H";
            WEATHER02D01H.ParameterName = "@WEATHER02D01H";
            #endregion
            #region
            PUBLISHDATE.Value = date.ToString("yyyy-MM-dd");
            FORECASTAREA.Value = ScoModel.FORECASTAREA;
            WINDFORCE00H.Value = ScoModel.WINDFORCE00H;
            WINDFORCE01H.Value = ScoModel.WINDFORCE01H;
            WINDFORCE02H.Value = ScoModel.WINDFORCE02H;
            WINDFORCE03H.Value = ScoModel.WINDFORCE03H;
            WINDFORCE04H.Value = ScoModel.WINDFORCE04H;
            WINDFORCE05H.Value = ScoModel.WINDFORCE05H;
            WINDFORCE06H.Value = ScoModel.WINDFORCE06H;
            WINDFORCE07H.Value = ScoModel.WINDFORCE07H;
            WINDFORCE08H.Value = ScoModel.WINDFORCE08H;
            WINDFORCE09H.Value = ScoModel.WINDFORCE09H;
            WINDFORCE10H.Value = ScoModel.WINDFORCE10H;
            WINDFORCE11H.Value = ScoModel.WINDFORCE11H;
            WINDFORCE12H.Value = ScoModel.WINDFORCE12H;
            WINDFORCE13H.Value = ScoModel.WINDFORCE13H;
            WINDFORCE14H.Value = ScoModel.WINDFORCE14H;
            WINDFORCE15H.Value = ScoModel.WINDFORCE15H;
            WINDFORCE16H.Value = ScoModel.WINDFORCE16H;
            WINDFORCE17H.Value = ScoModel.WINDFORCE17H;
            WINDFORCE18H.Value = ScoModel.WINDFORCE18H;
            WINDFORCE19H.Value = ScoModel.WINDFORCE19H;
            WINDFORCE20H.Value = ScoModel.WINDFORCE20H;
            WINDFORCE21H.Value = ScoModel.WINDFORCE21H;
            WINDFORCE22H.Value = ScoModel.WINDFORCE22H;
            WINDFORCE23H.Value = ScoModel.WINDFORCE23H;
            WINDFORCE24H.Value = ScoModel.WINDFORCE24H;
            WINDFORCE25H.Value = ScoModel.WINDFORCE25H;
            WINDFORCE26H.Value = ScoModel.WINDFORCE26H;
            WINDFORCE27H.Value = ScoModel.WINDFORCE27H;
            WINDFORCE28H.Value = ScoModel.WINDFORCE28H;
            WINDFORCE29H.Value = ScoModel.WINDFORCE29H;
            WINDFORCE30H.Value = ScoModel.WINDFORCE30H;
            WINDFORCE31H.Value = ScoModel.WINDFORCE31H;

            WINDDIRECTION00H.Value = ScoModel.WINDDIRECTION00H;
            WINDDIRECTION01H.Value = ScoModel.WINDDIRECTION01H;
            WINDDIRECTION02H.Value = ScoModel.WINDDIRECTION02H;
            WINDDIRECTION03H.Value = ScoModel.WINDDIRECTION03H;
            WINDDIRECTION04H.Value = ScoModel.WINDDIRECTION04H;
            WINDDIRECTION05H.Value = ScoModel.WINDDIRECTION05H;
            WINDDIRECTION06H.Value = ScoModel.WINDDIRECTION06H;
            WINDDIRECTION07H.Value = ScoModel.WINDDIRECTION07H;
            WINDDIRECTION08H.Value = ScoModel.WINDDIRECTION08H;
            WINDDIRECTION09H.Value = ScoModel.WINDDIRECTION09H;
            WINDDIRECTION10H.Value = ScoModel.WINDDIRECTION10H;
            WINDDIRECTION11H.Value = ScoModel.WINDDIRECTION11H;
            WINDDIRECTION12H.Value = ScoModel.WINDDIRECTION12H;
            WINDDIRECTION13H.Value = ScoModel.WINDDIRECTION13H;
            WINDDIRECTION14H.Value = ScoModel.WINDDIRECTION14H;
            WINDDIRECTION15H.Value = ScoModel.WINDDIRECTION15H;
            WINDDIRECTION16H.Value = ScoModel.WINDDIRECTION16H;
            WINDDIRECTION17H.Value = ScoModel.WINDDIRECTION17H;
            WINDDIRECTION18H.Value = ScoModel.WINDDIRECTION18H;
            WINDDIRECTION19H.Value = ScoModel.WINDDIRECTION19H;
            WINDDIRECTION20H.Value = ScoModel.WINDDIRECTION20H;
            WINDDIRECTION21H.Value = ScoModel.WINDDIRECTION21H;
            WINDDIRECTION22H.Value = ScoModel.WINDDIRECTION22H;
            WINDDIRECTION23H.Value = ScoModel.WINDDIRECTION23H;
            WINDDIRECTION24H.Value = ScoModel.WINDDIRECTION24H;
            WINDDIRECTION25H.Value = ScoModel.WINDDIRECTION25H;
            WINDDIRECTION26H.Value = ScoModel.WINDDIRECTION26H;
            WINDDIRECTION27H.Value = ScoModel.WINDDIRECTION27H;
            WINDDIRECTION28H.Value = ScoModel.WINDDIRECTION28H;
            WINDDIRECTION29H.Value = ScoModel.WINDDIRECTION29H;
            WINDDIRECTION30H.Value = ScoModel.WINDDIRECTION30H;
            WINDDIRECTION31H.Value = ScoModel.WINDDIRECTION31H;

            WEATHER00D00H.Value = ScoModel.WEATHER00D00H;
            WEATHER00D01H.Value = ScoModel.WEATHER00D01H;
            WEATHER01D00H.Value = ScoModel.WEATHER01D00H;
            WEATHER01D01H.Value = ScoModel.WEATHER01D01H;
            WEATHER02D00H.Value = ScoModel.WEATHER02D00H;
            WEATHER02D01H.Value = ScoModel.WEATHER02D01H;
            #endregion
            DbParameter[] parameters = { PUBLISHDATE, FORECASTAREA, WINDFORCE00H, WINDFORCE01H, WINDFORCE02H, WINDFORCE03H, WINDFORCE04H, WINDFORCE05H, WINDFORCE06H, WINDFORCE07H, WINDFORCE08H, WINDFORCE09H, WINDFORCE10H, WINDFORCE11H, WINDFORCE12H, WINDFORCE13H, WINDFORCE14H, WINDFORCE15H, WINDFORCE16H, WINDFORCE17H, WINDFORCE18H, WINDFORCE19H, WINDFORCE20H, WINDFORCE21H, WINDFORCE22H, WINDFORCE23H, WINDFORCE24H, WINDFORCE25H, WINDFORCE26H, WINDFORCE27H, WINDFORCE28H, WINDFORCE29H, WINDFORCE30H, WINDFORCE31H, WINDDIRECTION00H, WINDDIRECTION01H, WINDDIRECTION02H, WINDDIRECTION03H, WINDDIRECTION04H, WINDDIRECTION05H, WINDDIRECTION06H, WINDDIRECTION07H, WINDDIRECTION08H, WINDDIRECTION09H, WINDDIRECTION10H, WINDDIRECTION11H, WINDDIRECTION12H, WINDDIRECTION13H, WINDDIRECTION14H, WINDDIRECTION15H, WINDDIRECTION16H, WINDDIRECTION17H, WINDDIRECTION18H, WINDDIRECTION19H, WINDDIRECTION20H, WINDDIRECTION21H, WINDDIRECTION22H, WINDDIRECTION23H, WINDDIRECTION24H, WINDDIRECTION25H, WINDDIRECTION26H, WINDDIRECTION27H, WINDDIRECTION28H, WINDDIRECTION29H, WINDDIRECTION30H, WINDDIRECTION31H, WEATHER00D00H, WEATHER00D01H, WEATHER01D00H, WEATHER01D01H, WEATHER02D00H, WEATHER02D01H };
            return parameters;
        }
        /// <summary>
        /// 生浪的参数数组
        /// </summary>
        /// <param name="WaveModel"></param>
        /// <returns></returns>
        private DbParameter[] SetWaveParam(DateTime date,SCOWaveModel WaveModel)
        {
            #region 
            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTAREA = DataExe.GetDbParameter();
            var WAVEFORCE00H = DataExe.GetDbParameter();
            var WAVEFORCE01H = DataExe.GetDbParameter();
            var WAVEFORCE02H = DataExe.GetDbParameter();
            var WAVEFORCE03H = DataExe.GetDbParameter();
            var WAVEFORCE04H = DataExe.GetDbParameter();
            var WAVEFORCE05H = DataExe.GetDbParameter();
            var WAVEFORCE06H = DataExe.GetDbParameter();
            var WAVEFORCE07H = DataExe.GetDbParameter();
            var WAVEFORCE08H = DataExe.GetDbParameter();
            var WAVEFORCE09H = DataExe.GetDbParameter();
            var WAVEFORCE10H = DataExe.GetDbParameter();
            var WAVEFORCE11H = DataExe.GetDbParameter();
            var WAVEFORCE12H = DataExe.GetDbParameter();
            var WAVEFORCE13H = DataExe.GetDbParameter();
            var WAVEFORCE14H = DataExe.GetDbParameter();
            var WAVEFORCE15H = DataExe.GetDbParameter();
            var WAVEFORCE16H = DataExe.GetDbParameter();
            var WAVEFORCE17H = DataExe.GetDbParameter();
            var WAVEFORCE18H = DataExe.GetDbParameter();
            var WAVEFORCE19H = DataExe.GetDbParameter();
            var WAVEFORCE20H = DataExe.GetDbParameter();
            var WAVEFORCE21H = DataExe.GetDbParameter();
            var WAVEFORCE22H = DataExe.GetDbParameter();
            var WAVEFORCE23H = DataExe.GetDbParameter();
            var WAVEFORCE24H = DataExe.GetDbParameter();
            var WAVEFORCE25H = DataExe.GetDbParameter();
            var WAVEFORCE26H = DataExe.GetDbParameter();
            var WAVEFORCE27H = DataExe.GetDbParameter();
            var WAVEFORCE28H = DataExe.GetDbParameter();
            var WAVEFORCE29H = DataExe.GetDbParameter();
            var WAVEFORCE30H = DataExe.GetDbParameter();
            var WAVEFORCE31H = DataExe.GetDbParameter();

            var WAVEDIRECTION00H = DataExe.GetDbParameter();
            var WAVEDIRECTION01H = DataExe.GetDbParameter();
            var WAVEDIRECTION02H = DataExe.GetDbParameter();
            var WAVEDIRECTION03H = DataExe.GetDbParameter();
            var WAVEDIRECTION04H = DataExe.GetDbParameter();
            var WAVEDIRECTION05H = DataExe.GetDbParameter();
            var WAVEDIRECTION06H = DataExe.GetDbParameter();
            var WAVEDIRECTION07H = DataExe.GetDbParameter();
            var WAVEDIRECTION08H = DataExe.GetDbParameter();
            var WAVEDIRECTION09H = DataExe.GetDbParameter();
            var WAVEDIRECTION10H = DataExe.GetDbParameter();
            var WAVEDIRECTION11H = DataExe.GetDbParameter();
            var WAVEDIRECTION12H = DataExe.GetDbParameter();
            var WAVEDIRECTION13H = DataExe.GetDbParameter();
            var WAVEDIRECTION14H = DataExe.GetDbParameter();
            var WAVEDIRECTION15H = DataExe.GetDbParameter();
            var WAVEDIRECTION16H = DataExe.GetDbParameter();
            var WAVEDIRECTION17H = DataExe.GetDbParameter();
            var WAVEDIRECTION18H = DataExe.GetDbParameter();
            var WAVEDIRECTION19H = DataExe.GetDbParameter();
            var WAVEDIRECTION20H = DataExe.GetDbParameter();
            var WAVEDIRECTION21H = DataExe.GetDbParameter();
            var WAVEDIRECTION22H = DataExe.GetDbParameter();
            var WAVEDIRECTION23H = DataExe.GetDbParameter();
            var WAVEDIRECTION24H = DataExe.GetDbParameter();
            var WAVEDIRECTION25H = DataExe.GetDbParameter();
            var WAVEDIRECTION26H = DataExe.GetDbParameter();
            var WAVEDIRECTION27H = DataExe.GetDbParameter();
            var WAVEDIRECTION28H = DataExe.GetDbParameter();
            var WAVEDIRECTION29H = DataExe.GetDbParameter();
            var WAVEDIRECTION30H = DataExe.GetDbParameter();
            var WAVEDIRECTION31H = DataExe.GetDbParameter();

            #endregion
            #region
            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTAREA.ParameterName = "@FORECASTAREA";
            WAVEFORCE00H.ParameterName = "@WAVEFORCE00H";
            WAVEFORCE01H.ParameterName = "@WAVEFORCE01H";
            WAVEFORCE02H.ParameterName = "@WAVEFORCE02H";
            WAVEFORCE03H.ParameterName = "@WAVEFORCE03H";
            WAVEFORCE04H.ParameterName = "@WAVEFORCE04H";
            WAVEFORCE05H.ParameterName = "@WAVEFORCE05H";
            WAVEFORCE06H.ParameterName = "@WAVEFORCE06H";
            WAVEFORCE07H.ParameterName = "@WAVEFORCE07H";
            WAVEFORCE08H.ParameterName = "@WAVEFORCE08H";
            WAVEFORCE09H.ParameterName = "@WAVEFORCE09H";
            WAVEFORCE10H.ParameterName = "@WAVEFORCE10H";
            WAVEFORCE11H.ParameterName = "@WAVEFORCE11H";
            WAVEFORCE12H.ParameterName = "@WAVEFORCE12H";
            WAVEFORCE13H.ParameterName = "@WAVEFORCE13H";
            WAVEFORCE14H.ParameterName = "@WAVEFORCE14H";
            WAVEFORCE15H.ParameterName = "@WAVEFORCE15H";
            WAVEFORCE16H.ParameterName = "@WAVEFORCE16H";
            WAVEFORCE17H.ParameterName = "@WAVEFORCE17H";
            WAVEFORCE18H.ParameterName = "@WAVEFORCE18H";
            WAVEFORCE19H.ParameterName = "@WAVEFORCE19H";
            WAVEFORCE20H.ParameterName = "@WAVEFORCE20H";
            WAVEFORCE21H.ParameterName = "@WAVEFORCE21H";
            WAVEFORCE22H.ParameterName = "@WAVEFORCE22H";
            WAVEFORCE23H.ParameterName = "@WAVEFORCE23H";
            WAVEFORCE24H.ParameterName = "@WAVEFORCE24H";
            WAVEFORCE25H.ParameterName = "@WAVEFORCE25H";
            WAVEFORCE26H.ParameterName = "@WAVEFORCE26H";
            WAVEFORCE27H.ParameterName = "@WAVEFORCE27H";
            WAVEFORCE28H.ParameterName = "@WAVEFORCE28H";
            WAVEFORCE29H.ParameterName = "@WAVEFORCE29H";
            WAVEFORCE30H.ParameterName = "@WAVEFORCE30H";
            WAVEFORCE31H.ParameterName = "@WAVEFORCE31H";

            WAVEDIRECTION00H.ParameterName = "@WAVEDIRECTION00H";
            WAVEDIRECTION01H.ParameterName = "@WAVEDIRECTION01H";
            WAVEDIRECTION02H.ParameterName = "@WAVEDIRECTION02H";
            WAVEDIRECTION03H.ParameterName = "@WAVEDIRECTION03H";
            WAVEDIRECTION04H.ParameterName = "@WAVEDIRECTION04H";
            WAVEDIRECTION05H.ParameterName = "@WAVEDIRECTION05H";
            WAVEDIRECTION06H.ParameterName = "@WAVEDIRECTION06H";
            WAVEDIRECTION07H.ParameterName = "@WAVEDIRECTION07H";
            WAVEDIRECTION08H.ParameterName = "@WAVEDIRECTION08H";
            WAVEDIRECTION09H.ParameterName = "@WAVEDIRECTION09H";
            WAVEDIRECTION10H.ParameterName = "@WAVEDIRECTION10H";
            WAVEDIRECTION11H.ParameterName = "@WAVEDIRECTION11H";
            WAVEDIRECTION12H.ParameterName = "@WAVEDIRECTION12H";
            WAVEDIRECTION13H.ParameterName = "@WAVEDIRECTION13H";
            WAVEDIRECTION14H.ParameterName = "@WAVEDIRECTION14H";
            WAVEDIRECTION15H.ParameterName = "@WAVEDIRECTION15H";
            WAVEDIRECTION16H.ParameterName = "@WAVEDIRECTION16H";
            WAVEDIRECTION17H.ParameterName = "@WAVEDIRECTION17H";
            WAVEDIRECTION18H.ParameterName = "@WAVEDIRECTION18H";
            WAVEDIRECTION19H.ParameterName = "@WAVEDIRECTION19H";
            WAVEDIRECTION20H.ParameterName = "@WAVEDIRECTION20H";
            WAVEDIRECTION21H.ParameterName = "@WAVEDIRECTION21H";
            WAVEDIRECTION22H.ParameterName = "@WAVEDIRECTION22H";
            WAVEDIRECTION23H.ParameterName = "@WAVEDIRECTION23H";
            WAVEDIRECTION24H.ParameterName = "@WAVEDIRECTION24H";
            WAVEDIRECTION25H.ParameterName = "@WAVEDIRECTION25H";
            WAVEDIRECTION26H.ParameterName = "@WAVEDIRECTION26H";
            WAVEDIRECTION27H.ParameterName = "@WAVEDIRECTION27H";
            WAVEDIRECTION28H.ParameterName = "@WAVEDIRECTION28H";
            WAVEDIRECTION29H.ParameterName = "@WAVEDIRECTION29H";
            WAVEDIRECTION30H.ParameterName = "@WAVEDIRECTION30H";
            WAVEDIRECTION31H.ParameterName = "@WAVEDIRECTION31H";

            #endregion
            #region
            PUBLISHDATE.Value = date.ToString("yyyy-MM-dd");
            FORECASTAREA.Value = WaveModel.FORECASTAREA;
            WAVEFORCE00H.Value = WaveModel.WAVEFORCE00H;
            WAVEFORCE01H.Value = WaveModel.WAVEFORCE01H;
            WAVEFORCE02H.Value = WaveModel.WAVEFORCE02H;
            WAVEFORCE03H.Value = WaveModel.WAVEFORCE03H;
            WAVEFORCE04H.Value = WaveModel.WAVEFORCE04H;
            WAVEFORCE05H.Value = WaveModel.WAVEFORCE05H;
            WAVEFORCE06H.Value = WaveModel.WAVEFORCE06H;
            WAVEFORCE07H.Value = WaveModel.WAVEFORCE07H;
            WAVEFORCE08H.Value = WaveModel.WAVEFORCE08H;
            WAVEFORCE09H.Value = WaveModel.WAVEFORCE09H;
            WAVEFORCE10H.Value = WaveModel.WAVEFORCE10H;
            WAVEFORCE11H.Value = WaveModel.WAVEFORCE11H;
            WAVEFORCE12H.Value = WaveModel.WAVEFORCE12H;
            WAVEFORCE13H.Value = WaveModel.WAVEFORCE13H;
            WAVEFORCE14H.Value = WaveModel.WAVEFORCE14H;
            WAVEFORCE15H.Value = WaveModel.WAVEFORCE15H;
            WAVEFORCE16H.Value = WaveModel.WAVEFORCE16H;
            WAVEFORCE17H.Value = WaveModel.WAVEFORCE17H;
            WAVEFORCE18H.Value = WaveModel.WAVEFORCE18H;
            WAVEFORCE19H.Value = WaveModel.WAVEFORCE19H;
            WAVEFORCE20H.Value = WaveModel.WAVEFORCE20H;
            WAVEFORCE21H.Value = WaveModel.WAVEFORCE21H;
            WAVEFORCE22H.Value = WaveModel.WAVEFORCE22H;
            WAVEFORCE23H.Value = WaveModel.WAVEFORCE23H;
            WAVEFORCE24H.Value = WaveModel.WAVEFORCE24H;
            WAVEFORCE25H.Value = WaveModel.WAVEFORCE25H;
            WAVEFORCE26H.Value = WaveModel.WAVEFORCE26H;
            WAVEFORCE27H.Value = WaveModel.WAVEFORCE27H;
            WAVEFORCE28H.Value = WaveModel.WAVEFORCE28H;
            WAVEFORCE29H.Value = WaveModel.WAVEFORCE29H;
            WAVEFORCE30H.Value = WaveModel.WAVEFORCE30H;
            WAVEFORCE31H.Value = WaveModel.WAVEFORCE31H;

            WAVEDIRECTION00H.Value = WaveModel.WAVEDIRECTION00H;
            WAVEDIRECTION01H.Value = WaveModel.WAVEDIRECTION01H;
            WAVEDIRECTION02H.Value = WaveModel.WAVEDIRECTION02H;
            WAVEDIRECTION03H.Value = WaveModel.WAVEDIRECTION03H;
            WAVEDIRECTION04H.Value = WaveModel.WAVEDIRECTION04H;
            WAVEDIRECTION05H.Value = WaveModel.WAVEDIRECTION05H;
            WAVEDIRECTION06H.Value = WaveModel.WAVEDIRECTION06H;
            WAVEDIRECTION07H.Value = WaveModel.WAVEDIRECTION07H;
            WAVEDIRECTION08H.Value = WaveModel.WAVEDIRECTION08H;
            WAVEDIRECTION09H.Value = WaveModel.WAVEDIRECTION09H;
            WAVEDIRECTION10H.Value = WaveModel.WAVEDIRECTION10H;
            WAVEDIRECTION11H.Value = WaveModel.WAVEDIRECTION11H;
            WAVEDIRECTION12H.Value = WaveModel.WAVEDIRECTION12H;
            WAVEDIRECTION13H.Value = WaveModel.WAVEDIRECTION13H;
            WAVEDIRECTION14H.Value = WaveModel.WAVEDIRECTION14H;
            WAVEDIRECTION15H.Value = WaveModel.WAVEDIRECTION15H;
            WAVEDIRECTION16H.Value = WaveModel.WAVEDIRECTION16H;
            WAVEDIRECTION17H.Value = WaveModel.WAVEDIRECTION17H;
            WAVEDIRECTION18H.Value = WaveModel.WAVEDIRECTION18H;
            WAVEDIRECTION19H.Value = WaveModel.WAVEDIRECTION19H;
            WAVEDIRECTION20H.Value = WaveModel.WAVEDIRECTION20H;
            WAVEDIRECTION21H.Value = WaveModel.WAVEDIRECTION21H;
            WAVEDIRECTION22H.Value = WaveModel.WAVEDIRECTION22H;
            WAVEDIRECTION23H.Value = WaveModel.WAVEDIRECTION23H;
            WAVEDIRECTION24H.Value = WaveModel.WAVEDIRECTION24H;
            WAVEDIRECTION25H.Value = WaveModel.WAVEDIRECTION25H;
            WAVEDIRECTION26H.Value = WaveModel.WAVEDIRECTION26H;
            WAVEDIRECTION27H.Value = WaveModel.WAVEDIRECTION27H;
            WAVEDIRECTION28H.Value = WaveModel.WAVEDIRECTION28H;
            WAVEDIRECTION29H.Value = WaveModel.WAVEDIRECTION29H;
            WAVEDIRECTION30H.Value = WaveModel.WAVEDIRECTION30H;
            WAVEDIRECTION31H.Value = WaveModel.WAVEDIRECTION31H;

            #endregion
            DbParameter[] parameters = { PUBLISHDATE, FORECASTAREA, WAVEFORCE00H, WAVEFORCE01H, WAVEFORCE02H, WAVEFORCE03H, WAVEFORCE04H, WAVEFORCE05H, WAVEFORCE06H, WAVEFORCE07H, WAVEFORCE08H, WAVEFORCE09H, WAVEFORCE10H, WAVEFORCE11H, WAVEFORCE12H, WAVEFORCE13H, WAVEFORCE14H, WAVEFORCE15H, WAVEFORCE16H, WAVEFORCE17H, WAVEFORCE18H, WAVEFORCE19H, WAVEFORCE20H, WAVEFORCE21H, WAVEFORCE22H, WAVEFORCE23H, WAVEFORCE24H, WAVEFORCE25H, WAVEFORCE26H, WAVEFORCE27H, WAVEFORCE28H, WAVEFORCE29H, WAVEFORCE30H, WAVEFORCE31H, WAVEDIRECTION00H, WAVEDIRECTION01H, WAVEDIRECTION02H, WAVEDIRECTION03H, WAVEDIRECTION04H, WAVEDIRECTION05H, WAVEDIRECTION06H, WAVEDIRECTION07H, WAVEDIRECTION08H, WAVEDIRECTION09H, WAVEDIRECTION10H, WAVEDIRECTION11H, WAVEDIRECTION12H, WAVEDIRECTION13H, WAVEDIRECTION14H, WAVEDIRECTION15H, WAVEDIRECTION16H, WAVEDIRECTION17H, WAVEDIRECTION18H, WAVEDIRECTION19H, WAVEDIRECTION20H, WAVEDIRECTION21H, WAVEDIRECTION22H, WAVEDIRECTION23H, WAVEDIRECTION24H, WAVEDIRECTION25H, WAVEDIRECTION26H, WAVEDIRECTION27H, WAVEDIRECTION28H, WAVEDIRECTION29H, WAVEDIRECTION30H, WAVEDIRECTION31H };
            return parameters;
        }
        #endregion

        #endregion

        #region 水温

        #region 近海水温
        /// <summary>
        /// 获取近海水温源数据（TBLWATERTEMPERATURE）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public object GetOffShareWaterTemperature_S(DateTime dt)
        {
            try
            {
                string sql = "select ID,NAME, PUBLISHDATE, MAXVALUE_24H, MINVALUE_24H , MEAN_24H AS AVERAGE_24H, MAXVALUE_48H, MINVALUE_48H, MEAN_48H AS AVERAGE_48H, MAXVALUE_72H, MINVALUE_72H, MEAN_72H AS AVERAGE_72H from TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + dt.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and NAME='青岛' AND id='C10_XMD'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取上合峰会近海水温信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取近海水温(预报员修改后)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public object GetOffShareWaterTemperature(DateTime date) {
            try
            {
                string sql = "select * from TBL_TEMPERATURE_SH where PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') and TYPE = '近海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取上合峰会近海水温信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 添加近海水温
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public int AddOffShoreSW(SCOTemperatureMode temp)
        {
            try
            {
                string sql = "insert into TBL_TEMPERATURE_SH (PUBLISHDATE,MAXVALUE_24H,MINVALUE_24H,AVERAGE_24H,MAXVALUE_48H,MINVALUE_48H,AVERAGE_48H,MAXVALUE_72H,MINVALUE_72H,AVERAGE_72H,TYPE) values (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@MAX1,@MIN1,@AVG1,@MAX2,@MIN2,@AVG2,@MAX3,@MIN3,@AVG3,'近海')";
                DbParameter[] parameter = SetTempParameter(temp);
                return DataExe.GetIntExeData(sql, parameter);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加近海水温信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改近海水温
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public int EditOffShoreSW(SCOTemperatureMode temp) {
            try
            {
                string sql = "UPDATE TBL_TEMPERATURE_SH SET MAXVALUE_24H=@MAX1,MINVALUE_24H=@MIN1,AVERAGE_24H=@AVG1,MAXVALUE_48H=@MAX2,MINVALUE_48H=@MIN2,AVERAGE_48H=@AVG2,MAXVALUE_72H=@MAX3,MINVALUE_72H=@MIN3,AVERAGE_72H=@AVG3 where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and TYPE='近海'";
                DbParameter[] parameter = SetTempParameter(temp);
                return DataExe.GetIntExeData(sql, parameter);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改外海水温信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region 外海水温
        /// <summary>
        /// 获取外海水温源数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetOnShoreSW_S(DateTime date) {
            try
            {
                string sql = "select ID,NAME, PUBLISHDATE, MAXVALUE_24H, MINVALUE_24H , MEAN_24H AS AVERAGE_24H, MAXVALUE_48H, MINVALUE_48H, MEAN_48H AS AVERAGE_48H, MAXVALUE_72H, MINVALUE_72H, MEAN_72H AS AVERAGE_72H from TBLWATERTEMPERATURE where PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') and NAME='日照以东浮标2'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取外海水温信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取外海水温 （预报员修改后）
        /// </summary>
        /// <param name="date">填报日期</param>
        /// <returns></returns>
        public object GetOnShoreSW(DateTime date)
        {
            try
            {
                string sql = "select * from TBL_TEMPERATURE_SH where PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') and TYPE = '外海'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取外海水温信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 添加外海水温
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public int AddOnShoreSW(SCOTemperatureMode temp)
        {
            try
            {
                string sql = "insert into TBL_TEMPERATURE_SH (PUBLISHDATE,MAXVALUE_24H,MINVALUE_24H,AVERAGE_24H,MAXVALUE_48H,MINVALUE_48H,AVERAGE_48H,MAXVALUE_72H,MINVALUE_72H,AVERAGE_72H,TYPE) values (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@MAX1,@MIN1,@AVG1,@MAX2,@MIN2,@AVG2,@MAX3,@MIN3,@AVG3,'外海')";
                DbParameter[] parameter = SetTempParameter(temp);
                return DataExe.GetIntExeData(sql, parameter);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加外海水温信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        ///修改外海水温
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public int EditOnShoreSW(SCOTemperatureMode temp)
        {
            try
            {
                string sql = "UPDATE TBL_TEMPERATURE_SH SET MAXVALUE_24H=@MAX1,MINVALUE_24H=@MIN1,AVERAGE_24H=@AVG1,MAXVALUE_48H=@MAX2,MINVALUE_48H=@MIN2,AVERAGE_48H=@AVG2,MAXVALUE_72H=@MAX3,MINVALUE_72H=@MIN3,AVERAGE_72H=@AVG3 where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and TYPE='外海'";
                DbParameter[] parameter = SetTempParameter(temp);
                return DataExe.GetIntExeData(sql, parameter);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改外海水温信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 外海水温生成参数
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private DbParameter[] SetTempParameter(SCOTemperatureMode temp)
        {
            var PUBLISHDATE = DataExe.GetDbParameter();
            var MAX1 = DataExe.GetDbParameter();
            var MIN1= DataExe.GetDbParameter();
            var AVG1 = DataExe.GetDbParameter();
            var MAX2 = DataExe.GetDbParameter();
            var MIN2 = DataExe.GetDbParameter();
            var AVG2 = DataExe.GetDbParameter();
            var MAX3 = DataExe.GetDbParameter();
            var MIN3 = DataExe.GetDbParameter();
            var AVG3 = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            MAX1.ParameterName = "@MAX1";
            MAX2.ParameterName = "@MAX2";
            MAX3.ParameterName = "@MAX3";

            MIN1.ParameterName = "@MIN1";
            MIN2.ParameterName = "@MIN2";
            MIN3.ParameterName = "@MIN3";

            AVG1.ParameterName = "@AVG1";
            AVG2.ParameterName = "@AVG2";
            AVG3.ParameterName = "@AVG3";

            PUBLISHDATE.Value = temp.PUBLISHDATE.ToString("yyyy-MM-dd");
            MAX1.Value = temp.MAX1;
            MAX2.Value = temp.MAX2;
            MAX3.Value = temp.MAX3;

            MIN1.Value = temp.MIN1;
            MIN2.Value = temp.MIN2;
            MIN3.Value = temp.MIN3;

            AVG1.Value = temp.AVG1;
            AVG2.Value = temp.AVG2;
            AVG3.Value = temp.AVG3;

            DbParameter[] Parameter = { PUBLISHDATE, MAX1, MAX2, MAX3, MIN1, MIN2, MIN3, AVG1, AVG2, AVG3 };
            return Parameter;
        }
        #endregion
        #endregion
         
        #region  期数和综述
        /// <summary>
        /// 获取当前日期的期数和综述
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableSumAndPeriod(DateTime date)
        {
            try
            {
                string sql = "SELECT * FROM TBL_SUMMARIZE_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取期数和综述信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取期数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTablePeriod(DateTime date)
        {
            try
            {
                string sql = "SELECT PERIODS FROM TBL_SUMMARIZE_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取期数信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 添加期数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="Period"></param>
        /// <returns></returns> 
        public int AddTablePeriod(DateTime date, string Period)
        {
            try
            {
                string sql = "INSERT INTO TBL_SUMMARIZE_SH (PUBLISHDATE,PERIODS) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@PERIODS)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var PERIODS = DataExe.GetDbParameter();
                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                PERIODS.ParameterName = "@PERIODS";
                PUBLISHDATE.Value = date.ToString();
                PERIODS.Value = Period.ToString();

                DbParameter[] parameters = { PUBLISHDATE, PERIODS };

                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加期数信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改期数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="Period"></param>
        /// <returns></returns>
        public int EditTablePeriod(DateTime date, string Period)
        {
            try
            {
                string sql = "UPDATE TBL_SUMMARIZE_SH set PERIODS=@PERIODS where PUBLISHDATE = to_date(@PUBLISHDATE, 'yyyy-mm-dd hh24@mi@ss') ";
                var PUBLISHDATE = DataExe.GetDbParameter();
                var PERIODS = DataExe.GetDbParameter();
                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                PERIODS.ParameterName = "@PERIODS";
                PUBLISHDATE.Value = date.ToString();
                PERIODS.Value = Period.ToString();
                DbParameter[] parameters = { PUBLISHDATE, PERIODS };

                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改期数信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取综述
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableSum(DateTime date)
        {
            try
            {
                string sql = "SELECT USUMMARIZE FROM TBL_SUMMARIZE_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取综述信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 添加综述
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="Sum">综述信息</param>
        /// <returns></returns>
        public int AddTableSum(DateTime date, string Sum)
        {
            try
            {
                string sql = "insert into TBL_SUMMARIZE_SH (PUBLISHDATE,USUMMARIZE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@SUMMARIZE)";
                var PUBLISHDATE = DataExe.GetDbParameter();
                var SUMMARIZE = DataExe.GetDbParameter();
                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                SUMMARIZE.ParameterName = "@SUMMARIZE";

                PUBLISHDATE.Value = date.ToString("yyyy-MM-dd");
                SUMMARIZE.Value = Sum;
                DbParameter[] parameters = { PUBLISHDATE, SUMMARIZE };
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加综述信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 修改综述
        /// </summary>
        /// <param name="date"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public int EditTableSum(DateTime date, string sum)
        {
            try {
                string sql = "UPDATE TBL_SUMMARIZE_SH set USUMMARIZE='" + sum + " 'where PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') ";
                return DataExe.GetIntExeData(sql);
            } catch (Exception ex) {
                WriteLog.Write("修改综述信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取最近期期数
        /// </summary>
        /// <returns></returns>
        public object GetFirstPeriod()
        {
            try
            {
                string sql = "SELECT * FROM(SELECT * FROM TBL_SUMMARIZE_SH ORDER BY PUBLISHDATE DESC)WHERE ROWNUM=1";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取最近期期数信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }

        }
        #endregion


        #region 获取青岛近海潮汐 （TBLGOLDBEACH72HTIDALFORECAST） 
        public object GetQingDaoTide(DateTime dt)
        {
            try
            {
                string sql = "select * from TBLGOLDBEACH72HTIDALFORECAST where  PUBLISHDATE=to_date('" + dt.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and SEABEACH='青岛市区' ORDER BY forecastdate ";
                return DataExe.GetTableExeData(sql);
                
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取上合峰会近海潮汐信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region 黄海绿潮

        # region 获取 黄海绿潮 /近外海风、浪信息
        /// <summary>
        /// 获取绿潮近海风、浪信息_S
        /// </summary>
        /// <param name="date">填报日期</param>
        /// <returns></returns>
        public object GetTableOffShoreWindAndWave_S(DateTime date)
        {

            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='近海' ORDER BY FORECASTDATE ASC";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取近海风浪源信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取绿潮外海风\浪信息_S
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableONShoreWindAndWave_S(DateTime date)
        {

            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='外海' ORDER BY FORECASTDATE ASC";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取外海风浪源信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取绿潮近海风、浪数据（预报修改后）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOffShoreWindAndWave(DateTime date)
        {
            try    //TBL_WINDANDWAVEFORECAST_SH
            {
                string sql = "SELECT * FROM TBL_WINDANDWAVEFORECAST_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss') AND FORECASTAREA='近海' ORDER BY FORECASTDATE ASC";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取近海风、浪信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取绿潮外海风、浪数据（预报修改后）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object GetTableOnShoreWindAndWave(DateTime date)
        {
            try
            {
                string sql = "SELECT * FROM TBL_WINDANDWAVEFORECAST_SH WHERE PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss')AND FORECASTAREA='外海' ORDER BY FORECASTDATE ASC";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取近海风、浪信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #endregion

        #region 添加/修改  黄海绿潮  近/外海风、浪信息
        /// <summary>
        /// 添加近/外 海风、浪信息
        /// </summary>
        /// <param name="date">填报日期</param>
        /// <param name="LvChaoModel">绿潮实体类</param>
        /// <returns></returns>
        public int AddTableOffAndOnShoreWindAndWave(DateTime date, LvChaoWindAndWaveModel LvChaoModel)
        {
            string sql = "INSERT INTO TBL_WINDANDWAVEFORECAST_SH (PUBLISHDATE,FORECASTDATE,FORECASTAREA,WEATHER,WAVEHIGHT,WAVEDIRECTION,WINDFORCE,WINDDIRECTION) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@FORECASTAREA,@WEATHER,@WAVEHIGHT,@WAVEDIRECTION,@WINDFORCE,@WINDDIRECTION)";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var FORECASTAREA = DataExe.GetDbParameter();
            var WEATHER = DataExe.GetDbParameter();
            var WAVEHIGHT=DataExe.GetDbParameter();
            var WAVEDIRECTION=DataExe.GetDbParameter();
            var WINDFORCE=DataExe.GetDbParameter();
            var WINDDIRECTION = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            FORECASTAREA.ParameterName = "@FORECASTAREA";
            WEATHER.ParameterName = "@WEATHER";
            WAVEHIGHT.ParameterName = "@WAVEHIGHT";
            WAVEDIRECTION.ParameterName = "@WAVEDIRECTION";
            WINDFORCE.ParameterName = "@WINDFORCE";
            WINDDIRECTION.ParameterName = "@WINDDIRECTION";

            PUBLISHDATE.Value = LvChaoModel.PUBLISHDATE.ToString("yyyy-MM-dd");
            FORECASTDATE.Value = LvChaoModel.FORECASTDATE.ToString("yyyy-MM-dd");
            FORECASTAREA.Value = LvChaoModel.FORECASTAREA.ToString();
            WEATHER.Value = LvChaoModel.WEATHER.ToString();
            WAVEHIGHT.Value = LvChaoModel.WAVEHIGHT.ToString();
            WAVEDIRECTION.Value = LvChaoModel.WAVEDIRECTION.ToString();
            WINDFORCE.Value = LvChaoModel.WINDFORCE.ToString();
            WINDDIRECTION.Value = LvChaoModel.WINDDIRECTION.ToString();

            DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, FORECASTAREA, WEATHER, WAVEHIGHT, WAVEDIRECTION, WINDFORCE, WINDDIRECTION };
            try {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增黄海绿潮近外海风、浪信息失败！"+ex.Message+"\r\n");
                return 0;
            }
        }
        /// <summary>
        /// 修改 黄海绿潮近外海风浪信息
        /// </summary>
        /// <param name="date"></param>
        /// <param name="LvChaoModel"></param>
        /// <returns></returns>
        public int EditTableOffAndOnShoreWindAndWave(
            DateTime date, LvChaoWindAndWaveModel LvChaoModel)
        {
            string sql = "UPDATE TBL_WINDANDWAVEFORECAST_SH set WEATHER=@WEATHER,WAVEHIGHT=@WAVEHIGHT,WAVEDIRECTION=@WAVEDIRECTION,WINDFORCE=@WINDFORCE,WINDDIRECTION=@WINDDIRECTION where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') AND FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss') AND  FORECASTAREA= @FORECASTAREA";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var FORECASTAREA = DataExe.GetDbParameter();
            var WEATHER = DataExe.GetDbParameter();
            var WAVEHIGHT = DataExe.GetDbParameter();
            var WAVEDIRECTION = DataExe.GetDbParameter();
            var WINDFORCE = DataExe.GetDbParameter();
            var WINDDIRECTION = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            FORECASTAREA.ParameterName = "@FORECASTAREA";
            WEATHER.ParameterName = "@WEATHER";
            WAVEHIGHT.ParameterName = "@WAVEHIGHT";
            WAVEDIRECTION.ParameterName = "@WAVEDIRECTION";
            WINDFORCE.ParameterName = "@WINDFORCE";
            WINDDIRECTION.ParameterName = "@WINDDIRECTION";

            PUBLISHDATE.Value = LvChaoModel.PUBLISHDATE.ToString("yyyy-MM-dd");
            FORECASTDATE.Value = LvChaoModel.FORECASTDATE.ToString("yyyy-MM-dd");
            FORECASTAREA.Value = LvChaoModel.FORECASTAREA.ToString();
            WAVEHIGHT.Value = LvChaoModel.WAVEHIGHT.ToString();
            WEATHER.Value = LvChaoModel.WEATHER.ToString();
            WAVEDIRECTION.Value = LvChaoModel.WAVEDIRECTION.ToString();
            WINDFORCE.Value = LvChaoModel.WINDFORCE.ToString();
            WINDDIRECTION.Value = LvChaoModel.WINDDIRECTION.ToString();

            DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, FORECASTAREA, WEATHER, WAVEHIGHT, WAVEDIRECTION, WINDFORCE, WINDDIRECTION };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改黄海绿潮近外海风、浪信息失败！" + ex.Message + "\r\n");
                return 0;
            }
        }
            #endregion

        #endregion
    }

}