using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace PredicTable.ExportWord
{
    public class One72hWord
    {
        string PUBLISHTIME = "";

        string SDOSCWLOWESTWAVEHEIGHT1 = "";
        string SDOSCWLOWESTWAVEHEIGHT2 = "";
        string SDOSCWLOWESTWAVEHEIGHT3 = "";
        string SDOSCWLOWESTWAVEHEIGHT4 = "";
        string SDOSCWLOWESTWAVEHEIGHT5 = "";
        string SDOSCWLOWESTWAVEHEIGHT6 = "";
        string SDOSCWLOWESTWAVEHEIGHT7 = "";
        string SDOSCWLOWESTWAVEHEIGHT148 = "";
        string SDOSCWLOWESTWAVEHEIGHT248 = "";
        string SDOSCWLOWESTWAVEHEIGHT348 = "";
        string SDOSCWLOWESTWAVEHEIGHT448 = "";
        string SDOSCWLOWESTWAVEHEIGHT548 = "";
        string SDOSCWLOWESTWAVEHEIGHT648 = "";
        string SDOSCWLOWESTWAVEHEIGHT748 = "";
        string SDOSCWLOWESTWAVEHEIGHT172 = "";
        string SDOSCWLOWESTWAVEHEIGHT272 = "";
        string SDOSCWLOWESTWAVEHEIGHT372 = "";
        string SDOSCWLOWESTWAVEHEIGHT472 = "";
        string SDOSCWLOWESTWAVEHEIGHT572 = "";
        string SDOSCWLOWESTWAVEHEIGHT672 = "";
        string SDOSCWLOWESTWAVEHEIGHT772 = "";
        string SDOSCWSURFACETEMPERATURE1 = "";
        string SDOSCWSURFACETEMPERATURE2 = "";
        string SDOSCWSURFACETEMPERATURE3 = "";
        string SDOSCWSURFACETEMPERATURE4 = "";
        string SDOSCWSURFACETEMPERATURE5 = "";
        string SDOSCWSURFACETEMPERATURE6 = "";
        string SDOSCWSURFACETEMPERATURE7 = "";
        string SDOSCWSURFACETEMPERATURE148 = "";
        string SDOSCWSURFACETEMPERATURE248 = "";
        string SDOSCWSURFACETEMPERATURE348 = "";
        string SDOSCWSURFACETEMPERATURE448 = "";
        string SDOSCWSURFACETEMPERATURE548 = "";
        string SDOSCWSURFACETEMPERATURE648 = "";
        string SDOSCWSURFACETEMPERATURE748 = "";
        string SDOSCWSURFACETEMPERATURE172 = "";
        string SDOSCWSURFACETEMPERATURE272 = "";
        string SDOSCWSURFACETEMPERATURE372 = "";
        string SDOSCWSURFACETEMPERATURE472 = "";
        string SDOSCWSURFACETEMPERATURE572 = "";
        string SDOSCWSURFACETEMPERATURE672 = "";
        string SDOSCWSURFACETEMPERATURE772 = "";

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
                    string FRELEASEUNIT = "山东省海洋预报台";//tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                    //string FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                    //string FFAX = tblfooter.Rows[i]["FFAX"].ToString();

                    string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                    string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                    string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                    string FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话
                    string FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话

                    string FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                    string FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();
                    string FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();
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
                            SDOSCWLOWESTWAVEHEIGHT148 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT48H"].ToString();
                            SDOSCWSURFACETEMPERATURE148 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE48H"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT172 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT72H"].ToString();
                            SDOSCWSURFACETEMPERATURE172 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE72H"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "青岛近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT2 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE2 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT248 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT48H"].ToString();
                            SDOSCWSURFACETEMPERATURE248 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE48H"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT272 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT72H"].ToString();
                            SDOSCWSURFACETEMPERATURE272 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE72H"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "威海近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT3 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE3 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT348 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT48H"].ToString();
                            SDOSCWSURFACETEMPERATURE348 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE48H"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT372 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT72H"].ToString();
                            SDOSCWSURFACETEMPERATURE372 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE72H"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "烟台近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT4 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE4 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT448 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT48H"].ToString();
                            SDOSCWSURFACETEMPERATURE448 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE48H"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT472 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT72H"].ToString();
                            SDOSCWSURFACETEMPERATURE472 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE72H"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "潍坊近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT5 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE5 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT548 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT48H"].ToString();
                            SDOSCWSURFACETEMPERATURE548 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE48H"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT572 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT72H"].ToString();
                            SDOSCWSURFACETEMPERATURE572 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE72H"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "东营近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT6 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE6 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT648 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT48H"].ToString();
                            SDOSCWSURFACETEMPERATURE648 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE48H"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT672 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT72H"].ToString();
                            SDOSCWSURFACETEMPERATURE672 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE72H"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "滨州近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT7 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE7 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT748 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT48H"].ToString();
                            SDOSCWSURFACETEMPERATURE748 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE48H"].ToString();
                            SDOSCWLOWESTWAVEHEIGHT772 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWESTWAVEHEIGHT72H"].ToString();
                            SDOSCWSURFACETEMPERATURE772 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE72H"].ToString();
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
                            ESASWLOWESTWAVEHEIGHT1 = tbleachseaarea24hseawave.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                            ESASWHIGHTESTWAVEHEIGHT1 = tbleachseaarea24hseawave.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                            ESASWWAVETYPE1 = tbleachseaarea24hseawave.Rows[i]["ESASWWAVETYPE"].ToString();
                        }
                        else if (tbleachseaarea24hseawave.Rows[i]["ESASWAREA"].ToString() == "黄海北部")
                        {
                            ESASWLOWESTWAVEHEIGHT2 = tbleachseaarea24hseawave.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                            ESASWHIGHTESTWAVEHEIGHT2 = tbleachseaarea24hseawave.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                            ESASWWAVETYPE2 = tbleachseaarea24hseawave.Rows[i]["ESASWWAVETYPE"].ToString();
                        }
                        else if (tbleachseaarea24hseawave.Rows[i]["ESASWAREA"].ToString() == "黄海中部")
                        {
                            ESASWLOWESTWAVEHEIGHT3 = tbleachseaarea24hseawave.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                            ESASWHIGHTESTWAVEHEIGHT3 = tbleachseaarea24hseawave.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                            ESASWWAVETYPE3 = tbleachseaarea24hseawave.Rows[i]["ESASWWAVETYPE"].ToString();
                        }
                        else if (tbleachseaarea24hseawave.Rows[i]["ESASWAREA"].ToString() == "黄海南部")
                        {
                            ESASWLOWESTWAVEHEIGHT4 = tbleachseaarea24hseawave.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                            ESASWHIGHTESTWAVEHEIGHT4 = tbleachseaarea24hseawave.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                            ESASWWAVETYPE4 = tbleachseaarea24hseawave.Rows[i]["ESASWWAVETYPE"].ToString();
                        }

                    }

                }

                //获取山东72小时潮汐数据
                //预报日期
                string FORECASTDATE1 = "";
                string FORECASTDATE2 = "";
                string FORECASTDATE3 = "";
                string FORECASTDATE4 = "";
                string FORECASTDATE5 = "";
                string FORECASTDATE6 = "";
                string FORECASTDATE7 = "";
                string FORECASTDATE8 = "";
                string FORECASTDATE9 = "";
                string FORECASTDATE10 = "";
                string FORECASTDATE11 = "";
                string FORECASTDATE12 = "";
                string FORECASTDATE13 = "";
                string FORECASTDATE14 = "";
                string FORECASTDATE15 = "";
                string FORECASTDATE16 = "";
                string FORECASTDATE17 = "";
                string FORECASTDATE18 = "";
                string FORECASTDATE19 = "";
                string FORECASTDATE20 = "";
                string FORECASTDATE21 = "";
                //潮时/分
                string SDOSCTFIRSTHIGHWAVEHOUR1 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR2 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR3 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR4 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR5 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR6 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR7 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR8 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR9 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR10 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR11 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR12 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR13 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR14 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR15 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR16 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR17 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR18 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR19 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR20 = "";
                string SDOSCTFIRSTHIGHWAVEHOUR21 = "";

                string SDOSCTFIRSTHIGHWAVEMINUTE1 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE2 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE3 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE4 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE5 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE6 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE7 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE8 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE9 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE10 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE11 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE12 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE13 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE14 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE15 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE16 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE17 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE18 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE19 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE20 = "";
                string SDOSCTFIRSTHIGHWAVEMINUTE21 = "";

                string SDOSCTSECONDHIGHWAVEHOUR1 = "";
                string SDOSCTSECONDHIGHWAVEHOUR2 = "";
                string SDOSCTSECONDHIGHWAVEHOUR3 = "";
                string SDOSCTSECONDHIGHWAVEHOUR4 = "";
                string SDOSCTSECONDHIGHWAVEHOUR5 = "";
                string SDOSCTSECONDHIGHWAVEHOUR6 = "";
                string SDOSCTSECONDHIGHWAVEHOUR7 = "";
                string SDOSCTSECONDHIGHWAVEHOUR8 = "";
                string SDOSCTSECONDHIGHWAVEHOUR9 = "";
                string SDOSCTSECONDHIGHWAVEHOUR10 = "";
                string SDOSCTSECONDHIGHWAVEHOUR11 = "";
                string SDOSCTSECONDHIGHWAVEHOUR12 = "";
                string SDOSCTSECONDHIGHWAVEHOUR13 = "";
                string SDOSCTSECONDHIGHWAVEHOUR14 = "";
                string SDOSCTSECONDHIGHWAVEHOUR15 = "";
                string SDOSCTSECONDHIGHWAVEHOUR16 = "";
                string SDOSCTSECONDHIGHWAVEHOUR17 = "";
                string SDOSCTSECONDHIGHWAVEHOUR18 = "";
                string SDOSCTSECONDHIGHWAVEHOUR19 = "";
                string SDOSCTSECONDHIGHWAVEHOUR20 = "";
                string SDOSCTSECONDHIGHWAVEHOUR21 = "";

                string SDOSCTSECONDHIGHWAVEMINUTE1 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE2 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE3 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE4 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE5 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE6 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE7 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE8 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE9 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE10 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE11 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE12 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE13 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE14 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE15 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE16 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE17 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE18 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE19 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE20 = "";
                string SDOSCTSECONDHIGHWAVEMINUTE21 = "";

                string SDOSCTFIRSTLOWWAVEHOUR1 = "";
                string SDOSCTFIRSTLOWWAVEHOUR2 = "";
                string SDOSCTFIRSTLOWWAVEHOUR3 = "";
                string SDOSCTFIRSTLOWWAVEHOUR4 = "";
                string SDOSCTFIRSTLOWWAVEHOUR5 = "";
                string SDOSCTFIRSTLOWWAVEHOUR6 = "";
                string SDOSCTFIRSTLOWWAVEHOUR7 = "";
                string SDOSCTFIRSTLOWWAVEHOUR8 = "";
                string SDOSCTFIRSTLOWWAVEHOUR9 = "";
                string SDOSCTFIRSTLOWWAVEHOUR10 = "";
                string SDOSCTFIRSTLOWWAVEHOUR11 = "";
                string SDOSCTFIRSTLOWWAVEHOUR12 = "";
                string SDOSCTFIRSTLOWWAVEHOUR13 = "";
                string SDOSCTFIRSTLOWWAVEHOUR14 = "";
                string SDOSCTFIRSTLOWWAVEHOUR15 = "";
                string SDOSCTFIRSTLOWWAVEHOUR16 = "";
                string SDOSCTFIRSTLOWWAVEHOUR17 = "";
                string SDOSCTFIRSTLOWWAVEHOUR18 = "";
                string SDOSCTFIRSTLOWWAVEHOUR19 = "";
                string SDOSCTFIRSTLOWWAVEHOUR20 = "";
                string SDOSCTFIRSTLOWWAVEHOUR21 = "";

                string SDOSCTFIRSTLOWWAVEMINUTE1 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE2 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE3 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE4 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE5 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE6 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE7 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE8 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE9 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE10 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE11 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE12 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE13 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE14 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE15 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE16 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE17 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE18 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE19 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE20 = "";
                string SDOSCTFIRSTLOWWAVEMINUTE21 = "";

                string SDOSCTSECONDLOWWAVEHOUR1 = "";
                string SDOSCTSECONDLOWWAVEHOUR2 = "";
                string SDOSCTSECONDLOWWAVEHOUR3 = "";
                string SDOSCTSECONDLOWWAVEHOUR4 = "";
                string SDOSCTSECONDLOWWAVEHOUR5 = "";
                string SDOSCTSECONDLOWWAVEHOUR6 = "";
                string SDOSCTSECONDLOWWAVEHOUR7 = "";
                string SDOSCTSECONDLOWWAVEHOUR8 = "";
                string SDOSCTSECONDLOWWAVEHOUR9 = "";
                string SDOSCTSECONDLOWWAVEHOUR10 = "";
                string SDOSCTSECONDLOWWAVEHOUR11 = "";
                string SDOSCTSECONDLOWWAVEHOUR12 = "";
                string SDOSCTSECONDLOWWAVEHOUR13 = "";
                string SDOSCTSECONDLOWWAVEHOUR14 = "";
                string SDOSCTSECONDLOWWAVEHOUR15 = "";
                string SDOSCTSECONDLOWWAVEHOUR16 = "";
                string SDOSCTSECONDLOWWAVEHOUR17 = "";
                string SDOSCTSECONDLOWWAVEHOUR18 = "";
                string SDOSCTSECONDLOWWAVEHOUR19 = "";
                string SDOSCTSECONDLOWWAVEHOUR20 = "";
                string SDOSCTSECONDLOWWAVEHOUR21 = "";

                string SDOSCTSECONDLOWWAVEMINUTE1 = "";
                string SDOSCTSECONDLOWWAVEMINUTE2 = "";
                string SDOSCTSECONDLOWWAVEMINUTE3 = "";
                string SDOSCTSECONDLOWWAVEMINUTE4 = "";
                string SDOSCTSECONDLOWWAVEMINUTE5 = "";
                string SDOSCTSECONDLOWWAVEMINUTE6 = "";
                string SDOSCTSECONDLOWWAVEMINUTE7 = "";
                string SDOSCTSECONDLOWWAVEMINUTE8 = "";
                string SDOSCTSECONDLOWWAVEMINUTE9 = "";
                string SDOSCTSECONDLOWWAVEMINUTE10 = "";
                string SDOSCTSECONDLOWWAVEMINUTE11 = "";
                string SDOSCTSECONDLOWWAVEMINUTE12 = "";
                string SDOSCTSECONDLOWWAVEMINUTE13 = "";
                string SDOSCTSECONDLOWWAVEMINUTE14 = "";
                string SDOSCTSECONDLOWWAVEMINUTE15 = "";
                string SDOSCTSECONDLOWWAVEMINUTE16 = "";
                string SDOSCTSECONDLOWWAVEMINUTE17 = "";
                string SDOSCTSECONDLOWWAVEMINUTE18 = "";
                string SDOSCTSECONDLOWWAVEMINUTE19 = "";
                string SDOSCTSECONDLOWWAVEMINUTE20 = "";
                string SDOSCTSECONDLOWWAVEMINUTE21 = "";
                //潮汐数据潮高
                //第一次高潮潮高
                var FIRSTHIGHWAVETIDEDATA1 = "";
                var FIRSTHIGHWAVETIDEDATA2 = "";
                var FIRSTHIGHWAVETIDEDATA3 = "";
                var FIRSTHIGHWAVETIDEDATA4 = "";
                var FIRSTHIGHWAVETIDEDATA5 = "";
                var FIRSTHIGHWAVETIDEDATA6 = "";
                var FIRSTHIGHWAVETIDEDATA7 = "";
                var FIRSTHIGHWAVETIDEDATA8 = "";
                var FIRSTHIGHWAVETIDEDATA9 = "";
                var FIRSTHIGHWAVETIDEDATA10 = "";
                var FIRSTHIGHWAVETIDEDATA11 = "";
                var FIRSTHIGHWAVETIDEDATA12 = "";
                var FIRSTHIGHWAVETIDEDATA13 = "";
                var FIRSTHIGHWAVETIDEDATA14 = "";
                var FIRSTHIGHWAVETIDEDATA15 = "";
                var FIRSTHIGHWAVETIDEDATA16 = "";
                var FIRSTHIGHWAVETIDEDATA17 = "";
                var FIRSTHIGHWAVETIDEDATA18 = "";
                var FIRSTHIGHWAVETIDEDATA19 = "";
                var FIRSTHIGHWAVETIDEDATA20 = "";
                var FIRSTHIGHWAVETIDEDATA21 = "";

                //第一次低潮潮高
                var FIRSTLOWWAVETIDEDATA1 = "";
                var FIRSTLOWWAVETIDEDATA2 = "";
                var FIRSTLOWWAVETIDEDATA3 = "";
                var FIRSTLOWWAVETIDEDATA4 = "";
                var FIRSTLOWWAVETIDEDATA5 = "";
                var FIRSTLOWWAVETIDEDATA6 = "";
                var FIRSTLOWWAVETIDEDATA7 = "";
                var FIRSTLOWWAVETIDEDATA8 = "";
                var FIRSTLOWWAVETIDEDATA9 = "";
                var FIRSTLOWWAVETIDEDATA10 = "";
                var FIRSTLOWWAVETIDEDATA11 = "";
                var FIRSTLOWWAVETIDEDATA12 = "";
                var FIRSTLOWWAVETIDEDATA13 = "";
                var FIRSTLOWWAVETIDEDATA14 = "";
                var FIRSTLOWWAVETIDEDATA15 = "";
                var FIRSTLOWWAVETIDEDATA16 = "";
                var FIRSTLOWWAVETIDEDATA17 = "";
                var FIRSTLOWWAVETIDEDATA18 = "";
                var FIRSTLOWWAVETIDEDATA19 = "";
                var FIRSTLOWWAVETIDEDATA20 = "";
                var FIRSTLOWWAVETIDEDATA21 = "";
                //第二次高潮潮高
                var SECONDHIGHWAVETIDEDATA1 = "";
                var SECONDHIGHWAVETIDEDATA2 = "";
                var SECONDHIGHWAVETIDEDATA3 = "";
                var SECONDHIGHWAVETIDEDATA4 = "";
                var SECONDHIGHWAVETIDEDATA5 = "";
                var SECONDHIGHWAVETIDEDATA6 = "";
                var SECONDHIGHWAVETIDEDATA7 = "";
                var SECONDHIGHWAVETIDEDATA8 = "";
                var SECONDHIGHWAVETIDEDATA9 = "";
                var SECONDHIGHWAVETIDEDATA10 = "";
                var SECONDHIGHWAVETIDEDATA11 = "";
                var SECONDHIGHWAVETIDEDATA12 = "";
                var SECONDHIGHWAVETIDEDATA13 = "";
                var SECONDHIGHWAVETIDEDATA14 = "";
                var SECONDHIGHWAVETIDEDATA15 = "";
                var SECONDHIGHWAVETIDEDATA16 = "";
                var SECONDHIGHWAVETIDEDATA17 = "";
                var SECONDHIGHWAVETIDEDATA18 = "";
                var SECONDHIGHWAVETIDEDATA19 = "";
                var SECONDHIGHWAVETIDEDATA20 = "";
                var SECONDHIGHWAVETIDEDATA21 = "";
                //第二次低潮潮高
                var SECONDLOWWAVETIDEDATA1 = "";
                var SECONDLOWWAVETIDEDATA2 = "";
                var SECONDLOWWAVETIDEDATA3 = "";
                var SECONDLOWWAVETIDEDATA4 = "";
                var SECONDLOWWAVETIDEDATA5 = "";
                var SECONDLOWWAVETIDEDATA6 = "";
                var SECONDLOWWAVETIDEDATA7 = "";
                var SECONDLOWWAVETIDEDATA8 = "";
                var SECONDLOWWAVETIDEDATA9 = "";
                var SECONDLOWWAVETIDEDATA10 = "";
                var SECONDLOWWAVETIDEDATA11 = "";
                var SECONDLOWWAVETIDEDATA12 = "";
                var SECONDLOWWAVETIDEDATA13 = "";
                var SECONDLOWWAVETIDEDATA14 = "";
                var SECONDLOWWAVETIDEDATA15 = "";
                var SECONDLOWWAVETIDEDATA16 = "";
                var SECONDLOWWAVETIDEDATA17 = "";
                var SECONDLOWWAVETIDEDATA18 = "";
                var SECONDLOWWAVETIDEDATA19 = "";
                var SECONDLOWWAVETIDEDATA20 = "";
                var SECONDLOWWAVETIDEDATA21 = "";

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
                        if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "日照")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE1 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE1 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE2 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE3 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE3 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE4 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE4 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE5 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE5 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE6 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE6 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "威海")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE7 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE7 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE8 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR8 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE8 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR8 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE8 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR8 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE8 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR8 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE8 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE9 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR9 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE9 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR9 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE9 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR9 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE9 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR9 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE9 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "烟台")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE10 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR10 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE10 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR10 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE10 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR10 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE10 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR10 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE10 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE11 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR11 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE11 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR11 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE11 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR11 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE11 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR11 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE11 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE12 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR12 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE12 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR12 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE12 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR12 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE12 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR12 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE12 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "潍坊")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE13 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR13 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE13 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR13 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE13 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR13 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE13 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR13 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE13 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE14 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR14 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE14 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR14 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE14 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR14 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE14 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR14 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE14 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE15 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR15 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE15 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR15 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE15 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR15 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE15 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR15 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE15 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "东营")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE16 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR16 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE16 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR16 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE16 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR16 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE16 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR16 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE16 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE17 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR17 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE17 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR17 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE17 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR17 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE17 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR17 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE17 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE18 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR18 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE18 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR18 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE18 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR18 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE18 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR18 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE18 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "滨州")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE19 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR19 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE19 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR19 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE19 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR19 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE19 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR19 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE19 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE20 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR20 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE20 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR20 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE20 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR20 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE20 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR20 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE20 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE21 = datef;
                                SDOSCTFIRSTHIGHWAVEHOUR21 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                                SDOSCTFIRSTHIGHWAVEMINUTE21 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                                SDOSCTSECONDHIGHWAVEHOUR21 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                                SDOSCTSECONDHIGHWAVEMINUTE21 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                                SDOSCTFIRSTLOWWAVEHOUR21 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                                SDOSCTFIRSTLOWWAVEMINUTE21 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                                SDOSCTSECONDLOWWAVEHOUR21 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                                SDOSCTSECONDLOWWAVEMINUTE21 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                            }
                        }
                    }
                }
                sql_TideData sql = new sql_TideData();
                HT_TideData model = new HT_TideData();
                model.PUBLISHDATE = dt;
                DataTable tideData = (DataTable)sql.getTideData(model);
                if (tideData != null && tideData.Rows.Count > 0)
                {
                    var fdate1 = dt.AddDays(1).ToString();
                    var fdate2 = dt.AddDays(2).ToString();
                    var fdate3 = dt.AddDays(3).ToString();
                    for (int i = 0; i < tideData.Rows.Count; i++)
                    {
                        var forecastdate = Convert.ToDateTime(tideData.Rows[i]["FORECASTDATE"]).ToString();
                        var datef = Convert.ToDateTime(forecastdate).Day.ToString();
                        if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "日照")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE1 = datef;
                                FIRSTHIGHWAVETIDEDATA1 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA1 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA1 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA1 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE2 = datef;
                                FIRSTHIGHWAVETIDEDATA2 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA2 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA2 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA2 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE3 = datef;
                                FIRSTHIGHWAVETIDEDATA3 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA3 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA3 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA3 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE4 = datef;
                                FIRSTHIGHWAVETIDEDATA4 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA4 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA4 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA4 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE5 = datef;
                                FIRSTHIGHWAVETIDEDATA5 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA5 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA5 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA5 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE6 = datef;
                                FIRSTHIGHWAVETIDEDATA6 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA6 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA6 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA6 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "威海")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE7 = datef;
                                FIRSTHIGHWAVETIDEDATA7 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA7 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA7 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA7 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE8 = datef;
                                FIRSTHIGHWAVETIDEDATA8 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA8 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA8 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA8 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE9 = datef;
                                FIRSTHIGHWAVETIDEDATA9 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA9 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA9 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA9 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "烟台")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE10 = datef;
                                FIRSTHIGHWAVETIDEDATA10 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA10 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA10 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA10 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE11 = datef;
                                FIRSTHIGHWAVETIDEDATA11 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA11 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA11 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA11 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE12 = datef;
                                FIRSTHIGHWAVETIDEDATA12 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA12 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA12 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA12 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "潍坊")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE13 = datef;
                                FIRSTHIGHWAVETIDEDATA13 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA13 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA13 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA13 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE14 = datef;
                                FIRSTHIGHWAVETIDEDATA14 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA14 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA14 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA14 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE15 = datef;
                                FIRSTHIGHWAVETIDEDATA15 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA15 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA15 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA15 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "东营")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE16 = datef;
                                FIRSTHIGHWAVETIDEDATA16 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA16 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA16 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA16 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE17 = datef;
                                FIRSTHIGHWAVETIDEDATA17 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA17 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA17 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA17 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE18 = datef;
                                FIRSTHIGHWAVETIDEDATA18 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA18 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA18 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA18 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "滨州")
                        {
                            if (forecastdate == fdate1)
                            {
                                FORECASTDATE19 = datef;
                                FIRSTHIGHWAVETIDEDATA19 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA19 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA19 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA19 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate2)
                            {
                                FORECASTDATE20 = datef;
                                FIRSTHIGHWAVETIDEDATA20 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA20 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA20 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA20 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                            else if (forecastdate == fdate3)
                            {
                                FORECASTDATE21 = datef;
                                FIRSTHIGHWAVETIDEDATA21 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                                FIRSTLOWWAVETIDEDATA21 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                                SECONDHIGHWAVETIDEDATA21 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                                SECONDLOWWAVETIDEDATA21 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                            }
                        }
                    }
                }
                #region 声明书签数组
                //为了方便管理声明书签数组
                object[] BookMark = new object[338];//新增3个电话，335改为338
                //赋值书签名
                BookMark[0] = "FRELEASEUNIT";//发布单位
                BookMark[1] = "FZHIBANTEL";//预报值班
                BookMark[2] = "FSENDTEL";//预报值班

                //BookMark[1] = "FTELEPHONE";//电话
                //BookMark[2] = "FFAX";//传真
                BookMark[3] = "FWAVEFORECASTER";//海浪预报员
                BookMark[4] = "FWATERTEMPERATUREFORECASTER";//水温预报员
                BookMark[5] = "ESASWLOWESTWAVEHEIGHT1";
                BookMark[6] = "ESASWLOWESTWAVEHEIGHT2";
                BookMark[7] = "ESASWLOWESTWAVEHEIGHT3";
                BookMark[8] = "ESASWLOWESTWAVEHEIGHT4";
                BookMark[9] = "ESASWHIGHTESTWAVEHEIGHT1";
                BookMark[10] = "ESASWHIGHTESTWAVEHEIGHT2";
                BookMark[11] = "ESASWHIGHTESTWAVEHEIGHT3";
                BookMark[12] = "ESASWHIGHTESTWAVEHEIGHT4";
                BookMark[13] = "ESASWWAVETYPE1";
                BookMark[14] = "ESASWWAVETYPE2";
                BookMark[15] = "ESASWWAVETYPE3";
                BookMark[16] = "ESASWWAVETYPE4";
                BookMark[17] = "SDOSCWLOWESTWAVEHEIGHT1";
                BookMark[18] = "SDOSCWLOWESTWAVEHEIGHT2";
                BookMark[19] = "SDOSCWLOWESTWAVEHEIGHT3";
                BookMark[20] = "SDOSCWLOWESTWAVEHEIGHT4";
                BookMark[21] = "SDOSCWLOWESTWAVEHEIGHT5";
                BookMark[22] = "SDOSCWLOWESTWAVEHEIGHT6";
                BookMark[23] = "SDOSCWLOWESTWAVEHEIGHT7";
                BookMark[24] = "SDOSCWSURFACETEMPERATURE1";
                BookMark[25] = "SDOSCWSURFACETEMPERATURE2";
                BookMark[26] = "SDOSCWSURFACETEMPERATURE3";
                BookMark[27] = "SDOSCWSURFACETEMPERATURE4";
                BookMark[28] = "SDOSCWSURFACETEMPERATURE5";
                BookMark[29] = "SDOSCWSURFACETEMPERATURE6";
                BookMark[30] = "SDOSCWSURFACETEMPERATURE7";
                BookMark[31] = "PUBLISHTIME";




                BookMark[32] = "SDOSCWLOWESTWAVEHEIGHT148";
                BookMark[33] = "SDOSCWLOWESTWAVEHEIGHT248";
                BookMark[34] = "SDOSCWLOWESTWAVEHEIGHT348";
                BookMark[35] = "SDOSCWLOWESTWAVEHEIGHT448";
                BookMark[36] = "SDOSCWLOWESTWAVEHEIGHT548";
                BookMark[37] = "SDOSCWLOWESTWAVEHEIGHT648";
                BookMark[38] = "SDOSCWLOWESTWAVEHEIGHT748";
                BookMark[39] = "SDOSCWLOWESTWAVEHEIGHT172";
                BookMark[40] = "SDOSCWLOWESTWAVEHEIGHT272";
                BookMark[41] = "SDOSCWLOWESTWAVEHEIGHT372";
                BookMark[42] = "SDOSCWLOWESTWAVEHEIGHT472";
                BookMark[43] = "SDOSCWLOWESTWAVEHEIGHT572";
                BookMark[44] = "SDOSCWLOWESTWAVEHEIGHT672";
                BookMark[45] = "SDOSCWLOWESTWAVEHEIGHT772";

                BookMark[46] = "SDOSCWSURFACETEMPERATURE148";
                BookMark[47] = "SDOSCWSURFACETEMPERATURE248";
                BookMark[48] = "SDOSCWSURFACETEMPERATURE348";
                BookMark[49] = "SDOSCWSURFACETEMPERATURE448";
                BookMark[50] = "SDOSCWSURFACETEMPERATURE548";
                BookMark[51] = "SDOSCWSURFACETEMPERATURE648";
                BookMark[52] = "SDOSCWSURFACETEMPERATURE748";

                BookMark[53] = "SDOSCWSURFACETEMPERATURE172";
                BookMark[54] = "SDOSCWSURFACETEMPERATURE272";
                BookMark[55] = "SDOSCWSURFACETEMPERATURE372";
                BookMark[56] = "SDOSCWSURFACETEMPERATURE472";
                BookMark[57] = "SDOSCWSURFACETEMPERATURE572";
                BookMark[58] = "SDOSCWSURFACETEMPERATURE672";
                BookMark[59] = "SDOSCWSURFACETEMPERATURE772";




                //潮汐
                BookMark[60] = "FORECASTDATE1";
                BookMark[61] = "FORECASTDATE2";
                BookMark[62] = "FORECASTDATE3";
                BookMark[63] = "FORECASTDATE4";
                BookMark[64] = "FORECASTDATE5";
                BookMark[65] = "FORECASTDATE6";
                BookMark[66] = "FORECASTDATE7";
                BookMark[67] = "FORECASTDATE8";
                BookMark[68] = "FORECASTDATE9";
                BookMark[69] = "FORECASTDATE10";
                BookMark[70] = "FORECASTDATE11";
                BookMark[71] = "FORECASTDATE12";
                BookMark[72] = "FORECASTDATE13";
                BookMark[73] = "FORECASTDATE14";
                BookMark[74] = "FORECASTDATE15";
                BookMark[75] = "FORECASTDATE16";
                BookMark[76] = "FORECASTDATE17";
                BookMark[77] = "FORECASTDATE18";
                BookMark[78] = "FORECASTDATE19";
                BookMark[79] = "FORECASTDATE20";
                BookMark[80] = "FORECASTDATE21";
                BookMark[81] = "SDOSCTFIRSTHIGHWAVEHOUR1";
                BookMark[82] = "SDOSCTFIRSTHIGHWAVEHOUR2";
                BookMark[83] = "SDOSCTFIRSTHIGHWAVEHOUR3";
                BookMark[84] = "SDOSCTFIRSTHIGHWAVEHOUR4";
                BookMark[85] = "SDOSCTFIRSTHIGHWAVEHOUR5";
                BookMark[86] = "SDOSCTFIRSTHIGHWAVEHOUR6";
                BookMark[87] = "SDOSCTFIRSTHIGHWAVEHOUR7";
                BookMark[89] = "SDOSCTFIRSTHIGHWAVEHOUR8";
                BookMark[90] = "SDOSCTFIRSTHIGHWAVEHOUR9";
                BookMark[91] = "SDOSCTFIRSTHIGHWAVEHOUR10";
                BookMark[92] = "SDOSCTFIRSTHIGHWAVEHOUR11";
                BookMark[93] = "SDOSCTFIRSTHIGHWAVEHOUR12";
                BookMark[94] = "SDOSCTFIRSTHIGHWAVEHOUR13";
                BookMark[95] = "SDOSCTFIRSTHIGHWAVEHOUR14";
                BookMark[96] = "SDOSCTFIRSTHIGHWAVEHOUR15";
                BookMark[97] = "SDOSCTFIRSTHIGHWAVEHOUR16";
                BookMark[98] = "SDOSCTFIRSTHIGHWAVEHOUR17";
                BookMark[99] = "SDOSCTFIRSTHIGHWAVEHOUR18";
                BookMark[100] = "SDOSCTFIRSTHIGHWAVEHOUR19";
                BookMark[101] = "SDOSCTFIRSTHIGHWAVEHOUR20";
                BookMark[102] = "SDOSCTFIRSTHIGHWAVEHOUR21";
                BookMark[103] = "SDOSCTFIRSTHIGHWAVEMINUTE1";
                BookMark[104] = "SDOSCTFIRSTHIGHWAVEMINUTE2";
                BookMark[105] = "SDOSCTFIRSTHIGHWAVEMINUTE3";
                BookMark[106] = "SDOSCTFIRSTHIGHWAVEMINUTE4";
                BookMark[107] = "SDOSCTFIRSTHIGHWAVEMINUTE5";
                BookMark[108] = "SDOSCTFIRSTHIGHWAVEMINUTE6";
                BookMark[109] = "SDOSCTFIRSTHIGHWAVEMINUTE7";
                BookMark[110] = "SDOSCTFIRSTHIGHWAVEMINUTE8";
                BookMark[111] = "SDOSCTFIRSTHIGHWAVEMINUTE9";
                BookMark[112] = "SDOSCTFIRSTHIGHWAVEMINUTE10";
                BookMark[113] = "SDOSCTFIRSTHIGHWAVEMINUTE11";
                BookMark[114] = "SDOSCTFIRSTHIGHWAVEMINUTE12";
                BookMark[115] = "SDOSCTFIRSTHIGHWAVEMINUTE13";
                BookMark[116] = "SDOSCTFIRSTHIGHWAVEMINUTE14";
                BookMark[117] = "SDOSCTFIRSTHIGHWAVEMINUTE15";
                BookMark[118] = "SDOSCTFIRSTHIGHWAVEMINUTE16";
                BookMark[119] = "SDOSCTFIRSTHIGHWAVEMINUTE17";
                BookMark[120] = "SDOSCTFIRSTHIGHWAVEMINUTE18";
                BookMark[121] = "SDOSCTFIRSTHIGHWAVEMINUTE19";
                BookMark[122] = "SDOSCTFIRSTHIGHWAVEMINUTE20";
                BookMark[123] = "SDOSCTFIRSTHIGHWAVEMINUTE21";
                BookMark[124] = "SDOSCTSECONDHIGHWAVEHOUR1";
                BookMark[125] = "SDOSCTSECONDHIGHWAVEHOUR2";
                BookMark[126] = "SDOSCTSECONDHIGHWAVEHOUR3";
                BookMark[127] = "SDOSCTSECONDHIGHWAVEHOUR4";
                BookMark[128] = "SDOSCTSECONDHIGHWAVEHOUR5";
                BookMark[129] = "SDOSCTSECONDHIGHWAVEHOUR6";
                BookMark[130] = "SDOSCTSECONDHIGHWAVEHOUR7";
                BookMark[131] = "SDOSCTSECONDHIGHWAVEHOUR8";
                BookMark[132] = "SDOSCTSECONDHIGHWAVEHOUR9";
                BookMark[133] = "SDOSCTSECONDHIGHWAVEHOUR10";
                BookMark[134] = "SDOSCTSECONDHIGHWAVEHOUR11";
                BookMark[135] = "SDOSCTSECONDHIGHWAVEHOUR12";
                BookMark[136] = "SDOSCTSECONDHIGHWAVEHOUR13";
                BookMark[137] = "SDOSCTSECONDHIGHWAVEHOUR14";
                BookMark[138] = "SDOSCTSECONDHIGHWAVEHOUR15";
                BookMark[139] = "SDOSCTSECONDHIGHWAVEHOUR16";
                BookMark[140] = "SDOSCTSECONDHIGHWAVEHOUR17";
                BookMark[141] = "SDOSCTSECONDHIGHWAVEHOUR18";
                BookMark[142] = "SDOSCTSECONDHIGHWAVEHOUR19";
                BookMark[143] = "SDOSCTSECONDHIGHWAVEHOUR20";
                BookMark[144] = "SDOSCTSECONDHIGHWAVEHOUR21";
                BookMark[145] = "SDOSCTSECONDHIGHWAVEMINUTE1";
                BookMark[146] = "SDOSCTSECONDHIGHWAVEMINUTE2";
                BookMark[147] = "SDOSCTSECONDHIGHWAVEMINUTE3";
                BookMark[148] = "SDOSCTSECONDHIGHWAVEMINUTE4";
                BookMark[149] = "SDOSCTSECONDHIGHWAVEMINUTE5";
                BookMark[150] = "SDOSCTSECONDHIGHWAVEMINUTE6";
                BookMark[151] = "SDOSCTSECONDHIGHWAVEMINUTE7";
                BookMark[152] = "SDOSCTSECONDHIGHWAVEMINUTE8";
                BookMark[153] = "SDOSCTSECONDHIGHWAVEMINUTE9";
                BookMark[154] = "SDOSCTSECONDHIGHWAVEMINUTE10";
                BookMark[155] = "SDOSCTSECONDHIGHWAVEMINUTE11";
                BookMark[156] = "SDOSCTSECONDHIGHWAVEMINUTE12";
                BookMark[157] = "SDOSCTSECONDHIGHWAVEMINUTE13";
                BookMark[158] = "SDOSCTSECONDHIGHWAVEMINUTE14";
                BookMark[159] = "SDOSCTSECONDHIGHWAVEMINUTE15";
                BookMark[160] = "SDOSCTSECONDHIGHWAVEMINUTE16";
                BookMark[161] = "SDOSCTSECONDHIGHWAVEMINUTE17";
                BookMark[162] = "SDOSCTSECONDHIGHWAVEMINUTE18";
                BookMark[163] = "SDOSCTSECONDHIGHWAVEMINUTE19";
                BookMark[164] = "SDOSCTSECONDHIGHWAVEMINUTE20";
                BookMark[165] = "SDOSCTSECONDHIGHWAVEMINUTE21";
                BookMark[166] = "SDOSCTFIRSTLOWWAVEHOUR1";
                BookMark[167] = "SDOSCTFIRSTLOWWAVEHOUR2";
                BookMark[168] = "SDOSCTFIRSTLOWWAVEHOUR3";
                BookMark[169] = "SDOSCTFIRSTLOWWAVEHOUR4";
                BookMark[170] = "SDOSCTFIRSTLOWWAVEHOUR5";
                BookMark[171] = "SDOSCTFIRSTLOWWAVEHOUR6";
                BookMark[172] = "SDOSCTFIRSTLOWWAVEHOUR7";
                BookMark[173] = "SDOSCTFIRSTLOWWAVEHOUR8";
                BookMark[174] = "SDOSCTFIRSTLOWWAVEHOUR9";
                BookMark[175] = "SDOSCTFIRSTLOWWAVEHOUR10";
                BookMark[176] = "SDOSCTFIRSTLOWWAVEHOUR11";
                BookMark[177] = "SDOSCTFIRSTLOWWAVEHOUR12";
                BookMark[178] = "SDOSCTFIRSTLOWWAVEHOUR13";
                BookMark[179] = "SDOSCTFIRSTLOWWAVEHOUR14";
                BookMark[180] = "SDOSCTFIRSTLOWWAVEHOUR15";
                BookMark[181] = "SDOSCTFIRSTLOWWAVEHOUR16";
                BookMark[182] = "SDOSCTFIRSTLOWWAVEHOUR17";
                BookMark[183] = "SDOSCTFIRSTLOWWAVEHOUR18";
                BookMark[184] = "SDOSCTFIRSTLOWWAVEHOUR19";
                BookMark[185] = "SDOSCTFIRSTLOWWAVEHOUR20";
                BookMark[186] = "SDOSCTFIRSTLOWWAVEHOUR21";
                BookMark[187] = "SDOSCTFIRSTLOWWAVEMINUTE1";
                BookMark[188] = "SDOSCTFIRSTLOWWAVEMINUTE2";
                BookMark[189] = "SDOSCTFIRSTLOWWAVEMINUTE3";
                BookMark[190] = "SDOSCTFIRSTLOWWAVEMINUTE4";
                BookMark[191] = "SDOSCTFIRSTLOWWAVEMINUTE5";
                BookMark[192] = "SDOSCTFIRSTLOWWAVEMINUTE6";
                BookMark[193] = "SDOSCTFIRSTLOWWAVEMINUTE7";
                BookMark[194] = "SDOSCTFIRSTLOWWAVEMINUTE8";
                BookMark[195] = "SDOSCTFIRSTLOWWAVEMINUTE9";
                BookMark[196] = "SDOSCTFIRSTLOWWAVEMINUTE10";
                BookMark[197] = "SDOSCTFIRSTLOWWAVEMINUTE11";
                BookMark[198] = "SDOSCTFIRSTLOWWAVEMINUTE12";
                BookMark[199] = "SDOSCTFIRSTLOWWAVEMINUTE13";
                BookMark[200] = "SDOSCTFIRSTLOWWAVEMINUTE14";
                BookMark[201] = "SDOSCTFIRSTLOWWAVEMINUTE15";
                BookMark[202] = "SDOSCTFIRSTLOWWAVEMINUTE16";
                BookMark[203] = "SDOSCTFIRSTLOWWAVEMINUTE17";
                BookMark[204] = "SDOSCTFIRSTLOWWAVEMINUTE18";
                BookMark[205] = "SDOSCTFIRSTLOWWAVEMINUTE19";
                BookMark[206] = "SDOSCTFIRSTLOWWAVEMINUTE20";
                BookMark[207] = "SDOSCTFIRSTLOWWAVEMINUTE21";
                BookMark[208] = "SDOSCTSECONDLOWWAVEHOUR1";
                BookMark[209] = "SDOSCTSECONDLOWWAVEHOUR2";
                BookMark[210] = "SDOSCTSECONDLOWWAVEHOUR3";
                BookMark[211] = "SDOSCTSECONDLOWWAVEHOUR4";
                BookMark[212] = "SDOSCTSECONDLOWWAVEHOUR5";
                BookMark[213] = "SDOSCTSECONDLOWWAVEHOUR6";
                BookMark[214] = "SDOSCTSECONDLOWWAVEHOUR7";
                BookMark[215] = "SDOSCTSECONDLOWWAVEHOUR8";
                BookMark[216] = "SDOSCTSECONDLOWWAVEHOUR9";
                BookMark[217] = "SDOSCTSECONDLOWWAVEHOUR10";
                BookMark[218] = "SDOSCTSECONDLOWWAVEHOUR11";
                BookMark[219] = "SDOSCTSECONDLOWWAVEHOUR12";
                BookMark[220] = "SDOSCTSECONDLOWWAVEHOUR13";
                BookMark[221] = "SDOSCTSECONDLOWWAVEHOUR14";
                BookMark[222] = "SDOSCTSECONDLOWWAVEHOUR15";
                BookMark[223] = "SDOSCTSECONDLOWWAVEHOUR16";
                BookMark[224] = "SDOSCTSECONDLOWWAVEHOUR17";
                BookMark[225] = "SDOSCTSECONDLOWWAVEHOUR18";
                BookMark[226] = "SDOSCTSECONDLOWWAVEHOUR19";
                BookMark[227] = "SDOSCTSECONDLOWWAVEHOUR20";
                BookMark[228] = "SDOSCTSECONDLOWWAVEHOUR21";
                BookMark[229] = "SDOSCTSECONDLOWWAVEMINUTE1";
                BookMark[230] = "SDOSCTSECONDLOWWAVEMINUTE2";
                BookMark[231] = "SDOSCTSECONDLOWWAVEMINUTE3";
                BookMark[232] = "SDOSCTSECONDLOWWAVEMINUTE4";
                BookMark[233] = "SDOSCTSECONDLOWWAVEMINUTE5";
                BookMark[234] = "SDOSCTSECONDLOWWAVEMINUTE6";
                BookMark[235] = "SDOSCTSECONDLOWWAVEMINUTE7";
                BookMark[236] = "SDOSCTSECONDLOWWAVEMINUTE8";
                BookMark[237] = "SDOSCTSECONDLOWWAVEMINUTE9";
                BookMark[238] = "SDOSCTSECONDLOWWAVEMINUTE10";
                BookMark[239] = "SDOSCTSECONDLOWWAVEMINUTE11";
                BookMark[240] = "SDOSCTSECONDLOWWAVEMINUTE12";
                BookMark[241] = "SDOSCTSECONDLOWWAVEMINUTE13";
                BookMark[242] = "SDOSCTSECONDLOWWAVEMINUTE14";
                BookMark[243] = "SDOSCTSECONDLOWWAVEMINUTE15";
                BookMark[244] = "SDOSCTSECONDLOWWAVEMINUTE16";
                BookMark[245] = "SDOSCTSECONDLOWWAVEMINUTE17";
                BookMark[246] = "SDOSCTSECONDLOWWAVEMINUTE18";
                BookMark[247] = "SDOSCTSECONDLOWWAVEMINUTE19";
                BookMark[248] = "SDOSCTSECONDLOWWAVEMINUTE20";
                BookMark[249] = "SDOSCTSECONDLOWWAVEMINUTE21";
                BookMark[250] = "FIRSTHIGHWAVETIDEDATA1";
                BookMark[251] = "FIRSTHIGHWAVETIDEDATA2";
                BookMark[252] = "FIRSTHIGHWAVETIDEDATA3";
                BookMark[253] = "FIRSTHIGHWAVETIDEDATA4";
                BookMark[254] = "FIRSTHIGHWAVETIDEDATA5";
                BookMark[255] = "FIRSTHIGHWAVETIDEDATA6";
                BookMark[256] = "FIRSTHIGHWAVETIDEDATA7";
                BookMark[257] = "FIRSTHIGHWAVETIDEDATA8";
                BookMark[258] = "FIRSTHIGHWAVETIDEDATA9";
                BookMark[259] = "FIRSTHIGHWAVETIDEDATA10";
                BookMark[260] = "FIRSTHIGHWAVETIDEDATA11";
                BookMark[261] = "FIRSTHIGHWAVETIDEDATA12";
                BookMark[262] = "FIRSTHIGHWAVETIDEDATA13";
                BookMark[263] = "FIRSTHIGHWAVETIDEDATA14";
                BookMark[264] = "FIRSTHIGHWAVETIDEDATA15";
                BookMark[265] = "FIRSTHIGHWAVETIDEDATA16";
                BookMark[266] = "FIRSTHIGHWAVETIDEDATA17";
                BookMark[267] = "FIRSTHIGHWAVETIDEDATA18";
                BookMark[268] = "FIRSTHIGHWAVETIDEDATA19";
                BookMark[269] = "FIRSTHIGHWAVETIDEDATA20";
                BookMark[270] = "FIRSTHIGHWAVETIDEDATA21";
                BookMark[271] = "FIRSTLOWWAVETIDEDATA1";
                BookMark[272] = "FIRSTLOWWAVETIDEDATA2";
                BookMark[273] = "FIRSTLOWWAVETIDEDATA3";
                BookMark[274] = "FIRSTLOWWAVETIDEDATA4";
                BookMark[275] = "FIRSTLOWWAVETIDEDATA5";
                BookMark[276] = "FIRSTLOWWAVETIDEDATA6";
                BookMark[277] = "FIRSTLOWWAVETIDEDATA7";
                BookMark[278] = "FIRSTLOWWAVETIDEDATA8";
                BookMark[279] = "FIRSTLOWWAVETIDEDATA9";
                BookMark[280] = "FIRSTLOWWAVETIDEDATA10";
                BookMark[281] = "FIRSTLOWWAVETIDEDATA11";
                BookMark[282] = "FIRSTLOWWAVETIDEDATA12";
                BookMark[283] = "FIRSTLOWWAVETIDEDATA13";
                BookMark[284] = "FIRSTLOWWAVETIDEDATA14";
                BookMark[285] = "FIRSTLOWWAVETIDEDATA15";
                BookMark[286] = "FIRSTLOWWAVETIDEDATA16";
                BookMark[287] = "FIRSTLOWWAVETIDEDATA17";
                BookMark[288] = "FIRSTLOWWAVETIDEDATA18";
                BookMark[289] = "FIRSTLOWWAVETIDEDATA19";
                BookMark[290] = "FIRSTLOWWAVETIDEDATA20";
                BookMark[291] = "FIRSTLOWWAVETIDEDATA21";
                BookMark[292] = "SECONDHIGHWAVETIDEDATA1";
                BookMark[293] = "SECONDHIGHWAVETIDEDATA2";
                BookMark[294] = "SECONDHIGHWAVETIDEDATA3";
                BookMark[295] = "SECONDHIGHWAVETIDEDATA4";
                BookMark[296] = "SECONDHIGHWAVETIDEDATA5";
                BookMark[297] = "SECONDHIGHWAVETIDEDATA6";
                BookMark[298] = "SECONDHIGHWAVETIDEDATA7";
                BookMark[299] = "SECONDHIGHWAVETIDEDATA8";
                BookMark[300] = "SECONDHIGHWAVETIDEDATA9";
                BookMark[301] = "SECONDHIGHWAVETIDEDATA10";
                BookMark[302] = "SECONDHIGHWAVETIDEDATA11";
                BookMark[303] = "SECONDHIGHWAVETIDEDATA12";
                BookMark[304] = "SECONDHIGHWAVETIDEDATA13";
                BookMark[305] = "SECONDHIGHWAVETIDEDATA14";
                BookMark[306] = "SECONDHIGHWAVETIDEDATA15";
                BookMark[307] = "SECONDHIGHWAVETIDEDATA16";
                BookMark[308] = "SECONDHIGHWAVETIDEDATA17";
                BookMark[309] = "SECONDHIGHWAVETIDEDATA18";
                BookMark[310] = "SECONDHIGHWAVETIDEDATA19";
                BookMark[311] = "SECONDHIGHWAVETIDEDATA20";
                BookMark[312] = "SECONDHIGHWAVETIDEDATA21";
                BookMark[313] = "SECONDLOWWAVETIDEDATA1";
                BookMark[314] = "SECONDLOWWAVETIDEDATA2";
                BookMark[315] = "SECONDLOWWAVETIDEDATA3";
                BookMark[316] = "SECONDLOWWAVETIDEDATA4";
                BookMark[317] = "SECONDLOWWAVETIDEDATA5";
                BookMark[318] = "SECONDLOWWAVETIDEDATA6";
                BookMark[319] = "SECONDLOWWAVETIDEDATA7";
                BookMark[320] = "SECONDLOWWAVETIDEDATA8";
                BookMark[321] = "SECONDLOWWAVETIDEDATA9";
                BookMark[322] = "SECONDLOWWAVETIDEDATA10";
                BookMark[323] = "SECONDLOWWAVETIDEDATA11";
                BookMark[324] = "SECONDLOWWAVETIDEDATA12";
                BookMark[325] = "SECONDLOWWAVETIDEDATA13";
                BookMark[326] = "SECONDLOWWAVETIDEDATA14";
                BookMark[327] = "SECONDLOWWAVETIDEDATA15";
                BookMark[328] = "SECONDLOWWAVETIDEDATA16";
                BookMark[329] = "SECONDLOWWAVETIDEDATA17";
                BookMark[330] = "SECONDLOWWAVETIDEDATA18";
                BookMark[331] = "SECONDLOWWAVETIDEDATA19";
                BookMark[332] = "SECONDLOWWAVETIDEDATA20";
                BookMark[333] = "SECONDLOWWAVETIDEDATA21";
                BookMark[334] = "FTIDALFORECASTER";

                BookMark[335] = "FWAVEFORECASTERTEL";//海浪预报员电话
                BookMark[336] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
                BookMark[337] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话

                //赋值数据到书签的位置
                doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
                doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
                //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
                //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
                doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
                doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;

                doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = ESASWLOWESTWAVEHEIGHT1;
                doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = ESASWLOWESTWAVEHEIGHT2;
                doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = ESASWLOWESTWAVEHEIGHT3;
                doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = ESASWLOWESTWAVEHEIGHT4;
                doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = ESASWHIGHTESTWAVEHEIGHT1;
                doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = ESASWHIGHTESTWAVEHEIGHT2;
                doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = ESASWHIGHTESTWAVEHEIGHT3;
                doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = ESASWHIGHTESTWAVEHEIGHT4;
                doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = ESASWWAVETYPE1;
                doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = ESASWWAVETYPE2;
                doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = ESASWWAVETYPE3;
                doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = ESASWWAVETYPE4;
                doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = SDOSCWLOWESTWAVEHEIGHT1;
                doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = SDOSCWLOWESTWAVEHEIGHT2;
                doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = SDOSCWLOWESTWAVEHEIGHT3;
                doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = SDOSCWLOWESTWAVEHEIGHT4;
                doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = SDOSCWLOWESTWAVEHEIGHT5;
                doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = SDOSCWLOWESTWAVEHEIGHT6;
                doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = SDOSCWLOWESTWAVEHEIGHT7;
                doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = SDOSCWSURFACETEMPERATURE1;
                doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = SDOSCWSURFACETEMPERATURE2;
                doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = SDOSCWSURFACETEMPERATURE3;
                doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = SDOSCWSURFACETEMPERATURE4;
                doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = SDOSCWSURFACETEMPERATURE5;
                doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = SDOSCWSURFACETEMPERATURE6;
                doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = SDOSCWSURFACETEMPERATURE7;
                doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = PUBLISHTIME;

                doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = SDOSCWLOWESTWAVEHEIGHT148;
                doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = SDOSCWLOWESTWAVEHEIGHT248;
                doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = SDOSCWLOWESTWAVEHEIGHT348;

                doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = SDOSCWLOWESTWAVEHEIGHT448;
                doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = SDOSCWLOWESTWAVEHEIGHT548;
                doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = SDOSCWLOWESTWAVEHEIGHT648;
                doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = SDOSCWLOWESTWAVEHEIGHT748;
                doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = SDOSCWLOWESTWAVEHEIGHT172;
                doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = SDOSCWLOWESTWAVEHEIGHT272;
                doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = SDOSCWLOWESTWAVEHEIGHT372;
                doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = SDOSCWLOWESTWAVEHEIGHT472;
                doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = SDOSCWLOWESTWAVEHEIGHT572;
                doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = SDOSCWLOWESTWAVEHEIGHT672;
                doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = SDOSCWLOWESTWAVEHEIGHT772;

                doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = SDOSCWSURFACETEMPERATURE148;
                doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = SDOSCWSURFACETEMPERATURE248;
                doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = SDOSCWSURFACETEMPERATURE348;
                doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = SDOSCWSURFACETEMPERATURE448;
                doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = SDOSCWSURFACETEMPERATURE548;
                doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = SDOSCWSURFACETEMPERATURE648;
                doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = SDOSCWSURFACETEMPERATURE748;
                doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = SDOSCWSURFACETEMPERATURE172;
                doc.Bookmarks.get_Item(ref BookMark[54]).Range.Text = SDOSCWSURFACETEMPERATURE272;
                doc.Bookmarks.get_Item(ref BookMark[55]).Range.Text = SDOSCWSURFACETEMPERATURE372;
                doc.Bookmarks.get_Item(ref BookMark[56]).Range.Text = SDOSCWSURFACETEMPERATURE472;
                doc.Bookmarks.get_Item(ref BookMark[57]).Range.Text = SDOSCWSURFACETEMPERATURE572;
                doc.Bookmarks.get_Item(ref BookMark[58]).Range.Text = SDOSCWSURFACETEMPERATURE672;
                doc.Bookmarks.get_Item(ref BookMark[59]).Range.Text = SDOSCWSURFACETEMPERATURE772;


                //潮汐
                doc.Bookmarks.get_Item(ref BookMark[60]).Range.Text = FORECASTDATE1;
                doc.Bookmarks.get_Item(ref BookMark[61]).Range.Text = FORECASTDATE2;
                doc.Bookmarks.get_Item(ref BookMark[62]).Range.Text = FORECASTDATE3;
                doc.Bookmarks.get_Item(ref BookMark[63]).Range.Text = FORECASTDATE4;
                doc.Bookmarks.get_Item(ref BookMark[64]).Range.Text = FORECASTDATE5;
                doc.Bookmarks.get_Item(ref BookMark[65]).Range.Text = FORECASTDATE6;
                doc.Bookmarks.get_Item(ref BookMark[66]).Range.Text = FORECASTDATE7;
                doc.Bookmarks.get_Item(ref BookMark[67]).Range.Text = FORECASTDATE8;
                doc.Bookmarks.get_Item(ref BookMark[68]).Range.Text = FORECASTDATE9;
                doc.Bookmarks.get_Item(ref BookMark[69]).Range.Text = FORECASTDATE10;
                doc.Bookmarks.get_Item(ref BookMark[70]).Range.Text = FORECASTDATE11;
                doc.Bookmarks.get_Item(ref BookMark[71]).Range.Text = FORECASTDATE12;
                doc.Bookmarks.get_Item(ref BookMark[72]).Range.Text = FORECASTDATE13;
                doc.Bookmarks.get_Item(ref BookMark[73]).Range.Text = FORECASTDATE14;
                doc.Bookmarks.get_Item(ref BookMark[74]).Range.Text = FORECASTDATE15;
                doc.Bookmarks.get_Item(ref BookMark[75]).Range.Text = FORECASTDATE16;
                doc.Bookmarks.get_Item(ref BookMark[76]).Range.Text = FORECASTDATE17;
                doc.Bookmarks.get_Item(ref BookMark[77]).Range.Text = FORECASTDATE18;
                doc.Bookmarks.get_Item(ref BookMark[78]).Range.Text = FORECASTDATE19;
                doc.Bookmarks.get_Item(ref BookMark[79]).Range.Text = FORECASTDATE20;
                doc.Bookmarks.get_Item(ref BookMark[80]).Range.Text = FORECASTDATE21;
                doc.Bookmarks.get_Item(ref BookMark[81]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR1;
                doc.Bookmarks.get_Item(ref BookMark[82]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR2;
                doc.Bookmarks.get_Item(ref BookMark[83]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR3;
                doc.Bookmarks.get_Item(ref BookMark[84]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR4;
                doc.Bookmarks.get_Item(ref BookMark[85]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR5;
                doc.Bookmarks.get_Item(ref BookMark[86]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR6;
                doc.Bookmarks.get_Item(ref BookMark[87]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR7;
                doc.Bookmarks.get_Item(ref BookMark[89]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR8;
                doc.Bookmarks.get_Item(ref BookMark[90]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR9;
                doc.Bookmarks.get_Item(ref BookMark[91]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR10;
                doc.Bookmarks.get_Item(ref BookMark[92]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR11;
                doc.Bookmarks.get_Item(ref BookMark[93]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR12;
                doc.Bookmarks.get_Item(ref BookMark[94]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR13;
                doc.Bookmarks.get_Item(ref BookMark[95]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR14;
                doc.Bookmarks.get_Item(ref BookMark[96]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR15;
                doc.Bookmarks.get_Item(ref BookMark[97]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR16;
                doc.Bookmarks.get_Item(ref BookMark[98]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR17;
                doc.Bookmarks.get_Item(ref BookMark[99]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR18;
                doc.Bookmarks.get_Item(ref BookMark[100]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR19;
                doc.Bookmarks.get_Item(ref BookMark[101]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR20;
                doc.Bookmarks.get_Item(ref BookMark[102]).Range.Text = SDOSCTFIRSTHIGHWAVEHOUR21;
                doc.Bookmarks.get_Item(ref BookMark[103]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE1;
                doc.Bookmarks.get_Item(ref BookMark[104]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE2;
                doc.Bookmarks.get_Item(ref BookMark[105]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE3;
                doc.Bookmarks.get_Item(ref BookMark[106]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE4;
                doc.Bookmarks.get_Item(ref BookMark[107]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE5;
                doc.Bookmarks.get_Item(ref BookMark[108]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE6;
                doc.Bookmarks.get_Item(ref BookMark[109]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE7;
                doc.Bookmarks.get_Item(ref BookMark[110]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE8;
                doc.Bookmarks.get_Item(ref BookMark[111]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE9;
                doc.Bookmarks.get_Item(ref BookMark[112]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE10;
                doc.Bookmarks.get_Item(ref BookMark[113]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE11;
                doc.Bookmarks.get_Item(ref BookMark[114]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE12;
                doc.Bookmarks.get_Item(ref BookMark[115]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE13;
                doc.Bookmarks.get_Item(ref BookMark[116]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE14;
                doc.Bookmarks.get_Item(ref BookMark[117]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE15;
                doc.Bookmarks.get_Item(ref BookMark[118]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE16;
                doc.Bookmarks.get_Item(ref BookMark[119]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE17;
                doc.Bookmarks.get_Item(ref BookMark[120]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE18;
                doc.Bookmarks.get_Item(ref BookMark[121]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE19;
                doc.Bookmarks.get_Item(ref BookMark[122]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE20;
                doc.Bookmarks.get_Item(ref BookMark[123]).Range.Text = SDOSCTFIRSTHIGHWAVEMINUTE21;
                doc.Bookmarks.get_Item(ref BookMark[124]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR1;
                doc.Bookmarks.get_Item(ref BookMark[125]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR2;
                doc.Bookmarks.get_Item(ref BookMark[126]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR3;
                doc.Bookmarks.get_Item(ref BookMark[127]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR4;
                doc.Bookmarks.get_Item(ref BookMark[128]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR5;
                doc.Bookmarks.get_Item(ref BookMark[129]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR6;
                doc.Bookmarks.get_Item(ref BookMark[130]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR7;
                doc.Bookmarks.get_Item(ref BookMark[131]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR8;
                doc.Bookmarks.get_Item(ref BookMark[132]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR9;
                doc.Bookmarks.get_Item(ref BookMark[133]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR10;
                doc.Bookmarks.get_Item(ref BookMark[134]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR11;
                doc.Bookmarks.get_Item(ref BookMark[135]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR12;
                doc.Bookmarks.get_Item(ref BookMark[136]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR13;
                doc.Bookmarks.get_Item(ref BookMark[137]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR14;
                doc.Bookmarks.get_Item(ref BookMark[138]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR15;
                doc.Bookmarks.get_Item(ref BookMark[139]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR16;
                doc.Bookmarks.get_Item(ref BookMark[140]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR17;
                doc.Bookmarks.get_Item(ref BookMark[141]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR18;
                doc.Bookmarks.get_Item(ref BookMark[142]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR19;
                doc.Bookmarks.get_Item(ref BookMark[143]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR20;
                doc.Bookmarks.get_Item(ref BookMark[144]).Range.Text = SDOSCTSECONDHIGHWAVEHOUR21;
                doc.Bookmarks.get_Item(ref BookMark[145]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE1;
                doc.Bookmarks.get_Item(ref BookMark[146]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE2;
                doc.Bookmarks.get_Item(ref BookMark[147]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE3;
                doc.Bookmarks.get_Item(ref BookMark[148]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE4;
                doc.Bookmarks.get_Item(ref BookMark[149]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE5;
                doc.Bookmarks.get_Item(ref BookMark[150]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE6;
                doc.Bookmarks.get_Item(ref BookMark[151]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE7;
                doc.Bookmarks.get_Item(ref BookMark[152]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE8;
                doc.Bookmarks.get_Item(ref BookMark[153]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE9;
                doc.Bookmarks.get_Item(ref BookMark[154]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE10;
                doc.Bookmarks.get_Item(ref BookMark[155]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE11;
                doc.Bookmarks.get_Item(ref BookMark[156]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE12;
                doc.Bookmarks.get_Item(ref BookMark[157]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE13;
                doc.Bookmarks.get_Item(ref BookMark[158]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE14;
                doc.Bookmarks.get_Item(ref BookMark[159]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE15;
                doc.Bookmarks.get_Item(ref BookMark[160]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE16;
                doc.Bookmarks.get_Item(ref BookMark[161]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE17;
                doc.Bookmarks.get_Item(ref BookMark[162]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE18;
                doc.Bookmarks.get_Item(ref BookMark[163]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE19;
                doc.Bookmarks.get_Item(ref BookMark[164]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE20;
                doc.Bookmarks.get_Item(ref BookMark[165]).Range.Text = SDOSCTSECONDHIGHWAVEMINUTE21;
                doc.Bookmarks.get_Item(ref BookMark[166]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR1;
                doc.Bookmarks.get_Item(ref BookMark[167]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR2;
                doc.Bookmarks.get_Item(ref BookMark[168]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR3;
                doc.Bookmarks.get_Item(ref BookMark[169]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR4;
                doc.Bookmarks.get_Item(ref BookMark[170]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR5;
                doc.Bookmarks.get_Item(ref BookMark[171]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR6;
                doc.Bookmarks.get_Item(ref BookMark[172]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR7;
                doc.Bookmarks.get_Item(ref BookMark[173]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR8;
                doc.Bookmarks.get_Item(ref BookMark[174]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR9;
                doc.Bookmarks.get_Item(ref BookMark[175]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR10;
                doc.Bookmarks.get_Item(ref BookMark[176]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR11;
                doc.Bookmarks.get_Item(ref BookMark[177]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR12;
                doc.Bookmarks.get_Item(ref BookMark[178]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR13;
                doc.Bookmarks.get_Item(ref BookMark[179]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR14;
                doc.Bookmarks.get_Item(ref BookMark[180]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR15;
                doc.Bookmarks.get_Item(ref BookMark[181]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR16;
                doc.Bookmarks.get_Item(ref BookMark[182]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR17;
                doc.Bookmarks.get_Item(ref BookMark[183]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR18;
                doc.Bookmarks.get_Item(ref BookMark[184]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR19;
                doc.Bookmarks.get_Item(ref BookMark[185]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR20;
                doc.Bookmarks.get_Item(ref BookMark[186]).Range.Text = SDOSCTFIRSTLOWWAVEHOUR21;
                doc.Bookmarks.get_Item(ref BookMark[187]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE1;
                doc.Bookmarks.get_Item(ref BookMark[188]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE2;
                doc.Bookmarks.get_Item(ref BookMark[189]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE3;
                doc.Bookmarks.get_Item(ref BookMark[190]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE4;
                doc.Bookmarks.get_Item(ref BookMark[191]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE5;
                doc.Bookmarks.get_Item(ref BookMark[192]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE6;
                doc.Bookmarks.get_Item(ref BookMark[193]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE7;
                doc.Bookmarks.get_Item(ref BookMark[194]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE8;
                doc.Bookmarks.get_Item(ref BookMark[195]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE9;
                doc.Bookmarks.get_Item(ref BookMark[196]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE10;
                doc.Bookmarks.get_Item(ref BookMark[197]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE11;
                doc.Bookmarks.get_Item(ref BookMark[198]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE12;
                doc.Bookmarks.get_Item(ref BookMark[199]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE13;
                doc.Bookmarks.get_Item(ref BookMark[200]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE14;
                doc.Bookmarks.get_Item(ref BookMark[201]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE15;
                doc.Bookmarks.get_Item(ref BookMark[202]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE16;
                doc.Bookmarks.get_Item(ref BookMark[203]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE17;
                doc.Bookmarks.get_Item(ref BookMark[204]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE18;
                doc.Bookmarks.get_Item(ref BookMark[205]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE19;
                doc.Bookmarks.get_Item(ref BookMark[206]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE20;
                doc.Bookmarks.get_Item(ref BookMark[207]).Range.Text = SDOSCTFIRSTLOWWAVEMINUTE21;
                doc.Bookmarks.get_Item(ref BookMark[208]).Range.Text = SDOSCTSECONDLOWWAVEHOUR1;
                doc.Bookmarks.get_Item(ref BookMark[209]).Range.Text = SDOSCTSECONDLOWWAVEHOUR2;
                doc.Bookmarks.get_Item(ref BookMark[210]).Range.Text = SDOSCTSECONDLOWWAVEHOUR3;
                doc.Bookmarks.get_Item(ref BookMark[211]).Range.Text = SDOSCTSECONDLOWWAVEHOUR4;
                doc.Bookmarks.get_Item(ref BookMark[212]).Range.Text = SDOSCTSECONDLOWWAVEHOUR5;
                doc.Bookmarks.get_Item(ref BookMark[213]).Range.Text = SDOSCTSECONDLOWWAVEHOUR6;
                doc.Bookmarks.get_Item(ref BookMark[214]).Range.Text = SDOSCTSECONDLOWWAVEHOUR7;
                doc.Bookmarks.get_Item(ref BookMark[215]).Range.Text = SDOSCTSECONDLOWWAVEHOUR8;
                doc.Bookmarks.get_Item(ref BookMark[216]).Range.Text = SDOSCTSECONDLOWWAVEHOUR9;
                doc.Bookmarks.get_Item(ref BookMark[217]).Range.Text = SDOSCTSECONDLOWWAVEHOUR10;
                doc.Bookmarks.get_Item(ref BookMark[218]).Range.Text = SDOSCTSECONDLOWWAVEHOUR11;
                doc.Bookmarks.get_Item(ref BookMark[219]).Range.Text = SDOSCTSECONDLOWWAVEHOUR12;
                doc.Bookmarks.get_Item(ref BookMark[220]).Range.Text = SDOSCTSECONDLOWWAVEHOUR13;
                doc.Bookmarks.get_Item(ref BookMark[221]).Range.Text = SDOSCTSECONDLOWWAVEHOUR14;
                doc.Bookmarks.get_Item(ref BookMark[222]).Range.Text = SDOSCTSECONDLOWWAVEHOUR15;
                doc.Bookmarks.get_Item(ref BookMark[223]).Range.Text = SDOSCTSECONDLOWWAVEHOUR16;
                doc.Bookmarks.get_Item(ref BookMark[224]).Range.Text = SDOSCTSECONDLOWWAVEHOUR17;
                doc.Bookmarks.get_Item(ref BookMark[225]).Range.Text = SDOSCTSECONDLOWWAVEHOUR18;
                doc.Bookmarks.get_Item(ref BookMark[226]).Range.Text = SDOSCTSECONDLOWWAVEHOUR19;
                doc.Bookmarks.get_Item(ref BookMark[227]).Range.Text = SDOSCTSECONDLOWWAVEHOUR20;
                doc.Bookmarks.get_Item(ref BookMark[228]).Range.Text = SDOSCTSECONDLOWWAVEHOUR21;
                doc.Bookmarks.get_Item(ref BookMark[229]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE1;
                doc.Bookmarks.get_Item(ref BookMark[230]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE2;
                doc.Bookmarks.get_Item(ref BookMark[231]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE3;
                doc.Bookmarks.get_Item(ref BookMark[232]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE4;
                doc.Bookmarks.get_Item(ref BookMark[233]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE5;
                doc.Bookmarks.get_Item(ref BookMark[234]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE6;
                doc.Bookmarks.get_Item(ref BookMark[235]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE7;
                doc.Bookmarks.get_Item(ref BookMark[236]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE8;
                doc.Bookmarks.get_Item(ref BookMark[237]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE9;
                doc.Bookmarks.get_Item(ref BookMark[238]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE10;
                doc.Bookmarks.get_Item(ref BookMark[239]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE11;
                doc.Bookmarks.get_Item(ref BookMark[240]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE12;
                doc.Bookmarks.get_Item(ref BookMark[241]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE13;
                doc.Bookmarks.get_Item(ref BookMark[242]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE14;
                doc.Bookmarks.get_Item(ref BookMark[243]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE15;
                doc.Bookmarks.get_Item(ref BookMark[244]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE16;
                doc.Bookmarks.get_Item(ref BookMark[245]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE17;
                doc.Bookmarks.get_Item(ref BookMark[246]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE18;
                doc.Bookmarks.get_Item(ref BookMark[247]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE19;
                doc.Bookmarks.get_Item(ref BookMark[248]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE20;
                doc.Bookmarks.get_Item(ref BookMark[249]).Range.Text = SDOSCTSECONDLOWWAVEMINUTE21;
                doc.Bookmarks.get_Item(ref BookMark[250]).Range.Text = FIRSTHIGHWAVETIDEDATA1;
                doc.Bookmarks.get_Item(ref BookMark[251]).Range.Text = FIRSTHIGHWAVETIDEDATA2;
                doc.Bookmarks.get_Item(ref BookMark[252]).Range.Text = FIRSTHIGHWAVETIDEDATA3;
                doc.Bookmarks.get_Item(ref BookMark[253]).Range.Text = FIRSTHIGHWAVETIDEDATA4;
                doc.Bookmarks.get_Item(ref BookMark[254]).Range.Text = FIRSTHIGHWAVETIDEDATA5;
                doc.Bookmarks.get_Item(ref BookMark[255]).Range.Text = FIRSTHIGHWAVETIDEDATA6;
                doc.Bookmarks.get_Item(ref BookMark[256]).Range.Text = FIRSTHIGHWAVETIDEDATA7;
                doc.Bookmarks.get_Item(ref BookMark[257]).Range.Text = FIRSTHIGHWAVETIDEDATA8;
                doc.Bookmarks.get_Item(ref BookMark[258]).Range.Text = FIRSTHIGHWAVETIDEDATA9;
                doc.Bookmarks.get_Item(ref BookMark[259]).Range.Text = FIRSTHIGHWAVETIDEDATA10;
                doc.Bookmarks.get_Item(ref BookMark[260]).Range.Text = FIRSTHIGHWAVETIDEDATA11;
                doc.Bookmarks.get_Item(ref BookMark[261]).Range.Text = FIRSTHIGHWAVETIDEDATA12;
                doc.Bookmarks.get_Item(ref BookMark[262]).Range.Text = FIRSTHIGHWAVETIDEDATA13;
                doc.Bookmarks.get_Item(ref BookMark[263]).Range.Text = FIRSTHIGHWAVETIDEDATA14;
                doc.Bookmarks.get_Item(ref BookMark[264]).Range.Text = FIRSTHIGHWAVETIDEDATA15;
                doc.Bookmarks.get_Item(ref BookMark[265]).Range.Text = FIRSTHIGHWAVETIDEDATA16;
                doc.Bookmarks.get_Item(ref BookMark[266]).Range.Text = FIRSTHIGHWAVETIDEDATA17;
                doc.Bookmarks.get_Item(ref BookMark[267]).Range.Text = FIRSTHIGHWAVETIDEDATA18;
                doc.Bookmarks.get_Item(ref BookMark[268]).Range.Text = FIRSTHIGHWAVETIDEDATA19;
                doc.Bookmarks.get_Item(ref BookMark[269]).Range.Text = FIRSTHIGHWAVETIDEDATA20;
                doc.Bookmarks.get_Item(ref BookMark[270]).Range.Text = FIRSTHIGHWAVETIDEDATA21;
                doc.Bookmarks.get_Item(ref BookMark[271]).Range.Text = FIRSTLOWWAVETIDEDATA1;
                doc.Bookmarks.get_Item(ref BookMark[272]).Range.Text = FIRSTLOWWAVETIDEDATA2;
                doc.Bookmarks.get_Item(ref BookMark[273]).Range.Text = FIRSTLOWWAVETIDEDATA3;
                doc.Bookmarks.get_Item(ref BookMark[274]).Range.Text = FIRSTLOWWAVETIDEDATA4;
                doc.Bookmarks.get_Item(ref BookMark[275]).Range.Text = FIRSTLOWWAVETIDEDATA5;
                doc.Bookmarks.get_Item(ref BookMark[276]).Range.Text = FIRSTLOWWAVETIDEDATA6;
                doc.Bookmarks.get_Item(ref BookMark[277]).Range.Text = FIRSTLOWWAVETIDEDATA7;
                doc.Bookmarks.get_Item(ref BookMark[278]).Range.Text = FIRSTLOWWAVETIDEDATA8;
                doc.Bookmarks.get_Item(ref BookMark[279]).Range.Text = FIRSTLOWWAVETIDEDATA9;
                doc.Bookmarks.get_Item(ref BookMark[280]).Range.Text = FIRSTLOWWAVETIDEDATA10;
                doc.Bookmarks.get_Item(ref BookMark[281]).Range.Text = FIRSTLOWWAVETIDEDATA11;
                doc.Bookmarks.get_Item(ref BookMark[282]).Range.Text = FIRSTLOWWAVETIDEDATA12;
                doc.Bookmarks.get_Item(ref BookMark[283]).Range.Text = FIRSTLOWWAVETIDEDATA13;
                doc.Bookmarks.get_Item(ref BookMark[284]).Range.Text = FIRSTLOWWAVETIDEDATA14;
                doc.Bookmarks.get_Item(ref BookMark[285]).Range.Text = FIRSTLOWWAVETIDEDATA15;
                doc.Bookmarks.get_Item(ref BookMark[286]).Range.Text = FIRSTLOWWAVETIDEDATA16;
                doc.Bookmarks.get_Item(ref BookMark[287]).Range.Text = FIRSTLOWWAVETIDEDATA17;
                doc.Bookmarks.get_Item(ref BookMark[288]).Range.Text = FIRSTLOWWAVETIDEDATA18;
                doc.Bookmarks.get_Item(ref BookMark[289]).Range.Text = FIRSTLOWWAVETIDEDATA19;
                doc.Bookmarks.get_Item(ref BookMark[290]).Range.Text = FIRSTLOWWAVETIDEDATA20;
                doc.Bookmarks.get_Item(ref BookMark[291]).Range.Text = FIRSTLOWWAVETIDEDATA21;
                doc.Bookmarks.get_Item(ref BookMark[292]).Range.Text = SECONDHIGHWAVETIDEDATA1;
                doc.Bookmarks.get_Item(ref BookMark[293]).Range.Text = SECONDHIGHWAVETIDEDATA2;
                doc.Bookmarks.get_Item(ref BookMark[294]).Range.Text = SECONDHIGHWAVETIDEDATA3;
                doc.Bookmarks.get_Item(ref BookMark[295]).Range.Text = SECONDHIGHWAVETIDEDATA4;
                doc.Bookmarks.get_Item(ref BookMark[296]).Range.Text = SECONDHIGHWAVETIDEDATA5;
                doc.Bookmarks.get_Item(ref BookMark[297]).Range.Text = SECONDHIGHWAVETIDEDATA6;
                doc.Bookmarks.get_Item(ref BookMark[298]).Range.Text = SECONDHIGHWAVETIDEDATA7;
                doc.Bookmarks.get_Item(ref BookMark[299]).Range.Text = SECONDHIGHWAVETIDEDATA8;
                doc.Bookmarks.get_Item(ref BookMark[300]).Range.Text = SECONDHIGHWAVETIDEDATA9;
                doc.Bookmarks.get_Item(ref BookMark[301]).Range.Text = SECONDHIGHWAVETIDEDATA10;
                doc.Bookmarks.get_Item(ref BookMark[302]).Range.Text = SECONDHIGHWAVETIDEDATA11;
                doc.Bookmarks.get_Item(ref BookMark[303]).Range.Text = SECONDHIGHWAVETIDEDATA12;
                doc.Bookmarks.get_Item(ref BookMark[304]).Range.Text = SECONDHIGHWAVETIDEDATA13;
                doc.Bookmarks.get_Item(ref BookMark[305]).Range.Text = SECONDHIGHWAVETIDEDATA14;
                doc.Bookmarks.get_Item(ref BookMark[306]).Range.Text = SECONDHIGHWAVETIDEDATA15;
                doc.Bookmarks.get_Item(ref BookMark[307]).Range.Text = SECONDHIGHWAVETIDEDATA16;
                doc.Bookmarks.get_Item(ref BookMark[308]).Range.Text = SECONDHIGHWAVETIDEDATA17;
                doc.Bookmarks.get_Item(ref BookMark[309]).Range.Text = SECONDHIGHWAVETIDEDATA18;
                doc.Bookmarks.get_Item(ref BookMark[310]).Range.Text = SECONDHIGHWAVETIDEDATA19;
                doc.Bookmarks.get_Item(ref BookMark[311]).Range.Text = SECONDHIGHWAVETIDEDATA20;
                doc.Bookmarks.get_Item(ref BookMark[312]).Range.Text = SECONDHIGHWAVETIDEDATA21;
                doc.Bookmarks.get_Item(ref BookMark[313]).Range.Text = SECONDLOWWAVETIDEDATA1;
                doc.Bookmarks.get_Item(ref BookMark[314]).Range.Text = SECONDLOWWAVETIDEDATA2;
                doc.Bookmarks.get_Item(ref BookMark[315]).Range.Text = SECONDLOWWAVETIDEDATA3;
                doc.Bookmarks.get_Item(ref BookMark[316]).Range.Text = SECONDLOWWAVETIDEDATA4;
                doc.Bookmarks.get_Item(ref BookMark[317]).Range.Text = SECONDLOWWAVETIDEDATA5;
                doc.Bookmarks.get_Item(ref BookMark[318]).Range.Text = SECONDLOWWAVETIDEDATA6;
                doc.Bookmarks.get_Item(ref BookMark[319]).Range.Text = SECONDLOWWAVETIDEDATA7;
                doc.Bookmarks.get_Item(ref BookMark[320]).Range.Text = SECONDLOWWAVETIDEDATA8;
                doc.Bookmarks.get_Item(ref BookMark[321]).Range.Text = SECONDLOWWAVETIDEDATA9;
                doc.Bookmarks.get_Item(ref BookMark[322]).Range.Text = SECONDLOWWAVETIDEDATA10;
                doc.Bookmarks.get_Item(ref BookMark[323]).Range.Text = SECONDLOWWAVETIDEDATA11;
                doc.Bookmarks.get_Item(ref BookMark[324]).Range.Text = SECONDLOWWAVETIDEDATA12;
                doc.Bookmarks.get_Item(ref BookMark[325]).Range.Text = SECONDLOWWAVETIDEDATA13;
                doc.Bookmarks.get_Item(ref BookMark[326]).Range.Text = SECONDLOWWAVETIDEDATA14;
                doc.Bookmarks.get_Item(ref BookMark[327]).Range.Text = SECONDLOWWAVETIDEDATA15;
                doc.Bookmarks.get_Item(ref BookMark[328]).Range.Text = SECONDLOWWAVETIDEDATA16;
                doc.Bookmarks.get_Item(ref BookMark[329]).Range.Text = SECONDLOWWAVETIDEDATA17;
                doc.Bookmarks.get_Item(ref BookMark[330]).Range.Text = SECONDLOWWAVETIDEDATA18;
                doc.Bookmarks.get_Item(ref BookMark[331]).Range.Text = SECONDLOWWAVETIDEDATA19;
                doc.Bookmarks.get_Item(ref BookMark[332]).Range.Text = SECONDLOWWAVETIDEDATA20;
                doc.Bookmarks.get_Item(ref BookMark[333]).Range.Text = SECONDLOWWAVETIDEDATA21;
                doc.Bookmarks.get_Item(ref BookMark[334]).Range.Text = tblfooter_Model.FTIDALFORECASTER;

                doc.Bookmarks.get_Item(ref BookMark[335]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
                doc.Bookmarks.get_Item(ref BookMark[336]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
                doc.Bookmarks.get_Item(ref BookMark[337]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;

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
}