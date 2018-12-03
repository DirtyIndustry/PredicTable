using PageOffice.POServer;
using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.ExportWord.NewMediumAndLong;
using PredicTable.Model.NewMediumAndLong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace PredicTable.Ajax
{
    /// <summary>
    /// NewMediumAndLong 的摘要说明
    /// </summary>
    public class NewMediumAndLong : IHttpHandler
    {
        private sql_UpLoadModel uploadModel = new sql_UpLoadModel();
        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["method"].ToString();
            switch (method)
            {
                case "SetYear"://保存或更新北海、山东中长期年预报数据
                    this.SetYear(context);
                    break;
                case "SetMonthOrXun"://保存或更新北海、山东中长期月、旬数据
                    this.SetMonthOrXun(context);
                    break;
                case "HeadReporter"://获取主预报员
                    this.BindHeadReporter(context);
                    break;
                case "DeputyReporter"://获取副预报员
                    this.BindDeputyReporter(context);
                    break;
                case "GetYearData":
                    this.GetYearData(context);
                    break;
                case "GetMonthOrDays":
                    this.GetMonthOrDays(context);
                    break;
                default:
                    break;
            }
            
        }

        #region  添加或更新数据


        /// <summary>
        /// 添加中长期年预报
        /// </summary>
        /// <param name="context"></param>
        private void SetYear(HttpContext context)
        {
            try
            {
                ReportYearModel year = new ReportYearModel();
                year.PUBLISHTIME = context.Request.Form["PUBLISHTIME"];
                year.PUBLISHCOMPANY = context.Request.Form["PUBLISHCOMPANY"];
                year.REPORTNO = context.Request.Form["REPORTNO"];
                year.REPORTTITLE = context.Request.Form["REPORTTITLE"];
                year.STORMSURGE = context.Request.Form["STORMSURGE"];
                year.SEAWAVE = context.Request.Form["SEAWAVE"];
                year.REDTIDE = context.Request.Form["REDTIDE"];
                year.GREENTIDE = context.Request.Form["GREENTIDE"];
                year.TROPICALCYCLONE = context.Request.Form["TROPICALCYCLONE"];
                year.HEADREPORTER = context.Request.Form["HEADREPORTER"];
                year.DEPUTYREPORTER = context.Request.Form["DEPUTYREPORTER"];
                year.DOCNAME = context.Request.Form["DOCNAME"];

                year.SEAICE = NewMediumAndLongHBUploader.tableMessage;//获取表格中海冰的内容
                year.SEAICETABLEPATH = NewMediumAndLongHBUploader.filePath;//表格的路径


                //根据发布单位获取发送单位
                if (year.PUBLISHCOMPANY == "NCS")
                { 
                    year.SENDDEPARTMENT = this.GetSendUnit("中长期北海环境专项_年报");
                }
                else if (year.PUBLISHCOMPANY == "SD")
                {
                    year.SENDDEPARTMENT = this.GetSendUnit("中长期山东环境专项_年报");
                }
                
                //year.SENDDEPARTMENT = ""; //测试后删除，用this.GetSendUnit()

                var YBQUYU = "";
                var YBNEIRONG = "";
                var YBSHIXIAO = "";
                var YBSHIJIAN = "";
                var YBDANWEI = "";
                string templateFile = "";
                YBNEIRONG = "环境";
                YBSHIXIAO = "年";
                //YBSHIJIAN = DateTime.Now.Date.AddHours(10).ToString("yyyy-MM-dd  hh:mm:ss");
                YBSHIJIAN = DateTime.Now.Date.AddHours(10).ToString("yyyy-MM-dd");
                //复制Word
                if (year.PUBLISHCOMPANY == "NCS")
                {
                    YBQUYU = "北海区";
                    YBDANWEI = "北海预报中心";
                    templateFile = context.Server.MapPath("../pageoffice/doc/EN-MediumAndLong/国家海洋环境年预报.doc");
                }
                else if(year.PUBLISHCOMPANY == "SD")
                {
                    YBQUYU = "山东近海";
                    YBDANWEI = "山东省海洋预报台";
                    templateFile = context.Server.MapPath("../pageoffice/doc/EN-MediumAndLong/山东海洋环境年预报.doc");
                }
                string NewFile = context.Server.MapPath("../预报单共享/zcq/" + year.PUBLISHTIME);
                YearWord yearWord = new YearWord();
                int flag = yearWord.CopyWord(templateFile, NewFile, year);
                if (flag == 1)
                {
                    sql_ReportYear sqlReportYear = new sql_ReportYear();
                    DataTable dt = sqlReportYear.GetReportYear(year);
                    //字段属性、文件流入库
                    if (dt != null && dt.Rows.Count < 1)
                    {
                        int rultInsert = sqlReportYear.InsertReportYear(year);
                        //发送单位
                        CommonSendUnit commonSendUnit = new CommonSendUnit(year.DOCNAME, year.SENDDEPARTMENT);
                        string rult = commonSendUnit.resultSendUnit();
                        if (rultInsert == 1 && rult == "insertSuccess" || rult == "updateSuccess")
                        {
                            int r = yearWord.InsertFlow(NewFile+"\\", year.DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                            if (r == 1)
                            {
                                context.Response.Write(year.PUBLISHCOMPANY+"预报单生成成功；");
                            }
                            else
                            {
                                context.Response.Write(year.PUBLISHCOMPANY + "预报单生成失败；");
                            }
                        }
                        else
                        {
                            context.Response.Write(year.PUBLISHCOMPANY + "预报单生成失败；");
                        }
                    }
                    else
                    {
                        int rultUpdate = sqlReportYear.UpdateReportYear(year);
                        //发送单位
                        CommonSendUnit commonSendUnit = new CommonSendUnit(year.DOCNAME, year.SENDDEPARTMENT);
                        string rt = commonSendUnit.resultSendUnit();
                        if (rultUpdate == 1 && rt == "insertSuccess" || rt == "updateSuccess")
                        {
                            int res = yearWord.UpdateFlow(NewFile+"\\", year.DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                            if (res == 1)
                            {
                                context.Response.Write(year.PUBLISHCOMPANY + "预报单生成成功；");
                            }
                            else
                            {
                                context.Response.Write(year.PUBLISHCOMPANY + "预报单生成失败；");
                            }
                        }
                        else
                        {
                            context.Response.Write(year.PUBLISHCOMPANY + "预报单生成失败；");
                        }
                    }
                }
                else
                {
                    context.Response.Write(year.PUBLISHCOMPANY + "预报单生成失败；");
                }

                NewMediumAndLongHBUploader.tableMessage = null;
                NewMediumAndLongHBUploader.filePath = null;

                KillProcess();

            }
            catch(Exception error)
            {
                context.Response.Write("预报单生成失败；");
            }
        }

        /// <summary>
        /// 添加中长期旬、月预报
        /// 20180820，旬、月预报统一提交,update by Durriya
        /// </summary>
        /// <param name="context"></param>
        private void SetMonthOrXun(HttpContext context)
        {
            try
            {
                ReportMonthModel month = new ReportMonthModel();
                List<ReportMonthModel> list = new List<ReportMonthModel>();
                var serializer = new JavaScriptSerializer();

                var NCS = context.Request.Form["NCSM"];  //北海分局数据
                var SD = context.Request.Form["SDM"];    //山东预报数据
                var NPOIL = context.Request.Form["NPOILM"];//南堡油田数据
                var SLOIL = context.Request.Form["SLOILM"];//胜利油田数据
                var ContentDYOIL = context.Request.Form["ContentDYOILM"];//东营环境预报数据
                var reportType = context.Request.Form["reportType"];//type形式为X、M

                var NCSModel = serializer.Deserialize<ReportMonthModel>(NCS);
                list.Add(NCSModel);

                var SDModel = serializer.Deserialize<ReportMonthModel>(SD);
                list.Add(SDModel);

                if (NPOIL != null)
                {
                    var NPOILModel = serializer.Deserialize<ReportMonthModel>(NPOIL);
                    list.Add(NPOILModel);
                }

                if (SLOIL != null)
                {
                    var SLOILModel = serializer.Deserialize<ReportMonthModel>(SLOIL);
                    list.Add(SLOILModel);
                }

                if (ContentDYOIL != null)
                {
                    var ContentDYOILModel = serializer.Deserialize<ReportMonthModel>(ContentDYOIL);
                    list.Add(ContentDYOILModel);
                }
             
                SaveData(context, list, reportType);

            }
            catch (Exception error)
            {
                context.Response.Write("预报单生成失败；");
            }
        }

        /// <summary>
        /// 保存旬、月数据
        /// </summary>
        /// <param name="list"></param>
        private void SaveData(HttpContext context, List<ReportMonthModel> list, string reportType)
        {
            var YBQUYU = "";
            var YBNEIRONG = "";
            var YBSHIXIAO = "";
            var YBSHIJIAN = "";
            var YBDANWEI = "";
            var SENDDEPARTMENT = "";
            for (int i = 0; i < list.Count; i++)
            {
                var pbCompany = list[i].PUBLISHCOMPANY;
                //根据发布单位获取发送单位
                if (pbCompany == "NCS")
                {
                    SENDDEPARTMENT = this.GetSendUnit("中长期北海环境专项_月旬报");
                }
                else if (pbCompany == "SD")
                {
                    SENDDEPARTMENT = this.GetSendUnit("中长期山东环境专项_月旬报");
                }
                else if (pbCompany.Contains("东营"))
                {
                    SENDDEPARTMENT = this.GetSendUnit("中长期东营环境专项");
                }
                else if (pbCompany.Contains("胜利"))
                {
                    SENDDEPARTMENT = this.GetSendUnit("中长期胜利环境专项");
                }
                else if (pbCompany.Contains("南堡"))
                {
                    SENDDEPARTMENT = this.GetSendUnit("中长期南堡环境专项");
                }
              
                var EnTempTimeType = (reportType == "\"M\"") ? "月" : "旬";
                string TemplastPath = "";
                YBNEIRONG = "环境";
                YBSHIXIAO = EnTempTimeType;
                //YBSHIJIAN = DateTime.Now.Date.AddHours(10).ToString("yyyy-MM-dd  hh:mm:ss");
                YBSHIJIAN = DateTime.Now.Date.AddHours(10).ToString("yyyy-MM-dd");
                string OrganSubstitution = "";
                switch (pbCompany)
                {
                    case "NCS":
                        TemplastPath = context.Server.MapPath("../pageoffice/doc/EN-MediumAndLong/国家海洋环境" + EnTempTimeType + "预报.doc");
                        YBQUYU = "北海区";
                        YBDANWEI = "北海预报中心";
                        OrganSubstitution = "NCS";
                        break;
                    case "SD":
                        TemplastPath = context.Server.MapPath("../pageoffice/doc/EN-MediumAndLong/山东海洋环境" + EnTempTimeType + "预报.doc");
                        YBQUYU = "山东近海";
                        YBDANWEI = "山东省海洋预报台";
                        OrganSubstitution = "SD";
                        break;
                    case "南堡油田":
                        TemplastPath = context.Server.MapPath("../pageoffice/doc/CN-MediumAndLong/" + GetCNTemp(list[i].PUBLISHTIME, "南堡油田"));
                        YBQUYU = "南堡油田海域";
                        YBDANWEI = "北海预报中心";
                        OrganSubstitution = "NP";
                        break;
                    case "胜利油田":
                        TemplastPath = context.Server.MapPath("../pageoffice/doc/CN-MediumAndLong/" + GetCNTemp(list[i].PUBLISHTIME, "胜利油田"));
                        YBQUYU = "胜利油田海域";
                        YBDANWEI = "北海预报中心";
                        OrganSubstitution = "SL";
                        break;
                    case "东营环境预报":
                        TemplastPath = context.Server.MapPath("../pageoffice/doc/CN-MediumAndLong/" + GetCNTemp(list[i].PUBLISHTIME, "东营环境预报"));
                        YBQUYU = "东营海域";
                        YBDANWEI = "北海预报中心";
                        OrganSubstitution = "DY";
                        break;
                    default:
                        break;
                }
                string NewFile = context.Server.MapPath("../预报单共享/zcq/" + list[i].PUBLISHTIME);


                //获取海冰表格的路径
                string tablepath;
                if (HBParamStaticList.HBParamList != null )
                {
                    var tablepathlist = (from c in HBParamStaticList.HBParamList
                                     where c.ForcastArea == OrganSubstitution
                                     select c.FilePath).ToList();
                    tablepath = (tablepathlist == null || !(tablepathlist.Count > 0)) ? "" : tablepathlist[0].ToString();
                }
                else
                {

                    tablepath = "";
                }
                //获取海冰表格的路径




                //复制模板
                MonthOrDays monthOrDays = new MonthOrDays();
                int flag = monthOrDays.CopyWord(TemplastPath, NewFile, list[i],tablepath);
                //int flag = monthOrDays.CopyWord(TemplastPath, NewFile, list[i]);//原来的
                if (flag == 1)
                {
                    sql_ReportMonthOrDays sqlMonthOrDays = new sql_ReportMonthOrDays();
                    DataTable dt = sqlMonthOrDays.GetReportMonthOrDays(list[i], reportType);

                    //这样可以
                    //var tableInfo = (from c in HBParamStaticList.HBParamList
                    //                 where c.ForcastArea == OrganSubstitution
                    //                 select c.FileMessage).ToList();
                    //string message = (tableInfo == null || !(tableInfo.Count > 0)) ? "" : tableInfo[0].ToString();
                    //这样可以

                    string message;
                    if (HBParamStaticList.HBParamList != null)
                    {
                        var tableInfo = (from c in HBParamStaticList.HBParamList
                                     where c.ForcastArea == OrganSubstitution
                                     select c.FileMessage).ToList();
                        message = (tableInfo == null || !(tableInfo.Count > 0)) ? "" : tableInfo[0].ToString();
                    }
                    else
                    {
                        
                        message = "";
                    }
                    

                    //字段属性、文件流入库
                    if (dt != null && dt.Rows.Count < 1)
                    {
                        //var tableInfo = (from c in HBParamStaticList.HBParamList
                        //                    where c.ForcastArea == pbCompany
                        //                    select c.FileMessage).ToList();
                        //string message = (tableInfo == null || !(tableInfo.Count>0)) ? "" : tableInfo[0].ToString();
                        int rultInsert = sqlMonthOrDays.InsertReportMonthOrDays(list[i], message, reportType);



                        //int rultInsert = sqlMonthOrDays.InsertReportMonthOrDays(list[i], reportType);
                        //发送单位
                        //var SENDDEPARTMENT = this.GetSendUnit();
                        CommonSendUnit commonSendUnit = new CommonSendUnit(list[i].DOCNAME, SENDDEPARTMENT);
                        string rult = commonSendUnit.resultSendUnit();
                        if (rultInsert == 1 && rult == "insertSuccess" || rult == "updateSuccess")
                        {
                            string str_ME= monthOrDays.InsertOrUpdateFlow_ME(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                            string str_FILE = monthOrDays.InsertOrUpdateFlow_FILE(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                            if (str_ME == "Success" && str_FILE == "Success")
                            {
                                context.Response.Write(pbCompany + "预报单生成成功；");
                            }
                            else
                            {
                                context.Response.Write(pbCompany + "预报单生成失败；");
                            }
                            //int r = monthOrDays.InsertFlow(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                            //if (r == 1)
                            //{
                            //    context.Response.Write(pbCompany + "预报单生成成功；");
                            //}
                            //else
                            //{
                            //    context.Response.Write(pbCompany + "预报单生成失败；");
                            //}
                        }
                        else
                        {
                            context.Response.Write(pbCompany + "预报单生成失败；");
                        }
                    }
                    else
                    {
                        
                        int rultUpdate = sqlMonthOrDays.UpdateReportMonthOrDays(list[i], message, reportType);


                        //int rultUpdate = sqlMonthOrDays.UpdateReportMonthOrDays(list[i], reportType);
                        //发送单位
                        //var SENDDEPARTMENT = this.GetSendUnit();
                        CommonSendUnit commonSendUnit = new CommonSendUnit(list[i].DOCNAME, SENDDEPARTMENT);
                        string rt = commonSendUnit.resultSendUnit();
                        if (rultUpdate == 1 && rt == "insertSuccess" || rt == "updateSuccess")
                        {
                            string str_ME = monthOrDays.InsertOrUpdateFlow_ME(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                            string str_FILE = monthOrDays.InsertOrUpdateFlow_FILE(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                            if (str_ME == "Success" && str_FILE == "Success")
                            {
                                context.Response.Write(pbCompany + "预报单生成成功；");
                            }
                            else
                            {
                                context.Response.Write(pbCompany + "预报单生成失败；");
                            }
                            //int res = monthOrDays.UpdateFlow(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                            //if (res == 1)
                            //{
                            //    context.Response.Write(pbCompany + "预报单生成成功；");
                            //}
                            //else
                            //{
                            //    context.Response.Write(pbCompany + "预报单生成失败；");
                            //}
                        }
                        else
                        {
                            context.Response.Write(pbCompany + "预报单生成失败；");
                        }
                    }
                }
                else
                {
                    context.Response.Write(pbCompany + "预报单生成失败；");
                }


                KillProcess();
            }
            HBParamStaticList.HBParamList = null;
        }




        //中途备份 可用
        //private void SaveData(HttpContext context, List<ReportMonthModel> list, string reportType)
        //{
        //    var isTableUpload = NewMediumAndLongHBUploader.IsMonthTable;//判断是否上传海冰的表格，

        //    var YBQUYU = "";
        //    var YBNEIRONG = "";
        //    var YBSHIXIAO = "";
        //    var YBSHIJIAN = "";
        //    var YBDANWEI = "";
        //    var SENDDEPARTMENT = "";
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        var pbCompany = list[i].PUBLISHCOMPANY;
        //        //根据发布单位获取发送单位
        //        if (pbCompany == "NCS")
        //        {
        //            SENDDEPARTMENT = this.GetSendUnit("中长期北海环境专项_月旬报");
        //        }
        //        else if (pbCompany == "SD")
        //        {
        //            SENDDEPARTMENT = this.GetSendUnit("中长期山东环境专项_月旬报");
        //        }
        //        else if (pbCompany.Contains("东营"))
        //        {
        //            SENDDEPARTMENT = this.GetSendUnit("中长期东营环境专项");
        //        }
        //        else if (pbCompany.Contains("胜利"))
        //        {
        //            SENDDEPARTMENT = this.GetSendUnit("中长期胜利环境专项");
        //        }
        //        else if (pbCompany.Contains("南堡"))
        //        {
        //            SENDDEPARTMENT = this.GetSendUnit("中长期南堡环境专项");
        //        }

        //        var EnTempTimeType = (reportType == "\"M\"") ? "月" : "旬";
        //        string TemplastPath = "";
        //        YBNEIRONG = "环境";
        //        YBSHIXIAO = EnTempTimeType;
        //        //YBSHIJIAN = DateTime.Now.Date.AddHours(10).ToString("yyyy-MM-dd  hh:mm:ss");
        //        YBSHIJIAN = DateTime.Now.Date.AddHours(10).ToString("yyyy-MM-dd");
        //        string OrganSubstitution = "";
        //        switch (pbCompany)
        //        {
        //            case "NCS":
        //                TemplastPath = context.Server.MapPath("../pageoffice/doc/EN-MediumAndLong/国家海洋环境" + EnTempTimeType + "预报.doc");
        //                YBQUYU = "北海区";
        //                YBDANWEI = "北海预报中心";
        //                OrganSubstitution = "NCS";
        //                break;
        //            case "SD":
        //                TemplastPath = context.Server.MapPath("../pageoffice/doc/EN-MediumAndLong/山东海洋环境" + EnTempTimeType + "预报.doc");
        //                YBQUYU = "山东近海";
        //                YBDANWEI = "山东省海洋预报台";
        //                OrganSubstitution = "SD";
        //                break;
        //            case "南堡油田":
        //                TemplastPath = context.Server.MapPath("../pageoffice/doc/CN-MediumAndLong/" + GetCNTemp(list[i].PUBLISHTIME, "南堡油田"));
        //                YBQUYU = "南堡油田海域";
        //                YBDANWEI = "北海预报中心";
        //                OrganSubstitution = "NP";
        //                break;
        //            case "胜利油田":
        //                TemplastPath = context.Server.MapPath("../pageoffice/doc/CN-MediumAndLong/" + GetCNTemp(list[i].PUBLISHTIME, "胜利油田"));
        //                YBQUYU = "胜利油田海域";
        //                YBDANWEI = "北海预报中心";
        //                OrganSubstitution = "SL";
        //                break;
        //            case "东营环境预报":
        //                TemplastPath = context.Server.MapPath("../pageoffice/doc/CN-MediumAndLong/" + GetCNTemp(list[i].PUBLISHTIME, "东营环境预报"));
        //                YBQUYU = "东营海域";
        //                YBDANWEI = "北海预报中心";
        //                OrganSubstitution = "DY";
        //                break;
        //            default:
        //                break;
        //        }
        //        string NewFile = context.Server.MapPath("../预报单共享/zcq/" + list[i].PUBLISHTIME);

        //        //复制模板
        //        MonthOrDays monthOrDays = new MonthOrDays();
        //        int flag = monthOrDays.CopyWord(TemplastPath, NewFile, list[i]);
        //        if (flag == 1)
        //        {
        //            sql_ReportMonthOrDays sqlMonthOrDays = new sql_ReportMonthOrDays();
        //            DataTable dt = sqlMonthOrDays.GetReportMonthOrDays(list[i], reportType);

        //            //这样可以
        //            //var tableInfo = (from c in HBParamStaticList.HBParamList
        //            //                 where c.ForcastArea == OrganSubstitution
        //            //                 select c.FileMessage).ToList();
        //            //string message = (tableInfo == null || !(tableInfo.Count > 0)) ? "" : tableInfo[0].ToString();
        //            //这样可以

        //            string message;
        //            if (isTableUpload == "已上传")
        //            {
        //                var tableInfo = (from c in HBParamStaticList.HBParamList
        //                                 where c.ForcastArea == OrganSubstitution
        //                                 select c.FileMessage).ToList();
        //                message = (tableInfo == null || !(tableInfo.Count > 0)) ? "" : tableInfo[0].ToString();
        //            }
        //            else
        //            {

        //                message = "";
        //            }


        //            //字段属性、文件流入库
        //            if (dt != null && dt.Rows.Count < 1)
        //            {
        //                //var tableInfo = (from c in HBParamStaticList.HBParamList
        //                //                    where c.ForcastArea == pbCompany
        //                //                    select c.FileMessage).ToList();
        //                //string message = (tableInfo == null || !(tableInfo.Count>0)) ? "" : tableInfo[0].ToString();
        //                int rultInsert = sqlMonthOrDays.InsertReportMonthOrDays(list[i], message, reportType);



        //                //int rultInsert = sqlMonthOrDays.InsertReportMonthOrDays(list[i], reportType);
        //                //发送单位
        //                //var SENDDEPARTMENT = this.GetSendUnit();
        //                CommonSendUnit commonSendUnit = new CommonSendUnit(list[i].DOCNAME, SENDDEPARTMENT);
        //                string rult = commonSendUnit.resultSendUnit();
        //                if (rultInsert == 1 && rult == "insertSuccess" || rult == "updateSuccess")
        //                {
        //                    string str_ME = monthOrDays.InsertOrUpdateFlow_ME(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        //                    string str_FILE = monthOrDays.InsertOrUpdateFlow_FILE(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        //                    if (str_ME == "Success" && str_FILE == "Success")
        //                    {
        //                        context.Response.Write(pbCompany + "预报单生成成功；");
        //                    }
        //                    else
        //                    {
        //                        context.Response.Write(pbCompany + "预报单生成失败；");
        //                    }
        //                    //int r = monthOrDays.InsertFlow(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        //                    //if (r == 1)
        //                    //{
        //                    //    context.Response.Write(pbCompany + "预报单生成成功；");
        //                    //}
        //                    //else
        //                    //{
        //                    //    context.Response.Write(pbCompany + "预报单生成失败；");
        //                    //}
        //                }
        //                else
        //                {
        //                    context.Response.Write(pbCompany + "预报单生成失败；");
        //                }
        //            }
        //            else
        //            {

        //                int rultUpdate = sqlMonthOrDays.UpdateReportMonthOrDays(list[i], message, reportType);


        //                //int rultUpdate = sqlMonthOrDays.UpdateReportMonthOrDays(list[i], reportType);
        //                //发送单位
        //                //var SENDDEPARTMENT = this.GetSendUnit();
        //                CommonSendUnit commonSendUnit = new CommonSendUnit(list[i].DOCNAME, SENDDEPARTMENT);
        //                string rt = commonSendUnit.resultSendUnit();
        //                if (rultUpdate == 1 && rt == "insertSuccess" || rt == "updateSuccess")
        //                {
        //                    string str_ME = monthOrDays.InsertOrUpdateFlow_ME(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        //                    string str_FILE = monthOrDays.InsertOrUpdateFlow_FILE(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        //                    if (str_ME == "Success" && str_FILE == "Success")
        //                    {
        //                        context.Response.Write(pbCompany + "预报单生成成功；");
        //                    }
        //                    else
        //                    {
        //                        context.Response.Write(pbCompany + "预报单生成失败；");
        //                    }
        //                    //int res = monthOrDays.UpdateFlow(NewFile + "\\", list[i].DOCNAME, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        //                    //if (res == 1)
        //                    //{
        //                    //    context.Response.Write(pbCompany + "预报单生成成功；");
        //                    //}
        //                    //else
        //                    //{
        //                    //    context.Response.Write(pbCompany + "预报单生成失败；");
        //                    //}
        //                }
        //                else
        //                {
        //                    context.Response.Write(pbCompany + "预报单生成失败；");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            context.Response.Write(pbCompany + "预报单生成失败；");
        //        }
        //    }
        //    HBParamStaticList.HBParamList = null;
        //}

        /// <summary>
        /// 解析获取中文模板
        /// update by Durriya 这个是根据时间判断对应的模板
        /// </summary>
        private string GetCNTemp(string time, string company)
        {
            var CNTemp = "";
            var t = Convert.ToDateTime(time.Substring(0, 4) + "/" + time.Substring(4, 2) + "/" + time.Substring(6, 2));
            var nian = t.Year;
            var yue = t.Month;
            var ri = Convert.ToInt32(t.Day);

            if (ri == 9)
            {
                CNTemp = "中旬预报-" + company + ".doc";
            }
            else if (ri == 19)
            {
                CNTemp = "下旬预报-" + company + ".doc";
            }
            else if (ri == 29 || (ri == 28 && yue == 2))
            {
                t = t.AddMonths(1);
                nian = t.Year;
                yue = t.Month;
                CNTemp = "上旬预报-" + company + ".doc";
            }
            else if (ri == 25 || ri == 26)
            {
                CNTemp = "月预报-" + company + ".doc";
            }
              
            return CNTemp;
        }
        /*private string GetCNTemp(string time,string company)
        {
            var CNTemp = "";
            var t = Convert.ToDateTime(time.Substring(0, 4) + "/" + time.Substring(4, 2) + "/" + time.Substring(6, 2));
            var nian = t.Year;
            var yue = t.Month;
            var ri = Convert.ToInt32(t.Day);
            if (ri * 1 < 11)
            {
                CNTemp = "中旬预报-" + company + ".doc";
            }
            else if (ri * 1 > 10 && ri * 1 < 21)
            {
                CNTemp = "下旬预报-" + company + ".doc";
            }
            else if (ri * 1 > 20)
            {
                t = t.AddMonths(1);
                nian = t.Year;
                yue = t.Month;
                CNTemp = "上旬预报-" + company + ".doc";
            }
            return CNTemp;
        }*/

        /// <summary>
        /// 中长期获取发送单位
        /// </summary>
        /// <returns></returns>
        public string GetSendUnit(string sendUnit)
        {
            Sql_HT_CONTENTS ht_contents = new Sql_HT_CONTENTS();
            DataTable dt = ht_contents.GetGroupData(sendUnit);
            string strUnit = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    strUnit += dt.Rows[i]["USERNAME"].ToString() + ";";
                //}
                //strUnit = strUnit.Substring(0, strUnit.Length - 1);
                strUnit = dt.Rows[0]["USERNAME"].ToString();
            }
            return strUnit;
        }


        #endregion



        /// <summary>
        /// 查询年数据
        /// </summary>
        /// <param name="context"></param>
        private void GetYearData(HttpContext context)
        { 
            string ptime = context.Request["ptime"].ToString();
            string company = context.Request["company"].ToString();
            sql_ReportYear year = new sql_ReportYear();
            DataTable dt = year.GetReportYearAll(ptime, company);
            if(dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();

                sb_str.Append("{ \"data\":");
                sb_str.Append(result);
                context.Response.Write(sb_str.ToString() + "}");
            }
        }

        /// <summary>
        /// 查询月、旬数据
        /// </summary>
        /// <param name="context"></param>
        private void GetMonthOrDays(HttpContext context)
        {
            string ptime = context.Request["ptime"].ToString();
            string type = context.Request["type"].ToString();
            sql_ReportMonthOrDays month = new sql_ReportMonthOrDays();
            DataTable dt = month.GetReportMonthOrDaysAll(ptime, type);
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();

                sb_str.Append("{ \"data\":");
                sb_str.Append(result);
                context.Response.Write(sb_str.ToString() + "}");
            }
            else
            {
                DataTable dtnew = month.GetReportMonthOrDaysByType(type);
                if (dtnew != null && dtnew.Rows.Count > 0)
                {
                    var submitNO = "";
                    var reportNO = dtnew.Rows[0]["REPORTNO"];//倒叙显示的最大的编码值
                    var reportTime = dtnew.Rows[0]["PUBLISHTIME"].ToString();//时间

                    var nian = ptime.Substring(0, 4);
                    var yue = ptime.Substring(4, 2);
                    var ri = ptime.Substring(6, 2);

                    if ((yue == "01" && ri == "25") || (yue == "01" && ri == "26") || (yue == "01" && ri == "09"))
                    {
                        submitNO = "1";
                    }
                    else
                    {
                        submitNO = (Convert.ToInt32(reportNO) + 1).ToString();//自动+1后生成的编码
                    }

                    StringBuilder sb_str = new StringBuilder();
                    sb_str.Append("{ \"reportNo\":\"");
                    sb_str.Append(submitNO+"\"");
                    context.Response.Write(sb_str.ToString() + "}");
                }
            }
        }


        #region 绑定预报员

        /// <summary>
        /// 绑定主预报员
        /// </summary>
        public void BindHeadReporter(HttpContext context)
        {
            DataTable dt = uploadModel.GetHeaderReporter();
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"list\":[");
            if (dt!= null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                        + "\",\"REPORTERNAME\":\"" + dt.Rows[i]["REPORTERNAME"]
                        + "\",\"REPORTERCODE\":\"" + dt.Rows[i]["REPORTERCODE"]
                        + "\"},");
                }

            }
            if (dt != null && dt.Rows.Count <= 0)
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
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"id\":\"" + dt.Rows[i]["ID"]
                        + "\",\"REPORTERNAME\":\"" + dt.Rows[i]["REPORTERNAME"]
                        + "\",\"REPORTERCODE\":\"" + dt.Rows[i]["REPORTERCODE"]
                        + "\"},");
                }

            }
            if (dt != null && dt.Rows.Count <= 0)
            {
                context.Response.Write(sb.ToString() + "]}");
            }
            else
            {
                context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]}");
            }
        }

        //结束winword进程
        private void KillProcess()
        {
            Process myProcess = new Process();
            Process[] wordProcess = Process.GetProcessesByName("winword");
            try
            {
                foreach (Process pro in wordProcess) //这里是找到那些没有界面的Word进程
                {
                    IntPtr ip = pro.MainWindowHandle;

                    string str = pro.MainWindowTitle; //发现程序中打开跟用户自己打开的区别就在这个属性
                                                      //用户打开的str 是文件的名称，程序中打开的就是空字符串
                    if (string.IsNullOrEmpty(str))
                    {
                        pro.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }


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