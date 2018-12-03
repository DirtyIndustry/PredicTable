using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class iframUpLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 上传Word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// /// <param name="type">CN:中文报表；EN：英文报表</param>
        protected void BtnUpLoad_Click(object sender, EventArgs e)
        {
            if (FileUploadWord.HasFile)
            {
                string newName = this.newname.Value + ".doc";
                string type = this.type.Value;
                string filename = Path.GetFileName(FileUploadWord.FileName);//原文件的名字
                string extendName = System.IO.Path.GetExtension(filename).ToLower();   //获取后缀名
                string ErrorMessage = string.Empty;
                if (extendName != ".doc" && extendName != ".docx")
                {
                    this.txtMessage.Text = "上传文件格式错误！请重新选择！";
                }
                else
                {
                    if (type == "CN")
                    {
                        if (File.Exists(Server.MapPath("~/pageoffice/doc/CN-MediumAndLong/") + newName))
                        {
                            System.IO.File.Delete(filename);
                            FileUploadWord.SaveAs(Server.MapPath("~/pageoffice/doc/CN-MediumAndLong/") + newName);
                            this.txtMessage.Text = "上传成功,并已经替换已有模板";
                            SaveFile(filename, newName, type);

                        }
                        else
                        {
                            FileUploadWord.SaveAs(Server.MapPath("~/pageoffice/doc/CN-MediumAndLong/") + newName);
                            this.txtMessage.Text = "模板上传成功!";
                            SaveFile(filename, newName, type);
                        }
                    }
                    else if (type == "EN")
                    {
                        if (File.Exists(Server.MapPath("~/pageoffice/doc/EN-MediumAndLong/") + newName))
                        {
                            System.IO.File.Delete(filename);
                            FileUploadWord.SaveAs(Server.MapPath("~/pageoffice/doc/EN-MediumAndLong/") + newName);
                            this.txtMessage.Text = "上传成功,并已经替换已有模板";
                            SaveFile(filename, newName, type);

                        }
                        else
                        {
                            FileUploadWord.SaveAs(Server.MapPath("~/pageoffice/doc/EN-MediumAndLong/") + newName);
                            this.txtMessage.Text = "模板上传成功!";
                            SaveFile(filename, newName, type);
                        }
                    }
                    Response.Write("<script>parent.location.href='UpLoadMediumAndLong.aspx';</script>");
                }
            }
        }

        /// <summary>
        /// 存入数据库
        /// </summary>
        /// <param name="oldname">文件原名</param>
        /// <param name="newname">生成模板后文件名</param>
        /// <param name="type">CN:中文报表；EN：英文报表</param>
        /// <returns></returns>
        private int SaveFile(string oldNname, string newName, string type)
        {
            int a1 = 0;
            KJ_UPLOADWORD tbl = new KJ_UPLOADWORD();
            List<int> a = new List<int>();
            int i1 = 0;
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new Sql_UPLOADWORD().get_UPLOADWORDdata(newName, type);
            if (tblybddocument.Rows.Count > 0)
            {
                //edit
                UPLOADWORD uploadword = new UPLOADWORD();
                a1 = uploadword.uploadword(oldNname, newName, type);
            }
            else
            {
                //存入数据库
                UPLOADWORD insertword = new UPLOADWORD();
                a1 = insertword.addword(oldNname, newName, type);
            }

            return a1;
        }
    }
}