using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

/// <summary>
/// FifteenWord 的摘要说明
/// </summary>
public class FifteenWord
{
    public FifteenWord()
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
    string SA24HWFBOHAIWAVENOTES = "";
    string SA24HWFNORTHOFYSWAVENOTES = "";
    string SA24HWFMIDDLEOFYSWAVENOTES = "";
    string SA24HWFSOUTHOFYSWAVENOTES = "";


    string SA48HWFBOHAIWAVEHEIGHT = "";
    string SA48HWFBOHAIWAVEDIR = "";
    string SA48HWFBOHAISURGEDIR = "";
    string SA48HWFBOHAIWAVENOTES = "";
    string SA48HWFNORTHOFYSWAVEHEIGHT = "";
    string SA48HWFNORTHOFYSWAVEDIR = "";
    string SA48HWFNORTHOFYSSURGEDIR = "";
    string SA48HWFNORTHOFYSWAVENOTES = "";
    string SA48HWFMIDDLEOFYSWAVEHEIGHT = "";
    string SA48HWFMIDDLEOFYSWAVEDIR = "";
    string SA48HWFMIDDLEOFYSSURGEDIR = "";
    string SA48HWFMIDDLEOFYSWAVENOTES = "";
    string SA48HWFSOUTHOFYSWAVEHEIGHT = "";
    string SA48HWFSOUTHOFYSWAVEDIR = "";
    string SA48HWFSOUTHOFYSSURGEDIR = "";
    string SA48HWFSOUTHOFYSWAVENOTES = "";

    string SA72HWFBOHAIWAVEHEIGHT = "";
    string SA72HWFBOHAIWAVEDIR = "";
    string SA72HWFBOHAISURGEDIR = "";
    string SA72HWFBOHAIWAVENOTES = "";
    string SA72HWFNORTHOFYSWAVEHEIGHT = "";
    string SA72HWFNORTHOFYSWAVEDIR = "";
    string SA72HWFNORTHOFYSSURGEDIR = "";
    string SA72HWFNORTHOFYSWAVENOTES = "";
    string SA72HWFMIDDLEOFYSWAVEHEIGHT = "";
    string SA72HWFMIDDLEOFYSWAVEDIR = "";
    string SA72HWFMIDDLEOFYSSURGEDIR = "";
    string SA72HWFMIDDLEOFYSWAVENOTES = "";
    string SA72HWFSOUTHOFYSWAVEHEIGHT = "";
    string SA72HWFSOUTHOFYSWAVEDIR = "";
    string SA72HWFSOUTHOFYSSURGEDIR = "";
    string SA72HWFSOUTHOFYSWAVENOTES = "";
  


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
                string FRELEASEUNIT = tblfooter.Rows[i]["FRELEASEUNIT"].ToString();
                //string FTELEPHONE = tblfooter.Rows[i]["FTELEPHONE"].ToString();
                //string FFAX = tblfooter.Rows[i]["FFAX"].ToString();
                string FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();

                string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话

                string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                // PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
                PUBLISHTIME = PUBLISHDATE + "15时";
                tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                //tblfooter_Model.FFAX = FFAX;
                tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;

                tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                tblfooter_Model.FWAVEFORECASTERTEL = FWAVEFORECASTERTEL;

            }
            //海区24小时海浪预报
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
                SA24HWFBOHAIWAVENOTES = tblseaarea24hwaveforecast.Rows[0]["SA24HWFBOHAIWAVENOTES"].ToString();
                SA24HWFNORTHOFYSWAVENOTES = tblseaarea24hwaveforecast.Rows[0]["SA24HWFNORTHOFYSWAVENOTES"].ToString();
                SA24HWFMIDDLEOFYSWAVENOTES = tblseaarea24hwaveforecast.Rows[0]["SA24HWFMIDDLEOFYSWAVENOTES"].ToString();
                SA24HWFSOUTHOFYSWAVENOTES = tblseaarea24hwaveforecast.Rows[0]["SA24HWFSOUTHOFYSWAVENOTES"].ToString();
            }
            //海区48小时海浪预报
            TBLSEAAREA48HWAVEFORECAST tblseaarea48hwaveforecast_Model = new TBLSEAAREA48HWAVEFORECAST();
            tblseaarea48hwaveforecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblseaarea48hwaveforecast = (System.Data.DataTable)new sql_TBLSEAAREA48HWAVEFORECAST().get_TBLSEAAREA48HWAVEFORECAST_AllData(tblseaarea48hwaveforecast_Model);
            if (tblseaarea48hwaveforecast.Rows.Count == 0) { }
            else
            {
                SA48HWFBOHAIWAVEHEIGHT = tblseaarea48hwaveforecast.Rows[0]["SA48HWFBOHAIWAVEHEIGHT"].ToString();
                SA48HWFBOHAIWAVEDIR = tblseaarea48hwaveforecast.Rows[0]["SA48HWFBOHAIWAVEDIR"].ToString();
                SA48HWFBOHAISURGEDIR = tblseaarea48hwaveforecast.Rows[0]["SA48HWFBOHAISURGEDIR"].ToString();
                SA48HWFBOHAIWAVENOTES = tblseaarea48hwaveforecast.Rows[0]["SA48HWFBOHAIWAVENOTES"].ToString();
                SA48HWFNORTHOFYSWAVEHEIGHT = tblseaarea48hwaveforecast.Rows[0]["SA48HWFNORTHOFYSWAVEHEIGHT"].ToString();
                SA48HWFNORTHOFYSWAVEDIR = tblseaarea48hwaveforecast.Rows[0]["SA48HWFNORTHOFYSWAVEDIR"].ToString();
                SA48HWFNORTHOFYSSURGEDIR = tblseaarea48hwaveforecast.Rows[0]["SA48HWFNORTHOFYSSURGEDIR"].ToString();
                SA48HWFNORTHOFYSWAVENOTES = tblseaarea48hwaveforecast.Rows[0]["SA48HWFNORTHOFYSWAVENOTES"].ToString();
                SA48HWFMIDDLEOFYSWAVEHEIGHT = tblseaarea48hwaveforecast.Rows[0]["SA48HWFMIDDLEOFYSWAVEHEIGHT"].ToString();
                SA48HWFMIDDLEOFYSWAVEDIR = tblseaarea48hwaveforecast.Rows[0]["SA48HWFMIDDLEOFYSWAVEDIR"].ToString();
                SA48HWFMIDDLEOFYSSURGEDIR = tblseaarea48hwaveforecast.Rows[0]["SA48HWFMIDDLEOFYSSURGEDIR"].ToString();
                SA48HWFMIDDLEOFYSWAVENOTES = tblseaarea48hwaveforecast.Rows[0]["SA48HWFMIDDLEOFYSWAVENOTES"].ToString();
                SA48HWFSOUTHOFYSWAVEHEIGHT = tblseaarea48hwaveforecast.Rows[0]["SA48HWFSOUTHOFYSWAVEHEIGHT"].ToString();
                SA48HWFSOUTHOFYSWAVEDIR = tblseaarea48hwaveforecast.Rows[0]["SA48HWFSOUTHOFYSWAVEDIR"].ToString();
                SA48HWFSOUTHOFYSSURGEDIR = tblseaarea48hwaveforecast.Rows[0]["SA48HWFSOUTHOFYSSURGEDIR"].ToString();
                SA48HWFSOUTHOFYSWAVENOTES = tblseaarea48hwaveforecast.Rows[0]["SA48HWFSOUTHOFYSWAVENOTES"].ToString();


            }
            //海区72小时海浪预报
            TBLSEAAREA72HWAVEFORECAST tblseaarea72hwaveforecast_Model = new TBLSEAAREA72HWAVEFORECAST();
            tblseaarea72hwaveforecast_Model.PUBLISHDATE = dt;
            System.Data.DataTable tblseaarea72hwaveforecast = (System.Data.DataTable)new sql_TBLSEAAREA72HWAVEFORECAST().get_TBLSEAAREA72HWAVEFORECAST_AllData(tblseaarea72hwaveforecast_Model);
            if (tblseaarea72hwaveforecast.Rows.Count == 0) { }
            else
            {
                SA72HWFBOHAIWAVEHEIGHT = tblseaarea72hwaveforecast.Rows[0]["SA72HWFBOHAIWAVEHEIGHT"].ToString();
                SA72HWFBOHAIWAVEDIR = tblseaarea72hwaveforecast.Rows[0]["SA72HWFBOHAIWAVEDIR"].ToString();
                SA72HWFBOHAISURGEDIR = tblseaarea72hwaveforecast.Rows[0]["SA72HWFBOHAISURGEDIR"].ToString();
                SA72HWFBOHAIWAVENOTES = tblseaarea72hwaveforecast.Rows[0]["SA72HWFBOHAIWAVENOTES"].ToString();
                SA72HWFNORTHOFYSWAVEHEIGHT = tblseaarea72hwaveforecast.Rows[0]["SA72HWFNORTHOFYSWAVEHEIGHT"].ToString();
                SA72HWFNORTHOFYSWAVEDIR = tblseaarea72hwaveforecast.Rows[0]["SA72HWFNORTHOFYSWAVEDIR"].ToString();
                SA72HWFNORTHOFYSSURGEDIR = tblseaarea72hwaveforecast.Rows[0]["SA72HWFNORTHOFYSSURGEDIR"].ToString();
                SA72HWFNORTHOFYSWAVENOTES = tblseaarea72hwaveforecast.Rows[0]["SA72HWFNORTHOFYSWAVENOTES"].ToString();
                SA72HWFMIDDLEOFYSWAVEHEIGHT = tblseaarea72hwaveforecast.Rows[0]["SA72HWFMIDDLEOFYSWAVEHEIGHT"].ToString();
                SA72HWFMIDDLEOFYSWAVEDIR = tblseaarea72hwaveforecast.Rows[0]["SA72HWFMIDDLEOFYSWAVEDIR"].ToString();
                SA72HWFMIDDLEOFYSSURGEDIR = tblseaarea72hwaveforecast.Rows[0]["SA72HWFMIDDLEOFYSSURGEDIR"].ToString();
                SA72HWFMIDDLEOFYSWAVENOTES = tblseaarea72hwaveforecast.Rows[0]["SA72HWFMIDDLEOFYSWAVENOTES"].ToString();
                SA72HWFSOUTHOFYSWAVEHEIGHT = tblseaarea72hwaveforecast.Rows[0]["SA72HWFSOUTHOFYSWAVEHEIGHT"].ToString();
                SA72HWFSOUTHOFYSWAVEDIR = tblseaarea72hwaveforecast.Rows[0]["SA72HWFSOUTHOFYSWAVEDIR"].ToString();
                SA72HWFSOUTHOFYSSURGEDIR = tblseaarea72hwaveforecast.Rows[0]["SA72HWFSOUTHOFYSSURGEDIR"].ToString();
                SA72HWFSOUTHOFYSWAVENOTES = tblseaarea72hwaveforecast.Rows[0]["SA72HWFSOUTHOFYSWAVENOTES"].ToString();


            }

            //为了方便管理声明书签数组]]]]]]]]b     bbbbbbbbbbbbbbb b
            object[] BookMark = new object[54];//新增海浪预报员电话53改为54
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报值班
            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真    
            BookMark[3] = "FWAVEFORECASTER"; //海浪预报员
            BookMark[4] = "SA24HWFBOHAIWAVEHEIGHT";
            BookMark[5] = "SA24HWFBOHAIWAVEDIR";
            BookMark[6] = "SA24HWFBOHAISURGEDIR";
            BookMark[7] = "SA24HWFNORTHOFYSWAVEHEIGHT";
            BookMark[8] = "SA24HWFNORTHOFYSWAVEDIR";
            BookMark[9] = "SA24HWFNORTHOFYSSURGEDIR";
            BookMark[10] = "SA24HWFMIDDLEOFYSWAVEHEIGHT";
            BookMark[11] = "SA24HWFMIDDLEOFYSWAVEDIR";
            BookMark[12] = "SA24HWFMIDDLEOFYSSURGEDIR";
            BookMark[13] = "SA24HWFSOUTHOFYSWAVEHEIGHT";
            BookMark[14] = "SA24HWFSOUTHOFYSWAVEDIR";
            BookMark[15] = "SA24HWFSOUTHOFYSSURGEDIR";
            BookMark[16] = "SA24HWFBOHAIWAVENOTES";
            BookMark[17] = "SA24HWFNORTHOFYSWAVENOTES";
            BookMark[18] = "SA24HWFMIDDLEOFYSWAVENOTES";
            BookMark[19] = "SA24HWFSOUTHOFYSWAVENOTES";
            BookMark[20] = "SA48HWFBOHAIWAVEHEIGHT";
            BookMark[21] = "SA48HWFBOHAIWAVEDIR";
            BookMark[22] = "SA48HWFBOHAISURGEDIR";
            BookMark[23] = "SA48HWFBOHAIWAVENOTES";
            BookMark[24] = "SA48HWFNORTHOFYSWAVEHEIGHT";
            BookMark[25] = "SA48HWFNORTHOFYSWAVEDIR";
            BookMark[26] = "SA48HWFNORTHOFYSSURGEDIR";
            BookMark[27] = "SA48HWFNORTHOFYSWAVENOTES";
            BookMark[28] = "SA48HWFMIDDLEOFYSWAVEHEIGHT";
            BookMark[29] = "SA48HWFMIDDLEOFYSWAVEDIR";
            BookMark[30] = "SA48HWFMIDDLEOFYSSURGEDIR";
            BookMark[31] = "SA48HWFMIDDLEOFYSWAVENOTES";
            BookMark[32] = "SA48HWFSOUTHOFYSWAVEHEIGHT";
            BookMark[33] = "SA48HWFSOUTHOFYSWAVEDIR";
            BookMark[34] = "SA48HWFSOUTHOFYSSURGEDIR";
            BookMark[35] = "SA48HWFSOUTHOFYSWAVENOTES";
            BookMark[36] = "SA72HWFBOHAIWAVEHEIGHT";
            BookMark[37] = "SA72HWFBOHAIWAVEDIR";
            BookMark[38] = "SA72HWFBOHAISURGEDIR";
            BookMark[39] = "SA72HWFBOHAIWAVENOTES";
            BookMark[40] = "SA72HWFNORTHOFYSWAVEHEIGHT";
            BookMark[41] = "SA72HWFNORTHOFYSWAVEDIR";
            BookMark[42] = "SA72HWFNORTHOFYSSURGEDIR";
            BookMark[43] = "SA72HWFNORTHOFYSWAVENOTES";
            BookMark[44] = "SA72HWFMIDDLEOFYSWAVEHEIGHT";
            BookMark[45] = "SA72HWFMIDDLEOFYSWAVEDIR";
            BookMark[46] = "SA72HWFMIDDLEOFYSSURGEDIR";
            BookMark[47] = "SA72HWFMIDDLEOFYSWAVENOTES";
            BookMark[48] = "SA72HWFSOUTHOFYSWAVEHEIGHT";
            BookMark[49] = "SA72HWFSOUTHOFYSWAVEDIR";
            BookMark[50] = "SA72HWFSOUTHOFYSSURGEDIR";
            BookMark[51] = "SA72HWFSOUTHOFYSWAVENOTES";
            BookMark[52] = "PUBLISHTIME";

            BookMark[53] = "FWAVEFORECASTERTEL";//海浪预报员电话


            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = SA24HWFBOHAIWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = SA24HWFBOHAIWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = SA24HWFBOHAISURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = SA24HWFNORTHOFYSWAVEHEIGHT;

            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = SA24HWFNORTHOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = SA24HWFNORTHOFYSSURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = SA24HWFMIDDLEOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = SA24HWFMIDDLEOFYSWAVEDIR;


            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = SA24HWFMIDDLEOFYSSURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = SA24HWFSOUTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = SA24HWFSOUTHOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = SA24HWFSOUTHOFYSSURGEDIR;

            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = SA24HWFBOHAIWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = SA24HWFNORTHOFYSWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = SA24HWFMIDDLEOFYSWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = SA24HWFSOUTHOFYSWAVENOTES;

            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = SA48HWFBOHAIWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = SA48HWFBOHAIWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = SA48HWFBOHAISURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = SA48HWFBOHAIWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = SA48HWFNORTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = SA48HWFNORTHOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = SA48HWFNORTHOFYSSURGEDIR;

            doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = SA48HWFNORTHOFYSWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[28]).Range.Text = SA48HWFMIDDLEOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[29]).Range.Text = SA48HWFMIDDLEOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[30]).Range.Text = SA48HWFMIDDLEOFYSSURGEDIR;


            doc.Bookmarks.get_Item(ref BookMark[31]).Range.Text = SA48HWFMIDDLEOFYSWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[32]).Range.Text = SA48HWFSOUTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[33]).Range.Text = SA48HWFSOUTHOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[34]).Range.Text = SA48HWFSOUTHOFYSSURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[35]).Range.Text = SA48HWFSOUTHOFYSWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[36]).Range.Text = SA72HWFBOHAIWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[37]).Range.Text = SA72HWFBOHAIWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[38]).Range.Text = SA72HWFBOHAISURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[39]).Range.Text = SA72HWFBOHAIWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[40]).Range.Text = SA72HWFNORTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[41]).Range.Text = SA72HWFNORTHOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[42]).Range.Text = SA72HWFNORTHOFYSSURGEDIR;

            doc.Bookmarks.get_Item(ref BookMark[43]).Range.Text = SA72HWFNORTHOFYSWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[44]).Range.Text = SA72HWFMIDDLEOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[45]).Range.Text = SA72HWFMIDDLEOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[46]).Range.Text = SA72HWFMIDDLEOFYSSURGEDIR;


            doc.Bookmarks.get_Item(ref BookMark[47]).Range.Text = SA72HWFMIDDLEOFYSWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[48]).Range.Text = SA72HWFSOUTHOFYSWAVEHEIGHT;
            doc.Bookmarks.get_Item(ref BookMark[49]).Range.Text = SA72HWFSOUTHOFYSWAVEDIR;
            doc.Bookmarks.get_Item(ref BookMark[50]).Range.Text = SA72HWFSOUTHOFYSSURGEDIR;
            doc.Bookmarks.get_Item(ref BookMark[51]).Range.Text = SA72HWFSOUTHOFYSWAVENOTES;
            doc.Bookmarks.get_Item(ref BookMark[52]).Range.Text = PUBLISHTIME;

            doc.Bookmarks.get_Item(ref BookMark[53]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;

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