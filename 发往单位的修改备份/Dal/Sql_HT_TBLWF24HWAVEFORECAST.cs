using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

public class Sql_HT_TBLWF24HWAVEFORECAST
{
    DataExecution DataExe;//声明一个数据执行类
    public Sql_HT_TBLWF24HWAVEFORECAST()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 获取所有24小时海浪预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLWF24HWAVEFORECAST_AllData(HT_TBLWF24HWAVEFORECAST HT_TBLWF24HWAVEFORECAST)
    {

        try
        {

            return DataExe.GetTableExeData("select * from HT_TBLWF24HWAVEFORECAST where PUBLISHDATE=to_date('" + HT_TBLWF24HWAVEFORECAST.PUBLISHDATE.ToString() + "', 'yyyy-mm-dd hh24@mi@ss')");
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取24小时海浪出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 新增24小时海浪预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLWF24HWAVEFORECAST_hl(HT_TBLWF24HWAVEFORECAST HT_TBLWF24HWAVEFORECAST)
    {
       
          string  sql = "INSERT INTO  HT_TBLWF24HWAVEFORECAST (PUBLISHDATE,SA24HWFBOHAIWAVEHEIGHT,SA24HWFNORTHOFYSWAVEHEIGHT,SA24HWFMIDDLEOFYSWAVEHEIGHT,SA24HWFSOUTHOFYSWAVEHEIGHT,SA24HWFOFFSHOREWAVEHEIGHT) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@SA24HWFBOHAIWAVEHEIGHT,@SA24HWFNORTHOFYSWAVEHEIGHT,@SA24HWFMIDDLEOFYSWAVEHEIGHT,@SA24HWFSOUTHOFYSWAVEHEIGHT,@SA24HWFOFFSHOREWAVEHEIGHT)";

       
        var PUBLISHDATE = DataExe.GetDbParameter();
        var SA24HWFBOHAIWAVEHEIGHT = DataExe.GetDbParameter();
        var SA24HWFNORTHOFYSWAVEHEIGHT = DataExe.GetDbParameter();
        var SA24HWFMIDDLEOFYSWAVEHEIGHT = DataExe.GetDbParameter();
        var SA24HWFSOUTHOFYSWAVEHEIGHT = DataExe.GetDbParameter();
        var SA24HWFOFFSHOREWAVEHEIGHT = DataExe.GetDbParameter();
       // var SA24HWFOFFSHORESW = DataExe.GetDbParameter();
      




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        SA24HWFBOHAIWAVEHEIGHT.ParameterName = "@SA24HWFBOHAIWAVEHEIGHT";
        SA24HWFNORTHOFYSWAVEHEIGHT.ParameterName = "@SA24HWFNORTHOFYSWAVEHEIGHT";
        SA24HWFMIDDLEOFYSWAVEHEIGHT.ParameterName = "@SA24HWFMIDDLEOFYSWAVEHEIGHT";
        SA24HWFSOUTHOFYSWAVEHEIGHT.ParameterName = "@SA24HWFSOUTHOFYSWAVEHEIGHT";
        SA24HWFOFFSHOREWAVEHEIGHT.ParameterName = "@SA24HWFOFFSHOREWAVEHEIGHT";
        //SA24HWFOFFSHORESW.ParameterName = "@SA24HWFOFFSHORESW";
       




        PUBLISHDATE.Value = HT_TBLWF24HWAVEFORECAST.PUBLISHDATE.ToString();
        SA24HWFBOHAIWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFBOHAIWAVEHEIGHT;
        SA24HWFNORTHOFYSWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFNORTHOFYSWAVEHEIGHT;
        SA24HWFMIDDLEOFYSWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFMIDDLEOFYSWAVEHEIGHT;
        SA24HWFSOUTHOFYSWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFSOUTHOFYSWAVEHEIGHT.ToString();
        SA24HWFOFFSHOREWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFOFFSHOREWAVEHEIGHT;
       // SA24HWFOFFSHORESW.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFOFFSHORESW;
       


        DbParameter[] parameters = { PUBLISHDATE, SA24HWFBOHAIWAVEHEIGHT, SA24HWFNORTHOFYSWAVEHEIGHT, SA24HWFMIDDLEOFYSWAVEHEIGHT, SA24HWFSOUTHOFYSWAVEHEIGHT, SA24HWFOFFSHOREWAVEHEIGHT};
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增24小海浪位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }


    /// <summary>
    /// 新增24小时水温预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLWF24HWAVEFORECAST_sw(HT_TBLWF24HWAVEFORECAST HT_TBLWF24HWAVEFORECAST)
    {

        string sql = "INSERT INTO  HT_TBLWF24HWAVEFORECAST (PUBLISHDATE,SA24HWFOFFSHORESW) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@SA24HWFOFFSHORESW)";


        var PUBLISHDATE = DataExe.GetDbParameter();
        var SA24HWFOFFSHORESW = DataExe.GetDbParameter();





        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        SA24HWFOFFSHORESW.ParameterName = "@SA24HWFOFFSHORESW";





        PUBLISHDATE.Value = HT_TBLWF24HWAVEFORECAST.PUBLISHDATE.ToString();
        SA24HWFOFFSHORESW.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFOFFSHORESW;



        DbParameter[] parameters = { PUBLISHDATE, SA24HWFOFFSHORESW };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增24小海浪位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 修改24小时海浪预报
    /// </summary>
    public int Edit_TBLWF24HWAVEFORECAST_hl(HT_TBLWF24HWAVEFORECAST HT_TBLWF24HWAVEFORECAST)
    {
        
        string sql = "UPDATE   HT_TBLWF24HWAVEFORECAST set	SA24HWFBOHAIWAVEHEIGHT=@SA24HWFBOHAIWAVEHEIGHT,SA24HWFNORTHOFYSWAVEHEIGHT  =@SA24HWFNORTHOFYSWAVEHEIGHT,SA24HWFMIDDLEOFYSWAVEHEIGHT   =@SA24HWFMIDDLEOFYSWAVEHEIGHT,SA24HWFSOUTHOFYSWAVEHEIGHT=@SA24HWFSOUTHOFYSWAVEHEIGHT,SA24HWFOFFSHOREWAVEHEIGHT=@SA24HWFOFFSHOREWAVEHEIGHT where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";
        
       
            var PUBLISHDATE = DataExe.GetDbParameter();
            var SA24HWFBOHAIWAVEHEIGHT = DataExe.GetDbParameter();
            var SA24HWFNORTHOFYSWAVEHEIGHT = DataExe.GetDbParameter();
            var SA24HWFMIDDLEOFYSWAVEHEIGHT = DataExe.GetDbParameter();
            var SA24HWFSOUTHOFYSWAVEHEIGHT = DataExe.GetDbParameter();
            var SA24HWFOFFSHOREWAVEHEIGHT = DataExe.GetDbParameter();
           // var SA24HWFOFFSHORESW = DataExe.GetDbParameter();





            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            SA24HWFBOHAIWAVEHEIGHT.ParameterName = "@SA24HWFBOHAIWAVEHEIGHT";
            SA24HWFNORTHOFYSWAVEHEIGHT.ParameterName = "@SA24HWFNORTHOFYSWAVEHEIGHT";
            SA24HWFMIDDLEOFYSWAVEHEIGHT.ParameterName = "@SA24HWFMIDDLEOFYSWAVEHEIGHT";
            SA24HWFSOUTHOFYSWAVEHEIGHT.ParameterName = "@SA24HWFSOUTHOFYSWAVEHEIGHT";
            SA24HWFOFFSHOREWAVEHEIGHT.ParameterName = "@SA24HWFOFFSHOREWAVEHEIGHT";
           // SA24HWFOFFSHORESW.ParameterName = "@SA24HWFOFFSHORESW";



            PUBLISHDATE.Value = HT_TBLWF24HWAVEFORECAST.PUBLISHDATE.ToString();
            SA24HWFBOHAIWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFBOHAIWAVEHEIGHT;
            SA24HWFNORTHOFYSWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFNORTHOFYSWAVEHEIGHT;
            SA24HWFMIDDLEOFYSWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFMIDDLEOFYSWAVEHEIGHT;
            SA24HWFSOUTHOFYSWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFSOUTHOFYSWAVEHEIGHT.ToString();
            SA24HWFOFFSHOREWAVEHEIGHT.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFOFFSHOREWAVEHEIGHT;
            //SA24HWFOFFSHORESW.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFOFFSHORESW;



            DbParameter[] parameters = { PUBLISHDATE, SA24HWFBOHAIWAVEHEIGHT, SA24HWFNORTHOFYSWAVEHEIGHT, SA24HWFMIDDLEOFYSWAVEHEIGHT, SA24HWFSOUTHOFYSWAVEHEIGHT, SA24HWFOFFSHOREWAVEHEIGHT };
      
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改24小时海浪出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }
    /// <summary>
    /// 修改24小时水温预报
    /// </summary>
    public int Edit_TBLWF24HWAVEFORECAST_sw(HT_TBLWF24HWAVEFORECAST HT_TBLWF24HWAVEFORECAST)
    {
        
    string sql = "UPDATE  HT_TBLWF24HWAVEFORECAST set SA24HWFOFFSHORESW=@SA24HWFOFFSHORESW where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";


        var PUBLISHDATE = DataExe.GetDbParameter();
        var SA24HWFOFFSHORESW = DataExe.GetDbParameter();

        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        SA24HWFOFFSHORESW.ParameterName = "@SA24HWFOFFSHORESW";


        PUBLISHDATE.Value = HT_TBLWF24HWAVEFORECAST.PUBLISHDATE.ToString();
        SA24HWFOFFSHORESW.Value = HT_TBLWF24HWAVEFORECAST.SA24HWFOFFSHORESW;



        DbParameter[] parameters = { PUBLISHDATE,SA24HWFOFFSHORESW};

        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改24小时水温出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }
}