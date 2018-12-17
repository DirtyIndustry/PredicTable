using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace PredicTable.WebServiceClass
{
    public static class OperatorSQL
    {

        #region 数据访问方法
        public static DataTable queryData(string sql)
        {
            DataTable result = new DataTable();

            string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory Provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = ConnectionStr;
            DbCommand command = Provider.CreateCommand();
            command.Connection = conn;
            command.CommandText = sql;
            DbDataAdapter ada = Provider.CreateDataAdapter();
            try
            {
                conn.Open();
                ada.SelectCommand = command;
                DataSet ds = new DataSet();
                ada.Fill(ds, "tb");
                result = ds.Tables["tb"];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static DataTable queryData(string sql, DbConnection connection)
        {
            DataTable result = new DataTable();
            string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
            DbCommand command = provider.CreateCommand();
            command.Connection = connection;
            command.CommandText = sql;
            DbDataAdapter adapter = provider.CreateDataAdapter();
            try
            {
                adapter.SelectCommand = command;
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "table");
                result = dataset.Tables["table"];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return result;
        }

        public static DbConnection getDBConnection()
        {
            string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory Provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = ConnectionStr;
            return conn;
        }

        public static int executeSql(string sql, List<DbParameter> parameters)
        {
            int result = 0;

            string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory Provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = ConnectionStr;
            DbCommand command = Provider.CreateCommand();
            command.Connection = conn;
            command.CommandType = CommandType.Text;
            command.Parameters.Clear();
            for (int i = 0; i < parameters.Count; i++)
            {
                //if (sql.Contains("'" + parameters[i].ParameterName + "'"))
                //{
                //    string oldname = parameters[i].ParameterName;
                //    string newname = oldname.Replace("@", ":");
                //    parameters[i].ParameterName = oldname.Replace("@", "");
                //    sql = sql.Replace("'" + oldname + "'", "'" + newname + "'");
                //    command.Parameters.Add(parameters[i]);
                //}
                sql = sql.Replace("'" + parameters[i].ParameterName + "'", "'" + parameters[i].Value.ToString() + "'");
            }
            command.CommandText = sql;

            foreach (DbParameter p in command.Parameters)
            {
                System.Diagnostics.Debug.WriteLine(p.ParameterName + ": " + p.Value);
            }

            try
            {
                conn.Open();
                System.Diagnostics.Debug.WriteLine(command.CommandText);
                result = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                command.Dispose();
                conn.Close();
            }
            return result;
        }

        public static List<DbParameter> buildParameters<T>(T source) where T : class
        {
            List<DbParameter> result = new List<DbParameter>();

            string connectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);

            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                // System.Diagnostics.Debug.WriteLine(prop.PropertyType + " " + prop.Name + " : " + prop.GetValue(source, null));
                DbParameter parameter = provider.CreateParameter();
                // OracleParameter p = new OracleParameter();
                parameter.ParameterName = "@" + prop.Name;
                // p.ParameterName = "@" + prop.Name;
                if (prop.PropertyType == typeof(DateTime))
                {
                    parameter.Value = ((DateTime)prop.GetValue(source, null)).ToLocalTime().ToString("yyyy-MM-dd");
                    // p.OracleType = OracleType.VarChar;
                    // p.Value = ((DateTime)prop.GetValue(source, null)).ToString("yyyy-MM-dd");
                }
                else
                {
                    parameter.Value = (string)prop.GetValue(source, null);
                    // p.OracleType = OracleType.VarChar;
                    // p.Value = (string)prop.GetValue(source, null);
                }

                result.Add(parameter);
                // result.Add(p);
            }
            return result;
        }

        /// <summary>
        /// DataTable转化为List集合
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="dt">datatable表</param>
        /// <param name="isStoreDB">是否存入数据库datetime字段，date字段没事，取出不用判断</param>
        /// <returns>返回list集合</returns>
        public static List<T> TableToList<T>(DataTable dt, bool isStoreDB = true)
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            //List<string> listColums = new List<string>();
            PropertyInfo[] pArray = type.GetProperties(); //集合属性数组
            foreach (DataRow row in dt.Rows)
            {
                T entity = Activator.CreateInstance<T>(); //新建对象实例 
                foreach (PropertyInfo p in pArray)
                {
                    if (!dt.Columns.Contains(p.Name) || row[p.Name] == null || row[p.Name] == DBNull.Value)
                    {
                        continue;  //DataTable列中不存在集合属性或者字段内容为空则，跳出循环，进行下个循环   
                    }
                    if (isStoreDB && p.PropertyType == typeof(DateTime) && Convert.ToDateTime(row[p.Name]) < Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    try
                    {
                        var obj = Convert.ChangeType(row[p.Name], p.PropertyType);//类型强转，将table字段类型转为集合字段类型  
                        p.SetValue(entity, obj, null);
                    }
                    catch (Exception)
                    {
                        // throw;
                    }
                    //if (row[p.Name].GetType() == p.PropertyType)
                    //{
                    //    p.SetValue(entity, row[p.Name], null); //如果不考虑类型异常，foreach下面只要这一句就行
                    //}                    
                    //object obj = null;
                    //if (ConvertType(row[p.Name], p.PropertyType,isStoreDB, out obj))
                    //{                                        
                    //    p.SetValue(entity, obj, null);
                    //}                
                }
                list.Add(entity);
            }
            return list;
        }

        public static string DataTableToJson(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return "";
            }

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        #endregion

    }
}