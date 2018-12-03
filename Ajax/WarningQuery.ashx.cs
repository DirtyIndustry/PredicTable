using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.ExportWord;
using PredicTable.ExportWord.JingBao;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.SessionState;

namespace PredicTable.Ajax
{ 
       /// <summary>
      ///功能说明： 警报
      ///创建时间：2016.11.09
      ///创建人员：韩萌真
      /// </summary>
    public class WarningQuery : IHttpHandler, IRequiresSessionState
    {

        SetWordName wordname = new SetWordName();
        WordName wordName = new WordName();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string method = context.Request["method"].ToString();

                switch (method)
                {

                    case "getContents": getContents(context); break;
                    case "getGroup": getGroup(context); break;
                    case "AddJingBao": AddJingBao(context); break;
                    case "AddXiaoXi": AddXiaoXi(context); break;
                    case "AddJieChuJingBao": AddJieChuJingBao(context); break;
                    case "getXinXi":getXinXi(context);break;
                    case "getJingBao": getJingBao(context); break;
                    case "getJCJingBao": getJCJingBao(context); break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("操作表单数据出错" + ex.ToString());
            }

        }

        //返回联系人
        void getContents(HttpContext context)
        {
            var dataTable = new DataTable();
            var sqlCon = new Sql_HT_CONTENTS();
            dataTable = sqlCon.GetContentsData();
            var dataJson = JsonMore.Serialize(dataTable);
            context.Response.ContentType = "application/json";
            context.Response.Write(dataJson);
        }

        //返回发往组
        void getGroup(HttpContext context)
        {
            string FAXGROUP = context.Request.Form["FAXGROUP"]; ;
            var dataTable = new DataTable();
             var sqlCon = new Sql_HT_CONTENTS();
            dataTable = sqlCon.GetGroupData(FAXGROUP);
            var dataJson = JsonMore.Serialize(dataTable);
            context.Response.ContentType = "application/json";
            context.Response.Write(dataJson);
        }

