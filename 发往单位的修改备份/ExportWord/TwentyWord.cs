using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

/// <summary>
/// TwentyWord 的摘要说明
/// </summary>
public class TwentyWord
{
    public TwentyWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    string PUBLISHTIME = "";
    string SDOSCWLOWESTWAVEHEIGHT = "";
    string SDOSCWSURFACETEMPERATURE = "";

    string SA24HWFBOHAIWAVEHEIGHT = "";
    string SA24HWFNORTHOFYSWAVEHEIGHT = "";
    string SA24HWFMIDDLEOFYSWAVEHEIGHT = "";
    string SA24HWFSOUTHOFYSWAVEHEIGHT = "";


    string WF24HTFFIRSTHIGHWAVETIME = "";
    string WF24HTFFIRSTHIGHWAVEHEIGHT = "";
    string WF24HTFSECONDHIGHWAVETIME = "";
    string WF24HTFSECONDHIGHWAVEHEIGHT = "";
    string WF24HTFFIRSTLOWWAVETIME = "";
    string WF24HTFFIRSTLOWWAVEHEIGHT = "";
    string WF24HTFSECONDLOWWAVETIME = "";
    string WF24HTFSECONDLOWWAVEHEIGHT = "";

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
        try { 
        //数据库查询
       
        TBLFOOTER tblfooter_Model = new TBLFOOTER();
        tblfooter_Model.PUBLISHDATE = dt;
            //填报信息表提取数据
            # region 填报信息
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
                PUBLISHTIME = PUBLISHDATE + "15时";

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

            #region 黄河南海堤数据
            //黄河南海堤表提取数据
            TBLSDOFFSHORESEVENCITY24HWAVE tblsdoffshoresevencity24hwave_Model = new TBLSDOFFSHORESEVENCITY24HWAVE();
        tblsdoffshoresevencity24hwave_Model.PUBLISHDATE = dt;
        System.Data.DataTable tblsdoffshoresevencity24hwave = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HWAVE().get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(tblsdoffshoresevencity24hwave_Model);
        if (tblsdoffshoresevencity24hwave.Rows.Count == 0) { }
        else
        {
            for (int i = 0; i < tblsdoffshoresevencity24hwave.Rows.Count; i++)
            {

                if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "潍坊近海")
                {
                    SDOSCWLOWESTWAVEHEIGHT = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                    SDOSCWSURFACETEMPERATURE = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                }
        }
            #endregion

            # region 海区24小时海浪预报
            //海区24小时海浪预报
            TBLSEAAREA24HWAVEFORECAST tblseaarea24hwaveforecast_Model = new TBLSEAAREA24HWAVEFORECAST();
        tblseaarea24hwaveforecast_Model.PUBLISHDATE = dt;
        System.Data.DataTable tblseaarea24hwaveforecast = (System.Data.DataTable)new sql_TBLSEAAREA24HWAVEFORECAST().get_TBLSEAAREA24HWAVEFORECAST_AllData(tblseaarea24hwaveforecast_Model);
        if (tblseaarea24hwaveforecast.Rows.Count == 0) { }
        else
        {
            for (int i = 0; i < tblseaarea24hwaveforecast.Rows.Count; i++)
            {

                SA24HWFBOHAIWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[i]["SA24HWFBOHAIWAVEHEIGHT"].ToString();


                SA24HWFNORTHOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[i]["SA24HWFNORTHOFYSWAVEHEIGHT"].ToString();



                SA24HWFMIDDLEOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[i]["SA24HWFMIDDLEOFYSWAVEHEIGHT"].ToString();



                SA24HWFSOUTHOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[i]["SA24HWFSOUTHOFYSWAVEHEIGHT"].ToString();



            }
        }
            #endregion

            #region 潍坊港24小时潮汐预报
            //下午十二取消不用，从下午三潍坊去24小时预报
            //edit by Yuy 180718
            //潍坊港24小时潮汐预报
            #region 取消不用
            //TBLWF24HTIDALFORECAST tblwf24htidalforecast_Model = new TBLWF24HTIDALFORECAST();
            //tblwf24htidalforecast_Model.PUBLISHDATE = dt;
            //System.Data.DataTable tblwf24htidalforecast = (System.Data.DataTable)new sql_TBLWF24HTIDALFORECAST().get_TBLWF24HTIDALFORECAST_AllData(tblwf24htidalforecast_Model);
            //if (tblwf24htidalforecast.Rows.Count == 0) { }
            //else
            //{
            //    for (int i = 0; i < tblwf24htidalforecast.Rows.Count; i++)
            //    {
            //        WF24HTFFIRSTHIGHWAVETIME = tblwf24htidalforecast.Rows[i]["WF24HTFFIRSTHIGHWAVETIME"].ToString();
            //        WF24HTFFIRSTHIGHWAVEHEIGHT = tblwf24htidalforecast.Rows[i]["WF24HTFFIRSTHIGHWAVEHEIGHT"].ToString();
            //        WF24HTFSECONDHIGHWAVETIME = tblwf24htidalforecast.Rows[i]["WF24HTFSECONDHIGHWAVETIME"].ToString();
            //        WF24HTFSECONDHIGHWAVEHEIGHT = tblwf24htidalforecast.Rows[i]["WF24HTFSECONDHIGHWAVEHEIGHT"].ToString();
            //        WF24HTFFIRSTLOWWAVETIME = tblwf24htidalforecast.Rows[i]["WF24HTFFIRSTLOWWAVETIME"].ToString();
            //        WF24HTFFIRSTLOWWAVEHEIGHT = tblwf24htidalforecast.Rows[i]["WF24HTFFIRSTLOWWAVEHEIGHT"].ToString();
            //        WF24HTFSECONDLOWWAVETIME = tblwf24htidalforecast.Rows[i]["WF24HTFSECONDLOWWAVETIME"].ToString();
            //        WF24HTFSECONDLOWWAVEHEIGHT = tblwf24htidalforecast.Rows[i]["WF24HTFSECONDLOWWAVEHEIGHT"].ToString();
            //    }
            //}
            #endregion

            #region 获取潮时
            TBLSDOFFSHORESEVENCITY24HTIDE tblsdoffshoresevencity24htide_Model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            tblsdoffshoresevencity24htide_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblsdoffshoresevencity24htide = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HTIDE().get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(tblsdoffshoresevencity24htide_Model);
            if (tblsdoffshoresevencity24htide.Rows.Count == 0) { }
            else
            {
                var fdate1 = dt.AddDays(1).ToString();
                for (int i = 0; i < tblsdoffshoresevencity24htide.Rows.Count; i++)
                {
                    var forecastdate = Convert.ToDateTime(tblsdoffshoresevencity24htide.Rows[i]["FORECASTDATE"]).ToString();
                    var datef = Convert.ToDateTime(forecastdate).Day.ToString();
                    if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "潍坊")
                    {
                        if (forecastdate == fdate1)
                        {
                            WF24HTFFIRSTHIGHWAVETIME = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString()+ tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString(); 
                             WF24HTFSECONDHIGHWAVETIME = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString()+tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                            WF24HTFFIRSTLOWWAVETIME = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString()+tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                            WF24HTFSECONDLOWWAVETIME = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString()+ tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                        }
                    }
                }
            }
            #endregion

            #region 获取潮高
            sql_TideData sql1 = new sql_TideData();
            HT_TideData model1 = new HT_TideData();
            model1.PUBLISHDATE = dt;
            DataTable tideData = (DataTable)sql1.getTideData(model1);
            if (tideData != null && tideData.Rows.Count > 0)
            {
                var fdate1 = dt.AddDays(1).ToString();
                for (int i = 0; i < tideData.Rows.Count; i++)
                {
                    var forecastdate = Convert.ToDateTime(tideData.Rows[i]["FORECASTDATE"]).ToString();
                    var datef = Convert.ToDateTime(forecastdate).Day.ToString();
                    if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "潍坊")
                    {
                        if (forecastdate == fdate1)
                        {
                            WF24HTFFIRSTHIGHWAVEHEIGHT = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                            WF24HTFFIRSTLOWWAVEHEIGHT = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                            WF24HTFSECONDHIGHWAVEHEIGHT = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                            WF24HTFSECONDLOWWAVEHEIGHT = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                        }
                    }
                }
            }
            #endregion
            #endregion

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
