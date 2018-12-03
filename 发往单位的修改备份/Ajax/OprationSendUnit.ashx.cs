using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PredicTable.Dal;
using PredicTable.Commen;
using PredicTable.Model;
using System.Text;
using System.Data;

namespace PredicTable.Ajax
{
    /// <summary>
    /// OprationSendUnit 的摘要说明
    /// 操作发布单位
    /// </summary>
    public class OprationSendUnit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            
           
            try
            {
                string method = context.Request["method"].ToString();
                if (method == "getdata")
                {
                    getData(context);
                }
                else if (method == "add")
                {
                    addData(context);

                }
                else if (method == "edit")
                {

                    editData(context);
                }
                else if (method == "delete")
                {
                    deleteData(context);
                }
                else if (method == "getdataall") {
                    GetDataAll(context);
                }else if(method == "getGroupAndUnit")
                {
                    this.getGroupAndUnit(context);
                }
            }
            catch (Exception error)
            {
                WriteLog.Write("操作发布单位信息出错！" + error.ToString());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        void getData(HttpContext context)
        {
            try
            {
                int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                                                                                    //   var id = context.Request["id"].ToString();//第几页
                int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
                int total = 10;//实际一页中有的行数
                StringBuilder sb = new StringBuilder();

                var sql_HT_SENDUNIT = new sql_HT_SENDUNIT();
                //
                var dataTable = sql_HT_SENDUNIT.GetUnitData(page, rows);
                //获取发送单位总数
                total = sql_HT_SENDUNIT.GetUnitCount();
                //拼接Json
                sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sb.Append("{\"ID\":\"" + dataTable.Rows[i]["ID"] + "\",\"SENDUNIT\":\"" + dataTable.Rows[i]["SENDUNIT"] + "\",\"CREATEDATE\":\"" + dataTable.Rows[i]["CREATEDATE"] +
                        "\",\"UPDATEDATE\":\"" + dataTable.Rows[i]["UPDATEDATE"] + "\"},");
                }
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发布单位信息失败。" + ex.ToString());
                context.Response.Write("error");
            }

        }


        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        void addData(HttpContext context)
        {
            try
            {
                var unitName = HttpUtility.UrlDecode(context.Request["unitName"].ToString());
                var unit = new HT_SENDUNIT();
                unit.SENDUNIT = unitName;
                unit.CREATEDATE = DateTime.Now;
                var sql_HT_SENDUNIT = new sql_HT_SENDUNIT();
                var result = sql_HT_SENDUNIT.AddUnitData(unit);
                if (result > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加发布单位信息失败。" + ex.ToString());
                context.Response.Write("error");
            }

        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        void deleteData(HttpContext context)
        {
            try
            {
                var id = context.Request["id"].ToString();
                var sql_HT_SENDUNIT = new sql_HT_SENDUNIT();
                var result = sql_HT_SENDUNIT.DelUnitData(int.Parse(id));
                if (result > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除发布单位信息失败。" + ex.ToString());
                context.Response.Write("error");
            }

        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        void editData(HttpContext context)
        {
            try
            {
                var id = HttpUtility.UrlDecode(context.Request["id"].ToString());
                var unitName = HttpUtility.UrlDecode(context.Request["unitName"].ToString());
                var unit = new HT_SENDUNIT();
                unit.ID = int.Parse(id);
                unit.SENDUNIT = unitName;
                unit.UPDATEDATE = DateTime.Now;
                var sql_HT_SENDUNIT = new sql_HT_SENDUNIT();
                var result = sql_HT_SENDUNIT.EditUnitData(unit);
                if (result > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改发布单位信息失败。" + ex.ToString());
                context.Response.Write("error");
            }

        }

        /// <summary>
        /// 获取全部发送单位
        /// </summary>
        /// <param name="context"></param>
        private void GetDataAll(HttpContext context)
        {
            try
            {
                var sql_HT_SENDUNIT = new sql_HT_SENDUNIT();
                var dataTable = sql_HT_SENDUNIT.GetUnitDataAll();

                var dataJson = JsonMore.Serialize(dataTable);

                context.Response.ContentType = "application/json"; //"text/plain"; 
                context.Response.Write(dataJson);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取发布单位信息失败。" + ex.ToString());
                context.Response.Write("error");
            }
        }

        /// <summary>
        /// 获取分组及发送单位信息
        /// </summary>
        /// <param name="context"></param>
        private void getGroupAndUnit(HttpContext context)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                var sendUnit = new sql_HT_SENDUNIT();
                var dataTable = sendUnit.GetGroupAndUnit();
                var dataJson = JsonMore.Serialize(dataTable);

                context.Response.ContentType = "application/json"; //"text/plain"; 
                context.Response.Write(dataJson);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取信息失败。" + ex.ToString());
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