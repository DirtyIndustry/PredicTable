using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class PriViewSCOTable : System.Web.UI.Page, IHttpHandler, IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tblType = Request.QueryString["PriviewType"].ToString();
            string inFilePath = "";
            DateTime date = Convert.ToDateTime(Request.QueryString["date"].ToString());
            //string a = 
            if (!IsPostBack)
            {
                
                if (tblType != "1")
                {
                    inFilePath = AppDomain.CurrentDomain.BaseDirectory + "预览文件\\YB_PINGTAI_HJ_72hr_" + date.ToString("yyyyMMdd") + "15_NMFC.pdf";
                }
                else
                {
                    inFilePath = AppDomain.CurrentDomain.BaseDirectory + "预览文件\\YB_SHANGHE_HJ_72hr_" + date.ToString("yyyyMMdd") + "15_NMFC.pdf";
                }
                if (File.Exists(inFilePath))
                {
                    this.Page.Response.ContentType = "Application/pdf";

                    string fileName = inFilePath.Substring(inFilePath.LastIndexOf('\\') + 1);
                    this.Page.Response.AddHeader("content-disposition", "filename=" + fileName);
                    this.Page.Response.WriteFile(inFilePath);
                    this.Page.Response.End();
                }
                  
                //SerialID = GetQueryString("SerialID", "10001");//获取文件主键
                //ShowContent();//预览的方法
                // Priview(this, "C:\\Users\\10628\\Desktop\\YB_SHANGHE_HJ_72hr_2018053015_NMFC.pdf");
            }
            
        }
        public  void Priview(string inFilePath)
        {
            this.Page.Response.ContentType = "Application/pdf";

            string fileName = inFilePath.Substring(inFilePath.LastIndexOf('\\') + 1);
            this.Page.Response.AddHeader("content-disposition", "filename=" + fileName);
            this.Page.Response.WriteFile(inFilePath);
            this.Page.Response.End();
        }
        public void ShowContent()
        {
            //string AttachMent2pdf = Server.MapPath(@"\DownLoadAttachment\" + string.Format("{0}.pdf", SerialID));//判断此文件是否以前打开过
            //if (File.Exists(AttachMent2pdf) == true)
            //{
            //    Priview(this, Server.MapPath(@"\DownLoadAttachment\" + string.Format("{0}.pdf", SerialID)));//此文件打开过找到以前的文件直接打开
            //    return;
            //}
            //else//如果第一次打开
            //{
            //    if (Directory.Exists(Server.MapPath("DownLoadAttachment")) == false)//判断存放转换后的文件夹是否存在
            //    {
            //        Directory.CreateDirectory(Server.MapPath(@"\DownLoadAttachment\"));//不存在文件夹就创建文件夹
            //    }
            //    CopyFile();//复制文件下面有方法（我只想做一次转换后以后打开不转换，所以每次转化的时候都要复制一个fdf文件来准备被替换）
            //    string _Ext = System.IO.Path.GetExtension(GetPptOrWordStarPath(SerialID));//获取扩展名
                //if (_Ext == ".doc" || _Ext == ".docx")//判断是ppt还是word
                //{

                //    WordToPdf(GetPptOrWordStarPath(SerialID), Server.MapPath(@"\DownLoadAttachment\" + string.Format("{0}.pdf", SerialID)));//word转化pdf的方法，上面有特别提醒注意路径
                //}
                //if (_Ext == ".ppt")
                //{
                //    PPTConvertToPDF(GetPptOrWordStarPath(SerialID), Server.MapPath(@"\DownLoadAttachment\" + string.Format("{0}.pdf", SerialID)));
                //}
           // }
           // Priview(this, Server.MapPath(@"\DownLoadAttachment\" ));
        }

        //public void CopyFile()
        //{
        //    string sourceFile = Server.MapPath(@"\SourceFile\123456.pdf");
        //    string objectFile = Server.MapPath(@"\DownLoadAttachment\" + string.Format("{0}.pdf", SerialID));
        //    if (System.IO.File.Exists(sourceFile))
        //    {
        //        System.IO.File.Copy(sourceFile, objectFile, true);
        //    }
        //}
    }
}