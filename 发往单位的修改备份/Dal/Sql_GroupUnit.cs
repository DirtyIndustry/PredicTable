using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 发送单位分组
    /// </summary>
    public class Sql_GroupUnit
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_GroupUnit()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        ///获取所有发送单位分组数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnitGroupInfo(int pagenum, int pagerow)
        {
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;
            string sql = "select * from(select t.*,rownum rn from(select * from HT_GROUPUNIT order by ID) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
            try
            {
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发送单位分组数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// 获取发送单位分组总数
        /// </summary>
        /// <returns></returns>
        public int GetUnitGroupCount()
        {
            try
            {
                return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_GROUPUNIT "));
            }
            catch (Exception error)
            {
                WriteLog.Write("获取发送单位分组总数出现异常！" + error.Message + "\r\n" + error.StackTrace);
                return -1;
            }
        }

        /// <summary>
        ///导入发送单位分组数据到数据库
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public int AddUnitGroupData(GroupUnitModel groupUnitModel)
        {
            string sql = "INSERT INTO  HT_GROUPUNIT (ID,GROUPNAME,UNITNAME,CREATETIME) VALUES (GROUPUNIT.Nextval,@GROUPNAME,@UNITNAME, "
                + "to_date(@CREATETIME,'yyyy-mm-dd hh24@mi@ss'))";

            var GROUPNAME = DataExe.GetDbParameter();
            var UNITNAME = DataExe.GetDbParameter();
            var CREATETIME = DataExe.GetDbParameter();

            GROUPNAME.ParameterName = "@GROUPNAME";
            UNITNAME.ParameterName = "@UNITNAME";
            CREATETIME.ParameterName = "@CREATETIME";

            GROUPNAME.Value = groupUnitModel.GROUPNAME;
            UNITNAME.Value = groupUnitModel.UNITNAME;
            CREATETIME.Value = groupUnitModel.CREATETIME.ToString();

            DbParameter[] parameters = { GROUPNAME, UNITNAME, CREATETIME };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加发送单位分组数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改文档存储表
        /// </summary>
        public int EditUnitGroupData(GroupUnitModel groupUnitModel)
        {
            string sql = "UPDATE HT_GROUPUNIT set GROUPNAME=@GROUPNAME,UNITNAME=@UNITNAME"
                + " where ID=@ID";

            var GROUPNAME = DataExe.GetDbParameter();
            var UNITNAME = DataExe.GetDbParameter();
            var ID = DataExe.GetDbParameter();

            GROUPNAME.ParameterName = "@GROUPNAME";
            UNITNAME.ParameterName = "@UNITNAME";
            ID.ParameterName = "@ID";

            GROUPNAME.Value = groupUnitModel.GROUPNAME;
            UNITNAME.Value = groupUnitModel.UNITNAME;
            ID.Value = groupUnitModel.ID.ToString();

            DbParameter[] parameters = { GROUPNAME, UNITNAME, ID };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改发送单位分组数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public int DelUnitGroupData(int id)
        {
            try
            {
                var ID = DataExe.GetDbParameter();
                ID.ParameterName = "@ID";
                ID.Value = id;
                DbParameter[] parameters = { ID };

                int a = DataExe.GetIntExeData("delete from HT_GROUPUNIT where ID = @ID", parameters);
                return a;
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除发送单位分组数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }

        }
    }
}