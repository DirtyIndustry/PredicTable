using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;//.NET选项卡中的"Microsoft.Office.Interop.Word"

/// <summary>
/// FourWord 的摘要说明
/// </summary>
public class FourWord
{
    string PUBLISHTIME = "";

    string FORECASTDATE1 = "";
    string FORECASTDATE2 = "";
    string FORECASTDATE3 = "";
    string FORECASTDATE4 = "";
    string FORECASTDATE5 = "";
    string FORECASTDATE6 = "";
    string YRBHWWFWAVEHEIGHT1 = "";
    string YRBHWWFWAVEHEIGHT2 = "";
    string YRBHWWFWAVEHEIGHT3 = "";
    string YRBHWWFWAVEHEIGHT4 = "";
    string YRBHWWFWAVEHEIGHT5 = "";
    string YRBHWWFWAVEHEIGHT6 = "";
    string YRBHWWFWAVEDIR1 = "";
    string YRBHWWFWAVEDIR2 = "";
    string YRBHWWFWAVEDIR3 = "";
    string YRBHWWFWAVEDIR4 = "";
    string YRBHWWFWAVEDIR5 = "";
    string YRBHWWFWAVEDIR6 = "";
    string YRBHWWFFLOWDIR1 = "";
    string YRBHWWFFLOWDIR2 = "";
    string YRBHWWFFLOWDIR3 = "";
    string YRBHWWFFLOWDIR4 = "";
    string YRBHWWFFLOWDIR5 = "";
    string YRBHWWFFLOWDIR6 = "";
    string YRBHWWFFLOWLEVEL1 = "";
    string YRBHWWFFLOWLEVEL2 = "";
    string YRBHWWFFLOWLEVEL3 = "";
    string YRBHWWFFLOWLEVEL4 = "";
    string YRBHWWFFLOWLEVEL5 = "";
    string YRBHWWFFLOWLEVEL6 = "";
    string YRBHWWFWATERTEMPERATURE1 = "";
    string YRBHWWFWATERTEMPERATURE2 = "";
    string YRBHWWFWATERTEMPERATURE3 = "";
    string YRBHWWFWATERTEMPERATURE4 = "";
    string YRBHWWFWATERTEMPERATURE5 = "";
    string YRBHWWFWATERTEMPERATURE6 = "";

    string FORECASTDATE21 = "";
    string FORECASTDATE22 = "";
    string FORECASTDATE23 = "";
    string FORECASTDATE24 = "";
    string FORECASTDATE25 = "";
    string FORECASTDATE26 = "";
    string HTLFIRSTWAVEOFTIME1 = "";
    string HTLFIRSTWAVEOFTIME2 = "";
    string HTLFIRSTWAVEOFTIME3 = "";
    string HTLFIRSTWAVEOFTIME4 = "";
    string HTLFIRSTWAVEOFTIME5 = "";
    string HTLFIRSTWAVEOFTIME6 = "";
    string HTLFIRSTWAVETIDELEVEL1 = "";
    string HTLFIRSTWAVETIDELEVEL2 = "";
    string HTLFIRSTWAVETIDELEVEL3 = "";
    string HTLFIRSTWAVETIDELEVEL4 = "";
    string HTLFIRSTWAVETIDELEVEL5 = "";
    string HTLFIRSTWAVETIDELEVEL6 = "";
    string HTLFIRSTTIMELOWTIDE1 = "";
    string HTLFIRSTTIMELOWTIDE2 = "";
    string HTLFIRSTTIMELOWTIDE3 = "";
    string HTLFIRSTTIMELOWTIDE4 = "";
    string HTLFIRSTTIMELOWTIDE5 = "";
    string HTLFIRSTTIMELOWTIDE6 = "";
    string HTLLOWTIDELEVELFORTHEFIRSTTIME1 = "";
    string HTLLOWTIDELEVELFORTHEFIRSTTIME2 = "";
    string HTLLOWTIDELEVELFORTHEFIRSTTIME3 = "";
    string HTLLOWTIDELEVELFORTHEFIRSTTIME4 = "";
    string HTLLOWTIDELEVELFORTHEFIRSTTIME5 = "";
    string HTLLOWTIDELEVELFORTHEFIRSTTIME6 = "";
    string HTLSECONDWAVEOFTIME1 = "";
    string HTLSECONDWAVEOFTIME2 = "";
    string HTLSECONDWAVEOFTIME3 = "";
    string HTLSECONDWAVEOFTIME4 = "";
    string HTLSECONDWAVEOFTIME5 = "";
    string HTLSECONDWAVEOFTIME6 = "";
    string HTLSECONDWAVETIDELEVEL1 = "";
    string HTLSECONDWAVETIDELEVEL2 = "";
    string HTLSECONDWAVETIDELEVEL3 = "";
    string HTLSECONDWAVETIDELEVEL4 = "";
    string HTLSECONDWAVETIDELEVEL5 = "";
    string HTLSECONDWAVETIDELEVEL6 = "";
    string HTLSECONDTIMELOWTIDE1 = "";
    string HTLSECONDTIMELOWTIDE2 = "";
    string HTLSECONDTIMELOWTIDE3 = "";
    string HTLSECONDTIMELOWTIDE4 = "";
    string HTLSECONDTIMELOWTIDE5 = "";
    string HTLSECONDTIMELOWTIDE6 = "";
    string HTLLOWTIDELEVELFORTHESECONDTIM1 = "";
    string HTLLOWTIDELEVELFORTHESECONDTIM2 = "";
    string HTLLOWTIDELEVELFORTHESECONDTIM3 = "";
    string HTLLOWTIDELEVELFORTHESECONDTIM4 = "";
    string HTLLOWTIDELEVELFORTHESECONDTIM5 = "";
    string HTLLOWTIDELEVELFORTHESECONDTIM6 = "";

    string METEOROLOGICALREVIEW = "";
    string METEOROLOGICALREVIEW24HOUR = "";

    System.Data.DataTable dt1 = new DataTable("dt1");

