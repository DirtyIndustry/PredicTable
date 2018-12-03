using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;//.NET选项卡中的"Microsoft.Office.Interop.Word"
/// <summary>
/// ThreeExportWord 的摘要说明
/// </summary>
public class ThreeWord
{
    public ThreeWord()
    {
    }

    string PUBLISHTIME = "";

    string FORECASTDATE1 = "";
    string FORECASTDATE2 = "";
    string FORECASTDATE3 = "";
    string YRSSWWWAVEHEIGHT1 = "";
    string YRSSWWWAVEHEIGHT2 = "";
    string YRSSWWWAVEHEIGHT3 = "";
    string YRSSWWWAVEDIRECTION1 = "";
    string YRSSWWWAVEDIRECTION2 = "";
    string YRSSWWWAVEDIRECTION3 = ""; 
    string YRSSWWWINDDIRECTION1 = "";
    string YRSSWWWINDDIRECTION2 = "";
    string YRSSWWWINDDIRECTION3 = "";
    string YRSSWWWINDFORCE1 = "";
    string YRSSWWWINDFORCE2 = "";
    string YRSSWWWINDFORCE3 = "";


    string FORECASTDATE21 = "";
    string FORECASTDATE22 = "";
    string FORECASTDATE23 = "";
    string MZZTLFIRSTWAVEOFTIME1 = "";
    string MZZTLFIRSTWAVEOFTIME2 = "";
    string MZZTLFIRSTWAVEOFTIME3 = "";
    string MZZTLFIRSTWAVETIDELEVEL1 = "";
    string MZZTLFIRSTWAVETIDELEVEL2 = "";
    string MZZTLFIRSTWAVETIDELEVEL3 = "";
    string MZZTLFIRSTTIMELOWTIDE1 = "";
    string MZZTLFIRSTTIMELOWTIDE2 = "";
    string MZZTLFIRSTTIMELOWTIDE3 = "";
    string MZZTLLOWTIDELEVELFORTHEFIRSTTI1 = "";
    string MZZTLLOWTIDELEVELFORTHEFIRSTTI2 = "";
    string MZZTLLOWTIDELEVELFORTHEFIRSTTI3 = "";
    string MZZTLSECONDWAVEOFTIME1 = "";
    string MZZTLSECONDWAVEOFTIME2 = "";
    string MZZTLSECONDWAVEOFTIME3 = "";
    string MZZTLSECONDWAVETIDELEVEL1 = "";
    string MZZTLSECONDWAVETIDELEVEL2 = "";
    string MZZTLSECONDWAVETIDELEVEL3 = "";
    string MZZTLSECONDTIMELOWTIDE1 = "";
    string MZZTLSECONDTIMELOWTIDE2 = "";
    string MZZTLSECONDTIMELOWTIDE3 = "";
    string MZZTLLOWTIDELEVELFORTHESECONDT1 = "";
    string MZZTLLOWTIDELEVELFORTHESECONDT2 = "";
    string MZZTLLOWTIDELEVELFORTHESECONDT3 = "";

    /// <summary>
    /// 调用模板生成word
    /// </summary>
    /// <param name="templateFile">模板文件</param>
    /// <param name="fileName">生成的具有模板样式的新文件</param>
    public int ExportWord(string templateFile, string fileName,DateTime dt)
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

