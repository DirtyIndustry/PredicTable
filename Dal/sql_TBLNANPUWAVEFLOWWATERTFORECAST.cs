
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 南堡油田海域波浪、风、水温预报
/// </summary>
/// <returns></returns>
public class sql_TBLNANPUWAVEFLOWWATERTFORECAST
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLNANPUWAVEFLOWWATERTFORECAST()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 获取所有南堡油田海域波浪、风、水温预报预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLNANPUWAVEFLOWWATERTFORECAST_AllData(TBLNANPUWAVEFLOWWATERTFORECAST TBLNANPUWAVEFLOWWATERTFORECAST, string searchType = "p")
    {

        try
        {
            if (searchType == "f")
            {
                var startDate = TBLNANPUWAVEFLOWWATERTFORECAST.FORECASTDATE.AddDays(1.0);
                var endDate = startDate.AddDays(2.0);
                var sql = "select * from TBLNANPUWAVEFLOWWATERTFORECAST where FORECASTDATE between to_date('"
                    + startDate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and "
                    + " to_date('" + endDate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') ORDER BY publishdate";
                return DataExe.GetTableExeData(sql);
            }
            else
                return DataExe.GetTableExeData("select * from TBLNANPUWAVEFLOWWATERTFORECAST where PUBLISHDATE=to_date('" + TBLNANPUWAVEFLOWWATERTFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取南堡油田海域波浪、风、水温预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 新增南堡油田海域波浪、风、水温预报预报
    /// 1127  贾 ------ 添加quanxian参数
    /// </summary>
    /// <returns></returns>
    public int Add_TBLNANPUWAVEFLOWWATERTFORECAST(TBLNANPUWAVEFLOWWATERTFORECAST TBLNANPUWAVEFLOWWATERTFORECAST, string quanxian)
    {
        string sql = null;
        DbParameter[] parameters = null;
        if (quanxian.ToLower() == "fl")
        {
            sql = "INSERT INTO  TBLNANPUWAVEFLOWWATERTFORECAST (PUBLISHDATE,FORECASTDATE,NWFWTFWAVEHEIGHT,NWFWTFWAVEDIR,NWFWTFFLOWDIR,NWFWTFFLOWLEVEL,NWFWTFWEATHER) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@NWFWTFWAVEHEIGHT,@NWFWTFWAVEDIR,@NWFWTFFLOWDIR,@NWFWTFFLOWLEVEL,@NWFWTFWEATHER)";



            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var NWFWTFWAVEHEIGHT = DataExe.GetDbParameter();
            var NWFWTFWAVEDIR = DataExe.GetDbParameter();
            var NWFWTFFLOWDIR = DataExe.GetDbParameter();
            var NWFWTFFLOWLEVEL = DataExe.GetDbParameter();
            var NWFWTFWEATHER = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            NWFWTFWAVEHEIGHT.ParameterName = "@NWFWTFWAVEHEIGHT";
            NWFWTFWAVEDIR.ParameterName = "@NWFWTFWAVEDIR";
            NWFWTFFLOWDIR.ParameterName = "@NWFWTFFLOWDIR";
            NWFWTFFLOWLEVEL.ParameterName = "@NWFWTFFLOWLEVEL";
            NWFWTFWEATHER.ParameterName = "@NWFWTFWEATHER";




            PUBLISHDATE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.PUBLISHDATE.ToString();
            FORECASTDATE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.FORECASTDATE.ToString();
            NWFWTFWAVEHEIGHT.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFWAVEHEIGHT;
            NWFWTFWAVEDIR.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFWAVEDIR;
            NWFWTFFLOWDIR.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFFLOWDIR;
            NWFWTFFLOWLEVEL.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFFLOWLEVEL;
            NWFWTFWEATHER.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFWEATHER;


            parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, NWFWTFWAVEHEIGHT, NWFWTFWAVEDIR, NWFWTFFLOWDIR, NWFWTFFLOWLEVEL, NWFWTFWEATHER };
        }
        else if (quanxian.ToLower() == "sw")
        {
            sql = "INSERT INTO  TBLNANPUWAVEFLOWWATERTFORECAST (PUBLISHDATE,FORECASTDATE,NWFWTFWATERTEMPERATURE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@NWFWTFWATERTEMPERATURE)";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var NWFWTFWATERTEMPERATURE = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            NWFWTFWATERTEMPERATURE.ParameterName = "@NWFWTFWATERTEMPERATURE";

            PUBLISHDATE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.PUBLISHDATE.ToString();
            FORECASTDATE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.FORECASTDATE.ToString();
            NWFWTFWATERTEMPERATURE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFWATERTEMPERATURE;

            parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, NWFWTFWATERTEMPERATURE };
        }


        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增南堡油田海域波浪、风、水温预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改南堡油田海域波浪、风、水温预报预报
    /// 1127  贾 ------ 添加quanxian参数
    /// </summary>
    public int Edit_TBLNANPUWAVEFLOWWATERTFORECAST(TBLNANPUWAVEFLOWWATERTFORECAST TBLNANPUWAVEFLOWWATERTFORECAST, string quanxian)
    {
        string sql = null;
        DbParameter[] parameters = null;
        int count = 0;
        if (quanxian.ToLower() == "fl")
        {
            sql = "UPDATE   TBLNANPUWAVEFLOWWATERTFORECAST set	NWFWTFWAVEHEIGHT=@NWFWTFWAVEHEIGHT,NWFWTFWAVEDIR=@NWFWTFWAVEDIR,NWFWTFFLOWDIR=@NWFWTFFLOWDIR,NWFWTFFLOWLEVEL=@NWFWTFFLOWLEVEL,NWFWTFWEATHER=@NWFWTFWEATHER where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var NWFWTFWAVEHEIGHT = DataExe.GetDbParameter();
            var NWFWTFWAVEDIR = DataExe.GetDbParameter();
            var NWFWTFFLOWDIR = DataExe.GetDbParameter();
            var NWFWTFFLOWLEVEL = DataExe.GetDbParameter();
            var NWFWTFWEATHER = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            NWFWTFWAVEHEIGHT.ParameterName = "@NWFWTFWAVEHEIGHT";
            NWFWTFWAVEDIR.ParameterName = "@NWFWTFWAVEDIR";
            NWFWTFFLOWDIR.ParameterName = "@NWFWTFFLOWDIR";
            NWFWTFFLOWLEVEL.ParameterName = "@NWFWTFFLOWLEVEL";
            NWFWTFWEATHER.ParameterName = "@NWFWTFWEATHER";




            PUBLISHDATE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.PUBLISHDATE.ToString();
            FORECASTDATE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.FORECASTDATE.ToString();
            NWFWTFWAVEHEIGHT.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFWAVEHEIGHT;
            NWFWTFWAVEDIR.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFWAVEDIR;
            NWFWTFFLOWDIR.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFFLOWDIR;
            NWFWTFFLOWLEVEL.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFFLOWLEVEL;
            NWFWTFWEATHER.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFWEATHER;


            parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, NWFWTFWAVEHEIGHT, NWFWTFWAVEDIR, NWFWTFFLOWDIR, NWFWTFFLOWLEVEL, NWFWTFWEATHER };
            try
            {
                count= DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改南堡油田海域波浪、风预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                count = 0;
            }
        }
        else if (quanxian.ToLower() == "sw")
        {
            sql = "UPDATE   TBLNANPUWAVEFLOWWATERTFORECAST set	NWFWTFWATERTEMPERATURE=@NWFWTFWATERTEMPERATURE where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var NWFWTFWATERTEMPERATURE = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            NWFWTFWATERTEMPERATURE.ParameterName = "@NWFWTFWATERTEMPERATURE";




            PUBLISHDATE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.PUBLISHDATE.ToString();
            FORECASTDATE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.FORECASTDATE.ToString();
            NWFWTFWATERTEMPERATURE.Value = TBLNANPUWAVEFLOWWATERTFORECAST.NWFWTFWATERTEMPERATURE;


            parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, NWFWTFWATERTEMPERATURE };
            try
            {
                count = DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改南堡油田海域水温预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                count = 0;
            }
        }

        return count;


    }


}

