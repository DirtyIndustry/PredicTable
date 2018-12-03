using PredicTable.Commen;
using PredicTable.Dal;
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
    /// WarningImgUploader 的摘要说明
    /// </summary>
    public class WarningImgUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
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
        private void UploadImg(HttpContext context)
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
            string WainArea = "";
            string ListType = "";

            if (filePathName.Contains("NCS"))
            {
                WainArea = "国家海洋局北海预报中心";
                if (filePathName.Contains("JB"))
                {
                    ListType = "警报";
                }
                if (filePathName.Contains("JC"))
                {
                    ListType = "解除";
                }
                if (filePathName.Contains("XX"))
                {
                    ListType = "消息";
                }
            }
            if (filePathName.Contains("SD"))
            {
                WainArea = "山东省海洋预报台";

                if (filePathName.Contains("JB"))
                {
                    ListType = "警报";
                }
                if (filePathName.Contains("JC"))
                {
                    ListType = "解除";
                }
                if (filePathName.Contains("XX"))
                {
                    ListType = "消息";
                }
            }
            #endregion
            //word文档生成图片转二进制
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/SPOSFiles/Import");
            string result = "";
            byte[] byfileimg=null;
            try
            {
                if (System.IO.File.Exists(imagepath + "/" + filePathName))
                {
                    FileStream fs = new FileStream(imagepath + "/" + filePathName, FileMode.Open);
                    byfileimg = new byte[fs.Length];
                    fs.Read(byfileimg, 0, byfileimg.Length);
                    fs.Close();
                }
                if(byfileimg == null)
                {
                    return;
                }
                sql_WarningImgUploader imgUpLoader = new sql_WarningImgUploader();
                DataTable dtExist = new DataTable();
                string docName = filePathName.Split('.')[0] + ".doc";
                if (ListType == "警报")
                {
                    //判断是否存在
                    dtExist = imgUpLoader.GetImgInfo("CG_HT_JINGBAO_CONTENT", "JBWENJIANMING", docName);
                    if (dtExist != null && dtExist.Rows.Count > 0)
                    {
                        imgUpLoader.UpdateImg("CG_HT_JINGBAO_CONTENT", "JBWENJIANMING", "PICTURE", docName, byfileimg);
                        //result = "修改警报图片成功";
                    }
                    else
                    {
                        imgUpLoader.InsertImg("CG_HT_JINGBAO_CONTENT", "JBWENJIANMING", "PICTURE", docName, byfileimg);
                      //  result = "添加警报图片成功";
                    }
                }
                else if (ListType == "解除")
                {
                    //判断是否存在
                    dtExist = imgUpLoader.GetImgInfo("CG_HT_JIECHUJINGBAO_CONTENT", "JCJBWENJIANMING", docName);
                    if (dtExist != null && dtExist.Rows.Count > 0)
                    {
                        imgUpLoader.UpdateImg("CG_HT_JIECHUJINGBAO_CONTENT", "JCJBWENJIANMING", "PICTURE", docName, byfileimg);
                       // result = "修改解除警报图片成功";
                    }
                    else
                    {
                        imgUpLoader.InsertImg("CG_HT_JIECHUJINGBAO_CONTENT", "JCJBWENJIANMING", "PICTURE", docName, byfileimg);
                        result = "添加解除警报图片成功";
                    }
                }
            }
            catch (Exception error)
            {
                result = "图片上传失败";
            }
            context.Response.Write(result);
        }
        void GetImg(HttpContext context)
        {
            string pathfileName = context.Request["name"].ToString();
            //sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
            //NineteenNomalModel nomalModel = new NineteenNomalModel();
            //nomalModel.ID = pathfileName;

            //int results = sql_nineteenTable.GetNomalKey1(nomalModel);
            //context.Response.Write(results);
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