using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// GroupUnit 发送单位分组
    /// </summary>
    public class GroupUnit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string method = context.Request["method"].ToString();
                if (method == "getdata")
                {
                    this.getData(context);
                }
                else if (method == "add")
                {
                    this.addData(context);
                }
                else if (method == "edit")
                {
                    this.editData(context);
                }
                else if (method == "delete")
                {
                    this.deleteData(context);
                }
                else if (method == "getdataall")
                {
                    this.GetDataAll(context);
                }
            }
            catch (Exception error)
            {

            }
        }

        /// <summary>
        /// 获取发送单位分组数据
        /// </summary>
        /// <param name="context"></param>
        private void getData(HttpContext context)
        {
            int page = Convert.ToInt32(context.Request.Form["page"].ToString());//第几页
                                                                                //   var id = context.Request["id"].ToString();//第几页
            int rows = Convert.ToInt32(context.Request.Form["rows"].ToString());//一页多少行 
            int total = 10;//实际一页中有的行数
            StringBuilder sb = new StringBuilder();

            var sql_GroupUnit = new Sql_GroupUnit();
            //
            var dataTable = sql_GroupUnit.GetUnitGroupInfo(page, rows);
            //获取发送单位分组总数
            total = sql_GroupUnit.GetUnitGroupCount();
            //拼接Json
            sb.Append("{\"total\":\"" + total.ToString() + "\",\"rows\":[");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                sb.Append("{\"ID\":\"" + dataTable.Rows[i]["ID"] + "\",\"GROUPNAME\":\"" + dataTable.Rows[i]["GROUPNAME"] + "\",\"CREATETIME\":\"" + dataTable.Rows[i]["CREATETIME"] +
                    "\",\"UNITNAME\":\"" + dataTable.Rows[i]["UNITNAME"] + "\"},");
            }
            context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
        }

        /// <summary>
        /// 添加发送单位分组信息
        /// </summary>
        /// <param name="context"></param>
        private void addData(HttpContext context)
        {
            var unitGroupName = context.Request.Form["unitGroupName"].ToString();
            var grouplist = context.Request.Form["grouplist"].ToString();
            var groupUnitModel = new GroupUnitModel();
            groupUnitModel.GROUPNAME = unitGroupName;
            groupUnitModel.UNITNAME = grouplist;
            groupUnitModel.CREATETIME = DateTime.Now;
            var sql_GroupUnit = new Sql_GroupUnit();
            var result = sql_GroupUnit.AddUnitGroupData(groupUnitModel);
            if (result > 0)
                context.Response.Write("Success");
            else
                context.Response.Write("error");
        }

        /// <summary>
        /// 修改发送单位分组信息
        /// </summary>
        /// <param name="context"></param>
        private void editData(HttpContext context)
        {
            try
            {
                var id = context.Request.Form["id"].ToString();
                var unitGroupName = context.Request.Form["unitGroupName"].ToString();
                var grouplist = context.Request.Form["grouplist"].ToString();
                var groupUnitModel = new GroupUnitModel();
                groupUnitModel.GROUPNAME = unitGroupName;
                groupUnitModel.UNITNAME = grouplist;
                groupUnitModel.ID = id;
                var sql_GroupUnit = new Sql_GroupUnit();
                var result = sql_GroupUnit.EditUnitGroupData(groupUnitModel);
                if (result > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改发布单位分组信息失败。" + ex.ToString());
                context.Response.Write("error");
            }
        }

        /// <summary>
        /// 删除发送单位分组信息
        /// </summary>
        /// <param name="context"></param>
        private void deleteData(HttpContext context)
        {
            try
            {
                var id = context.Request["id"].ToString();
                var sql_GroupUnit = new Sql_GroupUnit();
                var result = sql_GroupUnit.DelUnitGroupData(Convert.ToInt32(id));
                if (result > 0)
                    context.Response.Write("Success");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                WriteLog.Write("删除发布单位分组信息失败。" + ex.ToString());
                context.Response.Write("error");
            }
        }


        private void GetDataAll(HttpContext context) { }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}