using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Sql_Caozuorizhi 的摘要说明
/// </summary>
public class Sql_Caozuorizhi
{
    DataExecution DataExe;//声明一个数据执行类
    public Sql_Caozuorizhi()
    {
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
    }

    /// <summary>
    ///根据用户名 取出操作日志
    /// </summary>
    /// <param name="yonghu"></param>
    /// <returns></returns>
    public DataTable GetRizhiByYonghu(string yonghu)
    {
        try
        {
            var Yonghu = DataExe.GetDbParameter();
            Yonghu.ParameterName = "@yonghu";
            Yonghu.Value = yonghu;
            DbParameter[] parameters = { Yonghu };
            return DataExe.GetTableExeData("select * from HT_KJ_CHAOZUO_RIZHI where ZHANGHAO=@yonghu ", parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("根据用户名取出操作记录出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return null;
        }
    }

    ///// <summary>
    ///// 返回部门所有信息
    ///// </summary>
    ///// <returns></returns>
    //public object GetBuMenAllName()
    //{
    //    return DataExe.GetTableExeData("select * from HT_KJ_BUMEN");
    //}

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pagenum">当前页数</param>
    /// <param name="pagerow">当页行数</param>
    /// <returns></returns>
    public object GetRizhipage(int pagenum, int pagerow, KJ_Caozuorizhi rizhi)
    {
        //1-10 11-20 21-30
        int pagefist = pagerow * (pagenum - 1) + 1;
        int pagelast = pagerow * (pagenum - 1) + pagerow;
        string wherestr = "";
        DbParameter[] parameters = {  };
        List<DbParameter> parameter = parameters.ToList();
        if (rizhi.Zhanghao != "")//账号
        {
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " ZHANGHAO=@ZHANGHAO ";
            var ZHANGHAO = DataExe.GetDbParameter();
            ZHANGHAO.ParameterName = "@ZHANGHAO";
            ZHANGHAO.Value = rizhi.Zhanghao;
            parameter.Add(ZHANGHAO);
        }
          if (rizhi.Daima != "")//代码
        {
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " DAIMA like '%'||@DAIMA||'%'  ";
            var DAIMA = DataExe.GetDbParameter();
            DAIMA.ParameterName = "@DAIMA";
            DAIMA.Value = rizhi.Daima;
            parameter.Add(DAIMA);
        }
        if (rizhi.Caozuo!="")//时间
        {
            string startdata = rizhi.Caozuo.Split(',')[0];
            string enddata = rizhi.Caozuo.Split(',')[1];
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " SHIJIAN>=to_date(@startdata,'yyyy-MM-dd') and SHIJIAN <= to_date(@enddata,'yyyy-MM-dd') ";
            var pstartdata = DataExe.GetDbParameter();
            var penddata = DataExe.GetDbParameter();
            pstartdata.ParameterName = "@startdata";
            penddata.ParameterName = "@enddata";
            pstartdata.Value = startdata;
            penddata.Value = enddata;
            parameter.Add(pstartdata);
            parameter.Add(penddata);
        }
        
        string sql2 = "select * from(select t.*,rownum rn from(select * from HT_KJ_CHAOZUO_RIZHI " + wherestr + " order by SHIJIAN desc ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
        parameters = parameter.ToArray();
        try
        {
            return DataExe.GetTableExeData(sql2, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("分页获取操作记录出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    /// <summary>
    /// 获取操作日志总行数信息
    /// </summary>
    /// <returns></returns>
    public int GetrizhiCount(KJ_Caozuorizhi rizhi)
    {
        string wherestr = "";
        DbParameter[] parameters = { };
        List<DbParameter> parameter = parameters.ToList();
        if (rizhi.Zhanghao != "")//账号
        {
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " ZHANGHAO=@ZHANGHAO ";
            var ZHANGHAO = DataExe.GetDbParameter();
            ZHANGHAO.ParameterName = "@ZHANGHAO";
            ZHANGHAO.Value = rizhi.Zhanghao;
            parameter.Add(ZHANGHAO);
        }
        if (rizhi.Daima != "")//代码
        {
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " DAIMA like '%'||@DAIMA||'%' ";
            var DAIMA = DataExe.GetDbParameter();
            DAIMA.ParameterName = "@DAIMA";
            DAIMA.Value = rizhi.Daima;
            parameter.Add(DAIMA);
        }
        if (rizhi.Caozuo != "")//时间
        {
            string startdata = rizhi.Caozuo.Split(',')[0];
            string enddata = rizhi.Caozuo.Split(',')[1];
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " SHIJIAN>=to_date(@startdata,'yyyy-MM-dd') and SHIJIAN <= to_date(@enddata,'yyyy-MM-dd') ";
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
           return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_KJ_CHAOZUO_RIZHI "+ wherestr + "", parameters));
          //   return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_KJ_CHAOZUO_RIZHI "+ wherestr + "", parameters));
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取操作日志总数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return -1;
        }

    }

    /// <summary>
    /// 新增操作日志
    /// </summary>
    /// <param name="rizhi"></param>
    /// <returns></returns>
    public  int AddCaozuorizhi(KJ_Caozuorizhi rizhi)
    {
        //string sql1 = "INSERT INTO  HT_KJ_USER (ID, PASSWORD, NAME, BUMEN, QUANXIAN, XINGBIE, ZHUANGTAI, TOUXIANG, ZHUCESHIJIAN, ZUIJINSHIJIAN, ANQUANCELUEID, YOUXIANG, MIMAXIUGAIRIQI, ERRORLOGIN) VALUES (@ID,@PASSWORD,@NAME,@BUMEN,@QUANXIAN,@XINGBIE,@ZHUANGTAI,@TOUXIANG,to_date(@ZHUCESHIJIAN,'yyyy-mm-dd hh24@mi@ss'),to_date(@ZUIJINSHIJIAN,'yyyy-mm-dd hh24@mi@ss'),@ANQUANCELUEID,@YOUXIANG,to_date(@MIMAXIUGAIRIQI,'yyyy-mm-dd hh24@mi@ss'),@ERRORLOGIN)";

        string sql = "INSERT INTO  HT_KJ_CHAOZUO_RIZHI (ZHANGHAO, MINGCHENG, SHIJIAN, DAIMA, CAOZUO) VALUES (@ZHANGHAO,@MINGCHENG,to_date(@SHIJIAN,'yyyy-mm-dd hh24@mi@ss'),@DAIMA,@CAOZUO)";
        var ZHANGHAO = DataExe.GetDbParameter();
        var MINGCHENG = DataExe.GetDbParameter();
        var SHIJIAN = DataExe.GetDbParameter();
        var DAIMA = DataExe.GetDbParameter();
        var CAOZUO = DataExe.GetDbParameter();
        ZHANGHAO.ParameterName = "@ZHANGHAO";
        MINGCHENG.ParameterName = "@MINGCHENG";
        SHIJIAN.ParameterName = "@SHIJIAN";
        DAIMA.ParameterName = "@DAIMA";
        CAOZUO.ParameterName = "@CAOZUO";
        ZHANGHAO.Value = rizhi.Zhanghao;
        MINGCHENG.Value = rizhi.Mingcheng;
        SHIJIAN.Value = rizhi.Shijain.ToString();
        DAIMA.Value = rizhi.Daima;
        CAOZUO.Value = rizhi.Caozuo;
        DbParameter[] parameters = { ZHANGHAO, MINGCHENG, SHIJIAN, DAIMA, CAOZUO };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write(" 新增操作日志出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 修改日志
    /// </summary>
    /// <param name="rizhi"></param>
    /// <returns></returns>
    public int Editrizhi(KJ_Caozuorizhi rizhi)
    {
        string sql = "UPDATE   HT_KJ_CHAOZUO_RIZHI set ZHANGHAO,MINGCHENG,SHIJIAN,DAIMA,CAOZUO  where ZHANGHAO=@ZHANGHAO";
        var ZHANGHAO = DataExe.GetDbParameter();
        var MINGCHENG = DataExe.GetDbParameter();
        var SHIJIAN = DataExe.GetDbParameter();
        var DAIMA = DataExe.GetDbParameter();
        var CAOZUO = DataExe.GetDbParameter();
        ZHANGHAO.ParameterName = "@ZHANGHAO";
        MINGCHENG.ParameterName = "@MINGCHENG";
        SHIJIAN.ParameterName = "@SHIJIAN";
        DAIMA.ParameterName = "@DAIMA";
        CAOZUO.ParameterName = "@CAOZUO";
        ZHANGHAO.Value = rizhi.Zhanghao;
        MINGCHENG.Value = rizhi.Mingcheng;
        SHIJIAN.Value = rizhi.Shijain;
        DAIMA.Value = rizhi.Daima;
        CAOZUO.Value = rizhi.Caozuo;
        DbParameter[] parameters = { ZHANGHAO, MINGCHENG, SHIJIAN, DAIMA, CAOZUO };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改操作日志出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 写日志 根据session
    /// </summary>
    /// <param name="sessionzhanghao"></param>
    /// <param name="daima"></param>
    /// <param name="caozuo"></param>
    public static void WriteRizhi(string sessionzhanghao, string daima, string caozuo)
    {
        try
        {
            var user = new Sql_User().GetUserById(sessionzhanghao);
            if (user.Rows.Count > 0)
            {
                KJ_Caozuorizhi rizhi = new KJ_Caozuorizhi();
                rizhi.Zhanghao = sessionzhanghao;
                rizhi.Mingcheng = user.Rows[0]["NAME"].ToString();
                rizhi.Shijain = DateTime.Now;
                rizhi.Daima = daima;
                rizhi.Caozuo = caozuo;
                new Sql_Caozuorizhi().AddCaozuorizhi(rizhi);
            }
        }
        catch (Exception ex)
        {
            WriteLog.Write("操作日志发生错误" + ex.ToString());
        }

    }
}