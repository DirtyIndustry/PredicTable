using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Core;//COM选项卡中的"Microsoft Office 11.0 Object Library"
using Word = Microsoft.Office.Interop.Word;//.NET选项卡中的"Microsoft.Office.Interop.Word"
using System.Collections;

namespace PredicTable
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        public object MessageBox { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //生成3号预报单
                ThreeWord();
                //生成4号预报单
                FourWord();
                //生成7号预报单
                SevenWord();
                //生成9号预报单
                NineWord();
                //生成10号预报单
                TenWord();
                //生成11号预报单
                ElevenWord();
                //生成12号预报单
                TwelveWord();
                //生成14号海上山东
                FourteenWord();
                //生成15号预报单
                FifteenWord();
                //20号潍坊市海洋预报台专项预报
                TwentyWord();
                //24号东营近海
                TwentyfourWord();
                Label1.Text = "生成预报单成功!";
            }
            catch (Exception ex)
            {

                WriteLog.Write(ex.ToString());
            }
           
        }
        /// <summary>
        /// 生成3号预报单
        /// </summary>
        private void ThreeWord()
        {
            try
            {
                //3号预报单生成
                string path = Server.MapPath("config/detail_1.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/3号黄河南海堤预报单.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //3号黄河南海堤预报单生成
                    ThreeWord three = new ThreeWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间    
                                               //入库  
                    three.ExportWord(templateFile, fileName,dt);
                    ruku(fileName, strone, dt);
                }

                //删除
                string path1 = Server.MapPath("config/detail_1.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }
        /// <summary>
        /// 生成4号预报单
        /// </summary>
        private void FourWord()
        {
            try
            {
                //4号预报单生成
                string path = Server.MapPath("config/detail_2.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/4号预报单.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //4号预报单
                    FourWord Four = new FourWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间             
                                               //入库     
 Four.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);
                }

                //删除
                string path1 = Server.MapPath("config/detail_2.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();
                return;
            }
        }

        /// <summary>
        /// 生成7号预报单
        /// </summary>
        private void SevenWord()
        {
            try
            {
                //7号预报单生成
                string path = Server.MapPath("config/detail_3.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/7号海洋水温海冰预报.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //7号
                    SevenWord seven = new SevenWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间  
 seven.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_3.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }
        /// <summary>
        /// 生成10号预报单
        /// </summary>
        private void TenWord()
        {
            try
            {
                //10号预报单生成
                string path = Server.MapPath("config/detail_7.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/10号预报单.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //10号黄河南海堤预报单生成
                    TenWord ten = new TenWord();
                    
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间     
ten.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_7.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }
        /// <summary>
        /// 生成9号预报单
        /// </summary>
        private void NineWord()
        {
            try
            {
                //9号预报单
                string path = Server.MapPath("config/detail_4.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/9号预报单.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //9号预报单
                    NineWord nine = new NineWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间   
 nine.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_4.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }

        /// <summary>
        /// 生成11号预报单
        /// </summary>
        private void ElevenWord()
        {
            try
            {
                //11号预报单
                string path = Server.MapPath("config/detail_6.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/11号预报单.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //11号预报单
                    ElevenWord eleven = new ElevenWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间     
 eleven.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_6.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }

        /// <summary>
        /// 生成12号预报单
        /// </summary>
        private void TwelveWord()
        {
            try
            {
                //11号预报单
                string path = Server.MapPath("config/detail_8.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/12号预报单.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //11号预报单
                    TwelveWord twelve = new TwelveWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间  
 twelve.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_8.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }

        /// <summary>
        /// 生成14号海上山东
        /// </summary>
        private void FourteenWord()
        {
            try
            {
                //11号预报单
                string path = Server.MapPath("config/detail_9.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/14号海上山东.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //11号预报单
                    FourteenWord fourteen = new FourteenWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间 
 fourteen.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_9.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }

        /// <summary>
        /// 生成15号预报单
        /// </summary>
        private void FifteenWord()
        {
            try
            {
                //11号预报单
                string path = Server.MapPath("config/detail_10.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/15号预报单.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);
                    //11号预报单
                    FifteenWord fifteen = new FifteenWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间   
 fifteen.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_10.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }

        /// <summary>
        /// 生成20号潍坊市海洋预报台专项预报
        /// </summary>
        private void TwentyWord()
        {
            try
            {
                //20号预报单
                string path = Server.MapPath("config/detail_11.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/20号潍坊市海洋预报台专项预报.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //20号预报单
                    TwentyWord twenty = new TwentyWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间   
 twenty.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_11.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }

        /// <summary>
        /// 生成24号东营近海
        /// </summary>
        private void TwentyfourWord()
        {
            try
            {
                //24号东营近海
                string path = Server.MapPath("config/detail_12.txt");
                ArrayList list = gettext(path);
                if (list.Count == 1)
                {
                    string strone = list[0].ToString();
                    //模板文件
                    string templateFile = Server.MapPath("word/24号东营近海.doc");
                    //生成的具有模板样式的新文件           
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\" + strone + ".doc";
                    //删除word文件
                    System.IO.File.Delete(fileName);

                    //24号东营近海
                    TwentyfourWord twentyfour = new TwentyfourWord();
                   
                    //判断数据库是不是有当前预报单
                    DateTime dt = DateTime.Now;//前台当前预报时间    
 twentyfour.ExportWord(templateFile, fileName, dt);
                    ruku(fileName, strone, dt);//入库  
                }

                //删除
                string path1 = Server.MapPath("config/detail_12.txt");
                ArrayList list0 = gettext(path1);
                System.IO.StreamWriter sw = new StreamWriter(path1);
                if (list0.Count == 0)
                {

                }
                else
                {
                    list0.RemoveAt(0);//测试删除一项

                }
                sw.Close();
            }
            catch (Exception Ex)
            {
                Label1.Text = Ex.ToString();

                return;
            }
        }
        protected ArrayList gettext(string path)
        {
            StreamReader objReader = new StreamReader(path);
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            return LineList;
        }
        /// <summary>
        /// 生成表单入库
        /// </summary>
        /// <param name="fileName">入库的文件名</param>
        /// <param name="strone">对应表单</param>
        /// <param name="dt">当前时间</param>
        private void ruku(string fileName, string strone, DateTime dt)
        {
            string userid = "";
            if (Session["userid"] != null)
            {
                userid = Session["userid"].ToString();
            }
            TBLYBDDOCUMENT tbl = new TBLYBDDOCUMENT();
            tbl.UPLOADDATE = dt;
            List<int> a = new List<int>();
            int i1 = 0;
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new sql_TBLYBDDOCUMENT().get_TBLYBDDOCUMENT_AllData(tbl);
            if (tblybddocument.Rows.Count != 0)
            {
                for (int i = 0; i < tblybddocument.Rows.Count; i++)
                {
                    DateTime UPLOADDATE = Convert.ToDateTime(tblybddocument.Rows[i]["UPLOADDATE"].ToString());
                    string YBDNAME = tblybddocument.Rows[i]["YBDNAME"].ToString();

                    if (UPLOADDATE.ToString("yyyy-MM-dd") == dt.ToString("yyyy-MM-dd") && YBDNAME == (strone + ".doc"))
                    {
                        i1++;
                        a.Add(i1);
                    }
                }
                if (a.Count > 0)
                {
                    UpdateWord updateword = new UpdateWord();
                    updateword.updateword(fileName, dt, userid);
                }
                else
                {
                    //存入数据库
                    InsertWord insertword = new InsertWord();
                    insertword.saveword(fileName, dt, userid);
                }

            }
            else
            {
                //存入数据库
                InsertWord insertword = new InsertWord();
                insertword.saveword(fileName, dt, userid);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                string dtxtpath = "config/detail_1.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("3号黄河南海堤预报单");
                dsw1.Dispose();
            }
            if (RadioButton2.Checked == true)
            {
                string dtxtpath = "config/detail_2.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("4号预报单");
                dsw1.Dispose();
            }
            if (RadioButton3.Checked == true)
            {
                string dtxtpath = "config/detail_3.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("7号海洋水温海冰预报");
                dsw1.Dispose();
            }
            if (RadioButton4.Checked == true)
            {
                string dtxtpath = "config/detail_4.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("9号预报单");
                dsw1.Dispose();
            }
            if (RadioButton5.Checked == true)
            {
                string dtxtpath = "config/detail_7.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("10号预报单");
                dsw1.Dispose();
            }
            if (RadioButton6.Checked == true)
            {
                string dtxtpath = "config/detail_6.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("11号预报单");
                dsw1.Dispose();
            }
            if (RadioButton7.Checked == true)
            {
                string dtxtpath = "config/detail_8.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("12号预报单");
                dsw1.Dispose();
            }
            if (RadioButton8.Checked == true)
            {
                string dtxtpath = "config/detail_9.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("14号海上山东");
                dsw1.Dispose();
            }
            if (RadioButton9.Checked == true)
            {
                string dtxtpath = "config/detail_10.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("15号预报单");
                dsw1.Dispose();
            }
            if (RadioButton10.Checked == true)
            {
                string dtxtpath = "config/detail_11.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("20号潍坊市海洋预报台专项预报");
                dsw1.Dispose();
            }
            if (RadioButton11.Checked == true)
            {
                string dtxtpath = "config/detail_12.txt";
                FileInfo dfileinfo = new FileInfo(Server.MapPath(dtxtpath));
                System.IO.StreamWriter dsw1 = dfileinfo.CreateText();
                dsw1.Write("24号东营近海");
                dsw1.Dispose();
            }
        }
    }
}