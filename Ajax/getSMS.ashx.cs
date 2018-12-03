using PredicTable.Commen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// getSMS 的摘要说明
    /// </summary>
    public class getSMS : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string method = context.Request["method"].ToString();
                if (method == "getdata")
                {
                    int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                    int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
                    int total = 10;//实际一页中有的行数
                    StringBuilder sb = new StringBuilder();
                    KJ_GongZhongPingTai gzpt = new KJ_GongZhongPingTai();
                    KJ_GONGZHONGPINGTAIFUJIAN gzptfj = new KJ_GONGZHONGPINGTAIFUJIAN();
                    Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
                    DataTable bumentable = (DataTable)sql_gzpt.GetTableQuerypage(page, rows, gzptfj,"短信");
                    total = sql_gzpt.GeTableQueryCount(gzpt, "短信");
                    sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                    for (int i = 0; i < bumentable.Rows.Count; i++)
                    {
                        sb.Append("{\"id\":\"" + bumentable.Rows[i]["ID"] + "\",\"time\":\""
                        + bumentable.Rows[i]["TIME"] + "\",\"userid\":\""
                        + bumentable.Rows[i]["USERID"] + "\",\"doctype\":\""
                        + bumentable.Rows[i]["DOCTYPE"] + "\",\"DXGROUP\":\""
                        + bumentable.Rows[i]["DXGROUP"] + "\",\"state\":\""
                        + bumentable.Rows[i]["STATE"] + "\",\"filemane\":\""
                         + bumentable.Rows[i]["FILENAME"] + "\",\"mestype\":\""
                         + bumentable.Rows[i]["MESTYPE"] + "\",\"vid\":\""
                         + bumentable.Rows[i]["VID"] + "\",\"type\":\""
                        + bumentable.Rows[i]["TYPE"] + "\"},");
                    }
                    if (bumentable.Rows.Count <= 0)
                    {
                        context.Response.Write(sb.ToString() + "]}");
                    }
                    else
                    {
                        context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
                    }
                }
                else if (method == "delall")
                {
                    var id = context.Request["id"].ToString();
                    Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
                    int nums = sql_gzpt.Del_All(id);
                    if (nums <= 0)
                    {
                        context.Response.Write("Error");
                    }
                    else
                    {
                        int count = sql_gzpt.GeTableQueryCountfb(id);
                        if (count > 0)
                        {
                            int nums1 = sql_gzpt.Del_AllFuJian(id);
                            if (nums1 <= 0)
                            {
                                context.Response.Write("Error");
                            }
                            else
                            {
                                context.Response.Write("Success");
                            }
                        }
                        else
                        {
                            context.Response.Write("Success");
                        }

                    }
                }
                else if (method == "getgroup")
                {
                    getGROUPdata(context);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取表单列表出错！  " + ex.ToString());
            }
        }
        public void getGROUPdata(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            StringBuilder sb = new StringBuilder();
            //sb.Clear();
            Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
            DataTable dt = (DataTable)sql_gzpt.GetGroupData();
            DataTable dtBH = null;
            DataTable dtSD = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                dtBH = dt.Clone();
                var dtBHQuery = dt.AsEnumerable().Where<DataRow>(a => a["tblsmscontactsgroupid"].ToString() == "2");
                foreach (DataRow item in dtBHQuery)
                {
                    dtBH.Rows.Add(item.ItemArray);
                }
                dtSD = dt.Clone();
                var dtSDQuery = dt.AsEnumerable().Where<DataRow>(a => a["tblsmscontactsgroupid"].ToString() == "1");
                foreach (DataRow item in dtSDQuery)
                {
                    dtSD.Rows.Add(item.ItemArray);
                }
                //var result = JsonMore.Serialize(dt);
                //sb_str.Append(",{ \"type\":\"t51\",\"children\":");
                //sb_str.Append(result);
                //sb_str.Append("}");
                //sb_str.ToString();
            }

            var result = "\"dtBH\":"; //
            if (dtBH != null && dtBH.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtBH);
            }
            else
            {
                result += "[{}]";
            }
            result += ",\"dtSD\":"; //
            if (dtSD != null && dtSD.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtSD);
            }
            else
            {
                result += "[{}]";
            }
            sb.Append("{");
            sb.Append(result);
            sb.Append("}");
            context.Response.Write(sb.ToString());
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