        //数据库查询
        try
        {


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

                string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话
                string FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话

                string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                //PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
                PUBLISHTIME = PUBLISHDATE + "15时";

                tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                //tblfooter_Model.FFAX = FFAX;
                tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;
                tblfooter_Model.FTIDALFORECASTER = FTIDALFORECASTER;

                tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                tblfooter_Model.FWAVEFORECASTERTEL = FWAVEFORECASTERTEL;
                tblfooter_Model.FTIDALFORECASTERTEL = FTIDALFORECASTERTEL;
            }
            //黄河南海堤表提取数据
            TBLYRSOUTHSEAWALL24WINDWAVE tbltrsouthseawall24windwave_Model = new TBLYRSOUTHSEAWALL24WINDWAVE();
            tbltrsouthseawall24windwave_Model.PUBLISHDATE = dt;
            System.Data.DataTable tbltrsouthseawall24windwave = (System.Data.DataTable)new sql_TBLYRSOUTHSEAWALL24WINDWAVE().get_TBLYRSOUTHSEAWALL24WINDWAVE_AllData(tbltrsouthseawall24windwave_Model);
            if (tbltrsouthseawall24windwave.Rows.Count == 0) { }
            else
            {
                FORECASTDATE1 = tbltrsouthseawall24windwave.Rows[0]["FORECASTDATE"].ToString();
                if (FORECASTDATE1 != null || FORECASTDATE1 != "")
                {
                    string[] FORECASTDATE11 = FORECASTDATE1.Split(' ');
                    FORECASTDATE1 = FORECASTDATE11[0].Substring(5).Replace("/", "月") + "日";
                }
                FORECASTDATE2 = tbltrsouthseawall24windwave.Rows[1]["FORECASTDATE"].ToString();
                if (FORECASTDATE2 != null || FORECASTDATE2 != "")
                {
                    string[] FORECASTDATE22 = FORECASTDATE2.Split(' ');
                    FORECASTDATE2 = FORECASTDATE22[0].Substring(5).Replace("/","月")+"日";

                }
                FORECASTDATE3 = tbltrsouthseawall24windwave.Rows[2]["FORECASTDATE"].ToString();
                if (FORECASTDATE3 != null || FORECASTDATE3 != "")
                {
                    string[] FORECASTDATE33 = FORECASTDATE3.Split(' ');
                    FORECASTDATE3 = FORECASTDATE33[0].Substring(5).Replace("/", "月") + "日";
                }
                YRSSWWWAVEHEIGHT1 = tbltrsouthseawall24windwave.Rows[0]["YRSSWWWAVEHEIGHT"].ToString();
                YRSSWWWAVEHEIGHT2 = tbltrsouthseawall24windwave.Rows[1]["YRSSWWWAVEHEIGHT"].ToString();
                YRSSWWWAVEHEIGHT3 = tbltrsouthseawall24windwave.Rows[2]["YRSSWWWAVEHEIGHT"].ToString();
                YRSSWWWAVEDIRECTION1 = tbltrsouthseawall24windwave.Rows[0]["YRSSWWWAVEDIRECTION"].ToString();
                YRSSWWWAVEDIRECTION2 = tbltrsouthseawall24windwave.Rows[1]["YRSSWWWAVEDIRECTION"].ToString();
                YRSSWWWAVEDIRECTION3 = tbltrsouthseawall24windwave.Rows[2]["YRSSWWWAVEDIRECTION"].ToString();
                YRSSWWWINDDIRECTION1 = tbltrsouthseawall24windwave.Rows[0]["YRSSWWWINDDIRECTION"].ToString();
                YRSSWWWINDDIRECTION2 = tbltrsouthseawall24windwave.Rows[1]["YRSSWWWINDDIRECTION"].ToString();
                YRSSWWWINDDIRECTION3 = tbltrsouthseawall24windwave.Rows[2]["YRSSWWWINDDIRECTION"].ToString();
                YRSSWWWINDFORCE1 = tbltrsouthseawall24windwave.Rows[0]["YRSSWWWINDFORCE"].ToString();
                YRSSWWWINDFORCE2 = tbltrsouthseawall24windwave.Rows[1]["YRSSWWWINDFORCE"].ToString();
                YRSSWWWINDFORCE3 = tbltrsouthseawall24windwave.Rows[2]["YRSSWWWINDFORCE"].ToString();

            }
            //明泽闸潮位预报数据库提取
            TBLMZZTIDELEVEL tblmzztidelevel_Model = new TBLMZZTIDELEVEL();
            tblmzztidelevel_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblmzztidelevel = (System.Data.DataTable)new sql_TBLMZZTIDELEVEL().get_TBLMZZTIDELEVEL_AllData(tblmzztidelevel_Model);
            if (tblmzztidelevel.Rows.Count == 0) { }
            else
            {
                FORECASTDATE21 = tblmzztidelevel.Rows[0]["FORECASTDATE"].ToString();
                if (FORECASTDATE21 != null || FORECASTDATE21 != "")
                {
                    string[] FORECASTDATE221 = FORECASTDATE21.Split(' ');
                    FORECASTDATE21 = FORECASTDATE221[0].Substring(5).Replace("/", "月") + "日";
                }
                FORECASTDATE22 = tblmzztidelevel.Rows[1]["FORECASTDATE"].ToString();
                if (FORECASTDATE22 != null || FORECASTDATE22 != "")
                {
                    string[] FORECASTDATE222 = FORECASTDATE22.Split(' ');
                    FORECASTDATE22 = FORECASTDATE222[0].Substring(5).Replace("/", "月") + "日";
                }
                FORECASTDATE23 = tblmzztidelevel.Rows[2]["FORECASTDATE"].ToString();
                if (FORECASTDATE23 != null || FORECASTDATE23 != "")
                {
                    string[] FORECASTDATE223 = FORECASTDATE23.Split(' ');
                    FORECASTDATE23 = FORECASTDATE223[0].Substring(5).Replace("/", "月") + "日";
                }
                MZZTLFIRSTWAVEOFTIME1 = tblmzztidelevel.Rows[0]["MZZTLFIRSTWAVEOFTIME"].ToString();
                MZZTLFIRSTWAVEOFTIME2 = tblmzztidelevel.Rows[1]["MZZTLFIRSTWAVEOFTIME"].ToString();
                MZZTLFIRSTWAVEOFTIME3 = tblmzztidelevel.Rows[2]["MZZTLFIRSTWAVEOFTIME"].ToString();
                MZZTLFIRSTWAVETIDELEVEL1 = tblmzztidelevel.Rows[0]["MZZTLFIRSTWAVETIDELEVEL"].ToString();
                MZZTLFIRSTWAVETIDELEVEL2 = tblmzztidelevel.Rows[1]["MZZTLFIRSTWAVETIDELEVEL"].ToString();
                MZZTLFIRSTWAVETIDELEVEL3 = tblmzztidelevel.Rows[2]["MZZTLFIRSTWAVETIDELEVEL"].ToString();
                MZZTLFIRSTTIMELOWTIDE1 = tblmzztidelevel.Rows[0]["MZZTLFIRSTTIMELOWTIDE"].ToString();
                MZZTLFIRSTTIMELOWTIDE2 = tblmzztidelevel.Rows[1]["MZZTLFIRSTTIMELOWTIDE"].ToString();
                MZZTLFIRSTTIMELOWTIDE3 = tblmzztidelevel.Rows[2]["MZZTLFIRSTTIMELOWTIDE"].ToString();
                MZZTLLOWTIDELEVELFORTHEFIRSTTI1 = tblmzztidelevel.Rows[0]["MZZTLLOWTIDELEVELFORTHEFIRSTTI"].ToString();
                MZZTLLOWTIDELEVELFORTHEFIRSTTI2 = tblmzztidelevel.Rows[1]["MZZTLLOWTIDELEVELFORTHEFIRSTTI"].ToString();
                MZZTLLOWTIDELEVELFORTHEFIRSTTI3 = tblmzztidelevel.Rows[2]["MZZTLLOWTIDELEVELFORTHEFIRSTTI"].ToString();
                MZZTLSECONDWAVEOFTIME1 = tblmzztidelevel.Rows[0]["MZZTLSECONDWAVEOFTIME"].ToString();
                MZZTLSECONDWAVEOFTIME2 = tblmzztidelevel.Rows[1]["MZZTLSECONDWAVEOFTIME"].ToString();
                MZZTLSECONDWAVEOFTIME3 = tblmzztidelevel.Rows[2]["MZZTLSECONDWAVEOFTIME"].ToString();
                MZZTLSECONDWAVETIDELEVEL1 = tblmzztidelevel.Rows[0]["MZZTLSECONDWAVETIDELEVEL"].ToString();
                MZZTLSECONDWAVETIDELEVEL2 = tblmzztidelevel.Rows[1]["MZZTLSECONDWAVETIDELEVEL"].ToString();
                MZZTLSECONDWAVETIDELEVEL3 = tblmzztidelevel.Rows[2]["MZZTLSECONDWAVETIDELEVEL"].ToString();
                MZZTLSECONDTIMELOWTIDE1 = tblmzztidelevel.Rows[0]["MZZTLSECONDTIMELOWTIDE"].ToString();
                MZZTLSECONDTIMELOWTIDE2 = tblmzztidelevel.Rows[1]["MZZTLSECONDTIMELOWTIDE"].ToString();
                MZZTLSECONDTIMELOWTIDE3 = tblmzztidelevel.Rows[2]["MZZTLSECONDTIMELOWTIDE"].ToString();
                MZZTLLOWTIDELEVELFORTHESECONDT1 = tblmzztidelevel.Rows[0]["MZZTLLOWTIDELEVELFORTHESECONDT"].ToString();
                MZZTLLOWTIDELEVELFORTHESECONDT2 = tblmzztidelevel.Rows[1]["MZZTLLOWTIDELEVELFORTHESECONDT"].ToString();
                MZZTLLOWTIDELEVELFORTHESECONDT3 = tblmzztidelevel.Rows[2]["MZZTLLOWTIDELEVELFORTHESECONDT"].ToString();

            }

