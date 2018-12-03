using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;
/// <summary>
/// SevenWord 的摘要说明
/// </summary>
public class SevenWord
{
    public SevenWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    string PUBLISHTIME = "";

    string SDOSCWSURFACETEMPERATURE1 = "";
    string SDOSCWSURFACETEMPERATURE2 = "";
    string SDOSCWSURFACETEMPERATURE3 = "";
    string SDOSCWSURFACETEMPERATURE4 = "";
    string SDOSCWSURFACETEMPERATURE5 = "";

    /// 最大结冰范围
    string MAXICEAREA1 = "";
    string MAXICEAREA2 = "";
    string MAXICEAREA3 = "";
    string MAXICEAREA4 = "";

    ///// 一般冰厚
    string COMMONTHICKNESS1 = "";
    string COMMONTHICKNESS2 = "";
    string COMMONTHICKNESS3 = "";
    string COMMONTHICKNESS4 = "";

    ///// 最大冰厚
    string MAXTHICKNESS1 = "";
    string MAXTHICKNESS2 = "";
    string MAXTHICKNESS3 = "";
    string MAXTHICKNESS4 = "";

    /// <summary>
    /// 判断周一日期
    /// </summary>
    /// <param name="someDate"></param>
    /// <returns></returns>
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
                string FWATERTEMPERATUREFORECASTER = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTER"].ToString();

                string ZHIBANTEL = tblfooter.Rows[i]["ZHIBANTEL"].ToString();//预报值班
                string SENDTEL = tblfooter.Rows[i]["SENDTEL"].ToString();//预报发送
                string FWATERTEMPERATUREFORECASTERTEL = tblfooter.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"].ToString();//水温预报员电话

                string PUBLISHDATE = DateTime.Parse(tblfooter.Rows[i]["PUBLISHDATE"].ToString()).ToLongDateString();
                string PUBLISHHOUR = tblfooter.Rows[i]["PUBLISHHOUR"].ToString();
                //PUBLISHTIME = PUBLISHDATE + PUBLISHHOUR + "时";
                PUBLISHTIME = PUBLISHDATE + "10时";

                tblfooter_Model.FRELEASEUNIT = FRELEASEUNIT;
                //tblfooter_Model.FTELEPHONE = FTELEPHONE;
                //tblfooter_Model.FFAX = FFAX;
                tblfooter_Model.FWATERTEMPERATUREFORECASTER = FWATERTEMPERATUREFORECASTER;

