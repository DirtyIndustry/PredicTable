using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PredicTable.WebServiceClass
{
    public static class OperatorDOC
    {

        public static void replaceDocStringMarkers<T>(List<T> table, Document document) where T : class
        {
            foreach (T data in table)
            {
                Type type = typeof(T);
                string typename = type.Name;
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.Name == "PUBLISHDATE")
                    {
                        System.Diagnostics.Debug.WriteLine("GOGOGOGOGO!");
                        DateTime pubdate = Convert.ToDateTime(pi.GetValue(data, null));
                        document.Replace("{yyyy年MM月dd日}", pubdate.ToString("yyyy年MM月dd日"), true, false);
                    }
                    else
                    {
                        document.Replace("{" + pi.Name + "}", pi.GetValue(data, null).ToString(), true, true);
                    }
                }
            }
        }

        public static void replaceDocComments<T>(List<T> table, Document document) where T : class
        {
            List<DateTime> forecastdatelist = new List<DateTime>();
            foreach (T data in table)
            {
                PropertyInfo fdate = typeof(T).GetProperty("FORECASTDATE");
                if (fdate != null)
                {
                    forecastdatelist.Add(Convert.ToDateTime(fdate.GetValue(data, null)));
                }
            }
            if (forecastdatelist.Count > 0)
            {
                forecastdatelist = forecastdatelist.Distinct().ToList();
                forecastdatelist.Sort();
            }
            else
            {
                PropertyInfo fdate = typeof(T).GetProperty("PUBLISHDATE");
                DateTime pubdate = Convert.ToDateTime(fdate.GetValue(table[0], null));
                forecastdatelist.Add(pubdate.AddDays(1));
                forecastdatelist.Add(pubdate.AddDays(2));
                forecastdatelist.Add(pubdate.AddDays(3));
            }
            foreach (global::Spire.Doc.Fields.Comment comment in document.Comments)
            {
                string property = comment.Body.Paragraphs[0].Text;
                string tablename = comment.Body.Paragraphs[1].Text;
                string day = comment.Body.Paragraphs[2].Text;
                DateTime forecastdate;
                switch (day)
                {
                    case "DAY1":
                        forecastdate = forecastdatelist[0];
                        break;
                    case "DAY2":
                        forecastdate = forecastdatelist[1];
                        break;
                    case "DAY3":
                        forecastdate = forecastdatelist[2];
                        break;
                    default:
                        forecastdate = forecastdatelist[0];
                        break;
                }
                string area = comment.Body.Paragraphs[3].Text;
                string areakey = "";
                string areavalue = "";
                if (area != "")
                {
                    areakey = area.Split('=')[0];
                    areavalue = area.Split('=')[1];
                }
                foreach (T data in table)
                {
                    Type type = typeof(T);
                    string typename = type.Name;
                    PropertyInfo FORECASTDATE = type.GetProperty("FORECASTDATE");
                    PropertyInfo FORECASTAREA = type.GetProperty(areakey);
                    PropertyInfo Target = type.GetProperty(property);
                    bool typecorrect = false;
                    bool datecorrect = false;
                    bool areacorrect = false;
                    if (typename == tablename)
                    {
                        typecorrect = true;
                    }
                    if (day == "")
                    {
                        datecorrect = true;
                    }
                    else if (FORECASTAREA == null)
                    {
                        datecorrect = true;
                    }
                    else if (Convert.ToDateTime(FORECASTDATE.GetValue(data, null)) == forecastdate)
                    {
                        datecorrect = true;
                    }
                    if (area == "")
                    {
                        areacorrect = true;
                    }
                    else if (FORECASTAREA == null)
                    {
                        areacorrect = true;
                    }
                    else if (FORECASTAREA.GetValue(data, null).ToString() == areavalue)
                    {
                        areacorrect = true;
                    }
                    if (typecorrect & datecorrect & areacorrect)
                    {
                        if (Target.Name == "FORECASTDATE")
                        {
                            DateTime datetime = (DateTime)(Target.GetValue(data, null));
                            comment.OwnerParagraph.Text = datetime.Month + "月" + datetime.Day + "日";
                        }
                        else if (Target.Name == "PUBLISHDATE")
                        {
                            DateTime datetime = (DateTime)(Target.GetValue(data, null));
                            string datestring = datetime.Year + "年" + datetime.Month + "月" + datetime.Day + "日";
                            PropertyInfo PUBLISHHOUR = type.GetProperty("PUBLISHHOUR");
                            if (PUBLISHHOUR != null)
                            {
                                datestring += PUBLISHHOUR.GetValue(data, null).ToString() + "时";
                            }
                            comment.OwnerParagraph.Text = datestring;
                        }
                        else
                        {
                            try
                            {
                                comment.OwnerParagraph.Text = Target.GetValue(data, null).ToString();
                            }
                            catch (Exception e)
                            {
                                System.Diagnostics.Debug.WriteLine(e);
                            }
                            // comment.OwnerParagraph.Replace("{a}", Target.GetValue(data, null).ToString(), false, false);
                        }
                    }
                }
            }
        }

    }
}