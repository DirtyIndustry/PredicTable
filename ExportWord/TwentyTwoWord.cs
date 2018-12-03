using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

public class TwentyTwoWord
{
    public TwentyTwoWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    string PUBLISHTIME = "";
    string PREDATE = "";
    string QDC24HWFQDOFFSHOREWAVEHEIGHT = "";
    string QDC24HWFQDOFFSHOREWATERTEMP = "";
    string QDC24HWFJMOFFSHOREWAVEHEIGHT = "";
    string QDC24HWFJMOFFSHOREWATERTEMP = "";
    string QDC24HWFJZWOFFSHOREWAVEHEIGHT = "";
    string QDC24HWFJZWOFFSHOREWATERTEMP = "";
    string QDC24HWFJNOFFSHOREWAVEHEIGHT = "";
    string QDC24HWFJNOFFSHOREWATERTEMP = "";


    string SA24HWFBOHAIWAVEHEIGHT1 = "";
    string SA24HWFBOHAIWAVEHEIGHT2 = "";
    string SA24HWFBOHAIWAVETYPE = "";
    string SA24HWFNORTHOFYSWAVEHEIGHT1 = "";
    string SA24HWFNORTHOFYSWAVEHEIGHT2 = "";
    string SA24HWFNORTHOFYSWAVETYPE = "";
    string SA24HWFMIDDLEOFYSWAVEHEIGHT1 = "";
    string SA24HWFMIDDLEOFYSWAVEHEIGHT2 = "";
    string SA24HWFMIDDLEOFYSWAVETYPE = "";


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
    DateTime PUBLISHDATE = DateTime.Now;
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
            System.Data.DataTable tblfooter = new System.Data.DataTable();
            tblfooter = (System.Data.DataTable)new sql_TBLFOOTER().get_TBLFOOTER_AllData(tblfooter_Model);
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

                PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString());
                string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                PUBLISHTIME = PUBLISHDATE.Year + "年" + PUBLISHDATE.Month + "月" + PUBLISHDATE.Day + "日" + "15时";
                //PREDATE = PUBLISHDATE.Month + "月" + (PUBLISHDATE.Day + 1) + "日";
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
            //青岛24小时潮位预报
            #region 注掉不用
            //TBLSEAAREA24HWAVEFORECAST tblseaarea24hwaveforecast_Model = new TBLSEAAREA24HWAVEFORECAST();
            //tblseaarea24hwaveforecast_Model.PUBLISHDATE = dt;
            //System.Data.DataTable tblseaarea24hwaveforecast = (System.Data.DataTable)new sql_TBLSEAAREA24HWAVEFORECAST().get_TBLSEAAREA24HWAVEFORECAST_AllData(tblseaarea24hwaveforecast_Model);
            //if (tblseaarea24hwaveforecast.Rows.Count == 0) { }
            //else
            //{
            //    var SA24HWFBOHAIWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAIWAVEHEIGHT"].ToString();
            //    if (SA24HWFBOHAIWAVEHEIGHT != null)
            //    {
            //        var heightArr = SA24HWFBOHAIWAVEHEIGHT.Split(new string[] { "-", "增至", "减至"},StringSplitOptions.RemoveEmptyEntries);
            //        if(heightArr.Length ==2)
            //        {
            //            SA24HWFBOHAIWAVEHEIGHT1 = heightArr[0];
            //            SA24HWFBOHAIWAVEHEIGHT2 = heightArr[1];
            //        }
            //        else
            //            SA24HWFBOHAIWAVEHEIGHT1 = SA24HWFBOHAIWAVEHEIGHT;
            //    }
            //    SA24HWFBOHAIWAVETYPE = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAIWAVETYPE"].ToString();


            //    var SA24HWFNORTHOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSWAVEHEIGHT"].ToString();

            //    if (SA24HWFNORTHOFYSWAVEHEIGHT != null)
            //    {
            //        var heightArr = SA24HWFNORTHOFYSWAVEHEIGHT.Split(new string[] { "-", "增至", "减至" }, StringSplitOptions.RemoveEmptyEntries);
            //        if (heightArr.Length == 2)
            //        {
            //            SA24HWFNORTHOFYSWAVEHEIGHT1 = heightArr[0];
            //            SA24HWFNORTHOFYSWAVEHEIGHT2 = heightArr[1];
            //        }
            //        else
            //            SA24HWFNORTHOFYSWAVEHEIGHT1 = SA24HWFNORTHOFYSWAVEHEIGHT;
            //    }

            //    SA24HWFNORTHOFYSWAVETYPE = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSWAVETYPE"].ToString();

            //    var SA24HWFMIDDLEOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSWAVEHEIGHT"].ToString();

            //    if (SA24HWFMIDDLEOFYSWAVEHEIGHT != null)
            //    {
            //        var heightArr = SA24HWFMIDDLEOFYSWAVEHEIGHT.Split(new string[] { "-", "增至", "减至" }, StringSplitOptions.RemoveEmptyEntries);
            //        if (heightArr.Length == 2)
            //        {
            //            SA24HWFMIDDLEOFYSWAVEHEIGHT1 = heightArr[0];
            //            SA24HWFMIDDLEOFYSWAVEHEIGHT2 = heightArr[1];
            //        }
            //        else
            //            SA24HWFMIDDLEOFYSWAVEHEIGHT1 = SA24HWFMIDDLEOFYSWAVEHEIGHT;
            //    }

            //    SA24HWFMIDDLEOFYSWAVETYPE = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSWAVETYPE"].ToString();

            //}
            #endregion 
            //海浪
            sql_TBLEACHSEAAREA24HSEAWAVE sql = new sql_TBLEACHSEAAREA24HSEAWAVE();
            TBLEACHSEAAREA24HSEAWAVE model = new TBLEACHSEAAREA24HSEAWAVE();
            model.PUBLISHDATE = dt;
            DataTable tblseaarea24hwaveforecast = (DataTable)sql.get_TBLEACHSEAAREA24HSEAWAVE_AllData(model);
            if (tblseaarea24hwaveforecast.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblseaarea24hwaveforecast.Rows.Count; i++)
                {
                    var ESASWAREA = tblseaarea24hwaveforecast.Rows[i]["ESASWAREA"].ToString();
                    if (ESASWAREA == "渤海")
                    {
                        SA24HWFBOHAIWAVEHEIGHT1 = tblseaarea24hwaveforecast.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                        SA24HWFBOHAIWAVEHEIGHT2 = tblseaarea24hwaveforecast.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                        SA24HWFBOHAIWAVETYPE = tblseaarea24hwaveforecast.Rows[i]["ESASWWAVETYPE"].ToString();
                    }
                    else if (ESASWAREA == "黄海北部")
                    {
                        SA24HWFNORTHOFYSWAVEHEIGHT1 = tblseaarea24hwaveforecast.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                        SA24HWFNORTHOFYSWAVEHEIGHT2 = tblseaarea24hwaveforecast.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                        SA24HWFNORTHOFYSWAVETYPE = tblseaarea24hwaveforecast.Rows[i]["ESASWWAVETYPE"].ToString();
                    }
                    else if (ESASWAREA == "黄海中部")
                    {
                        SA24HWFMIDDLEOFYSWAVEHEIGHT1 = tblseaarea24hwaveforecast.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                        SA24HWFMIDDLEOFYSWAVEHEIGHT2 = tblseaarea24hwaveforecast.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                        SA24HWFMIDDLEOFYSWAVETYPE = tblseaarea24hwaveforecast.Rows[i]["ESASWWAVETYPE"].ToString();
                    }
                }
            }

            //青岛周边海域24小时预报
            TBLQDCIRCUM24HWATERFORECAST tblqdcircum24Hwaterforecast_Model = new TBLQDCIRCUM24HWATERFORECAST();
            tblqdcircum24Hwaterforecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblqdcircum24Hwaterforecast = (System.Data.DataTable)new sql_TBLQDCIRCUM24HWATERFORECAST().get_TBLQDCIRCUM24HWATERFORECAST_AllData(tblqdcircum24Hwaterforecast_Model);
            if (tblqdcircum24Hwaterforecast.Rows.Count == 0) { }
            else
            {
                QDC24HWFQDOFFSHOREWAVEHEIGHT = tblqdcircum24Hwaterforecast.Rows[0]["QDC24HWFQDOFFSHOREWAVEHEIGHT"].ToString();
                QDC24HWFQDOFFSHOREWATERTEMP = tblqdcircum24Hwaterforecast.Rows[0]["QDC24HWFQDOFFSHOREWATERTEMP"].ToString();
                QDC24HWFJMOFFSHOREWAVEHEIGHT = tblqdcircum24Hwaterforecast.Rows[0]["QDC24HWFJMOFFSHOREWAVEHEIGHT"].ToString();
                QDC24HWFJMOFFSHOREWATERTEMP = tblqdcircum24Hwaterforecast.Rows[0]["QDC24HWFJMOFFSHOREWATERTEMP"].ToString();
                QDC24HWFJZWOFFSHOREWAVEHEIGHT = tblqdcircum24Hwaterforecast.Rows[0]["QDC24HWFJZWOFFSHOREWAVEHEIGHT"].ToString();
                QDC24HWFJZWOFFSHOREWATERTEMP = tblqdcircum24Hwaterforecast.Rows[0]["QDC24HWFJZWOFFSHOREWATERTEMP"].ToString();
                QDC24HWFJNOFFSHOREWAVEHEIGHT = tblqdcircum24Hwaterforecast.Rows[0]["QDC24HWFJNOFFSHOREWAVEHEIGHT"].ToString();
                QDC24HWFJNOFFSHOREWATERTEMP = tblqdcircum24Hwaterforecast.Rows[0]["QDC24HWFJNOFFSHOREWATERTEMP"].ToString();
            }

            //海区48小时潮汐预报 修改从下午三青岛获取48小时预报 edit by Yuy 20180717
            
            //TBLQDCOAST48HTIDALFORECAST tblqd48htidelevel_Model = new TBLQDCOAST48HTIDALFORECAST();
            //tblqd48htidelevel_Model.PUBLISHDATE = dt;
            //System.Data.DataTable tblqd48htidelevel = (System.Data.DataTable)new sql_TBLQDCOAST48HTIDALFORECAST().get_TBLQDCOAST48HTIDALFORECAST_AllData(tblqd48htidelevel_Model);
            //if (tblqd48htidelevel.Rows.Count == 0) { }
            //else
            //{
            #region 从七地市中获取青岛48小时潮汐时、分预报

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
                        if (forecastdate == fdate2) {
                            QDTLFIRSTHIGHWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                            QDTLFIRSTHIGHWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                            //QDTLFIRSTHIGHWAVEHEIGHT = tblsdoffshoresevencity24htide.Rows[0]["QDC48HTFFIRSTHIGHWAVEHEIGHT"].ToString();
                            QDTLFIRSTLOWWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                            QDTLFIRSTLOWWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                            //QDTLFIRSTLOWWAVEHEIGHT = tblsdoffshoresevencity24htide.Rows[0]["QDC48HTFSECONDHIGHWAVEHEIGHT"].ToString();
                            QDTLSECONDHIGHWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                            QDTLSECONDHIGHWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                            //QDTLSECONDHIGHWAVEHEIGHT = tblsdoffshoresevencity24htide.Rows[0]["QDC48HTFFIRSTLOWWAVEHEIGHT"].ToString();
                            QDTLSECONDLOWWAVEHOUR = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                            QDTLSECONDLOWWAVEMINUTE = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            //QDTLSECONDLOWWAVEHEIGHT = tblsdoffshoresevencity24htide.Rows[0]["QDC48HTFSECONDLOWWAVEHEIGHT"].ToString();
                        }
                    }
                }

                // QDTLTOMORROWWAVEHEIGHT = tblqd48htidelevel.Rows[0]["QDTLTOMORROWWAVEHEIGHT"].ToString();
                // QDTLTOMORROWWAVEDIR = tblqd48htidelevel.Rows[0]["QDTLTOMORROWWAVEDIR"].ToString();

            }
            //}
            #endregion
            #region 从七地市潮汐中获取青岛48小时潮汐潮高
            sql_TideData sql1 = new sql_TideData();
            HT_TideData model1 = new HT_TideData();
            model1.PUBLISHDATE = dt;
            DataTable tideData = (DataTable)sql1.getTideData(model1);
            if (tideData != null && tideData.Rows.Count > 0)
            {
                var fdate1 = dt.AddDays(1).ToString();
                var fdate2 = dt.AddDays(2).ToString();
                var fdate3 = dt.AddDays(3).ToString();
                for (int i = 0; i < tideData.Rows.Count; i++)
                {
                    var forecastdate = Convert.ToDateTime(tideData.Rows[i]["FORECASTDATE"]).ToString();
                    var datef = Convert.ToDateTime(forecastdate).Day.ToString();
                    if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                    {
                        if (forecastdate == fdate2)
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

            var TIMEMONTH = "";
            var TIMEDAY = "";
            TIMEMONTH = dt.AddDays(2).Month.ToString();
            TIMEDAY = dt.AddDays(2).Day.ToString();
            //为了方便管理声明书签数组
            object[] BookMark = new object[42];//新增3个预报员电话39改为42
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班

            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真    
            BookMark[3] = "FWAVEFORECASTER"; //海浪预报员
            BookMark[4] = "FTIDALFORECASTER"; //海浪预报员
            BookMark[5] = "FWATERTEMPERATUREFORECASTER";//水温预报员

            BookMark[6] = "SA24HWFBOHAIWAVEHEIGHT1";//第一次高潮时 SA24HWFBOHAIWAVEHEIGHT1
            BookMark[7] = "SA24HWFNORTHOFYSWAVEHEIGHT1";//第一次低潮时
            BookMark[8] = "SA24HWFMIDDLEOFYSWAVEHEIGHT1";//第二次高潮时

            BookMark[9] = "QDC24HWFQDOFFSHOREWAVEHEIGHT";//第一次高潮时
            BookMark[10] = "QDC24HWFQDOFFSHOREWATERTEMP";//第一次低潮时
            BookMark[11] = "QDC24HWFJMOFFSHOREWAVEHEIGHT";//第二次高潮时
            BookMark[12] = "QDC24HWFJMOFFSHOREWATERTEMP";//第一次高潮时
            BookMark[13] = "QDC24HWFJZWOFFSHOREWAVEHEIGHT";//第一次低潮时
            BookMark[14] = "QDC24HWFJZWOFFSHOREWATERTEMP";//第二次高潮时
            BookMark[15] = "QDC24HWFJNOFFSHOREWAVEHEIGHT";//第一次高潮时
            BookMark[16] = "QDC24HWFJNOFFSHOREWATERTEMP";//第一次低潮时

            //BookMark[17] = "QDTLFIRSTHIGHWAVEHOUR";//第一次高潮时
            //BookMark[18] = "QDTLFIRSTHIGHWAVEMINUTE";//第一次低潮时
            //BookMark[19] = "QDTLFIRSTHIGHWAVEHEIGHT";//第二次高潮时
            //BookMark[20] = "QDTLFIRSTLOWWAVEHOUR";//第一次高潮时
            //BookMark[21] = "QDTLFIRSTLOWWAVEMINUTE";//第一次低潮时
            //BookMark[22] = "QDTLFIRSTLOWWAVEHEIGHT";//第二次高潮时
            //BookMark[23] = "QDTLSECONDHIGHWAVEHOUR";//第一次高潮时
            //BookMark[24] = "QDTLSECONDHIGHWAVEMINUTE";//第一次低潮时
            //BookMark[25] = "QDTLSECONDHIGHWAVEHEIGHT";//第一次高潮时
            //BookMark[26] = "QDTLSECONDLOWWAVEHOUR";//第一次低潮时
            //BookMark[27] = "QDTLSECONDLOWWAVEMINUTE";//第二次高潮时
            //BookMark[28] = "QDTLSECONDLOWWAVEHEIGHT";//第一次高潮时


            //改-sl
            BookMark[17] = "QDTLFIRSTHIGHWAVEHOUR";//第一次高潮时
            BookMark[18] = "QDTLFIRSTHIGHWAVEMINUTE";//第一次高潮分
            BookMark[19] = "QDTLFIRSTHIGHWAVEHEIGHT";//第一次高潮高
            BookMark[20] = "QDTLSECONDHIGHWAVEHOUR";//"QDTLFIRSTLOWWAVEHOUR";//第二次高潮时
            BookMark[21] = "QDTLSECONDHIGHWAVEMINUTE";//"QDTLFIRSTLOWWAVEMINUTE";//第二次高潮分
            BookMark[22] = "QDTLSECONDHIGHWAVEHEIGHT";//"QDTLFIRSTLOWWAVEHEIGHT";//第二次高潮潮高
            BookMark[23] = "QDTLFIRSTLOWWAVEHOUR"; // "QDTLSECONDHIGHWAVEHOUR";//第一次低潮时
            BookMark[24] = "QDTLFIRSTLOWWAVEMINUTE"; //"QDTLSECONDHIGHWAVEMINUTE";//第一次低潮分
            BookMark[25] = "QDTLFIRSTLOWWAVEHEIGHT"; //"QDTLSECONDHIGHWAVEHEIGHT";//第一次低潮潮高
            BookMark[26] = "QDTLSECONDLOWWAVEHOUR";//第二次低潮时
            BookMark[27] = "QDTLSECONDLOWWAVEMINUTE";//第二次低潮分
            BookMark[28] = "QDTLSECONDLOWWAVEHEIGHT";//第二次低潮高




            BookMark[29] = "SA24HWFBOHAIWAVETYPE";//渤海波级
            BookMark[30] = "SA24HWFNORTHOFYSWAVETYPE";//渤海北部波级
            BookMark[31] = "SA24HWFMIDDLEOFYSWAVETYPE";//黄海中部波级

            BookMark[32] = "SA24HWFBOHAIWAVEHEIGHT2";//第一次高潮时
            BookMark[33] = "SA24HWFNORTHOFYSWAVEHEIGHT2";//第一次低潮时
            BookMark[34] = "SA24HWFMIDDLEOFYSWAVEHEIGHT2";//第二次高潮时

            BookMark[35] = "PUBLISHTIME";
            BookMark[36] = "PREDATE";

            //时间
            BookMark[37] = "TIMEMONTH";
            BookMark[38] = "TIMEDAY";

            BookMark[39] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[40] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[41] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话
            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = SA24HWFBOHAIWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = SA24HWFNORTHOFYSWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = SA24HWFMIDDLEOFYSWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = QDC24HWFQDOFFSHOREWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = QDC24HWFQDOFFSHOREWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = QDC24HWFJMOFFSHOREWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = QDC24HWFJMOFFSHOREWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = QDC24HWFJZWOFFSHOREWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = QDC24HWFJZWOFFSHOREWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = QDC24HWFJNOFFSHOREWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = QDC24HWFJNOFFSHOREWATERTEMP;

            
            
            
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = QDTLFIRSTHIGHWAVEHOUR; //高
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = QDTLFIRSTHIGHWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = QDTLFIRSTHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = QDTLSECONDHIGHWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = QDTLSECONDHIGHWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = QDTLSECONDHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = QDTLFIRSTLOWWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = QDTLFIRSTLOWWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = QDTLFIRSTLOWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = QDTLSECONDLOWWAVEHOUR;
            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = QDTLSECONDLOWWAVEMINUTE;
            doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = QDTLSECONDLOWWAVEHEIGHT;//低

            doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = SA24HWFBOHAIWAVETYPE;
            doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = SA24HWFNORTHOFYSWAVETYPE;
            doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = SA24HWFMIDDLEOFYSWAVETYPE;

            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = SA24HWFBOHAIWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = SA24HWFNORTHOFYSWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = SA24HWFMIDDLEOFYSWAVEHEIGHT2;

            doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = PUBLISHTIME;
            //doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = PREDATE;

            doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = TIMEMONTH;
            doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = TIMEDAY;

            doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;

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
