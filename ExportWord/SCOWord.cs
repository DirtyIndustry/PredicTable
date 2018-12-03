using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Word = Microsoft.Office.Interop.Word;
namespace PredicTable.ExportWord
{
    /// <summary>
    /// 近海水温实体类
    /// </summary>
    public class OffShareWaterTemperature
    {
        public string MAXVALUE_24H { get; set; }//外海第1天最高温度
        public string MINVALUE_24H { get; set; }//外海第1天最低温度
        public string AVERAGE_24H { get; set; }//外海第1天最平均温度
        public string MAXVALUE_48H { get; set; }//外海第2天最高温度
        public string MINVALUE_48H { get; set; }//外海第2天最低温度
        public string AVERAGE_48H { get; set; }//外海第2天最平均温度
        public string MAXVALUE_72H { get; set; }//外海第3天最高温度
        public string MINVALUE_72H { get; set; }//外海第3天最低温度
        public string AVERAGE_72H { get; set; }//外海第3天最平均温度
    }       

    /// <summary>
    /// 生成上合峰会转向海洋预报单 
    /// </summary>
    public class SCOWord
    { 
        public SCOWord()
        {
        }
        string PublishVersion = "";//期数
        string PublishTime = "";//发布时间 
        string Summarize = "";//综述

        #region 表二 近海潮汐 24
        
        string OffShareFirstTideHighTime1 = "";//第1天第一次高潮时
        string OffShareFirstTideHighData1 = "";//第1天第一次高潮潮高
        string OffShareFirstTideHighTime2 = "";//第2天第一次高潮时
        string OffShareFirstTideHighData2 = "";//第2天第一次高潮潮高
        string OffShareFirstTideHighTime3 = "";//第3天第一次高潮时
        string OffShareFirstTideHighData3 = "";//第3天第一次高潮潮高

        string OffShareFirstTideLowTime1 = "";//第1天第一次低潮时
        string OffShareFirstTideLowData1 = "";//第1天第一次低潮潮高
        string OffShareFirstTideLowTime2 = "";//第2天第一次低潮时
        string OffShareFirstTideLowData2 = "";//第2天第一次低潮潮高
        string OffShareFirstTideLowTime3 = "";//第3天第一次低潮时
        string OffShareFirstTideLowData3 = "";//第3天第一次低潮潮高

        string OffShareSecondTideHighTime1 = "";//第1天第二次高潮时
        string OffShareSecondTideHighData1 = "";//第1天第二次高潮潮高
        string OffShareSecondTideHighTime2 = "";//第2天第二次高潮时
        string OffShareSecondTideHighData2 = "";//第2天第二次高潮潮高
        string OffShareSecondTideHighTime3 = "";//第3天第二次高潮时
        string OffShareSecondTideHighData3 = "";//第3天第二次高潮潮高

        string OffShareSecondTideLowTime1 = "";//第1天第二次低潮时
        string OffShareSecondTideLowData1 = "";//第1天第二次低潮潮高
        string OffShareSecondTideLowTime2 = "";//第2天第二次低潮时
        string OffShareSecondTideLowData2 = "";//第2天第二次低潮潮高
        string OffShareSecondTideLowTime3 = "";//第3天第二次低潮时
        string OffShareSecondTideLowData3 = "";//第3天第二次低潮潮高

        #endregion 

