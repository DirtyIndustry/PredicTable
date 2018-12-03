using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Core;//COM选项卡中的"Microsoft Office 11.0 Object Library"
using Word = Microsoft.Office.Interop.Word;//.NET选项卡中的"Microsoft.Office.Interop.Word"
using System.Collections;
using System.IO;

namespace PredicTable
{
    public partial class TableList : System.Web.UI.Page
    {
        public DateTime dt;
        public string type="";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(Request.Form["type"]))
                {
                    type = Request.Form["type"];
                }
                //type = "fl";//测试完毕删除

            }
            catch (Exception)
            {
                throw;
            }   
        }
 

    }
}