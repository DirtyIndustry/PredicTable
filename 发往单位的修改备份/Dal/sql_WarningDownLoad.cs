using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 警报单下载、查询等处理
    /// </summary>
    public class sql_WarningDownLoad
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_WarningDownLoad()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        ///// <summary>
        ///// 获取列表
        ///// </summary>
        ///// <param name="pagenum"></param>
        ///// <param name="pagerow"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public DataTable GetWarningList(int pagenum, int pagerow, string type)
        //{
        //    int pagefist = pagerow * (pagenum - 1) + 1;
        //    int pagelast = pagerow * (pagenum - 1) + pagerow;

        //    string sql = "select * from(select t.*,rownum, rn from(";
        //    //" SELECT A.ID,A.CONTENTSNAME, A.CONTENTSCODE  FROM HT_CONTENTS A" +
        //    if (type == "XX")
        //    {
        //        sql += " SELECT A.XXWENJIANMING docName FROM CG_XIAOXI_file A";
        //    }
        //    else if (type == "JB")
        //    {
        //        sql += " SELECT A.JBWENJIANMING docName  FROM CG_JINGBAO_file A";
        //    }
        //    else if (type == "JC")
        //    {
        //        sql += " SELECT A.JCWENJIANMING docName FROM CG_JIECHUJINGBAO_ME A";
        //    }
        //    sql +=  " ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + " ORDER BY docName DESC ";
        //    try
        //    {
        //        DataTable dt = DataExe.GetTableExeData(sql);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            return dt;
        //        }
        //        return null;

        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
        //        return null;
        //    }
        //}



        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagerow"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetWarningList(int pagenum, int pagerow, string type)
        {
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;

            string sql = "select * from(select t.*,rownum rn from(";
            //" SELECT A.ID,A.CONTENTSNAME, A.CONTENTSCODE  FROM HT_CONTENTS A" +
            if (type == "XX")
            {
                sql += " select CG_XIAOXI_FILE.XXWENJIANMING docName,CG_XIAOXI_ME.xxshijian sj from CG_XIAOXI_FILE ,CG_XIAOXI_ME where CG_XIAOXI_FILE.XXWENJIANMING = CG_XIAOXI_ME.XXWENJIANMING";
            }
            else if (type == "JB")
            {
                sql += "select CG_JINGBAO_FILE.JBWENJIANMING docName,CG_JINGBAO_ME.JBSHIJIAN  sj from CG_JINGBAO_FILE ,CG_JINGBAO_ME where CG_JINGBAO_FILE.JBWENJIANMING = CG_JINGBAO_ME.JBWENJIANMING";
            }
            else if (type == "JC")
            {
                
                sql += " select CG_JIECHUJINGBAO_FILE.JCWENJIANMING docName,CG_JIECHUJINGBAO_ME.JCSHIJIAN sj from CG_JIECHUJINGBAO_FILE, CG_JIECHUJINGBAO_ME where CG_JIECHUJINGBAO_FILE.JCWENJIANMING = CG_JIECHUJINGBAO_ME.JCWENJIANMING";
            }
            //sql += " ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + " ORDER BY sj DESC ";
            sql += " ORDER BY sj DESC  ) t where rownum<=" + pagelast + ") where rn>=" + pagefist;
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
        /// 获取警报数
        /// </summary>
        /// <returns></returns>
        public int GetWarningListCount(string type)
        {
            try
            {
                var sql = "";
                if (type == "XX")
                {
                    sql += " SELECT COUNT(*) FROM CG_XIAOXI_FILE A";
                }
                else if (type == "JB")
                {
                    sql += " SELECT COUNT(*) FROM CG_JINGBAO_FILE A";
                }
                else if (type == "JC")
                {
                    sql += " SELECT COUNT(*) FROM CG_JIECHUJINGBAO_FILE A";
                }
                return Convert.ToInt32(DataExe.GetObjectExeData(sql));
            }
            catch (Exception error)
            {
                WriteLog.Write("获取警报总数出现异常！" + error.Message + "\r\n" + error.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileType">文件类型（消息、警报、警报解除）</param>
        /// <returns></returns>
        public object GetWarningListFileFlow(string fileName,string fileType)
        {
            try
            {
                var sql = "";
                if (fileType == "XX")
                {
                    sql += " SELECT A.XXNEIRONG FILEfLOW,A.XXWENJIANMING DOCNAME FROM CG_XIAOXI_FILE A WHERE A.XXWENJIANMING = '" + fileName +"'";
                }
                else if (fileType == "JB")
                {
                    sql += " SELECT A.JBNEIRONG FILEfLOW,A.JBWENJIANMING DOCNAME FROM CG_JINGBAO_FILE A WHERE A.JBWENJIANMING = '" + fileName + "'";
                }
                else if (fileType == "JC")
                {
                    sql += " SELECT A.JCNEIRONG FILEfLOW,A.JCWENJIANMING DOCNAME FROM CG_JIECHUJINGBAO_FILE A WHERE A.JCWENJIANMING = '" + fileName + "'";
                }
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除警报（文件信息表）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Deletejb(string fileName, string type)
        {
            string sql = "";
            if (type == "XX")
            {
                sql = "DELETE FROM CG_XIAOXI_ME WHERE XXWENJIANMING = '" + fileName + "'";

                
            }
            else if (type == "JB")
            {
                sql = "DELETE FROM CG_JINGBAO_ME WHERE JBWENJIANMING = '" + fileName + "'";
            }
            else if (type == "JC")
            {
                sql = "DELETE FROM CG_JIECHUJINGBAO_ME WHERE JCWENJIANMING = '" + fileName + "'";
            }
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除警报信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 检查警报（文件信息表）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Checkjb(string fileName, string type)
        {
            string sql = "";
            if (type == "XX")
            {
                sql = "SELECT * FROM CG_XIAOXI_ME WHERE XXWENJIANMING = '" + fileName + "'";

            }
            else if (type == "JB")
            {
                sql = "SELECT *  FROM CG_JINGBAO_ME WHERE JBWENJIANMING = '" + fileName + "'";
            }
            else if (type == "JC")
            {
                sql = "SELECT *  FROM CG_JIECHUJINGBAO_ME WHERE JCWENJIANMING = '" + fileName + "'";
            }
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                WriteLog.Write("查询警报信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除警报（文件表）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Deletejbwj(string fileName, string type)
        {
            string sql = "";
            if (type == "XX")
            {
                sql = "DELETE FROM CG_XIAOXI_FILE WHERE XXWENJIANMING = '" + fileName + "'";
            }
            else if (type == "JB")
            {
                sql = "DELETE FROM CG_JINGBAO_FILE WHERE JBWENJIANMING = '" + fileName + "'";

            }
            else if (type == "JC")
            {
                sql = "DELETE FROM CG_JIECHUJINGBAO_FILE WHERE JCWENJIANMING = '" + fileName + "'";
            }
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除警报信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 查询警报（文件表）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Checkjbwj(string fileName, string type)
        {
            string sql = "";
            if (type == "XX")
            {
                sql = "SELECT *  FROM CG_XIAOXI_FILE WHERE XXWENJIANMING = '" + fileName + "'";
            }
            else if (type == "JB")
            {
                sql = "SELECT *  FROM CG_JINGBAO_FILE WHERE JBWENJIANMING = '" + fileName + "'";

            }
            else if (type == "JC")
            {
                sql = "SELECT *  FROM CG_JIECHUJINGBAO_FILE WHERE JCWENJIANMING = '" + fileName + "'";
            }
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                WriteLog.Write("查询警报信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除警报（内容表）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Deletejbnr(string fileName, string type)
        {
            string sql = "";
            if (type == "XX")
            {
                sql = "DELETE FROM CG_HT_XIAOXI_CONTENT WHERE XXWENJIANMING ='" + fileName + "'";
            }
            else if (type == "JB")
            {
                sql = "DELETE FROM CG_HT_JINGBAO_CONTENT WHERE JBWENJIANMING = '" + fileName + "'";


                // sql = "DELETE FROM CG_JINGBAO_FILE WHERE JBWENJIANMING = '" + fileName + "';
                // " + "DELETE FROM CG_JINGBAO_ME WHERE JBWENJIANMING = '" + fileName + "';
                // " + "DELETE FROM CG_HT_JINGBAO_CONTENT WHERE JBWENJIANMING = '" + fileName + "';
                // " + "DELETE FROM CG_HT_COAST_TABLE WHERE WENJIANMING = '" + fileName + "';
                // " + "DELETE FROM CG_HT_JINGBAO_TABLE WHERE JBWENJIANMING = '" + fileName + "'";

            }
            else if (type == "JC")
            {
                sql = "DELETE FROM CG_HT_JIECHUJINGBAO_CONTENT WHERE JCJBWENJIANMING = '" + fileName + "'";
                 //   " + "DELETE FROM CG_JIECHUJINGBAO_ME WHERE JCWENJIANMING = '" + fileName + "';
               // " + "DELETE FROM CG_HT_JIECHUJINGBAO_CONTENT WHERE JCJBWENJIANMING = '" + fileName+ "'";
            }
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除警报信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 查询警报（内容表）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Checkjbnr(string fileName, string type)
        {
            string sql = "";
            if (type == "XX")
            {
                sql = "SELECT *  FROM CG_HT_XIAOXI_CONTENT WHERE XXWENJIANMING ='" + fileName + "'";
            }
            else if (type == "JB")
            {
                sql = "SELECT *  FROM CG_HT_JINGBAO_CONTENT WHERE JBWENJIANMING = '" + fileName + "'";
            }
            else if (type == "JC")
            {
                sql = "SELECT *  FROM CG_HT_JIECHUJINGBAO_CONTENT WHERE JCJBWENJIANMING = '" + fileName + "'";
            }
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                WriteLog.Write("查询信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除警报（海冰警报表1）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Deletejbtb1(string fileName, string type)
        {
            string sql = "DELETE FROM CG_HT_COAST_TABLE WHERE WENJIANMING = '" + fileName + "'";
           
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除警报信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除警报（海冰警报表2）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Deletejbtb2(string fileName, string type)
        {
            string sql = "DELETE FROM CG_HT_SEATABLE WHERE WENJIANMING = '" + fileName + "'";
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除警报信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 删除警报（其他警报表）
        /// </summary>
        /// <param name="id">警报文件名</param>
        /// <returns></returns>
        public int Deletejbtb(string fileName, string type)
        {
            string sql = "DELETE FROM CG_HT_JINGBAO_TABLE WHERE JBWENJIANMING = '" + fileName + "'";
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除警报信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取文件是否存在（警报表1）
        /// </summary>
        public int GetFilejbtb1(string fileName)
        {
            string sql = "SELECT * FROM CG_HT_COAST_TABLE WHERE WENJIANMING = '" + fileName + "'";

          
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取文件是否存在（警报表2）
        /// </summary>
        public int GetFilejbtb2(string fileName)
        {
            string sql = "SELECT *  FROM CG_HT_SEATABLE WHERE WENJIANMING = '" + fileName + "'";


            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 获取文件是否存在（警报表）
        /// </summary>
        public int GetFilejbtb(string fileName)
        {
            string sql = "SELECT *  FROM CG_HT_JINGBAO_TABLE WHERE JBWENJIANMING = '" + fileName + "'";


            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
    }
}
