using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class test3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["userid"] != null)
                {
                    string userid = Session["userid"].ToString();
                    string docname = Session["download"].ToString();
                    if (File.Exists(Server.MapPath("~/download/") + docname))
                    {
                        //如果文件存在，先删除再保存
                        System.IO.File.Delete(docname);
                        PageOffice.FileSaver fs = new PageOffice.FileSaver();

                        fs.SaveToFile(Server.MapPath("~/download/" + docname));
                        fs.Close();
                        //Sql_Caozuorizhi.WriteRizhi(userid, "longer_table", "生成中长期预报：" + docname + " 成功");
                        Response.Write("保存文件成功！");

                    }
                    else
                    {
                        PageOffice.FileSaver fs = new PageOffice.FileSaver();

                        fs.SaveToFile(Server.MapPath("~/download/" + docname));
                        fs.Close();
                        //Sql_Caozuorizhi.WriteRizhi(userid, "longer_table", "生成中长期预报：" + docname + " 成功");
                        Response.Write("保存文件成功！");
                    }
                }
                else
                {
                    //未登录或超时
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改文件失败" + ex.ToString());
            }
        }
    }
}