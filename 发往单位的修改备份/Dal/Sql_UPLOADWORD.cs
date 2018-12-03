using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;


public class Sql_UPLOADWORD
{
    DataExecution DataExe;//声明一个数据执行类
    public Sql_UPLOADWORD()
    {
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
    } /// <summary>

    /// <summary>
    /// 获取表单数据
    /// </summary>
    /// <returns></returns>
    public object get_UPLOADWORDdata(string newname,string type)
    {

        try
        {
            string sql = "select * from HT_KJ_UPLOADWORD where TYPE=" + "'" + type.ToString() + "'" + " and NEWNAME = "+"'"+ newname + "'"+"";
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    public object get_UPLOADWORDdata( string type)
    {

        try
        {
            string sql = "select * from HT_KJ_UPLOADWORD where TYPE=" + "'" + type.ToString() + "'" + " OR TYPE = 'EN' order by newname";
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 获取中长期模板
    /// </summary>
    /// <returns></returns>
    public object get_upLoadModel()
    {

        try
        {
            string sql = "select * from HT_KJ_UPLOADWORD where TYPE='EN' or TYPE='CN'";
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 获取中长期表单列表
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public object GetUpLoadWordData(string type) {
        try
        {
            string sql = "select * from HT_KJ_UPLOADWORD where (TYPE="+ type.ToString() + ")";
            return DataExe.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    //更新
    public int Edit_UPLOADWORD(KJ_UPLOADWORD KJ_UPLOADWORD)
    {
        string sql = "UPDATE   HT_KJ_UPLOADWORD set OLDNAME=@OLDNAME where  NEWNAME=@NEWNAME AND TYPE = @TYPE";


       
        var OLDNAME = DataExe.GetDbParameter();
        var NEWNAME = DataExe.GetDbParameter();
        var TYPE = DataExe.GetDbParameter();

       
        OLDNAME.ParameterName = "@OLDNAME";
        NEWNAME.ParameterName = "@NEWNAME";
        TYPE.ParameterName = "@TYPE";


        // YBDID.Value = "";
        OLDNAME.Value = KJ_UPLOADWORD.Oldname;
        NEWNAME.Value = KJ_UPLOADWORD.Newname;
        TYPE.Value = KJ_UPLOADWORD.Type;
       



        DbParameter[] parameters = { OLDNAME, NEWNAME, TYPE };
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
    //新增
    public int Add_UPLOADWORD(KJ_UPLOADWORD KJ_UPLOADWORD)
    {

        string sql = "INSERT INTO  HT_KJ_UPLOADWORD (ID,OLDNAME,NEWNAME,TYPE) VALUES (UPLOADWORD.Nextval,@OLDNAME,@NEWNAME,@TYPE)";



        var OLDNAME = DataExe.GetDbParameter();
        var NEWNAME = DataExe.GetDbParameter();
        var TYPE = DataExe.GetDbParameter();


        OLDNAME.ParameterName = "@OLDNAME";
        NEWNAME.ParameterName = "@NEWNAME";
        TYPE.ParameterName = "@TYPE";


        // YBDID.Value = "";
        OLDNAME.Value = KJ_UPLOADWORD.Oldname;
        NEWNAME.Value = KJ_UPLOADWORD.Newname;
        TYPE.Value = KJ_UPLOADWORD.Type;



        DbParameter[] parameters = { OLDNAME, NEWNAME, TYPE };
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
 

    public int Del_UPLOADWORD(string id)
    {
        try
        {
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = id;
            DbParameter[] parameters = { ID };
            DataExe.GetIntExeData("delete from HT_KJ_UPLOADWORD where ID=@ID", parameters);
            return 1;

        }
        catch (Exception ex)
        {
           
            return 0;
        }

    }

}



