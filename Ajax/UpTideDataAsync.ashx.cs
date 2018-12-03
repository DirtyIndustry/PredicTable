using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// Summary description for UpTideDataAsync
    /// </summary>
    public class UpTideDataAsync : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var stream = context.Request.InputStream;

            var fileName = context.Request.Params["fileName"];
            var fileDisplayName = fileName.Substring(0, fileName.IndexOf('.'));
            var result = ImportData.ImportDataToDB(stream, fileDisplayName);

            var msg = "Hello";
            if (result > 0)
                msg = fileName + "数据导入成功";
            else
                msg = "操作出现异常";

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


    }

    /// <summary>
    /// 
    /// </summary>
    public static class ImportData
    {
        //导入数据到数据库
        public static int ImportDataToDB(Stream stream, string fileDisplayName) //HT_YB_Tide newTide)
        {
            var tides = new List<HT_YB_Tide>();
            using (StreamReader sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    var tide = ConvertLineToTide(fileDisplayName, line);
                    tides.Add(tide);
                }
            }

            var sqlTide = new Sql_HT_YB_Tide();
            foreach (var tide in tides)
                sqlTide.DelTideDataForStationAndPredictionDate(tide.STATION, tide.PREDICTIONDATE);
            return sqlTide.AddTidesDataBatch(tides);

        }

        //导入数据到数据库
        static HT_YB_Tide ConvertLineToTide(string fileName, string line)
        {
            var tide = new HT_YB_Tide();
            var items = line.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int k = 0; k < items.Length; k++)
            {
                tide.STATION = fileName;
                tide.PREDICTIONDATE = items[0];
                tide.H0 = int.Parse(items[1]);
                tide.H1 = int.Parse(items[2]);
                tide.H2 = int.Parse(items[3]);
                tide.H3 = int.Parse(items[4]);
                tide.H4 = int.Parse(items[5]);
                tide.H5 = int.Parse(items[6]);
                tide.H6 = int.Parse(items[7]);
                tide.H7 = int.Parse(items[8]);
                tide.H8 = int.Parse(items[9]);
                tide.H9 = int.Parse(items[10]);
                tide.H10 = int.Parse(items[11]);
                tide.H11 = int.Parse(items[12]);
                tide.H12 = int.Parse(items[13]);
                tide.H13 = int.Parse(items[14]);
                tide.H14 = int.Parse(items[15]);
                tide.H15 = int.Parse(items[16]);
                tide.H16 = int.Parse(items[17]);
                tide.H17 = int.Parse(items[18]);
                tide.H18 = int.Parse(items[19]);
                tide.H19 = int.Parse(items[20]);
                tide.H20 = int.Parse(items[21]);
                tide.H21 = int.Parse(items[22]);
                tide.H22 = int.Parse(items[23]);
                tide.H23 = int.Parse(items[24]);
                //tide.FSTHIGHWIDETIME = items[25].Replace (":","");
                //tide.FSTHIGHWIDEHEIGHT = items[26];
                //tide.FSTLOWWIDETIME = items[27].Replace(":", "");
                //tide.FSTLOWWIDEHEIGHT = items[28];
                //tide.SCDHIGHWIDETIME = items[29].Replace(":", "");
                //tide.SCDHIGHWIDEHEIGHT = items[30];
                //tide.SCDLOWWIDETIME = items[31].Replace(":", "");
                //tide.SCDLOWWIDEHEIGHT = items[32];



                tide.FSTHIGHWIDETIME = items[25];
                tide.FSTHIGHWIDEHEIGHT = items[26];
                tide.FSTLOWWIDETIME = items[27];
                tide.FSTLOWWIDEHEIGHT = items[28];
                tide.SCDHIGHWIDETIME = items[29];
                tide.SCDHIGHWIDEHEIGHT = items[30];
                tide.SCDLOWWIDETIME = items[31];
                tide.SCDLOWWIDEHEIGHT = items[32];
            }

            return tide;

        }

    }

}