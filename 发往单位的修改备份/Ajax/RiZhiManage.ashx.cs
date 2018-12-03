using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.SessionState;
 

namespace PredicTable.Ajax
{
    /// <summary>
    /// RiZhiManage 的摘要说明
    /// </summary>
    public class RiZhiManage : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (context.Session["userid"] != null)
                {
                    string  userids = context.Session["userid"].ToString();
                    string id = context.Request["id"].ToString();
                if (id == "getdata")
                {
                    int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                    int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
                    int total = 2;//实际一页中有的行数
                    StringBuilder sb = new StringBuilder();
                    //rows 是数据集合
                    KJ_Caozuorizhi rizhi = new KJ_Caozuorizhi();
                    rizhi.Caozuo = "";
                    rizhi.Daima = "";
                    rizhi.Zhanghao = userids;
                    Sql_Caozuorizhi bumen = new Sql_Caozuorizhi();
                    DataTable bumentable = (DataTable)bumen.GetRizhipage(page, rows, rizhi);
                    //   DataTable k1 = (DataTable)s1.testdata();
                    total = bumen.GetrizhiCount(rizhi);
                    sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
                    for (int i = 0; i < bumentable.Rows.Count; i++)
                    {
                        sb.Append("{\"yhzh\":\""
                            + bumentable.Rows[i]["ZHANGHAO"] + "\",\"yhmc\":\""
                            + bumentable.Rows[i]["MINGCHENG"] + "\",\"czsj\":\""
                            + bumentable.Rows[i]["SHIJIAN"] + "\", \"zldm\":\""
                            + bumentable.Rows[i]["DAIMA"] + "\", \"xxsm\":\""
                            + bumentable.Rows[i]["CAOZUO"] + "\" },");
                    }
                    context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
                }
                else if (id == "getbywhere")
                {
                    int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                    int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
                    string firstdata = context.Request.Form["firstdata"].ToString();//开始时间
                    string enddata = context.Request.Form["enddata"].ToString();//结束时间
                    string type = context.Request.Form["type"].ToString();//类型
                  
                    int total = 2;//实际一页中有的行数
                    StringBuilder sb = new StringBuilder();
                    //rows 是数据集合
                    Sql_Caozuorizhi bumen = new Sql_Caozuorizhi();
                    KJ_Caozuorizhi rizhi = new KJ_Caozuorizhi();

                    if (firstdata == "" || enddata == "")
                    {
                        rizhi.Caozuo = "";
                    }
                    else
                    {
                        rizhi.Caozuo = firstdata + "," + enddata;
                    }
                    rizhi.Zhanghao = userids;
                    rizhi.Daima = type;
                    DataTable bumentable = (DataTable)bumen.GetRizhipage(page, rows, rizhi);
                    //   DataTable k1 = (DataTable)s1.testdata();
                    total = bumen.GetrizhiCount(rizhi);
                    sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");

                    for (int i = 0; i < bumentable.Rows.Count; i++)
                    {
                        sb.Append("{\"yhzh\":\""
                            + bumentable.Rows[i]["ZHANGHAO"] + "\",\"yhmc\":\""
                            + bumentable.Rows[i]["MINGCHENG"] + "\",\"czsj\":\""
                            + bumentable.Rows[i]["SHIJIAN"] + "\", \"zldm\":\""
                            + bumentable.Rows[i]["DAIMA"] + "\", \"xxsm\":\""
                            + bumentable.Rows[i]["CAOZUO"] + "\" },");
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
            }
            }
            catch (Exception ex)
            {
                //   Sql_Caozuorizhi.WriteRizhi(context.Session["userid"].ToString(), "Error", "日志管理出错！");
                WriteLog.Write("日志管理出错！  " + ex.ToString());
                //HttpContext.Current.Response.Write("<script>top.location.href='../admin/main.aspx';</script>");
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