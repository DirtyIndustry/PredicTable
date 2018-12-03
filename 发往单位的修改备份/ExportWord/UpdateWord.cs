using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// UpdateWord 的摘要说明
/// </summary>
public class UpdateWord
{
    public UpdateWord()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    //保存word文档
    public int updateword(string path, DateTime dt,string userid)
    {
        FileInfo fi = new FileInfo(path);
        FileStream fs = fi.OpenRead();
        byte[] wbytes = new byte[fs.Length];
        fs.Read(wbytes, 0, Convert.ToInt32(fs.Length));
        fs.Close();
        
        sql_TBLYBDDOCUMENT sql_tblybddocument = new sql_TBLYBDDOCUMENT();
        TBLYBDDOCUMENT tbl = new TBLYBDDOCUMENT();
        tbl.YBDCONTENT = wbytes;
        tbl.UPLOADDATE = dt;//DateTime.Now;
        tbl.UPLOADER = userid;
        string[] Path = path.Split('\\');
        string path1 = Path[Path.Length - 1].ToString();
        //string path2 = path1.Split('.')[0] + "_" + dt.ToString("yyyyMMdd") + "." + path1.Split('.')[1];
        // string path2 = path1.Split('.')[0] + "." + path1.Split('.')[1];
        //tbl.YBDNAME = path2;
        tbl.YBDNAME = path1;
        tbl.YBDSIZE = Convert.ToString(wbytes.Length / 1024) + "KB";
        int a= sql_tblybddocument.Edit_TBLYBDDOCUMENT(tbl);
        return a;
    }
}