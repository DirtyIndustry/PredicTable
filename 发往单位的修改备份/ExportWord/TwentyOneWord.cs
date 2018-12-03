using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


public class TwentyOneWord
{

    public TwentyOneWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    string PUBLISHTIME = "";//发布时间
    string PREDATE = "";
    //string PUBLISHHOUR = "";//小时
    string FRELEASEUNIT = "";//发布单位
    //string FFAX = "";//传真
    //string FTELEPHONE = "";//联系电话
    string FTIDALFORECASTER = "";//潮汐预报员
    string FWAVEFORECASTER = "";//海浪预报员
    string FWATERTEMPERATUREFORECASTER = "";//海温预报员

    //新增预报值班电话和预报员电话
    string ZHIBANTEL = "";//预报值班
    string SENDTEL = "";//预报发送
    string FTIDALFORECASTERTEL = "";//潮汐预报员电话
    string FWAVEFORECASTERTEL = "";//海浪预报员电话
    string FWATERTEMPERATUREFORECASTERTEL = "";//海温预报员电话

    string WAVEHEIGHT = "";

    //青岛市区海水浴场
    string GB24HTFFIRSTHIGHWAVEHOUR1 = ""; //第一次高潮时
    string GB24HTFFIRSTHIGHWAVEMINUTE1 = ""; //第一次高潮分
    string GB24HTFFIRSTLOWWAVEHOUR1 = "";//第一次低潮时
    string GB24HTFFIRSTLOWWAVEMINUTE1 = "";//第一次低潮分
    string GB24HTFSECONDHIGHWAVEHOUR1 = "";//第二次高潮时
    string GB24HTFSECONDHIGHWAVEMINUTE1 = "";//第二次高潮分
    string GB24HTFSECONDLOWWAVEHOUR1 = "";//第二次低潮时
    string GB24HTFSECONDLOWWAVEMINUTE1 = "";//第二次低潮分
    //金沙滩海水浴场
    string GB24HTFFIRSTHIGHWAVEHOUR2 = ""; //第一次高潮时
    string GB24HTFFIRSTHIGHWAVEMINUTE2 = ""; //第一次高潮分
    string GB24HTFFIRSTLOWWAVEHOUR2 = "";//第一次低潮时
    string GB24HTFFIRSTLOWWAVEMINUTE2 = "";//第一次低潮分
    string GB24HTFSECONDHIGHWAVEHOUR2 = "";//第二次高潮时
    string GB24HTFSECONDHIGHWAVEMINUTE2 = "";//第二次高潮分
    string GB24HTFSECONDLOWWAVEHOUR2 = "";//第二次低潮时
    string GB24HTFSECONDLOWWAVEMINUTE2 = "";//第二次低潮分

    //第一海水浴场
    string SB24HWFFIRSTBATHINGWAVEHEIGHT = ""; //浪高
    string SB24HWFFIRSTBATHINGWATERTEMP = ""; //水温
    string SB24HWFFIRSTBATHINGSWIMWARN = "";//预警
    //第六海水浴场
    string SB24HWFSIXTHBATHINGWAVEHEIGHT = ""; //浪高
    string SB24HWFSIXTHBATHINGWATERTEMP = ""; //水温
    string SB24HWFSIXTHBATHINGSWIMWARN = "";//预警
    //石老人海水浴场
    string SB24HWFSLRBATHINGWAVEHEIGHT = ""; //浪高
    string SB24HWFSLRBATHINGWATERTEMP = ""; //水温
    string SB24HWFSLRBATHINGSWIMWARN = "";//预警
    //金沙滩海水浴场
    string SB24HWFGOLDBEACHWAVEHEIGHT = ""; //浪高
    string SB24HWFGOLDBEACHWATERTEMP = ""; //水温
    string SB24HWFGOLDBEACHSWIMWAIN = "";//预警
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
        Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
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
                FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                //FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                ////string PUBLISHDATE = tblfooter.Rows[i]["PUBLISHDATE"].ToString();
                ////string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                //FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();
                FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();

                ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话
                FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话

