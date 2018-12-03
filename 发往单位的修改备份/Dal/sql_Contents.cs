using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 联系人管理
    /// </summary>
    public class sql_Contents
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_Contents()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagerow"></param>
        /// <returns></returns>
        public DataTable GetContents(int pagenum, int pagerow)
        {
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;

            string sql = "select * from(select t.*,rownum rn from(" +
                    " SELECT A.ID,A.CONTENTSNAME, A.CONTENTSCODE  FROM HT_CONTENTS A" +
                    " ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                return null;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取联系人总数
        /// </summary>
        /// <returns></returns>
        public int GetContentsCount()
        {
            try
            {
                return Convert.ToInt32(DataExe.GetObjectExeData("SELECT COUNT(*) from HT_CONTENTS "));
            }
            catch (Exception error)
            {
                WriteLog.Write("获取联系人总数出现异常！" + error.Message + "\r\n" + error.StackTrace);
                return -1;
            }
        }
        /// <summary>
        /// 添加联系人信息
        /// </summary>
        /// REPORTER
        /// <returns></returns>
        public int SubmitContents(string contentsName,string contentCode)
        {
            string sql = "INSERT INTO  HT_CONTENTS (ID,CONTENTSNAME,CONTENTSCODE ) VALUES (CONTENTS.Nextval,@CONTENTSNAME,@CONTENTSCODE)";

            var CONTENTSNAME = DataExe.GetDbParameter();
            var CONTENTSCODE = DataExe.GetDbParameter();

            CONTENTSNAME.ParameterName = "@CONTENTSNAME";
            CONTENTSCODE.ParameterName = "@CONTENTSCODE";

            CONTENTSNAME.Value = contentsName;
            CONTENTSCODE.Value = contentCode;

            DbParameter[] parameters = { CONTENTSNAME, CONTENTSCODE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加联系人信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改联系人信息
        /// </summary>
        /// <returns></returns>
        public int EditContents(string id,string contentsName,string contentsCode)
        {
            string sql = "UPDATE  HT_CONTENTS SET  CONTENTSNAME = @CONTENTSNAME, CONTENTSCODE = @CONTENTSCODE WHERE ID = @ID";

            var ID = DataExe.GetDbParameter();
            var CONTENTSNAME = DataExe.GetDbParameter();
            var CONTENTSCODE = DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            CONTENTSNAME.ParameterName = "@CONTENTSNAME";
            CONTENTSCODE.ParameterName = "@CONTENTSCODE";

            ID.Value = id;
            CONTENTSNAME.Value = contentsName;
            CONTENTSCODE.Value = contentsCode;

            DbParameter[] parameters = { ID, CONTENTSNAME, CONTENTSCODE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改联系人信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 删除联系人信息
        /// </summary>
        /// <param name="id">预报员id</param>
        /// <returns></returns>
        public int DeleteContents(int id)
        {
            string sql = "DELETE FROM HT_CONTENTS WHERE ID = " + id;

            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除联系人信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}