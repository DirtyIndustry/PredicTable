
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 发送单位
    /// </summary>
    public class sql_CommonSendUnit
    {
        DataExecution DataExe;//声明一个数据执行类
        DataExecution_JB DataExe_JB;//声明一个数据执行类
        public sql_CommonSendUnit()
        {
            DataExe = new DataExecution();//声明一个数据执行类
            DataExe_JB = new DataExecution_JB();//声明一个数据执行类
        }

        /// <summary>
        /// 获取发送单位
        /// </summary>
        /// <returns></returns>
        public DataTable GetSendUnit(string docName)
        {
            try
            {
                //string sql = "SELECT * FROM CG_HT_YUBAO_CONTENT WHERE YBWENJIANMING = '" + docName + "'";
                string sql = "SELECT * FROM CG_HT_YUBAO_CONTENT WHERE YBWENJIANMING = @YBWENJIANMING";


                var YBWENJIANMING = DataExe.GetDbParameter();

               
                YBWENJIANMING.ParameterName = "@YBWENJIANMING";

             
                YBWENJIANMING.Value = docName;

                DbParameter[] parameters = { YBWENJIANMING };
                return DataExe.GetTableExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发送单位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 插入发送单位数据
        /// </summary>
        /// <param name="docName"> 文档名称</param>
        /// <param name="sendUnits">发送单位</param>
        /// <returns></returns>
        public int InsertSendUnit(string docName, string sendUnits)
        {
            try
            {
                string sql = "INSERT INTO CG_HT_YUBAO_CONTENT ( YBWENJIANMING ,SENTTO ) VALUES ('" + docName + "','"+ sendUnits+"')";
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加发送单位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 插入发送单位数据
        /// </summary>
        /// <param name="docName"> 文档名称</param>
        /// <param name="sendUnits">发送单位</param>
        /// <param name="BZ">发往备注</param>
        /// <returns></returns>
        public int InsertSendUnit1(string docName, string sendUnits, string BZ1)
        {
            try
            {
                string sql = "INSERT INTO CG_HT_YUBAO_CONTENT ( YBWENJIANMING ,SENTTO,BZ ) VALUES (@YBWENJIANMING,@SENTTO,@BZ)";
                var SENTTO = DataExe.GetDbParameter();
                var BZ = DataExe.GetDbParameter();
                var YBWENJIANMING = DataExe.GetDbParameter();

                SENTTO.ParameterName = "@SENTTO";
                BZ.ParameterName = "@BZ";
                YBWENJIANMING.ParameterName = "@YBWENJIANMING";

                SENTTO.Value = sendUnits;
                BZ.Value = BZ1;
                YBWENJIANMING.Value = docName;

                DbParameter[] parameters = { SENTTO, BZ, YBWENJIANMING };
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加发送单位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 更新发送单位数据
        /// </summary>
        /// <param name="docName"></param>
        /// <param name="sendUnits"></param>
        /// <returns></returns>
        public int UpdateSendUnit(string docName, string sendUnits)
        {
            try
            {
                string sql = "UPDATE CG_HT_YUBAO_CONTENT SET SENTTO =  '" + sendUnits + "' WHERE YBWENJIANMING ='" + docName + "' ";
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改发送单位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 更新发送单位数据
        /// </summary>
        /// <param name="docName"></param>
        /// <param name="sendUnits"></param>
        /// <param name="BZ"></param>
        /// <returns></returns>
        public int UpdateSendUnit1(string docName, string sendUnits, string BZ1)
        {
            try
            {
                //string sql = "UPDATE CG_HT_YUBAO_CONTENT SET SENTTO =  '" + sendUnits + "',BZ= '" + BZ1 + "' WHERE YBWENJIANMING ='" + docName + "' ";
                string sql = "UPDATE CG_HT_YUBAO_CONTENT SET SENTTO =  @SENTTO,BZ= @BZ WHERE YBWENJIANMING =@YBWENJIANMING ";

                var SENTTO = DataExe.GetDbParameter();
                var BZ = DataExe.GetDbParameter();
                var YBWENJIANMING = DataExe.GetDbParameter();

                SENTTO.ParameterName = "@SENTTO";
                BZ.ParameterName = "@BZ";
                YBWENJIANMING.ParameterName = "@YBWENJIANMING";

                SENTTO.Value = sendUnits;
                BZ.Value = BZ1;
                YBWENJIANMING.Value = docName;

                DbParameter[] parameters = { SENTTO, BZ, YBWENJIANMING};
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改发送单位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}