using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class Sql_BHHBSendUnit
    {
        DataExecution_JB DataExe_JB;
        public Sql_BHHBSendUnit()
        {
            DataExe_JB = new DataExecution_JB();//声明一个数据执行类
        }

        /// <summary>
        /// 获取冀东、胜利油田、东营、青岛发送单位
        /// </summary>
        /// <returns></returns>
        public object GetHBSendUnits(string unitGroup)
        {
            try
            {
                string sql = "SELECT B.* FROM tblfaxcontacts A "
                          + " INNER JOIN tblfaxcontactsdetails B"
                          + " ON a.faxid = b.faxid"
                          + " WHERE a.faxgroup = '" + unitGroup + "'";
                return DataExe_JB.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发送单位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}