        //警报保存
        void AddJingBao(HttpContext context)
        {
            string returnStr = "";

            #region 警报内容保存
            
            CG_HT_JINGBAO_CONTENT Contentvalue = new CG_HT_JINGBAO_CONTENT();
            //警报文件内容
            Contentvalue.JBWENJIANMING = context.Request.Form["JBWENJIANMING"];//文件名称
            Contentvalue.CONTENT = context.Request.Form["CONTENT"];//内容
            Contentvalue.SENTTO = context.Request.Form["SENTTO"];//发往
            Contentvalue.LINKMAN = context.Request.Form["LINKMAN"];//联系人
            Contentvalue.CONTENTTABLE = context.Request.Form["CONTENT"];//表格
            Contentvalue.JBTITLE = context.Request.Form["DOCTITLE"];
            Contentvalue.DATETIME=Convert.ToDateTime(context.Request.Form["Time"]+" "+ context.Request.Form["TM"]+":00:00");

            //获取签发图片地址
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/" + context.Request.Form["ISSUEPICTURE"]);
            byte[] byData;
            //根据图片文件的路径使用文件流打开，并保存为byte[] 
            if (System.IO.File.Exists(imagepath))
            {
                FileStream fs = new FileStream(imagepath, FileMode.Open);
                byData = new byte[fs.Length];
                fs.Read(byData, 0, byData.Length);
                fs.Close();
                Contentvalue.ISSUEPICTURE = byData;
                Contentvalue.PICTURE = byData;//图片
            }
            else
            {
                byData = new byte[0];
                context.Response.Write("图片已丢失。");
                Contentvalue.ISSUEPICTURE = byData;
                Contentvalue.PICTURE = byData;//图片
            }

            //警报内容表数据保存
            var sqlConC = new Sql_HT_CONTENTS();
            CG_HT_JINGBAO_CONTENT tblC = new Model.CG_HT_JINGBAO_CONTENT();
            tblC.JBWENJIANMING = context.Request.Form["JBWENJIANMING"];
            int num = -1;
            //判断数据库是不是有数据
            System.Data.DataTable tblybddocumentC = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JingBaoCON_AllData(tblC);
            if (tblybddocumentC.Rows.Count > 0)
            {
                num = sqlConC.UpdateJingBaoContent(Contentvalue);
            }
            else
            {
                num = sqlConC.AddJingBaoContent(Contentvalue);
            }
            if (num <= 0)
            {
                returnStr += "警报文件内容提交失败。 ";

            }
            else
            {
                returnStr += "警报文件内容提交成功。 ";

            }
            //发往保存
            CommonSendUnit sunit = new CommonSendUnit(context.Request.Form["JBWENJIANMING"], context.Request.Form["SENTTO"]);
            returnStr += sunit.resultSendUnit();
            //word 文档参数
            List<string> param = new List<string>();
            param.Insert(0, context.Request.Form["Types"]);
            param.Insert(1, context.Request.Form["times"]);
            param.Insert(2, context.Request.Form["TM"]);
            param.Insert(3, imagepath);

            #endregion

            #region 警报文件属性
            
            CG_JINGBAO_ME JingBaomI = new CG_JINGBAO_ME();
            JingBaomI.JBWENJIANMING = context.Request.Form["JBWENJIANMING"];
            JingBaomI.JBQUYU = context.Request.Form["JBQUYU"];
            JingBaomI.JBNEIRONG = context.Request.Form["JBNEIRONG"];
            JingBaomI.JBBIANHAO = context.Request.Form["bianhao"];//保存到数据库中的编号
            JingBaomI.JBJIBIE = context.Request.Form["JBJIBIE"];
            JingBaomI.JBSHIJIAN = Convert.ToDateTime(context.Request.Form["JBSHIJIAN"]);
            JingBaomI.JBDANWEI = context.Request.Form["JBDANWEI"];
          

            //警报文件属性数据保存
            var sqlConME = new Sql_HT_CONTENTS();
            CG_JINGBAO_ME tblME = new Model.CG_JINGBAO_ME();
            tblME.JBWENJIANMING = context.Request.Form["JBWENJIANMING"];
            int numMe = -1;
            //判断数据库是不是有数据
            System.Data.DataTable tblybddocumentME = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JingBaoME_AllData(tblME);
            if (tblybddocumentME.Rows.Count > 0)
            {
                numMe = sqlConME.UpdateJingBaoMe(JingBaomI);
            }
            else
            {
                numMe = sqlConME.AddJingBaoMe(JingBaomI);
            }
            if (numMe <= 0)
            {
                returnStr += " 警报文件属性数据提交失败。 ";
            }
            else
            {
                returnStr += " 警报文件属性数据提交成功。 ";
            }
            #endregion

            #region 警报文件
            try
            {
                //文件名称
                string strone = context.Request.Form["JBWENJIANMING"];
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/JingBao/JB.doc");
                //生成的具有模板样式的新文件

                string filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\duanqi\\" + context.Request.Form["XXTime"];

                string fileName =strone;

                JingBaomI.JBBIANHAO = context.Request.Form["JBBIANHAO"];//保存到文档中的编号

                //警报Word 文档插入数据 生成
                JingBao jc = new JingBao();
                int flag0 = jc.ExportWord(templateFile, filepath, fileName, Contentvalue, JingBaomI, param);

                //警报文件保存到数据库
                int flag1 = JBruku(filepath+"\\"+fileName, filepath, strone);

            
                if (flag0 == 0)
                {
                    returnStr = returnStr + "  " + strone + " word生成失败!";
                }
                if (flag1 == 0)
                {
                    returnStr = returnStr + "  " + strone + " word插入数据库失败!";
                }
                if (flag0 == 1)
                {
                    returnStr = returnStr + "  " + strone + " word生成成功!";
                }
                if (flag1 == 1)
                {
                    returnStr = returnStr + "  " + strone + " word插入数据库成功!";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("1号预报单出错" + ex.ToString());
                returnStr = returnStr + "出错了！" + ex.ToString();
            }
            context.Response.Write(returnStr); 
            
            #endregion            
        }

        //消息保存
        void AddXiaoXi(HttpContext context)
        {
            string returnStr = "";

            #region 消息内容保存

            CG_HT_XIAOXI_CONTENT Contentvalue = new CG_HT_XIAOXI_CONTENT();
            //消息文件内容
            Contentvalue.XXWENJIANMING = context.Request.Form["JBWENJIANMING"];//文件名称
            Contentvalue.CONTENT = context.Request.Form["CONTENT"];//内容
            Contentvalue.SENTTO = context.Request.Form["SENTTO"];//发往
            Contentvalue.LINKMAN = context.Request.Form["LINKMAN"];//联系人
            Contentvalue.XXTITLE = context.Request.Form["DOCTITLE"];//文档标题
            Contentvalue.DATETIME = Convert.ToDateTime(context.Request.Form["Time"] + " " + context.Request.Form["TM"] + ":00:00");
            //获取签发图片地址
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/" + context.Request.Form["ISSUEPICTURE"]);
            byte[] byData;
            //根据图片文件的路径使用文件流打开，并保存为byte[] 
            if (System.IO.File.Exists(imagepath))
            {
                FileStream fs = new FileStream(imagepath, FileMode.Open);
                byData = new byte[fs.Length];
                fs.Read(byData, 0, byData.Length);
                fs.Close();
                Contentvalue.ISSUEPICTURE = byData;
            }
            else
            {
                byData = new byte[0];
                context.Response.Write("图片已丢失。");
                Contentvalue.ISSUEPICTURE = byData;
            }

            //消息内容表数据保存

            var sqlConC = new Sql_HT_CONTENTS();
            CG_HT_XIAOXI_CONTENT tblC = new Model.CG_HT_XIAOXI_CONTENT();
            tblC.XXWENJIANMING = context.Request.Form["JBWENJIANMING"];
            int num = -1;
            //判断数据库是不是有当前数据
            System.Data.DataTable tblybddocumentC = (System.Data.DataTable)new Sql_HT_CONTENTS().get_XiaoXiCON_AllData(tblC);
            if (tblybddocumentC.Rows.Count > 0)
            {
                num = sqlConC.UpdateXiaoXiContent(Contentvalue);
            }
            else
            {
                num = sqlConC.AddXiaoXiContent(Contentvalue);
            }
            
            if (num <= 0)
            {
                returnStr += "消息文件内容提交失败。 ";
            }
            else
            {
                returnStr += "消息文件内容提交成功。 ";
            }
            //发往保存
            CommonSendUnit sunit = new CommonSendUnit(context.Request.Form["JBWENJIANMING"], context.Request.Form["SENTTO"]);
            returnStr += sunit.resultSendUnit();
            //word 文档参数
            List<string> param = new List<string>();
            param.Insert(0, context.Request.Form["Types"]);
            param.Insert(1, context.Request.Form["times"]);
            param.Insert(2, context.Request.Form["TM"]);
            param.Insert(3, imagepath);

            #endregion

            #region 消息文件属性

            CG_XIAOXI_ME JingBaomI = new CG_XIAOXI_ME();
            JingBaomI.XXWENJIANMING = context.Request.Form["JBWENJIANMING"];
            JingBaomI.XXQUYU = context.Request.Form["JBQUYU"];
            JingBaomI.XXNEIRONG = context.Request.Form["JBNEIRONG"];
            JingBaomI.XXBIANHAO ="";
            JingBaomI.XXSHIJIAN = Convert.ToDateTime(context.Request.Form["JBSHIJIAN"]);
            JingBaomI.XXDANWEI = context.Request.Form["JBDANWEI"];
           
            //消息文件属性数据保存

            var sqlConME = new Sql_HT_CONTENTS();
            CG_XIAOXI_ME tblME = new Model.CG_XIAOXI_ME();
            tblME.XXWENJIANMING = context.Request.Form["JBWENJIANMING"];
            
            int numMe = -1;
            //判断数据库是不是有当前数据
            System.Data.DataTable tblybddocumentME = (System.Data.DataTable)new Sql_HT_CONTENTS().get_XiaoXiME_AllData(tblME);
            if (tblybddocumentME.Rows.Count > 0)
            {
                numMe = sqlConME.UpdateXiaoXiMe(JingBaomI);
            }
            else
            {
                numMe = sqlConME.AddXiaoXiMe(JingBaomI);
            }
            
            if (numMe <= 0)
            {
                returnStr += " 消息文件属性数据提交失败。 ";
            }
            else
            {
                returnStr += " 消息文件属性数据提交成功。 ";
            }
            #endregion

            #region 消息文件
            try
            {
                //文件名称
                string strone = context.Request.Form["JBWENJIANMING"];
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/JingBao/XX.doc");
                //生成的具有模板样式的新文件           
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\duanqi\\" + context.Request.Form["XXTime"];

                string fileName = strone;
                
                //消息Word 文档插入数据 生成
                XiaoXi jc = new XiaoXi();
                int flag0 = jc.ExportWord(templateFile, filepath, fileName, Contentvalue, JingBaomI, param);
                //消息文件保存到数据库
                int flag1 = XXruku(filepath + "\\" + fileName, filepath, strone);

                if (flag0 == 0)
                {
                    returnStr = returnStr + "  " + strone + " word生成失败!";
                }
                if (flag1 == 0)
                {
                    returnStr = returnStr + "  " + strone + " word插入数据库失败!";
                }
                if (flag0 == 1)
                {
                    returnStr = returnStr + "  " + strone + " word生成成功!";
                }
                if (flag1 == 1)
                {
                    returnStr = returnStr + "  " + strone + " word插入数据库成功!";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("预报单出错" + ex.ToString());
                returnStr = returnStr + "出错了！" + ex.ToString();
            }
            context.Response.Write(returnStr);

            #endregion
        }


        //解除警报保存
        void AddJieChuJingBao(HttpContext context)
        {
            string returnStr = "";

            #region 解除警报内容保存

            CG_HT_JIECHUJINGBAO_CONTENT Contentvalue = new CG_HT_JIECHUJINGBAO_CONTENT();
            //解除警报文件内容
            Contentvalue.JCJBWENJIANMING = context.Request.Form["JBWENJIANMING"];//文件名称
            Contentvalue.CONTENT = context.Request.Form["CONTENT"];//内容
            Contentvalue.SENTTO = context.Request.Form["SENTTO"];//发往
            Contentvalue.LINKMAN = context.Request.Form["LINKMAN"];//联系人
            Contentvalue.JCTITLE = context.Request.Form["DOCTITLE"];
            Contentvalue.DATETIME = Convert.ToDateTime(context.Request.Form["Time"] + " " + context.Request.Form["TM"] + ":00:00");
            //获取签发图片地址
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/" + context.Request.Form["ISSUEPICTURE"]);
            byte[] byData;
            //根据图片文件的路径使用文件流打开，并保存为byte[] 
            if (System.IO.File.Exists(imagepath))
            {
                FileStream fs = new FileStream(imagepath, FileMode.Open);
                byData = new byte[fs.Length];
                fs.Read(byData, 0, byData.Length);
                fs.Close();
                Contentvalue.ISSUEPICTURE = byData;
            }
            else
            {
                byData = new byte[0];
                context.Response.Write("图片已丢失。");
                Contentvalue.ISSUEPICTURE = byData;
            }

            //解除警报内容表数据保存
            var sqlConC = new Sql_HT_CONTENTS();
            CG_HT_JIECHUJINGBAO_CONTENT tblC = new Model.CG_HT_JIECHUJINGBAO_CONTENT();
            tblC.JCJBWENJIANMING = context.Request.Form["JBWENJIANMING"];
            int num = -1;
            //判断数据库是不是有当前数据
            System.Data.DataTable tblybddocumentC = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JieChuJingBaoCON_AllData(tblC);
            if (tblybddocumentC.Rows.Count > 0)
            {
                num = sqlConC.UpdateJieChuJingBaoContent(Contentvalue);
            }
            else
            {
                num = sqlConC.AddJieChuJingBaoContent(Contentvalue);
            }
            
            if (num <= 0)
            {
                returnStr += "解除警报文件内容提交失败。 ";
            }
            else
            {
                returnStr += "解除警报文件内容提交成功。 ";
            }
            //发往保存
            CommonSendUnit sunit = new CommonSendUnit(context.Request.Form["JBWENJIANMING"], context.Request.Form["SENTTO"]);
            returnStr += sunit.resultSendUnit();
            //word 文档参数
            List<string> param = new List<string>();
            param.Insert(0, context.Request.Form["Types"]);
            param.Insert(1, context.Request.Form["times"]);
            param.Insert(2, context.Request.Form["TM"]);
            param.Insert(3, imagepath);

            #endregion

            #region 解除警报文件属性

            CG_JIECHUJINGBAO_ME JingBaomI = new CG_JIECHUJINGBAO_ME();
            JingBaomI.JCWENJIANMING = context.Request.Form["JBWENJIANMING"];
            JingBaomI.JCQUYU = context.Request.Form["JBQUYU"];
            JingBaomI.JCNEIRONG = context.Request.Form["JBNEIRONG"];
            JingBaomI.JCBIANHAO = context.Request.Form["bianhao"];//保存到数据库中的编号
            JingBaomI.JCJIBIE = context.Request.Form["JBJIBIE"];
            JingBaomI.JCSHIJIAN = Convert.ToDateTime(context.Request.Form["JBSHIJIAN"]);
            JingBaomI.JCDANWEI = context.Request.Form["JBDANWEI"];
           

            //警报文件属性数据保存
            CG_JIECHUJINGBAO_ME tbl = new Model.CG_JIECHUJINGBAO_ME();
            tbl.JCWENJIANMING = context.Request.Form["JBWENJIANMING"];
            int numMe = -1;
            //判断数据库是不是有当前数据
            var sqlConME = new Sql_HT_CONTENTS();
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JieChuJingBaoME_AllData(tbl);
            if (tblybddocument.Rows.Count > 0)
            {
                //编辑
                numMe = sqlConME.UpdateJieChuJingBaoMe(JingBaomI);
            }
            else
            {
                //添加
                numMe = sqlConME.AddJieChuJingBaoMe(JingBaomI);

            }
            if (numMe <= 0)
            {
                returnStr += " 解除警报文件属性提交失败。 ";
            }
            else
            {
                returnStr += " 解除警报文件属性提交成功。 ";
            }
            #endregion

            #region 解除警报文件
            try
            {
                //文件名称
                string strone = context.Request.Form["JBWENJIANMING"];
                //模板文件
                string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/JingBao/JC.doc");
                //生成的具有模板样式的新文件           
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\duanqi\\" + context.Request.Form["XXTime"];

                string fileName = strone;
                JingBaomI.JCBIANHAO = context.Request.Form["JBBIANHAO"];//保存到文档中的编号
                //警报Word 文档插入数据 生成
                JieChuJingBao jc = new JieChuJingBao();
                 int flag0 = jc.ExportWord(templateFile, filepath, fileName, Contentvalue, JingBaomI, param);

                //警报文件保存到数据库
                int flag1 = JCruku(filepath + "\\" + fileName, filepath, strone);

                if (flag0 == 0)
                {
                    returnStr = returnStr + "  " + strone + " word生成失败!";
                }
                if (flag1 == 0)
                {
                    returnStr = returnStr + "  " + strone + " word插入数据库失败!";
                }
                if (flag0 == 1)
                {
                    returnStr = returnStr + "  " + strone + " word生成成功!";
                }
                if (flag1 == 1)
                {
                    returnStr = returnStr + "  " + strone + " word插入数据库成功!";
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("1号预报单出错" + ex.ToString());
                returnStr = returnStr + "出错了！" + ex.ToString();
            }
            context.Response.Write(returnStr);

            #endregion
        }

        /// <summary>
        /// 警报word入库
        /// </summary>
        /// <param name="file">对应word文档</param>
        /// <param name="filepath">word文档路径</param>
        /// <param name="strone">word文档文件名</param>
        private int JBruku(string file,string filepath, string strone)
        {
            int a1 = 0;
            CG_JINGBAO_FILE tbl = new CG_JINGBAO_FILE();
            tbl.JBWENJIANMING = strone;
            byte[] byFile;

            //word文档转二进制
            if (System.IO.File.Exists(file))
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                byFile = new byte[fs.Length];
                fs.Read(byFile, 0, byFile.Length);
                fs.Close();
                tbl.JBNEIRONG = byFile;
            }
            else
            {
                return 0;
            }
            //word文档生成图片转二进制
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/Images/JingBaoImg/ContentImg");
            PredicTable.ExportWord.JingBao.Word word = new ExportWord.JingBao.Word();
            word.WordToImage(strone, filepath, imagepath, strone.Split('.')[0], ImageFormat.Png, 2);

            byte[] byfileimg;
            if (System.IO.File.Exists(imagepath+"/"+ strone.Split('.')[0]+".png"))
            {
                FileStream fs = new FileStream(imagepath + "/" + strone.Split('.')[0]+".png", FileMode.Open);
                byfileimg = new byte[fs.Length];
                fs.Read(byfileimg, 0, byfileimg.Length);
                fs.Close();
                tbl.PICFILE = byfileimg;
            }
            else
            {
                return 0;
            }

            //判断数据库是不是有当前word文档
            List<int> a = new List<int>();
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JingBaoFILE_AllData(tbl);
            Sql_HT_CONTENTS content = new Sql_HT_CONTENTS();
            if (tblybddocument.Rows.Count > 0)
            {
                //编辑
                a1 = content.UpdateJingBaoFile(tbl);
               
            }
            else
            {  
                //添加
                 a1 = content.AddJingBaoFile(tbl);
              
            }
            return a1;
        }

        


        /// <summary>
        /// 生成表单
        /// </summary>
        //private string MakeWord(string JBWENJIANMING)
        //{
        //    try
        //    {
        //        //文件名称
        //        string strone = JBWENJIANMING;
        //        //模板文件
        //        string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/JingBao/JB.doc");
        //        //生成的具有模板样式的新文件           
        //        string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\JingBao\\" + strone + ".doc";
        //        //删除word文件
        //        System.IO.File.Delete(fileName);
        //        //3号黄河南海堤预报单生成
        //        OneWord One = new OneWord();
        //        int flag0 = One.ExportWord(templateFile, fileName, dt);

        /// <summary>
        /// 生成表单
        /// </summary>
        //private string MakeWord(string JBWENJIANMING)
        //{
        //    try
        //    {
        //        //文件名称
        //        string strone = JBWENJIANMING;
        //        //模板文件
        //        string templateFile = System.Web.HttpContext.Current.Server.MapPath("/word/JingBao/JB.doc");
        //        //生成的具有模板样式的新文件           
        //        string fileName = AppDomain.CurrentDomain.BaseDirectory + "scword\\JingBao\\" + strone + ".doc";
        //        //删除word文件
        //        System.IO.File.Delete(fileName);
        //        //3号黄河南海堤预报单生成
        //        OneWord One = new OneWord();
        //        int flag0 = One.ExportWord(templateFile, fileName, dt);


        /// <summary>
        /// 消息word入库
        /// </summary>
        /// <param name="file">对应word文档</param>
        /// <param name="filepath">word文档路径</param>
        /// <param name="strone">word文档文件名</param>
        private int XXruku(string file, string filepath, string strone)
        {
            int a1 = 0;
            CG_XIAOXI_FILE tbl = new CG_XIAOXI_FILE();
            tbl.XXWENJIANMING = strone;
            byte[] byFile;

            //word文档转二进制
            if (System.IO.File.Exists(file))
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                byFile = new byte[fs.Length];
                fs.Read(byFile, 0, byFile.Length);
                fs.Close();
                tbl.XXNEIRONG = byFile;
            }
            else
            {
                return 0;
            }
            //word文档生成图片转二进制
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/Images/JingBaoImg/ContentImg");
            PredicTable.ExportWord.JingBao.Word word = new ExportWord.JingBao.Word();
            word.WordToImage(strone, filepath, imagepath, strone.Split('.')[0], ImageFormat.Png, 2);

            byte[] byfileimg;
            if (System.IO.File.Exists(imagepath + "/" + strone.Split('.')[0] + ".png"))
            {
                FileStream fs = new FileStream(imagepath + "/" + strone.Split('.')[0] + ".png", FileMode.Open);
                byfileimg = new byte[fs.Length];
                fs.Read(byfileimg, 0, byfileimg.Length);
                fs.Close();
                tbl.PICFILE = byfileimg;
            }
            else
            {
                return 0;
            }

            //判断数据库是不是有当前word文档
            List<int> a = new List<int>();
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new Sql_HT_CONTENTS().get_XiaoXiFILE_AllData(tbl);
            Sql_HT_CONTENTS content = new Sql_HT_CONTENTS();
            if (tblybddocument.Rows.Count > 0)
            {
                //编辑
                a1 = content.UpdateXiaoXiFile(tbl);

            }
            else
            {
                //添加
                a1 = content.AddXiaoXiFile(tbl);

            }
            return a1;
        }

        /// <summary>
        /// 解除警报word入库
        /// </summary>
        /// <param name="file">对应word文档</param>
        /// <param name="filepath">word文档路径</param>
        /// <param name="strone">word文档文件名</param>
        private int JCruku(string file, string filepath, string strone)
        {
            int a1 = 0;
            CG_JIECHUJINGBAO_FILE tbl = new CG_JIECHUJINGBAO_FILE();
            tbl.JCWENJIANMING = strone;
            byte[] byFile;

            //word文档转二进制
            if (System.IO.File.Exists(file))
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                byFile = new byte[fs.Length];
                fs.Read(byFile, 0, byFile.Length);
                fs.Close();
                tbl.JCNEIRONG = byFile;
            }
            else
            {
                return 0;
            }
            //word文档生成图片转二进制
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("/Images/JingBaoImg/ContentImg");
            PredicTable.ExportWord.JingBao.Word word = new ExportWord.JingBao.Word();
            word.WordToImage(strone, filepath, imagepath, strone.Split('.')[0], ImageFormat.Png, 2);

            byte[] byfileimg;
            if (System.IO.File.Exists(imagepath + "/" + strone.Split('.')[0] + ".png"))
            {
                FileStream fs = new FileStream(imagepath + "/" + strone.Split('.')[0] + ".png", FileMode.Open);
                byfileimg = new byte[fs.Length];
                fs.Read(byfileimg, 0, byfileimg.Length);
                fs.Close();
                tbl.PICFILE = byfileimg;
            }
            else
            {
                return 0;
            }

            //判断数据库是不是有当前word文档
            List<int> a = new List<int>();
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JieChuJingBaoFILE_AllData(tbl);
            Sql_HT_CONTENTS content = new Sql_HT_CONTENTS();
            if (tblybddocument.Rows.Count > 0)
            {
                //编辑
                a1 = content.UpdateJCJingBaoFile(tbl);

            }
            else
            {
                //添加
                a1 = content.AddJCJingBaoFile(tbl);

            }
            return a1;
        }
        //返回信息警报数据
        void getXinXi(HttpContext context)
        {
            CG_XIAOXI_ME XXME= new CG_XIAOXI_ME();
            XXME.XXDANWEI = context.Request.Form["XXDANWEI"];//发布单位
            XXME.XXNEIRONG = context.Request.Form["XXNEIRONG"];//警报类型
            var dataTable = new DataTable();
            var sqlCon = new Sql_HT_CONTENTS();

            dataTable = sqlCon.getXinXi(XXME);

            var dataJson = JsonMore.Serialize(dataTable);
            context.Response.ContentType = "application/json";
            context.Response.Write(dataJson);
        }
        //返回警报数据
        void getJingBao(HttpContext context)
        {
            string bianhao = "";
             CG_JINGBAO_ME XXME = new CG_JINGBAO_ME();
            XXME.JBDANWEI = context.Request.Form["JBDANWEI"];//发布单位
            XXME.JBNEIRONG = context.Request.Form["JBNEIRONG"];//警报类型
            var dataTable = new DataTable();
            var sqlCon = new Sql_HT_CONTENTS();
            dataTable = sqlCon.getJingBao(XXME);
            if (dataTable.Rows.Count == 0)
            {
                var dataJson = JsonMore.Serialize(dataTable);
                context.Response.ContentType = "application/json";
                context.Response.Write(dataJson);
                return;

            }
            bianhao = dataTable.Rows[0][2].ToString().Substring(0, dataTable.Rows[0][2].ToString().Length - 2);
            CG_JIECHUJINGBAO_ME JCME = new CG_JIECHUJINGBAO_ME();
            JCME.JCBIANHAO = bianhao;
            JCME.JCNEIRONG = dataTable.Rows[0][1].ToString();
            var dataTable1 = new DataTable();
            dataTable1 = sqlCon.getCouldJCJingBao(JCME);
            if (dataTable1.Rows.Count > 0)
            {//已经解除警报，获取当前最初01word文档警报
                CG_JINGBAO_ME XXME1 = new CG_JINGBAO_ME();
                XXME1.JBBIANHAO = bianhao + "01";
                XXME1.JBNEIRONG = dataTable.Rows[0][1].ToString();
                dataTable = sqlCon.getJingBaoBH(XXME1);
                var dataJson = JsonMore.Serialize(dataTable);
                context.Response.ContentType = "application/json";
                context.Response.Write(dataJson);
            }
            else
            {//未解除当前警报，获取当前word文档内容
                var dataJson = JsonMore.Serialize(dataTable);
                context.Response.ContentType = "application/json";
                context.Response.Write(dataJson);
            }
            
        } 
        //返回解除警报数据
        void getJCJingBao(HttpContext context)
        {
            //查询警报表中是否有未解除警报的数据
            string bianhao = "";
            CG_JINGBAO_ME XXMEM = new CG_JINGBAO_ME();
            XXMEM.JBDANWEI = context.Request.Form["JCDANWEI"];//发布单位
            XXMEM.JBNEIRONG = context.Request.Form["JCNEIRONG"];//警报类型
            var dataTable = new DataTable();
            var sqlCon = new Sql_HT_CONTENTS();
            dataTable = sqlCon.getJingBao(XXMEM);
            if (dataTable.Rows.Count == 0)
            {
                var dataJson = JsonMore.Serialize(dataTable);
                context.Response.ContentType = "application/json";
                context.Response.Write(dataJson);
                return;

            }
            bianhao = dataTable.Rows[0][2].ToString().Substring(0, dataTable.Rows[0][2].ToString().Length - 2);
            CG_JIECHUJINGBAO_ME JCME = new CG_JIECHUJINGBAO_ME();
            JCME.JCBIANHAO = bianhao;
            JCME.JCNEIRONG = dataTable.Rows[0][1].ToString();
            var dataTable1 = new DataTable();
            dataTable1 = sqlCon.getCouldJCJingBao(JCME);
            if (dataTable1.Rows.Count > 0)
            {
             //已经解除警报，绑定最新解除警报word文档内容
                CG_JIECHUJINGBAO_ME XXME = new CG_JIECHUJINGBAO_ME();
                XXME.JCDANWEI = context.Request.Form["JCDANWEI"];//发布单位
                XXME.JCNEIRONG = context.Request.Form["JCNEIRONG"];//警报类型
                var dataTableJC = new DataTable();
                var sqlConJC = new Sql_HT_CONTENTS();

                dataTableJC = sqlConJC.getJCJingBao(XXME);

                var dataJson = JsonMore.Serialize(dataTableJC);
                context.Response.ContentType = "application/json";
                context.Response.Write(dataJson);
            }
            else
            {//未解除当前警报，获取当前word文档内容
                var dataJson = JsonMore.Serialize(dataTable);
                context.Response.ContentType = "application/json";
                context.Response.Write(dataJson);
            }
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










