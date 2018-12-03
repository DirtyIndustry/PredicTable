using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;


public class ThirteenWord
{
    public ThirteenWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    string PUBLISHTIME = "";
    string QDTLFIRSTHIGHWAVEHOUR = "";
    string QDTLFIRSTHIGHWAVEMINUTE = "";
    string QDTLFIRSTHIGHWAVEHEIGHT = "";
    string QDTLFIRSTLOWWAVEHOUR = "";
    string QDTLFIRSTLOWWAVEMINUTE = "";
    string QDTLFIRSTLOWWAVEHEIGHT = "";

    string QDTLSECONDHIGHWAVEHOUR = "";
    string QDTLSECONDHIGHWAVEMINUTE = "";
    string QDTLSECONDHIGHWAVEHEIGHT = "";
    string QDTLSECONDLOWWAVEHOUR = "";
    string QDTLSECONDLOWWAVEMINUTE = "";
    string QDTLSECONDLOWWAVEHEIGHT = "";

    string SA24HWFBOHAIWAVEHEIGHT = "";
    string SA24HWFBOHAIWAVEDIR = "";
    string SA24HWFBOHAISURGEDIR = "";
    string SA24HWFNORTHOFYSWAVEHEIGHT = "";
    string SA24HWFNORTHOFYSWAVEDIR = "";
    string SA24HWFNORTHOFYSSURGEDIR = "";
    string SA24HWFMIDDLEOFYSWAVEHEIGHT = "";
    string SA24HWFMIDDLEOFYSWAVEDIR = "";
    string SA24HWFMIDDLEOFYSSURGEDIR = "";
    string SA24HWFSOUTHOFYSWAVEHEIGHT = "";
    string SA24HWFSOUTHOFYSWAVEDIR = "";
    string SA24HWFSOUTHOFYSSURGEDIR = "";

    string SA24HWFQDOFFSHOREWAVEHEIGHT = "";
    string SA24HWFQDOFFSHOREWAVEDIR = "";
    string SA24HWFQDOFFSHORESURGEDIR = "";
    //青岛滨海从表格TBLSDOFFSHORESEVENCITY24HWAVE获取数据


