
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 表尾
/// </summary>
/// <returns></returns>
public class sql_TBLFOOTER
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLFOOTER()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 获取所有表尾预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLFOOTER_AllData(TBLFOOTER TBLFOOTER)
    {

        try
        {
            return DataExe.GetTableExeData("select * from TBLFOOTER where PUBLISHDATE=to_date('" + TBLFOOTER.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表尾出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 若今天未填写填报信息
    /// 默认昨天填报信息
    /// Add：sl
    /// </summary>
    /// <param name="TBLFOOTER"></param>
    /// <returns></returns>
    public object GetTblFooterLastDay(TBLFOOTER TBLFOOTER)
    {

        try
        {
            string sql = "select * from TBLFOOTER where PUBLISHDATE=to_date('" + TBLFOOTER.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取昨天填报信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 新增表尾预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLFOOTER(TBLFOOTER TBLFOOTER)
    {

        //string sql = "INSERT INTO  TBLFOOTER (PUBLISHDATE,PUBLISHHOUR,FRELEASEUNIT,FTELEPHONE,FFAX,FWAVEFORECASTER,FTIDALFORECASTER,FWATERTEMPERATUREFORECASTER) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@PUBLISHHOUR,@FRELEASEUNIT,@FTELEPHONE,@FFAX,@FWAVEFORECASTER,@FTIDALFORECASTER,@FWATERTEMPERATUREFORECASTER)";

        string sql = "INSERT INTO  TBLFOOTER (PUBLISHDATE,PUBLISHHOUR,FRELEASEUNIT,FTELEPHONE,FFAX,FWAVEFORECASTER,FTIDALFORECASTER,FWATERTEMPERATUREFORECASTER，FWAVEFORECASTERTEL,FTIDALFORECASTERTEL,FWATERTEMPERATUREFORECASTERTEL,ZHIBANTEL,SENDTEL) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@PUBLISHHOUR,@FRELEASEUNIT,@FTELEPHONE,@FFAX,@FWAVEFORECASTER,@FTIDALFORECASTER,@FWATERTEMPERATUREFORECASTER,@FWAVEFORECASTERTEL,@FTIDALFORECASTERTEL,@FWATERTEMPERATUREFORECASTERTEL,@ZHIBANTEL,@SENDTEL)";

        var PUBLISHDATE = DataExe.GetDbParameter();
        var PUBLISHHOUR = DataExe.GetDbParameter();
        var FRELEASEUNIT = DataExe.GetDbParameter();
        var FTELEPHONE = DataExe.GetDbParameter();
        var FFAX = DataExe.GetDbParameter();
        var FWAVEFORECASTER = DataExe.GetDbParameter();
        var FTIDALFORECASTER = DataExe.GetDbParameter();
        var FWATERTEMPERATUREFORECASTER = DataExe.GetDbParameter();
        var FWAVEFORECASTERTEL = DataExe.GetDbParameter();
        var FTIDALFORECASTERTEL = DataExe.GetDbParameter();
        var FWATERTEMPERATUREFORECASTERTEL = DataExe.GetDbParameter();
        var ZHIBANTEL = DataExe.GetDbParameter();
        var SENDTEL = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        PUBLISHHOUR.ParameterName = "@PUBLISHHOUR";
        FRELEASEUNIT.ParameterName = "@FRELEASEUNIT";
        FTELEPHONE.ParameterName = "@FTELEPHONE";
        FFAX.ParameterName = "@FFAX";
        FWAVEFORECASTER.ParameterName = "@FWAVEFORECASTER";
        FTIDALFORECASTER.ParameterName = "@FTIDALFORECASTER";
        FWATERTEMPERATUREFORECASTER.ParameterName = "@FWATERTEMPERATUREFORECASTER";
        FWAVEFORECASTERTEL.ParameterName = "@FWAVEFORECASTERTEL";
        FTIDALFORECASTERTEL.ParameterName = "@FTIDALFORECASTERTEL";
        FWATERTEMPERATUREFORECASTERTEL.ParameterName = "@FWATERTEMPERATUREFORECASTERTEL";
        ZHIBANTEL.ParameterName = "@ZHIBANTEL";
        SENDTEL.ParameterName = "@SENDTEL";




        PUBLISHDATE.Value = TBLFOOTER.PUBLISHDATE.ToString();
        PUBLISHHOUR.Value = TBLFOOTER.PUBLISHHOUR;
        FRELEASEUNIT.Value = TBLFOOTER.FRELEASEUNIT;
        FTELEPHONE.Value = TBLFOOTER.FTELEPHONE;
        FFAX.Value = TBLFOOTER.FFAX;
        FWAVEFORECASTER.Value = TBLFOOTER.FWAVEFORECASTER;
        FTIDALFORECASTER.Value = TBLFOOTER.FTIDALFORECASTER;
        FWATERTEMPERATUREFORECASTER.Value = TBLFOOTER.FWATERTEMPERATUREFORECASTER;
        FWAVEFORECASTERTEL.Value = TBLFOOTER.FWAVEFORECASTERTEL;
        FTIDALFORECASTERTEL.Value = TBLFOOTER.FTIDALFORECASTERTEL;
        FWATERTEMPERATUREFORECASTERTEL.Value = TBLFOOTER.FWATERTEMPERATUREFORECASTERTEL;
        ZHIBANTEL.Value = TBLFOOTER.ZHIBANTEL;
        SENDTEL.Value = TBLFOOTER.SENDTEL;


        DbParameter[] parameters = { PUBLISHDATE, PUBLISHHOUR, FRELEASEUNIT, FTELEPHONE, FFAX, FWAVEFORECASTER, FTIDALFORECASTER, FWATERTEMPERATUREFORECASTER, FWAVEFORECASTERTEL, FTIDALFORECASTERTEL, FWATERTEMPERATUREFORECASTERTEL, ZHIBANTEL, SENDTEL };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增表尾出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改表尾预报
    /// </summary>
    public int Edit_TBLFOOTER(TBLFOOTER TBLFOOTER,string quanxian)
    {
        string sql = "";
        DbParameter[] parameters = null;
        if (quanxian == "fl")
        {
            //sql = "UPDATE   TBLFOOTER set	PUBLISHHOUR=@PUBLISHHOUR,FRELEASEUNIT=@FRELEASEUNIT,FTELEPHONE=@FTELEPHONE,FFAX=@FFAX,FWAVEFORECASTER=@FWAVEFORECASTER  where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";
            sql = "UPDATE   TBLFOOTER set	PUBLISHHOUR=@PUBLISHHOUR,FRELEASEUNIT=@FRELEASEUNIT,FTELEPHONE=@FTELEPHONE,FFAX=@FFAX,FWAVEFORECASTER=@FWAVEFORECASTER,FWAVEFORECASTERTEL=@FWAVEFORECASTERTEL,ZHIBANTEL=@ZHIBANTEL,SENDTEL=@SENDTEL  where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var PUBLISHHOUR = DataExe.GetDbParameter();
            var FRELEASEUNIT = DataExe.GetDbParameter();
            var FTELEPHONE = DataExe.GetDbParameter();
            var FFAX = DataExe.GetDbParameter();
            var FWAVEFORECASTER = DataExe.GetDbParameter();
            var FWAVEFORECASTERTEL = DataExe.GetDbParameter();
            var ZHIBANTEL = DataExe.GetDbParameter();
            var SENDTEL = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            PUBLISHHOUR.ParameterName = "@PUBLISHHOUR";
            FRELEASEUNIT.ParameterName = "@FRELEASEUNIT";
            FTELEPHONE.ParameterName = "@FTELEPHONE";
            FFAX.ParameterName = "@FFAX";
            FWAVEFORECASTER.ParameterName = "@FWAVEFORECASTER";
            FWAVEFORECASTERTEL.ParameterName = "@FWAVEFORECASTERTEL";
            ZHIBANTEL.ParameterName = "@ZHIBANTEL";
            SENDTEL.ParameterName = "@SENDTEL";




            PUBLISHDATE.Value = TBLFOOTER.PUBLISHDATE.ToString();
            PUBLISHHOUR.Value = TBLFOOTER.PUBLISHHOUR;
            FRELEASEUNIT.Value = TBLFOOTER.FRELEASEUNIT;
            FTELEPHONE.Value = TBLFOOTER.FTELEPHONE;
            FFAX.Value = TBLFOOTER.FFAX;
            FWAVEFORECASTER.Value = TBLFOOTER.FWAVEFORECASTER;
            FWAVEFORECASTERTEL.Value = TBLFOOTER.FWAVEFORECASTERTEL;
            ZHIBANTEL.Value = TBLFOOTER.ZHIBANTEL;
            SENDTEL.Value = TBLFOOTER.SENDTEL;


            parameters = new DbParameter[] { PUBLISHDATE, PUBLISHHOUR, FRELEASEUNIT, FTELEPHONE, FFAX, FWAVEFORECASTER, FWAVEFORECASTERTEL,ZHIBANTEL,SENDTEL };
        }
        else if (quanxian == "cx")
        {
            //sql = "UPDATE   TBLFOOTER set	PUBLISHHOUR=@PUBLISHHOUR,FRELEASEUNIT=@FRELEASEUNIT,FTELEPHONE=@FTELEPHONE,FFAX=@FFAX,FTIDALFORECASTER=@FTIDALFORECASTER  where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";
            sql = "UPDATE   TBLFOOTER set	PUBLISHHOUR=@PUBLISHHOUR,FRELEASEUNIT=@FRELEASEUNIT,FTELEPHONE=@FTELEPHONE,FFAX=@FFAX,FTIDALFORECASTER=@FTIDALFORECASTER,FTIDALFORECASTERTEL=@FTIDALFORECASTERTEL,ZHIBANTEL=@ZHIBANTEL,SENDTEL=@SENDTEL  where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var PUBLISHHOUR = DataExe.GetDbParameter();
            var FRELEASEUNIT = DataExe.GetDbParameter();
            var FTELEPHONE = DataExe.GetDbParameter();
            var FFAX = DataExe.GetDbParameter();
            var FTIDALFORECASTER = DataExe.GetDbParameter();
            var FTIDALFORECASTERTEL = DataExe.GetDbParameter();
            var ZHIBANTEL = DataExe.GetDbParameter();
            var SENDTEL = DataExe.GetDbParameter();



            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            PUBLISHHOUR.ParameterName = "@PUBLISHHOUR";
            FRELEASEUNIT.ParameterName = "@FRELEASEUNIT";
            FTELEPHONE.ParameterName = "@FTELEPHONE";
            FFAX.ParameterName = "@FFAX";
            FTIDALFORECASTER.ParameterName = "@FTIDALFORECASTER";
            FTIDALFORECASTERTEL.ParameterName = "@FTIDALFORECASTERTEL";
            ZHIBANTEL.ParameterName = "@ZHIBANTEL";
            SENDTEL.ParameterName = "@SENDTEL";



            PUBLISHDATE.Value = TBLFOOTER.PUBLISHDATE.ToString();
            PUBLISHHOUR.Value = TBLFOOTER.PUBLISHHOUR;
            FRELEASEUNIT.Value = TBLFOOTER.FRELEASEUNIT;
            FTELEPHONE.Value = TBLFOOTER.FTELEPHONE;
            FFAX.Value = TBLFOOTER.FFAX;
            FTIDALFORECASTER.Value = TBLFOOTER.FTIDALFORECASTER;
            FTIDALFORECASTERTEL.Value = TBLFOOTER.FTIDALFORECASTERTEL;
            ZHIBANTEL.Value = TBLFOOTER.ZHIBANTEL;
            SENDTEL.Value = TBLFOOTER.SENDTEL;

            parameters = new DbParameter[] { PUBLISHDATE, PUBLISHHOUR, FRELEASEUNIT, FTELEPHONE, FFAX, FTIDALFORECASTER, FTIDALFORECASTERTEL, ZHIBANTEL, SENDTEL };
        }
        else if(quanxian == "sw")
        {
            //sql = "UPDATE   TBLFOOTER set	PUBLISHHOUR=@PUBLISHHOUR,FRELEASEUNIT=@FRELEASEUNIT,FTELEPHONE=@FTELEPHONE,FFAX=@FFAX,FWATERTEMPERATUREFORECASTER=@FWATERTEMPERATUREFORECASTER where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";
            sql = "UPDATE   TBLFOOTER set	PUBLISHHOUR=@PUBLISHHOUR,FRELEASEUNIT=@FRELEASEUNIT,FTELEPHONE=@FTELEPHONE,FFAX=@FFAX,FWATERTEMPERATUREFORECASTER=@FWATERTEMPERATUREFORECASTER,FWATERTEMPERATUREFORECASTERTEL=@FWATERTEMPERATUREFORECASTERTEL,ZHIBANTEL=@ZHIBANTEL,SENDTEL=@SENDTEL where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var PUBLISHHOUR = DataExe.GetDbParameter();
            var FRELEASEUNIT = DataExe.GetDbParameter();
            var FTELEPHONE = DataExe.GetDbParameter();
            var FFAX = DataExe.GetDbParameter();
            var FWATERTEMPERATUREFORECASTER = DataExe.GetDbParameter();
            var FWATERTEMPERATUREFORECASTERTEL = DataExe.GetDbParameter();
            var ZHIBANTEL = DataExe.GetDbParameter();
            var SENDTEL = DataExe.GetDbParameter();



            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            PUBLISHHOUR.ParameterName = "@PUBLISHHOUR";
            FRELEASEUNIT.ParameterName = "@FRELEASEUNIT";
            FTELEPHONE.ParameterName = "@FTELEPHONE";
            FFAX.ParameterName = "@FFAX";
            FWATERTEMPERATUREFORECASTER.ParameterName = "@FWATERTEMPERATUREFORECASTER";
            FWATERTEMPERATUREFORECASTERTEL.ParameterName = "@FWATERTEMPERATUREFORECASTERTEL";
            ZHIBANTEL.ParameterName = "@ZHIBANTEL";
            SENDTEL.ParameterName = "@SENDTEL";



            PUBLISHDATE.Value = TBLFOOTER.PUBLISHDATE.ToString();
            PUBLISHHOUR.Value = TBLFOOTER.PUBLISHHOUR;
            FRELEASEUNIT.Value = TBLFOOTER.FRELEASEUNIT;
            FTELEPHONE.Value = TBLFOOTER.FTELEPHONE;
            FFAX.Value = TBLFOOTER.FFAX;
            FWATERTEMPERATUREFORECASTER.Value = TBLFOOTER.FWATERTEMPERATUREFORECASTER;
            FWATERTEMPERATUREFORECASTERTEL.Value = TBLFOOTER.FWATERTEMPERATUREFORECASTERTEL;
            ZHIBANTEL.Value = TBLFOOTER.ZHIBANTEL;
            SENDTEL.Value = TBLFOOTER.SENDTEL;

            parameters = new DbParameter[] { PUBLISHDATE, PUBLISHHOUR, FRELEASEUNIT, FTELEPHONE, FFAX, FWATERTEMPERATUREFORECASTER, FWATERTEMPERATUREFORECASTERTEL , ZHIBANTEL, SENDTEL };
        }
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改表尾出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }


}

