using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
//using Microsoft.Office.Core;//COM选项卡中的"Microsoft Office 11.0 Object Library"
//using Word = Microsoft.Office.Interop.Word;//.NET选项卡中的"Microsoft.Office.Interop.Word"
using System.Collections;
using System.IO;

using System.Web.UI;
using System.Web.SessionState;
using PredicTable.ExportWord;
using PredicTable.Model;
using PredicTable.Commen;
using System.Diagnostics;
using PredicTable.Dal;
using PredicTable.ExportWord.OceanSilk;
using System.Data;

namespace PredicTable.Ajax
{
    /// <summary>
    /// releasetable 的摘要说明
    /// </summary>
    public class releasetable : TemplateControl, IHttpHandler,IRequiresSessionState
    {
        string userid;
        public DateTime dt;
        public string hourStr;
        string Message = "";
        SetWordName wordname = new SetWordName();
        WordName wordName = new WordName();
        string currentTemplateName = "";
        string makewordtime = "";
        public void ProcessRequest(HttpContext context)
        {
            KillProcess();
            context.Response.ContentType = "text/plain";
            string datas=context.Request.Form["datas"].ToString();
            dt = DateTime.Parse(context.Request.Form["dates"]);
            var hour = int.Parse(context.Request.Form["hours"]);
            makewordtime = context.Request.Form["makewordtime"];
            if (hour < 10)
                hourStr = "0" + hour;
            else
                hourStr = hour.ToString();
            context.Session["userid"] = "userid";//测试完毕删除
            if (context.Session["userid"] != null)
            {
                userid = context.Session["userid"].ToString();
                foreach (string data in datas.Split(','))
                {
                    currentTemplateName = data;
                    
                    switch (data)
                    {
                        case "1号山东省电视台预报单": OneWord(); break;
                        //case "2号明日海洋预报(a4)":; break;
                        case "3号黄河南海堤2006(a4)-3hao": ThreeWord(); break;
                        case "4号预报单(a4)-2014": FourWord(); break;
                        case "5号预报单（a4）": FiveWord(); break; 
                        case "6号预报单（a4）": SixWord(); break;
                        case "7号海洋水温海冰预报": SevenWord(); break;
                        case "9号预报单(a4)": NineWord(); break;
                        case "10号预报单(A4)-南堡": TenWord(); break;
                        case "11号预报单（a4）": ElevenWord(); break;
                        case "12号预报单(a4)": TwelveWord(); break;
                        case "13号预报": ThirteenWord(); break;//改为“13号预报”
                        case "14号海上山东（18a4）-gai14": FourteenWord(); break;
                        case "15号预报单（a4）": FifteenWord(); break;
                        //case "16号赤潮预报单(a4)":; break;
                        case "19号预报单--周（动态内容同月）":; break;
                        case "19号预报单--年（红笔为动态内容）":; break;
                        case "19号预报单--旬（动态内容同月）":; break;
                        case "19号预报单--月（红笔为动态内容）":; break;
                        case "20号潍坊市海洋预报台专项预报": TwentyWord(); break;
                        case "20号潍坊市海洋预报台专项预报(10时)": TwentyWordAM(); break;                            //20号潍坊市海洋预报台专项预报(10时)
                        case "21号青岛海水浴场预报-电视台播出": TwentyOneWord(); break;
                        case "22号海洋预报-电视台播出-非游泳季节": TwentyTwoWord(); break; //===
                        case "24号东营专项预报": TwentyfourWord(); break; //改为"24号东营专项预报"
                        case "25号预报单": TwentyfiveWord(); break;
                        case "26号预报单": TwentySixWord(); break;
                        case "上午的指挥处预报": ZhihuichuWord("07"); YZJWord("07");break;
                        case "下午的指挥处预报": ZhihuichuWord("16"); break;
                        case "农业部黄渤海区渔政局专项预报": YuzhengjuWord("16");break;
                        case "海上丝绸之路预报": SilkForcastWord();break;
                        case "东营埕岛油田海洋环境预报": DongYingOilWord(); break;
                        case "海阳近岸专项预报单": HaiYangCoastWord(); break;
                        case "1号72小时山东省电视台预报单": One72hWord(); break;
                        case "14号72小时预报单": Fourteen72hWord(); break;
                        case "东营广利渔港预报": DongYingGLWord(); break;
                        case "日照桃花岛预报": RiZhaoTHDWord(); break;
                        case "潍坊度假区预报": WeiFangDJQWord(); break;
                        case "威海新区预报": WeiHaiXQWord(); break;
                        case "烟台清泉预报": YanTaiQQWord(); break;
                        case "董家口港预报": DongJiaKouWord(); break;
                        case "东营渔港预报": DongYingYGWord(); break; 
                        case "上合峰会海洋环境预报单预览": PrivewSCOPingTaiWord(context); break;
                        //case "上合峰会海洋环境预报单": SCOWord(); SCOPingTaiWord(); break;
                        case "上合峰会海洋环境预报单": LvChaoSCOWord(); break;
                    }
                    KillProcess();//新添加的

                }
              
                context.Response.Write(Message);
                KillProcess();
            }
            else
            {
                //未登录
                context.Response.Write("-1");
            }
        }

