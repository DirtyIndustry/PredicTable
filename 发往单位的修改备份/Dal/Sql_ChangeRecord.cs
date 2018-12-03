//变更记录 add by yy in 2018-04-17
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using PredicTable.Model;
using System.Data.Common;

namespace PredicTable.Dal
{
    /// <summary>
    /// 数据变更Dal
    /// </summary>
    public class Sql_ChangeRecord
    {
        DataExecution DataExe;//声明一个数据执行类

        public Sql_ChangeRecord()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        /// 获取变更记录信息
        /// </summary>
        /// <param name="pagenum">页数</param>
        /// <param name="pagerow">一页中的行数</param>
        /// <returns></returns>
        public DataTable GetChangeRecord(int pagenum, int pagerow)
        {
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;
            string sql = "select * from (select t.*,rownum rn from(select ID,CHANGECONTENT,CHANGEPERSON,CJSJ from HT_CHANGERECORD order by CJSJ) t where rownum<=" + pagelast + ") where rn >=" + pagefist + "";
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
        /// 获取变更记录总数
        /// </summary>
        /// <returns></returns>
        public int GetChangeRecordCount()
        {
            try {
                return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_CHANGERECORD "));
            } catch (Exception ex) {
                WriteLog.Write("获取变更记录总数异常！"+ex.Message+"\r\n"+ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 提交变更记录
        /// </summary>
        /// <param name="Changemodel"></param>
        /// <returns></returns>
        public int SubmitChangeRecord(ChangeRecordModel Changemodel)
        {
            string sql = "INSERT INTO HT_CHANGERECORD(ID,CHANGECONTENT,CHANGEPERSON,CJSJ) VALUES (CHANGERECORD.Nextval,@ChangeContent,@ChangePerson,@ChangeCJSJ)";
            var ChangeContent = DataExe.GetDbParameter();
            var ChangePerson = DataExe.GetDbParameter();
            var ChangeCJSJ = DataExe.GetDbParameter();

            ChangeContent.ParameterName = "@ChangeContent";
            ChangePerson.ParameterName = "@ChangePerson";
            ChangeCJSJ.ParameterName = "@ChangeCJSJ";

            ChangeContent.Value = Changemodel.ChangeContent;
            ChangePerson.Value = Changemodel.ChangePerson;
            ChangeCJSJ.Value = Changemodel.ChangeTime;
            DbParameter[] parameters = { ChangeContent, ChangePerson, ChangeCJSJ };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加变更记录异常" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }

        }
        /// <summary>
        /// 修改变更记录
        /// </summary>
        /// <param name="changeModel"></param>
        /// <returns></returns>
        public int EditChangeRecord(ChangeRecordModel changeModel)
        {
            string sql = "update HT_CHANGERECORD set CHANGECONTENT=@ChangeContent,CHANGEPERSON=@ChangePerson WHERE ID = @ID";
            var ID = DataExe.GetDbParameter();
            var ChangeContent= DataExe.GetDbParameter();
            var ChangePerson=DataExe.GetDbParameter();

            ID.ParameterName = "@ID";
            ChangeContent.ParameterName = "@ChangeContent";
            ChangePerson.ParameterName = "@ChangePerson";

            ID.Value = changeModel.ID;
            ChangeContent.Value = changeModel.ChangeContent;
            ChangePerson.Value = changeModel.ChangePerson;
            DbParameter[] param = { ID, ChangeContent, ChangePerson };
            try
            {
                return DataExe.GetIntExeData(sql, param);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改变更记录异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
            //return -1;
        }
        /// <summary>
        /// 删除变更记录
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <returns></returns>
        public int DeleteChangeRecord(int id)
        {
            string sql = "delete from HT_CHANGERECORD where ID = " + id;
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch(Exception ex) {
                WriteLog.Write("删除变更记录异常" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}