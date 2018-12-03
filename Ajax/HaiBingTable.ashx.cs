using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.ExportWord.NineteenWord;
using PredicTable.Model;
using PredicTable.Model.NineteenWord;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// 海冰Ueditor版
    /// </summary>
    public class HaiBingTable : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();
            switch (method)
            {
                case "getContents": getContents(context); break;
                case "AddDocContent": AddDocContent(context); break;
                case "AddHaiBing": AddHaiBing(context); break;
                default:
                    break;
            }
        }
        //模板保存
        void AddDocContent(HttpContext context)
        {
            HT_KJ_BHHB_CONTENTS Contentvalue = new HT_KJ_BHHB_CONTENTS();
            //警报文件内容
            Contentvalue.FILENAME = context.Request.Form["FILENAME"];//文件名称
            Contentvalue.CONTENT = context.Request.Form["CONTENT"];//内容
            var sqlConC = new Sql_BHHB_CONTENTS();
            var num = sqlConC.AddDocContent(Contentvalue);
           
        }
        //返回模板内容
        void getContents(HttpContext context)
        {
            HT_KJ_BHHB_CONTENTS Contentvalue = new HT_KJ_BHHB_CONTENTS();
            Contentvalue.FILENAME = context.Request.Form["FILENAME"];//文件名称
            var dataTable = new DataTable();
            var sqlConC = new Sql_BHHB_CONTENTS();   
            dataTable = sqlConC.GetContentsData(Contentvalue);
            var dataJson = JsonMore.Serialize(dataTable);
            context.Response.ContentType = "application/json";
            context.Response.Write(dataJson);
        } 

        //保存
        void AddHaiBing(HttpContext context)
        {
            string downFile = "";
            string returnStr = "";
            string filepath = "";
            string fileName = "";
            DateTime time = DateTime.Now;
            string fawang = "";
            string WainArea = "";
            string yubaodanwei = "";
            string haibing = "";
            #region Word 本 地 保 存
            try
            {
                //word 文档参数
                List<string> param = new List<string>();
               
                param.Insert(0, context.Request.Form["Nos"]);
                param.Insert(1, context.Request.Form["times"]);
                param.Insert(2, context.Request.Form["CONTENT"]);
                param.Insert(3, context.Request.Form["User"]);
                param.Insert(4, context.Request.Form["SENTTO"]);

               
                string templateFileName = context.Request.Form["DOCNAME"];
                string pubDateStr = context.Request.Form["XXTime"];
                fileName = context.Request.Form["fileName"]+ ".doc";

                #region 文件名判断
               
                //文件名格式判断
                if (fileName.Split('.')[1] == "docx")
                {
                    fileName = fileName.Split('.')[0] + ".doc";
                }
                if (fileName.Contains("NCS"))
                {
                    yubaodanwei = "北海预报中心";
                    WainArea = "北海区";
                    haibing = "海冰";
                    switch (templateFileName)
                    {
                        case "旬":
                            templateFileName = "19号预报单--旬.doc";
                            break;
                        case "月":
                            templateFileName = "19号预报单--月.doc";
                            break;
                        case "周":
                            templateFileName = "19号预报单--周.doc";
                            break;
                        case "年":
                            templateFileName = "19号预报单--年.doc";
                            break;
                        default:
                            break;

                    }
                }
                if (fileName.Contains("SD"))
                {
                    yubaodanwei = "山东省海洋预报台";
                    WainArea = "山东近海";
                    haibing = "海冰";
                    switch (templateFileName)
                    {
                        case "旬":
                            templateFileName = "19号预报单--旬.doc";
                            break;
                        case "月":
                            templateFileName = "19号预报单--月.doc";
                            break;
                        case "周":
                            templateFileName = "19号预报单--周.doc";
                            break;
                        case "年":
                            templateFileName = "19号预报单--年.doc";
                            break;
                        default:
                            break;

                    }

                }
                if (fileName.Contains("东营"))
                {
                    yubaodanwei = "北海预报中心";
                    WainArea = "东营近海";
                    haibing = "东营海冰";
                    templateFileName = "东营预报单.doc";


                }
                if (fileName.Contains("冀东"))
                {
                    yubaodanwei = "北海预报中心";
                    WainArea = "冀东油田";
                    haibing = "冀东海冰";
                    templateFileName = "冀东预报单.doc";

                }
                if (fileName.Contains("胜利"))
                {
                    yubaodanwei = "北海预报中心";
                    WainArea = "东营胜利油田";
                    haibing = "胜利海冰";
                    templateFileName = "胜利预报单.doc";
                }
                if (fileName.Contains("青岛"))
                {
                    yubaodanwei = "北海预报中心";
                    WainArea = "青岛近海";
                    haibing = "青岛海冰";
                    templateFileName = "青岛预报单.doc";
                }
                #endregion
                
                #region 预报表文件属性

                CG_YUBAO_ME yubaomi = new CG_YUBAO_ME();
                yubaomi.YBWENJIANMING = fileName;
                yubaomi.YBQUYU = WainArea;
                yubaomi.YBNEIRONG = haibing;
                yubaomi.YBSHIXIAO = context.Request.Form["ListType"];
                yubaomi.YBSHIJIAN = Convert.ToDateTime(context.Request.Form["JBSHIJIAN"]);
                yubaomi.YBDANWEI = yubaodanwei;

                //消息文件属性数据保存

                var sqlConME = new sql_CG_YUBAO_ME();
                CG_YUBAO_ME tblME = new Model.CG_YUBAO_ME();
                tblME.YBWENJIANMING = fileName;

                int numMe = -1;
                //判断数据库是不是有当前数据
                System.Data.DataTable tblybddocumentME = (System.Data.DataTable)new sql_CG_YUBAO_ME().get_YUBAOME_AllData(tblME);
                if (tblybddocumentME.Rows.Count > 0)
                {
                    numMe = sqlConME.UpdateYUBAOMe(yubaomi);
                }
                else
                {
                    numMe = sqlConME.Add_CG_YUBAO_ME(yubaomi);
                }

                if (numMe <= 0)
                {
                    returnStr += fileName+ " 预报文件属性数据提交失败。 ";
                }
                else
                {
                    returnStr += fileName+ " 预报文件属性数据提交成功。 ";
                }
                #endregion


                //模板文件               
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/s-19/"+ templateFileName);
                //生成的具有模板样式的新文件           
                filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\zcq\\"+ context.Request.Form["Time"];
                downFile = "\\预报单共享\\zcq\\" + context.Request.Form["Time"] + "\\" + fileName;
                //消息Word 文档插入数据 生成
                HaiBingNian jc = new HaiBingNian();
                int flag0 = jc.ExportWord(templateFile, filepath, fileName, param);

                if (flag0 == 0)
                {
                    returnStr = returnStr + "  " + fileName + " word生成失败!";
                }
                if (flag0 == 1)
                {
                    returnStr = returnStr + "  " + fileName + " word生成成功!";
                }

                #region 预报表文件
                try
                {
                    //预报文件保存到数据库
                    int flag1 = YUBAOHBruku(filepath + "\\" + fileName, filepath, fileName);


                    if (flag1 == 0)
                    {
                        returnStr = returnStr + "  " + fileName + " word插入预报文件表数据库失败!";
                    }
                    if (flag1 == 1)
                    {
                        returnStr = returnStr + "  " + fileName + " word插入预报文件表数据库成功!";
                    }
                }
                catch (Exception ex)
                {
                    WriteLog.Write("预报单出错" + ex.ToString());
                    returnStr = returnStr + "出错了！" + ex.ToString();
                }
               // context.Response.Write(returnStr);

                #endregion

            }
            catch (Exception ex)
            {
                WriteLog.Write("海冰预报单生成文档出错" + ex.ToString());
                returnStr = returnStr + "出错了！" + ex.ToString();
            }
            try
            {
                //=====================入库===============================
                //获取文件时效-分类保存
                //sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
               // var effection = context.Request.Form["ListType"];//文件类型（年月旬日）
                 //Word流文件保存
               // NineteenNomalFileModel nomalFileModel = new Model.NineteenWord.NineteenNomalFileModel();

                if (WainArea == "冀东油田")
                {
                    #region 冀东油田

                    sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
                    var effection = context.Request.Form["ListType"];//文件类型（年月旬日）
                    //Word流文件保存
                    NineteenNomalFileModel nomalFileModel = new Model.NineteenWord.NineteenNomalFileModel();

                    //获取文件时效-分类保存
                    if (effection == "年")
                    {
                        #region 年
                        //打开word，解析
                        SaveNineteenTablesJDYear nineteenYearTables = new SaveNineteenTablesJDYear(filepath + "\\" + fileName);
                        NineteenYearModel yearModel = new NineteenYearModel();
                        nineteenYearTables.assignment();
                        string[,] tableNormal = nineteenYearTables.table_str;
                        if (tableNormal != null)
                        {
                            //保存文件中表格NineteenYearModel
                            this.getJDYearTableData(yearModel, tableNormal);
                        }

                        //其他字段
                        yearModel.ID = fileName;
                        yearModel.PUBLISHDATE = nineteenYearTables.post_time;
                        yearModel.ICESITUATION = nineteenYearTables.ice_situation;
                        yearModel.PREDICT = nineteenYearTables.predict;
                        yearModel.DESCRIPTION = nineteenYearTables.description;
                        yearModel.FASONGDANWEI = nineteenYearTables.senddanwei;//发送单位
                        yearModel.SENDUNIT = nineteenYearTables.fawang;//发往
                        fawang = nineteenYearTables.fawang;

                        //入库
                        int yearkey = sql_nineteenTable.GetNomalYearTableKey(yearModel);
                        if (yearkey != 0)
                        {   //保存年内容
                            int tableresult = sql_nineteenTable.SaveYearTable(yearModel);
                            if (tableresult != 0)
                            {//保存年表格数据

                                if (yearModel.nineteenYearICEModel != null && yearModel.nineteenYearICEModel.Count > 0)
                                {
                                    for (int i = 0; i < yearModel.nineteenYearICEModel.Count; i++)
                                    {
                                        sql_nineteenTable.SaveYearTableIce(yearModel.nineteenYearICEModel[i], yearModel);
                                    }
                                }
                                if (yearModel.nineteenYearLineModel != null && yearModel.nineteenYearLineModel.Count > 0)
                                {
                                    for (int i = 0; i < yearModel.nineteenYearLineModel.Count; i++)
                                    {
                                        sql_nineteenTable.SaveYearTableLine(yearModel.nineteenYearLineModel[i], yearModel);
                                    }
                                }

                            }
                        }
                        nomalFileModel.PUBLISHDATE = yearModel.PUBLISHDATE;
                        nomalFileModel.FILETYPE = "001year";
                        #endregion

                    }

                    else
                    {
                        #region  冀东 周、旬、月 数 据 处 理
                        SaveNineteenTablesJD nineteenTables = new SaveNineteenTablesJD(filepath + "\\" + fileName);
                        nineteenTables.assignmentPredict_aging();
                        nineteenTables.assignment();
                        //获取表格数据

                        string[,] tableNormal = nineteenTables.table_str;

                        //文件中表格处理
                        NineteenNomalModel nomalModel = new NineteenNomalModel();
                        if (tableNormal != null)
                        {
                            this.getJDNomalTableData(nomalModel, tableNormal);
                        }


                        //其他字段
                        nomalModel.ID = fileName;
                        nomalModel.PUBLISHDATE = nineteenTables.post_time;
                        nomalModel.ICESITUATION = nineteenTables.ice_situation;
                        nomalModel.PREDICT = nineteenTables.predict;
                        nomalModel.PREDICTAGING = nineteenTables.predict_aging;
                        nomalModel.DESCRIPTION = nineteenTables.description;
                        nomalModel.FASONGDANWEI = nineteenTables.senddanwei;//发送单位
                        nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                        fawang = nineteenTables.fawang;
                        int tableresult = 0;
                        //内容入库
                        int results = sql_nineteenTable.GetNomalKey(nomalModel);
                        if (results > 0)
                        {
                            //修改内容
                            tableresult = sql_nineteenTable.UpdateNomalTable(nomalModel);
                        }
                        else
                        {
                            //添加内容
                            tableresult = sql_nineteenTable.SaveNomalTable(nomalModel);
                        }
                        if (tableresult <= 0)
                        {
                            returnStr += fileName + " 周、旬、月内容数据提交失败。 ";
                        }

                        if (tableresult != 0 && nomalModel.NineteenNomalLine != null && nomalModel.NineteenNomalLine.Count > 0)
                        { //先删除
                            if (nomalModel.ID != null && nomalModel.ID != "")
                            {
                                sql_nineteenTable.DelNomalTableLine(nomalModel);
                            }
                            for (int i = 0; i < nomalModel.NineteenNomalLine.Count; i++)
                            {
                                sql_nineteenTable.SaveNomalTableLine(nomalModel.NineteenNomalLine[i], nomalModel);
                            }
                        }

                        nomalFileModel.PUBLISHDATE = nomalModel.PUBLISHDATE;
                        nomalFileModel.FILETYPE = nomalModel.PREDICTAGING;
                        #endregion

                    }
                    #region 入库-word流文件(HT_KJ_BHHB_FILE)

                    FileStream fStream = File.OpenRead(filepath + "\\" + fileName);
                    byte[] b = new byte[fStream.Length];
                    fStream.Read(b, 0, b.Length);
                    fStream.Close();
                    nomalFileModel.FILENAME = fileName;
                    nomalFileModel.FILEFLOW = b;
                    int a = 0;
                    NineteenNomalFileModel model = new Model.NineteenWord.NineteenNomalFileModel();
                    model.FILENAME = fileName;
                    int Key = sql_nineteenTable.GetFile(model);
                    if (Key > 0)
                    {
                        //先删除
                        int a1 = sql_nineteenTable.DeleteFile(nomalFileModel);
                    }
                    int fileKeys = sql_nineteenTable.GetNomalTableFileKey(nomalFileModel);
                    if (fileKeys > 0)
                    {
                        //表单存入数据库
                        a = sql_nineteenTable.SaveFile(nomalFileModel);
                    }

                    if (a > 0)
                    {
                        //returnStr += filePathName + "入库成功。";
                    }
                    else
                    {
                        returnStr += fileName + "入库失败。";
                    }

                    #endregion
                    #endregion
                }
                else
                {

                    #region 北海区 山东近海  东营胜利油田 东营近海 青岛

                    //获取文件时效-分类保存
                    sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
                    var effection = context.Request.Form["ListType"];//文件类型（年月旬日）
                    //Word流文件保存
                    NineteenNomalFileModel nomalFileModel = new Model.NineteenWord.NineteenNomalFileModel();

                    if (effection == "年")
                    {
                        #region 年
                        SaveNineteenYearTables nineteenYearTables = new SaveNineteenYearTables(filepath + "\\" + fileName);
                        NineteenYearModel yearModel = new NineteenYearModel();
                        nineteenYearTables.assignment();
                        string[,] table_str_1 = nineteenYearTables.table_str_1;
                        string[,] table_str_2 = nineteenYearTables.table_str_2;
                        string[,] table_str_3 = nineteenYearTables.table_str_3;

                        if (table_str_1 != null && table_str_2 != null && table_str_3 != null)
                        {
                            //保存文件中表格NineteenYearModel
                            this.getYearTableData(yearModel, table_str_1, table_str_2, table_str_3);
                        }

                        //其他字段
                        yearModel.ID = fileName;
                        yearModel.PUBLISHDATE = nineteenYearTables.post_time;
                        time = nineteenYearTables.post_time;
                        yearModel.ICESITUATION = nineteenYearTables.ice_situation;
                        yearModel.PREDICT = nineteenYearTables.predict;
                        yearModel.DESCRIPTION = nineteenYearTables.description;

                        yearModel.CHUANZHEN = nineteenYearTables.chuanzhen;
                        yearModel.IPHONE = nineteenYearTables.tel;
                        yearModel.LINKMAN = nineteenYearTables.link;
                        yearModel.FASONGDANWEI = nineteenYearTables.seddanwei;//发送单位
                        yearModel.SENDUNIT = nineteenYearTables.fawang;//发往
                        fawang = nineteenYearTables.fawang;

                        //判断数据库是不是有当前数据
                        yearModel.ID = fileName;
                        int result = sql_nineteenTable.GetYearKey(yearModel);
                        int tableresult = 0;
                        if (result > 0)
                        {
                            //修改年内容
                            tableresult = sql_nineteenTable.UpdateYearTable(yearModel);
                        }
                        else
                        {
                            //添加年内容
                            tableresult = sql_nineteenTable.SaveYearTable(yearModel);
                        }

                        if (tableresult <= 0)
                        {
                            returnStr += fileName + " 年内容数据提交失败。 ";
                        }
                        else
                        {
                            //returnStr += filePathName + " 年内容数据提交成功。 ";
                        }

                        if (tableresult != 0)
                        {//保存年表格数据
                            if (yearModel.nineteenYearICEModel != null && yearModel.nineteenYearICEModel.Count > 0)
                            { //先删除
                                if (yearModel.ID != null && yearModel.ID != "")
                                {
                                    sql_nineteenTable.DelYearTableIce(yearModel);
                                }

                                for (int i = 0; i < yearModel.nineteenYearICEModel.Count; i++)
                                {
                                    sql_nineteenTable.SaveYearTableIce(yearModel.nineteenYearICEModel[i], yearModel);
                                }
                            }
                            if (yearModel.nineteenYearLineModel != null && yearModel.nineteenYearLineModel.Count > 0)
                            {
                                if (yearModel.ID != null && yearModel.ID != "")
                                {
                                    sql_nineteenTable.DelYearTableLine(yearModel);
                                }
                                for (int i = 0; i < yearModel.nineteenYearLineModel.Count; i++)
                                {
                                    sql_nineteenTable.SaveYearTableLine(yearModel.nineteenYearLineModel[i], yearModel);
                                }
                            }
                            if (yearModel.nineteenYearCknessModel != null && yearModel.nineteenYearCknessModel.Count > 0)
                            {
                                if (yearModel.ID != null && yearModel.ID != "")
                                {
                                    sql_nineteenTable.DelYearTableCkness(yearModel);
                                }
                                for (int i = 0; i < yearModel.nineteenYearCknessModel.Count; i++)
                                {
                                    sql_nineteenTable.SaveYearTableCkness(yearModel.nineteenYearCknessModel[i], yearModel);
                                }
                            }
                        }

                        nomalFileModel.PUBLISHDATE = yearModel.PUBLISHDATE;
                        nomalFileModel.FILETYPE = "001year";

                        #endregion
                    }

                    else
                    {
                        #region  周、旬、月 数 据 处 理
                        SaveNineteenTables nineteenTables = new SaveNineteenTables(filepath + "/" + fileName);
                        nineteenTables.assignmentPredict_aging();
                        nineteenTables.assignment();
                        //获取表格数据
                        string[,] tableNormal = nineteenTables.table_str;
                        //文件中表格处理
                        NineteenNomalModel nomalModel = new NineteenNomalModel();
                        if (tableNormal != null)
                        {
                            this.getNomalTableData(nomalModel, tableNormal);
                        }
                        nomalModel.ID = fileName;
                        nomalModel.PUBLISHDATE = nineteenTables.post_time;
                        time = nineteenTables.post_time;
                        nomalModel.ICESITUATION = nineteenTables.ice_situation;
                        nomalModel.PREDICT = nineteenTables.predict;
                        nomalModel.PREDICTAGING = nineteenTables.predict_aging;
                        nomalModel.DESCRIPTION = nineteenTables.description;
                        nomalModel.CHUANZHEN = nineteenTables.chuanzhen;
                        nomalModel.IPHONE = nineteenTables.tel;
                        nomalModel.LINKMAN = nineteenTables.link;
                        nomalModel.FASONGDANWEI = nineteenTables.seddanwei;//发送单位
                        nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                        fawang = nineteenTables.fawang;
                        //入库
                        nomalModel.ID = fileName;
                        int results = sql_nineteenTable.GetNomalKey(nomalModel);
                        int tableresults = 0;
                        if (results > 0)
                        {
                            //修改内容
                            tableresults = sql_nineteenTable.UpdateNomalTable(nomalModel);
                        }
                        else
                        {
                            //添加内容
                            tableresults = sql_nineteenTable.SaveNomalTable(nomalModel);
                        }
                        if (tableresults <= 0)
                        {
                            returnStr += fileName + " 周、旬、月内容数据提交失败。 ";
                        }
                        else
                        {
                            //returnStr += filePathName + " 周、旬、月内容数据提交成功。 ";
                        }

                        if (tableresults != 0 && nomalModel.NineteenNomalLine != null && nomalModel.NineteenNomalLine.Count > 0)
                        { //先删除
                            if (nomalModel.ID != null && nomalModel.ID != "")
                            {
                                sql_nineteenTable.DelNomalTableLine(nomalModel);
                            }
                            for (int i = 0; i < nomalModel.NineteenNomalLine.Count; i++)
                            {
                                sql_nineteenTable.SaveNomalTableLine(nomalModel.NineteenNomalLine[i], nomalModel);
                            }
                        }

                        nomalFileModel.PUBLISHDATE = nomalModel.PUBLISHDATE;
                        nomalFileModel.FILETYPE = nomalModel.PREDICTAGING;
                        #endregion

                    }

                    #region 入库-word流文件(HT_KJ_BHHB_FILE)

                    FileStream fStream = File.OpenRead(filepath + "\\" + fileName);
                    byte[] b = new byte[fStream.Length];
                    fStream.Read(b, 0, b.Length);
                    fStream.Close();
                    nomalFileModel.FILENAME = fileName;
                    nomalFileModel.FILEFLOW = b;
                    int a = 0;
                    NineteenNomalFileModel model = new Model.NineteenWord.NineteenNomalFileModel();
                    model.FILENAME = fileName;
                    int Key = sql_nineteenTable.GetFile(model);
                    if (Key > 0)
                    {
                        //先删除
                        int a1 = sql_nineteenTable.DeleteFile(nomalFileModel);
                    }
                    int fileKeys = sql_nineteenTable.GetNomalTableFileKey(nomalFileModel);
                    if (fileKeys > 0)
                    {
                        //表单存入数据库
                        a = sql_nineteenTable.SaveFile(nomalFileModel);
                    }

                    if (a > 0)
                    {
                        //returnStr += filePathName + "入库成功。";
                    }
                    else
                    {
                        returnStr += fileName + "入库失败。";
                    }

                    #endregion
                  #endregion

                }

                //发往保存
                CommonSendUnit sunit = new CommonSendUnit();
                string ret = sunit.resultSendUnitbz(fileName, context.Request.Form["SENTTO"], "");
            }
            catch (Exception e)
            {
                WriteLog.Write("海冰预报入库出错" + e.ToString());
                returnStr = returnStr + "出错了！" + e.ToString();
            }
            returnStr += "|" + downFile;
            context.Response.Write(returnStr);

            #endregion
                      
           
        }
        //保存文件中表格NineteenYearModel
        private void getJDYearTableData(NineteenYearModel yearModel, string[,] table_str)
        {
            List<NineteenYearICEModel> iceList = new List<NineteenYearICEModel>();
            List<NineteenYearLineModel> lineList = new List<NineteenYearLineModel>();
            List<NineteenYearCknessModel> cknessList = new List<NineteenYearCknessModel>();
            //渤海及黄海北部冰日预测
            var icerow = table_str.GetLength(0);
            NineteenYearICEModel iceModel = new NineteenYearICEModel();
            iceModel.NAME = table_str[1, 0].ToString();
            iceModel.FIRSTFROZENDAY = table_str[4, 1].ToString();
            iceModel.SERIOUSICE = table_str[4, 2].ToString();
            iceModel.ICEMELTINGDAY = table_str[4, 3].ToString();
            iceModel.ICEDISAPPDAY = "";
            iceList.Add(iceModel);
            NineteenYearICEModel iceModel1 = new NineteenYearICEModel();
            iceModel1.NAME = table_str[5, 0].ToString();
            iceModel1.FIRSTFROZENDAY = table_str[8, 1].ToString();
            iceModel1.SERIOUSICE = table_str[8, 2].ToString();
            iceModel1.ICEMELTINGDAY = table_str[8, 3].ToString();
            iceModel1.ICEDISAPPDAY = "";
            iceList.Add(iceModel1);
            yearModel.nineteenYearICEModel = iceList;

            NineteenYearLineModel lineModel = new NineteenYearLineModel();
            lineModel.NAME = table_str[1, 0].ToString();
            lineModel.TERMINALLINE = table_str[2, 1].ToString();
            lineModel.GENERALICETHICKNESS = table_str[2, 2].ToString();
            lineModel.MAXICETHICKNESS = table_str[2, 3].ToString();
            lineList.Add(lineModel);
            NineteenYearLineModel lineModel1 = new NineteenYearLineModel();
            lineModel1.NAME = table_str[5, 0].ToString();
            lineModel1.TERMINALLINE = table_str[6, 1].ToString();
            lineModel1.GENERALICETHICKNESS = table_str[6, 2].ToString();
            lineModel1.MAXICETHICKNESS = table_str[6, 3].ToString();
            lineList.Add(lineModel1);
            yearModel.nineteenYearLineModel = lineList;

        }

        /// <summary>
        /// 获取冀东周旬月表格数据
        /// </summary>
        private void getJDNomalTableData(NineteenNomalModel nomalModel, string[,] tableNormal)
        {
            List<NineteenNomalLineModel> list = new List<NineteenNomalLineModel>();
            var row = tableNormal.GetLength(0);
            NineteenNomalLineModel nineteenNomalLineModel = new NineteenNomalLineModel();

            nineteenNomalLineModel.NAME = tableNormal[1, 0].ToString();
            nineteenNomalLineModel.TERMINALLINE = tableNormal[1, 1].ToString();
            nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[1, 2] == null ? nineteenNomalLineModel.GENERALICETHICKNESS = "" : nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[1, 2].ToString();
            nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[1, 3] == null ? nineteenNomalLineModel.MAXICETHICKNESS = "" : nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[1, 3].ToString();
            list.Add(nineteenNomalLineModel);
            NineteenNomalLineModel nineteenNomalLineModel1 = new NineteenNomalLineModel();

            nineteenNomalLineModel1.NAME = tableNormal[3, 0].ToString();
            nineteenNomalLineModel1.TERMINALLINE = tableNormal[4, 1].ToString();
            nineteenNomalLineModel1.GENERALICETHICKNESS = tableNormal[4, 2] == null ? nineteenNomalLineModel.GENERALICETHICKNESS = "" : nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[4, 2].ToString();
            nineteenNomalLineModel1.MAXICETHICKNESS = tableNormal[4, 3] == null ? nineteenNomalLineModel.MAXICETHICKNESS = "" : nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[4, 3].ToString();
            list.Add(nineteenNomalLineModel1);

            nomalModel.NineteenNomalLine = list;
        }


        /// <summary>
        /// 获取周旬月表格数据
        /// </summary>
        private void getNomalTableData(NineteenNomalModel nomalModel, string[,] tableNormal)
        {
            List<NineteenNomalLineModel> list = new List<NineteenNomalLineModel>();
            var row = tableNormal.GetLength(0);
            //var col = tableNormal.GetLength(1);
            for (int i = 1; i < row; i++)
            {
                NineteenNomalLineModel nineteenNomalLineModel = new Model.NineteenWord.NineteenNomalLineModel();
                nineteenNomalLineModel.NAME = tableNormal[i, 0].ToString();
                nineteenNomalLineModel.TERMINALLINE = tableNormal[i, 1].ToString();
                nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[i, 2] == null ? nineteenNomalLineModel.GENERALICETHICKNESS = "" : nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[i, 2].ToString();
                nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[i, 3] == null ? nineteenNomalLineModel.MAXICETHICKNESS = "" : nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[i, 3].ToString();
                //nomalModel.NineteenNomalLine.Add(nineteenNomalLineModel);
                list.Add(nineteenNomalLineModel);
            }
            nomalModel.NineteenNomalLine = list;
        }

        /// <summary>
        /// 获取年表格数据
        /// </summary>
        /// <param name="yearModel"></param>
        /// <param name="table_str_1">NineteenYearICEModel  </param>
        /// <param name="table_str_2">NineteenYearLineModel</param>
        /// <param name="table_str_3">NineteenYearCknessModel</param>
        private void getYearTableData(NineteenYearModel yearModel, string[,] table_str_1, string[,] table_str_2, string[,] table_str_3)
        {
            List<NineteenYearICEModel> iceList = new List<NineteenYearICEModel>();
            List<NineteenYearLineModel> lineList = new List<NineteenYearLineModel>();
            List<NineteenYearCknessModel> cknessList = new List<NineteenYearCknessModel>();
            //渤海及黄海北部冰日预测
            var icerow = table_str_1.GetLength(0);
            for (int i = 1; i < icerow; i++)
            {
                NineteenYearICEModel iceModel = new NineteenYearICEModel();
                iceModel.NAME = table_str_1[i, 0].ToString();
                iceModel.FIRSTFROZENDAY = table_str_1[i, 1].ToString();
                iceModel.SERIOUSICE = table_str_1[i, 2].ToString();
                iceModel.ICEMELTINGDAY = table_str_1[i, 3].ToString();
                iceModel.ICEDISAPPDAY = table_str_1[i, 4].ToString();
                iceList.Add(iceModel);
            }
            yearModel.nineteenYearICEModel = iceList;
            //浮冰外缘线离岸最大距离及平整冰厚度预测
            var linerow = table_str_2.GetLength(0);
            for (int j = 1; j < linerow; j++)
            {
                NineteenYearLineModel lineModel = new Model.NineteenWord.NineteenYearLineModel();
                lineModel.NAME = table_str_2[j, 0].ToString();
                lineModel.TERMINALLINE = table_str_2[j, 1].ToString();
                lineModel.GENERALICETHICKNESS = table_str_2[j, 2].ToString();
                lineModel.MAXICETHICKNESS = table_str_2[j, 3].ToString();
                lineList.Add(lineModel);
            }
            yearModel.nineteenYearLineModel = lineList;
            //严重冰期沿岸主要港口与海岛平整冰厚度预测
            var cknessrow = table_str_3.GetLength(0);
            var cknesscol = table_str_3.GetLength(1);
            for (int g = 1; g < cknesscol; g++)
            {
                NineteenYearCknessModel cknessModel = new Model.NineteenWord.NineteenYearCknessModel();
                cknessModel.NAME = table_str_3[0, g].ToString();
                cknessModel.GENERALICETHICKNESS = table_str_3[1, g].ToString();
                cknessModel.MAXICETHICKNESS = table_str_3[2, g].ToString();
                cknessList.Add(cknessModel);
            }
            yearModel.nineteenYearCknessModel = cknessList;
        }

        /// <summary>
        /// bitMap转byte[]
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] Bitmap2Byte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }


        /// <summary>
        /// 消息word入库
        /// </summary>
        /// <param name="file">对应word文档</param>
        /// <param name="filepath">word文档路径</param>
        /// <param name="strone">word文档文件名</param>
        private int YUBAOHBruku(string file, string filepath, string strone)
        {
            int a1 = 0;
            CG_YUBAO_FILE tbl = new CG_YUBAO_FILE();
            tbl.YBWENJIANMING = strone;
            byte[] byFile;

            //word文档转二进制
            if (System.IO.File.Exists(file))
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                byFile = new byte[fs.Length];
                fs.Read(byFile, 0, byFile.Length);
                fs.Close();
                tbl.YBNEIRONG = byFile;
            }
            else
            {
                return 0;
            }
            //word文档生成图片转二进制
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/Images/YUBAOhbImg");
            PredicTable.ExportWord.JingBao.Word word = new ExportWord.JingBao.Word();
            word.WordToImage(strone, filepath, imagepath, strone.Split('.')[0], ImageFormat.Png, 2);

            byte[] byfileimg;
            if (System.IO.File.Exists(imagepath + "/" + strone.Split('.')[0] + ".png"))
            {
                FileStream fs = new FileStream(imagepath + "/" + strone.Split('.')[0] + ".png", FileMode.Open);
                byfileimg = new byte[fs.Length];
                fs.Read(byfileimg, 0, byfileimg.Length);
                fs.Close();
                tbl.PICFILE = byfileimg;
            }
            else
            {
                return 0;
            }

            //判断数据库是不是有当前word文档
            List<int> a = new List<int>();
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new sql_CG_YUBAO_FILE().get_YuBaoFILE_AllData(tbl);
            sql_CG_YUBAO_FILE content = new sql_CG_YUBAO_FILE();
            if (tblybddocument.Rows.Count > 0)
            {
                //编辑
                a1 = content.Update_CG_YUBAO_FILE(tbl);

            }
            else
            {
                //添加
                a1 = content.Add_CG_YUBAO_FILE(tbl);

            }
            return a1;
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