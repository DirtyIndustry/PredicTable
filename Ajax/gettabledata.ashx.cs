using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Web.SessionState;

namespace PredicTable.Ajax
{
    /// <summary>
    /// gettabledata 的摘要说明
    /// </summary>
    public class gettabledata : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder jsonstr = new StringBuilder();
            //要验证session
            sql_TBLYRBHWINDWAVE72HFORECASTTWO table_01 = new sql_TBLYRBHWINDWAVE72HFORECASTTWO();
             TBLYRBHWINDWAVE72HFORECASTTWO model_01 = new TBLYRBHWINDWAVE72HFORECASTTWO();
            // context.Request.Form["dat"]
            model_01.PUBLISHDATE = DateTime.Now;
            DataTable dt_01= (DataTable)table_01.get_TBLYRBHWINDWAVE72HFORECASTTWO_AllData(model_01, "p");
            if (dt_01.Rows.Count>0) {

            }

            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}