        /// <summary>
        /// 调用模板生成word
        /// </summary> 
        /// <param name="templateFile">模板文件</param>
        /// <param name="fileName">生成的具有模板样式的新文件</param>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public int ExportWord(string templateFile, string fileName, DateTime dt, string hour)
        {
            PublishTime = dt.ToString("yyyy年MM月dd日") + hour + "时";
            int PublishTime_Day = dt.Day;
            //生成word程序对象
            Word.Application app = new Word.Application();

            // 模板文件
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
                string tempName = "";
                sql_SCOTableList sql_Sh = new sql_SCOTableList();
                DataTable tbl0 = (DataTable)sql_Sh.GetTableSumAndPeriod(dt);//取出期数和综述
                StringBuilder sb = new StringBuilder();
                if (tbl0 != null && tbl0.Rows.Count > 0)
                {
                    PublishVersion = tbl0.Rows[0]["PERIODS"].ToString();
                    Summarize = tbl0.Rows[0]["USUMMARIZE"].ToString();
                    Summarize += "                                                                            ";
                }

                DataTable tbl2 = (DataTable)sql_Sh.GetTableOffShoreWind(dt);//获取近海风、天气
               
                List<object> SH_JH_Wind_List = new ModelConvertHelper<SCOFLModel>().ConvertToModel(tbl2);
               
                #region
                DataTable tbl3 = (DataTable)sql_Sh.GetTableOffShoreWave(dt);//获取近波浪 
                List<object> SH_JH_Wave_List = new ModelConvertHelper<SCOWaveModel>().ConvertToModel(tbl3);
                
                //近海潮汐
                DataTable tbl7 = (DataTable)sql_Sh.GetQingDaoTide(dt);
                if (tbl7==null||tbl7.Rows.Count == 0) { }
                else
                {
                    //第一天第一次
                    OffShareFirstTideHighTime1 = tbl7.Rows[0]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData1 = tbl7.Rows[0]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime1= tbl7.Rows[0]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData1= tbl7.Rows[0]["FIRSTLOWLEVEL"].ToString();
                    //第一天第二次
                    OffShareSecondTideHighTime1 = tbl7.Rows[0]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData1= tbl7.Rows[0]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime1 = tbl7.Rows[0]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData1 = tbl7.Rows[0]["SECONDLOWLEVEL"].ToString();

                    //第二天第一次
                    OffShareFirstTideHighTime2 = tbl7.Rows[1]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData2 = tbl7.Rows[1]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime2 = tbl7.Rows[1]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData2 = tbl7.Rows[1]["FIRSTLOWLEVEL"].ToString();
                    //第二天第二次
                    OffShareSecondTideHighTime2 = tbl7.Rows[1]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData2 = tbl7.Rows[1]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime2 = tbl7.Rows[1]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData2 = tbl7.Rows[1]["SECONDLOWLEVEL"].ToString();

                    //第三天第一次
                    OffShareFirstTideHighTime3 = tbl7.Rows[2]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData3 = tbl7.Rows[2]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime3 = tbl7.Rows[2]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData3 = tbl7.Rows[2]["FIRSTLOWLEVEL"].ToString();
                    //第三天第二次
                    OffShareSecondTideHighTime3 = tbl7.Rows[2]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData3 = tbl7.Rows[2]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime3 = tbl7.Rows[2]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData3 = tbl7.Rows[2]["SECONDLOWLEVEL"].ToString();
                }
                //近海水温
                DataTable tbl6 = (DataTable)sql_Sh.GetOffShareWaterTemperature(dt);
                List<object> JHSWList1 = new ModelConvertHelper<OffShareWaterTemperature>().ConvertToModel(tbl6);
                
                DataTable tbl4 = (DataTable)sql_Sh.GetTableOffOnShoreWind(dt);//获取外海风、天气
                List<object> SH_WH_Wind_List = new ModelConvertHelper<SCOFLModel>().ConvertToModel(tbl4);
                
                DataTable tbl5 = (DataTable)sql_Sh.GetTableOpenShoreWave(dt);//获取外海波浪
                List<object> SH_WH_Wave_List = new ModelConvertHelper<SCOWaveModel>().ConvertToModel(tbl5);
                
                DataTable tbl15 = (DataTable)sql_Sh.GetOnShoreSW(dt);//获取外海水温
                List<object> WHSWList1 = new ModelConvertHelper<OffShareWaterTemperature>().ConvertToModel(tbl15);

                //为了方便管理声明书签数组

                object[] BookMark = NewBookmarks();
                //// List<object> BookMark = new List<object>();


                #region
                doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = PublishVersion;
                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = PublishTime;
                doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = Summarize;
                //给日期书签赋值
                DateBookMarksToData(dt,doc,"shanghe");
                //近、外海天气、风书签赋值
                for (int i = 1; i <= 32; i++)
                {
                    if (i < 7)
                    {
                        object obBookMark = "OffShareWeather" + i;
                        object obBookMark_On = "OnShareWeather" + i;
                        if (SH_JH_Wind_List.Count > 0)
                        {
                            doc.Bookmarks.get_Item(ref obBookMark).Range.Text = SH_JH_Wind_List[i + 1].ToString();
                        }
                        if (SH_WH_Wind_List.Count > 0)
                        {
                            doc.Bookmarks.get_Item(ref obBookMark_On).Range.Text = SH_WH_Wind_List[i + 1].ToString();
                        }
                    }
                    object obBookMark1 = "OffShareWindForce" + i;
                    object obBookMark2 = "OffShareWindDirection" + i;

                    object obBookMark1_On = "OnShareWindForce" + i;
                    object obBookMark2_On = "OnShareWindDirection" + i;
                    if (SH_JH_Wind_List.Count > 0)
                    {
                        doc.Bookmarks.get_Item(ref obBookMark1).Range.Text = SH_JH_Wind_List[i + 7].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2).Range.Text = SH_JH_Wind_List[i + 39].ToString();
                    }
                    if (SH_WH_Wind_List.Count > 0)
                    {
                        doc.Bookmarks.get_Item(ref obBookMark1_On).Range.Text = SH_WH_Wind_List[i + 7].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2_On).Range.Text = SH_WH_Wind_List[i + 39].ToString();
                    }
                }


