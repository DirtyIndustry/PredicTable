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
    public partial class gzptiframefj : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            KJ_GongZhongPingTai pt = new KJ_GongZhongPingTai();
            Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();


            KJ_GONGZHONGPINGTAIFUJIAN ptfj = new KJ_GONGZHONGPINGTAIFUJIAN();
            Sql_GONGZHONGPINGTAI gzpt = new Sql_GONGZHONGPINGTAI();


            string waiid = this.waiid.Value;

            //遍历表单元素
            HttpFileCollection files = HttpContext.Current.Request.Files;
            try
            {
                //获取文件类型
                string ss1 = Request.Form["vtype"];
                //获取图片排序
                string ss2 = Request.Form["SORTID"];
                int a2 = 0;
                if (files.Count > 0)
                {
                    
                    for (int iFile = 0; iFile < files.Count; iFile++)
                    {
                        string[] type = ss1.Split(',');
                        string[] SORTID = ss2.Split(',');
                        ptfj.SORTID = SORTID[iFile].ToString();
                        ptfj.TYPE = type[iFile].ToString();
                        ptfj.WAIID = waiid;
                        int a1 = 0;
                        //访问单独文件 
                      
                        HttpPostedFile postedFile = files[iFile];
                       // string fileName = System.IO.Path.GetFileName(postedFile.FileName);
                        string fileName = Path.GetFileName(postedFile.FileName);

                        if (postedFile.FileName != "")
                        {
                            postedFile.SaveAs(Server.MapPath("~/gzptfj/") + fileName);
                            FileStream fs = File.OpenRead(Server.MapPath("~/gzptfj/") + fileName);
                            byte[] b = new byte[fs.Length];
                            fs.Read(b, 0, b.Length);
                            fs.Close();
                            System.IO.File.Delete(Server.MapPath("~/gzptfj/") + fileName);
                            ptfj.ANNEX = b;
                            ptfj.FILENAME = fileName;
                          a1 = sql_gzpt.Add_GongZhongPingTaiFJ(ptfj);
                        }
                        else
                        {
                            Label1.Text = "<br>请您选择一个文件!!!";
                        }

                        if (a1 > 0)
                        {
                            a2 = a2 + a1;

                        }
                    }
                    if (a2 > 0)
                    {
                        Response.Write("<script>parent.location.href='GongZhongPingTaiGL.aspx';</script>");

                    }
                }
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.Message.ToString();
            }

        }
    }
}