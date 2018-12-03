using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace PredicTable.ExportWord
{
    /// <summary>
    /// 处理Word文件
    /// 复制新文件并处理书签
    /// </summary>
    public class HandleWord : System.Web.UI.Page
    {
        Sql_HandelWord sql_handelWord;
        public HandleWord()
        {
            sql_handelWord = new Sql_HandelWord();
        }

        /**********************操作中文模板***************************************/
        /// <summary>
        /// 复制Word模板
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="fileName"></param>
        public int CopyCNWord(string templateFile, string fileName,string method, string pbtime, string ybtime, string ybcontent, string headReporter, string deputyReporter, string sendDepartment)
        {
            //生成word程序对象
            Word.Application app = new Word.Application();

            //模板文件
            string TemplateFile = templateFile;
            //生成的具有模板样式的新文件
            string FileName = fileName;
            //删除word文件
            System.IO.File.Delete(FileName);
            //模板文件拷贝到新文件
            File.Copy(TemplateFile, FileName);
            int flag = SaveCNMark(app, FileName, pbtime, ybtime, ybcontent, headReporter, deputyReporter, sendDepartment);
            return flag;
        }

        /// <summary>
        /// 更改中文预报单标签
        /// </summary>
        private int SaveCNMark(Word.Application app,string FileName, string pbtime, string ybtime, string ybcontent, string headReporter, string deputyReporter, string sendDepartment)
        {
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
            //修改书签
            try
            {
                //保存发布时间到标签时，改为年月日格式
                string date = this.FormatDate(pbtime);
                object[] BookMarks = new object[5];
                BookMarks[0] = "PUBLISHTIME"; //发布时间
                BookMarks[1] = "REPORTNAME";  //预报名称
                BookMarks[2] = "REPORTCONTENT";//预报内容
                //BookMarks[3] = "SENDDEPARTMENT";//主、抄送机关
                BookMarks[3] = "HEADREPORTER";//主预报员
                BookMarks[4] = "DEPUTYREPORTER";//副预报员
                doc.Bookmarks.get_Item(ref BookMarks[0]).Range.Text = date;
                doc.Bookmarks.get_Item(ref BookMarks[1]).Range.Text = ybtime;
                doc.Bookmarks.get_Item(ref BookMarks[2]).Range.Text = ybcontent;
                //doc.Bookmarks.get_Item(ref BookMarks[3]).Range.Text = sendDepartment;
                doc.Bookmarks.get_Item(ref BookMarks[3]).Range.Text = headReporter;
                doc.Bookmarks.get_Item(ref BookMarks[4]).Range.Text = deputyReporter;
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

        /// <summary>
        /// 保存新建中文预报单入库
        /// </summary>
        /// <param name="FileName"></param>
        public int SaveCNProject(string FileName, Project_CN projectCN,string docName,string type)
        {
            FileStream fs = File.OpenRead(FileName);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();
            int rult = 0;
            //word转图片
            byte[] btImg = this.TransWordToImg(docName, FileName);
            if (type == "add")
            {
                rult = sql_handelWord.SaveCNWord(FileName, b, projectCN, docName, btImg);
            }
            else if (type == "update")
            {
                rult = sql_handelWord.UpdateCNWord(FileName, b, docName, btImg);
            }
            return rult;
        }

        /**********************操作英文旬、月模板***************************************/

        /// <summary>
        /// 复制Word模板
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="fileName">复制生成的新文件</param>
        /// <param name="projectDay">实体类</param>
        /// <returns></returns>
        public int CopyENDayWord(string templateFile,string fileName, Project_ENDay projectDay)
        {
            //生成word程序对象
            Word.Application app = new Word.Application();

            //模板文件
            string TemplateFile = templateFile;
            //生成的具有模板样式的新文件
            string FileName = fileName;
            //删除word文件
            System.IO.File.Delete(FileName);
            //模板文件拷贝到新文件
            File.Copy(TemplateFile, FileName);
            int flag = SaveENDayMark(app, FileName,projectDay);
            return flag;
        }

        /// <summary>
        /// 属性值插入到Word标签
        /// </summary>
        /// <param name="app"></param>
        /// <param name="FileName"></param>
        /// <param name="projectDay"></param>
        /// <returns></returns>
        private int SaveENDayMark(Word.Application app,string FileName, Project_ENDay projectDay)
        {
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
            //修改书签
            //修改书签
            try
            {
                //保存发布时间到标签时，改为年月日格式
                string date = this.FormatDate(projectDay.publishTime);
                object[] BookMarks = new object[8];
                BookMarks[0] = "REPORTNO"; //预报单编号
                BookMarks[1] = "PUBLISHTIME"; //发布时间
                BookMarks[2] = "REPORTTITLE";  //预报名称
                BookMarks[3] = "REPORTTIME";  //预报时间
                BookMarks[4] = "REPORTNORTH";//渤海、黄海北部预告
                BookMarks[5] = "REPORTSOUTH";//黄海中部、黄海南部
                //BookMarks[6] = "SENDDEPARTMENT";//主、抄送机关
                BookMarks[6] = "HEADREPORTER";//主预报员
                BookMarks[7] = "DEPUTYREPORTER";//副预报员
                doc.Bookmarks.get_Item(ref BookMarks[0]).Range.Text = projectDay.reportNo;
                doc.Bookmarks.get_Item(ref BookMarks[1]).Range.Text = date;
                doc.Bookmarks.get_Item(ref BookMarks[2]).Range.Text = projectDay.reportTitle;
                doc.Bookmarks.get_Item(ref BookMarks[3]).Range.Text = projectDay.reportTime;
                doc.Bookmarks.get_Item(ref BookMarks[4]).Range.Text = projectDay.reportNorth;
                doc.Bookmarks.get_Item(ref BookMarks[5]).Range.Text = projectDay.reportSouth;
                //doc.Bookmarks.get_Item(ref BookMarks[6]).Range.Text = projectDay.sendDepartment;
                doc.Bookmarks.get_Item(ref BookMarks[6]).Range.Text = projectDay.headReporter;
                doc.Bookmarks.get_Item(ref BookMarks[7]).Range.Text = projectDay.deputyReporter;
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

        /// <summary>
        /// 保存Word文件流
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="projectDay"></param>
        public int SaveENDayProject(string FileName, Project_ENDay projectDay,string docName,string type,string ENDayType)
        {
            FileStream fs = File.OpenRead(FileName);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();
            int rult = 0;
            //word转图片
            byte[] btImg = this.TransWordToImg(docName, FileName);
            if (type == "add")
            {
                rult = sql_handelWord.SaveENDayWord(FileName, b, projectDay, docName, ENDayType, btImg);
            }
            else if (type == "update")
            {
                rult = sql_handelWord.UpdateENDayWord(FileName, b, docName, ENDayType, btImg);
            }
            return rult;
        }

        /**********************操作英文年模板***************************************/
        /// <summary>
        /// 生成新年预报单
        /// </summary>
        /// <param name="templateFile">模板路径</param>
        /// <param name="fileName">新生成预报单名称</param>
        /// <param name="projectYear">实体类</param>
        /// <returns></returns>
        public int CopyENYearWord(string templateFile, string fileName, Project_ENYear projectYear)
        {
            //生成word程序对象
            Word.Application app = new Word.Application();

            //模板文件
            string TemplateFile = templateFile;
            //生成的具有模板样式的新文件
            string FileName = fileName;
            //删除word文件
            System.IO.File.Delete(FileName);
            //模板文件拷贝到新文件
            File.Copy(TemplateFile, FileName);
            int flag = this.SaveENYearMark(app, FileName, projectYear);
            return flag;
        }
        /// <summary>
        /// 年预报单标签中插入属性值
        /// </summary>
        /// <param name="app"></param>
        /// <param name="FileName"></param>
        /// <param name="projectYear"></param>
        /// <returns></returns>
        private int SaveENYearMark(Word.Application app, string FileName, Project_ENYear projectYear)
        {
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
            //修改书签
            //修改书签
            try
            {
                //保存发布时间到标签时，改为年月日格式
                string date = this.FormatDate(projectYear.publishTime);
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
                doc.Bookmarks.get_Item(ref BookMarks[0]).Range.Text = projectYear.reportNo;
                doc.Bookmarks.get_Item(ref BookMarks[1]).Range.Text = date;
                doc.Bookmarks.get_Item(ref BookMarks[2]).Range.Text = projectYear.reportTitle;
                doc.Bookmarks.get_Item(ref BookMarks[3]).Range.Text = projectYear.stormSurge;
                doc.Bookmarks.get_Item(ref BookMarks[4]).Range.Text = projectYear.seaWave;
                doc.Bookmarks.get_Item(ref BookMarks[5]).Range.Text = projectYear.redTide;
                doc.Bookmarks.get_Item(ref BookMarks[6]).Range.Text = projectYear.greebTide;
                doc.Bookmarks.get_Item(ref BookMarks[7]).Range.Text = projectYear.tropicalCyclone;
                doc.Bookmarks.get_Item(ref BookMarks[8]).Range.Text = projectYear.headReporter;
                doc.Bookmarks.get_Item(ref BookMarks[9]).Range.Text = projectYear.deputyReporter;
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
        /// <summary>
        /// 保存年预报单入库
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="projectYear"></param>
        public int SaveENYearProject(string FileName, Project_ENYear projectYear, string docName, string type)
        {
            FileStream fs = File.OpenRead(FileName);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();
            //System.IO.File.Delete(Server.MapPath("pageoffice/doc/oplonger/") + FileName);
            //word转图片
            int rult = 0;
            byte[] btImg = this.TransWordToImg(docName, FileName);
            if (type == "add")
            {
                rult = sql_handelWord.SaveENYearWord(FileName, b, projectYear, docName, btImg);
            }
            else if(type == "update")
            {
                rult = sql_handelWord.UpdateENYearWord(FileName, b, docName, btImg);
            }
            return rult;
        }

        private string FormatDate(string date)
        {
            string formatDate = date.Substring(0,4)+"年"+ date.Substring(4, 2)+"月"+ date.Substring(6, 2)+"日";
            return formatDate;
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
            string path =  filepath.Substring(0, filepath.LastIndexOf("\\"));
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