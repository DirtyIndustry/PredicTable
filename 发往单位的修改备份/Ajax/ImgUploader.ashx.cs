using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.Model.NineteenWord;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// ImgUploader 的摘要说明
    /// </summary>
    public class ImgUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            if (context.Request["method"] != null)
            {
                string method = context.Request["method"].ToString();

                switch (method)
                {
                    case "DeleteImg": DeleteImg(context); break;
                    case "GetImg": GetImg(context); break;
                     
                    default:
                        break;
                }
            }
            else
            {
                if (context.Request["REQUEST_METHOD"] == "OPTIONS")
                {
                    context.Response.End();
                }
                UploadImg(context);
            }
        }
        void UploadImg(HttpContext context)
        {
            string filePathName = string.Empty;
            string id = context.Request["id"].ToString();
            string name = context.Request["name"].ToString();
            string type = context.Request["type"].ToString();
            string lastModifiedDate = context.Request["lastModifiedDate"].ToString();
            string size = context.Request["size"].ToString();
            HttpPostedFile file = context.Request.Files["file"];

            // BaseResult br = new BaseResult();
            // br.state = false;
            var jsonData = new object();
            //检查上传文件
            if (context.Request.Files.Count == 0)
            {
                jsonData = new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" };
                //br.jdata = jsonData;
                //return CreateJsonResult(br);
                context.Response.Write(jsonData);
            }

            string path = "SPOSFiles/Import/";//默认文件保存的路径
            string myselfpPath = context.Request["myselfpPath"];//指定路径
            if (myselfpPath != null && myselfpPath != "")
            {
                path = myselfpPath;
            }

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, path);
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = file.FileName;

            file.SaveAs(Path.Combine(localPath, filePathName));
            
            DataTable dt = new DataTable();
            dt.Columns.Add("jsonrpc", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("fileName", typeof(string));
            dt.Columns.Add("filePath", typeof(string));
            DataRow dr = dt.NewRow();
            dr["jsonrpc"] = "2.0";
            dr["id"] = id;
            dr["fileName"] = filePathName;
            dr["filePath"] = path + filePathName;
            dt.Rows.Add(dr);
            var dataJson = JsonMore.Serialize(dt);
            context.Response.Write(dataJson);

            #region 文件名判断
            DateTime time = DateTime.Now;
            string ListType = "";
           // string WainArea = "";
            if (filePathName.Contains("NCS"))
            {
                //WainArea = "北海区";
               
                if (filePathName.Contains("1yr"))
                {
                    ListType = "年";
                }
                else
                {
                    ListType = "月旬周";

                }
            }
            if (filePathName.Contains("SD"))
            {
               // WainArea = "山东近海";

                if (filePathName.Contains("1yr"))
                {
                    ListType = "年";
                }
                else
                {
                    ListType = "月旬周";
                }
            }
            if (filePathName.Contains("东营"))
            {
               // WainArea = "东营近海";
              
                if (filePathName.Contains("年") && !filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "年";
                }
                else
                {
                    ListType = "月旬周";
                }
            }
            if (filePathName.Contains("冀东"))
            {
               // WainArea = "冀东油田";

                if (filePathName.Contains("年") && !filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "年";
                }
                else
                {
                    ListType = "月旬周";
                }
            }
            if (filePathName.Contains("胜利"))
            {
               // WainArea = "东营胜利油田";

                if (filePathName.Contains("年") && !filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "年";
                }
                else
                {
                    ListType = "月旬周";
                }
            }
            #endregion

            string returnStr = "";
            //word文档生成图片转二进制
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/SPOSFiles/Import");

            byte[] byfileimg;
            if (ListType == "年")
            {
                sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
                NineteenYearModel yearModel = new NineteenYearModel();
                if (System.IO.File.Exists(imagepath + "/" + filePathName))
                {
                    FileStream fs = new FileStream(imagepath + "/" + filePathName, FileMode.Open);
                    byfileimg = new byte[fs.Length];
                    fs.Read(byfileimg, 0, byfileimg.Length);
                    fs.Close();
                    yearModel.BMP = byfileimg;
                    //判断数据库是不是有当前数据
                    yearModel.ID = filePathName.Split('.')[0]+".doc";
                int result = sql_nineteenTable.GetYearKey(yearModel);
                int tableresult = 0;
                if (result > 0)
                {
                        //修改年图片内容
                     tableresult = sql_nineteenTable.UpdateImgYearTable(yearModel);
                }
                else
                {
                    //添加年图片内容
                    tableresult = sql_nineteenTable.SaveImgYearTable(yearModel);
                }

                if (tableresult <= 0)
                {
                    returnStr += filePathName + " 年内容图片提交失败。 ";
                }
                else
                {
                   // returnStr += filePathName + " 年内容图片提交成功。 ";
                }
              }
            }
            else
            {
                sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
                NineteenNomalModel nomalModel = new NineteenNomalModel();
                if (System.IO.File.Exists(imagepath + "\\" + filePathName))
                {
                    FileStream fs = new FileStream(imagepath + "\\" + filePathName, FileMode.Open);
                    byfileimg = new byte[fs.Length];
                    fs.Read(byfileimg, 0, byfileimg.Length);
                    fs.Close();
                    nomalModel.BMP = byfileimg;
                    //判断数据库是不是有当前数据
                   
                    nomalModel.ID = filePathName.Split('.')[0]+ ".doc";
                    int results = sql_nineteenTable.GetNomalKey1(nomalModel);
                    int tableresults = 0;
                    if (results > 0)
                    {
                        //修改图片内容
                        tableresults = sql_nineteenTable.UpdateImgNomalTable(nomalModel);
                    }
                    else
                    { //保存图片内容
                        tableresults = sql_nineteenTable.SaveImgNomalTable(nomalModel);
                    } 
                    if (tableresults <= 0)
                    {
                        returnStr += filePathName + "图片数据提交失败。";
                    }
                    else
                    {
                      //  returnStr += filePathName + "图片数据提交成功。";
                    }

                }
            }
            context.Response.Write(returnStr);
        }
        void GetImg(HttpContext context)
        {
            string pathfileName = context.Request["name"].ToString();
            sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
            NineteenNomalModel nomalModel = new NineteenNomalModel();
            nomalModel.ID = pathfileName;

            int results = sql_nineteenTable.GetNomalKey1(nomalModel);
            context.Response.Write(results);
        }
        void DeleteImg(HttpContext context)
        {
            string pathfileName = context.Request["fileName"].ToString();
            try
            {
                string path = "SPOSFiles\\Import\\";//默认文件保存的路径
                string fullName = Path.Combine(HttpRuntime.AppDomainAppPath + path, pathfileName);

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