using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace PredicTable.ExportWord
{
    public class SixWord
    {
        string PUBLISHTIME = "";
        //日 期
        string[] FORECASTDATEArr = new string[14];

        /// 第一次高潮时间
        string[] HTLFIRSTWAVEOFTIMEArr = new string[14];

        /// 第一次高潮潮位
        string[] HTLFIRSTWAVETIDELEVELArr = new string[14];

        /// 第一次低潮时间
        string[] HTLFIRSTTIMELOWTIDEArr = new string[14];

        /// 第一次低潮潮位
        string[] HTLLOWTIDELEVELFORTHEFIRSTTIMEArr = new string[14];

        /// 第二次高潮时间
        string[] HTLSECONDWAVEOFTIMEArr = new string[14];

        /// 第二次高潮潮位
        string[] HTLSECONDWAVETIDELEVELArr = new string[14];

        /// 第二次低潮时间
        string[] HTLSECONDTIMELOWTIDEArr = new string[14];

        /// 第二次低潮潮位
        string[] HTLLOWTIDELEVELFORTHESECONDTIMArr = new string[14];

        public SixWord()
        {
        }
        public static DateTime GetMondayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。   
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }
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
                    string FTIDALFORECASTER = tblfooter.Rows[i]["FTIDALFORECASTER"].ToString();

                    string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                    string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();

                    string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                    string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                    string FTIDALFORECASTERTEL = tblfooter.Rows[i]["FTIDALFORECASTERTEL"].ToString();//潮汐电话

                    //PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
                    PUBLISHTIME = PUBLISHDATE + "10时";

                    tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                    //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                    //tblfooter_Model.FFAX = FFAX;
                    tblfooter_Model.FTIDALFORECASTER = FTIDALFORECASTER;

                    tblfooter_Model.ZHIBANTEL = ZHIBANTEL;
                    tblfooter_Model.SENDTEL = SENDTEL;
                    tblfooter_Model.FTIDALFORECASTERTEL = FTIDALFORECASTERTEL;

                }
                //7day 港口潮位
                TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL_Model = new TBLHARBOURTIDELEVEL();
                //var date = DateTime.Now;
                //DateTime weekPublishTime = date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")));
                //model_01.PUBLISHDATE = weekPublishTime;
                DateTime weekPublishTime = GetMondayDate(dt);
                TBLHARBOURTIDELEVEL_Model.PUBLISHDATE = weekPublishTime;
                sql_TBLHARBOURTIDELEVEL7DAY TBLHARBOURTIDELEVEL7DAY = new sql_TBLHARBOURTIDELEVEL7DAY();
                System.Data.DataTable TableForTBLHARBOURTIDELEVEL =  (DataTable)TBLHARBOURTIDELEVEL7DAY.GETTBLHARBOURTIDELEVEL7DAYWORD(TBLHARBOURTIDELEVEL_Model);
                if (TableForTBLHARBOURTIDELEVEL.Rows.Count == 0) { }
                else
                {
                    int m = 0;
                    int n = 7;
                    for (int i = 0; i < TableForTBLHARBOURTIDELEVEL.Rows.Count; i++)
                    {
                        var row = TableForTBLHARBOURTIDELEVEL.Rows[i];
                        var HTLHARBOUR = row["HTLHARBOUR"].ToString();

                        if (HTLHARBOUR == "龙口港")
                        {
                            var FORECASTDATE = row["FORECASTDATE"].ToString();
                            if (FORECASTDATE != null || FORECASTDATE != "")
                            {
                                string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                                FORECASTDATE = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                            }
                            FORECASTDATEArr[m] = FORECASTDATE;
                            /// 第一次高潮时间
                            HTLFIRSTWAVEOFTIMEArr[m] = row["HTLFIRSTWAVEOFTIME"].ToString();

                            /// 第一次高潮潮位
                            HTLFIRSTWAVETIDELEVELArr[m] = row["HTLFIRSTWAVETIDELEVEL"].ToString();

                            /// 第一次低潮时间
                            HTLFIRSTTIMELOWTIDEArr[m] = row["HTLFIRSTTIMELOWTIDE"].ToString();

                            /// 第一次低潮潮位
                            HTLLOWTIDELEVELFORTHEFIRSTTIMEArr[m] = row["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();

                            /// 第二次高潮时间
                            HTLSECONDWAVEOFTIMEArr[m] = row["HTLSECONDWAVEOFTIME"].ToString();

                            /// 第二次高潮潮位
                            HTLSECONDWAVETIDELEVELArr[m] = row["HTLSECONDWAVETIDELEVEL"].ToString();

                            /// 第二次低潮时间
                            HTLSECONDTIMELOWTIDEArr[m] = row["HTLSECONDTIMELOWTIDE"].ToString();

                            /// 第二次低潮潮位
                            HTLLOWTIDELEVELFORTHESECONDTIMArr[m] = row["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();


                            //YRBHWWFFLOWDIRArr[m] = row["YRBHWWFFLOWDIR"].ToString();
                            //YRBHWWFFLOWLEVELRArr[m] = row["YRBHWWFFLOWLEVEL"].ToString();
                            //YRBHWWFWAVEHEIGHTArr[m] = row["YRBHWWFWAVEHEIGHT"].ToString();
                            //YRBHWWFWAVEDIRArr[m] = row["YRBHWWFWAVEDIR"].ToString();
                            m++;
                        }
                        if (HTLHARBOUR == "黄河海港")
                        {
                            var FORECASTDATE = row["FORECASTDATE"].ToString();
                            if (FORECASTDATE != null || FORECASTDATE != "")
                            {
                                string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                                FORECASTDATE = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                            }
                            FORECASTDATEArr[n] = FORECASTDATE;

                            /// 第一次高潮时间
                            HTLFIRSTWAVEOFTIMEArr[n] = row["HTLFIRSTWAVEOFTIME"].ToString();

                            /// 第一次高潮潮位
                            HTLFIRSTWAVETIDELEVELArr[n] = row["HTLFIRSTWAVETIDELEVEL"].ToString();

                            /// 第一次低潮时间
                            HTLFIRSTTIMELOWTIDEArr[n] = row["HTLFIRSTTIMELOWTIDE"].ToString();

                            /// 第一次低潮潮位
                            HTLLOWTIDELEVELFORTHEFIRSTTIMEArr[n] = row["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString();

                            /// 第二次高潮时间
                            HTLSECONDWAVEOFTIMEArr[n] = row["HTLSECONDWAVEOFTIME"].ToString();

                            /// 第二次高潮潮位
                            HTLSECONDWAVETIDELEVELArr[n] = row["HTLSECONDWAVETIDELEVEL"].ToString();

                            /// 第二次低潮时间
                            HTLSECONDTIMELOWTIDEArr[n] = row["HTLSECONDTIMELOWTIDE"].ToString();

                            /// 第二次低潮潮位
                            HTLLOWTIDELEVELFORTHESECONDTIMArr[n] = row["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString();
                            n++;
                        }

                    }
                }


                var label = "";
                var markValues = new string[14];
                object mark;
                for (int i = 0; i < 9; i++)
                {
                    switch (i)
                    {
                        case 0: label = "FORECASTDATE"; markValues = FORECASTDATEArr; break;
                        case 1: label = "HTLFIRSTWAVEOFTIME"; markValues = HTLFIRSTWAVEOFTIMEArr; break;
                        case 2: label = "HTLFIRSTWAVETIDELEVEL"; markValues = HTLFIRSTWAVETIDELEVELArr; break;
                        case 3: label = "HTLFIRSTTIMELOWTIDE"; markValues = HTLFIRSTTIMELOWTIDEArr; break;
                        case 4: label = "HTLLOWTIDELEVELFORTHEFIRSTTIME"; markValues = HTLLOWTIDELEVELFORTHEFIRSTTIMEArr; break;
                        case 5: label = "HTLSECONDWAVEOFTIME"; markValues = HTLSECONDWAVEOFTIMEArr; break;
                        case 6: label = "HTLSECONDWAVETIDELEVEL"; markValues = HTLSECONDWAVETIDELEVELArr; break;
                        case 7: label = "HTLSECONDTIMELOWTIDE"; markValues = HTLSECONDTIMELOWTIDEArr; break;
                        case 8: label = "HTLLOWTIDELEVELFORTHESECONDTIM"; markValues = HTLLOWTIDELEVELFORTHESECONDTIMArr; break;
                        default: break;

                    }

                    for (int j = 0; j < 14; j++)
                    {

                        mark = label + (j + 1);
                        doc.Bookmarks.get_Item(ref mark).Range.Text = markValues[j];
                    }

                }


                mark = "FRELEASEUNIT";//发布单位
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.FRELEASEUNIT;
                //mark = "FTELEPHONE";//电话
                //doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.FTELEPHONE;
                //mark = "FFAX";//传真
                //doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.FFAX;
                mark = "FTIDALFORECASTER";//潮汐预报员
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.FTIDALFORECASTER;
                mark = "PUBLISHTIME";//
                doc.Bookmarks.get_Item(ref mark).Range.Text = PUBLISHTIME;

                //新添加书签
                mark = "FZHIBANTEL";//预报值班
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.ZHIBANTEL;
                mark = "FSENDTEL";//预报发送
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.SENDTEL;
                mark = "FTIDALFORECASTERTEL";//潮汐预报员
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.FTIDALFORECASTERTEL;


                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                return 1;
            }
            catch (Exception ex)
            {
                //throw ex;

                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                WriteLog.Write(ex.ToString());
                return 0;
            }
        }

    }
}