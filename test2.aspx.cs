using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Session["userid"] = "admin";//测试完毕删除
              
                if (Session["userid"] != null)
                {
                   
                    string userid = Session["userid"].ToString();
                    if (Session["warning"] != null)
                    {
                        string docname = Session["warning"].ToString();
                        
                        if (File.Exists(Server.MapPath("~/pageoffice/doc/newwarning/") + docname))
                        {
                            //如果文件存在，先删除再保存
                            System.IO.File.Delete(docname);
                            PageOffice.FileSaver fs = new PageOffice.FileSaver();

                            fs.SaveToFile(Server.MapPath("pageoffice/doc/newwarning/" + docname));
                            fs.Close();
                            Sql_Caozuorizhi.WriteRizhi(userid, "warning_table", "生成灾害预警报：" + docname + " 成功");
                            Response.Write("保存成功！");

                        }
                        else {
                            PageOffice.FileSaver fs = new PageOffice.FileSaver();

                            fs.SaveToFile(Server.MapPath("pageoffice/doc/newwarning/" + docname));
                            fs.Close();
                            Sql_Caozuorizhi.WriteRizhi(userid, "warning_table", "生成灾害预警报：" + docname + " 成功");
                            Response.Write("保存成功！");

                        }
                    }
                    else
                    {
                        WriteLog.Write("未登录或超时2");
                        Sql_Caozuorizhi.WriteRizhi(userid, "warning_table", "生成灾害预警报失败！");
                        Response.Write("保存失败！");
                    }
                }
                else
                {
                    //未登录或超时
                    WriteLog.Write("未登录或超时");
                }

            }
            catch (Exception ex)
            {
                WriteLog.Write("保存word失败" + ex.ToString());
            }
        }
    }
}