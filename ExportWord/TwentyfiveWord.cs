/*
    变更记录1
变更时间：10180710    
变更内容：下午十八威海取消，从下午三威海24小时预报取数
变更人员：Yuy         
--*/
using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;


public class TwentyfiveWord
{
    public TwentyfiveWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    string PUBLISHTIME = "";
    string WTV24HWH48HFWAVEHEIGHT = "";
    string WTV24HWH48HFWATERTEMP = "";

    string WTV24HSD24HFWAVEHEIGHT = "";
    string WTV24HSD24HFWATERTEMP = "";
    string WTV24HSD48HFWAVEHEIGHT = "";
    string WTV24HSD48HFWATERTEMP = "";

    string WTV24HWD24HFWAVEHEIGHT = "";
    string WTV24HWD24HFWATERTEMP = "";
    string WTV24HWD48HFWAVEHEIGHT = "";
    string WTV24HWD48HFWATERTEMP = "";

    string WTV24HCST24HFWAVEHEIGHT = "";
    string WTV24HCST24HFWATERTEMP = "";
    string WTV24HCST48HFWAVEHEIGHT = "";
    string WTV24HCST48HFWATERTEMP = "";

    string WTV24HRS24HFWAVEHEIGHT = "";
    string WTV24HRS24HFWATERTEMP = "";
    string WTV24HRS48HFWAVEHEIGHT = "";
    string WTV24HRS48HFWATERTEMP = "";

    string[] ForecastDateArr = new string[10]; //潮汐预报日期
    string[] FstHighTideTimeArr = new string[10]; //第一次高潮潮时
    string[] FstHighTideHeightArr = new string[10]; //第一次高潮潮高
    string[] FstLowTideTimeArr = new string[10]; //第一次低潮潮时
    string[] FstLowTideHeightArr = new string[10]; //第一次低潮潮高
    string[] SndHighTideTimeArr = new string[10]; //第二次高潮潮时
    string[] SndHighTideHeightArr = new string[10]; //第二次高潮潮高
    string[] SndLowTideTimeArr = new string[10]; //第二次低潮潮时
    string[] SndLowTideHeightArr = new string[10]; //第二次低潮潮高  

