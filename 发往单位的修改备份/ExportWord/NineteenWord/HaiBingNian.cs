using Microsoft.Office.Interop.Word;
using PredicTable.ExportWord.JingBao;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.ExportWord.NineteenWord
{
    public class HaiBingNian
    {
        Common common;
        public HaiBingNian()
        {
            common = new Common();
        }
        string DATE = "";
        string NO = "";
        string CONTENT = "";
        string SENTTO = "";
        string USER = "";

        /// <summary>
        /// 调用模板生成word
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="filepath">生成的具有模板样式的路径</param>
        /// <param name="fileName">生成的具有模板样式的新文件</param>
        /// <param name="Contentvalue">参数</param>
        /// <param name="JingBaomI">参数</param>
        /// <param name="param">参数</param>
        public int ExportWord(string templateFile, string filepath, string fileName, List<string> param)
        {
           
            //生成word程序对象
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            //生成新文件
            string file = common.CreateFile(app, templateFile, filepath, fileName);
            
            //生成documnet对象
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            object Obj_FileName = file;
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
                NO = param[0];
                DATE = param[1];
                CONTENT= param[2];
                USER = param[3];
                SENTTO = param[4];

                #region 把带有标签的数据放到html文件中

                string htmlFile = System.Web.HttpContext.Current.Server.MapPath("/word/s-19/HBHtml");//temp.html路径        
                if (!System.IO.Directory.Exists(htmlFile))
                {
                    System.IO.Directory.CreateDirectory(htmlFile);
                }
                
                StreamWriter sw = new StreamWriter(htmlFile+ "\\xxtemp.html", false, Encoding.Default);
                sw.Write("<html><head></head><body>");
                sw.Write(CONTENT.ToString());
                sw.Write("</body></html>");
                sw.Close();
                #endregion
                //为了方便管理声明书签数组
                object[] BookMark = new object[5];
                //赋值书签名
                BookMark[0] = "NO";//编号
                BookMark[1] = "DATE";//时间前
                BookMark[2] = "CONTENT";//内容
                BookMark[3] = "SENTTO";//发往
                BookMark[4] = "USER";//联系人
                //赋值数据到书签的位置
                doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = NO;
                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = DATE;
                //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = CONTENT;
                
                
                #region 将html文件的内容写入对应的书签
                object oFalse = false;
                object oTrue = true;
                //doc.Bookmarks.get_Item(ref BookMark[2]).Range.InsertFile(htmlFile + "\\xxtemp.html", ref missing, ref oFalse, ref oTrue, ref oFalse);
                doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = SENTTO;
                doc.Bookmarks.get_Item(ref BookMark[4]).Range.Text = USER;
                doc.Bookmarks.get_Item(ref BookMark[2]).Range.InsertFile(htmlFile + "\\xxtemp.html", ref missing, ref oFalse, ref oTrue, ref oFalse);
                #endregion
                // 插入图片
                //   common.InsertImg(doc,app, BookMark, Qfaimg,4);


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
                //throw ex;
            }
        }

    }
}