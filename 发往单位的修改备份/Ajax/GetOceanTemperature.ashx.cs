using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// GetOceanTemperature 的摘要说明
    /// 获取海温数据
    /// </summary>
    public class GetOceanTemperature : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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