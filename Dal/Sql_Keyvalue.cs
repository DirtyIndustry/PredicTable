using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Sql_Keyvalue 的摘要说明
/// </summary>
public class Sql_Keyvalue
{
    DataExecution DataExe;//声明一个数据执行类
    public Sql_Keyvalue()
    {
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
    }



    /// <summary>
    /// 获取系统信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetAll()
    {
        string sql = "select * from HT_KJ_KEYVALUE ";
        try
        {
            return DataExe.GetTableExeData(sql);
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取系统信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return null;
        }
    }
    public string GetByKey(string key) {
        return "";
    }

    /// <summary>
    /// 修改系统信息
    /// </summary>
    /// <returns></returns>
    public int  EditByKey(KJ_Keyvalue keyvalue)
    {
        string sql = "UPDATE   HT_KJ_KEYVALUE set VALUE=@VALUE where KEY=@KEY ";
        var KEY = DataExe.GetDbParameter();
        var VALUE = DataExe.GetDbParameter();
        KEY.ParameterName = "@KEY";
        VALUE.ParameterName = "@VALUE";
        KEY.Value = keyvalue.Key;
        VALUE.Value = keyvalue.Value;
        DbParameter[] parameters = { KEY, VALUE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改系统信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 添加系统信息
    /// </summary>
    /// <returns></returns>
    public int AddKeyValue(KJ_Keyvalue keyvalue)
    {
        string sql = "INSERT INTO  HT_KJ_KEYVALUE (KEY,VALUE) VALUES (@KEY,@VALUE)";
        var KEY = DataExe.GetDbParameter();
        var VALUE = DataExe.GetDbParameter();
        KEY.ParameterName = "@KEY";
        VALUE.ParameterName = "@VALUE";
        KEY.Value = keyvalue.Key;
        VALUE.Value = keyvalue.Value;
        DbParameter[] parameters = { KEY, VALUE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增系统信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    ///判断有没有存在
    /// </summary>
    /// <param name="keyvalue"></param>
    /// <returns></returns>
    public int KeyValuecount(KJ_Keyvalue keyvalue) {
      //  return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_KJ_USER "));
        string sql = "select count(*) from HT_KJ_KEYVALUE  where KEY=@KEY";
       // string sql = "INSERT INTO  HT_KJ_KEYVALUE (KEY,VALUE) VALUES (@KEY,@VALUE)";
        var KEY = DataExe.GetDbParameter();
        KEY.ParameterName = "@KEY";
        KEY.Value = keyvalue.Key;
        DbParameter[] parameters = { KEY};
        try
        {
            return Convert.ToInt32(DataExe.GetObjectExeData(sql, parameters));
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取系统信息个数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
}