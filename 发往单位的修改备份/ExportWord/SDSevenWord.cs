using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace PredicTable.ExportWord
{
    /// <summary>
    /// 山东七地市
    /// </summary>
    public class SDSevenWord
    {
        string PUBLISHTIME = "";

        //潮位预报
        string[] ForecastDateArr = new string[10]; //潮位预报日期

        string[] FstHighTideTimeArr = new string[10]; //第一次高潮潮时
        string[] FstHighTideHeightArr = new string[10]; //第一次高潮潮高
        string[] FstLowTideTimeArr = new string[10]; //第一次低潮潮时
        string[] FstLowTideHeightArr = new string[10]; //第一次低潮潮高

        string[] SndHighTideTimeArr = new string[10]; //第二次高潮潮时
        string[] SndHighTideHeightArr = new string[10]; //第二次高潮潮高
        string[] SndLowTideTimeArr = new string[10]; //第二次低潮潮时
        string[] SndLowTideHeightArr = new string[10]; //第二次低潮潮高

        //海面风及海浪预报
        string[] BGWH = new string[10]; //波高
        string[] FHWH = new string[10]; //风向
        string[] FLWH = new string[10]; //风力

        DataTable dtFL = new DataTable("dtFL");
        DataTable dtCX = new DataTable("dtCX");
        DataTable dtSW = new DataTable("dtSW");

        public int ExportWord(string templateFile, string fileName, DateTime dt,string dtTide,string dbWave,string dbTemperature)
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
                string FWAVEFORECASTER = "";
                string FTIDALFORECASTER = "";
                string FWATERTEMPERATUREFORECASTER = "";

                string ZHIBANTEL = "";//预报值班
                string SENDTEL = "";//预报发送
                string FWAVEFORECASTERTEL = "";//海浪预报员电话
                string FTIDALFORECASTERTEL = "";//潮汐电话
                string FWATERTEMPERATUREFORECASTERTEL = "";//水温预报员电话

                for (int i = 0; i < tblfooter.Rows.Count; i++)
                {
                    FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                    //FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                    //FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                    //FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                    //FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();
                    string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                    //PUBLISHTIME = PUBLISHDATE + "15时";

                    ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                    SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                    FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                    FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话
                    FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话

                    PUBLISHTIME = PUBLISHDATE;
                    tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                    //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                    //tblfooter_Model.FFAX = FFAX;

                    FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                    FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();
                    FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();

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

                object FWAVEFORECASTERS = "FWAVEFORECASTER";
                doc.Bookmarks.get_Item(ref FWAVEFORECASTERS).Range.Text = FWAVEFORECASTER;
                object FTIDALFORECASTERS = "FTIDALFORECASTER";
                doc.Bookmarks.get_Item(ref FTIDALFORECASTERS).Range.Text = FTIDALFORECASTER;
                object FWATERTEMPERATUREFORECASTERS = "FWATERTEMPERATUREFORECASTER";
                doc.Bookmarks.get_Item(ref FWATERTEMPERATUREFORECASTERS).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;

                object ZHIBANTELS = "FZHIBANTEL";
                doc.Bookmarks.get_Item(ref ZHIBANTELS).Range.Text = ZHIBANTEL;
                object SENDTELS = "FSENDTEL";
                doc.Bookmarks.get_Item(ref SENDTELS).Range.Text = SENDTEL;

                object FWAVEFORECASTERTELS = "FWAVEFORECASTERTEL";
                doc.Bookmarks.get_Item(ref FWAVEFORECASTERTELS).Range.Text = FWAVEFORECASTERTEL;
                object FTIDALFORECASTERTELS = "FTIDALFORECASTERTEL";
                doc.Bookmarks.get_Item(ref FTIDALFORECASTERTELS).Range.Text = FTIDALFORECASTERTEL;
                object FWATERTEMPERATUREFORECASTERTELS = "FWATERTEMPERATUREFORECASTERTEL";
                doc.Bookmarks.get_Item(ref FWATERTEMPERATUREFORECASTERTELS).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;


                //***********获取预报编号*******************************//
                Sql_SDSEVENNO sqlNo = new Sql_SDSEVENNO();
                DataTable dtNo = (DataTable)sqlNo.GetSDSevenNO(dt);
                if (dtNo!= null && dtNo.Rows.Count > 0)
                {

                    object mark_PUBLISHDATE = "PUBLISHDATE";
                    doc.Bookmarks.get_Item(ref mark_PUBLISHDATE).Range.Text = dtNo.Rows[0]["PROYEAR"].ToString();
                    object PUBLISHNO = "PUBLISHNO";
                    doc.Bookmarks.get_Item(ref PUBLISHNO).Range.Text = dtNo.Rows[0]["PRONO"].ToString();
                }

                //***********生成临时表,并插入数据***********************//
                //山东七地市-未来三天高/低潮预报
                TBLSEVENTIDE model = new TBLSEVENTIDE();
                model.PUBLISHDATE = dt;
                sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
                DataTable tblDYTide = (DataTable)sql.GetTideData(dtTide, model);
                if (tblDYTide.Rows.Count == 0) { }
                else
                {
                    dtCX.Columns.Add("PUBLISHDATE", typeof(string));
                    dtCX.Columns.Add("FORECASTDATE", typeof(string));
                    dtCX.Columns.Add("NOTFFIRSTHIGHWAVETIME", typeof(string));
                    dtCX.Columns.Add("NOTFFIRSTHIGHWAVEHEIGHT", typeof(string));
                    dtCX.Columns.Add("NOTFFIRSTLOWWAVETIME", typeof(string));
                    dtCX.Columns.Add("NOTFFIRSTLOWWAVEHEIGHT", typeof(string));
                    dtCX.Columns.Add("NOTFSECONDHIGHWAVETIME", typeof(string));
                    dtCX.Columns.Add("NOTFSECONDHIGHWAVEHEIGHT", typeof(string));
                    dtCX.Columns.Add("NOTFSECONDLOWWAVETIME", typeof(string));
                    dtCX.Columns.Add("NOTFSECONDLOWWAVEHEIGHT", typeof(string));
                    foreach (System.Data.DataRow row in tblDYTide.Rows)
                    {
                        var FORECASTDATE = DateTime.Parse(row["FORECASTDATE"].ToString());
                        DataRow rowCX = dtCX.NewRow();
                        rowCX["PUBLISHDATE"] = row["PUBLISHDATE"].ToString();
                        rowCX["FORECASTDATE"] = FORECASTDATE.Month + "月" + FORECASTDATE.Day + "日";
                        rowCX["NOTFFIRSTHIGHWAVETIME"] = row["FIRSTHIGHTIME"].ToString();
                        rowCX["NOTFFIRSTHIGHWAVEHEIGHT"] = row["FIRSTHIGHLEVEL"].ToString();
                        rowCX["NOTFFIRSTLOWWAVETIME"] = row["FIRSTLOWTIME"].ToString();
                        rowCX["NOTFFIRSTLOWWAVEHEIGHT"] = row["FIRSTLOWLEVEL"].ToString();
                        rowCX["NOTFSECONDHIGHWAVETIME"] = row["SECONDHIGHTIME"].ToString();
                        rowCX["NOTFSECONDHIGHWAVEHEIGHT"] = row["SECONDHIGHLEVEL"].ToString();
                        rowCX["NOTFSECONDLOWWAVETIME"] = row["SECONDLOWTIME"].ToString();
                        rowCX["NOTFSECONDLOWWAVEHEIGHT"] = row["SECONDLOWLEVEL"].ToString();
                        dtCX.Rows.Add(rowCX);
                    }
                }

                //山东七地市-未来三天的海面风及海浪有效波高预报（20时起报）
                TBLSEVENWAVE modelWave = new TBLSEVENWAVE();
                modelWave.PUBLISHDATE = dt;
                sql_TBLSEVENFORECAST sqlWave = new sql_TBLSEVENFORECAST();
                DataTable tblDYWave = (DataTable)sqlWave.GetWaveData(dbWave,modelWave);

                if (tblDYWave!= null && tblDYWave.Rows.Count == 0)
                {

                }
                else
                {
                    dtFL.Columns.Add("PUBLISHDATE", typeof(string));
                    dtFL.Columns.Add("FORECASTDATE", typeof(string));
                    dtFL.Columns.Add("WINDDIRECTION", typeof(string));
                    dtFL.Columns.Add("WINDFORCE", typeof(string));
                    dtFL.Columns.Add("WAVEHEIGHT", typeof(string));
                    foreach (System.Data.DataRow row in tblDYWave.Rows)
                    {
                        DataRow rowFL = dtFL.NewRow();
                        rowFL["PUBLISHDATE"] = row["PUBLISHDATE"].ToString();
                        rowFL["FORECASTDATE"] = row["FORECASTDATE"].ToString();
                        rowFL["WINDDIRECTION"] = row["WINDDIRECTION"].ToString();
                        rowFL["WINDFORCE"] = row["WINDFORCE"].ToString();
                        rowFL["WAVEHEIGHT"] = row["WAVEHEIGHT"].ToString();
                        dtFL.Rows.Add(rowFL);
                    }
                }
                //山东七地市-未来三天海温预报
                TBLSEVENTEMPERATURE modelTemperature = new TBLSEVENTEMPERATURE();
                modelTemperature.PUBLISHDATE = dt;
                sql_TBLSEVENFORECAST sqlTemperature = new sql_TBLSEVENFORECAST();
                DataTable tblSDTemperature = (DataTable)sqlTemperature.GetTemperatureData(dbTemperature, modelTemperature);

                if (tblSDTemperature != null && tblSDTemperature.Rows.Count == 0)
                {

                }
                else
                {
                    dtSW.Columns.Add("PUBLISHDATE", typeof(string));
                    dtSW.Columns.Add("FORECASTDATE", typeof(string));
                    dtSW.Columns.Add("WATERTEMPERATURE", typeof(string));
                    foreach (System.Data.DataRow row in tblSDTemperature.Rows)
                    {
                        DataRow rowSW = dtSW.NewRow();
                        var FORECASTDATE = DateTime.Parse(row["FORECASTDATE"].ToString());
                        rowSW["PUBLISHDATE"] = row["PUBLISHDATE"].ToString();
                        rowSW["FORECASTDATE"] = FORECASTDATE.Month + "月" + FORECASTDATE.Day + "日";
                        rowSW["WATERTEMPERATURE"] = row["WATERTEMPERATURE"].ToString();
                        dtSW.Rows.Add(rowSW);
                    }
                }

                //********************插入标签***********************//
                //东营埕岛-未来三天高/低潮预报
                object mark = "";
                for (int i = 0; i < 3; i++)
                {
                    mark = "FORECASTDATE" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["FORECASTDATE"].ToString();
                    mark = "NOTFFIRSTHIGHWAVETIME" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["NOTFFIRSTHIGHWAVETIME"].ToString();
                    mark = "NOTFFIRSTHIGHWAVEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["NOTFFIRSTHIGHWAVEHEIGHT"].ToString();
                    mark = "NOTFFIRSTLOWWAVETIME" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["NOTFFIRSTLOWWAVETIME"].ToString();
                    mark = "NOTFFIRSTLOWWAVEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["NOTFFIRSTLOWWAVEHEIGHT"].ToString();
                    mark = "NOTFSECONDHIGHWAVETIME" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["NOTFSECONDHIGHWAVETIME"].ToString();
                    mark = "NOTFSECONDHIGHWAVEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["NOTFSECONDHIGHWAVEHEIGHT"].ToString();
                    mark = "NOTFSECONDLOWWAVETIME" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["NOTFSECONDLOWWAVETIME"].ToString();
                    mark = "NOTFSECONDLOWWAVEHEIGHT" + (i + 1);
                    doc.Bookmarks.get_Item(ref mark).Range.Text = dtCX.Rows[i]["NOTFSECONDLOWWAVEHEIGHT"].ToString();
                }

                //东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
                object markWH = "";
                string dt1 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                string dt2 = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
                string dt3 = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
                string dt4 = DateTime.Now.AddDays(4).ToString("yyyy-MM-dd");
                for (int i = 0; i < 4; i++)
                {
                    string forecastDate = Convert.ToDateTime(dtFL.Rows[i]["FORECASTDATE"]).ToString("yyyy-MM-dd");
                    if (forecastDate == dt1)
                    {
                        markWH = "WINDDIRECTION1";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDDIRECTION"].ToString();
                        markWH = "WINDFORCE1";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDFORCE"].ToString();
                        markWH = "WAVEHEIGHT1";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WAVEHEIGHT"].ToString();
                    }
                    if (forecastDate == dt2)
                    {
                        markWH = "WINDDIRECTION2";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDDIRECTION"].ToString();
                        markWH = "WINDFORCE2";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDFORCE"].ToString();
                        markWH = "WAVEHEIGHT2";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WAVEHEIGHT"].ToString();
                    }
                    if (forecastDate == dt3)
                    {
                        markWH = "WINDDIRECTION3";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDDIRECTION"].ToString();
                        markWH = "WINDFORCE3";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDFORCE"].ToString();
                        markWH = "WAVEHEIGHT3";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WAVEHEIGHT"].ToString();
                    }
                    if (forecastDate == dt4)
                    {
                        markWH = "WINDDIRECTION4";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDDIRECTION"].ToString();
                        markWH = "WINDFORCE4";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDFORCE"].ToString();
                        markWH = "WAVEHEIGHT4";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WAVEHEIGHT"].ToString();
                    }
                }

                object markSW = "";
                string dtsw1 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                string dtsw2 = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
                string dtsw3 = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
                for (int i = 0; i < 3; i++)
                {
                    string swforecastDate = Convert.ToDateTime(dtSW.Rows[i]["FORECASTDATE"]).ToString("yyyy-MM-dd");
                    if (swforecastDate == dtsw1)
                    {
                        markWH = "SWFORECASTDATE1";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtSW.Rows[i]["FORECASTDATE"].ToString();
                        markWH = "WATERTEMPERATURE1";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtSW.Rows[i]["WATERTEMPERATURE"].ToString();
                    }
                    if (swforecastDate == dtsw2)
                    {
                        markWH = "SWFORECASTDATE2";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtSW.Rows[i]["FORECASTDATE"].ToString();
                        markWH = "WATERTEMPERATURE2";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtSW.Rows[i]["WATERTEMPERATURE"].ToString();
                    }
                    if (swforecastDate == dtsw3)
                    {
                        markWH = "SWFORECASTDATE3";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtSW.Rows[i]["FORECASTDATE"].ToString();
                        markWH = "WATERTEMPERATURE3";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtSW.Rows[i]["WATERTEMPERATURE"].ToString();
                    }
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
    }
}