using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

/// <summary>
/// TwelveWord 的摘要说明
/// </summary>
public class TwelveWord
{
    public TwelveWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    //sql_TBLQD24HTIDELEVEL

    string PUBLISHTIME = "";
    string QDC24HWFQDOFFSHOREWAVEHEIGHT = "";
    string QDC24HWFQDOFFSHOREWATERTEMP = "";


    string QD24HTFFIRSTHIGHWAVETIME = "";
    string QD24HTFFIRSTHIGHWAVEHEIGHT = "";
    string QD24HTFSECONDHIGHWAVETIME = "";
    string QD24HTFSECONDHIGHWAVEHEIGHT = "";
    string QD24HTFFIRSTLOWWAVETIME = "";
    string QD24HTFFIRSTLOWWAVEHEIGHT = "";
    string QD24HTFSECONDLOWWAVETIME = "";
    string QD24HTFSECONDLOWWAVEHEIGHT = "";
    string SA24HWFQDOFFSHORESURGEDIR = "";

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
           //青岛周边海域24小时预报
            //TBLQDCIRCUM24HWATERFORECAST tblqdcircum24hwaterforecast_Model = new TBLQDCIRCUM24HWATERFORECAST();
            //tblqdcircum24hwaterforecast_Model.PUBLISHDATE = dt;
            //System.Data.DataTable tblqdcircum24hwaterforecast = (System.Data.DataTable)new sql_TBLQDCIRCUM24HWATERFORECAST().get_TBLQDCIRCUM24HWATERFORECAST_AllData(tblqdcircum24hwaterforecast_Model);

            //if (tblqdcircum24hwaterforecast == null || tblqdcircum24hwaterforecast.Rows.Count == 0)
            //{

            //}
            //else
            //{
            //    QDC24HWFQDOFFSHOREWAVEHEIGHT = tblqdcircum24hwaterforecast.Rows[0]["QDC24HWFQDOFFSHOREWAVEHEIGHT"].ToString();
            //    QDC24HWFQDOFFSHOREWATERTEMP = tblqdcircum24hwaterforecast.Rows[0]["QDC24HWFQDOFFSHOREWATERTEMP"].ToString();