    DataTable WHChaoXiData = new DataTable();//临时存放威海五区所有潮汐数据
    /// <summary>
    /// 调用模板生成word
    /// </summary>
    /// <param name="templateFile">模板文件</param>
    /// <param name="fileName">生成的具有模板样式的新文件</param>
    public int ExportWord(string templateFile, string fileName, DateTime dt)
    {

        #region //生成word程序对象
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
        #endregion
        try
        {
            //数据库查询
            #region 填报信息表数据
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

                //string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                //string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                string FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话
                string FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话

                string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                // PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
                PUBLISHTIME = PUBLISHDATE + "15时";

                tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;

                //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                //tblfooter_Model.FFAX = FFAX;

                //tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                //tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;
                tblfooter_Model.FTIDALFORECASTER = FTIDALFORECASTER;
                tblfooter_Model.FWATERTEMPERATUREFORECASTER = FWATERTEMPERATUREFORECASTER;

                tblfooter_Model.FWAVEFORECASTERTEL = FWAVEFORECASTERTEL;
                tblfooter_Model.FTIDALFORECASTERTEL = FTIDALFORECASTERTEL;
                tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL = FWATERTEMPERATUREFORECASTERTEL;

            }
            #endregion

            #region 威海五市区 风浪水温
            TBLWEIHAITV24HFORECAST tblweihaitv24hforecast_Model = new TBLWEIHAITV24HFORECAST();
            tblweihaitv24hforecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblweihaitv24hforecast = (System.Data.DataTable)new sql_TBLWEIHAITV24HFORECAST().get_TBLWEIHAITV24HFORECAST_AllData(tblweihaitv24hforecast_Model);


            if (tblweihaitv24hforecast.Rows.Count == 0) { }
            else
            {
                WTV24HSD24HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HSD24HFWAVEHEIGHT"].ToString();
                WTV24HSD24HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HSD24HFWATERTEMP"].ToString();
                WTV24HWH48HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HWH48HFWAVEHEIGHT"].ToString();
                //WTV24HWH48HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HWH48HFWATERTEMP"].ToString();
                WTV24HSD48HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HSD48HFWAVEHEIGHT"].ToString();

                WTV24HSD48HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HSD48HFWATERTEMP"].ToString();
                WTV24HWD24HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HWD24HFWAVEHEIGHT"].ToString();
                WTV24HWD24HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HWD24HFWATERTEMP"].ToString();
                WTV24HCST24HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HCST24HFWAVEHEIGHT"].ToString();
                WTV24HCST24HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HCST24HFWATERTEMP"].ToString();
                WTV24HRS24HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HRS24HFWAVEHEIGHT"].ToString();
                WTV24HRS24HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HRS24HFWATERTEMP"].ToString();
                WTV24HWD48HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HWD48HFWAVEHEIGHT"].ToString();
                WTV24HWD48HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HWD48HFWATERTEMP"].ToString();

                WTV24HCST48HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HCST48HFWAVEHEIGHT"].ToString();
                WTV24HCST48HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HCST48HFWATERTEMP"].ToString();
                WTV24HRS48HFWAVEHEIGHT = tblweihaitv24hforecast.Rows[0]["WTV24HRS48HFWAVEHEIGHT"].ToString();
                WTV24HRS48HFWATERTEMP = tblweihaitv24hforecast.Rows[0]["WTV24HRS48HFWATERTEMP"].ToString();


            }
            string tDayOne24 = dt.Day.ToString();
            string tDayTwo24 = dt.AddDays(1).Day.ToString();
            string tDayOne48 = dt.AddDays(1).Day.ToString();
            string tDayTwo48 = dt.AddDays(2).Day.ToString();
            object DayOne24 = "DayOne24";
            doc.Bookmarks.get_Item(ref DayOne24).Range.Text = tDayOne24;
            object DayTwo24 = "DayTwo24";
            doc.Bookmarks.get_Item(ref DayTwo24).Range.Text = tDayTwo24;
            object DayOne48 = "DayOne48";
            doc.Bookmarks.get_Item(ref DayOne48).Range.Text = tDayOne48;
            object DayTwo48 = "DayTwo48";
            doc.Bookmarks.get_Item(ref DayTwo48).Range.Text = tDayTwo48;
            #endregion

            #region 获取威海市区潮汐数据并添加到临时数据表中

            #region 获取潮时
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            TBLSDOFFSHORESEVENCITY24HTIDE tblsdoffshoresevencity24htide_Model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            tblsdoffshoresevencity24htide_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblsdoffshoresevencity24htide = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HTIDE().get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(tblsdoffshoresevencity24htide_Model);
            if (tblsdoffshoresevencity24htide.Rows.Count == 0) { }
            else
            {
                var fdate1 = dt.AddDays(1).ToString();
                var fdate2 = dt.AddDays(2).ToString();
                var fdate3 = dt.AddDays(3).ToString();

                // var forecastdate = Convert.ToDateTime(tblsdoffshoresevencity24htide.Rows[i]["FORECASTDATE"]).ToString();
                //var datef = Convert.ToDateTime(forecastdate).Day.ToString();

                dt1 = UniteDataTableColumns(tblsdoffshoresevencity24htide, "FIRSTHIGHWAVETIME", "SDOSCTFIRSTHIGHWAVEHOUR", "SDOSCTFIRSTHIGHWAVEMINUTE");//合并第一次高潮时

                dt1 = UniteDataTableColumns(dt1, "SECONDHIGHWAVETIME", "SDOSCTSECONDHIGHWAVEHOUR", "SDOSCTSECONDHIGHWAVEMINUTE");//合并第二次高潮潮时

                dt1 = UniteDataTableColumns(dt1, "FIRSTLOWWAVETIME", "SDOSCTFIRSTLOWWAVEHOUR", "SDOSCTFIRSTLOWWAVEMINUTE");//合并第一次低潮潮时

                dt1 = UniteDataTableColumns(dt1, "SECONDLOWWAVETIME", "SDOSCTSECONDLOWWAVEHOUR", "SDOSCTSECONDLOWWAVEMINUTE");//合并第二次低潮潮时 
            }
            // DataTable datNew = datSource.DefaultView.ToTable(false, new string[] { "列名", "列名".....})
            dt2 = dt1.DefaultView.ToTable(false, new string[] { "FORECASTDATE", "SDOSCTCITY", "FIRSTHIGHWAVETIME", "SECONDHIGHWAVETIME", "FIRSTLOWWAVETIME", "SECONDLOWWAVETIME", "PUBLISHDATE" });
            WHChaoXiData = dt2.Clone();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (dt2.Rows[i]["SDOSCTCITY"].ToString() == "威海")
                {
                    WHChaoXiData.ImportRow(dt2.Rows[i]);
                }

            }

