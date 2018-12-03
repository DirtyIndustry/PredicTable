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
    /// 东营埕岛油田海洋环境预报
    /// </summary>
    public class DongYingOilWord
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

        /// <summary>
        /// 调用模板生成word
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="fileName">生成的具有模板样式的新文件</param>
        public int ExportWord(string templateFile, string fileName,DateTime dt)
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

                string ZHIBANTEL = "";//预报值班
                string SENDTEL = "";//预报发送
                string FWAVEFORECASTERTEL = "";//海浪预报员电话
                string FTIDALFORECASTERTEL = "";//潮汐电话


                for (int i = 0; i < tblfooter.Rows.Count; i++)
                {
                    FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                    //FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                    //FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                    FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                    FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();

                    ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                    SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                    FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                    FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话


                    string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                    PUBLISHTIME = PUBLISHDATE + "16时";

                    tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                    //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                    //tblfooter_Model.FFAX = FFAX;
                    tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;
                    tblfooter_Model.FTIDALFORECASTER = FTIDALFORECASTER;

                    tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                    tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                    tblfooter_Model.FWAVEFORECASTERTEL = FWAVEFORECASTERTEL;
                    tblfooter_Model.FTIDALFORECASTERTEL = FTIDALFORECASTERTEL;
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


                //***********获取预报编号*******************************//
                Sql_DYNO sqlNo = new Sql_DYNO();
                DataTable dtNo = (DataTable)sqlNo.GetDYNo(dt);
                if (dtNo.Rows.Count > 0)
                {

                    object mark_PUBLISHDATE = "PUBLISHDATE";
                    doc.Bookmarks.get_Item(ref mark_PUBLISHDATE).Range.Text = dtNo.Rows[0]["PROYEAR"].ToString();
                    object PUBLISHNO = "PUBLISHNO";
                    doc.Bookmarks.get_Item(ref PUBLISHNO).Range.Text = dtNo.Rows[0]["PRONO"].ToString();
                }

                //***********生成临时表,并插入数据***********************//
                //东营埕岛-未来三天高/低潮预报
                HT_DYTIDEFORECAST model = new HT_DYTIDEFORECAST();
                model.PUBLISHDATE = dt;
                Sql_DYTIDEFOREAST sql = new Sql_DYTIDEFOREAST();
                DataTable tblDYTide = (DataTable)sql.GetDyTideForecastData(model);
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
                        rowCX["NOTFFIRSTHIGHWAVETIME"] = row["NOTFFIRSTHIGHWAVETIME"].ToString();
                        rowCX["NOTFFIRSTHIGHWAVEHEIGHT"] = row["NOTFFIRSTHIGHWAVEHEIGHT"].ToString();
                        rowCX["NOTFFIRSTLOWWAVETIME"] = row["NOTFFIRSTLOWWAVETIME"].ToString();
                        rowCX["NOTFFIRSTLOWWAVEHEIGHT"] = row["NOTFFIRSTLOWWAVEHEIGHT"].ToString();
                        rowCX["NOTFSECONDHIGHWAVETIME"] = row["NOTFSECONDHIGHWAVETIME"].ToString();
                        rowCX["NOTFSECONDHIGHWAVEHEIGHT"] = row["NOTFSECONDHIGHWAVEHEIGHT"].ToString();
                        rowCX["NOTFSECONDLOWWAVETIME"] = row["NOTFSECONDLOWWAVETIME"].ToString();
                        rowCX["NOTFSECONDLOWWAVEHEIGHT"] = row["NOTFSECONDLOWWAVEHEIGHT"].ToString();
                        dtCX.Rows.Add(rowCX);
                    }
                }

                //东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
                HT_DYWAVEFORECAST modelWave = new HT_DYWAVEFORECAST();
                modelWave.PUBLISHDATE = dt;
                Sql_DYWAVEFOREAST sqlWave = new Sql_DYWAVEFOREAST();
                DataTable tblDYWave = (DataTable)sqlWave.GetDyWaveForecastData(modelWave);

                if (tblDYWave.Rows.Count == 0)
                {

                }
                else
                {
                    dtFL.Columns.Add("PUBLISHDATE", typeof(string));
                    dtFL.Columns.Add("TIMEEFFECTIVE", typeof(string));
                    dtFL.Columns.Add("WINDDIRECTION", typeof(string));
                    dtFL.Columns.Add("WINDFORCE", typeof(string));
                    dtFL.Columns.Add("WAVEHEIGHT", typeof(string));
                    foreach (System.Data.DataRow row in tblDYWave.Rows)
                    {
                        DataRow rowFL = dtFL.NewRow();
                        rowFL["PUBLISHDATE"] = row["PUBLISHDATE"].ToString();
                        rowFL["TIMEEFFECTIVE"] = row["TIMEEFFECTIVE"].ToString();
                        rowFL["WINDDIRECTION"] = row["WINDDIRECTION"].ToString();
                        rowFL["WINDFORCE"] = row["WINDFORCE"].ToString();
                        rowFL["WAVEHEIGHT"] = row["WAVEHEIGHT"].ToString();
                        dtFL.Rows.Add(rowFL);
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
                for (int i = 0; i < 4; i++)
                {
                    if(dtFL.Rows[i]["TIMEEFFECTIVE"].ToString() == "12")
                    {
                        markWH = "WINDDIRECTION1";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDDIRECTION"].ToString();
                        markWH = "WINDFORCE1";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDFORCE"].ToString();
                        markWH = "WAVEHEIGHT1";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WAVEHEIGHT"].ToString();
                    }
                    if (dtFL.Rows[i]["TIMEEFFECTIVE"].ToString() == "24")
                    {
                        markWH = "WINDDIRECTION2";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDDIRECTION"].ToString();
                        markWH = "WINDFORCE2";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDFORCE"].ToString();
                        markWH = "WAVEHEIGHT2";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WAVEHEIGHT"].ToString();
                    }
                    if (dtFL.Rows[i]["TIMEEFFECTIVE"].ToString() == "48")
                    {
                        markWH = "WINDDIRECTION3";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDDIRECTION"].ToString();
                        markWH = "WINDFORCE3";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDFORCE"].ToString();
                        markWH = "WAVEHEIGHT3";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WAVEHEIGHT"].ToString();
                    }
                    if (dtFL.Rows[i]["TIMEEFFECTIVE"].ToString() == "72")
                    {
                        markWH = "WINDDIRECTION4";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDDIRECTION"].ToString();
                        markWH = "WINDFORCE4";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WINDFORCE"].ToString();
                        markWH = "WAVEHEIGHT4";
                        doc.Bookmarks.get_Item(ref markWH).Range.Text = dtFL.Rows[i]["WAVEHEIGHT"].ToString();
                    }
                }


                //*******************ftp图片下载添加**********//
                //公司内网测试网址：ftp://192.168.2.33 123qwer
                //海洋局网址：ftp://192.168.103.52  123qwer,
                string dateNow = dt.ToString("yyyyMMdd");
                string newFilePath = System.Web.HttpContext.Current.Server.MapPath("/ftp/" + dateNow);
                if (!Directory.Exists(newFilePath))
                {
                    Directory.CreateDirectory(newFilePath);
                }
                string[] docName = FtpDownLoad.GetFtpFileNames("ftp://192.168.103.52/dongying/", "szyb", "123qwer,");

                List<string> list = new List<string>();
                string deleteName = "UV-"+ dateNow + "00-012-d04-dv.jpg";
                for(int j = 0; j < docName.Length; j++)
                {
                    if(!docName[j].ToString().Contains("-012-"))
                    {
                        list.Add(docName[j].ToString());
                    }
                }
                string[] newDocName = list.ToArray();
                for (int i = 0; i < newDocName.Length; i++)
                {
                    FtpDownLoad.FTPDownloadFile("ftp://192.168.103.52/dongying/" + newDocName[i], "szyb", "123qwer,", newFilePath);
                }
                if(newDocName.Length > 0)
                {
                    object[] BookMark = new object[6];
                    BookMark[0] = "PIC1";
                    BookMark[1] = "PIC2";
                    BookMark[2] = "PIC3";
                    BookMark[3] = "PIC4";
                    BookMark[4] = "PIC5";
                    BookMark[5] = "PIC6";

                    for (int i = 0; i < newDocName.Length; i++)
                    {
                        InsertImg(doc, app, BookMark, newFilePath + "/" + newDocName[i], i);
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
        public void InsertImg(Microsoft.Office.Interop.Word.Document doc, Microsoft.Office.Interop.Word.Application app, object[] BookMark, string imgName, int bookMarkPosition)
        {
            try
            {
                object Nothing = System.Reflection.Missing.Value;
                //定义插入图片是否随word文档一起保存
                object saveWithDocument = true;

                if (doc.Bookmarks.Exists(Convert.ToString(BookMark[bookMarkPosition])) == true)
                {
                    //查找书签
                    doc.Bookmarks.get_Item(ref BookMark[bookMarkPosition]).Select();
                    object linkToFile = true;
                    //设置图片位置
                    app.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    //在书签的位置添加图片
                    Word.InlineShape inlineShape = app.Selection.InlineShapes.AddPicture(imgName, ref linkToFile,
                        ref saveWithDocument, ref Nothing);
                    //设置图片大小
                    inlineShape.Width = 410;
                    inlineShape.Height = 330;

                    doc.Save();
                }
                else
                {

                    //word文档中不存在该书签，关闭文档

                    //doc.Close(ref Nothing, ref Nothing, ref Nothing);
                }
            }
            catch
            {

            }
        }


        private string ConvertTimeStr(string timeStr)
        {
            return (string.IsNullOrEmpty(timeStr) || timeStr.Contains("-")) ?
                timeStr : timeStr.Substring(0, 2) + "时" + timeStr.Substring(timeStr.Length - 2) + "分";
        }
    }
}