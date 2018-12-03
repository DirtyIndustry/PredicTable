using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class AMTableList : System.Web.UI.Page
    {
        public DateTime dt;
        public string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (Session["type"] != null)
                {
                    // Response.Write("<script language='javascript'>alert('" + Session["type"].ToString() + "');</script>");
                    type = Session["type"].ToString(); 
                }
                else
                {
                   
                }
                //Session["type"] = "fl";
                //Session["type"] = "sw";
                //Session["type"] = "cx";
            }
            catch (Exception)
            {
                throw;
            }
        }


        readonly string DownloadPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Download\";//本地存放路径
        /// <summary>
        /// 下载
        /// </summary>
        private bool DownLoadSoft(string Version, string FullFilePath, string FileName)
        {
            string path = DownloadPath.Remove(DownloadPath.Length - 1);
            bool flag = false;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fs = new FileStream(DownloadPath + FileName, FileMode.Create, FileAccess.Write))
                {
                    //创建请求
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FullFilePath);
                    //接收响应
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //输出流
                    Stream responseStream = response.GetResponseStream();
                    byte[] bufferBytes = new byte[10000];//缓冲字节数组
                    int bytesRead = -1;
                    while ((bytesRead = responseStream.Read(bufferBytes, 0, bufferBytes.Length)) > 0)
                    {
                        fs.Write(bufferBytes, 0, bytesRead);
                    }
                    if (fs.Length > 0)
                    {
                        flag = true;
                    }
                    //关闭写入
                    fs.Flush();
                    fs.Close();
                }

            }
            catch (Exception exp)
            {
                //返回错误消息

            }
            return flag;
        }

    }
}