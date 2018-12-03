using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable.Ascx
{
    //public delegate void GetCNModelDelegate(string fbtime, string ybtime, string ybcontent);
    public partial class CNModel : System.Web.UI.UserControl
    {
        //public event GetCNModelDelegate getCNModelEvent;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["BackImage"] != null)
            {
                string reponse = "<script  language='javascript' type='text/javascript'>"
                                 + "$(function(){$('#ctl07_bg_img').css('background-image', 'url(" + Session["BackImage"].ToString() + ")');});";
                if (Session["ENPublishTime"] != null) {
                    reponse += "$('#hid_publishTime').val('" + Session["ENPublishTime"].ToString() + "');";
                }
                if (Session["companyName"] != null) {
                    reponse += "$('#hid_publishCompanyName').val('" + Session["companyName"].ToString() + "');";
                }
                reponse += "</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", reponse);
            }
        }
    }
}