using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace PredicTable.Ajax
{
    /// <summary>
    /// WarningDownLoad 的摘要说明
    /// 警报单查询、下载等
    /// </summary>
    public class WarningDownLoad : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            switch (method)
            {
                case "GetWarningList":// 获取警报信息
                     this.GetWarningList(context);
                    break;
                case "Delete":// 删除警报
                    this.Delete(context);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 获取警报单列表
        /// </summary>
        /// <param name="context"></param>
        private void GetWarningList(HttpContext context)
        {
            var type = HttpUtility.UrlDecode(context.Request["type"].ToString());

            int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                                                                                //   var id = context.Request["id"].ToString();//第几页
            int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
            int total = 10;//实际一页中有的行数
            StringBuilder sb = new StringBuilder();
            sql_WarningDownLoad warningDownLoad = new sql_WarningDownLoad();
            var dataTable = warningDownLoad.GetWarningList(page, rows, type);
            total = warningDownLoad.GetWarningListCount(type);
            if (total > 0 && dataTable != null && dataTable.Rows.Count > 0)
            {
                //拼接Json
                sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    // sb.Append("{\"docName\":\"" + dataTable.Rows[i]["docName"] + "\"},");
                    sb.Append("{\"docName\":\"" + dataTable.Rows[i]["docName"] + "\",\"sj\":\""
                          + dataTable.Rows[i]["sj"] + "\"},");
                }
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
            else
            {
                sb.Append("{\"total\":\"0\",\"rows\":[]}");
                context.Response.Write(sb.ToString());
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            int cs = 0;//记录查询条数
            int ts = 0;//记录删除条数

            try
            {
                string userid = context.Session["userid"].ToString(); //获取session
                var id = context.Request["id"];
                var type =context.Request["type"].ToString();
                string filename = id;
                string filetype = filename.Split('_')[2];
                string types = filename.Split('_')[0];
                sql_WarningDownLoad warningDownLoad = new sql_WarningDownLoad();
                int checkResult=  warningDownLoad.Checkjb(id, type);//检查数据是否存在（警报信息）
                if (checkResult > 0)
                {
                    cs = cs + checkResult;
                    int deleteResult = warningDownLoad.Deletejb(id, type);//删除从表数据（警报信息）
                    if (deleteResult > 0)
                    {
                        ts = ts + deleteResult;
                        int checkResult_wj = warningDownLoad.Checkjbwj(id, type);//检查是否存在（警报文件）
                        if (checkResult_wj > 0)
                        {
                            cs = cs + checkResult_wj;
                            int deleteResult_wj = warningDownLoad.Deletejbwj(id, type);//删除主表数据（警报文件）
                            if(deleteResult_wj>0)
                            {
                                ts = ts + deleteResult_wj;
                                int checkResult_nr = warningDownLoad.Checkjbnr(id, type);//检查是否存在（警报文件）
                                if (checkResult_nr > 0)
                                {
                                    cs = cs + checkResult_nr;
                                    int deleteResult_nr = warningDownLoad.Deletejbnr(id, type);//删除主表数据（警报文件）
                                    if (deleteResult_nr > 0)
                                    {
                                        ts = ts + deleteResult_nr;
                                        string path = "预报单共享\\duanqi\\";//默认文件保存的路径
                                        string fullName = Path.Combine(HttpRuntime.AppDomainAppPath + path, id);

                                        if (System.IO.File.Exists(fullName))
                                        {
                                            System.IO.File.Delete(fullName);
                                        }
                                        if (filetype == "HB")//如果是海冰需要删除两个表（不确定一定有表格数据）
                                        {
                                            int tb1 = warningDownLoad.GetFilejbtb1(id);
                                            if (tb1 > 0)
                                            {
                                                warningDownLoad.Deletejbtb1(id, type);
                                            }

                                            int tb2 = warningDownLoad.GetFilejbtb2(id);
                                            if (tb2 > 0)
                                            {
                                                warningDownLoad.Deletejbtb2(id, type);
                                            }
                                        }
                                        else
                                        {
                                            //其他类型的警报（风暴潮）表格数据删除
                                            int tb = warningDownLoad.GetFilejbtb(id);
                                            if (tb > 0)
                                            {
                                                warningDownLoad.Deletejbtb(id, type);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (ts == cs)
                {
                    WriteLog.Write("警报删除成功。用户:" + userid+"，名称是："+ filename);
                    context.Response.Write("Success");
                }
                else
                {
                    //Sql_Caozuorizhi.WriteRizhi(context.Session["userid"].ToString(), "", "删除警报失败");
                    WriteLog.Write("警报删除失败。用户:" + userid + "，名称是：" + filename);
                    context.Response.Write("error");
                }

            }
            catch (Exception ex)
            {
                WriteLog.Write("删除警报失败。" + ex.ToString());
                context.Response.Write("error");
            }
        }
        /// <summary>
        /// 删除上传的文件
        /// </summary>
        /// <param name="context"></param>
        private void DeleteFile(HttpContext context)
        {
            var pathfileName = context.Request["id"].ToString();
            try
            {
                string path = "预报单共享\\duanqi\\";//默认文件保存的路径
                string fullName = Path.Combine(HttpRuntime.AppDomainAppPath + path, pathfileName);

                if (System.IO.File.Exists(fullName))
                {
                    System.IO.File.Delete(fullName);
                }
                context.Response.Write("删除成功。");
                WriteLog.Write("警报删除成功。用户:" +context.Session["userid"].ToString()+"删除上传文件："+ fullName);
            }
            catch (Exception e)
            {
                WriteLog.Write("警报删除失败。用户:" + context.Session["userid"].ToString());
                context.Response.Write("删除失败。" + e.Message);

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