    System.Data.DataTable dt2 = new DataTable("dt2");
    System.Data.DataTable dt3 = new DataTable("dt3");
    System.Data.DataTable dt4 = new DataTable("dt4");
    public FourWord()
    {
    }

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
            //72小时渤海海区及黄河海港风、浪预报
            TBLYRBHWINDWAVE72HFORECASTTWO tblyrbhwindwave72hforecasttwo_Model = new TBLYRBHWINDWAVE72HFORECASTTWO();
            tblyrbhwindwave72hforecasttwo_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblyrbhwindwave72hforecasttwo = (System.Data.DataTable)new sql_TBLYRBHWINDWAVE72HFORECASTTWO().get_TBLYRBHWINDWAVE72HFORECASTTWO_AllData(tblyrbhwindwave72hforecasttwo_Model);
            if (tblyrbhwindwave72hforecasttwo.Rows.Count == 0) {

            }
            else
            {
                dt1.Columns.Add("PUBLISHDATE", typeof(string));
                dt1.Columns.Add("REPORTAREA", typeof(string));
                dt1.Columns.Add("FORECASTDATE", typeof(string));
                dt1.Columns.Add("YRBHWWFWAVEHEIGHT", typeof(string));
                dt1.Columns.Add("YRBHWWFWAVEDIR", typeof(string));
                dt1.Columns.Add("YRBHWWFFLOWDIR", typeof(string));
                dt1.Columns.Add("YRBHWWFFLOWLEVEL", typeof(string));
                dt1.Columns.Add("YRBHWWFWATERTEMPERATURE", typeof(string));

                dt2.Columns.Add("PUBLISHDATE", typeof(string));
                dt2.Columns.Add("REPORTAREA", typeof(string));
                dt2.Columns.Add("FORECASTDATE", typeof(string));
                dt2.Columns.Add("YRBHWWFWAVEHEIGHT", typeof(string));
                dt2.Columns.Add("YRBHWWFWAVEDIR", typeof(string));
                dt2.Columns.Add("YRBHWWFFLOWDIR", typeof(string));
                dt2.Columns.Add("YRBHWWFFLOWLEVEL", typeof(string));
                dt2.Columns.Add("YRBHWWFWATERTEMPERATURE", typeof(string));



                for (int i = 0; i < tblyrbhwindwave72hforecasttwo.Rows.Count; i++)
                {
                    if (tblyrbhwindwave72hforecasttwo.Rows[i]["REPORTAREA"].ToString() == "渤海")
                    {
                        DataRow row = dt1.NewRow();
                        row["PUBLISHDATE"] = tblyrbhwindwave72hforecasttwo.Rows[i]["PUBLISHDATE"].ToString();
                        row["REPORTAREA"] = tblyrbhwindwave72hforecasttwo.Rows[i]["REPORTAREA"].ToString();
                        row["FORECASTDATE"] = tblyrbhwindwave72hforecasttwo.Rows[i]["FORECASTDATE"].ToString();
                        row["YRBHWWFWAVEHEIGHT"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFWAVEHEIGHT"].ToString();
                        row["YRBHWWFWAVEDIR"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFWAVEDIR"].ToString();
                        row["YRBHWWFFLOWDIR"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFFLOWDIR"].ToString();
                        row["YRBHWWFFLOWLEVEL"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFFLOWLEVEL"].ToString();
                        row["YRBHWWFWATERTEMPERATURE"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFWATERTEMPERATURE"].ToString();
                        dt1.Rows.Add(row);
                    }
                    if (tblyrbhwindwave72hforecasttwo.Rows[i]["REPORTAREA"].ToString() == "黄河海港")
                    {
                        DataRow row = dt2.NewRow();
                        row["PUBLISHDATE"] = tblyrbhwindwave72hforecasttwo.Rows[i]["PUBLISHDATE"].ToString();
                        row["REPORTAREA"] = tblyrbhwindwave72hforecasttwo.Rows[i]["REPORTAREA"].ToString();
                        row["FORECASTDATE"] = tblyrbhwindwave72hforecasttwo.Rows[i]["FORECASTDATE"].ToString();
                        row["YRBHWWFWAVEHEIGHT"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFWAVEHEIGHT"].ToString();
                        row["YRBHWWFWAVEDIR"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFWAVEDIR"].ToString();
                        row["YRBHWWFFLOWDIR"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFFLOWDIR"].ToString();
                        row["YRBHWWFFLOWLEVEL"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFFLOWLEVEL"].ToString();
                        row["YRBHWWFWATERTEMPERATURE"] = tblyrbhwindwave72hforecasttwo.Rows[i]["YRBHWWFWATERTEMPERATURE"].ToString();
                        dt2.Rows.Add(row);
                    }
                }
                //渤海
                FORECASTDATE1 = dt1.Rows[0]["FORECASTDATE"].ToString();
                if (FORECASTDATE1 != null || FORECASTDATE1 != "")
                {
                    string[] FORECASTDATE11 = FORECASTDATE1.Split(' ');
                    FORECASTDATE1 = FORECASTDATE11[0].ToString();
                }
                FORECASTDATE2 = dt1.Rows[1]["FORECASTDATE"].ToString();
                if (FORECASTDATE2 != null || FORECASTDATE2 != "")
                {
                    string[] FORECASTDATE22 = FORECASTDATE2.Split(' ');
                    FORECASTDATE2 = FORECASTDATE22[0].ToString();
                }
                FORECASTDATE3 = dt1.Rows[2]["FORECASTDATE"].ToString();
                if (FORECASTDATE3 != null || FORECASTDATE3 != "")
                {
                    string[] FORECASTDATE33 = FORECASTDATE3.Split(' ');
                    FORECASTDATE3 = FORECASTDATE33[0].ToString();
                }
                YRBHWWFWAVEHEIGHT1 = dt1.Rows[0]["YRBHWWFWAVEHEIGHT"].ToString();
                YRBHWWFWAVEHEIGHT2 = dt1.Rows[1]["YRBHWWFWAVEHEIGHT"].ToString();
                YRBHWWFWAVEHEIGHT3 = dt1.Rows[2]["YRBHWWFWAVEHEIGHT"].ToString();
                YRBHWWFWAVEDIR1 = dt1.Rows[0]["YRBHWWFWAVEDIR"].ToString();
                YRBHWWFWAVEDIR2 = dt1.Rows[1]["YRBHWWFWAVEDIR"].ToString();
                YRBHWWFWAVEDIR3 = dt1.Rows[2]["YRBHWWFWAVEDIR"].ToString();
                YRBHWWFFLOWDIR1 = dt1.Rows[0]["YRBHWWFFLOWDIR"].ToString();
                YRBHWWFFLOWDIR2 = dt1.Rows[1]["YRBHWWFFLOWDIR"].ToString();
                YRBHWWFFLOWDIR3 = dt1.Rows[2]["YRBHWWFFLOWDIR"].ToString();
                YRBHWWFFLOWLEVEL1 = dt1.Rows[0]["YRBHWWFFLOWLEVEL"].ToString();
                YRBHWWFFLOWLEVEL2 = dt1.Rows[1]["YRBHWWFFLOWLEVEL"].ToString();
                YRBHWWFFLOWLEVEL3 = dt1.Rows[2]["YRBHWWFFLOWLEVEL"].ToString();
                YRBHWWFWATERTEMPERATURE1 = dt1.Rows[0]["YRBHWWFWATERTEMPERATURE"].ToString();
                YRBHWWFWATERTEMPERATURE2 = dt1.Rows[1]["YRBHWWFWATERTEMPERATURE"].ToString();
                YRBHWWFWATERTEMPERATURE3 = dt1.Rows[2]["YRBHWWFWATERTEMPERATURE"].ToString();


                //黄河海域
                FORECASTDATE4 = dt2.Rows[0]["FORECASTDATE"].ToString();
                if (FORECASTDATE4 != null || FORECASTDATE4 != "")
                {
                    string[] FORECASTDATE11 = FORECASTDATE4.Split(' ');
                    FORECASTDATE4 = FORECASTDATE11[0].ToString();
                }
                FORECASTDATE5 = dt2.Rows[1]["FORECASTDATE"].ToString();
                if (FORECASTDATE5 != null || FORECASTDATE5 != "")
                {
                    string[] FORECASTDATE22 = FORECASTDATE5.Split(' ');
                    FORECASTDATE5 = FORECASTDATE22[0].ToString();
                }
                FORECASTDATE6 = dt2.Rows[2]["FORECASTDATE"].ToString();
                if (FORECASTDATE6 != null || FORECASTDATE6 != "")
                {
                    string[] FORECASTDATE33 = FORECASTDATE6.Split(' ');
                    FORECASTDATE6 = FORECASTDATE33[0].ToString();
                }
                YRBHWWFWAVEHEIGHT4 = dt2.Rows[0]["YRBHWWFWAVEHEIGHT"].ToString();
                YRBHWWFWAVEHEIGHT5 = dt2.Rows[1]["YRBHWWFWAVEHEIGHT"].ToString();
                YRBHWWFWAVEHEIGHT6 = dt2.Rows[2]["YRBHWWFWAVEHEIGHT"].ToString();
                YRBHWWFWAVEDIR4 = dt2.Rows[0]["YRBHWWFWAVEDIR"].ToString();
                YRBHWWFWAVEDIR5 = dt2.Rows[1]["YRBHWWFWAVEDIR"].ToString();
                YRBHWWFWAVEDIR6 = dt2.Rows[2]["YRBHWWFWAVEDIR"].ToString();
                YRBHWWFFLOWDIR4 = dt2.Rows[0]["YRBHWWFFLOWDIR"].ToString();
                YRBHWWFFLOWDIR5 = dt2.Rows[1]["YRBHWWFFLOWDIR"].ToString();
                YRBHWWFFLOWDIR6 = dt2.Rows[2]["YRBHWWFFLOWDIR"].ToString();
                YRBHWWFFLOWLEVEL4 = dt2.Rows[0]["YRBHWWFFLOWLEVEL"].ToString();
                YRBHWWFFLOWLEVEL5 = dt2.Rows[1]["YRBHWWFFLOWLEVEL"].ToString();
                YRBHWWFFLOWLEVEL6 = dt2.Rows[2]["YRBHWWFFLOWLEVEL"].ToString();
                YRBHWWFWATERTEMPERATURE4 = dt2.Rows[0]["YRBHWWFWATERTEMPERATURE"].ToString();
                YRBHWWFWATERTEMPERATURE5 = dt2.Rows[1]["YRBHWWFWATERTEMPERATURE"].ToString();
                YRBHWWFWATERTEMPERATURE6 = dt2.Rows[2]["YRBHWWFWATERTEMPERATURE"].ToString();
            }
            //3天海洋水文气象预报综述、24小时海洋水文气象预报综述
            //TBLMARINEMETEOROLOGICALREVIEW tblmarinemeteorologicalreview_Model = new TBLMARINEMETEOROLOGICALREVIEW();
            //tblmarinemeteorologicalreview_Model.PUBLISHDATE = dt;
            //System.Data.DataTable tblmarinemeteorologicalreview = (System.Data.DataTable)new sql_TBLMARINEMETEOROLOGICALREVIEW().get_TBLMARINEMETEOROLOGICALREVIEW_AllData(tblmarinemeteorologicalreview_Model);
            //METEOROLOGICALREVIEW= tblmarinemeteorologicalreview.Rows[0]["METEOROLOGICALREVIEW"].ToString();
            //METEOROLOGICALREVIEW24HOUR = tblmarinemeteorologicalreview.Rows[0]["METEOROLOGICALREVIEW24HOUR"].ToString();





            //港口潮位预报数据库提取
            TBLHARBOURTIDELEVEL tblharbourtidelevel_Model = new TBLHARBOURTIDELEVEL();
            tblharbourtidelevel_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblharbourtidelevel = (System.Data.DataTable)new sql_TBLHARBOURTIDELEVEL().get_TBLHARBOURTIDELEVEL_AllData(tblharbourtidelevel_Model);

            if (tblharbourtidelevel.Rows.Count == 0) { }
            else
            {
                dt3.Columns.Add("PUBLISHDATE", typeof(string));
                dt3.Columns.Add("HTLHARBOUR", typeof(string));
                dt3.Columns.Add("FORECASTDATE", typeof(string));
                dt3.Columns.Add("HTLFIRSTWAVEOFTIME", typeof(string));
                dt3.Columns.Add("HTLFIRSTWAVETIDELEVEL", typeof(string));
                dt3.Columns.Add("HTLFIRSTTIMELOWTIDE", typeof(string));
                dt3.Columns.Add("HTLLOWTIDELEVELFORTHEFIRSTTIME", typeof(string));
                dt3.Columns.Add("HTLSECONDWAVEOFTIME", typeof(string));
                dt3.Columns.Add("HTLSECONDWAVETIDELEVEL", typeof(string));
                dt3.Columns.Add("HTLSECONDTIMELOWTIDE", typeof(string));
                dt3.Columns.Add("HTLLOWTIDELEVELFORTHESECONDTIM", typeof(string));


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
                    if (tblharbourtidelevel.Rows[i]["HTLHARBOUR"].ToString() == "龙口港")
                    {
                        DataRow row = dt3.NewRow();
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
                        dt3.Rows.Add(row);
                    }
                    if (tblharbourtidelevel.Rows[i]["HTLHARBOUR"].ToString() == "黄河海港")
                    {
                        DataRow row = dt4.NewRow();
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
                //龙口港
                FORECASTDATE21 = dt3.Rows[0]["FORECASTDATE"].ToString();
                if (FORECASTDATE21 != null || FORECASTDATE21 != "")
                {
                    string[] FORECASTDATE221 = FORECASTDATE21.Split(' ');
                    FORECASTDATE21 = FORECASTDATE221[0].ToString();
                }
                FORECASTDATE22 = dt3.Rows[1]["FORECASTDATE"].ToString();
                if (FORECASTDATE22 != null || FORECASTDATE22 != "")
                {
                    string[] FORECASTDATE222 = FORECASTDATE22.Split(' ');
                    FORECASTDATE22 = FORECASTDATE222[0].ToString();
                }
                FORECASTDATE23 = dt3.Rows[2]["FORECASTDATE"].ToString();
                if (FORECASTDATE23 != null || FORECASTDATE23 != "")
                {
                    string[] FORECASTDATE223 = FORECASTDATE23.Split(' ');
                    FORECASTDATE23 = FORECASTDATE223[0].ToString();
                }
                HTLFIRSTWAVEOFTIME1 = dt3.Rows[0]["HTLFIRSTWAVEOFTIME"].ToString();
                HTLFIRSTWAVEOFTIME2 = dt3.Rows[1]["HTLFIRSTWAVEOFTIME"].ToString();
                HTLFIRSTWAVEOFTIME3 = dt3.Rows[2]["HTLFIRSTWAVEOFTIME"].ToString();
                HTLFIRSTWAVETIDELEVEL1 = dt3.Rows[0]["HTLFIRSTWAVETIDELEVEL"].ToString();
                HTLFIRSTWAVETIDELEVEL2 = dt3.Rows[1]["HTLFIRSTWAVETIDELEVEL"].ToString();
                HTLFIRSTWAVETIDELEVEL3 = dt3.Rows[2]["HTLFIRSTWAVETIDELEVEL"].ToString();
                HTLFIRSTTIMELOWTIDE1 = dt3.Rows[0]["HTLFIRSTTIMELOWTIDE"].ToString();
                HTLFIRSTTIMELOWTIDE2 = dt3.Rows[1]["HTLFIRSTTIMELOWTIDE"].ToString();
                HTLFIRSTTIMELOWTIDE3 = dt3.Rows[2]["HTLFIRSTTIMELOWTIDE"].ToString();
                HTLLOWTIDELEVELFORTHEFIRSTTIME1 = dt3.Rows[0]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                HTLLOWTIDELEVELFORTHEFIRSTTIME2 = dt3.Rows[1]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                HTLLOWTIDELEVELFORTHEFIRSTTIME3 = dt3.Rows[2]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                HTLSECONDWAVEOFTIME1 = dt3.Rows[0]["HTLSECONDWAVEOFTIME"].ToString();
                HTLSECONDWAVEOFTIME2 = dt3.Rows[1]["HTLSECONDWAVEOFTIME"].ToString();
                HTLSECONDWAVEOFTIME3 = dt3.Rows[2]["HTLSECONDWAVEOFTIME"].ToString();
                HTLSECONDWAVETIDELEVEL1 = dt3.Rows[0]["HTLSECONDWAVETIDELEVEL"].ToString();
                HTLSECONDWAVETIDELEVEL2 = dt3.Rows[1]["HTLSECONDWAVETIDELEVEL"].ToString();
                HTLSECONDWAVETIDELEVEL3 = dt3.Rows[2]["HTLSECONDWAVETIDELEVEL"].ToString();
                HTLSECONDTIMELOWTIDE1 = dt3.Rows[0]["HTLSECONDTIMELOWTIDE"].ToString();
                HTLSECONDTIMELOWTIDE2 = dt3.Rows[1]["HTLSECONDTIMELOWTIDE"].ToString();
                HTLSECONDTIMELOWTIDE3 = dt3.Rows[2]["HTLSECONDTIMELOWTIDE"].ToString();
                HTLLOWTIDELEVELFORTHESECONDTIM1 = dt3.Rows[0]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                HTLLOWTIDELEVELFORTHESECONDTIM2 = dt3.Rows[1]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                HTLLOWTIDELEVELFORTHESECONDTIM3 = dt3.Rows[2]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();

                //黄河海港
                FORECASTDATE24 = dt4.Rows[0]["FORECASTDATE"].ToString();
                if (FORECASTDATE24 != null || FORECASTDATE24 != "")
                {
                    string[] FORECASTDATE221 = FORECASTDATE24.Split(' ');
                    FORECASTDATE24 = FORECASTDATE221[0].ToString();
                }
                FORECASTDATE25 = dt4.Rows[1]["FORECASTDATE"].ToString();
                if (FORECASTDATE25 != null || FORECASTDATE25 != "")
                {
                    string[] FORECASTDATE222 = FORECASTDATE25.Split(' ');
                    FORECASTDATE25 = FORECASTDATE222[0].ToString();
                }
                FORECASTDATE26 = dt4.Rows[2]["FORECASTDATE"].ToString();
                if (FORECASTDATE26 != null || FORECASTDATE26 != "")
                {
                    string[] FORECASTDATE223 = FORECASTDATE26.Split(' ');
                    FORECASTDATE26 = FORECASTDATE223[0].ToString();
                }
                HTLFIRSTWAVEOFTIME4 = dt4.Rows[0]["HTLFIRSTWAVEOFTIME"].ToString();
                HTLFIRSTWAVEOFTIME5 = dt4.Rows[1]["HTLFIRSTWAVEOFTIME"].ToString();
                HTLFIRSTWAVEOFTIME6 = dt4.Rows[2]["HTLFIRSTWAVEOFTIME"].ToString();
                HTLFIRSTWAVETIDELEVEL4 = dt4.Rows[0]["HTLFIRSTWAVETIDELEVEL"].ToString();
                HTLFIRSTWAVETIDELEVEL5 = dt4.Rows[1]["HTLFIRSTWAVETIDELEVEL"].ToString();
                HTLFIRSTWAVETIDELEVEL6 = dt4.Rows[2]["HTLFIRSTWAVETIDELEVEL"].ToString();
                HTLFIRSTTIMELOWTIDE4 = dt4.Rows[0]["HTLFIRSTTIMELOWTIDE"].ToString();
                HTLFIRSTTIMELOWTIDE5 = dt4.Rows[1]["HTLFIRSTTIMELOWTIDE"].ToString();
                HTLFIRSTTIMELOWTIDE6 = dt4.Rows[2]["HTLFIRSTTIMELOWTIDE"].ToString();
                HTLLOWTIDELEVELFORTHEFIRSTTIME4 = dt4.Rows[0]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                HTLLOWTIDELEVELFORTHEFIRSTTIME5 = dt4.Rows[1]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                HTLLOWTIDELEVELFORTHEFIRSTTIME6 = dt4.Rows[2]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                HTLSECONDWAVEOFTIME4 = dt4.Rows[0]["HTLSECONDWAVEOFTIME"].ToString();
                HTLSECONDWAVEOFTIME5 = dt4.Rows[1]["HTLSECONDWAVEOFTIME"].ToString();
                HTLSECONDWAVEOFTIME6 = dt4.Rows[2]["HTLSECONDWAVEOFTIME"].ToString();
                HTLSECONDWAVETIDELEVEL4 = dt4.Rows[0]["HTLSECONDWAVETIDELEVEL"].ToString();
                HTLSECONDWAVETIDELEVEL5 = dt4.Rows[1]["HTLSECONDWAVETIDELEVEL"].ToString();
                HTLSECONDWAVETIDELEVEL6 = dt4.Rows[2]["HTLSECONDWAVETIDELEVEL"].ToString();
                HTLSECONDTIMELOWTIDE4 = dt4.Rows[0]["HTLSECONDTIMELOWTIDE"].ToString();
                HTLSECONDTIMELOWTIDE5 = dt4.Rows[1]["HTLSECONDTIMELOWTIDE"].ToString();
                HTLSECONDTIMELOWTIDE6 = dt4.Rows[2]["HTLSECONDTIMELOWTIDE"].ToString();
                HTLLOWTIDELEVELFORTHESECONDTIM4 = dt4.Rows[0]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                HTLLOWTIDELEVELFORTHESECONDTIM5 = dt4.Rows[1]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                HTLLOWTIDELEVELFORTHESECONDTIM6 = dt4.Rows[2]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
            }


            //为了方便管理声明书签数组
            object[] BookMark = new object[101];//原来98新添加3个预报员电话改为
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班

            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真
            BookMark[3] = "FWAVEFORECASTER";//海浪预报员
            BookMark[4] = "FTIDALFORECASTER"; //潮汐预报员
            BookMark[5] = "FWATERTEMPERATUREFORECASTER";//海温预报员
            BookMark[6] = "FORECASTDATE1";//72小时渤海海区及黄河海港风、浪预报日期1
            BookMark[7] = "FORECASTDATE2";//72小时渤海海区及黄河海港风、浪预报日期2
            BookMark[8] = "FORECASTDATE3";//72小时渤海海区及黄河海港风、浪预报日期3
            BookMark[9] = "FORECASTDATE4";//72小时渤海海区及黄河海港风、浪预报日期4
            BookMark[10] = "FORECASTDATE5";//72小时渤海海区及黄河海港风、浪预报日期5
            BookMark[11] = "FORECASTDATE6";//72小时渤海海区及黄河海港风、浪预报日期6
            BookMark[12] = "YRBHWWFWAVEHEIGHT1";//72小时渤海海区及黄河海港风、浪预报波高1
            BookMark[13] = "YRBHWWFWAVEHEIGHT2";//72小时渤海海区及黄河海港风、浪预报波高2
            BookMark[14] = "YRBHWWFWAVEHEIGHT3";//72小时渤海海区及黄河海港风、浪预报波高3
            BookMark[15] = "YRBHWWFWAVEHEIGHT4";//72小时渤海海区及黄河海港风、浪预报波高4
            BookMark[16] = "YRBHWWFWAVEHEIGHT5";//72小时渤海海区及黄河海港风、浪预报波高5
            BookMark[17] = "YRBHWWFWAVEHEIGHT6";//72小时渤海海区及黄河海港风、浪预报波高6
            BookMark[18] = "YRBHWWFWAVEDIR1";//72小时渤海海区及黄河海港风、浪预报波向1
            BookMark[19] = "YRBHWWFWAVEDIR2";//72小时渤海海区及黄河海港风、浪预报波向2
            BookMark[20] = "YRBHWWFWAVEDIR3";//72小时渤海海区及黄河海港风、浪预报波向3
            BookMark[21] = "YRBHWWFWAVEDIR4";//72小时渤海海区及黄河海港风、浪预报波向4
            BookMark[22] = "YRBHWWFWAVEDIR5";//72小时渤海海区及黄河海港风、浪预报波向5
            BookMark[23] = "YRBHWWFWAVEDIR6";//72小时渤海海区及黄河海港风、浪预报波向6
            BookMark[24] = "YRBHWWFFLOWDIR1";//72小时渤海海区及黄河海港风、浪预报风向1
            BookMark[25] = "YRBHWWFFLOWDIR2";//72小时渤海海区及黄河海港风、浪预报风向2
            BookMark[26] = "YRBHWWFFLOWDIR3";//72小时渤海海区及黄河海港风、浪预报风向3
            BookMark[27] = "YRBHWWFFLOWDIR4";//72小时渤海海区及黄河海港风、浪预报风向4
            BookMark[28] = "YRBHWWFFLOWDIR5";//72小时渤海海区及黄河海港风、浪预报风向5
            BookMark[29] = "YRBHWWFFLOWDIR6";//72小时渤海海区及黄河海港风、浪预报风向6
            BookMark[30] = "YRBHWWFFLOWLEVEL1";//72小时渤海海区及黄河海港风、浪预报风力1
            BookMark[31] = "YRBHWWFFLOWLEVEL2";//72小时渤海海区及黄河海港风、浪预报风力2
            BookMark[32] = "YRBHWWFFLOWLEVEL3"; //72小时渤海海区及黄河海港风、浪预报风力3
            BookMark[33] = "YRBHWWFFLOWLEVEL4";//72小时渤海海区及黄河海港风、浪预报风力4
            BookMark[34] = "YRBHWWFFLOWLEVEL5";//72小时渤海海区及黄河海港风、浪预报风力5
            BookMark[35] = "YRBHWWFFLOWLEVEL6";//72小时渤海海区及黄河海港风、浪预报风力6
            BookMark[36] = "YRBHWWFWATERTEMPERATURE4";//72小时渤海海区及黄河海港风、浪预报水温4
            BookMark[37] = "YRBHWWFWATERTEMPERATURE5";//72小时渤海海区及黄河海港风、浪预报水温5
            BookMark[38] = "YRBHWWFWATERTEMPERATURE6";//72小时渤海海区及黄河海港风、浪预报水温6
            BookMark[39] = "FORECASTDATE21";//港口潮位预报日期1
            BookMark[40] = "FORECASTDATE22";//港口潮位预报日期2
            BookMark[41] = "FORECASTDATE23";//港口潮位预报日期3
            BookMark[42] = "FORECASTDATE24";//港口潮位预报日期4
            BookMark[43] = "FORECASTDATE25";//港口潮位预报日期5
            BookMark[44] = "FORECASTDATE26";//港口潮位预报日期6
            BookMark[45] = "HTLFIRSTWAVEOFTIME1"; //第一次高潮时间1
            BookMark[46] = "HTLFIRSTWAVEOFTIME2"; //第一次高潮时间2
            BookMark[47] = "HTLFIRSTWAVEOFTIME3";//第一次高潮时间3
            BookMark[48] = "HTLFIRSTWAVEOFTIME4";//第一次高潮时间4
            BookMark[49] = "HTLFIRSTWAVEOFTIME5"; //第一次高潮时间5
            BookMark[50] = "HTLFIRSTWAVEOFTIME6";//第一次高潮时间6
            BookMark[51] = "HTLFIRSTWAVETIDELEVEL1";//第一次高潮潮位1
            BookMark[52] = "HTLFIRSTWAVETIDELEVEL2"; //第一次高潮潮位2
            BookMark[53] = "HTLFIRSTWAVETIDELEVEL3";//第一次高潮潮位3
            BookMark[54] = "HTLFIRSTWAVETIDELEVEL4";//第一次高潮潮位4
            BookMark[55] = "HTLFIRSTWAVETIDELEVEL5"; //第一次高潮潮位5
            BookMark[56] = "HTLFIRSTWAVETIDELEVEL6";//第一次高潮潮位6
            BookMark[57] = "HTLFIRSTTIMELOWTIDE1";//第一次低潮时间1
            BookMark[58] = "HTLFIRSTTIMELOWTIDE2"; //第一次低潮时间2
            BookMark[59] = "HTLFIRSTTIMELOWTIDE3";//第一次低潮时间3
            BookMark[60] = "HTLFIRSTTIMELOWTIDE4";//第一次低潮时间4
            BookMark[61] = "HTLFIRSTTIMELOWTIDE5"; //第一次低潮时间5
            BookMark[62] = "HTLFIRSTTIMELOWTIDE6";//第一次低潮时间6
            BookMark[63] = "HTLLOWTIDELEVELFORTHEFIRSTTIME1";//第一次低潮潮位1
            BookMark[64] = "HTLLOWTIDELEVELFORTHEFIRSTTIME2"; //第一次低潮潮位2
            BookMark[65] = "HTLLOWTIDELEVELFORTHEFIRSTTIME3";//第一次低潮潮位3
            BookMark[66] = "HTLLOWTIDELEVELFORTHEFIRSTTIME4";//第一次低潮潮位4
            BookMark[67] = "HTLLOWTIDELEVELFORTHEFIRSTTIME5"; //第一次低潮潮位5
            BookMark[68] = "HTLLOWTIDELEVELFORTHEFIRSTTIME6";//第一次低潮潮位6
            BookMark[69] = "HTLSECONDWAVEOFTIME1";//第二次高潮时间1
            BookMark[70] = "HTLSECONDWAVEOFTIME2"; //第二次高潮时间2
            BookMark[71] = "HTLSECONDWAVEOFTIME3";//第二次高潮时间3
            BookMark[72] = "HTLSECONDWAVEOFTIME4";//第二次高潮时间4
            BookMark[73] = "HTLSECONDWAVEOFTIME5"; //第二次高潮时间5
            BookMark[74] = "HTLSECONDWAVEOFTIME6";//第二次高潮时间6
            BookMark[75] = "HTLSECONDWAVETIDELEVEL1";//第二次高潮潮位1
            BookMark[76] = "HTLSECONDWAVETIDELEVEL2"; //第二次高潮潮位2
            BookMark[77] = "HTLSECONDWAVETIDELEVEL3";//第二次高潮潮位3
            BookMark[78] = "HTLSECONDWAVETIDELEVEL4"; //第二次高潮潮位4
            BookMark[79] = "HTLSECONDWAVETIDELEVEL5";//第二次高潮潮位5
            BookMark[80] = "HTLSECONDWAVETIDELEVEL6";//第二次高潮潮位6
            BookMark[81] = "HTLSECONDTIMELOWTIDE1"; //第二次低潮时间1
            BookMark[82] = "HTLSECONDTIMELOWTIDE2";//第二次低潮时间2
            BookMark[83] = "HTLSECONDTIMELOWTIDE3";//第二次低潮时间3
            BookMark[84] = "HTLSECONDTIMELOWTIDE4"; //第二次低潮时间4
            BookMark[85] = "HTLSECONDTIMELOWTIDE5";//第二次低潮时间5
            BookMark[86] = "HTLSECONDTIMELOWTIDE6";//第二次低潮时间6
            BookMark[87] = "HTLLOWTIDELEVELFORTHESECONDTIM1"; //第二次低潮潮位1
            BookMark[88] = "HTLLOWTIDELEVELFORTHESECONDTIM2";//第二次低潮潮位2
            BookMark[89] = "HTLLOWTIDELEVELFORTHESECONDTIM3"; //第二次低潮潮位3
            BookMark[90] = "HTLLOWTIDELEVELFORTHESECONDTIM4";//第二次低潮潮位4  
            BookMark[91] = "HTLLOWTIDELEVELFORTHESECONDTIM5"; //第二次低潮潮位5
            BookMark[92] = "HTLLOWTIDELEVELFORTHESECONDTIM6";//第二次低潮潮位6
            BookMark[93] = "METEOROLOGICALREVIEW"; //综述3天
            BookMark[94] = "METEOROLOGICALREVIEW24HOUR";//综述24小时

            BookMark[95] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[96] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[97] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话
            //BookMark[95] = "PUBLISHTIME";

            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;

            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送

            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;

            doc.Bookmarks.get_Item(ref BookMark[95]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[96]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[97]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;



            if (FORECASTDATE1 != null && FORECASTDATE1 != "")
            {
                doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = this.transDateToString(FORECASTDATE1);
            }
            if (FORECASTDATE2 != null && FORECASTDATE2 != "")
            {
                doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = this.transDateToString(FORECASTDATE2);
            }
            if (FORECASTDATE3 != null && FORECASTDATE3 != "")
            {
                doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = this.transDateToString(FORECASTDATE3);
            }
            if (FORECASTDATE4 != null && FORECASTDATE4 != "")
            {
                doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = this.transDateToString(FORECASTDATE4);
            }
            if (FORECASTDATE5 != null && FORECASTDATE5 != "")
            {
                doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = this.transDateToString(FORECASTDATE5);
            }
            if (FORECASTDATE6 != null && FORECASTDATE6 != "")
            {
                doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = this.transDateToString(FORECASTDATE6);
            }



            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = YRBHWWFWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = YRBHWWFWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = YRBHWWFWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = YRBHWWFWAVEHEIGHT4;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = YRBHWWFWAVEHEIGHT5;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = YRBHWWFWAVEHEIGHT6;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = YRBHWWFWAVEDIR1;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = YRBHWWFWAVEDIR2;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = YRBHWWFWAVEDIR3;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = YRBHWWFWAVEDIR4;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = YRBHWWFWAVEDIR5;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = YRBHWWFWAVEDIR6;
            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = YRBHWWFFLOWDIR1;
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = YRBHWWFFLOWDIR2;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = YRBHWWFFLOWDIR3;
            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = YRBHWWFFLOWDIR4;
            doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = YRBHWWFFLOWDIR5;
            doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = YRBHWWFFLOWDIR6;
            doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = YRBHWWFFLOWLEVEL1;
            doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = YRBHWWFFLOWLEVEL2;
            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = YRBHWWFFLOWLEVEL3;
            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = YRBHWWFFLOWLEVEL4;
            doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = YRBHWWFFLOWLEVEL5;
            doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = YRBHWWFFLOWLEVEL6;
            doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = YRBHWWFWATERTEMPERATURE4;
            doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = YRBHWWFWATERTEMPERATURE5;
            doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = YRBHWWFWATERTEMPERATURE6;
            doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = this.transDateToString(FORECASTDATE21);
            doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = this.transDateToString(FORECASTDATE22);
            doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = this.transDateToString(FORECASTDATE23);
            doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = this.transDateToString(FORECASTDATE24);
            doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = this.transDateToString(FORECASTDATE25);
            doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = this.transDateToString(FORECASTDATE26);


            doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = HTLFIRSTWAVEOFTIME1;
            doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = HTLFIRSTWAVEOFTIME2;
            doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = HTLFIRSTWAVEOFTIME3;
            doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = HTLFIRSTWAVEOFTIME4;
            doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = HTLFIRSTWAVEOFTIME5;
            doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = HTLFIRSTWAVEOFTIME6;
            doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = HTLFIRSTWAVETIDELEVEL1;
            doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = HTLFIRSTWAVETIDELEVEL2;
            doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = HTLFIRSTWAVETIDELEVEL3;
            doc.Bookmarks.get_Item(ref BookMark[54]).Range.Text = HTLFIRSTWAVETIDELEVEL4;
            doc.Bookmarks.get_Item(ref BookMark[55]).Range.Text = HTLFIRSTWAVETIDELEVEL5;
            doc.Bookmarks.get_Item(ref BookMark[56]).Range.Text = HTLFIRSTWAVETIDELEVEL6;
            doc.Bookmarks.get_Item(ref BookMark[57]).Range.Text = HTLFIRSTTIMELOWTIDE1;
            doc.Bookmarks.get_Item(ref BookMark[58]).Range.Text = HTLFIRSTTIMELOWTIDE2;
            doc.Bookmarks.get_Item(ref BookMark[59]).Range.Text = HTLFIRSTTIMELOWTIDE3;
            doc.Bookmarks.get_Item(ref BookMark[60]).Range.Text = HTLFIRSTTIMELOWTIDE4;
            doc.Bookmarks.get_Item(ref BookMark[61]).Range.Text = HTLFIRSTTIMELOWTIDE5;
            doc.Bookmarks.get_Item(ref BookMark[62]).Range.Text = HTLFIRSTTIMELOWTIDE6;
            doc.Bookmarks.get_Item(ref BookMark[63]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME1;
            doc.Bookmarks.get_Item(ref BookMark[64]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME2;
            doc.Bookmarks.get_Item(ref BookMark[65]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME3;
            doc.Bookmarks.get_Item(ref BookMark[66]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME4;
            doc.Bookmarks.get_Item(ref BookMark[67]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME5;
            doc.Bookmarks.get_Item(ref BookMark[68]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME6;
            doc.Bookmarks.get_Item(ref BookMark[69]).Range.Text = HTLSECONDWAVEOFTIME1; 
            doc.Bookmarks.get_Item(ref BookMark[70]).Range.Text = HTLSECONDWAVEOFTIME2; 
            doc.Bookmarks.get_Item(ref BookMark[71]).Range.Text = HTLSECONDWAVEOFTIME3; 
            doc.Bookmarks.get_Item(ref BookMark[72]).Range.Text = HTLSECONDWAVEOFTIME4; 
            doc.Bookmarks.get_Item(ref BookMark[73]).Range.Text = HTLSECONDWAVEOFTIME5;
            doc.Bookmarks.get_Item(ref BookMark[74]).Range.Text = HTLSECONDWAVEOFTIME6;
            doc.Bookmarks.get_Item(ref BookMark[75]).Range.Text = HTLSECONDWAVETIDELEVEL1;
            doc.Bookmarks.get_Item(ref BookMark[76]).Range.Text = HTLSECONDWAVETIDELEVEL2;
            doc.Bookmarks.get_Item(ref BookMark[77]).Range.Text = HTLSECONDWAVETIDELEVEL3;
            doc.Bookmarks.get_Item(ref BookMark[78]).Range.Text = HTLSECONDWAVETIDELEVEL4;
            doc.Bookmarks.get_Item(ref BookMark[79]).Range.Text = HTLSECONDWAVETIDELEVEL5;
            doc.Bookmarks.get_Item(ref BookMark[80]).Range.Text = HTLSECONDWAVETIDELEVEL6;
            doc.Bookmarks.get_Item(ref BookMark[81]).Range.Text = HTLSECONDTIMELOWTIDE1;
            doc.Bookmarks.get_Item(ref BookMark[82]).Range.Text = HTLSECONDTIMELOWTIDE2;
            doc.Bookmarks.get_Item(ref BookMark[83]).Range.Text = HTLSECONDTIMELOWTIDE3;
            doc.Bookmarks.get_Item(ref BookMark[84]).Range.Text = HTLSECONDTIMELOWTIDE4;
            doc.Bookmarks.get_Item(ref BookMark[85]).Range.Text = HTLSECONDTIMELOWTIDE5;
            doc.Bookmarks.get_Item(ref BookMark[86]).Range.Text = HTLSECONDTIMELOWTIDE6;
            doc.Bookmarks.get_Item(ref BookMark[87]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM1;
            doc.Bookmarks.get_Item(ref BookMark[88]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM2;
            doc.Bookmarks.get_Item(ref BookMark[89]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM3;
            doc.Bookmarks.get_Item(ref BookMark[90]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM4;
            doc.Bookmarks.get_Item(ref BookMark[91]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM5;
            doc.Bookmarks.get_Item(ref BookMark[92]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM6;
            doc.Bookmarks.get_Item(ref BookMark[93]).Range.Text = METEOROLOGICALREVIEW;
            doc.Bookmarks.get_Item(ref BookMark[94]).Range.Text = METEOROLOGICALREVIEW24HOUR;
            //doc.Bookmarks.get_Item(ref BookMark[95]).Range.Text = PUBLISHTIME;

            //3天海洋水文气象预报综述
            sql_TBLZS hyzs_3DAYS = new sql_TBLZS();
            DataTable dt_zs3DAYS = (DataTable)hyzs_3DAYS.get_TBLSWQX_ZS_3DayS_OR_24HourS(dt);
            if (dt_zs3DAYS != null && dt_zs3DAYS.Rows.Count > 0)
            {
                object mark_3DAYS = "HYQXYBZS3DAYS";
                doc.Bookmarks.get_Item(ref mark_3DAYS).Range.Text = dt_zs3DAYS.Rows[0]["METEOROLOGICALREVIEW"].ToString()+ dt_zs3DAYS.Rows[0]["METEOROLOGICALREVIEWCX"].ToString();
            }

            //输出完毕后关闭doc对象
            object IsSave = true;
            doc.Close(ref IsSave, ref missing, ref missing);
            return 1;
        }
        catch (Exception ex)
        {
            //throw ex;

            //输出完毕后关闭doc对象
            object IsSave = true;
            doc.Close(ref IsSave, ref missing, ref missing);
            WriteLog.Write(ex.ToString());
            return 0;
        }
    }

    public string transDateToString(string dateTime)
    {
        DateTime dt = Convert.ToDateTime(dateTime);
        var month = dt.Month;
        var day = dt.Day;
        var result = month + "月" + day + "日";
        return result;
    }
}