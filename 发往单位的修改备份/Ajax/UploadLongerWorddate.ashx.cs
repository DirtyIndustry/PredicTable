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
    /// UploadLongerWorddate 的摘要说明
    /// </summary>
    public class UploadLongerWorddate : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            string type = context.Request["type"].ToString();
            if (method == "del")
            {
                var id = context.Request["Id"].ToString();
                int nums = new Sql_UPLOADWORD().Del_UPLOADWORD(id);
                if (nums <= 0)
                {

                    context.Response.Write("Error");
                }
                else
                {

                    context.Response.Write("Success");
                }


            }
            if (method == "getall")
            {
                getbydata_YCYB_WAVE(context);
            }
              
         
        }
        //获取数据列表
        public void getbydata_YCYB_WAVE(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string type = context.Request["type"].ToString();
            StringBuilder sb = new StringBuilder();
            //sb.Clear();
            Sql_UPLOADWORD sql_uploadword = new Sql_UPLOADWORD();
            DataTable dt = (DataTable)sql_uploadword.get_UPLOADWORDdata(type);
            sb.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                        + "\",\"oldname\":\"" + dt.Rows[i]["OLDNAME"]
                        + "\",\"newname\":\"" + dt.Rows[i]["NEWNAME"]
                        + "\",\"type\":\"" + dt.Rows[i]["TYPE"] + "\"},");
                }
                
            }
            if (dt.Rows.Count <= 0)
            {
                context.Response.Write(sb.ToString() + "]");
            }
            else
            {
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]");
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