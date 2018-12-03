using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System.Web.SessionState;
using PredicTable.Dal;
using PredicTable.Model;
using PredicTable.Commen;

namespace PredicTable.Ajax
{
    /// <summary>
    /// CheckTideData 的摘要说明
    /// </summary>
    public class CheckTideData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            switch (method)
            {
                case "getdata":// 获取全部信息
                    this.getalldata(context);
                    break;
                case "getbywhere":// 按时间查询
                    this.getwheredata(context);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        public void getalldata(HttpContext context)
        {
            int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                                                                                //   var id = context.Request["id"].ToString();//第几页
            int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
         
            int total = 0;//实际一页中有的行数
            StringBuilder sb = new StringBuilder();

            Sql_HT_YB_Tide sql_tide = new Sql_HT_YB_Tide();
            //
            var dataTable = sql_tide.GetAllTide(page, rows);
            //获取总数
            total = sql_tide.GetCount();
            if (total > 0)
            {
                //拼接Json
                sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sb.Append("{\"STATION\":\"" + dataTable.Rows[i]["STATION"] +
                        "\",\"PREDICTIONDATE\":\"" + dataTable.Rows[i]["PREDICTIONDATE"] +
                        "\",\"FSTHIGHWIDETIME\":\"" + dataTable.Rows[i]["FSTHIGHWIDETIME"] +
                        "\",\"FSTHIGHWIDEHEIGHT\":\"" + dataTable.Rows[i]["FSTHIGHWIDEHEIGHT"] +
                         "\",\"FSTLOWWIDETIME\":\"" + dataTable.Rows[i]["FSTLOWWIDETIME"] +
                        "\",\"FSTLOWWIDEHEIGHT\":\"" + dataTable.Rows[i]["FSTLOWWIDEHEIGHT"] +
                        "\",\"SCDHIGHWIDETIME\":\"" + dataTable.Rows[i]["SCDHIGHWIDETIME"] +
                         "\",\"SCDHIGHWIDEHEIGHT\":\"" + dataTable.Rows[i]["SCDHIGHWIDEHEIGHT"] +
                        "\",\"SCDLOWWIDETIME\":\"" + dataTable.Rows[i]["SCDLOWWIDETIME"] +
                        "\",\"SCDLOWWIDEHEIGHT\":\"" + dataTable.Rows[i]["SCDLOWWIDEHEIGHT"] + "\"},");
                }
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }


        /// <summary>
        ///查询
        /// </summary>
        public void getwheredata(HttpContext context)
        {
            int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                                                                                //   var id = context.Request["id"].ToString();//第几页
            int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
            string firstdata = context.Request.Form["firstdata"].ToString();//开始时间
            string enddata = context.Request.Form["enddata"].ToString();//结束时间
            int total = 0;//实际一页中有的行数
            StringBuilder sb = new StringBuilder();

            Sql_HT_YB_Tide sql_tide = new Sql_HT_YB_Tide();
            HT_YB_Tide tide = new HT_YB_Tide();

            if (firstdata == "" || enddata == "")
            {
                tide.PREDICTIONDATE = "";
            }
            else
            {
                tide.PREDICTIONDATE = firstdata + "," + enddata;
            }

          
            DataTable dataTable = (DataTable)sql_tide.GetTableQuerypage(page, rows, tide);
            //获取总数
            total = sql_tide.GetCount(tide);
            if (total > 0)
            {
                //拼接Json
                sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sb.Append("{\"STATION\":\"" + dataTable.Rows[i]["STATION"] +
                        "\",\"PREDICTIONDATE\":\"" + dataTable.Rows[i]["PREDICTIONDATE"] +
                        "\",\"FSTHIGHWIDETIME\":\"" + dataTable.Rows[i]["FSTHIGHWIDETIME"] +
                        "\",\"FSTHIGHWIDEHEIGHT\":\"" + dataTable.Rows[i]["FSTHIGHWIDEHEIGHT"] +
                         "\",\"FSTLOWWIDETIME\":\"" + dataTable.Rows[i]["FSTLOWWIDETIME"] +
                        "\",\"FSTLOWWIDEHEIGHT\":\"" + dataTable.Rows[i]["FSTLOWWIDEHEIGHT"] +
                        "\",\"SCDHIGHWIDETIME\":\"" + dataTable.Rows[i]["SCDHIGHWIDETIME"] +
                         "\",\"SCDHIGHWIDEHEIGHT\":\"" + dataTable.Rows[i]["SCDHIGHWIDEHEIGHT"] +
                        "\",\"SCDLOWWIDETIME\":\"" + dataTable.Rows[i]["SCDLOWWIDETIME"] +
                        "\",\"SCDLOWWIDEHEIGHT\":\"" + dataTable.Rows[i]["SCDLOWWIDEHEIGHT"] + "\"},");
                }
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
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