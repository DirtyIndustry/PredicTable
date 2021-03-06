﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_TBLHH3DAYSFORECAST
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLHH3DAYSFORECAST()
        {
            //
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
                                          //
        }
        /// <summary>
        /// 获取黄河海港附近海域风、浪预报
        /// </summary>
        /// <returns></returns>
        public object GETTBLHH3DAYSFORECAST(TBLYRSOUTHSEAWALL24WINDWAVE TBLYRSOUTHSEAWALL24WINDWAVE, string searchType = "p")
        {

            try
            {
                if (searchType == "f")
                {
                    var startDate = TBLYRSOUTHSEAWALL24WINDWAVE.FORECASTDATE.AddDays(1.0);
                    var endDate = startDate.AddDays(2.0);
                    var sql = "select * from HT_TBLHH3DAYSFORECAST where FORECASTDATE between to_date('"
                        + startDate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and "
                        + " to_date('" + endDate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') "
                         + " and PUBLISHDATE = to_date('" + TBLYRSOUTHSEAWALL24WINDWAVE.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')  ORDER BY publishdate";
                    return DataExe.GetTableExeData(sql);
                }
                else
                    return DataExe.GetTableExeData("select * from HT_TBLHH3DAYSFORECAST where PUBLISHDATE=to_date('" + TBLYRSOUTHSEAWALL24WINDWAVE.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取黄河海港附近海域风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 新增黄河南海堤附近海域24小时风浪预报
        /// </summary>
        /// <returns></returns>
        public int AddTBLHH3DAYSFORECAST(TBLYRSOUTHSEAWALL24WINDWAVE TBLYRSOUTHSEAWALL24WINDWAVE)
        {

            string sql = "INSERT INTO  HT_TBLHH3DAYSFORECAST (PUBLISHDATE,FORECASTDATE,YRSSWWWAVEHEIGHT,YRSSWWWAVEDIRECTION,YRSSWWWINDDIRECTION,YRSSWWWINDFORCE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@YRSSWWWAVEHEIGHT,@YRSSWWWAVEDIRECTION,@YRSSWWWINDDIRECTION,@YRSSWWWINDFORCE)";



            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var YRSSWWWAVEHEIGHT = DataExe.GetDbParameter();
            var YRSSWWWAVEDIRECTION = DataExe.GetDbParameter();
            var YRSSWWWINDDIRECTION = DataExe.GetDbParameter();
            var YRSSWWWINDFORCE = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            YRSSWWWAVEHEIGHT.ParameterName = "@YRSSWWWAVEHEIGHT";
            YRSSWWWAVEDIRECTION.ParameterName = "@YRSSWWWAVEDIRECTION";
            YRSSWWWINDDIRECTION.ParameterName = "@YRSSWWWINDDIRECTION";
            YRSSWWWINDFORCE.ParameterName = "@YRSSWWWINDFORCE";




            PUBLISHDATE.Value = TBLYRSOUTHSEAWALL24WINDWAVE.PUBLISHDATE.ToString();
            FORECASTDATE.Value = TBLYRSOUTHSEAWALL24WINDWAVE.FORECASTDATE.ToString();
            YRSSWWWAVEHEIGHT.Value = TBLYRSOUTHSEAWALL24WINDWAVE.YRSSWWWAVEHEIGHT;
            YRSSWWWAVEDIRECTION.Value = TBLYRSOUTHSEAWALL24WINDWAVE.YRSSWWWAVEDIRECTION;
            YRSSWWWINDDIRECTION.Value = TBLYRSOUTHSEAWALL24WINDWAVE.YRSSWWWINDDIRECTION;
            YRSSWWWINDFORCE.Value = TBLYRSOUTHSEAWALL24WINDWAVE.YRSSWWWINDFORCE;


            DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, YRSSWWWAVEHEIGHT, YRSSWWWAVEDIRECTION, YRSSWWWINDDIRECTION, YRSSWWWINDFORCE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增黄河海港附近海域风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改黄河南海堤附近海域24小时风浪预报
        /// </summary>
        public int EditTBLHH3DAYSFORECAST(TBLYRSOUTHSEAWALL24WINDWAVE TBLYRSOUTHSEAWALL24WINDWAVE)
        {
            string sql = "UPDATE   HT_TBLHH3DAYSFORECAST set	YRSSWWWAVEHEIGHT=@YRSSWWWAVEHEIGHT,YRSSWWWAVEDIRECTION=@YRSSWWWAVEDIRECTION,YRSSWWWINDDIRECTION=@YRSSWWWINDDIRECTION,YRSSWWWINDFORCE=@YRSSWWWINDFORCE where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var YRSSWWWAVEHEIGHT = DataExe.GetDbParameter();
            var YRSSWWWAVEDIRECTION = DataExe.GetDbParameter();
            var YRSSWWWINDDIRECTION = DataExe.GetDbParameter();
            var YRSSWWWINDFORCE = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            YRSSWWWAVEHEIGHT.ParameterName = "@YRSSWWWAVEHEIGHT";
            YRSSWWWAVEDIRECTION.ParameterName = "@YRSSWWWAVEDIRECTION";
            YRSSWWWINDDIRECTION.ParameterName = "@YRSSWWWINDDIRECTION";
            YRSSWWWINDFORCE.ParameterName = "@YRSSWWWINDFORCE";




            PUBLISHDATE.Value = TBLYRSOUTHSEAWALL24WINDWAVE.PUBLISHDATE.ToString();
            FORECASTDATE.Value = TBLYRSOUTHSEAWALL24WINDWAVE.FORECASTDATE.ToString();
            YRSSWWWAVEHEIGHT.Value = TBLYRSOUTHSEAWALL24WINDWAVE.YRSSWWWAVEHEIGHT;
            YRSSWWWAVEDIRECTION.Value = TBLYRSOUTHSEAWALL24WINDWAVE.YRSSWWWAVEDIRECTION;
            YRSSWWWINDDIRECTION.Value = TBLYRSOUTHSEAWALL24WINDWAVE.YRSSWWWINDDIRECTION;
            YRSSWWWINDFORCE.Value = TBLYRSOUTHSEAWALL24WINDWAVE.YRSSWWWINDFORCE;


            DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, YRSSWWWAVEHEIGHT, YRSSWWWAVEDIRECTION, YRSSWWWINDDIRECTION, YRSSWWWINDFORCE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改黄河南海堤附近海域24小时风浪出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }

        /// <summary>
        /// 获取三天数据
        /// </summary>
        /// <param name="TBLYRSOUTHSEAWALL24WINDWAVE"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public object GETTBLHH3DAYSFORECAST3DAYS(TBLYRSOUTHSEAWALL24WINDWAVE TBLYRSOUTHSEAWALL24WINDWAVE)
        {

            try
            {
                var startDate = TBLYRSOUTHSEAWALL24WINDWAVE.FORECASTDATE.AddDays(1.0);
                var endDate = startDate.AddDays(1.0);
                var sql = "select * from HT_TBLHH3DAYSFORECAST where FORECASTDATE between to_date('"
                       + startDate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and "
                       + " to_date('" + endDate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') "
                       + " and PUBLISHDATE = to_date('" + TBLYRSOUTHSEAWALL24WINDWAVE.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"                         + "ORDER BY publishdate";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取黄河海港附近海域风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}