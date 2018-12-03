
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 海洋气象综述
/// </summary>
/// <returns></returns>
public class sql_TBLMARINEMETEOROLOGICALREVIEW
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLMARINEMETEOROLOGICALREVIEW()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 获取所有海洋气象综述预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLMARINEMETEOROLOGICALREVIEW_AllData(TBLMARINEMETEOROLOGICALREVIEW TBLMARINEMETEOROLOGICALREVIEW)
    {

        try
        {
            return DataExe.GetTableExeData("select * from TBLMARINEMETEOROLOGICALREVIEW where PUBLISHDATE=to_date('" + TBLMARINEMETEOROLOGICALREVIEW.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");
           
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取海洋气象综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 新增海洋气象综述预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLMARINEMETEOROLOGICALREVIEW(TBLMARINEMETEOROLOGICALREVIEW TBLMARINEMETEOROLOGICALREVIEW)
    {

        string sql = "INSERT INTO  TBLMARINEMETEOROLOGICALREVIEW (PUBLISHDATE,METEOROLOGICALREVIEW,METEOROLOGICALREVIEW24HOUR) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@METEOROLOGICALREVIEW,@METEOROLOGICALREVIEW24HOUR)";



        var PUBLISHDATE = DataExe.GetDbParameter();
        var METEOROLOGICALREVIEW = DataExe.GetDbParameter();
        var METEOROLOGICALREVIEW24HOUR = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        METEOROLOGICALREVIEW.ParameterName = "@METEOROLOGICALREVIEW";
        METEOROLOGICALREVIEW24HOUR.ParameterName = "@METEOROLOGICALREVIEW24HOUR";




        PUBLISHDATE.Value = TBLMARINEMETEOROLOGICALREVIEW.PUBLISHDATE;
        METEOROLOGICALREVIEW.Value = TBLMARINEMETEOROLOGICALREVIEW.METEOROLOGICALREVIEW;
        METEOROLOGICALREVIEW24HOUR.Value = TBLMARINEMETEOROLOGICALREVIEW.METEOROLOGICALREVIEW24HOUR;


        DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW, METEOROLOGICALREVIEW24HOUR };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增海洋气象综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改海洋气象综述预报
    /// </summary>
    public int Edit_TBLMARINEMETEOROLOGICALREVIEW(TBLMARINEMETEOROLOGICALREVIEW TBLMARINEMETEOROLOGICALREVIEW)
    {
        string sql = "UPDATE   TBLMARINEMETEOROLOGICALREVIEW set METEOROLOGICALREVIEW=@METEOROLOGICALREVIEW,METEOROLOGICALREVIEW24HOUR=@METEOROLOGICALREVIEW24HOUR where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";


        var PUBLISHDATE = DataExe.GetDbParameter();
        var METEOROLOGICALREVIEW = DataExe.GetDbParameter();
        var METEOROLOGICALREVIEW24HOUR = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        METEOROLOGICALREVIEW.ParameterName = "@METEOROLOGICALREVIEW";
        METEOROLOGICALREVIEW24HOUR.ParameterName = "@METEOROLOGICALREVIEW24HOUR";




        PUBLISHDATE.Value = TBLMARINEMETEOROLOGICALREVIEW.PUBLISHDATE;
        METEOROLOGICALREVIEW.Value = TBLMARINEMETEOROLOGICALREVIEW.METEOROLOGICALREVIEW;
        METEOROLOGICALREVIEW24HOUR.Value = TBLMARINEMETEOROLOGICALREVIEW.METEOROLOGICALREVIEW24HOUR;


        DbParameter[] parameters = { PUBLISHDATE, METEOROLOGICALREVIEW, METEOROLOGICALREVIEW24HOUR };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改海洋气象综述出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }

}


    