                tblfooter_Model.ZHIBANTEL = ZHIBANTEL;//预报值班
                tblfooter_Model.SENDTEL = SENDTEL;//预报发送
                tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL = FWATERTEMPERATUREFORECASTERTEL;//水温预报员电话
            }
            object mark_PUBLISHTIME = "PUBLISHTIME";
            doc.Bookmarks.get_Item(ref mark_PUBLISHTIME).Range.Text = PUBLISHTIME;
            //黄河南海堤表提取数据
            TBLSDOFFSHORESEVENCITY24HWAVE tblsdoffshoresevencity24hwave_Model = new TBLSDOFFSHORESEVENCITY24HWAVE();
            DateTime weekPublishTime = GetMondayDate(dt);

            tblsdoffshoresevencity24hwave_Model.PUBLISHDATE = weekPublishTime;
            //System.Data.DataTable tblsdoffshoresevencity24hwave = (System.Data.DataTable)new sql_TBLSDOFFSHORESEVENCITY24HWAVE().get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(tblsdoffshoresevencity24hwave_Model);
            System.Data.DataTable tblsdoffshoresevencity24hwave = (System.Data.DataTable)new sql_TBLSLYTWEEKFORECAST().GETTBLSLYTWEEKFORECAST(tblsdoffshoresevencity24hwave_Model);
            if (tblsdoffshoresevencity24hwave.Rows.Count == 0) { }
            else
            {
                for (int i = 0; i < tblsdoffshoresevencity24hwave.Rows.Count; i++)
                {
                    if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "威海近海")
                    {
                        SDOSCWSURFACETEMPERATURE1 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "烟台近海")
                    {
                        SDOSCWSURFACETEMPERATURE2 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "潍坊近海")
                    {
                        SDOSCWSURFACETEMPERATURE3 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "东营近海")
                    {
                        SDOSCWSURFACETEMPERATURE4 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                    else if (tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWAREA"].ToString() == "滨州近海")
                    {
                        SDOSCWSURFACETEMPERATURE5 = tblsdoffshoresevencity24hwave.Rows[i]["SDOSCWSURFACETEMPERATURE"].ToString();
                    }
                }


            }


            //黄河南海堤表提取数据
            TBLSEAAREASEAICEFORECAST TBLSEAAREASEAICEFORECAST_Model = new TBLSEAAREASEAICEFORECAST();
            TBLSEAAREASEAICEFORECAST_Model.PUBLISHDATE = dt;
            System.Data.DataTable TbTBLSEAAREASEAICEFORECAST = (System.Data.DataTable)new sql_TBLSEAAREASEAICEFORECAST().get_TBLSEAAREASEAICEFORECAST_AllData(TBLSEAAREASEAICEFORECAST_Model);
            if (TbTBLSEAAREASEAICEFORECAST.Rows.Count == 0) { }
            else
            {

                for (int i = 0; i < TbTBLSEAAREASEAICEFORECAST.Rows.Count; i++)
                {
                    if (TbTBLSEAAREASEAICEFORECAST.Rows[i]["SEAAREA"].ToString() == "辽东湾")
                    {
                        MAXICEAREA1 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["MAXICEAREA"].ToString();

                        COMMONTHICKNESS1 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["COMMONTHICKNESS"].ToString();

                        MAXTHICKNESS1 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["MAXTHICKNESS"].ToString();

                    }
                    else if (TbTBLSEAAREASEAICEFORECAST.Rows[i]["SEAAREA"].ToString() == "渤海湾")
                    {
                        MAXICEAREA2 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["MAXICEAREA"].ToString();

                        COMMONTHICKNESS2 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["COMMONTHICKNESS"].ToString();

                        MAXTHICKNESS2 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["MAXTHICKNESS"].ToString();
                    }
                    else if (TbTBLSEAAREASEAICEFORECAST.Rows[i]["SEAAREA"].ToString() == "莱州湾")
                    {
                        MAXICEAREA3 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["MAXICEAREA"].ToString();

                        COMMONTHICKNESS3 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["COMMONTHICKNESS"].ToString();

                        MAXTHICKNESS3 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["MAXTHICKNESS"].ToString();
                    }
                    else if (TbTBLSEAAREASEAICEFORECAST.Rows[i]["SEAAREA"].ToString() == "黄海北部")
                    {
                        MAXICEAREA4 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["MAXICEAREA"].ToString();

                        COMMONTHICKNESS4 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["COMMONTHICKNESS"].ToString();

                        MAXTHICKNESS4 = TbTBLSEAAREASEAICEFORECAST.Rows[i]["MAXTHICKNESS"].ToString();
                    }
                }


            }

            //为了方便管理声明书签数组
            object[] BookMark = new object[23];//新增1个潮汐预报员电话，22改为23
            //赋值书签名
            BookMark[0] = "FRELEASEUNIT";//发布单位
            BookMark[1] = "FZHIBANTEL";//预报值班
            BookMark[2] = "FSENDTEL";//预报发送

            //BookMark[1] = "FTELEPHONE";//电话
            //BookMark[2] = "FFAX";//传真
            BookMark[3] = "FWATERTEMPERATUREFORECASTER";//水温预报员
            BookMark[4] = "SDOSCWSURFACETEMPERATURE1"; //潮汐预报员
            BookMark[5] = "SDOSCWSURFACETEMPERATURE2";//表层水温1
            BookMark[6] = "SDOSCWSURFACETEMPERATURE3";//表层水温2
            BookMark[7] = "SDOSCWSURFACETEMPERATURE4";//表层水温3
            BookMark[8] = "SDOSCWSURFACETEMPERATURE5";//表层水温4

            //string MAXICEAREA1 = "";
            //string MAXICEAREA2 = "";
            //string MAXICEAREA3 = "";
            //string MAXICEAREA4 = "";

            /////// 一般冰厚
            //string COMMONTHICKNESS1 = "";
            //string COMMONTHICKNESS2 = "";
            //string COMMONTHICKNESS3 = "";
            //string COMMONTHICKNESS4 = "";


            ///// 最大结冰范围
            BookMark[9] = "MAXICEAREA1";
            BookMark[10] = "MAXICEAREA2";
            BookMark[11] = "MAXICEAREA3";
            BookMark[12] = "MAXICEAREA4";

            /////// 一般冰厚
            BookMark[13] = "COMMONTHICKNESS1";
            BookMark[14] = "COMMONTHICKNESS2";
            BookMark[15] = "COMMONTHICKNESS3";
            BookMark[16] = "COMMONTHICKNESS4";

            /////// 最大冰厚
            BookMark[17] = "MAXTHICKNESS1";
            BookMark[18] = "MAXTHICKNESS2";
            BookMark[19] = "MAXTHICKNESS3";
            BookMark[20] = "MAXTHICKNESS4";
            //BookMark[21] = "PUBLISHTIME";

            BookMark[21] = "FWATERTEMPERATUREFORECASTERTEL";//水温预报员电话



            //赋值数据到书签的位置
            doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = tblfooter_Model.FRELEASEUNIT;

            doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.ZHIBANTEL;//预报值班
            doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.SENDTEL;//预报发送

            //doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = tblfooter_Model.FTELEPHONE;
            //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = tblfooter_Model.FFAX;
            doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTER;
            doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = SDOSCWSURFACETEMPERATURE1;

            doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = SDOSCWSURFACETEMPERATURE2;
            doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = SDOSCWSURFACETEMPERATURE3;
            doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = SDOSCWSURFACETEMPERATURE4;
            doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = SDOSCWSURFACETEMPERATURE5;


            doc.Bookmarks.get_Item(ref BookMark[9]).Range.Text = MAXICEAREA1;
            doc.Bookmarks.get_Item(ref BookMark[10]).Range.Text = MAXICEAREA2;
            doc.Bookmarks.get_Item(ref BookMark[11]).Range.Text = MAXICEAREA3;
            doc.Bookmarks.get_Item(ref BookMark[12]).Range.Text = MAXICEAREA4;

            doc.Bookmarks.get_Item(ref BookMark[13]).Range.Text = COMMONTHICKNESS1;
            doc.Bookmarks.get_Item(ref BookMark[14]).Range.Text = COMMONTHICKNESS2;
            doc.Bookmarks.get_Item(ref BookMark[15]).Range.Text = COMMONTHICKNESS3;
            doc.Bookmarks.get_Item(ref BookMark[16]).Range.Text = COMMONTHICKNESS4;

            doc.Bookmarks.get_Item(ref BookMark[17]).Range.Text = MAXTHICKNESS1;
            doc.Bookmarks.get_Item(ref BookMark[18]).Range.Text = MAXTHICKNESS2;
            doc.Bookmarks.get_Item(ref BookMark[19]).Range.Text = MAXTHICKNESS3;
            doc.Bookmarks.get_Item(ref BookMark[20]).Range.Text = MAXTHICKNESS4;
            //doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = PUBLISHTIME;
            doc.Bookmarks.get_Item(ref BookMark[21]).Range.Text = tblfooter_Model.FWATERTEMPERATUREFORECASTERTEL;

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