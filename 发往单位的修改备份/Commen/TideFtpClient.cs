using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace PredicTable.Commen
{
    public class TideFtpClient
    {
        /*
    * 
    * 文件夹是否存在、上传、删除、重命名，获取文件夹下文件和文件夹
    * 文件是否存在、上传、删除、重命名、下载
    * 
    * */
        #region FTP上传和下载
        /// <summary>
        /// 获取ftp上某个路径下所有文件名
        /// </summary>
        /// <param name="requestUriString">ftp路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string[] GetFtpFileNames(string requestUriString, string userName, string password)
        {
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                List<string> list = new List<string>();
                request = (FtpWebRequest)WebRequest.Create(requestUriString);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(userName, password);
                response = (FtpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string s = reader.ReadLine();
                while (s != null)
                {
                    list.Add(s);
                    s = reader.ReadLine();
                }
                switch (GuessFileListStyle(list.ToArray()))
                {
                    case 1:
                        return GetUnixFiles(list.ToArray());
                    case 2:
                        return GetWindowsFiles(list.ToArray());
                    case 3:
                        return GetUnknownFiles(requestUriString, userName, password);
                    default:
                        throw new Exception("系统判断出错！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
                if (reader != null)
                    reader.Close();
            }
        }
        /// <summary>
        /// 获取ftp上某个路径下所有文件夹名
        /// </summary>
        /// <param name="requestUriString">ftp路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string[] GetFtpDirectoryNames(string requestUriString, string userName, string password)
        {
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                List<string> list = new List<string>();
                request = (FtpWebRequest)WebRequest.Create(requestUriString);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(userName, password);
                response = (FtpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string s = reader.ReadLine();
                while (s != null)
                {
                    list.Add(s);
                    s = reader.ReadLine();
                }
                switch (GuessFileListStyle(list.ToArray()))
                {
                    case 1:
                        return GetUnixDirectory(list.ToArray());
                    case 2:
                        return GetWindowsDirectory(list.ToArray());
                    default:
                        throw new Exception("系统判断出错！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
                if (reader != null)
                    reader.Close();
            }
        }
        /// <summary>
        /// 判断服务器类型，1位Unix，2为windows
        /// </summary>
        /// <param name="recordList"></param>
        /// <returns></returns>
        private static int GuessFileListStyle(string[] recordList)
        {
            foreach (string s in recordList)
            {
                if (s.Length > 10
                 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                {
                    return 1;// FileListStyle.UnixStyle;
                }
                else if (s.Length > 8
                 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                {
                    return 2;// FileListStyle.WindowsStyle;
                }
            }
            return 3;// FileListStyle.Unknown;
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="recordList"></param>
        /// <returns></returns>
        private static string[] GetWindowsFiles(string[] recordList)
        {
            List<string> list = new List<string>();
            foreach (string str in recordList)
            {
                if (!(str.Substring(0, 39).ToUpper().Contains("<DIR>")))
                {
                    list.Add(str.Substring(39));
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 获取文件夹
        /// </summary>
        /// <param name="recordList"></param>
        /// <returns></returns>
        private static string[] GetWindowsDirectory(string[] recordList)
        {
            List<string> list = new List<string>();
            foreach (string str in recordList)
            {
                if (str.Substring(0, 39).ToUpper().Contains("<DIR>"))
                {
                    list.Add(str.Substring(39));
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="recordList"></param>
        /// <returns></returns>
        private static string[] GetUnixFiles(string[] recordList)
        {
            List<string> list = new List<string>();
            foreach (string str in recordList)
            {
                if (str.Length > 55 && (int.Parse(str.Substring(29, 13).Trim()) != 0))
                {
                    list.Add(str.Substring(55));
                }
            }
            list.Remove(".");
            list.Remove("..");
            return list.ToArray();
        }
        /// <summary>
        /// 获取文件夹
        /// </summary>
        /// <param name="recordList"></param>
        /// <returns></returns>
        private static string[] GetUnixDirectory(string[] recordList)
        {
            List<string> list = new List<string>();
            foreach (string str in recordList)
            {
                if (int.Parse(str.Substring(29, 13).Trim()) == 0)
                {
                    list.Add(str.Substring(55));
                }
            }
            list.Remove(".");
            list.Remove("..");
            return list.ToArray();
        }
        /// <summary>
        /// 按照WebRequestMethods.Ftp.ListDirectory进行下载
        /// </summary>
        /// <param name="requestUriString"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string[] GetUnknownFiles(string requestUriString, string userName, string password)
        {
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                List<string> list = new List<string>();
                request = (FtpWebRequest)WebRequest.Create(requestUriString);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(userName, password);
                response = (FtpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string s = reader.ReadLine();
                while (s != null)
                {
                    list.Add(s);
                    s = reader.ReadLine();
                }
                response.Close();
                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
                if (reader == null)
                    reader.Close();
            }
        }

        /// <summary>
        /// 判断路径是否存在并进行创建然后上传文件
        /// </summary>
        /// <param name="requestUriString">上传路径</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        /// <param name="file">文件</param>
        public static void FTPUploadFile(string requestUriString, string ftpUserID, string ftpPassword, FileInfo file)
        {
            FileStream uploadFileStream = null;
            FtpWebRequest ftpRequest = null;
            Stream ftpStream = null;
            try
            {
                CreateDirector(requestUriString, ftpUserID, ftpPassword);
                if (!requestUriString.EndsWith(file.Name))
                {
                    if (!requestUriString.EndsWith("/"))
                    {
                        requestUriString += "/";
                    }
                }
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(requestUriString + file.Name));
                ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpRequest.KeepAlive = false;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.ContentLength = file.Length;
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                uploadFileStream = file.OpenRead();
                ftpStream = ftpRequest.GetRequestStream();
                contentLen = uploadFileStream.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    ftpStream.Write(buff, 0, contentLen);
                    contentLen = uploadFileStream.Read(buff, 0, buffLength);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                if (uploadFileStream != null)
                {
                    uploadFileStream.Close();
                }
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
            }
        }
        /// <summary>
        /// 不进行判断路径是否存在直接上传文件
        /// </summary>
        /// <param name="requestUriString">上传路径</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        /// <param name="file">文件</param>
        public static void DirectlyFTPUploadFile(string requestUriString, string ftpUserID, string ftpPassword, FileInfo file)
        {
            FileStream uploadFileStream = null;
            FtpWebRequest ftpRequest = null;
            Stream ftpStream = null;
            try
            {
                if (!requestUriString.EndsWith(file.Name))
                {
                    if (!requestUriString.EndsWith("/"))
                    {
                        requestUriString += "/";
                    }
                }
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(requestUriString + file.Name));
                ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpRequest.KeepAlive = false;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.ContentLength = file.Length;
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                uploadFileStream = file.OpenRead();
                ftpStream = ftpRequest.GetRequestStream();
                contentLen = uploadFileStream.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    ftpStream.Write(buff, 0, contentLen);
                    contentLen = uploadFileStream.Read(buff, 0, buffLength);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                if (uploadFileStream != null)
                {
                    uploadFileStream.Close();
                }
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
            }
        }

        /// <summary>
        /// 查看文件是否存在
        /// </summary>
        /// <param name="requestUriString">ip如：192.168.2.111</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        /// <returns></returns>
        private static bool FtpFileIsExis(string requestUriString, string ftpUserID, string ftpPassword)
        {
            //查看文件大小，能正常返回则文件存在，报异常者文件不存在

            FtpWebRequest request = null;
            FtpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                List<string> list = new List<string>();
                request = (FtpWebRequest)WebRequest.Create(requestUriString);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                response = (FtpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (response == null)
                    response.Close();
                if (reader == null)
                    reader.Close();
            }
        }

        /// <summary>
        /// ftp文件下载
        /// </summary>
        /// <param name="requestUriString">FTP服务器IP</param>
        /// <param name="username">FTP登录帐号</param>
        /// <param name="password">FTP登录密码</param>
        /// <param name="folder">服务器保存文件路径</param>
        public static void FTPDownloadFile(string requestUriString, string username, string password, string saveFilePath)
        {
            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;
            FileStream saveStream = null;
            Stream ftpStream = null;
            try
            {
                string fileName = requestUriString.Substring(requestUriString.LastIndexOf("/"));
                if (!saveFilePath.EndsWith("\\"))
                {
                    saveFilePath += "\\";
                }
                saveStream = new FileStream(saveFilePath + fileName, FileMode.Create);
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(requestUriString));
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.Credentials = new NetworkCredential(username, password);
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpStream = ftpResponse.GetResponseStream();
                long cl = ftpResponse.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    saveStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
                if (saveStream != null)
                {
                    saveStream.Close();
                }
                if (ftpResponse != null)
                {
                    ftpResponse.Close();
                }
            }
        }

        /// <summary>
        /// ftp文件夹创建，逐级遍历，逐级创建
        /// </summary>
        /// <param name="requestUriString"></param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        public static void CreateDirector(string requestUriString, string ftpUserID, string ftpPassword)
        {
            if (!DirectorIsHave(requestUriString, ftpUserID, ftpPassword))
            {
                string[] str = Regex.Split(requestUriString, "//", RegexOptions.IgnoreCase);
                if (str.Length == 2)
                {
                    string[] folder = str[1].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    string ftpPath = "ftp://" + folder[0];
                    if (!DirectorIsHave(ftpPath, ftpUserID, ftpPassword))
                    {
                        throw new Exception("ftp服务器连接失败！");
                    }

                    for (int j = 1; j < folder.Length; j++)
                    {
                        ftpPath += "/" + folder[j];
                        if (!DirectorIsHave(ftpPath, ftpUserID, ftpPassword))
                        {
                            DirectlyCreateDirector(ftpPath, ftpUserID, ftpPassword);
                        }
                    }

                }
                else
                {
                    throw new Exception("requestUriString参数错误！");
                }

            }



        }
        /// <summary>
        /// 检查ftp路径是否存在
        /// </summary>
        /// <param name="requestUriString"></param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        /// <returns></returns>
        public static bool DirectorIsHave(string requestUriString, string ftpUserID, string ftpPassword)
        {
            if (!requestUriString.EndsWith("/"))
                requestUriString += "/";
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            try
            {
                request = (FtpWebRequest)WebRequest.Create(requestUriString);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
            finally
            {
                if (response != null)
                    response.Close();
            }

        }
        /// <summary>
        /// 不进行判断直接在ftp上进行文件夹创建
        /// </summary>
        /// <param name="requestUriString"></param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        public static void DirectlyCreateDirector(string requestUriString, string ftpUserID, string ftpPassword)
        {
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            try
            {
                request = (FtpWebRequest)WebRequest.Create(requestUriString);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="requestUriString"></param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        public static void RemoveDirector(string requestUriString, string ftpUserID, string ftpPassword)
        {
            if (DirectorIsHave(requestUriString, ftpUserID, ftpPassword))
            {
                FtpWebRequest request = null;
                FtpWebResponse response = null;
                try
                {
                    request = (FtpWebRequest)WebRequest.Create(requestUriString);
                    request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                    request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                    response = (FtpWebResponse)request.GetResponse();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }
        }
        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="requestUriString"></param>
        /// <param name="newRequestUriString">新文件夹名或文件名，不需要路径</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        public static void RenameDirector(string requestUriString, string newRequestUriString, string ftpUserID, string ftpPassword)
        {
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            try
            {
                if (requestUriString.EndsWith("/"))
                {
                    requestUriString.Remove(requestUriString.Length - 2);
                }
                request = (FtpWebRequest)WebRequest.Create(requestUriString);
                request.Method = WebRequestMethods.Ftp.Rename;
                request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                request.RenameTo = newRequestUriString;
                response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }


        }
        #endregion
    }
}