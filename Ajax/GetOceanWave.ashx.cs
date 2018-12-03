using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// GetOceanWave 的摘要说明
    /// 获取海浪解析数据
    /// </summary>
    public class GetOceanWave : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["method"].ToString();
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