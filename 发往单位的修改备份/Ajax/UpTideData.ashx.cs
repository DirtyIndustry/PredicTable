using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// Summary description for UpTideData
    /// </summary>
    public class UpTideData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var msg = "";

            try
            {
                var folderPath = HttpContext.Current.Server.MapPath("/") + "\\TideData\\";
                var files = context.Request.Files;
                string[] fileNames = new string[files.Count];
                int[] result = new int[files.Count];
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    var fileName = file.FileName;
                    if (fileName == "" || string.IsNullOrEmpty(fileName) || file.ContentLength == 0) continue;
                    var stream = file.InputStream;
                    var fileDisplayName = fileName.Substring(0, fileName.IndexOf('.'));

                    result[i] = ImportData.ImportDataToDB(stream, fileDisplayName);

                    //file.SaveAs(folderPath + file.FileName);
                    fileNames[i] = file.FileName;
                }
                //result = ImportDataToDB(folderPath, fileNames);
                for (int i = 0; i < result.Length;i++)
                {
                    if(result[i]==0)
                    {
                        var failFileName = fileNames[i];
                        msg = msg + failFileName+",";
                    }
                }

            }
            catch (HttpException exp)
            {
                //WriteLog.Write("上传潮汐预报数据出现异常！" + exp.Message + "\r\n" + exp.StackTrace);
                throw exp;
            }
            if (msg!="")
                msg = msg + " 数据导入失败";
            else
                msg = "数据导入成功";

            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        ////导入数据到数据库
        //int ImportDataToDB(string folderPath,string[] fileNames)
        //{
        //    try
        //    {
        //        var tides = new List<HT_YB_Tide>();
        //        for (int i = 0; i < fileNames.Length; i++)
        //        {
        //            var fileName = fileNames[i];
        //            if (fileName == "" || string.IsNullOrEmpty(fileName)) continue;

        //            var fileDisplayName = fileName.Substring(0, fileName.IndexOf('.'));
        //            var filePath = folderPath + fileNames[i];
        //            var lines = File.ReadAllLines(filePath);

        //            for (int j = 0; j < lines.Length; j++)
        //            {
        //                var tide = new HT_YB_Tide();

        //                var items = lines[j].Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        //                for (int k = 0; k < items.Length; k++)
        //                {
        //                    tide.STATION = fileDisplayName;
        //                    tide.PREDICTIONDATE = items[0];
        //                    tide.H0 = int.Parse(items[1]);
        //                    tide.H1 = int.Parse(items[2]);
        //                    tide.H2 = int.Parse(items[3]);
        //                    tide.H3 = int.Parse(items[4]);
        //                    tide.H4 = int.Parse(items[5]);
        //                    tide.H5 = int.Parse(items[6]);
        //                    tide.H6 = int.Parse(items[7]);
        //                    tide.H7 = int.Parse(items[8]);
        //                    tide.H8 = int.Parse(items[9]);
        //                    tide.H9 = int.Parse(items[10]);
        //                    tide.H10 = int.Parse(items[11]);
        //                    tide.H11 = int.Parse(items[12]);
        //                    tide.H12 = int.Parse(items[13]);
        //                    tide.H13 = int.Parse(items[14]);
        //                    tide.H14 = int.Parse(items[15]);
        //                    tide.H15 = int.Parse(items[16]);
        //                    tide.H16 = int.Parse(items[17]);
        //                    tide.H17 = int.Parse(items[18]);
        //                    tide.H18 = int.Parse(items[19]);
        //                    tide.H19 = int.Parse(items[20]);
        //                    tide.H20 = int.Parse(items[21]);
        //                    tide.H21 = int.Parse(items[22]);
        //                    tide.H22 = int.Parse(items[23]);
        //                    tide.H23 = int.Parse(items[24]);
        //                    tide.FSTHIGHWIDETIME = items[25];
        //                    tide.FSTHIGHWIDEHEIGHT = items[26];
        //                    tide.FSTLOWWIDETIME = items[27];
        //                    tide.FSTLOWWIDEHEIGHT = items[28];
        //                    tide.SCDHIGHWIDETIME = items[29];
        //                    tide.SCDHIGHWIDEHEIGHT = items[30];
        //                    tide.SCDLOWWIDETIME = items[31];
        //                    tide.SCDLOWWIDEHEIGHT = items[32];
        //                }

        //                tides.Add(tide);
        //            }

        //        }

        //        var sqlTide = new Sql_HT_YB_Tide();
        //        foreach (var tide in tides)
        //            sqlTide.DelTideDataForStationAndPredictionDate(tide.STATION, tide.PREDICTIONDATE);
        //        var result = 1;

        //        foreach (var tide in tides)
        //        {
        //            result = sqlTide.AddTidesData(tide);
        //        }

        //        return result;
        //        //return  sqlTide.AddTidesData2(tides);
        //    }
        //    catch(Exception exp)
        //    {
        //        //WriteLog.Write("从文件读取潮汐预报数据出现异常！" + exp.Message + "\r\n" + exp.StackTrace);

        //        throw exp;
        //    }

        //}
    }
}