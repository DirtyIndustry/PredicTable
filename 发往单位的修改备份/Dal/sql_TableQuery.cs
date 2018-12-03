using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;


public class sql_TableQuery
{
    DataExecution DataExe;//声明一个数据执行类
    public sql_TableQuery()
    {
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
    } 
    /// <summary>
    /// <summary>
    /// 获取表单数据
    /// </summary>
    /// <returns></returns>
    public object get_TableQuerybydata(TBLYBDDOCUMENT query)
    {

        try
        {
            return DataExe.GetTableExeData("select * from TBLYBDDOCUMENT where YBDID=" + query.TYPE.ToString()+ "");

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 获取表单数据
    /// </summary>
    /// <returns></returns>
    public object get_TableQuerybydata(string ids)
    {
        try
        {
            return DataExe.GetTableExeData("select * from TBLYBDDOCUMENT where YBDID in (" + ids + ")");
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// 分页查询
    /// </summary>
    /// <param name="pagenum">当前页数</param>
    /// <param name="pagerow">当页行数</param>
    /// <returns></returns>
    public object GetTableQuerypage(int pagenum, int pagerow, TBLYBDDOCUMENT query)
    {
        //1-10 11-20 21-30
        int pagefist = pagerow * (pagenum - 1) + 1;
        int pagelast = pagerow * (pagenum - 1) + pagerow;
        string wherestr = "";
        DbParameter[] parameters = { };
        List<DbParameter> parameter = parameters.ToList();
        if ( query.YBDSIZE != "")//时间
        {
            string startdata = query.YBDSIZE.Split(',')[0];
            string enddata = query.YBDSIZE.Split(',')[1];
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " UPLOADDATE>=to_date(@startdata,'yyyy-MM-dd') and UPLOADDATE <= to_date(@enddata,'yyyy-MM-dd') ";
            var pstartdata = DataExe.GetDbParameter();
            var penddata = DataExe.GetDbParameter();
            pstartdata.ParameterName = "@startdata";
            penddata.ParameterName = "@enddata";
            pstartdata.Value = startdata;
            penddata.Value = enddata;
            parameter.Add(pstartdata);
            parameter.Add(penddata);
        }
        string sql2 = "select * from(select t.*,rownum rn from(select * from TBLYBDDOCUMENT " + wherestr + " order by UPLOADDATE desc ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
        parameters = parameter.ToArray();
        try
        {
            return DataExe.GetTableExeData(sql2, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("分页获取预报表单出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    /// <summary>
    /// 获取操作日志总行数信息
    /// </summary>
    /// <returns></returns>
    public int GeTableQueryCount(TBLYBDDOCUMENT query)
    {
        string wherestr = "";
        DbParameter[] parameters = { };
        List<DbParameter> parameter = parameters.ToList();
        if (query.YBDSIZE != "")//时间
        {
            string startdata = query.YBDSIZE.Split(',')[0];
            string enddata = query.YBDSIZE.Split(',')[1];
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " UPLOADDATE>=to_date(@startdata,'yyyy-MM-dd') and UPLOADDATE <= to_date(@enddata,'yyyy-MM-dd') ";
            var pstartdata = DataExe.GetDbParameter();
            var penddata = DataExe.GetDbParameter();
            pstartdata.ParameterName = "@startdata";
            penddata.ParameterName = "@enddata";
            pstartdata.Value = startdata;
            penddata.Value = enddata;
            parameter.Add(pstartdata);
            parameter.Add(penddata);
        }
        parameters = parameter.ToArray();
        try
        {
            return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from TBLYBDDOCUMENT " + wherestr + "", parameters));
            //   return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_KJ_CHAOZUO_RIZHI "+ wherestr + "", parameters));
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取预报表单总数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return -1;
        }

    }
}
