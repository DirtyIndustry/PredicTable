using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;


public class OneWord
{
    public OneWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    string PUBLISHTIME = "";

    string SDOSCWLOWESTWAVEHEIGHT1 = "";
    string SDOSCWLOWESTWAVEHEIGHT2 = "";
    string SDOSCWLOWESTWAVEHEIGHT3 = "";
    string SDOSCWLOWESTWAVEHEIGHT4 = "";
    string SDOSCWLOWESTWAVEHEIGHT5 = "";
    string SDOSCWLOWESTWAVEHEIGHT6 = "";
    string SDOSCWLOWESTWAVEHEIGHT7 = "";
    string SDOSCWSURFACETEMPERATURE1 = "";
    string SDOSCWSURFACETEMPERATURE2 = "";
    string SDOSCWSURFACETEMPERATURE3 = "";
    string SDOSCWSURFACETEMPERATURE4 = "";
    string SDOSCWSURFACETEMPERATURE5 = "";
    string SDOSCWSURFACETEMPERATURE6 = "";
    string SDOSCWSURFACETEMPERATURE7 = "";

    string ESASWLOWESTWAVEHEIGHT1 = "";
    string ESASWLOWESTWAVEHEIGHT2 = "";
    string ESASWLOWESTWAVEHEIGHT3 = "";
    string ESASWLOWESTWAVEHEIGHT4 = "";

    string ESASWHIGHTESTWAVEHEIGHT1 = "";
    string ESASWHIGHTESTWAVEHEIGHT2 = "";
    string ESASWHIGHTESTWAVEHEIGHT3 = "";
    string ESASWHIGHTESTWAVEHEIGHT4 = "";

    string ESASWWAVETYPE1 = "";
    string ESASWWAVETYPE2 = "";
    string ESASWWAVETYPE3 = "";
    string ESASWWAVETYPE4 = "";

    string SDOSCTFIRSTHIGHWAVEHOUR1 = "";
    string SDOSCTFIRSTHIGHWAVEHOUR2 = "";
    string SDOSCTFIRSTHIGHWAVEHOUR3 = "";
    string SDOSCTFIRSTHIGHWAVEHOUR4 = "";
    string SDOSCTFIRSTHIGHWAVEHOUR5 = "";
    string SDOSCTFIRSTHIGHWAVEHOUR6 = "";
    string SDOSCTFIRSTHIGHWAVEHOUR7 = "";
    string SDOSCTFIRSTHIGHWAVEMINUTE1 = "";
    string SDOSCTFIRSTHIGHWAVEMINUTE2 = "";
    string SDOSCTFIRSTHIGHWAVEMINUTE3 = "";
    string SDOSCTFIRSTHIGHWAVEMINUTE4 = "";
    string SDOSCTFIRSTHIGHWAVEMINUTE5 = "";
    string SDOSCTFIRSTHIGHWAVEMINUTE6 = "";
    string SDOSCTFIRSTHIGHWAVEMINUTE7 = "";

    string SDOSCTSECONDHIGHWAVEHOUR1 = "";
    string SDOSCTSECONDHIGHWAVEHOUR2 = "";
    string SDOSCTSECONDHIGHWAVEHOUR3 = "";
    string SDOSCTSECONDHIGHWAVEHOUR4 = "";
    string SDOSCTSECONDHIGHWAVEHOUR5 = "";
    string SDOSCTSECONDHIGHWAVEHOUR6 = "";
    string SDOSCTSECONDHIGHWAVEHOUR7 = "";
    string SDOSCTSECONDHIGHWAVEMINUTE1 = "";
    string SDOSCTSECONDHIGHWAVEMINUTE2 = "";
    string SDOSCTSECONDHIGHWAVEMINUTE3 = "";
    string SDOSCTSECONDHIGHWAVEMINUTE4 = "";
    string SDOSCTSECONDHIGHWAVEMINUTE5 = "";
    string SDOSCTSECONDHIGHWAVEMINUTE6 = "";
    string SDOSCTSECONDHIGHWAVEMINUTE7 = "";

    string SDOSCTFIRSTLOWWAVEHOUR1 = "";
    string SDOSCTFIRSTLOWWAVEHOUR2 = "";
    string SDOSCTFIRSTLOWWAVEHOUR3 = "";
    string SDOSCTFIRSTLOWWAVEHOUR4 = "";
    string SDOSCTFIRSTLOWWAVEHOUR5 = "";
    string SDOSCTFIRSTLOWWAVEHOUR6 = "";
    string SDOSCTFIRSTLOWWAVEHOUR7 = "";
    string SDOSCTFIRSTLOWWAVEMINUTE1 = "";
    string SDOSCTFIRSTLOWWAVEMINUTE2 = "";
    string SDOSCTFIRSTLOWWAVEMINUTE3 = "";
    string SDOSCTFIRSTLOWWAVEMINUTE4 = "";
    string SDOSCTFIRSTLOWWAVEMINUTE5 = "";
    string SDOSCTFIRSTLOWWAVEMINUTE6 = "";
    string SDOSCTFIRSTLOWWAVEMINUTE7 = "";

