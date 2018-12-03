using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.ExportWord;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace PredicTable.Ajax
{
    /// <summary>
    /// UpLoadModelInfo 的摘要说明
    /// 保存中文模板信息到数据库
    /// </summary>
    public class UpLoadModelInfo : IHttpHandler, IReadOnlySessionState
    {
        private Project_CN projectCN = new Project_CN();

        private sql_UpLoadModel uploadModel = new sql_UpLoadModel();
        string templateFile = "";
        string fileName = "";
        string docName = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            templateFile = context.Session["templateFile"].ToString();
            fileName = context.Session["fileName"].ToString();
            docName = context.Session["docName"].ToString();
            switch (method)
            {
                case "SaveCNModel":
                    this.CNModel(context, method);
                    break;
                case "SaveENModelDay":
                    this.ENModelDay(context);
                    break;
                case "SaveENModelYear":
                    this.ENModelYear(context);
                    break;
                case "HeadReporter"://获取主预报员
                    this.BindHeadReporter(context);
                    break;
                case "DeputyReporter"://获取副预报员
                    this.BindDeputyReporter(context);
                    break;
                case "CNForecastInfo": //按照发布单位获取中文数据
                    this.CNForecastInfo(context);
                    break;
                case "ENGetForecastInfo": //获取海洋局北海英文预报信息
                    this.ENGetForecastInfo(context);
                    break;
                case "ENYearGetForecastInfo": //获取海洋局北海英文年预报信息
                    this.ENYearGetForecastInfo(context);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 绑定主预报员
        /// </summary>
        public void BindHeadReporter(HttpContext context)
        {
            DataTable dt = uploadModel.GetHeaderReporter();
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"list\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                        + "\",\"REPORTERNAME\":\"" + dt.Rows[i]["REPORTERNAME"]
                        + "\",\"REPORTERCODE\":\"" + dt.Rows[i]["REPORTERCODE"]
                        + "\"},");
                }

            }
            if (dt.Rows.Count <= 0)
            {
                context.Response.Write(sb.ToString() + "]}");
            }
            else
            {
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        /// <summary>
        /// 绑定副预报员
        /// </summary>
        public void BindDeputyReporter(HttpContext context)
        {
            DataTable dt = uploadModel.GetDeputyRepoter();
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"list\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                        + "\",\"REPORTERNAME\":\"" + dt.Rows[i]["REPORTERNAME"]
                        + "\",\"REPORTERCODE\":\"" + dt.Rows[i]["REPORTERCODE"]
                        + "\"},");
                }

            }
            if (dt.Rows.Count <= 0)
            {
                context.Response.Write(sb.ToString() + "]}");
            }
            else
            {
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        #region 操 作 中 文 模 板


        /**********************操作中文模板***************************************/

        /// <summary>
        /// 获取、处理中文预报单信息
        /// </summary>
        /// <param name="context"></param>
        private void CNModel(HttpContext context, string method)
        {
            string pbtime = HttpUtility.UrlDecode(context.Request["pbtime"].ToString());
            string ybtime = HttpUtility.UrlDecode(context.Request["ybtime"].ToString());
            string ybcontent = HttpUtility.UrlDecode(context.Request["ybcontent"].ToString());
            string headReporter = HttpUtility.UrlDecode(context.Request["headReporter"].ToString());
            string deputyReporter = HttpUtility.UrlDecode(context.Request["deputyReporter"].ToString());
            string sendDepartment = HttpUtility.UrlDecode(context.Request["sendDepartment"].ToString());
            string publishCompanyName = HttpUtility.UrlDecode(context.Request["publishCompanyName"].ToString());
            HandleCNWord(context, method, pbtime, ybtime, ybcontent, headReporter, deputyReporter, sendDepartment, publishCompanyName);
        }

        /// <summary>
        /// 获取数据并返回显示
        /// </summary>
        /// <param name="context"></param>
        private void CNForecastInfo(HttpContext context)
        {
            string pbtime = HttpUtility.UrlDecode(context.Request["publishTime"].ToString());
            string publishCompanyName = HttpUtility.UrlDecode(context.Request["publishCompany"].ToString());
            StringBuilder sb = new StringBuilder();
            //判断是否存在
            DataTable dt = uploadModel.GetCNInfo(pbtime, publishCompanyName);
            sb.Append("{\"list\":[");
            if (dt == null || dt.Rows.Count < 0)
            {
                //若不存在当前发布单位预报单数据
                //按照发布单位查找相对应的发布单位数据
                if (publishCompanyName == "南堡油田")
                {
                    dt = uploadModel.GetCNNBInfo(pbtime, "10day", "NMFC");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                                + "\",\"REPORTNAME\":\"" + dt.Rows[i]["REPORTTITLE"]
                                + "\",\"REPORTCONTENT\":\"" + dt.Rows[i]["REPORTNORTH"]
                                + "\",\"HEADREPORTER\":\"" + dt.Rows[i]["HEADREPORTERNAME"]
                                + "\",\"DEPUTYREPORTER\":\"" + dt.Rows[i]["DEPUTYREPORTERNAME"]
                                + "\"}");
                        }
                    }
                }
                else if (publishCompanyName == "东营环境预报")
                {
                    dt = uploadModel.GetCNInfo(pbtime, "南堡油田");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                                + "\",\"REPORTNAME\":\"" + dt.Rows[i]["REPORTNAME"]
                                + "\",\"REPORTCONTENT\":\"" + dt.Rows[i]["REPORTCONTENT"]
                                + "\",\"HEADREPORTER\":\"" + dt.Rows[i]["HEADREPORTERNAME"]
                                + "\",\"DEPUTYREPORTER\":\"" + dt.Rows[i]["DEPUTYREPORTERNAME"]
                                + "\"}");
                        }
                    }
                }
                else if (publishCompanyName == "胜利油田")
                {
                    dt = uploadModel.GetCNInfo(pbtime, "东营环境预报");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                                + "\",\"REPORTNAME\":\"" + dt.Rows[i]["REPORTNAME"]
                                + "\",\"REPORTCONTENT\":\"" + dt.Rows[i]["REPORTCONTENT"]
                                + "\",\"HEADREPORTER\":\"" + dt.Rows[i]["HEADREPORTERNAME"]
                                + "\",\"DEPUTYREPORTER\":\"" + dt.Rows[i]["DEPUTYREPORTERNAME"]
                                + "\"}");
                        }
                    }
                }
            }
            else
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                            + "\",\"REPORTNAME\":\"" + dt.Rows[i]["REPORTNAME"]
                            + "\",\"REPORTCONTENT\":\"" + dt.Rows[i]["REPORTCONTENT"]
                            + "\",\"HEADREPORTER\":\"" + dt.Rows[i]["HEADREPORTERNAME"]
                            + "\",\"DEPUTYREPORTER\":\"" + dt.Rows[i]["DEPUTYREPORTERNAME"]
                            + "\"}");
                    }

                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Append("]}");
                context.Response.Write(sb.ToString());
            }
            else
            {
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        /// <summary>
        /// 复制word模板
        /// 修改标签信息
        /// 保存字段信息入库
        /// </summary>
        /// <param name="context"></param>
        private void HandleCNWord(HttpContext context, string method, string pbtime, string ybtime, string ybcontent, string headReporter,
            string deputyReporter, string sendDepartment, string publishCompanyName)
        {

            //生成的具有模板样式的新文件 
            HandleWord handleWord = new HandleWord();
            //复制Word，修改标签信息
            int flag = handleWord.CopyCNWord(templateFile, fileName, method, pbtime, ybtime, ybcontent, headReporter, deputyReporter, sendDepartment);
            if (flag == 1)
            {
                DataTable dt = uploadModel.GetCNInfo(pbtime, publishCompanyName);
                sendDepartment = this.GetSendUnit();
                int rult = 0;
                if (dt == null || dt.Rows.Count < 0)
                {
                    //获取主键
                    Project_CN projectCN = new Project_CN();
                    int CNID = uploadModel.GetCNID(projectCN);
                    if (CNID == 1)
                    {

                        //保存字段信息入库
                        DateTime createTime = DateTime.Now;
                        int cnModel = uploadModel.AddCNModel(pbtime, ybtime, ybcontent, headReporter, deputyReporter, createTime, sendDepartment, projectCN, publishCompanyName);
                        //保存word文件流入库
                        rult = handleWord.SaveCNProject(fileName, projectCN, docName, "add");
                        CommonSendUnit commonSendUnit = new CommonSendUnit(docName, sendDepartment);
                        commonSendUnit.resultSendUnit();
                        if (rult == 1)
                        {
                            context.Response.Write("success");
                        }
                        else
                        {
                            context.Response.Write("failed");
                        }
                    }
                }
                else
                {//更新
                    int cnModel = uploadModel.UpdateCNModel(pbtime, ybtime, ybcontent, headReporter, deputyReporter, sendDepartment, projectCN, publishCompanyName);
                    //更新word文件流
                    rult = handleWord.SaveCNProject(fileName, projectCN, docName, "update");
                    CommonSendUnit commonSendUnit = new CommonSendUnit(docName, sendDepartment);
                    commonSendUnit.resultSendUnit();
                    if (rult == 1)
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("failed");
                    }
                }
            }
        }


        #endregion

        #region 操 作 英 文 旬 、 月 模 板


        /**********************操作英文旬、月模板***************************************/

        Project_ENDay projectDay = new Project_ENDay();

        /// <summary>
        /// 
        /// 若为山东预报台，若不存在山东预报台数据，则获取海洋局北海预报中心数据
        /// 若为海洋局北海预报中心，获取海洋局北海预报中心数据
        /// </summary>
        /// <param name="context"></param>
        public void ENGetForecastInfo(HttpContext context)
        {
            DataTable dt = new DataTable();
            string publishTime = HttpUtility.UrlDecode(context.Request["publishTime"].ToString());
            string publishEffect = HttpUtility.UrlDecode(context.Request["publishEffect"].ToString());
            string publishCompany = HttpUtility.UrlDecode(context.Request["publishCompany"].ToString());
            if (publishCompany == "SDMF")
            {
                dt = uploadModel.ENGetForecastInfo(publishTime, publishEffect, "SDMF");
                if (dt == null || dt.Rows.Count < 0)
                {
                    dt = uploadModel.ENGetForecastInfo(publishTime, publishEffect, "NMFC");
                }
            }
            else
            {
                dt = uploadModel.ENGetForecastInfo(publishTime, publishEffect, "NMFC");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"list\":[");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                        + "\",\"REPORTNO\":\"" + dt.Rows[i]["REPORTNO"]
                        + "\",\"PUBLISHTIME\":\"" + dt.Rows[i]["PUBLISHTIME"]
                        + "\",\"REPORTTITLE\":\"" + dt.Rows[i]["REPORTTITLE"]
                        + "\",\"REPORTTIME\":\"" + dt.Rows[i]["REPORTTIME"]
                        + "\",\"REPORTNORTH\":\"" + dt.Rows[i]["REPORTNORTH"]
                        + "\",\"REPORTSOUTH\":\"" + dt.Rows[i]["REPORTSOUTH"]
                        + "\",\"HEADREPORTERNAME\":\"" + dt.Rows[i]["HEADREPORTERNAME"]
                        + "\",\"DEPUTYREPORTERNAME\":\"" + dt.Rows[i]["DEPUTYREPORTERNAME"]
                        + "\",\"SENDDEPARTMENT\":\"" + dt.Rows[i]["SENDDEPARTMENT"]
                        + "\",\"PUBLISHCOMPANY\":\"" + dt.Rows[i]["PUBLISHCOMPANY"]
                        + "\"}");
                }

            }
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Append("]}");
                context.Response.Write(sb.ToString());
            }
            else
            {
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="context"></param>
        public void ENModelDay(HttpContext context)
        {
            string reportNo = context.Request["reportNo"].ToString();
            string publishTime = HttpUtility.UrlDecode(context.Request["publishTime"].ToString());
            string reportTitle = HttpUtility.UrlDecode(context.Request["reportTitle"].ToString());
            string reportTime = HttpUtility.UrlDecode(context.Request["reportTime"].ToString());
            string reportNorth = HttpUtility.UrlDecode(context.Request["reportNorth"].ToString());
            string reportSouth = HttpUtility.UrlDecode(context.Request["reportSouth"].ToString());
            string headReporter = HttpUtility.UrlDecode(context.Request["headReporter"].ToString());
            string deputyReporter = HttpUtility.UrlDecode(context.Request["deputyReporter"].ToString());
            string sendDepartment = HttpUtility.UrlDecode(context.Request["sendDepartment"].ToString());
            string publishEffect = HttpUtility.UrlDecode(context.Request["publishEffect"].ToString());
            string publishCompany = HttpUtility.UrlDecode(context.Request["publishCompany"].ToString());
            projectDay.reportNo = reportNo;
            projectDay.publishTime = publishTime;
            projectDay.reportTitle = reportTitle;
            projectDay.reportTime = reportTime;
            projectDay.reportNorth = reportNorth;
            projectDay.reportSouth = reportSouth;
            projectDay.headReporter = headReporter;
            projectDay.deputyReporter = deputyReporter;
            projectDay.sendDepartment = sendDepartment;
            projectDay.publishEffect = publishEffect;
            projectDay.publishCompany = publishCompany;
            this.HandelENDayWord(context, projectDay);
        }

        /// <summary>
        /// 操作Word
        /// </summary>
        /// <param name="projectDay"></param>
        private void HandelENDayWord(HttpContext context, Project_ENDay projectDay)
        {
            string ENDayType = context.Session["ENDayType"].ToString();
            //生成的具有模板样式的新文件 
            HandleWord handleWord = new HandleWord();
            //复制Word，修改标签信息
            int flag = handleWord.CopyENDayWord(templateFile, fileName, projectDay);
            int rult = 0;
            //保存预报单变量值和Word流
            if (flag == 1)
            {
                DataTable dt = uploadModel.ENGetInfo(projectDay);
                projectDay.sendDepartment = this.GetSendUnit();
                if (dt == null || dt.Rows.Count < 0)
                {
                    //保存字段值
                    int ENDayID = uploadModel.GetENDayID(projectDay);
                    if (ENDayID == 1)
                    {
                        int cnModel = uploadModel.AddENDayModel(projectDay);
                        rult = handleWord.SaveENDayProject(fileName, projectDay, docName, "add", ENDayType);
                        CommonSendUnit commonSendUnit = new CommonSendUnit(docName, projectDay.sendDepartment);
                        commonSendUnit.resultSendUnit();
                        if (rult == 1)
                        {
                            context.Response.Write("success");
                        }
                        else
                        {
                            context.Response.Write("failed");
                        }
                    }
                }
                else//更新
                {
                    int cnModel = uploadModel.UpdateENDayModel(projectDay);
                    rult = handleWord.SaveENDayProject(fileName, projectDay, docName, "update", ENDayType);
                    CommonSendUnit commonSendUnit = new CommonSendUnit(docName, projectDay.sendDepartment);
                    commonSendUnit.resultSendUnit();
                    if (rult == 1)
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("failed");
                    }
                }

            }
        }


        #endregion

        #region 操 作 英 文 年 模 板


        /**********************操作英文年模板***************************************/

        Project_ENYear projectYear = new Project_ENYear();

        /// <summary>
        /// 山东预报台预报
        /// 获取海洋局北海预报中心数据
        /// </summary>
        /// <param name="context"></param>
        public void ENYearGetForecastInfo(HttpContext context)
        {
            DataTable dt = new DataTable();
            string publishTime = HttpUtility.UrlDecode(context.Request["publishTime"].ToString());
            string publishCompany = HttpUtility.UrlDecode(context.Request["publishCompany"].ToString());
            //若为山东预报台预报，获取当天山东预报台预报
            //若不存在，获取国家北海预报中心预报
            if (publishCompany == "SDMF")
            {
                dt = uploadModel.ENYearGetForecastInfoSDMF(publishTime, publishCompany);
                if (dt == null || dt.Rows.Count < 0)
                {
                    dt = uploadModel.ENYearGetForecastInfo(publishTime, publishCompany);
                }
            }
            else
            {
                //若为国家北海预报中心，获取国家北海预报中心预报
                dt = uploadModel.ENYearGetForecastInfo(publishTime, publishCompany);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"list\":[");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                        + "\",\"REPORTNO\":\"" + dt.Rows[i]["REPORTNO"]
                        + "\",\"PUBLISHTIME\":\"" + dt.Rows[i]["PUBLISHTIME"]
                        + "\",\"REPORTTITLE\":\"" + dt.Rows[i]["REPORTTITLE"]
                        + "\",\"STORMSURGE\":\"" + dt.Rows[i]["STORMSURGE"]
                        + "\",\"SEAWAVE\":\"" + dt.Rows[i]["SEAWAVE"]
                        + "\",\"REDTIDE\":\"" + dt.Rows[i]["REDTIDE"]
                        + "\",\"GREENTIDE\":\"" + dt.Rows[i]["GREENTIDE"]
                        + "\",\"TROPICALCYCLONE\":\"" + dt.Rows[i]["TROPICALCYCLONE"]
                        + "\",\"HEADREPORTERNAME\":\"" + dt.Rows[i]["HEADREPORTERNAME"]
                        + "\",\"DEPUTYREPORTERNAME\":\"" + dt.Rows[i]["DEPUTYREPORTERNAME"]
                        + "\",\"SENDDEPARTMENT\":\"" + dt.Rows[i]["SENDDEPARTMENT"]
                        + "\",\"PUBLISHCOMPANY\":\"" + dt.Rows[i]["PUBLISHCOMPANY"]
                        + "\"}");
                }

            }
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Append("]}");
                context.Response.Write(sb.ToString());
            }
            else
            {
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        /// <summary>
        /// 读取英文年模板属性值
        /// </summary>
        /// <param name="context"></param>
        public void ENModelYear(HttpContext context)
        {
            string reportNo = context.Request["reportNo"].ToString();
            string publishTime = HttpUtility.UrlDecode(context.Request["publishTime"].ToString());
            string reportTitle = HttpUtility.UrlDecode(context.Request["reportTitle"].ToString());
            string stormSurge = HttpUtility.UrlDecode(context.Request["stormSurge"].ToString());
            string seaWave = HttpUtility.UrlDecode(context.Request["seaWave"].ToString());
            string redTide = HttpUtility.UrlDecode(context.Request["redTide"].ToString());
            string greebTide = HttpUtility.UrlDecode(context.Request["greebTide"].ToString());
            string tropicalCyclone = HttpUtility.UrlDecode(context.Request["tropicalCyclone"].ToString());
            string headReporter = HttpUtility.UrlDecode(context.Request["headReporter"].ToString());
            string deputyReporter = HttpUtility.UrlDecode(context.Request["deputyReporter"].ToString());
            string sendDepartment = HttpUtility.UrlDecode(context.Request["sendDepartment"].ToString());
            string publishCompany = HttpUtility.UrlDecode(context.Request["publishCompany"].ToString());
            projectYear.reportNo = reportNo;
            projectYear.publishTime = publishTime;
            projectYear.reportTitle = reportTitle;
            projectYear.stormSurge = stormSurge;
            projectYear.seaWave = seaWave;
            projectYear.redTide = redTide;
            projectYear.greebTide = greebTide;
            projectYear.tropicalCyclone = tropicalCyclone;
            projectYear.headReporter = headReporter;
            projectYear.deputyReporter = deputyReporter;
            projectYear.sendDepartment = sendDepartment;
            projectYear.publishCompany = publishCompany;
            this.HandelENYearWord(context, projectYear);
        }
        /// <summary>
        /// 出入标签值
        /// 处理数据及流文件入库
        /// </summary>
        /// <param name="projectYear"></param>
        private void HandelENYearWord(HttpContext context, Project_ENYear projectYear)
        {
            //生成的具有模板样式的新文件 
            HandleWord handleWord = new HandleWord();
            //复制Word，修改标签信息
            int flag = handleWord.CopyENYearWord(templateFile, fileName, projectYear);
            //保存预报单变量值和Word流
            if (flag == 1)
            {
                //查看是否已存在该表单
                DataTable existYearProject = uploadModel.GetYearModel(projectYear);
                projectYear.sendDepartment = this.GetSendUnit();
                int rult = 0;
                if (existYearProject == null)
                {
                    //保存字段值
                    int EYearID = uploadModel.GetENYearID(projectYear);
                    if (EYearID == 1)
                    {
                        int addENYearModel = uploadModel.AddENYearModel(projectYear);
                        rult = handleWord.SaveENYearProject(fileName, projectYear, docName, "add");
                        CommonSendUnit commonSendUnit = new CommonSendUnit(docName, projectYear.sendDepartment);
                        commonSendUnit.resultSendUnit();
                        if (rult == 1)
                        {
                            context.Response.Write("success");
                        }
                        else
                        {
                            context.Response.Write("failed");
                        }
                    }
                }
                else
                {
                    int updateENYearModel = uploadModel.UpdateENYearModel(projectYear);
                    rult = handleWord.SaveENYearProject(fileName, projectYear, docName, "update");
                    CommonSendUnit commonSendUnit = new CommonSendUnit(docName, projectYear.sendDepartment);
                    commonSendUnit.resultSendUnit();
                    if (rult == 1)
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("failed");
                    }
                }
            }
        }


        #endregion

        /// <summary>
        /// 中长期获取发送单位
        /// </summary>
        /// <returns></returns>
        public string GetSendUnit()
        {
            Sql_HT_CONTENTS ht_contents = new Sql_HT_CONTENTS();
            DataTable dt = ht_contents.GetGroupData("中长期18号");
            string strUnit = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strUnit += dt.Rows[i]["USERNAME"].ToString() + ";";
                }
                strUnit = strUnit.Substring(0, strUnit.Length - 1);
            }
            return strUnit;
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