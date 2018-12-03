using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class StaticClass
{
    /// <summary>
    /// x-x
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] StrToString(string str)
    {
        string[] rstr = { "", "", };
        if (str.Contains('-'))
        {
            rstr[0] = str.Substring(0, str.IndexOf('-'));
            rstr[1] = str.Substring(str.IndexOf('-'));
        }
        else if (str.Contains('~'))
        {

            rstr[0] = str.Substring(0, str.IndexOf('~'));
            rstr[1] = str.Substring(str.IndexOf('~'));
        }
        else
            rstr[0] = str;
        return rstr;
    }
    /// <summary>
    /// 转换为length为2的数组,x增至x
    /// </summary>
    /// <param name="str"></param>
    /// <param name="changeWord">转折词，length=2，"增至;减至"</param>
    /// <returns></returns>
    public static string[] StrTo2String(string str, string[] changeWord)
    {
        
        string[] rstr = { "", "", };
        string word = null;
        foreach (string wordTest in changeWord)
        {
            if (str.Contains(wordTest))
                word = wordTest;

        }
        if (word != null)
        {
            rstr[0] = str.Substring(0, str.IndexOf(word));
            rstr[1] = str.Substring(str.IndexOf(word));
        }
        else
        {
            rstr[0] = str;
        }
        return rstr;
    }

    /// <summary>
    /// 将字符串转为length为5的数组x-x增至x-x
    /// </summary>
    /// <param name="str"></param>
    /// <param name="changeWord"></param>
    /// <returns></returns>
    public static string[] StrTo5String(string str, string[] changeWord)
    {
        string[] rstr = { "", "", "", "", "" };
        if (str.Contains(changeWord[0]))
        {
            string str1 = str.Substring(0, str.IndexOf(changeWord[0]));
            if (str1.Contains("-"))
            {
                rstr[0] = str1.Substring(0, str1.IndexOf("-"));
                rstr[1] = str1.Substring(str1.IndexOf("-") + 1);
            }
            else if (str1.Contains("~"))
            {
                rstr[0] = str1.Substring(0, str1.IndexOf("~"));
                rstr[1] = str1.Substring(str1.IndexOf("~") + 1);
            }
            else
            {
                rstr[0] = str1;
            }
            rstr[2] = changeWord[0];
            string str2 = str.Substring(str.IndexOf(changeWord[0]) + 2, str.Length - str.IndexOf(changeWord[0]) - 2);
            if (str2.Contains("-"))
            {
                rstr[3] = str2.Substring(0, str1.IndexOf("-"));
                rstr[4] = str2.Substring(str1.IndexOf("-") + 1);
            }
            else if (str1.Contains("~"))
            {
                rstr[3] = str2.Substring(0, str1.IndexOf("~"));
                rstr[4] = str2.Substring(str1.IndexOf("~") + 1);
            }
            else
            {
                rstr[3] = str2;
            }
        }
        else if (str.Contains(changeWord[1]))
        {
            string str1 = str.Substring(0, str.IndexOf(changeWord[1]));
            if (str1.Contains("-"))
            {
                rstr[0] = str1.Substring(0, str1.IndexOf("-"));
                rstr[1] = str1.Substring(str1.IndexOf("-") + 1, str.IndexOf(changeWord[1]));
            }
            else if (str1.Contains("~"))
            {
                rstr[0] = str1.Substring(0, str1.IndexOf("~"));
                rstr[1] = str1.Substring(str1.IndexOf("~") + 1, str.IndexOf(changeWord[0]));
            }
            else
            {
                rstr[0] = str.Substring(0, str.IndexOf(changeWord[1]));
            }
            rstr[2] = changeWord[1];
            string str2 = str.Substring(str.IndexOf(changeWord[1]) + 2, str.Length - str.IndexOf(changeWord[1]) - 2);
            if (str2.Contains("-"))
            {
                rstr[3] = str2.Substring(0, str1.IndexOf("-"));
                rstr[4] = str2.Substring(str1.IndexOf("-") + 1, str2.Length - str1.IndexOf("-") - 1);
            }
            else if (str1.Contains("~"))
            {
                rstr[3] = str2.Substring(0, str1.IndexOf("~"));
                rstr[4] = str2.Substring(str1.IndexOf("~") + 1, str2.Length - str1.IndexOf("~") - 1);
            }
            else
            {
                rstr[3] = str2;
            }
        }
        else
        {
            if (str.Contains("-"))
            {
                rstr[0] = str.Substring(0, str.IndexOf("-"));
                rstr[1] = str.Substring(str.IndexOf("-") + 1, str.Length - str.IndexOf("-") - 1);
            }
            else if (str.Contains("~"))
            {
                rstr[0] = str.Substring(0, str.IndexOf("~"));
                rstr[1] = str.Substring(str.IndexOf("~") + 1, str.Length - str.IndexOf("~") - 1);
            }
            else
            {
                rstr[0] = str;
            }
        }
        return rstr;
    }
}
