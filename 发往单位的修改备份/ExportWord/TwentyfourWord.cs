using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

/// <summary>
/// TwentyfourWord 的摘要说明
/// </summary>
public class TwentyfourWord
{
    public TwentyfourWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    string PUBLISHTIME = "";

    string EFWWBHLOWESTWAVE = "";
    string EFWWBHWAVETYPE = "";
    string EFWWBHNORTHLOWESTWAVE = "";
    string EFWWBHNORTHWAVETYPE = "";
    string EFWWDKSEAAREAWAVEHEIGHT = "";
    string EFWWDKSEAAREAWATERTEMPE = "";
    string EFWWHHKSEAAREAWAVEHEIGHT = "";
    string EFWWHHKSEAAREAWATERTEMP = "";
    string EFWWGLGSEAAREAWAVEHEIGHT = "";
    string EFWWGLGSEAAREAWATERTEMP = "";
    string EFWWDYGWAVEHEIGHT = "";
    string EFWWDYGWATERTEMPERATURE = "";
    string EFWWXHWAVEHEIGHT = "";
    string EFWWXHWATERTEMPERATURE = "";
    string EFWWCKWAVEHEIGHT = "";
    string EFWWCKWATERTEMPERATURE = "";


    string FORECASTDATE1 = "";
    string FORECASTDATE2 = "";
    string FORECASTDATE3 = "";
    string FORECASTDATE4 = "";
    string FORECASTDATE5 = "";
    string FORECASTDATE6 = "";
    string TLFIRSTWAVEOFTIME1 = "";
    string TLFIRSTWAVEOFTIME2 = "";
    string TLFIRSTWAVEOFTIME3 = "";
    string TLFIRSTWAVEOFTIME4 = "";
    string TLFIRSTWAVEOFTIME5 = "";
    string TLFIRSTWAVEOFTIME6 = "";
    string TLFIRSTWAVETIDELEVEL1 = "";
    string TLFIRSTWAVETIDELEVEL2 = "";
    string TLFIRSTWAVETIDELEVEL3 = "";
    string TLFIRSTWAVETIDELEVEL4 = "";
    string TLFIRSTWAVETIDELEVEL5 = "";
    string TLFIRSTWAVETIDELEVEL6 = "";
    string TLFIRSTTIMELOWTIDE1 = "";
    string TLFIRSTTIMELOWTIDE2 = "";
    string TLFIRSTTIMELOWTIDE3 = "";
    string TLFIRSTTIMELOWTIDE4 = "";
    string TLFIRSTTIMELOWTIDE5 = "";
    string TLFIRSTTIMELOWTIDE6 = "";
    string TLLOWTIDELEVELFORTHEFIRSTTIME1 = "";
    string TLLOWTIDELEVELFORTHEFIRSTTIME2 = "";
    string TLLOWTIDELEVELFORTHEFIRSTTIME3 = "";
    string TLLOWTIDELEVELFORTHEFIRSTTIME4 = "";
    string TLLOWTIDELEVELFORTHEFIRSTTIME5 = "";
    string TLLOWTIDELEVELFORTHEFIRSTTIME6 = "";
    string TLSECONDWAVEOFTIME1 = "";
    string TLSECONDWAVEOFTIME2 = "";
    string TLSECONDWAVEOFTIME3 = "";
    string TLSECONDWAVEOFTIME4 = "";
    string TLSECONDWAVEOFTIME5 = "";
    string TLSECONDWAVEOFTIME6 = "";
    string TLSECONDWAVETIDELEVEL1 = "";
    string TLSECONDWAVETIDELEVEL2 = "";
    string TLSECONDWAVETIDELEVEL3 = "";
    string TLSECONDWAVETIDELEVEL4 = "";
    string TLSECONDWAVETIDELEVEL5 = "";
    string TLSECONDWAVETIDELEVEL6 = "";
    string TLSECONDTIMELOWTIDE1 = "";
    string TLSECONDTIMELOWTIDE2 = "";
    string TLSECONDTIMELOWTIDE3 = "";
    string TLSECONDTIMELOWTIDE4 = "";
    string TLSECONDTIMELOWTIDE5 = "";
    string TLSECONDTIMELOWTIDE6 = "";
    string TLLOWTIDELEVELFORTHESECONDTIME1 = "";
    string TLLOWTIDELEVELFORTHESECONDTIME2 = "";
    string TLLOWTIDELEVELFORTHESECONDTIME3 = "";
    string TLLOWTIDELEVELFORTHESECONDTIME4 = "";
    string TLLOWTIDELEVELFORTHESECONDTIME5 = "";
    string TLLOWTIDELEVELFORTHESECONDTIME6 = "";
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
        try { 
        //数据库查询
      
        TBLFOOTER tblfooter_Model = new TBLFOOTER();
        tblfooter_Model.PUBLISHDATE = dt;
            string PUBLISHDATE = "";
        //填报信息表提取数据
        System.Data.DataTable tblfooter = (System.Data.DataTable)new sql_TBLFOOTER().get_TBLFOOTER_AllData(tblfooter_Model);
            string FRELEASEUNIT = "";
            //string FTELEPHONE = "";
            //string FFAX = "";
            for (int i = 0; i < tblfooter.Rows.Count; i++)
            {
                FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                //FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                //FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                string FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                string FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();
                string FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();

                string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                string FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话
                string FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话

                PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                //PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
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

            string Date1 = dt.Day.ToString();
            string Date2 = dt.AddDays(1).Day.ToString();

            
            //object FTELEPHONES = "FTELEPHONE";
            //doc.Bookmarks.get_Item(ref FTELEPHONES).Range.Text = FTELEPHONE;
            //object FFAXS = "FFAX";
            //doc.Bookmarks.get_Item(ref FFAXS).Range.Text = FFAX;
            //object FRELEASEUNITS = "FRELEASEUNIT";
            //doc.Bookmarks.get_Item(ref FRELEASEUNITS).Range.Text = FRELEASEUNIT;
            //预计未来24小时海浪、水温预报
            TBLEXPECTEDFUTURE24HWAVEWATER tblexpectedfuture24hwavewater_Model = new TBLEXPECTEDFUTURE24HWAVEWATER();
        tblexpectedfuture24hwavewater_Model.PUBLISHDATE = dt;
        System.Data.DataTable tblexpectedfuture24hwavewater = (System.Data.DataTable)new sql_TBLEXPECTEDFUTURE24HWAVEWATER().get_TBLEXPECTEDFUTURE24HWAVEWATER_AllData(tblexpectedfuture24hwavewater_Model);
        if (tblexpectedfuture24hwavewater.Rows.Count == 0) { }
        else
        {
            for (int i = 0; i < tblexpectedfuture24hwavewater.Rows.Count; i++)
            {

                EFWWBHLOWESTWAVE = tblexpectedfuture24hwavewater.Rows[i]["EFWWBHLOWESTWAVE"].ToString() + "-" + tblexpectedfuture24hwavewater.Rows[i]["EFWWBHHIGHESTWAVE"].ToString();
                EFWWBHWAVETYPE = tblexpectedfuture24hwavewater.Rows[i]["EFWWBHWAVETYPE"].ToString();
                EFWWBHNORTHLOWESTWAVE = tblexpectedfuture24hwavewater.Rows[i]["EFWWBHNORTHLOWESTWAVE"].ToString() + "-" + tblexpectedfuture24hwavewater.Rows[i]["EFWWBHNORTHHIGHESTWAVE"].ToString();
                EFWWBHNORTHWAVETYPE = tblexpectedfuture24hwavewater.Rows[i]["EFWWBHNORTHWAVETYPE"].ToString();
                EFWWDKSEAAREAWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["EFWWDKSEAAREAWAVEHEIGHT"].ToString();
                EFWWDKSEAAREAWATERTEMPE = tblexpectedfuture24hwavewater.Rows[i]["EFWWDKSEAAREAWATERTEMPE"].ToString();
                EFWWHHKSEAAREAWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["EFWWHHKSEAAREAWAVEHEIGHT"].ToString();
                EFWWHHKSEAAREAWATERTEMP = tblexpectedfuture24hwavewater.Rows[i]["EFWWHHKSEAAREAWATERTEMP"].ToString();
                EFWWGLGSEAAREAWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["EFWWGLGSEAAREAWAVEHEIGHT"].ToString();
                EFWWGLGSEAAREAWATERTEMP = tblexpectedfuture24hwavewater.Rows[i]["EFWWGLGSEAAREAWATERTEMP"].ToString();
                EFWWDYGWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["EFWWDYGWAVEHEIGHT"].ToString();
                EFWWDYGWATERTEMPERATURE = tblexpectedfuture24hwavewater.Rows[i]["EFWWDYGWATERTEMPERATURE"].ToString();

                EFWWXHWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["EFWWXHWAVEHEIGHT"].ToString();
                EFWWXHWATERTEMPERATURE = tblexpectedfuture24hwavewater.Rows[i]["EFWWXHWATERTEMPERATURE"].ToString();
                EFWWCKWAVEHEIGHT = tblexpectedfuture24hwavewater.Rows[i]["EFWWCKWAVEHEIGHT"].ToString();
                EFWWCKWATERTEMPERATURE = tblexpectedfuture24hwavewater.Rows[i]["EFWWCKWATERTEMPERATURE"].ToString();       
            }


        }

        //24小时潮位预报
        TBL24HTIDELEVEL tbl24htidelevel_Model = new TBL24HTIDELEVEL();
        tbl24htidelevel_Model.PUBLISHDATE = dt;
        System.Data.DataTable tbl24htidelevel = (System.Data.DataTable)new sql_TBL24HTIDELEVEL().get_TBL24HTIDELEVEL_AllData(tbl24htidelevel_Model);
        if (tbl24htidelevel.Rows.Count == 0) { }
        else
        {
            for (int i = 0; i < tbl24htidelevel.Rows.Count; i++)
            {
                if (tbl24htidelevel.Rows[i]["TLFORECASTSTANCE"].ToString() == "新户")
                {
                    var FORECASTDATE = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                    if (FORECASTDATE != null || FORECASTDATE != "")
                    {
                        string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                        FORECASTDATE1 = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                    }
                    //FORECASTDATE1 = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                    TLFIRSTWAVEOFTIME1 = tbl24htidelevel.Rows[i]["TLFIRSTWAVEOFTIME"].ToString();
                    TLFIRSTWAVETIDELEVEL1 = tbl24htidelevel.Rows[i]["TLFIRSTWAVETIDELEVEL"].ToString();
                    TLFIRSTTIMELOWTIDE1 = tbl24htidelevel.Rows[i]["TLFIRSTTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHEFIRSTTIME1 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    TLSECONDWAVEOFTIME1 = tbl24htidelevel.Rows[i]["TLSECONDWAVEOFTIME"].ToString();
                    TLSECONDWAVETIDELEVEL1 = tbl24htidelevel.Rows[i]["TLSECONDWAVETIDELEVEL"].ToString();
                    TLSECONDTIMELOWTIDE1 = tbl24htidelevel.Rows[i]["TLSECONDTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHESECONDTIME1 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHESECONDTIME"].ToString();
          
                }
                else if (tbl24htidelevel.Rows[i]["TLFORECASTSTANCE"].ToString() == "飞雁滩")
                {
                        var FORECASTDATE = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                        if (FORECASTDATE != null || FORECASTDATE != "")
                        {
                            string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                            FORECASTDATE2 = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                        }

                        //FORECASTDATE2 = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                    TLFIRSTWAVEOFTIME2 = tbl24htidelevel.Rows[i]["TLFIRSTWAVEOFTIME"].ToString();
                    TLFIRSTWAVETIDELEVEL2 = tbl24htidelevel.Rows[i]["TLFIRSTWAVETIDELEVEL"].ToString();
                    TLFIRSTTIMELOWTIDE2 = tbl24htidelevel.Rows[i]["TLFIRSTTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHEFIRSTTIME2 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    TLSECONDWAVEOFTIME2 = tbl24htidelevel.Rows[i]["TLSECONDWAVEOFTIME"].ToString();
                    TLSECONDWAVETIDELEVEL2 = tbl24htidelevel.Rows[i]["TLSECONDWAVETIDELEVEL"].ToString();
                    TLSECONDTIMELOWTIDE2 = tbl24htidelevel.Rows[i]["TLSECONDTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHESECONDTIME2 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHESECONDTIME"].ToString();
                }
                else if (tbl24htidelevel.Rows[i]["TLFORECASTSTANCE"].ToString() == "桩西")
                {
                        var FORECASTDATE = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                        if (FORECASTDATE != null || FORECASTDATE != "")
                        {
                            string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                            FORECASTDATE3 = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                        }

                        //FORECASTDATE3 = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                    TLFIRSTWAVEOFTIME3 = tbl24htidelevel.Rows[i]["TLFIRSTWAVEOFTIME"].ToString();
                    TLFIRSTWAVETIDELEVEL3 = tbl24htidelevel.Rows[i]["TLFIRSTWAVETIDELEVEL"].ToString();
                    TLFIRSTTIMELOWTIDE3 = tbl24htidelevel.Rows[i]["TLFIRSTTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHEFIRSTTIME3 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    TLSECONDWAVEOFTIME3 = tbl24htidelevel.Rows[i]["TLSECONDWAVEOFTIME"].ToString();
                    TLSECONDWAVETIDELEVEL3 = tbl24htidelevel.Rows[i]["TLSECONDWAVETIDELEVEL"].ToString();
                    TLSECONDTIMELOWTIDE3 = tbl24htidelevel.Rows[i]["TLSECONDTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHESECONDTIME3 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHESECONDTIME"].ToString();
                }
                else if (tbl24htidelevel.Rows[i]["TLFORECASTSTANCE"].ToString() == "东营港")
                {
                        var FORECASTDATE = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                        if (FORECASTDATE != null || FORECASTDATE != "")
                        {
                            string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                            FORECASTDATE4 = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                        }

                        //FORECASTDATE4 = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                    TLFIRSTWAVEOFTIME4 = tbl24htidelevel.Rows[i]["TLFIRSTWAVEOFTIME"].ToString();
                    TLFIRSTWAVETIDELEVEL4 = tbl24htidelevel.Rows[i]["TLFIRSTWAVETIDELEVEL"].ToString();
                    TLFIRSTTIMELOWTIDE4 = tbl24htidelevel.Rows[i]["TLFIRSTTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHEFIRSTTIME4 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    TLSECONDWAVEOFTIME4 = tbl24htidelevel.Rows[i]["TLSECONDWAVEOFTIME"].ToString();
                    TLSECONDWAVETIDELEVEL4 = tbl24htidelevel.Rows[i]["TLSECONDWAVETIDELEVEL"].ToString();
                    TLSECONDTIMELOWTIDE4 = tbl24htidelevel.Rows[i]["TLSECONDTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHESECONDTIME4 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHESECONDTIME"].ToString();
                }
                else if (tbl24htidelevel.Rows[i]["TLFORECASTSTANCE"].ToString() == "孤东")
                {
                        var FORECASTDATE = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                        if (FORECASTDATE != null || FORECASTDATE != "")
                        {
                            string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                            FORECASTDATE5 = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                        }

                        //FORECASTDATE5 = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                    TLFIRSTWAVEOFTIME5 = tbl24htidelevel.Rows[i]["TLFIRSTWAVEOFTIME"].ToString();
                    TLFIRSTWAVETIDELEVEL5 = tbl24htidelevel.Rows[i]["TLFIRSTWAVETIDELEVEL"].ToString();
                    TLFIRSTTIMELOWTIDE5 = tbl24htidelevel.Rows[i]["TLFIRSTTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHEFIRSTTIME5 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    TLSECONDWAVEOFTIME5 = tbl24htidelevel.Rows[i]["TLSECONDWAVEOFTIME"].ToString();
                    TLSECONDWAVETIDELEVEL5 = tbl24htidelevel.Rows[i]["TLSECONDWAVETIDELEVEL"].ToString();
                    TLSECONDTIMELOWTIDE5 = tbl24htidelevel.Rows[i]["TLSECONDTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHESECONDTIME5 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHESECONDTIME"].ToString();
                }
                else if (tbl24htidelevel.Rows[i]["TLFORECASTSTANCE"].ToString() == "小岛河")
                {
                        var FORECASTDATE = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                        if (FORECASTDATE != null || FORECASTDATE != "")
                        {
                            string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                            FORECASTDATE6 = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                        }

                        //FORECASTDATE6 = tbl24htidelevel.Rows[i]["FORECASTDATE"].ToString();
                    TLFIRSTWAVEOFTIME6 = tbl24htidelevel.Rows[i]["TLFIRSTWAVEOFTIME"].ToString();
                    TLFIRSTWAVETIDELEVEL6 = tbl24htidelevel.Rows[i]["TLFIRSTWAVETIDELEVEL"].ToString();
                    TLFIRSTTIMELOWTIDE6 = tbl24htidelevel.Rows[i]["TLFIRSTTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHEFIRSTTIME6 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    TLSECONDWAVEOFTIME6 = tbl24htidelevel.Rows[i]["TLSECONDWAVEOFTIME"].ToString();
                    TLSECONDWAVETIDELEVEL6 = tbl24htidelevel.Rows[i]["TLSECONDWAVETIDELEVEL"].ToString();
                    TLSECONDTIMELOWTIDE6 = tbl24htidelevel.Rows[i]["TLSECONDTIMELOWTIDE"].ToString();
                    TLLOWTIDELEVELFORTHESECONDTIME6 = tbl24htidelevel.Rows[i]["TLLOWTIDELEVELFORTHESECONDTIME"].ToString();
                }
            }


        }
        //为了方便管理声明书签数组
        object[] BookMark = new object[81];//新增3个预报员电话 78改为81
        //赋值书签名
        BookMark[0] = "FRELEASEUNIT";//发布单位
        BookMark[1] = "FZHIBANTEL";//预报值班
        BookMark[2] = "FSENDTEL";//预报值班

        //BookMark[1] = "FTELEPHONE";//电话
        //BookMark[2] = "FFAX";//传真
        BookMark[3] = "FWAVEFORECASTER";//海浪预报员
        BookMark[4] = "FTIDALFORECASTER";
        BookMark[5] = "FWATERTEMPERATUREFORECASTER";
        BookMark[6] = "EFWWBHLOWESTWAVE";
        BookMark[7] = "EFWWBHWAVETYPE";
        BookMark[8] = "EFWWBHNORTHLOWESTWAVE";
        BookMark[9] = "EFWWBHNORTHWAVETYPE";
        BookMark[10] = "EFWWDKSEAAREAWAVEHEIGHT";
        BookMark[11] = "EFWWDKSEAAREAWATERTEMPE";
        BookMark[12] = "EFWWHHKSEAAREAWAVEHEIGHT";
        BookMark[13] = "EFWWHHKSEAAREAWATERTEMP";
        BookMark[14] = "EFWWGLGSEAAREAWAVEHEIGHT";
        BookMark[15] = "EFWWGLGSEAAREAWATERTEMP";
        BookMark[16] = "EFWWDYGWAVEHEIGHT";
        BookMark[17] = "EFWWDYGWATERTEMPERATURE";
        BookMark[18] = "EFWWXHWAVEHEIGHT";
        BookMark[19] = "EFWWXHWATERTEMPERATURE";
        BookMark[20] = "EFWWCKWAVEHEIGHT";
        BookMark[21] = "EFWWCKWATERTEMPERATURE";
        BookMark[22] = "FORECASTDATE1";
        BookMark[23] = "FORECASTDATE2";
        BookMark[24] = "FORECASTDATE3";
        BookMark[25] = "FORECASTDATE4";
        BookMark[26] = "FORECASTDATE5";
        BookMark[27] = "FORECASTDATE6";
        BookMark[28] = "TLFIRSTWAVEOFTIME1";
        BookMark[29] = "TLFIRSTWAVEOFTIME2";
        BookMark[30] = "TLFIRSTWAVEOFTIME3";
        BookMark[31] = "TLFIRSTWAVEOFTIME4";
        BookMark[32] = "TLFIRSTWAVEOFTIME5";
        BookMark[33] = "TLFIRSTWAVEOFTIME6";
        BookMark[34] = "TLFIRSTWAVETIDELEVEL1";
        BookMark[35] = "TLFIRSTWAVETIDELEVEL2";
        BookMark[36] = "TLFIRSTWAVETIDELEVEL3";
        BookMark[37] = "TLFIRSTWAVETIDELEVEL4";
        BookMark[38] = "TLFIRSTWAVETIDELEVEL5";
        BookMark[39] = "TLFIRSTWAVETIDELEVEL6";
        BookMark[40] = "TLFIRSTTIMELOWTIDE1";
        BookMark[41] = "TLFIRSTTIMELOWTIDE2";
        BookMark[42] = "TLFIRSTTIMELOWTIDE3";
        BookMark[43] = "TLFIRSTTIMELOWTIDE4";
        BookMark[44] = "TLFIRSTTIMELOWTIDE5";
        BookMark[45] = "TLFIRSTTIMELOWTIDE6";
        BookMark[46] = "TLLOWTIDELEVELFORTHEFIRSTTIME1";
        BookMark[47] = "TLLOWTIDELEVELFORTHEFIRSTTIME2";
        BookMark[48] = "TLLOWTIDELEVELFORTHEFIRSTTIME3";
        BookMark[49] = "TLLOWTIDELEVELFORTHEFIRSTTIME4";
        BookMark[50] = "TLLOWTIDELEVELFORTHEFIRSTTIME5";
        BookMark[51] = "TLLOWTIDELEVELFORTHEFIRSTTIME6";
        BookMark[52] = "TLSECONDWAVEOFTIME1";
        BookMark[53] = "TLSECONDWAVEOFTIME2";
        BookMark[54] = "TLSECONDWAVEOFTIME3";
        BookMark[55] = "TLSECONDWAVEOFTIME4";
        BookMark[56] = "TLSECONDWAVEOFTIME5";
        BookMark[57] = "TLSECONDWAVEOFTIME6";
        BookMark[58] = "TLSECONDWAVETIDELEVEL1";
        BookMark[59] = "TLSECONDWAVETIDELEVEL2";
        BookMark[60] = "TLSECONDWAVETIDELEVEL3";
        BookMark[61] = "TLSECONDWAVETIDELEVEL4";
        BookMark[62] = "TLSECONDWAVETIDELEVEL5";
        BookMark[63] = "TLSECONDWAVETIDELEVEL6";
        BookMark[64] = "TLSECONDTIMELOWTIDE1";
        BookMark[65] = "TLSECONDTIMELOWTIDE2";
        BookMark[66] = "TLSECONDTIMELOWTIDE3";
        BookMark[67] = "TLSECONDTIMELOWTIDE4";
        BookMark[68] = "TLSECONDTIMELOWTIDE5";
        BookMark[69] = "TLSECONDTIMELOWTIDE6";
        BookMark[70] = "TLLOWTIDELEVELFORTHESECONDTIME1";
        BookMark[71] = "TLLOWTIDELEVELFORTHESECONDTIME2";
        BookMark[72] = "TLLOWTIDELEVELFORTHESECONDTIME3";
        BookMark[73] = "TLLOWTIDELEVELFORTHESECONDTIME4";
        BookMark[74] = "TLLOWTIDELEVELFORTHESECONDTIME5";
        BookMark[75] = "TLLOWTIDELEVELFORTHESECONDTIME6";

            BookMark[76] = "DATE1";
            BookMark[77] = "DATE2";

            BookMark[78] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[79] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[80] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话

            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送

            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
        doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
        doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
        doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = EFWWBHLOWESTWAVE;
        doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = EFWWBHWAVETYPE;
        doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = EFWWBHNORTHLOWESTWAVE;
        doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = EFWWBHNORTHWAVETYPE;
        doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = EFWWDKSEAAREAWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = EFWWDKSEAAREAWATERTEMPE;
        doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = EFWWHHKSEAAREAWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = EFWWHHKSEAAREAWATERTEMP;
        doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = EFWWGLGSEAAREAWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = EFWWGLGSEAAREAWATERTEMP;
        doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = EFWWDYGWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = EFWWDYGWATERTEMPERATURE;
        doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = EFWWXHWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = EFWWXHWATERTEMPERATURE;
        doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = EFWWCKWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = EFWWCKWATERTEMPERATURE;
        doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = FORECASTDATE1;
        doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = FORECASTDATE2;
        doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = FORECASTDATE3;
        doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = FORECASTDATE4;
        doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = FORECASTDATE5;
        doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = FORECASTDATE6;
        doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = TLFIRSTWAVEOFTIME1;
        doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = TLFIRSTWAVEOFTIME2;
        doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = TLFIRSTWAVEOFTIME3;
        doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = TLFIRSTWAVEOFTIME4;
        doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = TLFIRSTWAVEOFTIME5;
        doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = TLFIRSTWAVEOFTIME6;
        doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = TLFIRSTWAVETIDELEVEL1;
        doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = TLFIRSTWAVETIDELEVEL2;
        doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = TLFIRSTWAVETIDELEVEL3;
        doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = TLFIRSTWAVETIDELEVEL4;
        doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = TLFIRSTWAVETIDELEVEL5;
        doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = TLFIRSTWAVETIDELEVEL6;
        doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = TLFIRSTTIMELOWTIDE1;
        doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = TLFIRSTTIMELOWTIDE2;
        doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = TLFIRSTTIMELOWTIDE3;
        doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = TLFIRSTTIMELOWTIDE4;
        doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = TLFIRSTTIMELOWTIDE5;
        doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = TLFIRSTTIMELOWTIDE6;
        doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = TLLOWTIDELEVELFORTHEFIRSTTIME1;
        doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = TLLOWTIDELEVELFORTHEFIRSTTIME2;
        doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = TLLOWTIDELEVELFORTHEFIRSTTIME3;
        doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = TLLOWTIDELEVELFORTHEFIRSTTIME4;
        doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = TLLOWTIDELEVELFORTHEFIRSTTIME5;
        doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = TLLOWTIDELEVELFORTHEFIRSTTIME6;
        doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = TLSECONDWAVEOFTIME1;
        doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = TLSECONDWAVEOFTIME2;
        doc.Bookmarks.get_Item(ref BookMark[54]).Range.Text = TLSECONDWAVEOFTIME3;
        doc.Bookmarks.get_Item(ref BookMark[55]).Range.Text = TLSECONDWAVEOFTIME4;
        doc.Bookmarks.get_Item(ref BookMark[56]).Range.Text = TLSECONDWAVEOFTIME5;
        doc.Bookmarks.get_Item(ref BookMark[57]).Range.Text = TLSECONDWAVEOFTIME6;
        doc.Bookmarks.get_Item(ref BookMark[58]).Range.Text = TLSECONDWAVETIDELEVEL1;
        doc.Bookmarks.get_Item(ref BookMark[59]).Range.Text = TLSECONDWAVETIDELEVEL2;
        doc.Bookmarks.get_Item(ref BookMark[60]).Range.Text = TLSECONDWAVETIDELEVEL3;
        doc.Bookmarks.get_Item(ref BookMark[61]).Range.Text = TLSECONDWAVETIDELEVEL4;
        doc.Bookmarks.get_Item(ref BookMark[62]).Range.Text = TLSECONDWAVETIDELEVEL5;
        doc.Bookmarks.get_Item(ref BookMark[63]).Range.Text = TLSECONDWAVETIDELEVEL6;
        doc.Bookmarks.get_Item(ref BookMark[64]).Range.Text = TLSECONDTIMELOWTIDE1;
        doc.Bookmarks.get_Item(ref BookMark[65]).Range.Text = TLSECONDTIMELOWTIDE2;
        doc.Bookmarks.get_Item(ref BookMark[66]).Range.Text = TLSECONDTIMELOWTIDE3;
        doc.Bookmarks.get_Item(ref BookMark[67]).Range.Text = TLSECONDTIMELOWTIDE4;
        doc.Bookmarks.get_Item(ref BookMark[68]).Range.Text = TLSECONDTIMELOWTIDE5;
        doc.Bookmarks.get_Item(ref BookMark[69]).Range.Text = TLSECONDTIMELOWTIDE6;
        doc.Bookmarks.get_Item(ref BookMark[70]).Range.Text = TLLOWTIDELEVELFORTHESECONDTIME1;
        doc.Bookmarks.get_Item(ref BookMark[71]).Range.Text = TLLOWTIDELEVELFORTHESECONDTIME2;
        doc.Bookmarks.get_Item(ref BookMark[72]).Range.Text = TLLOWTIDELEVELFORTHESECONDTIME3;
        doc.Bookmarks.get_Item(ref BookMark[73]).Range.Text = TLLOWTIDELEVELFORTHESECONDTIME4;
        doc.Bookmarks.get_Item(ref BookMark[74]).Range.Text = TLLOWTIDELEVELFORTHESECONDTIME5;
        doc.Bookmarks.get_Item(ref BookMark[75]).Range.Text = TLLOWTIDELEVELFORTHESECONDTIME6;

            doc.Bookmarks.get_Item(ref BookMark[76]).Range.Text = Date1;
            doc.Bookmarks.get_Item(ref BookMark[77]).Range.Text = Date2;

            doc.Bookmarks.get_Item(ref BookMark[78]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[79]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[80]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;


            //24小时海洋水纹预报综述
            sql_TBLZS hyzs_24HOURS = new sql_TBLZS();
            DataTable dt_zs24HOURS = (DataTable)hyzs_24HOURS.get_TBLSWQX_ZS_3DayS_OR_24HourS(dt);
            if (dt_zs24HOURS != null && dt_zs24HOURS.Rows.Count > 0)
            {
                object mark_24HOURS = "HYQXYBZS24HOURS";
                doc.Bookmarks.get_Item(ref mark_24HOURS).Range.Text = dt_zs24HOURS.Rows[0]["METEOROLOGICALREVIEW24HOUR"].ToString() + dt_zs24HOURS.Rows[0]["METEOROLOGICALREVIEW24HOURCX"].ToString();
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