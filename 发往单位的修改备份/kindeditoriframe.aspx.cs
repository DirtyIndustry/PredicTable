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
    public partial class kindeditoriframe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void WXButton_Click(object sender, EventArgs e)
        {
            string userid = "";
            string strout = "";
          
            KJ_GongZhongPingTai pt = new KJ_GongZhongPingTai();
            Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
            pt.TIME = DateTime.Now;
            // Session["userid"] = "admin";//测试完毕删除
            //if (Session["userid"] != null)
            //{
            //    Response.Write("<script language='javascript'>alert('" + Session["userid"].ToString() + "');</script>");
            //}
            //else
            //{
            //    Response.Write("<script language='javascript'>alert(11);</script>");
            //}

            if (Session["userid"] != null)
            {
                userid = Session["userid"].ToString();
               // Response.Write("<script language='javascript'>alert('" + Session["userid"].ToString() + "');</script>");
            }
            else {

                userid = "";
               // Response.Write("<script language='javascript'>alert(11);</script>");
            }
            pt.USERID = userid;
            pt.MESTYPE = "微信";
            pt.STATE = "已入库";
            pt.DOCTYPE = DOCTYPE.Value;
            pt.DOCUMENTCONTENT = schtmlnr.Value;
            pt.DXGROUP = "";
            pt.ABSTRACT = ABSTRACT.Value;
            pt.TYPE = TYPE.Value;
            pt.SUBJECT = SUBJECT.Value;
           
            int a = sql_gzpt.Add_GongZhongPingTaiWX(pt);
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
                // HttpFileCollection files = HttpContext.Current.Request.Files;
                try
                {
                    //获取文件类型
                    int a2 = 0;
                    string ss = urllist.Value;
                    if (ss != "")
                    {
                        ss = ss.Substring(0, ss.Length - 1);

                        string[] files = ss.Split(',');
                        for (int i = 0; i < files.Length; i++)
                        {

                            ptfj.SORTID = (i + 1).ToString();

                            ptfj.TYPE = "展示文件";
                            ptfj.WAIID = waiid;
                            string fileName = files[i];
                            if (fileName != "")
                            {
                                FileStream fs = File.OpenRead(Server.MapPath(files[i].ToString()));
                                byte[] b = new byte[fs.Length];
                                fs.Read(b, 0, b.Length);
                                fs.Close();
                                ptfj.ANNEX = b;
                                ptfj.FILENAME = System.IO.Path.GetFileName(files[i].ToString());
                                int a1 = sql_gzpt.Add_GongZhongPingTaiFJ(ptfj);
                                if (a1 > 0)
                                {
                                    a2 = a2 + a1;
                                }
                            }
                        }
                     
                    }
                    //遍历表单元素
                    HttpFileCollection files1 = HttpContext.Current.Request.Files;

                    //获取文件类型
                    string ss1 = Request.Form["vtype"];
                    //获取图片排序
                    string ss2 = Request.Form["SORTID"];
                    
                    if (files1.Count > 0)
                    {

                        for (int iFile = 0; iFile < files1.Count; iFile++)
                        {
                            string[] type = ss1.Split(',');
                            string[] SORTID = ss2.Split(',');
                            ptfj.SORTID = SORTID[iFile].ToString();
                            ptfj.TYPE = type[iFile].ToString();
                            ptfj.WAIID = waiid;
                            int a1 = 0;
                            //访问单独文件 

                            HttpPostedFile postedFile = files1[iFile];
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
                              //  Label1.Text = "<br>请您选择一个文件!!!";
                            }
                            if (a1 > 0)
                            {
                                a2 = a2 + a1;


                            }

                        }

                    }
                    if (a2 > 0)
                    {
                        Response.Write("<script>parent.location.href='GongZhongPingTaiGL.aspx';</script>");
                    }
                }

                catch (Exception Ex)
                {
                    // Label1.Text = Ex.ToString();
                  //  Response.Write("<script language='javascript'>alert('" + Ex.ToString() + "');</script>");
                }
            }

        }
              
    }
}
