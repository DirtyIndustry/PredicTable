//add by yy in 2018-04-17
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PredicTable.Model;
using System.Text;
using PredicTable.Dal;

namespace PredicTable.Ajax
{
    /// <summary>
    /// ChangeRecord 的摘要说明
    /// </summary>
    public class ChangeRecord : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            switch (method)
            {
                case "GetChangeRecord"://获取
                    this.GetChangeRecord(context); break;
                case "SubmitChange"://提交
                    this.SubmitChange(context); break;
                case "EditChange"://修改
                    this.EditChange(context); break;
                case "DeleteChange"://删除
                    this.DeleteChange(context); break;
                default: break;
            }
        }
        /// <summary>
        /// 获取变更记录
        /// </summary>
        /// <param name="context"></param>
        public void GetChangeRecord(HttpContext context)
        {
            int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
            int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页中多少行
            int total = 10;//实际一页中的行数
            StringBuilder sb = new StringBuilder();
            Sql_ChangeRecord sql_Change = new Sql_ChangeRecord();
            var dataTable = sql_Change.GetChangeRecord(page, rows);
            total = sql_Change.GetChangeRecordCount();
            if (total > 0)
            { //拼接Json
                sb.Append("{\"total\":\""+total.ToString()+"\",\"rows\":[");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sb.Append("{\"ID\":\"" + dataTable.Rows[i]["ID"] + "\",\"ChangeContent\":\"" + 
                        dataTable.Rows[i]["CHANGECONTENT"] + "\",\"ChangePerson\":\"" + 
                        dataTable.Rows[i]["CHANGEPERSON"] + "\",\"ChangeDate\":\"" + 
                        dataTable.Rows[i]["CJSJ"]+ "\"},");
                }
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
            else
            {
                context.Response.Write("{\"total\":\"0\",\"rows\":[]}");
            }
        }   

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="context"></param>
        public void SubmitChange(HttpContext context)
        {
            try
            {
                ChangeRecordModel ChangeModel = new ChangeRecordModel();
                var ChangeContent = HttpUtility.UrlDecode(context.Request["ChangeRecordContent"].ToString());
                var ChangePerson = HttpUtility.UrlDecode(context.Request["ChangeRecordPerson"].ToString());
                DateTime NowTime = DateTime.Now;

                ChangeModel.ChangeContent = ChangeContent;
                ChangeModel.ChangePerson = ChangePerson;
                ChangeModel.ChangeTime = NowTime;

                Sql_ChangeRecord sql_Change = new Sql_ChangeRecord();
                int SubmitRes = sql_Change.SubmitChangeRecord(ChangeModel);
                if (SubmitRes > 0)
                {
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("error");
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("添加变更记录失败！"+ex.Message+"\r\n"+ex.StackTrace);
                context.Response.Write("error");
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="context"></param>
        public void EditChange(HttpContext context)
        {
            try
            {
                ChangeRecordModel changeModel = new ChangeRecordModel();
                var id = HttpUtility.UrlDecode(context.Request["ID"].ToString());
                var ChangeContext = HttpUtility.UrlDecode(context.Request["ChangeRecordContent"].ToString());
                var ChangePerson = HttpUtility.UrlDecode(context.Request["ChangeRecordPerson"].ToString());

                changeModel.ID = id;
                changeModel.ChangeContent = ChangeContext;
                changeModel.ChangePerson = ChangePerson;

                Sql_ChangeRecord sql_Change = new Sql_ChangeRecord();
                int editRes = sql_Change.EditChangeRecord(changeModel);
                if (editRes > 0)
                {
                    context.Response.Write("success");
                }
                else {
                    context.Response.Write("error");
                }

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改变更信息失败！" + ex.Message);
                context.Response.Write("error");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        public void DeleteChange(HttpContext context)
        {
            try {
                int id = Convert.ToInt32(HttpUtility.UrlDecode(context.Request["id"]));
                Sql_ChangeRecord change_Model = new Sql_ChangeRecord();
                int deleteRes = change_Model.DeleteChangeRecord(id);
                if (deleteRes > 0)
                {
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("errpr");
                }
            } catch (Exception ex) {
                WriteLog.Write("删除变更信息失败。" + ex.ToString());
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