            #endregion

            #region 获取潮高
            sql_TideData sql1 = new sql_TideData();
            HT_TideData model1 = new HT_TideData();
            DataTable dt3 = new DataTable();
            model1.PUBLISHDATE = dt;
            DataTable tideData = (DataTable)sql1.getTideData(model1);
            dt3 = tideData.Clone();
            if (tideData != null && tideData.Rows.Count > 0)
            {
                for (int i = 0; i < tideData.Rows.Count; i++)
                {
                    if (tideData.Rows[i]["SDOSCTCITY"].ToString() == "威海")
                    {
                        dt3.ImportRow(tideData.Rows[i]);
                    }
                }

                WHChaoXiData = AddColumn(WHChaoXiData, dt3);
                //修改列名与其他区一致
                // DataTableA.Columns["原来的列名"].ColumnName = "新的列名";
                WHChaoXiData.Columns["SDOSCTCITY"].ColumnName = "REPORTAREA";
                WHChaoXiData.Columns["FIRSTHIGHWAVETIDEDATA"].ColumnName = "FIRSTHIGHWAVEHEIGHT";
                WHChaoXiData.Columns["FIRSTLOWWAVETIDEDATA"].ColumnName = "FIRSTLOWWAVEHEIGHT";
                WHChaoXiData.Columns["SECONDHIGHWAVETIDEDATA"].ColumnName = "SECONDHIGHWAVEHEIGHT";
                WHChaoXiData.Columns["SECONDLOWWAVETIDEDATA"].ColumnName = "SECONDLOWWAVEHEIGHT";
                WHChaoXiData.Rows.Remove(WHChaoXiData.Rows[WHChaoXiData.Rows.Count - 1]);
            }
            #endregion
            #endregion

            #region 威海五区潮汐数据赋值给数组
            TBLWEIHAISHIDAOTIDALFORECAST TBLWEIHAISHIDAOTIDALFORECAST_Model = new TBLWEIHAISHIDAOTIDALFORECAST();
            TBLWEIHAISHIDAOTIDALFORECAST_Model.PUBLISHDATE = dt;
            System.Data.DataTable TBLWEIHAISHIDAOTIDALFORECAST_Table = (System.Data.DataTable)new sql_TBLWEIHAISHIDAOTIDALFORECAST().get_TBLWEIHAISHIDAOTIDALFORECAST_AllData(TBLWEIHAISHIDAOTIDALFORECAST_Model);
            TBLWEIHAISHIDAOTIDALFORECAST_Table.Merge(WHChaoXiData);
            if (TBLWEIHAISHIDAOTIDALFORECAST_Table.Rows.Count == 0) { }
            else
            {
                foreach (System.Data.DataRow row in TBLWEIHAISHIDAOTIDALFORECAST_Table.Rows)
                {
                    var REPORTAREA = row["REPORTAREA"].ToString();
                    var FORECASTDATE = DateTime.Parse(row["FORECASTDATE"].ToString());

                    var forecastIndex = ((FORECASTDATE - dt).Days - 1);
                    var areaIndex = 0;
                    switch (REPORTAREA)
                    {
                        case "威海":
                            areaIndex = 0;
                            break;
                        case "文登":
                            areaIndex = 2;
                            break;
                        case "成山头":
                            areaIndex = 4;
                            break;
                        case "石岛":
                            areaIndex = 6;
                            break;
                        case "乳山":
                            areaIndex = 8;
                            break;
                        default:
                            break;

                    }
                    var index = areaIndex + forecastIndex;
                    ForecastDateArr[index] = FORECASTDATE.Month + "月" + FORECASTDATE.Day + "日";
                    FstHighTideTimeArr[index] = ConvertTimeStr(row["FIRSTHIGHWAVETIME"].ToString());//第一次高潮潮时
                    FstHighTideHeightArr[index] = row["FIRSTHIGHWAVEHEIGHT"].ToString(); //第一次高潮潮高
                    FstLowTideTimeArr[index] = ConvertTimeStr(row["FIRSTLOWWAVETIME"].ToString()); //第一次低潮潮时
                    FstLowTideHeightArr[index] = row["FIRSTLOWWAVEHEIGHT"].ToString(); //第一次低潮潮高
                    SndHighTideTimeArr[index] = ConvertTimeStr(row["SECONDHIGHWAVETIME"].ToString()); //第二次高潮潮时
                    SndHighTideHeightArr[index] = row["SECONDHIGHWAVEHEIGHT"].ToString(); //第二次高潮潮高
                    SndLowTideTimeArr[index] = ConvertTimeStr(row["SECONDLOWWAVETIME"].ToString()); //第二次低潮潮时
                    SndLowTideHeightArr[index] = row["SECONDLOWWAVEHEIGHT"].ToString(); //第二次低潮潮高
                }
            }
            #endregion