            //PUBLISHTIME = dt.ToLongDateString() + hourStr + "时";

            //为了方便管理声明书签数组
            object[] BookMark = new object[50];//新增两个预报员电话48改为50
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真
            BookMark[3] = "FWAVEFORECASTER";//海浪预报员
            BookMark[4] = "FTIDALFORECASTER"; //潮汐预报员
            BookMark[5] = "FORECASTDATE1";//黄河南海堤预报日期1
            BookMark[6] = "FORECASTDATE2";//黄河南海堤预报日期2
            BookMark[7] = "FORECASTDATE3";//黄河南海堤预报日期3
            BookMark[8] = "YRSSWWWAVEHEIGHT1";//黄河南海堤波高1
            BookMark[9] = "YRSSWWWAVEHEIGHT2";//黄河南海堤波高2
            BookMark[10] = "YRSSWWWAVEHEIGHT3";//黄河南海堤波高3
            BookMark[11] = "YRSSWWWAVEDIRECTION1";//黄河南海堤波向1
            BookMark[12] = "YRSSWWWAVEDIRECTION2";//黄河南海堤波向2
            BookMark[13] = "YRSSWWWAVEDIRECTION3";//黄河南海堤波向3
            BookMark[14] = "YRSSWWWINDDIRECTION1";//黄河南海堤风向1
            BookMark[15] = "YRSSWWWINDDIRECTION2";//黄河南海堤风向2
            BookMark[16] = "YRSSWWWINDDIRECTION3";//黄河南海堤风向3
            BookMark[17] = "YRSSWWWINDFORCE1";//黄河南海堤风力
            BookMark[18] = "YRSSWWWINDFORCE2";//黄河南海堤风力
            BookMark[19] = "YRSSWWWINDFORCE3";//黄河南海堤风力
            BookMark[20] = "FORECASTDATE21";// 明泽闸潮位预报预报日期1
            BookMark[21] = "FORECASTDATE22";// 明泽闸潮位预报预报日期2
            BookMark[22] = "FORECASTDATE23";// 明泽闸潮位预报预报日期3
            BookMark[23] = "MZZTLFIRSTWAVEOFTIME1"; //明泽闸潮第一次高潮时间1
            BookMark[24] = "MZZTLFIRSTWAVEOFTIME2";//明泽闸潮第一次高潮时间2
            BookMark[25] = "MZZTLFIRSTWAVEOFTIME3";//明泽闸潮第一次高潮时间3
            BookMark[26] = "MZZTLFIRSTWAVETIDELEVEL1"; //明泽闸潮第一次高潮潮位1
            BookMark[27] = "MZZTLFIRSTWAVETIDELEVEL2"; //明泽闸潮第一次高潮潮位2
            BookMark[28] = "MZZTLFIRSTWAVETIDELEVEL3"; //明泽闸潮第一次高潮潮位3
            BookMark[29] = "MZZTLFIRSTTIMELOWTIDE1";//明泽闸潮第一次低潮时间1
            BookMark[30] = "MZZTLFIRSTTIMELOWTIDE2";//明泽闸潮第一次低潮时间2
            BookMark[31] = "MZZTLFIRSTTIMELOWTIDE3";//明泽闸潮第一次低潮时间3
            BookMark[32] = "MZZTLLOWTIDELEVELFORTHEFIRSTTI1";//明泽闸潮第一次低潮潮位1
            BookMark[33] = "MZZTLLOWTIDELEVELFORTHEFIRSTTI2";//明泽闸潮第一次低潮潮位2
            BookMark[34] = "MZZTLLOWTIDELEVELFORTHEFIRSTTI3";//明泽闸潮第一次低潮潮位3
            BookMark[35] = "MZZTLSECONDWAVEOFTIME1";//明泽闸潮第二次高潮时间1
            BookMark[36] = "MZZTLSECONDWAVEOFTIME2";//明泽闸潮第二次高潮时间2
            BookMark[37] = "MZZTLSECONDWAVEOFTIME3";//明泽闸潮第二次高潮时间3
            BookMark[38] = "MZZTLSECONDWAVETIDELEVEL1";//明泽闸潮第二次高潮潮位1
            BookMark[39] = "MZZTLSECONDWAVETIDELEVEL2";//明泽闸潮第二次高潮潮位2
            BookMark[40] = "MZZTLSECONDWAVETIDELEVEL3";//明泽闸潮第二次高潮潮位3
            BookMark[41] = "MZZTLSECONDTIMELOWTIDE1";//明泽闸潮第二次低潮时间1
            BookMark[42] = "MZZTLSECONDTIMELOWTIDE2";//明泽闸潮第二次低潮时间2
            BookMark[43] = "MZZTLSECONDTIMELOWTIDE3";//明泽闸潮第二次低潮时间3
            BookMark[44] = "MZZTLLOWTIDELEVELFORTHESECONDT1";//明泽闸潮第二次低潮潮位
            BookMark[45] = "MZZTLLOWTIDELEVELFORTHESECONDT2"; //明泽闸潮第二次低潮潮位
            BookMark[46] = "MZZTLLOWTIDELEVELFORTHESECONDT3";//明泽闸潮第二次低潮潮位
            BookMark[47] = "PUBLISHTIME";//发布时间

