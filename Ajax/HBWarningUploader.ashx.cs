using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.ExportWord.JingBao;
using PredicTable.ExportWord.NineteenWord;
using PredicTable.ExportWord.StormWordAnalysis;
using PredicTable.ExportWord.WaveWordAnalysis;
using PredicTable.Model;
using PredicTable.Model.NineteenWord;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// HBWarningUploader 的摘要说明
    /// 海冰警报文件上传
    /// </summary>
    public class HBWarningUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
                context.Response.ContentType = "text/plain";
                context.Response.ContentEncoding = Encoding.UTF8;
                if (context.Request["method"] != null)
                {
                    string method = context.Request["method"].ToString();

                    switch (method)
                    {
                        case "DeleteFile": DeleteFile(context); break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (context.Request["REQUEST_METHOD"] == "OPTIONS")//????
                    {
                        context.Response.End();
                    }
                    //this.AddInfo("F:\\Project\\yubaodan2.0\\PredicTable1207\\PredicTable\\PredicTable\\预报单共享\\duanqi\\20161226\\JB_NCS_HB_B160121_2016012115_NMFC.doc");

                    UploadFile(context);
                }
            
        }
      
        /// <summary>
        /// 上传文件
        /// </summary>
        private void UploadFile(HttpContext context)
        {
            string filePathName = string.Empty;
            string id = context.Request["id"].ToString();
            string name = context.Request["name"].ToString();
            string type = context.Request["type"].ToString();
            string lastModifiedDate = context.Request["lastModifiedDate"].ToString();
            string size = context.Request["size"].ToString();
            HttpPostedFile file = context.Request.Files["file"];
            string FaWangbz = context.Request["FaWangbz"].ToString().Trim(',');//发往备注
            // BaseResult br = new BaseResult();
            // br.state = false;
            var jsonData = new object();
            //检查上传文件
            if (context.Request.Files.Count == 0)
            {
                jsonData = new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" };
                //br.jdata = jsonData;
                //return CreateJsonResult(br);
                context.Response.Write(jsonData);
            }
            string nameTime="";
            string filename = file.FileName;
            string filetype= filename.Split('_')[2];
            string types= filename.Split('_')[0];
            if (filetype == "HL")
            {//海浪
                if (types == "JB")
                {
                    nameTime = filename.Split('_')[4].Substring(0, 8);
                }
                else if (types == "JC")
                {
                    nameTime = filename.Split('_')[4].Substring(0, 8);
                }
                else if (types == "XX")
                {
                    nameTime ="20"+filename.Split('_')[3].Substring(0,6);
                }
            }
            else if (filetype == "HB")
            {//海冰
                if (types == "JB")
                {
                    nameTime = filename.Split('_')[4].Substring(0, 8);
                }
                else if (types == "JC")
                {
                    nameTime = filename.Split('_')[4].Substring(0, 8);
                }
                else if (types == "XX")
                {
                    nameTime = "20"+ filename.Split('_')[3].Substring(0, 6);
                }
            }
            else if (filetype == "FBC")
            {//风暴潮
                if (types == "JB")
                {
                    nameTime = filename.Split('_')[4].Substring(0, 8);
                }
                else if (types == "JC")
                {
                    nameTime = filename.Split('_')[4].Substring(0, 8);
                }
                else if (types == "XX")
                {
                    nameTime = filename.Split('_')[4].Substring(0, 8);
                }
            }
            
            string path = "预报单共享\\duanqi\\" + nameTime;//默认文件保存的路径
            string myselfpPath = context.Request["myselfpPath"];//指定路径
            if (myselfpPath != null && myselfpPath != "")
            {
                path = myselfpPath;
            }

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, path);
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            
            string ex = Path.GetExtension(file.FileName);
            filePathName = file.FileName;
            //文件保存到指定路径下
            file.SaveAs(Path.Combine(localPath, filePathName));
            path = AppDomain.CurrentDomain.BaseDirectory + path;

            #region 文件名判断
            DateTime time = DateTime.Now;
            string danwei = "";
            string WainArea = "";
            string ListType = "";
            string WainType = "";
            if (filePathName.Contains("NCS"))
            {
                danwei = "北海预报中心";
                WainArea = "北海区";
                if (filePathName.Contains("JB"))
                {
                    ListType = "警报";
                }
                if (filePathName.Contains("JC"))
                {
                    ListType = "解除";
                }
                if (filePathName.Contains("XX"))
                {
                    ListType = "消息";
                }
            }
            if (filePathName.Contains("SD"))
            {
                danwei = "山东省海洋预报台";
                WainArea = "山东近海";

                if (filePathName.Contains("JB"))
                {
                    ListType = "警报";
                }
                if (filePathName.Contains("JC"))
                {
                    ListType = "解除";
                }
                if (filePathName.Contains("XX"))
                {
                    ListType = "消息";
                }
            }
           
            if (filePathName.Contains("HL"))
            {
                WainType = "海浪";
            }
            if (filePathName.Contains("HB"))
            {
                WainType = "海冰";
            }
            if (filePathName.Contains("FBC"))
            {
                WainType = "风暴潮";
            }
            
            #endregion

            #region  文件入库及表格数据解析入库
            var result="";
            WaveContent wave = new WaveContent();
            StormContent storm = new StormContent();
            try
            {
                if (ListType == "消息")
                {
                    if (WainType == "海浪") {
                        result += wave.AddXXInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                    else if (WainType == "海冰") {
                        result += AddHBXXInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                    else {
                        result += storm.AddXXInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                    
                }
                else if (ListType == "警报")
                {
                    if (WainType == "海浪")
                    {
                        result += wave.AddJBInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                    else if (WainType == "海冰")
                    {
                        result += AddHBJBInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                    else
                    {
                        result += storm.AddJBInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                }
                else if (ListType == "解除")
                {
                    if (WainType == "海浪")
                    {
                        result += wave.AddJCInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                    else if (WainType == "海冰")
                    {
                        result += AddHBJCInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                    else
                    {
                        result += storm.AddJCInfo(localPath + "\\" + filePathName, path + "\\", filePathName, WainArea, FaWangbz, WainType, danwei);
                    }
                }
            }
            catch(Exception error)
            {
                result += "文件上传失败。";
            }
            context.Response.Write(result);
            #endregion
        }



        #region 保存字段属性Content
        /// <summary>
        /// 保存海冰消息内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="filePathName"></param>
        private string AddHBXXInfo(string filePath ,string path ,string filePathName,string WainArea,string FaWangbz,string WainType,string danwei)
        {
            string returnStr = "";

            WarningTranslateWord nineteenYearTables = new WarningTranslateWord(filePath);
            nineteenYearTables.assignmentTable();

            //字段解析
            CG_HT_XIAOXI_CONTENT Contentvalue = new CG_HT_XIAOXI_CONTENT();
            Contentvalue.IPHONE = nineteenYearTables.tel;
            Contentvalue.LINKMAN = nineteenYearTables.link;
            var fw = nineteenYearTables.fawang;
            if (fw != "" && fw != null)
            {

                if (fw.Substring(0, 3) == "发往：")
                {

                    fw = fw.Replace("发往：", "");
                    fw = fw.Replace("、", ";");
                    fw = fw.Replace("。", "");
                }
                else
                {
                    fw = fw.Replace("、", ";");
                    fw = fw.Replace("。", "");
                }
                Contentvalue.SENTTO = fw;
            }
            else
            {
                Contentvalue.SENTTO = nineteenYearTables.fawang;
            }

           
            Contentvalue.XXWENJIANMING = filePathName;
            Contentvalue.DATETIME = nineteenYearTables.publish_time;
            Contentvalue.CONTENT = nineteenYearTables.ice_situation + nineteenYearTables.predict + nineteenYearTables.description;
            Contentvalue.XXTITLE = nineteenYearTables.title;
            //获取签发图片地址
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("..\\Images\\JingBaoImg\\quanfa.png");
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
            int num = -1;
            var sqlConC = new Sql_HT_CONTENTS();
            CG_HT_XIAOXI_CONTENT tblC = new Model.CG_HT_XIAOXI_CONTENT();
            tblC.XXWENJIANMING = filePathName;
            //判断数据库是不是有当前数据
            System.Data.DataTable tblybddocumentC = (System.Data.DataTable)new Sql_HT_CONTENTS().get_XiaoXiCON_AllData(tblC);
            if (tblybddocumentC.Rows.Count > 0)
            {
                num = sqlConC.UpdateXiaoXiContent(Contentvalue);
                returnStr += "消息文件属性更新成功。";
            }
            else
            {
                num = sqlConC.AddXiaoXiContent(Contentvalue);
                returnStr += "消息文件属性添加成功。";
            }

            if (num > 0)
            {
                int res = this.XXruku(filePath, path, filePathName, WainArea, filePathName.Split('_')[3], WainType, danwei, nineteenYearTables.publish_time);
                if(res > 0)
                {
                    returnStr += "消息文件流上传成功。";
                }
                else
                {
                    returnStr += "消息文件流上传失败。";
                }
            }
            else
            {
                returnStr += "消息文件属性上传失败。";
            }
            CommonSendUnit sunit = new CommonSendUnit();
            string ret = sunit.resultSendUnitbz(filePathName, Contentvalue.SENTTO, FaWangbz);
            return returnStr;
        }
       

        /// <summary>
        /// 保存海冰警报内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="filePathName"></param>
        private string AddHBJBInfo(string filePath, string path , string filePathName, string WainArea,string FaWangbz,string WainType,string danwei)
        {
            string returnStr = "";
            //sql_NineteenTable sql_nineteenTable = new sql_NineteenTable();

            WarningTranslateWord nineteenYearTables = new WarningTranslateWord(filePath);
            nineteenYearTables.assignmentTable();
            NineteenYearModel yearModel = new NineteenYearModel();
            NineteenNomalModel nomalModel = new NineteenNomalModel();

            //字段解析
            CG_HT_JINGBAO_CONTENT Contentvalue = new CG_HT_JINGBAO_CONTENT();
            Contentvalue.IPHONE = nineteenYearTables.tel;
            Contentvalue.LINKMAN = nineteenYearTables.link;
            var fw = nineteenYearTables.fawang;
            if (fw != "" && fw != null)
            {

                if (fw.Substring(0, 3) == "发往：")
                {

                    fw = fw.Replace("发往：", "");
                    fw = fw.Replace("、", ";");
                    fw = fw.Replace("。", "");
                }
                else
                {
                    fw = fw.Replace("、", ";");
                    fw = fw.Replace("。", "");
                }
                Contentvalue.SENTTO = fw;
            }
            else
            {
                Contentvalue.SENTTO = nineteenYearTables.fawang;
            }
            Contentvalue.JBWENJIANMING = filePathName;
            Contentvalue.DATETIME = nineteenYearTables.publish_time;
            Contentvalue.CONTENT = nineteenYearTables.ice_situation + nineteenYearTables.predict + nineteenYearTables.description;
            Contentvalue.JBTITLE = nineteenYearTables.title ;
            Contentvalue.CONTENTTABLE = nineteenYearTables.biaohao;
            Contentvalue.JBREMARKS = (nineteenYearTables.JBREMARKS == null) ? "" : nineteenYearTables.JBREMARKS;
            //Contentvalue.PICTURE = new byte[1];
            //获取签发图片地址
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("..\\Images\\JingBaoImg\\quanfa.png");
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

            //表格1 - 海区浮冰外缘线与平整冰厚度
            string[,] table_str_1 = nineteenYearTables.table_str_1;
            nomalModel = this.getNomalTableData(nomalModel, table_str_1);

            //表格2-沿岸主要港口和海岛平整冰厚度预报
            string[,] table_str_3 = nineteenYearTables.table_str_3;
            yearModel = this.getYearTableData(yearModel, table_str_3);



            int num = -1;
            var sqlConC = new Sql_HT_CONTENTS();

            //判断数据库是不是有数据
            CG_HT_JINGBAO_CONTENT tblC = new Model.CG_HT_JINGBAO_CONTENT();
            tblC.JBWENJIANMING = filePathName;
            System.Data.DataTable tblybddocumentC = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JingBaoCON_AllData(tblC);
            if (tblybddocumentC.Rows.Count > 0)
            {
                num = sqlConC.UpdateJingBaoContents(Contentvalue);
                returnStr += "警报文件属性更新成功。";
            }
            else
            {
                num = sqlConC.AddJingBaoContents(Contentvalue);
                returnStr += "警报文件属性添加成功。";
            }
            if (num > 0)
            {
                SaveTableDataSea(nomalModel, filePathName);
                SaveTableDataCoast(yearModel, filePathName);
                string level = this.GetLevel(filePathName);
                int res = this.JBruku(filePath, path , filePathName, WainArea, filePathName.Split('_')[3], level, WainType, danwei, nineteenYearTables.publish_time);
                if (res > 0)
                {
                    returnStr += "警报文件流上传成功。";
                }
                else
                {
                    returnStr += "警报文件流上传失败。";
                }
               
            }
            else
            {
                returnStr += "警报文件属性上传失败。";
            }
            CommonSendUnit sunit = new CommonSendUnit();
            string ret = sunit.resultSendUnitbz(filePathName, Contentvalue.SENTTO, FaWangbz);
            return returnStr;
        }

      
        /// <summary>
        /// 保存解除警报内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ListType"></param>
        /// <param name="filePathName"></param>
        private string AddHBJCInfo(string filePath, string path, string filePathName, string WainArea,string FaWangbz, string WainType,string danwei)
         {
            string returnStr = "";
            WarningTranslateWord nineteenYearTables = new WarningTranslateWord(filePath);
            nineteenYearTables.assignmentTable();
            NineteenYearModel yearModel = new NineteenYearModel();
            NineteenNomalModel nomalModel = new NineteenNomalModel();

            //字段解析
            CG_HT_JIECHUJINGBAO_CONTENT Contentvalue = new CG_HT_JIECHUJINGBAO_CONTENT();
            Contentvalue.IPHONE = nineteenYearTables.tel;
            Contentvalue.LINKMAN = nineteenYearTables.link;
            var fw = nineteenYearTables.fawang;
            if (fw != "" && fw != null)
            {

                if (fw.Substring(0, 3) == "发往：")
                {

                    fw = fw.Replace("发往：", "");
                    fw = fw.Replace("、", ";");
                    fw = fw.Replace("。", "");
                }
                else
                {
                    fw = fw.Replace("、", ";");
                    fw = fw.Replace("。", "");
                }
                Contentvalue.SENTTO = fw;
            }
            else
            {
                Contentvalue.SENTTO = nineteenYearTables.fawang;
            }
            //string biaohao = nineteenYearTables.biaohao;
            Contentvalue.JCJBWENJIANMING = filePathName;
            Contentvalue.DATETIME = nineteenYearTables.publish_time;
            Contentvalue.CONTENT = nineteenYearTables.ice_situation + nineteenYearTables.predict + nineteenYearTables.description;
            Contentvalue.JCTITLE = nineteenYearTables.title;
            Contentvalue.CONTENTTABLE = nineteenYearTables.biaohao;
            Contentvalue.JCREMARKS = (nineteenYearTables.JBREMARKS == null) ? "" : nineteenYearTables.JBREMARKS;
            //获取签发图片地址
            string imagepath = System.Web.HttpContext.Current.Server.MapPath("..\\Images\\JingBaoImg\\quanfa.png");
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

            //表格1 - 海区浮冰外缘线与平整冰厚度
            string[,] table_str_1 = nineteenYearTables.table_str_1;
            nomalModel = this.getNomalTableData(nomalModel, table_str_1);

            //表格2-沿岸主要港口和海岛平整冰厚度预报
            string[,] table_str_3 = nineteenYearTables.table_str_3;
            yearModel = this.getYearTableData(yearModel, table_str_3);

            int num = -1;
            var sqlConC = new Sql_HT_CONTENTS();
            //解除警报内容表数据保存
            CG_HT_JIECHUJINGBAO_CONTENT tblC = new Model.CG_HT_JIECHUJINGBAO_CONTENT();
            tblC.JCJBWENJIANMING = filePathName;
            //判断数据库是不是有当前数据
            System.Data.DataTable tblybddocumentC = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JieChuJingBaoCON_AllData(tblC);
            if (tblybddocumentC.Rows.Count > 0)
            {
                num = sqlConC.UpdateJieChuJingBaoContent(Contentvalue);
                returnStr += "解除警报文件属性更新成功。";
            }
            else
            {
                num = sqlConC.AddJieChuJingBaoContent(Contentvalue);
                returnStr += "解除警报文件属性添加成功。";
            }

            if (num > 0)
            {
                SaveTableDataSea(nomalModel, filePathName);
                SaveTableDataCoast(yearModel, filePathName);
                string level = this.GetLevel(filePathName);
                int res = this.JCruku(filePath, path , filePathName, WainArea, filePathName.Split('_')[3], level, WainType, danwei, nineteenYearTables.publish_time);
                if (res > 0)
                {
                    returnStr += "解除警报文件流上传成功。";
                }
                else
                {
                    returnStr += "解除警报文件流上传失败。";
                }
            }
            else
            {
                returnStr += "解除警报文件属性上传失败。";
            }
            //发往保存
            CommonSendUnit sunit = new CommonSendUnit();
            string ret = sunit.resultSendUnitbz(filePathName, Contentvalue.SENTTO, FaWangbz);
            return returnStr;
        }

        #endregion

        #region 解析表格数据、表格数据入库

        /// <summary>
        /// 解析表格1-海区浮冰外缘线与平整冰厚度
        /// 表格数据入库
        /// </summary>
        /// <param name="nomalModel"></param>
        /// <param name="tableNormal"></param>
        private NineteenNomalModel getNomalTableData(NineteenNomalModel nomalModel, string[,] tableNormal)// NineteenNomalLineModel nineteenNomalLineModel,
        {
            List<NineteenNomalLineModel> list = new List<NineteenNomalLineModel>();
            var row = tableNormal.GetLength(0);
            //var col = tableNormal.GetLength(1);
            for (int i = 1; i < row; i++)
            {
                NineteenNomalLineModel nineteenNomalLineModel = new Model.NineteenWord.NineteenNomalLineModel();
                nineteenNomalLineModel.NAME = tableNormal[i, 0].ToString();
                nineteenNomalLineModel.TERMINALLINE = tableNormal[i, 1].ToString();
                nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[i, 2] == null ? nineteenNomalLineModel.GENERALICETHICKNESS = "" : nineteenNomalLineModel.GENERALICETHICKNESS = tableNormal[i, 2].ToString();
                nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[i, 3] == null ? nineteenNomalLineModel.MAXICETHICKNESS = "" : nineteenNomalLineModel.MAXICETHICKNESS = tableNormal[i, 3].ToString();
                list.Add(nineteenNomalLineModel);
            }
            nomalModel.NineteenNomalLine = list;
            return nomalModel;
        }
        /// <summary>
        /// 解析表格2-沿岸主要港口和海岛平整冰厚度预报
        /// 表格数据入库
        /// </summary>
        /// <param name="yearModel"></param>
        /// <param name="table_str_3"></param>
        private NineteenYearModel getYearTableData(NineteenYearModel yearModel, string[,] table_str_3)
        {
            List<NineteenYearCknessModel> cknessList = new List<NineteenYearCknessModel>();
            //沿岸主要港口和海岛平整冰厚度预报
            var cknessrow = table_str_3.GetLength(0);
            var cknesscol = table_str_3.GetLength(1);
            for (int g = 1; g < cknesscol; g++)
            {
                NineteenYearCknessModel cknessModel = new Model.NineteenWord.NineteenYearCknessModel();
                //cknessModel.SITE = table_str_3[g - 1, 0].ToString();
                cknessModel.NAME = table_str_3[0, g].ToString();
                cknessModel.GENERALICETHICKNESS = table_str_3[1, g].ToString();
                cknessModel.MAXICETHICKNESS = table_str_3[2, g].ToString();
                cknessList.Add(cknessModel);
            }
            yearModel.nineteenYearCknessModel = cknessList;
            return yearModel;
        }

        /// <summary>
        /// 保存表格1
        /// </summary>
        /// <param name="nomalModel"></param>
        /// <param name="WENJIANMING"></param>
        public void SaveTableDataSea(NineteenNomalModel nomalModel,string WENJIANMING)
        {
            sql_SeaTable seaTable = new sql_SeaTable();
            int result = 0;
            for(int i = 0;i < nomalModel.NineteenNomalLine.Count; i++)
            {
                DataTable dt = seaTable.GetTableData((NineteenNomalLineModel)nomalModel.NineteenNomalLine[i],WENJIANMING);
                if(dt != null && dt.Rows.Count > 0)
                {
                    result = seaTable.UpdateTableData((NineteenNomalLineModel)nomalModel.NineteenNomalLine[i], WENJIANMING);
                }
                else
                {
                    result = seaTable.InsertTableData((NineteenNomalLineModel)nomalModel.NineteenNomalLine[i], WENJIANMING);
                }
            }
        }

        /// <summary>
        /// 保存表格2
        /// </summary>
        /// <param name="yearModel"></param>
        /// <param name="WENJIANMING"></param>
        private void SaveTableDataCoast(NineteenYearModel yearModel, string WENJIANMING)
        {
            sql_CoastTable coastTable = new sql_CoastTable();
            int result = 0;
            for (int i = 0; i < yearModel.nineteenYearCknessModel.Count; i++)
            {
                DataTable dt = coastTable.GetTableData((NineteenYearCknessModel)yearModel.nineteenYearCknessModel[i], WENJIANMING);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = coastTable.UpdateTableData((NineteenYearCknessModel)yearModel.nineteenYearCknessModel[i], WENJIANMING);
                }
                else
                {
                    result = coastTable.InsertTableData((NineteenYearCknessModel)yearModel.nineteenYearCknessModel[i], WENJIANMING);
                }
            }
        }

        #endregion

        #region  文件流入库

        /// <summary>
        /// 消息word入库
        /// </summary>
        /// <param name="file">对应word文档</param>
        /// <param name="filepath">word文档路径</param>
        /// <param name="strone">word文档文件名</param>
        public int XXruku(string file, string filepath, string strone, string WainArea,string biaohao, string WainType,string danwei,DateTime pbtime)
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
            var numMe = 0;
            Sql_HT_CONTENTS content = new Sql_HT_CONTENTS();
            CG_XIAOXI_ME tblME = new Model.CG_XIAOXI_ME();
            tblME.XXWENJIANMING = strone;
            tblME.XXBIANHAO = biaohao;
            tblME.XXDANWEI = danwei;
            tblME.XXNEIRONG = WainType;
            tblME.XXQUYU = WainArea;
            tblME.XXSHIJIAN = pbtime;
            //判断数据库是不是有当前数据
            System.Data.DataTable tblybddocumentME = (System.Data.DataTable)new Sql_HT_CONTENTS().get_XiaoXiME_AllData(tblME);
            if (tblybddocumentME.Rows.Count > 0)
            {
                numMe = content.UpdateXiaoXiMe(tblME);
            }
            else
            {
                numMe = content.AddXiaoXiMe(tblME);
            }
            if (numMe > 0)
            {
                //判断数据库是不是有当前word文档
                List<int> a = new List<int>();
                System.Data.DataTable tblybddocument = (System.Data.DataTable)new Sql_HT_CONTENTS().get_XiaoXiFILE_AllData(tbl);

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
            }
            return a1;
        }

        /// <summary>
        /// 警报word入库
        /// </summary>
        /// <param name="file">对应word文档</param>
        /// <param name="filepath">word文档路径</param>
        /// <param name="strone">word文档文件名</param>
        public int JBruku(string file, string filepath, string strone, string WainArea,string biaohao,string level,string WainType,string danwei, DateTime pbtime)
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
            CG_JINGBAO_ME JingBaomI = new CG_JINGBAO_ME();
            JingBaomI.JBWENJIANMING = strone;
            JingBaomI.JBQUYU = WainArea;
            JingBaomI.JBNEIRONG = WainType;
            JingBaomI.JBBIANHAO = biaohao;
            JingBaomI.JBJIBIE = level;
            JingBaomI.JBSHIJIAN = pbtime;
            JingBaomI.JBDANWEI = danwei;


            //警报文件属性数据保存
            var sqlConME = new Sql_HT_CONTENTS();
            //CG_JINGBAO_ME tblME = new Model.CG_JINGBAO_ME();
            //tblME.JBWENJIANMING = strone;
            int numMe = 0;
            //判断数据库是不是有数据
            System.Data.DataTable tblybddocumentME = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JingBaoME_AllData(JingBaomI);
            if (tblybddocumentME.Rows.Count > 0)
            {
                numMe = sqlConME.UpdateJingBaoMe(JingBaomI);
            }
            else
            {
                numMe = sqlConME.AddJingBaoMe(JingBaomI);
            }
            if (numMe > 0)
            {
                //判断数据库是不是有当前word文档
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
            }
            return a1;
        }


        /// <summary>
        /// 解除警报word入库
        /// </summary>
        /// <param name="file">对应word文档</param>
        /// <param name="filepath">word文档路径</param>
        /// <param name="strone">word文档文件名</param>
        public int JCruku(string file, string filepath, string strone, string WainArea, string biaohao, string level,string WainType,string danwei, DateTime pbtime)
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
            //解除警报文件属性数据保存
            CG_JIECHUJINGBAO_ME JingBaomI = new CG_JIECHUJINGBAO_ME();
            JingBaomI.JCWENJIANMING = strone;
            JingBaomI.JCQUYU = WainArea;
            JingBaomI.JCNEIRONG = WainType;
            JingBaomI.JCBIANHAO = biaohao;
            JingBaomI.JCJIBIE = level;
            JingBaomI.JCSHIJIAN = pbtime;
            JingBaomI.JCDANWEI = danwei;



            //CG_JIECHUJINGBAO_ME jctbl = new Model.CG_JIECHUJINGBAO_ME();
            //jctbl.JCWENJIANMING = strone;
            int numMe = -1;
            //判断数据库是不是有当前数据
            var sqlConME = new Sql_HT_CONTENTS();
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JieChuJingBaoME_AllData(JingBaomI);
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

            //判断数据库是不是有当前word文档
            System.Data.DataTable tblybddocuments = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JieChuJingBaoFILE_AllData(tbl);
            Sql_HT_CONTENTS content = new Sql_HT_CONTENTS();
            if (tblybddocuments.Rows.Count > 0)
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

        #endregion


        /// <summary>
        /// 删除上传的文件
        /// </summary>
        /// <param name="context"></param>
        private void DeleteFile(HttpContext context)
        {
            string pathfileName = context.Request["fileName"].ToString();
            try
            {
                string path = "预报单共享\\duanqi\\";//默认文件保存的路径
                string fullName = Path.Combine(HttpRuntime.AppDomainAppPath + path, pathfileName);

                if (System.IO.File.Exists(fullName))
                {
                    System.IO.File.Delete(fullName);
                }
                context.Response.Write("删除成功。");
            }
            catch (Exception e)
            {
                context.Response.Write("删除失败。" + e.Message);

            }
        }

        /// <summary>
        /// 判断警报等级
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        public string GetLevel(string No)
        {
            string Level = "";
            if(No != null && No != "")
            {
                string color = No.Split('_')[3].ToString().Substring(0,1);
                if(color == "R")
                {
                    Level = "红色警报";
                }
                else if (color == "Y")
                {
                    Level = "黄色警报";
                }
                else if (color == "B")
                {
                    Level = "蓝色警报";
                }
                else if (color == "O")
                {
                    Level = "橙色警报";
                }
            }
            return Level;
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