                string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                //PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
                PUBLISHTIME = PUBLISHDATE + "15时";
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
                        WAVEHEIGHT = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                    }
                }
            }


            //海水浴场
             # region 海水浴场
            TBLSEABEACH24HWAVEFORECAST tblseabach24hwaveforecast_Model = new TBLSEABEACH24HWAVEFORECAST();
            //TBLSEAAREA24HWAVEFORECAST tblseaarea24hwaveforecast_Model = new TBLSEAAREA24HWAVEFORECAST();
            tblseabach24hwaveforecast_Model.PUBLISHDATE = dt;
                                                                                             
            System.Data.DataTable tblseabach24hwaveforecast = (System.Data.DataTable)new sql_TBLSEABEACH24HWAVEFORECAST().get_TBLSEABEACH24HWAVEFORECAST_AllData(tblseabach24hwaveforecast_Model);
            if (tblseabach24hwaveforecast.Rows.Count == 0) { }
            else
            {
                SB24HWFFIRSTBATHINGWAVEHEIGHT = tblseabach24hwaveforecast.Rows[0]["SB24HWFFIRSTBATHINGWAVEHEIGHT"].ToString();
                SB24HWFFIRSTBATHINGWATERTEMP = tblseabach24hwaveforecast.Rows[0]["SB24HWFFIRSTBATHINGWATERTEMP"].ToString();
                SB24HWFFIRSTBATHINGSWIMWARN = tblseabach24hwaveforecast.Rows[0]["SB24HWFFIRSTBATHINGSWIMWARN"].ToString();

                SB24HWFSIXTHBATHINGWAVEHEIGHT = tblseabach24hwaveforecast.Rows[0]["SB24HWFSIXTHBATHINGWAVEHEIGHT"].ToString();
                SB24HWFSIXTHBATHINGWATERTEMP = tblseabach24hwaveforecast.Rows[0]["SB24HWFSIXTHBATHINGWATERTEMP"].ToString();
                SB24HWFSIXTHBATHINGSWIMWARN = tblseabach24hwaveforecast.Rows[0]["SB24HWFSIXTHBATHINGSWIMWARN"].ToString();

                SB24HWFSLRBATHINGWAVEHEIGHT = tblseabach24hwaveforecast.Rows[0]["SB24HWFSLRBATHINGWAVEHEIGHT"].ToString();
                SB24HWFSLRBATHINGWATERTEMP = tblseabach24hwaveforecast.Rows[0]["SB24HWFSLRBATHINGWATERTEMP"].ToString();
                SB24HWFSLRBATHINGSWIMWARN = tblseabach24hwaveforecast.Rows[0]["SB24HWFSLRBATHINGSWIMWARN"].ToString();

                SB24HWFGOLDBEACHWAVEHEIGHT = tblseabach24hwaveforecast.Rows[0]["SB24HWFGOLDBEACHWAVEHEIGHT"].ToString();
                SB24HWFGOLDBEACHWATERTEMP = tblseabach24hwaveforecast.Rows[0]["SB24HWFGOLDBEACHWATERTEMP"].ToString();
                SB24HWFGOLDBEACHSWIMWAIN = tblseabach24hwaveforecast.Rows[0]["SB24HWFGOLDBEACHSWIMWAIN"].ToString();

            }
            #endregion
            //青岛浴场潮汐
            #region  第一海水浴场潮汐
            TBLGOLDBEACH72HTIDALFORECAST tblgoldbeach24Htidalforecast_Model = new TBLGOLDBEACH72HTIDALFORECAST();
            tblgoldbeach24Htidalforecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblgoldbeach24Htidalforecast = (System.Data.DataTable)new sql_TBLGOLDBEACH24HTIDALFORECAST().get24HourTideData(tblgoldbeach24Htidalforecast_Model);
            if (tblgoldbeach24Htidalforecast == null || tblgoldbeach24Htidalforecast.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblgoldbeach24Htidalforecast.Rows.Count; i++)
                {
                                                          
                    if (tblgoldbeach24Htidalforecast.Rows[i]["SEABEACH"].ToString() == "青岛市区") //市区
                    {
                      
                        GB24HTFFIRSTHIGHWAVEHOUR1 = GetTideHour(tblgoldbeach24Htidalforecast.Rows[i]["FIRSTHIGHTIME"].ToString());
                        GB24HTFFIRSTHIGHWAVEMINUTE1 = GetTideMinute(tblgoldbeach24Htidalforecast.Rows[i]["FIRSTHIGHTIME"].ToString());
                        GB24HTFFIRSTLOWWAVEHOUR1 = GetTideHour(tblgoldbeach24Htidalforecast.Rows[i]["FIRSTLOWTIME"].ToString());
                        GB24HTFFIRSTLOWWAVEMINUTE1 = GetTideMinute(tblgoldbeach24Htidalforecast.Rows[i]["FIRSTLOWTIME"].ToString());
                        GB24HTFSECONDHIGHWAVEHOUR1 = GetTideHour(tblgoldbeach24Htidalforecast.Rows[i]["SECONDHIGHTIME"].ToString());
                        GB24HTFSECONDHIGHWAVEMINUTE1 = GetTideMinute(tblgoldbeach24Htidalforecast.Rows[i]["SECONDHIGHTIME"].ToString());
                        GB24HTFSECONDLOWWAVEHOUR1 = GetTideHour(tblgoldbeach24Htidalforecast.Rows[i]["SECONDLOWTIME"].ToString());
                        GB24HTFSECONDLOWWAVEMINUTE1 = GetTideMinute(tblgoldbeach24Htidalforecast.Rows[i]["SECONDLOWTIME"].ToString());
                    }
                    //else if (tblgoldbeach24Htidalforecast.Rows[i]["SEABEACH"].ToString() == "金沙滩") //五号码头
                    //{
                    //    GB24HTFFIRSTHIGHWAVEHOUR2 = GetTideHour(tblgoldbeach24Htidalforecast.Rows[i]["FIRSTHIGHTIME"].ToString());
                    //    GB24HTFFIRSTHIGHWAVEMINUTE2 = GetTideMinute(tblgoldbeach24Htidalforecast.Rows[i]["FIRSTHIGHTIME"].ToString());
                    //    GB24HTFFIRSTLOWWAVEHOUR2 = GetTideHour(tblgoldbeach24Htidalforecast.Rows[i]["FIRSTLOWTIME"].ToString());
                    //    GB24HTFFIRSTLOWWAVEMINUTE2 = GetTideMinute(tblgoldbeach24Htidalforecast.Rows[i]["FIRSTLOWTIME"].ToString());
                    //    GB24HTFSECONDHIGHWAVEHOUR2 = GetTideHour(tblgoldbeach24Htidalforecast.Rows[i]["SECONDHIGHTIME"].ToString());
                    //    GB24HTFSECONDHIGHWAVEMINUTE2 = GetTideMinute(tblgoldbeach24Htidalforecast.Rows[i]["SECONDHIGHTIME"].ToString());
                    //    GB24HTFSECONDLOWWAVEHOUR2 = GetTideHour(tblgoldbeach24Htidalforecast.Rows[i]["SECONDLOWTIME"].ToString());
                    //    GB24HTFSECONDLOWWAVEMINUTE2 = GetTideMinute(tblgoldbeach24Htidalforecast.Rows[i]["SECONDLOWTIME"].ToString());
                    //}

                }

            }
            #endregion

            #region 金沙滩海水浴场
            //修改从下午三青岛三天取数  Edit By Yuy 180718

            TBLSDOFFSHORESEVENCITY24HTIDE tblsdoffshoresevencity24htide_Model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            tblsdoffshoresevencity24htide_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblsdoffshoresevencity24htide = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HTIDE().get24TideData(tblsdoffshoresevencity24htide_Model);
            if (tblsdoffshoresevencity24htide.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblsdoffshoresevencity24htide.Rows.Count; i++)
                {
                    if (tblsdoffshoresevencity24htide.Rows[i]["SDOSCTCITY"].ToString() == "青岛")
                    {
                        GB24HTFFIRSTHIGHWAVEHOUR2= tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString();
                        GB24HTFFIRSTHIGHWAVEMINUTE2= tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString();
                        GB24HTFFIRSTLOWWAVEHOUR2= tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString();
                        GB24HTFFIRSTLOWWAVEMINUTE2= tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString();
                        GB24HTFSECONDHIGHWAVEHOUR2= tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString();
                        GB24HTFSECONDHIGHWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString();
                        GB24HTFSECONDLOWWAVEHOUR2= tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString();
                        GB24HTFSECONDLOWWAVEMINUTE2 = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString();
                    }
                }
            }
            #endregion
                    PREDATE = dt.AddDays(1).Day.ToString();// + "日12时～" + dt.AddDays(1) + "日12时";
            string PREDATE2 = dt.AddDays(2).Day.ToString();
            #region
            //为了方便管理声明书签数组
            object[] BookMark = new object[43];//新增3个预报员电话40改为43
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真    
            BookMark[3] = "FWAVEFORECASTER"; //海浪预报员
            BookMark[4] = "FTIDALFORECASTER"; //海浪预报员
            BookMark[5] = "FWATERTEMPERATUREFORECASTER";//水温预报员
            BookMark[6] = "PUBLISHDATE";//发布时间
            BookMark[7] = "PUBLISHHOUR";//小时



            //青岛市区海水浴场
            BookMark[8] = "GB24HTFFIRSTHIGHWAVEHOUR1";//第一次高潮时
            BookMark[9] = "GB24HTFFIRSTHIGHWAVEMINUTE1";//第一次高潮分
            BookMark[10] = "GB24HTFFIRSTLOWWAVEHOUR1";//第一次低潮时
            BookMark[11] = "GB24HTFFIRSTLOWWAVEMINUTE1";//第一次低潮分
            BookMark[12] = "GB24HTFSECONDHIGHWAVEHOUR1";//第一次高潮时
            BookMark[13] = "GB24HTFSECONDHIGHWAVEMINUTE1";//第二次高潮时
            BookMark[14] = "GB24HTFSECONDLOWWAVEHOUR1";//第二次低潮时
            BookMark[15] = "GB24HTFSECONDLOWWAVEMINUTE1";//第二次低潮分
            //金沙滩海水浴场
            BookMark[16] = "GB24HTFFIRSTHIGHWAVEHOUR2";//第一次高潮时
            BookMark[17] = "GB24HTFFIRSTHIGHWAVEMINUTE2";//第一次高潮分
            BookMark[18] = "GB24HTFFIRSTLOWWAVEHOUR2";//第一次低潮时
            BookMark[19] = "GB24HTFFIRSTLOWWAVEMINUTE2";//第一次低潮分
            BookMark[20] = "GB24HTFSECONDHIGHWAVEHOUR2";//第二次高潮时
            BookMark[21] = "GB24HTFSECONDHIGHWAVEMINUTE2";//第二次高潮分
            BookMark[22] = "GB24HTFSECONDLOWWAVEHOUR2";//第二次低潮时
            BookMark[23] = "GB24HTFSECONDLOWWAVEMINUTE2";//第二次低潮分

         
            BookMark[24] = "SB24HWFFIRSTBATHINGWAVEHEIGHT";
            BookMark[25] = "SB24HWFFIRSTBATHINGWATERTEMP";
            BookMark[26] = "SB24HWFFIRSTBATHINGSWIMWARN";
      
            BookMark[27] = "SB24HWFSIXTHBATHINGWAVEHEIGHT";
            BookMark[28] = "SB24HWFSIXTHBATHINGWATERTEMP";
            BookMark[29] = "SB24HWFSIXTHBATHINGSWIMWARN";
      
            BookMark[30] = "SB24HWFSLRBATHINGWAVEHEIGHT";
            BookMark[31] = "SB24HWFSLRBATHINGWATERTEMP";
            BookMark[32] = "SB24HWFSLRBATHINGSWIMWARN";
        
            BookMark[33] = "SB24HWFGOLDBEACHWAVEHEIGHT";
            BookMark[34] = "SB24HWFGOLDBEACHWATERTEMP";
            BookMark[35] = "SB24HWFGOLDBEACHSWIMWAIN";

            BookMark[36] = "PUBLISHTIME";
            BookMark[37] = "PREDATE";
            BookMark[38] = "WAVEHEIGHT";
            BookMark[39] = "PREDATE2";

            BookMark[40] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[41] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[42] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话


            //赋值数据到书签的位置

            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = FWATERTEMPERATUREFORECASTER;
            //doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = PUBLISHDATE;
            //doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = PUBLISHHOUR;

            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = GB24HTFFIRSTHIGHWAVEHOUR1;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = GB24HTFFIRSTHIGHWAVEMINUTE1;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = GB24HTFFIRSTLOWWAVEHOUR1;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = GB24HTFFIRSTLOWWAVEMINUTE1;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = GB24HTFSECONDHIGHWAVEHOUR1;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = GB24HTFSECONDHIGHWAVEMINUTE1;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = GB24HTFSECONDLOWWAVEHOUR1;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = GB24HTFSECONDLOWWAVEMINUTE1;

            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = GB24HTFFIRSTHIGHWAVEHOUR2;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = GB24HTFFIRSTHIGHWAVEMINUTE2;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = GB24HTFFIRSTLOWWAVEHOUR2;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = GB24HTFFIRSTLOWWAVEMINUTE2;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = GB24HTFSECONDHIGHWAVEHOUR2;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = GB24HTFSECONDHIGHWAVEMINUTE2;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = GB24HTFSECONDLOWWAVEHOUR2;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = GB24HTFSECONDLOWWAVEMINUTE2;

            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = SB24HWFFIRSTBATHINGWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = SB24HWFFIRSTBATHINGWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = SB24HWFFIRSTBATHINGSWIMWARN;

            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = SB24HWFSIXTHBATHINGWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = SB24HWFSIXTHBATHINGWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = SB24HWFSIXTHBATHINGSWIMWARN;

            doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = SB24HWFSLRBATHINGWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = SB24HWFSLRBATHINGWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = SB24HWFSLRBATHINGSWIMWARN;

            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = SB24HWFGOLDBEACHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = SB24HWFGOLDBEACHWATERTEMP;
            doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = SB24HWFGOLDBEACHSWIMWAIN;

            doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = PUBLISHTIME;
            doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = PREDATE;
            doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = WAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = PREDATE2;

            doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = FWATERTEMPERATUREFORECASTERTEL;
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

    /// <summary>
    /// 拆分或者合并获取小时
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string GetTideHour(string str)
    {
        string strString = "";
        if (str.Contains("-")) {
            if(str.Length < 4)
            {
                strString = "-";
            }
        }
        else {
            strString = str.Substring(0,2);
        }
        return strString;
    }

    /// <summary>
    /// 拆分或者合并获取分钟
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string GetTideMinute(string str)
    {
        string strString = "";
        if (str.Contains("-"))
        {
            if (str.Length < 4)
            {
                strString = "-";
            }
        }
        else
        {
            strString = str.Substring(2, 2);
        }
        return strString;
    }
}