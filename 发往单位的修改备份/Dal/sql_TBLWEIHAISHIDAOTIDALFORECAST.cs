
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 威海石岛区域潮汐预报
/// </summary>
/// <returns></returns>
public class sql_TBLWEIHAISHIDAOTIDALFORECAST
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLWEIHAISHIDAOTIDALFORECAST()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 根据地区获取所有威海石岛区域潮汐预报预报
    /// add by Lian
    /// </summary>
    /// <returns></returns>
    public object get_TBLWEIHAISHIDAOTIDALFORECAST_AllDataByArea(TBLWEIHAISHIDAOTIDALFORECAST TBLWEIHAISHIDAOTIDALFORECAST,string AREANAME)
    {

        try
        {
            return DataExe.GetTableExeData("select * from TBLWEIHAISHIDAOTIDALFORECAST where PUBLISHDATE=to_date('" + TBLWEIHAISHIDAOTIDALFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and REPORTAREA='"+AREANAME+"'");

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取威海石岛区域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 获取所有威海石岛区域潮汐预报预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLWEIHAISHIDAOTIDALFORECAST_AllData(TBLWEIHAISHIDAOTIDALFORECAST TBLWEIHAISHIDAOTIDALFORECAST)
    {

        try
        {
            return DataExe.GetTableExeData("select * from TBLWEIHAISHIDAOTIDALFORECAST where PUBLISHDATE=to_date('" + TBLWEIHAISHIDAOTIDALFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");
          
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取威海石岛区域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    public object get_TBLWEIHAISHIDAOTIDALFORECAST(TBLWEIHAISHIDAOTIDALFORECAST TBLWEIHAISHIDAOTIDALFORECAST,string searchType="p")
    {

        try
        {
            string sql = "";
            if (searchType == "p")
            {
                sql = "select * from TBLWEIHAISHIDAOTIDALFORECAST where PUBLISHDATE=to_date('" + TBLWEIHAISHIDAOTIDALFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            }
            else
            {
                sql = "select * from TBLWEIHAISHIDAOTIDALFORECAST "
                    + " where FORECASTDATE > to_date('" + TBLWEIHAISHIDAOTIDALFORECAST.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + " and FORECASTDATE < to_date('" + TBLWEIHAISHIDAOTIDALFORECAST.FORECASTDATE.AddDays(2).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + " and PUBLISHDATE=to_date('" + TBLWEIHAISHIDAOTIDALFORECAST.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            }
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取威海石岛区域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }
    /// <summary>
    /// 新增威海石岛区域潮汐预报预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLWEIHAISHIDAOTIDALFORECAST(TBLWEIHAISHIDAOTIDALFORECAST TBLWEIHAISHIDAOTIDALFORECAST)
    {

        string sql = "INSERT INTO  TBLWEIHAISHIDAOTIDALFORECAST (PUBLISHDATE,SECONDLOWWAVETIME,SECONDLOWWAVEHEIGHT,REPORTAREA,FORECASTDATE,FIRSTHIGHWAVETIME,FIRSTHIGHWAVEHEIGHT,FIRSTLOWWAVETIME,FIRSTLOWWAVEHEIGHT,SECONDHIGHWAVETIME,SECONDHIGHWAVEHEIGHT) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@SECONDLOWWAVETIME,@SECONDLOWWAVEHEIGHT,@REPORTAREA,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@FIRSTHIGHWAVETIME,@FIRSTHIGHWAVEHEIGHT,@FIRSTLOWWAVETIME,@FIRSTLOWWAVEHEIGHT,@SECONDHIGHWAVETIME,@SECONDHIGHWAVEHEIGHT)";



        var PUBLISHDATE = DataExe.GetDbParameter();
        var SECONDLOWWAVETIME = DataExe.GetDbParameter();
        var SECONDLOWWAVEHEIGHT = DataExe.GetDbParameter();
        var REPORTAREA = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var FIRSTHIGHWAVETIME = DataExe.GetDbParameter();
        var FIRSTHIGHWAVEHEIGHT = DataExe.GetDbParameter();
        var FIRSTLOWWAVETIME = DataExe.GetDbParameter();
        var FIRSTLOWWAVEHEIGHT = DataExe.GetDbParameter();
        var SECONDHIGHWAVETIME = DataExe.GetDbParameter();
        var SECONDHIGHWAVEHEIGHT = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        SECONDLOWWAVETIME.ParameterName = "@SECONDLOWWAVETIME";
        SECONDLOWWAVEHEIGHT.ParameterName = "@SECONDLOWWAVEHEIGHT";
        REPORTAREA.ParameterName = "@REPORTAREA";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        FIRSTHIGHWAVETIME.ParameterName = "@FIRSTHIGHWAVETIME";
        FIRSTHIGHWAVEHEIGHT.ParameterName = "@FIRSTHIGHWAVEHEIGHT";
        FIRSTLOWWAVETIME.ParameterName = "@FIRSTLOWWAVETIME";
        FIRSTLOWWAVEHEIGHT.ParameterName = "@FIRSTLOWWAVEHEIGHT";
        SECONDHIGHWAVETIME.ParameterName = "@SECONDHIGHWAVETIME";
        SECONDHIGHWAVEHEIGHT.ParameterName = "@SECONDHIGHWAVEHEIGHT";




        PUBLISHDATE.Value = TBLWEIHAISHIDAOTIDALFORECAST.PUBLISHDATE.ToString();
        SECONDLOWWAVETIME.Value = TBLWEIHAISHIDAOTIDALFORECAST.SECONDLOWWAVETIME;
        SECONDLOWWAVEHEIGHT.Value = TBLWEIHAISHIDAOTIDALFORECAST.SECONDLOWWAVEHEIGHT;
        REPORTAREA.Value = TBLWEIHAISHIDAOTIDALFORECAST.REPORTAREA;
        FORECASTDATE.Value = TBLWEIHAISHIDAOTIDALFORECAST.FORECASTDATE.ToString();
        FIRSTHIGHWAVETIME.Value = TBLWEIHAISHIDAOTIDALFORECAST.FIRSTHIGHWAVETIME;
        FIRSTHIGHWAVEHEIGHT.Value = TBLWEIHAISHIDAOTIDALFORECAST.FIRSTHIGHWAVEHEIGHT;
        FIRSTLOWWAVETIME.Value = TBLWEIHAISHIDAOTIDALFORECAST.FIRSTLOWWAVETIME;
        FIRSTLOWWAVEHEIGHT.Value = TBLWEIHAISHIDAOTIDALFORECAST.FIRSTLOWWAVEHEIGHT;
        SECONDHIGHWAVETIME.Value = TBLWEIHAISHIDAOTIDALFORECAST.SECONDHIGHWAVETIME;
        SECONDHIGHWAVEHEIGHT.Value = TBLWEIHAISHIDAOTIDALFORECAST.SECONDHIGHWAVEHEIGHT;


        DbParameter[] parameters = { PUBLISHDATE, SECONDLOWWAVETIME, SECONDLOWWAVEHEIGHT, REPORTAREA, FORECASTDATE, FIRSTHIGHWAVETIME, FIRSTHIGHWAVEHEIGHT, FIRSTLOWWAVETIME, FIRSTLOWWAVEHEIGHT, SECONDHIGHWAVETIME, SECONDHIGHWAVEHEIGHT };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增威海石岛区域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改威海石岛区域潮汐预报预报
    /// </summary>
    public int Edit_TBLWEIHAISHIDAOTIDALFORECAST(TBLWEIHAISHIDAOTIDALFORECAST TBLWEIHAISHIDAOTIDALFORECAST)
    {
        string sql = "UPDATE   TBLWEIHAISHIDAOTIDALFORECAST set	SECONDLOWWAVETIME=@SECONDLOWWAVETIME,SECONDLOWWAVEHEIGHT=@SECONDLOWWAVEHEIGHT,FIRSTHIGHWAVETIME=@FIRSTHIGHWAVETIME,FIRSTHIGHWAVEHEIGHT=@FIRSTHIGHWAVEHEIGHT,FIRSTLOWWAVETIME=@FIRSTLOWWAVETIME,FIRSTLOWWAVEHEIGHT=@FIRSTLOWWAVEHEIGHT,SECONDHIGHWAVETIME=@SECONDHIGHWAVETIME,SECONDHIGHWAVEHEIGHT=@SECONDHIGHWAVEHEIGHT where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and REPORTAREA=@REPORTAREA and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";
        var PUBLISHDATE = DataExe.GetDbParameter();
        var SECONDLOWWAVETIME = DataExe.GetDbParameter();
        var SECONDLOWWAVEHEIGHT = DataExe.GetDbParameter();
        var REPORTAREA = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var FIRSTHIGHWAVETIME = DataExe.GetDbParameter();
        var FIRSTHIGHWAVEHEIGHT = DataExe.GetDbParameter();
        var FIRSTLOWWAVETIME = DataExe.GetDbParameter();
        var FIRSTLOWWAVEHEIGHT = DataExe.GetDbParameter();
        var SECONDHIGHWAVETIME = DataExe.GetDbParameter();
        var SECONDHIGHWAVEHEIGHT = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        SECONDLOWWAVETIME.ParameterName = "@SECONDLOWWAVETIME";
        SECONDLOWWAVEHEIGHT.ParameterName = "@SECONDLOWWAVEHEIGHT";
        REPORTAREA.ParameterName = "@REPORTAREA";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        FIRSTHIGHWAVETIME.ParameterName = "@FIRSTHIGHWAVETIME";
        FIRSTHIGHWAVEHEIGHT.ParameterName = "@FIRSTHIGHWAVEHEIGHT";
        FIRSTLOWWAVETIME.ParameterName = "@FIRSTLOWWAVETIME";
        FIRSTLOWWAVEHEIGHT.ParameterName = "@FIRSTLOWWAVEHEIGHT";
        SECONDHIGHWAVETIME.ParameterName = "@SECONDHIGHWAVETIME";
        SECONDHIGHWAVEHEIGHT.ParameterName = "@SECONDHIGHWAVEHEIGHT";




        PUBLISHDATE.Value = TBLWEIHAISHIDAOTIDALFORECAST.PUBLISHDATE.ToString();
        SECONDLOWWAVETIME.Value = TBLWEIHAISHIDAOTIDALFORECAST.SECONDLOWWAVETIME;
        SECONDLOWWAVEHEIGHT.Value = TBLWEIHAISHIDAOTIDALFORECAST.SECONDLOWWAVEHEIGHT;
        REPORTAREA.Value = TBLWEIHAISHIDAOTIDALFORECAST.REPORTAREA;
        FORECASTDATE.Value = TBLWEIHAISHIDAOTIDALFORECAST.FORECASTDATE.ToString();
        FIRSTHIGHWAVETIME.Value = TBLWEIHAISHIDAOTIDALFORECAST.FIRSTHIGHWAVETIME;
        FIRSTHIGHWAVEHEIGHT.Value = TBLWEIHAISHIDAOTIDALFORECAST.FIRSTHIGHWAVEHEIGHT;
        FIRSTLOWWAVETIME.Value = TBLWEIHAISHIDAOTIDALFORECAST.FIRSTLOWWAVETIME;
        FIRSTLOWWAVEHEIGHT.Value = TBLWEIHAISHIDAOTIDALFORECAST.FIRSTLOWWAVEHEIGHT;
        SECONDHIGHWAVETIME.Value = TBLWEIHAISHIDAOTIDALFORECAST.SECONDHIGHWAVETIME;
        SECONDHIGHWAVEHEIGHT.Value = TBLWEIHAISHIDAOTIDALFORECAST.SECONDHIGHWAVEHEIGHT;


        DbParameter[] parameters = { PUBLISHDATE, SECONDLOWWAVETIME, SECONDLOWWAVEHEIGHT, REPORTAREA, FORECASTDATE, FIRSTHIGHWAVETIME, FIRSTHIGHWAVEHEIGHT, FIRSTLOWWAVETIME, FIRSTLOWWAVEHEIGHT, SECONDHIGHWAVETIME, SECONDHIGHWAVEHEIGHT };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改威海石岛区域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }


}

