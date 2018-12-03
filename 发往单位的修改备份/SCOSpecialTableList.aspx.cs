using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class SCOSpecialTableList : System.Web.UI.Page
    {
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
                //Session["type"] = "fl";
                //Session["type"] = "sw";
            }
            catch (Exception)
            {   
                throw;
            }
        }
       
    }
}