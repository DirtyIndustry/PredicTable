using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 获取txt文件解析到数据库数据
    /// </summary>
    public class Sql_GetDataBy_S
    {
        DataExecution DataExe;//声明一个数据执行类

        public Sql_GetDataBy_S()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        /// 精细化风速和风向的原始数据
        /// </summary>
        /// <param name="PATTERNTIME">模式起报时间</param>
        /// <param name="NAME">中文全名</param>
        /// <returns></returns>
        public object GetWaveAndWindDataJXH(DateTime PATTERNTIME, string NAME)
        {
            try
            {
                string sql = "SELECT * FROM WIND_POINT_JXH WHERE PATTERNTIME = '" + PATTERNTIME.ToString("yyyyMMdd12") + "' AND NAME = '" + NAME + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海浪、气象数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// 获取海浪、气象数据
        /// </summary>
        /// <param name="PUBLISHDATE">填报时间</param>
        /// <param name="FORECASTEFFECT">预报时效（AP、PM）</param>
        /// <returns></returns>
        public object GetWaveAndWindData(DateTime PUBLISHDATE, string FORECASTEFFECT)
        {
            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = '" + FORECASTEFFECT + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海浪、气象数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取海浪、气象数据
        /// </summary>
        /// <param name="PUBLISHDATE">填报时间</param>
        /// <param name="FORECASTAREA">预报海区</param>
        /// <param name="FORECASTEFFECT">预报时效（AP、PM）</param>
        /// <returns></returns>
        public object GetWaveAndWindData(DateTime PUBLISHDATE, string FORECASTAREA, string FORECASTEFFECT)
        {
            try
            {
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = '" + FORECASTEFFECT + "' AND FORECASTAREA = '" + FORECASTAREA + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海浪、气象数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取REFINEFORECAST表中精细化五地市的浪高数据
        /// </summary>
        /// <param name="PUBLISHDATE"></param>
        /// <param name="FORECASTAREA"></param>
        /// <returns></returns>
        public object GetWaveHeight(DateTime PUBLISHDATE, string FORECASTAREA)
        {
            try
            {
                string sql = "SELECT * FROM REFINEFORECAST WHERE FILEDATE = to_date('" + PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')  AND NAME = '" + FORECASTAREA + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海浪、气象数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        public object GetWaveAndWindData(DateTime PUBLISHDATE, string[] FORECASTAREA, string FORECASTEFFECT)
        {
            try
            {
                string area = "";
                string sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = '" + FORECASTEFFECT + "' AND  FORECASTAREA in (";
                for (int i = 0; i < FORECASTAREA.Length; i++)
                {
                    area += "'" + FORECASTAREA[i].ToString() + "',";
                }
                sql += area.Remove(area.Length - 1, 1) + " )";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海浪、气象数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取指挥处渔政局数据
        /// </summary>
        /// <param name="PUBLISHDATE"></param>
        /// <param name="FORECASTAREA"></param>
        /// <returns></returns>
        public object GetYZJdData(DateTime PUBLISHDATE, string[] FORECASTAREA)
        {
            try
            {
                string area = "";
                string sql = "SELECT * FROM TBLZHCANDYZJFORECAST WHERE PUBLISHDATE = to_date('" + PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')  AND  FORECASTAREA in (";
                for (int i = 0; i < FORECASTAREA.Length; i++)
                {
                    area += "'" + FORECASTAREA[i].ToString() + "',";
                }
                sql += area.Remove(area.Length - 1, 1) + " )";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海浪、气象数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取周报数据
        /// </summary>
        /// <param name="PUBLISHDATE"></param>
        /// <param name="FORECASTAREA"></param>
        /// <returns></returns>
        public object GetWeekData(DateTime PUBLISHDATE, string[] FORECASTAREA)
        {
            try
            {
                string area = "";
                string sql = "SELECT * FROM TBLSEVENWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')  AND  FORECASTAREA in (";
                for (int i = 0; i < FORECASTAREA.Length; i++)
                {
                    area += "'" + FORECASTAREA[i].ToString() + "',";
                }
                sql += area.Remove(area.Length - 1, 1) + " )";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取周报海浪、气象数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}