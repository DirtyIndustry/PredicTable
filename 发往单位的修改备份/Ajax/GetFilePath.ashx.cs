using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Handler1 的摘要说明
/// </summary>
public class GetFilePath : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string path = System.Web.HttpContext.Current.Server.MapPath("/scword/");//需要遍历的文件夹
        context.Response.ContentType = "text/plain";
        Regex regex = new Regex(path);
        string[] files = regex.Split(GetFiles(path).ToString());
        StringBuilder retsb = new StringBuilder();
        foreach (string str in files)
        {
            retsb.Append(str + "*");
        }
        context.Response.Write(retsb);
    }
    public StringBuilder GetFiles(string path)
    {
        StringBuilder strbu = new StringBuilder();
        if (Directory.GetDirectories(path).Length != 0)
        {

            foreach (string str in Directory.GetDirectories(path))
            {
                strbu.Append(GetFiles(str));
            }
        }
        foreach (string str in Directory.GetFiles(path))
        {
            if (File.GetCreationTime(str).AddDays(1).AddHours(1) > DateTime.Now)
            {
                strbu.Append(str);
            }
        }

        return strbu;

    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
