/*
 * 变更记录 180711
 *上午九 潍坊港潮汐预报删除，取上午八潍坊港24小时预报生成预报单 edit by Yuy
 * 周报十一取消，从周报五中取潍坊数据
 */
using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;


public class TwentyWordAM
{
    public TwentyWordAM()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    //潍坊港潮汐临时表
    System.Data.DataTable dt4 = new DataTable("dt4");

    string PUBLISHTIME = "";
    string SDOSCWLOWESTWAVEHEIGHT = "";
    string SDOSCWSURFACETEMPERATURE = "";

    string SA24HWFBOHAIWAVEHEIGHT = "";
    string SA24HWFNORTHOFYSWAVEHEIGHT = "";
    string SA24HWFMIDDLEOFYSWAVEHEIGHT = "";
    string SA24HWFSOUTHOFYSWAVEHEIGHT = "";


    string WF24HTFFIRSTHIGHWAVETIME = "";//第一次高潮时
    string WF24HTFFIRSTHIGHWAVEHEIGHT = "";//第一次高潮高
    string WF24HTFSECONDHIGHWAVETIME = ""; //第一次低潮时
    string WF24HTFSECONDHIGHWAVEHEIGHT = "";//第一次低潮高
    string WF24HTFFIRSTLOWWAVETIME = "";//第二次高潮时
    string WF24HTFFIRSTLOWWAVEHEIGHT = "";//第二次高潮高
    string WF24HTFSECONDLOWWAVETIME = "";//第二次低潮时
    string WF24HTFSECONDLOWWAVEHEIGHT = "";//第二次低潮高

