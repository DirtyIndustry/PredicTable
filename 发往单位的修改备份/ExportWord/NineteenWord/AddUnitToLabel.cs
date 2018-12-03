using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace PredicTable.ExportWord.NineteenWord
{
    public class AddUnitToLabel
    {
        /// <summary>
        /// 向Word标签中添加发送单位
        /// </summary>
        /// <param name="templateName">已生成文件位置</param>
        /// <param name="unitName">发送单位名称</param>
        public void AddUnit(string templateName,string unitName)
        {
            //生成word程序对象
            Word.Application app = new Word.Application();
            SaveMark(app,templateName, unitName);
        }
        /// <summary>
        /// 保存到书签
        /// </summary>
        /// <param name="app"></param>
        /// <param name="templateName"></param>
        /// <param name="unitName">发送单位名称</param>
        private int SaveMark(Word.Application app, string templateName, string unitName)
        {
            //生成documnet对象
            Word.Document doc = new Word.Document();
            object Obj_FileName = templateName;
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
                object bookMark = new object();
                bookMark = "SendUnit";
                doc.Bookmarks.get_Item(ref bookMark).Range.Text = unitName;
                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                return 1;
            }
            catch (Exception error)
            {
                //输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                WriteLog.Write(error.ToString());
                return 0;
            }
        }
    }
}