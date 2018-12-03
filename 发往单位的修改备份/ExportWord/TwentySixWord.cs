using Word = Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using PredicTable.Dal;

namespace PredicTable.ExportWord
{
    /// <summary>
    /// 26号预报单
    /// </summary>
    public class TwentySixWord
    {
        public TwentySixWord()
        {

        }
        string PUBLISHTIME = "";

        //挡潮闸
        string[] ForecastDateArr = new string[10]; //潮位预报日期

        string[] FstHighTideTimeArr = new string[10]; //第一次高潮潮时
        string[] FstHighTideHeightArr = new string[10]; //第一次高潮潮高
        string[] FstLowTideTimeArr = new string[10]; //第一次低潮潮时
        string[] FstLowTideHeightArr = new string[10]; //第一次低潮潮高

        string[] SndHighTideTimeArr = new string[10]; //第二次高潮潮时
        string[] SndHighTideHeightArr = new string[10]; //第二次高潮潮高
        string[] SndLowTideTimeArr = new string[10]; //第二次低潮潮时
        string[] SndLowTideHeightArr = new string[10]; //第二次低潮潮高

        //黄河风浪
        string[] ForecastDateArrWH = new string[10]; //预报日期
        string[] BGWH = new string[10]; //波高
        string[] BHWH = new string[10]; //波向
        string[] FHWH = new string[10]; //风向
        string[] FLWH = new string[10]; //风力

        int index = 0;
        int indexWH = 0;

        System.Data.DataTable dtFL = new DataTable("dtFL");
        System.Data.DataTable dtCX = new DataTable("dtCX");
        /// <summary>
        /// 调用模板生成word
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="fileName">生成的具有模板样式的新文件</param>
        public int ExportWord(string templateFile, string fileName, DateTime dt)
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

                TBLFOOTER tblfooter_Model = new TBLFOOTER();
                tblfooter_Model.PUBLISHDATE = dt;
                //填报信息表提取数据
                System.Data.DataTable tblfooter = (System.Data.DataTable)new sql_TBLFOOTER().get_TBLFOOTER_AllData(tblfooter_Model);

                string FRELEASEUNIT = "";
                //string FTELEPHONE = "";
                //string FFAX = "";
                string ZHIBANTEL = "";
                string SENDTEL = "";
                string FWAVEFORECASTERTEL = "";
                string FTIDALFORECASTERTEL = "";
                string FWATERTEMPERATUREFORECASTERTEL = "";

                string FWAVEFORECASTER = "";
                string FTIDALFORECASTER = "";
                string FWATERTEMPERATUREFORECASTER = "";
                for (int i = 0; i < tblfooter.Rows.Count; i++)
                {
                     FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                     //FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                     //FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                     FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                     FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();
                     FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();

                    ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                    SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                    FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                    FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话
                    FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话

                    string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                    string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                    // PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
                    PUBLISHTIME = PUBLISHDATE + "10时";


                    tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                    //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                    //tblfooter_Model.FFAX = FFAX;
                    tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;
                    tblfooter_Model.FTIDALFORECASTER = FTIDALFORECASTER;
                    tblfooter_Model.FWATERTEMPERATUREFORECASTER = FWATERTEMPERATUREFORECASTER;

                    tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                    tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                    tblfooter_Model.FWAVEFORECASTERTEL = FWAVEFORECASTERTEL;
                    tblfooter_Model.FTIDALFORECASTERTEL = FTIDALFORECASTERTEL;
                    tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL = FWATERTEMPERATUREFORECASTERTEL;
                }
                object mark_PUBLISHTIME = "PUBLISHTIME";
                doc.Bookmarks.get_Item(ref mark_PUBLISHTIME).Range.Text = PUBLISHTIME;
                //object FTELEPHONES = "FTELEPHONE";
                //doc.Bookmarks.get_Item(ref FTELEPHONES).Range.Text = FTELEPHONE;
                //object FFAXS = "FFAX";
                //doc.Bookmarks.get_Item(ref FFAXS).Range.Text = FFAX;
                object FRELEASEUNITS = "FRELEASEUNIT";
                doc.Bookmarks.get_Item(ref FRELEASEUNITS).Range.Text = FRELEASEUNIT;
                object FWAVEFORECASTERS = "FWAVEFORECASTER";
                doc.Bookmarks.get_Item(ref FWAVEFORECASTERS).Range.Text = FWAVEFORECASTER;
                object FTIDALFORECASTERS = "FTIDALFORECASTER";
                doc.Bookmarks.get_Item(ref FTIDALFORECASTERS).Range.Text = FTIDALFORECASTER;

