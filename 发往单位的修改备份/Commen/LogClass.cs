//****************************************************************************
//文件名(FileName):LogClass.cs
//作者(Author):石磊
//日期(Create Date):20180730
//修改记录(Revision History):
//      R1:
//          修改做着:
//          修改日期:
//          修改理由:
//****************************************************************************
using System;
using System.IO;

namespace PredicTable.Commen
{
    public class LogClass
    {
        private static StreamWriter streamWriter; //写文件  
        //static string directPath = AppDomain.CurrentDomain.BaseDirectory;//根路径
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void WriteErrorLog(string errorMessage)
        {
            try
            {
                string directPath = "";
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log\\ErrorLog");
                if (!Directory.Exists(logPath))   //判断文件夹是否存在，如果不存在则创建
                {
                    Directory.CreateDirectory(logPath);
                }
                directPath = Path.Combine(logPath, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                if (streamWriter == null)
                {
                    streamWriter = !File.Exists(directPath) ? File.CreateText(directPath) : File.AppendText(directPath);    //判断文件是否存在如果不存在则创建，如果存在则添加。
                }
                if (errorMessage != null)
                {
                    streamWriter.WriteLine(DateTime.Now.ToString("HH:mm:ss")+"      异常信息：" + errorMessage);
                }
            }
            catch
            {

            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                    streamWriter = null;
                }
            }
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="oprationMessage"></param>
        public static void WriteOprationLog(string oprationMessage)
        {
            try
            {
                string directPath = "";
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log\\OprationLog");
                if (!Directory.Exists(logPath))   //判断文件夹是否存在，如果不存在则创建
                {
                    Directory.CreateDirectory(logPath);
                }
                directPath = Path.Combine(logPath, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                if (streamWriter == null)
                {
                    streamWriter = !File.Exists(directPath) ? File.CreateText(directPath) : File.AppendText(directPath);    //判断文件是否存在如果不存在则创建，如果存在则添加。
                }
                if (oprationMessage != null)
                {
                    streamWriter.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "      操作信息：" + oprationMessage);
                }
            }
            catch
            {

            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                    streamWriter = null;
                }
            }
        }
    }
}