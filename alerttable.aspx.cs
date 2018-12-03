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
    public partial class alerttable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
            if (!IsPostBack)
            {
                fillList();
            }

        }
        //填充下拉框
        protected void fillList()
        {
            string type = this.type.Value;
            Sql_UPLOADWORD sql_uploadword = new Sql_UPLOADWORD();
            DataTable dt = (DataTable)sql_uploadword.get_UPLOADWORDdata(type);


            if (dt.Rows.Count > 0)
            {
                this.list.DataSource = dt;
                this.list.DataTextField = "OLDNAME";
                this.list.DataValueField = "NEWNAME";
                this.list.DataBind();


            }
        }
       
        protected void startoperation_Click(object sender, EventArgs e)
        {
           // this.OperationWord.Style.Add("display", "block");

            string newname = "";//编辑后预报单文件名
            string filename = "";//模板文件名
            if (checkbox1 .Checked)
            {
                filename = list.Value;
                string time = DateTime.Now.ToString("yyyyMMdd");
                string[] s = filename.Split(new char[] { '_' });
                newname = s[0] + "_" + s[1] + "_" + s[2] + "_" + s[3] + "_"+ time + "_"+ s[4];
            }
            else
            {
                newname = this.newname.Value + ".doc";
                filename = uploadname.Value + ".doc";
            }
             
            HttpContext.Current.Session["warning"] = newname;

            PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
            PageOfficeCtrl1.SaveFilePage = "test2.aspx";
            // filename = uploadname.Value + ".doc";
            string filePath = "pageoffice/doc/warning/" + filename;

            PageOfficeCtrl1.Visible = true;
            PageOfficeCtrl1.WebOpen(filePath, PageOffice.OpenModeType.docNormalEdit, "Tom");

        }
    }
}