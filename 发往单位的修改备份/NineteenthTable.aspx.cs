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
    public partial class NineteenthTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        }
        //编辑按钮
        protected void startoperation_Click(object sender, EventArgs e)
        {
            string desFileName = "";
            string templateFileName = list.Value;
            string pubDateStr = pubDate.Value.Replace("-","");
            switch (templateFileName)
            {
                case "19号预报单--周.doc":
                    desFileName = "YB_NCS_HB_7day_" + pubDateStr + "_NMFC.doc";
                    break;
                case "19号预报单--旬.doc":
                    desFileName = "YB_NCS_HB_10day_" + pubDateStr + "_NMFC.doc";
                    break;
                case "19号预报单--月.doc":
                    desFileName = "YB_NCS_HB_1mon_"+ pubDateStr + "_NMFC.doc";
                    break;
                case "19号预报单--年.doc":
                    desFileName = "YB_NCS_HB_1yr_" + pubDateStr + "_NMFC.doc";
                    break;
                default:
                    break;

            }
            string templateFilePath = System.Web.HttpContext.Current.Server.MapPath("/word/" + templateFileName);
            string desFilePath = System.Web.HttpContext.Current.Server.MapPath("/scword/" + desFileName);
            if(File.Exists(desFilePath))
                System.IO.File.Delete(desFilePath);//删除word文件
            File.Copy(templateFilePath, desFilePath);
            Session["op19table"] = desFileName;
            Session["userid"] = "admin";//测试代码，用完删除
            Session["SendUnit"] = this.hidUnitName.Value;
            Session["templateFileName"] = templateFileName;
            PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
            PageOfficeCtrl1.SaveFilePage = "testNineteenTable.aspx";

            PageOfficeCtrl1.Visible = true;
            PageOfficeCtrl1.WebOpen(desFilePath, PageOffice.OpenModeType.docNormalEdit, "Tom");

        }
    }
}