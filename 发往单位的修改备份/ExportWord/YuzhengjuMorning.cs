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
    /// <summary>
    /// 上午渔政局
    /// </summary>
    public class YuzhengjuMorning
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

        //只预报24h的海区  青岛近海改为3天的
        //青岛市 --> QD
        //渤海海峡 --> BHHX

        string[] p24hAreaArr = { "QD", "BHHX" };


        //预报3d的海区
        //青岛近海 --> QDJH
        //渤海 --> BH
        //黄海北部 --> NHH

        //黄海中部 --> MHH
        //黄海南部 --> SHH

        string[] p3dAreaArr = { "QDJH", "BH", "NHH", "MHH", "SHH" };


        string[] AreaForecastValuesArr = new string[88];//共有74处单元格(青岛水温合并单元格)，78改为加上10之后的
        List<string[]> addedSeaAreaList = new List<string[]>();//不固定海区数据
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
                    PUBLISHTIME = PUBLISHDATE + "07时";

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