
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 金沙滩24小时潮汐预报
/// </summary>
/// <returns></returns>
public class sql_TBLGOLDBEACH24HTIDALFORECAST
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLGOLDBEACH24HTIDALFORECAST()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 获取所有金沙滩72小时潮汐预报预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLGOLDBEACH24HTIDALFORECAST_AllData(TBLGOLDBEACH72HTIDALFORECAST TBLGOLDBEACH72HTIDALFORECAST)
    {

        try
        {
                                                          
            return DataExe.GetTableExeData("select * from TBLGOLDBEACH72HTIDALFORECAST where PUBLISHDATE=to_date('" + TBLGOLDBEACH72HTIDALFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取金沙滩72小时潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 获取24小时数据
    /// 生成预报单时使用
    /// </summary>
    /// <param name="TBLGOLDBEACH72HTIDALFORECAST"></param>
    /// <returns></returns>
    public object get24HourTideData(TBLGOLDBEACH72HTIDALFORECAST TBLGOLDBEACH72HTIDALFORECAST)
    {
        try
        {
            string str = "select * from TBLGOLDBEACH72HTIDALFORECAST where PUBLISHDATE=to_date('" + TBLGOLDBEACH72HTIDALFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date('" + TBLGOLDBEACH72HTIDALFORECAST.PUBLISHDATE.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            return DataExe.GetTableExeData(str);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取金沙滩72小时潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 新增金沙滩24小时潮汐预报预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLGOLDBEACH24HTIDALFORECAST(TBLGOLDBEACH72HTIDALFORECAST TBLGOLDBEACH72HTIDALFORECAST)
    {

        string sql = "INSERT INTO  TBLGOLDBEACH72HTIDALFORECAST (PUBLISHDATE,FORECASTDATE,SEABEACH,FIRSTHIGHTIME,FIRSTHIGHLEVEL,SECONDHIGHTIME,SECONDHEIGHTLEVEL,FIRSTLOWTIME,FIRSTLOWLEVEL,SECONDLOWTIME,SECONDLOWLEVEL) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@SEABEACH,@FIRSTHIGHTIME,@FIRSTHIGHLEVEL,@SECONDHIGHTIME,@SECONDHEIGHTLEVEL,@FIRSTLOWTIME,@FIRSTLOWLEVEL,@SECONDLOWTIME,@SECONDLOWLEVEL)";



        var PUBLISHDATE = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var SEABEACH = DataExe.GetDbParameter();
        var FIRSTHIGHTIME = DataExe.GetDbParameter();
        var FIRSTHIGHLEVEL = DataExe.GetDbParameter();
        var SECONDHIGHTIME = DataExe.GetDbParameter();
        var SECONDHEIGHTLEVEL = DataExe.GetDbParameter();
        var FIRSTLOWTIME = DataExe.GetDbParameter();
        var FIRSTLOWLEVEL = DataExe.GetDbParameter();
        var SECONDLOWTIME = DataExe.GetDbParameter();
        var SECONDLOWLEVEL = DataExe.GetDbParameter();


        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        SEABEACH.ParameterName = "@SEABEACH";
        FIRSTHIGHTIME.ParameterName = "@FIRSTHIGHTIME";
        FIRSTHIGHLEVEL.ParameterName = "@FIRSTHIGHLEVEL";
        SECONDHIGHTIME.ParameterName = "@SECONDHIGHTIME";
        SECONDHEIGHTLEVEL.ParameterName = "@SECONDHEIGHTLEVEL";
        FIRSTLOWTIME.ParameterName = "@FIRSTLOWTIME";
        FIRSTLOWLEVEL.ParameterName = "@FIRSTLOWLEVEL";
        SECONDLOWTIME.ParameterName = "@SECONDLOWTIME";
        SECONDLOWLEVEL.ParameterName = "@SECONDLOWLEVEL";
        

        PUBLISHDATE.Value = TBLGOLDBEACH72HTIDALFORECAST.PUBLISHDATE.ToString();
        FORECASTDATE.Value = TBLGOLDBEACH72HTIDALFORECAST.FORECASTDATE.ToString();
        SEABEACH.Value = TBLGOLDBEACH72HTIDALFORECAST.SEABEACH;
        FIRSTHIGHTIME.Value = TBLGOLDBEACH72HTIDALFORECAST.FIRSTHIGHTIME;
        FIRSTHIGHLEVEL.Value = TBLGOLDBEACH72HTIDALFORECAST.FIRSTHIGHLEVEL;
        SECONDHIGHTIME.Value = TBLGOLDBEACH72HTIDALFORECAST.SECONDHIGHTIME;
        SECONDHEIGHTLEVEL.Value = TBLGOLDBEACH72HTIDALFORECAST.SECONDHEIGHTLEVEL;
        FIRSTLOWTIME.Value = TBLGOLDBEACH72HTIDALFORECAST.FIRSTLOWTIME;
        FIRSTLOWLEVEL.Value = TBLGOLDBEACH72HTIDALFORECAST.FIRSTLOWLEVEL;
        SECONDLOWTIME.Value = TBLGOLDBEACH72HTIDALFORECAST.SECONDLOWTIME;
        SECONDLOWLEVEL.Value = TBLGOLDBEACH72HTIDALFORECAST.SECONDLOWLEVEL;

        DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, SEABEACH, FIRSTHIGHTIME, FIRSTHIGHLEVEL, SECONDHIGHTIME, SECONDHEIGHTLEVEL, FIRSTLOWTIME, FIRSTLOWLEVEL, SECONDLOWTIME, SECONDLOWLEVEL };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增金沙滩72小时潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改金沙滩24小时潮汐预报预报
    /// </summary>
    public int Edit_TBLGOLDBEACH24HTIDALFORECAST(TBLGOLDBEACH72HTIDALFORECAST TBLGOLDBEACH72HTIDALFORECAST)
    {
        string sql = "UPDATE TBLGOLDBEACH72HTIDALFORECAST set FIRSTHIGHTIME=@FIRSTHIGHTIME,FIRSTHIGHLEVEL=@FIRSTHIGHLEVEL,SECONDHIGHTIME=@SECONDHIGHTIME,SECONDHEIGHTLEVEL=@SECONDHEIGHTLEVEL,FIRSTLOWTIME=@FIRSTLOWTIME,FIRSTLOWLEVEL=@FIRSTLOWLEVEL,SECONDLOWTIME=@SECONDLOWTIME,SECONDLOWLEVEL=@SECONDLOWLEVEL where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss') and SEABEACH=@SEABEACH";


        var PUBLISHDATE = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var SEABEACH = DataExe.GetDbParameter();
        var FIRSTHIGHTIME = DataExe.GetDbParameter();
        var FIRSTHIGHLEVEL = DataExe.GetDbParameter();
        var SECONDHIGHTIME = DataExe.GetDbParameter();
        var SECONDHEIGHTLEVEL = DataExe.GetDbParameter();
        var FIRSTLOWTIME = DataExe.GetDbParameter();
        var FIRSTLOWLEVEL = DataExe.GetDbParameter();
        var SECONDLOWTIME = DataExe.GetDbParameter();
        var SECONDLOWLEVEL = DataExe.GetDbParameter();
        

        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        SEABEACH.ParameterName = "@SEABEACH";
        FIRSTHIGHTIME.ParameterName = "@FIRSTHIGHTIME";
        FIRSTHIGHLEVEL.ParameterName = "@FIRSTHIGHLEVEL";
        SECONDHIGHTIME.ParameterName = "@SECONDHIGHTIME";
        SECONDHEIGHTLEVEL.ParameterName = "@SECONDHEIGHTLEVEL";
        FIRSTLOWTIME.ParameterName = "@FIRSTLOWTIME";
        FIRSTLOWLEVEL.ParameterName = "@FIRSTLOWLEVEL";
        SECONDLOWTIME.ParameterName = "@SECONDLOWTIME";
        SECONDLOWLEVEL.ParameterName = "@SECONDLOWLEVEL";
        

        PUBLISHDATE.Value = TBLGOLDBEACH72HTIDALFORECAST.PUBLISHDATE.ToString();
        FORECASTDATE.Value = TBLGOLDBEACH72HTIDALFORECAST.FORECASTDATE.ToString();
        SEABEACH.Value = TBLGOLDBEACH72HTIDALFORECAST.SEABEACH;
        FIRSTHIGHTIME.Value = TBLGOLDBEACH72HTIDALFORECAST.FIRSTHIGHTIME;
        FIRSTHIGHLEVEL.Value = TBLGOLDBEACH72HTIDALFORECAST.FIRSTHIGHLEVEL;
        SECONDHIGHTIME.Value = TBLGOLDBEACH72HTIDALFORECAST.SECONDHIGHTIME;
        SECONDHEIGHTLEVEL.Value = TBLGOLDBEACH72HTIDALFORECAST.SECONDHEIGHTLEVEL;
        FIRSTLOWTIME.Value = TBLGOLDBEACH72HTIDALFORECAST.FIRSTLOWTIME;
        FIRSTLOWLEVEL.Value = TBLGOLDBEACH72HTIDALFORECAST.FIRSTLOWLEVEL;
        SECONDLOWTIME.Value = TBLGOLDBEACH72HTIDALFORECAST.SECONDLOWTIME;
        SECONDLOWLEVEL.Value = TBLGOLDBEACH72HTIDALFORECAST.SECONDLOWLEVEL;
        

        DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, SEABEACH, FIRSTHIGHTIME, FIRSTHIGHLEVEL, SECONDHIGHTIME, SECONDHEIGHTLEVEL, FIRSTLOWTIME, FIRSTLOWLEVEL, SECONDLOWTIME, SECONDLOWLEVEL };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改金沙滩72小时潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }


}

