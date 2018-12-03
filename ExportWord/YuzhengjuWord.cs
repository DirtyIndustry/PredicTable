using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;
/*
变更记录1
变更时间：2018.8.30  
变更内容：将青岛近海预报24h改为预报3天

变更人员：Lian     
*/

namespace PredicTable.ExportWord
{
    public class YuzhengjuWord
    {

        //发布时间
        string PUBDTTIME = "";
        //日 期
        string[] FORECASTDATEArr = new string[6];

        //列名
        //天气现象 --> WEATHERAPPEARANCE
        //风向（方位）--> WINDDIRECTION
        //风速（级）--> WINDFORCE
        //波高（m）--> WAVEHEIGHT
        //波向（方位）--> WAVEDIRECTION


        string[] colomns = { "WEATHERAPPEARANCE", "WINDDIRECTION", "WINDFORCE", "WAVEHEIGHT", "WAVEDIRECTION" };

        //只预报24h的海区
        //青岛市 --> QD
        //渤海海峡 --> BHHX

        string[] p24hAreaArr = { "QD", "BHHX" };


        //预报3d的海区
        //青岛近海 --> QDJH
        //渤海 --> BH
        //黄海北部 --> NHH
        //黄海中部 --> MHH
        //黄海南部 --> SHH

        string[] p3dAreaArr = { "QDJH","BH", "NHH", "MHH", "SHH" };


        string[] AreaForecastValuesArr = new string[88];//共有74处单元格(青岛水温合并单元格),78改为加上10之后的
        List<string[]> addedSeaAreaList = new List<string[]>();//不固定海区数据
        //渔政局
        List<string[]> addyuzhengju = new List<string[]>();
        string PUBLISHTIME = "";



