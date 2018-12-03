
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// sql_TBLYRBHWINDWAVE72HFORECASTTWO 72小时渤海海区及黄河海港风、浪预报
/// </summary>
public class sql_TBLYRBHWINDWAVE72HFORECASTTWO
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLYRBHWINDWAVE72HFORECASTTWO()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }

    /// <summary>
    /// 获取所有72小时渤海海区及黄河海港风、浪预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLYRBHWINDWAVE72HFORECASTTWO_AllData(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo, string searchType = "p")
    {
        try
        {
            if (searchType == "f")

                return DataExe.GetTableExeData("select * from Tblyrbhwindwave72hforecasttwo "
                    + "where FORECASTDATE > to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + "and FORECASTDATE < to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    );
            else
                return DataExe.GetTableExeData("select * from Tblyrbhwindwave72hforecasttwo where PUBLISHDATE=to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取所有72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    public object TBLYRBHWINDWAVE72HFORECASTTWOAllData(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
    {
        try
        {
            string sql = "";
            //if (searchType == "f")
            //{
            //    sql = "select * from Tblyrbhwindwave72hforecasttwo "
            //    + " where FORECASTDATE > to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
            //    + " and FORECASTDATE < to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
            //    + " and PUBLISHDATE = to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            //}
            //else
            //{
                sql = "select * from Tblyrbhwindwave72hforecasttwo where PUBLISHDATE=to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            //}
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取所有72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    public object get_TBLYRBHWINDWAVE72HFORECASTTWO(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
    {
        try
        {
            string sql = "select * from Tblyrbhwindwave72hforecasttwo "
               + " where FORECASTDATE > to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
               + " and FORECASTDATE < to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.AddDays(3).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
               + " and PUBLISHDATE = to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            return DataExe.GetTableExeData(sql);
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取所有72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    /// <summary>
    /// 获取3天数据
    /// </summary>
    /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
    /// <returns></returns>
    public object get_TBLYRBHWINDWAVE72HFORECASTTWO_3Daysata(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
    {
        try
        {
            string sql = "select * from Tblyrbhwindwave72hforecasttwo "
                    + " where FORECASTDATE > to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + " and FORECASTDATE < to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + " and PUBLISHDATE=to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            return DataExe.GetTableExeData(sql);
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取所有72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 检索改预报日期数据是否存在
    /// </summary>
    /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
    /// <returns></returns>
    public object GETTBLYRBHWINDWAVE72HFORECASTTWO(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
    {
        try
        {
            string sql = "select * from Tblyrbhwindwave72hforecasttwo "
                  + "where FORECASTDATE = to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                  + " and REPORTAREA='"+ Tblyrbhwindwave72hforecasttwo.REPORTAREA + "'";
            return DataExe.GetTableExeData(sql);
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取所有72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    /// <summary>
    /// 新增72小时渤海海区及黄河海港风、浪预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLYRBHWINDWAVE72HFORECASTTWO(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
    {

        string sql = "INSERT INTO  TBLYRBHWINDWAVE72HFORECASTTWO (PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL, YRBHWWFWATERTEMPERATURE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@REPORTAREA,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@YRBHWWFWAVEHEIGHT,@YRBHWWFWAVEDIR,@YRBHWWFFLOWDIR,@YRBHWWFFLOWLEVEL,@YRBHWWFWATERTEMPERATURE)";

        var PUBLISHDATE = DataExe.GetDbParameter();
        var REPORTAREA = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var YRBHWWFWAVEHEIGHT = DataExe.GetDbParameter();
        var YRBHWWFWAVEDIR = DataExe.GetDbParameter();
        var YRBHWWFFLOWDIR = DataExe.GetDbParameter();
        var YRBHWWFFLOWLEVEL = DataExe.GetDbParameter();
        var YRBHWWFWATERTEMPERATURE = DataExe.GetDbParameter();
        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        REPORTAREA.ParameterName = "@REPORTAREA";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        YRBHWWFWAVEHEIGHT.ParameterName = "@YRBHWWFWAVEHEIGHT";
        YRBHWWFWAVEDIR.ParameterName = "@YRBHWWFWAVEDIR";
        YRBHWWFFLOWDIR.ParameterName = "@YRBHWWFFLOWDIR";
        YRBHWWFFLOWLEVEL.ParameterName = "@YRBHWWFFLOWLEVEL";
        YRBHWWFWATERTEMPERATURE.ParameterName = "@YRBHWWFWATERTEMPERATURE";
        PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
        REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
        FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
        YRBHWWFWAVEHEIGHT.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEHEIGHT;
        YRBHWWFWAVEDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEDIR.ToString();
        YRBHWWFFLOWDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWDIR.ToString();
        YRBHWWFFLOWLEVEL.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWLEVEL;
        YRBHWWFWATERTEMPERATURE.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWATERTEMPERATURE;
        DbParameter[] parameters = { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL, YRBHWWFWATERTEMPERATURE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
          /// 修改72小时渤海海区及黄河海港风、浪预报
          /// </summary>
          /// <param name="Anquaucelue"></param>
          /// <returns></returns>
    public int Edit_TBLYRBHWINDWAVE72HFORECASTTWO(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
    {
        string sql = "UPDATE   TBLYRBHWINDWAVE72HFORECASTTWO set  YRBHWWFWAVEHEIGHT=@YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR=@YRBHWWFWAVEDIR, YRBHWWFFLOWDIR=@YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL=@YRBHWWFFLOWLEVEL, YRBHWWFWATERTEMPERATURE=@YRBHWWFWATERTEMPERATURE where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and REPORTAREA=@REPORTAREA and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";

        var PUBLISHDATE = DataExe.GetDbParameter();
        var REPORTAREA = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var YRBHWWFWAVEHEIGHT = DataExe.GetDbParameter();
        var YRBHWWFWAVEDIR = DataExe.GetDbParameter();
        var YRBHWWFFLOWDIR = DataExe.GetDbParameter();
        var YRBHWWFFLOWLEVEL = DataExe.GetDbParameter();
        var YRBHWWFWATERTEMPERATURE = DataExe.GetDbParameter();
        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        REPORTAREA.ParameterName = "@REPORTAREA";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        YRBHWWFWAVEHEIGHT.ParameterName = "@YRBHWWFWAVEHEIGHT";
        YRBHWWFWAVEDIR.ParameterName = "@YRBHWWFWAVEDIR";
        YRBHWWFFLOWDIR.ParameterName = "@YRBHWWFFLOWDIR";
        YRBHWWFFLOWLEVEL.ParameterName = "@YRBHWWFFLOWLEVEL";
        YRBHWWFWATERTEMPERATURE.ParameterName = "@YRBHWWFWATERTEMPERATURE";
        PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
        REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
        FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
        YRBHWWFWAVEHEIGHT.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEHEIGHT;
        YRBHWWFWAVEDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEDIR.ToString();
        YRBHWWFFLOWDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWDIR.ToString();
        YRBHWWFFLOWLEVEL.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWLEVEL;
        YRBHWWFWATERTEMPERATURE.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWATERTEMPERATURE;
        DbParameter[] parameters = { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL, YRBHWWFWATERTEMPERATURE};

        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 根据预报日期修改数据
    /// </summary>
    /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
    /// <returns></returns>
    public int EditTBLYRBHWINDWAVE72HFORECASTTWO(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo,string quanxian)
    {
        string sql = "";
        DbParameter[] parameters = null;
        if (quanxian == "fl")
        {
            sql = "UPDATE   TBLYRBHWINDWAVE72HFORECASTTWO set  "
            + "YRBHWWFWAVEHEIGHT=@YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR=@YRBHWWFWAVEDIR, YRBHWWFFLOWDIR=@YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL=@YRBHWWFFLOWLEVEL "
            + "where  PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and REPORTAREA=@REPORTAREA and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var REPORTAREA = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var YRBHWWFWAVEHEIGHT = DataExe.GetDbParameter();
            var YRBHWWFWAVEDIR = DataExe.GetDbParameter();
            var YRBHWWFFLOWDIR = DataExe.GetDbParameter();
            var YRBHWWFFLOWLEVEL = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            REPORTAREA.ParameterName = "@REPORTAREA";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            YRBHWWFWAVEHEIGHT.ParameterName = "@YRBHWWFWAVEHEIGHT";
            YRBHWWFWAVEDIR.ParameterName = "@YRBHWWFWAVEDIR";
            YRBHWWFFLOWDIR.ParameterName = "@YRBHWWFFLOWDIR";
            YRBHWWFFLOWLEVEL.ParameterName = "@YRBHWWFFLOWLEVEL";

            PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
            REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
            FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
            YRBHWWFWAVEHEIGHT.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEHEIGHT;
            YRBHWWFWAVEDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEDIR.ToString();
            YRBHWWFFLOWDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWDIR.ToString();
            YRBHWWFFLOWLEVEL.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWLEVEL;

            DbParameter[] parametersFL = { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL };
            parameters = parametersFL;
        }
        else if (quanxian == "sw")
        {
            sql = "UPDATE   TBLYRBHWINDWAVE72HFORECASTTWO set  "
            + " YRBHWWFWATERTEMPERATURE=@YRBHWWFWATERTEMPERATURE "
            + "where   PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and REPORTAREA=@REPORTAREA and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var YRBHWWFWATERTEMPERATURE = DataExe.GetDbParameter();
            var REPORTAREA = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();

            YRBHWWFWATERTEMPERATURE.ParameterName = "@YRBHWWFWATERTEMPERATURE";
            REPORTAREA.ParameterName = "@REPORTAREA";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            PUBLISHDATE.ParameterName = "@PUBLISHDATE";

            PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
            YRBHWWFWATERTEMPERATURE.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWATERTEMPERATURE;
            REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
            FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();

            DbParameter[] parametersSW = { PUBLISHDATE, REPORTAREA, FORECASTDATE,YRBHWWFWATERTEMPERATURE };
            parameters = parametersSW;
        }
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改72小时渤海海区及黄河海港风、浪预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
}
