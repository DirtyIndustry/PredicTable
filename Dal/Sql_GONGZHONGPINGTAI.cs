using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;


public class Sql_GONGZHONGPINGTAI
    {
    DataExecution DataExe= new DataExecution();//声明一个数据执行类
    DataExecution_JB DataExe_JB = new DataExecution_JB();//声明一个数据执行类
    /// <summary>
    /// 新增主表数据
    /// </summary>
    /// <param name="KJ_GongZhongPingTai"></param>
    /// <returns></returns>
    public int Add_GongZhongPingTai(KJ_GongZhongPingTai KJ_GongZhongPingTai)
    {
       // string sql = "INSERT INTO  HT_KJ_GONGZHONGPINGTAI (ID,TIME,USERID,DOCTYPE,DOCUMENTCONTENT,MESTYPE,STATE) VALUES (HT_KJ_GONGZHONGPINGTAI.Nextval,(to_date(@TIME,'yyyy-mm-dd hh24@mi@ss'),@USERID,@DOCTYPE,@DOCUMENTCONTENT,@MESTYPE,@STATE)";
        string sql = "INSERT INTO  HT_KJ_GONGZHONGPINGTAI (ID,TIME,USERID,DOCTYPE,DOCUMENTCONTENT,MESTYPE,STATE,DXGROUP) VALUES (GONGZHONGPINGTAI.Nextval,to_date(@TIME,'yyyy-mm-dd hh24@mi@ss'),@USERID,@DOCTYPE,@DOCUMENTCONTENT,@MESTYPE,@STATE,@DXGROUP)";
        
       
         var TIME = DataExe.GetDbParameter();
        var USERID = DataExe.GetDbParameter();
        var DOCTYPE = DataExe.GetDbParameter();
        var DOCUMENTCONTENT = DataExe.GetDbParameter();
        var MESTYPE = DataExe.GetDbParameter();
        var STATE = DataExe.GetDbParameter();
        var DXGROUP = DataExe.GetDbParameter();

        TIME.ParameterName = "@TIME";
        USERID.ParameterName = "@USERID";
        DOCTYPE.ParameterName = "@DOCTYPE";
        DOCUMENTCONTENT.ParameterName = "@DOCUMENTCONTENT";
        MESTYPE.ParameterName = "@MESTYPE";
        STATE.ParameterName = "@STATE";
        DXGROUP.ParameterName = "@DXGROUP";

        TIME.Value = KJ_GongZhongPingTai.TIME.ToString();
        USERID.Value = KJ_GongZhongPingTai.USERID;
        DOCTYPE.Value = KJ_GongZhongPingTai.DOCTYPE;
        DOCUMENTCONTENT.Value = KJ_GongZhongPingTai.DOCUMENTCONTENT;
        MESTYPE.Value = KJ_GongZhongPingTai.MESTYPE;
        STATE.Value = KJ_GongZhongPingTai.STATE;
        DXGROUP.Value = KJ_GongZhongPingTai.DXGROUP;
        
        DbParameter[] parameters = { TIME, USERID, DOCTYPE, DOCUMENTCONTENT, MESTYPE,STATE, DXGROUP };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {
          
            return 0;
        }
    }

    /// <summary>
    /// 微信新增主表数据
    /// </summary>
    /// <param name="KJ_GongZhongPingTai"></param>
    /// <returns></returns>
    public int Add_GongZhongPingTaiWX(KJ_GongZhongPingTai KJ_GongZhongPingTai)
    {
        // string sql = "INSERT INTO  HT_KJ_GONGZHONGPINGTAI (ID,TIME,USERID,DOCTYPE,DOCUMENTCONTENT,MESTYPE,STATE) VALUES (HT_KJ_GONGZHONGPINGTAI.Nextval,(to_date(@TIME,'yyyy-mm-dd hh24@mi@ss'),@USERID,@DOCTYPE,@DOCUMENTCONTENT,@MESTYPE,@STATE)";
        string sql = "INSERT INTO  HT_KJ_GONGZHONGPINGTAI (ID,TIME,USERID,DOCTYPE,DOCUMENTCONTENT,MESTYPE,STATE, SUBJECT, ABSTRACT, TYPE) VALUES (GONGZHONGPINGTAI.Nextval,to_date(@TIME,'yyyy-mm-dd hh24@mi@ss'),@USERID,@DOCTYPE,@DOCUMENTCONTENT,@MESTYPE,@STATE,@SUBJECT, @ABSTRACT, @TYPE)";


        var TIME = DataExe.GetDbParameter();
        var USERID = DataExe.GetDbParameter();
        var DOCTYPE = DataExe.GetDbParameter();
        var DOCUMENTCONTENT = DataExe.GetDbParameter();
        var MESTYPE = DataExe.GetDbParameter();
        var STATE = DataExe.GetDbParameter();
        var SUBJECT = DataExe.GetDbParameter();
        var ABSTRACT = DataExe.GetDbParameter();
        var TYPE = DataExe.GetDbParameter();
        //var COVER = DataExe.GetDbParameter();
        //var COVERNAME = DataExe.GetDbParameter();

        TIME.ParameterName = "@TIME";
        USERID.ParameterName = "@USERID";
        DOCTYPE.ParameterName = "@DOCTYPE";
        DOCUMENTCONTENT.ParameterName = "@DOCUMENTCONTENT";
        MESTYPE.ParameterName = "@MESTYPE";
        STATE.ParameterName = "@STATE";
        SUBJECT.ParameterName = "@SUBJECT";
        ABSTRACT.ParameterName = "@ABSTRACT";
        TYPE.ParameterName = "@TYPE";
        //COVER.ParameterName = "@COVER";
        //COVERNAME.ParameterName = "@COVERNAME";


      

        TIME.Value = KJ_GongZhongPingTai.TIME.ToString();
        USERID.Value = KJ_GongZhongPingTai.USERID;
        DOCTYPE.Value = KJ_GongZhongPingTai.DOCTYPE;
        DOCUMENTCONTENT.Value = KJ_GongZhongPingTai.DOCUMENTCONTENT;
        MESTYPE.Value = KJ_GongZhongPingTai.MESTYPE;
        STATE.Value = KJ_GongZhongPingTai.STATE;
        SUBJECT.Value = KJ_GongZhongPingTai.SUBJECT;
        ABSTRACT.Value = KJ_GongZhongPingTai.ABSTRACT;
        TYPE.Value = KJ_GongZhongPingTai.TYPE;
        //COVER.Value = KJ_GongZhongPingTai.COVER;
        //COVERNAME.Value = KJ_GongZhongPingTai.COVERNAME;
      



        DbParameter[] parameters = { TIME, USERID, DOCTYPE, DOCUMENTCONTENT, MESTYPE, STATE, SUBJECT, ABSTRACT, TYPE };
        try
        {
            return DataExe.GetIntExeData(sql, parameters);
        }
        catch (Exception ex)
        {

            return 0;
        }
    }
    /// <summary>
    /// 查询最大ID
    /// </summary>
    /// <param name="userid">当前操作人ID避免多人同时操作同一个表，造成当前id错乱</param>
    /// <returns></returns>
    public object getMaxid(string userid )
    {
        try
        {
            string sql1 = "SELECT MAX(ID) FROM HT_KJ_GONGZHONGPINGTAI where USERID= " + "'" + userid + "'" + "";

            return DataExe.GetTableExeData(sql1);
           
           
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 为主表添加附件
    /// </summary>
    /// <param name="KJ_GONGZHONGPINGTAIFUJIAN"></param>
    /// <returns></returns>
    public int Add_GongZhongPingTaiFJ(KJ_GONGZHONGPINGTAIFUJIAN KJ_GONGZHONGPINGTAIFUJIAN)
    {
        string sql = "INSERT INTO  HT_KJ_GONGZHONGPINGTAIFUJIAN (ID,ANNEX,SORTID,FILENAME,TYPE,WAIID) VALUES (GONGZHONGPINGTAIFUJIAN.Nextval,@ANNEX,@SORTID,@FILENAME,@TYPE,@WAIID)";

        var ANNEX = DataExe.GetDbParameter();
        var SORTID = DataExe.GetDbParameter();
        var FILENAME = DataExe.GetDbParameter();
        var TYPE = DataExe.GetDbParameter();
        var WAIID = DataExe.GetDbParameter();


        ANNEX.ParameterName = "@ANNEX";
        SORTID.ParameterName = "@SORTID";
        FILENAME.ParameterName = "@FILENAME";

        TYPE.ParameterName = "@TYPE";
        WAIID.ParameterName = "@WAIID";
        ANNEX.Value = KJ_GONGZHONGPINGTAIFUJIAN.ANNEX;
        SORTID.Value = KJ_GONGZHONGPINGTAIFUJIAN.SORTID;
        FILENAME.Value = KJ_GONGZHONGPINGTAIFUJIAN.FILENAME;
        TYPE.Value = KJ_GONGZHONGPINGTAIFUJIAN.TYPE;
        WAIID.Value = KJ_GONGZHONGPINGTAIFUJIAN.WAIID;
        DbParameter[] parameters = { ANNEX, SORTID, FILENAME, TYPE, WAIID };
        try
        {
            DataExe.GetIntExeData(sql, parameters);
            return 1;
        }
        catch (Exception ex)
        {

            return 0;
        }
    }


    /// 分页查询
    /// </summary>
    /// <param name="pagenum">当前页数</param>
    /// <param name="pagerow">当页行数</param>
    /// <returns></returns>
    public object GetTableQuerypage(int pagenum, int pagerow, KJ_GONGZHONGPINGTAIFUJIAN query)
    {
        int pagefist = pagerow * (pagenum - 1) + 1;
        int pagelast = pagerow * (pagenum - 1) + pagerow;
        string wherestr = "";
        DbParameter[] parameters = { };
        List<DbParameter> parameter = parameters.ToList();
        if (query.TYPE != "")
        {
          
        }
        string sql2 = "select * from(select t.*,rownum rn from(select ht_kj_gongzhongpingtai.id,ht_kj_gongzhongpingtai.doctype, ht_kj_gongzhongpingtai.documentcontent,ht_kj_gongzhongpingtai.mestype,ht_kj_gongzhongpingtai.DXGROUP, ht_kj_gongzhongpingtai.state,  ht_kj_gongzhongpingtai.time, ht_kj_gongzhongpingtai.userid,ht_kj_gongzhongpingtaifujian.annex, ht_kj_gongzhongpingtaifujian.filename, ht_kj_gongzhongpingtaifujian.id as vid, ht_kj_gongzhongpingtaifujian.sortid, ht_kj_gongzhongpingtaifujian.type,  ht_kj_gongzhongpingtaifujian.waiid from HT_KJ_GONGZHONGPINGTAI left join HT_KJ_GONGZHONGPINGTAIFUJIAN  on HT_KJ_GONGZHONGPINGTAI.ID = HT_KJ_GONGZHONGPINGTAIFUJIAN.WAIID order by  ht_kj_gongzhongpingtai.time desc) t where rownum<=" + pagelast + " ) where rn>=" + pagefist + "";
       // string sql2 = "select * from(select t.*,rownum rn from(select ht_kj_gongzhongpingtai.id,ht_kj_gongzhongpingtai.doctype, ht_kj_gongzhongpingtai.documentcontent,ht_kj_gongzhongpingtai.mestype,ht_kj_gongzhongpingtai.DXGROUP, ht_kj_gongzhongpingtai.state, ht_kj_gongzhongpingtai.time, ht_kj_gongzhongpingtai.userid,ht_kj_gongzhongpingtaifujian.annex, ht_kj_gongzhongpingtaifujian.filename, ht_kj_gongzhongpingtaifujian.id as vid, ht_kj_gongzhongpingtaifujian.sortid, ht_kj_gongzhongpingtaifujian.type, ht_kj_gongzhongpingtaifujian.waiid from HT_KJ_GONGZHONGPINGTAI left join HT_KJ_GONGZHONGPINGTAIFUJIAN on HT_KJ_GONGZHONGPINGTAI.ID = HT_KJ_GONGZHONGPINGTAIFUJIAN.WAIID  ORDER BY  time desc) t) ";

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

    /// 分页查询
    /// </summary>
    /// <param name="pagenum">当前页数</param>
    /// <param name="pagerow">当页行数</param>
    /// <returns></returns>
    public object GetTableQuerypage(int pagenum, int pagerow, KJ_GONGZHONGPINGTAIFUJIAN query,string msgtype)
    {
        int pagefist = pagerow * (pagenum - 1) + 1;
        int pagelast = pagerow * (pagenum - 1) + pagerow;
        string wherestr = "";
        DbParameter[] parameters = { };
        List<DbParameter> parameter = parameters.ToList();
        if (query.TYPE != "")
        {

        }
        string sql2 = "select * from(select t.*,rownum rn from(select ht_kj_gongzhongpingtai.id,ht_kj_gongzhongpingtai.doctype, ht_kj_gongzhongpingtai.documentcontent,ht_kj_gongzhongpingtai.mestype,ht_kj_gongzhongpingtai.DXGROUP, ht_kj_gongzhongpingtai.state,  ht_kj_gongzhongpingtai.time, ht_kj_gongzhongpingtai.userid,ht_kj_gongzhongpingtaifujian.annex, ht_kj_gongzhongpingtaifujian.filename, ht_kj_gongzhongpingtaifujian.id as vid, ht_kj_gongzhongpingtaifujian.sortid, ht_kj_gongzhongpingtaifujian.type,  ht_kj_gongzhongpingtaifujian.waiid from HT_KJ_GONGZHONGPINGTAI left join HT_KJ_GONGZHONGPINGTAIFUJIAN  on HT_KJ_GONGZHONGPINGTAI.ID = HT_KJ_GONGZHONGPINGTAIFUJIAN.WAIID WHERE HT_KJ_GONGZHONGPINGTAI.MESTYPE = '" + msgtype + "' order by  ht_kj_gongzhongpingtai.time desc) t where rownum<=" + pagelast + " ) where rn>=" + pagefist + "";
        // string sql2 = "select * from(select t.*,rownum rn from(select ht_kj_gongzhongpingtai.id,ht_kj_gongzhongpingtai.doctype, ht_kj_gongzhongpingtai.documentcontent,ht_kj_gongzhongpingtai.mestype,ht_kj_gongzhongpingtai.DXGROUP, ht_kj_gongzhongpingtai.state, ht_kj_gongzhongpingtai.time, ht_kj_gongzhongpingtai.userid,ht_kj_gongzhongpingtaifujian.annex, ht_kj_gongzhongpingtaifujian.filename, ht_kj_gongzhongpingtaifujian.id as vid, ht_kj_gongzhongpingtaifujian.sortid, ht_kj_gongzhongpingtaifujian.type, ht_kj_gongzhongpingtaifujian.waiid from HT_KJ_GONGZHONGPINGTAI left join HT_KJ_GONGZHONGPINGTAIFUJIAN on HT_KJ_GONGZHONGPINGTAI.ID = HT_KJ_GONGZHONGPINGTAIFUJIAN.WAIID  ORDER BY  time desc) t) ";

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
    /// 获取主表总行数信息
    /// </summary>
    /// <returns></returns>
    public int GeTableQueryCount(KJ_GongZhongPingTai query)
    {
        string wherestr = "";
        DbParameter[] parameters = { };
        List<DbParameter> parameter = parameters.ToList();
       
        parameters = parameter.ToArray();
        try
        {
            return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from (select * from  HT_KJ_GONGZHONGPINGTAI left join HT_KJ_GONGZHONGPINGTAIFUJIAN on HT_KJ_GONGZHONGPINGTAI.ID = HT_KJ_GONGZHONGPINGTAIFUJIAN.WAIID) t", parameters));
           
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取预报表单总数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return -1;
        }

    }

    /// <summary>
    /// 获取主表总行数信息
    /// </summary>
    /// <returns></returns>
    public int GeTableQueryCount(KJ_GongZhongPingTai query,string msgtype)
    {
        string wherestr = "";
        DbParameter[] parameters = { };
        List<DbParameter> parameter = parameters.ToList();

        parameters = parameter.ToArray();
        try
        {
            return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from (select * from  HT_KJ_GONGZHONGPINGTAI  left join HT_KJ_GONGZHONGPINGTAIFUJIAN on HT_KJ_GONGZHONGPINGTAI.ID = HT_KJ_GONGZHONGPINGTAIFUJIAN.WAIID WHERE HT_KJ_GONGZHONGPINGTAI.MESTYPE = '" + msgtype + "') t", parameters));

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取预报表单总数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return -1;
        }

    }

    /// <summary>
    /// 获取副表总行数信息
    /// </summary>
    /// <returns></returns>
    public int GeTableQueryCountfb(string waiid)
    {
        string wherestr = "";
        DbParameter[] parameters = { };
        List<DbParameter> parameter = parameters.ToList();

        parameters = parameter.ToArray();
        try
        {
            return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_KJ_GONGZHONGPINGTAIFUJIAN where WAIID="+waiid+""));

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取预报表单总数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return -1;
        }

    }
    /// <summary>
    /// 删除附件
    /// </summary>
    /// <param name="id">附件表主键id</param>
    /// <returns></returns>
    public int Del_FuJian(string id)
    {
        try
        {
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = id;
            DbParameter[] parameters = { ID };
            DataExe.GetIntExeData("delete from HT_KJ_GONGZHONGPINGTAIFUJIAN where ID=@ID", parameters);
            return 1;

        }
        catch (Exception ex)
        {

            return 0;
        }

    }
    /// <summary>
    /// 删除主表
    /// </summary>
    /// <param name="id">主表主键id</param>
    /// <returns></returns>
    public int Del_All(string id)
    {
        try
        {
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = id;
            DbParameter[] parameters = { ID };
            int a = DataExe.GetIntExeData("delete from HT_KJ_GONGZHONGPINGTAI where ID=@ID", parameters);
            return a;

        }
        catch (Exception ex)
        {

            return 0;
        }

    }
    /// <summary>
    /// 删除与主表相关的附件
    /// </summary>
    /// <param name="waiid">附件表外键</param>
    /// <returns></returns>
    public int Del_AllFuJian(string id)
    {
        try
        {
            var ID = DataExe.GetDbParameter();
            ID.ParameterName = "@ID";
            ID.Value = id;
            DbParameter[] parameters = { ID };
            
            int a =  DataExe.GetIntExeData("delete from HT_KJ_GONGZHONGPINGTAIFUJIAN where WAIID=@ID", parameters);
            return a;

        }
        catch (Exception ex)
        {

            return 0;
        }

    }
    /// <summary>
    /// 获取短信组数据
    /// </summary>
    /// <returns></returns>
    public object get_GROUPdata()
    {

        try
        {
            string sql = "select SMSGROUP from TBLSMSCONTACTS";
            return DataExe_JB.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
    /// <summary>
    /// 分组分为山东和北海组
    /// </summary>
    /// <returns></returns>
    public object GetGroupData()
    {
        try
        {
            string sql = "select a.tblsmscontactsgroupid, a.smsgroupname, b.smsid,c.smsgroup from tblsmscontactsgroup a "
                + " INNER join tblsmscontgrouprelate b"
                + " on a.tblsmscontactsgroupid = b.tblsmscontactsgroupid"
                + " INNER join tblsmscontacts c"
                + " on c.smsid = b.smsid"
                + " where a.tblsmscontactsgroupid = '1' or a.tblsmscontactsgroupid = '2'";
            return DataExe_JB.GetTableExeData(sql);

        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
}