        /// <summary>
        /// 调用模板生成word
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="fileName">生成的具有模板样式的新文件</param>
        public int ExportWord(string templateFile, string fileName, DateTime dt, string publishTime)
        {
            //生成word程序对象

            Word.Application app = new Word.Application();

            //模板文件
            string TemplateFile = templateFile;
            //生成的具有模板样式的新文件
            string FileName = fileName;

            //模板文件拷贝到新文件
            File.Copy(TemplateFile, FileName);
            //生成documnet对象
            Word.Document doc = new Word.Document();
            object Obj_FileName = FileName;
            object Visible = false;
            object ReadOnly = false;
            object missing = System.Reflection.Missing.Value;

            //打开文件
            doc = app.Documents.Open(ref Obj_FileName, ref missing, ref ReadOnly, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref Visible,
                ref missing, ref missing, ref missing,
                ref missing);
            doc.Activate();
            #region 上半部分
            try
            {
                //数据库查询

                //TBLFOOTER tblfooter_Model = new TBLFOOTER();
                //tblfooter_Model.PUBLISHDATE = dt;
                //填报信息表提取数据
                //System.Data.DataTable tblfooter = (System.Data.DataTable)new sql_TBLFOOTER().get_TBLFOOTER_AllData(tblfooter_Model);
                //for (int i = 0; i < tblfooter.Rows.Count; i++)
                //{
                //    string FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                //    string FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                //    string FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                //    string FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                //    tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                //    tblfooter_Model.FTELEPHONE = FTELEPHONE;
                //    tblfooter_Model.FFAX = FFAX;
                //    tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;
                //}
                var fstDay = dt.Day;
                var sndDay = dt.AddDays(1).Day;
                var thdDay = dt.AddDays(2).Day;
                var fthDay = dt.AddDays(3).Day;
                //FORECASTDATEArr[0] = fstDay + "日20时至" + sndDay + "日20时";
                //FORECASTDATEArr[1] = sndDay + "日20时至" + thdDay + "日20时";
                //FORECASTDATEArr[2] = thdDay + "日20时至" + fthDay + "日20时";



                TBLZHCFORECAST TBLZHCFORECAST_Model = new TBLZHCFORECAST();
                //if (publishTime == "07")
                //{
                //    PUBDTTIME = dt.ToLongDateString() + "07时"; ;
                //    FORECASTDATEArr[0] = fstDay + "日08时至" + sndDay + "日08时";
                //    FORECASTDATEArr[1] = sndDay + "日08时至" + thdDay + "日08时";
                //    FORECASTDATEArr[2] = thdDay + "日08时至" + fthDay + "日08时";
                //    TBLZHCFORECAST_Model.PUBLISHDATE = dt.AddHours(7);
                //}
                if (publishTime == "16")
                {
                    PUBDTTIME = dt.ToLongDateString() + "16时";
                    FORECASTDATEArr[0] = fstDay + "日20时";
                    FORECASTDATEArr[1] = sndDay + "日20时";
                    FORECASTDATEArr[2] = thdDay + "日20时";
                    FORECASTDATEArr[3] = sndDay + "日20时";
                    FORECASTDATEArr[4] = thdDay + "日20时";
                    FORECASTDATEArr[5] = fthDay + "日20时";
                    TBLZHCFORECAST_Model.PUBLISHDATE = dt.AddHours(16);
                }
              
                System.Data.DataTable dtTBLZHCFORECAST = (System.Data.DataTable)new sql_TBLZHCFORECAST().get_TBLZHCFORECAST_AllData(TBLZHCFORECAST_Model);
                if (dtTBLZHCFORECAST.Rows.Count == 0) { }
                else
                {
                    for (int i = 0; i < dtTBLZHCFORECAST.Rows.Count; i++)
                    {
                        var row = dtTBLZHCFORECAST.Rows[i];

                        var SEAAREA = row["SEAAREA"].ToString();
                        int areaIndex = 0;
                        switch (SEAAREA)
                        {
                            case "青岛市": areaIndex = 0; break;
                            case "渤海海峡": areaIndex = 1; break;
                            case "青岛近海": areaIndex = 2; break;
                            case "渤海": areaIndex = 5; break;
                            case "黄海北部": areaIndex = 8; break;
                            case "黄海中部": areaIndex = 11; break;
                            case "黄海南部": areaIndex = 14; break;
                            default:
                                var sea = new string[] {
                                    SEAAREA, row["FORECASTDATE"].ToString(),
                                    row["WINDDIRECTION"].ToString(),row["WINDFORCE"].ToString(),
                                    row["WAVEHEIGHT"].ToString()};
                                addedSeaAreaList.Add(sea);
                                areaIndex = -1;
                                break;
                        }
                        if (areaIndex >= 0)
                        {
                            var FORECASTDATE = DateTime.Parse(row["FORECASTDATE"].ToString());
                            var days = (FORECASTDATE - dt).Days - 1;
                            var startIndex = (areaIndex + days) * 5;
                            AreaForecastValuesArr[startIndex] = row["WEATHERAPPEARANCE"].ToString();
                            AreaForecastValuesArr[startIndex + 1] = row["WINDDIRECTION"].ToString();
                            AreaForecastValuesArr[startIndex + 2] = row["WINDFORCE"].ToString();
                            AreaForecastValuesArr[startIndex + 3] = row["WAVEHEIGHT"].ToString();
                            AreaForecastValuesArr[startIndex + 4] = row["WAVEDIRECTION"].ToString();
                        }
                    }
                }

                object mark;

                //日 期
                for (int i = 0; i < 6; i++)
                {
                    mark = "FORECAST" + (i + 1).ToString();
                    doc.Bookmarks.get_Item(ref mark).Range.Text = FORECASTDATEArr[i];
                }

                //只预报24h的海区
                //青岛市 --> QD
                //渤海海峡 --> BHHX
                //填值
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        var area = p24hAreaArr[i].ToString();
                        var type = colomns[j].ToString();
                        if (area == "QD")
                        {
                            if (type == "WAVEHEIGHT")
                            {
                                continue;
                            }
                        }
                        mark = p24hAreaArr[i] + colomns[j];
                        doc.Bookmarks.get_Item(ref mark).Range.Text = AreaForecastValuesArr[i * 5 + j];
                    }
                }

                //预报3d的海区
                //青岛近海 --> QDJH
                //渤海 --> BH
                //黄海北部 --> NHH
                //黄海中部 --> MHH
                //黄海南部 --> SHH
                //填值