            #region  威海市区24小时浪高水温
            //获取24小时威海浪高及海温预报数据
            var SDOSCWHIGHTESTWAVEHEIGHT = "";
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
                    if(SDOSCWAREA == "威海近海")
                    {
                        SDOSCWHIGHTESTWAVEHEIGHT = row["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        SDOSCWSURFACETEMPERATURE = row["SDOSCWSURFACETEMPERATURE"].ToString();
                        WTV24HWH48HFWATERTEMP = row["SDOSCWSURFACETEMPERATURE48H"].ToString();
                    }
                }
            }
            #endregion 


            #region 

            //为了方便管理声明书签数组
            object[] BookMark = new object[28];//新增3个预报员电话，27改为30,但是删除了2个书签
            //赋值书签名TBLSDOFFSHORESEVENCITY24HWAVE.SDOSCWSURFACETEMPERATURE
            BookMark[0] = "FRELEASEUNIT";//发布单位

            BookMark[1] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[2] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[27] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话

            //BookMark[1] = "FZHIBANTEL";//预报值班
            //BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真    
            BookMark[3] = "FWAVEFORECASTER"; //海浪预报员
            BookMark[4] = "FTIDALFORECASTER";//潮汐预报员
            BookMark[5] = "FWATERTEMPERATUREFORECASTER";//水温预报员

            BookMark[6] = "WTV24HSD24HFWAVEHEIGHT";
            BookMark[7] = "WTV24HSD24HFWATERTEMP";
            BookMark[8] = "WTV24HWH48HFWAVEHEIGHT";
            BookMark[9] = "WTV24HWH48HFWATERTEMP";
            BookMark[10] = "WTV24HSD48HFWAVEHEIGHT";

            BookMark[11] = "WTV24HSD48HFWATERTEMP";
            BookMark[12] = "WTV24HWD24HFWAVEHEIGHT";
            BookMark[13] = "WTV24HWD24HFWATERTEMP";
            BookMark[14] = "WTV24HCST24HFWAVEHEIGHT";
            BookMark[15] = "WTV24HCST24HFWATERTEMP";
            BookMark[16] = "WTV24HRS24HFWAVEHEIGHT";
            BookMark[17] = "WTV24HRS24HFWATERTEMP";
            BookMark[18] = "WTV24HWD48HFWAVEHEIGHT";
            BookMark[19] = "WTV24HWD48HFWATERTEMP";

            BookMark[20] = "WTV24HCST48HFWAVEHEIGHT";
            BookMark[21] = "WTV24HCST48HFWATERTEMP";
            BookMark[22] = "WTV24HRS48HFWAVEHEIGHT";
            BookMark[23] = "WTV24HRS48HFWATERTEMP";
            BookMark[24] = "PUBLISHTIME";

            //威海近海24小时
            BookMark[25] = "SDOSCWHIGHTESTWAVEHEIGHT";
            BookMark[26] = "SDOSCWSURFACETEMPERATURE";

            

            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = WTV24HSD24HFWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = WTV24HSD24HFWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = WTV24HWH48HFWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = WTV24HWH48HFWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = WTV24HSD48HFWAVEHEIGHT;

            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = WTV24HSD48HFWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = WTV24HWD24HFWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = WTV24HWD24HFWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = WTV24HCST24HFWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = WTV24HCST24HFWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = WTV24HRS24HFWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = WTV24HRS24HFWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = WTV24HWD48HFWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = WTV24HWD48HFWATERTEMP;

            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = WTV24HCST48HFWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = WTV24HCST48HFWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = WTV24HRS48HFWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = WTV24HRS48HFWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = PUBLISHTIME;

            //威海近海244小时
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = SDOSCWHIGHTESTWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = SDOSCWSURFACETEMPERATURE;

            

            object mark = "";
            for(int i=0;i<10;i++)
            {
                mark = "FORECASTDATE"+(i+1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = ForecastDateArr[i];
                mark = "FSTHIGHTIDETIME" + (i + 1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = FstHighTideTimeArr[i];
                mark = "FSTHIGHTIDEHEIGHT" + (i + 1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = FstHighTideHeightArr[i];
                mark = "FSTLOWTIDETIME" + (i + 1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = FstLowTideTimeArr[i];
                mark = "FSTLOWTIDEHEIGHT" + (i + 1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = FstLowTideHeightArr[i];
                mark = "SNDHIGHTIDETIME" + (i + 1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = SndHighTideTimeArr[i];
                mark = "SNDHIGHTIDEHEIGHT" + (i + 1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = SndHighTideHeightArr[i];
                mark = "SNDLOWTIDETIME" + (i + 1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = SndLowTideTimeArr[i];
                mark = "SNDLOWTIDEHEIGHT" + (i + 1);
                doc.Bookmarks.get_Item(ref mark).Range.Text = SndLowTideHeightArr[i];
            }

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
    string ConvertTimeStr(string timeStr)
    {
        return (string.IsNullOrEmpty(timeStr) || timeStr.Contains("-")) ? 
            timeStr : timeStr.Substring(0, 2) + timeStr.Substring(timeStr.Length -2);
    }
    //合并Datatable两行
    public static DataTable UniteDataTableColumns(DataTable dt, String newColumnName, string ColumnName1, string ColumnName2)
    {
        //汇总的表达式
        string expression = "";
        expression = String.Format("{0}+{1}", ColumnName1, ColumnName2);
        //增加汇总列
        System.Type myDataType = System.Type.GetType("System.String");
        DataColumn dcCol = new DataColumn(newColumnName, myDataType, expression, MappingType.Attribute);
        //增加合并列
        dt.Columns.Add(dcCol);
        //移除合并了的行
        //dt.Columns.Remove(dt.Columns[ColumnName1]);
        //dt.Columns.Remove(dt.Columns[ColumnName2]);
        return dt;
    }

    private static DataTable AddColumn(DataTable WHChaoXiData, DataTable dt)
    {
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string columnName = dt.Columns[i].ColumnName;
            if (columnName == "FIRSTHIGHWAVETIDEDATA" || columnName == "FIRSTLOWWAVETIDEDATA" || columnName == "SECONDHIGHWAVETIDEDATA" || columnName == "SECONDLOWWAVETIDEDATA")
            {
                WHChaoXiData.Columns.Add(columnName, typeof(string));
                int j = -1;
                while (++j < WHChaoXiData.Rows.Count)
                {
                    WHChaoXiData.Rows[j][columnName] = j + 1 > dt.Rows.Count ? " " : dt.Rows[j][columnName];
                }
            }
        }
        return WHChaoXiData;
    }
    //    dt.Columns[0].ColumnName;
    //"PUBLISHDATE"
    //dt.Columns[1].ColumnName;
    //"SDOSCTCITY"
    //dt.Columns[2].ColumnName;
    //"FIRSTHIGHWAVETIDEDATA"
    //dt.Columns[3].ColumnName;
    //"FIRSTLOWWAVETIDEDATA"
    //dt.Columns[4].ColumnName;
    //"SECONDHIGHWAVETIDEDATA"
    //dt.Columns[5].ColumnName;
    //"SECONDLOWWAVETIDEDATA"
    //dt.Columns[6].ColumnName;
    //"FORECASTDATE"
}
