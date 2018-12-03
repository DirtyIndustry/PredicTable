//**********************************************************************************************

//文件名(File Name)：                   StormContent

//作者(Author)：                        sl

//日期(Create Date)：                   2017-02-07

//修改记录(Revision History)：
//        R1：
//         修改作者：                   sl  
//         修改日期：                   2017-02-07
//         修改理由：                   添加
//                                      风暴潮属性解析、入库
//                                      
//
//**********************************************************************************************
using PredicTable.Ajax;
using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace PredicTable.ExportWord.StormWordAnalysis
{
    public class StormContent
    {
        /// <summary>
        /// 风暴潮消息数据解析、入库
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="path"></param>
        /// <param name="filePathName"></param>
        /// <param name="WainArea"></param>
        /// <param name="FaWangbz">发往备注</param>
        /// <returns></returns>
        public string AddXXInfo(string filePath, string path, string filePathName, string WainArea, string FaWangbz,string WainType,string danwei)
        {
            string returnStr = "";
            XiaoXi stormXX = new XiaoXi(filePath);
            stormXX.assignmentTable();

            //字段解析
            CG_HT_XIAOXI_CONTENT Contentvalue = new CG_HT_XIAOXI_CONTENT();
            Contentvalue.IPHONE = stormXX.tel;
            Contentvalue.LINKMAN = stormXX.link;
            //Contentvalue.SENTTO = stormXX.fawang;
            var fw = stormXX.fawang;
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
                Contentvalue.SENTTO = stormXX.fawang;
            }
            Contentvalue.XXWENJIANMING = filePathName;
            Contentvalue.DATETIME = stormXX.publish_time;
            Contentvalue.XXTITLE = "风暴潮消息";
            Contentvalue.CONTENT = stormXX.content;
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
            }
            else
            {
                num = sqlConC.AddXiaoXiContent(Contentvalue);
            }

            if (num > 0)
            {
                returnStr += "消息文件属性上传成功。";
            }
            else
            {
                returnStr += "消息文件属性上传失败。";
            }
            if (num > 0)
            {
                HBWarningUploader hbwaring = new HBWarningUploader();
                int res = hbwaring.XXruku(filePath, path, filePathName, WainArea, filePathName.Split('_')[3], WainType, danwei,stormXX.publish_time);
                if (res > 0)
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
            //发往备注保存
            CommonSendUnit sunit = new CommonSendUnit();
            string ret = sunit.resultSendUnitbz(filePathName, Contentvalue.SENTTO, FaWangbz);
            return returnStr;
        }

        /// <summary>
        /// 风暴潮警报属性解析、入库
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="path"></param>
        /// <param name="filePathName"></param>
        /// <param name="WainArea"></param>
        /// <param name="FaWangbz"></param>
        /// <returns></returns>
        public string AddJBInfo(string filePath, string path, string filePathName, string WainArea, string FaWangbz, string WainType,string danwei)
        {
            string returnStr = "";

            JingBao stormJB = new JingBao(filePath);
            stormJB.assignmentTable();

            //字段解析
            CG_HT_JINGBAO_CONTENT Contentvalue = new CG_HT_JINGBAO_CONTENT();
            Contentvalue.IPHONE = stormJB.tel;
            Contentvalue.LINKMAN = stormJB.link;
            Contentvalue.SENTTO = stormJB.fawang;
            var fw = stormJB.fawang;
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
                Contentvalue.SENTTO = stormJB.fawang;
            }
            Contentvalue.JBWENJIANMING = filePathName;
            Contentvalue.DATETIME = stormJB.publish_time;
            Contentvalue.JBTITLE = stormJB.title;
            Contentvalue.CONTENTTABLE = stormJB.biaohao;
            Contentvalue.CONTENT = stormJB.content;
            Contentvalue.JBREMARKS = "";
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
                //Contentvalue.PICTURE = byData;//图片
            }
            string[,] table_str_1 = stormJB.table_str_1;

            int num = -1;
            var sqlConC = new Sql_HT_CONTENTS();

            //判断数据库是不是有数据
            CG_HT_JINGBAO_CONTENT tblC = new Model.CG_HT_JINGBAO_CONTENT();
            tblC.JBWENJIANMING = filePathName;
            System.Data.DataTable tblybddocumentC = (System.Data.DataTable)new Sql_HT_CONTENTS().get_JingBaoCON_AllData(tblC);
            if (tblybddocumentC.Rows.Count > 0)
            {
                num = sqlConC.UpdateJingBaoContents(Contentvalue);
            }
            else
            {
                num = sqlConC.AddJingBaoContents(Contentvalue);
            }
            if (num > 0)
            {
                //文件内容保存成功，保存word中表格数据
                StromTableModel stromModel = new StromTableModel();
                List<StromTableModel> list = new List<StromTableModel>();
                if(table_str_1 != null && table_str_1.Length > 0)
                {
                    list = this.getTableData(table_str_1);
                    if (list.Count > 0)
                    {
                        SaveTableData(list, filePathName);
                    }
                }
                
                returnStr += "警报文件属性上传成功。";
            }
            else
            {
                returnStr += "警报文件属性上传失败。";
            }
            if (num > 0)
            {
                HBWarningUploader hbwaring = new HBWarningUploader();
                string level = hbwaring.GetLevel(filePathName);
                int res = hbwaring.JBruku(filePath, path, filePathName, WainArea, filePathName.Split('_')[3], level, WainType, danwei, stormJB.publish_time);
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
            //发往备注保存
            CommonSendUnit sunit = new CommonSendUnit();
            string ret = sunit.resultSendUnitbz(filePathName, Contentvalue.SENTTO, FaWangbz);
            return returnStr;
        }
        /// <summary>
        /// 表格数据映射
        /// </summary>
        /// <param name="table_str_1"></param>
        /// <returns></returns>
        private List<StromTableModel> getTableData(string[,] table_str_1)
        {
            List<StromTableModel> list = new List<StromTableModel>();
            var stromrow = table_str_1.GetLength(0);//计算表格行数
            var stromcol = table_str_1.GetLength(1);//计算表格列数
            for (int r = 1; r < stromrow; r++)
            {
                StromTableModel stromModel = new StromTableModel();
                //cknessModel.SITE = table_str_3[g - 1, 0].ToString();
                stromModel.STATION = table_str_1[r, 0].ToString();
                stromModel.PUBLISHTIME = table_str_1[r, 1].ToString();
                stromModel.HIGHTIME = table_str_1[r, 2].ToString();
                stromModel.HIGHVALUE = table_str_1[r, 3].ToString();
                stromModel.WARNINGTIDEVALUE = table_str_1[r, 4].ToString();
                stromModel.WARNINGLEVEL = table_str_1[r, 5].ToString();
                list.Add(stromModel);
            }
            return list;
        }
        /// <summary>
        /// 保存表格数据文件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="WENJIANMING"></param>
        private void SaveTableData(List<StromTableModel> list, string WENJIANMING)
        {
            sql_StromTable stromTable = new sql_StromTable();
            int result = 0;
            DataTable dt = stromTable.GetTableData((StromTableModel)list[0], WENJIANMING);
            if(dt!=null && dt.Rows.Count > 0)
            {
                stromTable.DeleteTableData(WENJIANMING);
            }
            for (int i = 0; i < list.Count; i++)
            {
                result = stromTable.InsertTableData((StromTableModel)list[i], WENJIANMING);
                //DataTable dt = stromTable.GetTableData((StromTableModel)list[i], WENJIANMING);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    result = stromTable.UpdateTableData((StromTableModel)list[i], WENJIANMING);
                //}
                //else
                //{
                //    result = stromTable.InsertTableData((StromTableModel)list[i], WENJIANMING);
                //}
            }
        }
        /// <summary>
        /// 风暴潮解除警报属性解析、入库
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="path"></param>
        /// <param name="filePathName"></param>
        /// <param name="WainArea"></param>
        /// <param name="FaWangbz"></param>
        /// <returns></returns>
        public string AddJCInfo(string filePath, string path, string filePathName, string WainArea, string FaWangbz,string WainType,string danwei)
        {
            string returnStr = "";
            JieChuJingBao stormJC = new JieChuJingBao(filePath);
            stormJC.assignmentTable();

            //字段解析
            CG_HT_JIECHUJINGBAO_CONTENT Contentvalue = new CG_HT_JIECHUJINGBAO_CONTENT();
            Contentvalue.IPHONE = stormJC.tel;
            Contentvalue.LINKMAN = stormJC.link;
            //Contentvalue.SENTTO = stormJC.fawang;
            var fw = stormJC.fawang;
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
                Contentvalue.SENTTO = stormJC.fawang;
            }
            string biaohao = stormJC.biaohao;
            Contentvalue.JCJBWENJIANMING = filePathName;
            Contentvalue.DATETIME = stormJC.publish_time;
            Contentvalue.CONTENTTABLE = stormJC.biaohao;
            Contentvalue.JCTITLE = stormJC.title;
            Contentvalue.CONTENT = stormJC.content;
            Contentvalue.JCREMARKS = "";
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
            //解除警报内容表数据保存
            CG_HT_JIECHUJINGBAO_CONTENT tblC = new Model.CG_HT_JIECHUJINGBAO_CONTENT();
            tblC.JCJBWENJIANMING = filePathName;
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

            if (num > 0)
            {
                returnStr += "解除警报文件属性上传成功。";
            }
            else
            {
                returnStr += "解除警报文件属性上传失败。";
            }
            if (num > 0)
            {
                HBWarningUploader hbwaring = new HBWarningUploader();
                string level = hbwaring.GetLevel(filePathName);
                int res = hbwaring.JCruku(filePath, path, filePathName, WainArea, filePathName.Split('_')[3], level, WainType, danwei, stormJC.publish_time);
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
            //发往备注保存
            CommonSendUnit sunit = new CommonSendUnit();
            string ret = sunit.resultSendUnitbz(filePathName, Contentvalue.SENTTO, FaWangbz);
            return returnStr;
        }
    }
}