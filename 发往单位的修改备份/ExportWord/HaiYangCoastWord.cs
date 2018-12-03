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
    public class HaiYangCoastWord
    {
        string PUBLISHTIME = "";

        string WAVELEVELONE ="";
        string WAVELEVELTWO ="";
        string WAVELEVELTYPE ="";
        string WAVEDIRECTION ="";
        string WATERTEMPERATURE ="";

        string FIRSTHIGHTIME ="";
        string FIRSTHIGHLEVEL ="";
        string FIRSTLOWTIME ="";
        string FIRSTLOWLEVEL ="";
        string SECONDHIGHTIME ="";
        string SECONDHIGHLEVEL ="";
        string SECONDLOWTIME ="";
        string SECONDLOWLEVEL ="";

        string WEATERSTATE ="";
        string TEMPERATURE ="";
        string WINDSPEED ="";
        string WINDDIRECTION ="";
        string WAVEHEIGHT ="";

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
                //填报信息表提取数据
                TBLFOOTER tblfooter_Model = new TBLFOOTER();
                tblfooter_Model.PUBLISHDATE = dt;
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
                    PUBLISHTIME = PUBLISHDATE + "10时";

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
                object mark_PUBLISHTIME = "PUBLISHTIME";
                doc.Bookmarks.get_Item(ref mark_PUBLISHTIME).Range.Text = PUBLISHTIME;

                //上午十一、烟台南部海浪、水温预报
                Sql_YT_WaveForecast sqlWaveForecast = new Sql_YT_WaveForecast();
                YT_WaveForecast modelWaveForecast = new YT_WaveForecast();
                modelWaveForecast.PUBLISHDATE = dt;
                DataTable dtWaveForecast = new DataTable();
                dtWaveForecast = (DataTable)sqlWaveForecast.GetWaveDataBy_T(modelWaveForecast);
                if (dtWaveForecast != null && dtWaveForecast.Rows.Count > 0)
                {
                    WAVELEVELONE = dtWaveForecast.Rows[0]["WAVELEVELONE"].ToString();
                    WAVELEVELTYPE = dtWaveForecast.Rows[0]["WAVELEVELTYPE"].ToString();
                    WAVEDIRECTION = dtWaveForecast.Rows[0]["WAVEDIRECTION"].ToString();
                    WATERTEMPERATURE = dtWaveForecast.Rows[0]["WATERTEMPERATURE"].ToString();
                }
                //上午十二、海阳近岸海域潮汐预报
                Sql_YT_TideForecast sqlTideForecast = new Sql_YT_TideForecast();
                YT_TideForecast modelTideForecast = new YT_TideForecast();
                modelTideForecast.PUBLISHDATE = dt;
                DataTable dtTideForecast = new DataTable();
                dtTideForecast = (DataTable)sqlTideForecast.GetTideDataBy_T(modelTideForecast);
                if (dtTideForecast != null && dtTideForecast.Rows.Count > 0)
                {
                    FIRSTHIGHTIME = dtTideForecast.Rows[0]["FIRSTHIGHTIME"].ToString();
                    FIRSTHIGHLEVEL = dtTideForecast.Rows[0]["FIRSTHIGHLEVEL"].ToString();
                    FIRSTLOWTIME = dtTideForecast.Rows[0]["FIRSTLOWTIME"].ToString();
                    FIRSTLOWLEVEL = dtTideForecast.Rows[0]["FIRSTLOWLEVEL"].ToString();
                    SECONDHIGHTIME = dtTideForecast.Rows[0]["SECONDHIGHTIME"].ToString();
                    SECONDHIGHLEVEL = dtTideForecast.Rows[0]["SECONDHIGHLEVEL"].ToString();
                    SECONDLOWTIME = dtTideForecast.Rows[0]["SECONDLOWTIME"].ToString();
                    SECONDLOWLEVEL = dtTideForecast.Rows[0]["SECONDLOWLEVEL"].ToString();
                }
                //上午十三、海阳万米海滩海水浴场风、浪预报
                Sql_YT_YC sqlYC = new Sql_YT_YC();
                YT_YC modelYC = new YT_YC();
                modelYC.PUBLISHDATE = dt;
                DataTable dtYC = new DataTable();
                dtYC = (DataTable)sqlYC.GetYcDataBy_T(modelYC);
                if (dtYC != null && dtYC.Rows.Count > 0)
                {
                    WEATERSTATE = dtYC.Rows[0]["WEATERSTATE"].ToString();
                    TEMPERATURE = dtYC.Rows[0]["TEMPERATURE"].ToString();
                    WINDSPEED = dtYC.Rows[0]["WINDSPEED"].ToString();
                    WINDDIRECTION = dtYC.Rows[0]["WINDDIRECTION"].ToString();
                    WAVEHEIGHT = dtYC.Rows[0]["WAVEHEIGHT"].ToString();
                }
                object[] BookMark = new object[26];//新增3个预报员电话，23改为26
                BookMark[0] = "FRELEASEUNIT";//发布单位
                BookMark[1] = "FZHIBANTEL";//预报值班
                BookMark[2] = "FSENDTEL";//预报值班
                //BookMark[1] = "FTELEPHONE";//电话
                //BookMark[2] = "FFAX";//传真
                BookMark[3] = "FWAVEFORECASTER";//海浪预报员
                BookMark[4] = "FTIDALFORECASTER"; //潮汐预报员
                BookMark[5] = "FWATERTEMPERATUREFORECASTER";//海温预报员

                BookMark[6] = "WAVELEVELONE";
                BookMark[7] = "WAVELEVELTYPE";
                BookMark[8] = "WAVEDIRECTION";
                BookMark[9] = "WATERTEMPERATURE";


                BookMark[10] = "FIRSTHIGHTIME";
                BookMark[11] = "FIRSTHIGHLEVEL";
                BookMark[12] = "FIRSTLOWTIME";
                BookMark[13] = "FIRSTLOWLEVEL";
                BookMark[14] = "SECONDHIGHTIME";
                BookMark[15] = "SECONDHIGHLEVEL";
                BookMark[16] = "SECONDLOWTIME";
                BookMark[17] = "SECONDLOWLEVEL";

                BookMark[18] = "WEATERSTATE";
                BookMark[19] = "TEMPERATURE";
                BookMark[20] = "WINDSPEED";
                BookMark[21] = "WINDDIRECTION";
                BookMark[22] = "WAVEHEIGHT";

                BookMark[23] = "FWAVEFORECASTERTEL";//海浪预报员电话
                BookMark[24] = "FTIDALFORECASTERTEL"; //潮汐预报员电话
                BookMark[25] = "FWATERTEMPERATUREFORECASTERTEL";//海温预报员电话


                //赋值数据到书签的位置
                doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;
                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
                doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送
                //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
                //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
                doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWAVEFORECASTER;
                doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = tblfooter_Model.FTIDALFORECASTER;
                doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;

                doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = WAVELEVELONE;
                doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = WAVELEVELTYPE;
                doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = WAVEDIRECTION;
                doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = WATERTEMPERATURE;
                doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = FIRSTHIGHTIME;
                doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = FIRSTHIGHLEVEL;
                doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = FIRSTLOWTIME;
                doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = FIRSTLOWLEVEL;
                doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = SECONDHIGHTIME;
                doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = SECONDHIGHLEVEL;
                doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = SECONDLOWTIME;
                doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = SECONDLOWLEVEL;
                doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = WEATERSTATE;
                doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = TEMPERATURE;
                doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = WINDSPEED;
                doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = WINDDIRECTION;
                doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = WAVEHEIGHT;

                doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;
                doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;
                doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;

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