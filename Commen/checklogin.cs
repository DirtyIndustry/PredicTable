using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
/// <summary>
/// checklogin 的摘要说明
/// </summary>
public class checklogin : System.Web.UI.Page, IRequiresSessionState
{
    public checklogin()
    {
    }
    delegate void dg();

    public void setmsg(ref string touxiang, ref string name, ref string email, ref string bumen, ref string weidu) {

    }
    override protected void OnInit(EventArgs e)
    {

        base.OnInit(e);
       var k= base.ToString();
        if (Session["userid"] == null)
        {
            HttpContext.Current.Response.Write("<script>  top.location.href='../Login.aspx';</script>");
        }
        if (!Request.IsAuthenticated)
        {
           FormsAuthentication.RedirectToLoginPage();
        }
    }

}
