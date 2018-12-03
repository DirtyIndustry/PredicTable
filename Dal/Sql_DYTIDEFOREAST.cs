using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    ///  东营埕岛-未来三天高/低潮预报
    /// </summary>
    public class Sql_DYTIDEFOREAST
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_DYTIDEFOREAST()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        /// 获取所有东营埕岛-未来三天高/低潮预报
        /// </summary>
        /// <returns></returns>
        public object GetDyTideForecastData(HT_DYTIDEFORECAST DYTIDEFORECAST)
        {

            try
            {
                return DataExe.GetTableExeData("select * from HT_DYTIDEFORECAST where PUBLISHDATE=to_date('" + DYTIDEFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取东营埕岛油田海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 新增东营埕岛-未来三天高/低潮预报
        /// </summary>
        /// <returns></returns>
        public int AddDyTideForecastData(HT_DYTIDEFORECAST DYTIDEFORECAST)
        {

            string sql = "INSERT INTO  HT_DYTIDEFORECAST (PUBLISHDATE,NOTFSECONDLOWWAVEHEIGHT,FORECASTDATE,NOTFFIRSTHIGHWAVETIME,NOTFFIRSTHIGHWAVEHEIGHT,NOTFFIRSTLOWWAVETIME,NOTFFIRSTLOWWAVEHEIGHT,NOTFSECONDHIGHWAVETIME,NOTFSECONDHIGHWAVEHEIGHT,NOTFSECONDLOWWAVETIME) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@NOTFSECONDLOWWAVEHEIGHT,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@NOTFFIRSTHIGHWAVETIME,@NOTFFIRSTHIGHWAVEHEIGHT,@NOTFFIRSTLOWWAVETIME,@NOTFFIRSTLOWWAVEHEIGHT,@NOTFSECONDHIGHWAVETIME,@NOTFSECONDHIGHWAVEHEIGHT,@NOTFSECONDLOWWAVETIME)";



            var PUBLISHDATE = DataExe.GetDbParameter();
            var NOTFSECONDLOWWAVEHEIGHT = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var NOTFFIRSTHIGHWAVETIME = DataExe.GetDbParameter();
            var NOTFFIRSTHIGHWAVEHEIGHT = DataExe.GetDbParameter();
            var NOTFFIRSTLOWWAVETIME = DataExe.GetDbParameter();
            var NOTFFIRSTLOWWAVEHEIGHT = DataExe.GetDbParameter();
            var NOTFSECONDHIGHWAVETIME = DataExe.GetDbParameter();
            var NOTFSECONDHIGHWAVEHEIGHT = DataExe.GetDbParameter();
            var NOTFSECONDLOWWAVETIME = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            NOTFSECONDLOWWAVEHEIGHT.ParameterName = "@NOTFSECONDLOWWAVEHEIGHT";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            NOTFFIRSTHIGHWAVETIME.ParameterName = "@NOTFFIRSTHIGHWAVETIME";
            NOTFFIRSTHIGHWAVEHEIGHT.ParameterName = "@NOTFFIRSTHIGHWAVEHEIGHT";
            NOTFFIRSTLOWWAVETIME.ParameterName = "@NOTFFIRSTLOWWAVETIME";
            NOTFFIRSTLOWWAVEHEIGHT.ParameterName = "@NOTFFIRSTLOWWAVEHEIGHT";
            NOTFSECONDHIGHWAVETIME.ParameterName = "@NOTFSECONDHIGHWAVETIME";
            NOTFSECONDHIGHWAVEHEIGHT.ParameterName = "@NOTFSECONDHIGHWAVEHEIGHT";
            NOTFSECONDLOWWAVETIME.ParameterName = "@NOTFSECONDLOWWAVETIME";




            PUBLISHDATE.Value = DYTIDEFORECAST.PUBLISHDATE.ToString();
            NOTFSECONDLOWWAVEHEIGHT.Value = DYTIDEFORECAST.NOTFSECONDLOWWAVEHEIGHT;
            FORECASTDATE.Value = DYTIDEFORECAST.FORECASTDATE.ToString();
            NOTFFIRSTHIGHWAVETIME.Value = DYTIDEFORECAST.NOTFFIRSTHIGHWAVETIME;
            NOTFFIRSTHIGHWAVEHEIGHT.Value = DYTIDEFORECAST.NOTFFIRSTHIGHWAVEHEIGHT;
            NOTFFIRSTLOWWAVETIME.Value = DYTIDEFORECAST.NOTFFIRSTLOWWAVETIME;
            NOTFFIRSTLOWWAVEHEIGHT.Value = DYTIDEFORECAST.NOTFFIRSTLOWWAVEHEIGHT;
            NOTFSECONDHIGHWAVETIME.Value = DYTIDEFORECAST.NOTFSECONDHIGHWAVETIME;
            NOTFSECONDHIGHWAVEHEIGHT.Value = DYTIDEFORECAST.NOTFSECONDHIGHWAVEHEIGHT;
            NOTFSECONDLOWWAVETIME.Value = DYTIDEFORECAST.NOTFSECONDLOWWAVETIME;


            DbParameter[] parameters = { PUBLISHDATE, NOTFSECONDLOWWAVEHEIGHT, FORECASTDATE, NOTFFIRSTHIGHWAVETIME, NOTFFIRSTHIGHWAVEHEIGHT, NOTFFIRSTLOWWAVETIME, NOTFFIRSTLOWWAVEHEIGHT, NOTFSECONDHIGHWAVETIME, NOTFSECONDHIGHWAVEHEIGHT, NOTFSECONDLOWWAVETIME };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增东营埕岛油田海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改东营埕岛-未来三天高/低潮预报
        /// </summary>
        public int EditDyTideForecastData(HT_DYTIDEFORECAST DYTIDEFORECAST)
        {
            string sql = "UPDATE   HT_DYTIDEFORECAST set  NOTFSECONDLOWWAVEHEIGHT=@NOTFSECONDLOWWAVEHEIGHT,NOTFFIRSTHIGHWAVETIME=@NOTFFIRSTHIGHWAVETIME,NOTFFIRSTHIGHWAVEHEIGHT=@NOTFFIRSTHIGHWAVEHEIGHT,NOTFFIRSTLOWWAVETIME=@NOTFFIRSTLOWWAVETIME,NOTFFIRSTLOWWAVEHEIGHT=@NOTFFIRSTLOWWAVEHEIGHT,NOTFSECONDHIGHWAVETIME=@NOTFSECONDHIGHWAVETIME,NOTFSECONDHIGHWAVEHEIGHT=@NOTFSECONDHIGHWAVEHEIGHT,NOTFSECONDLOWWAVETIME=@NOTFSECONDLOWWAVETIME where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var NOTFSECONDLOWWAVEHEIGHT = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var NOTFFIRSTHIGHWAVETIME = DataExe.GetDbParameter();
            var NOTFFIRSTHIGHWAVEHEIGHT = DataExe.GetDbParameter();
            var NOTFFIRSTLOWWAVETIME = DataExe.GetDbParameter();
            var NOTFFIRSTLOWWAVEHEIGHT = DataExe.GetDbParameter();
            var NOTFSECONDHIGHWAVETIME = DataExe.GetDbParameter();
            var NOTFSECONDHIGHWAVEHEIGHT = DataExe.GetDbParameter();
            var NOTFSECONDLOWWAVETIME = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            NOTFSECONDLOWWAVEHEIGHT.ParameterName = "@NOTFSECONDLOWWAVEHEIGHT";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            NOTFFIRSTHIGHWAVETIME.ParameterName = "@NOTFFIRSTHIGHWAVETIME";
            NOTFFIRSTHIGHWAVEHEIGHT.ParameterName = "@NOTFFIRSTHIGHWAVEHEIGHT";
            NOTFFIRSTLOWWAVETIME.ParameterName = "@NOTFFIRSTLOWWAVETIME";
            NOTFFIRSTLOWWAVEHEIGHT.ParameterName = "@NOTFFIRSTLOWWAVEHEIGHT";
            NOTFSECONDHIGHWAVETIME.ParameterName = "@NOTFSECONDHIGHWAVETIME";
            NOTFSECONDHIGHWAVEHEIGHT.ParameterName = "@NOTFSECONDHIGHWAVEHEIGHT";
            NOTFSECONDLOWWAVETIME.ParameterName = "@NOTFSECONDLOWWAVETIME";




            PUBLISHDATE.Value = DYTIDEFORECAST.PUBLISHDATE.ToString();
            NOTFSECONDLOWWAVEHEIGHT.Value = DYTIDEFORECAST.NOTFSECONDLOWWAVEHEIGHT;
            FORECASTDATE.Value = DYTIDEFORECAST.FORECASTDATE.ToString();
            NOTFFIRSTHIGHWAVETIME.Value = DYTIDEFORECAST.NOTFFIRSTHIGHWAVETIME;
            NOTFFIRSTHIGHWAVEHEIGHT.Value = DYTIDEFORECAST.NOTFFIRSTHIGHWAVEHEIGHT;
            NOTFFIRSTLOWWAVETIME.Value = DYTIDEFORECAST.NOTFFIRSTLOWWAVETIME;
            NOTFFIRSTLOWWAVEHEIGHT.Value = DYTIDEFORECAST.NOTFFIRSTLOWWAVEHEIGHT;
            NOTFSECONDHIGHWAVETIME.Value = DYTIDEFORECAST.NOTFSECONDHIGHWAVETIME;
            NOTFSECONDHIGHWAVEHEIGHT.Value = DYTIDEFORECAST.NOTFSECONDHIGHWAVEHEIGHT;
            NOTFSECONDLOWWAVETIME.Value = DYTIDEFORECAST.NOTFSECONDLOWWAVETIME;


            DbParameter[] parameters = { PUBLISHDATE, NOTFSECONDLOWWAVEHEIGHT, FORECASTDATE, NOTFFIRSTHIGHWAVETIME, NOTFFIRSTHIGHWAVEHEIGHT, NOTFFIRSTLOWWAVETIME, NOTFFIRSTLOWWAVEHEIGHT, NOTFSECONDHIGHWAVETIME, NOTFSECONDHIGHWAVEHEIGHT, NOTFSECONDLOWWAVETIME };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改东营埕岛油田海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }
    }
}