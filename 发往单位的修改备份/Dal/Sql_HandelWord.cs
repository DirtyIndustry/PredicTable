using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;


public class Sql_HandelWord
{

    DataExecution DataExe;//声明一个数据执行类
    public Sql_HandelWord()
    {
        DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
    }
    /// <summary>
    /// 保存中文预报单
    /// </summary>
    /// <param name="FileName">文件名</param>
    /// <param name="b">文件流</param>
    public int SaveCNWord(string FileName, byte[] b, Project_CN projectCN ,string docName, byte[] btImg)
    {
        #region
        //string sql = "INSERT INTO  HT_REPORT_WORD SELECT REPORT_WORD.Nextval,@CNID,empty_blob(),to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE FROM HT_REPORT_CN   WHERE ID = @CNID";

        //string sql = "INSERT INTO  HT_REPORT_WORD (ID,reportid,wordflow,createtime,reporttype)VALUES(REPORT_WORD.Nextval,@CNID,empty_blob(),to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE)";
        //string sql = "INSERT INTO  HT_REPORT_WORD (ID,reportid,wordflow,createtime,reporttype,WORDNAME)VALUES(REPORT_WORD.Nextval,@CNID,@WORDFLOW,to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE,@WORDNAME)";

        //var WORDFLOW = DataExe.GetDbParameter();
        //var REPORTTYPE = DataExe.GetDbParameter();
        //var CNID = DataExe.GetDbParameter();
        //var WORDNAME = DataExe.GetDbParameter();

        //REPORTTYPE.ParameterName = "@REPORTTYPE";
        //CNID.ParameterName = "@CNID";
        //WORDFLOW.ParameterName = "@WORDFLOW";
        //WORDNAME.ParameterName = "@WORDNAME";

        //WORDFLOW.Value = b;
        //REPORTTYPE.Value = "CN";
        //CNID.Value = projectCN.ID;
        //WORDNAME.Value = docName;

        //DbParameter[] parameters = { CNID,REPORTTYPE };
        //DbParameter[] parameters = { CNID, REPORTTYPE, WORDFLOW, WORDNAME };
        #endregion
        string sql = "INSERT INTO CG_YUBAO_ME(YBWENJIANMING,YBSHIJIAN) VALUES ('"+ docName + "','"+ projectCN .pbtime+ "')";
        string sql2 = "INSERT INTO CG_YUBAO_FILE(YBWENJIANMING, YBNEIRONG, PICFILE) VALUES(@YBWENJIANMING,@YBNEIRONG , @PICFILE)";

        var YBWENJIANMING = DataExe.GetDbParameter();
        var YBNEIRONG = DataExe.GetDbParameter();
        var PICFILE = DataExe.GetDbParameter();

        YBWENJIANMING.ParameterName = "@YBWENJIANMING";
        YBNEIRONG.ParameterName = "@YBNEIRONG";
        PICFILE.ParameterName = "@PICFILE";

        YBWENJIANMING.Value = docName;
        YBNEIRONG.Value = b;
        PICFILE.Value = btImg;

        DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
        try
        {
            DataExe.GetIntExeData(sql);
            DataExe.GetIntExeData(sql2, parameters);
            return 1;
        }
        catch (Exception ex)
        {

            return 0;
        }
    }

