using PredicTable.Dal;
using PredicTable.Model.NewMediumAndLong;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace PredicTable.ExportWord.NewMediumAndLong
{
    /// <summary>
    /// 
    /// </summary>
    public class YearWord
    {
        public int CopyWord(string templateFile, string fileName, ReportYearModel year)
        {
            //生成word程序对象
            Word.Application app = new Word.Application();

            //模板文件
            string TemplateFile = templateFile;
            //生成的具有模板样式的新文件
            string FileName = fileName+"/"+year.DOCNAME;
            if(Directory.Exists(fileName) == false)
            {
                Directory.CreateDirectory(fileName);
            }
            //删除word文件
            System.IO.File.Delete(FileName);
            //模板文件拷贝到新文件
            File.Copy(TemplateFile, FileName);
            int flag = SaveMark(app, FileName, year);
            return flag;
        }

        private int SaveMark(Word.Application app, string FileName, ReportYearModel year)
        {
            Word.Document tabledoc = null;
            if (year.SEAICE != null && year.SEAICETABLEPATH != null)
            {
                //复制表格部分
                tabledoc = new Word.Document();
                object Nothing = System.Reflection.Missing.Value;
                object IsReadOnly = false;
                object filename = year.SEAICETABLEPATH;
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
                string date = this.FormatDate(year.PUBLISHTIME);
                object[] BookMarks = new object[10];
                BookMarks[0] = "REPORTNO"; //预报单编号
                BookMarks[1] = "PUBLISHTIME"; //发布时间
                BookMarks[2] = "REPORTTITLE";  //预报名称
                BookMarks[3] = "STORMSURGE";//风暴潮
                BookMarks[4] = "SEAWAVE";//海浪
                BookMarks[5] = "REDTIDE";//赤潮
                BookMarks[6] = "GREENTIDE";//绿潮
                BookMarks[7] = "TROPICALCYCLONE";//热带气旋
                BookMarks[8] = "HEADREPORTER";//主预报员
                BookMarks[9] = "DEPUTYREPORTER";//副预报员
                doc.Bookmarks.get_Item(ref BookMarks[0]).Range.Text = year.REPORTNO;
                doc.Bookmarks.get_Item(ref BookMarks[1]).Range.Text = date;
                doc.Bookmarks.get_Item(ref BookMarks[2]).Range.Text = year.REPORTTITLE;
                doc.Bookmarks.get_Item(ref BookMarks[3]).Range.Text = year.STORMSURGE;
                doc.Bookmarks.get_Item(ref BookMarks[4]).Range.Text = year.SEAWAVE;
                doc.Bookmarks.get_Item(ref BookMarks[5]).Range.Text = year.REDTIDE;
                doc.Bookmarks.get_Item(ref BookMarks[6]).Range.Text = year.GREENTIDE;
                doc.Bookmarks.get_Item(ref BookMarks[7]).Range.Text = year.TROPICALCYCLONE;
                doc.Bookmarks.get_Item(ref BookMarks[8]).Range.Text = year.HEADREPORTER;
                doc.Bookmarks.get_Item(ref BookMarks[9]).Range.Text = year.DEPUTYREPORTER;

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
                if (tabledoc != null) {
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
        public int InsertFlow(string FileName, string docName,string  YBQUYU, string YBNEIRONG, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            FileStream fs = File.OpenRead(FileName + "\\" + docName);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();
            //word转图片
            byte[] btImg = this.TransWordToImg(docName, FileName + "//" + docName);
            sql_ReportYear sqlReportYear = new sql_ReportYear();
            return sqlReportYear.InsertFlow(docName,b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
        }
        /// <summary>
        /// 修改文件流
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="docName"></param>
        /// <returns></returns>
        public int UpdateFlow(string FileName, string docName, string YBQUYU, string YBNEIRONG, string YBSHIXIAO, string YBSHIJIAN, string YBDANWEI)
        {
            FileStream fs = File.OpenRead(FileName+ "\\"+docName);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();
            //word转图片
            byte[] btImg = this.TransWordToImg(docName, FileName+"//"+ docName);
            sql_ReportYear sqlReportYear = new sql_ReportYear();
            return sqlReportYear.UploadFlow(docName, b, btImg, YBQUYU, YBNEIRONG, YBSHIXIAO, YBSHIJIAN, YBDANWEI);
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