using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// Contents 的摘要说明
    /// 联系人
    /// </summary>
    public class Contents : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            switch (method)
            {
                case "GetContents":// 获取联系人信息
                    this.GetContents(context);
                    break;
                case "SubmitContents"://提交联系人信息
                    this.SubmitContents(context);
                    break;
                case "EditContents"://修改联系人信息
                    this.EditContents(context);
                    break;
                case "DeleteContents"://删除联系人信息
                    this.DeleteContents(context);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="context"></param>
        private void GetContents(HttpContext context)
        {
            int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                                                                                //   var id = context.Request["id"].ToString();//第几页
            int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
            int total = 10;//实际一页中有的行数
            StringBuilder sb = new StringBuilder();

            sql_Contents sql_contents = new sql_Contents();
            //
            var dataTable = sql_contents.GetContents(page, rows);
            //获取发送单位总数
            total = sql_contents.GetContentsCount();
            if (total > 0)
            {
                //拼接Json
                sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sb.Append("{\"ID\":\"" + dataTable.Rows[i]["ID"] + "\",\"CONTENTSNAME\":\"" + dataTable.Rows[i]["CONTENTSNAME"] + "\",\"CONTENTSCODE\":\"" + dataTable.Rows[i]["CONTENTSCODE"] +
                        "\"},");
                }
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        /// <summary>
        /// 提交联系人
        /// </summary>
        /// <param name="context"></param>
        private void SubmitContents(HttpContext context)
        {
            try
            {
                var contentsName = HttpUtility.UrlDecode(context.Request["contentsName"].ToString());
                var contentsCode = HttpUtility.UrlDecode(context.Request["contentsCode"].ToString());
                sql_Contents sql_contents = new sql_Contents();
                int submitResult = sql_contents.SubmitContents(contentsName, contentsCode);
                if (submitResult > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加联系人信息失败。" + ex.ToString());
                context.Response.Write("error");
            }
        }

        /// <summary>
        /// 修改联系人信息
        /// </summary>
        /// <param name="context"></param>
        private void EditContents(HttpContext context)
        {
            try
            {
                var id = HttpUtility.UrlDecode(context.Request["id"].ToString());
                var contentsName = HttpUtility.UrlDecode(context.Request["contentsName"].ToString());
                var contentsCode = HttpUtility.UrlDecode(context.Request["contentsCode"].ToString());
                sql_Contents sql_contents = new sql_Contents();
                int editReuslt = sql_contents.EditContents(id, contentsName, contentsCode);
                if (editReuslt > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改联系人信息失败。" + ex.ToString());
                context.Response.Write("error");
            }
        }
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="context"></param>
        private void DeleteContents(HttpContext context)
        {
            try
            {
                var id = Convert.ToInt32(HttpUtility.UrlDecode(context.Request["id"]));
                sql_Contents sql_contents = new sql_Contents();
                int deleteResult = sql_contents.DeleteContents(id);
                if (deleteResult > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除联系人信息失败。" + ex.ToString());
                context.Response.Write("error");
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