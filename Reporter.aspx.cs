using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class Reporter : System.Web.UI.Page
    {
        public DateTime dt;
        public string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (Session["type"] != null)
                {
                    // Response.Write("<script language='javascript'>alert('" + Session["type"].ToString() + "');</script>");
                    type = Session["type"].ToString();
                }
                else
                {

                }

                //Session["type"] = "fl";
                //Session["type"] = "sw";
                //Session["type"] = "cx";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}