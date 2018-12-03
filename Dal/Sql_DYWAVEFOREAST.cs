using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
    /// </summary>
    public class Sql_DYWAVEFOREAST
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_DYWAVEFOREAST()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        /// 获取东营埕岛-未来三天的海面风及海浪有效波高预报
        /// </summary>
        /// <returns></returns>
        public object GetDyWaveForecastData(HT_DYWAVEFORECAST DYWAVEFORECAST)
        {

            try
            {
                return DataExe.GetTableExeData("select * from HT_DYWAVEFORECAST where PUBLISHDATE=to_date('" + DYWAVEFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取东营埕岛-未来三天的海面风及海浪有效波高预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 新增东营埕岛-未来三天的海面风及海浪有效波高预报
        /// </summary>
        /// <returns></returns>
        public int AddDyWaveForecastData(HT_DYWAVEFORECAST DYWAVEFORECAST)
        {

            string sql = "INSERT INTO  HT_DYWAVEFORECAST "
                + " (PUBLISHDATE,TIMEEFFECTIVE,WINDDIRECTION,WINDFORCE,WAVEHEIGHT) VALUES "
                + " (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@TIMEEFFECTIVE,@WINDDIRECTION,@WINDFORCE,@WAVEHEIGHT)";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var TIMEEFFECTIVE = DataExe.GetDbParameter();
            var WINDDIRECTION = DataExe.GetDbParameter();
            var WINDFORCE = DataExe.GetDbParameter();
            var WAVEHEIGHT = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            TIMEEFFECTIVE.ParameterName = "@TIMEEFFECTIVE";
            WINDDIRECTION.ParameterName = "@WINDDIRECTION";
            WINDFORCE.ParameterName = "@WINDFORCE";
            WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";

            PUBLISHDATE.Value = DYWAVEFORECAST.PUBLISHDATE.ToString();
            TIMEEFFECTIVE.Value = DYWAVEFORECAST.TIMEEFFECTIVE;
            WINDDIRECTION.Value = DYWAVEFORECAST.WINDDIRECTION.ToString();
            WINDFORCE.Value = DYWAVEFORECAST.WINDFORCE;
            WAVEHEIGHT.Value = DYWAVEFORECAST.WAVEHEIGHT;

            DbParameter[] parameters = { PUBLISHDATE, TIMEEFFECTIVE, WINDDIRECTION, WINDFORCE, WAVEHEIGHT };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增东营埕岛-未来三天的海面风及海浪有效波高预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改东营埕岛-未来三天的海面风及海浪有效波高预报
        /// </summary>
        public int EditDyWaveForecastData(HT_DYWAVEFORECAST DYWAVEFORECAST)
        {
            string sql = "UPDATE   HT_DYWAVEFORECAST set  WINDDIRECTION=@WINDDIRECTION,WINDFORCE=@WINDFORCE,WAVEHEIGHT=@WAVEHEIGHT "
                + " where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and TIMEEFFECTIVE=@TIMEEFFECTIVE";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var TIMEEFFECTIVE = DataExe.GetDbParameter();
            var WINDDIRECTION = DataExe.GetDbParameter();
            var WINDFORCE = DataExe.GetDbParameter();
            var WAVEHEIGHT = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            TIMEEFFECTIVE.ParameterName = "@TIMEEFFECTIVE";
            WINDDIRECTION.ParameterName = "@WINDDIRECTION";
            WINDFORCE.ParameterName = "@WINDFORCE";
            WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";

            PUBLISHDATE.Value = DYWAVEFORECAST.PUBLISHDATE.ToString();
            TIMEEFFECTIVE.Value = DYWAVEFORECAST.TIMEEFFECTIVE;
            WINDDIRECTION.Value = DYWAVEFORECAST.WINDDIRECTION.ToString();
            WINDFORCE.Value = DYWAVEFORECAST.WINDFORCE;
            WAVEHEIGHT.Value = DYWAVEFORECAST.WAVEHEIGHT;

            DbParameter[] parameters = { PUBLISHDATE, TIMEEFFECTIVE, WINDDIRECTION, WINDFORCE, WAVEHEIGHT };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改东营埕岛-未来三天的海面风及海浪有效波高预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}