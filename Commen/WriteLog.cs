using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

 

 
    public class WriteLog
    {
        private static string m_logFilePath = AppDomain.CurrentDomain.BaseDirectory + "Tmp\\HYGC_Log";//modify by xp 2018-8-23
            
        //private static string m_logFilePath = "E:\\Tmp\\HYGC_Log";          // 日志文件存放目录
        private static long m_fileMaxSize = 1024 * 500;                     // 文件最大值（Byte）(默认 500KB)
        private static string m_fullFilePath = String.Empty;                // 日志文件全路径

        private static string m_LogType = System.Configuration.ConfigurationManager.AppSettings["WriteLog"] ?? "Release";



    // 写日志
    public static void Write(string log)
        {
        //string ip = System.Web.HttpContext.Current.Request.ServerVariables.Get("Local_Addr").ToString();
        //if (new Maticsoft.BLL.sysconfig().GetModel(ip).islog == "否")
        //{
        //    return;
        //}
        try
        {
            if (!System.IO.Directory.Exists(m_logFilePath))//add by xp 2018-8-23
            {
                System.IO.Directory.CreateDirectory(m_logFilePath);//不存在就创建目录 
            }

            Sql_Keyvalue kv = new Sql_Keyvalue();
            var kvdt = kv.GetAll();
            if (kvdt.Rows.Count > 0)
            {
                string keys = "";
                string values = "";
                for (int i = 0; i < kvdt.Rows.Count; i++)
                {
                    keys = kvdt.Rows[i]["KEY"].ToString();
                    values = kvdt.Rows[i]["VALUE"].ToString();
                    //   LOGPATH
                    switch (keys)
                    {
                        case "LOGPATH": m_logFilePath = values; break;
                        default: break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("获取日志路径出现异常！" + ex.Message);
        }

        StreamWriter sw = null;
        string strLogFile = String.Empty;

            try
            {
                //string logFilePath = @"D:\\Tmp\\HYGC_Log";
                //if (!String.IsNullOrEmpty(BaseConfig.GetInstance().LogFilePath))
                //{
                //    logFilePath = BaseConfig.GetInstance().LogFilePath;
                //}

                //long fileMaxSize = 1024 * 1024; // (1MB)
                //if (BaseConfig.GetInstance().FileMaxSize > 0)
                //{
                //    fileMaxSize = BaseConfig.GetInstance().FileMaxSize;
                //}

                if (String.IsNullOrEmpty(m_fullFilePath))
                {
                    if (!Directory.Exists(m_logFilePath))                   // 验证目录是否存在
                    {
                        Directory.CreateDirectory(m_logFilePath);
                    }

                    strLogFile = DateTime.Now.ToString("yyyy-MM-dd");       // 组织日志文件路径
                    strLogFile = m_logFilePath + "\\" + strLogFile + ".log";

                    strLogFile = CheckFile(strLogFile, m_fileMaxSize);      // 验证日志文件大小
                }
                else
                {
                    FileInfo fInfo = new FileInfo(m_fullFilePath);              // 验证文件是否存在

                    if (fInfo.Exists)
                    {
                        strLogFile = CheckFile(m_fullFilePath, m_fileMaxSize);  // 验证日志文件大小
                    }
                    else
                    {
                        if (!Directory.Exists(m_logFilePath))                   // 验证目录是否存在
                        {
                            Directory.CreateDirectory(m_logFilePath);
                        }

                        strLogFile = DateTime.Now.ToString("yyyy-MM-dd");       // 组织日志文件路径
                        strLogFile = m_logFilePath + "\\" + strLogFile + ".log";
                        strLogFile = CheckFile(strLogFile, m_fileMaxSize);      // 验证日志文件大小
                    }
                    
                }
                    
                m_fullFilePath = strLogFile;    // 记录当前日志文件路径

                // 写入日志
                sw = new StreamWriter(strLogFile, true);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + log);
            }
            catch (Exception e)
            {
                Console.WriteLine("创建日志文件出现异常！" + e.Message);
            }
            finally
            { 
                sw.Close();                
            }
        }

    public static void WriteDebug(string log)
    {
        if (m_LogType == "Debug" )
        {
            Write(log);
        }
        
    }

        /// <summary>
        /// 验证日志文件大小
        /// </summary>
        /// <param name="strLogFile">日志文件路径</param>
        /// <param name="fileMaxSize">文件最大值</param>
        /// <returns>文件路径</returns>
        public static string CheckFile(string strLogFile, long fileMaxSize)
        {
            try
            {
                FileInfo fInfo = new FileInfo(strLogFile);

                if (!fInfo.Exists)
                {
                    return strLogFile;
                }

                long lFileSize = fInfo.Length;

                if (lFileSize > fileMaxSize)            // 判断日志文件是否超出最大值要求
                {
                    string fileName = fInfo.Name;       // 文件名
                    string fileExt = fInfo.Extension;   // 文件扩展名

                    fileName = fileName.Substring(0, fileName.IndexOf(fileExt));

                    string[] strArr = fileName.Split('_');
                    int num = 0;

                    try
                    {
                        if (strArr.Length > 1)
                        {
                            num = Int32.Parse(strArr[strArr.Length - 1]);
                        }
                    }
                    catch (Exception)
                    {}

                    if (num <= 0)
                    {
                        fileName = fileName + "_1" + fileExt;

                        strLogFile = fInfo.Directory.FullName + "\\" + fileName; ;
                    }
                    else
                    {
                        num++;
                        strArr[strArr.Length - 1] = num.ToString();

                        fileName = "";
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            if (i == strArr.Length - 1)
                            {
                                fileName += strArr[i];
                            }
                            else
                            {
                                fileName += strArr[i] + "_";
                            }
                        }

                        fileName = fileName + fileExt;

                        strLogFile = fInfo.Directory.FullName + "\\" + fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("验证日志文件出现异常！" + ex.Message);
            }

            return strLogFile;
        }

    }
 