    /// <summary>
    /// 调用模板生成word
    /// </summary>
    /// <param name="templateFile">模板文件</param>
    /// <param name="fileName">生成的具有模板样式的新文件</param>
    public int ExportWord(string templateFile, string fileName, DateTime dt)
    {
        //生成word程序对象

        Word._Application app = new Word.Application();


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
            # region 填报信息表
            TBLFOOTER tblfooter_Model = new TBLFOOTER();
            tblfooter_Model.PUBLISHDATE = dt;
            //填报信息表提取数据
            System.Data.DataTable tblfooter = (System.Data.DataTable)new sql_TBLFOOTER().get_TBLFOOTER_AllData(tblfooter_Model);
            for (int i = 0; i < tblfooter.Rows.Count; i++)
            {
                string FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                //string FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                //string FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                string FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                string FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();
                string FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();

                string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                string FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话
                string FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话

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
            #endregion
            #region 风浪和水温
            //风浪和水温
            HT_TBLWF24HWAVEFORECAST tblexpectedfuture24hwavewater_Model = new HT_TBLWF24HWAVEFORECAST();
            tblexpectedfuture24hwavewater_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblexpectedfuture24hwavewater = (System.Data.DataTable)new Sql_HT_TBLWF24HWAVEFORECAST().get_TBLWF24HWAVEFORECAST_AllData(tblexpectedfuture24hwavewater_Model);
            if (tblexpectedfuture24hwavewater.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblexpectedfuture24hwavewater.Rows.Count; i++)
                {

                    SA24HWFBOHAIWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["SA24HWFBOHAIWAVEHEIGHT"].ToString();

                    SA24HWFNORTHOFYSWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["SA24HWFNORTHOFYSWAVEHEIGHT"].ToString();
                    SA24HWFMIDDLEOFYSWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["SA24HWFMIDDLEOFYSWAVEHEIGHT"].ToString();
                    SA24HWFSOUTHOFYSWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["SA24HWFSOUTHOFYSWAVEHEIGHT"].ToString();
                    SDOSCWLOWESTWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["SA24HWFOFFSHOREWAVEHEIGHT"].ToString();
                    SDOSCWSURFACETEMPERATURE = tblexpectedfuture24hwavewater.Rows[i]["SA24HWFOFFSHORESW"].ToString();
                }

            }
            #endregion

            #region 潍坊港24小时潮汐预报 
            //从上午八 潍坊港24小时预报获取 edit by yuy 180710
            //潍坊港24小时潮汐预报
            TBLWF24HTIDALFORECASTAM tblwf24htidalforecast_Model = new TBLWF24HTIDALFORECASTAM();
            //tblwf24htidalforecast_Model.PUBLISHDATE = dt;
            //System.Data.DataTable tblwf24htidalforecast = (System.Data.DataTable)new sql_TBLWF24HTIDALFORECASTAM
            //    ().get_TBLWF24HTIDALFORECASTAM_AllData(tblwf24htidalforecast_Model);
            //海上丝绸之路三天潮汐预报数据提取
            //海上丝绸之路三天海浪、气象预报
            sql_SilkWaveAndTide silkWaveAndTide = new sql_SilkWaveAndTide();
            TBLHARBOURTIDELEVEL tblharbourtidelevel_Model = new TBLHARBOURTIDELEVEL();
            tblharbourtidelevel_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblharbourtidelevel = (DataTable)silkWaveAndTide.GetSilkTide(tblharbourtidelevel_Model);
            if (tblharbourtidelevel.Rows.Count == 0) { }
            else
            {

                dt4.Columns.Add("PUBLISHDATE", typeof(string));
                dt4.Columns.Add("HTLHARBOUR", typeof(string));
                dt4.Columns.Add("FORECASTDATE", typeof(string));
                dt4.Columns.Add("HTLFIRSTWAVEOFTIME", typeof(string));
                dt4.Columns.Add("HTLFIRSTWAVETIDELEVEL", typeof(string));
                dt4.Columns.Add("HTLFIRSTTIMELOWTIDE", typeof(string));
                dt4.Columns.Add("HTLLOWTIDELEVELFORTHEFIRSTTIME", typeof(string));
                dt4.Columns.Add("HTLSECONDWAVEOFTIME", typeof(string));
                dt4.Columns.Add("HTLSECONDWAVETIDELEVEL", typeof(string));
                dt4.Columns.Add("HTLSECONDTIMELOWTIDE", typeof(string));
                dt4.Columns.Add("HTLLOWTIDELEVELFORTHESECONDTIM", typeof(string));

                for (int i = 0; i < tblharbourtidelevel.Rows.Count; i++)
                {
                    if (tblharbourtidelevel.Rows[i]["HTLHARBOUR"].ToString() == "潍坊港")
                    {
                        DataRow row =dt4.NewRow();
                        row["PUBLISHDATE"] = tblharbourtidelevel.Rows[i]["PUBLISHDATE"].ToString();
                        row["HTLHARBOUR"] = tblharbourtidelevel.Rows[i]["HTLHARBOUR"].ToString();
                        row["FORECASTDATE"] = tblharbourtidelevel.Rows[i]["FORECASTDATE"].ToString();
                        row["HTLFIRSTWAVEOFTIME"] = tblharbourtidelevel.Rows[i]["HTLFIRSTWAVEOFTIME"].ToString();
                        row["HTLFIRSTWAVETIDELEVEL"] = tblharbourtidelevel.Rows[i]["HTLFIRSTWAVETIDELEVEL"].ToString();
                        row["HTLFIRSTTIMELOWTIDE"] = tblharbourtidelevel.Rows[i]["HTLFIRSTTIMELOWTIDE"].ToString();
                        row["HTLLOWTIDELEVELFORTHEFIRSTTIME"] = tblharbourtidelevel.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                        row["HTLSECONDWAVEOFTIME"] = tblharbourtidelevel.Rows[i]["HTLSECONDWAVEOFTIME"].ToString();
                        row["HTLSECONDWAVETIDELEVEL"] = tblharbourtidelevel.Rows[i]["HTLSECONDWAVETIDELEVEL"].ToString();
                        row["HTLSECONDTIMELOWTIDE"] = tblharbourtidelevel.Rows[i]["HTLSECONDTIMELOWTIDE"].ToString();
                        row["HTLLOWTIDELEVELFORTHESECONDTIM"] = tblharbourtidelevel.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                        dt4.Rows.Add(row);
                    }
                }
                WF24HTFFIRSTHIGHWAVETIME= dt4.Rows[0]["HTLFIRSTWAVEOFTIME"].ToString();
                WF24HTFFIRSTHIGHWAVEHEIGHT= dt4.Rows[0]["HTLFIRSTWAVETIDELEVEL"].ToString();
                WF24HTFSECONDHIGHWAVETIME = dt4.Rows[0]["HTLSECONDWAVEOFTIME"].ToString();
                WF24HTFSECONDHIGHWAVEHEIGHT=dt4.Rows[0]["HTLSECONDWAVETIDELEVEL"].ToString();
                WF24HTFFIRSTLOWWAVETIME = dt4.Rows[0]["HTLFIRSTTIMELOWTIDE"].ToString();
                WF24HTFFIRSTLOWWAVEHEIGHT = dt4.Rows[0]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                WF24HTFSECONDLOWWAVETIME = dt4.Rows[0]["HTLSECONDTIMELOWTIDE"].ToString();
                WF24HTFSECONDLOWWAVEHEIGHT = dt4.Rows[0]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
            }
            //if (tblwf24htidalforecast.Rows.Count == 0) { }
            //else
            //{
            //    for (int i = 0; i < tblwf24htidalforecast.Rows.Count; i++)
            //    {
            //        WF24 HTF FIRST HIGH WAVE TIME = tblwf24htidalforecast.Rows[i]["WF24HTFFIRSTHIGHWAVETIME"].ToString();第一次高潮时
            //        WF24HT FFIRST HIGH WAVE HEIGHT = tblwf24htidalforecast.Rows[i]["WF24HTFFIRSTHIGHWAVEHEIGHT"].ToString();第一次高潮高
            //        WF24HTFSECONDHIGHWAVETIME = tblwf24htidalforecast.Rows[i]["WF24HTFSECONDHIGHWAVETIME"].ToString();
            //        WF24HTFSECONDHIGHWAVEHEIGHT = tblwf24htidalforecast.Rows[i]["WF24HTFSECONDHIGHWAVEHEIGHT"].ToString();
            //        WF24HTFFIRSTLOWWAVETIME = tblwf24htidalforecast.Rows[i]["WF24HTFFIRSTLOWWAVETIME"].ToString();
            //        WF24HTFFIRSTLOWWAVEHEIGHT = tblwf24htidalforecast.Rows[i]["WF24HTFFIRSTLOWWAVEHEIGHT"].ToString();
            //        WF24HTFSECONDLOWWAVETIME = tblwf24htidalforecast.Rows[i]["WF24HTFSECONDLOWWAVETIME"].ToString();
            //        WF24HTFSECONDLOWWAVEHEIGHT = tblwf24htidalforecast.Rows[i]["WF24HTFSECONDLOWWAVEHEIGHT"].ToString();

            //    }
            //}
            #endregion
            #region 书签
            //为了方便管理声明书签数组
            object[] BookMark = new object[24];//新增3个电话 21改为24
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真
            BookMark[3] = "FWAVEFORECASTER";//海浪预报员
            BookMark[4] = "FTIDALFORECASTER";//潮汐预报员
            BookMark[20] = "FWATERTEMPERATUREFORECASTER";//水温预报员
            BookMark[5] = "SDOSCWLOWESTWAVEHEIGHT";
            BookMark[6] = "SA24HWFBOHAIWAVEHEIGHT";
            BookMark[7] = "SA24HWFNORTHOFYSWAVEHEIGHT";
            BookMark[8] = "SA24HWFMIDDLEOFYSWAVEHEIGHT";
            BookMark[9] = "SA24HWFSOUTHOFYSWAVEHEIGHT";

            BookMark[10] = "WF24HTFFIRSTHIGHWAVETIME";
            BookMark[11] = "WF24HTFFIRSTHIGHWAVEHEIGHT";
            BookMark[12] = "WF24HTFSECONDHIGHWAVETIME";
            BookMark[13] = "WF24HTFSECONDHIGHWAVEHEIGHT";
            BookMark[14] = "WF24HTFFIRSTLOWWAVETIME";
            BookMark[15] = "WF24HTFFIRSTLOWWAVEHEIGHT";
            BookMark[16] = "WF24HTFSECONDLOWWAVETIME";
            BookMark[17] = "WF24HTFSECONDLOWWAVEHEIGHT";

            BookMark[18] = "SDOSCWSURFACETEMPERATURE";
            BookMark[19] = "PUBLISHTIME";

            BookMark[21] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[22] = "FTIDALFORECASTERTEL";//潮汐预报员电话
            BookMark[23] = "FWATERTEMPERATUREFORECASTERTEL";//水温预报员电话


            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = SDOSCWLOWESTWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = SA24HWFBOHAIWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = SA24HWFNORTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = SA24HWFMIDDLEOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = SA24HWFSOUTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = WF24HTFFIRSTHIGHWAVETIME;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = WF24HTFFIRSTHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = WF24HTFSECONDHIGHWAVETIME;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = WF24HTFSECONDHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = WF24HTFFIRSTLOWWAVETIME;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = WF24HTFFIRSTLOWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = WF24HTFSECONDLOWWAVETIME;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = WF24HTFSECONDLOWWAVEHEIGHT;

            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = SDOSCWSURFACETEMPERATURE;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = PUBLISHTIME;

            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;

            #endregion
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