                object ZHIBANTELS = "FZHIBANTEL";
                doc.Bookmarks.get_Item(ref ZHIBANTELS).Range.Text = ZHIBANTEL;
                object SENDTELS = "FSENDTEL";
                doc.Bookmarks.get_Item(ref SENDTELS).Range.Text = SENDTEL;
                object FWAVEFORECASTERTELS = "FWAVEFORECASTERTEL";
                doc.Bookmarks.get_Item(ref FWAVEFORECASTERTELS).Range.Text = FWAVEFORECASTERTEL;
                object FTIDALFORECASTERTELS = "FTIDALFORECASTERTEL";
                doc.Bookmarks.get_Item(ref FTIDALFORECASTERTELS).Range.Text = FTIDALFORECASTERTEL;


                //object FWATERTEMPERATUREFORECASTERS = "FWATERTEMPERATUREFORECASTER";
                //doc.Bookmarks.get_Item(ref FWATERTEMPERATUREFORECASTERS).Range.Text = FWATERTEMPERATUREFORECASTER;
                //TBLMZZTIDELEVEL TBLMZZTIDELEVEL = new TBLMZZTIDELEVEL();
                //TBLMZZTIDELEVEL.PUBLISHDATE = dt;
                //TBLMZZTIDELEVEL.FORECASTDATE = dt;
                //挡潮闸
                //sql_TBLSXGTIDELEVEL MZLevel = new sql_TBLSXGTIDELEVEL();
                //DataTable dtMZLevel = (DataTable)MZLevel.GETTBLSXGTIDELEVEL(TBLMZZTIDELEVEL,"f");
                TBLHARBOURTIDELEVEL tblharbourtidelevel_Model = new TBLHARBOURTIDELEVEL();
                tblharbourtidelevel_Model.PUBLISHDATE = dt;
                System.Data.DataTable tblharbourtidelevel = (System.Data.DataTable)new sql_TBLHARBOURTIDELEVEL().get_TBLHARBOURTIDELEVEL_AllData(tblharbourtidelevel_Model);
                if (tblharbourtidelevel.Rows.Count == 0) { }
                else
                {
                    dtCX.Columns.Add("PUBLISHDATE", typeof(string));
                    dtCX.Columns.Add("HTLHARBOUR", typeof(string));
                    dtCX.Columns.Add("FORECASTDATE", typeof(string));
                    dtCX.Columns.Add("HTLFIRSTWAVEOFTIME", typeof(string));
                    dtCX.Columns.Add("HTLFIRSTWAVETIDELEVEL", typeof(string));
                    dtCX.Columns.Add("HTLFIRSTTIMELOWTIDE", typeof(string));
                    dtCX.Columns.Add("HTLLOWTIDELEVELFORTHEFIRSTTIME", typeof(string));
                    dtCX.Columns.Add("HTLSECONDWAVEOFTIME", typeof(string));
                    dtCX.Columns.Add("HTLSECONDWAVETIDELEVEL", typeof(string));
                    dtCX.Columns.Add("HTLSECONDTIMELOWTIDE", typeof(string));
                    dtCX.Columns.Add("HTLLOWTIDELEVELFORTHESECONDTIM", typeof(string));
                    foreach (System.Data.DataRow row in tblharbourtidelevel.Rows)
                    {
                        if (row["HTLHARBOUR"].ToString() == "黄河海港")
                        {
                            var FORECASTDATE = DateTime.Parse(row["FORECASTDATE"].ToString());
                            DataRow rowCX = dtCX.NewRow();
                            rowCX["PUBLISHDATE"] = row["PUBLISHDATE"].ToString();
                            rowCX["HTLHARBOUR"] = row["HTLHARBOUR"].ToString();
                            rowCX["FORECASTDATE"] = FORECASTDATE.Month + "月" + FORECASTDATE.Day + "日";
                            rowCX["HTLFIRSTWAVEOFTIME"] = row["HTLFIRSTWAVEOFTIME"].ToString();
                            rowCX["HTLFIRSTWAVETIDELEVEL"] = row["HTLFIRSTWAVETIDELEVEL"].ToString();
                            rowCX["HTLFIRSTTIMELOWTIDE"] = row["HTLFIRSTTIMELOWTIDE"].ToString();
                            rowCX["HTLLOWTIDELEVELFORTHEFIRSTTIME"] = row["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                            rowCX["HTLSECONDWAVEOFTIME"] = row["HTLSECONDWAVEOFTIME"].ToString();
                            rowCX["HTLSECONDWAVETIDELEVEL"] = row["HTLSECONDWAVETIDELEVEL"].ToString();
                            rowCX["HTLSECONDTIMELOWTIDE"] = row["HTLSECONDTIMELOWTIDE"].ToString();
                            rowCX["HTLLOWTIDELEVELFORTHESECONDTIM"] = row["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                            dtCX.Rows.Add(rowCX);
                        }
                        //    var FORECASTDATE = DateTime.Parse(row["FORECASTDATE"].ToString());
                        ////var areaIndex = 0;
                        ////var forecastIndex = ((FORECASTDATE - dt).Days - 1);

                        //ForecastDateArr[index] = FORECASTDATE.Month + "月" + FORECASTDATE.Day + "日";
                        //FstHighTideTimeArr[index] = ConvertTimeStr(row["MZZTLFIRSTWAVEOFTIME"].ToString());//第一次高潮潮时
                        //FstHighTideHeightArr[index] = row["MZZTLFIRSTWAVETIDELEVEL"].ToString(); //第一次高潮潮高
                        //FstLowTideTimeArr[index] = ConvertTimeStr(row["MZZTLFIRSTTIMELOWTIDE"].ToString()); //第一次低潮潮时
                        //FstLowTideHeightArr[index] = row["MZZTLLOWTIDELEVELFORTHEFIRSTTI"].ToString(); //第一次低潮潮高
                        //SndHighTideTimeArr[index] = ConvertTimeStr(row["MZZTLSECONDWAVEOFTIME"].ToString()); //第二次高潮潮时
                        //SndHighTideHeightArr[index] = row["MZZTLSECONDWAVETIDELEVEL"].ToString(); //第二次高潮潮高
                        //SndLowTideTimeArr[index] = ConvertTimeStr(row["MZZTLSECONDTIMELOWTIDE"].ToString()); //第二次低潮潮时
                        //SndLowTideHeightArr[index] = row["MZZTLLOWTIDELEVELFORTHESECONDT"].ToString(); //第二次低潮潮高
                        //index++;
                    }
                }

                //黄河风浪预报
                //sql_TBLHH3DAYSFORECAST WindWave_3Days = new sql_TBLHH3DAYSFORECAST();
                //TBLYRSOUTHSEAWALL24WINDWAVE TBLYRSOUTHSEAWALL24WINDWAVE = new TBLYRSOUTHSEAWALL24WINDWAVE();
                //TBLYRSOUTHSEAWALL24WINDWAVE.FORECASTDATE = dt;
                //TBLYRSOUTHSEAWALL24WINDWAVE.PUBLISHDATE = dt;
                //DataTable dtWindWave = (DataTable)WindWave_3Days.GETTBLHH3DAYSFORECAST(TBLYRSOUTHSEAWALL24WINDWAVE, "f");

                TBLYRBHWINDWAVE72HFORECASTTWO tblyrbhwindwave72hforecasttwo_Model = new TBLYRBHWINDWAVE72HFORECASTTWO();
                tblyrbhwindwave72hforecasttwo_Model.PUBLISHDATE = dt;
                tblyrbhwindwave72hforecasttwo_Model.FORECASTDATE = dt;
                System.Data.DataTable tblyrbhwindwave72hforecasttwo = (System.Data.DataTable)new sql_TBLYRBHWINDWAVE72HFORECASTTWO().get_TBLYRBHWINDWAVE72HFORECASTTWO_3Daysata(tblyrbhwindwave72hforecasttwo_Model);

                
                if (tblyrbhwindwave72hforecasttwo.Rows.Count == 0)
                {

                }
                else
                {
                    dtFL.Columns.Add("PUBLISHDATE", typeof(string));
                    dtFL.Columns.Add("REPORTAREA", typeof(string));
                    dtFL.Columns.Add("FORECASTDATE", typeof(string));
                    dtFL.Columns.Add("YRBHWWFWAVEHEIGHT", typeof(string));
                    dtFL.Columns.Add("YRBHWWFWAVEDIR", typeof(string));
                    dtFL.Columns.Add("YRBHWWFFLOWDIR", typeof(string));
                    dtFL.Columns.Add("YRBHWWFFLOWLEVEL", typeof(string));
                    dtFL.Columns.Add("YRBHWWFWATERTEMPERATURE", typeof(string));
                    foreach (System.Data.DataRow row in tblyrbhwindwave72hforecasttwo.Rows)
                    {
                        if(row["REPORTAREA"].ToString() == "黄河海港")
                        {
                            var FORECASTDATE = DateTime.Parse(row["FORECASTDATE"].ToString());
                            DataRow rowFL = dtFL.NewRow();
                            rowFL["PUBLISHDATE"] = row["PUBLISHDATE"].ToString();
                            rowFL["REPORTAREA"] = row["REPORTAREA"].ToString();
                            rowFL["FORECASTDATE"] = FORECASTDATE.Month + "月" + FORECASTDATE.Day + "日";
                            rowFL["YRBHWWFWAVEHEIGHT"] = row["YRBHWWFWAVEHEIGHT"].ToString();
                            rowFL["YRBHWWFWAVEDIR"] = row["YRBHWWFWAVEDIR"].ToString();
                            rowFL["YRBHWWFFLOWDIR"] = row["YRBHWWFFLOWDIR"].ToString();
                            rowFL["YRBHWWFFLOWLEVEL"] = row["YRBHWWFFLOWLEVEL"].ToString();
                            rowFL["YRBHWWFWATERTEMPERATURE"] = row["YRBHWWFWATERTEMPERATURE"].ToString();
                            dtFL.Rows.Add(rowFL);
                        }
                        //var FORECASTDATE = DateTime.Parse(row["FORECASTDATE"].ToString());
                        //ForecastDateArrWH[indexWH] = FORECASTDATE.Month + "月" + FORECASTDATE.Day + "日";
                        //BGWH[indexWH] = row["YRSSWWWAVEHEIGHT"].ToString();
                        //BHWH[indexWH] = row["YRSSWWWAVEDIRECTION"].ToString();
                        //FHWH[indexWH] = row["YRSSWWWINDDIRECTION"].ToString();
                        //FLWH[indexWH] = row["YRSSWWWINDFORCE"].ToString();
                        //indexWH++;
                    }
                }

                //object mark_PUBLISHTIME = "PUBLISHTIME";
                //doc.Bookmarks.get_Item(ref mark_PUBLISHTIME).Range.Text = PUBLISHTIME;

                //挡潮闸预报
                object mark = "";
                for (int i = 0; i < 3; i++)
                {
                    mark = "FORECASTDATE" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["FORECASTDATE"].ToString();
                    mark = "FSTHIGHTIDETIME" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["HTLFIRSTWAVEOFTIME"].ToString();
                    mark = "FSTHIGHTIDEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    mark = "FSTLOWTIDETIME" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["HTLFIRSTTIMELOWTIDE"].ToString();
                    mark = "FSTLOWTIDEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    mark = "SNDHIGHTIDETIME" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["HTLSECONDWAVEOFTIME"].ToString();
                    mark = "SNDHIGHTIDEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["HTLSECONDWAVETIDELEVEL"].ToString();
                    mark = "SNDLOWTIDETIME" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["HTLSECONDTIMELOWTIDE"].ToString();
                    mark = "SNDLOWTIDEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                }

                //黄河风浪预报
                object markWH = "";
                for (int i = 0; i < 3; i++)
                {
                    markWH = "FORECASTDATEWH" + (i + 1);
                    doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["FORECASTDATE"].ToString();
                    markWH = "YRSSWWWAVEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["YRBHWWFWAVEHEIGHT"].ToString();
                    markWH = "YRSSWWWAVEDIRECTION" + (i + 1);
                    doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["YRBHWWFWAVEDIR"].ToString();
                    markWH = "YRSSWWWINDDIRECTION" + (i + 1);
                    doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["YRBHWWFFLOWDIR"].ToString();
                    markWH = "YRSSWWWINDFORCE" + (i + 1);
                    doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["YRBHWWFFLOWLEVEL"].ToString();
                }



                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                return 1;
            }
            catch (Exception ex)
            {

                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                WriteLog.Write(ex.ToString());
                return 0;
            }
        }
        private string ConvertTimeStr(string timeStr)
        {
            return (string.IsNullOrEmpty(timeStr) || timeStr.Contains("-")) ?
                timeStr : timeStr.Substring(0, 2) + "时" + timeStr.Substring(timeStr.Length - 2) + "分";
        }
    }
}