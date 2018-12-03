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
    public partial class gzptiframe : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Browser.Browser.ToUpper().Contains("IE"))
            //    Response.Headers.Add("P3P", "CP=CAO PSA OUR");

            //测试完毕删除
            //if (Session["userid"] != null)
            //{
            //    Response.Write("<script language='javascript'>alert('" + Session["userid"].ToString() + "');</script>");
            //}
            //else
            //{
            //    Response.Write("<script language='javascript'>alert(11);</script>");
            //}
           
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string userid = "";
            string strout = "";
            Label1.Text ="";
            KJ_GongZhongPingTai pt = new KJ_GongZhongPingTai();
            Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
            pt.TIME = DateTime.Now;
           // Session["userid"] = "admin";//测试完毕删除
            if (Session["userid"] != null)
            {
                userid = Session["userid"].ToString();
            }
            else {

                userid = "";
            }
            pt.USERID = userid;
            pt.MESTYPE ="微博";
            pt.STATE = "已入库";
            pt.DOCTYPE = DOCTYPE.Value;
            pt.DOCUMENTCONTENT = DOCUMENTCONTENT.Value;
            pt.DXGROUP = ""; 
            int a = sql_gzpt.Add_GongZhongPingTai(pt);
            if (a > 0)
            {
                KJ_GONGZHONGPINGTAIFUJIAN ptfj = new KJ_GONGZHONGPINGTAIFUJIAN();
                Sql_GONGZHONGPINGTAI gzpt = new Sql_GONGZHONGPINGTAI();
                DataTable dt = (DataTable)gzpt.getMaxid(Session["userid"].ToString());
                string waiid = "";
                if (dt != null)
                {
                   waiid = dt.Rows[0][0].ToString();
                }
                //遍历表单元素
                HttpFileCollection files = HttpContext.Current.Request.Files;
                try
                {
                    //获取文件类型
                    string ss1 = Request.Form["vtype"];
                    //获取图片排序
                    string ss2 = Request.Form["SORTID"];
                    for (int iFile = 0; iFile < files.Count; iFile++)
                    {
                        string[] type = ss1.Split(',');
                        string[] SORTID = ss2.Split(',');
                        ptfj.SORTID = SORTID[iFile].ToString();
                        ptfj.TYPE = type[iFile].ToString();
                        ptfj.WAIID = waiid;
                        //访问单独文件 
                        HttpPostedFile postedFile = files[iFile];
                        string fileName = Path.GetFileName(postedFile.FileName);
                        if (postedFile.FileName != "")
                        {
                            postedFile.SaveAs(Server.MapPath("~/gzptfj/") + fileName);
                            FileStream fs = File.OpenRead(Server.MapPath("~/gzptfj/") + fileName);
                            //FileStream fs = File.OpenRead(postedFile.FileName);
                            byte[] b = new byte[fs.Length];
                            fs.Read(b, 0, b.Length);
                            fs.Close();
                            System.IO.File.Delete(Server.MapPath("~/gzptfj/") + fileName);
                            ptfj.ANNEX = b;
                            ptfj.FILENAME = fileName;
                            int a1 = sql_gzpt.Add_GongZhongPingTaiFJ(ptfj);
                            if (a1 > 0)
                            {
                                Response.Write("<script>parent.location.href='GongZhongPingTaiGL.aspx';</script>");
                            }
                        }
                        else
                        {
                                Response.Write("<script>parent.location.href='GongZhongPingTaiGL.aspx';</script>");
                        }
                    }
                     Label1.Text = strout.ToString();
                }
                catch (Exception Ex)
                {
                    Label1.Text = Ex.Message.ToString();
                }
            }
        }
       
    
      

    }
}