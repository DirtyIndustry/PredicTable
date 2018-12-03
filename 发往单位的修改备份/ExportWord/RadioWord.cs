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
    /// <summary>
    /// 北海视频文件
    /// </summary>
    public class RadioWord
    {
        //**********北海区************
        
        string ESASWHIGHTESTWAVEHEIGHT1 = "";
        string ESASWHIGHTESTWAVEHEIGHT2= "";
        string ESASWHIGHTESTWAVEHEIGHT3 = "";
        string ESASWHIGHTESTWAVEHEIGHT4 = "";

        string ESASWLOWESTWAVEHEIGHT1 = "";
        string ESASWLOWESTWAVEHEIGHT2 = "";
        string ESASWLOWESTWAVEHEIGHT3 = "";
        string ESASWLOWESTWAVEHEIGHT4 = "";

        string ESASWWAVETYPE1 = "";
        string ESASWWAVETYPE2 = "";
        string ESASWWAVETYPE3 = "";
        string ESASWWAVETYPE4 = "";

        //********七地市**********
        //海浪、海温
        string SDOSCWLOWESTWAVEHEIGHT1 = "";
        string SDOSCWLOWESTWAVEHEIGHT2 = "";
        string SDOSCWLOWESTWAVEHEIGHT3 = "";
        string SDOSCWLOWESTWAVEHEIGHT4 = "";
        string SDOSCWLOWESTWAVEHEIGHT5 = "";
        string SDOSCWSURFACETEMPERATURE1 = "";
        string SDOSCWSURFACETEMPERATURE2 = "";
        string SDOSCWSURFACETEMPERATURE3 = "";
        string SDOSCWSURFACETEMPERATURE4 = "";
        string SDOSCWSURFACETEMPERATURE5 = "";
        
        //潮汐潮时
        string FIRSTHIGHTIME1 = "";
        string SECONDHIGHTIME1 = "";
        string FIRSTLOWTIME1 = "";
        string SECONDLOWTIME1 = "";
        string FIRSTHIGHTIME2 = "";
        string SECONDHIGHTIME2 = "";
        string FIRSTLOWTIME2 = "";
        string SECONDLOWTIME2 = "";
        string FIRSTHIGHTIME3 = "";
        string SECONDHIGHTIME3 = "";
        string FIRSTLOWTIME3 = "";
        string SECONDLOWTIME3 = "";
        string FIRSTHIGHTIME4 = "";
        string SECONDHIGHTIME4 = "";
        string FIRSTLOWTIME4 = "";
        string SECONDLOWTIME4 = "";
        string FIRSTHIGHTIME5 = "";
        string SECONDHIGHTIME5 = "";
        string FIRSTLOWTIME5 = "";
        string SECONDLOWTIME5 = "";
        //潮汐数据潮高
        //第一次高潮潮高
        string FIRSTHIGHWAVETIDEDATA1 = "";
        string FIRSTHIGHWAVETIDEDATA2 = "";
        string FIRSTHIGHWAVETIDEDATA3 = "";
        string FIRSTHIGHWAVETIDEDATA4 = "";
        string FIRSTHIGHWAVETIDEDATA5 = "";
        //第二次高潮潮高
        string SECONDHIGHWAVETIDEDATA1 = "";
        string SECONDHIGHWAVETIDEDATA2 = "";
        string SECONDHIGHWAVETIDEDATA3 = "";
        string SECONDHIGHWAVETIDEDATA4 = "";
        string SECONDHIGHWAVETIDEDATA5 = "";
        //第一次低潮潮高
        string FIRSTLOWWAVETIDEDATA1 = "";
        string FIRSTLOWWAVETIDEDATA2 = "";
        string FIRSTLOWWAVETIDEDATA3 = "";
        string FIRSTLOWWAVETIDEDATA4 = "";
        string FIRSTLOWWAVETIDEDATA5 = "";
        //第二次低潮潮高
        string SECONDLOWWAVETIDEDATA1 = "";
        string SECONDLOWWAVETIDEDATA2 = "";
        string SECONDLOWWAVETIDEDATA3 = "";
        string SECONDLOWWAVETIDEDATA4 = "";
        string SECONDLOWWAVETIDEDATA5 = "";


        //********大连、天津、秦皇岛**********
        //海浪、海温
        string SDOSCWLOWESTWAVEHEIGHT6 = "";
        string SDOSCWLOWESTWAVEHEIGHT7 = "";
        string SDOSCWLOWESTWAVEHEIGHT8 = ""; 

        string SDOSCWSURFACETEMPERATURE6 = "";
        string SDOSCWSURFACETEMPERATURE7 = "";
        string SDOSCWSURFACETEMPERATURE8 = "";

        //潮汐潮时
        string FIRSTHIGHTIME6 = "";
        string SECONDHIGHTIME6 = "";
        string FIRSTLOWTIME6 = "";
        string SECONDLOWTIME6 = "";
        string FIRSTHIGHTIME7 = "";
        string SECONDHIGHTIME7 = "";
        string FIRSTLOWTIME7 = "";
        string SECONDLOWTIME7 = "";
        string FIRSTHIGHTIME8 = "";
        string SECONDHIGHTIME8 = "";
        string FIRSTLOWTIME8 = "";
        string SECONDLOWTIME8 = "";
        //潮汐数据潮高
        //第一次高潮潮高
        string FIRSTHIGHWAVETIDEDATA6 = "";
        string FIRSTHIGHWAVETIDEDATA7 = "";
        string FIRSTHIGHWAVETIDEDATA8 = "";
        //第二次高潮潮高
        string SECONDHIGHWAVETIDEDATA6 = "";
        string SECONDHIGHWAVETIDEDATA7 = "";
        string SECONDHIGHWAVETIDEDATA8 = "";
        //第一次低潮潮高
        string FIRSTLOWWAVETIDEDATA6 = "";
        string FIRSTLOWWAVETIDEDATA7 = "";
        string FIRSTLOWWAVETIDEDATA8 = "";
        //第二次低潮潮高
        string SECONDLOWWAVETIDEDATA6 = "";
        string SECONDLOWWAVETIDEDATA7 = "";
        string SECONDLOWWAVETIDEDATA8 = "";

        #region
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

                //北海区
                sql_TBLEACHSEAAREA24HSEAWAVE sql = new sql_TBLEACHSEAAREA24HSEAWAVE();
                TBLEACHSEAAREA24HSEAWAVE model = new TBLEACHSEAAREA24HSEAWAVE();
                model.PUBLISHDATE = dt;
                DataTable BHTable = (DataTable)sql.get_TBLEACHSEAAREA24HSEAWAVE_AllData(model);
                if(BHTable!=null && BHTable.Rows.Count > 0)
                {
                    for(int i=0;i< BHTable.Rows.Count; i++)
                    {
                        string ESASWAREA = BHTable.Rows[i]["ESASWAREA"].ToString();
                        if (ESASWAREA == "渤海") {
                            ESASWHIGHTESTWAVEHEIGHT1= BHTable.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                            ESASWLOWESTWAVEHEIGHT1 = BHTable.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                            ESASWWAVETYPE1 = BHTable.Rows[i]["ESASWWAVETYPE"].ToString().Contains("-")? BHTable.Rows[i]["ESASWWAVETYPE"].ToString().Replace('-','到'): BHTable.Rows[i]["ESASWWAVETYPE"].ToString();
                        }
                        else if (ESASWAREA == "黄海北部") {
                            ESASWHIGHTESTWAVEHEIGHT2 = BHTable.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                            ESASWLOWESTWAVEHEIGHT2 = BHTable.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                            ESASWWAVETYPE2 = BHTable.Rows[i]["ESASWWAVETYPE"].ToString().Contains("-") ? BHTable.Rows[i]["ESASWWAVETYPE"].ToString().Replace('-', '到') : BHTable.Rows[i]["ESASWWAVETYPE"].ToString();
                        }
                        else if (ESASWAREA == "黄海中部") {
                            ESASWHIGHTESTWAVEHEIGHT3 = BHTable.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                            ESASWLOWESTWAVEHEIGHT3 = BHTable.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                            ESASWWAVETYPE3 = BHTable.Rows[i]["ESASWWAVETYPE"].ToString().Contains("-") ? BHTable.Rows[i]["ESASWWAVETYPE"].ToString().Replace('-', '到') : BHTable.Rows[i]["ESASWWAVETYPE"].ToString();
                        }
                        else if (ESASWAREA == "黄海南部") {
                            ESASWHIGHTESTWAVEHEIGHT4 = BHTable.Rows[i]["ESASWHIGHTESTWAVEHEIGHT"].ToString();
                            ESASWLOWESTWAVEHEIGHT4 = BHTable.Rows[i]["ESASWLOWESTWAVEHEIGHT"].ToString();
                            ESASWWAVETYPE4 = BHTable.Rows[i]["ESASWWAVETYPE"].ToString().Contains("-") ? BHTable.Rows[i]["ESASWWAVETYPE"].ToString().Replace('-', '到') : BHTable.Rows[i]["ESASWWAVETYPE"].ToString();
                        }
                    }
                    object ESASWHIGHTESTWAVEHEIGHT11 = "ESASWHIGHTESTWAVEHEIGHT1";
                    doc.Bookmarks.get_Item(ref ESASWHIGHTESTWAVEHEIGHT11).Range.Text = ESASWHIGHTESTWAVEHEIGHT1;
                    object ESASWHIGHTESTWAVEHEIGHT22 = "ESASWHIGHTESTWAVEHEIGHT2";
                    doc.Bookmarks.get_Item(ref ESASWHIGHTESTWAVEHEIGHT22).Range.Text = ESASWHIGHTESTWAVEHEIGHT2;
                    object ESASWHIGHTESTWAVEHEIGHT33 = "ESASWHIGHTESTWAVEHEIGHT3";
                    doc.Bookmarks.get_Item(ref ESASWHIGHTESTWAVEHEIGHT33).Range.Text = ESASWHIGHTESTWAVEHEIGHT3;
                    object ESASWHIGHTESTWAVEHEIGHT44 = "ESASWHIGHTESTWAVEHEIGHT4";
                    doc.Bookmarks.get_Item(ref ESASWHIGHTESTWAVEHEIGHT44).Range.Text = ESASWHIGHTESTWAVEHEIGHT4;
                    object ESASWLOWESTWAVEHEIGHT11 = "ESASWLOWESTWAVEHEIGHT1";
                    doc.Bookmarks.get_Item(ref ESASWLOWESTWAVEHEIGHT11).Range.Text = ESASWLOWESTWAVEHEIGHT1;
                    object ESASWLOWESTWAVEHEIGHT22 = "ESASWLOWESTWAVEHEIGHT2";
                    doc.Bookmarks.get_Item(ref ESASWLOWESTWAVEHEIGHT22).Range.Text = ESASWLOWESTWAVEHEIGHT2;
                    object ESASWLOWESTWAVEHEIGHT33 = "ESASWLOWESTWAVEHEIGHT3";
                    doc.Bookmarks.get_Item(ref ESASWLOWESTWAVEHEIGHT33).Range.Text = ESASWLOWESTWAVEHEIGHT3;
                    object ESASWLOWESTWAVEHEIGHT44 = "ESASWLOWESTWAVEHEIGHT4";
                    doc.Bookmarks.get_Item(ref ESASWLOWESTWAVEHEIGHT44).Range.Text = ESASWLOWESTWAVEHEIGHT4;

                    object ESASWWAVETYPE11 = "ESASWWAVETYPE1";
                    doc.Bookmarks.get_Item(ref ESASWWAVETYPE11).Range.Text = ESASWWAVETYPE1;
                    object ESASWWAVETYPE22 = "ESASWWAVETYPE2";
                    doc.Bookmarks.get_Item(ref ESASWWAVETYPE22).Range.Text = ESASWWAVETYPE2;
                    object ESASWWAVETYPE33 = "ESASWWAVETYPE3";
                    doc.Bookmarks.get_Item(ref ESASWWAVETYPE33).Range.Text = ESASWWAVETYPE3;
                    object ESASWWAVETYPE44 = "ESASWWAVETYPE4";
                    doc.Bookmarks.get_Item(ref ESASWWAVETYPE44).Range.Text = ESASWWAVETYPE4;
                }
                else
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                
                //七地市海浪、海温数据
                TBLSDOFFSHORESEVENCITY24HWAVE tblsdoffshoresevencity24hwave_Model = new TBLSDOFFSHORESEVENCITY24HWAVE();
                tblsdoffshoresevencity24hwave_Model.PUBLISHDATE = dt;
                System.Data.DataTable tblsdoffshoresevencity24hwave = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HWAVE().get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(tblsdoffshoresevencity24hwave_Model);
                if (tblsdoffshoresevencity24hwave.Rows.Count == 0)
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                else
                {
                    for (int i = 0; i < tblsdoffshoresevencity24hwave.Rows.Count; i++)
                    {
                        if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "东营近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT1 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE1 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "潍坊近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT2 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE2 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "烟台近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT3 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE3 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "威海近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT4 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE4 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                        }
                        else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "青岛近海")
                        {
                            SDOSCWLOWESTWAVEHEIGHT5 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                            SDOSCWSURFACETEMPERATURE5 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                        }

                    }
                }


                //七地市潮汐潮时数据
                TBLSDOFFSHORESEVENCITY24HTIDE tblsdoffshoresevencity24htide_Model = new TBLSDOFFSHORESEVENCITY24HTIDE();
                tblsdoffshoresevencity24htide_Model.PUBLISHDATE = dt;
                System.Data.DataTable tblsdoffshoresevencity24htide = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HTIDE().get24TideData(tblsdoffshoresevencity24htide_Model);
                if (tblsdoffshoresevencity24htide.Rows.Count == 0)
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                else
                {
                    for (int i = 0; i < tblsdoffshoresevencity24htide.Rows.Count; i++)
                    {
                        string firstHighTime = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString() + tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        string secondHighTime = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString() + tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        string firstLowTime = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString() + tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        string secondLowTime = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString() + tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();

                        //判断
                        firstHighTime = firstHighTime.Contains("-") ? "—" : firstHighTime.Substring(0, 2) + "时" + firstHighTime.Substring(2, 2) + "分";
                        secondHighTime = secondHighTime.Contains("-") ? "—" : secondHighTime.Substring(0, 2) + "时" + secondHighTime.Substring(2, 2) + "分";
                        firstLowTime = firstLowTime.Contains("-") ? "—" : firstLowTime.Substring(0, 2) + "时" + firstLowTime.Substring(2, 2) + "分";
                        secondLowTime = secondLowTime.Contains("-") ? "—" : secondLowTime.Substring(0, 2) + "时" + secondLowTime.Substring(2, 2) + "分";

                        if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "东营")
                        {
                            FIRSTHIGHTIME1 = firstHighTime;
                            SECONDHIGHTIME1 = secondHighTime;
                            FIRSTLOWTIME1 = firstLowTime;
                            SECONDLOWTIME1 = secondLowTime;
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "潍坊")
                        {
                            FIRSTHIGHTIME2 = firstHighTime;
                            SECONDHIGHTIME2 = secondHighTime;
                            FIRSTLOWTIME2 = firstLowTime;
                            SECONDLOWTIME2 = secondLowTime;
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "烟台")
                        {
                            FIRSTHIGHTIME3 = firstHighTime;
                            SECONDHIGHTIME3 = secondHighTime;
                            FIRSTLOWTIME3 = firstLowTime;
                            SECONDLOWTIME3 = secondLowTime;
                        }
                       
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "威海")
                        {
                            FIRSTHIGHTIME4 = firstHighTime;
                            SECONDHIGHTIME4 = secondHighTime;
                            FIRSTLOWTIME4 = firstLowTime;
                            SECONDLOWTIME4 = secondLowTime;
                        }
                        else if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                        {
                            FIRSTHIGHTIME5 = firstHighTime;
                            SECONDHIGHTIME5 = secondHighTime;
                            FIRSTLOWTIME5 = firstLowTime;
                            SECONDLOWTIME5 = secondLowTime;
                        }
                    }
                }

                //七地市潮汐潮高数据
                sql_TideData sqlTideData = new sql_TideData();
                HT_TideData modelTideData = new HT_TideData();
                modelTideData.PUBLISHDATE = dt;
                DataTable tideData = (DataTable)sqlTideData.get24TideData(modelTideData);
                if (tideData != null && tideData.Rows.Count > 0)
                {
                    for (int i = 0; i < tideData.Rows.Count; i++)
                    {
                        if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "东营")
                        {
                            FIRSTHIGHWAVETIDEDATA1 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString().Contains("-")? "—" : tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                            FIRSTLOWWAVETIDEDATA1 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                            SECONDHIGHWAVETIDEDATA1 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                            SECONDLOWWAVETIDEDATA1 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "潍坊")
                        {
                            FIRSTHIGHWAVETIDEDATA2 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                            FIRSTLOWWAVETIDEDATA2 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                            SECONDHIGHWAVETIDEDATA2 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                            SECONDLOWWAVETIDEDATA2 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "烟台")
                        {
                            FIRSTHIGHWAVETIDEDATA3 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                            FIRSTLOWWAVETIDEDATA3 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                            SECONDHIGHWAVETIDEDATA3 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                            SECONDLOWWAVETIDEDATA3 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "威海")
                        {
                            FIRSTHIGHWAVETIDEDATA4 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                            FIRSTLOWWAVETIDEDATA4 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                            SECONDHIGHWAVETIDEDATA4 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                            SECONDLOWWAVETIDEDATA4 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                        }
                        else if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                        {

                            FIRSTHIGHWAVETIDEDATA5 = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                            FIRSTLOWWAVETIDEDATA5 = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                            SECONDHIGHWAVETIDEDATA5 = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                            SECONDLOWWAVETIDEDATA5 = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString().Contains("-") ? "—" : tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                        }
                    }
                }
                else
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                #endregion

                #region  天津、大连和秦皇岛   
                //************************海浪***************************
                //天津数据获取
                sql_RadioForecast reaio = new sql_RadioForecast();
                
                //大连
                DataTable dtDL = (DataTable)reaio.GetWave(dt, "大连");
                DataTable dtTJNewTable = (DataTable)reaio.GetTianJinData(dt);
                if (dtDL != null && dtDL.Rows.Count > 0)
                {
                    SDOSCWLOWESTWAVEHEIGHT6 = (dtDL.Rows[0]["LGCZXDZ"].ToString()==null|| dtDL.Rows[0]["LGCZXDZ"].ToString() == "") ? dtDL.Rows[0]["LGCZQSZ"].ToString() : dtDL.Rows[0]["LGCZQSZ"].ToString() + "到" + dtDL.Rows[0]["LGCZXDZ"].ToString();
                }
                else
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                
                //秦皇岛
                DataTable dtQHD = (DataTable)reaio.GetWave(dt, "秦皇岛市沿海海域");
                if (dtQHD != null && dtQHD.Rows.Count > 0)
                {
                    SDOSCWLOWESTWAVEHEIGHT7 = (dtQHD.Rows[0]["LGCZXDZ"].ToString() == null || dtQHD.Rows[0]["LGCZXDZ"].ToString() == "") ? dtQHD.Rows[0]["LGCZQSZ"].ToString() : dtQHD.Rows[0]["LGCZQSZ"].ToString() + "到" + dtQHD.Rows[0]["LGCZXDZ"].ToString();
                }
                else
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                //天津
                DataTable dtTJ = (DataTable)reaio.GetWave(dt, "天津");
                if (dtTJ != null && dtTJ.Rows.Count > 0)
                {
                    SDOSCWLOWESTWAVEHEIGHT8 = (dtTJ.Rows[0]["LGCZXDZ"].ToString() == null || dtTJ.Rows[0]["LGCZXDZ"].ToString() == "") ? dtTJ.Rows[0]["LGCZQSZ"].ToString() : dtTJ.Rows[0]["LGCZQSZ"].ToString() + "到" + dtTJ.Rows[0]["LGCZXDZ"].ToString();
                }
                else
                {
                    if(dtTJNewTable != null && dtTJNewTable.Rows.Count > 0)
                    {
                        if (dtTJNewTable.Rows[0]["WAVE"] != null && dtTJNewTable.Rows[0]["WAVE"].ToString() != "")
                        {
                            SDOSCWLOWESTWAVEHEIGHT8 = dtTJNewTable.Rows[0]["WAVE"].ToString();
                        }
                        else
                        {
                            doc.Close(true, ref missing, ref missing);
                            return 0;
                        }
                    }
                    else
                    {
                        doc.Close(true, ref missing, ref missing);
                        return 0;
                    }
                }
                //************************海温****************************
                //大连
                DataTable dtDLWater = (DataTable)reaio.GetWater(dt, "大连");
                if (dtDLWater != null && dtDLWater.Rows.Count > 0)
                {
                    SDOSCWSURFACETEMPERATURE6 = dtDLWater.Rows[0]["HWCZQSZ"].ToString();
                }
                else
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                //秦皇岛
                DataTable dtQHDWater = (DataTable)reaio.GetWater(dt, "秦皇岛市沿海海域");
                if (dtQHDWater != null && dtQHDWater.Rows.Count > 0)
                {
                    SDOSCWSURFACETEMPERATURE7 = dtQHDWater.Rows[0]["HWCZQSZ"].ToString();
                }
                else
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                //天津
                DataTable dtTJWater = (DataTable)reaio.GetWater(dt, "天津");
                if (dtTJWater != null && dtTJWater.Rows.Count > 0)
                {
                    SDOSCWSURFACETEMPERATURE8 = dtTJWater.Rows[0]["HWCZQSZ"].ToString();
                }
                else
                {
                    if (dtTJNewTable != null && dtTJNewTable.Rows.Count > 0)
                    {
                        if (dtTJNewTable.Rows[0]["SEA"] != null && dtTJNewTable.Rows[0]["SEA"].ToString() != "")
                        {
                            SDOSCWSURFACETEMPERATURE8 = dtTJNewTable.Rows[0]["SEA"].ToString();
                        }
                        else
                        {
                            doc.Close(true, ref missing, ref missing);
                            return 0;
                        }
                    }
                    else
                    {
                        doc.Close(true, ref missing, ref missing);
                        return 0;
                    }
                }
                //************************潮汐***************************
                //大连
                sql_dl dlsql = new sql_dl();
                DataSet dsDLTide = dlsql.GetTide("大连港",dt.ToString("yyyy-MM-dd"), dt.AddDays(1).ToString("yyyy-MM-dd"));
                if(dsDLTide !=null && dsDLTide.Tables.Count > 0)
                {
                    DataTable dtDLTide = dsDLTide.Tables[0];
                    if (dtDLTide != null && dtDLTide.Rows.Count > 0)
                    {
                        FIRSTHIGHTIME6 = dtDLTide.Rows[0]["FirstHighTime"] == null ? "" : dtDLTide.Rows[0]["FirstHighTime"].ToString().Replace(":", "");
                        SECONDHIGHTIME6 = dtDLTide.Rows[0]["SecondHighTime"] == null ? "" : dtDLTide.Rows[0]["SecondHighTime"].ToString().Replace(":", "");
                        FIRSTLOWTIME6 = dtDLTide.Rows[0]["FirstLowTime"] == null ? "" : dtDLTide.Rows[0]["FirstLowTime"].ToString().Replace(":", "");
                        SECONDLOWTIME6 = dtDLTide.Rows[0]["SecondLowTime"] == null ? "" : dtDLTide.Rows[0]["SecondLowTime"].ToString().Replace(":", "");

                        FIRSTHIGHTIME6 = FIRSTHIGHTIME6 == "" ? "—" : FIRSTHIGHTIME6.Contains("-") ? "—" : FIRSTHIGHTIME6.Substring(0, 2) + "时" + FIRSTHIGHTIME6.Substring(2, 2) + "分";
                        SECONDHIGHTIME6 = SECONDHIGHTIME6 == "" ? "—" : SECONDHIGHTIME6.Contains("-") ? "—" : SECONDHIGHTIME6.Substring(0, 2) + "时" + SECONDHIGHTIME6.Substring(2, 2) + "分";
                        FIRSTLOWTIME6 = FIRSTLOWTIME6 == "" ? "—" : FIRSTLOWTIME6.Contains("-") ? "—" : FIRSTLOWTIME6.Substring(0, 2) + "时" + FIRSTLOWTIME6.Substring(2, 2) + "分";
                        SECONDLOWTIME6 = SECONDLOWTIME6 == "" ? "—" : SECONDLOWTIME6.Contains("-") ? "—" : SECONDLOWTIME6.Substring(0, 2) + "时" + SECONDLOWTIME6.Substring(2, 2) + "分";

                        FIRSTHIGHWAVETIDEDATA6 = dtDLTide.Rows[0]["FirstHighHeight"] == null ? "" : dtDLTide.Rows[0]["FirstHighHeight"].ToString();
                        FIRSTLOWWAVETIDEDATA6 = dtDLTide.Rows[0]["FirstLowHeight"] == null ? "" : dtDLTide.Rows[0]["FirstLowHeight"].ToString();
                        SECONDHIGHWAVETIDEDATA6 = dtDLTide.Rows[0]["SecondHighHeight"] == null ? "" : dtDLTide.Rows[0]["SecondHighHeight"].ToString();
                        SECONDLOWWAVETIDEDATA6 = dtDLTide.Rows[0]["SecondLowHeigth"] == null ? "" : dtDLTide.Rows[0]["SecondLowHeigth"].ToString();

                        FIRSTHIGHWAVETIDEDATA6 = FIRSTHIGHWAVETIDEDATA6 == "" ? "—" : FIRSTHIGHWAVETIDEDATA6.Contains("-") ? "—" : FIRSTHIGHWAVETIDEDATA6;
                        FIRSTLOWWAVETIDEDATA6 = FIRSTLOWWAVETIDEDATA6 == "" ? "—" : FIRSTLOWWAVETIDEDATA6.Contains("-") ? "—" : FIRSTLOWWAVETIDEDATA6;
                        SECONDHIGHWAVETIDEDATA6 = SECONDHIGHWAVETIDEDATA6 == "" ? "—" : SECONDHIGHWAVETIDEDATA6.Contains("-") ? "—" : SECONDHIGHWAVETIDEDATA6;
                        SECONDLOWWAVETIDEDATA6 = SECONDLOWWAVETIDEDATA6 == "" ? "—" : SECONDLOWWAVETIDEDATA6.Contains("-") ? "—" : SECONDLOWWAVETIDEDATA6;
                    }
                    else
                    {
                        doc.Close(true, ref missing, ref missing);
                        return 0;
                    }
                }
                else
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                //DataTable dtDLTide = (DataTable)reaio.GetActualTide(dt, "大连");

                //秦皇岛
                DataTable dtQHDTide = (DataTable)reaio.GetActualTide(dt, "秦皇岛港");
                if (dtQHDTide != null && dtQHDTide.Rows.Count > 0)
                {
                    FIRSTHIGHTIME7 = dtQHDTide.Rows[0]["GCS1"] == null ? "" : dtQHDTide.Rows[0]["GCS1"].ToString().Replace(":", "");
                    SECONDHIGHTIME7 = dtQHDTide.Rows[0]["GCS2"] == null ? "" : dtQHDTide.Rows[0]["GCS2"].ToString().Replace(":", "");
                    FIRSTLOWTIME7 = dtQHDTide.Rows[0]["DCS1"] == null ? "" : dtQHDTide.Rows[0]["DCS1"].ToString().Replace(":", "");
                    SECONDLOWTIME7 = dtQHDTide.Rows[0]["DCS2"] == null ? "" : dtQHDTide.Rows[0]["DCS2"].ToString().Replace(":", "");

                    FIRSTHIGHTIME7 = FIRSTHIGHTIME7 == "" ? "—" : FIRSTHIGHTIME7.Contains("-") ? "—" : FIRSTHIGHTIME7.Substring(0, 2) + "时" + FIRSTHIGHTIME7.Substring(2, 2) + "分";
                    SECONDHIGHTIME7 = SECONDHIGHTIME7 == "" ? "—" : SECONDHIGHTIME7.Contains("-") ? "—" : SECONDHIGHTIME7.Substring(0, 2) + "时" + SECONDHIGHTIME7.Substring(2, 2) + "分";
                    FIRSTLOWTIME7 = FIRSTLOWTIME7 == "" ? "—" : FIRSTLOWTIME7.Contains("-") ? "—" : FIRSTLOWTIME7.Substring(0, 2) + "时" + FIRSTLOWTIME7.Substring(2, 2) + "分";
                    SECONDLOWTIME7 = SECONDLOWTIME7 == "" ? "—" : SECONDLOWTIME7.Contains("-") ? "—" : SECONDLOWTIME7.Substring(0, 2) + "时" + SECONDLOWTIME7.Substring(2, 2) + "分";

                    FIRSTHIGHWAVETIDEDATA7 = dtQHDTide.Rows[0]["GCW1"] == null ? "" : dtQHDTide.Rows[0]["GCW1"].ToString();
                    FIRSTLOWWAVETIDEDATA7 = dtQHDTide.Rows[0]["DCW1"] == null ? "" : dtQHDTide.Rows[0]["DCW1"].ToString();
                    SECONDHIGHWAVETIDEDATA7 = dtQHDTide.Rows[0]["GCW2"] == null ? "" : dtQHDTide.Rows[0]["GCW2"].ToString();
                    SECONDLOWWAVETIDEDATA7 = dtQHDTide.Rows[0]["DCW2"] == null ? "" : dtQHDTide.Rows[0]["DCW2"].ToString();

                    FIRSTHIGHWAVETIDEDATA7 = FIRSTHIGHWAVETIDEDATA7 == "" ? "—" : FIRSTHIGHWAVETIDEDATA7.Contains("-") ? "—" : FIRSTHIGHWAVETIDEDATA7;
                    FIRSTLOWWAVETIDEDATA7 = FIRSTLOWWAVETIDEDATA7 == "" ? "—" : FIRSTLOWWAVETIDEDATA7.Contains("-") ? "—" : FIRSTLOWWAVETIDEDATA7;
                    SECONDHIGHWAVETIDEDATA7 = SECONDHIGHWAVETIDEDATA7 == "" ? "—" : SECONDHIGHWAVETIDEDATA7.Contains("-") ? "—" : SECONDHIGHWAVETIDEDATA7;
                    SECONDLOWWAVETIDEDATA7 = SECONDLOWWAVETIDEDATA7 == "" ? "—" : SECONDLOWWAVETIDEDATA7.Contains("-") ? "—" : SECONDLOWWAVETIDEDATA7;
                }
                else
                {
                    doc.Close(true, ref missing, ref missing);
                    return 0;
                }
                //天津
                DataTable dtTJTide = (DataTable)reaio.GetTide(dt, "天津");
                if (dtTJTide != null && dtTJTide.Rows.Count > 0)
                {
                    FIRSTHIGHTIME8 = dtTJTide.Rows[0]["GCS1"] == null ? "" : dtTJTide.Rows[0]["GCS1"].ToString().Replace(":", "");
                    SECONDHIGHTIME8 = dtTJTide.Rows[0]["GCS2"] == null ? "" : dtTJTide.Rows[0]["GCS2"].ToString().Replace(":", "");
                    FIRSTLOWTIME8 = dtTJTide.Rows[0]["DCS1"] == null ? "" : dtTJTide.Rows[0]["DCS1"].ToString().Replace(":", "");
                    SECONDLOWTIME8 = dtTJTide.Rows[0]["DCS2"] == null ? "" : dtTJTide.Rows[0]["DCS2"].ToString().Replace(":", "");

                    FIRSTHIGHTIME8 = FIRSTHIGHTIME8 == "" ? "—" : FIRSTHIGHTIME8.Contains("-") ? "—" : FIRSTHIGHTIME8.Substring(0, 2) + "时" + FIRSTHIGHTIME8.Substring(2, 2) + "分";
                    SECONDHIGHTIME8 = SECONDHIGHTIME8 == "" ? "—" : SECONDHIGHTIME8.Contains("-") ? "—" : SECONDHIGHTIME8.Substring(0, 2) + "时" + SECONDHIGHTIME8.Substring(2, 2) + "分";
                    FIRSTLOWTIME8 = FIRSTLOWTIME8 == "" ? "—" : FIRSTLOWTIME8.Contains("-") ? "—" : FIRSTLOWTIME8.Substring(0, 2) + "时" + FIRSTLOWTIME8.Substring(2, 2) + "分";
                    SECONDLOWTIME8 = SECONDLOWTIME8 == "" ? "—" : SECONDLOWTIME8.Contains("-") ? "—" : SECONDLOWTIME8.Substring(0, 2) + "时" + SECONDLOWTIME8.Substring(2, 2) + "分";

                    FIRSTHIGHWAVETIDEDATA8 = dtTJTide.Rows[0]["GCW1"] == null ? "" : dtTJTide.Rows[0]["GCW1"].ToString();
                    FIRSTLOWWAVETIDEDATA8 = dtTJTide.Rows[0]["DCW1"] == null ? "" : dtTJTide.Rows[0]["DCW1"].ToString();
                    SECONDHIGHWAVETIDEDATA8 = dtTJTide.Rows[0]["GCW2"] == null ? "" : dtTJTide.Rows[0]["GCW2"].ToString();
                    SECONDLOWWAVETIDEDATA8 = dtTJTide.Rows[0]["DCW2"] == null ? "" : dtTJTide.Rows[0]["DCW2"].ToString();

                    FIRSTHIGHWAVETIDEDATA8 = FIRSTHIGHWAVETIDEDATA8 == "" ? "—" : FIRSTHIGHWAVETIDEDATA8.Contains("-") ? "—" : FIRSTHIGHWAVETIDEDATA8;
                    FIRSTLOWWAVETIDEDATA8 = FIRSTLOWWAVETIDEDATA8 == "" ? "—" : FIRSTLOWWAVETIDEDATA8.Contains("-") ? "—" : FIRSTLOWWAVETIDEDATA8;
                    SECONDHIGHWAVETIDEDATA8 = SECONDHIGHWAVETIDEDATA8 == "" ? "—" : SECONDHIGHWAVETIDEDATA8.Contains("-") ? "—" : SECONDHIGHWAVETIDEDATA8;
                    SECONDLOWWAVETIDEDATA8 = SECONDLOWWAVETIDEDATA8 == "" ? "—" : SECONDLOWWAVETIDEDATA8.Contains("-") ? "—" : SECONDLOWWAVETIDEDATA8;
                }
                else
                {
                    if (dtTJNewTable != null && dtTJNewTable.Rows.Count > 0)
                    {
                        if (dtTJNewTable.Rows[0]["GCS1"] != null && dtTJNewTable.Rows[0]["GCS1"].ToString() != "")
                        {
                            FIRSTHIGHTIME8 = dtTJNewTable.Rows[0]["GCS1"] == null ? "" : dtTJNewTable.Rows[0]["GCS1"].ToString().Replace(":", "");
                            SECONDHIGHTIME8 = dtTJNewTable.Rows[0]["GCS2"] == null ? "" : dtTJNewTable.Rows[0]["GCS2"].ToString().Replace(":", "");
                            FIRSTLOWTIME8 = dtTJNewTable.Rows[0]["DCS1"] == null ? "" : dtTJNewTable.Rows[0]["DCS1"].ToString().Replace(":", "");
                            SECONDLOWTIME8 = dtTJNewTable.Rows[0]["DCS2"] == null ? "" : dtTJNewTable.Rows[0]["DCS2"].ToString().Replace(":", "");

                            FIRSTHIGHTIME8 = FIRSTHIGHTIME8 == "" ? "—" : FIRSTHIGHTIME8.Contains("-") ? "—" : FIRSTHIGHTIME8.Substring(0, 2) + "时" + FIRSTHIGHTIME8.Substring(2, 2) + "分";
                            SECONDHIGHTIME8 = SECONDHIGHTIME8 == "" ? "—" : SECONDHIGHTIME8.Contains("-") ? "—" : SECONDHIGHTIME8.Substring(0, 2) + "时" + SECONDHIGHTIME8.Substring(2, 2) + "分";
                            FIRSTLOWTIME8 = FIRSTLOWTIME8 == "" ? "—" : FIRSTLOWTIME8.Contains("-") ? "—" : FIRSTLOWTIME8.Substring(0, 2) + "时" + FIRSTLOWTIME8.Substring(2, 2) + "分";
                            SECONDLOWTIME8 = SECONDLOWTIME8 == "" ? "—" : SECONDLOWTIME8.Contains("-") ? "—" : SECONDLOWTIME8.Substring(0, 2) + "时" + SECONDLOWTIME8.Substring(2, 2) + "分";

                            FIRSTHIGHWAVETIDEDATA8 = dtTJNewTable.Rows[0]["GCW1"] == null ? "" : dtTJNewTable.Rows[0]["GCW1"].ToString();
                            FIRSTLOWWAVETIDEDATA8 = dtTJNewTable.Rows[0]["DCW1"] == null ? "" : dtTJNewTable.Rows[0]["DCW1"].ToString();
                            SECONDHIGHWAVETIDEDATA8 = dtTJNewTable.Rows[0]["GCW2"] == null ? "" : dtTJNewTable.Rows[0]["GCW2"].ToString();
                            SECONDLOWWAVETIDEDATA8 = dtTJNewTable.Rows[0]["DCW2"] == null ? "" : dtTJNewTable.Rows[0]["DCW2"].ToString();

                            FIRSTHIGHWAVETIDEDATA8 = FIRSTHIGHWAVETIDEDATA8 == "" ? "—" : FIRSTHIGHWAVETIDEDATA8.Contains("-") ? "—" : FIRSTHIGHWAVETIDEDATA8;
                            FIRSTLOWWAVETIDEDATA8 = FIRSTLOWWAVETIDEDATA8 == "" ? "—" : FIRSTLOWWAVETIDEDATA8.Contains("-") ? "—" : FIRSTLOWWAVETIDEDATA8;
                            SECONDHIGHWAVETIDEDATA8 = SECONDHIGHWAVETIDEDATA8 == "" ? "—" : SECONDHIGHWAVETIDEDATA8.Contains("-") ? "—" : SECONDHIGHWAVETIDEDATA8;
                            SECONDLOWWAVETIDEDATA8 = SECONDLOWWAVETIDEDATA8 == "" ? "—" : SECONDLOWWAVETIDEDATA8.Contains("-") ? "—" : SECONDLOWWAVETIDEDATA8;
                        }
                        else
                        {
                            doc.Close(true, ref missing, ref missing);
                            return 0;
                        }
                    }
                    else
                    {
                        doc.Close(true, ref missing, ref missing);
                        return 0;
                    }
                }
                #endregion

                //为了方便管理声明书签数组
                object[] BookMark = new object[80];
                //赋值书签名
                BookMark[0] = "SDOSCWLOWESTWAVEHEIGHT1";
                BookMark[1] = "SDOSCWLOWESTWAVEHEIGHT2";
                BookMark[2] = "SDOSCWLOWESTWAVEHEIGHT3";
                BookMark[3] = "SDOSCWLOWESTWAVEHEIGHT4";
                BookMark[4] = "SDOSCWLOWESTWAVEHEIGHT5";
                BookMark[5] = "SDOSCWSURFACETEMPERATURE1";
                BookMark[6] = "SDOSCWSURFACETEMPERATURE2";
                BookMark[7] = "SDOSCWSURFACETEMPERATURE3";
                BookMark[8] = "SDOSCWSURFACETEMPERATURE4";
                BookMark[9] = "SDOSCWSURFACETEMPERATURE5";
                BookMark[10] = "FIRSTHIGHTIME1";
                BookMark[11] = "SECONDHIGHTIME1";
                BookMark[12] = "FIRSTLOWTIME1";
                BookMark[13] = "SECONDLOWTIME1";
                BookMark[14] = "FIRSTHIGHTIME2";
                BookMark[15] = "SECONDHIGHTIME2";
                BookMark[16] = "FIRSTLOWTIME2";
                BookMark[17] = "SECONDLOWTIME2";
                BookMark[18] = "FIRSTHIGHTIME3";
                BookMark[19] = "SECONDHIGHTIME3";
                BookMark[20] = "FIRSTLOWTIME3";
                BookMark[21] = "SECONDLOWTIME3";
                BookMark[22] = "FIRSTHIGHTIME4";
                BookMark[23] = "SECONDHIGHTIME4";
                BookMark[24] = "FIRSTLOWTIME4";
                BookMark[25] = "SECONDLOWTIME4";
                BookMark[26] = "FIRSTHIGHTIME5";
                BookMark[27] = "SECONDHIGHTIME5";
                BookMark[28] = "FIRSTLOWTIME5";
                BookMark[29] = "SECONDLOWTIME5";
                BookMark[30] = "FIRSTHIGHWAVETIDEDATA1";
                BookMark[31] = "FIRSTHIGHWAVETIDEDATA2";
                BookMark[32] = "FIRSTHIGHWAVETIDEDATA3";
                BookMark[33] = "FIRSTHIGHWAVETIDEDATA4";
                BookMark[34] = "FIRSTHIGHWAVETIDEDATA5";
                BookMark[35] = "SECONDHIGHWAVETIDEDATA1";
                BookMark[36] = "SECONDHIGHWAVETIDEDATA2";
                BookMark[37] = "SECONDHIGHWAVETIDEDATA3";
                BookMark[38] = "SECONDHIGHWAVETIDEDATA4";
                BookMark[39] = "SECONDHIGHWAVETIDEDATA5";
                BookMark[40] = "FIRSTLOWWAVETIDEDATA1";
                BookMark[41] = "FIRSTLOWWAVETIDEDATA2";
                BookMark[42] = "FIRSTLOWWAVETIDEDATA3";
                BookMark[43] = "FIRSTLOWWAVETIDEDATA4";
                BookMark[44] = "FIRSTLOWWAVETIDEDATA5";
                BookMark[45] = "SECONDLOWWAVETIDEDATA1";
                BookMark[46] = "SECONDLOWWAVETIDEDATA2";
                BookMark[47] = "SECONDLOWWAVETIDEDATA3";
                BookMark[48] = "SECONDLOWWAVETIDEDATA4";
                BookMark[49] = "SECONDLOWWAVETIDEDATA5";

                BookMark[50] = "SDOSCWLOWESTWAVEHEIGHT6";
                BookMark[51] = "SDOSCWLOWESTWAVEHEIGHT7";
                BookMark[52] = "SDOSCWLOWESTWAVEHEIGHT8";
                BookMark[53] = "SDOSCWSURFACETEMPERATURE6";
                BookMark[54] = "SDOSCWSURFACETEMPERATURE7";
                BookMark[55] = "SDOSCWSURFACETEMPERATURE8";
                BookMark[56] = "FIRSTHIGHTIME6";
                BookMark[57] = "SECONDHIGHTIME6";
                BookMark[58] = "FIRSTLOWTIME6";
                BookMark[59] = "SECONDLOWTIME6";
                BookMark[60] = "FIRSTHIGHTIME7";
                BookMark[61] = "SECONDHIGHTIME7";
                BookMark[62] = "FIRSTLOWTIME7";
                BookMark[63] = "SECONDLOWTIME7";
                BookMark[64] = "FIRSTHIGHTIME8";
                BookMark[65] = "SECONDHIGHTIME8";
                BookMark[66] = "FIRSTLOWTIME8";
                BookMark[67] = "SECONDLOWTIME8";
                BookMark[68] = "FIRSTHIGHWAVETIDEDATA6";
                BookMark[69] = "FIRSTHIGHWAVETIDEDATA7";
                BookMark[70] = "FIRSTHIGHWAVETIDEDATA8";
                BookMark[71] = "SECONDHIGHWAVETIDEDATA6";
                BookMark[72] = "SECONDHIGHWAVETIDEDATA7";
                BookMark[73] = "SECONDHIGHWAVETIDEDATA8";
                BookMark[74] = "FIRSTLOWWAVETIDEDATA6";
                BookMark[75] = "FIRSTLOWWAVETIDEDATA7";
                BookMark[76] = "FIRSTLOWWAVETIDEDATA8";
                BookMark[77] = "SECONDLOWWAVETIDEDATA6";
                BookMark[78] = "SECONDLOWWAVETIDEDATA7";
                BookMark[79] = "SECONDLOWWAVETIDEDATA8";

                //赋值数据到书签的位置
                doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = SDOSCWLOWESTWAVEHEIGHT1;
                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = SDOSCWLOWESTWAVEHEIGHT2;
                doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = SDOSCWLOWESTWAVEHEIGHT3;
                doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = SDOSCWLOWESTWAVEHEIGHT4;
                doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = SDOSCWLOWESTWAVEHEIGHT5;
                doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = SDOSCWSURFACETEMPERATURE1;
                doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = SDOSCWSURFACETEMPERATURE2;
                doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = SDOSCWSURFACETEMPERATURE3;
                doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = SDOSCWSURFACETEMPERATURE4;
                doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = SDOSCWSURFACETEMPERATURE5;
                doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text =FIRSTHIGHTIME1;
                doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text =SECONDHIGHTIME1;
                doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text =FIRSTLOWTIME1;
                doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text =SECONDLOWTIME1;
                doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text =FIRSTHIGHTIME2;
                doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text =SECONDHIGHTIME2;
                doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text =FIRSTLOWTIME2;
                doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text =SECONDLOWTIME2;
                doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = FIRSTHIGHTIME3;
                doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text =SECONDHIGHTIME3;
                doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text =FIRSTLOWTIME3;
                doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text =SECONDLOWTIME3;
                doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = FIRSTHIGHTIME4;
                doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text =SECONDHIGHTIME4;
                doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text =FIRSTLOWTIME4;
                doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text =SECONDLOWTIME4;
                doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text =FIRSTHIGHTIME5;
                doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text =SECONDHIGHTIME5;
                doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text =FIRSTLOWTIME5;
                doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text =SECONDLOWTIME5;
                doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = FIRSTHIGHWAVETIDEDATA1;
                doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = FIRSTHIGHWAVETIDEDATA2;
                doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = FIRSTHIGHWAVETIDEDATA3;
                doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = FIRSTHIGHWAVETIDEDATA4;
                doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = FIRSTHIGHWAVETIDEDATA5;
                doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = SECONDHIGHWAVETIDEDATA1;
                doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = SECONDHIGHWAVETIDEDATA2;
                doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = SECONDHIGHWAVETIDEDATA3;
                doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = SECONDHIGHWAVETIDEDATA4;
                doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = SECONDHIGHWAVETIDEDATA5;
                doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = FIRSTLOWWAVETIDEDATA1;
                doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = FIRSTLOWWAVETIDEDATA2;
                doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = FIRSTLOWWAVETIDEDATA3;
                doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = FIRSTLOWWAVETIDEDATA4;
                doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = FIRSTLOWWAVETIDEDATA5;
                doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text =SECONDLOWWAVETIDEDATA1;
                doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text =SECONDLOWWAVETIDEDATA2;
                doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text =SECONDLOWWAVETIDEDATA3;
                doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text =SECONDLOWWAVETIDEDATA4;
                doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = SECONDLOWWAVETIDEDATA5;

                doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = SDOSCWLOWESTWAVEHEIGHT6;
                doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = SDOSCWLOWESTWAVEHEIGHT7;
                doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = SDOSCWLOWESTWAVEHEIGHT8;
                doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = SDOSCWSURFACETEMPERATURE6;
                doc.Bookmarks.get_Item(ref BookMark[54]).Range.Text = SDOSCWSURFACETEMPERATURE7;
                doc.Bookmarks.get_Item(ref BookMark[55]).Range.Text = SDOSCWSURFACETEMPERATURE8;
                doc.Bookmarks.get_Item(ref BookMark[56]).Range.Text = FIRSTHIGHTIME6;
                doc.Bookmarks.get_Item(ref BookMark[57]).Range.Text = SECONDHIGHTIME6;
                doc.Bookmarks.get_Item(ref BookMark[58]).Range.Text = FIRSTLOWTIME6;
                doc.Bookmarks.get_Item(ref BookMark[59]).Range.Text = SECONDLOWTIME6;
                doc.Bookmarks.get_Item(ref BookMark[60]).Range.Text = FIRSTHIGHTIME7;
                doc.Bookmarks.get_Item(ref BookMark[61]).Range.Text = SECONDHIGHTIME7;
                doc.Bookmarks.get_Item(ref BookMark[62]).Range.Text = FIRSTLOWTIME7;
                doc.Bookmarks.get_Item(ref BookMark[63]).Range.Text = SECONDLOWTIME7;
                doc.Bookmarks.get_Item(ref BookMark[64]).Range.Text = FIRSTHIGHTIME8;
                doc.Bookmarks.get_Item(ref BookMark[65]).Range.Text = SECONDHIGHTIME8;
                doc.Bookmarks.get_Item(ref BookMark[66]).Range.Text = FIRSTLOWTIME8;
                doc.Bookmarks.get_Item(ref BookMark[67]).Range.Text = SECONDLOWTIME8;
                doc.Bookmarks.get_Item(ref BookMark[68]).Range.Text = FIRSTHIGHWAVETIDEDATA6;
                doc.Bookmarks.get_Item(ref BookMark[69]).Range.Text = FIRSTHIGHWAVETIDEDATA7;
                doc.Bookmarks.get_Item(ref BookMark[70]).Range.Text = FIRSTHIGHWAVETIDEDATA8;
                doc.Bookmarks.get_Item(ref BookMark[71]).Range.Text = SECONDHIGHWAVETIDEDATA6;
                doc.Bookmarks.get_Item(ref BookMark[72]).Range.Text = SECONDHIGHWAVETIDEDATA7;
                doc.Bookmarks.get_Item(ref BookMark[73]).Range.Text = SECONDHIGHWAVETIDEDATA8;
                doc.Bookmarks.get_Item(ref BookMark[74]).Range.Text = FIRSTLOWWAVETIDEDATA6;
                doc.Bookmarks.get_Item(ref BookMark[75]).Range.Text = FIRSTLOWWAVETIDEDATA7;
                doc.Bookmarks.get_Item(ref BookMark[76]).Range.Text = FIRSTLOWWAVETIDEDATA8;
                doc.Bookmarks.get_Item(ref BookMark[77]).Range.Text = SECONDLOWWAVETIDEDATA6;
                doc.Bookmarks.get_Item(ref BookMark[78]).Range.Text = SECONDLOWWAVETIDEDATA7;
                doc.Bookmarks.get_Item(ref BookMark[79]).Range.Text = SECONDLOWWAVETIDEDATA8;

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