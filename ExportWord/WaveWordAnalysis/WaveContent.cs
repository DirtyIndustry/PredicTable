//**********************************************************************************************

//文件名(File Name)：                   WaveContent

//作者(Author)：                        sl

//日期(Create Date)：                   2017-02-07

//修改记录(Revision History)：
//        R1：
//         修改作者：                   sl  
//         修改日期：                   2017-02-07
//         修改理由：                   添加
//                                      海浪属性解析、入库
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

namespace PredicTable.ExportWord.WaveWordAnalysis
{
    public class WaveContent
    {
        /// <summary>
        /// 海浪消息数据解析、入库
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
            XiaoXi waveXX = new XiaoXi(filePath);
            waveXX.assignmentTable();

            //字段解析
             CG_HT_XIAOXI_CONTENT Contentvalue = new CG_HT_XIAOXI_CONTENT();
            Contentvalue.IPHONE = waveXX.tel;
            Contentvalue.LINKMAN = waveXX.link;
            //Contentvalue.SENTTO = waveXX.fawang;
            var fw = waveXX.fawang;
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
                Contentvalue.SENTTO = waveXX.fawang;
            }
            Contentvalue.XXWENJIANMING = filePathName;
            Contentvalue.DATETIME = waveXX.publish_time;//DateTime.Now;
            Contentvalue.XXTITLE = "海浪消息";
            Contentvalue.CONTENT = waveXX.content;
            // 获取签发图片地址
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
                int res = hbwaring.XXruku(filePath, path, filePathName, WainArea, filePathName.Split('_')[3], WainType, danwei, waveXX.publish_time);
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
        /// 海浪警报属性解析、入库
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="path"></param>
        /// <param name="filePathName"></param>
        /// <param name="WainArea"></param>
        /// <param name="FaWangbz"></param>
        /// <returns></returns>
        public string AddJBInfo(string filePath, string path, string filePathName, string WainArea, string FaWangbz, string WainType, string danwei)
        {
            string returnStr = "";

            JingBao waveJB = new JingBao(filePath);
            waveJB.assignmentTable();

            //字段解析
            CG_HT_JINGBAO_CONTENT Contentvalue = new CG_HT_JINGBAO_CONTENT();
            Contentvalue.IPHONE = waveJB.tel;
            Contentvalue.LINKMAN = waveJB.link;
            //Contentvalue.SENTTO = waveJB.fawang;
            var fw = waveJB.fawang;
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
                Contentvalue.SENTTO = waveJB.fawang;
            }
            Contentvalue.JBWENJIANMING = filePathName;
            Contentvalue.DATETIME = waveJB.publish_time;
            Contentvalue.JBTITLE = waveJB.title;
            Contentvalue.CONTENTTABLE = waveJB.biaohao;
            Contentvalue.CONTENT = waveJB.content;
            Contentvalue.JBREMARKS =  "";
            //Contentvalue.PICTURE = waveJB.picture;
            //Contentvalue.WAVEJBIMG = new byte[1];
            //Contentvalue.WAVEJBIMG = waveJB.picture;
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

            int num = -1;
            var sqlConC = new Sql_HT_CONTENTS();

            #region
            //if (Contentvalue.PICTURE != null && Contentvalue.PICTURE.Length > 1)
            //{
            //    sql_WarningImgUploader imgUpLoader = new sql_WarningImgUploader();
            //    DataTable dtExist = new DataTable();
            //    string docName = filePathName.Split('.')[0] + ".doc";
            //    //判断是否存在
            //    dtExist = imgUpLoader.GetImgInfo("CG_HT_JINGBAO_CONTENT", "JBWENJIANMING", docName);
            //    if (dtExist != null && dtExist.Rows.Count > 0)
            //    {
            //        imgUpLoader.UpdateImg("CG_HT_JINGBAO_CONTENT", "JBWENJIANMING", "PICTURE", docName, Contentvalue.PICTURE);
            //        //result = "修改警报图片成功";
            //    }
            //    else
            //    {
            //        imgUpLoader.InsertImg("CG_HT_JINGBAO_CONTENT", "JBWENJIANMING", "PICTURE", docName, Contentvalue.PICTURE);
            //        //  result = "添加警报图片成功";
            //    }
            //}
            //else
            //{
            //    Contentvalue.PICTURE = new byte[1];
            //}
            #endregion
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
                int res = hbwaring.JBruku(filePath, path, filePathName, WainArea, filePathName.Split('_')[3], level, WainType, danwei, waveJB.publish_time);
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
        /// 海浪解除警报属性解析、入库
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="path"></param>
        /// <param name="filePathName"></param>
        /// <param name="WainArea"></param>
        /// <param name="FaWangbz"></param>
        /// <returns></returns>
        public string AddJCInfo(string filePath, string path, string filePathName, string WainArea, string FaWangbz, string WainType,string danwei)
        {
            string returnStr = "";
            JieChuJingBao waveJC = new JieChuJingBao(filePath);
            waveJC.assignmentTable();

            //字段解析
            CG_HT_JIECHUJINGBAO_CONTENT Contentvalue = new CG_HT_JIECHUJINGBAO_CONTENT();
            Contentvalue.IPHONE = waveJC.tel;
            Contentvalue.LINKMAN = waveJC.link;
            //Contentvalue.SENTTO = waveJC.fawang;
            var fw = waveJC.fawang;
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
                Contentvalue.SENTTO = waveJC.fawang;
            }
            string biaohao = waveJC.biaohao;
            Contentvalue.JCJBWENJIANMING = filePathName;
            Contentvalue.DATETIME = waveJC.publish_time;
            Contentvalue.CONTENTTABLE = waveJC.biaohao;
            Contentvalue.JCTITLE = waveJC.title;
            Contentvalue.CONTENT = waveJC.content;
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
                int res = hbwaring.JCruku(filePath, path, filePathName, WainArea, filePathName.Split('_')[3], level, WainType, danwei, waveJC.publish_time);
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