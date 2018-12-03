
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// 各海区24小时海浪
/// </summary>
/// <returns></returns>
public class sql_TBLEACHSEAAREA24HSEAWAVE
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLEACHSEAAREA24HSEAWAVE()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 获取所有各海区24小时海浪预报
    /// </summary>
    /// <returns></returns>
    public object get_TBLEACHSEAAREA24HSEAWAVE_AllData(TBLEACHSEAAREA24HSEAWAVE TBLEACHSEAAREA24HSEAWAVE)
    {

        try
        {
            return DataExe.GetTableExeData("select * from TBLEACHSEAAREA24HSEAWAVE where PUBLISHDATE=to_date('" + TBLEACHSEAAREA24HSEAWAVE.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')");
           
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取各海区24小时海浪出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 新增各海区24小时海浪预报
    /// </summary>
    /// <returns></returns>
    public int Add_TBLEACHSEAAREA24HSEAWAVE(TBLEACHSEAAREA24HSEAWAVE TBLEACHSEAAREA24HSEAWAVE)
    {

        string sql = "INSERT INTO  TBLEACHSEAAREA24HSEAWAVE (PUBLISHDATE,ESASWAREA,ESASWLOWESTWAVEHEIGHT,ESASWHIGHTESTWAVEHEIGHT,ESASWWAVETYPE) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@ESASWAREA,@ESASWLOWESTWAVEHEIGHT,@ESASWHIGHTESTWAVEHEIGHT,@ESASWWAVETYPE)";



        var PUBLISHDATE = DataExe.GetDbParameter();
        var ESASWAREA = DataExe.GetDbParameter();
        var ESASWLOWESTWAVEHEIGHT = DataExe.GetDbParameter();
        var ESASWHIGHTESTWAVEHEIGHT = DataExe.GetDbParameter();
        var ESASWWAVETYPE = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        ESASWAREA.ParameterName = "@ESASWAREA";
        ESASWLOWESTWAVEHEIGHT.ParameterName = "@ESASWLOWESTWAVEHEIGHT";
        ESASWHIGHTESTWAVEHEIGHT.ParameterName = "@ESASWHIGHTESTWAVEHEIGHT";
        ESASWWAVETYPE.ParameterName = "@ESASWWAVETYPE";




        PUBLISHDATE.Value = TBLEACHSEAAREA24HSEAWAVE.PUBLISHDATE.ToString();
        ESASWAREA.Value = TBLEACHSEAAREA24HSEAWAVE.ESASWAREA;
        ESASWLOWESTWAVEHEIGHT.Value = TBLEACHSEAAREA24HSEAWAVE.ESASWLOWESTWAVEHEIGHT;
        ESASWHIGHTESTWAVEHEIGHT.Value = TBLEACHSEAAREA24HSEAWAVE.ESASWHIGHTESTWAVEHEIGHT;
        ESASWWAVETYPE.Value = TBLEACHSEAAREA24HSEAWAVE.ESASWWAVETYPE;


        DbParameter[] parameters = { PUBLISHDATE, ESASWAREA, ESASWLOWESTWAVEHEIGHT, ESASWHIGHTESTWAVEHEIGHT, ESASWWAVETYPE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增各海区24小时海浪出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改各海区24小时海浪预报
    /// </summary>
    public int Edit_TBLEACHSEAAREA24HSEAWAVE(TBLEACHSEAAREA24HSEAWAVE TBLEACHSEAAREA24HSEAWAVE)
    {
        string sql = "UPDATE   TBLEACHSEAAREA24HSEAWAVE set	PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),ESASWLOWESTWAVEHEIGHT=@ESASWLOWESTWAVEHEIGHT,ESASWHIGHTESTWAVEHEIGHT=@ESASWHIGHTESTWAVEHEIGHT,ESASWWAVETYPE=@ESASWWAVETYPE where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and ESASWAREA=@ESASWAREA";


        var PUBLISHDATE = DataExe.GetDbParameter();
        var ESASWAREA = DataExe.GetDbParameter();
        var ESASWLOWESTWAVEHEIGHT = DataExe.GetDbParameter();
        var ESASWHIGHTESTWAVEHEIGHT = DataExe.GetDbParameter();
        var ESASWWAVETYPE = DataExe.GetDbParameter();




        PUBLISHDATE.ParameterName = "@PUBLISHDATE";
        ESASWAREA.ParameterName = "@ESASWAREA";
        ESASWLOWESTWAVEHEIGHT.ParameterName = "@ESASWLOWESTWAVEHEIGHT";
        ESASWHIGHTESTWAVEHEIGHT.ParameterName = "@ESASWHIGHTESTWAVEHEIGHT";
        ESASWWAVETYPE.ParameterName = "@ESASWWAVETYPE";




        PUBLISHDATE.Value = TBLEACHSEAAREA24HSEAWAVE.PUBLISHDATE.ToString();
        ESASWAREA.Value = TBLEACHSEAAREA24HSEAWAVE.ESASWAREA;
        ESASWLOWESTWAVEHEIGHT.Value = TBLEACHSEAAREA24HSEAWAVE.ESASWLOWESTWAVEHEIGHT;
        ESASWHIGHTESTWAVEHEIGHT.Value = TBLEACHSEAAREA24HSEAWAVE.ESASWHIGHTESTWAVEHEIGHT;
        ESASWWAVETYPE.Value = TBLEACHSEAAREA24HSEAWAVE.ESASWWAVETYPE;


        DbParameter[] parameters = { PUBLISHDATE, ESASWAREA, ESASWLOWESTWAVEHEIGHT, ESASWHIGHTESTWAVEHEIGHT, ESASWWAVETYPE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改各海区24小时海浪出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }


}

