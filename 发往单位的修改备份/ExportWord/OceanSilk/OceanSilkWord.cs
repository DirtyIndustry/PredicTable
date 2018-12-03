using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;//.NET选项卡中的"Microsoft.Office.Interop.Word"

namespace PredicTable.ExportWord.OceanSilk
{
    /// <summary>
    /// 海上丝绸之路Word文档处理
    /// Add:sl
    /// </summary>
    public class OceanSilkWord
    {
        string PUBLISHTIME = "";

        //风浪、气象临时表
        System.Data.DataTable dt1 = new DataTable("dt1");
        System.Data.DataTable dt2 = new DataTable("dt2");
        System.Data.DataTable dt3 = new DataTable("dt3");
        //潮汐临时表
        System.Data.DataTable dt4 = new DataTable("dt4");
        System.Data.DataTable dt5 = new DataTable("dt5");
        System.Data.DataTable dt6 = new DataTable("dt6");


        //风浪、气象书签
        string FORECASTDATEFL1 = "";
        string FORECASTDATEFL2 = "";
        string FORECASTDATEFL3 = "";
        string FORECASTDATEFL4 = "";
        string FORECASTDATEFL5 = "";
        string FORECASTDATEFL6 = "";
        string FORECASTDATEFL7 = "";
        string FORECASTDATEFL8 = "";
        string FORECASTDATEFL9 = "";
        string YRBHWWFWAVEHEIGHT1 = "";
        string YRBHWWFWAVEHEIGHT2 = "";
        string YRBHWWFWAVEHEIGHT3 = "";
        string YRBHWWFWAVEHEIGHT4 = "";
        string YRBHWWFWAVEHEIGHT5 = "";
        string YRBHWWFWAVEHEIGHT6 = "";
        string YRBHWWFWAVEHEIGHT7 = "";
        string YRBHWWFWAVEHEIGHT8 = "";
        string YRBHWWFWAVEHEIGHT9 = "";
        string YRBHWWFWAVEDIR1 = "";
        string YRBHWWFWAVEDIR2 = "";
        string YRBHWWFWAVEDIR3 = "";
        string YRBHWWFWAVEDIR4 = "";
        string YRBHWWFWAVEDIR5 = "";
        string YRBHWWFWAVEDIR6 = "";
        string YRBHWWFWAVEDIR7 = "";
        string YRBHWWFWAVEDIR8 = "";
        string YRBHWWFWAVEDIR9 = "";
        string YRBHWWFFLOWDIR1 = "";
        string YRBHWWFFLOWDIR2 = "";
        string YRBHWWFFLOWDIR3 = "";
        string YRBHWWFFLOWDIR4 = "";
        string YRBHWWFFLOWDIR5 = "";
        string YRBHWWFFLOWDIR6 = "";
        string YRBHWWFFLOWDIR7 = "";
        string YRBHWWFFLOWDIR8 = "";
        string YRBHWWFFLOWDIR9 = "";
        string YRBHWWFFLOWLEVEL1 = "";
        string YRBHWWFFLOWLEVEL2 = "";
        string YRBHWWFFLOWLEVEL3 = "";
        string YRBHWWFFLOWLEVEL4 = "";
        string YRBHWWFFLOWLEVEL5 = "";
        string YRBHWWFFLOWLEVEL6 = "";
        string YRBHWWFFLOWLEVEL7 = "";
        string YRBHWWFFLOWLEVEL8 = "";
        string YRBHWWFFLOWLEVEL9 = "";
        string YRBHWWFWATERTEMPERATURE1 = "";
        string YRBHWWFWATERTEMPERATURE2 = "";
        string YRBHWWFWATERTEMPERATURE3 = "";
        string YRBHWWFWATERTEMPERATURE4 = "";
        string YRBHWWFWATERTEMPERATURE5 = "";
        string YRBHWWFWATERTEMPERATURE6 = "";
        string YRBHWWFWATERTEMPERATURE7 = "";
        string YRBHWWFWATERTEMPERATURE8 = "";
        string YRBHWWFWATERTEMPERATURE9 = "";

        //潮汐书签
        string FORECASTDATE1 = "";
        string FORECASTDATE2 = "";
        string FORECASTDATE3 = "";
        string FORECASTDATE4 = "";
        string FORECASTDATE5 = "";
        string FORECASTDATE6 = "";
        string FORECASTDATE7 = "";
        string FORECASTDATE8 = "";
        string FORECASTDATE9 = "";

        string HTLFIRSTWAVEOFTIME1 = "";
        string HTLFIRSTWAVEOFTIME2 = "";
        string HTLFIRSTWAVEOFTIME3 = "";
        string HTLFIRSTWAVEOFTIME4 = "";
        string HTLFIRSTWAVEOFTIME5 = "";
        string HTLFIRSTWAVEOFTIME6 = "";
        string HTLFIRSTWAVEOFTIME7 = "";
        string HTLFIRSTWAVEOFTIME8 = "";
        string HTLFIRSTWAVEOFTIME9 = "";

        string HTLFIRSTWAVETIDELEVEL1 = "";
        string HTLFIRSTWAVETIDELEVEL2 = "";
        string HTLFIRSTWAVETIDELEVEL3 = "";
        string HTLFIRSTWAVETIDELEVEL4 = "";
        string HTLFIRSTWAVETIDELEVEL5 = "";
        string HTLFIRSTWAVETIDELEVEL6 = "";
        string HTLFIRSTWAVETIDELEVEL7 = "";
        string HTLFIRSTWAVETIDELEVEL8 = "";
        string HTLFIRSTWAVETIDELEVEL9 = "";

        string HTLFIRSTTIMELOWTIDE1 = "";
        string HTLFIRSTTIMELOWTIDE2 = "";
        string HTLFIRSTTIMELOWTIDE3 = "";
        string HTLFIRSTTIMELOWTIDE4 = "";
        string HTLFIRSTTIMELOWTIDE5 = "";
        string HTLFIRSTTIMELOWTIDE6 = "";
        string HTLFIRSTTIMELOWTIDE7 = "";
        string HTLFIRSTTIMELOWTIDE8 = "";
        string HTLFIRSTTIMELOWTIDE9 = "";

        string HTLLOWTIDELEVELFORTHEFIRSTTIME1 = "";
        string HTLLOWTIDELEVELFORTHEFIRSTTIME2 = "";
        string HTLLOWTIDELEVELFORTHEFIRSTTIME3 = "";
        string HTLLOWTIDELEVELFORTHEFIRSTTIME4 = "";
        string HTLLOWTIDELEVELFORTHEFIRSTTIME5 = "";
        string HTLLOWTIDELEVELFORTHEFIRSTTIME6 = "";
        string HTLLOWTIDELEVELFORTHEFIRSTTIME7 = "";
        string HTLLOWTIDELEVELFORTHEFIRSTTIME8 = "";
        string HTLLOWTIDELEVELFORTHEFIRSTTIME9 = "";

        string HTLSECONDWAVEOFTIME1 = "";
        string HTLSECONDWAVEOFTIME2 = "";
        string HTLSECONDWAVEOFTIME3 = "";
        string HTLSECONDWAVEOFTIME4 = "";
        string HTLSECONDWAVEOFTIME5 = "";
        string HTLSECONDWAVEOFTIME6 = "";
        string HTLSECONDWAVEOFTIME7 = "";
        string HTLSECONDWAVEOFTIME8 = "";
        string HTLSECONDWAVEOFTIME9 = "";

