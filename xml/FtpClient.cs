using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

    public class FtpClient
    {
        /// <summary>
        /// 不进行判断路径是否存在直接上传文件
        /// </summary>
        /// <param name="requestUriString">上传路径</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        /// <param name="file">文件</param>
        public static void DirectlyFTPUploadFile(FileInfo file,string requestUriString = "ftp://202.108.199.26/GK", string ftpUserID= "bh", string ftpPassword= "bh!123")
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
        


    }
