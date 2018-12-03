using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

/// <summary>
/// FourteenWord 的摘要说明
/// </summary>
public class FourteenWord
{
    public FourteenWord()
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
    string SDOSCWLOWESTWAVEHEIGHT148 = "";
    string SDOSCWLOWESTWAVEHEIGHT248 = "";
    string SDOSCWLOWESTWAVEHEIGHT348 = "";
    string SDOSCWLOWESTWAVEHEIGHT448= "";
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
                string FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();
                string FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();

                string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                string FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温电话

                string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                //PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
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
            //为了方便管理声明书签数组
            object[] BookMark = new object[34];//新增2个预报员电话60改为62, 模板中存在的有34个书签
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


            BookMark[32] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[33] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话



            //BookMark[32] = "SDOSCWLOWESTWAVEHEIGHT148";
            //BookMark[33] = "SDOSCWLOWESTWAVEHEIGHT248";
            //BookMark[34] = "SDOSCWLOWESTWAVEHEIGHT348";
            //BookMark[35] = "SDOSCWLOWESTWAVEHEIGHT448";
            //BookMark[36] = "SDOSCWLOWESTWAVEHEIGHT548";
            //BookMark[37] = "SDOSCWLOWESTWAVEHEIGHT648";
            //BookMark[38] = "SDOSCWLOWESTWAVEHEIGHT748";
            //BookMark[39] = "SDOSCWLOWESTWAVEHEIGHT172";
            //BookMark[40] = "SDOSCWLOWESTWAVEHEIGHT272";
            //BookMark[41] = "SDOSCWLOWESTWAVEHEIGHT372";
            //BookMark[42] = "SDOSCWLOWESTWAVEHEIGHT472";
            //BookMark[43] = "SDOSCWLOWESTWAVEHEIGHT572";
            //BookMark[44] = "SDOSCWLOWESTWAVEHEIGHT672";
            //BookMark[45] = "SDOSCWLOWESTWAVEHEIGHT772";

            //BookMark[46] = "SDOSCWSURFACETEMPERATURE148";
            //BookMark[47] = "SDOSCWSURFACETEMPERATURE248";
            //BookMark[48] = "SDOSCWSURFACETEMPERATURE348";
            //BookMark[49] = "SDOSCWSURFACETEMPERATURE448";
            //BookMark[50] = "SDOSCWSURFACETEMPERATURE548";
            //BookMark[51] = "SDOSCWSURFACETEMPERATURE648";
            //BookMark[52] = "SDOSCWSURFACETEMPERATURE748";

            //BookMark[53] = "SDOSCWSURFACETEMPERATURE172";
            //BookMark[54] = "SDOSCWSURFACETEMPERATURE272";
            //BookMark[55] = "SDOSCWSURFACETEMPERATURE372";
            //BookMark[56] = "SDOSCWSURFACETEMPERATURE472";
            //BookMark[57] = "SDOSCWSURFACETEMPERATURE572";
            //BookMark[58] = "SDOSCWSURFACETEMPERATURE672";
            //BookMark[59] = "SDOSCWSURFACETEMPERATURE772";

            




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

            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;



            //doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = SDOSCWLOWESTWAVEHEIGHT148;
            //doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = SDOSCWLOWESTWAVEHEIGHT248;
            //doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = SDOSCWLOWESTWAVEHEIGHT348;

            //doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = SDOSCWLOWESTWAVEHEIGHT448;
            //doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = SDOSCWLOWESTWAVEHEIGHT548;
            //doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = SDOSCWLOWESTWAVEHEIGHT648;
            //doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = SDOSCWLOWESTWAVEHEIGHT748;
            //doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = SDOSCWLOWESTWAVEHEIGHT172;
            //doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = SDOSCWLOWESTWAVEHEIGHT272;
            //doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = SDOSCWLOWESTWAVEHEIGHT372;
            //doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = SDOSCWLOWESTWAVEHEIGHT472;
            //doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = SDOSCWLOWESTWAVEHEIGHT572;
            //doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = SDOSCWLOWESTWAVEHEIGHT672;
            //doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = SDOSCWLOWESTWAVEHEIGHT772;

            //doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = SDOSCWSURFACETEMPERATURE148;
            //doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = SDOSCWSURFACETEMPERATURE248;
            //doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = SDOSCWSURFACETEMPERATURE348;
            //doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = SDOSCWSURFACETEMPERATURE448;
            //doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = SDOSCWSURFACETEMPERATURE548;
            //doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = SDOSCWSURFACETEMPERATURE648;
            //doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = SDOSCWSURFACETEMPERATURE748;
            //doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = SDOSCWSURFACETEMPERATURE172;
            //doc.Bookmarks.get_Item(ref BookMark[54]).Range.Text = SDOSCWSURFACETEMPERATURE272;
            //doc.Bookmarks.get_Item(ref BookMark[55]).Range.Text = SDOSCWSURFACETEMPERATURE372;
            //doc.Bookmarks.get_Item(ref BookMark[56]).Range.Text = SDOSCWSURFACETEMPERATURE472;
            //doc.Bookmarks.get_Item(ref BookMark[57]).Range.Text = SDOSCWSURFACETEMPERATURE572;
            //doc.Bookmarks.get_Item(ref BookMark[58]).Range.Text = SDOSCWSURFACETEMPERATURE672;
            //doc.Bookmarks.get_Item(ref BookMark[59]).Range.Text = SDOSCWSURFACETEMPERATURE772;




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