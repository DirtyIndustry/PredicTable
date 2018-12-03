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
/// NineWord 的摘要说明
/// </summary>
public class NineWord
{
    public NineWord()
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
    string QDTLTOMORROWWAVEHEIGHT = "";
    string QDTLTOMORROWWAVEDIR = "";
    string QDOFFSHOREWATERTEMP = "";


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
            #region 填报信息
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

            TBLSDOFFSHORESEVENCITY24HWAVE tblsdoffshoresevencity24hwave_Model = new TBLSDOFFSHORESEVENCITY24HWAVE();
            tblsdoffshoresevencity24hwave_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblsdoffshoresevencity24hwave = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HWAVE().get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(tblsdoffshoresevencity24hwave_Model);
            if (tblsdoffshoresevencity24hwave.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblsdoffshoresevencity24hwave.Rows.Count; i++)
                {
                    if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "青岛近海")
                    {
                        QDOFFSHOREWATERTEMP = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                }
            }
            #endregion

            //青岛24小时潮位预报 改从下午三青岛24小时获取
            #region 浪高、浪向、水温
            TBLQD24HTIDELEVEL tblqd24htidelevel_Model = new TBLQD24HTIDELEVEL();
            tblqd24htidelevel_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblqd24htidelevel = (System.Data.DataTable)new sql_TBLQD24HTIDELEVEL().get_TBLQD24HTIDELEVEL_AllData(tblqd24htidelevel_Model);
            if (tblqd24htidelevel.Rows.Count == 0) { }
            else
            {
                //QDTLFIRSTHIGHWAVEHOUR = tblqd24htidelevel.Rows[0]["QDTLFIRSTHIGHWAVEHOUR"].ToString();
                //QDTLFIRSTHIGHWAVEMINUTE = tblqd24htidelevel.Rows[0]["QDTLFIRSTHIGHWAVEMINUTE"].ToString();
                //QDTLFIRSTHIGHWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLFIRSTHIGHWAVEHEIGHT"].ToString();
                //QDTLFIRSTLOWWAVEHOUR = tblqd24htidelevel.Rows[0]["QDTLFIRSTLOWWAVEHOUR"].ToString();
                //QDTLFIRSTLOWWAVEMINUTE = tblqd24htidelevel.Rows[0]["QDTLFIRSTLOWWAVEMINUTE"].ToString();
                //QDTLFIRSTLOWWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLFIRSTLOWWAVEHEIGHT"].ToString();
                //QDTLSECONDHIGHWAVEHOUR = tblqd24htidelevel.Rows[0]["QDTLSECONDHIGHWAVEHOUR"].ToString();
                //QDTLSECONDHIGHWAVEMINUTE = tblqd24htidelevel.Rows[0]["QDTLSECONDHIGHWAVEMINUTE"].ToString();
                //QDTLSECONDHIGHWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLSECONDHIGHWAVEHEIGHT"].ToString();
                //QDTLSECONDLOWWAVEHOUR = tblqd24htidelevel.Rows[0]["QDTLSECONDLOWWAVEHOUR"].ToString();
                //QDTLSECONDLOWWAVEMINUTE = tblqd24htidelevel.Rows[0]["QDTLSECONDLOWWAVEMINUTE"].ToString();
                QDTLSECONDLOWWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLSECONDLOWWAVEHEIGHT"].ToString();
                QDTLTOMORROWWAVEHEIGHT = tblqd24htidelevel.Rows[0]["QDTLTOMORROWWAVEHEIGHT"].ToString();
                QDTLTOMORROWWAVEDIR = tblqd24htidelevel.Rows[0]["QDTLTOMORROWWAVEDIR"].ToString();
            }
            #endregion

            #region 下午三获取青岛潮汐24小时潮时
            TBLSDOFFSHORESEVENCITY24HTIDE tblsdoffshoresevencity24htide_Model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            tblsdoffshoresevencity24htide_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblsdoffshoresevencity24htide = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HTIDE().get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(tblsdoffshoresevencity24htide_Model);
            if (tblsdoffshoresevencity24htide.Rows.Count == 0) { }
            else
            {
                var fdate1 = dt.AddDays(1).ToString();
                var fdate2 = dt.AddDays(2).ToString();
                var fdate3 = dt.AddDays(3).ToString();
                for (int i = 0; i < tblsdoffshoresevencity24htide.Rows.Count; i++)
                {
                    var forecastdate = Convert.ToDateTime(tblsdoffshoresevencity24htide.Rows[i]["FORECASTDATE"]).ToString();
                    var datef = Convert.ToDateTime(forecastdate).Day.ToString();
                    if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                    {
                        if (forecastdate == fdate1)
                        {
                            QDTLFIRSTHIGHWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();//第一次高潮时
                            QDTLFIRSTHIGHWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString() ; //第一次高潮分
                            QDTLSECONDHIGHWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString() ;//第二次高潮时
                            QDTLSECONDHIGHWAVEMINUTE =tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString() ;//第二次高潮分
                            QDTLFIRSTLOWWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString() ;//第一次低潮时
                            QDTLFIRSTLOWWAVEMINUTE =tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();//第一次低潮分
                            QDTLSECONDLOWWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString(); //第二次低潮时
                            QDTLSECONDLOWWAVEMINUTE =tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();//第二次低潮分
                        }
                    }
                }
            }
            #endregion
            #region 获取下午三青岛潮汐24小时潮高
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
                    if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                    {
                        if (forecastdate == fdate1)
                        {
                            QDTLFIRSTHIGHWAVEHEIGHT = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                            QDTLFIRSTLOWWAVEHEIGHT = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                            QDTLSECONDHIGHWAVEHEIGHT = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                            QDTLSECONDLOWWAVEHEIGHT = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                        }
                    }
                }
            }
            #endregion
            //为了方便管理声明书签数组
            object[] BookMark = new object[25];//新增3个预报员电话22改为25
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真    
            BookMark[3] = "FWAVEFORECASTER"; //海浪预报员
            BookMark[4] = "FTIDALFORECASTER";//潮汐预报员
            BookMark[5] = "FWATERTEMPERATUREFORECASTER";//水温预报员

            BookMark[6] = "QDTLFIRSTHIGHWAVEHOUR";//第一次高潮时
            BookMark[7] = "QDTLFIRSTHIGHWAVEMINUTE";//第一次高潮分
            BookMark[8] = "QDTLFIRSTHIGHWAVEHEIGHT";//第一次高潮高度
            BookMark[9] = "QDTLFIRSTLOWWAVEHOUR";//第一次低潮时
            BookMark[10] = "QDTLFIRSTLOWWAVEMINUTE";//第一次低潮分
            BookMark[11] = "QDTLFIRSTLOWWAVEHEIGHT";//第一次低潮高度
            BookMark[12] = "QDTLSECONDHIGHWAVEHOUR";//第二次高潮时
            BookMark[13] = "QDTLSECONDHIGHWAVEMINUTE";//第二次高潮分
            BookMark[14] = "QDTLSECONDHIGHWAVEHEIGHT";//第二次高潮高度
            BookMark[15] = "QDTLSECONDLOWWAVEHOUR";//第二次低潮时
            BookMark[16] = "QDTLSECONDLOWWAVEMINUTE";//第二次低潮分
            BookMark[17] = "QDTLSECONDLOWWAVEHEIGHT";//第二次低潮高度
            BookMark[18] = "QDTLTOMORROWWAVEHEIGHT";//明日滨海浪高
            BookMark[19] = "QDTLTOMORROWWAVEDIR";//浪向
            BookMark[20] = "PUBLISHTIME";//
            BookMark[21] = "QDOFFSHOREWATERTEMP";

            BookMark[22] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[23] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[24] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话

            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = QDTLFIRSTHIGHWAVEHOUR;

            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = QDTLFIRSTHIGHWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = QDTLFIRSTHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = QDTLFIRSTLOWWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = QDTLFIRSTLOWWAVEMINUTE;


            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = QDTLFIRSTLOWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = QDTLSECONDHIGHWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = QDTLSECONDHIGHWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = QDTLSECONDHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = QDTLSECONDLOWWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = QDTLSECONDLOWWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = QDTLSECONDLOWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = QDTLTOMORROWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = QDTLTOMORROWWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = PUBLISHTIME;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = QDOFFSHOREWATERTEMP;

            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;


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