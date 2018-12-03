using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_CG_YUBAO_ME
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_CG_YUBAO_ME()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public int Add_CG_YUBAO_ME(CG_YUBAO_ME CG_YUBAO_ME)
        {

            string sql =
                "INSERT INTO CG_YUBAO_ME"
                + " (YBWENJIANMING,YBQUYU,YBNEIRONG,YBSHIXIAO,YBSHIJIAN,YBDANWEI) VALUES"
                + " (@YBWENJIANMING,@YBQUYU,@YBNEIRONG,@YBSHIXIAO,@YBSHIJIAN,@YBDANWEI)";

            var YBWENJIANMING = DataExe.GetDbParameter();
            var YBQUYU = DataExe.GetDbParameter();
            var YBNEIRONG = DataExe.GetDbParameter();
            var YBSHIXIAO = DataExe.GetDbParameter();
            var YBSHIJIAN = DataExe.GetDbParameter();
            var YBDANWEI = DataExe.GetDbParameter();

            YBWENJIANMING.ParameterName = "@YBWENJIANMING";
            YBQUYU.ParameterName = "@YBQUYU";
            YBNEIRONG.ParameterName = "@YBNEIRONG";
            YBSHIXIAO.ParameterName = "@YBSHIXIAO";
            YBSHIJIAN.ParameterName = "@YBSHIJIAN";
            YBDANWEI.ParameterName = "@YBDANWEI";

            YBWENJIANMING.Value = CG_YUBAO_ME.YBWENJIANMING;
            YBQUYU.Value = CG_YUBAO_ME.YBQUYU;
            YBNEIRONG.Value = CG_YUBAO_ME.YBNEIRONG;
            YBSHIXIAO.Value = CG_YUBAO_ME.YBSHIXIAO;
            YBSHIJIAN.Value = CG_YUBAO_ME.YBSHIJIAN;
            YBDANWEI.Value = CG_YUBAO_ME.YBDANWEI;

            DbParameter[] parameters = { YBWENJIANMING, YBQUYU,YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI };
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
        /// 文件属性保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_YUBAOME_AllData(CG_YUBAO_ME File)
        {
            try
            {
                string sql = "select YBWENJIANMING from CG_YUBAO_ME where YBWENJIANMING=@YBWENJIANMING";
                var YBWENJIANMING = DataExe.GetDbParameter();
                YBWENJIANMING.ParameterName = "@YBWENJIANMING";
                YBWENJIANMING.Value = File.YBWENJIANMING;
                DbParameter[] parameters = { YBWENJIANMING};
                return DataExe.GetTableExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 消息文件属性保存
        /// </summary>
        /// <returns></returns>
        public int UpdateYUBAOMe(CG_YUBAO_ME CG_YUBAO_ME)
        {
            string sql = "UPDATE CG_YUBAO_ME SET YBQUYU=@YBQUYU,YBNEIRONG=@YBNEIRONG,YBSHIXIAO=@YBSHIXIAO,YBSHIJIAN=to_date(@YBSHIJIAN,'yyyy-mm-dd'),YBDANWEI=@YBDANWEI where YBWENJIANMING=@YBWENJIANMING";
            var YBWENJIANMING = DataExe.GetDbParameter();
            var YBQUYU = DataExe.GetDbParameter();
            var YBNEIRONG = DataExe.GetDbParameter();
            var YBSHIXIAO = DataExe.GetDbParameter();
            var YBSHIJIAN = DataExe.GetDbParameter();
            var YBDANWEI = DataExe.GetDbParameter();

            YBWENJIANMING.ParameterName = "@YBWENJIANMING";
            YBQUYU.ParameterName = "@YBQUYU";
            YBNEIRONG.ParameterName = "@YBNEIRONG";
            YBSHIXIAO.ParameterName = "@YBSHIXIAO";
            YBSHIJIAN.ParameterName = "@YBSHIJIAN";
            YBDANWEI.ParameterName = "@YBDANWEI";

            YBWENJIANMING.Value = CG_YUBAO_ME.YBWENJIANMING;
            YBQUYU.Value = CG_YUBAO_ME.YBQUYU;
            YBNEIRONG.Value = CG_YUBAO_ME.YBNEIRONG;
            YBSHIXIAO.Value = CG_YUBAO_ME.YBSHIXIAO;
            YBSHIJIAN.Value = CG_YUBAO_ME.YBSHIJIAN.ToString("yyyy-MM-dd");
            YBDANWEI.Value = CG_YUBAO_ME.YBDANWEI;

            DbParameter[] parameters = { YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN,YBDANWEI, YBWENJIANMING };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("编辑消息文件属性出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


    }
}