using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class WarningDownLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
            Session["userid"] = "admin";
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDownLoad_Click(object sender, EventArgs e)
        {
            string FileName = this.hidFileName.Value;
            string FileType = this.hidFileType.Value;
            var dt = (DataTable)new sql_WarningDownLoad().GetWarningListFileFlow(FileName, FileType);
            List<string> filename = new List<string>();
            List<byte[]> fileflow = new List<byte[]>();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    filename.Add(dt.Rows[i]["DOCNAME"].ToString());
                    fileflow.Add((byte[])dt.Rows[i]["FILEfLOW"]);
                }
            }
            for (int j = 0; j < fileflow.Count; j++)
            {
                string result = ByteConvertWord(fileflow[j], filename[j]);
                if (!result.Equals(""))
                {
                    Response.ContentType = @"application/msword";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename[j]));
                    Response.Flush();
                    Response.WriteFile(result, true);
                    //Response.Write(result);  
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string FileName = this.hidFileName.Value;
            string FileType = this.hidFileType.Value;
            var dt = (DataTable)new sql_WarningDownLoad().GetWarningListFileFlow(FileName, FileType);
            byte[] showbyte;
            var fileName = "";
            List<string> filename = new List<string>();
            if (dt.Rows.Count > 0)
            {
                showbyte = (byte[])dt.Rows[0]["FILEfLOW"];
                fileName = dt.Rows[0]["DOCNAME"].ToString();
                string filePath = Server.MapPath("/download/" + fileName);
                if (File.Exists(filePath))
                {
                    //如果存在则删除
                    File.Delete(filePath);
                }
                //生成Word并加载到pageoffice
                GetWordBByte(showbyte, filePath, fileName);
            }
        }
        //在本地生成文件
        public bool GetWordBByte(byte[] data, string filePath, string fileName)
        {
            string reponse = "<script  language='javascript' type='text/javascript'>"
                            + "$(function(){"
                            + "$('#dlg_show').dialog('open');"
                            + "});"
                            + "</script>";
            try
            {
                File.WriteAllBytes(System.Web.HttpContext.Current.Server.MapPath("/download/" + fileName), data);
                //加载到pageoffice
               // Session["userid"] = "admin";//测试代码，用完删除
               // Session["download"] = fileName;
               // PageOfficeCtrl1.Visible = true;
              //  PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
              //  PageOfficeCtrl1.SaveFilePage = "test3.aspx";

              //  PageOfficeCtrl1.WebOpen(filePath, PageOffice.OpenModeType.docNormalEdit, "Tom");

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", reponse);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog.Write("流生成本地文件出错！  " + ex.ToString());
                return false;
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
    }
}