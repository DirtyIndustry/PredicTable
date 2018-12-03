using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


public class UPLOADWORD
{
    public UPLOADWORD()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
   //更新
    public int uploadword(string oldname, string newname, string type)
    {

        Sql_UPLOADWORD sql_uploadword  = new Sql_UPLOADWORD();
        KJ_UPLOADWORD tbl = new KJ_UPLOADWORD();

         tbl.Oldname = oldname;
         tbl.Newname = newname;
         tbl.Type = type;
        int a = sql_uploadword.Edit_UPLOADWORD(tbl);
        return a;
    }
    //新增
    public int addword(string oldname, string newname, string type)
    {

        Sql_UPLOADWORD sql_uploadword = new Sql_UPLOADWORD();
        KJ_UPLOADWORD tbl = new KJ_UPLOADWORD();

        tbl.Oldname = oldname;
        tbl.Newname = newname;
        tbl.Type = type;
        int a = sql_uploadword.Add_UPLOADWORD(tbl);
        return a;
    }
   // 删除
    public int DelByid(string id)
    {
        Sql_UPLOADWORD sql_uploadword = new Sql_UPLOADWORD();
        KJ_UPLOADWORD tbl = new KJ_UPLOADWORD();

        tbl.Id = id;
        int a = sql_uploadword.Add_UPLOADWORD(tbl);
        return a;
    }
}

