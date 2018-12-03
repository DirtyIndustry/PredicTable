using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    ///  大连、秦皇岛、天津
    ///  海浪、海温、潮汐数据
    /// </summary>
    public class sql_RadioForecast
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_RadioForecast()
        {
            //
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
            //
        }

        //海浪
        public object GetWave(DateTime dt,string DXYBQYMC)
        {

            try
            {
                string sql = "select * from NMFC_WAVEFORECAST where to_date(SUBSTR(PUBDATE,0,10),'yyyy-mm-dd,hh24:mi:ss') =to_date('" + dt.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND DXYBQYMC = '"+ DXYBQYMC + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取"+ DXYBQYMC + "海浪出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        //海温
        public object GetWater(DateTime dt, string DXYBQYMC)
        {

            try
            {

                string sql = "select * from NMFC_SEAFORECAST where to_date(SUBSTR(PUBDATE,0,10),'yyyy-mm-dd,hh24:mi:ss') =to_date('" + dt.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND DXYBQYMC = '" + DXYBQYMC + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取"+ DXYBQYMC + "海温出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        //潮汐
        /// <summary>
        /// 天津潮汐数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="DXYBQYMC"></param>
        /// <returns></returns>
        public object GetTide(DateTime dt, string DXYBQYMC)
        {

            try
            {

                string sql = "select * from NMFC_TIDALWATERFORECAST where to_date(SUBSTR(PUBDATE,0,10),'yyyy-mm-dd,hh24:mi:ss') =to_date('" + dt.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND DXYBQYMC =  '" + DXYBQYMC + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取"+ DXYBQYMC + "潮汐出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 大连、秦皇岛取实际潮汐数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="DXYBQYMC"></param>
        /// <returns></returns>
        public object GetActualTide(DateTime dt, string DXYBQYMC)
        {

            try
            {

                string sql = "select * from NMFC_ACTUALWATERFORECAST where to_date(SUBSTR(PUBDATE,0,10),'yyyy-mm-dd,hh24:mi:ss') =to_date('" + dt.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND DXYBQYMC =  '" + DXYBQYMC + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取" + DXYBQYMC + "潮汐出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #region 获取天津表2数据
        /// <summary>
        /// 获取天津海浪、海温、潮汐数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public object GetTianJinData(DateTime dt)
        {

            try
            {
                string sql = "select * from NMFC_TIANJIN where to_date(SUBSTR(to_char(PUBLISHDATE,'yyyy-MM-dd'),0,10),'yyyy-mm-dd,hh24:mi:ss') =to_date('" + dt.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取天津数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #endregion
    }
}