        string HTLSECONDWAVETIDELEVEL1 = "";
        string HTLSECONDWAVETIDELEVEL2 = "";
        string HTLSECONDWAVETIDELEVEL3 = "";
        string HTLSECONDWAVETIDELEVEL4 = "";
        string HTLSECONDWAVETIDELEVEL5 = "";
        string HTLSECONDWAVETIDELEVEL6 = "";
        string HTLSECONDWAVETIDELEVEL7 = "";
        string HTLSECONDWAVETIDELEVEL8 = "";
        string HTLSECONDWAVETIDELEVEL9 = "";

        string HTLSECONDTIMELOWTIDE1 = "";
        string HTLSECONDTIMELOWTIDE2 = "";
        string HTLSECONDTIMELOWTIDE3 = "";
        string HTLSECONDTIMELOWTIDE4 = "";
        string HTLSECONDTIMELOWTIDE5 = "";
        string HTLSECONDTIMELOWTIDE6 = "";
        string HTLSECONDTIMELOWTIDE7 = "";
        string HTLSECONDTIMELOWTIDE8 = "";
        string HTLSECONDTIMELOWTIDE9 = "";

        string HTLLOWTIDELEVELFORTHESECONDTIM1 = "";
        string HTLLOWTIDELEVELFORTHESECONDTIM2 = "";
        string HTLLOWTIDELEVELFORTHESECONDTIM3 = "";
        string HTLLOWTIDELEVELFORTHESECONDTIM4 = "";
        string HTLLOWTIDELEVELFORTHESECONDTIM5 = "";
        string HTLLOWTIDELEVELFORTHESECONDTIM6 = "";
        string HTLLOWTIDELEVELFORTHESECONDTIM7 = "";
        string HTLLOWTIDELEVELFORTHESECONDTIM8 = "";
        string HTLLOWTIDELEVELFORTHESECONDTIM9 = "";
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

                //版头及版记数据
                TBLFOOTER tblfooter_Model = new TBLFOOTER();
                tblfooter_Model.PUBLISHDATE = dt;
                //填报信息表提取数据
                System.Data.DataTable tblfooter = (System.Data.DataTable)new sql_TBLFOOTER().get_TBLFOOTER_AllData(tblfooter_Model);
                if(tblfooter != null && tblfooter.Rows.Count > 0)
                {
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

                        tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                        tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                        tblfooter_Model.FWAVEFORECASTERTEL = FWAVEFORECASTERTEL;
                        tblfooter_Model.FTIDALFORECASTERTEL = FTIDALFORECASTERTEL;
                        tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL = FWATERTEMPERATUREFORECASTERTEL;
                    }
                }
               
                object mark_PUBLISHTIME = "PUBLISHTIME";
                doc.Bookmarks.get_Item(ref mark_PUBLISHTIME).Range.Text = PUBLISHTIME;

