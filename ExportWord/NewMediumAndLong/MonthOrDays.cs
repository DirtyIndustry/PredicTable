using PredicTable.Dal;
using PredicTable.Model.NewMediumAndLong;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace PredicTable.ExportWord.NewMediumAndLong
{

    /// <summary>
    /// 中长期月、旬预报单处理
    /// </summary>
    public class MonthOrDays
    {
        /// <summary>
        /// 复制Word模板
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="fileName"></param>
        /// <param name="year"></param>
        /// <returns></returns>

        public int CopyWord(string templateFile, string fileName, ReportMonthModel month,string tablepathlist)
        {
            //生成word程序对象
            Word.Application app = new Word.Application();

            //模板文件
            string TemplateFile = templateFile;
            //生成的具有模板样式的新文件
            string FileName = fileName + "/" + month.DOCNAME;
            if (Directory.Exists(fileName) == false)
            {
                Directory.CreateDirectory(fileName);
            }
            //删除word文件
            System.IO.File.Delete(FileName);
            //模板文件拷贝到新文件
            File.Copy(TemplateFile, FileName);
            int flag = 0;
            if (month.PUBLISHCOMPANY == "NCS" || month.PUBLISHCOMPANY == "SD")
            {
                flag = SaveENMark(app, FileName, month,tablepathlist);
            }
            else if (month.PUBLISHCOMPANY == "南堡油田" || month.PUBLISHCOMPANY == "胜利油田" || month.PUBLISHCOMPANY == "东营环境预报")
            {
                flag = SaveCNMark(app, FileName, month, tablepathlist);
            }

            return flag;
        }

        /// <summary>
        /// 保存北海、山东旬、月数据到标签
        /// </summary>
        /// <param name="app"></param>
        /// <param name="FileName"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private int SaveENMark(Word.Application app, string FileName, ReportMonthModel month,string TablePathList)
        {
            //表格复制部分的代码
            Word.Document tabledoc = null;
            if(TablePathList != "")
            {
                //复制表格部分
                tabledoc = new Word.Document();
                object Nothing = System.Reflection.Missing.Value;
                object IsReadOnly = false;
                object filename = TablePathList;
                tabledoc = app.Documents.Open(ref filename, ref Nothing, ref IsReadOnly, ref IsReadOnly,
                    ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                    ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                    ref Nothing, ref Nothing, ref Nothing,
                    ref Nothing);

                //选中word中的第一个表格
                tabledoc.Tables[1].Select();
                //将表格复制到剪切板中
                app.Selection.Copy();
            } 


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

            Word.Paragraph _wordPara = doc.Content.Paragraphs.Add(ref missing);
            Word.Range _wordRange = doc.Paragraphs[1].Range;

            doc.Activate();
            //修改书签
            //修改书签
            try
            {
                //保存发布时间到标签时，改为年月日格式
                string date = this.FormatDate(month.PUBLISHTIME);
                object[] BookMarks = new object[8];
                BookMarks[0] = "REPORTNO"; //预报单编号
                BookMarks[1] = "PUBLISHTIME"; //发布时间
                BookMarks[2] = "REPORTTITLE";  //预报名称
                BookMarks[4] = "REPORTCONTENT"; //预报内容
                //BookMarks[3] = "REPORTTIME";  //预报时间
                //BookMarks[4] = "REPORTNORTH";//渤海、黄海北部预告
                //BookMarks[5] = "REPORTSOUTH";//黄海中部、黄海南部
                //BookMarks[6] = "SENDDEPARTMENT";//主、抄送机关
                BookMarks[5] = "HEADREPORTER";//主预报员
                BookMarks[6] = "DEPUTYREPORTER";//副预报员
                doc.Bookmarks.get_Item(ref BookMarks[0]).Range.Text = month.REPORTNO;
                doc.Bookmarks.get_Item(ref BookMarks[1]).Range.Text = date;
                doc.Bookmarks.get_Item(ref BookMarks[2]).Range.Text = month.REPORTTITLE;
                //doc.Bookmarks.get_Item(ref BookMarks[3]).Range.Text = projectDay.reportTime;
                doc.Bookmarks.get_Item(ref BookMarks[4]).Range.Text = month.REPORTCONTENT;
                //doc.Bookmarks.get_Item(ref BookMarks[5]).Range.Text = month.REPORTSOUTH;
                //doc.Bookmarks.get_Item(ref BookMarks[6]).Range.Text = projectDay.sendDepartment;
                doc.Bookmarks.get_Item(ref BookMarks[5]).Range.Text = month.HEADREPORTER;
                doc.Bookmarks.get_Item(ref BookMarks[6]).Range.Text = month.DEPUTYREPORTER;


                if (tabledoc != null)
                {
                    object what = Word.WdGoToItem.wdGoToBookmark;
                    object BookMarkName = "SEAICE";
                    doc.ActiveWindow.Selection.GoTo(ref what, ref missing, ref missing, ref BookMarkName);
                    app.ActiveWindow.Selection.PasteAndFormat(Word.WdRecoveryType.wdPasteDefault);
                }





                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                if (tabledoc != null)
                {
                    tabledoc.Close(ref IsSave, ref missing, ref missing);
                }
                return 1;
            }
            catch (Exception ex)
            {
                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                if (tabledoc != null)
                {
                    tabledoc.Close(ref IsSave, ref missing, ref missing);
                }
                WriteLog.Write(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 保存各油田数据到标签
        /// </summary>
        /// <param name="app"></param>
        /// <param name="FileName"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private int SaveCNMark(Word.Application app, string FileName, ReportMonthModel month, string TablePathList)
        {
            //表格复制部分的代码
            Word.Document tabledoc = null;
            if (TablePathList != "")
            {
                //复制表格部分
                tabledoc = new Word.Document();
                object Nothing = System.Reflection.Missing.Value;
                object IsReadOnly = false;
                object filename = TablePathList;
                tabledoc = app.Documents.Open(ref filename, ref Nothing, ref IsReadOnly, ref IsReadOnly,
                    ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                    ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                    ref Nothing, ref Nothing, ref Nothing,
                    ref Nothing);

                //选中word中的第一个表格
                tabledoc.Tables[1].Select();
                //将表格复制到剪切板中
                app.Selection.Copy();
            }



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

            Word.Paragraph _wordPara = doc.Content.Paragraphs.Add(ref missing);
            Word.Range _wordRange = doc.Paragraphs[1].Range;

            doc.Activate();
            //修改书签
            try
            {
                //保存发布时间到标签时，改为年月日格式
                string date = this.FormatDate(month.PUBLISHTIME);
                object[] BookMarks = new object[5];
                BookMarks[0] = "PUBLISHTIME"; //发布时间
                BookMarks[1] = "REPORTNAME";  //预报名称
                BookMarks[2] = "REPORTCONTENT";//预报内容
                //BookMarks[3] = "SENDDEPARTMENT";//主、抄送机关
                BookMarks[3] = "HEADREPORTER";//主预报员
                BookMarks[4] = "DEPUTYREPORTER";//副预报员
                doc.Bookmarks.get_Item(ref BookMarks[0]).Range.Text = date;
                doc.Bookmarks.get_Item(ref BookMarks[1]).Range.Text = month.REPORTTITLE;
                doc.Bookmarks.get_Item(ref BookMarks[2]).Range.Text = month.REPORTCONTENT;
                //doc.Bookmarks.get_Item(ref BookMarks[3]).Range.Text = sendDepartment;
                doc.Bookmarks.get_Item(ref BookMarks[3]).Range.Text = month.HEADREPORTER;
                doc.Bookmarks.get_Item(ref BookMarks[4]).Range.Text = month.DEPUTYREPORTER;

                if (tabledoc != null)
                {
                    object what = Word.WdGoToItem.wdGoToBookmark;
                    object BookMarkName = "SEAICE";
                    doc.ActiveWindow.Selection.GoTo(ref what, ref missing, ref missing, ref BookMarkName);
                    app.ActiveWindow.Selection.PasteAndFormat(Word.WdRecoveryType.wdPasteDefault);
                }


                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                if (tabledoc != null)
                {
                    tabledoc.Close(ref IsSave, ref missing, ref missing);
                }
                return 1;
            }
            catch (Exception ex)
            {
                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                if (tabledoc != null)
                {
                    tabledoc.Close(ref IsSave, ref missing, ref missing);
                }
                WriteLog.Write(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 时间格式格式化
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string FormatDate(string date)
        {
            string formatDate = date.Substring(0, 4) + "年" + date.Substring(4, 2) + "月" + date.Substring(6, 2) + "日";
            return formatDate;
        }

        /// <summary>
        /// 插入文件流
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="docName"></param>
        /// <returns></returns>
        //public int InsertFlow(string FileName, string docName, string YBQUYU, string YBNEIRONG, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        //{
        //    FileStream fs = File.OpenRead(FileName + "\\" + docName);
        //    byte[] b = new byte[fs.Length];
        //    fs.Read(b, 0, b.Length);
        //    fs.Close();
        //    //word转图片
        //    byte[] btImg = this.TransWordToImg(docName, FileName + "//" + docName);
        //    sql_ReportMonthOrDays sqlMonthOrDays = new sql_ReportMonthOrDays();
        //    return sqlMonthOrDays.InsertFlow(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        //}
        /// <summary>
        /// 修改文件流
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="docName"></param>
        /// <returns></returns>
        //public int UpdateFlow(string FileName, string docName, string YBQUYU, string YBNEIRONG, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        //{
        //    FileStream fs = File.OpenRead(FileName + "\\" + docName);
        //    byte[] b = new byte[fs.Length];
        //    fs.Read(b, 0, b.Length);
        //    fs.Close();
        //    //word转图片
        //    byte[] btImg = this.TransWordToImg(docName, FileName + "//" + docName);
        //    sql_ReportMonthOrDays sqlMonthOrDays = new sql_ReportMonthOrDays();
        //    return sqlMonthOrDays.UploadFlow(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        //}
        /// <summary>
        /// 插入修改文件流
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="docName"></param>
        /// <returns></returns>
        public string InsertOrUpdateFlow_ME(string FileName, string docName, string YBQUYU, string YBNEIRONG, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI )
        {
            FileStream fs = File.OpenRead(FileName + "\\" + docName);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();
            //word转图片
            byte[] btImg = this.TransWordToImg(docName, FileName + "//" + docName);

            sql_ReportMonthOrDays sqlMonthOrDays = new sql_ReportMonthOrDays();
            //sql_CommonSendUnit commonSendUnits = new sql_CommonSendUnit();
            DataTable dt = sqlMonthOrDays.GetYUBAO_ME(docName);
            int rultInsert = 0;//返回值
            int rultUpdate = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                rultUpdate = sqlMonthOrDays.UploadFlow_ME(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                if (rultUpdate == 0)
                {
                    return "Failed";
                }
                else
                {
                    return "Success";
                }
            }
            else
            {
                rultInsert = sqlMonthOrDays.InsertFlow_ME(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI); 
                if (rultInsert == 0)
                {
                    return "Failed";
                }
                else
                {
                    return "Success";
                }
            }

            //FileStream fs = File.OpenRead(FileName + "\\" + docName);
            //byte[] b = new byte[fs.Length];
            //fs.Read(b, 0, b.Length);
            //fs.Close();
            ////word转图片
            //byte[] btImg = this.TransWordToImg(docName, FileName + "//" + docName);
            //sql_ReportMonthOrDays sqlMonthOrDays = new sql_ReportMonthOrDays();
            //return sqlMonthOrDays.UploadFlow(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        }
        /// <summary>
        /// 插入修改文件流
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="docName"></param>
        /// <returns></returns>
        public string InsertOrUpdateFlow_FILE(string FileName, string docName, string YBQUYU, string YBNEIRONG, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            FileStream fs = File.OpenRead(FileName + "\\" + docName);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();
            //word转图片
            byte[] btImg = this.TransWordToImg(docName, FileName + "//" + docName);

            sql_ReportMonthOrDays sqlMonthOrDays = new sql_ReportMonthOrDays();
            //sql_CommonSendUnit commonSendUnits = new sql_CommonSendUnit();
            DataTable dt = sqlMonthOrDays.GetYUBAO_FILE(docName);
            int rultInsert = 0;//返回值
            int rultUpdate = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                rultUpdate = sqlMonthOrDays.UploadFlow_FILE(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                if (rultUpdate == 0)
                {
                    return "Failed";
                }
                else
                {
                    return "Success";
                }
            }
            else
            {
                rultInsert = sqlMonthOrDays.InsertFlow_FILE(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
                if (rultInsert == 0)
                {
                    return "Failed";
                }
                else
                {
                    return "Success";
                }
            }

            //FileStream fs = File.OpenRead(FileName + "\\" + docName);
            //byte[] b = new byte[fs.Length];
            //fs.Read(b, 0, b.Length);
            //fs.Close();
            ////word转图片
            //byte[] btImg = this.TransWordToImg(docName, FileName + "//" + docName);
            //sql_ReportMonthOrDays sqlMonthOrDays = new sql_ReportMonthOrDays();
            //return sqlMonthOrDays.UploadFlow(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        }
        /// <summary>
        /// word转图片
        /// </summary>
        /// <param name="strone">word名称</param>
        /// <param name="filepath">word路径</param>
        /// <returns></returns>
        private byte[] TransWordToImg(string strone, string filepath)
        {

            //word文档生成图片转二进制
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/Images/YUBAOhbImg");
            PredicTable.ExportWord.JingBao.Word word = new ExportWord.JingBao.Word();
            string path = filepath.Substring(0, filepath.LastIndexOf("\\"));
            word.WordToImage(strone, path, imagepath, strone.Split('.')[0], ImageFormat.Png, 2);

            byte[] byfileimg;
            if (System.IO.File.Exists(imagepath + "/" + strone.Split('.')[0] + ".png"))
            {
                FileStream fs = new FileStream(imagepath + "/" + strone.Split('.')[0] + ".png", FileMode.Open);
                byfileimg = new byte[fs.Length];
                fs.Read(byfileimg, 0, byfileimg.Length);
                fs.Close();
            }
            else
            {
                byfileimg = null;
            }
            return byfileimg;
        }
    }
}