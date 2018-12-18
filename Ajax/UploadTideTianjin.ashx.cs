using Newtonsoft.Json;
using PredicTable.WebServiceClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

namespace PredicTable.Ajax
{
    /// <summary>
    /// Summary description for UploadTideTianjin
    /// </summary>
    public class UploadTideTianjin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            List<ModelUploadTide> datalist = new List<ModelUploadTide>();
            int insertcount = 0;
            int updatecount = 0;
            if (context.Request.Files.Count > 0)
            {
                string folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles", "tidedata");
                string filepath = "";
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    string filename = Path.GetFileName(context.Request.Files[i].FileName);
                    filepath = Path.Combine(folderpath, filename);
                    try
                    {
                        context.Request.Files[i].SaveAs(filepath);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
                
                try
                {
                    using (StreamReader reader = new StreamReader(filepath, OperatorTXT.GetEncoding(filepath)))
                    {
                        string line;
                        string location = "";
                        string year = "";
                        string month = "";
                        int linecount = 0;
                        while ((line = reader.ReadLine()) != null)
                        {
                            linecount++;
                            switch (linecount)
                            {
                                case 1:
                                    location = getLocation(line.Trim());
                                    //System.Diagnostics.Debug.WriteLine("Location: " + location);
                                    break;
                                case 2:
                                    year = line.Substring(0, 4).Trim();
                                    month = line.Substring(line.Length - 3).Trim();
                                    month = month.Split('月')[0];
                                    //System.Diagnostics.Debug.WriteLine("Year: " + year);
                                    //System.Diagnostics.Debug.WriteLine("Month: " + month);
                                    break;
                                case 3:
                                case 4:
                                    break;
                                default:
                                    if (line.StartsWith("时区"))
                                    {
                                        // System.Diagnostics.Debug.WriteLine("时区行");
                                        linecount = 0;
                                    }
                                    else
                                    {
                                        datalist.Add(getUploadTide(line, location, year, month));
                                    }
                                    break;
                            }
                        }
                        reader.Close();
                    }
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                string sqlselect = "SELECT * FROM HT_YB_TIDE WHERE STATION='@STATION' AND PREDICTIONDATE=to_date('@PREDICTIONDATE','yyyy-mm-dd hh24@mi@ss')";
                string sqlinsert = "INSERT INTO HT_YB_TIDE "
                    + "(STATION, PREDICTIONDATE, H0, H1, H2, H3, H4, H5, H6, H7, H8, H9, H10, H11, H12, H13, H14, H15, H16, H17, H18, H19, H20, H21, H22, H23, FSTHIGHWIDETIME, FSTHIGHWIDEHEIGHT, FSTLOWWIDETIME, FSTLOWWIDEHEIGHT, SCDHIGHWIDETIME, SCDHIGHWIDEHEIGHT, SCDLOWWIDETIME, SCDLOWWIDEHEIGHT) "
                    + "VALUES ('@STATION', to_date('@PREDICTIONDATE','yyyy-mm-dd hh24@mi@ss'), '@H0', '@H1', '@H2', '@H3', '@H4', '@H5', '@H6', '@H7', '@H8', '@H9', '@H10', '@H11', '@H12', '@H13', '@H14', '@H15', '@H16', '@H17', '@H18', '@H19', '@H20', '@H21', '@H22', '@H23', '@FSTHIGHWIDETIME', '@FSTHIGHWIDEHEIGHT', '@FSTLOWWIDETIME', '@FSTLOWWIDEHEIGHT', '@SCDHIGHWIDETIME', '@SCDHIGHWIDEHEIGHT', '@SCDLOWWIDETIME', '@SCDLOWWIDEHEIGHT')";
                string sqlupdate = "UPDATE HT_YB_TIDE "
                    + "SET STATION='@STATION', PREDICTIONDATE=to_date('@PREDICTIONDATE','yyyy-mm-dd hh24@mi@ss'), H0='@H0', H1='@H1', H2='@H2', H3='@H3', H4='@H4', H5='@H5', H6='@H6', H7='@H7', H8='@H8', H9='@H9', H10='@H10', H11='@H11', H12='@H12', H13='@H13', H14='@H14', H15='@H15', H16='@H16', H17='@H17', H18='@H18', H19='@H19', H20='@H20', H21='@H21', H22='@H22', H23='@H23', "
                    + "FSTHIGHWIDETIME='@FSTHIGHWIDETIME', FSTHIGHWIDEHEIGHT='@FSTHIGHWIDEHEIGHT', FSTLOWWIDETIME='@FSTLOWWIDETIME', FSTLOWWIDEHEIGHT='@FSTLOWWIDEHEIGHT', SCDHIGHWIDETIME='@SCDHIGHWIDETIME', SCDHIGHWIDEHEIGHT='@SCDHIGHWIDEHEIGHT', SCDLOWWIDETIME='@SCDLOWWIDETIME', SCDLOWWIDEHEIGHT='@SCDLOWWIDEHEIGHT' "
                    + "WHERE STATION='@STATION' AND PREDICTIONDATE=to_date('@PREDICTIONDATE','yyyy-mm-dd hh24@mi@ss')";
                insertcount = 0;
                updatecount = 0;

                string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
                DbProviderFactory Provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
                DbConnection connection = Provider.CreateConnection();
                connection.ConnectionString = ConnectionStr;
                try
                {
                    connection.Open();
                    foreach (ModelUploadTide daydata in datalist)
                    {
                        List<DbParameter> parameters = OperatorSQL.buildParameters(daydata);
                        DataTable dt = OperatorSQL.queryData(sqlselect, parameters, connection);
                        
                        if (dt.Rows.Count > 0)
                        {
                            updatecount += OperatorSQL.executeSql(sqlupdate, parameters, connection);
                        }
                        else
                        {
                            insertcount += OperatorSQL.executeSql(sqlinsert, parameters, connection);
                        }
                    }
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            string report = "";
            if (datalist.Count > 0)
            {
                report = "成功记录" + datalist[0].STATION + "站点潮汐数据" + sqlcount + "条";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(report);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string getLocation(string name)
        {
            string result = "";
            switch (name)
            {
                case "老北河口":
                    result = "tj_lhbk";
                    break;
                case "锦州港（笔架山）":
                    result = "tj_jzg";
                    break;
                default: break;
            }
            return result;
        }

        private ModelUploadTide getUploadTide(string line, string location, string year, string month)
        {
            ModelUploadTide result = new ModelUploadTide();
            result.STATION = location;
            List<string> datalist = line.Split(new char[1]{' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
            string day = datalist[0].Trim();
            //System.Diagnostics.Debug.WriteLine("Day: " + day);
            try
            {
                result.PREDICTIONDATE = Convert.ToDateTime(year + "/" + month + "/" + day);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(line);
            }
            result.H0 = datalist[1].Trim();
            result.H1 = datalist[2].Trim();
            result.H2 = datalist[3].Trim();
            result.H3 = datalist[4].Trim();
            result.H4 = datalist[5].Trim();
            result.H5 = datalist[6].Trim();
            result.H6 = datalist[7].Trim();
            result.H7 = datalist[8].Trim();
            result.H8 = datalist[9].Trim();
            result.H9 = datalist[10].Trim();
            result.H10 = datalist[11].Trim();
            result.H11 = datalist[12].Trim();
            result.H12 = datalist[13].Trim();
            result.H13 = datalist[14].Trim();
            result.H14 = datalist[15].Trim();
            result.H15 = datalist[16].Trim();
            result.H16 = datalist[17].Trim();
            result.H17 = datalist[18].Trim();
            result.H18 = datalist[19].Trim();
            result.H19 = datalist[20].Trim();
            result.H20 = datalist[21].Trim();
            result.H21 = datalist[22].Trim();
            result.H22 = datalist[23].Trim();
            result.H23 = datalist[24].Trim();
            List<KeyValuePair<string, string>> tidelist = new List<KeyValuePair<string, string>>();
            string key = "";
            for (int i = 25; i < datalist.Count; i++)
            {
                if (datalist[i].Trim() != "")
                {
                    if (key == "")
                    {
                        key = datalist[i].Trim();
                    } else
                    {
                        tidelist.Add(new KeyValuePair<string, string>(key, datalist[i].Trim()));
                        key = "";
                    }
                }
            }
            int highcount = 0;
            int lowcount = 0;
            foreach(KeyValuePair<string, string> pair in tidelist)
            {
                //System.Diagnostics.Debug.WriteLine(pair.Key + ": " + pair.Value);
                int index = Convert.ToInt32(pair.Key.Substring(0, 2)) + 1;
                int hourvalue = Convert.ToInt32(datalist[index].Trim());
                int tidevalue = Convert.ToInt32(pair.Value);
                if (hourvalue > tidevalue)
                {
                    if (lowcount == 0)
                    {
                        result.FSTLOWWIDETIME = pair.Key;
                        result.FSTLOWWIDEHEIGHT = pair.Value;
                        lowcount++;
                    }
                    else if (lowcount == 1)
                    {
                        result.SCDLOWWIDETIME = pair.Key;
                        result.SCDLOWWIDEHEIGHT = pair.Value;
                        lowcount++;
                    }
                }
                else
                {
                    if (highcount == 0)
                    {
                        result.FSTHIGHWIDETIME = pair.Key;
                        result.FSTHIGHWIDEHEIGHT = pair.Value;
                        highcount++;
                    }
                    else if (highcount == 1)
                    {
                        result.SCDHIGHWIDETIME = pair.Key;
                        result.SCDHIGHWIDEHEIGHT = pair.Value;
                        highcount++;
                    }
                }
            }
            return result;
        }
    }
}