                #region 海上丝绸之路三天海浪、气象预报
                //海上丝绸之路三天海浪、气象预报
                sql_SilkWaveAndTide silkWaveAndTide = new sql_SilkWaveAndTide();
                TBLYRBHWINDWAVE72HFORECASTTWO model = new TBLYRBHWINDWAVE72HFORECASTTWO();
                model.PUBLISHDATE = dt;
                System.Data.DataTable dtSilkWave = (DataTable)silkWaveAndTide.GetSilkWaveWord(model);
                if (dtSilkWave.Rows.Count == 0)
                {

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

                    dt2.Columns.Add("PUBLISHDATE", typeof(string));
                    dt2.Columns.Add("REPORTAREA", typeof(string));
                    dt2.Columns.Add("FORECASTDATE", typeof(string));
                    dt2.Columns.Add("YRBHWWFWAVEHEIGHT", typeof(string));
                    dt2.Columns.Add("YRBHWWFWAVEDIR", typeof(string));
                    dt2.Columns.Add("YRBHWWFFLOWDIR", typeof(string));
                    dt2.Columns.Add("YRBHWWFFLOWLEVEL", typeof(string));

                    dt3.Columns.Add("PUBLISHDATE", typeof(string));
                    dt3.Columns.Add("REPORTAREA", typeof(string));
                    dt3.Columns.Add("FORECASTDATE", typeof(string));
                    dt3.Columns.Add("YRBHWWFWAVEHEIGHT", typeof(string));
                    dt3.Columns.Add("YRBHWWFWAVEDIR", typeof(string));
                    dt3.Columns.Add("YRBHWWFFLOWDIR", typeof(string));
                    dt3.Columns.Add("YRBHWWFFLOWLEVEL", typeof(string));



                    for (int i = 0; i < dtSilkWave.Rows.Count; i++)
                    {
                        if (dtSilkWave.Rows[i]["REPORTAREA"].ToString() == "青岛港")
                        {
                            DataRow row = dt1.NewRow();
                            row["PUBLISHDATE"] = dtSilkWave.Rows[i]["PUBLISHDATE"].ToString();
                            row["REPORTAREA"] = dtSilkWave.Rows[i]["REPORTAREA"].ToString();
                            row["FORECASTDATE"] = dtSilkWave.Rows[i]["FORECASTDATE"].ToString();
                            row["YRBHWWFWAVEHEIGHT"] = dtSilkWave.Rows[i]["YRBHWWFWAVEHEIGHT"].ToString();
                            row["YRBHWWFWAVEDIR"] = dtSilkWave.Rows[i]["YRBHWWFWAVEDIR"].ToString();
                            row["YRBHWWFFLOWDIR"] = dtSilkWave.Rows[i]["YRBHWWFFLOWDIR"].ToString();
                            row["YRBHWWFFLOWLEVEL"] = dtSilkWave.Rows[i]["YRBHWWFFLOWLEVEL"].ToString();
                            dt1.Rows.Add(row);
                        }
                        if (dtSilkWave.Rows[i]["REPORTAREA"].ToString() == "潍坊港")
                        {
                            DataRow row = dt2.NewRow();
                            row["PUBLISHDATE"] = dtSilkWave.Rows[i]["PUBLISHDATE"].ToString();
                            row["REPORTAREA"] = dtSilkWave.Rows[i]["REPORTAREA"].ToString();
                            row["FORECASTDATE"] = dtSilkWave.Rows[i]["FORECASTDATE"].ToString();
                            row["YRBHWWFWAVEHEIGHT"] = dtSilkWave.Rows[i]["YRBHWWFWAVEHEIGHT"].ToString();
                            row["YRBHWWFWAVEDIR"] = dtSilkWave.Rows[i]["YRBHWWFWAVEDIR"].ToString();
                            row["YRBHWWFFLOWDIR"] = dtSilkWave.Rows[i]["YRBHWWFFLOWDIR"].ToString();
                            row["YRBHWWFFLOWLEVEL"] = dtSilkWave.Rows[i]["YRBHWWFFLOWLEVEL"].ToString();
                            dt2.Rows.Add(row);
                        }
                        if (dtSilkWave.Rows[i]["REPORTAREA"].ToString() == "营口港")
                        {
                            DataRow row = dt3.NewRow();
                            row["PUBLISHDATE"] = dtSilkWave.Rows[i]["PUBLISHDATE"].ToString();
                            row["REPORTAREA"] = dtSilkWave.Rows[i]["REPORTAREA"].ToString();
                            row["FORECASTDATE"] = dtSilkWave.Rows[i]["FORECASTDATE"].ToString();
                            row["YRBHWWFWAVEHEIGHT"] = dtSilkWave.Rows[i]["YRBHWWFWAVEHEIGHT"].ToString();
                            row["YRBHWWFWAVEDIR"] = dtSilkWave.Rows[i]["YRBHWWFWAVEDIR"].ToString();
                            row["YRBHWWFFLOWDIR"] = dtSilkWave.Rows[i]["YRBHWWFFLOWDIR"].ToString();
                            row["YRBHWWFFLOWLEVEL"] = dtSilkWave.Rows[i]["YRBHWWFFLOWLEVEL"].ToString();
                            dt3.Rows.Add(row);
                        }
                    }
                    //青岛港
                    FORECASTDATEFL1 = dt1.Rows[0]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL1 != null || FORECASTDATEFL1 != "")
                    {
                        string[] FORECASTDATE11 = FORECASTDATEFL1.Split(' ');
                        FORECASTDATEFL1 = FORECASTDATE11[0].ToString();
                    }
                    FORECASTDATEFL2 = dt1.Rows[1]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL2 != null || FORECASTDATEFL2 != "")
                    {
                        string[] FORECASTDATE22 = FORECASTDATEFL2.Split(' ');
                        FORECASTDATEFL2 = FORECASTDATE22[0].ToString();
                    }
                    FORECASTDATEFL3 = dt1.Rows[2]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL3 != null || FORECASTDATEFL3 != "")
                    {
                        string[] FORECASTDATE33 = FORECASTDATEFL3.Split(' ');
                        FORECASTDATEFL3 = FORECASTDATE33[0].ToString();
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


                    //潍坊港
                    FORECASTDATEFL4 = dt2.Rows[0]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL4 != null || FORECASTDATEFL4 != "")
                    {
                        string[] FORECASTDATE11 = FORECASTDATEFL4.Split(' ');
                        FORECASTDATEFL4 = FORECASTDATE11[0].ToString();
                    }
                    FORECASTDATEFL5 = dt2.Rows[1]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL5 != null || FORECASTDATEFL5 != "")
                    {
                        string[] FORECASTDATE22 = FORECASTDATEFL5.Split(' ');
                        FORECASTDATEFL5 = FORECASTDATE22[0].ToString();
                    }
                    FORECASTDATEFL6 = dt2.Rows[2]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL6 != null || FORECASTDATEFL6 != "")
                    {
                        string[] FORECASTDATE33 = FORECASTDATEFL6.Split(' ');
                        FORECASTDATEFL6 = FORECASTDATE33[0].ToString();
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

                    //营口港
                    FORECASTDATEFL7 = dt3.Rows[0]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL7 != null || FORECASTDATEFL7 != "")
                    {
                        string[] FORECASTDATE11 = FORECASTDATEFL7.Split(' ');
                        FORECASTDATEFL7 = FORECASTDATE11[0].ToString();
                    }
                    FORECASTDATEFL8 = dt3.Rows[1]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL8 != null || FORECASTDATEFL8 != "")
                    {
                        string[] FORECASTDATE22 = FORECASTDATEFL8.Split(' ');
                        FORECASTDATEFL8 = FORECASTDATE22[0].ToString();
                    }
                    FORECASTDATEFL9 = dt3.Rows[2]["FORECASTDATE"].ToString();
                    if (FORECASTDATEFL9 != null || FORECASTDATEFL9 != "")
                    {
                        string[] FORECASTDATE33 = FORECASTDATEFL9.Split(' ');
                        FORECASTDATEFL9 = FORECASTDATE33[0].ToString();
                    }
                    YRBHWWFWAVEHEIGHT7 = dt3.Rows[0]["YRBHWWFWAVEHEIGHT"].ToString();
                    YRBHWWFWAVEHEIGHT8 = dt3.Rows[1]["YRBHWWFWAVEHEIGHT"].ToString();
                    YRBHWWFWAVEHEIGHT9 = dt3.Rows[2]["YRBHWWFWAVEHEIGHT"].ToString();
                    YRBHWWFWAVEDIR7 = dt3.Rows[0]["YRBHWWFWAVEDIR"].ToString();
                    YRBHWWFWAVEDIR8 = dt3.Rows[1]["YRBHWWFWAVEDIR"].ToString();
                    YRBHWWFWAVEDIR9 = dt3.Rows[2]["YRBHWWFWAVEDIR"].ToString();
                    YRBHWWFFLOWDIR7 = dt3.Rows[0]["YRBHWWFFLOWDIR"].ToString();
                    YRBHWWFFLOWDIR8 = dt3.Rows[1]["YRBHWWFFLOWDIR"].ToString();
                    YRBHWWFFLOWDIR9 = dt3.Rows[2]["YRBHWWFFLOWDIR"].ToString();
                    YRBHWWFFLOWLEVEL7 = dt3.Rows[0]["YRBHWWFFLOWLEVEL"].ToString();
                    YRBHWWFFLOWLEVEL8 = dt3.Rows[1]["YRBHWWFFLOWLEVEL"].ToString();
                    YRBHWWFFLOWLEVEL9 = dt3.Rows[2]["YRBHWWFFLOWLEVEL"].ToString();
                }
                #endregion

                #region 海上丝绸之路三天潮汐预报数据提取


                //海上丝绸之路三天潮汐预报数据提取
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


                    dt5.Columns.Add("PUBLISHDATE", typeof(string));
                    dt5.Columns.Add("HTLHARBOUR", typeof(string));
                    dt5.Columns.Add("FORECASTDATE", typeof(string));
                    dt5.Columns.Add("HTLFIRSTWAVEOFTIME", typeof(string));
                    dt5.Columns.Add("HTLFIRSTWAVETIDELEVEL", typeof(string));
                    dt5.Columns.Add("HTLFIRSTTIMELOWTIDE", typeof(string));
                    dt5.Columns.Add("HTLLOWTIDELEVELFORTHEFIRSTTIME", typeof(string));
                    dt5.Columns.Add("HTLSECONDWAVEOFTIME", typeof(string));
                    dt5.Columns.Add("HTLSECONDWAVETIDELEVEL", typeof(string));
                    dt5.Columns.Add("HTLSECONDTIMELOWTIDE", typeof(string));
                    dt5.Columns.Add("HTLLOWTIDELEVELFORTHESECONDTIM", typeof(string));

                    dt6.Columns.Add("PUBLISHDATE", typeof(string));
                    dt6.Columns.Add("HTLHARBOUR", typeof(string));
                    dt6.Columns.Add("FORECASTDATE", typeof(string));
                    dt6.Columns.Add("HTLFIRSTWAVEOFTIME", typeof(string));
                    dt6.Columns.Add("HTLFIRSTWAVETIDELEVEL", typeof(string));
                    dt6.Columns.Add("HTLFIRSTTIMELOWTIDE", typeof(string));
                    dt6.Columns.Add("HTLLOWTIDELEVELFORTHEFIRSTTIME", typeof(string));
                    dt6.Columns.Add("HTLSECONDWAVEOFTIME", typeof(string));
                    dt6.Columns.Add("HTLSECONDWAVETIDELEVEL", typeof(string));
                    dt6.Columns.Add("HTLSECONDTIMELOWTIDE", typeof(string));
                    dt6.Columns.Add("HTLLOWTIDELEVELFORTHESECONDTIM", typeof(string));

                    for (int i = 0; i < tblharbourtidelevel.Rows.Count; i++)
                    {
                        if (tblharbourtidelevel.Rows[i]["HTLHARBOUR"].ToString() == "青岛港")
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
                        if (tblharbourtidelevel.Rows[i]["HTLHARBOUR"].ToString() == "潍坊港")
                        {
                            DataRow row = dt5.NewRow();
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
                            dt5.Rows.Add(row);
                        }
                        if (tblharbourtidelevel.Rows[i]["HTLHARBOUR"].ToString() == "营口港")
                        {
                            DataRow row = dt6.NewRow();
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
                            dt6.Rows.Add(row);
                        }
                    }

                    //青岛港
                    FORECASTDATE1 = dt4.Rows[0]["FORECASTDATE"].ToString();
                    if (FORECASTDATE1 != null || FORECASTDATE1 != "")
                    {
                        string[] FORECASTDATE221 = FORECASTDATE1.Split(' ');
                        FORECASTDATE1 = FORECASTDATE221[0].ToString();
                    }
                    FORECASTDATE2 = dt4.Rows[1]["FORECASTDATE"].ToString();
                    if (FORECASTDATE2 != null || FORECASTDATE2 != "")
                    {
                        string[] FORECASTDATE222 = FORECASTDATE2.Split(' ');
                        FORECASTDATE2 = FORECASTDATE222[0].ToString();
                    }
                    FORECASTDATE3 = dt4.Rows[2]["FORECASTDATE"].ToString();
                    if (FORECASTDATE3 != null || FORECASTDATE3 != "")
                    {
                        string[] FORECASTDATE223 = FORECASTDATE3.Split(' ');
                        FORECASTDATE3 = FORECASTDATE223[0].ToString();
                    }
                    HTLFIRSTWAVEOFTIME1 = dt4.Rows[0]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVEOFTIME2 = dt4.Rows[1]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVEOFTIME3 = dt4.Rows[2]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVETIDELEVEL1 = dt4.Rows[0]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTWAVETIDELEVEL2 = dt4.Rows[1]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTWAVETIDELEVEL3 = dt4.Rows[2]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTTIMELOWTIDE1 = dt4.Rows[0]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLFIRSTTIMELOWTIDE2 = dt4.Rows[1]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLFIRSTTIMELOWTIDE3 = dt4.Rows[2]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME1 = dt4.Rows[0]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME2 = dt4.Rows[1]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME3 = dt4.Rows[2]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLSECONDWAVEOFTIME1 = dt4.Rows[0]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVEOFTIME2 = dt4.Rows[1]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVEOFTIME3 = dt4.Rows[2]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVETIDELEVEL1 = dt4.Rows[0]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDWAVETIDELEVEL2 = dt4.Rows[1]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDWAVETIDELEVEL3 = dt4.Rows[2]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDTIMELOWTIDE1 = dt4.Rows[0]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLSECONDTIMELOWTIDE2 = dt4.Rows[1]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLSECONDTIMELOWTIDE3 = dt4.Rows[2]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM1 = dt4.Rows[0]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM2 = dt4.Rows[1]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM3 = dt4.Rows[2]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();

                    //潍坊港
                    FORECASTDATE4 = dt5.Rows[0]["FORECASTDATE"].ToString();
                    if (FORECASTDATE4 != null || FORECASTDATE4 != "")
                    {
                        string[] FORECASTDATE221 = FORECASTDATE4.Split(' ');
                        FORECASTDATE4 = FORECASTDATE221[0].ToString();
                    }
                    FORECASTDATE5 = dt5.Rows[1]["FORECASTDATE"].ToString();
                    if (FORECASTDATE5 != null || FORECASTDATE5 != "")
                    {
                        string[] FORECASTDATE222 = FORECASTDATE5.Split(' ');
                        FORECASTDATE5 = FORECASTDATE222[0].ToString();
                    }
                    FORECASTDATE6 = dt5.Rows[2]["FORECASTDATE"].ToString();
                    if (FORECASTDATE6 != null || FORECASTDATE6 != "")
                    {
                        string[] FORECASTDATE223 = FORECASTDATE6.Split(' ');
                        FORECASTDATE6 = FORECASTDATE223[0].ToString();
                    }
                    
                    HTLFIRSTWAVEOFTIME4 = dt5.Rows[0]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVEOFTIME5 = dt5.Rows[1]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVEOFTIME6 = dt5.Rows[2]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVETIDELEVEL4 = dt5.Rows[0]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTWAVETIDELEVEL5 = dt5.Rows[1]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTWAVETIDELEVEL6 = dt5.Rows[2]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTTIMELOWTIDE4 = dt5.Rows[0]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLFIRSTTIMELOWTIDE5 = dt5.Rows[1]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLFIRSTTIMELOWTIDE6 = dt5.Rows[2]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME4 = dt5.Rows[0]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME5 = dt5.Rows[1]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME6 = dt5.Rows[2]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLSECONDWAVEOFTIME4 = dt5.Rows[0]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVEOFTIME5 = dt5.Rows[1]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVEOFTIME6 = dt5.Rows[2]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVETIDELEVEL4 = dt5.Rows[0]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDWAVETIDELEVEL5 = dt5.Rows[1]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDWAVETIDELEVEL6 = dt5.Rows[2]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDTIMELOWTIDE4 = dt5.Rows[0]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLSECONDTIMELOWTIDE5 = dt5.Rows[1]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLSECONDTIMELOWTIDE6 = dt5.Rows[2]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM4 = dt5.Rows[0]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM5 = dt5.Rows[1]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM6 = dt5.Rows[2]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                    //营口港
                    FORECASTDATE7 = dt6.Rows[0]["FORECASTDATE"].ToString();
                    if (FORECASTDATE7 != null || FORECASTDATE7 != "")
                    {
                        string[] FORECASTDATE221 = FORECASTDATE7.Split(' ');
                        FORECASTDATE7 = FORECASTDATE221[0].ToString();
                    }
                    FORECASTDATE8 = dt6.Rows[1]["FORECASTDATE"].ToString();
                    if (FORECASTDATE8 != null || FORECASTDATE8 != "")
                    {
                        string[] FORECASTDATE222 = FORECASTDATE8.Split(' ');
                        FORECASTDATE8 = FORECASTDATE222[0].ToString();
                    }
                    FORECASTDATE9 = dt6.Rows[2]["FORECASTDATE"].ToString();
                    if (FORECASTDATE9 != null || FORECASTDATE9 != "")
                    {
                        string[] FORECASTDATE223 = FORECASTDATE9.Split(' ');
                        FORECASTDATE9 = FORECASTDATE223[0].ToString();
                    }
                    HTLFIRSTWAVEOFTIME7 = dt6.Rows[0]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVEOFTIME8 = dt6.Rows[1]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVEOFTIME9 = dt6.Rows[2]["HTLFIRSTWAVEOFTIME"].ToString();
                    HTLFIRSTWAVETIDELEVEL7 = dt6.Rows[0]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTWAVETIDELEVEL8 = dt6.Rows[1]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTWAVETIDELEVEL9 = dt6.Rows[2]["HTLFIRSTWAVETIDELEVEL"].ToString();
                    HTLFIRSTTIMELOWTIDE7 = dt6.Rows[0]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLFIRSTTIMELOWTIDE8 = dt6.Rows[1]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLFIRSTTIMELOWTIDE9 = dt6.Rows[2]["HTLFIRSTTIMELOWTIDE"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME7 = dt6.Rows[0]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME8 = dt6.Rows[1]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLLOWTIDELEVELFORTHEFIRSTTIME9 = dt6.Rows[2]["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();
                    HTLSECONDWAVEOFTIME7 = dt6.Rows[0]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVEOFTIME8 = dt6.Rows[1]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVEOFTIME9 = dt6.Rows[2]["HTLSECONDWAVEOFTIME"].ToString();
                    HTLSECONDWAVETIDELEVEL7 = dt6.Rows[0]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDWAVETIDELEVEL8 = dt6.Rows[1]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDWAVETIDELEVEL9 = dt6.Rows[2]["HTLSECONDWAVETIDELEVEL"].ToString();
                    HTLSECONDTIMELOWTIDE7 = dt6.Rows[0]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLSECONDTIMELOWTIDE8 = dt6.Rows[1]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLSECONDTIMELOWTIDE9 = dt6.Rows[2]["HTLSECONDTIMELOWTIDE"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM7 = dt6.Rows[0]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM8 = dt6.Rows[1]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                    HTLLOWTIDELEVELFORTHESECONDTIM9 = dt6.Rows[2]["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                }

                #endregion

                #region 书签管理
                //为了方便管理声明书签数组
                object[] BookMark = new object[134];//新增两个预报员电话132改为134
                //赋值书签名
                BookMark[0] = "FRELEASEUNIT";//发布单位
                BookMark[1] = "FZHIBANTEL";//预报值班
                BookMark[2] = "FSENDTEL";//预报值班

                //BookMark[1] = "FTELEPHONE";//电话
                //BookMark[2] = "FFAX";//传真
                BookMark[3] = "FWAVEFORECASTER";//海浪预报员
                BookMark[4] = "FTIDALFORECASTER"; //潮汐预报员

                BookMark[6] = "FORECASTDATEFL1";//海上丝绸之路三天海浪、气象预报预报日期1
                BookMark[7] = "FORECASTDATEFL2";//海上丝绸之路三天海浪、气象预报预报日期2
                BookMark[8] = "FORECASTDATEFL3";//海上丝绸之路三天海浪、气象预报预报日期3
                BookMark[9] = "FORECASTDATEFL4";//海上丝绸之路三天海浪、气象预报预报日期4
                BookMark[10] = "FORECASTDATEFL5";//海上丝绸之路三天海浪、气象预报预报日期5
                BookMark[11] = "FORECASTDATEFL6";//海上丝绸之路三天海浪、气象预报预报日期6
                BookMark[12] = "FORECASTDATEFL7";//海上丝绸之路三天海浪、气象预报预报日期7
                BookMark[13] = "FORECASTDATEFL8";//海上丝绸之路三天海浪、气象预报预报日期8
                BookMark[14] = "FORECASTDATEFL9";//海上丝绸之路三天海浪、气象预报预报日期9

                BookMark[15] = "YRBHWWFWAVEHEIGHT1";//海上丝绸之路三天海浪、气象预报预报波高1
                BookMark[16] = "YRBHWWFWAVEHEIGHT2";//海上丝绸之路三天海浪、气象预报预报波高2
                BookMark[17] = "YRBHWWFWAVEHEIGHT3";//海上丝绸之路三天海浪、气象预报预报波高3
                BookMark[18] = "YRBHWWFWAVEHEIGHT4";//海上丝绸之路三天海浪、气象预报预报波高4
                BookMark[19] = "YRBHWWFWAVEHEIGHT5";//海上丝绸之路三天海浪、气象预报预报波高5
                BookMark[20] = "YRBHWWFWAVEHEIGHT6";//海上丝绸之路三天海浪、气象预报预报波高6
                BookMark[21] = "YRBHWWFWAVEHEIGHT7";//海上丝绸之路三天海浪、气象预报预报波高7
                BookMark[22] = "YRBHWWFWAVEHEIGHT8";//海上丝绸之路三天海浪、气象预报预报波高8
                BookMark[23] = "YRBHWWFWAVEHEIGHT9";//海上丝绸之路三天海浪、气象预报预报波高9

                BookMark[24] = "YRBHWWFWAVEDIR1";//海上丝绸之路三天海浪、气象预报预报波向1
                BookMark[25] = "YRBHWWFWAVEDIR2";//海上丝绸之路三天海浪、气象预报预报波向2
                BookMark[26] = "YRBHWWFWAVEDIR3";//海上丝绸之路三天海浪、气象预报预报波向3
                BookMark[27] = "YRBHWWFWAVEDIR4";//海上丝绸之路三天海浪、气象预报预报波向4
                BookMark[28] = "YRBHWWFWAVEDIR5";//海上丝绸之路三天海浪、气象预报预报波向5
                BookMark[29] = "YRBHWWFWAVEDIR6";//海上丝绸之路三天海浪、气象预报预报波向6
                BookMark[30] = "YRBHWWFWAVEDIR7";//海上丝绸之路三天海浪、气象预报预报波向7
                BookMark[31] = "YRBHWWFWAVEDIR8";//海上丝绸之路三天海浪、气象预报预报波向8
                BookMark[32] = "YRBHWWFWAVEDIR9";//海上丝绸之路三天海浪、气象预报预报波向9

                BookMark[33] = "YRBHWWFFLOWDIR1";//海上丝绸之路三天海浪、气象预报风向1
                BookMark[34] = "YRBHWWFFLOWDIR2";//海上丝绸之路三天海浪、气象预报风向2
                BookMark[35] = "YRBHWWFFLOWDIR3";//海上丝绸之路三天海浪、气象预报风向3
                BookMark[36] = "YRBHWWFFLOWDIR4";//海上丝绸之路三天海浪、气象预报风向4
                BookMark[37] = "YRBHWWFFLOWDIR5";//海上丝绸之路三天海浪、气象预报风向5
                BookMark[38] = "YRBHWWFFLOWDIR6";//海上丝绸之路三天海浪、气象预报风向6
                BookMark[39] = "YRBHWWFFLOWDIR7";//海上丝绸之路三天海浪、气象预报风向7
                BookMark[40] = "YRBHWWFFLOWDIR8";//海上丝绸之路三天海浪、气象预报风向8
                BookMark[41] = "YRBHWWFFLOWDIR9";//海上丝绸之路三天海浪、气象预报风向9

                BookMark[42] = "YRBHWWFFLOWLEVEL1";//海上丝绸之路三天海浪、气象预报风力1
                BookMark[43] = "YRBHWWFFLOWLEVEL2";//海上丝绸之路三天海浪、气象预报风力2
                BookMark[44] = "YRBHWWFFLOWLEVEL3"; //海上丝绸之路三天海浪、气象预报风力3
                BookMark[45] = "YRBHWWFFLOWLEVEL4";//海上丝绸之路三天海浪、气象预报风力4
                BookMark[46] = "YRBHWWFFLOWLEVEL5";//海上丝绸之路三天海浪、气象预报风力5
                BookMark[47] = "YRBHWWFFLOWLEVEL6";//海上丝绸之路三天海浪、气象预报风力6
                BookMark[48] = "YRBHWWFFLOWLEVEL7";//海上丝绸之路三天海浪、气象预报风力7
                BookMark[49] = "YRBHWWFFLOWLEVEL8";//海上丝绸之路三天海浪、气象预报风力8
                BookMark[50] = "YRBHWWFFLOWLEVEL9";//海上丝绸之路三天海浪、气象预报风力9

                BookMark[51] = "FORECASTDATE1";//港口潮位预报日期1
                BookMark[52] = "FORECASTDATE2";//港口潮位预报日期2
                BookMark[53] = "FORECASTDATE3";//港口潮位预报日期3
                BookMark[54] = "FORECASTDATE4";//港口潮位预报日期4
                BookMark[55] = "FORECASTDATE5";//港口潮位预报日期5
                BookMark[56] = "FORECASTDATE6";//港口潮位预报日期6
                BookMark[57] = "FORECASTDATE7";//港口潮位预报日期7
                BookMark[58] = "FORECASTDATE8";//港口潮位预报日期8
                BookMark[59] = "FORECASTDATE9";//港口潮位预报日期9

                BookMark[60] = "HTLFIRSTWAVEOFTIME1"; //第一次高潮时间1
                BookMark[61] = "HTLFIRSTWAVEOFTIME2"; //第一次高潮时间2
                BookMark[62] = "HTLFIRSTWAVEOFTIME3";//第一次高潮时间3
                BookMark[63] = "HTLFIRSTWAVEOFTIME4";//第一次高潮时间4
                BookMark[64] = "HTLFIRSTWAVEOFTIME5"; //第一次高潮时间5
                BookMark[65] = "HTLFIRSTWAVEOFTIME6";//第一次高潮时间6
                BookMark[66] = "HTLFIRSTWAVEOFTIME7";//第一次高潮时间7
                BookMark[67] = "HTLFIRSTWAVEOFTIME8"; //第一次高潮时间8
                BookMark[68] = "HTLFIRSTWAVEOFTIME9";//第一次高潮时间9

                BookMark[69] = "HTLFIRSTWAVETIDELEVEL1";//第一次高潮潮位1
                BookMark[70] = "HTLFIRSTWAVETIDELEVEL2"; //第一次高潮潮位2
                BookMark[71] = "HTLFIRSTWAVETIDELEVEL3";//第一次高潮潮位3
                BookMark[72] = "HTLFIRSTWAVETIDELEVEL4";//第一次高潮潮位4
                BookMark[73] = "HTLFIRSTWAVETIDELEVEL5"; //第一次高潮潮位5
                BookMark[74] = "HTLFIRSTWAVETIDELEVEL6";//第一次高潮潮位6
                BookMark[75] = "HTLFIRSTWAVETIDELEVEL7";//第一次高潮潮位7
                BookMark[76] = "HTLFIRSTWAVETIDELEVEL8"; //第一次高潮潮位8
                BookMark[77] = "HTLFIRSTWAVETIDELEVEL9";//第一次高潮潮位9

                BookMark[78] = "HTLFIRSTTIMELOWTIDE1";//第一次低潮时间1
                BookMark[79] = "HTLFIRSTTIMELOWTIDE2"; //第一次低潮时间2
                BookMark[80] = "HTLFIRSTTIMELOWTIDE3";//第一次低潮时间3
                BookMark[81] = "HTLFIRSTTIMELOWTIDE4";//第一次低潮时间4
                BookMark[82] = "HTLFIRSTTIMELOWTIDE5"; //第一次低潮时间5
                BookMark[83] = "HTLFIRSTTIMELOWTIDE6";//第一次低潮时间6
                BookMark[84] = "HTLFIRSTTIMELOWTIDE7";//第一次低潮时间7
                BookMark[85] = "HTLFIRSTTIMELOWTIDE8"; //第一次低潮时间8
                BookMark[86] = "HTLFIRSTTIMELOWTIDE9";//第一次低潮时间9

                BookMark[87] = "HTLLOWTIDELEVELFORTHEFIRSTTIME1";//第一次低潮潮位1
                BookMark[88] = "HTLLOWTIDELEVELFORTHEFIRSTTIME2"; //第一次低潮潮位2
                BookMark[89] = "HTLLOWTIDELEVELFORTHEFIRSTTIME3";//第一次低潮潮位3
                BookMark[90] = "HTLLOWTIDELEVELFORTHEFIRSTTIME4";//第一次低潮潮位4
                BookMark[91] = "HTLLOWTIDELEVELFORTHEFIRSTTIME5"; //第一次低潮潮位5
                BookMark[92] = "HTLLOWTIDELEVELFORTHEFIRSTTIME6";//第一次低潮潮位6
                BookMark[93] = "HTLLOWTIDELEVELFORTHEFIRSTTIME7";//第一次低潮潮位7
                BookMark[94] = "HTLLOWTIDELEVELFORTHEFIRSTTIME8"; //第一次低潮潮位8
                BookMark[95] = "HTLLOWTIDELEVELFORTHEFIRSTTIME9";//第一次低潮潮位9

                BookMark[96] = "HTLSECONDWAVEOFTIME1";//第二次高潮时间1
                BookMark[97] = "HTLSECONDWAVEOFTIME2"; //第二次高潮时间2
                BookMark[98] = "HTLSECONDWAVEOFTIME3";//第二次高潮时间3
                BookMark[99] = "HTLSECONDWAVEOFTIME4";//第二次高潮时间4
                BookMark[100] = "HTLSECONDWAVEOFTIME5"; //第二次高潮时间5
                BookMark[101] = "HTLSECONDWAVEOFTIME6";//第二次高潮时间6
                BookMark[102] = "HTLSECONDWAVEOFTIME7";//第二次高潮时间7
                BookMark[103] = "HTLSECONDWAVEOFTIME8";//第二次高潮时间8
                BookMark[104] = "HTLSECONDWAVEOFTIME9"; //第二次高潮时间9

                BookMark[105] = "HTLSECONDWAVETIDELEVEL1";//第二次高潮潮位1
                BookMark[106] = "HTLSECONDWAVETIDELEVEL2"; //第二次高潮潮位2
                BookMark[107] = "HTLSECONDWAVETIDELEVEL3";//第二次高潮潮位3
                BookMark[108] = "HTLSECONDWAVETIDELEVEL4"; //第二次高潮潮位4
                BookMark[109] = "HTLSECONDWAVETIDELEVEL5";//第二次高潮潮位5
                BookMark[110] = "HTLSECONDWAVETIDELEVEL6";//第二次高潮潮位6
                BookMark[111] = "HTLSECONDWAVETIDELEVEL7"; //第二次高潮潮位7
                BookMark[112] = "HTLSECONDWAVETIDELEVEL8";//第二次高潮潮位8
                BookMark[113] = "HTLSECONDWAVETIDELEVEL9";//第二次高潮潮位9

                BookMark[114] = "HTLSECONDTIMELOWTIDE1"; //第二次低潮时间1
                BookMark[115] = "HTLSECONDTIMELOWTIDE2";//第二次低潮时间2
                BookMark[116] = "HTLSECONDTIMELOWTIDE3";//第二次低潮时间3
                BookMark[117] = "HTLSECONDTIMELOWTIDE4"; //第二次低潮时间4
                BookMark[118] = "HTLSECONDTIMELOWTIDE5";//第二次低潮时间5
                BookMark[119] = "HTLSECONDTIMELOWTIDE6";//第二次低潮时间6
                BookMark[120] = "HTLSECONDTIMELOWTIDE7"; //第二次低潮时间7
                BookMark[121] = "HTLSECONDTIMELOWTIDE8";//第二次低潮时间8
                BookMark[122] = "HTLSECONDTIMELOWTIDE9";//第二次低潮时间9

                BookMark[123] = "HTLLOWTIDELEVELFORTHESECONDTIM1"; //第二次低潮潮位1
                BookMark[124] = "HTLLOWTIDELEVELFORTHESECONDTIM2";//第二次低潮潮位2
                BookMark[125] = "HTLLOWTIDELEVELFORTHESECONDTIM3"; //第二次低潮潮位3
                BookMark[126] = "HTLLOWTIDELEVELFORTHESECONDTIM4";//第二次低潮潮位4  
                BookMark[127] = "HTLLOWTIDELEVELFORTHESECONDTIM5"; //第二次低潮潮位5
                BookMark[128] = "HTLLOWTIDELEVELFORTHESECONDTIM6";//第二次低潮潮位6
                BookMark[129] = "HTLLOWTIDELEVELFORTHESECONDTIM7";//第二次低潮潮位7  
                BookMark[130] = "HTLLOWTIDELEVELFORTHESECONDTIM8"; //第二次低潮潮位8
                BookMark[131] = "HTLLOWTIDELEVELFORTHESECONDTIM9";//第二次低潮潮位9

                BookMark[132] = "FWAVEFORECASTERTEL";//海浪预报员
                BookMark[133] = "FTIDALFORECASTERTEL"; //潮汐预报员

                #endregion

                #region 书签赋值
                //赋值数据到书签的位置
                doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;

                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
                doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送

                //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
                //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
                doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
                doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;

                doc.Bookmarks.get_Item(ref BookMark[132]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
                doc.Bookmarks.get_Item(ref BookMark[133]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
                //doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;

                if (FORECASTDATEFL1 != null && FORECASTDATEFL1 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = this.transDateToString(FORECASTDATEFL1);
                }
                if (FORECASTDATEFL2 != null && FORECASTDATEFL2 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = this.transDateToString(FORECASTDATEFL2);
                }
                if (FORECASTDATEFL3 != null && FORECASTDATEFL3 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = this.transDateToString(FORECASTDATEFL3);
                }
                if (FORECASTDATEFL4 != null && FORECASTDATEFL4 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = this.transDateToString(FORECASTDATEFL4);
                }
                if (FORECASTDATEFL5 != null && FORECASTDATEFL5 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = this.transDateToString(FORECASTDATEFL5);
                }
                if (FORECASTDATEFL6 != null && FORECASTDATEFL6 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = this.transDateToString(FORECASTDATEFL6);
                }
                if (FORECASTDATEFL7 != null && FORECASTDATEFL7 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = this.transDateToString(FORECASTDATEFL7);
                }
                if (FORECASTDATEFL8 != null && FORECASTDATEFL8 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = this.transDateToString(FORECASTDATEFL8);
                }
                if (FORECASTDATEFL9 != null && FORECASTDATEFL9 != "")
                {
                    doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = this.transDateToString(FORECASTDATEFL9);
                }

                doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = YRBHWWFWAVEHEIGHT1;
                doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = YRBHWWFWAVEHEIGHT2;
                doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = YRBHWWFWAVEHEIGHT3;
                doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = YRBHWWFWAVEHEIGHT4;
                doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = YRBHWWFWAVEHEIGHT5;
                doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = YRBHWWFWAVEHEIGHT6;
                doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = YRBHWWFWAVEHEIGHT7;
                doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = YRBHWWFWAVEHEIGHT8;
                doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = YRBHWWFWAVEHEIGHT9;

                doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = YRBHWWFWAVEDIR1;
                doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = YRBHWWFWAVEDIR2;
                doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = YRBHWWFWAVEDIR3;
                doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = YRBHWWFWAVEDIR4;
                doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = YRBHWWFWAVEDIR5;
                doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = YRBHWWFWAVEDIR6;
                doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = YRBHWWFWAVEDIR7;
                doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = YRBHWWFWAVEDIR8;
                doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = YRBHWWFWAVEDIR9;

                doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = YRBHWWFFLOWDIR1;
                doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = YRBHWWFFLOWDIR2;
                doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = YRBHWWFFLOWDIR3;
                doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = YRBHWWFFLOWDIR4;
                doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = YRBHWWFFLOWDIR5;
                doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = YRBHWWFFLOWDIR6;
                doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = YRBHWWFFLOWDIR7;
                doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = YRBHWWFFLOWDIR8;
                doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = YRBHWWFFLOWDIR9;

                doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = YRBHWWFFLOWLEVEL1;
                doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = YRBHWWFFLOWLEVEL2;
                doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = YRBHWWFFLOWLEVEL3;
                doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = YRBHWWFFLOWLEVEL4;
                doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = YRBHWWFFLOWLEVEL5;
                doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = YRBHWWFFLOWLEVEL6;
                doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = YRBHWWFFLOWLEVEL7;
                doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = YRBHWWFFLOWLEVEL8;
                doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = YRBHWWFFLOWLEVEL9;

                doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = this.transDateToString(FORECASTDATE1);
                doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = this.transDateToString(FORECASTDATE2);
                doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = this.transDateToString(FORECASTDATE3);
                doc.Bookmarks.get_Item(ref BookMark[54]).Range.Text = this.transDateToString(FORECASTDATE4);
                doc.Bookmarks.get_Item(ref BookMark[55]).Range.Text = this.transDateToString(FORECASTDATE5);
                doc.Bookmarks.get_Item(ref BookMark[56]).Range.Text = this.transDateToString(FORECASTDATE6);
                doc.Bookmarks.get_Item(ref BookMark[57]).Range.Text = this.transDateToString(FORECASTDATE7);
                doc.Bookmarks.get_Item(ref BookMark[58]).Range.Text = this.transDateToString(FORECASTDATE8);
                doc.Bookmarks.get_Item(ref BookMark[59]).Range.Text = this.transDateToString(FORECASTDATE9);


                doc.Bookmarks.get_Item(ref BookMark[60]).Range.Text = HTLFIRSTWAVEOFTIME1;
                doc.Bookmarks.get_Item(ref BookMark[61]).Range.Text = HTLFIRSTWAVEOFTIME2;
                doc.Bookmarks.get_Item(ref BookMark[62]).Range.Text = HTLFIRSTWAVEOFTIME3;
                doc.Bookmarks.get_Item(ref BookMark[63]).Range.Text = HTLFIRSTWAVEOFTIME4;
                doc.Bookmarks.get_Item(ref BookMark[64]).Range.Text = HTLFIRSTWAVEOFTIME5;
                doc.Bookmarks.get_Item(ref BookMark[65]).Range.Text = HTLFIRSTWAVEOFTIME6;
                doc.Bookmarks.get_Item(ref BookMark[66]).Range.Text = HTLFIRSTWAVEOFTIME7;
                doc.Bookmarks.get_Item(ref BookMark[67]).Range.Text = HTLFIRSTWAVEOFTIME8;
                doc.Bookmarks.get_Item(ref BookMark[68]).Range.Text = HTLFIRSTWAVEOFTIME9;

                doc.Bookmarks.get_Item(ref BookMark[69]).Range.Text = HTLFIRSTWAVETIDELEVEL1;
                doc.Bookmarks.get_Item(ref BookMark[70]).Range.Text = HTLFIRSTWAVETIDELEVEL2;
                doc.Bookmarks.get_Item(ref BookMark[71]).Range.Text = HTLFIRSTWAVETIDELEVEL3;
                doc.Bookmarks.get_Item(ref BookMark[72]).Range.Text = HTLFIRSTWAVETIDELEVEL4;
                doc.Bookmarks.get_Item(ref BookMark[73]).Range.Text = HTLFIRSTWAVETIDELEVEL5;
                doc.Bookmarks.get_Item(ref BookMark[74]).Range.Text = HTLFIRSTWAVETIDELEVEL6;
                doc.Bookmarks.get_Item(ref BookMark[75]).Range.Text = HTLFIRSTWAVETIDELEVEL7;
                doc.Bookmarks.get_Item(ref BookMark[76]).Range.Text = HTLFIRSTWAVETIDELEVEL8;
                doc.Bookmarks.get_Item(ref BookMark[77]).Range.Text = HTLFIRSTWAVETIDELEVEL9;

                doc.Bookmarks.get_Item(ref BookMark[78]).Range.Text = HTLFIRSTTIMELOWTIDE1;
                doc.Bookmarks.get_Item(ref BookMark[79]).Range.Text = HTLFIRSTTIMELOWTIDE2;
                doc.Bookmarks.get_Item(ref BookMark[80]).Range.Text = HTLFIRSTTIMELOWTIDE3;
                doc.Bookmarks.get_Item(ref BookMark[81]).Range.Text = HTLFIRSTTIMELOWTIDE4;
                doc.Bookmarks.get_Item(ref BookMark[82]).Range.Text = HTLFIRSTTIMELOWTIDE5;
                doc.Bookmarks.get_Item(ref BookMark[83]).Range.Text = HTLFIRSTTIMELOWTIDE6;
                doc.Bookmarks.get_Item(ref BookMark[84]).Range.Text = HTLFIRSTTIMELOWTIDE7;
                doc.Bookmarks.get_Item(ref BookMark[85]).Range.Text = HTLFIRSTTIMELOWTIDE8;
                doc.Bookmarks.get_Item(ref BookMark[86]).Range.Text = HTLFIRSTTIMELOWTIDE9;

                doc.Bookmarks.get_Item(ref BookMark[87]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME1;
                doc.Bookmarks.get_Item(ref BookMark[88]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME2;
                doc.Bookmarks.get_Item(ref BookMark[89]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME3;
                doc.Bookmarks.get_Item(ref BookMark[90]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME4;
                doc.Bookmarks.get_Item(ref BookMark[91]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME5;
                doc.Bookmarks.get_Item(ref BookMark[92]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME6;
                doc.Bookmarks.get_Item(ref BookMark[93]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME7;
                doc.Bookmarks.get_Item(ref BookMark[94]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME8;
                doc.Bookmarks.get_Item(ref BookMark[95]).Range.Text = HTLLOWTIDELEVELFORTHEFIRSTTIME9;

                doc.Bookmarks.get_Item(ref BookMark[96]).Range.Text = HTLSECONDWAVEOFTIME1;
                doc.Bookmarks.get_Item(ref BookMark[97]).Range.Text = HTLSECONDWAVEOFTIME2;
                doc.Bookmarks.get_Item(ref BookMark[98]).Range.Text = HTLSECONDWAVEOFTIME3;
                doc.Bookmarks.get_Item(ref BookMark[99]).Range.Text = HTLSECONDWAVEOFTIME4;
                doc.Bookmarks.get_Item(ref BookMark[100]).Range.Text = HTLSECONDWAVEOFTIME5;
                doc.Bookmarks.get_Item(ref BookMark[101]).Range.Text = HTLSECONDWAVEOFTIME6;
                doc.Bookmarks.get_Item(ref BookMark[102]).Range.Text = HTLSECONDWAVEOFTIME7;
                doc.Bookmarks.get_Item(ref BookMark[103]).Range.Text = HTLSECONDWAVEOFTIME8;
                doc.Bookmarks.get_Item(ref BookMark[104]).Range.Text = HTLSECONDWAVEOFTIME9;

                doc.Bookmarks.get_Item(ref BookMark[105]).Range.Text = HTLSECONDWAVETIDELEVEL1;
                doc.Bookmarks.get_Item(ref BookMark[106]).Range.Text = HTLSECONDWAVETIDELEVEL2;
                doc.Bookmarks.get_Item(ref BookMark[107]).Range.Text = HTLSECONDWAVETIDELEVEL3;
                doc.Bookmarks.get_Item(ref BookMark[108]).Range.Text = HTLSECONDWAVETIDELEVEL4;
                doc.Bookmarks.get_Item(ref BookMark[109]).Range.Text = HTLSECONDWAVETIDELEVEL5;
                doc.Bookmarks.get_Item(ref BookMark[110]).Range.Text = HTLSECONDWAVETIDELEVEL6;
                doc.Bookmarks.get_Item(ref BookMark[111]).Range.Text = HTLSECONDWAVETIDELEVEL7;
                doc.Bookmarks.get_Item(ref BookMark[112]).Range.Text = HTLSECONDWAVETIDELEVEL8;
                doc.Bookmarks.get_Item(ref BookMark[113]).Range.Text = HTLSECONDWAVETIDELEVEL9;

                doc.Bookmarks.get_Item(ref BookMark[114]).Range.Text = HTLSECONDTIMELOWTIDE1;
                doc.Bookmarks.get_Item(ref BookMark[115]).Range.Text = HTLSECONDTIMELOWTIDE2;
                doc.Bookmarks.get_Item(ref BookMark[116]).Range.Text = HTLSECONDTIMELOWTIDE3;
                doc.Bookmarks.get_Item(ref BookMark[117]).Range.Text = HTLSECONDTIMELOWTIDE4;
                doc.Bookmarks.get_Item(ref BookMark[118]).Range.Text = HTLSECONDTIMELOWTIDE5;
                doc.Bookmarks.get_Item(ref BookMark[119]).Range.Text = HTLSECONDTIMELOWTIDE6;
                doc.Bookmarks.get_Item(ref BookMark[120]).Range.Text = HTLSECONDTIMELOWTIDE7;
                doc.Bookmarks.get_Item(ref BookMark[121]).Range.Text = HTLSECONDTIMELOWTIDE8;
                doc.Bookmarks.get_Item(ref BookMark[122]).Range.Text = HTLSECONDTIMELOWTIDE9;

                doc.Bookmarks.get_Item(ref BookMark[123]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM1;
                doc.Bookmarks.get_Item(ref BookMark[124]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM2;
                doc.Bookmarks.get_Item(ref BookMark[125]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM3;
                doc.Bookmarks.get_Item(ref BookMark[126]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM4;
                doc.Bookmarks.get_Item(ref BookMark[127]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM5;
                doc.Bookmarks.get_Item(ref BookMark[128]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM6;
                doc.Bookmarks.get_Item(ref BookMark[129]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM7;
                doc.Bookmarks.get_Item(ref BookMark[130]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM8;
                doc.Bookmarks.get_Item(ref BookMark[131]).Range.Text = HTLLOWTIDELEVELFORTHESECONDTIM9;

#endregion
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
       
        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="dateTime">格式化时间参数</param>
        /// <returns></returns>
        public string transDateToString(string dateTime)
        {
            DateTime dt = Convert.ToDateTime(dateTime);
            var month = dt.Month;
            var day = dt.Day;
            var result = month + "月" + day + "日";
            return result;
        }
    }
}