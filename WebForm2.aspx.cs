using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    int nPageControls = Page.Controls.Count;
                    for (int i = 0; i < nPageControls; i++)
                    {
                        foreach (System.Web.UI.Control control in Page.Controls[i].Controls)
                        {
                            if (control.GetType().FullName.ToLower().Equals("system.web.ui.webcontrols.textbox"))

                            {
                                TextBox txt = (TextBox)control;
                                txt .Attributes.Add("readonly", "true");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
               
            }

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
               // tb.Attributes.Add("readonly", "false");
                tb.BackColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
            }



        }

    }
}