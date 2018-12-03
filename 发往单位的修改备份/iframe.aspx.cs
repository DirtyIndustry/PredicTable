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
    public partial class iframe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (FileUploadWord.HasFile)
            {
                string newname = this.newname.Value+".doc";
                string type = this.type.Value;
                //string filename = FileUploadWord.PostedFile.FileName;
                string filename = Path.GetFileName(FileUploadWord.FileName);//原文件的名字
                string extendName = System.IO.Path.GetExtension(filename).ToLower();   //获取后缀名
                string ErrorMessage = string.Empty;
                if (extendName != ".doc" && extendName != ".docx")
                {
                    TextBox1.Text = "上传文件格式错误！请重新选择！";
                }
                else
                {
                    if (type == "0")
                    {

                        if (File.Exists(Server.MapPath("~/pageoffice/doc/oplonger/") + newname))
                        {
                            System.IO.File.Delete(filename);
                            FileUploadWord.SaveAs(Server.MapPath("~/pageoffice/doc/oplonger/") + newname);
                            TextBox1.Text = "上传成功,并已经替换已有模板";
                            ruku(filename, newname, type);

                        }
                        else
                        {
                            FileUploadWord.SaveAs(Server.MapPath("~/pageoffice/doc/oplonger/") + newname);
                            TextBox1.Text = "上传成功!";
                            ruku(filename, newname, type);
                        }
                        Response.Write("<script>parent.location.href='UploadLongerword.aspx';</script>");
                    }
                    else if (type == "1")
                    {
                        if (File.Exists(Server.MapPath("~/pageoffice/doc/warning/") + newname))
                        {
                            System.IO.File.Delete(filename );
                            FileUploadWord.SaveAs(Server.MapPath("~/pageoffice/doc/warning/") + newname);
                            TextBox1.Text = "上传成功,并已经替换已有模板";
                            ruku(filename, newname, type);

                        }
                        else
                        {
                            FileUploadWord.SaveAs(Server.MapPath("~/pageoffice/doc/warning/") + newname);
                            TextBox1.Text = "上传成功!";
                            ruku(filename, newname, type);
                        }
                        Response.Write("<script>parent.location.href='UploadAlerttableword.aspx';</script>");
                    }
                }
                
            }
        }

        /// <summary>
        /// 生成表单入库
        /// </summary>
        /// <param name="fileName">入库的文件名</param>
        /// <param name="strone">新的文件名</param>
        /// <param name="type">中长期or灾害</param>
        private int ruku(string oldname, string newname, string type)
        {
            int a1 = 0;
            KJ_UPLOADWORD tbl = new KJ_UPLOADWORD();
            List<int> a = new List<int>();
            int i1 = 0;
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new Sql_UPLOADWORD().get_UPLOADWORDdata (newname,type);
            if (tblybddocument.Rows.Count > 0)
            {
                //edit
                UPLOADWORD uploadword = new UPLOADWORD();
                a1 = uploadword.uploadword(oldname, newname, type);
               
            }
            else
            {
                //存入数据库
                UPLOADWORD insertword = new UPLOADWORD();
                a1 = insertword.addword(oldname, newname, type);
            }
           
            return a1;
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

