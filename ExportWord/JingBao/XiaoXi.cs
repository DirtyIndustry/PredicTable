﻿using Microsoft.Office.Interop.Word;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.ExportWord.JingBao
{
    public class XiaoXi
    {
        Common common;
        public XiaoXi()
        {
            common = new Common();
        }
        string Dwei = "";
        string waves = "";
        string Types = "";
        string times = "";
        string TM = "";
        string Qfaimg = "";
        string ueditor = "";
        string Fwang = "";
        string content = "";
        string XXTITLE = "";

        /// <summary>
        /// 调用模板生成word
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="filepath">生成的具有模板样式的路径</param>
        /// <param name="fileName">生成的具有模板样式的新文件</param>
        /// <param name="Contentvalue">参数</param>
        /// <param name="JingBaomI">参数</param>
        /// <param name="param">参数</param>
        public int ExportWord(string templateFile, string filepath, string fileName, CG_HT_XIAOXI_CONTENT Contentvalue, CG_XIAOXI_ME JingBaomI, List<string> param)
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
               
                Dwei = "国家海洋局"+JingBaomI.XXDANWEI;
                waves = JingBaomI.XXNEIRONG;
                Types = param[0];
                times = param[1];
                TM = param[2];
                Qfaimg = param[3];
                ueditor = Contentvalue.CONTENT;
                Fwang = Contentvalue.SENTTO;
                content = Contentvalue.LINKMAN;
                XXTITLE = Contentvalue.XXTITLE;
                #region 把带有标签的数据放到html文件中
                string htmlFile = System.Web.HttpContext.Current.Server.MapPath("/word/JingBao/JBHtml/xxtemp.html");//temp.html路径
                StreamWriter sw = new StreamWriter(htmlFile, false, Encoding.Default);
                sw.Write("<html><head></head><body>");
                sw.Write(ueditor.ToString());
                sw.Write("</body></html>");
                sw.Close();
                #endregion
                //为了方便管理声明书签数组
                object[] BookMark = new object[9];
                //赋值书签名
                BookMark[0] = "Dwei";//发布单位
                                     //BookMark[1] = "waves";//标题1前
                                     //BookMark[2] = "Types";//标题1后
                BookMark[1] = "wavesandtypes";
                BookMark[2] = "times";//时间前
                BookMark[3] = "TM";//时间后
                BookMark[4] = "Qfaimg";//签发图
                BookMark[5] = "ueditor";//内容
                BookMark[6] = "Fwang";//发往
                BookMark[7] = "content";//联系人
                BookMark[8] = "XXTITLE";//消息标题
                //赋值数据到书签的位置
                doc.Bookmarks.get_Item(ref BookMark[0]).Range.Text = Dwei;
                doc.Bookmarks.get_Item(ref BookMark[1]).Range.Text = waves+Types;
                //doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = Types;
                doc.Bookmarks.get_Item(ref BookMark[2]).Range.Text = times;
                doc.Bookmarks.get_Item(ref BookMark[3]).Range.Text = TM;

                //doc.Bookmarks.get_Item(ref BookMark[5]).Range.Text = Qfaimg;
              
                #region 将html文件的内容写入对应的书签
                object oFalse = false;
                object oTrue = true;
                doc.Bookmarks.get_Item(ref BookMark[5]).Range.InsertFile(htmlFile, ref missing, ref oFalse, ref oTrue, ref oFalse);
                #endregion
                // 插入图片
                common.InsertImg(doc,app, BookMark, Qfaimg,4);

                doc.Bookmarks.get_Item(ref BookMark[6]).Range.Text = Fwang;
                doc.Bookmarks.get_Item(ref BookMark[7]).Range.Text = content;
                doc.Bookmarks.get_Item(ref BookMark[8]).Range.Text = XXTITLE;
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
}