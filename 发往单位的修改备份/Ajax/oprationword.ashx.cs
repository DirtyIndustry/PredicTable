using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace PredicTable.Ajax
{
    /// <summary>
    /// oprationword 的摘要说明
    /// </summary>
    public class oprationword : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                context.Response.ContentType = "text/plain";
                string method = context.Request["method"].ToString();
                switch (method)
                {
                    case "oplonger": oplonger(context); break;
                    case "warning": warning(context); break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("操作表单数据出错" + ex.ToString());
            }
           
        }
       
        public void oplonger(HttpContext context) {
            //获取值 生成Session
            string datas = context.Request["datas"].ToString();
          
            context.Session["oplonger"] = datas;
            string templatedatas = context.Request["templatedatas"].ToString();
            context.Session["templatedatas"] = templatedatas;
            
        }
        public void warning(HttpContext context)
        {
            //获取值 生成Session
            string datas = context.Request["datas"].ToString();
            context.Session["warning"] = datas;
            // context.Response.Write("Hello World");
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