using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

/// <summary>
/// TenWord 的摘要说明
/// </summary>
public class TenWord
{
    public TenWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    string PUBLISHTIME = "";

    string FORECASTDATE1 = "";
    string FORECASTDATE2 = "";
    string FORECASTDATE3 = "";
    string NWFWTFWAVEHEIGHT1 = "";
    string NWFWTFWAVEHEIGHT2 = "";
    string NWFWTFWAVEHEIGHT3 = "";
    string NWFWTFWAVEDIR1 = "";
    string NWFWTFWAVEDIR2 = "";
    string NWFWTFWAVEDIR3 = "";
    string NWFWTFFLOWDIR1 = "";
    string NWFWTFFLOWDIR2 = "";
    string NWFWTFFLOWDIR3 = "";
    string NWFWTFFLOWLEVEL1 = "";
    string NWFWTFFLOWLEVEL2 = "";
    string NWFWTFFLOWLEVEL3 = "";
    string NWFWTFWATERTEMPERATURE1 = "";
    string NWFWTFWATERTEMPERATURE2 = "";
    string NWFWTFWATERTEMPERATURE3 = "";
    string NWFWTFWEATHER1 = "";
    string NWFWTFWEATHER2 = "";
    string NWFWTFWEATHER3 = "";

    string FORECASTDATE21 = "";
    string FORECASTDATE22 = "";
    string FORECASTDATE23 = "";
    string NOTFFIRSTHIGHWAVETIME1 = "";
    string NOTFFIRSTHIGHWAVETIME2 = "";
    string NOTFFIRSTHIGHWAVETIME3 = "";
    string NOTFFIRSTHIGHWAVEHEIGHT1 = "";
    string NOTFFIRSTHIGHWAVEHEIGHT2 = "";
    string NOTFFIRSTHIGHWAVEHEIGHT3 = "";
    string NOTFFIRSTLOWWAVETIME1 = "";
    string NOTFFIRSTLOWWAVETIME2 = "";
    string NOTFFIRSTLOWWAVETIME3 = "";
    string NOTFFIRSTLOWWAVEHEIGHT1 = "";
    string NOTFFIRSTLOWWAVEHEIGHT2 = "";
    string NOTFFIRSTLOWWAVEHEIGHT3 = "";
    string NOTFSECONDHIGHWAVETIME1 = "";
    string NOTFSECONDHIGHWAVETIME2 = "";
    string NOTFSECONDHIGHWAVETIME3 = "";
    string NOTFSECONDHIGHWAVEHEIGHT1 = "";
    string NOTFSECONDHIGHWAVEHEIGHT2 = "";
    string NOTFSECONDHIGHWAVEHEIGHT3 = "";
    string NOTFSECONDLOWWAVETIME1 = "";
    string NOTFSECONDLOWWAVETIME2 = "";
    string NOTFSECONDLOWWAVETIME3 = "";
    string NOTFSECONDLOWWAVEHEIGHT1 = "";
    string NOTFSECONDLOWWAVEHEIGHT2 = "";
    string NOTFSECONDLOWWAVEHEIGHT3 = "";

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
            //南堡油田海域波浪、风、水温预报
            TBLNANPUWAVEFLOWWATERTFORECAST tblnanpuwaveflowwatertforecast_Model = new TBLNANPUWAVEFLOWWATERTFORECAST();
            tblnanpuwaveflowwatertforecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblnanpuwaveflowwatertforecast = (System.Data.DataTable)new sql_TBLNANPUWAVEFLOWWATERTFORECAST().get_TBLNANPUWAVEFLOWWATERTFORECAST_AllData(tblnanpuwaveflowwatertforecast_Model);
            if (tblnanpuwaveflowwatertforecast.Rows.Count == 0) { }
            else
            {
                FORECASTDATE1 = tblnanpuwaveflowwatertforecast.Rows[0]["FORECASTDATE"].ToString();
                if (FORECASTDATE1 != null || FORECASTDATE1 != "")
                {
                    string[] FORECASTDATE11 = FORECASTDATE1.Split(' ');
                    FORECASTDATE1 = FORECASTDATE11[0].Substring(5).Replace("/", "月") + "日";
                }
                FORECASTDATE2 = tblnanpuwaveflowwatertforecast.Rows[1]["FORECASTDATE"].ToString();
                if (FORECASTDATE2 != null || FORECASTDATE2 != "")
                {
                    string[] FORECASTDATE22 = FORECASTDATE2.Split(' ');
                    FORECASTDATE2 = FORECASTDATE22[0].Substring(5).Replace("/", "月") + "日";
                }
                FORECASTDATE3 = tblnanpuwaveflowwatertforecast.Rows[2]["FORECASTDATE"].ToString();
                if (FORECASTDATE3 != null || FORECASTDATE3 != "")
                {
                    string[] FORECASTDATE33 = FORECASTDATE3.Split(' ');
                    FORECASTDATE3 = FORECASTDATE33[0].Substring(5).Replace("/", "月") + "日";
                }
                NWFWTFWAVEHEIGHT1 = tblnanpuwaveflowwatertforecast.Rows[0]["NWFWTFWAVEHEIGHT"].ToString();
                NWFWTFWAVEHEIGHT2 = tblnanpuwaveflowwatertforecast.Rows[1]["NWFWTFWAVEHEIGHT"].ToString();
                NWFWTFWAVEHEIGHT3 = tblnanpuwaveflowwatertforecast.Rows[2]["NWFWTFWAVEHEIGHT"].ToString();
                NWFWTFWAVEDIR1 = tblnanpuwaveflowwatertforecast.Rows[0]["NWFWTFWAVEDIR"].ToString();
                NWFWTFWAVEDIR2 = tblnanpuwaveflowwatertforecast.Rows[1]["NWFWTFWAVEDIR"].ToString();
                NWFWTFWAVEDIR3 = tblnanpuwaveflowwatertforecast.Rows[2]["NWFWTFWAVEDIR"].ToString();
                NWFWTFFLOWDIR1 = tblnanpuwaveflowwatertforecast.Rows[0]["NWFWTFFLOWDIR"].ToString();
                NWFWTFFLOWDIR2 = tblnanpuwaveflowwatertforecast.Rows[1]["NWFWTFFLOWDIR"].ToString();
                NWFWTFFLOWDIR3 = tblnanpuwaveflowwatertforecast.Rows[2]["NWFWTFFLOWDIR"].ToString();
                NWFWTFFLOWLEVEL1 = tblnanpuwaveflowwatertforecast.Rows[0]["NWFWTFFLOWLEVEL"].ToString();
                NWFWTFFLOWLEVEL2 = tblnanpuwaveflowwatertforecast.Rows[1]["NWFWTFFLOWLEVEL"].ToString();
                NWFWTFFLOWLEVEL3 = tblnanpuwaveflowwatertforecast.Rows[2]["NWFWTFFLOWLEVEL"].ToString();
                NWFWTFWATERTEMPERATURE1 = tblnanpuwaveflowwatertforecast.Rows[0]["NWFWTFWATERTEMPERATURE"].ToString();
                NWFWTFWATERTEMPERATURE2 = tblnanpuwaveflowwatertforecast.Rows[1]["NWFWTFWATERTEMPERATURE"].ToString();
                NWFWTFWATERTEMPERATURE3 = tblnanpuwaveflowwatertforecast.Rows[2]["NWFWTFWATERTEMPERATURE"].ToString();
                NWFWTFWEATHER1 = tblnanpuwaveflowwatertforecast.Rows[0]["NWFWTFWEATHER"].ToString();
                NWFWTFWEATHER2 = tblnanpuwaveflowwatertforecast.Rows[1]["NWFWTFWEATHER"].ToString();
                NWFWTFWEATHER3 = tblnanpuwaveflowwatertforecast.Rows[2]["NWFWTFWEATHER"].ToString();

            }
            //南堡油田海域潮汐预报
            TBLNANPUOILFIELDTIDALFORECAST tblnanpuoilfieldtidalforcecast_Model = new TBLNANPUOILFIELDTIDALFORECAST();
            tblnanpuoilfieldtidalforcecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblnanpuoilfieldtidalforcecast = (System.Data.DataTable)new sql_TBLNANPUOILFIELDTIDALFORECAST().get_TBLNANPUOILFIELDTIDALFORECAST_AllData(tblnanpuoilfieldtidalforcecast_Model);
            if (tblnanpuoilfieldtidalforcecast.Rows.Count == 0) { }
            else
            {
                FORECASTDATE21 = tblnanpuoilfieldtidalforcecast.Rows[0]["FORECASTDATE"].ToString();
                if (FORECASTDATE21 != null || FORECASTDATE21 != "")
                {
                    string[] FORECASTDATE221 = FORECASTDATE21.Split(' ');
                    FORECASTDATE21 = FORECASTDATE221[0].Substring(5).Replace("/", "月") + "日";
                }
                FORECASTDATE22 = tblnanpuoilfieldtidalforcecast.Rows[1]["FORECASTDATE"].ToString();
                if (FORECASTDATE22 != null || FORECASTDATE22 != "")
                {
                    string[] FORECASTDATE222 = FORECASTDATE22.Split(' ');
                    FORECASTDATE22 = FORECASTDATE222[0].Substring(5).Replace("/", "月") + "日";
                }
                FORECASTDATE23 = tblnanpuoilfieldtidalforcecast.Rows[2]["FORECASTDATE"].ToString();
                if (FORECASTDATE23 != null || FORECASTDATE23 != "")
                {
                    string[] FORECASTDATE223 = FORECASTDATE23.Split(' ');
                    FORECASTDATE23 = FORECASTDATE223[0].Substring(5).Replace("/", "月") + "日";
                }
                NOTFFIRSTHIGHWAVETIME1 = tblnanpuoilfieldtidalforcecast.Rows[0]["NOTFFIRSTHIGHWAVETIME"].ToString();
                NOTFFIRSTHIGHWAVETIME2 = tblnanpuoilfieldtidalforcecast.Rows[1]["NOTFFIRSTHIGHWAVETIME"].ToString();
                NOTFFIRSTHIGHWAVETIME3 = tblnanpuoilfieldtidalforcecast.Rows[2]["NOTFFIRSTHIGHWAVETIME"].ToString();
                NOTFFIRSTHIGHWAVEHEIGHT1 = tblnanpuoilfieldtidalforcecast.Rows[0]["NOTFFIRSTHIGHWAVEHEIGHT"].ToString();
                NOTFFIRSTHIGHWAVEHEIGHT2 = tblnanpuoilfieldtidalforcecast.Rows[1]["NOTFFIRSTHIGHWAVEHEIGHT"].ToString();
                NOTFFIRSTHIGHWAVEHEIGHT3 = tblnanpuoilfieldtidalforcecast.Rows[2]["NOTFFIRSTHIGHWAVEHEIGHT"].ToString();
                NOTFFIRSTLOWWAVETIME1 = tblnanpuoilfieldtidalforcecast.Rows[0]["NOTFFIRSTLOWWAVETIME"].ToString();
                NOTFFIRSTLOWWAVETIME2 = tblnanpuoilfieldtidalforcecast.Rows[1]["NOTFFIRSTLOWWAVETIME"].ToString();
                NOTFFIRSTLOWWAVETIME3 = tblnanpuoilfieldtidalforcecast.Rows[2]["NOTFFIRSTLOWWAVETIME"].ToString();
                NOTFFIRSTLOWWAVEHEIGHT1 = tblnanpuoilfieldtidalforcecast.Rows[0]["NOTFFIRSTLOWWAVEHEIGHT"].ToString();
                NOTFFIRSTLOWWAVEHEIGHT2 = tblnanpuoilfieldtidalforcecast.Rows[1]["NOTFFIRSTLOWWAVEHEIGHT"].ToString();
                NOTFFIRSTLOWWAVEHEIGHT3 = tblnanpuoilfieldtidalforcecast.Rows[2]["NOTFFIRSTLOWWAVEHEIGHT"].ToString();
                NOTFSECONDHIGHWAVETIME1 = tblnanpuoilfieldtidalforcecast.Rows[0]["NOTFSECONDHIGHWAVETIME"].ToString();
                NOTFSECONDHIGHWAVETIME2 = tblnanpuoilfieldtidalforcecast.Rows[1]["NOTFSECONDHIGHWAVETIME"].ToString();
                NOTFSECONDHIGHWAVETIME3 = tblnanpuoilfieldtidalforcecast.Rows[2]["NOTFSECONDHIGHWAVETIME"].ToString();
                NOTFSECONDHIGHWAVEHEIGHT1 = tblnanpuoilfieldtidalforcecast.Rows[0]["NOTFSECONDHIGHWAVEHEIGHT"].ToString();
                NOTFSECONDHIGHWAVEHEIGHT2 = tblnanpuoilfieldtidalforcecast.Rows[1]["NOTFSECONDHIGHWAVEHEIGHT"].ToString();
                NOTFSECONDHIGHWAVEHEIGHT3 = tblnanpuoilfieldtidalforcecast.Rows[2]["NOTFSECONDHIGHWAVEHEIGHT"].ToString();
                NOTFSECONDLOWWAVETIME1 = tblnanpuoilfieldtidalforcecast.Rows[0]["NOTFSECONDLOWWAVETIME"].ToString();
                NOTFSECONDLOWWAVETIME2 = tblnanpuoilfieldtidalforcecast.Rows[1]["NOTFSECONDLOWWAVETIME"].ToString();
                NOTFSECONDLOWWAVETIME3 = tblnanpuoilfieldtidalforcecast.Rows[2]["NOTFSECONDLOWWAVETIME"].ToString();
                NOTFSECONDLOWWAVEHEIGHT1 = tblnanpuoilfieldtidalforcecast.Rows[0]["NOTFSECONDLOWWAVEHEIGHT"].ToString();
                NOTFSECONDLOWWAVEHEIGHT2 = tblnanpuoilfieldtidalforcecast.Rows[1]["NOTFSECONDLOWWAVEHEIGHT"].ToString();
                NOTFSECONDLOWWAVEHEIGHT3 = tblnanpuoilfieldtidalforcecast.Rows[2]["NOTFSECONDLOWWAVEHEIGHT"].ToString();

            }
            //为了方便管理声明书签数组
            object[] BookMark = new object[58];//新增3个预报员55改为58
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真
            BookMark[3] = "FWAVEFORECASTER";//海浪预报员
            BookMark[4] = "FTIDALFORECASTER"; //潮汐预报员
            BookMark[5] = "FWATERTEMPERATUREFORECASTER"; //水温预报员
            BookMark[6] = "FORECASTDATE1";//南堡油田海域日期1
            BookMark[7] = "FORECASTDATE2";//南堡油田海域日期2
            BookMark[8] = "FORECASTDATE3";//南堡油田海域日期3
            BookMark[9] = "NWFWTFWAVEHEIGHT1";//南堡油田海域波高1
            BookMark[10] = "NWFWTFWAVEHEIGHT2";//南堡油田海域波高2
            BookMark[11] = "NWFWTFWAVEHEIGHT3";//南堡油田海域波高3
            BookMark[12] = "NWFWTFWAVEDIR1";//南堡油田海域波向1
            BookMark[13] = "NWFWTFWAVEDIR2";//南堡油田海域波向2
            BookMark[14] = "NWFWTFWAVEDIR3";//南堡油田海域波向3
            BookMark[15] = "NWFWTFFLOWDIR1";//南堡油田海域风向1
            BookMark[16] = "NWFWTFFLOWDIR2";//南堡油田海域风向2
            BookMark[17] = "NWFWTFFLOWDIR3";//南堡油田海域风向3
            BookMark[18] = "NWFWTFFLOWLEVEL1";//南堡油田海域风力
            BookMark[19] = "NWFWTFFLOWLEVEL2";//南堡油田海域风力
            BookMark[20] = "NWFWTFFLOWLEVEL3";//南堡油田海域风力
            BookMark[21] = "NWFWTFWATERTEMPERATURE1";// 南堡油田海域水温
            BookMark[22] = "NWFWTFWATERTEMPERATURE2";// 南堡油田海域水温
            BookMark[23] = "NWFWTFWATERTEMPERATURE3";// 南堡油田海域水温
            BookMark[24] = "NWFWTFWEATHER1"; //南堡油田海域天气
            BookMark[25] = "NWFWTFWEATHER2";//南堡油田海域天气
            BookMark[26] = "NWFWTFWEATHER3";//南堡油田海域天气
            BookMark[27] = "FORECASTDATE21"; //南堡油田海域预报日前
            BookMark[28] = "FORECASTDATE22"; //南堡油田海域预报日前
            BookMark[29] = "FORECASTDATE23"; //南堡油田海域预报日前
            BookMark[30] = "NOTFFIRSTHIGHWAVETIME1";//第一次高潮潮时
            BookMark[31] = "NOTFFIRSTHIGHWAVETIME2";//第一次高潮潮时
            BookMark[32] = "NOTFFIRSTHIGHWAVETIME3";//第一次高潮潮时
            BookMark[33] = "NOTFFIRSTHIGHWAVEHEIGHT1";//第一次高潮潮位
            BookMark[34] = "NOTFFIRSTHIGHWAVEHEIGHT2";//第一次高潮潮位
            BookMark[35] = "NOTFFIRSTHIGHWAVEHEIGHT3";//第一次高潮潮位
            BookMark[36] = "NOTFFIRSTLOWWAVETIME1";//第一次低潮潮时
            BookMark[37] = "NOTFFIRSTLOWWAVETIME2";//第一次低潮潮时
            BookMark[38] = "NOTFFIRSTLOWWAVETIME3";//第一次低潮潮时
            BookMark[39] = "NOTFFIRSTLOWWAVEHEIGHT1";//第一次低潮潮位
            BookMark[40] = "NOTFFIRSTLOWWAVEHEIGHT2";//第一次低潮潮位
            BookMark[41] = "NOTFFIRSTLOWWAVEHEIGHT3";//第一次低潮潮位
            BookMark[42] = "NOTFSECONDHIGHWAVETIME1";//第二次高潮潮时
            BookMark[43] = "NOTFSECONDHIGHWAVETIME2";//第二次高潮潮时
            BookMark[44] = "NOTFSECONDHIGHWAVETIME3";//第二次高潮潮时
            BookMark[45] = "NOTFSECONDHIGHWAVEHEIGHT1";//第二次高潮潮位
            BookMark[46] = "NOTFSECONDHIGHWAVEHEIGHT2"; //第二次高潮潮位
            BookMark[47] = "NOTFSECONDHIGHWAVEHEIGHT3";//第二次高潮潮位
            BookMark[48] = "NOTFSECONDLOWWAVETIME1";//第二次低潮潮时
            BookMark[49] = "NOTFSECONDLOWWAVETIME2";//第二次低潮潮时
            BookMark[50] = "NOTFSECONDLOWWAVETIME3";//第二次低潮潮时
            BookMark[51] = "NOTFSECONDLOWWAVEHEIGHT1";//第二次低潮潮位
            BookMark[52] = "NOTFSECONDLOWWAVEHEIGHT2"; //第二次低潮潮位
            BookMark[53] = "NOTFSECONDLOWWAVEHEIGHT3";//第二次低潮潮位
            BookMark[54] = "PUBLISHTIME";

