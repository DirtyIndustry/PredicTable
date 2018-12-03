using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;
/// <summary>
/// ElevenWord 的摘要说明
/// </summary>
public class ElevenWord
{
    public ElevenWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    string PUBLISHTIME = "";

    string SA24HWFBOHAIWAVEHEIGHT = "";
    string SA24HWFBOHAIWAVEDIR = "";
    string SA24HWFBOHAISURGEDIR = "";
    string SA24HWFNORTHOFYSWAVEHEIGHT = "";
    string SA24HWFNORTHOFYSWAVEDIR = "";

    string SA24HWFNORTHOFYSSURGEDIR = "";
    string SA24HWFMIDDLEOFYSWAVEHEIGHT = "";
    string SA24HWFMIDDLEOFYSWAVEDIR = "";
    string SA24HWFMIDDLEOFYSSURGEDIR = "";
    string SA24HWFSOUTHOFYSWAVEHEIGHT = "";
    string SA24HWFSOUTHOFYSWAVEDIR = "";
    string SA24HWFSOUTHOFYSSURGEDIR = "";
    string SA24HWFQDOFFSHOREWAVEHEIGHT = "";
    string SA24HWFQDOFFSHOREWAVEDIR = "";

    string SA24HWFQDOFFSHORESURGEDIR = "";
    string QDOFFSHOREWATERTEMP = "";

    /// <summary>
    /// 调用模板生成word
    /// </summary>
    /// <param name="templateFile">模板文件</param>
    /// <param name="fileName">生成的具有模板样式的新文件</param>
    public int ExportWord(string templateFile, string fileName,DateTime dt)
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
            string FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();

                string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                string FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话


                string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
            string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
           // PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
            PUBLISHTIME = PUBLISHDATE + "15时";
            tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
            //tblfooter_Model.FTELEPHONE = FTELEPHONE;
            //tblfooter_Model.FFAX = FFAX;
            tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;
            tblfooter_Model.FWATERTEMPERATUREFORECASTER = FWATERTEMPERATUREFORECASTER;

                tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                tblfooter_Model.FWAVEFORECASTERTEL = FWAVEFORECASTERTEL;
                tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL = FWATERTEMPERATUREFORECASTERTEL;

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
                        QDOFFSHOREWATERTEMP = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                }
            }
            //青岛24小时潮位预报
            TBLSEAAREA24HWAVEFORECAST tblseaarea24hwaveforecast_Model = new TBLSEAAREA24HWAVEFORECAST();
        tblseaarea24hwaveforecast_Model.PUBLISHDATE = dt;
        System.Data.DataTable tblseaarea24hwaveforecast = (System.Data.DataTable)new sql_TBLSEAAREA24HWAVEFORECAST().get_TBLSEAAREA24HWAVEFORECAST_AllData(tblseaarea24hwaveforecast_Model);
        if (tblseaarea24hwaveforecast.Rows.Count == 0) { }
        else
        {
            SA24HWFBOHAIWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAIWAVEHEIGHT"].ToString();
            SA24HWFBOHAIWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAIWAVEDIR"].ToString();
            SA24HWFBOHAISURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAISURGEDIR"].ToString();
            SA24HWFNORTHOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSWAVEHEIGHT"].ToString();
            SA24HWFNORTHOFYSWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSWAVEDIR"].ToString();
            SA24HWFNORTHOFYSSURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSSURGEDIR"].ToString();
            SA24HWFMIDDLEOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSWAVEHEIGHT"].ToString();
            SA24HWFMIDDLEOFYSWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSWAVEDIR"].ToString();
            SA24HWFMIDDLEOFYSSURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSSURGEDIR"].ToString();
            SA24HWFSOUTHOFYSWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFSOUTHOFYSWAVEHEIGHT"].ToString();
            SA24HWFSOUTHOFYSWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFSOUTHOFYSWAVEDIR"].ToString();
            SA24HWFSOUTHOFYSSURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFSOUTHOFYSSURGEDIR"].ToString();
            SA24HWFQDOFFSHOREWAVEHEIGHT = tblseaarea24hwaveforecast.Rows[0]["SA24HWFQDOFFSHOREWAVEHEIGHT"].ToString();
            SA24HWFQDOFFSHOREWAVEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFQDOFFSHOREWAVEDIR"].ToString();
            SA24HWFQDOFFSHORESURGEDIR = tblseaarea24hwaveforecast.Rows[0]["SA24HWFQDOFFSHORESURGEDIR"].ToString();


        }
        //为了方便管理声明书签数组
        object[] BookMark = new object[24];//新增2个预报员电话22改为24
        //赋值书签名
        BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班

            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真    
            BookMark[3] = "FWAVEFORECASTER"; //海浪预报员
        BookMark[4] = "FWATERTEMPERATUREFORECASTER";//水温预报员

        BookMark[5] = "SA24HWFBOHAIWAVEHEIGHT";//第一次高潮时
        BookMark[6] = "SA24HWFBOHAIWAVEDIR";//第一次高潮分
        BookMark[7] = "SA24HWFBOHAISURGEDIR";//第一次高潮高度
        BookMark[8] = "SA24HWFNORTHOFYSWAVEHEIGHT";//第一次低潮时
        BookMark[9] = "SA24HWFNORTHOFYSWAVEDIR";//第一次低潮分
        BookMark[10] = "SA24HWFNORTHOFYSSURGEDIR";//第一次低潮高度
        BookMark[11] = "SA24HWFMIDDLEOFYSWAVEHEIGHT";//第二次高潮时
        BookMark[12] = "SA24HWFMIDDLEOFYSWAVEDIR";//第二次高潮分
        BookMark[13] = "SA24HWFMIDDLEOFYSSURGEDIR";//第二次高潮高度
        BookMark[14] = "SA24HWFSOUTHOFYSWAVEHEIGHT";//第二次低潮时
        BookMark[15] = "SA24HWFSOUTHOFYSWAVEDIR";//第二次低潮分
        BookMark[16] = "SA24HWFSOUTHOFYSSURGEDIR";//第二次低潮高度
        BookMark[17] = "SA24HWFQDOFFSHOREWAVEHEIGHT";//明日滨海浪高
        BookMark[18] = "SA24HWFQDOFFSHOREWAVEDIR";//浪向
        BookMark[19] = "SA24HWFQDOFFSHORESURGEDIR";//浪向
        BookMark[20] = "PUBLISHTIME";
        BookMark[21] = "QDOFFSHOREWATERTEMP";

            BookMark[22] = "FWAVEFORECASTERTEL"; //海浪预报员
            BookMark[23] = "FWATERTEMPERATUREFORECASTERTEL";//水温预报员

            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
        doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
        doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = SA24HWFBOHAIWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = SA24HWFBOHAIWAVEDIR;

        doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = SA24HWFBOHAISURGEDIR;
        doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = SA24HWFNORTHOFYSWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = SA24HWFNORTHOFYSWAVEDIR;
        doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = SA24HWFNORTHOFYSSURGEDIR;


        doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = SA24HWFMIDDLEOFYSWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = SA24HWFMIDDLEOFYSWAVEDIR;
        doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = SA24HWFMIDDLEOFYSSURGEDIR;
        doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = SA24HWFSOUTHOFYSWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = SA24HWFSOUTHOFYSWAVEDIR;
        doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = SA24HWFSOUTHOFYSSURGEDIR;
        doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = SA24HWFQDOFFSHOREWAVEHEIGHT;
        doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = SA24HWFQDOFFSHOREWAVEDIR;
        doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = SA24HWFQDOFFSHORESURGEDIR;

        doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = PUBLISHTIME;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = QDOFFSHOREWATERTEMP;

            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;


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