using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class PMTableListNew : System.Web.UI.Page
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
                    type = Session["type"].ToString();
                }
                else
                {

                }
               // Session["type"] = "fl";
                //Session["type"] = "cx";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}