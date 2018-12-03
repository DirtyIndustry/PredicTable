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
    public class FiveWord
    {

        string PUBLISHTIME = "";

        //日 期
        string[] FORECASTDATEArr = new string[14];

        //风向（方位）
        string[] YRBHWWFFLOWDIRArr = new string[14];

        //风速（级）
        string[] YRBHWWFFLOWLEVELRArr = new string[14];

        //波高（m）
        string[] YRBHWWFWAVEHEIGHTArr = new string[14];

        //波向（方位）
        string[] YRBHWWFWAVEDIRArr = new string[14];


        public FiveWord()
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
                    string FWAVEFORECASTER = tblfooter.Rows[i]["FWAVEFORECASTER"].ToString();

                    string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                    string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();

                    string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                    string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                    string FWAVEFORECASTERTEL = tblfooter.Rows[i]["FWAVEFORECASTERTEL"].ToString();//海浪预报员电话


                    // PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
                    PUBLISHTIME = PUBLISHDATE + "10时";

                    tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                    //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                    //tblfooter_Model.FFAX = FFAX;
                    tblfooter_Model.FWAVEFORECASTER = FWAVEFORECASTER;

                    tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                    tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                    tblfooter_Model.FWAVEFORECASTERTEL = FWAVEFORECASTERTEL;//海浪预报员电话
                }
                object mark_PUBLISHTIME = "PUBLISHTIME";
                doc.Bookmarks.get_Item(ref mark_PUBLISHTIME).Range.Text = PUBLISHTIME;
                //7day渤海海区及黄河海港风、浪预报
                TBLYRBHWINDWAVE72HFORECASTTWO tblyrbhwindwave7dforecasttwo_Model = new TBLYRBHWINDWAVE72HFORECASTTWO();
                DateTime weekPublishTime = GetMondayDate(dt);
                //model.PUBLISHDATE = date;
                tblyrbhwindwave7dforecasttwo_Model.PUBLISHDATE = weekPublishTime;
                System.Data.DataTable tblyrbhwindwave7dforecasttwo = (System.Data.DataTable)new sql_TBLYRBHWINDWAVE7DAYFORECASTTWO().GETTBLYRBHWINDWAVE7DAYFORECASTTWO(tblyrbhwindwave7dforecasttwo_Model);
                if (tblyrbhwindwave7dforecasttwo.Rows.Count == 0) { }
                else
                {
                    int m = 0;
                    int n = 7;
                    for (int i = 0; i < tblyrbhwindwave7dforecasttwo.Rows.Count; i++)
                    {
                        var row = tblyrbhwindwave7dforecasttwo.Rows[i];
                        var REPORTAREA = row["REPORTAREA"].ToString();

                        if (REPORTAREA == "渤海")
                        {
                            var FORECASTDATE = row["FORECASTDATE"].ToString();
                            if (FORECASTDATE != null || FORECASTDATE != "")
                            {
                                string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                                FORECASTDATE = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                            }
                            FORECASTDATEArr[m] = FORECASTDATE;

                            YRBHWWFFLOWDIRArr[m]= row["YRBHWWFFLOWDIR"].ToString();
                            YRBHWWFFLOWLEVELRArr[m] = row["YRBHWWFFLOWLEVEL"].ToString();
                            YRBHWWFWAVEHEIGHTArr[m]= row["YRBHWWFWAVEHEIGHT"].ToString();
                            YRBHWWFWAVEDIRArr[m] = row["YRBHWWFWAVEDIR"].ToString();
                            m++;
                        }
                        if (REPORTAREA == "黄河海港")
                        {
                            var FORECASTDATE = row["FORECASTDATE"].ToString();
                            if (FORECASTDATE != null || FORECASTDATE != "")
                            {
                                string[] FORECASTDATEArr = FORECASTDATE.Split(' ');
                                FORECASTDATE = FORECASTDATEArr[0].Substring(5).Replace("/", "月") + "日";
                            }
                            FORECASTDATEArr[n] = FORECASTDATE;

                            YRBHWWFFLOWDIRArr[n] = row["YRBHWWFFLOWDIR"].ToString();
                            YRBHWWFFLOWLEVELRArr[n] = row["YRBHWWFFLOWLEVEL"].ToString();
                            YRBHWWFWAVEHEIGHTArr[n] = row["YRBHWWFWAVEHEIGHT"].ToString();
                            YRBHWWFWAVEDIRArr[n] = row["YRBHWWFWAVEDIR"].ToString();
                            n++;
                        }

                    }
                }
                //7天海洋水纹预报综述
                sql_TBLZS hyzs_7Days = new Dal.sql_TBLZS();
                DataTable dt_zs7Days = (DataTable)hyzs_7Days.get_TBLSWQX_ZS_3DayS_OR_24HourS(dt);
                if(dt_zs7Days != null && dt_zs7Days.Rows.Count > 0)
                {
                    object mark_7Days= "HYQXYBZS7DAYS";
                    doc.Bookmarks.get_Item(ref mark_7Days).Range.Text = dt_zs7Days.Rows[0]["METEOROLOGICALREVIEW7DAYS"].ToString()+ dt_zs7Days.Rows[0]["METEOROLOGICALREVIEW7DAYSCX"].ToString() ;
                }


                var label = "";
                var markValues = new string[14];
                object mark;
                for (int i = 0; i < 5; i++)
                {
                    switch (i)
                    {
                        case 0: label = "FORECASTDATE"; markValues = FORECASTDATEArr; break;
                        case 1: label = "YRBHWWFFLOWDIR"; markValues = YRBHWWFFLOWDIRArr; break;
                        case 2: label = "YRBHWWFFLOWLEVEL"; markValues = YRBHWWFFLOWLEVELRArr; break;
                        case 3: label = "YRBHWWFWAVEHEIGHT"; markValues = YRBHWWFWAVEHEIGHTArr; break;
                        case 4: label = "YRBHWWFWAVEDIR"; markValues = YRBHWWFWAVEDIRArr; break;
                        default: break;

                    }

                    for (int j=0;j<14;j++)
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
                mark = "FWAVEFORECASTER";//海浪预报员
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.FWAVEFORECASTER;
                //mark = "PUBLISHTIME";//发布时间
                //doc.Bookmarks.get_Item(ref mark).Range.Text = PUBLISHTIME;

                //新添加书签
                mark = "FZHIBANTEL";//预报值班
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.ZHIBANTEL;
                mark = "FSENDTEL";//预报发送
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.SENDTEL;
                mark = "FWAVEFORECASTERTEL";//海浪预报员
                doc.Bookmarks.get_Item(ref mark).Range.Text = tblfooter_Model.FWAVEFORECASTERTEL;

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