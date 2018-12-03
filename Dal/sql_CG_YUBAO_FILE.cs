using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_CG_YUBAO_FILE
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_CG_YUBAO_FILE()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public int Add_CG_YUBAO_FILE(CG_YUBAO_FILE CG_YUBAO_FILE)
        {

            string sql = "INSERT INTO  CG_YUBAO_FILE (YBWENJIANMING,YBNEIRONG,PICFILE) VALUES (@YBWENJIANMING,@YBNEIRONG,@PICFILE)";

            var YBWENJIANMING = DataExe.GetDbParameter();
            var YBNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            YBWENJIANMING.ParameterName = "@YBWENJIANMING";
            YBNEIRONG.ParameterName = "@YBNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            YBWENJIANMING.Value = CG_YUBAO_FILE.YBWENJIANMING;
            YBNEIRONG.Value = CG_YUBAO_FILE.YBNEIRONG;
            PICFILE.Value = CG_YUBAO_FILE.PICFILE;

            DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE};
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("插入word！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public int Update_CG_YUBAO_FILE(CG_YUBAO_FILE CG_YUBAO_FILE)
        {
            string sql = "UPDATE CG_YUBAO_FILE SET YBNEIRONG=@YBNEIRONG,PICFILE=@PICFILE where YBWENJIANMING=@YBWENJIANMING";

            var YBWENJIANMING = DataExe.GetDbParameter();
            var YBNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            YBWENJIANMING.ParameterName = "@YBWENJIANMING";
            YBNEIRONG.ParameterName = "@YBNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            YBWENJIANMING.Value = CG_YUBAO_FILE.YBWENJIANMING;
            YBNEIRONG.Value = CG_YUBAO_FILE.YBNEIRONG;
            PICFILE.Value = CG_YUBAO_FILE.PICFILE;

            DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("插入word！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 消息文件保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_YuBaoFILE_AllData(CG_YUBAO_FILE JingBaomFile)
        {
            try
            {
                string sql = "select YBWENJIANMING from CG_YUBAO_FILE where YBWENJIANMING=@YBWENJIANMING";

                var YBWENJIANMING = DataExe.GetDbParameter();

                YBWENJIANMING.ParameterName = "@YBWENJIANMING";

                YBWENJIANMING.Value = JingBaomFile.YBWENJIANMING;

                DbParameter[] parameters = { YBWENJIANMING};

                return DataExe.GetTableExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}