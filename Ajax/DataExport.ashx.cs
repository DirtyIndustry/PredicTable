using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Text;
using System.Data;
using PredicTable.Model;

namespace PredicTable.Ajax
{
    /// <summary>
    /// DataExport 的摘要说明
    /// </summary>
    public class DataExport : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            switch (method)
            {
                case "GetRes":getSelectedData(context);break;//获取选择的条件下的数据
                default:break;
            }
        }

        private void getSelectedData(HttpContext context) {
            string ybsd = context.Request["ybsd"].ToString();//预报时段（上下午）
            string sjlx = context.Request["sjlx"].ToString();//数据类型（订正前后）
            string ybhq = context.Request["ybhq"].ToString();//预报海区
            string ybsx = context.Request["ybsx"].ToString();//预报时效（24*48*72）
            string wjgs = context.Request["wjgs"].ToString();//文件格式（txt/excel）
            string ybys = context.Request["ybys"].ToString();//预报要素(浪高/浪向)
            DateTime tbsjs = DateTime.Parse(context.Request["tbsjs"]);//开始时间
            DateTime tbsje = DateTime.Parse(context.Request["tbsje"]);//结束时间
            string[] ybysArr = ybys.Split('/');

            ExportDataModel ExDataModel = new ExportDataModel();
            ExDataModel.SJLX = sjlx;
            ExDataModel.YBHQ = ybhq;
            ExDataModel.YBSX = ybsx;
            ExDataModel.WJGS = wjgs;
            ExDataModel.YBYSARR = ybysArr;
            ExDataModel.TBSJS = tbsjs;
            ExDataModel.TBSJE = tbsje;

            if (ybsd == "上午")
            {
                getAMSelectedData(ExDataModel);
            }
            else if (ybsd == "下午")
            {
                getPMSelectedData(ExDataModel);
            }

        }
        /// <summary>
        /// 上午数据导出
        /// </summary>
        /// <param name="ExDataModel"></param>
        private void getAMSelectedData(ExportDataModel ExDataModel)
        {
            //string[] ybysArr = ExDataModel.YBYSARR;
            string ybhq = ExDataModel.YBHQ;
            switch (ybhq)
            {   
                case "渤海":GetTb01Data(ExDataModel);break;
                case "黄河海港": getTb02Data(ExDataModel);break; 
                case "龙口港": getTb03Data(ExDataModel); break;
                case "黄海北部":getTb04Data(ExDataModel);break;
                case "刁口海域": getTb05Data(ExDataModel); break;
                case "黄河口海域": getTb06Data(ExDataModel); break;
                case "广利港海域": getTb07Data(ExDataModel); break;
                case "东营港海域": getTb08Data(ExDataModel); break;
                case "新户海域": getTb09Data(ExDataModel); break;
                case "埕口海域": getTb10Data(ExDataModel); break;
                case "青岛港": getTb11Data(ExDataModel); break;
                case "潍坊港": getTb12Data(ExDataModel); break;
                case "营口港": getTb13Data(ExDataModel); break;
                case "黄海中部": getTb14Data(ExDataModel); break;
                case "黄海南部": getTb15Data(ExDataModel); break;
                case "潍坊近海": getTb16Data(ExDataModel); break;
                case "海阳": getTb17Data(ExDataModel); break;
            }
        }
        /// <summary>
        /// 下午数据导出
        /// </summary>
        /// <param name="ExDataModel"></param>
        private void getPMSelectedData(ExportDataModel ExDataModel)
        {
        }
        #region 上午各海域数据导出
        /// <summary>
        /// 上午渤海
        /// </summary>
        /// <param name="ExDataModel"></param>
        private void GetTb01Data(ExportDataModel ExDataModel) { }
        /// <summary>
        /// 上午黄河海港
        /// </summary>
        /// <param name="ExDataModel"></param>
        private void getTb02Data(ExportDataModel ExDataModel) { }
        /// <summary>
        /// 龙口港
        /// </summary>
        /// <param name="ExDataModel"></param>
        private void getTb03Data(ExportDataModel ExDataModel) { }
        private void getTb04Data(ExportDataModel ExDataModel) { }
        private void getTb05Data(ExportDataModel ExDataModel) { }
        private void getTb06Data(ExportDataModel ExDataModel) { }
        private void getTb07Data(ExportDataModel ExDataModel) { }
        private void getTb08Data(ExportDataModel ExDataModel) { }
        private void getTb09Data(ExportDataModel ExDataModel) { }
        private void getTb10Data(ExportDataModel ExDataModel) { }
        private void getTb11Data(ExportDataModel ExDataModel) { }

        private void getTb12Data(ExportDataModel ExDataModel) { }
        private void getTb13Data(ExportDataModel ExDataModel) { }
        private void getTb14Data(ExportDataModel ExDataModel) { }
        private void getTb15Data(ExportDataModel ExDataModel) { }
        private void getTb16Data(ExportDataModel ExDataModel) { }
        private void getTb17Data(ExportDataModel ExDataModel) { }
        #endregion




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}