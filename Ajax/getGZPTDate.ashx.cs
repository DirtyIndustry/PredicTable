using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using ICSharpCode.SharpZipLib;

using ICSharpCode.SharpZipLib.Checksums;
using System.Diagnostics;
using System.Web.SessionState;

namespace PredicTable.Ajax
{
    /// <summary>
    /// getGZPTDate 的摘要说明
    /// </summary>
    public class getGZPTDate : IHttpHandler, IRequiresSessionState
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
                    int total = 2;//实际一页中有的行数
                    StringBuilder sb = new StringBuilder();
                    //rows 是数据集合
                    KJ_GongZhongPingTai gzpt = new KJ_GongZhongPingTai();
                    KJ_GONGZHONGPINGTAIFUJIAN gzptfj = new KJ_GONGZHONGPINGTAIFUJIAN();
                    Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
                    gzpt.STATE = "";
                    DataTable bumentable = (DataTable)sql_gzpt.GetTableQuerypage(page, rows, gzptfj);
                    total = sql_gzpt.GeTableQueryCount(gzpt);
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

                else if (method == "delfujian")
                {
                    var id = context.Request["id"].ToString();
                    Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
                    int nums = sql_gzpt.Del_FuJian(id);
                    if (nums <= 0)
                    {
                        context.Response.Write("Error");
                    }
                    else
                    {
                        context.Response.Write("Success");
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
                        else {
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
        //获取短信组数据列表
        public void getGROUPdata(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
           
            StringBuilder sb = new StringBuilder();
            //sb.Clear();
            Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
            DataTable dt = (DataTable)sql_gzpt.get_GROUPdata ();
            sb.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("\""+dt.Rows[i]["SMSGROUP"]+"\"" + ",");
                }

            }
            if (dt.Rows.Count <= 0)
            {
                context.Response.Write(sb.ToString() + "]");
            }
            else
            {
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]");
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