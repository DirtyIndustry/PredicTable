using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
namespace PredicTable
{
    /// <summary>
    /// 测试检索书签
    /// </summary>
    public partial class TestBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCheckBook_Click(object sender, EventArgs e)
        {
            var docFileName = "F:/Project/预报单/02预警报制作系统/Test书签.docx";
               Application wordApp = new Microsoft.Office.Interop.Word.Application();
            object fileobj = docFileName;
            object nullobj = System.Reflection.Missing.Value;

            object missing = Missing.Value;

            object testpath = docFileName.ToString();

            //Microsoft.Office.Interop.Word._Document doc = wordApp.Documents.Open(ref testpath, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            Word.Document doc = wordApp.Documents.Open(ref fileobj, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj
                );
            doc.Activate();
            string outText = doc.Content.Text;
            object bq = "fbcontent";
            Bookmark mbook = doc.Bookmarks.get_Item(ref bq);
            object range = doc.Bookmarks.get_Item(ref bq).Range;
            Paragraph wp = doc.Content.Paragraphs.Add(ref range);
            // Bookmark mbook = doc.Bookmarks.get_Item(ref bq);
            if (mbook.Name.Equals("fbcontent"))
            {
                mbook.Select();
                mbook.Range.ShowAll = true;
                mbook.Range.Select();
                mbook.Range.WholeStory();
                string test = mbook.Range.Text;
                Response.Write(test);
            }
            doc.Close();
        }
    }
}