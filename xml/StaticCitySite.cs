using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class StaticCitySite
    {
        public Dictionary<string, string> city_site = new Dictionary<string, string>();
        public StaticCitySite()
        {
            city_site.Add("青岛港", "370200");
            city_site.Add("营口港", "210800");
            city_site.Add("潍坊港", "370700");
            city_site.Add("渤海", "070100");
            city_site.Add("潍坊近海", "370700");
            city_site.Add("威海市区", "371000");
            city_site.Add("文登", "371081");
            city_site.Add("文登区", "371081");
            city_site.Add("成山头", "371082");
            city_site.Add("荣成成山头", "371082");
            city_site.Add("石岛", "371082");
            city_site.Add("荣成石岛", "371082");
            city_site.Add("乳山", "371083");
            city_site.Add("乳山市", "371083");
            city_site.Add("青岛市区海水浴场", "370200");
            city_site.Add("青岛近海", "370200");
            city_site.Add("青岛沿岸", "370200");
            city_site.Add("日照", "371100");
            city_site.Add("青岛", "370200");
            city_site.Add("威海", "371000");
            city_site.Add("烟台", "370600");
            city_site.Add("潍坊", "370700");
            city_site.Add("东营", "370500");
            city_site.Add("滨州", "371600");
            city_site.Add("日照近海", "371100");
            city_site.Add("威海近海", "371000");
            city_site.Add("烟台近海", "370600");
            city_site.Add("东营近海", "370500");
            city_site.Add("滨州近海", "371600");
            city_site.Add("即墨近海", "370282");
            city_site.Add("胶州近海", "370281");
            city_site.Add("黄岛近海", "370211");
            city_site.Add("黄海北部", "070201");
            city_site.Add("黄海中部", "070202");
            city_site.Add("黄海南部", "070203");
        }
        public string GetSite(string city)
        {
            return city_site[city].ToString();
        }
    }
