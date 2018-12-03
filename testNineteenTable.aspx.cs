using PredicTable.Dal;
using PredicTable.ExportWord.NineteenWord;
using PredicTable.Model.NineteenWord;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class testNineteenTable : System.Web.UI.Page
    {
        /// <summary>
        /// 19号预报单
        /// 1、pageoffice将word保存到本地
        /// 2、将发送单位保存到Word
        /// 3、保存表格、图片、发送单位入库
        /// 4、保存word入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["userid"] != null)
                {
                    string userid = Session["userid"].ToString();
                    if (Session["op19table"] != null) {
                        string docname = Session["op19table"].ToString();
                        var filePath = System.Web.HttpContext.Current.Server.MapPath("/scword/" + docname);
                        #region Word 本 地 保 存
                        //
                        //======================1、word-保存文件到本地======================
                        if (File.Exists(filePath))
                            System.IO.File.Delete(docname);//如果文件存在，先删除再保存
                        PageOffice.FileSaver fs = new PageOffice.FileSaver();
                        fs.SaveToFile(filePath);
                        fs.Close();

                        //
                        //======================2、将发送单位保存到Word======================
                        if (Session["SendUnit"] != null) {
                            string unitName = Session["SendUnit"].ToString();
                            AddUnitToLabel addUnitToLabel = new AddUnitToLabel();
                            addUnitToLabel.AddUnit(filePath, Session["SendUnit"].ToString());
                        }
                        #endregion


                        //=====================入库===============================
                        //获取文件时效-分类保存
                        sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();
                        var effection = this.getSX(docname);
                        //Word流文件保存
                        NineteenNomalFileModel nomalFileModel = new Model.NineteenWord.NineteenNomalFileModel();
                        if (effection == "年")
                        {
                            #region 年
                            SaveNineteenYearTables nineteenYearTables = new SaveNineteenYearTables(filePath);
                            NineteenYearModel yearModel = new NineteenYearModel();
                            nineteenYearTables.assignment();
                            //nineteenYearTables.assignmentTable();
                            string[,] table_str_1 = nineteenYearTables.table_str_1;
                            string[,] table_str_2 = nineteenYearTables.table_str_2;
                            string[,] table_str_3 = nineteenYearTables.table_str_3;
                            string[,] table_xml_1 = nineteenYearTables.table_xml_1;
                            string[,] table_xml_2 = nineteenYearTables.table_xml_2;
                            string[,] table_xml_3 = nineteenYearTables.table_xml_3;
                            //保存文件中表格NineteenYearModel
                            this.getYearTableData(yearModel, table_str_1, table_str_2, table_str_3, table_xml_1, table_xml_2, table_xml_3);
                            //图片
                            yearModel.BMP = Bitmap2Byte(nineteenYearTables.bmp);
                            //其他字段
                            yearModel.PUBLISHDATE = nineteenYearTables.post_time;
                            yearModel.ICESITUATION = nineteenYearTables.ice_situation;
                            yearModel.PREDICT = nineteenYearTables.predict;
                            yearModel.DESCRIPTION = nineteenYearTables.description;
                            //发送单位
                            if (Session["SendUnit"] != null)
                            {
                                string unitName = Session["SendUnit"].ToString();
                                yearModel.SENDUNIT = unitName;
                            }
                            //入库
                            int yearkey = sql_nineteenTable.GetNomalYearTableKey(yearModel);
                            if (yearkey != 0)
                            {
                                int tableresult = sql_nineteenTable.SaveYearTable(yearModel);
                                if (tableresult != 0)
                                {
                                    if(yearModel.nineteenYearICEModel.Count > 0)
                                    {
                                        for (int i = 0; i < yearModel.nineteenYearICEModel.Count; i++)
                                        {
                                            sql_nineteenTable.SaveYearTableIce(yearModel.nineteenYearICEModel[i], yearModel);
                                        }
                                    }
                                    if (yearModel.nineteenYearLineModel.Count > 0)
                                    {
                                        for (int i = 0; i < yearModel.nineteenYearLineModel.Count; i++)
                                        {
                                            sql_nineteenTable.SaveYearTableLine(yearModel.nineteenYearLineModel[i], yearModel);
                                        }
                                    }
                                    if (yearModel.nineteenYearCknessModel.Count > 0)
                                    {
                                        for (int i = 0; i < yearModel.nineteenYearCknessModel.Count; i++)
                                        {
                                            sql_nineteenTable.SaveYearTableCkness(yearModel.nineteenYearCknessModel[i], yearModel);
                                        }
                                    }
                                }
                            }
                            nomalFileModel.PUBLISHDATE = yearModel.PUBLISHDATE;
                            nomalFileModel.FILETYPE = "001year";
                            #endregion
                        }
                        else {
                            #region  周、旬、月 数 据 处 理
                            SaveNineteenTables nineteenTables = new SaveNineteenTables(filePath);
                            nineteenTables.assignmentPredict_aging();
                            nineteenTables.assignment();
                            //获取表格数据
                            string[,] tableNormal = nineteenTables.table_str;
                            string[,] tableNormalXml = nineteenTables.table_xml;
                            //文件中表格处理
                            NineteenNomalModel nomalModel = new NineteenNomalModel();
                            this.getNomalTableData(nomalModel, tableNormal, tableNormalXml);//nineteenNomalLineModel,
                            //图片
                            nomalModel.BMP = nineteenTables.bmp == null?nomalModel.BMP=null:Bitmap2Byte(nineteenTables.bmp);
                            //其他字段
                            nomalModel.PUBLISHDATE = nineteenTables.post_time;
                            nomalModel.ICESITUATION = nineteenTables.ice_situation;
                            nomalModel.PREDICT = nineteenTables.predict;
                            nomalModel.PREDICTAGING = nineteenTables.predict_aging;
                            nomalModel.DESCRIPTION = nineteenTables.description;
                            //发送单位
                            if (Session["SendUnit"] != null)
                            {
                                string unitName = Session["SendUnit"].ToString();
                                nomalModel.SENDUNIT = unitName;
                            }
                            //入库
                            int key = sql_nineteenTable.GetNomalTableKey(nomalModel);
                            if(key != 0)
                            {
                                int tableresult=sql_nineteenTable.SaveNomalTable(nomalModel);
                                if (tableresult != 0 && nomalModel.NineteenNomalLine.Count > 0)
                                {
                                    for (int i = 0; i < nomalModel.NineteenNomalLine.Count; i++) {
                                        sql_nineteenTable.SaveNomalTableLine(nomalModel.NineteenNomalLine[i], nomalModel);
                                    }
                                }
                            }
                            nomalFileModel.PUBLISHDATE = nomalModel.PUBLISHDATE;
                            nomalFileModel.FILETYPE = nomalModel.PREDICTAGING;
                            #endregion
                        }
                        //入库-word流文件
                        
                        int fileKey = sql_nineteenTable.GetNomalTableFileKey(nomalFileModel);
                        if (fileKey != 0)
                        {
                            FileStream fStream = File.OpenRead(filePath);
                            byte[] b = new byte[fStream.Length];
                            fStream.Read(b, 0, b.Length);
                            fStream.Close();
                            nomalFileModel.FILENAME = docname;
                            nomalFileModel.FILEFLOW = b;
                            
                            //TODO:
                            //表单存入数据库
                            sql_nineteenTable.SaveFile(nomalFileModel);
                        }
                        //string text = Session["templateFileName"].ToString() + "(" + docname + ")" + "保存成功！";
                        //string reponse = "<script  language='javascript' type='text/javascript'>"
                        //        + "$(function(){"
                        //        + "     alert('"+ text + "');"
                        //        + "});"
                        //        + "</script>";
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", reponse);
                    }
                }
            }
            catch (Exception error)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取文件时效
        /// </summary>
        private string getSX(string docname)
        {
            var docInfoArr = docname.Split('_');
            var effection = "";
            if (docInfoArr.Length == 6)
            {
                switch (docInfoArr[3])
                {
                    case "7day":
                        effection = "周";
                        break;
                    case "10day":
                        effection = "旬";
                        break;
                    case "1mon":
                        effection = "月";
                        break;
                    case "1yr":
                        effection = "年";
                        break;
                    default: break;
                }
            }
            return effection;
        }

        /// <summary>
        /// 获取周旬月表格数据
        /// </summary>
        private void getNomalTableData(NineteenNomalModel nomalModel,string[,] tableNormal, string[,] tableNormalXml)// NineteenNomalLineModel nineteenNomalLineModel,
        {
            List<NineteenNomalLineModel> list = new List<NineteenNomalLineModel>();
            var row = tableNormal.GetLength(0);
            //var col = tableNormal.GetLength(1);
            for (int i = 1; i < row; i++) {
                NineteenNomalLineModel nineteenNomalLineModel = new Model.NineteenWord.NineteenNomalLineModel();
                nineteenNomalLineModel.SITE = tableNormalXml[i-1, 0].ToString();
                nineteenNomalLineModel.NAME = tableNormal[i, 0].ToString();
                nineteenNomalLineModel.TERMINALLINE = tableNormal[i, 1].ToString();
                nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[i,  2]==null? nineteenNomalLineModel.GENERALICETHICKNESS ="": nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[i, 2].ToString();
                nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[i,  3] == null ? nineteenNomalLineModel.MAXICETHICKNESS="": nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[i, 3].ToString();
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
        private void getYearTableData(NineteenYearModel yearModel, string[,] table_str_1, string[,] table_str_2, string[,] table_str_3, string[,] table_xml_1, string[,] table_xml_2, string[,] table_xml_3)
        {
            List<NineteenYearICEModel> iceList = new List<NineteenYearICEModel>();
            List<NineteenYearLineModel> lineList = new List<NineteenYearLineModel>();
            List<NineteenYearCknessModel> cknessList = new List<NineteenYearCknessModel>();
            //渤海及黄海北部冰日预测
            var icerow = table_str_1.GetLength(0);
            for(int i = 1; i < icerow; i++)
            {
                NineteenYearICEModel iceModel = new NineteenYearICEModel();
                iceModel.SITE = table_xml_1[i-1, 0].ToString();
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
                lineModel.SITE= table_xml_2[j-1, 0].ToString();
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
                cknessModel.SITE = table_xml_3[g-1, 0].ToString();
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
    }
}