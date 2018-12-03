using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
//using System.Data.OracleClient;
using System.Data;
using System.Data.Common;
/// <summary>
/// 用户数据操作表
/// </summary>
public class Sql_User
{
    DataExecution DataExe;//声明一个数据执行类
    public Sql_User()
    {
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
    }
    /// <summary>
    /// 获取所有用户
    /// </summary>
    /// <returns></returns>
    public object getUserAllData()
    {
        try
        {
            return DataExe.GetTableExeData("select * from HT_KJ_USER");
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取所有用户出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    /// <summary>
    /// 根据用户id查信息
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public DataTable GetUserById(string userid)
    {
        string sql = "select * from  HT_KJ_USER where ID=@ID";
        var ID = DataExe.GetDbParameter();
        ID.ParameterName = "@ID";
        ID.Value = userid;
        DbParameter[] parameters = { ID };
        try
        {
            return DataExe.GetTableExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("查询用户出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return null;
        }
    }
    

    /// <summary>
    /// 获取用户总数信息
    /// </summary>
    /// <returns></returns>
    public int GetuserCount()
    {
        try
        {
            return Convert.ToInt32( DataExe.GetObjectExeData("select count(*) from HT_KJ_USER "));
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取用户总数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return -1;
        }

    }


    /// <summary>
    /// 根据部门获取用户人数
    /// </summary>
    /// <param name="bumenid">部门人数</param>
    /// <returns></returns>
    //public int GetuserCount(string bumenid)
    //{
    //    string sql = "select * from  HT_KJ_USER where BUMEN=@ID";
    //    var ID = DataExe.GetDbParameter();
    //    ID.ParameterName = "@ID";
    //    ID.Value = bumenid;
    //    DbParameter[] parameters = { ID };
    //    try
    //    {
    //        return DataExe.GetIntExeData(sql, parameters);
    //    }
    //    catch (Exception ex)
    //    {
    //        WriteLog.Write("根据部门获取用户数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
    //        return -1;
    //    }

    //}

    /// <summary>
    /// 根据id删除用户信息
    /// </summary>
    /// <returns></returns>
    public int DeluserByid(string id)
    {
        try
        {
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = id;
            DbParameter[] parameters = { ID };
            return DataExe.GetIntExeData("delete from HT_KJ_USER where id=@ID", parameters);
           
        }
        catch (Exception ex)
        {
            WriteLog.Write("删除用户出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pagenum">当前页数</param>
    /// <param name="pagerow">当页行数</param>
    /// <returns></returns>
    public object GetuserBypage(int pagenum, int pagerow)
    {
        //1-10 11-20 21-30
        int pagefist = pagerow * (pagenum - 1) + 1;
        int pagelast = pagerow * (pagenum - 1) + pagerow;
        string sql2 = "select * from(select t.*,rownum rn from(select * from HT_KJ_USER order by ID desc) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";

        try
        {
            return DataExe.GetTableExeData(sql2);
        }
        catch (Exception ex)
        {
            WriteLog.Write("分页获取用户出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public int AddUser(KJ_User user)
    {
        string sql = "INSERT INTO  HT_KJ_USER (ID, PASSWORD, NAME, BUMEN, QUANXIAN, XINGBIE, ZHUANGTAI, TOUXIANG, ZHUCESHIJIAN, ZUIJINSHIJIAN, ANQUANCELUEID, YOUXIANG, MIMAXIUGAIRIQI, ERRORLOGIN) VALUES (@ID,@PASSWORD,@NAME,@BUMEN,@QUANXIAN,@XINGBIE,@ZHUANGTAI,@TOUXIANG,to_date(@ZHUCESHIJIAN,'yyyy-mm-dd hh24@mi@ss'),to_date(@ZUIJINSHIJIAN,'yyyy-mm-dd hh24@mi@ss'),@ANQUANCELUEID,@YOUXIANG,to_date(@MIMAXIUGAIRIQI,'yyyy-mm-dd hh24@mi@ss'),@ERRORLOGIN)";
        var ID = DataExe.GetDbParameter();
        var PASSWORD = DataExe.GetDbParameter();
        var NAME = DataExe.GetDbParameter();
        var BUMEN = DataExe.GetDbParameter();
        var QUANXIAN = DataExe.GetDbParameter();
        var XINGBIE = DataExe.GetDbParameter();
        var ZHUANGTAI = DataExe.GetDbParameter();
        var TOUXIANG = DataExe.GetDbParameter();
        var ZHUCESHIJIAN = DataExe.GetDbParameter();
        var ZUIJINSHIJIAN = DataExe.GetDbParameter();
        var ANQUANCELUEID = DataExe.GetDbParameter();
        var YOUXIANG = DataExe.GetDbParameter();
        var MIMAXIUGAIRIQI = DataExe.GetDbParameter();
        var ERRORLOGIN = DataExe.GetDbParameter();
        ID.ParameterName = "@ID";
        PASSWORD.ParameterName = "@PASSWORD";
        NAME.ParameterName = "@NAME";
        BUMEN.ParameterName = "@BUMEN";
        QUANXIAN.ParameterName = "@QUANXIAN";
        XINGBIE.ParameterName = "@XINGBIE";
        ZHUANGTAI.ParameterName = "@ZHUANGTAI";
        TOUXIANG.ParameterName = "@TOUXIANG";
        ZHUCESHIJIAN.ParameterName = "@ZHUCESHIJIAN";
        ZUIJINSHIJIAN.ParameterName = "@ZUIJINSHIJIAN";
        ANQUANCELUEID.ParameterName = "@ANQUANCELUEID";
        YOUXIANG.ParameterName = "@YOUXIANG";
        MIMAXIUGAIRIQI.ParameterName = "@MIMAXIUGAIRIQI";
        ERRORLOGIN.ParameterName = "@ERRORLOGIN";
        ID.Value = user.Id;
        PASSWORD.Value = user.Password;
        NAME.Value = user.Name;
        BUMEN.Value = user.Bumen;
        QUANXIAN.Value = user.Quanxian;
        XINGBIE.Value = user.Xingbie;
        ZHUANGTAI.Value = user.Zhaungtai;
        TOUXIANG.Value = user.Touxiang;
        ZHUCESHIJIAN.Value = user.Zhuceshijian.ToString();
        ZUIJINSHIJIAN.Value = user.Zuijinshijian.ToString();
        ANQUANCELUEID.Value = user.Anquancelueid;
        YOUXIANG.Value = user.Youxiang;
        MIMAXIUGAIRIQI.Value = user.Mimaxiougairiqi.ToString();
        ERRORLOGIN.Value = user.Errrorlogin;
        DbParameter[] parameters = { ID, PASSWORD, NAME, BUMEN, QUANXIAN, XINGBIE, ZHUANGTAI, TOUXIANG, ZHUCESHIJIAN, ZUIJINSHIJIAN, ANQUANCELUEID, YOUXIANG, MIMAXIUGAIRIQI, ERRORLOGIN };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("新增用户出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 修改用户
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public int Edituser(KJ_User user)
    {
        string sql = "UPDATE   HT_KJ_USER set   PASSWORD=@PASSWORD, NAME=@NAME, BUMEN=@BUMEN, QUANXIAN=@QUANXIAN, XINGBIE=@XINGBIE, ZHUANGTAI=@ZHUANGTAI, TOUXIANG=@TOUXIANG, ZHUCESHIJIAN=to_date(@ZHUCESHIJIAN,'yyyy-mm-dd hh24@mi@ss'), ZUIJINSHIJIAN=to_date(@ZUIJINSHIJIAN,'yyyy-mm-dd hh24@mi@ss'), ANQUANCELUEID=@ANQUANCELUEID, YOUXIANG=@YOUXIANG, MIMAXIUGAIRIQI=to_date(@MIMAXIUGAIRIQI,'yyyy-mm-dd hh24@mi@ss'), ERRORLOGIN=@ERRORLOGIN where ID=@ID";

        var ID = DataExe.GetDbParameter();
        var PASSWORD = DataExe.GetDbParameter();
        var NAME = DataExe.GetDbParameter();
        var BUMEN = DataExe.GetDbParameter();
        var QUANXIAN = DataExe.GetDbParameter();
        var XINGBIE = DataExe.GetDbParameter();
        var ZHUANGTAI = DataExe.GetDbParameter();
        var TOUXIANG = DataExe.GetDbParameter();
        var ZHUCESHIJIAN = DataExe.GetDbParameter();
        var ZUIJINSHIJIAN = DataExe.GetDbParameter();
        var ANQUANCELUEID = DataExe.GetDbParameter();
        var YOUXIANG = DataExe.GetDbParameter();
        var MIMAXIUGAIRIQI = DataExe.GetDbParameter();
        var ERRORLOGIN = DataExe.GetDbParameter();
        ID.ParameterName = "@ID";
        PASSWORD.ParameterName = "@PASSWORD";
        NAME.ParameterName = "@NAME";
        BUMEN.ParameterName = "@BUMEN";
        QUANXIAN.ParameterName = "@QUANXIAN";
        XINGBIE.ParameterName = "@XINGBIE";
        ZHUANGTAI.ParameterName = "@ZHUANGTAI";
        TOUXIANG.ParameterName = "@TOUXIANG";
        ZHUCESHIJIAN.ParameterName = "@ZHUCESHIJIAN";
        ZUIJINSHIJIAN.ParameterName = "@ZUIJINSHIJIAN";
        ANQUANCELUEID.ParameterName = "@ANQUANCELUEID";
        YOUXIANG.ParameterName = "@YOUXIANG";
        MIMAXIUGAIRIQI.ParameterName = "@MIMAXIUGAIRIQI";
        ERRORLOGIN.ParameterName = "@ERRORLOGIN";
        ID.Value = user.Id;
        PASSWORD.Value = user.Password;
        NAME.Value = user.Name;
        BUMEN.Value = user.Bumen;
        QUANXIAN.Value = user.Quanxian;
        XINGBIE.Value = user.Xingbie;
        ZHUANGTAI.Value = user.Zhaungtai;
        TOUXIANG.Value = user.Touxiang;
        ZHUCESHIJIAN.Value = user.Zhuceshijian.ToString();
        ZUIJINSHIJIAN.Value = user.Zuijinshijian.ToString();
        ANQUANCELUEID.Value = user.Anquancelueid;
        YOUXIANG.Value = user.Youxiang;
        MIMAXIUGAIRIQI.Value = user.Mimaxiougairiqi.ToString();
        ERRORLOGIN.Value = user.Errrorlogin;
        DbParameter[] parameters = { ID, PASSWORD, NAME, BUMEN, QUANXIAN, XINGBIE, ZHUANGTAI, TOUXIANG, ZHUCESHIJIAN, ZUIJINSHIJIAN, ANQUANCELUEID, YOUXIANG, MIMAXIUGAIRIQI, ERRORLOGIN };

        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("修改用户出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }


    public DataTable testdataTable()
    {
        DbParameter db = DataExe.GetDbParameter();
        db.ParameterName = "@ids";
        db.Value = "14";
        DbParameter[] dbs = new DbParameter[1];
        dbs[0] = db;
        return DataExe.GetTableExeData("select * from ht_kj_user where id=@ids", dbs);

    }

}