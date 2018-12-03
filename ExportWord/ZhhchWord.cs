using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Word = Microsoft.Office.Interop.Word;


namespace PredicTable.ExportWord
{
    public class ZhhchWord
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
        //青岛近海 --> QDJH（2018.8.28需求修改，青岛近海预报改为预报3d的）
        //渤海海峡 --> BHHX

        //string[] p24hAreaArr = { "QD", "QDJH", "BHHX" };
        string[] p24hAreaArr = { "QD", "BHHX" };


        //预报3d的海区
        //青岛近海--> QDJH  2018.8.28新的需求
        //渤海 --> BH
        //黄海北部 --> NHH

        //黄海中部 --> MHH
        //黄海南部 --> SHH

        string[] p3dAreaArr = {"QDJH", "BH", "NHH", "MHH", "SHH" };


        //string[] AreaForecastValuesArr = new string[75];//共有74处单元格(青岛水温合并单元格)
        //原来共有74处单元格(青岛水温合并单元格)，现在新添加10个单元格，青岛近海的
        string[] AreaForecastValuesArr = new string[85];//modify by Durriya

        List<string[]> addedSeaAreaList = new List<string[]>();//不固定海区数据

        public ZhhchWord()
        {
        }

        /// <summary>
        /// 调用模板生成word
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="fileName">生成的具有模板样式的新文件</param>
        public int ExportWord(string templateFile, string fileName, DateTime dt,string publishTime)
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
                if (publishTime == "07")
                {
                    PUBDTTIME = dt.ToLongDateString() + "07时"; ;
                    FORECASTDATEArr[0] = fstDay + "日08时";
                    FORECASTDATEArr[1] = sndDay + "日08时";
                    FORECASTDATEArr[2] = thdDay + "日08时";
                    FORECASTDATEArr[3] = sndDay + "日08时";
                    FORECASTDATEArr[4] = thdDay + "日08时";
                    FORECASTDATEArr[5] = fthDay + "日08时";
                    TBLZHCFORECAST_Model.PUBLISHDATE = dt.AddHours(7);
                }
                if (publishTime == "16")
                {
                    PUBDTTIME = dt.ToLongDateString()+"16时";
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
                        //是根据地区取数据，每个地区完全取完后执行下个地区数据
                        switch (SEAAREA)
                        {
                            //case "青岛市": areaIndex = 0; break;
                            //case "青岛近海": areaIndex = 1; break;
                            //case "渤海海峡": areaIndex = 2; break;
                            //case "渤海": areaIndex = 3; break;
                            //case "黄海北部": areaIndex = 6; break;
                            //case "黄海中部": areaIndex = 9; break;
                            //case "黄海南部": areaIndex = 12; break;
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
                                    Convert.ToDateTime(row["FORECASTDATE"]).Day.ToString(),
                                    
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
                for (int i=0;i<6;i++)
                {
                    mark = "FORECAST" + (i+1).ToString();
                    doc.Bookmarks.get_Item(ref mark).Range.Text = FORECASTDATEArr[i];
                }

                //只预报24h的海区
                //青岛市 --> QD
                //青岛近海 --> QDJH
                //渤海海峡 --> BHHX
                //填值
                /*for (int i=0;i<3;i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        var area = p24hAreaArr[i].ToString();
                        var type = colomns[j].ToString();
                        if (area == "QD")
                        {
                            if(type == "WAVEHEIGHT")
                            {
                                continue;
                            }
                        }
                        mark = p24hAreaArr[i]+colomns[j];
                        doc.Bookmarks.get_Item(ref mark).Range.Text = AreaForecastValuesArr[i*5+j];
                    }
                }*/


                //修改只预报24h的海区的地区
                //青岛市 --> QD
                //渤海海峡 --> BHHX
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
                //青岛近海 --> QDJH  新添加的预报3d的地区
                //渤海 --> BH
                //黄海北部 --> NHH
                //黄海中部 --> MHH
                //黄海南部 --> SHH
                //填值

                //for (int i = 0; i < 5; i++)//4改为5 Durriya
                //{
                //    for (int k = 0; k < 3; k++)
                //    {
                //        for (int j = 0; j < 5; j++)
                //        {
                //            mark = p3dAreaArr[i] + colomns[j]+(k+1).ToString();
                //            doc.Bookmarks.get_Item(ref mark).Range.Text = AreaForecastValuesArr[(i+1) * 15+ k*5 +j ];
                //        }
                //    }
                //}

                //string[] colomns = { "WEATHERAPPEARANCE", "WINDDIRECTION", "WINDFORCE", "WAVEHEIGHT", "WAVEDIRECTION" };
                //string[] p3dAreaArr = {"QDJH", "BH", "NHH", "MHH", "SHH" };
                for (int i = 0; i < 5; i++)
                {
                    for(int k = 0; k < 3; k++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            mark = p3dAreaArr[i] + colomns[j] + (k + 1).ToString();
                            doc.Bookmarks.get_Item(ref mark).Range.Text = AreaForecastValuesArr[10 + i * 15 + k * 5 + j];
                        }
                    }
                }


                // 不固定海区预报部分
                string list = "";
                var count = addedSeaAreaList.Count;
                for (int a = 0; a < count; a++)
                {
                    list += addedSeaAreaList[a][0].ToString()+",";
                }
                if(list != null && list.Length > 0)
                {
                    list = list.Substring(0, list.Length - 1);
                    string[] strs = list.Split(',');
                    //int st = strs.Length;
                    string[] list1 = (string.Join(",", list.Split(',').Distinct().ToArray())).Split(',');


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
                        oTable.Cell(rowIndex, 1).Range.Text = "预报区域";
                        oTable.Cell(rowIndex, 2).Range.Text = "日期";
                        oTable.Cell(rowIndex, 3).Range.Text = "风向(方位)";
                        oTable.Cell(rowIndex, 4).Range.Text = "风力（级）";
                        oTable.Cell(rowIndex, 5).Range.Text = "浪高（米）";
                        int allcount = 2;
                        
                        var AreaStartIndex = 2;

                        var query = addedSeaAreaList.GroupBy(sea => sea[0]);

                        foreach (var group in query)
                        {
                            var groupCount = group.Count();

                            oTable.Cell(AreaStartIndex, 1).Merge(oTable.Cell(AreaStartIndex + groupCount - 1, 1));//合并海区名称



                            for (int I = 0; I < groupCount; I++)

                            {
                                var arr = group.ElementAt(I);
                                if (I == 0)
                                {


                                    oTable.Cell(AreaStartIndex + I, 1).Range.Text = arr[0];

                                    oTable.Cell(AreaStartIndex + I, 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                    for (var col = 2; col <= 5; col++)
                                    {
                                        //if (col == 2)
                                        //{
                                        //    oTable.Cell(AreaStartIndex + I, col).Range.Text = (DateTime.Parse(arr[col])).GetDateTimeFormats('M')[0].ToString();
                                        //}
                                        //else
                                        //{
                                            var strTextt = arr[col];  // "r" + r + "c" + c;

                                            oTable.Cell(AreaStartIndex + I, col).Range.Text = strTextt;
                                        //}

                                    }
                                }
                                else
                                {
                                    for (var col = 2; col <= 5; col++)
                                    {
                                        //if (col == 2)
                                        //{
                                        //    oTable.Cell(AreaStartIndex + I, col).Range.Text = (DateTime.Parse(arr[col])).GetDateTimeFormats('M')[0].ToString();
                                        //}
                                        //else
                                        //{
                                            var strTextt = arr[col];  // "r" + r + "c" + c;

                                            oTable.Cell(AreaStartIndex + I, col).Range.Text = strTextt;
                                        //}

                                    }

                                }

                            }

                            AreaStartIndex += groupCount;

                        }
                    }
                }
                mark = "PUBDTTIME";
                doc.Bookmarks.get_Item(ref mark).Range.Text = PUBDTTIME;

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
        /// <summary>
        /// 查询每个不固定海区预报天数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public int CharNum(string str, string search)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(str) || !string.IsNullOrEmpty(search))
            {
                string[] resultString = Regex.Split(str, search, RegexOptions.IgnoreCase);

                count = resultString.Length;
            }
            return count;

        }


    }
}