                for (int i = 0; i < 5; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            mark = p3dAreaArr[i] + colomns[j] + (k + 1).ToString();
                            doc.Bookmarks.get_Item(ref mark).Range.Text = AreaForecastValuesArr[10 + i * 15 + k * 5 + j];
                        }
                    }
                }
                #endregion
                #region 填报信息表提取数据


                //数据库查询
               
                TBLFOOTER tblfooter_Model = new TBLFOOTER();
                tblfooter_Model.PUBLISHDATE = dt;
                //填报信息表提取数据
                System.Data.DataTable tblfooter = (System.Data.DataTable)new sql_TBLFOOTER().get_TBLFOOTER_AllData(tblfooter_Model);
                for (int i = 0; i < tblfooter.Rows.Count; i++)
                {
                    string FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                    string FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                    string FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                    string FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();

                    string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                    string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                    PUBLISHTIME = PUBLISHDATE+ "16时";

                    tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;//发布单位
                    tblfooter_Model.FTELEPHONE = FTELEPHONE;
                    tblfooter_Model.FFAX = FFAX;
                    tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;
                }
                object mark_PUBLISHTIME = "PUBLISHTIME";
                doc.Bookmarks.get_Item(ref mark_PUBLISHTIME).Range.Text = PUBLISHTIME;
                mark = "FRELEASEUNIT";//发布单位
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.FRELEASEUNIT;
                #endregion
                #region 海区预报部分
                string list = "";
                var count = 0;
                //海区数据

                //日 期
                string[] FORECASTDATEhx = new string[13];

                //风向（方位）
                string[] WINDDIRECTIONhx = new string[13];

                //风速（级）
                string[] WINDFORCEhx = new string[13];

                //浪高（m）
                string[] WAVEHEIGHThx = new string[13];
                TBLYZJFORECAST tblyzjforeast_Model = new TBLYZJFORECAST();
                tblyzjforeast_Model.PUBLISHDATE = dt.AddHours(16);
                System.Data.DataTable tblyzjforeast = (System.Data.DataTable)new sql_TBLYZJFORECAST().TBLYZJFORECAST(tblyzjforeast_Model);
                Dictionary<DataRow, int> dic = new Dictionary<DataRow, int>();
                if (tblyzjforeast != null && tblyzjforeast.Rows.Count == 0)
                {

                }
                else if (tblyzjforeast != null && tblyzjforeast.Rows.Count >= 0)
                {
                    int index = 0;
                    for (int i = 0; i < tblyzjforeast.Rows.Count; i++)
                    {
                        var row = tblyzjforeast.Rows[i];
                        var SEAAREA = row["SEAAREA"].ToString();
                        
                        switch (SEAAREA)
                        {
                            case "旅顺":
                                index = 0;
                                break;
                            case "烟台":
                                index = 1;
                                break;
                            case "威海":
                                index = 2;
                                break;
                            case "石岛":
                                index = 3;
                                break;
                            case "责任海区1":
                                index = 4;
                                break;
                            case "责任海区2":
                                index = 5;
                                break;
                            case "青岛":
                                index = 6;
                                break;
                            default:
                                index = 13 + i;
                                break;
                        }
                        dic.Add(tblyzjforeast.Rows[i],index);//将数据放到dictionary中，方便排序
                    }
                }

                if(dic != null && dic.Count > 0)
                {
                    var dtRow = dic.OrderBy(o => o.Value).ToList();//将数据按照海区排序，并转化成list
                    for (int i = 0; i < dtRow.Count; i++)
                    {
                        DataRow dr = dtRow[i].Key;
                        var SEAAREA = dr["SEAAREA"].ToString();
                        var yuzhengju = new string[]
                                {
                                SEAAREA, dr["FORECASTDATE"].ToString(),
                                    //row["FORECASTDATE"].ToString(),
                                    Convert.ToDateTime(dr["FORECASTDATE"]).Day.ToString(),
                                    dr["WINDDIRECTION"].ToString(),dr["WINDFORCE"].ToString(),  
                                    dr["WAVEHEIGHT"].ToString()
                                };
                        addyuzhengju.Add(yuzhengju);
                    }
                }
                //不固定责任海区预报部分
                string[] list1 = null;
                if (addyuzhengju != null && addyuzhengju.Count > 0)
                {
                    list = "";
                    count = addyuzhengju.Count;
                    for (int a = 0; a < count; a++)
                    {
                        list += addyuzhengju[a][0].ToString() + ",";
                    }
                    if (list != null && list.Length > 0)
                    {
                        list = list.Substring(0, list.Length - 1);
                        string[] strs = list.Split(',');
                        //int st = strs.Length;
                        list1 = (string.Join(",", list.Split(',').Distinct().ToArray())).Split(',');
                    }
                }
                if (list1.Length > 0)
                {
                    

                    // int counts = CharNum(list, list1[n].ToString ());
                    object objTABLE2Flag = "TABLE2"; /* \TABLE2 is a predefined bookmark */
                    Word.Table oTable;
                    Word.Range wrdRng = doc.Bookmarks.get_Item(ref objTABLE2Flag).Range;
                    wrdRng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//设置样式居中
                    oTable = doc.Tables.Add(wrdRng, count + 1, 5, ref missing, ref missing);
                    oTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    oTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                   
                    //oTable.Range.ParagraphFormat.SpaceAfter = 2;
                    int r, c;
                    string strText;
                    int rowIndex = 1;
                    oTable.Cell(rowIndex, 1).Range.Text = "海区";
                    oTable.Cell(rowIndex, 2).Range.Text = "日期";
                    oTable.Cell(rowIndex, 3).Range.Text = "风向";
                    oTable.Cell(rowIndex, 4).Range.Text = "风力";
                    oTable.Cell(rowIndex, 5).Range.Text = "浪高";
                    int allcount = 2;
                    var AreaStartIndex = 2;
                    var query = addyuzhengju.GroupBy(addyuzhengju => addyuzhengju[0]);
                    foreach (var group in query)
                    {
                        var groupCount = group.Count();
                        if(group.Key != "青岛")
                        {
                            oTable.Cell(AreaStartIndex, 1).Merge(oTable.Cell(AreaStartIndex + groupCount - 1, 1));//合并海区名称
                        }
                        for (int I = 0; I < groupCount; I++)

                        {
                            
                            var arr = group.ElementAt(I);
                            if (I == 0)
                            {
                                oTable.Cell(AreaStartIndex + I, 1).Range.Text = arr[0];

                                oTable.Cell(AreaStartIndex + I, 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                for (var col = 2; col <= 5; col++)
                                {
                                        var strTextt = arr[col]; 

                                        oTable.Cell(AreaStartIndex + I, col).Range.Text = strTextt;
                                }
                            }
                            else
                            {
                                for (var col = 2; col <= 5; col++)
                                {
                                        var strTextt = arr[col];  // "r" + r + "c" + c;

                                        oTable.Cell(AreaStartIndex + I, col).Range.Text = strTextt;
                                }

                            }

                        }

                        AreaStartIndex += groupCount;

                    }
                }
                //var label = "";
                //var markValues = new string[13];
              
                //for (int i = 0; i <4; i++)
                //{
                //    switch (i)
                //    {
                //        case 0: label = "FORECASTDATE"; markValues = FORECASTDATEhx; break;
                //        case 1: label = "WINDDIRECTION"; markValues = WINDDIRECTIONhx; break;
                //        case 2: label = "WINDFORCE"; markValues = WINDFORCEhx; break;
                //        case 3: label = "WAVEHEIGHT"; markValues = WAVEHEIGHThx; break;
                //        default: break;

                //    }

                //    for (int j = 0; j <13; j++)
                //    {

                //        mark = label + (j + 1);
                //        doc.Bookmarks.get_Item(ref mark).Range.Text = markValues[j];
                //    }


                //}
                #endregion
                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                return 1;
            }
            catch (Exception ex)
            {
                //throw ex;

                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                WriteLog.Write(ex.ToString());
                return 0;
            }
        }

    }
}