using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.ExportWord
{
    public class SetWordName
    {
        string a = "";
        string b = "";
        string c = "";
        string d = "";
        string e = "";
        string f = "";
        public string setWordName(List<KJ_Wordname> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                a = list[i].a;
                b = list[i].b;
                c = list[i].c;
                d = list[i].d;
                e = list[i].e;
                f = list[i].f;
            }
            string S = a + "_" + b + "_" + c + "_" + d + "_" + e + "_" + f;
            return S;
        }
    }

}