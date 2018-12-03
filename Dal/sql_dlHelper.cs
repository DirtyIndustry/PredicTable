using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// sqlServer处理类
    /// </summary>
    public class sql_dlHelper
    {
        private string SqlConnectionString;    //数据库连接
        public sql_dlHelper()
        {
            SqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DlPreTableConStr"].ConnectionString;//连接数据库的字符串
        }
        
        /// <summary>  
        /// 打开数据库连接  
        /// </summary>  
        /// <param name="cn">连接</param>  
        public void Open(SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
        }

        /// <summary>  
        /// 关闭数据库连接  
        /// </summary>  
        /// <param name="cn">连接</param>  
        public void Close(SqlConnection cn)
        {
            if (cn != null)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Dispose();
            }
        }
        /// <summary>  
        /// 查询（DataSet）  
        /// </summary>  
        /// <param name="strSql">SQL语句</param>  
        /// <returns>查询结果</returns>  
        public DataSet GetData(string strSql)
        {
            SqlConnection cn = new SqlConnection(SqlConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter();
            try
            {
                Open(cn);
                sda = new SqlDataAdapter(strSql, cn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sda.Dispose();
                Close(cn);
            }
        }
    }
}