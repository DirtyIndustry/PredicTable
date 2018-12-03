using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PredicTable.Commen
{
    public class Opt_PublishedTemplates
    {
        public static void addPublishedTemplete(string templeteName)
        {
            try
            {
                var pubPath = HttpContext.Current.Server.MapPath("/PublishedTemplate/PublishedTemplate.txt");
                if (!File.Exists(pubPath))                   // 验证目录是否存在
                {
                    File.CreateText(pubPath);
                }
                else
                {
                    var lastWriteTime = File.GetLastWriteTime(pubPath);
                    if(lastWriteTime.Date !=DateTime.Now.Date)
                    {
                        File.WriteAllText(pubPath,"");
                    }
                }
                var pubedTempletesStr = File.ReadAllText(pubPath);

                if (!pubedTempletesStr.Contains(templeteName + ","))
                {
                    pubedTempletesStr += templeteName + ",";
                    File.WriteAllText(pubPath, pubedTempletesStr);
                }

            }
            catch (Exception exp)
            {
                WriteLog.Write("添加当天已发布模板出错:" + templeteName+ exp.ToString());
            }
        }
        public static string getPublishedTempletes()
        {
            try
            {
                var pubPath = HttpContext.Current.Server.MapPath("/PublishedTemplate/PublishedTemplate.txt");
                if (!File.Exists(pubPath))                   // 验证目录是否存在
                {
                    return "notFound";
                }
                var lastWriteTime = File.GetLastWriteTime(pubPath);
                if (lastWriteTime.Date != DateTime.Now.Date)
                {
                    return "";
                }

                // 读取发布模板名称
                using (var sr = new StreamReader(pubPath))
                {
                    return sr.ReadToEnd();
                }

            }
            catch (Exception exp)
            {
                WriteLog.Write("读取当天已发布模板出错:"  + exp.ToString());
                return "error";
            }
        }
    }
}