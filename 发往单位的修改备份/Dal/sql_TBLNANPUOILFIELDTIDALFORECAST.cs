﻿
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 南堡油田海域潮汐预报
/// </summary>
/// <returns></returns>
public class sql_TBLNANPUOILFIELDTIDALFORECAST
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLNANPUOILFIELDTIDALFORECAST()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 获取所有南堡油田海域潮汐预报预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLNANPUOILFIELDTIDALFORECAST_AllData(TBLNANPUOILFIELDTIDALFORECAST TBLNANPUOILFIELDTIDALFORECAST)
    {

        try
        {
            return DataExe.GetTableExeData("select * from TBLNANPUOILFIELDTIDALFORECAST where PUBLISHDATE=to_date('" + TBLNANPUOILFIELDTIDALFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取南堡油田海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    public object get_TBLNANPUOILFIELDTIDALFORECAST(TBLNANPUOILFIELDTIDALFORECAST TBLNANPUOILFIELDTIDALFORECAST,string searchType)
    {

        try
        {
            string sql = "";
            if (searchType == "p")
            {
                sql = "select * from TBLNANPUOILFIELDTIDALFORECAST where PUBLISHDATE=to_date('" + TBLNANPUOILFIELDTIDALFORECAST.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            }
            else
            {
                sql = "select * from TBLNANPUOILFIELDTIDALFORECAST "
                    + " where FORECASTDATE > to_date('" + TBLNANPUOILFIELDTIDALFORECAST.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + " and FORECASTDATE < to_date('" + TBLNANPUOILFIELDTIDALFORECAST.FORECASTDATE.AddDays(3).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + " and PUBLISHDATE=to_date('" + TBLNANPUOILFIELDTIDALFORECAST.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            }
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取南堡油田海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 新增南堡油田海域潮汐预报预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLNANPUOILFIELDTIDALFORECAST(TBLNANPUOILFIELDTIDALFORECAST TBLNANPUOILFIELDTIDALFORECAST)
    {

        string sql = "INSERT INTO  TBLNANPUOILFIELDTIDALFORECAST (PUBLISHDATE,NOTFSECONDLOWWAVEHEIGHT,FORECASTDATE,NOTFFIRSTHIGHWAVETIME,NOTFFIRSTHIGHWAVEHEIGHT,NOTFFIRSTLOWWAVETIME,NOTFFIRSTLOWWAVEHEIGHT,NOTFSECONDHIGHWAVETIME,NOTFSECONDHIGHWAVEHEIGHT,NOTFSECONDLOWWAVETIME) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@NOTFSECONDLOWWAVEHEIGHT,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@NOTFFIRSTHIGHWAVETIME,@NOTFFIRSTHIGHWAVEHEIGHT,@NOTFFIRSTLOWWAVETIME,@NOTFFIRSTLOWWAVEHEIGHT,@NOTFSECONDHIGHWAVETIME,@NOTFSECONDHIGHWAVEHEIGHT,@NOTFSECONDLOWWAVETIME)";



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




        PUBLISHDATE.Value = TBLNANPUOILFIELDTIDALFORECAST.PUBLISHDATE.ToString();
        NOTFSECONDLOWWAVEHEIGHT.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFSECONDLOWWAVEHEIGHT;
        FORECASTDATE.Value = TBLNANPUOILFIELDTIDALFORECAST.FORECASTDATE.ToString();
        NOTFFIRSTHIGHWAVETIME.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFFIRSTHIGHWAVETIME;
        NOTFFIRSTHIGHWAVEHEIGHT.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFFIRSTHIGHWAVEHEIGHT;
        NOTFFIRSTLOWWAVETIME.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFFIRSTLOWWAVETIME;
        NOTFFIRSTLOWWAVEHEIGHT.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFFIRSTLOWWAVEHEIGHT;
        NOTFSECONDHIGHWAVETIME.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFSECONDHIGHWAVETIME;
        NOTFSECONDHIGHWAVEHEIGHT.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFSECONDHIGHWAVEHEIGHT;
        NOTFSECONDLOWWAVETIME.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFSECONDLOWWAVETIME;


        DbParameter[] parameters = { PUBLISHDATE, NOTFSECONDLOWWAVEHEIGHT, FORECASTDATE, NOTFFIRSTHIGHWAVETIME, NOTFFIRSTHIGHWAVEHEIGHT, NOTFFIRSTLOWWAVETIME, NOTFFIRSTLOWWAVEHEIGHT, NOTFSECONDHIGHWAVETIME, NOTFSECONDHIGHWAVEHEIGHT, NOTFSECONDLOWWAVETIME };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增南堡油田海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改南堡油田海域潮汐预报预报
    /// </summary>
    public int Edit_TBLNANPUOILFIELDTIDALFORECAST(TBLNANPUOILFIELDTIDALFORECAST TBLNANPUOILFIELDTIDALFORECAST)
    {
        string sql = "UPDATE   TBLNANPUOILFIELDTIDALFORECAST set  NOTFSECONDLOWWAVEHEIGHT=@NOTFSECONDLOWWAVEHEIGHT,NOTFFIRSTHIGHWAVETIME=@NOTFFIRSTHIGHWAVETIME,NOTFFIRSTHIGHWAVEHEIGHT=@NOTFFIRSTHIGHWAVEHEIGHT,NOTFFIRSTLOWWAVETIME=@NOTFFIRSTLOWWAVETIME,NOTFFIRSTLOWWAVEHEIGHT=@NOTFFIRSTLOWWAVEHEIGHT,NOTFSECONDHIGHWAVETIME=@NOTFSECONDHIGHWAVETIME,NOTFSECONDHIGHWAVEHEIGHT=@NOTFSECONDHIGHWAVEHEIGHT,NOTFSECONDLOWWAVETIME=@NOTFSECONDLOWWAVETIME where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


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




        PUBLISHDATE.Value = TBLNANPUOILFIELDTIDALFORECAST.PUBLISHDATE.ToString();
        NOTFSECONDLOWWAVEHEIGHT.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFSECONDLOWWAVEHEIGHT;
        FORECASTDATE.Value = TBLNANPUOILFIELDTIDALFORECAST.FORECASTDATE.ToString();
        NOTFFIRSTHIGHWAVETIME.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFFIRSTHIGHWAVETIME;
        NOTFFIRSTHIGHWAVEHEIGHT.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFFIRSTHIGHWAVEHEIGHT;
        NOTFFIRSTLOWWAVETIME.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFFIRSTLOWWAVETIME;
        NOTFFIRSTLOWWAVEHEIGHT.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFFIRSTLOWWAVEHEIGHT;
        NOTFSECONDHIGHWAVETIME.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFSECONDHIGHWAVETIME;
        NOTFSECONDHIGHWAVEHEIGHT.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFSECONDHIGHWAVEHEIGHT;
        NOTFSECONDLOWWAVETIME.Value = TBLNANPUOILFIELDTIDALFORECAST.NOTFSECONDLOWWAVETIME;


        DbParameter[] parameters = { PUBLISHDATE, NOTFSECONDLOWWAVEHEIGHT, FORECASTDATE, NOTFFIRSTHIGHWAVETIME, NOTFFIRSTHIGHWAVEHEIGHT, NOTFFIRSTLOWWAVETIME, NOTFFIRSTLOWWAVEHEIGHT, NOTFSECONDHIGHWAVETIME, NOTFSECONDHIGHWAVEHEIGHT, NOTFSECONDLOWWAVETIME };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改南堡油田海域潮汐预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }


}

