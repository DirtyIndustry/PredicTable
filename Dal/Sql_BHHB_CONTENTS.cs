using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class Sql_BHHB_CONTENTS
    {
        DataExecution DataExe = new DataExecution();//声明一个数据执行类
        /// <summary>
        ///模板文件内容保存
        /// </summary>
        /// <returns></returns>
                                                   
        public int AddDocContent(HT_KJ_BHHB_CONTENTS Contentvalue)
        {
            string sql = "INSERT INTO  HT_KJ_BHHB_CONTENTS (FILENAME,CONTENT) VALUES (@FILENAME,@CONTENT)";
            var FILENAME = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();          

            FILENAME.ParameterName = "@FILENAME";
            CONTENT.ParameterName = "@CONTENT";
          

            FILENAME.Value = Contentvalue.FILENAME;
            CONTENT.Value = Contentvalue.CONTENT;
          
            DbParameter[] parameters = { FILENAME, CONTENT };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增模板文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 模板内容绑定
        /// </summary>
        /// <returns></returns>
        public DataTable GetContentsData(HT_KJ_BHHB_CONTENTS Contentvalue)
        {
            try
            {
              //  string sql = "select * from HT_KJ_BHHB_CONTENTS where FILENAME='"+ Contentvalue.FILENAME+ "'";


                string sql = "select * from HT_KJ_BHHB_CONTENTS where FILENAME=@FILENAME";

                var FILENAME = DataExe.GetDbParameter();

                FILENAME.ParameterName = "@FILENAME";

                FILENAME.Value = Contentvalue.FILENAME;


                DbParameter[] parameters = { FILENAME };


                return DataExe.GetTableExeData(sql, parameters);

                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