    /// <summary>
    /// 调用模板生成word
    /// </summary>
    /// <param name="templateFile">模板文件</param>
    /// <param name="fileName">生成的具有模板样式的新文件</param>
    public int ExportWord(string templateFile, string fileName, DateTime dt)
    {
        //生成word程序对象

        // Word._Application app = new Word.Application();
        Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

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
                //PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
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

            //24小时海浪预报
            TBLSEAAREA24HWAVEFORECAST tblseaarea24hwaveforecast_Model = new TBLSEAAREA24HWAVEFORECAST();
            tblseaarea24hwaveforecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblseaarea24hwaveforecast = (System.Data.DataTable)new sql_TBLSEAAREA24HWAVEFORECAST().get_TBLSEAAREA24HWAVEFORECAST_AllData(tblseaarea24hwaveforecast_Model);
            if (tblseaarea24hwaveforecast.Rows.Count == 0) { }
            else
            {
                SA24HWFBOHAIWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAIWAVEHEIGHT"].ToString();
                SA24HWFBOHAIWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAIWAVEDIR"].ToString();
                SA24HWFBOHAISURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAISURGEDIR"].ToString();
                SA24HWFNORTHOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSWAVEHEIGHT"].ToString();
                SA24HWFNORTHOFYSWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSWAVEDIR"].ToString();
                SA24HWFNORTHOFYSSURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSSURGEDIR"].ToString();
                SA24HWFMIDDLEOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSWAVEHEIGHT"].ToString();
                SA24HWFMIDDLEOFYSWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSWAVEDIR"].ToString();
                SA24HWFMIDDLEOFYSSURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSSURGEDIR"].ToString();
                SA24HWFSOUTHOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFSOUTHOFYSWAVEHEIGHT"].ToString();
                SA24HWFSOUTHOFYSWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFSOUTHOFYSWAVEDIR"].ToString();
                SA24HWFSOUTHOFYSSURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFSOUTHOFYSSURGEDIR"].ToString();
                SA24HWFQDOFFSHOREWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFQDOFFSHOREWAVEHEIGHT"].ToString();//青岛滨海
                SA24HWFQDOFFSHOREWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFQDOFFSHOREWAVEDIR"].ToString();
                SA24HWFQDOFFSHORESURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFQDOFFSHORESURGEDIR"].ToString();

            }

            #region //青岛24小时潮位预报 edit by Yuy 20180717 
            //TBLQD24HTIDELEVEL tblqd24htidelevel_Model = new TBLQD24HTIDELEVEL();
            //tblqd24htidelevel_Model.PUBLISHDATE = dt;
            //System.Data.DataTable tblqd24htidelevel = (System.Data.DataTable)new sql_TBLQD24HTIDELEVEL().get_TBLQD24HTIDELEVEL_AllData(tblqd24htidelevel_Model);
            //if (tblqd24htidelevel.Rows.Count == 0) { }
            //else
            //{
            //    QDTLFIRSTHIGHWAVEHOUR = tblqd24htidelevel.Rows[0]["QDTLFIRSTHIGHWAVEHOUR"].ToString();
            //    QDTLFIRSTHIGHWAVEMINUTE = tblqd24htidelevel.Rows[0]["QDTLFIRSTHIGHWAVEMINUTE"].ToString();
            //    QDTLFIRSTHIGHWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLFIRSTHIGHWAVEHEIGHT"].ToString();
            //    QDTLFIRSTLOWWAVEHOUR = tblqd24htidelevel.Rows[0]["QDTLFIRSTLOWWAVEHOUR"].ToString();
            //    QDTLFIRSTLOWWAVEMINUTE = tblqd24htidelevel.Rows[0]["QDTLFIRSTLOWWAVEMINUTE"].ToString();
            //    QDTLFIRSTLOWWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLFIRSTLOWWAVEHEIGHT"].ToString();
            //    QDTLSECONDHIGHWAVEHOUR = tblqd24htidelevel.Rows[0]["QDTLSECONDHIGHWAVEHOUR"].ToString();
            //    QDTLSECONDHIGHWAVEMINUTE = tblqd24htidelevel.Rows[0]["QDTLSECONDHIGHWAVEMINUTE"].ToString();
            //    QDTLSECONDHIGHWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLSECONDHIGHWAVEHEIGHT"].ToString();
            //    QDTLSECONDLOWWAVEHOUR = tblqd24htidelevel.Rows[0]["QDTLSECONDLOWWAVEHOUR"].ToString();
            //    QDTLSECONDLOWWAVEMINUTE = tblqd24htidelevel.Rows[0]["QDTLSECONDLOWWAVEMINUTE"].ToString();
            //    QDTLSECONDLOWWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLSECONDLOWWAVEHEIGHT"].ToString();

            //}["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
            #endregion
            //青岛24小时潮位预报 从下午三获取，原从下午四获取edit by Yuy 20180717 
            #region 青岛24小时潮位预报 潮汐时分
            TBLSDOFFSHORESEVENCITY24HTIDE tblsdoffshoresevencity24htide_Model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            tblsdoffshoresevencity24htide_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblsdoffshoresevencity24htide = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HTIDE().get24TideData(tblsdoffshoresevencity24htide_Model);
            if (tblsdoffshoresevencity24htide.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblsdoffshoresevencity24htide.Rows.Count; i++)
                {
                    if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                    {
                        QDTLFIRSTHIGHWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();//第一次工潮时
                        QDTLFIRSTHIGHWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();//第一次高潮分
                        QDTLFIRSTLOWWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();//第一次低潮时
                        QDTLFIRSTLOWWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();//第一次低潮分
                        QDTLSECONDHIGHWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();//第二次高潮时
                        QDTLSECONDHIGHWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();//第二次高潮分
                        QDTLSECONDLOWWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();//第二次低潮时
                        QDTLSECONDLOWWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();//第二次低潮分
                    }
                }
            }
            #endregion

            #region 青岛24小时潮位预报 潮高
            sql_TideData sql = new sql_TideData();
            HT_TideData model = new HT_TideData();
            model.PUBLISHDATE = dt;
            DataTable tideData = (DataTable)sql.get24TideData(model);
            if (tideData != null && tideData.Rows.Count > 0)
            {
                for (int i = 0; i < tideData.Rows.Count; i++)
                {
                    if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                    {

                        QDTLFIRSTHIGHWAVEHEIGHT = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();//第一次高潮潮高  
                        QDTLFIRSTLOWWAVEHEIGHT = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();   //第一次低潮潮高
                        QDTLSECONDHIGHWAVEHEIGHT = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();//第二次高潮潮高
                        QDTLSECONDLOWWAVEHEIGHT = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();//第二次低潮潮高
                    }
                }
            }
            #endregion

