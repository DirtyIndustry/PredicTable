using PredicTable.Commen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// Summary description for getPublishedTemplates
    /// </summary>
    public class getPublishedTemplates : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var templatesStr = Opt_PublishedTemplates.getPublishedTempletes();
            context.Response.ContentType = "text/plain";
            context.Response.Write(templatesStr);
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