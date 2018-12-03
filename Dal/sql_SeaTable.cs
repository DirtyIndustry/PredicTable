using PredicTable.Model.NineteenWord;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_SeaTable
    {
        DataExecution DataExe = new DataExecution();//声明一个数据执行类
        public DataTable GetTableData(NineteenNomalLineModel nomalModel,string WENJIANMING)
        {
            try
            {
                string sql = "SELECT * FROM CG_HT_SEATABLE WHERE WENJIANMING = '" + WENJIANMING + "' AND NAME = '" + nomalModel.NAME + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int InsertTableData(NineteenNomalLineModel nomalModel, string WENJIANMING)
        {
            string sql = "INSERT INTO CG_HT_SEATABLE (WENJIANMING,NAME,TERMINALLINE ,GENERALICETHICKNESS,MAXICETHICKNESS) VALUES ("
                + " '"+ WENJIANMING + "','"+ nomalModel.NAME + "','" + nomalModel.TERMINALLINE + "','" + nomalModel.GENERALICETHICKNESS + "','" + nomalModel.MAXICETHICKNESS + "')";
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增模板文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        public int UpdateTableData(NineteenNomalLineModel nomalModel, string WENJIANMING)
        {
            string sql = "UPDATE CG_HT_SEATABLE SET TERMINALLINE =  '" + nomalModel.TERMINALLINE + "' ,GENERALICETHICKNESS = '" + nomalModel.GENERALICETHICKNESS + "',MAXICETHICKNESS = '" + nomalModel.MAXICETHICKNESS + "' WHERE WENJIANMING='" + WENJIANMING + "' AND NAME = '" + nomalModel.NAME + "'";
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增模板文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}