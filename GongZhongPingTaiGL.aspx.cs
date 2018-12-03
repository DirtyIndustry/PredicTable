using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class GongZhongPingTaiGL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

      
        //protected void DXButton_Click(object sender, EventArgs e)
        //{
        //    string userid = "";
        //    KJ_GongZhongPingTai pt = new KJ_GongZhongPingTai();
        //    Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
        //    pt.TIME = DateTime.Now;
        //    Session["userid"] = "admin";//测试完毕删除
        //    if (Session["userid"] != null)
        //    {
        //        userid = Session["userid"].ToString();
        //    }
        //    else {

        //        userid = "";
        //    }
        //    pt.MESTYPE = "短信";
        //    pt.USERID = userid;
        //    pt.STATE = "未发送";
        //    pt.DOCTYPE = DOCTYPEdx.Value;
        //    pt.DOCUMENTCONTENT = duanxin1.Value ;
        //    pt.DXGROUP = group.Value;
          
        //    int a = sql_gzpt.Add_GongZhongPingTai(pt);
        //    if (a > 0)
        //    {
        //        Response.Write("<script>location.href='GongZhongPingTaiGL.aspx';</script>");
        //    }
        //}
    }
}