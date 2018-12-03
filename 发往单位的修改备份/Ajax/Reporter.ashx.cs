using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;


namespace PredicTable.Ajax
{
    /// <summary>
    /// Reporter 的摘要说明
    /// </summary>
    public class Reporter : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain"; 
            string method = context.Request["method"].ToString();
            string reportid = "";

            if(context.Session["type"] != null )
            {
                if(context.Session["type"].ToString() == "fl")
                {
                    reportid = "HLYB";
                }
                else
                {
                    reportid = "FBHBYB";
                }
            }
            else
            {
                return;
            }


            switch (method)
            {
                case "GetReporter":// 获取全部预报员信息
                    this.GetReporter(context);
                    //this.GetReporter(context,reportid);
                    break;
                case "SubmitReporter"://提交预报员信息
                    this.SubmitReporter(context);
                    break;
                case "EditReporter"://修改预报员信息
                    this.EditReporter(context);
                    break;
                case "DeleteReporter"://删除预报员信息
                    this.DeleteReporter(context);
                    break;
                case "GetReporterByQX":// 获取全部预报员信息
                    this.GetReporter(context, reportid);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 获取预报员信息
        /// </summary>
        public void GetReporter(HttpContext context)
        {
            int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                                                                                //   var id = context.Request["id"].ToString();//第几页
            int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
            int total = 10;//实际一页中有的行数
            StringBuilder sb = new StringBuilder();

            sql_Reporter sql_reporter = new sql_Reporter();
            //
            var dataTable = sql_reporter.GetReporter(page, rows);
            //获取发送单位总数
            total = sql_reporter.GetReportCount();
            if (total > 0)
            {
                //拼接Json
                sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //sb.Append("{\"ID\":\"" + dataTable.Rows[i]["ID"] + "\",\"reportername\":\"" + dataTable.Rows[i]["reportername"] + "\",\"reportercode\":\"" + dataTable.Rows[i]["reportercode"] + "\",\"reportertype\":\"" + dataTable.Rows[i]["reportertype"] + "\",\"reportertypeid\":\"" + dataTable.Rows[i]["reportertypeid"] + "\"},");
                    sb.Append("{\"ID\":\"" + dataTable.Rows[i]["ID"] + "\",\"reportername\":\"" + dataTable.Rows[i]["reportername"] + "\",\"reportercode\":\"" + dataTable.Rows[i]["reportercode"] + "\",\"reportertype\":\"" + dataTable.Rows[i]["reportertype"] + "\",\"reportertel\":\"" + dataTable.Rows[i]["reportertel"] + "\",\"reportertypeid\":\"" + dataTable.Rows[i]["reportertypeid"] + "\"},");
                }
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        public void GetReporter(HttpContext context,string reportid)
        {
            int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                                                                                //   var id = context.Request["id"].ToString();//第几页
            int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
            int total = 10;//实际一页中有的行数
            StringBuilder sb = new StringBuilder();

            sql_Reporter sql_reporter = new sql_Reporter();
            //
            var dataTable = sql_reporter.GetReporter(page, rows,reportid);
            //获取发送单位总数
            total = sql_reporter.GetReportCount();
            if (total > 0)
            {
                //拼接Json
                sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sb.Append("{\"ID\":\"" + dataTable.Rows[i]["ID"] + "\",\"reportername\":\"" + dataTable.Rows[i]["reportername"] + "\",\"reportercode\":\"" + dataTable.Rows[i]["reportercode"] + "\",\"reportertype\":\"" + dataTable.Rows[i]["reportertype"] + "\",\"reportertel\":\"" + dataTable.Rows[i]["reportertel"] + "\",\"reportertypeid\":\"" + dataTable.Rows[i]["reportertypeid"] + "\"},");
                }
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        /// <summary>
        /// 提交预报员信息
        /// </summary>
        /// <param name="context"></param>
        private void SubmitReporter(HttpContext context)
        {
            try
            {
                ReporterModel reporterModel = new ReporterModel();
                var reporterName = HttpUtility.UrlDecode(context.Request["reporterName"].ToString());
                var reporterCode = HttpUtility.UrlDecode(context.Request["reporterCode"].ToString());
                var reporterType = HttpUtility.UrlDecode(context.Request["reporterType"].ToString());
                var reporterTel = HttpUtility.UrlDecode(context.Request["reporterTel"].ToString());

                reporterModel.ReporterName = reporterName;
                reporterModel.ReporterCode = reporterCode;
                reporterModel.ReporterType = reporterType;
                reporterModel.ReporterTel = reporterTel;

                sql_Reporter sql_reporter = new sql_Reporter();
                int submitResult = sql_reporter.SubmitReporter(reporterModel);
                if (submitResult > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加预报员信息失败。" + ex.ToString());
                context.Response.Write("error");
            }
        }

        /// <summary>
        /// 修改预报员信息
        /// </summary>
        /// <param name="context"></param>
        private void EditReporter(HttpContext context)
        {
            try
            {
                ReporterModel reporterModel = new ReporterModel();
                var id = HttpUtility.UrlDecode(context.Request["id"].ToString());
                var reporterName = HttpUtility.UrlDecode(context.Request["reporterName"].ToString());
                var reporterCode = HttpUtility.UrlDecode(context.Request["reporterCode"].ToString());
                var reporterType = HttpUtility.UrlDecode(context.Request["reporterType"].ToString());
                var reporterTel = HttpUtility.UrlDecode(context.Request["reporterTel"].ToString());

                reporterModel.ID = Convert.ToInt32(id);
                reporterModel.ReporterName = reporterName;
                reporterModel.ReporterCode = reporterCode;
                reporterModel.ReporterTel = reporterTel;
                reporterModel.ReporterType = reporterType;
                sql_Reporter sql_reporter = new sql_Reporter();
                int editReuslt = sql_reporter.EditReporter(reporterModel);
                if (editReuslt > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改预报员信息失败。" + ex.ToString());
                context.Response.Write("error");
            }
        }

        /// <summary>
        /// 删除预报员信息
        /// </summary>
        /// <param name="context"></param>
        private void DeleteReporter(HttpContext context)
        {
            try
            {
                var id = Convert.ToInt32(HttpUtility.UrlDecode(context.Request["id"]));
                sql_Reporter sql_reporter = new sql_Reporter();
                int deleteResult = sql_reporter.DeleteReporter(id);
                if (deleteResult > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除预报员信息失败。" + ex.ToString());
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