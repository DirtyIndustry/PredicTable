using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// GetRole 的摘要说明
    /// </summary>
    public class GetRole : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string method = context.Request["method"].ToString();
                switch (method)
                {
                    case "getall":
                        getall(context);
                        break;
                    case "editsys":
                        editsys(context);
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
               // Sql_Caozuorizhi.WriteRizhi(context.Session["userid"].ToString(), "Error", "系统管理出错！");
                WriteLog.Write("系统管理出错！  " + ex.ToString());
               // HttpContext.Current.Response.Write("<script>top.location.href='../admin/main.aspx';</script>");
            }
        }

        /// <summary>
        ///修改系统配置
        /// </summary>
        /// <param name="context"></param>
        public void editsys(HttpContext context)
        {
            Sql_Keyvalue kv = new Sql_Keyvalue();
            KJ_Keyvalue keyvalue = new KJ_Keyvalue();
            var keys = context.Request.Form["keys"].ToString();
            var values = context.Request.Form["values"].ToString();
            var key = keys.Split(',');
            var value = values.Split(',');
            for (int i = 0; i < key.Length; i++)
            {
                keyvalue.Key = key[i];
                keyvalue.Value = value[i];
                if (addkey(kv, keyvalue))//表中包含KEY数据，可以进行下一步修改操作
                {
                    if (kv.EditByKey(keyvalue) <= 0)
                    {
                     //   Sql_Caozuorizhi.WriteRizhi(context.Session["userid"].ToString(), "edit_system", "修改系统消息失败");
                        context.Response.Write(key[i] + " 修改失败");
                        return;
                    }
                }
                else
                {
                   // Sql_Caozuorizhi.WriteRizhi(context.Session["userid"].ToString(), "edit_system", "修改系统消息失败");
                    context.Response.Write(key[i] + " 修改失败");
                    return;
                }
            }
           // Sql_Caozuorizhi.WriteRizhi(context.Session["userid"].ToString(), "edit_system", "修改系统消息成功");
            context.Response.Write("Success");
        }

        /// <summary>
        /// 如果Keyvalue表中没有这个key 则添加
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool addkey(Sql_Keyvalue kv, KJ_Keyvalue keyvalue)
        {
            int num = kv.KeyValuecount(keyvalue);
            if (num <= 0)
            {
                if (kv.AddKeyValue(keyvalue) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        ///获取系统配置
        /// </summary>
        /// <param name="context"></param>
        public void getall(HttpContext context)
        {
            Sql_Keyvalue kv = new Sql_Keyvalue();
            DataTable dt = kv.GetAll();
            if (dt.Rows.Count > 0)
            {
                //  context.Response.Write("Success");
                //StringBuilder sb = new StringBuilder();
                //sb.Append("[");
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    sb.Append("{ \"" + dt.Rows[i]["KEY"] + "\":\"" + dt.Rows[i]["VALUE"] + "\"},");
                //}
                //context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]");
                string sb="{";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb=sb+"\"" + dt.Rows[i]["KEY"] + "\":\"" + dt.Rows[i]["VALUE"] + "\",";
                }
                sb = sb.Remove(sb.Length - 1, 1)+"}";
                context.Response.Write(sb);
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