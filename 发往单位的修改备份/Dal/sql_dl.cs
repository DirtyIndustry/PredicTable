using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_dl
    {
        sql_dlHelper sqlHelper = null;
        public sql_dl()
        {
            sqlHelper = new sql_dlHelper();
        }
        /// <summary>
        /// 获取大连海浪数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetTide(string station,string fillTime,string preTime)
        {
            try
            {
                string sql = "SELECT * FROM ImportTide WHERE PreStation='"+ station + "' AND FillTime='"+ fillTime + "' AND PreTime='"+ preTime + "' ";
                return sqlHelper.GetData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取大连潮汐数据异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }
    }
}