using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 获取水温数据
    /// 2018-01-23
    /// </summary>
    public class GetWaterTemperature
    {
        DataExecution DataExe;//声明一个数据执行类
        public GetWaterTemperature()
        {
            //
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
                                          //
        }

        /// <summary>
        /// 获取海温数据
        /// </summary>
        /// <param name="dateTime">数据库数据解析日期-预报单填报日期</param>
        /// <param name="area">预报地区</param>
        /// <returns></returns>
        public object GetWaterTemperatureData(DateTime dateTime,string[] area)
        {
            try
            {
                string FORECASTAREA = "";
                string sql = "select * from  TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + dateTime.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND  NAME IN (";
                
                for (int i = 0; i < area.Length; i++)
                {
                    FORECASTAREA += "'" + area[i].ToString() + "',";
                }
                sql += FORECASTAREA.Remove(FORECASTAREA.Length - 1, 1) + " )";
                
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取文档存储表信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取海温数据
        /// </summary>
        /// <param name="dateTime">数据库数据解析日期-预报单填报日期</param>
        /// <param name="area">预报地区</param>
        /// <returns></returns>
        public object GetWaterTemperatureData(DateTime dateTime, string[] area,string[] ids)
        {
            try
            {
                string FORECASTAREA = "";
                string id = "";

                string sql = "select * from  TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + dateTime.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND  NAME IN (";
                for (int i = 0; i < area.Length; i++)
                {
                    FORECASTAREA += "'" + area[i].ToString() + "',";
                }
                sql += FORECASTAREA.Remove(FORECASTAREA.Length - 1, 1) + " )";

                sql += " UNION ";

                sql += "select * from  TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + dateTime.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND  ID IN (";
                for (int j = 0; j < ids.Length; j++)
                {
                    id += "'" + ids[j].ToString() + "',";
                }
                sql += id.Remove(id.Length - 1, 1) + " )";

                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取文档存储表信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}