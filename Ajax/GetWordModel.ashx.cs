using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// GetWordModel 的摘要说明
    /// </summary>
    public class GetWordModel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            string type = context.Request["type"].ToString();
            if (method == "getModel")
            {
                Sql_UPLOADWORD sql_uploadword = new Sql_UPLOADWORD();
                DataTable dt = (DataTable)sql_uploadword.get_UPLOADWORDdata(type);
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"list\":[");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                            + "\",\"oldname\":\"" + dt.Rows[i]["OLDNAME"]
                            + "\",\"newname\":\"" + dt.Rows[i]["NEWNAME"]
                            + "\",\"type\":\"" + dt.Rows[i]["TYPE"] + "\"},");
                    }

                }
                if (dt != null && dt.Rows.Count <= 0)
                {
                    context.Response.Write(sb.ToString() + "]}");
                }
                else
                {
                    context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
                }
            }
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