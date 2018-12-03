using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.ExportWord.NineteenWord
{
    public class staticVar
    {
        public Dictionary<string, string> city_site = new Dictionary<string, string>();
        
        public string[] en_Link;
        public staticVar()
        {
            try
            {
                StreamReader file = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "linkman", Encoding.UTF8);
                StringBuilder sb = new System.Text.StringBuilder();
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Replace(" ", "") != "")
                    {
                        sb.Append(line);
                    }
                }
                en_Link = sb.ToString().Split(';');
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                StreamReader file = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "citysite", Encoding.Default);
                StringBuilder sb = new System.Text.StringBuilder();
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Replace(" ", "") != "")
                    {
                        sb.Append(line);
                    }
                }
                string[] city = sb.ToString().Split(';');
                foreach (string str in city)
                {
                    string[] cist = str.Split(',');
                    if (cist.Length == 2)
                        city_site.Add(cist[0], cist[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}