    string SDOSCTSECONDLOWWAVEHOUR1 = "";
    string SDOSCTSECONDLOWWAVEHOUR2 = "";
    string SDOSCTSECONDLOWWAVEHOUR3 = "";
    string SDOSCTSECONDLOWWAVEHOUR4 = "";
    string SDOSCTSECONDLOWWAVEHOUR5 = "";
    string SDOSCTSECONDLOWWAVEHOUR6 = "";
    string SDOSCTSECONDLOWWAVEHOUR7 = "";
    string SDOSCTSECONDLOWWAVEMINUTE1 = "";
    string SDOSCTSECONDLOWWAVEMINUTE2 = "";
    string SDOSCTSECONDLOWWAVEMINUTE3 = "";
    string SDOSCTSECONDLOWWAVEMINUTE4 = "";
    string SDOSCTSECONDLOWWAVEMINUTE5 = "";
    string SDOSCTSECONDLOWWAVEMINUTE6 = "";
    string SDOSCTSECONDLOWWAVEMINUTE7 = "";

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
            if (tblfooter.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblfooter.Rows.Count; i++)
                {
                    string FRELEASEUNIT = "山东省海洋预报台"; //tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
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
            }
            //黄河南海堤表提取数据
            TBLSDOFFSHORESEVENCITY24HWAVE tblsdoffshoresevencity24hwave_Model = new TBLSDOFFSHORESEVENCITY24HWAVE();
            tblsdoffshoresevencity24hwave_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblsdoffshoresevencity24hwave = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HWAVE().get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(tblsdoffshoresevencity24hwave_Model);
            if (tblsdoffshoresevencity24hwave.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblsdoffshoresevencity24hwave.Rows.Count; i++)
                {
                    if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "日照近海")
                    {
                        SDOSCWLOWESTWAVEHEIGHT1 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        SDOSCWSURFACETEMPERATURE1 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "青岛近海")
                    {
                        SDOSCWLOWESTWAVEHEIGHT2 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        SDOSCWSURFACETEMPERATURE2 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "威海近海")
                    {
                        SDOSCWLOWESTWAVEHEIGHT3 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        SDOSCWSURFACETEMPERATURE3 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "烟台近海")
                    {
                        SDOSCWLOWESTWAVEHEIGHT4 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        SDOSCWSURFACETEMPERATURE4 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "潍坊近海")
                    {
                        SDOSCWLOWESTWAVEHEIGHT5 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        SDOSCWSURFACETEMPERATURE5 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "东营近海")
                    {
                        SDOSCWLOWESTWAVEHEIGHT6 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        SDOSCWSURFACETEMPERATURE6 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "滨州近海")
                    {
                        SDOSCWLOWESTWAVEHEIGHT7 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        SDOSCWSURFACETEMPERATURE7 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }

                }


            }



            TBLSDOFFSHORESEVENCITY24HTIDE tblsdoffshoresevencity24htide_Model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            tblsdoffshoresevencity24htide_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblsdoffshoresevencity24htide = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HTIDE().get24TideData(tblsdoffshoresevencity24htide_Model);
            if (tblsdoffshoresevencity24htide.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblsdoffshoresevencity24htide.Rows.Count; i++)
                {
                    if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "日照")
                    {
                        SDOSCTFIRSTHIGHWAVEHOUR1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                        SDOSCTFIRSTHIGHWAVEMINUTE1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        SDOSCTSECONDHIGHWAVEHOUR1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                        SDOSCTSECONDHIGHWAVEMINUTE1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        SDOSCTFIRSTLOWWAVEHOUR1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                        SDOSCTFIRSTLOWWAVEMINUTE1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        SDOSCTSECONDLOWWAVEHOUR1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                        SDOSCTSECONDLOWWAVEMINUTE1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();

                    }
                    else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                    {
                        SDOSCTFIRSTHIGHWAVEHOUR2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                        SDOSCTFIRSTHIGHWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        SDOSCTSECONDHIGHWAVEHOUR2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                        SDOSCTSECONDHIGHWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        SDOSCTFIRSTLOWWAVEHOUR2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                        SDOSCTFIRSTLOWWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        SDOSCTSECONDLOWWAVEHOUR2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                        SDOSCTSECONDLOWWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "威海")
                    {
                        SDOSCTFIRSTHIGHWAVEHOUR3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                        SDOSCTFIRSTHIGHWAVEMINUTE3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        SDOSCTSECONDHIGHWAVEHOUR3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                        SDOSCTSECONDHIGHWAVEMINUTE3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        SDOSCTFIRSTLOWWAVEHOUR3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                        SDOSCTFIRSTLOWWAVEMINUTE3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        SDOSCTSECONDLOWWAVEHOUR3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                        SDOSCTSECONDLOWWAVEMINUTE3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "烟台")
                    {
                        SDOSCTFIRSTHIGHWAVEHOUR4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                        SDOSCTFIRSTHIGHWAVEMINUTE4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        SDOSCTSECONDHIGHWAVEHOUR4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                        SDOSCTSECONDHIGHWAVEMINUTE4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        SDOSCTFIRSTLOWWAVEHOUR4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                        SDOSCTFIRSTLOWWAVEMINUTE4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        SDOSCTSECONDLOWWAVEHOUR4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                        SDOSCTSECONDLOWWAVEMINUTE4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "潍坊")
                    {
                        SDOSCTFIRSTHIGHWAVEHOUR5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                        SDOSCTFIRSTHIGHWAVEMINUTE5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        SDOSCTSECONDHIGHWAVEHOUR5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                        SDOSCTSECONDHIGHWAVEMINUTE5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        SDOSCTFIRSTLOWWAVEHOUR5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                        SDOSCTFIRSTLOWWAVEMINUTE5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        SDOSCTSECONDLOWWAVEHOUR5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                        SDOSCTSECONDLOWWAVEMINUTE5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "东营")
                    {
                        SDOSCTFIRSTHIGHWAVEHOUR6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                        SDOSCTFIRSTHIGHWAVEMINUTE6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        SDOSCTSECONDHIGHWAVEHOUR6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                        SDOSCTSECONDHIGHWAVEMINUTE6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        SDOSCTFIRSTLOWWAVEHOUR6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                        SDOSCTFIRSTLOWWAVEMINUTE6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        SDOSCTSECONDLOWWAVEHOUR6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                        SDOSCTSECONDLOWWAVEMINUTE6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "滨州")
                    {
                        SDOSCTFIRSTHIGHWAVEHOUR7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                        SDOSCTFIRSTHIGHWAVEMINUTE7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        SDOSCTSECONDHIGHWAVEHOUR7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                        SDOSCTSECONDHIGHWAVEMINUTE7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        SDOSCTFIRSTLOWWAVEHOUR7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                        SDOSCTFIRSTLOWWAVEMINUTE7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        SDOSCTSECONDLOWWAVEHOUR7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                        SDOSCTSECONDLOWWAVEMINUTE7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                    }

                }


            }


            //各海区24小时海浪预报
            TBLEACHSEAAREA24HSEAWAVE tbleachseaarea24hseawave_Model = new TBLEACHSEAAREA24HSEAWAVE();
            tbleachseaarea24hseawave_Model.PUBLISHDATE = dt;
            System.Data.DataTable tbleachseaarea24hseawave = (System.Data.DataTable)new sql_TBLEACHSEAAREA24HSEAWAVE().get_TBLEACHSEAAREA24HSEAWAVE_AllData(tbleachseaarea24hseawave_Model);
            if (tbleachseaarea24hseawave.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tbleachseaarea24hseawave.Rows.Count; i++)
                {
                    if (tbleachseaarea24hseawave.Rows[i]["ESASWAREA"].ToString() == "渤海")
                    {
                        ESASWLOWESTWAVEHEIGHT1 = tbleachseaarea24hseawave.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();//最低浪高
                        ESASWHIGHTESTWAVEHEIGHT1 = tbleachseaarea24hseawave.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();//最高浪高
                        ESASWWAVETYPE1 = tbleachseaarea24hseawave.Rows[i]["ESASWWAVETYPE"].ToString();//浪高类型

                    }
                    else if (tbleachseaarea24hseawave.Rows[i]["ESASWAREA"].ToString() == "黄海北部")
                    {
                        ESASWLOWESTWAVEHEIGHT2 = tbleachseaarea24hseawave.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                        ESASWHIGHTESTWAVEHEIGHT2 = tbleachseaarea24hseawave.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                        ESASWWAVETYPE2 = tbleachseaarea24hseawave.Rows[i]["ESASWWAVETYPE"].ToString();//浪高类型

                    }
                    else if (tbleachseaarea24hseawave.Rows[i]["ESASWAREA"].ToString() == "黄海中部")
                    {
                        ESASWLOWESTWAVEHEIGHT3 = tbleachseaarea24hseawave.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                        ESASWHIGHTESTWAVEHEIGHT3 = tbleachseaarea24hseawave.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                        ESASWWAVETYPE3 = tbleachseaarea24hseawave.Rows[i]["ESASWWAVETYPE"].ToString();//浪高类型
                    }
                    else if (tbleachseaarea24hseawave.Rows[i]["ESASWAREA"].ToString() == "黄海南部")
                    {
                        ESASWLOWESTWAVEHEIGHT4 = tbleachseaarea24hseawave.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                        ESASWHIGHTESTWAVEHEIGHT4 = tbleachseaarea24hseawave.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                        ESASWWAVETYPE4 = tbleachseaarea24hseawave.Rows[i]["ESASWWAVETYPE"].ToString();//浪高类型
                    }

                }


            }
            //潮汐数据潮高
            //第一次高潮潮高
            var FIRSTHIGHWAVETIDEDATA1 = "";
            var FIRSTHIGHWAVETIDEDATA2 = "";
            var FIRSTHIGHWAVETIDEDATA3 = "";
            var FIRSTHIGHWAVETIDEDATA4 = "";
            var FIRSTHIGHWAVETIDEDATA5 = "";
            var FIRSTHIGHWAVETIDEDATA6 = "";
            var FIRSTHIGHWAVETIDEDATA7 = "";
            //第一次低潮潮高
            var FIRSTLOWWAVETIDEDATA1 = "";
            var FIRSTLOWWAVETIDEDATA2 = "";
            var FIRSTLOWWAVETIDEDATA3 = "";
            var FIRSTLOWWAVETIDEDATA4 = "";
            var FIRSTLOWWAVETIDEDATA5 = "";
            var FIRSTLOWWAVETIDEDATA6 = "";
            var FIRSTLOWWAVETIDEDATA7 = "";
            //第二次高潮潮高
            var SECONDHIGHWAVETIDEDATA1 = "";
            var SECONDHIGHWAVETIDEDATA2 = "";
            var SECONDHIGHWAVETIDEDATA3 = "";
            var SECONDHIGHWAVETIDEDATA4 = "";
            var SECONDHIGHWAVETIDEDATA5 = "";
            var SECONDHIGHWAVETIDEDATA6 = "";
            var SECONDHIGHWAVETIDEDATA7 = "";
            //第二次低潮潮高
            var SECONDLOWWAVETIDEDATA1 = "";
            var SECONDLOWWAVETIDEDATA2 = "";
            var SECONDLOWWAVETIDEDATA3 = "";
            var SECONDLOWWAVETIDEDATA4 = "";
            var SECONDLOWWAVETIDEDATA5 = "";
            var SECONDLOWWAVETIDEDATA6 = "";
            var SECONDLOWWAVETIDEDATA7 = "";


            sql_TideData sql = new sql_TideData();
            HT_TideData model = new HT_TideData();
            model.PUBLISHDATE = dt;
            DataTable tideData = (DataTable)sql.get24TideData(model);
            if (tideData != null && tideData.Rows.Count > 0){
                for (int i = 0; i < tideData.Rows.Count; i++)
                {
                    if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "日照")
                    {
                        FIRSTHIGHWAVETIDEDATA1 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                        FIRSTLOWWAVETIDEDATA1 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                        SECONDHIGHWAVETIDEDATA1 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                        SECONDLOWWAVETIDEDATA1 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                    }
                    else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "青岛") {

                        FIRSTHIGHWAVETIDEDATA2 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                        FIRSTLOWWAVETIDEDATA2 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                        SECONDHIGHWAVETIDEDATA2 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                        SECONDLOWWAVETIDEDATA2 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                    }
                    else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "威海") {
                        FIRSTHIGHWAVETIDEDATA3 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                        FIRSTLOWWAVETIDEDATA3 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                        SECONDHIGHWAVETIDEDATA3 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                        SECONDLOWWAVETIDEDATA3 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                    }
                    else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "烟台") {
                        FIRSTHIGHWAVETIDEDATA4 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                        FIRSTLOWWAVETIDEDATA4 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                        SECONDHIGHWAVETIDEDATA4 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                        SECONDLOWWAVETIDEDATA4 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                    }
                    else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "潍坊") {
                        FIRSTHIGHWAVETIDEDATA5 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                        FIRSTLOWWAVETIDEDATA5 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                        SECONDHIGHWAVETIDEDATA5 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                        SECONDLOWWAVETIDEDATA5 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                    }
                    else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "东营") {
                        FIRSTHIGHWAVETIDEDATA6 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                        FIRSTLOWWAVETIDEDATA6 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                        SECONDHIGHWAVETIDEDATA6 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                        SECONDLOWWAVETIDEDATA6 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                    }
                    else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "滨州") {
                        FIRSTHIGHWAVETIDEDATA7 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                        FIRSTLOWWAVETIDEDATA7 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                        SECONDHIGHWAVETIDEDATA7 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                        SECONDLOWWAVETIDEDATA7 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                    }
                }
            }
            //PUBLISHTIME = dt.ToLongDateString() + hourStr + "时";

            //为了方便管理声明书签数组
            object[] BookMark = new object[120]; //新增3个预报员电话117改为120
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真
            BookMark[3] = "FWAVEFORECASTER";//海浪预报员
            BookMark[4] = "FTIDALFORECASTER";//海浪预报员
            BookMark[5] = "FWATERTEMPERATUREFORECASTER";//水温预报员

            BookMark[6] = "ESASWLOWESTWAVEHEIGHT1";
            BookMark[7] = "ESASWLOWESTWAVEHEIGHT2";
            BookMark[8] = "ESASWLOWESTWAVEHEIGHT3";
            BookMark[9] = "ESASWLOWESTWAVEHEIGHT4";

         

            BookMark[10] = "SDOSCWLOWESTWAVEHEIGHT1";
            BookMark[11] = "SDOSCWLOWESTWAVEHEIGHT2";
            BookMark[12] = "SDOSCWLOWESTWAVEHEIGHT3";
            BookMark[13] = "SDOSCWLOWESTWAVEHEIGHT4";
            BookMark[14] = "SDOSCWLOWESTWAVEHEIGHT5";
            BookMark[15] = "SDOSCWLOWESTWAVEHEIGHT6";
            BookMark[16] = "SDOSCWLOWESTWAVEHEIGHT7";
            BookMark[17] = "SDOSCWSURFACETEMPERATURE1";
            BookMark[18] = "SDOSCWSURFACETEMPERATURE2";
            BookMark[19] = "SDOSCWSURFACETEMPERATURE3";
            BookMark[20] = "SDOSCWSURFACETEMPERATURE4";
            BookMark[21] = "SDOSCWSURFACETEMPERATURE5";
            BookMark[22] = "SDOSCWSURFACETEMPERATURE6";
            BookMark[23] = "SDOSCWSURFACETEMPERATURE7";

            BookMark[24] = "SDOSCTFIRSTHIGHWAVEHOUR1";
            BookMark[25] = "SDOSCTFIRSTHIGHWAVEHOUR2";
            BookMark[26] = "SDOSCTFIRSTHIGHWAVEHOUR3";
            BookMark[27] = "SDOSCTFIRSTHIGHWAVEHOUR4";
            BookMark[28] = "SDOSCTFIRSTHIGHWAVEHOUR5";
            BookMark[29] = "SDOSCTFIRSTHIGHWAVEHOUR6";
            BookMark[30] = "SDOSCTFIRSTHIGHWAVEHOUR7";
            BookMark[31] = "SDOSCTFIRSTHIGHWAVEMINUTE1";
            BookMark[32] = "SDOSCTFIRSTHIGHWAVEMINUTE2";
            BookMark[33] = "SDOSCTFIRSTHIGHWAVEMINUTE3";
            BookMark[34] = "SDOSCTFIRSTHIGHWAVEMINUTE4";
            BookMark[35] = "SDOSCTFIRSTHIGHWAVEMINUTE5";
            BookMark[36] = "SDOSCTFIRSTHIGHWAVEMINUTE6";
            BookMark[37] = "SDOSCTFIRSTHIGHWAVEMINUTE7";

            BookMark[38] = "SDOSCTSECONDHIGHWAVEHOUR1";
            BookMark[39] = "SDOSCTSECONDHIGHWAVEHOUR2";
            BookMark[40] = "SDOSCTSECONDHIGHWAVEHOUR3";
            BookMark[41] = "SDOSCTSECONDHIGHWAVEHOUR4";
            BookMark[42] = "SDOSCTSECONDHIGHWAVEHOUR5";
            BookMark[43] = "SDOSCTSECONDHIGHWAVEHOUR6";
            BookMark[44] = "SDOSCTSECONDHIGHWAVEHOUR7";
            BookMark[45] = "SDOSCTSECONDHIGHWAVEMINUTE1";
            BookMark[46] = "SDOSCTSECONDHIGHWAVEMINUTE2";
            BookMark[47] = "SDOSCTSECONDHIGHWAVEMINUTE3";
            BookMark[48] = "SDOSCTSECONDHIGHWAVEMINUTE4";
            BookMark[49] = "SDOSCTSECONDHIGHWAVEMINUTE5";
            BookMark[50] = "SDOSCTSECONDHIGHWAVEMINUTE6";
            BookMark[51] = "SDOSCTSECONDHIGHWAVEMINUTE7";
        
            BookMark[52] = "SDOSCTFIRSTLOWWAVEHOUR1";
            BookMark[53] = "SDOSCTFIRSTLOWWAVEHOUR2";
            BookMark[54] = "SDOSCTFIRSTLOWWAVEHOUR3";
            BookMark[55] = "SDOSCTFIRSTLOWWAVEHOUR4";
            BookMark[56] = "SDOSCTFIRSTLOWWAVEHOUR5";
            BookMark[57] = "SDOSCTFIRSTLOWWAVEHOUR6";
            BookMark[58] = "SDOSCTFIRSTLOWWAVEHOUR7";
            BookMark[59] = "SDOSCTFIRSTLOWWAVEMINUTE1";
            BookMark[60] = "SDOSCTFIRSTLOWWAVEMINUTE2";
            BookMark[61] = "SDOSCTFIRSTLOWWAVEMINUTE3";
            BookMark[62] = "SDOSCTFIRSTLOWWAVEMINUTE4";
            BookMark[63] = "SDOSCTFIRSTLOWWAVEMINUTE5";
            BookMark[64] = "SDOSCTFIRSTLOWWAVEMINUTE6";
            BookMark[65] = "SDOSCTFIRSTLOWWAVEMINUTE7";

            BookMark[66] = "SDOSCTSECONDLOWWAVEHOUR1";
            BookMark[67] = "SDOSCTSECONDLOWWAVEHOUR2";
            BookMark[68] = "SDOSCTSECONDLOWWAVEHOUR3";
            BookMark[69] = "SDOSCTSECONDLOWWAVEHOUR4";
            BookMark[70] = "SDOSCTSECONDLOWWAVEHOUR5";
            BookMark[71] = "SDOSCTSECONDLOWWAVEHOUR6";
            BookMark[72] = "SDOSCTSECONDLOWWAVEHOUR7";
            BookMark[73] = "SDOSCTSECONDLOWWAVEMINUTE1";
            BookMark[74] = "SDOSCTSECONDLOWWAVEMINUTE2";
            BookMark[75] = "SDOSCTSECONDLOWWAVEMINUTE3";
            BookMark[76] = "SDOSCTSECONDLOWWAVEMINUTE4";
            BookMark[77] = "SDOSCTSECONDLOWWAVEMINUTE5";
            BookMark[78] = "SDOSCTSECONDLOWWAVEMINUTE6";
            BookMark[79] = "SDOSCTSECONDLOWWAVEMINUTE7";

            //最高浪高
            BookMark[80] = "ESASWHIGHTESTWAVEHEIGHT1";
            BookMark[81] = "ESASWHIGHTESTWAVEHEIGHT2";
            BookMark[82] = "ESASWHIGHTESTWAVEHEIGHT3";
            BookMark[83] = "ESASWHIGHTESTWAVEHEIGHT4";

            //浪级
            BookMark[84] = "ESASWWAVETYPE1";
            BookMark[85] = "ESASWWAVETYPE2";
            BookMark[86] = "ESASWWAVETYPE3";
            BookMark[87] = "ESASWWAVETYPE4";

            BookMark[88] = "PUBLISHTIME";

            BookMark[89] = "FIRSTHIGHWAVETIDEDATA1";
            BookMark[90] = "FIRSTHIGHWAVETIDEDATA2";
            BookMark[91] = "FIRSTHIGHWAVETIDEDATA3";
            BookMark[92] = "FIRSTHIGHWAVETIDEDATA4";
            BookMark[93] = "FIRSTHIGHWAVETIDEDATA5";
            BookMark[94] = "FIRSTHIGHWAVETIDEDATA6";
            BookMark[95] = "FIRSTHIGHWAVETIDEDATA7";

            BookMark[96] = "FIRSTLOWWAVETIDEDATA1";
            BookMark[97] = "FIRSTLOWWAVETIDEDATA2";
            BookMark[98] = "FIRSTLOWWAVETIDEDATA3";
            BookMark[99] = "FIRSTLOWWAVETIDEDATA4";
            BookMark[100] = "FIRSTLOWWAVETIDEDATA5";
            BookMark[101] = "FIRSTLOWWAVETIDEDATA6";
            BookMark[102] = "FIRSTLOWWAVETIDEDATA7";

            BookMark[103] = "SECONDHIGHWAVETIDEDATA1";
            BookMark[104] = "SECONDHIGHWAVETIDEDATA2";
            BookMark[105] = "SECONDHIGHWAVETIDEDATA3";
            BookMark[106] = "SECONDHIGHWAVETIDEDATA4";
            BookMark[107] = "SECONDHIGHWAVETIDEDATA5";
            BookMark[108] = "SECONDHIGHWAVETIDEDATA6";
            BookMark[109] = "SECONDHIGHWAVETIDEDATA7";

            BookMark[110] = "SECONDLOWWAVETIDEDATA1";
            BookMark[111] = "SECONDLOWWAVETIDEDATA2";
            BookMark[112] = "SECONDLOWWAVETIDEDATA3";
            BookMark[113] = "SECONDLOWWAVETIDEDATA4";
            BookMark[114] = "SECONDLOWWAVETIDEDATA5";
            BookMark[115] = "SECONDLOWWAVETIDEDATA6";
            BookMark[116] = "SECONDLOWWAVETIDEDATA7";

            BookMark[117] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[118] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[119] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话


            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;

            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = ESASWLOWESTWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = ESASWLOWESTWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = ESASWLOWESTWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = ESASWLOWESTWAVEHEIGHT4;

            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = SDOSCWLOWESTWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = SDOSCWLOWESTWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = SDOSCWLOWESTWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = SDOSCWLOWESTWAVEHEIGHT4;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = SDOSCWLOWESTWAVEHEIGHT5;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = SDOSCWLOWESTWAVEHEIGHT6;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = SDOSCWLOWESTWAVEHEIGHT7;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = SDOSCWSURFACETEMPERATURE1;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = SDOSCWSURFACETEMPERATURE2;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = SDOSCWSURFACETEMPERATURE3;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = SDOSCWSURFACETEMPERATURE4;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = SDOSCWSURFACETEMPERATURE5;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = SDOSCWSURFACETEMPERATURE6;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = SDOSCWSURFACETEMPERATURE7;

            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR1;
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR2;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR3;
            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR4;
            doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR5;
            doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR6;
            doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR7;
            doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE1;
            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE2;
            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE3;
            doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE4;
            doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE5;
            doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE6;
            doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE7;

            doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR1;
            doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR2;
            doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR3;
            doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR4;
            doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR5;
            doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR6;
            doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR7;
            doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE1;
            doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE2;
            doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE3;
            doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE4;
            doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE5;
            doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE6;
            doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE7;

            doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR1;
            doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR2;
            doc.Bookmarks.get_Item(ref BookMark[54]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR3;
            doc.Bookmarks.get_Item(ref BookMark[55]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR4;
            doc.Bookmarks.get_Item(ref BookMark[56]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR5;
            doc.Bookmarks.get_Item(ref BookMark[57]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR6;
            doc.Bookmarks.get_Item(ref BookMark[58]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR7;
            doc.Bookmarks.get_Item(ref BookMark[59]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE1;
            doc.Bookmarks.get_Item(ref BookMark[60]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE2;
            doc.Bookmarks.get_Item(ref BookMark[61]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE3;
            doc.Bookmarks.get_Item(ref BookMark[62]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE4;
            doc.Bookmarks.get_Item(ref BookMark[63]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE5;
            doc.Bookmarks.get_Item(ref BookMark[64]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE6;
            doc.Bookmarks.get_Item(ref BookMark[65]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE7;

            doc.Bookmarks.get_Item(ref BookMark[66]).Range.Text = SDOSCTSECONDLOWWAVEHOUR1;
            doc.Bookmarks.get_Item(ref BookMark[67]).Range.Text = SDOSCTSECONDLOWWAVEHOUR2;
            doc.Bookmarks.get_Item(ref BookMark[68]).Range.Text = SDOSCTSECONDLOWWAVEHOUR3;
            doc.Bookmarks.get_Item(ref BookMark[69]).Range.Text = SDOSCTSECONDLOWWAVEHOUR4;
            doc.Bookmarks.get_Item(ref BookMark[70]).Range.Text = SDOSCTSECONDLOWWAVEHOUR5;
            doc.Bookmarks.get_Item(ref BookMark[71]).Range.Text = SDOSCTSECONDLOWWAVEHOUR6;
            doc.Bookmarks.get_Item(ref BookMark[72]).Range.Text = SDOSCTSECONDLOWWAVEHOUR7;
            doc.Bookmarks.get_Item(ref BookMark[73]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE1;
            doc.Bookmarks.get_Item(ref BookMark[74]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE2;
            doc.Bookmarks.get_Item(ref BookMark[75]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE3;
            doc.Bookmarks.get_Item(ref BookMark[76]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE4;
            doc.Bookmarks.get_Item(ref BookMark[77]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE5;
            doc.Bookmarks.get_Item(ref BookMark[78]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE6;
            doc.Bookmarks.get_Item(ref BookMark[79]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE7;

            doc.Bookmarks.get_Item(ref BookMark[80]).Range.Text = ESASWHIGHTESTWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[81]).Range.Text = ESASWHIGHTESTWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[82]).Range.Text = ESASWHIGHTESTWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[83]).Range.Text = ESASWHIGHTESTWAVEHEIGHT4;

            doc.Bookmarks.get_Item(ref BookMark[84]).Range.Text = ESASWWAVETYPE1;
            doc.Bookmarks.get_Item(ref BookMark[85]).Range.Text = ESASWWAVETYPE2;
            doc.Bookmarks.get_Item(ref BookMark[86]).Range.Text = ESASWWAVETYPE3;
            doc.Bookmarks.get_Item(ref BookMark[87]).Range.Text = ESASWWAVETYPE4;

            doc.Bookmarks.get_Item(ref BookMark[88]).Range.Text = PUBLISHTIME;

            doc.Bookmarks.get_Item(ref BookMark[89]).Range.Text = FIRSTHIGHWAVETIDEDATA1;
            doc.Bookmarks.get_Item(ref BookMark[90]).Range.Text = FIRSTHIGHWAVETIDEDATA2;
            doc.Bookmarks.get_Item(ref BookMark[91]).Range.Text = FIRSTHIGHWAVETIDEDATA3;
            doc.Bookmarks.get_Item(ref BookMark[92]).Range.Text = FIRSTHIGHWAVETIDEDATA4;
            doc.Bookmarks.get_Item(ref BookMark[93]).Range.Text = FIRSTHIGHWAVETIDEDATA5;
            doc.Bookmarks.get_Item(ref BookMark[94]).Range.Text = FIRSTHIGHWAVETIDEDATA6;
            doc.Bookmarks.get_Item(ref BookMark[95]).Range.Text = FIRSTHIGHWAVETIDEDATA7;

            doc.Bookmarks.get_Item(ref BookMark[96]).Range.Text = FIRSTLOWWAVETIDEDATA1;
            doc.Bookmarks.get_Item(ref BookMark[97]).Range.Text = FIRSTLOWWAVETIDEDATA2;
            doc.Bookmarks.get_Item(ref BookMark[98]).Range.Text = FIRSTLOWWAVETIDEDATA3;
            doc.Bookmarks.get_Item(ref BookMark[99]).Range.Text = FIRSTLOWWAVETIDEDATA4;
            doc.Bookmarks.get_Item(ref BookMark[100]).Range.Text = FIRSTLOWWAVETIDEDATA5;
            doc.Bookmarks.get_Item(ref BookMark[101]).Range.Text = FIRSTLOWWAVETIDEDATA6;
            doc.Bookmarks.get_Item(ref BookMark[102]).Range.Text = FIRSTLOWWAVETIDEDATA7;

            doc.Bookmarks.get_Item(ref BookMark[103]).Range.Text = SECONDHIGHWAVETIDEDATA1;
            doc.Bookmarks.get_Item(ref BookMark[104]).Range.Text = SECONDHIGHWAVETIDEDATA2;
            doc.Bookmarks.get_Item(ref BookMark[105]).Range.Text = SECONDHIGHWAVETIDEDATA3;
            doc.Bookmarks.get_Item(ref BookMark[106]).Range.Text = SECONDHIGHWAVETIDEDATA4;
            doc.Bookmarks.get_Item(ref BookMark[107]).Range.Text = SECONDHIGHWAVETIDEDATA5;
            doc.Bookmarks.get_Item(ref BookMark[108]).Range.Text = SECONDHIGHWAVETIDEDATA6;
            doc.Bookmarks.get_Item(ref BookMark[109]).Range.Text = SECONDHIGHWAVETIDEDATA7;

            doc.Bookmarks.get_Item(ref BookMark[110]).Range.Text = SECONDLOWWAVETIDEDATA1;
            doc.Bookmarks.get_Item(ref BookMark[111]).Range.Text = SECONDLOWWAVETIDEDATA2;
            doc.Bookmarks.get_Item(ref BookMark[112]).Range.Text = SECONDLOWWAVETIDEDATA3;
            doc.Bookmarks.get_Item(ref BookMark[113]).Range.Text = SECONDLOWWAVETIDEDATA4;
            doc.Bookmarks.get_Item(ref BookMark[114]).Range.Text = SECONDLOWWAVETIDEDATA5;
            doc.Bookmarks.get_Item(ref BookMark[115]).Range.Text = SECONDLOWWAVETIDEDATA6;
            doc.Bookmarks.get_Item(ref BookMark[116]).Range.Text = SECONDLOWWAVETIDEDATA7;

            doc.Bookmarks.get_Item(ref BookMark[117]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[118]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[119]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;

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