            //获取24小时海温预报数据
            var SDOSCWSURFACETEMPERATURE = "";
            TBLSDOFFSHORESEVENCITY24HWAVE TBLSDOFFSHORESEVENCITY24HWAVE = new TBLSDOFFSHORESEVENCITY24HWAVE();
            TBLSDOFFSHORESEVENCITY24HWAVE.PUBLISHDATE = dt;
            System.Data.DataTable TBLWEIHAI24FORCAST = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HWAVE().get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(TBLSDOFFSHORESEVENCITY24HWAVE);
            if (TBLWEIHAI24FORCAST.Rows.Count == 0) { }
            else
            {
                foreach (System.Data.DataRow row in TBLWEIHAI24FORCAST.Rows)
                {
                    var SDOSCWAREA = row["SDOSCWAREA"].ToString();
                    if (SDOSCWAREA == "青岛近海")
                    {
                        SDOSCWSURFACETEMPERATURE = row["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                }
            }
            //为了方便管理声明书签数组
            object[] BookMark = new object[38];//新增3个预报员电话35改为38
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真    
            BookMark[3] = "FWAVEFORECASTER"; //海浪预报员
            BookMark[4] = "FWATERTEMPERATUREFORECASTER";//水温预报员
            BookMark[5] = "FTIDALFORECASTER"; //海浪预报员

            BookMark[6] = "SA24HWFBOHAIWAVEHEIGHT";//第一次高潮时
            BookMark[7] = "SA24HWFBOHAIWAVEDIR";//第一次高潮分
            BookMark[8] = "SA24HWFBOHAISURGEDIR";//第一次高潮高度
            BookMark[9] = "SA24HWFNORTHOFYSWAVEHEIGHT";//第一次低潮时
            BookMark[10] = "SA24HWFNORTHOFYSWAVEDIR";//第一次低潮分
            BookMark[11] = "SA24HWFNORTHOFYSSURGEDIR";//第一次低潮高度
            BookMark[12] = "SA24HWFMIDDLEOFYSWAVEHEIGHT";//第二次高潮时
            BookMark[13] = "SA24HWFMIDDLEOFYSWAVEDIR";//第二次高潮分
            BookMark[14] = "SA24HWFMIDDLEOFYSSURGEDIR";//第二次高潮高度
            BookMark[15] = "SA24HWFSOUTHOFYSWAVEHEIGHT";//第二次低潮时
            BookMark[16] = "SA24HWFSOUTHOFYSWAVEDIR";//第二次低潮分
            BookMark[17] = "SA24HWFSOUTHOFYSSURGEDIR";//第二次低潮高度
            BookMark[18] = "SA24HWFQDOFFSHOREWAVEHEIGHT";//明日滨海浪高
            BookMark[19] = "SA24HWFQDOFFSHOREWAVEDIR";//浪向
            BookMark[20] = "SA24HWFQDOFFSHORESURGEDIR";//浪向

            BookMark[21] = "QDTLFIRSTHIGHWAVEHOUR";//第一次高潮时
            BookMark[22] = "QDTLFIRSTHIGHWAVEMINUTE";//第一次高潮分
            BookMark[23] = "QDTLFIRSTHIGHWAVEHEIGHT";//第一次高潮高度
            BookMark[24] = "QDTLFIRSTLOWWAVEHOUR";//第一次低潮时
            BookMark[25] = "QDTLFIRSTLOWWAVEMINUTE";//第一次低潮分
            BookMark[26] = "QDTLFIRSTLOWWAVEHEIGHT";//第一次低潮高度
            BookMark[27] = "QDTLSECONDHIGHWAVEHOUR";//第二次高潮时
            BookMark[28] = "QDTLSECONDHIGHWAVEMINUTE";//第二次高潮分
            BookMark[29] = "QDTLSECONDHIGHWAVEHEIGHT";//第二次高潮高度
            BookMark[30] = "QDTLSECONDLOWWAVEHOUR";//第二次低潮时
            BookMark[31] = "QDTLSECONDLOWWAVEMINUTE";//第二次低潮分
            BookMark[32] = "QDTLSECONDLOWWAVEHEIGHT";//第二次低潮高度
            BookMark[33] = "PUBLISHTIME";


            BookMark[34] = "SDOSCWSURFACETEMPERATURE";

            BookMark[35] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[36] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[37] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话

            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = SA24HWFBOHAIWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = SA24HWFBOHAIWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = SA24HWFBOHAISURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = SA24HWFNORTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = SA24HWFNORTHOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = SA24HWFNORTHOFYSSURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = SA24HWFMIDDLEOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = SA24HWFMIDDLEOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = SA24HWFMIDDLEOFYSSURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = SA24HWFSOUTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = SA24HWFSOUTHOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = SA24HWFSOUTHOFYSSURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = SA24HWFQDOFFSHOREWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = SA24HWFQDOFFSHOREWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = SA24HWFQDOFFSHORESURGEDIR;


            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = QDTLFIRSTHIGHWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = QDTLFIRSTHIGHWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = QDTLFIRSTHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = QDTLFIRSTLOWWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = QDTLFIRSTLOWWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = QDTLFIRSTLOWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = QDTLSECONDHIGHWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = QDTLSECONDHIGHWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = QDTLSECONDHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = QDTLSECONDLOWWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = QDTLSECONDLOWWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = QDTLSECONDLOWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = PUBLISHTIME;

            doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = SDOSCWSURFACETEMPERATURE;

            doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;

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
