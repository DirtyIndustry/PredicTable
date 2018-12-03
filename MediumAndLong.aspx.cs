
using System;
using System;
using System.Collections.Generic;
using System.Data;

﻿using System;
using System.Data;

using System.Web;

using Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Data;
//using Microsoft.Office.Core;

using System.Web.UI;
using System.Web.UI.WebControls;




namespace PredicTable
{
    public partial class MediumAndLong : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
            if (!IsPostBack)
            {
                this.GetWordModel();
            }
        }
        //protected void btnDq_Click(object sender, EventArgs e)
        //{
            
        //    string Aa = HttpContext.Current.Server.MapPath("pageoffice/doc/CN-MediumAndLong/2016年8月及上旬预报-东营环境预报.doc");
        //    Doc2Text(Aa);
        //    // TextBox1.Text = M_wd.ToString();

        //}
        ////获得word文件的文本内容
        //public void Doc2Text(string docFileName)
        //{
        //    ////实例化COM
        //    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
        //    object fileobj = docFileName;
        //    object nullobj = System.Reflection.Missing.Value;

        //    object missing = Missing.Value;

        //    object testpath = docFileName.ToString();

        //    //Microsoft.Office.Interop.Word._Document doc = wordApp.Documents.Open(ref testpath, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        //    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref fileobj, ref nullobj, ref nullobj,
        //        ref nullobj, ref nullobj, ref nullobj,
        //        ref nullobj, ref nullobj, ref nullobj,
        //        ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj
        //        );
        //    doc.Activate();
        //    string outText = doc.Content.Text;
        //    object bq = "date";
        //    Bookmark mbook = doc.Bookmarks.get_Item(ref bq);
        //    object range = doc.Bookmarks.get_Item(ref bq).Range;
        //    Paragraph wp = doc.Content.Paragraphs.Add(ref range);
        //    // Bookmark mbook = doc.Bookmarks.get_Item(ref bq);
        //    if (mbook.Name.Equals("date"))
        //    {
        //        mbook.Select();
        //        mbook.Range.ShowAll = true;
        //        mbook.Range.Select();
        //        mbook.Range.WholeStory();
        //        string test = mbook.Range.Text;
        //        TextBox1.Text = test.ToString();
        //    }
        //    doc.Close();
        //}
        /// <summary>
        /// 获取word模板
        /// </summary>
        private void GetWordModel()
        {
            string type = this.type.Value;
            Sql_UPLOADWORD sql_uploadword = new Sql_UPLOADWORD();
            System.Data.DataTable dt = (System.Data.DataTable)sql_uploadword.get_UPLOADWORDdata(type);
            if (dt.Rows.Count > 0)
            {
                this.list.DataSource = dt;
                //this.list.DataTextField = "OLDNAME";
                //this.list.DataValueField = "NEWNAME";
                this.list.DataTextField = "NEWNAME";
                this.list.DataValueField = "OLDNAME";
                this.list.DataBind();
            }
        }

        //引用模板
        protected void startoperation_Click(object sender, EventArgs e)
        {
            string fileName = "";//编辑后预报单文件名
            string type = this.type.Value;
            string filePath = "";
            string time = DateTime.Now.ToString("yyyyMMdd");
            string newName = this.newname.Value.ToString();
            Session["userid"] = "admin";//测试代码，用完删除
            if (this.chk_checkModel.Checked)
            {
                if (type == "EN")
                {
                    fileName = this.hidd_model.Value;
                    filePath = Server.MapPath("pageoffice/doc/EN-MediumAndLong/") + fileName;
                }
                else if (type == "CN")
                {
                    fileName = list.Items[list.SelectedIndex].Text;
                    filePath = Server.MapPath("pageoffice/doc/CN-MediumAndLong/") + fileName;
                }
                else {
                    return;
                }
            }
            else
            {
                //newName = this.newname.Value+ time + ".doc";
                //fileNane = uploadname.Value + ".doc";
            }
            HttpContext.Current.Session["oplonger"] = newName+".doc";
            PageOfficeCtrl1.Visible = true;
            PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
            PageOfficeCtrl1.SaveFilePage = "test.aspx";
            //StructureDocumentTagInline sd = new StructureDocumentTagInline(filePath);
            PageOfficeCtrl1.WebOpen(filePath, PageOffice.OpenModeType.docNormalEdit, "Tom");
        }
    }
}