                //近、外海浪书签赋值
                for (int i = 1; i <= 32; i++)
                {
                    object obBookMark1 = "OffShareWaveHeight" + i;
                    object obBookMark2 = "OffShareWaveDirection" + i;
                    object obBookMark1_On = "OnShareWaveHeight" + i;
                    object obBookMark2_On = "OnShareWaveDirection" + i;
                    if (SH_JH_Wave_List.Count > 0) { 
                    doc.Bookmarks.get_Item(ref obBookMark1).Range.Text = SH_JH_Wave_List[i + 1].ToString();
                    doc.Bookmarks.get_Item(ref obBookMark2).Range.Text = SH_JH_Wave_List[i + 33].ToString();
                    }
                    if (SH_WH_Wave_List.Count > 0) { 
                    doc.Bookmarks.get_Item(ref obBookMark1_On).Range.Text = SH_WH_Wave_List[i + 1].ToString();
                    doc.Bookmarks.get_Item(ref obBookMark2_On).Range.Text = SH_WH_Wave_List[i + 33].ToString();
                    }
                }
                //内、外海水温书签赋值
                for (int i = 1; i <= 3; i++)
                {
                    object obBookMark = "OnShareMaxTemp" + i;
                    object obBookMark1 = "OnShareMinTemp" + i;
                    object obBookMark2 = "OnShareAvgTemp" + i;
                    object obBookMark_off = "OffShareMaxTemp" + i;
                    object obBookMark1_off = "OffShareMinTemp" + i;
                    object obBookMark2_off = "OffShareAvgTemp" + i;
                    if (WHSWList1.Count == 0) { }
                    else
                    {
                        doc.Bookmarks.get_Item(ref obBookMark).Range.Text = WHSWList1[3 * i - 3].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark1).Range.Text = WHSWList1[3 * i - 2].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2).Range.Text = WHSWList1[3 * i - 1].ToString();
                    }
                    if (JHSWList1.Count == 0) { }
                    else
                    {
                        doc.Bookmarks.get_Item(ref obBookMark_off).Range.Text = JHSWList1[3 * i - 3].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark1_off).Range.Text = JHSWList1[3 * i - 2].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2_off).Range.Text = JHSWList1[3 * i - 1].ToString();
                    }
                }
                #endregion

                doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = OffShareFirstTideHighTime1;
                doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = OffShareFirstTideHighData1;
                doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = OffShareFirstTideLowTime1;
                doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = OffShareFirstTideLowData1;
                doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = OffShareSecondTideHighTime1;
                doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = OffShareSecondTideHighData1;
                doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = OffShareSecondTideLowTime1;
                doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = OffShareSecondTideLowData1;
                doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = OffShareFirstTideHighTime2;
                doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = OffShareFirstTideHighData2;
                doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = OffShareFirstTideLowTime2;
                doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = OffShareFirstTideLowData2;
                doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = OffShareSecondTideHighTime2;
                doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = OffShareSecondTideHighData2;
                doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = OffShareSecondTideLowTime2;
                doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = OffShareSecondTideLowData2;
                doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = OffShareFirstTideHighTime3;
                doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = OffShareFirstTideHighData3;
                doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = OffShareFirstTideLowTime3;
                doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = OffShareFirstTideLowData3;
                doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = OffShareSecondTideHighTime3;
                doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = OffShareSecondTideHighData3;
                doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = OffShareSecondTideLowTime3;
                doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = OffShareSecondTideLowData3;
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
                WriteLog.Write("生成上合峰会专项预报单失败" + ex.ToString());
                return 0;
            }

        }
        /// <summary>
        /// 生成上合平台预报word
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="fileName"></param>
        /// <param name="dt"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public int ExportWord1(string templateFile, string fileName, DateTime dt, string hour)
        {
            PublishTime = dt.ToString("yyyy年MM月dd日") + hour + "时";
            int PublishTime_Day = dt.Day;
            //生成word程序对象
            Word.Application app = new Word.Application();

            // 模板文件
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
                ref missing, ref missing, ref  missing,
                ref missing);
            doc.Activate();
            //数据库查询
            try
            {
                string tempName = "";
                sql_SCOTableList sql_Sh = new sql_SCOTableList();
                #region 获取近海浪/水温/潮汐数据
                //近海波浪 
                DataTable tbl3 = (DataTable)sql_Sh.GetTableOffShoreWave(dt);//获取近波浪 
                List<object> SH_JH_Wave_List = new ModelConvertHelper<SCOWaveModel>().ConvertToModel(tbl3);

                //近海潮汐
                DataTable tbl7 = (DataTable)sql_Sh.GetQingDaoTide(dt);
                if (tbl7 == null || tbl7.Rows.Count == 0) { }
                else
                {
                    //第一天第一次
                    OffShareFirstTideHighTime1 = tbl7.Rows[0]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData1 = tbl7.Rows[0]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime1 = tbl7.Rows[0]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData1 = tbl7.Rows[0]["FIRSTLOWLEVEL"].ToString();
                    //第一天第二次
                    OffShareSecondTideHighTime1 = tbl7.Rows[0]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData1 = tbl7.Rows[0]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime1 = tbl7.Rows[0]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData1 = tbl7.Rows[0]["SECONDLOWLEVEL"].ToString();

                    //第二天第一次
                    OffShareFirstTideHighTime2 = tbl7.Rows[1]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData2 = tbl7.Rows[1]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime2 = tbl7.Rows[1]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData2 = tbl7.Rows[1]["FIRSTLOWLEVEL"].ToString();
                    //第二天第二次
                    OffShareSecondTideHighTime2 = tbl7.Rows[1]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData2 = tbl7.Rows[1]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime2 = tbl7.Rows[1]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData2 = tbl7.Rows[1]["SECONDLOWLEVEL"].ToString();

                    //第三天第一次
                    OffShareFirstTideHighTime3 = tbl7.Rows[2]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData3 = tbl7.Rows[2]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime3 = tbl7.Rows[2]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData3 = tbl7.Rows[2]["FIRSTLOWLEVEL"].ToString();
                    //第三天第二次
                    OffShareSecondTideHighTime3 = tbl7.Rows[2]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData3 = tbl7.Rows[2]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime3 = tbl7.Rows[2]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData3 = tbl7.Rows[2]["SECONDLOWLEVEL"].ToString();
                }
                //近海水温
                DataTable tbl6 = (DataTable)sql_Sh.GetOffShareWaterTemperature(dt);
                List<object> JHSWList1 = new ModelConvertHelper<OffShareWaterTemperature>().ConvertToModel(tbl6);
                #endregion
                //为了方便管理声明书签数组  

                object[] BookMark = NewBookmarks();
                 
                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = PublishTime;

                DateBookMarksToData(dt, doc, "pingtai");

                //近海海水温书签赋值
                OffShoreWaterTemp(JHSWList1, doc);
                //近海浪书签赋值
                for (int i = 1; i <= 32; i++)
                {
                    object obBookMark1 = "OffShareWaveHeight" + i;
                    object obBookMark2 = "OffShareWaveDirection" + i;
                    if (SH_JH_Wave_List.Count > 0)
                    {
                        doc.Bookmarks.get_Item(ref obBookMark1).Range.Text = SH_JH_Wave_List[i + 1].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2).Range.Text = SH_JH_Wave_List[i + 33].ToString();
                    }
                }
                #region
                //潮汐书签赋值
                doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = OffShareFirstTideHighTime1;
                doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = OffShareFirstTideHighData1;
                doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = OffShareFirstTideLowTime1;
                doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = OffShareFirstTideLowData1;
                doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = OffShareSecondTideHighTime1;
                doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = OffShareSecondTideHighData1;
                doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = OffShareSecondTideLowTime1;
                doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = OffShareSecondTideLowData1;
                doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = OffShareFirstTideHighTime2;
                doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = OffShareFirstTideHighData2;
                doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = OffShareFirstTideLowTime2;
                doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = OffShareFirstTideLowData2;
                doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = OffShareSecondTideHighTime2;
                doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = OffShareSecondTideHighData2;
                doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = OffShareSecondTideLowTime2;
                doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = OffShareSecondTideLowData2;
                doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = OffShareFirstTideHighTime3;
                doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = OffShareFirstTideHighData3;
                doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = OffShareFirstTideLowTime3;
                doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = OffShareFirstTideLowData3;
                doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = OffShareSecondTideHighTime3;
                doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = OffShareSecondTideHighData3;
                doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = OffShareSecondTideLowTime3;
                doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = OffShareSecondTideLowData3;
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
                WriteLog.Write("生成上合峰会平台预报单失败" + ex.ToString());
                return 0;
            }

        }
        /// <summary>
        /// 绿潮生成文件
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="fileName"></param>
        /// <param name="dt"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public int LvChaoExportWord(string templateFile, string fileName, DateTime dt, string hour)
        {
            PublishTime = dt.ToString("yyyy年MM月dd日") + hour + "时";
            int PublishTime_Day = dt.Day;
            //生成word程序对象
            Word.Application app = new Word.Application();

            // 模板文件
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
                string tempName = "";
                sql_SCOTableList sql_Sh = new sql_SCOTableList();
                DataTable tbl0 = (DataTable)sql_Sh.GetTableSumAndPeriod(dt);//取出期数和综述
                StringBuilder sb = new StringBuilder();
                if (tbl0 != null && tbl0.Rows.Count > 0)
                {
                    PublishVersion = tbl0.Rows[0]["PERIODS"].ToString();
                    Summarize = tbl0.Rows[0]["USUMMARIZE"].ToString();
                    Summarize += "                                                                            ";
                }
                #region
                //获取近海风、浪、天气
                DataTable tbl2 = (DataTable)sql_Sh.GetTableOffShoreWindAndWave(dt);
                List<object> SH_JH_WindAndWaveAndWeather_List = new ModelConvertHelper<LvChaoWindAndWaveModel>().ConvertToModel(tbl2);

                //近海水温
                DataTable tbl6 = (DataTable)sql_Sh.GetOffShareWaterTemperature(dt);
                List<object> JHSWList1 = new ModelConvertHelper<OffShareWaterTemperature>().ConvertToModel(tbl6);
                //DataTable tbl3 = (DataTable)sql_Sh.GetTableOffShoreWave(dt);//获取近波浪 
                //List<object> SH_JH_Wave_List = new ModelConvertHelper<SCOWaveModel>().ConvertToModel(tbl3);
                //DataTable tbl5 = (DataTable)sql_Sh.GetTableOpenShoreWave(dt);//获取外海波浪
                //List<object> SH_WH_Wave_List = new ModelConvertHelper<SCOWaveModel>().ConvertToModel(tbl5);

                //近海潮汐
                DataTable tbl7 = (DataTable)sql_Sh.GetQingDaoTide(dt);
                if (tbl7 == null || tbl7.Rows.Count == 0) { }
                else
                {
                    //第一天第一次
                    OffShareFirstTideHighTime1 = tbl7.Rows[0]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData1 = tbl7.Rows[0]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime1 = tbl7.Rows[0]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData1 = tbl7.Rows[0]["FIRSTLOWLEVEL"].ToString();
                    //第一天第二次
                    OffShareSecondTideHighTime1 = tbl7.Rows[0]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData1 = tbl7.Rows[0]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime1 = tbl7.Rows[0]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData1 = tbl7.Rows[0]["SECONDLOWLEVEL"].ToString();

                    //第二天第一次
                    OffShareFirstTideHighTime2 = tbl7.Rows[1]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData2 = tbl7.Rows[1]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime2 = tbl7.Rows[1]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData2 = tbl7.Rows[1]["FIRSTLOWLEVEL"].ToString();
                    //第二天第二次
                    OffShareSecondTideHighTime2 = tbl7.Rows[1]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData2 = tbl7.Rows[1]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime2 = tbl7.Rows[1]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData2 = tbl7.Rows[1]["SECONDLOWLEVEL"].ToString();

                    //第三天第一次
                    OffShareFirstTideHighTime3 = tbl7.Rows[2]["FIRSTHIGHTIME"].ToString();
                    OffShareFirstTideHighData3 = tbl7.Rows[2]["FIRSTHIGHLEVEL"].ToString();
                    OffShareFirstTideLowTime3 = tbl7.Rows[2]["FIRSTLOWTIME"].ToString();
                    OffShareFirstTideLowData3 = tbl7.Rows[2]["FIRSTLOWLEVEL"].ToString();
                    //第三天第二次
                    OffShareSecondTideHighTime3 = tbl7.Rows[2]["SECONDHIGHTIME"].ToString();
                    OffShareSecondTideHighData3 = tbl7.Rows[2]["SECONDHEIGHTLEVEL"].ToString();
                    OffShareSecondTideLowTime3 = tbl7.Rows[2]["SECONDLOWTIME"].ToString();
                    OffShareSecondTideLowData3 = tbl7.Rows[2]["SECONDLOWLEVEL"].ToString();
                }
                //获取外海风、天气
                DataTable tbl4 = (DataTable)sql_Sh.GetTableOnShoreWindAndWave(dt);
                List<object> SH_WH_WindAndWaveAndWeather_List = new ModelConvertHelper<LvChaoWindAndWaveModel>().ConvertToModel(tbl4);
                //获取外海水温
                DataTable tbl15 = (DataTable)sql_Sh.GetOnShoreSW(dt);
                List<object> WHSWList1 = new ModelConvertHelper<OffShareWaterTemperature>().ConvertToModel(tbl15);

                //为了方便管理声明书签数组

                object[] BookMark = NewBookmarks();
                //// List<object> BookMark = new List<object>();


                #region
                doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = PublishVersion;
                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = PublishTime;
                doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = Summarize;
                //给日期书签赋值
                DateBookMarksToData(dt, doc, "shanghe");
                //近、外海天气、风浪、风书签赋值
                for (int i = 0; i < 3; i++)
                {
                    object obBookMark = "OffShareWeather" + (i+1);
                    object obBookMark1 = "OffShareWindForce" + (i + 1);
                    object obBookMark2 = "OffShareWindDirection" + (i + 1);
                    object obBookMark3 = "OffShareWaveHeight" + (i+ 1);
                    object obBookMark4 = "OffShareWaveDirection" + (i + 1);

                    object obBookMark_On = "OnShareWeather" + (i + 1);
                    object obBookMark1_On = "OnShareWindForce" + (i + 1);
                    object obBookMark2_On = "OnShareWindDirection" + (i + 1);
                    object obBookMark3_On = "OnShareWaveHeight" + (i + 1);
                    object obBookMark4_ON = "OnShareWaveDirection" + (i + 1);
                    if (SH_JH_WindAndWaveAndWeather_List.Count > 0)
                    {
                        doc.Bookmarks.get_Item(ref obBookMark).Range.Text = SH_JH_WindAndWaveAndWeather_List[(i * 8) + 3].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark1).Range.Text = SH_JH_WindAndWaveAndWeather_List[(i * 8) + 4].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2).Range.Text = SH_JH_WindAndWaveAndWeather_List[(i * 8) + 5].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark3).Range.Text = SH_JH_WindAndWaveAndWeather_List[(i * 8) + 6].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark4).Range.Text = SH_JH_WindAndWaveAndWeather_List[(i * 8) + 7].ToString();

                    }
                    if (SH_WH_WindAndWaveAndWeather_List.Count > 0)
                    {
                        doc.Bookmarks.get_Item(ref obBookMark_On).Range.Text = SH_WH_WindAndWaveAndWeather_List[(i * 8) + 3].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark1_On).Range.Text = SH_WH_WindAndWaveAndWeather_List[(i * 8) + 4].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2_On).Range.Text = SH_WH_WindAndWaveAndWeather_List[(i * 8) + 5].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark3_On).Range.Text = SH_WH_WindAndWaveAndWeather_List[(i * 8) + 6].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark4_ON).Range.Text = SH_WH_WindAndWaveAndWeather_List[(i * 8) + 7].ToString();
                    }
                    
                }

                //内、外海水温书签赋值
                for (int i = 1; i <= 3; i++)
                {
                    object obBookMark = "OnShareMaxTemp" + i;
                    object obBookMark1 = "OnShareMinTemp" + i;
                    object obBookMark2 = "OnShareAvgTemp" + i;
                    object obBookMark_off = "OffShareMaxTemp" + i;
                    object obBookMark1_off = "OffShareMinTemp" + i;
                    object obBookMark2_off = "OffShareAvgTemp" + i;
                    if (WHSWList1.Count == 0) { }
                    else
                    {
                        doc.Bookmarks.get_Item(ref obBookMark).Range.Text = WHSWList1[3 * i - 3].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark1).Range.Text = WHSWList1[3 * i - 2].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2).Range.Text = WHSWList1[3 * i - 1].ToString();
                    }
                    if (JHSWList1.Count == 0) { }
                    else
                    {
                        doc.Bookmarks.get_Item(ref obBookMark_off).Range.Text = JHSWList1[3 * i - 3].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark1_off).Range.Text = JHSWList1[3 * i - 2].ToString();
                        doc.Bookmarks.get_Item(ref obBookMark2_off).Range.Text = JHSWList1[3 * i - 1].ToString();
                    }
                }
                #endregion
                //潮汐
                doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = OffShareFirstTideHighTime1;
                doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = OffShareFirstTideHighData1;
                doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = OffShareFirstTideLowTime1;
                doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = OffShareFirstTideLowData1;
                doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = OffShareSecondTideHighTime1;
                doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = OffShareSecondTideHighData1;
                doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = OffShareSecondTideLowTime1;
                doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = OffShareSecondTideLowData1;
                doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = OffShareFirstTideHighTime2;
                doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = OffShareFirstTideHighData2;
                doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = OffShareFirstTideLowTime2;
                doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = OffShareFirstTideLowData2;
                doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = OffShareSecondTideHighTime2;
                doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = OffShareSecondTideHighData2;
                doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = OffShareSecondTideLowTime2;
                doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = OffShareSecondTideLowData2;
                doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = OffShareFirstTideHighTime3;
                doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = OffShareFirstTideHighData3;
                doc.Bookmarks.get_Item(ref BookMark[22]).Range.Text = OffShareFirstTideLowTime3;
                doc.Bookmarks.get_Item(ref BookMark[23]).Range.Text = OffShareFirstTideLowData3;
                doc.Bookmarks.get_Item(ref BookMark[24]).Range.Text = OffShareSecondTideHighTime3;
                doc.Bookmarks.get_Item(ref BookMark[25]).Range.Text = OffShareSecondTideHighData3;
                doc.Bookmarks.get_Item(ref BookMark[26]).Range.Text = OffShareSecondTideLowTime3;
                doc.Bookmarks.get_Item(ref BookMark[27]).Range.Text = OffShareSecondTideLowData3;
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
                WriteLog.Write("生成黄海绿潮专项预报单失败" + ex.ToString());
                return 0;
            }

        }
        /// <summary>
        /// 近海水温赋值
        /// </summary>
        /// <param name="JHSWList1"></param>
        /// <param name="doc"></param>
        private void OffShoreWaterTemp(List<object> JHSWList1, Word.Document doc)
        {
            for (int i = 1; i <= 3; i++)
            {
                object obBookMark_off = "OffShareMaxTemp" + i;
                object obBookMark1_off = "OffShareMinTemp" + i;
                object obBookMark2_off = "OffShareAvgTemp" + i;

                if (JHSWList1.Count == 0) { }
                else
                {
                    doc.Bookmarks.get_Item(ref obBookMark_off).Range.Text = JHSWList1[3 * i - 3].ToString();
                    doc.Bookmarks.get_Item(ref obBookMark1_off).Range.Text = JHSWList1[3 * i - 2].ToString();
                    doc.Bookmarks.get_Item(ref obBookMark2_off).Range.Text = JHSWList1[3 * i - 1].ToString();
                }
            }
        }
        /// <summary>
        /// 预报日期书签赋值
        /// </summary>
        /// <param name="doc"></param>
        private void DateBookMarksToData(DateTime PublishTime_Day,Word.Document doc,string type)
        {
            int dateBookMarks;
            if (type == "pingtai")
            {
                dateBookMarks = 9;
            }
            else
            { dateBookMarks = 15; }
            for (int i = 1; i <= dateBookMarks; i++)
            {
                object obBookMark = "ForecastDate" + i;
                if (i % 3 == 1)
                {
                    doc.Bookmarks.get_Item(ref obBookMark).Range.Text = PublishTime_Day.AddDays(1).Day + "日";
                }
                if (i % 3 == 2)
                {
                    doc.Bookmarks.get_Item(ref obBookMark).Range.Text = PublishTime_Day.AddDays(2).Day + "日";
                }
                if (i % 3 == 0)
                {
                    doc.Bookmarks.get_Item(ref obBookMark).Range.Text = PublishTime_Day.AddDays(3).Day + "日";
                }
            }
        }
        /// <summary>
        /// 声明书签数组
        /// </summary>
        /// <returns></returns>
        private object[] NewBookmarks()
        {
            //为了方便管理声明书签数组

            object[] BookMark = new object[48];
            // List<object> BookMark = new List<object>();

            //赋值书签名
            BookMark[0] = "PublishVersion";////期数
            BookMark[1] = "PublishTime";////发布时间
            BookMark[2] = "SignAndIssue";////签发
            BookMark[3] = "Summarize";////综述

            BookMark[4] = "OffShareFirstTideHighTime1";
            BookMark[5] = "OffShareFirstTideHighData1";
            BookMark[6] = "OffShareFirstTideLowTime1";
            BookMark[7] = "OffShareFirstTideLowData1";
            BookMark[8] = "OffShareSecondTideHighTime1";
            BookMark[9] = "OffShareSecondTideHighData1";
            BookMark[10] = "OffShareSecondTideLowTime1";
            BookMark[11] = "OffShareSecondTideLowData1";

            BookMark[12] = "OffShareFirstTideHighTime2";
            BookMark[13] = "OffShareFirstTideHighData2";
            BookMark[14] = "OffShareFirstTideLowTime2";
            BookMark[15] = "OffShareFirstTideLowData2";
            BookMark[16] = "OffShareSecondTideHighTime2";
            BookMark[17] = "OffShareSecondTideHighData2";
            BookMark[18] = "OffShareSecondTideLowTime2";
            BookMark[19] = "OffShareSecondTideLowData2";

            BookMark[20] = "OffShareFirstTideHighTime3";
            BookMark[21] = "OffShareFirstTideHighData3";
            BookMark[22] = "OffShareFirstTideLowTime3";
            BookMark[23] = "OffShareFirstTideLowData3";
            BookMark[24] = "OffShareSecondTideHighTime3";
            BookMark[25] = "OffShareSecondTideHighData3";
            BookMark[26] = "OffShareSecondTideLowTime3";
            BookMark[27] = "OffShareSecondTideLowData3";
            return BookMark;
        }

        /// <summary>
        /// 把Word文件转换成pdf文件
        /// </summary>
        /// <param name="sourcePath">需要转换的文件路径和文件名称</param>
        /// <param name="targetPath">转换完成后的文件的路径和文件名名称</param>
        /// <returns></returns>
        //public  bool WordToPdf(object sourcePath, string targetPath)
        //{
        //    bool result = false;
        //    Word.WdExportFormat WdFormat = Word.WdExportFormat.wdExportFormatPDF;
        //    object missing = Type.Missing;
        //    Microsoft.Office.Interop.Word.ApplicationClass applicationClass = null;
        //    Word.Document document = null;
        //    try
        //    {
        //        applicationClass = new Word.ApplicationClass();
        //        object inputfileName = sourcePath;//需要转格式的文件路径
        //        string outputFileName = targetPath;//转换完成后PDF或XPS文件的路径和文件名名称
        //        Word.WdExportFormat exportFormat = WdFormat;//导出文件所使用的格式
        //        bool openAfterExport = false;//转换完成后是否打开
        //        Word.WdExportOptimizeFor wdExportOptimizeForOnScreen = Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen;
        //        //导出方式
        //        Word.WdExportRange wdExportAllDocument = Word.WdExportRange.wdExportAllDocument;//导出全部内容（枚举）
        //        int from = 0;//起始页码
        //        int to = 0;//结束页码
        //        Word.WdExportItem wdExportDocumentContent = Word.WdExportItem.wdExportDocumentContent;//指定导出过程中是否只包含文本或包含文本的标记.
        //        bool includeDocProps = true;//指定是否包含新导出的文件在文档属性
        //        bool keepIRM = true;//
        //        Word.WdExportCreateBookmarks wdExportCreateWordBookmarks = Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
        //        bool docStructureTags = true;
        //        bool bitmapMissingFonts = true;
        //        bool UseISO19005_1 = false;//生成的文档是否符合 ISO 19005-1 (PDF/A)
        //        document = applicationClass.Documents.Open(ref inputfileName, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        //        if (document != null)
        //        {
        //            document.ExportAsFixedFormat(outputFileName, exportFormat, openAfterExport, wdExportOptimizeForOnScreen, wdExportAllDocument, from, to, wdExportDocumentContent, includeDocProps, keepIRM, wdExportCreateWordBookmarks, docStructureTags, bitmapMissingFonts, UseISO19005_1, ref missing);
        //        }
        //        result = true;
        //    }
        //    catch(Exception ex)
        //    {
        //        WriteLog.Write("生成上合峰会平台预报单失败" + ex.ToString());
        //        result = false;
        //    }
        //    finally
        //    {
        //        if (document != null)
        //        {
        //            document.Close(ref missing, ref missing, ref missing);
        //            document = null;
        //        }
        //        if (applicationClass != null)
        //        {
        //            applicationClass.Quit(ref missing, ref missing, ref missing);
        //            applicationClass = null;
        //        }
        //    }
        //    return result;
        //}
       

    }
    




    /// <summary>
    /// 定义泛型类生成不同书签值的list
    /// </summary>
    /// <typeparam name="T">不同实体类</typeparam>
    public class ModelConvertHelper<T> where T : new()
    {
        /// <summary>
        /// 生成书签值的list
        /// </summary>
        /// <param name="dt">不同表数据</param>
        /// <returns></returns>
        public  List<object> ConvertToModel(System.Data.DataTable dt)
        {
            List<object> ts = new List<object>();
            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();   //new 一个新的实体类
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                       // if (value != DBNull.Value)
                       // pi.SetValue(t, value, null);
                        ts.Add(value);
                    }
                }
               
            }
            return ts;
        }
        

    }
    }   