        //结束winword进程
        private void KillProcess()
        {
            Process myProcess = new Process();
            Process[] wordProcess = Process.GetProcessesByName("winword");
            try
            {
                foreach (Process pro in wordProcess) //这里是找到那些没有界面的Word进程
                {
                    IntPtr ip = pro.MainWindowHandle;

                    string str = pro.MainWindowTitle; //发现程序中打开跟用户自己打开的区别就在这个属性
                                                      //用户打开的str 是文件的名称，程序中打开的就是空字符串
                    if (string.IsNullOrEmpty(str))
                    {
                        pro.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            
        }


        /// <summary>
        /// 2016文件保存路径最新需求：例2016/上午（下午）/文件（短期预报单文件或是警报文件）
        /// </summary>
        /// <returns></returns>
        private string FilePath()
        {
            string filepath = "";
            if (makewordtime == "am")
            {
                string daynumber = "";

                filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\duanqi\\" +dt.ToString("yyyyMMdd") + "\\上午";

                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
                }
            }

           else if (makewordtime == "pm")
            {
                filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\duanqi\\" + dt.ToString("yyyyMMdd")+ "\\下午";
                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
                }
            }

            return filepath;
        }

        /// <summary>
        /// 指挥处文件生成路径-2015-5-22
        /// </summary>
        /// <returns></returns>
        private string ZHCFilePath()
        {
            string filepath = "";
            
            if (makewordtime == "am")
            {
                filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\指挥处预报" + "\\上午";

                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
                }
            }

            else if (makewordtime == "pm")
            {
                filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\指挥处预报" + "\\下午";
                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
                }
            }
            return filepath;
        }

        /// <summary>
        /// 渔政局文件生成路径-2015-5-22
        /// </summary>
        /// <returns></returns>
        private string YZJFilePath()
        {
            string filepath = "";

            if (makewordtime == "am")
            {
                filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\渔政局预报" + "\\上午";

                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
                }
            }

