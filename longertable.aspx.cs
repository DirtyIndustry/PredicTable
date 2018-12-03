using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

namespace PredicTable
{
    public partial class longertable : System.Web.UI.Page
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
     //编辑按钮
        protected void startoperation_Click(object sender, EventArgs e)
        {
            string newname = "";//编辑后预报单文件名
            string filename = "";//模板文件名
            if (checkbox1.Checked)
            {
                filename = list.Value;
                string time = DateTime.Now.ToString("yyyyMMdd");
                string[] s = filename.Split(new char[] { '_' });
                newname = s[0] + "_" + s[1] + "_" + s[2] + "_" + s[3] + "_" + time + "_" + s[4];
            }
            else
            {
                newname = this.newname.Value + ".doc";
                filename = uploadname.Value + ".doc";
            }
            HttpContext.Current.Session["oplonger"] = newname;

            PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
            PageOfficeCtrl1.SaveFilePage = "test.aspx";
            string filePath = "pageoffice/doc/oplonger/" + filename;
            
            PageOfficeCtrl1.Visible= true;
            PageOfficeCtrl1.WebOpen(filePath, PageOffice.OpenModeType.docNormalEdit, "Tom");
            
        }

    }
}