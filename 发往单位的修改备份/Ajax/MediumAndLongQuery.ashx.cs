using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using ICSharpCode.SharpZipLib;

using ICSharpCode.SharpZipLib.Checksums;
using System.Diagnostics;


namespace PredicTable.Ajax
{
    /// <summary>
    /// MediumAndLongQuery 的摘要说明
    /// </summary>
    public class MediumAndLongQuery : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/plain";
            try
            {
                string fileDownPath = "";
                string method = context.Request["method"].ToString();
                if (method == "getdata")
                {
                    int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                    int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
                    int total = 2;//实际一页中有的行数
                    StringBuilder sb = new StringBuilder();
                    //rows 是数据集合
                    TBLYBDDOCUMENT rizhi = new TBLYBDDOCUMENT();
                    Sql_HandelWord bumen = new Sql_HandelWord();
                    rizhi.YBDSIZE = "";
                    DataTable bumentable = (DataTable)bumen.GetTableQuerypage(page, rows, rizhi);
                    total = bumen.GeTableQueryCount(rizhi);
                    sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                    for (int i = 0; i < bumentable.Rows.Count; i++)
                    {
                        //\"status\":\""+"P" + "\",

                        sb.Append("{\"id\":\""
                           + bumentable.Rows[i]["ID"]  + "\",\"wjm\":\""
                           + bumentable.Rows[i]["WORDNAME"] + "\",\"scsj\":\""
                           + bumentable.Rows[i]["CREATETIME"] + "\"},");
                    }
                    if (bumentable.Rows.Count <= 0)
                    {
                        context.Response.Write(sb.ToString() + "]}");
                    }
                    else
                    {
                        context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
                    }
                }
                else if (method == "getbywhere")
                {
                    int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                    int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
                    string firstdata = context.Request.Form["firstdata"].ToString();//开始时间
                    string enddata = context.Request.Form["enddata"].ToString();//结束时间
                                                                                // string userid = context.Request.Form["userid"].ToString();//用户id
                    int total = 2;//实际一页中有的行数
                    StringBuilder sb = new StringBuilder();
                    //rows 是数据集合
                    TBLYBDDOCUMENT rizhi = new TBLYBDDOCUMENT();
                    Sql_HandelWord bumen = new Sql_HandelWord();
                    if (firstdata == "" || enddata == "")
                    {
                        rizhi.YBDSIZE = "";
                    }
                    else
                    {
                        rizhi.YBDSIZE = firstdata + "," + enddata;
                    }
                    DataTable bumentable = (DataTable)bumen.GetTableQuerypage(page, rows, rizhi);
                    total = bumen.GeTableQueryCount(rizhi);
                    sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                    for (int i = 0; i < bumentable.Rows.Count; i++)
                    {
                        sb.Append("{\"id\":\""
                        + bumentable.Rows[i]["ID"] + "\",\"wjm\":\""
                           + bumentable.Rows[i]["WORDNAME"] + "\",\"scsj\":\""
                           + bumentable.Rows[i]["CREATETIME"] + "\"},");
                    }
                    if (bumentable.Rows.Count <= 0)
                    {
                        context.Response.Write(sb.ToString() + "]}");

                    }
                    else
                    {
                        context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
                    }

                }
                else if (method == "getbyid")
                {
                    string idss = context.Request["id"].ToString();
                    var dt = (DataTable)new Sql_HandelWord().get_TableQuerybydata(idss);
                    List<byte[]> fileby = new List<byte[]>();
                    List<string> filename = new List<string>();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            fileby.Add((byte[])dt.Rows[i]["YBDCONTENT"]);
                            filename.Add(dt.Rows[i]["YBDNAME"].ToString());
                        }
                    }
                    for (int j = 0; j < fileby.Count; j++)
                    {
                        //fileDownPath = System.Web.HttpContext.Current.Server.MapPath("/download/" + ".doc");
                        if (File.Exists(fileDownPath))
                        {
                            //如果存在则删除
                            File.Delete(fileDownPath);
                        }
                        string result = ByteConvertWord(fileby[j], filename[j]);
                        if (!result.Equals(""))
                        {
                            context.Response.Write("success");
                        }
                        else
                        {
                            context.Response.Write("failed");
                        }
                    }
                }
                else if (method == "showbyid")
                {
                    string idss = context.Request["id"].ToString();
                    var dt = (DataTable)new Sql_HandelWord().get_TableQuerybydata(idss);
                    byte[] showbyte;
                    List<string> filename = new List<string>();
                    if (dt.Rows.Count > 0)
                    {

                        showbyte = (byte[])dt.Rows[0]["YBDCONTENT"];
                        string zippath = System.Web.HttpContext.Current.Server.MapPath("/download/show.doc");
                        if (File.Exists(zippath))
                        {
                            //如果存在则删除
                            File.Delete(zippath);
                        }

                        if (getwordbybyte(showbyte))
                        {
                            wordToHtml(zippath);
                        }
                        else
                        {
                            context.Response.Write("Error");
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                //  Sql_Caozuorizhi.WriteRizhi(context.Session["userid"].ToString(), "Error", "获取表单列表出错！");
                WriteLog.Write("获取表单列表出错！  " + ex.ToString());
                // HttpContext.Current.Response.Write("<script>top.location.href='../admin/main.aspx';</script>");
            }
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileby">二进制流数组</param>
        /// <param name="filename">文件名数组</param>
        /// <param name="ZipPath">解压缩文件的路径</param>
        /// <returns></returns>
        public bool ZipCom(byte[][] fileby, string[] filename, string ZipPath)
        {

            ZipOutputStream s = new ZipOutputStream(File.Open(ZipPath, FileMode.CreateNew));
            ZipEntry entry = null;
            Crc32 crc = new Crc32();
            try
            {
                for (int i = 0; i < filename.Length; i++)
                {
                    entry = new ZipEntry(Path.GetFileName(filename[i]));
                    //fs.Read(buffer, 0, buffer.Length);
                    entry.DateTime = DateTime.Now;
                    entry.Size = fileby[i].Length;
                    crc.Reset();
                    crc.Update(fileby[i]);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(fileby[i], 0, fileby[i].Length);
                    s.Flush();
                }

                return true;
            }
            catch { return false; }
            finally
            {
                s.Close();
                if (entry != null)
                {
                    entry = null;
                }
            }

        }

        /// <summary>
        ///二进制数据转换为word文件
        /// </summary>
        /// <param name="data">二进制数据</param>
        /// <param name="fileName">word文件名</param>
        /// <returns>word保存的相对路径</returns>
        public string ByteConvertWord(byte[] data, string fileName)
        {

            string savePath = System.Web.HttpContext.Current.Server.MapPath("/download/");
            if (!System.IO.Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            savePath += fileName;
            string filePath = savePath;
            FileStream fs;
            if (System.IO.File.Exists(filePath))
            {
                fs = new FileStream(filePath, FileMode.Truncate);
            }
            else
            {
                fs = new FileStream(filePath, FileMode.CreateNew);
            }
            BinaryWriter br = new BinaryWriter(fs);
            br.Write(data, 0, data.Length);
            br.Close();
            fs.Close();
            return savePath;
        }

        //在本地生成文件
        public bool getwordbybyte(byte[] data)
        {
            try
            {
                File.WriteAllBytes(System.Web.HttpContext.Current.Server.MapPath("/download/show.doc"), data);
                return true;
            }
            catch (Exception ex)
            {

                WriteLog.Write("流生成本地文件出错！  " + ex.ToString());
                return false;
            }


        }

        ///<summary>  
        ///上传文件并转存为html  
        ///</summary>  
        ///<param name="wordFilePath">word文档在客户机的位置</param>  
        ///<returns>上传的html文件的地址</returns>  
        public string wordToHtml(object fileName)
        {
            //Microsoft.Office.Interop.Word.ApplicationClass word = new Microsoft.Office.Interop.Word.ApplicationClass();
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

            Type wordType = word.GetType();
            Microsoft.Office.Interop.Word.Documents docs = word.Documents;

            // 打开文件  
            Type docsType = docs.GetType();

            //应当先把文件上传至服务器然后再解析文件为html  
            //string filePath = uploadWord(wordFilePath);

            ////判断是否上传文件成功  
            //if (filePath == "0")
            //    return "0";
            ////判断是否为word文件  
            //if (filePath == "1")
            //    return "1";



            Microsoft.Office.Interop.Word.Document doc = (Microsoft.Office.Interop.Word.Document)docsType.InvokeMember("Open",
            System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { fileName, true, true });

            // 转换格式，另存为html  
            Type docType = doc.GetType();

            string filename = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString();

            // 判断指定目录下是否存在文件夹，如果不存在，则创建  
            //if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~\\html")))
            //{
            //    // 创建up文件夹  
            //    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~\\html"));
            //}

            //被转换的html文档保存的位置  
            string ConfigPath = HttpContext.Current.Server.MapPath("/download/show.html");
            object saveFileName = ConfigPath;

            /*下面是Microsoft Word 9 Object Library的写法，如果是10，可能写成： 
         * docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, 
         * null, doc, new object[]{saveFileName, Word.WdSaveFormat.wdFormatFilteredHTML}); 
         * 其它格式： 
         * wdFormatHTML 
         * wdFormatDocument 
         * wdFormatDOSText 
         * wdFormatDOSTextLineBreaks 
         * wdFormatEncodedText 
         * wdFormatRTF 
         * wdFormatTemplate 
         * wdFormatText 
         * wdFormatTextLineBreaks 
         * wdFormatUnicodeText 
         */
            docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod,
            null, doc, new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatFilteredHTML });

            //关闭文档  
            docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod,
            null, doc, new object[] { null, null, null });

            // 退出 Word  
            wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);

            //修改编码
            //// convert DBCS-932 encoded file to unicode-file 
            //using (StreamReader sr = new StreamReader(ConfigPath, Encoding.Default, false))
            //{
            //    using (StreamWriter sw = new StreamWriter(ConfigPath+"l", false, Encoding.UTF8))
            //    {
            //        sw.Write(sr.ReadToEnd());
            //    }
            //}

            string strHtm;
            //读取
            using (StreamReader sr = new StreamReader(ConfigPath, Encoding.Default, false))
            {
                strHtm = sr.ReadToEnd();
                sr.Close();
            }
            //保存
            //  FileStream fs = new FileStream(ConfigPath, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(ConfigPath, false, Encoding.UTF8))
            {
                sw.Write(strHtm);
            }
            //其中采用了Encoding.GetEncoding(936)编码即gb2312简体中文编码方式


            //转到新生成的页面  
            return ("/download/show.html");

        }


        public void ConvertPdfToSwf(string inFilename, string swfFilename)
        {
            try
            {
                string flashPrinter = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "C:/Program Files (x86)/Macromedia/FlashPaper 2/FlashPrinter.exe");
                ProcessStartInfo startInfo = new ProcessStartInfo(flashPrinter);
                startInfo.Arguments = string.Concat(inFilename, " -o ", swfFilename);
                Process process = new Process();
                process.StartInfo = startInfo;
                bool isStart = process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                WriteLog.Write("转换swf出错！" + ex.ToString());
                // Response.Write(ex.Message);
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