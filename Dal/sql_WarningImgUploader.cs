using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_WarningImgUploader
    {
        DataExecution DataExe = new DataExecution();//声明一个数据执行类

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="docName">文件名</param>
        /// <returns></returns>
        public DataTable GetImgInfo(string tableName,string columnName,string docName)
        {
            try
            {
                string sql = "select * from "+ tableName + " where "+ columnName + "='" + docName + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int InsertImg(string tableName, string columnName1, string columnName2,string docName,byte[] bt)
        {
            //string sql = "INSERT INTO " + tableName + " (" + columnName1 + "," + columnName2 + ") VALUES ('"+ docName + "',"+ bt + ")";
            string sql = "INSERT INTO  " + tableName + " (" + columnName1 + "," + columnName2 + ") VALUES (@WENJIANMING,@PICTURE)";
            var WENJIANMING = DataExe.GetDbParameter();
            var PICTURE = DataExe.GetDbParameter();

            WENJIANMING.ParameterName = "@WENJIANMING";
            PICTURE.ParameterName = "@PICTURE";

            WENJIANMING.Value = docName;
            PICTURE.Value = bt;

            DbParameter[] parameters = { WENJIANMING, PICTURE };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateImg(string tableName, string columnName1, string columnName2, string docName, byte[] bt)
        {
            //string sql = "UPDATE  " + tableName + " SET  " + columnName2 + " = " + bt + "  WHERE " + columnName1 + " = '" + docName + "'";
            string sql = "UPDATE  " + tableName + " SET  " + columnName2 + " = @PICTURE WHERE  " + columnName1 + " = @WENJIANMING";

            var WENJIANMING = DataExe.GetDbParameter();
            var PICTURE = DataExe.GetDbParameter();

            WENJIANMING.ParameterName = "@WENJIANMING";
            PICTURE.ParameterName = "@PICTURE";

            WENJIANMING.Value = docName;
            PICTURE.Value = bt;

            DbParameter[] parameters = { WENJIANMING, PICTURE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}