            BookMark[48] = "FWAVEFORECASTERTEL";//海浪预报员电话
            BookMark[49] = "FTIDALFORECASTERTEL"; //潮汐预报员电话

            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;

            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = FORECASTDATE1;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = FORECASTDATE2;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = FORECASTDATE3;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = YRSSWWWAVEHEIGHT1;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = YRSSWWWAVEHEIGHT2;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = YRSSWWWAVEHEIGHT3;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = YRSSWWWAVEDIRECTION1;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = YRSSWWWAVEDIRECTION2;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = YRSSWWWAVEDIRECTION3;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = YRSSWWWINDDIRECTION1;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = YRSSWWWINDDIRECTION2;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = YRSSWWWINDDIRECTION3;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = YRSSWWWINDFORCE1;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = YRSSWWWINDFORCE2;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = YRSSWWWINDFORCE3;

            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = FORECASTDATE21;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = FORECASTDATE22;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = FORECASTDATE23;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = MZZTLFIRSTWAVEOFTIME1;
            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = MZZTLFIRSTWAVEOFTIME2;
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = MZZTLFIRSTWAVEOFTIME3;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = MZZTLFIRSTWAVETIDELEVEL1;
            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = MZZTLFIRSTWAVETIDELEVEL2;
            doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = MZZTLFIRSTWAVETIDELEVEL3;
            doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = MZZTLFIRSTTIMELOWTIDE1;
            doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = MZZTLFIRSTTIMELOWTIDE2;
            doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = MZZTLFIRSTTIMELOWTIDE3;
            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = MZZTLLOWTIDELEVELFORTHEFIRSTTI1;
            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = MZZTLLOWTIDELEVELFORTHEFIRSTTI2;
            doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = MZZTLLOWTIDELEVELFORTHEFIRSTTI3;
            doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = MZZTLSECONDWAVEOFTIME1;
            doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = MZZTLSECONDWAVEOFTIME2;
            doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = MZZTLSECONDWAVEOFTIME3;
            doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = MZZTLSECONDWAVETIDELEVEL1;
            doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = MZZTLSECONDWAVETIDELEVEL2;
            doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = MZZTLSECONDWAVETIDELEVEL3;
            doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = MZZTLSECONDTIMELOWTIDE1;
            doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = MZZTLSECONDTIMELOWTIDE2;
            doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = MZZTLSECONDTIMELOWTIDE3;
            doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = MZZTLLOWTIDELEVELFORTHESECONDT1;
            doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = MZZTLLOWTIDELEVELFORTHESECONDT2;
            doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = MZZTLLOWTIDELEVELFORTHESECONDT3;

            doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = PUBLISHTIME;

            doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
            doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;

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