            //}
            sql_TBLSDOFFSHORESEVENCITY24HWAVE sql = new sql_TBLSDOFFSHORESEVENCITY24HWAVE();
            TBLSDOFFSHORESEVENCITY24HWAVE model = new TBLSDOFFSHORESEVENCITY24HWAVE();
            model.PUBLISHDATE = dt;
            System.Data.DataTable dataTable = (System.Data.DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(model);
            if(dataTable == null || dataTable.Rows.Count == 0)
            {

            }
            else
            {
                for(int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var area = dataTable.Rows[i]["SDOSCWAREA"].ToString();
                    if(area == "青岛近海")
                    {
                        QDC24HWFQDOFFSHOREWAVEHEIGHT = dataTable.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"].ToString();
                        QDC24HWFQDOFFSHOREWATERTEMP = dataTable.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                }
            }

            //潍坊港24小时潮汐预报
            //TBLWF24HTIDALFORECAST tblwf24htidalforecast_Model = new TBLWF24HTIDALFORECAST();
            //tblwf24htidalforecast_Model.PUBLISHDATE = dt;
            //System.Data.DataTable tblwf24htidalforecast = (System.Data.DataTable)new sql_TBLWF24HTIDALFORECAST().get_TBLWF24HTIDALFORECAST_AllData(tblwf24htidalforecast_Model);

            //青岛24小时潮汐预报
            #region 获取青岛24小时潮时  
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
                        if (forecastdate == fdate1)
                        {
                            QD24HTFFIRSTHIGHWAVETIME = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEHOUR"].ToString()+"时"+ tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTHIGHWAVEMINUTE"].ToString() + "分"; //第一次高潮时
                            QD24HTFSECONDHIGHWAVETIME = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEHOUR"].ToString()+ "时" + tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDHIGHWAVEMINUTE"].ToString() + "分";//第二次高潮时
                            QD24HTFFIRSTLOWWAVETIME = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEHOUR"].ToString() + "时" + tblsdoffshoresevencity24htide.Rows[i]["SDOSCTFIRSTLOWWAVEMINUTE"].ToString() + "分";//第一次低潮时
                            QD24HTFSECONDLOWWAVETIME = tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEHOUR"].ToString() + "时" + tblsdoffshoresevencity24htide.Rows[i]["SDOSCTSECONDLOWWAVEMINUTE"].ToString() + "分";//第二次低潮时
                        }
                    }
                }
            }
            #endregion
            #region 获取青岛24小时潮高
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
                        if (forecastdate == fdate1)
                        {
                            QD24HTFFIRSTHIGHWAVEHEIGHT = tideData.Rows[i]["FIRSTHIGHWAVETIDEDATA"].ToString();
                            QD24HTFFIRSTLOWWAVEHEIGHT = tideData.Rows[i]["FIRSTLOWWAVETIDEDATA"].ToString();
                            QD24HTFSECONDHIGHWAVEHEIGHT = tideData.Rows[i]["SECONDHIGHWAVETIDEDATA"].ToString();
                            QD24HTFSECONDLOWWAVEHEIGHT = tideData.Rows[i]["SECONDLOWWAVETIDEDATA"].ToString();
                        }
                    }
                }
            }
            #endregion
            #region 下午四取消不用，取下午三青岛24小时数据 取消不用 Edit By Yuy 20180717
            //TBLQD24HTIDELEVEL TBLQD24HTIDELEVEL_Model = new TBLQD24HTIDELEVEL();
            //TBLQD24HTIDELEVEL_Model.PUBLISHDATE = dt;
            //System.Data.DataTable TBLQD24HTIDELEVEL_Table = (System.Data.DataTable)new sql_TBLQD24HTIDELEVEL().get_TBLQD24HTIDELEVEL_AllData(TBLQD24HTIDELEVEL_Model);

            ////sql_TBLQD24HTIDELEVEL
            //if (TBLQD24HTIDELEVEL_Table.Rows.Count == 0) { }
            //else 
            //{

            //        QD24HTFFIRSTHIGHWAVETIME = TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLFIRSTHIGHWAVEHOUR"].ToString()+"时"
            //                                 + TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLFIRSTHIGHWAVEMINUTE"].ToString() + "分";
            //        QD24HTFFIRSTHIGHWAVEHEIGHT = TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLFIRSTHIGHWAVEHEIGHT"].ToString();
            //        QD24HTFSECONDHIGHWAVETIME = TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLSECONDHIGHWAVEHOUR"].ToString() + "时"
            //                                 + TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLSECONDHIGHWAVEMINUTE"].ToString() + "分";
            //        QD24HTFSECONDHIGHWAVEHEIGHT = TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLSECONDHIGHWAVEHEIGHT"].ToString();
            //        QD24HTFFIRSTLOWWAVETIME = TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLFIRSTLOWWAVEHOUR"].ToString() + "时"
            //                                + TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLFIRSTLOWWAVEMINUTE"].ToString() + "分";
            //        QD24HTFFIRSTLOWWAVEHEIGHT = TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLFIRSTLOWWAVEHEIGHT"].ToString();
            //        QD24HTFSECONDLOWWAVETIME = TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLSECONDLOWWAVEHOUR"].ToString() + "时"
            //                                 + TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLSECONDLOWWAVEMINUTE"].ToString() + "分";
            //        QD24HTFSECONDLOWWAVEHEIGHT = TBLQD24HTIDELEVEL_Table.Rows[0]["QDTLSECONDLOWWAVEHEIGHT"].ToString();
            //}
            #endregion

            //青岛24小时潮位预报--涌向
            TBLSEAAREA24HWAVEFORECAST tblseaarea24hwaveforecast_Model = new TBLSEAAREA24HWAVEFORECAST();
            tblseaarea24hwaveforecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblseaarea24hwaveforecast = (System.Data.DataTable)new sql_TBLSEAAREA24HWAVEFORECAST().get_TBLSEAAREA24HWAVEFORECAST_AllData(tblseaarea24hwaveforecast_Model);
            if (tblseaarea24hwaveforecast.Rows.Count == 0) { }
            else
            {
               
                SA24HWFQDOFFSHORESURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFQDOFFSHORESURGEDIR"].ToString();


            }
            //为了方便管理声明书签数组
            object[] BookMark = new object[21];//新增3个预报员电话18改为21
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真    
            BookMark[3] = "FWAVEFORECASTER"; //海浪预报员
            BookMark[4] = "FTIDALFORECASTER";//潮汐预报员
            BookMark[5] = "FWATERTEMPERATUREFORECASTER";//水温预报员
            BookMark[6] = "QDC24HWFQDOFFSHOREWAVEHEIGHT";//青岛近海浪高
            BookMark[7] = "QDC24HWFQDOFFSHOREWATERTEMP";//青岛近海水温
            BookMark[8] = "QD24HTFFIRSTHIGHWAVETIME";//第一次高潮潮时
            BookMark[9] = "QD24HTFFIRSTHIGHWAVEHEIGHT";//第一次高潮潮高
            BookMark[10] = "QD24HTFSECONDHIGHWAVETIME";//第二次高潮潮时
            BookMark[11] = "QD24HTFSECONDHIGHWAVEHEIGHT";//第二次高潮潮高
            BookMark[12] = "QD24HTFFIRSTLOWWAVETIME";//第一次低潮潮时
            BookMark[13] = "QD24HTFFIRSTLOWWAVEHEIGHT";//第一次低潮潮高
            BookMark[14] = "QD24HTFSECONDLOWWAVETIME";//第二次低潮潮时
            BookMark[15] = "QD24HTFSECONDLOWWAVEHEIGHT";//第二次低潮潮高
            BookMark[16] = "PUBLISHTIME";
            BookMark[17] = "SA24HWFQDOFFSHORESURGEDIR";//涌向

            BookMark[18] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[19] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
            BookMark[20] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话


            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送

            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = QDC24HWFQDOFFSHOREWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = QDC24HWFQDOFFSHOREWATERTEMP;

            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = QD24HTFFIRSTHIGHWAVETIME;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = QD24HTFFIRSTHIGHWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = QD24HTFSECONDHIGHWAVETIME;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = QD24HTFSECONDHIGHWAVEHEIGHT;


            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = QD24HTFFIRSTLOWWAVETIME;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = QD24HTFFIRSTLOWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = QD24HTFSECONDLOWWAVETIME;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = QD24HTFSECONDLOWWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = PUBLISHTIME;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = SA24HWFQDOFFSHORESURGEDIR;

            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;

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