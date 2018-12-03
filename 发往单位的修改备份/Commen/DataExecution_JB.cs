using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Configuration;

/// <summary>
/// DataExecution 的摘要说明
/// </summary>
public class DataExecution_JB
{
    string LinkConfigNames = "DataBaseCon_JB";//用来存储数据库连接字符串的名称       
    DbProviderFactory Provider;//声明一个数据库执行通用类
    String ConnectionStr;//数据库连接字符串；
    String Conntype; 
    /// <summary>
    /// 使用配置文件中默认配置的进行数据库连接配置
    /// </summary>
    public DataExecution_JB()
    {
        LinkConfigName = LinkConfigNames;
    }
    /// <summary>
    /// 使用配置文件中指定名字类型的数据库进行连接操作
    /// </summary>
    /// <param name="dbconfigName">配置文件中链接数据库的字符串名称</param>
    public DataExecution_JB(string dbconfigName)
    {
        LinkConfigName = dbconfigName;
    }
    /// <summary>
    /// 获取或设置当前使用的数据库的链接字符名
    /// </summary>
    public string LinkConfigName
    {
        set {
            LinkConfigNames = value;
            ConnectionStr = ConfigurationManager.ConnectionStrings[LinkConfigNames].ConnectionString;
            Provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings[LinkConfigNames].ProviderName);
        }
        get { return LinkConfigNames; }
    }
    /// <summary>
    /// 根据执行语句返回一个整形
    /// </summary>
    /// <param name="comStr">要执行的sql语句</param>
    /// <returns></returns>
    public int GetIntExeData(String comStr)
    {
        DbConnection conn = Provider.CreateConnection();
        conn.ConnectionString = ConnectionStr;
        DbCommand com= Provider.CreateCommand();
        com.Connection = conn;
        com.CommandText = comStr;
        conn.Open();
        int count = com.ExecuteNonQuery();
        conn.Close();
        return count;
    }
    /// <summary>
    /// 根据执行语句返回一个object类型
    /// </summary>
    /// <param name="comStr">要执行的sql语句</param>
    /// <returns></returns>
    public object GetObjectExeData(String comStr)
    {
        DbConnection conn = Provider.CreateConnection();
        conn.ConnectionString = ConnectionStr;
        DbCommand com = Provider.CreateCommand();
        com.Connection = conn;
        com.CommandText = comStr;
        conn.Open();
        object count = com.ExecuteScalar();
        conn.Close();
        return count;
    }
    /// <summary>
    /// 根据执行语句返回一个Table类型
    /// </summary>
    /// <param name="comStr">要执行的sql语句</param>
    /// <returns></returns>
    public DataTable GetTableExeData(String comStr)
    {
        try
        {
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = ConnectionStr;
            DbCommand com = Provider.CreateCommand();
            com.Connection = conn;
            com.CommandText = comStr;
            DbDataAdapter ada = Provider.CreateDataAdapter();
            conn.Open();
            ada.SelectCommand = com;
            DataSet ds = new DataSet();
            ada.Fill(ds, "tb");
            conn.Close();
            return ds.Tables["tb"];
        }
        catch (Exception ex)
        {
            WriteLog.Write("连接出错"+ex.ToString());
            return null;
        }
        
    }
   /// <summary>
   /// 执行带参数sql语句并返回整形的执行结果
   /// </summary>
   /// <param name="comStr">sql命令</param>
   /// <param name="paravalues">命令中的参数</param>
   /// <returns></returns>
    public int GetIntExeData(String comStr, DbParameter[] paravalues)
    {
        DbConnection conn = Provider.CreateConnection();
        conn.ConnectionString = ConnectionStr;
        DbCommand com = Provider.CreateCommand();
        com.Connection = conn;
      
        com.CommandType = CommandType.Text;
        if (Provider.GetType().ToString().Contains("Oracle"))
        {
            for (int i = 0; i < paravalues.Length; i++)
            {
                string pname = paravalues[i].ParameterName;
                string xnames = pname.Replace("@", ":");
                comStr = comStr.Replace(pname,xnames);
                paravalues[i].ParameterName = xnames;
            }
        }
        com.CommandText = comStr;
        for (int i = 0; i < paravalues.Length; i++)
        {
          
            DbParameter dbpar=  com.CreateParameter();
            dbpar.ParameterName = paravalues[i].ParameterName;
            dbpar.Value = paravalues[i].Value;
            com.Parameters.Add(dbpar);
        }
        conn.Open();
        int count = com.ExecuteNonQuery();
        conn.Close();
        return count;
    }
    /// <summary>
    /// 返回一个可执行的DbCommand类 注意:需要手动打开和关闭DbCommand的数据库连接
    /// </summary>
    /// <returns></returns>
    public DbCommand GetDbCommandExeData()
    {
        DbConnection conn = Provider.CreateConnection();
        conn.ConnectionString = ConnectionStr;
        DbCommand com = Provider.CreateCommand();
        com.Connection = conn;
        return com;
    }



    /// <summary>
    /// 根据当前的类型数据库创建并返回一个sql参数  DbParameter
    /// </summary>
    /// <returns></returns>
    public DbParameter GetDbParameter()
    {
        return Provider.CreateParameter();
    }
   /// <summary>
   /// 带参数的sql查询将查询结果以DataTable返回
   /// </summary>
   /// <param name="comStr">要执行select查询语句</param>
   /// <param name="paravalues">查询语句中包含的参数</param>
   /// <returns></returns>
    public DataTable GetTableExeData(String comStr, DbParameter[] paravalues)
    {
       
        DbConnection conn = Provider.CreateConnection();
        conn.ConnectionString = ConnectionStr;
        DbCommand com = Provider.CreateCommand();
        com.Connection = conn;

        com.CommandType = CommandType.Text;
        if (Provider.GetType().ToString().Contains("Oracle"))
        {
            for (int i = 0; i < paravalues.Length; i++)
            {
                string pname = paravalues[i].ParameterName;
                string xnames = pname.Replace("@", ":");
                comStr = comStr.Replace(pname, xnames);
                paravalues[i].ParameterName = xnames;
            }
        }
        com.CommandText = comStr;
        for (int i = 0; i < paravalues.Length; i++)
        {

            DbParameter dbpar = com.CreateParameter();
            dbpar.ParameterName = paravalues[i].ParameterName;
            dbpar.Value = paravalues[i].Value;
            com.Parameters.Add(dbpar);
        }
        DbDataAdapter ada = Provider.CreateDataAdapter();
        ada.SelectCommand = com;
        try//超时或数据库连接错误 会报错
        {
        DataSet ds = new DataSet();
        ada.Fill(ds, "tb");
         conn.Open();
         DataTable dt=  ds.Tables["tb"];
         conn.Close();
          return dt;
        }
        catch (Exception)
        {
            return null;
        }  
       
       
    }

    public object GetObjectExeData(String comStr, DbParameter[] paravalues)
    {
        DbConnection conn = Provider.CreateConnection();
        conn.ConnectionString = ConnectionStr;
        DbCommand com = Provider.CreateCommand();
        com.Connection = conn;

        com.CommandType = CommandType.Text;
        if (Provider.GetType().ToString().Contains("Oracle"))
        {
            for (int i = 0; i < paravalues.Length; i++)
            {
                string pname = paravalues[i].ParameterName;
                string xnames = pname.Replace("@", ":");
                comStr = comStr.Replace(pname, xnames);
                paravalues[i].ParameterName = xnames;
            }
        }

        com.CommandText = comStr;

        for (int i = 0; i < paravalues.Length; i++)
        {

            DbParameter dbpar = com.CreateParameter();
            dbpar.ParameterName = paravalues[i].ParameterName;
            dbpar.Value = paravalues[i].Value;
            com.Parameters.Add(dbpar);
        }

        conn.Open();
        object count = com.ExecuteScalar();
        conn.Close();
        return count;
    }

}