using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_HT_SENDUNIT
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_HT_SENDUNIT()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        ///获取所有发送单位数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnitDataAll()
        {
            string sql = "select * from HT_SENDUNIT";
            try
            {
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发送单位数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        ///获取所有发送单位数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnitData(int pagenum, int pagerow)
        {
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;
            string sql = "select * from(select t.*,rownum rn from(select * from HT_SENDUNIT order by ID) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
            try
            {
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发送单位数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取发送单位总数
        /// </summary>
        /// <returns></returns>
        public int GetUnitCount()
        {
            try
            {
                return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_SENDUNIT "));
            }
            catch (Exception error) {
                WriteLog.Write("获取发送单位总数出现异常！" + error.Message + "\r\n" + error.StackTrace);
                return -1;
            }
        }
        /// <summary>
        ///获取发送单位数据
        /// </summary>
        /// <param name="sendUnit"></param>
        /// <returns></returns>
        public DataTable GetUnitDataForSendUnit(string sendUnit)
        {
            try
            {
                var sql = "select * from HT_SENDUNIT where SENDUNIT ='" + sendUnit + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                //throw ex;
                WriteLog.Write("获取发送单位数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }


        /// <summary>
        ///导入发送单位数据到数据库
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public int AddUnitData(HT_SENDUNIT unit)
        {
            string sql = "INSERT INTO  HT_SENDUNIT (ID,SENDUNIT,CREATEDATE) VALUES (SEQUENCE_HT_SENDUNIT.Nextval,@SENDUNIT,"
                + "to_date(@CREATEDATE,'yyyy-mm-dd hh24@mi@ss'))";

            var SENDUNIT = DataExe.GetDbParameter();
            var CREATEDATE = DataExe.GetDbParameter();

            SENDUNIT.ParameterName = "@SENDUNIT";
            CREATEDATE.ParameterName = "@CREATEDATE";

            SENDUNIT.Value = unit.SENDUNIT;
            CREATEDATE.Value = unit.CREATEDATE.ToString();

            DbParameter[] parameters = { SENDUNIT, CREATEDATE};
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加发送单位数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改文档存储表
        /// </summary>
        public int EditUnitData(HT_SENDUNIT unit)
        {
            string sql = "UPDATE HT_SENDUNIT set SENDUNIT=@SENDUNIT,"
                + "UPDATEDATE=to_date(@UPDATEDATE,'yyyy-mm-dd hh24@mi@ss') where ID=@ID";

            var SENDUNIT = DataExe.GetDbParameter();
            var UPDATEDATE = DataExe.GetDbParameter();
            var ID = DataExe.GetDbParameter();

            SENDUNIT.ParameterName = "@SENDUNIT";
            UPDATEDATE.ParameterName = "@UPDATEDATE";
            ID.ParameterName = "@ID";

            SENDUNIT.Value = unit.SENDUNIT;
            UPDATEDATE.Value = unit.UPDATEDATE.ToString();
            ID.Value = unit.ID.ToString();

            DbParameter[] parameters = { SENDUNIT, UPDATEDATE, ID };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改发送单位数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public int DelUnitData(int id)
        {
            try
            {
                var ID = DataExe.GetDbParameter();
                ID.ParameterName = "@ID";
                ID.Value = id;
                DbParameter[] parameters = { ID };

                int a = DataExe.GetIntExeData("delete from HT_SENDUNIT where ID = @ID", parameters);
                return a;
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除发送单位数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }

        }

        public DataTable GetGroupAndUnit()
        {
            string sql = "select ID,GROUPNAME,UNITNAME from HT_GROUPUNIT order by ID";
            try
            {
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发送单位数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }
    }
}