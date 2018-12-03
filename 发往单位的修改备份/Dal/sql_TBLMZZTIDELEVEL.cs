
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 明泽闸潮位
/// </summary>
/// <returns></returns>
public class sql_TBLMZZTIDELEVEL
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLMZZTIDELEVEL()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 获取所有明泽闸潮位预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLMZZTIDELEVEL_AllData(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
    {

        try
        {
            return DataExe.GetTableExeData("select * from TBLMZZTIDELEVEL where PUBLISHDATE=to_date('" + TBLMZZTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");
          
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取明泽闸潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
   }

    public object GETTBLMZZTIDELEVELDATA(TBLMZZTIDELEVEL TBLMZZTIDELEVEL,string searchType)
    {

        try
        {
            string sql = "";
            if (searchType == "p")
            {
                sql = "select * from TBLMZZTIDELEVEL where PUBLISHDATE=to_date('" + TBLMZZTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            }
            else
            {
                sql = "select * from TBLMZZTIDELEVEL "
                    + " where FORECASTDATE > to_date('" + TBLMZZTIDELEVEL.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + " and FORECASTDATE < to_date('" + TBLMZZTIDELEVEL.FORECASTDATE.AddDays(3).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + " and PUBLISHDATE=to_date('" + TBLMZZTIDELEVEL.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";

            }
            return DataExe.GetTableExeData(sql);
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取明泽闸潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 新增明泽闸潮位预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLMZZTIDELEVEL(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
    {

        string sql = "INSERT INTO  TBLMZZTIDELEVEL (PUBLISHDATE,MZZTLLOWTIDELEVELFORTHESECONDT,FORECASTDATE,MZZTLFIRSTWAVEOFTIME,MZZTLFIRSTWAVETIDELEVEL,MZZTLFIRSTTIMELOWTIDE,MZZTLLOWTIDELEVELFORTHEFIRSTTI,MZZTLSECONDWAVEOFTIME,MZZTLSECONDWAVETIDELEVEL,MZZTLSECONDTIMELOWTIDE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@MZZTLLOWTIDELEVELFORTHESECONDT,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@MZZTLFIRSTWAVEOFTIME,@MZZTLFIRSTWAVETIDELEVEL,@MZZTLFIRSTTIMELOWTIDE,@MZZTLLOWTIDELEVELFORTHEFIRSTTI,@MZZTLSECONDWAVEOFTIME,@MZZTLSECONDWAVETIDELEVEL,@MZZTLSECONDTIMELOWTIDE)";



        var PUBLISHDATE = DataExe.GetDbParameter();
        var MZZTLLOWTIDELEVELFORTHESECONDT = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var MZZTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
        var MZZTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
        var MZZTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
        var MZZTLLOWTIDELEVELFORTHEFIRSTTI = DataExe.GetDbParameter();
        var MZZTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
        var MZZTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();
        var MZZTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        MZZTLLOWTIDELEVELFORTHESECONDT.ParameterName = "@MZZTLLOWTIDELEVELFORTHESECONDT";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        MZZTLFIRSTWAVEOFTIME.ParameterName = "@MZZTLFIRSTWAVEOFTIME";
        MZZTLFIRSTWAVETIDELEVEL.ParameterName = "@MZZTLFIRSTWAVETIDELEVEL";
        MZZTLFIRSTTIMELOWTIDE.ParameterName = "@MZZTLFIRSTTIMELOWTIDE";
        MZZTLLOWTIDELEVELFORTHEFIRSTTI.ParameterName = "@MZZTLLOWTIDELEVELFORTHEFIRSTTI";
        MZZTLSECONDWAVEOFTIME.ParameterName = "@MZZTLSECONDWAVEOFTIME";
        MZZTLSECONDWAVETIDELEVEL.ParameterName = "@MZZTLSECONDWAVETIDELEVEL";
        MZZTLSECONDTIMELOWTIDE.ParameterName = "@MZZTLSECONDTIMELOWTIDE";




        PUBLISHDATE.Value = TBLMZZTIDELEVEL.PUBLISHDATE.ToString();
        MZZTLLOWTIDELEVELFORTHESECONDT.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHESECONDT;
        FORECASTDATE.Value = TBLMZZTIDELEVEL.FORECASTDATE.ToString();
        MZZTLFIRSTWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVEOFTIME;
        MZZTLFIRSTWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVETIDELEVEL;
        MZZTLFIRSTTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLFIRSTTIMELOWTIDE;
        MZZTLLOWTIDELEVELFORTHEFIRSTTI.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHEFIRSTTI;
        MZZTLSECONDWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVEOFTIME;
        MZZTLSECONDWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVETIDELEVEL;
        MZZTLSECONDTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLSECONDTIMELOWTIDE;


        DbParameter[] parameters = { PUBLISHDATE, MZZTLLOWTIDELEVELFORTHESECONDT, FORECASTDATE, MZZTLFIRSTWAVEOFTIME, MZZTLFIRSTWAVETIDELEVEL, MZZTLFIRSTTIMELOWTIDE, MZZTLLOWTIDELEVELFORTHEFIRSTTI, MZZTLSECONDWAVEOFTIME, MZZTLSECONDWAVETIDELEVEL, MZZTLSECONDTIMELOWTIDE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增明泽闸潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改明泽闸潮位预报
    /// </summary>
    public int Edit_TBLMZZTIDELEVEL(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
    {
        string sql = "UPDATE   TBLMZZTIDELEVEL set	MZZTLLOWTIDELEVELFORTHESECONDT=@MZZTLLOWTIDELEVELFORTHESECONDT,MZZTLFIRSTWAVEOFTIME=@MZZTLFIRSTWAVEOFTIME,MZZTLFIRSTWAVETIDELEVEL=@MZZTLFIRSTWAVETIDELEVEL,MZZTLFIRSTTIMELOWTIDE=@MZZTLFIRSTTIMELOWTIDE,MZZTLLOWTIDELEVELFORTHEFIRSTTI=@MZZTLLOWTIDELEVELFORTHEFIRSTTI,MZZTLSECONDWAVEOFTIME=@MZZTLSECONDWAVEOFTIME,MZZTLSECONDWAVETIDELEVEL=@MZZTLSECONDWAVETIDELEVEL,MZZTLSECONDTIMELOWTIDE=@MZZTLSECONDTIMELOWTIDE where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


        var PUBLISHDATE = DataExe.GetDbParameter();
        var MZZTLLOWTIDELEVELFORTHESECONDT = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var MZZTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
        var MZZTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
        var MZZTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
        var MZZTLLOWTIDELEVELFORTHEFIRSTTI = DataExe.GetDbParameter();
        var MZZTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
        var MZZTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();
        var MZZTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        MZZTLLOWTIDELEVELFORTHESECONDT.ParameterName = "@MZZTLLOWTIDELEVELFORTHESECONDT";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        MZZTLFIRSTWAVEOFTIME.ParameterName = "@MZZTLFIRSTWAVEOFTIME";
        MZZTLFIRSTWAVETIDELEVEL.ParameterName = "@MZZTLFIRSTWAVETIDELEVEL";
        MZZTLFIRSTTIMELOWTIDE.ParameterName = "@MZZTLFIRSTTIMELOWTIDE";
        MZZTLLOWTIDELEVELFORTHEFIRSTTI.ParameterName = "@MZZTLLOWTIDELEVELFORTHEFIRSTTI";
        MZZTLSECONDWAVEOFTIME.ParameterName = "@MZZTLSECONDWAVEOFTIME";
        MZZTLSECONDWAVETIDELEVEL.ParameterName = "@MZZTLSECONDWAVETIDELEVEL";
        MZZTLSECONDTIMELOWTIDE.ParameterName = "@MZZTLSECONDTIMELOWTIDE";




        PUBLISHDATE.Value = TBLMZZTIDELEVEL.PUBLISHDATE.ToString();
        MZZTLLOWTIDELEVELFORTHESECONDT.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHESECONDT;
        FORECASTDATE.Value = TBLMZZTIDELEVEL.FORECASTDATE.ToString();
        MZZTLFIRSTWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVEOFTIME;
        MZZTLFIRSTWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVETIDELEVEL;
        MZZTLFIRSTTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLFIRSTTIMELOWTIDE;
        MZZTLLOWTIDELEVELFORTHEFIRSTTI.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHEFIRSTTI;
        MZZTLSECONDWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVEOFTIME;
        MZZTLSECONDWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVETIDELEVEL;
        MZZTLSECONDTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLSECONDTIMELOWTIDE;


        DbParameter[] parameters = { PUBLISHDATE, MZZTLLOWTIDELEVELFORTHESECONDT, FORECASTDATE, MZZTLFIRSTWAVEOFTIME, MZZTLFIRSTWAVETIDELEVEL, MZZTLFIRSTTIMELOWTIDE, MZZTLLOWTIDELEVELFORTHEFIRSTTI, MZZTLSECONDWAVEOFTIME, MZZTLSECONDWAVETIDELEVEL, MZZTLSECONDTIMELOWTIDE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改明泽闸潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }

    /// <summary>
    /// 根据预报日期获取闸位数据
    /// </summary>
    /// <param name="TBLMZZTIDELEVEL"></param>
    /// <returns></returns>
    public object GETTBLMZZTIDELEVEL(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
    {

        try
        {
            var startDate = TBLMZZTIDELEVEL.FORECASTDATE.AddDays(1);
            var endDate = startDate.AddDays(2);
            string sql = "select * from TBLMZZTIDELEVEL where FORECASTDATE between  to_date('"
                + startDate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') and "
                + " to_date('" + endDate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"; ;
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取明泽闸潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 判断当前预报日期是否存在数据
    /// </summary>
    /// <param name="TBLMZZTIDELEVEL"></param>
    /// <returns></returns>
    public object GETTBLMZZTIDELEVELDATA(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
    {

        try
        {
            string sql = "select * from TBLMZZTIDELEVEL where FORECASTDATE =  to_date('" + TBLMZZTIDELEVEL.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"; ;
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取明泽闸潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 根据预报日期修改挡潮闸数据
    /// </summary>
    /// <param name="TBLMZZTIDELEVEL"></param>
    /// <returns></returns>
    public int EDITTBLMZZTIDELEVEL(TBLMZZTIDELEVEL TBLMZZTIDELEVEL)
    {
        string sql = "UPDATE   TBLMZZTIDELEVEL set	MZZTLLOWTIDELEVELFORTHESECONDT=@MZZTLLOWTIDELEVELFORTHESECONDT,MZZTLFIRSTWAVEOFTIME=@MZZTLFIRSTWAVEOFTIME,MZZTLFIRSTWAVETIDELEVEL=@MZZTLFIRSTWAVETIDELEVEL,MZZTLFIRSTTIMELOWTIDE=@MZZTLFIRSTTIMELOWTIDE,MZZTLLOWTIDELEVELFORTHEFIRSTTI=@MZZTLLOWTIDELEVELFORTHEFIRSTTI,MZZTLSECONDWAVEOFTIME=@MZZTLSECONDWAVEOFTIME,MZZTLSECONDWAVETIDELEVEL=@MZZTLSECONDWAVETIDELEVEL,MZZTLSECONDTIMELOWTIDE=@MZZTLSECONDTIMELOWTIDE where "
            +" FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";
        
        var MZZTLLOWTIDELEVELFORTHESECONDT = DataExe.GetDbParameter();
        var FORECASTDATE = DataExe.GetDbParameter();
        var MZZTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
        var MZZTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
        var MZZTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
        var MZZTLLOWTIDELEVELFORTHEFIRSTTI = DataExe.GetDbParameter();
        var MZZTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
        var MZZTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();
        var MZZTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();


        
        MZZTLLOWTIDELEVELFORTHESECONDT.ParameterName = "@MZZTLLOWTIDELEVELFORTHESECONDT";
        FORECASTDATE.ParameterName = "@FORECASTDATE";
        MZZTLFIRSTWAVEOFTIME.ParameterName = "@MZZTLFIRSTWAVEOFTIME";
        MZZTLFIRSTWAVETIDELEVEL.ParameterName = "@MZZTLFIRSTWAVETIDELEVEL";
        MZZTLFIRSTTIMELOWTIDE.ParameterName = "@MZZTLFIRSTTIMELOWTIDE";
        MZZTLLOWTIDELEVELFORTHEFIRSTTI.ParameterName = "@MZZTLLOWTIDELEVELFORTHEFIRSTTI";
        MZZTLSECONDWAVEOFTIME.ParameterName = "@MZZTLSECONDWAVEOFTIME";
        MZZTLSECONDWAVETIDELEVEL.ParameterName = "@MZZTLSECONDWAVETIDELEVEL";
        MZZTLSECONDTIMELOWTIDE.ParameterName = "@MZZTLSECONDTIMELOWTIDE";

        
        MZZTLLOWTIDELEVELFORTHESECONDT.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHESECONDT;
        FORECASTDATE.Value = TBLMZZTIDELEVEL.FORECASTDATE.ToString();
        MZZTLFIRSTWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVEOFTIME;
        MZZTLFIRSTWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLFIRSTWAVETIDELEVEL;
        MZZTLFIRSTTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLFIRSTTIMELOWTIDE;
        MZZTLLOWTIDELEVELFORTHEFIRSTTI.Value = TBLMZZTIDELEVEL.MZZTLLOWTIDELEVELFORTHEFIRSTTI;
        MZZTLSECONDWAVEOFTIME.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVEOFTIME;
        MZZTLSECONDWAVETIDELEVEL.Value = TBLMZZTIDELEVEL.MZZTLSECONDWAVETIDELEVEL;
        MZZTLSECONDTIMELOWTIDE.Value = TBLMZZTIDELEVEL.MZZTLSECONDTIMELOWTIDE;


        DbParameter[] parameters = { MZZTLLOWTIDELEVELFORTHESECONDT, FORECASTDATE, MZZTLFIRSTWAVEOFTIME, MZZTLFIRSTWAVETIDELEVEL, MZZTLFIRSTTIMELOWTIDE, MZZTLLOWTIDELEVELFORTHEFIRSTTI, MZZTLSECONDWAVEOFTIME, MZZTLSECONDWAVETIDELEVEL, MZZTLSECONDTIMELOWTIDE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改明泽闸潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }


}