            else if (makewordtime == "pm")
            {
                filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\渔政局预报" + "\\下午";
                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
                }
            }
            return filepath;
        }

        /// <summary>
        /// 生成1号预报单
        /// </summary>
        private string OneWord()
        {
            try
            {

                //  string strone = "1号黄河南海堤预报单";
                List<KJ_Wordname> list = wordName.wordname("1", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/1号山东省电视台预报单.doc");
                //生成的具有模板样式的新文件 
                string fileName = FilePath() +"\\"+ strone + ".doc";
               
                //删除word文件
                System.IO.File.Delete(fileName);
                //3号黄河南海堤预报单生成
                OneWord One = new OneWord();
                int flag0 = One.ExportWord(templateFile, fileName, dt);

                //判断数据库是不是有当前预报单
                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("1号预报单出错" + ex.ToString());
                Message = Message + "出错了！"+ex.ToString ();
            }
            return Message;
        }
        /// <summary>
        /// 北海区视频预报单
        /// </summary>
        /// <returns></returns>
        private string RadioWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("Radio", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/北海视频预报单.doc");
                //生成的具有模板样式的新文件 
                string fileName = FilePath() + "\\" + strone + ".doc";

                //删除word文件
                System.IO.File.Delete(fileName);
                //3号黄河南海堤预报单生成
                RadioWord radioWord = new RadioWord();
                int flag0 = radioWord.ExportWord(templateFile, fileName, dt);

                //判断数据库是不是有当前预报单
                int flag1 = flag0 == 0 ? 0 : ruku(fileName, strone, dt);//入库 ;
                if (flag0 == 0)
                {
                    //删除word文件
                    System.IO.File.Delete(fileName);
                    Message = Message + "  " + strone + " word生成失败! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("1号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        /// <summary>
        /// 生成1号72小时预报单
        /// </summary>
        private string One72hWord()
        { 
            try
            {

                //  string strone = "1号黄河南海堤预报单";
                List<KJ_Wordname> list = wordName.wordname("172h", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/1号山东省电视台预报单72h.docx");
                //生成的具有模板样式的新文件 
                string fileName = FilePath() + "\\" + strone + ".doc";

                //删除word文件
                System.IO.File.Delete(fileName);
                //3号黄河南海堤预报单生成
                One72hWord One72h = new One72hWord();
                int flag0 = One72h.ExportWord(templateFile, fileName, dt);

                //判断数据库是不是有当前预报单
                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("1号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成3号预报单
        /// </summary>
        private string ThreeWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("3", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/3号黄河南海堤预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //3号黄河南海堤预报单生成
                ThreeWord three = new ThreeWord();
                int flag0=three.ExportWord(templateFile, fileName, dt);

                //判断数据库是不是有当前预报单
                int flag1=ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                  Message=Message+"  " + strone + " word生成失败! <br/>";
                  Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                 Message = Message+"  "+ strone+" word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message +"  "+ strone + " word生成成功! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
               
            }
            catch (Exception ex) {
                WriteLog.Write("3号预报单出错"+ ex.ToString());
                Message = Message + "出错了！"+ ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成4号预报单
        /// </summary>
        private string  FourWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("4", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/4号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                    //4号预报单
                    FourWord Four = new FourWord();
                    int flag0=Four.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
                            
                    int flag1=ruku(fileName, strone, dt); //入库    
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);

                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }              
            catch (Exception ex)
            {
                WriteLog.Write("4号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成5号预报单
        /// </summary>
        private string FiveWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("5", dt);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/5号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //5号预报单
                FiveWord Five = new FiveWord();
                int flag0 = Five.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt); //入库    
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("5号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }


        /// <summary>
        /// 生成6号预报单
        /// </summary>
        private string SixWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("6", dt);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/6号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //6号预报单
                SixWord Six = new SixWord();
                int flag0 = Six.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt); //入库    
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>";
                    Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("6号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        /// <summary>
        /// 生成7号预报单
        /// </summary>
        private string  SevenWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("7", dt);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/7号海洋水温海冰预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);

                    //7号
                    SevenWord seven = new SevenWord();
                  int flag0=seven.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
                
                  int flag1=  ruku(fileName, strone, dt);//入库 
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
                
            catch (Exception ex)
            {
                WriteLog.Write("7号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成10号预报单
        /// </summary>
        private string  TenWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("10", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/10号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                    //10号黄河南海堤预报单生成
                    TenWord ten = new TenWord();
                    int flag0=ten.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
                    int flag1=ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("10号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成9号预报单 下午四数据取消，去取下午三青岛24 
        /// edit by Yuy 20180717
        /// </summary>
        private string NineWord()
        {
            try
            {
                //9号预报单
                List<KJ_Wordname> list = wordName.wordname("9", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/9号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                    //9号预报单
                    NineWord nine = new NineWord();
                    int flag0=nine.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
                    
                    int flag1= ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("9号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();

            }
            return Message;
        }

        /// <summary>
        /// 生成11号预报单
        /// </summary>
        private string ElevenWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("11", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/11号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                    //11号预报单
                    ElevenWord eleven = new ElevenWord();
                  int flag0=  eleven.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
               
                  int flag1=  ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("11号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成12号预报单 下午四青岛24潮汐预报取消，去下午三七地市青岛24取数 
        /// edit by Yuy 20180717
        /// </summary>
        private string TwelveWord()
        {
            try
            {

                List<KJ_Wordname> list = wordName.wordname("12", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/12号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);

                    //11号预报单
                    TwelveWord twelve = new TwelveWord();
                  int flag0=  twelve.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
                  

                  int flag1=  ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("12号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成13号预报单 用到下午四潮汐，取数改成从下午三青岛取 
        /// edit by Yuy 180717
        /// </summary>
        private string ThirteenWord()
        {
            try
            {

                // string strone = "13号预报单";
                List<KJ_Wordname> list = wordName.wordname("13", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/13号预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);

                //13号预报单
                ThirteenWord twelve = new ThirteenWord();
                int flag0 = twelve.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单


                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("13号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成14号海上山东
        /// </summary>
        private string  FourteenWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("14", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/14号海上山东.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                    //11号预报单
                    FourteenWord fourteen = new FourteenWord();
                  int flag0=  fourteen.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
                  
                  int flag1=  ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成14号72小时预保单
        /// </summary>
        private string Fourteen72hWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("1472h", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/14号海上山东72h.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //11号预报单
                Fourteen72hWord fourteen72h = new Fourteen72hWord();
                int flag0 = fourteen72h.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 山东七地市精细化预报文件生成路径
        /// </summary>
        /// <returns></returns>
        private string SDFilePath()
        {
            string filepath = "";
            filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\精细化预报\\" + dt.ToString("yyyyMMdd");
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
            }
            return filepath;
        }
        /// <summary>
        /// 东营广利渔港
        /// </summary>
        /// <returns></returns>
        private string DongYingGLWord()
        {
            try
            {
                string strone = "YB_DYGLFP_JX_" + dt.ToString("yyyyMMdd") + hourStr + "_SDMF";
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/东营广利一级渔港预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = SDFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //11号预报单
                SDSevenWord sdSevenWord = new SDSevenWord();
                int flag0 = sdSevenWord.ExportWord(templateFile, fileName, dt, "DYGLFP", "DYGLFP", "DYGLFP");
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 日照桃花岛
        /// </summary>
        /// <returns></returns>
        private string RiZhaoTHDWord()
        {
            try
            {
                string strone = "YB_RZTHD_JX_" + dt.ToString("yyyyMMdd") + hourStr + "_SDMF";
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/日照桃花岛预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = SDFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //11号预报单
                SDSevenWord sdSevenWord = new SDSevenWord();
                int flag0 = sdSevenWord.ExportWord(templateFile, fileName, dt, "RZTHD", "RZTHD", "RZTHD");
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 潍坊度假区
        /// </summary>
        /// <returns></returns>
        private string WeiFangDJQWord()
        {
            try
            {
                string strone = "YB_WFDJQ_JX_" + dt.ToString("yyyyMMdd") + hourStr + "_SDMF";
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/潍坊旅游度假区预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = SDFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //11号预报单
                SDSevenWord sdSevenWord = new SDSevenWord();
                int flag0 = sdSevenWord.ExportWord(templateFile, fileName, dt, "WFDJQ", "WFDJQ", "WFDJQ");
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 威海新区
        /// </summary>
        /// <returns></returns>
        private string WeiHaiXQWord()
        {
            try
            {
                string strone = "YB_WHXQ_JX_" + dt.ToString("yyyyMMdd") + hourStr + "_SDMF";
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/威海南海新区预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = SDFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //11号预报单
                SDSevenWord sdSevenWord = new SDSevenWord();
                int flag0 = sdSevenWord.ExportWord(templateFile, fileName, dt, "WHXQ", "WHXQ", "WHXQ");
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }


        /// <summary>
        /// 烟台清泉
        /// </summary>
        /// <returns></returns>
        private string YanTaiQQWord()
        {
            try
            {
                string strone = "YB_YTQQ_JX_" + dt.ToString("yyyyMMdd") + hourStr + "_SDMF";
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/烟台清泉码头预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = SDFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //11号预报单
                SDSevenWord sdSevenWord = new SDSevenWord();
                int flag0 = sdSevenWord.ExportWord(templateFile, fileName, dt, "YTQQ", "YTQQ", "YTQQ");
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 董家口预报单
        /// </summary>
        /// <returns></returns>
        private string DongJiaKouWord()
        {
            try
            {
                string strone = "YB_DJKP_JX_" + dt.ToString("yyyyMMdd") + hourStr + "_SDMF";
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/董家口港预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = SDFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //11号预报单
                SDSevenWord sdSevenWord = new SDSevenWord();
                int flag0 = sdSevenWord.ExportWord(templateFile, fileName, dt, "DJKP", "DJKP", "DJKP");
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 东营渔港
        /// </summary>
        /// <returns></returns>
        private string DongYingYGWord()
        {
            try
            {
                string strone = "YB_DYFP_JX_" + dt.ToString("yyyyMMdd") + hourStr + "_SDMF";
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/东营渔港预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = SDFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //11号预报单
                SDSevenWord sdSevenWord = new SDSevenWord();
                int flag0 = sdSevenWord.ExportWord(templateFile, fileName, dt, "DYFP", "DYFP", "DYFP");
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("14号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成15号预报单
        /// </summary>
        private string  FifteenWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("15", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/15号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                    //11号预报单
                    FifteenWord fifteen = new FifteenWord();
                   int flag0= fifteen.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
                    

                   int flag1= ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("15号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成20号潍坊市海洋预报台专项预报PM
        /// edit by Yuy 180718 下午十二预报单取消，从下午三中去潍坊24小时预报数据
        /// </summary>
        private string TwentyWord()
        {
            try
            {
                //20号预报单
                List<KJ_Wordname> list = wordName.wordname("20", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/20号潍坊市海洋预报台专项预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                    //20号预报单
                    TwentyWord twenty = new TwentyWord();
                 int flag0=   twenty.ExportWord(templateFile, fileName,  dt);
                    //判断数据库是不是有当前预报单
                    
                 int flag1=   ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("20号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成20号潍坊市海洋预报台专项预报(上午)
        /// edit by Yuy 180719 周报下午十一预报单取消，从下午五中去潍坊24小时预报数据
        /// </summary>
        private string TwentyWordAM()
        {
            try
            {
                //20号预报单
                List<KJ_Wordname> list = wordName.wordname("20", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/20号潍坊市海洋预报台专项预报.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //20号预报单
                TwentyWordAM twenty = new TwentyWordAM();
                int flag0 = twenty.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("20号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        /// <summary>
        /// 生成21号预报单 下午十四金沙滩潮汐预报取消，取下午三青岛三天预报
        /// edit by Yuy 1080718
        /// </summary>
        private string TwentyOneWord()
        {
            try
            {

                //string strone = "22号预报单";
                List<KJ_Wordname> list = wordName.wordname("21", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/21号青岛海水浴场预报-电视台播出.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);

                //21号预报单
                TwentyOneWord twelveTwoWord = new TwentyOneWord();
                int flag0 = twelveTwoWord.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单


                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("21号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
    
        /// <summary>
        /// 生成22号预报单
        /// </summary>
        private string TwentyTwoWord()
        {
            try
            {
                // string strone = "22号预报单";
                List<KJ_Wordname> list = wordName.wordname("22", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/22号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);

                //22号预报单
                TwentyTwoWord twelveTwoWord = new TwentyTwoWord();
                int flag0 = twelveTwoWord.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单


                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("22号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成24号东营近海
        /// </summary>
        private string TwentyfourWord()
        {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("24", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/24号东营近海.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                    //24号东营近海
                    TwentyfourWord twentyfour = new TwentyfourWord();
                  int flag0=  twentyfour.ExportWord(templateFile, fileName,dt);
                    //判断数据库是不是有当前预报单
                   
                  int flag1=  ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + " word生成失败! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成失败!");
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + " word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + " word生成成功! <br/>"; Sql_Caozuorizhi.WriteRizhi(userid, "release_table", strone + " word生成成功!");
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + " word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("24号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成25号 下午十八威海市区潮汐预报取消，从下午三威海去数据
        /// edit by Yuy 20180719
        /// </summary>
        private string TwentyfiveWord()
        {
            try
            {
                //  string strone = "25号东营近海";
                List<KJ_Wordname> list = wordName.wordname("25", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/25号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //24号东营近海
                TwentyfiveWord twentyfive = new TwentyfiveWord();
                int flag0 = twentyfive.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("25号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 生成26号表单
        /// </summary>
        /// <returns></returns>
        private string TwentySixWord() {
            try
            {
                List<KJ_Wordname> list = wordName.wordname("26", dt, hourStr);
                string strone = wordname.setWordName(list);
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/26号预报单.doc");
                //生成的具有模板样式的新文件           
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //26号东营近海
                TwentySixWord twentyfive = new TwentySixWord();
                int flag0 = twentyfive.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("26号预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        /// <summary>
        /// 生成指挥处预报单
        /// </summary>
        private string ZhihuichuWord(string publishTime)
        {
            try
            {
                //模板文件
                string templateFile = templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/指挥处预报单.doc"); 
                    //  string strone = "指挥处";
                    //  List<KJ_Wordname> list = wordName.wordname("zhihuichu"+ publishTime, dt, "7");

                    //string strone = wordname.setWordName(list);
                string strone = "YB_ZHC_ZX_72hr_" + dt.ToString("yyyyMMdd") + publishTime + "_NMFC";
                
                //if (publishTime =="07")
                //    templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/指挥处上午预报单" + ".doc");
                //else if(publishTime == "16")
                //    templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/指挥处下午预报单" + ".doc");

                //生成的具有模板样式的新文件           
                string fileName = ZHCFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //指挥处
                ZhhchWord zhhchWord = new ZhhchWord();
                int flag0 = zhhchWord.ExportWord(templateFile, fileName, dt,publishTime);
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("指挥处预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 上午指挥处附带渔政局预报单
        /// </summary>
        /// <returns></returns>
        private string YZJWord(string publishTime)
        {
            try
            {
                //模板文件
                string templateFile = templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/农业部黄渤海区渔政局专项预报.doc");
                //  string strone = "指挥处";
                //  List<KJ_Wordname> list = wordName.wordname("zhihuichu"+ publishTime, dt, "7");

                //string strone = wordname.setWordName(list);
                string strone = "YB_YZJ_ZX_72hr_" + dt.ToString("yyyyMMdd") + publishTime +"_NMFC";

                //if (publishTime =="07")
                //    templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/指挥处上午预报单" + ".doc");
                //else if(publishTime == "16")
                //    templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/指挥处下午预报单" + ".doc");

                //生成的具有模板样式的新文件           
                string fileName = YZJFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                //指挥处
                YuzhengjuMorning yuzhengjuM = new YuzhengjuMorning();
                int flag0 = yuzhengjuM.ExportWord(templateFile, fileName, dt, "07");
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("指挥处预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }


        /// <summary>
        /// 渔政局专项（27）上半部分数据和指挥处下午上半部分数据相同
        /// </summary>
        /// <param name="publishTime"></param>
        /// <returns></returns>
        private string YuzhengjuWord(string publishTime)
        {
            try
            {
               // List<KJ_Wordname> list = wordName.wordname("yuzhengju" + publishTime, dt, hourStr);
               // string strone = wordname.setWordName(list);

                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/农业部黄渤海区渔政局专项预报.doc");
                //生成的具有模板样式的新文件   
                string strone = "YB_YZJ_ZX_72hr_" + dt.ToString("yyyyMMdd")+ publishTime+ "_NMFC";        
                string fileName = YZJFilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                // 渔政局专项
                YuzhengjuWord yuzhengju = new YuzhengjuWord();
                int flag0 = yuzhengju.ExportWord(templateFile, fileName, dt, publishTime);
                //判断数据库是不是有当前预报单

                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("指挥处预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 海上丝绸之路预报单生成
        /// </summary>
        private void SilkForcastWord()
        {
            try
            {
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/海上丝绸之路.doc");
                //生成的具有模板样式的新文件           
                string newFilePath = System.Web.HttpContext.Current.Server.MapPath("/预报单共享/海上丝绸之路/" + dt.ToString("yyyyMMdd"));
                if (!Directory.Exists(newFilePath))
                {
                    Directory.CreateDirectory(newFilePath);
                }
                string fileName = newFilePath + "/YB_SS_ZX_72hr_" + dt.ToString("yyyyMMdd") + "_NMFC.doc";
                //删除word文件
                System.IO.File.Delete(fileName);

                OceanSilkWord oceanSilkWord = new OceanSilkWord();
                CreateXML.CreateSilkXML(dt);
                int flag0 = oceanSilkWord.ExportWord(templateFile, fileName, dt);
                if (flag0 == 1)
                {
                    Message = Message + "  "+"丝绸之路预报单生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                else
                {
                    Message = Message +"  " + "丝绸之路预报单生成失败! <br/>";
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 生成东营埕岛油田海洋环境预报
        /// </summary>
        /// <returns></returns>
        private string DongYingOilWord()
        {
            try
            {
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/东营埕岛油田海洋环境预报.doc");
                //生成的具有模板样式的新文件           
                //string strone = "YB_DY_FPORT_HJ_" + dt.ToString("yyyyMMdd") + "16";
                string strone = "YB_DY_CDOF_72hr_" + dt.ToString("yyyyMMdd") + "16";
                string fileName = FilePath() + "\\" + strone+ ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                DongYingOilWord dyOilWord = new DongYingOilWord();
                int flag0 = dyOilWord.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单
                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("东营埕岛油田预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }

        /// <summary>
        /// 海阳近岸专项预报单
        /// </summary>
        /// <returns></returns>
        private string HaiYangCoastWord()
        {
            try
            {
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/海阳近岸专项预报单.doc");
                //生成的具有模板样式的新文件           
                string strone = "YB_HY_ZX_24hr_" + dt.ToString("yyyyMMdd") + "10_NMFC";
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除word文件
                System.IO.File.Delete(fileName);
                HaiYangCoastWord hyCoastWord = new HaiYangCoastWord();
                int flag0 = hyCoastWord.ExportWord(templateFile, fileName, dt);
                //判断数据库是不是有当前预报单
                int flag1 = ruku(fileName, strone, dt);//入库  
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("海阳近岸专项预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        /// <summary> 
        /// 上合峰会专项海洋环境预报
        /// </summary>  
        /// <returns></returns>
        private string SCOWord() {
            try
            {
                //专项预报单模版文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/上合峰会专项海洋环境预报单.doc");
                //专项预报单生成文件名  YB_SHANGHE_HJ_72hr_2018060215_NMFC.doc
                string strone = "YB_SHANGHE_HJ_72hr_" + dt.ToString("yyyyMMdd") + "15_NMFC";
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除专项预报单word文件
                System.IO.File.Delete(fileName);
                PredicTable.ExportWord.SCOWord SH_word = new ExportWord.SCOWord();
                //专项预报单
                int flag0 = SH_word.ExportWord(templateFile, fileName, dt, hourStr);
                int flag1 = ruku(fileName, strone, dt);//入库 
               
                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    //Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }

               
            }
            catch (Exception ex)
            {
                WriteLog.Write("上合峰会专项预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        /// <summary>
        /// 上合峰会平台海洋环境预报
        /// </summary>
        /// <returns></returns>
        private string SCOPingTaiWord()
        {
            try
            {
                //平台预报单模版文件
                string PingTaiFile = HttpContext.Current.Server.MapPath("/word/上合峰会平台海洋环境预报单.doc");
                //平台预报单生成文件名  YB_PINGTAI_HJ_72hr_2018051415_NMFC.doc
                string PingTaiStrone = "YB_PINGTAI_HJ_72hr_" + dt.ToString("yyyyMMdd") + "15_NMFC";
                string PingTaiFileName = FilePath() + "\\" + PingTaiStrone + ".doc";
                File.Delete(PingTaiFileName);

                PredicTable.ExportWord.SCOWord SH_word = new ExportWord.SCOWord();
                //平台预报单
                int flag2 = SH_word.ExportWord1(PingTaiFile, PingTaiFileName, dt, hourStr);
                int flag3 = ruku(PingTaiFileName, PingTaiStrone, dt);//入库 
                if (flag2 == 0)
                {
                    Message = Message + "  " + PingTaiStrone + "平台word生成失败! <br/>";
                }
                if (flag3 == 0)
                {
                    Message = Message + "  " + PingTaiStrone + "平台word插入数据库失败! <br/>";
                }
                if (flag2 == 1)
                {
                    Message = Message + "  " + PingTaiStrone + "平台word生成成功! <br/>";
                    //Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag3 == 1)
                {
                    Message = Message + "  " + PingTaiStrone + "平台word插入数据库成功! <br/>";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("上合峰会平台预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        private string LvChaoSCOWord()
        {
            try
            {
                sql_SCOTableList sql_Sh = new sql_SCOTableList();
                DataTable tbl0 = (DataTable)sql_Sh.GetTableSumAndPeriod(dt);//取出期数和综述
                string model = "/word/黄海绿潮专项海洋环境预报.doc";
                if (tbl0 != null && tbl0.Rows.Count > 0)
                {
                    string Summarize = tbl0.Rows[0]["USUMMARIZE"].ToString();
                    if (!string.IsNullOrEmpty(Summarize))
                    {
                        int len = GetLengthOfStr.StrLength(Summarize);
                        model = (len > 56) ? "/word/黄海绿潮专项海洋环境预报.doc" : "/word/黄海绿潮专项海洋环境预报2.doc";
                    }
                }
                //专项预报单模版文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath(model);
                //专项预报单生成文件名  YB_SHANGHE_HJ_72hr_2018060215_NMFC.doc
                string strone = "YB_SHANGHE_HJ_72hr_" + dt.ToString("yyyyMMdd") + "15_NMFC";
                string fileName = FilePath() + "\\" + strone + ".doc";
                //删除专项预报单word文件
                System.IO.File.Delete(fileName);
                PredicTable.ExportWord.SCOWord SH_word = new ExportWord.SCOWord();
                //专项预报单
                int flag0 = SH_word.LvChaoExportWord(templateFile, fileName, dt, hourStr);
                int flag1 = ruku(fileName, strone, dt);//入库 

                if (flag0 == 0)
                {
                    Message = Message + "  " + strone + "word生成失败! <br/>";
                }
                if (flag1 == 0)
                {
                    Message = Message + "  " + strone + "word插入数据库失败! <br/>";
                }
                if (flag0 == 1)
                {
                    Message = Message + "  " + strone + "word生成成功! <br/>";
                    //Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
                if (flag1 == 1)
                {
                    Message = Message + "  " + strone + "word插入数据库成功! <br/>";
                }


            }
            catch (Exception ex)
            {
                WriteLog.Write("黄海绿潮专项预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        /// <summary>
        /// 黄海绿潮海洋环境预报预览
        /// </summary>
        /// <returns></returns>
        private string PrivewSCOPingTaiWord(HttpContext context)
        {
            try
            {
                int flag;
                string PingTaiFile, PingTaiStrone;
                string Id = context.Request.Form["Id"].ToString();
                
                if (Id != "1")
                {
                    //PingTaiFile = HttpContext.Current.Server.MapPath("/word/上合峰会平台海洋环境预报单.doc");
                    //PingTaiStrone = "YB_PINGTAI_HJ_72hr_" + dt.ToString("yyyyMMdd") + "15_NMFC";
                    PingTaiFile = ""; PingTaiStrone = "";
                }
                else
                {
                    //获取上合综述
                    sql_SCOTableList sql_Sh = new sql_SCOTableList();
                    DataTable tbl0 = (DataTable)sql_Sh.GetTableSumAndPeriod(dt);//取出期数和综述
                    string model = "/word/黄海绿潮专项海洋环境预报.doc";
                    if (tbl0 != null && tbl0.Rows.Count > 0)
                    {
                        string Summarize = tbl0.Rows[0]["USUMMARIZE"].ToString();
                        if (!string.IsNullOrEmpty(Summarize))
                        {
                            int len = GetLengthOfStr.StrLength(Summarize);
                            model = (len > 56) ? "/word/黄海绿潮专项海洋环境预报.doc" : "/word/黄海绿潮专项海洋环境预报2.doc";
                        }
                    }
                    PingTaiFile = HttpContext.Current.Server.MapPath(model);
                    PingTaiStrone = "YB_SHANGHE_HJ_72hr_" + dt.ToString("yyyyMMdd") + "15_NMFC";
                }
                string FilePath = PriviewFilePath() + "\\" ;
                string PingTaiFileName = FilePath + PingTaiStrone + ".doc";
                string PriviewFileName = FilePath + PingTaiStrone + ".pdf";
                File.Delete(PingTaiFileName);
                PredicTable.ExportWord.SCOWord SH_word = new ExportWord.SCOWord();
                PredicTable.ExportWord.JingBao.Word word = new ExportWord.JingBao.Word();//word转pdf

                if (Id != "1")
                {
                    // flag = SH_word.ExportWord1(PingTaiFile, PingTaiFileName, dt, hourStr); //平台预报单
                    flag = 0;
                }
                else
                {
                    flag = SH_word.LvChaoExportWord(PingTaiFile, PingTaiFileName, dt, hourStr); //绿潮专项预报单
                }
                if (flag == 0)
                {
                    Message = Message + "  " + PingTaiStrone + "平台word预览失败! <br/>";
                }
                if (flag == 1)//生成word成功，
                {
                    FileInfo file = new FileInfo(PingTaiFileName);
                    //将word转pdf
                    //if (SH_word.WordToPdf(PingTaiFileName, PriviewFileName))
                    //{
                    //    Message = "PDFSuccess";
                    //}
                    word.WordToPDF(file, FilePath, "");
                   if( File.Exists(PriviewFileName))
                    {
                        Message = "PDFSuccess";
                    }
                    // Message = Message + "  " + PingTaiStrone + "平台word生成成功! <br/>";
                    //Opt_PublishedTemplates.addPublishedTemplete(currentTemplateName);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("绿潮预览预报单出错" + ex.ToString());
                Message = Message + "出错了！" + ex.ToString();
            }
            return Message;
        }
        private string PriviewFilePath()
        {
            string filepath = "";
            filepath = AppDomain.CurrentDomain.BaseDirectory + "预览文件" ;

            if (System.IO.Directory.Exists(filepath))
            {
                DeleteFolder(filepath);
                
            }
                System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
            
            return filepath;
        }
        public void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件 
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir); //删除已空文件夹 
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
        private int ruku(string fileName, string strone, DateTime dt)
        {
            int a1 = 0;
            TBLYBDDOCUMENT tbl = new TBLYBDDOCUMENT();
            tbl.UPLOADDATE = dt;
            tbl.YBDNAME = strone+".doc";
           
            List<int> a = new List<int>();
            int i1 = 0;
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new sql_TBLYBDDOCUMENT().get_TBLYBDDOCUMENT_AllData(tbl);
            if (tblybddocument.Rows.Count > 0)
            {
                //edit
                UpdateWord updateword = new UpdateWord();
                a1= updateword.updateword(fileName, dt,userid);
            }
            else
            { 
             //存入数据库
                 InsertWord insertword = new InsertWord();
                 a1=insertword.saveword(fileName, dt,userid);
            }
            return a1;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}