            BookMark[55] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[56] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[57] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话


            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送

            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = FORECASTDATE1;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = FORECASTDATE2;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = FORECASTDATE3;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = NWFWTFWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = NWFWTFWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = NWFWTFWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = NWFWTFWAVEDIR1;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = NWFWTFWAVEDIR2;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = NWFWTFWAVEDIR3;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = NWFWTFFLOWDIR1;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = NWFWTFFLOWDIR2;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = NWFWTFFLOWDIR3;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = NWFWTFFLOWLEVEL1;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = NWFWTFFLOWLEVEL2;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = NWFWTFFLOWLEVEL3;

            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = NWFWTFWATERTEMPERATURE1;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = NWFWTFWATERTEMPERATURE2;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = NWFWTFWATERTEMPERATURE3;
            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = NWFWTFWEATHER1;
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = NWFWTFWEATHER2;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = NWFWTFWEATHER3;
            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = FORECASTDATE21;
            doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = FORECASTDATE22;
            doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = FORECASTDATE23;
            doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = NOTFFIRSTHIGHWAVETIME1;
            doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = NOTFFIRSTHIGHWAVETIME2;
            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = NOTFFIRSTHIGHWAVETIME3;
            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = NOTFFIRSTHIGHWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = NOTFFIRSTHIGHWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = NOTFFIRSTHIGHWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = NOTFFIRSTLOWWAVETIME1;
            doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = NOTFFIRSTLOWWAVETIME2;
            doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = NOTFFIRSTLOWWAVETIME3;
            doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = NOTFFIRSTLOWWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = NOTFFIRSTLOWWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = NOTFFIRSTLOWWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = NOTFSECONDHIGHWAVETIME1;
            doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = NOTFSECONDHIGHWAVETIME2;
            doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = NOTFSECONDHIGHWAVETIME3;
            doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = NOTFSECONDHIGHWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = NOTFSECONDHIGHWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = NOTFSECONDHIGHWAVEHEIGHT3;

            doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = NOTFSECONDLOWWAVETIME1;
            doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = NOTFSECONDLOWWAVETIME2;
            doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = NOTFSECONDLOWWAVETIME3;
            doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = NOTFSECONDLOWWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = NOTFSECONDLOWWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = NOTFSECONDLOWWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[54]).Range.Text = PUBLISHTIME;

            doc.Bookmarks.get_Item(ref BookMark[55]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[56]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[57]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;
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