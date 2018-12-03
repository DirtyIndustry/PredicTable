using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class MediumAndLongQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            var rowId = this.hidRowId.Value;
            var dt = (DataTable)new Sql_HandelWord().get_TableQuerybyid(rowId);
            byte[] showbyte;
            var fileName = "";
            List<string> filename = new List<string>();
            if (dt.Rows.Count > 0)
            {
                showbyte = (byte[])dt.Rows[0]["WORDFLOW"];
                fileName = dt.Rows[0]["WORDNAME"].ToString();
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
                Session["userid"] = "admin";//测试代码，用完删除
                Session["download"] = fileName;
                PageOfficeCtrl1.Visible = true;
                PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
                PageOfficeCtrl1.SaveFilePage = "test3.aspx";

                PageOfficeCtrl1.WebOpen(filePath, PageOffice.OpenModeType.docNormalEdit, "Tom");

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
        /// 下载文件，另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDownLoad_Click(object sender, EventArgs e)
        {
            string FileId = this.hidFileID.Value;
            string FileName = this.hidFileName.Value;
            var dt = (DataTable)new Sql_HandelWord().get_TableQuerybyid(FileId);
            List<byte[]> fileby = new List<byte[]>();
            List<string> filename = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fileby.Add((byte[])dt.Rows[i]["WORDFLOW"]);
                    filename.Add(dt.Rows[i]["WORDNAME"].ToString());
                }
            }
            for (int j = 0; j < fileby.Count; j++)
            {
                string result = ByteConvertWord(fileby[j], filename[j]);
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