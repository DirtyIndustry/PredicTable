using Newtonsoft.Json;
using PredicTable.WebServiceClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Services;

namespace PredicTable
{
    /// <summary>
    /// WebServices 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://123.234.129.234:10001/WebService/WebServices.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebServices : WebService
    {
        [WebMethod]
        public string GetAmShortTableData(DateTime date)
        {
            ModelAmShortResponse result = new ModelAmShortResponse();
            result.AmShort1Data = FuncAmShort.getAmShort1(date, result.AmShortFakeData, 1);
            result.AmShort2Data = FuncAmShort.getAmShort2(date, result.AmShortFakeData, 2);
            result.AmShort3and4Data = FuncAmShort.getAmShort3and4(date, result.AmShortFakeData, 3);
            result.AmShort5Data = FuncAmShort.getAmShort5(date, result.AmShortFakeData, 5);
            result.AmShort6Data = FuncAmShort.getAmShort6(date, result.AmShortFakeData, 6);
            result.AmShort7Data = FuncAmShort.getAmShort7(date, result.AmShortFakeData, 7);
            result.AmShort8Data = FuncAmShort.getAmShort8(date, result.AmShortFakeData, 8);
            result.AmShort9Data = FuncAmShort.getAmShort9(date, result.AmShortFakeData, 9);
            result.AmShort10Data = FuncAmShort.getAmShort10(date, result.AmShortFakeData, 10);
            result.AmShort11Data = FuncAmShort.getAmShort11(date, result.AmShortFakeData, 11);
            result.AmShort12Data = FuncAmShort.getAmShort12(date, result.AmShortFakeData, 12);
            result.PublishMetaInfo = FuncAmShort.getPublishMetaInfo(date, result.AmShortFakeData, 0);

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public string SetAmShortTableData(int tablenumber, string usertype, string datajson)
        {
            System.Diagnostics.Debug.WriteLine("SetAmShortTableData()");
            System.Diagnostics.Debug.WriteLine("tablenumber: " + tablenumber);
            System.Diagnostics.Debug.WriteLine("usertype: " + usertype);
            System.Diagnostics.Debug.WriteLine("datajson: " + datajson);
            string result = "";
            switch (tablenumber)
            {
                case 0:
                    result = FuncAmShort.setPublishMetaInfo(usertype, datajson);
                    break;
                case 1:
                    result = FuncAmShort.setAmShort1(usertype, datajson);
                    break;
                case 2:
                    result = FuncAmShort.setAmShort2(usertype, datajson);
                    break;
                case 3:
                    result = FuncAmShort.setAmShort3and4(3, usertype, datajson);
                    break;
                case 4:
                    result = FuncAmShort.setAmShort3and4(4, usertype, datajson);
                    break;
                case 5:
                    result = FuncAmShort.setAmShort5(usertype, datajson);
                    break;
                case 6:
                    result = FuncAmShort.setAmShort6(usertype, datajson);
                    break;
                case 7:
                    result = FuncAmShort.setAmShort7(usertype, datajson);
                    break;
                case 8:
                    result = FuncAmShort.setAmShort8(usertype, datajson);
                    break;
                case 9:
                    result = FuncAmShort.setAmShort9(usertype, datajson);
                    break;
                case 10:
                    result = FuncAmShort.setAmShort10(usertype, datajson);
                    break;
                case 11:
                    result = FuncAmShort.setAmShort11(usertype, datajson);
                    break;
                case 12:
                    result = FuncAmShort.setAmShort12(usertype, datajson);
                    break;
                default: break;
            }
            return result;
        }

        [WebMethod]
        public string GetAmShortReportStatus(DateTime publishdate, int publishhour, string datajson)
        {
            List<ModelAmShortReport> reportlist = new List<ModelAmShortReport>();
            if (datajson != "")
            {
                reportlist = JsonConvert.DeserializeObject<List<ModelAmShortReport>>(datajson);
            }
            foreach (ModelAmShortReport report in reportlist)
            {
                report.reportStatus = "ready";
                report.reportStatusDesc = "";
                string outputfilepath = FuncAmShort.getAmShortOutputPath(report.reportTitle, publishdate, publishhour);
                if (File.Exists(outputfilepath))
                {
                    FileInfo fileinfo = new FileInfo(outputfilepath);
                    DateTime modified = fileinfo.LastWriteTime;
                    report.reportStatus = "done";
                    report.reportStatusDesc = "完成于" + modified.ToString("yyyy年MM月dd日HH时mm分ss秒");
                }
                if (report.reportStatus != "done")
                {
                    foreach (int tablenumber in report.datasource)
                    {
                        List<int> fakelist = new List<int>() { 0 };
                        switch (tablenumber)
                        {
                            case 0:
                                List<ModelPublishMetaInfo> list0 = FuncAmShort.getPublishMetaInfo(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "填报信息海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "填报信息风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "填报信息潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "填报信息水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list0))
                                {
                                    report.reportStatusDesc += "填报信息数据不完整,";
                                }
                                break;
                            case 1:
                                List<ModelAmShort1> list1 = FuncAmShort.getAmShort1(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单一海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单一风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单一潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单一水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list1))
                                {
                                    report.reportStatusDesc += "表单一数据不完整,";
                                }
                                break;
                            case 2:
                                List<ModelAmShort2> list2 = FuncAmShort.getAmShort2(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单二海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单二风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单二潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单二水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list2))
                                {
                                    report.reportStatusDesc += "表单二数据不完整,";
                                }
                                break;
                            case 3:
                                List<ModelAmShort3and4> list3 = FuncAmShort.getAmShort3and4(publishdate, fakelist, 0);
                                if (list3[0].METEOROLOGICALREVIEW == "")
                                {
                                    report.reportStatusDesc += "表单三风浪部分未提交,";
                                }
                                else if (list3[0].METEOROLOGICALREVIEWCX == "")
                                {
                                    report.reportStatusDesc += "表单三潮汐部分未提交,";
                                }
                                break;
                            case 4:
                                List<ModelAmShort3and4> list4 = FuncAmShort.getAmShort3and4(publishdate, fakelist, 0);
                                if (list4[0].METEOROLOGICALREVIEW24HOUR == "")
                                {
                                    report.reportStatusDesc += "表单三风浪部分未提交,";
                                }
                                else if (list4[0].METEOROLOGICALREVIEW24HOURCX == "")
                                {
                                    report.reportStatusDesc += "表单三潮汐部分未提交,";
                                }
                                break;
                            case 5:
                                List<ModelAmShort5> list5 = FuncAmShort.getAmShort5(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单五海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单五风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单五潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单五水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list5))
                                {
                                    report.reportStatusDesc += "表单五数据不完整,";
                                }
                                break;
                            case 6:
                                List<ModelAmShort6> list6 = FuncAmShort.getAmShort6(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单六海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单六风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单六潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单六水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list6))
                                {
                                    report.reportStatusDesc += "表单六数据不完整,";
                                }
                                break;
                            case 7:
                                List<ModelAmShort7> list7 = FuncAmShort.getAmShort7(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单七海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单七风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单七潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单七水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list7))
                                {
                                    report.reportStatusDesc += "表单七数据不完整,";
                                }
                                break;
                            case 8:
                                List<ModelAmShort8> list8 = FuncAmShort.getAmShort8(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单八海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单八风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单八潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单八水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list8))
                                {
                                    report.reportStatusDesc += "表单八数据不完整,";
                                }
                                break;
                            case 9:
                                List<ModelAmShort9> list9 = FuncAmShort.getAmShort9(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单九海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单九风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单九潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单九水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list9))
                                {
                                    report.reportStatusDesc += "表单九数据不完整,";
                                }
                                break;
                            case 10:
                                List<ModelAmShort10> list10 = FuncAmShort.getAmShort10(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单十海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单十风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单十潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单十水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list10))
                                {
                                    report.reportStatusDesc += "表单十数据不完整,";
                                }
                                break;
                            case 11:
                                List<ModelAmShort11> list11 = FuncAmShort.getAmShort11(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单十一海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单十一风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单十一潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单十一水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list11))
                                {
                                    report.reportStatusDesc += "表单十一数据不完整,";
                                }
                                break;
                            case 12:
                                List<ModelAmShort12> list12 = FuncAmShort.getAmShort12(publishdate, fakelist, 0);
                                if (fakelist[0] / 1000 > 0)
                                {
                                    report.reportStatusDesc += "表单十二海冰部分未提交,";
                                }
                                else if (fakelist[0] / 100 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单十二风浪部分未提交,";
                                }
                                else if (fakelist[0] / 10 > 0 & fakelist[0] / 100 < 10)
                                {
                                    report.reportStatusDesc += "表单十二潮汐部分未提交,";
                                }
                                else if (fakelist[0] > 0 & fakelist[0] < 10)
                                {
                                    report.reportStatusDesc += "表单十二水温部分未提交,";
                                }
                                else if (FuncAmShort.hasEmpty(list12))
                                {
                                    report.reportStatusDesc += "表单十二数据不完整,";
                                }
                                break;
                            default: break;
                        }
                    }
                    if (report.reportStatusDesc.EndsWith(","))
                    {
                        report.reportStatusDesc = report.reportStatusDesc.Substring(0, report.reportStatusDesc.Length - 1);
                        report.reportStatus = "notready";
                    }
                    if (report.reportStatus == "ready")
                    {
                        report.reportStatusDesc = "准备就绪, 可以生成预报单";
                    }
                }
            }
            return JsonConvert.SerializeObject(reportlist);
        }

        [WebMethod]
        public string GenerateAmShortReport(DateTime publishdate, int publishhour, string datajson)
        {
            List<ModelAmShortReport> reportlist = new List<ModelAmShortReport>();
            if (datajson != "")
            {
                reportlist = JsonConvert.DeserializeObject<List<ModelAmShortReport>>(datajson);
            }
            foreach (ModelAmShortReport report in reportlist)
            {
                if (report.selected & report.reportStatus != "notready")
                {
                    string output = FuncAmShort.createWordDoc(publishdate, publishhour, report);

                    if (output == "完成")
                    {
                        report.reportStatus = "done";
                        report.reportStatusDesc = "完成于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒");
                    }
                    else
                    {
                        report.reportStatus = "error";
                        report.reportStatusDesc = output;
                    }
                }
            }
            return JsonConvert.SerializeObject(reportlist);
        }
        
        [WebMethod]
        public string UploadTideDataTianjin(byte[] file, string filename)
        {
            System.Diagnostics.Debug.WriteLine(file);
            string folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles", "tidedata");
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            string filepath = Path.Combine(folderpath, "upload.txt");
            try
            {
                File.WriteAllBytes(filepath, file);
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            return "完成";
        }

        [WebMethod]
        public string DevTest(object o)
        {
            System.Diagnostics.Debug.WriteLine(o);
            return "";
        }

        [WebMethod]
        public string CloseWord()
        {
            int count = 0;
            try
            {
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process p in processes)
                {
                    if (p.ProcessName == "WINWORD")
                    {
                        count++;
                        p.Kill();
                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return count.ToString();
        }

    }
}
