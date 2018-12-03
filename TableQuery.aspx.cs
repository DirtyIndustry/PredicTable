using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Data;

namespace PredicTable
{
    public partial class TableQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string[] ids = { "8323", "8322" };
            //foreach (var id in ids) {

            //}
            //string idss = "8323,8322";
            //var dt=(DataTable)   new sql_TableQuery().get_TableQuerybydata(idss);
            //List<byte[]> fileby=new List<byte[]> ();
            //List <string> filename=new List<string> ();
            //if (dt.Rows.Count>0) {
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        fileby.Add((byte[])dt.Rows[i]["YBDCONTENT"]);
            //        filename.Add(dt.Rows[i]["YBDNAME"].ToString());
            //    }
            //}
            //ZipCom(fileby.ToArray(), filename.ToArray(), Server.MapPath("/download/1.zip"));

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


        //上传文件并转换为html wordToHtml(wordFilePath)  
        ///<summary>  
        ///上传文件并转存为html  
        ///</summary>  
        ///<param name="wordFilePath">word文档在客户机的位置</param>  
        ///<returns>上传的html文件的地址</returns>  
        public string wordToHtml(System.Web.UI.HtmlControls.HtmlInputFile wordFilePath)
        {
          //  Microsoft.Office.Interop.Word.ApplicationClass word = new Microsoft.Office.Interop.Word.ApplicationClass();
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

            object fileName = wordFilePath;

            Microsoft.Office.Interop.Word.Document doc = (Microsoft.Office.Interop.Word.Document)docsType.InvokeMember("Open",
            System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { fileName, true, true });

            // 转换格式，另存为html  
            Type docType = doc.GetType();

            string filename = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString();

            // 判断指定目录下是否存在文件夹，如果不存在，则创建  
            if (!Directory.Exists(Server.MapPath("~\\html")))
            {
                // 创建up文件夹  
                Directory.CreateDirectory(Server.MapPath("~\\html"));
            }

            //被转换的html文档保存的位置  
            string ConfigPath = HttpContext.Current.Server.MapPath("/" + filename + ".html");
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
            //转到新生成的页面  
            return ("/" + filename + ".html");

        }

    }
}