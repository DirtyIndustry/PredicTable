using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// md5 的摘要说明
/// </summary>
public class md5
{
    public md5()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static string MD5(string str, int code)
    {
        if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
        }

        if (code == 32) //32位加密 
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }
        return "";
    }
}