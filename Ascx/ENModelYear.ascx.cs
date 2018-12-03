using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable.Ascx
{
    /// <summary>
    /// 英文预报年预报
    /// </summary>
    public partial class ENModelYear : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["BackImage"] != null)
            {
                string reponse = "<script  language='javascript' type='text/javascript'>"
                                  + "$(function(){$('#ctl07_bg_img').css('background-image', 'url(" + Session["BackImage"].ToString() + ")');});";
                //发布时间、发布时效、发布单位
                if (Session["ENPublishTime"] != null && Session["ENFBDW"] != null)
                {
                    reponse += "$('#hid_fbdw').val('" + Session["ENFBDW"].ToString() + "');"
                                   + "$('#hid_publishTime').val('" + Session["ENPublishTime"].ToString() + "');";
                }
                reponse += "</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", reponse);
            }
        }
    }
}