    /// <summary>
    /// 更新中文预报单文件流
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="b"></param>
    /// <param name="docName"></param>
    /// <returns></returns>
    public int UpdateCNWord(string FileName, byte[] b, string docName, byte[] btImg)
    {
        #region
        //string sql = "INSERT INTO  HT_REPORT_WORD SELECT REPORT_WORD.Nextval,@CNID,empty_blob(),to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE FROM HT_REPORT_CN   WHERE ID = @CNID";

        //string sql = "INSERT INTO  HT_REPORT_WORD (ID,reportid,wordflow,createtime,reporttype)VALUES(REPORT_WORD.Nextval,@CNID,empty_blob(),to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE)";
        //string sql = "UPDATE  HT_REPORT_WORD  SET "
        //            + " wordflow = @WORDFLOW"
        //            + " WHERE reporttype = @REPORTTYPE AND WORDNAME = @WORDNAME";

        //var WORDFLOW = DataExe.GetDbParameter();
        //var REPORTTYPE = DataExe.GetDbParameter();
        //var WORDNAME = DataExe.GetDbParameter();

        //REPORTTYPE.ParameterName = "@REPORTTYPE";
        //WORDFLOW.ParameterName = "@WORDFLOW";
        //WORDNAME.ParameterName = "@WORDNAME";

        //WORDFLOW.Value = b;
        //REPORTTYPE.Value = "CN";
        //WORDNAME.Value = docName;

        ////DbParameter[] parameters = { CNID,REPORTTYPE };

        //DbParameter[] parameters = { REPORTTYPE, WORDFLOW, WORDNAME };
        #endregion
        string sql = "";
        //sql = "UPDATE CG_YUBAO_ME SET YBWENJIANMING = '" + docName + "'";
        sql = "UPDATE CG_YUBAO_FILE SET YBNEIRONG=@YBNEIRONG, PICFILE = @PICFILE WHERE YBWENJIANMING = @YBWENJIANMING";
        var YBWENJIANMING = DataExe.GetDbParameter();
        var YBNEIRONG = DataExe.GetDbParameter();
        var PICFILE = DataExe.GetDbParameter();

        YBWENJIANMING.ParameterName = "@YBWENJIANMING";
        YBNEIRONG.ParameterName = "@YBNEIRONG";
        PICFILE.ParameterName = "@PICFILE";

        YBWENJIANMING.Value = docName;
        YBNEIRONG.Value = b;
        PICFILE.Value = btImg;

        DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
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

    /// <summary>
    /// 保存英文旬、月预报单入库
    /// </summary>
    /// <param name="FileName">文件名</param>
    /// <param name="b"></param>
    /// <param name="projectCN"></param>
    /// <returns></returns>
    public int SaveENDayWord(string FileName, byte[] b, Project_ENDay projectDay, string docName,string ENDayType, byte[] btImg)
    {
        #region
        ////string sql = "INSERT INTO  HT_REPORT_WORD SELECT REPORT_WORD.Nextval,@ENDayID,empty_blob(),to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE FROM HT_REPORT_EN  WHERE ID = @ENDayID";
        ////string sql = "INSERT INTO  HT_REPORT_WORD (ID,reportid,wordflow,createtime,reporttype)VALUES(REPORT_WORD.Nextval,@ENDayID,empty_blob(),to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE)";
        //string sql = "INSERT INTO  HT_REPORT_WORD (ID,reportid,wordflow,createtime,reporttype,WORDNAME)VALUES(REPORT_WORD.Nextval,@ENDayID,@WORDFLOW,to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE,@WORDNAME)";
        //var WORDFLOW = DataExe.GetDbParameter();
        //var REPORTTYPE = DataExe.GetDbParameter();
        //var ENDayID = DataExe.GetDbParameter();
        //var WORDNAME = DataExe.GetDbParameter();

        //REPORTTYPE.ParameterName = "@REPORTTYPE";
        //ENDayID.ParameterName = "@ENDayID";
        //WORDFLOW.ParameterName = "@WORDFLOW";
        //WORDNAME.ParameterName = "@WORDNAME";

        //WORDFLOW.Value = b;
        //REPORTTYPE.Value = ENDayType;
        //ENDayID.Value = projectDay.ID;
        //WORDNAME.Value = docName;

        //DbParameter[] parameters = { ENDayID, REPORTTYPE, WORDFLOW,WORDNAME };
        //try
        //{
        //    DataExe.GetIntExeData(sql, parameters);
        //    return 1;
        //}
        //catch (Exception ex)
        //{

        //    return 0;
        //}
        #endregion
        string sql = "INSERT INTO CG_YUBAO_ME(YBWENJIANMING) VALUES ('" + docName + "')";
        string sql2 = "INSERT INTO CG_YUBAO_FILE(YBWENJIANMING, YBNEIRONG, PICFILE) VALUES(@YBWENJIANMING,@YBNEIRONG , @PICFILE)";

        var YBWENJIANMING = DataExe.GetDbParameter();
        var YBNEIRONG = DataExe.GetDbParameter();
        var PICFILE = DataExe.GetDbParameter();

        YBWENJIANMING.ParameterName = "@YBWENJIANMING";
        YBNEIRONG.ParameterName = "@YBNEIRONG";
        PICFILE.ParameterName = "@PICFILE";

        YBWENJIANMING.Value = docName;
        YBNEIRONG.Value = b;
        PICFILE.Value = btImg;

        DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
        try
        {
            DataExe.GetIntExeData(sql);
            DataExe.GetIntExeData(sql2, parameters);
            return 1;
        }
        catch (Exception ex)
        {

            return 0;
        }
    }

    /// <summary>
    /// 更新旬月预报单文件流
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="b"></param>
    /// <param name="projectDay"></param>
    /// <param name="docName"></param>
    /// <returns></returns>
    public int UpdateENDayWord(string FileName, byte[] b, string docName ,string ENDayType, byte[] btImg)
    {
        #region
        //string sql = "UPDATE HT_REPORT_WORD SET "
        //            + " wordflow = @WORDFLOW"
        //            + " WHERE reporttype = @REPORTTYPE AND WORDNAME = @WORDNAME";

        //var WORDFLOW = DataExe.GetDbParameter();
        //var REPORTTYPE = DataExe.GetDbParameter();
        //var WORDNAME = DataExe.GetDbParameter();

        //REPORTTYPE.ParameterName = "@REPORTTYPE";
        //WORDFLOW.ParameterName = "@WORDFLOW";
        //WORDNAME.ParameterName = "@WORDNAME";

        //WORDFLOW.Value = b;
        //REPORTTYPE.Value = ENDayType;
        //WORDNAME.Value = docName;

        ////DbParameter[] parameters = { ENDayID, REPORTTYPE };
        //DbParameter[] parameters = { REPORTTYPE, WORDFLOW, WORDNAME };
        //try
        //{
        //    DataExe.GetIntExeData(sql, parameters);
        //    return 1;
        //}
        //catch (Exception ex)
        //{

        //    return 0;
        //}
        #endregion
        string sql = "";
        //sql = "UPDATE CG_YUBAO_ME SET YBWENJIANMING = '" + docName + "'";
        sql = "UPDATE CG_YUBAO_FILE SET YBNEIRONG=@YBNEIRONG, PICFILE = @PICFILE WHERE YBWENJIANMING = @YBWENJIANMING";
        var YBWENJIANMING = DataExe.GetDbParameter();
        var YBNEIRONG = DataExe.GetDbParameter();
        var PICFILE = DataExe.GetDbParameter();

        YBWENJIANMING.ParameterName = "@YBWENJIANMING";
        YBNEIRONG.ParameterName = "@YBNEIRONG";
        PICFILE.ParameterName = "@PICFILE";

        YBWENJIANMING.Value = docName;
        YBNEIRONG.Value = b;
        PICFILE.Value = btImg;

        DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
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

    /// <summary>
    /// 保存英文年预报预报单入库
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="b"></param>
    /// <param name="projectYear"></param>
    /// <returns></returns>
    public int SaveENYearWord(string FileName, byte[] b, Project_ENYear projectYear, string docName, byte[] btImg)
    {
        #region
        ////string sql = "INSERT INTO  HT_REPORT_WORD SELECT REPORT_WORD.Nextval,@ENYearID,empty_blob(),to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE FROM HT_REPORT_EN_YEAR WHERE ID = @ENYearID";
        ////string sql = "INSERT INTO  HT_REPORT_WORD (ID,reportid,wordflow,createtime,reporttype)VALUES(REPORT_WORD.Nextval,@ENYearID,empty_blob(),to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE)";
        //string sql = "INSERT INTO  HT_REPORT_WORD (ID,reportid,wordflow,createtime,reporttype,WORDNAME)VALUES(REPORT_WORD.Nextval,@ENYearID,@WORDFLOW,to_char(sysdate,'yyyy-mm-dd'),@REPORTTYPE,@WORDNAME)";
        //var WORDFLOW = DataExe.GetDbParameter();
        //var REPORTTYPE = DataExe.GetDbParameter();
        //var ENYearID = DataExe.GetDbParameter();
        //var WORDNAME = DataExe.GetDbParameter();

        //REPORTTYPE.ParameterName = "@REPORTTYPE";
        //ENYearID.ParameterName = "@ENYearID";
        //WORDFLOW.ParameterName = "@WORDFLOW";
        //WORDNAME.ParameterName = "@WORDNAME";

        //WORDFLOW.Value = b;
        //REPORTTYPE.Value = "ENYear";
        //ENYearID.Value = projectYear.ID;
        //WORDNAME.Value = docName;

        //DbParameter[] parameters = { ENYearID, REPORTTYPE, WORDFLOW, WORDNAME };
        //try
        //{
        //    DataExe.GetIntExeData(sql, parameters);
        //    return 1;
        //}
        //catch (Exception ex)
        //{

        //    return 0;
        //}
        #endregion

        string sql = "INSERT INTO CG_YUBAO_ME(YBWENJIANMING) VALUES ('" + docName + "')";
        string sql2 = "INSERT INTO CG_YUBAO_FILE(YBWENJIANMING, YBNEIRONG, PICFILE) VALUES(@YBWENJIANMING,@YBNEIRONG , @PICFILE)";

        var YBWENJIANMING = DataExe.GetDbParameter();
        var YBNEIRONG = DataExe.GetDbParameter();
        var PICFILE = DataExe.GetDbParameter();

        YBWENJIANMING.ParameterName = "@YBWENJIANMING";
        YBNEIRONG.ParameterName = "@YBNEIRONG";
        PICFILE.ParameterName = "@PICFILE";

        YBWENJIANMING.Value = docName;
        YBNEIRONG.Value = b;
        PICFILE.Value = btImg;

        DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
        try
        {
            DataExe.GetIntExeData(sql);
            DataExe.GetIntExeData(sql2, parameters);
            return 1;
        }
        catch (Exception ex)
        {

            return 0;
        }
    }

    /// <summary>
    /// 更新英文年预报单文件流
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="b"></param>
    /// <param name="projectYear"></param>
    /// <param name="docName"></param>
    /// <returns></returns>
    public int UpdateENYearWord(string FileName, byte[] b, string docName, byte[] btImg)
    {
        #region
        //string sql = "UPDATE HT_REPORT_WORD SET "
        //            + " wordflow = @WORDFLOW"
        //            + " WHERE reporttype = @REPORTTYPE AND WORDNAME = @WORDNAME";

        //var WORDFLOW = DataExe.GetDbParameter();
        //var REPORTTYPE = DataExe.GetDbParameter();
        //var WORDNAME = DataExe.GetDbParameter();

        //REPORTTYPE.ParameterName = "@REPORTTYPE";
        //WORDFLOW.ParameterName = "@WORDFLOW";
        //WORDNAME.ParameterName = "@WORDNAME";

        //WORDFLOW.Value = b;
        //REPORTTYPE.Value = "ENYear";
        //WORDNAME.Value = docName;

        //DbParameter[] parameters = { REPORTTYPE, WORDFLOW, WORDNAME };
        //try
        //{
        //    DataExe.GetIntExeData(sql, parameters);
        //    return 1;
        //}
        //catch (Exception ex)
        //{

        //    return 0;
        //}
        #endregion
        string sql = "";
        //sql = "UPDATE CG_YUBAO_ME SET YBWENJIANMING = '" + docName + "'";
        sql = "UPDATE CG_YUBAO_FILE SET YBNEIRONG=@YBNEIRONG, PICFILE = @PICFILE WHERE YBWENJIANMING = @YBWENJIANMING";
        var YBWENJIANMING = DataExe.GetDbParameter();
        var YBNEIRONG = DataExe.GetDbParameter();
        var PICFILE = DataExe.GetDbParameter();

        YBWENJIANMING.ParameterName = "@YBWENJIANMING";
        YBNEIRONG.ParameterName = "@YBNEIRONG";
        PICFILE.ParameterName = "@PICFILE";

        YBWENJIANMING.Value = docName;
        YBNEIRONG.Value = b;
        PICFILE.Value = btImg;

        DbParameter[] parameters = { YBWENJIANMING, YBNEIRONG, PICFILE };
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

    /// <summary>
    /// 获取表单数据
    /// </summary>
    /// <returns></returns>
    public object get_TableQuerybydata(string ids)
    {
        try
        {
            return DataExe.GetTableExeData("select * from HT_REPORT_WORD where ID in (" + ids + ")");
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
        if (query.YBDSIZE != "")//时间
        {
            string startdata = query.YBDSIZE.Split(',')[0];
            string enddata = query.YBDSIZE.Split(',')[1];
            wherestr += wherestr == "" ? " where " : " and ";
            wherestr += " to_date(CREATETIME,'yyyy-MM-dd')>=to_date('" + startdata + "','yyyy-MM-dd') and to_date(CREATETIME,'yyyy-MM-dd') <= to_date('" + enddata + "','yyyy-MM-dd') ";
            //var pstartdata = DataExe.GetDbParameter();
            //var penddata = DataExe.GetDbParameter();
            //pstartdata.ParameterName = "@startdata";
            //penddata.ParameterName = "@enddata";
            //pstartdata.Value = startdata;
            //penddata.Value = enddata;
            //parameter.Add(pstartdata);
            //parameter.Add(penddata);
        }
        string sql2 = "select * from(select t.*,rownum rn from(select * from HT_REPORT_WORD " + wherestr + " order by CREATETIME desc ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
        //parameters = parameter.ToArray();
        //string sql2 = "select * from(select t.*,rownum rn from(select * from HT_REPORT_WORD " + wherestr + " order by CREATETIME desc ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
        try
        {
            return DataExe.GetTableExeData(sql2, parameters);
        }
        catch (Exception ex)
        {
            WriteLog.Write("分页获取预报表单出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }

    } /// <summary>
      /// 获取总行数信息
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
            wherestr += " to_date(CREATETIME,'yyyy-MM-dd')>=to_date(@startdata,'yyyy-MM-dd') and to_date(CREATETIME,'yyyy-MM-dd') <= to_date(@enddata,'yyyy-MM-dd') ";
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
            return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_REPORT_WORD " + wherestr + "", parameters));
            //   return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_KJ_CHAOZUO_RIZHI "+ wherestr + "", parameters));
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取预报表单总数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return -1;
        }

    }
    /// <summary>
    /// 获取表单数据
    /// </summary>
    /// <returns></returns>
    public object get_TableQuerybyid(string ids)
    {
        try
        {
            return DataExe.GetTableExeData("select * from HT_REPORT_WORD where ID in (" + ids + ")");
        }
        catch (Exception ex)
        {
            WriteLog.Write("获取表单数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }
}
