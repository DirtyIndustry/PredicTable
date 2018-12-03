using System;
using System.IO;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Spire.Doc;
using Microsoft.Office.Interop.Word;
using PredicTable.Model.NewMediumAndLong;


//using Microsoft.Office.Interop.Word;
//using Application = Microsoft.Office.Interop.Word.Application;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;


namespace PredicTable.Ajax
{
    /// <summary>
    /// NewMediumAndLongHBUploader 的摘要说明
    /// </summary>
    public class NewMediumAndLongHBUploader : IHttpHandler
    {
        public static string filePath;
        public static string tableMessage;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            if (context.Request["method"] != null)
            {
                string method = context.Request["method"].ToString();

                switch (method)
                {
                    case "DeleteFile": DeleteFile(context); break;
                    case "UploadFile_Year": UploadFile(context);break;
                    case "UploadFile_MonthOrXun": UploadFile_MonthOrXun(context);break;
                    default:
                        break;
                }
            }
            else
            {
                if (context.Request["REQUEST_METHOD"] == "OPTIONS")//????
                {
                    context.Response.End();
                }

            }
        }
        public void UploadFile(HttpContext context)
        {
            string filePathName = string.Empty;
            string id = context.Request["id"].ToString();
            string name = context.Request["name"].ToString();
            string type = context.Request["type"].ToString();
            string lastModifiedDate = context.Request["lastModifiedDate"].ToString();
            string size = context.Request["size"].ToString();

            HttpPostedFile file = context.Request.Files["file"];

            #region 文件保存到路径下
            var jsonData = new object();
            //检查上传文件
            if (context.Request.Files.Count == 0)
            {
                jsonData = new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" };

                context.Response.Write(jsonData);
            }

            string path = AppDomain.CurrentDomain.BaseDirectory + "ZCQHB\\" + System.DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, path);
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = file.FileName;
            //文件保存到指定路径下
            file.SaveAs(Path.Combine(localPath, filePathName));

            filePath = path + "\\" + filePathName;//用来word表格的路径
            #endregion

            tableMessage = "";
            #region 表格的复制 用于添加入库
            if (filePath != null)
            {
                var wordApp = new Application();
                object Nothing = Missing.Value;
                object missing = Missing.Value;
                object IsReadOnly = false;
                object filenamebiao = filePath;

                var wordDoc = wordApp.Documents.Open(ref filenamebiao, ref Nothing, ref IsReadOnly, ref IsReadOnly, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);


                Microsoft.Office.Interop.Word.Table table = wordDoc.Tables[1];

                for (int rowPos = 1; rowPos <= table.Rows.Count; rowPos++)
                {
                    for (int columPos = 1; columPos <= table.Columns.Count; columPos++)
                    {
                        tableMessage += table.Cell(rowPos, columPos).Range.Text;
                        tableMessage = tableMessage.Remove(tableMessage.Length - 2, 2);//remove \r\a
                        tableMessage += "\t";
                    }

                    tableMessage += "\n";
                }

                context.Response.Write(tableMessage);
                wordDoc.Close();
                wordApp.Quit();
            }
            
            #endregion

        }

        public void UploadFile_MonthOrXun(HttpContext context)
        {
            string filePathName = string.Empty;
            string id = context.Request["id"].ToString();
            string name = context.Request["name"].ToString();
            string type = context.Request["type"].ToString();
            string lastModifiedDate = context.Request["lastModifiedDate"].ToString();
            string size = context.Request["size"].ToString();
            string yubaoarea = context.Request["YuBaoArea"].ToString();

           
            HttpPostedFile file = context.Request.Files["file"];

            #region 文件保存到路径下
            var jsonData = new object();
            //检查上传文件
            if (context.Request.Files.Count == 0)
            {
                jsonData = new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" };

                context.Response.Write(jsonData);
            }

            string path = AppDomain.CurrentDomain.BaseDirectory + "ZCQHB\\" + System.DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, path);
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = file.FileName;
            //文件保存到指定路径下
            file.SaveAs(Path.Combine(localPath, filePathName));

            filePath = path + "\\" + filePathName;//用来word表格的路径
            #endregion

            string tableMessage_MonthXun = "";
            #region 表格的复制 用于添加入库
            if (filePath != null)
            {
                var wordApp = new Application();
                object Nothing = Missing.Value;
                object missing = Missing.Value;
                object IsReadOnly = false;
                object filenamebiao = filePath;

                var wordDoc = wordApp.Documents.Open(ref filenamebiao, ref Nothing, ref IsReadOnly, ref IsReadOnly, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

                tableMessage_MonthXun = "";
                Microsoft.Office.Interop.Word.Table table = wordDoc.Tables[1];

                for (int rowPos = 1; rowPos <= table.Rows.Count; rowPos++)
                {
                    for (int columPos = 1; columPos <= table.Columns.Count; columPos++)
                    {
                        tableMessage_MonthXun += table.Cell(rowPos, columPos).Range.Text;
                        tableMessage_MonthXun = tableMessage_MonthXun.Remove(tableMessage_MonthXun.Length - 2, 2);//remove \r\a
                        tableMessage_MonthXun += "\t";
                    }

                    tableMessage_MonthXun += "\n";
                }

                context.Response.Write(tableMessage_MonthXun);
                wordDoc.Close();
                wordApp.Quit();
            }

            #endregion

            if (HBParamStaticList.HBParamList == null)
            {
                HBParamStaticList.HBParamList = new List<ZCQHBParamModel>();
            }
            ZCQHBParamModel model = new ZCQHBParamModel();
            model.ForcastArea = yubaoarea;
            model.FilePath = filePath;
            model.FileMessage = tableMessage_MonthXun;
            HBParamStaticList.HBParamList.Add(model);

        }

        /// <summary>
        /// 删除上传的文件
        /// </summary>
        /// <param name="context"></param>
        private void DeleteFile(HttpContext context)
        {
            string pathfileName = context.Request["fileName"].ToString();
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "ZCQHB\\" + System.DateTime.Now.ToString("yyyyMMdd");//默认文件保存的路径
                string fullName = Path.Combine( path, pathfileName);

                if (System.IO.File.Exists(fullName))
                {
                    System.IO.File.Delete(fullName);
                }
                context.Response.Write("删除成功。");
            }
            catch (Exception e)
            {
                context.Response.Write("删除失败。" + e.Message);

            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}