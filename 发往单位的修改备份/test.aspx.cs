using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["userid"] != null)
                {
                    string userid = Session["userid"].ToString();
                    if (Session["oplonger"] != null)
                    {
                        string docname = Session["oplonger"].ToString();
                        if (File.Exists(Server.MapPath("~/pageoffice/doc/newoplonger/") + docname))
                        {
                            //如果文件存在，先删除再保存
                            System.IO.File.Delete(docname);
                            PageOffice.FileSaver fs = new PageOffice.FileSaver();

                            fs.SaveToFile(Server.MapPath("pageoffice/doc/newoplonger/" + docname));
                            fs.Close();
                            Sql_Caozuorizhi.WriteRizhi(userid, "longer_table", "生成中长期预报：" + docname + " 成功");
                            Response.Write("保存成功！");

                        }
                        else {
                            PageOffice.FileSaver fs = new PageOffice.FileSaver();
                            fs.SaveToFile(Server.MapPath("pageoffice/doc/newoplonger/" + docname));
                            fs.Close();
                            Sql_Caozuorizhi.WriteRizhi(userid, "longer_table", "生成中长期预报：" + docname + " 成功");
                            Response.Write("保存成功！");
                        }
                    }
                    else
                    {
                        Sql_Caozuorizhi.WriteRizhi(userid, "longer_table", "生成中长期预报失败");
                        Response.Write("保存失败");
                    }
                }
                else
                {
//未登录或超时
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("保存word失败" + ex.ToString());
            }
        }


    }
}