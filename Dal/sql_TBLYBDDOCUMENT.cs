using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// sql_TBLYBDDOCUMENT 的摘要说明
/// </summary>
public class sql_TBLYBDDOCUMENT
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TBLYBDDOCUMENT()
    {
        //
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 新增word文档sql
    /// </summary>
    /// <returns></returns>
    public int Add_TBLYBDDOCUMENT(TBLYBDDOCUMENT TBLYBDDOCUMENT)
    {

        string sql = "INSERT INTO  TBLYBDDOCUMENT (YBDID,YBDNAME,UPLOADDATE,UPLOADER,YBDCONTENT,YBDSIZE) VALUES (TBLYBDDOCUMENT_SEQ.Nextval,@YBDNAME,to_date(@UPLOADDATE,'yyyy-mm-dd hh24@mi@ss'),@UPLOADER,@YBDCONTENT,@YBDSIZE)";

        var YBDNAME = DataExe.GetDbParameter();
        var UPLOADDATE = DataExe.GetDbParameter();
        var UPLOADER = DataExe.GetDbParameter();
        var YBDCONTENT = DataExe.GetDbParameter();
        var YBDSIZE = DataExe.GetDbParameter();

        //YBDID.ParameterName = "@YBDID";
        YBDNAME.ParameterName = "@YBDNAME";
        UPLOADDATE.ParameterName = "@UPLOADDATE";
        UPLOADER.ParameterName = "@UPLOADER";
        YBDCONTENT.ParameterName = "@YBDCONTENT";
        YBDSIZE.ParameterName = "@YBDSIZE";

       // YBDID.Value = "";
        YBDNAME.Value = TBLYBDDOCUMENT.YBDNAME;
        UPLOADDATE.Value = TBLYBDDOCUMENT.UPLOADDATE.ToString();
        UPLOADER.Value = TBLYBDDOCUMENT.UPLOADER;
        YBDCONTENT.Value = TBLYBDDOCUMENT.YBDCONTENT;
        YBDSIZE.Value = TBLYBDDOCUMENT.YBDSIZE;
       


        DbParameter[] parameters = {YBDNAME, UPLOADDATE, UPLOADER, YBDCONTENT, YBDSIZE};
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("插入word！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 文档存储表信息
    /// </summary>
    /// <returns></returns>
    public object get_TBLYBDDOCUMENT_AllData(TBLYBDDOCUMENT TBLYBDDOCUMENT)
    {

        try
        {

           // return DataExe.GetTableExeData("select * from TBLYBDDOCUMENT where UPLOADDATE>=to_date('" + TBLYBDDOCUMENT.UPLOADDATE.ToString("yyyy-MM-dd") + "', 'yyyy-MM-dd') and "+ "UPLOADDATE<=to_date('" + TBLYBDDOCUMENT.UPLOADDATE.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-MM-dd')");
            return DataExe.GetTableExeData("select* from  TBLYBDDOCUMENT where   YBDNAME='"+ TBLYBDDOCUMENT.YBDNAME + "'");
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取文档存储表信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }


   // select* from  TBLYBDDOCUMENT where UPLOADDATE >=to_date('2015-05-07 00:00:00','yyyy-MM-dd hh24@mi@ss') and UPLOADDATE <=to_date('2015-05-07 23:59:59','yyyy-MM-dd hh24@mi@ss')

    /// <summary>
    /// 修改文档存储表
    /// </summary>
    public int Edit_TBLYBDDOCUMENT(TBLYBDDOCUMENT TBLYBDDOCUMENT)
    {
        string sql = "UPDATE   TBLYBDDOCUMENT set UPLOADDATE=to_date(@UPLOADDATE,'yyyy-mm-dd hh24@mi@ss'),UPLOADER=@UPLOADER,YBDCONTENT=@YBDCONTENT,YBDSIZE=@YBDSIZE where  YBDNAME=@YBDNAME";


        //var YBDID = DataExe.GetDbParameter();
        var YBDNAME = DataExe.GetDbParameter();
        var UPLOADDATE = DataExe.GetDbParameter();
        var UPLOADER = DataExe.GetDbParameter();
        var YBDCONTENT = DataExe.GetDbParameter();
        var YBDSIZE = DataExe.GetDbParameter();

        //YBDID.ParameterName = "@YBDID";
        YBDNAME.ParameterName = "@YBDNAME";
        UPLOADDATE.ParameterName = "@UPLOADDATE";
        UPLOADER.ParameterName = "@UPLOADER";
        YBDCONTENT.ParameterName = "@YBDCONTENT";
        YBDSIZE.ParameterName = "@YBDSIZE";

        // YBDID.Value = "";
        YBDNAME.Value = TBLYBDDOCUMENT.YBDNAME;
        UPLOADDATE.Value = TBLYBDDOCUMENT.UPLOADDATE.ToString();
        UPLOADER.Value = TBLYBDDOCUMENT.UPLOADER;
        YBDCONTENT.Value = TBLYBDDOCUMENT.YBDCONTENT;
        YBDSIZE.Value = TBLYBDDOCUMENT.YBDSIZE;



        DbParameter[] parameters = { YBDNAME, UPLOADDATE, UPLOADER, YBDCONTENT, YBDSIZE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改文档存储表出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }


    }
}