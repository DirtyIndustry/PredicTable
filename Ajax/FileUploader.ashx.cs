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
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace PredicTable.Ajax
{
    /// <summary>
    /// 海冰文件上传
    /// </summary>
    public class FileUploader : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            
            if (context.Request["method"] != null)
            {
                string method = context.Request["method"].ToString();

                switch (method)
                {
                    case "DeleteFile": DeleteFile(context); break;
                    default:
                        break;
                }
            }
            else
            {
                if (context.Request["REQUEST_METHOD"] == "OPTIONS")
                {
                    context.Response.End();
                }
                UploadFile(context);
            }
        }


        void UploadFile(HttpContext context)
        {

            string filePathName = string.Empty;
            string id = context.Request["id"].ToString();
            string name = context.Request["name"].ToString();
            string type = context.Request["type"].ToString();
            string lastModifiedDate = context.Request["lastModifiedDate"].ToString();
            string size = context.Request["size"].ToString();
            string FaWangbz = context.Request["FaWangbz"].ToString().Trim(',');//发往备注
            
            HttpPostedFile file = context.Request.Files["file"];
            
            #region 文件保存到路径下
            var jsonData = new object();
            //检查上传文件
            if (context.Request.Files.Count == 0)
            {
                jsonData = new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" };
              
                context.Response.Write(jsonData);
            }

            string path = "预报单共享\\zcq\\" + System.DateTime.Now.ToString("yyyyMMdd");//默认文件保存的路径
            string myselfpPath = context.Request["myselfpPath"];//指定路径
            if (myselfpPath != null && myselfpPath != "")
            {
                path = myselfpPath;
            }

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, path);
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = file.FileName;
            //文件保存到指定路径下
            file.SaveAs(Path.Combine(localPath, filePathName));
            path = AppDomain.CurrentDomain.BaseDirectory + path;

            DataTable dt = new DataTable();
            dt.Columns.Add("jsonrpc", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("fileName", typeof(string));
            dt.Columns.Add("filePath", typeof(string)); 
            DataRow dr = dt.NewRow();
            dr["jsonrpc"] = "2.0";
            dr["id"] = id;
            dr["fileName"] = filePathName;
            dr["filePath"] = path + filePathName;
            dt.Rows.Add(dr);
            var dataJson = JsonMore.Serialize(dt);
            context.Response.Write(dataJson);
            #endregion

            #region 文件名判断
            DateTime time = DateTime.Now;
            string fawang = "";
            string returnStr = "";
            string WainArea = "";
            string ListType = "";
            string yubaodanwei = "";
            string haibing = "";
            //文件名格式判断
            if (filePathName.Split('.')[1] == "docx")
            {
                filePathName = filePathName.Split('.')[0] + ".doc";
            }
            if (filePathName.Contains("NCS"))
            {
                yubaodanwei = "北海预报中心";
                WainArea = "北海区";
                haibing = "海冰";
                if (filePathName.Contains("7day"))
                {
                    ListType = "周"; 
                }
                if (filePathName.Contains("10day"))
                {
                    ListType = "旬";
                }
                if (filePathName.Contains("1mon"))
                {
                    ListType = "月";
                }
                if (filePathName.Contains("1yr"))
                {
                    ListType = "年";
                }
            }
            if (filePathName.Contains("SD"))
            {
                yubaodanwei = "山东省海洋预报台";
                WainArea = "山东近海";
                haibing = "海冰";
                if (filePathName.Contains("10day"))
                {
                    ListType = "旬";
                }
                if (filePathName.Contains("1mon"))
                {
                    ListType = "月";
                }
                if (filePathName.Contains("1yr"))
                {
                    ListType = "年";
                }
                if (filePathName.Contains("7day"))
                {
                    ListType = "周";
                }
            }
            if (filePathName.Contains("东营"))
            {
                yubaodanwei = "北海预报中心";
                WainArea = "东营近海";
                haibing = "东营海冰";
                if (filePathName.Contains("旬"))
                {
                    ListType = "旬";
                }
                if (filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "月";
                }
                if (filePathName.Contains("年") && !filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "年";
                }
                if (filePathName.Contains("周"))
                {
                    ListType = "周";
                }
            }
            if (filePathName.Contains("冀东"))
            {
                yubaodanwei = "北海预报中心";
                WainArea = "冀东油田";
                haibing = "冀东海冰";
                if (filePathName.Contains("旬"))
                {
                    ListType = "旬";
                }
                if (filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "月";
                }
                if (filePathName.Contains("年") && !filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "年";
                }
            }
            if (filePathName.Contains("胜利"))
            {
                yubaodanwei = "北海预报中心";
                WainArea = "东营胜利油田";
                haibing = "胜利海冰";
                if (filePathName.Contains("旬"))
                {
                    ListType = "旬";
                }
                if (filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "月";
                }
                if (filePathName.Contains("年") && !filePathName.Contains("月") && !filePathName.Contains("旬"))
                {
                    ListType = "年";
                }
                if (filePathName.Contains("周"))
                {
                    ListType = "周";
                }
            }
            if (filePathName.Contains("青岛"))
            {
                yubaodanwei = "北海预报中心";
                WainArea = "青岛近海";
                ListType = "周";
                haibing = "青岛海冰";
            }
            #endregion
            
            #region 海冰
            try
            {
                #region 预报表文件属性

                CG_YUBAO_ME yubaomi = new CG_YUBAO_ME();
                yubaomi.YBWENJIANMING = filePathName;

                yubaomi.YBQUYU = WainArea;

                yubaomi.YBNEIRONG = haibing;
                yubaomi.YBSHIXIAO = ListType;
                yubaomi.YBSHIJIAN = time;

                yubaomi.YBDANWEI = yubaodanwei;

                //消息文件属性数据保存

                var sqlConME = new sql_CG_YUBAO_ME();
                CG_YUBAO_ME tblME = new Model.CG_YUBAO_ME();
                tblME.YBWENJIANMING = filePathName;

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
                    returnStr += filePathName + " 预报文件属性数据提交失败。 ";
                }
                else
                {
                    // returnStr += filePathName + " 预报文件属性数据提交成功。 ";
                }
                #endregion

                #region 预报表文件
                try
                {
                    //预报文件保存到数据库
                    int flag1 = YUBAOHBruku(path + "\\" + filePathName, path, filePathName);

                    if (flag1 == 0)
                    {
                        returnStr = returnStr + "  " + filePathName + " word插入预报文件表数据库失败!";
                    }
                    if (flag1 == 1)
                    {
                        // returnStr = returnStr + "  " + filePathName + " word插入预报文件表数据库成功!";
                    }
                }
                catch (Exception e)
                {
                    WriteLog.Write("预报单出错" + e.ToString());
                    returnStr = returnStr + "出错了！" + e.ToString();
                }
                context.Response.Write(returnStr);

                #endregion

                if (WainArea == "冀东油田")
                {
                    #region 冀东油田
                   
                    sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
                    var effection = ListType;//文件类型（年月旬）
                    //Word流文件保存
                    NineteenNomalFileModel nomalFileModel = new Model.NineteenWord.NineteenNomalFileModel();
                    
                    //获取文件时效-分类保存
                    if (effection == "年")
                    {
                        #region 年
                        //打开word，解析
                        SaveNineteenTablesJDYear nineteenYearTables = new SaveNineteenTablesJDYear(path + "\\" + filePathName);
                        NineteenYearModel yearModel = new NineteenYearModel();
                        nineteenYearTables.assignment();
                        string[,] tableNormal = nineteenYearTables.table_str;
                        if (tableNormal != null)
                        {
                            //保存文件中表格NineteenYearModel
                            this.getJDYearTableData(yearModel, tableNormal);
                        }
                       
                        //其他字段
                        yearModel.ID = filePathName;
                        yearModel.PUBLISHDATE = nineteenYearTables.post_time;
                        yearModel.ICESITUATION = nineteenYearTables.ice_situation;
                        yearModel.PREDICT = nineteenYearTables.predict;
                        yearModel.DESCRIPTION = nineteenYearTables.description;
                        yearModel.FASONGDANWEI = nineteenYearTables.senddanwei;//发送单位
                        yearModel.SENDUNIT = this.GetSendUnit("中长期冀东海冰专项");//nineteenYearTables.fawang;//发往
                        fawang = this.GetSendUnit("中长期冀东海冰专项"); //nineteenYearTables.fawang;

                        //入库
                        int yearkey = sql_nineteenTable.GetYearKey(yearModel);//.GetNomalYearTableKey(yearModel);
                        if (yearkey == 0)
                        {   //保存年内容
                            int tableresult = sql_nineteenTable.SaveYearTable(yearModel);
                            if (tableresult != 0)
                            {//保存年表格数据
                              
                                if (yearModel.nineteenYearICEModel!=null && yearModel.nineteenYearICEModel.Count > 0)
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
                        SaveNineteenTablesJD nineteenTables = new SaveNineteenTablesJD(path + "\\" + filePathName);
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
                        nomalModel.ID = filePathName;
                        nomalModel.PUBLISHDATE = nineteenTables.post_time;
                        nomalModel.ICESITUATION = nineteenTables.ice_situation;
                        nomalModel.PREDICT = nineteenTables.predict;
                        nomalModel.PREDICTAGING = nineteenTables.predict_aging;
                        nomalModel.DESCRIPTION = nineteenTables.description;
                        nomalModel.FASONGDANWEI = nineteenTables.fawang;//发往
                        nomalModel.SENDUNIT = this.GetSendUnit("中长期冀东海冰专项");
                        fawang = this.GetSendUnit("中长期冀东海冰专项"); //nineteenTables.fawang;
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
                            returnStr += filePathName + " 周、旬、月内容数据提交失败。 ";
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

                    FileStream fStream = File.OpenRead(path + "\\" + filePathName);
                    byte[] b = new byte[fStream.Length];
                    fStream.Read(b, 0, b.Length);
                    fStream.Close();
                    nomalFileModel.FILENAME = filePathName;
                    nomalFileModel.FILEFLOW = b;
                    int a = 0;
                    NineteenNomalFileModel model = new Model.NineteenWord.NineteenNomalFileModel();
                    model.FILENAME = filePathName;
                    int fileKey = sql_nineteenTable.GetFile(model);
                    if (fileKey > 0)
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
                        returnStr += filePathName + "入库失败。";
                    }

                    #endregion
                    #endregion
                }
                else
                {
                    #region 北海区 山东近海  东营胜利油田 东营近海 青岛

                    //获取文件时效-分类保存
                    sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
                    var effection = ListType;//文件类型（年月旬日）
                    //Word流文件保存
                    NineteenNomalFileModel nomalFileModel = new Model.NineteenWord.NineteenNomalFileModel();

                    if (effection == "年")
                    {
                        #region 年
                        SaveNineteenYearTables nineteenYearTables = new SaveNineteenYearTables(path + "\\" + filePathName);
                        NineteenYearModel yearModel = new NineteenYearModel();
                        nineteenYearTables.assignment();
                        string[,] table_str_1 = nineteenYearTables.table_str_1;
                        string[,] table_str_2 = nineteenYearTables.table_str_2;
                        string[,] table_str_3 = nineteenYearTables.table_str_3;

                        if (table_str_1 != null&& table_str_2!=null&& table_str_3!=null)
                        {
                            //保存文件中表格NineteenYearModel
                            this.getYearTableData(yearModel, table_str_1, table_str_2, table_str_3);
                        }
           
                        //其他字段
                        yearModel.ID = filePathName;
                        yearModel.PUBLISHDATE = nineteenYearTables.post_time;
                        time = nineteenYearTables.post_time;
                        yearModel.ICESITUATION = nineteenYearTables.ice_situation;
                        yearModel.PREDICT = nineteenYearTables.predict;
                        yearModel.DESCRIPTION = nineteenYearTables.description;
                        yearModel.CHUANZHEN = nineteenYearTables.chuanzhen;
                        yearModel.IPHONE = nineteenYearTables.tel;
                        yearModel.LINKMAN = nineteenYearTables.link;
                        yearModel.FASONGDANWEI = nineteenYearTables.seddanwei;//发送单位
                        if (filePathName.Contains("胜利"))
                        {
                            yearModel.SENDUNIT = this.GetSendUnit("中长期胜利海冰专项");
                            fawang = this.GetSendUnit("中长期胜利海冰专项"); //nineteenYearTables.fawang;
                        }
                        else if(filePathName.Contains("东营")){
                            yearModel.SENDUNIT = this.GetSendUnit("中长期东营海冰专项");
                            fawang = this.GetSendUnit("中长期东营海冰专项"); //nineteenYearTables.fawang;
                        }
                        else if (filePathName.Contains("青岛"))
                        {
                            yearModel.SENDUNIT = this.GetSendUnit("中长期青岛海冰专项");
                            //nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = this.GetSendUnit("中长期青岛海冰专项"); //nineteenTables.fawang;
                        }
                        else if (filePathName.Contains("YB_NCS_HB"))
                        {
                            yearModel.SENDUNIT = this.GetSendUnit("中长期北海海冰");
                            //nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = this.GetSendUnit("中长期北海海冰"); //nineteenTables.fawang;
                        }
                        else if (filePathName.Contains("YB_SD_HB_"))
                        {
                            yearModel.SENDUNIT = this.GetSendUnit("中长期山东海冰");
                            //nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = this.GetSendUnit("中长期山东海冰"); //nineteenTables.fawang;
                        }
                        else
                        {
                            yearModel.SENDUNIT = nineteenYearTables.fawang;//发往
                            fawang = nineteenYearTables.fawang;
                        }
                        //判断数据库是不是有当前数据
                        yearModel.ID = filePathName;
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
                            returnStr += filePathName + " 年内容数据提交失败。 ";
                        }
                        else
                        {
                            //returnStr += filePathName + " 年内容数据提交成功。 ";
                        }

                        if (tableresult != 0)
                        {//保存年表格数据
                            if (yearModel.nineteenYearICEModel!=null&&yearModel.nineteenYearICEModel.Count > 0)
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
                            if (yearModel.nineteenYearLineModel!=null&&yearModel.nineteenYearLineModel.Count > 0)
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
                            if (yearModel.nineteenYearCknessModel!=null&&yearModel.nineteenYearCknessModel.Count > 0)
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
                        SaveNineteenTables nineteenTables = new SaveNineteenTables(path + "/" + filePathName);
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
                        nomalModel.ID = filePathName;
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
                        if (filePathName.Contains("胜利"))
                        {
                            nomalModel.SENDUNIT = this.GetSendUnit("中长期胜利海冰专项");
                            //nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = this.GetSendUnit("中长期胜利海冰专项"); //nineteenTables.fawang;
                        }
                        else if (filePathName.Contains("东营"))
                        {
                            nomalModel.SENDUNIT = this.GetSendUnit("中长期东营海冰专项");
                            //nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = this.GetSendUnit("中长期东营海冰专项"); //nineteenTables.fawang;
                        }
                        else if (filePathName.Contains("青岛"))
                        {
                            nomalModel.SENDUNIT = this.GetSendUnit("中长期青岛海冰专项");
                            //nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = this.GetSendUnit("中长期青岛海冰专项"); //nineteenTables.fawang;
                        }
                        else if (filePathName.Contains("YB_NCS_HB"))
                        {
                            nomalModel.SENDUNIT = this.GetSendUnit("中长期北海海冰");
                            //nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = this.GetSendUnit("中长期北海海冰"); //nineteenTables.fawang;
                        }
                        else if (filePathName.Contains("YB_SD_HB_"))
                        {
                            nomalModel.SENDUNIT = this.GetSendUnit("中长期山东海冰");
                            //nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = this.GetSendUnit("中长期山东海冰"); //nineteenTables.fawang;
                        }
                        else
                        {
                            nomalModel.SENDUNIT = nineteenTables.fawang;//发往
                            fawang = nineteenTables.fawang;
                        }
                        //入库
                        nomalModel.ID = filePathName;
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
                            returnStr += filePathName + " 周、旬、月内容数据提交失败。 ";
                        }
                        else
                        {
                            //returnStr += filePathName + " 周、旬、月内容数据提交成功。 ";
                        }

                        if (tableresults != 0 && nomalModel.NineteenNomalLine!=null && nomalModel.NineteenNomalLine.Count > 0)
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

                    FileStream fStream = File.OpenRead(path + "\\" + filePathName);
                    byte[] b = new byte[fStream.Length];
                    fStream.Read(b, 0, b.Length);
                    fStream.Close();
                    nomalFileModel.FILENAME = filePathName;
                    nomalFileModel.FILEFLOW = b;
                    int a = 0;
                    NineteenNomalFileModel model = new Model.NineteenWord.NineteenNomalFileModel();
                    model.FILENAME = filePathName;
                    int fileKey = sql_nineteenTable.GetFile(model);
                    if (fileKey > 0)
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
                        returnStr += filePathName + "入库失败。";
                    }

                    #endregion
                    #endregion

                }

                //发往保存
                CommonSendUnit sunit = new CommonSendUnit();
                string ret = sunit.resultSendUnitbz(filePathName, fawang, FaWangbz);
            }
            catch (Exception e)
            {
                WriteLog.Write("海冰预报入库出错" + e.ToString());
                returnStr = returnStr + e.ToString();
            }
            context.Response.Write(returnStr);

            #endregion


        }

        private void getJDYearTableData(NineteenYearModel yearModel,string [,] table_str)
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
        /// 预报word文件入库
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
            if (table_str_1 == null)
            { return; }
            var icerow = table_str_1.GetLength(0);
            for (int i = 1; i < icerow; i++)
            {
                NineteenYearICEModel iceModel = new NineteenYearICEModel();
                iceModel.NAME = table_str_1[i, 0] != null ? table_str_1[i, 0].ToString() : "";
                iceModel.FIRSTFROZENDAY = table_str_1[i, 1] != null ? table_str_1[i, 1].ToString() : "";
                iceModel.SERIOUSICE = table_str_1[i, 2] != null ? table_str_1[i, 2].ToString() : "";
                iceModel.ICEMELTINGDAY = table_str_1[i, 3] != null ? table_str_1[i, 3].ToString() : "";
                iceModel.ICEDISAPPDAY = table_str_1[i, 4] != null ? table_str_1[i, 4].ToString() : "";
                iceList.Add(iceModel);
            }
            yearModel.nineteenYearICEModel = iceList;
            //浮冰外缘线离岸最大距离及平整冰厚度预测
            var linerow = table_str_2.GetLength(0);
            for (int j = 1; j < linerow; j++)
            {
                NineteenYearLineModel lineModel = new Model.NineteenWord.NineteenYearLineModel();

                lineModel.NAME = table_str_2[j, 0] != null ? table_str_2[j, 0].ToString() : "";
                lineModel.TERMINALLINE = table_str_2[j, 1] != null ? table_str_2[j, 1].ToString() : "";
                lineModel.GENERALICETHICKNESS = table_str_2[j, 2] != null ? table_str_2[j, 2].ToString():"" ;
                lineModel.MAXICETHICKNESS = table_str_2[j, 3] != null ? table_str_2[j, 3].ToString() : "";
                lineList.Add(lineModel);
            }
            yearModel.nineteenYearLineModel = lineList;
            //严重冰期沿岸主要港口与海岛平整冰厚度预测
            var cknessrow = table_str_3.GetLength(0);
            var cknesscol = table_str_3.GetLength(1);
            for (int g = 1; g < cknesscol; g++)
            {
                NineteenYearCknessModel cknessModel = new Model.NineteenWord.NineteenYearCknessModel();

                cknessModel.NAME = table_str_3[0, g] != null ? table_str_3[0, g].ToString() : "";
                cknessModel.GENERALICETHICKNESS = table_str_3[1, g] != null ? table_str_3[1, g].ToString() : "";
                cknessModel.MAXICETHICKNESS = table_str_3[2, g] != null ? table_str_3[2, g].ToString() : "";
                cknessList.Add(cknessModel);
            }
            yearModel.nineteenYearCknessModel = cknessList;
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

            nineteenNomalLineModel1.NAME = tableNormal[2, 0].ToString();
            nineteenNomalLineModel1.TERMINALLINE = tableNormal[2, 1].ToString();
            nineteenNomalLineModel1.GENERALICETHICKNESS = tableNormal[2, 2] == null ? nineteenNomalLineModel.GENERALICETHICKNESS = "" : nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[2, 2].ToString();
            nineteenNomalLineModel1.MAXICETHICKNESS = tableNormal[2, 3] == null ? nineteenNomalLineModel.MAXICETHICKNESS = "" : nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[2, 3].ToString();
            list.Add(nineteenNomalLineModel1);
            
            nomalModel.NineteenNomalLine = list;
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

        void DeleteFile(HttpContext context)
        {
            string pathfileName = context.Request["fileName"].ToString();
            try
            {
                string path = "预报单共享\\duanqi\\";//默认文件保存的路径
                string fullName = Path.Combine(HttpRuntime.AppDomainAppPath + path, pathfileName);

                if (System.IO.File.Exists(fullName))
                {
                    System.IO.File.Delete(fullName);
                }
                context.Response.Write("删除成功。");
            }
            catch (Exception e)
            {
                context.Response.Write("删除失败。" + e.Message);

            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private string GetSendUnit(string unit)
        {
            try
            {
                Sql_BHHBSendUnit hbsendUnit = new Sql_BHHBSendUnit();
                DataTable hbunit = (DataTable)hbsendUnit.GetHBSendUnits(unit);
                string unitStr = "";
                if (hbunit != null && hbunit.Rows.Count > 0)
                {
                    foreach (DataRow item in hbunit.Rows)
                    {
                        unitStr += item["USERNAME"].ToString() + ";";
                    }
                    unitStr = unitStr.Substring(0, unitStr.Length-1);
                }
                return unitStr;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}