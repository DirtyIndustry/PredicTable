using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 渔政局表2
/// </summary>
public class sql_TBLYZJFORECAST
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLYZJFORECAST()
    {
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
    }

    public object get_TBLYZJFORECAST(TBLYZJFORECAST mode)
    {
        string sql = "select * from TBLYZJFORECAST where PUBLISHDATE=to_date('" + mode.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24@mi@ss') order by  SEAAREA,FORECASTDATE ";
        try
        {
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取渔政局预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
        
    }

    /// <summary>
    /// 渔政局责任海区
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public object GetYZJZRArea(TBLYZJFORECAST mode)
    {
        string sql = "select * from TBLYZJFORECAST where PUBLISHDATE=to_date('" + mode.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24@mi@ss') AND WEATHERAPPEARANCE='bgdhq' order by  SEAAREA,FORECASTDATE ";
        try
        {
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取渔政局预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public object delete_TBLYZJFORECAST(TBLYZJFORECAST mode)
    {
        string sql = "delete from TBLYZJFORECAST where PUBLISHDATE=to_date('" + mode.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24@mi@ss')  ";
        try
        {
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取渔政局预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    public int Add_TBLYZJFORECAST(TBLYZJFORECAST mode, string quanxian)
    {
        string sql = null;
        DbParameter[] parameters = null;
        if (quanxian.ToLower() == "fl")
        {
            sql = @"INSERT INTO  TBLYZJFORECAST(PUBLISHDATE,FORECASTDATE,SEAAREA,WINDDIRECTION,WINDFORCE,WAVEHEIGHT) 
                VALUES(to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@FORECASTDATE,@SEAAREA,@WINDDIRECTION,@WINDFORCE,@WAVEHEIGHT)";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var SEAAREA = DataExe.GetDbParameter();
            var WINDDIRECTION = DataExe.GetDbParameter();
            var WINDFORCE = DataExe.GetDbParameter();
            var WAVEHEIGHT = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            SEAAREA.ParameterName = "@SEAAREA";
            WINDDIRECTION.ParameterName = "@WINDDIRECTION";
            WINDFORCE.ParameterName = "@WINDFORCE";
            WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";

            PUBLISHDATE.Value = mode.PUBLISHDATE.ToString("yyyy-MM-dd HH:mm:ss");
            FORECASTDATE.Value = mode.FORECASTDATE;
            SEAAREA.Value = mode.SEAAREA;
            WINDDIRECTION.Value = mode.WINDDIRECTION;
            WINDFORCE.Value = mode.WINDFORCE;
            WAVEHEIGHT.Value = mode.WAVEHEIGHT;

            parameters = new DbParameter[] { PUBLISHDATE, FORECASTDATE, SEAAREA, WINDDIRECTION, WINDFORCE, WAVEHEIGHT };
        }
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增渔政局预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    public int Edit_TBLYZJFORECAST(TBLYZJFORECAST mode, string quanxian)
    {
        string sql = null;
        DbParameter[] parameters = null;
        if (quanxian.ToLower() == "fl")
        {
//            sql = @"UPDATE  TBLZHCFORECAST SET WINDDIRECTION=@WINDDIRECTION,WINDFORCE=@WINDFORCE,WAVEHEIGHT=@WAVEHEIGHT
//WHERE to_char(publishdate, 'dd-mm-yyyy')='" + mode.PUBLISHDATE.ToString("dd-MM-yyyy") + "' AND to_char(FORECASTDATE, 'dd-mm-yyyy')='" + mode.FORECASTDATE.ToString("dd-MM-yyyy") + "' AND SEAAREA=@SEAAREA";

            sql = "UPDATE (select * from (select WINDDIRECTION,WINDFORCE,WAVEHEIGHT,to_char(publishdate, 'dd-mm-yyyy') as publishtime,"
                + " to_char(FORECASTDATE, 'dd-mm-yyyy  HH:mm:ss') as forecasedate,SEAAREA from tblyzjforecast) where publishtime='" + mode.PUBLISHDATE.ToString("dd-MM-yyyy  HH:mm:ss") + "' and forecasedate = '"+ mode.FORECASTDATE.ToString("dd-MM-yyyy") + "' AND SEAAREA=@SEAAREA) A"
                + " SET WINDDIRECTION=@WINDDIRECTION,WINDFORCE=@WINDFORCE,WAVEHEIGHT=@WAVEHEIGHT";
            var PUBLISHDATE = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var SEAAREA = DataExe.GetDbParameter();
            var WINDDIRECTION = DataExe.GetDbParameter();
            var WINDFORCE = DataExe.GetDbParameter();
            var WAVEHEIGHT = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            SEAAREA.ParameterName = "@SEAAREA";
            WINDDIRECTION.ParameterName = "@WINDDIRECTION";
            WINDFORCE.ParameterName = "@WINDFORCE";
            WAVEHEIGHT.ParameterName = "@WAVEHEIGHT";

            PUBLISHDATE.Value = mode.PUBLISHDATE.ToString("dd-MM-yyyy");
            FORECASTDATE.Value = mode.FORECASTDATE.ToString("dd-MM-yyyy");
            SEAAREA.Value = mode.SEAAREA;
            WINDDIRECTION.Value = mode.WINDDIRECTION;
            WINDFORCE.Value = mode.WINDFORCE;
            WAVEHEIGHT.Value = mode.WAVEHEIGHT;

            parameters = new DbParameter[] { WINDDIRECTION, WINDFORCE, WAVEHEIGHT, SEAAREA };
        }
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增渔政局预报出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 获取海区数据
    /// </summary>
    /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
    /// <param name="searchType"></param>
    /// <returns></returns>
    public object TBLYZJFORECAST(TBLYZJFORECAST tblyzjforeast, string searchType = "p")
    {
        try
        {
            string sql = "select * from TBLYZJFORECAST where PUBLISHDATE=to_date('" + tblyzjforeast.PUBLISHDATE.ToString("yyyy-MM-dd  HH:mm:ss") + "', 'yyyy-mm-dd hh24@mi@ss')  order by  SEAAREA,FORECASTDATE";
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取所有海区出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
}
