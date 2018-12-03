using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PredicTable.ExportWord.JingBao
{
    /// <summary>
    /// 警报表单公共方法
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 生成新文件
        /// </summary>
        /// <param name="templateFile">模板路径</param>
        /// <param name="filepath">文件路径</param>
        /// <param name="fileName">文件名称</param>
        public string CreateFile(Microsoft.Office.Interop.Word.Application app ,string templateFile, string filepath, string fileName)
        {
            //模板文件
            string TemplateFile = templateFile;
            //生成的具有模板样式的新文件
            string FileName = fileName;
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory(filepath);
            }
            string file = filepath + "\\" + fileName;
            //检查文件是否存在
            if (File.Exists(@file))
            {
                File.Delete(@file);
                File.Copy(TemplateFile, file);
            }
            else
            {
                //模板文件拷贝到新文件
                File.Copy(TemplateFile, file);
            }
            return file;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="app"></param>
        /// <param name="BookMark">标签</param>
        /// <param name="Qfaimg">签发人姓名图片</param>
        /// <param name="bookMarkPosition">图片在Word中的标签号</param>
        public void InsertImg(Microsoft.Office.Interop.Word.Document doc, Microsoft.Office.Interop.Word.Application app, object[] BookMark,string Qfaimg,int bookMarkPosition) {
            try
            {
                object Nothing = System.Reflection.Missing.Value;
                //定义插入图片是否随word文档一起保存
                object saveWithDocument = true;

                if (doc.Bookmarks.Exists(Convert.ToString(BookMark[bookMarkPosition])) == true)
                {
                    //查找书签
                    doc.Bookmarks.get_Item(ref BookMark[bookMarkPosition]).Select();
                    object linkToFile = true;
                    //设置图片位置
                    app.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    //在书签的位置添加图片
                    InlineShape inlineShape = app.Selection.InlineShapes.AddPicture(Qfaimg, ref linkToFile,
                        ref saveWithDocument, ref Nothing);
                    //设置图片大小
                    inlineShape.Width = 100;
                    inlineShape.Height = 60;

                    doc.Save();
                }
                else
                {

                    //word文档中不存在该书签，关闭文档

                    //doc.Close(ref Nothing, ref Nothing, ref Nothing);
                }
            }
            catch {

            }
        }
    }
}