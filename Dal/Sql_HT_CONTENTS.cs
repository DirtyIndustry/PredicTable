using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PredicTable.Dal
{
    public class Sql_HT_CONTENTS
    {
        DataExecution DataExe = new DataExecution();//声明一个数据执行类
        DataExecution_JB DataExe_JB = new DataExecution_JB();//声明一个数据执行类
        /// <summary>
        ///获取警报联系人数据
        /// </summary>
        /// <returns></returns>

        public DataTable GetContentsData()
        {
            try
            {
                var sql = "select id, contentsname, contentscode from HT_CONTENTS";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///获取发往数据
        /// </summary>
        /// <returns></returns>

        public DataTable GetGroupData(string FAXGROUP1)
        {
            try
            {
                //var sql = "select ails.USERNAME from TBLFAXCONTACTS acts left join TBLFAXCONTACTSDETAILS ails on acts.FAXID=ails.FAXID where acts.FAXGROUP='"+ FAXGROUP + "'";
                //var sql = "select replace(WMSYS.WM_CONCAT(ails.USERNAME),',',';') USERNAME from TBLFAXCONTACTS acts left join TBLFAXCONTACTSDETAILS ails on acts.FAXID=ails.FAXID where acts.FAXGROUP='" + FAXGROUP + "'";
                var sql = "select  replace(WMSYS.WM_CONCAT(USERNAME),',',';') USERNAME from  "
                    + " ("
                    + " select ails.USERNAME  from TBLFAXCONTACTS acts left join TBLFAXCONTACTSDETAILS ails on acts.FAXID=ails.FAXID where acts.FAXGROUP=@FAXGROUP"
                + " union"
                    + " select b.USERNAME  from TBLMAILCONTACTS a left join TBLMAILCONTACTSDETAILS b on a.MAILID=b.MAILID where a.MAILGROUP=@FAXGROUP"
                + " )";
                //var sql = "select replace(WMSYS.WM_CONCAT(ails.USERNAME),',',';') USERNAME from TBLFAXCONTACTSDETAILS ails where FAXID = (select FAXID from TBLFAXCONTACTS where FAXGROUP='" + FAXGROUP + "')";

                var FAXGROUP = DataExe.GetDbParameter();
                FAXGROUP.ParameterName = "@FAXGROUP";
                FAXGROUP.Value = FAXGROUP1;
                DbParameter[] parameters = { FAXGROUP};
                return DataExe_JB.GetTableExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///警报文件内容保存
        /// </summary>
        /// <returns></returns>

        public DataTable AddJingBaoContent()
        {
            try
            {
                var sql = "select ID, groupname, createtime, unitname from HT_GROUPUNIT";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 警报

        /// <summary>
        /// 警报文件内容新增保存
        /// </summary>
        /// <returns></returns>
        public int AddJingBaoContent(CG_HT_JINGBAO_CONTENT Contentvalue)
        {
            string sql = "INSERT INTO  CG_HT_JINGBAO_CONTENT (JBWENJIANMING,CONTENT,SENTTO,PICTURE,CONTENTTABLE,ISSUEPICTURE,LINKMAN,JBTITLE,DATETIME) VALUES (@JBWENJIANMING,@CONTENT,@SENTTO,@PICTURE,@CONTENTTABLE,@ISSUEPICTURE,@LINKMAN,@JBTITLE,@DATETIME)";
            var JBWENJIANMING = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();
            var SENTTO = DataExe.GetDbParameter();
            var PICTURE = DataExe.GetDbParameter();
            var CONTENTTABLE = DataExe.GetDbParameter();
            var ISSUEPICTURE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var JBTITLE= DataExe.GetDbParameter();
            var DATETIME= DataExe.GetDbParameter();

            JBWENJIANMING.ParameterName = "@JBWENJIANMING";
            CONTENT.ParameterName = "@CONTENT";
            SENTTO.ParameterName = "@SENTTO";
            PICTURE.ParameterName = "@PICTURE";
            CONTENTTABLE.ParameterName = "@CONTENTTABLE";
            ISSUEPICTURE.ParameterName = "@ISSUEPICTURE";
            LINKMAN.ParameterName = "@LINKMAN";
            JBTITLE.ParameterName = "@JBTITLE";
            DATETIME.ParameterName = "@DATETIME";

            JBWENJIANMING.Value = Contentvalue.JBWENJIANMING;
            CONTENT.Value = Contentvalue.CONTENT;
            SENTTO.Value = Contentvalue.SENTTO;
            PICTURE.Value = Contentvalue.PICTURE;
            CONTENTTABLE.Value = Contentvalue.CONTENTTABLE;
            ISSUEPICTURE.Value = Contentvalue.ISSUEPICTURE;
            LINKMAN.Value = Contentvalue.LINKMAN;
            JBTITLE.Value = Contentvalue.JBTITLE;
            DATETIME.Value = Contentvalue.DATETIME;

            DbParameter[] parameters = { JBWENJIANMING, CONTENT, SENTTO, PICTURE , CONTENTTABLE , ISSUEPICTURE , LINKMAN , JBTITLE, DATETIME };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增警报文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 警报文件内容编辑保存
        /// </summary>
        /// <returns></returns>
        public int UpdateJingBaoContent(CG_HT_JINGBAO_CONTENT Contentvalue)
        {
            string sql = "UPDATE CG_HT_JINGBAO_CONTENT SET CONTENT=@CONTENT,SENTTO=@SENTTO,PICTURE=@PICTURE,CONTENTTABLE=@CONTENTTABLE,ISSUEPICTURE=@ISSUEPICTURE,LINKMAN=@LINKMAN,JBTITLE=@JBTITLE,DATETIME=@DATETIME where JBWENJIANMING=@JBWENJIANMING";
            var JBWENJIANMING = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();
            var SENTTO = DataExe.GetDbParameter();
            var PICTURE = DataExe.GetDbParameter();
            var CONTENTTABLE = DataExe.GetDbParameter();
            var ISSUEPICTURE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var JBTITLE = DataExe.GetDbParameter();
            var DATETIME = DataExe.GetDbParameter();

            JBWENJIANMING.ParameterName = "@JBWENJIANMING";
            CONTENT.ParameterName = "@CONTENT";
            SENTTO.ParameterName = "@SENTTO";
            PICTURE.ParameterName = "@PICTURE";
            CONTENTTABLE.ParameterName = "@CONTENTTABLE";
            ISSUEPICTURE.ParameterName = "@ISSUEPICTURE";
            LINKMAN.ParameterName = "@LINKMAN";
            JBTITLE.ParameterName = "@JBTITLE";
            DATETIME.ParameterName = "@DATETIME";

            JBWENJIANMING.Value = Contentvalue.JBWENJIANMING;
            CONTENT.Value = Contentvalue.CONTENT;
            SENTTO.Value = Contentvalue.SENTTO;
            PICTURE.Value = Contentvalue.PICTURE;
            CONTENTTABLE.Value = Contentvalue.CONTENTTABLE;
            ISSUEPICTURE.Value = Contentvalue.ISSUEPICTURE;
            LINKMAN.Value = Contentvalue.LINKMAN;
            JBTITLE.Value= Contentvalue.JBTITLE;
            DATETIME.Value = Contentvalue.DATETIME;


            DbParameter[] parameters = { JBWENJIANMING, CONTENT, SENTTO, PICTURE, CONTENTTABLE, ISSUEPICTURE, LINKMAN, JBTITLE, DATETIME };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("编辑警报文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 警报文件属性保存
        /// </summary>
        /// <returns></returns>
        public int AddJingBaoMe(CG_JINGBAO_ME JingBaomI)
        {
            string sql = "INSERT INTO  CG_JINGBAO_ME (JBWENJIANMING,JBQUYU,JBNEIRONG,JBBIANHAO,JBJIBIE,JBSHIJIAN,JBDANWEI) VALUES (@JBWENJIANMING,@JBQUYU,@JBNEIRONG,@JBBIANHAO,@JBJIBIE, @JBSHIJIAN,@JBDANWEI)";
            
            var JBWENJIANMING = DataExe.GetDbParameter();
            var JBQUYU = DataExe.GetDbParameter();
            var JBNEIRONG = DataExe.GetDbParameter();
            var JBBIANHAO = DataExe.GetDbParameter();
            var JBJIBIE = DataExe.GetDbParameter();
            var JBSHIJIAN = DataExe.GetDbParameter();
            var JBDANWEI = DataExe.GetDbParameter();

            JBWENJIANMING.ParameterName = "@JBWENJIANMING";
            JBQUYU.ParameterName = "@JBQUYU";
            JBNEIRONG.ParameterName = "@JBNEIRONG";
            JBBIANHAO.ParameterName = "@JBBIANHAO";
            JBJIBIE.ParameterName = "@JBJIBIE";
            JBSHIJIAN.ParameterName = "@JBSHIJIAN";
            JBDANWEI.ParameterName = "@JBDANWEI";

            JBWENJIANMING.Value = JingBaomI.JBWENJIANMING;
            JBQUYU.Value = JingBaomI.JBQUYU;
            JBNEIRONG.Value = JingBaomI.JBNEIRONG;
            JBBIANHAO.Value = JingBaomI.JBBIANHAO;
            JBJIBIE.Value = JingBaomI.JBJIBIE;
            JBSHIJIAN.Value = JingBaomI.JBSHIJIAN;
            JBDANWEI.Value = JingBaomI.JBDANWEI;

            DbParameter[] parameters = { JBWENJIANMING, JBQUYU, JBNEIRONG, JBBIANHAO, JBJIBIE, JBSHIJIAN, JBDANWEI };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增警报文件属性出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 警报文件属性保存
        /// </summary>
        /// <returns></returns>
        public int UpdateJingBaoMe(CG_JINGBAO_ME JingBaomI)
        {
            string sql = "UPDATE CG_JINGBAO_ME SET JBQUYU=@JBQUYU,JBNEIRONG=@JBNEIRONG,JBBIANHAO=@JBBIANHAO,JBJIBIE=@JBJIBIE,JBSHIJIAN=@JBSHIJIAN,JBDANWEI=@JBDANWEI where JBWENJIANMING=@JBWENJIANMING";
            var JBWENJIANMING = DataExe.GetDbParameter();
            var JBQUYU = DataExe.GetDbParameter();
            var JBNEIRONG = DataExe.GetDbParameter();
            var JBBIANHAO = DataExe.GetDbParameter();
            var JBJIBIE = DataExe.GetDbParameter();
            var JBSHIJIAN = DataExe.GetDbParameter();
            var JBDANWEI = DataExe.GetDbParameter();

            JBWENJIANMING.ParameterName = "@JBWENJIANMING";
            JBQUYU.ParameterName = "@JBQUYU";
            JBNEIRONG.ParameterName = "@JBNEIRONG";
            JBBIANHAO.ParameterName = "@JBBIANHAO";
            JBJIBIE.ParameterName = "@JBJIBIE";
            JBSHIJIAN.ParameterName = "@JBSHIJIAN";
            JBDANWEI.ParameterName = "@JBDANWEI";

            JBWENJIANMING.Value = JingBaomI.JBWENJIANMING;
            JBQUYU.Value = JingBaomI.JBQUYU;
            JBNEIRONG.Value = JingBaomI.JBNEIRONG;
            JBBIANHAO.Value = JingBaomI.JBBIANHAO;
            JBJIBIE.Value = JingBaomI.JBJIBIE;
            JBSHIJIAN.Value = JingBaomI.JBSHIJIAN;
            JBDANWEI.Value = JingBaomI.JBDANWEI;

            DbParameter[] parameters = { JBWENJIANMING, JBQUYU, JBNEIRONG, JBBIANHAO, JBJIBIE, JBSHIJIAN, JBDANWEI };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("编辑警报文件属性出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        
        /// <summary>
        /// 警报文件保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_JingBaoFILE_AllData(CG_JINGBAO_FILE JingBaomFile)
        {
            try
            {
                string sql = "select JBWENJIANMING from CG_JINGBAO_FILE where JBWENJIANMING='" + JingBaomFile.JBWENJIANMING+"'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 警报文件属性保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_JingBaoME_AllData(CG_JINGBAO_ME JingBaomFile)
        {
            try
            {
                string sql = "select JBWENJIANMING from CG_JINGBAO_FILE where JBWENJIANMING='" + JingBaomFile.JBWENJIANMING + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 警报文件内容保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_JingBaoCON_AllData(CG_HT_JINGBAO_CONTENT JingBaomFile)
        {
            try
            {
                string sql = "select JBWENJIANMING from CG_HT_JINGBAO_CONTENT where JBWENJIANMING='" + JingBaomFile.JBWENJIANMING + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int AddJingBaoContents(CG_HT_JINGBAO_CONTENT Contentvalue)
        {
            string sql = "INSERT INTO  CG_HT_JINGBAO_CONTENT (JBWENJIANMING,CONTENT,SENTTO,CONTENTTABLE,ISSUEPICTURE,LINKMAN,JBTITLE,DATETIME,JBREMARKS) VALUES (@JBWENJIANMING,@CONTENT,@SENTTO,@CONTENTTABLE,@ISSUEPICTURE,@LINKMAN,@JBTITLE,@DATETIME,@JBREMARKS)";
            var JBWENJIANMING = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();
            var SENTTO = DataExe.GetDbParameter();
            var CONTENTTABLE = DataExe.GetDbParameter();
            var ISSUEPICTURE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var JBTITLE = DataExe.GetDbParameter();
            var DATETIME = DataExe.GetDbParameter();
            var JBREMARKS = DataExe.GetDbParameter();
            // var PICTURE = DataExe.GetDbParameter();

            JBWENJIANMING.ParameterName = "@JBWENJIANMING";
            CONTENT.ParameterName = "@CONTENT";
            SENTTO.ParameterName = "@SENTTO";
            CONTENTTABLE.ParameterName = "@CONTENTTABLE";
            ISSUEPICTURE.ParameterName = "@ISSUEPICTURE";
            LINKMAN.ParameterName = "@LINKMAN";
            JBTITLE.ParameterName = "@JBTITLE";
            DATETIME.ParameterName = "@DATETIME";
            JBREMARKS.ParameterName = "@JBREMARKS";
            // PICTURE.ParameterName = "@PICTURE";

            JBWENJIANMING.Value = Contentvalue.JBWENJIANMING;
            CONTENT.Value = Contentvalue.CONTENT;
            SENTTO.Value = Contentvalue.SENTTO;
            CONTENTTABLE.Value = Contentvalue.CONTENTTABLE;
            ISSUEPICTURE.Value = Contentvalue.ISSUEPICTURE;
            LINKMAN.Value = Contentvalue.LINKMAN;
            JBTITLE.Value = Contentvalue.JBTITLE;
            DATETIME.Value = Contentvalue.DATETIME;
            JBREMARKS.Value = Contentvalue.JBREMARKS;
            // PICTURE.Value = Contentvalue.PICTURE;

            DbParameter[] parameters = { JBWENJIANMING, CONTENT, SENTTO, CONTENTTABLE, ISSUEPICTURE, LINKMAN, JBTITLE, DATETIME, JBREMARKS };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增警报文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 警报文件内容编辑保存
        /// </summary>
        /// <returns></returns>
        public int UpdateJingBaoContents(CG_HT_JINGBAO_CONTENT Contentvalue)
        {
            string sql = "UPDATE CG_HT_JINGBAO_CONTENT SET CONTENT=@CONTENT,SENTTO=@SENTTO,CONTENTTABLE=@CONTENTTABLE,ISSUEPICTURE=@ISSUEPICTURE,LINKMAN=@LINKMAN,JBTITLE=@JBTITLE,DATETIME=@DATETIME，JBREMARKS=@JBREMARKS where JBWENJIANMING=@JBWENJIANMING";
            var JBWENJIANMING = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();
            var SENTTO = DataExe.GetDbParameter();
            var CONTENTTABLE = DataExe.GetDbParameter();
            var ISSUEPICTURE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var JBTITLE = DataExe.GetDbParameter();
            var DATETIME = DataExe.GetDbParameter();
            var JBREMARKS = DataExe.GetDbParameter();
            //var PICTURE = DataExe.GetDbParameter();

            JBWENJIANMING.ParameterName = "@JBWENJIANMING";
            CONTENT.ParameterName = "@CONTENT";
            SENTTO.ParameterName = "@SENTTO";
            CONTENTTABLE.ParameterName = "@CONTENTTABLE";
            ISSUEPICTURE.ParameterName = "@ISSUEPICTURE";
            LINKMAN.ParameterName = "@LINKMAN";
            JBTITLE.ParameterName = "@JBTITLE";
            DATETIME.ParameterName = "@DATETIME";
            JBREMARKS.ParameterName = "@JBREMARKS";
            //  PICTURE.ParameterName = "@PICTURE";

            JBWENJIANMING.Value = Contentvalue.JBWENJIANMING;
            CONTENT.Value = Contentvalue.CONTENT;
            SENTTO.Value = Contentvalue.SENTTO;
            CONTENTTABLE.Value = Contentvalue.CONTENTTABLE;
            ISSUEPICTURE.Value = Contentvalue.ISSUEPICTURE;
            LINKMAN.Value = Contentvalue.LINKMAN;
            JBTITLE.Value = Contentvalue.JBTITLE;
            DATETIME.Value = Contentvalue.DATETIME;
            JBREMARKS.Value = Contentvalue.JBREMARKS;
            // PICTURE.Value = Contentvalue.PICTURE;

            // DbParameter[] parameters = { JBWENJIANMING, CONTENT, SENTTO, CONTENTTABLE, ISSUEPICTURE, LINKMAN, JBTITLE, DATETIME, PICTURE }
            DbParameter[] parameters = { JBWENJIANMING, CONTENT, SENTTO, CONTENTTABLE, ISSUEPICTURE, LINKMAN, JBTITLE, DATETIME, JBREMARKS };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("编辑警报文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 警报文件新增保存
        /// </summary>
        /// <returns></returns>
        public int AddJingBaoFile(CG_JINGBAO_FILE JingBaomFile)
        {
            string sql = "INSERT INTO  CG_JINGBAO_FILE (JBWENJIANMING,JBNEIRONG,PICFILE) VALUES (@JBWENJIANMING,@JBNEIRONG,@PICFILE)";
            var JBWENJIANMING = DataExe.GetDbParameter();
            var JBNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            JBWENJIANMING.ParameterName = "@JBWENJIANMING";
            JBNEIRONG.ParameterName = "@JBNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            JBWENJIANMING.Value = JingBaomFile.JBWENJIANMING;
            JBNEIRONG.Value = JingBaomFile.JBNEIRONG;
            PICFILE.Value = JingBaomFile.PICFILE;

            DbParameter[] parameters = { JBWENJIANMING, JBNEIRONG, PICFILE};

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增警报文件出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 警报文件修改保存
        /// </summary>
        /// <returns></returns>
        public int UpdateJingBaoFile(CG_JINGBAO_FILE JingBaomFile)
        {
            string sql = "UPDATE  CG_JINGBAO_FILE SET  JBNEIRONG = @JBNEIRONG, PICFILE = @PICFILE WHERE JBWENJIANMING = @JBWENJIANMING";

            var JBWENJIANMING = DataExe.GetDbParameter();
            var JBNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            JBWENJIANMING.ParameterName = "@JBWENJIANMING";
            JBNEIRONG.ParameterName = "@JBNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            JBWENJIANMING.Value = JingBaomFile.JBWENJIANMING;
            JBNEIRONG.Value = JingBaomFile.JBNEIRONG;
            PICFILE.Value = JingBaomFile.PICFILE;

            DbParameter[] parameters = { JBWENJIANMING, JBNEIRONG, PICFILE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改联系人信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 警报数据查询绑定
        /// </summary>
        /// <returns></returns>
        public DataTable getJingBao(CG_JINGBAO_ME XXME)
        {
            try
            {
                string sql = "select* from(select C.CONTENT,M.JBNEIRONG,M.JBBIANHAO,C.LINKMAN,C.SENTTO from CG_JINGBAO_ME M left join CG_HT_JINGBAO_CONTENT C on M.JBWENJIANMING=C.JBWENJIANMING where M.JBDANWEI='" + XXME.JBDANWEI + "'and M.JBNEIRONG='" + XXME.JBNEIRONG + "'order by C.DATETIME desc ) where rownum<2";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 警报数据根据编号查询绑定
        /// </summary>
        /// <returns></returns>
        public DataTable getJingBaoBH(CG_JINGBAO_ME XXME)
        {
            try
            {
                string sql = "select* from(select C.CONTENT,M.JBNEIRONG,M.JBBIANHAO,C.LINKMAN,C.SENTTO from CG_JINGBAO_ME M left join CG_HT_JINGBAO_CONTENT C on M.JBWENJIANMING=C.JBWENJIANMING where M.JBNEIRONG='" + XXME.JBNEIRONG + "' and M.JBBIANHAO='" + XXME.JBBIANHAO+ "' order by C.DATETIME desc ) where rownum<2";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 消息
        /// <summary>
        /// 消息文件内容新增保存
        /// </summary>
        /// <returns></returns>
        public int AddXiaoXiContent(CG_HT_XIAOXI_CONTENT Contentvalue)
        {
            string sql = "INSERT INTO  CG_HT_XIAOXI_CONTENT (XXWENJIANMING,CONTENT,SENTTO,ISSUEPICTURE,LINKMAN,XXTITLE,DATETIME) VALUES (@XXWENJIANMING,@CONTENT,@SENTTO,@ISSUEPICTURE,@LINKMAN,@XXTITLE,@DATETIME)";
            var XXWENJIANMING = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();
            var SENTTO = DataExe.GetDbParameter();
            var ISSUEPICTURE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var XXTITLE = DataExe.GetDbParameter();
            var DATETIME= DataExe.GetDbParameter();

            XXWENJIANMING.ParameterName = "@XXWENJIANMING";
            CONTENT.ParameterName = "@CONTENT";
            SENTTO.ParameterName = "@SENTTO";
            ISSUEPICTURE.ParameterName = "@ISSUEPICTURE";
            LINKMAN.ParameterName = "@LINKMAN";
            XXTITLE.ParameterName = "@XXTITLE";
            DATETIME.ParameterName = "@DATETIME";

            XXWENJIANMING.Value = Contentvalue.XXWENJIANMING;
            CONTENT.Value = Contentvalue.CONTENT;
            SENTTO.Value = Contentvalue.SENTTO;
            ISSUEPICTURE.Value = Contentvalue.ISSUEPICTURE;
            LINKMAN.Value = Contentvalue.LINKMAN;
            XXTITLE.Value = Contentvalue.XXTITLE;
            DATETIME.Value = Contentvalue.DATETIME;


            DbParameter[] parameters = { XXWENJIANMING, CONTENT, SENTTO, ISSUEPICTURE, LINKMAN, XXTITLE, DATETIME };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增消息文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 消息文件内容编辑保存
        /// </summary>
        /// <returns></returns>
        public int UpdateXiaoXiContent(CG_HT_XIAOXI_CONTENT Contentvalue)
        {
            string sql = "UPDATE CG_HT_XIAOXI_CONTENT SET CONTENT=@CONTENT,SENTTO=@SENTTO,ISSUEPICTURE=@ISSUEPICTURE,LINKMAN=@LINKMAN,XXTITLE=@XXTITLE,DATETIME=@DATETIME where XXWENJIANMING=@XXWENJIANMING";
            var XXWENJIANMING = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();
            var SENTTO = DataExe.GetDbParameter();
            var ISSUEPICTURE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var XXTITLE = DataExe.GetDbParameter();
            var DATETIME = DataExe.GetDbParameter();
            XXWENJIANMING.ParameterName = "@XXWENJIANMING";
            CONTENT.ParameterName = "@CONTENT";
            SENTTO.ParameterName = "@SENTTO";
            ISSUEPICTURE.ParameterName = "@ISSUEPICTURE";
            LINKMAN.ParameterName = "@LINKMAN";
            XXTITLE.ParameterName = "@XXTITLE";
            DATETIME.ParameterName = "@DATETIME";

            XXWENJIANMING.Value = Contentvalue.XXWENJIANMING;
            CONTENT.Value = Contentvalue.CONTENT;
            SENTTO.Value = Contentvalue.SENTTO;
            ISSUEPICTURE.Value = Contentvalue.ISSUEPICTURE;
            LINKMAN.Value = Contentvalue.LINKMAN;
            XXTITLE.Value = Contentvalue.XXTITLE;
            DATETIME.Value = Contentvalue.DATETIME;

            DbParameter[] parameters = { XXWENJIANMING, CONTENT, SENTTO, ISSUEPICTURE, LINKMAN, XXTITLE, DATETIME };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("编辑消息文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        
        /// <summary>
        /// 消息文件属性保存
        /// </summary>
        /// <returns></returns>
        public int AddXiaoXiMe(CG_XIAOXI_ME JingBaomI)
        {
            string sql = "INSERT INTO  CG_XIAOXI_ME (XXWENJIANMING,XXQUYU,XXNEIRONG,XXBIANHAO,XXSHIJIAN,XXDANWEI) VALUES (@XXWENJIANMING,@XXQUYU,@XXNEIRONG,@XXBIANHAO,@XXSHIJIAN,@XXDANWEI)";
            var XXWENJIANMING = DataExe.GetDbParameter();
            var XXQUYU = DataExe.GetDbParameter();
            var XXNEIRONG = DataExe.GetDbParameter();
            var XXBIANHAO = DataExe.GetDbParameter();
            var XXSHIJIAN = DataExe.GetDbParameter();
            var XXDANWEI = DataExe.GetDbParameter();

            XXWENJIANMING.ParameterName = "@XXWENJIANMING";
            XXQUYU.ParameterName = "@XXQUYU";
            XXNEIRONG.ParameterName = "@XXNEIRONG";
            XXBIANHAO.ParameterName = "@XXBIANHAO";
            XXSHIJIAN.ParameterName = "@XXSHIJIAN";
            XXDANWEI.ParameterName = "@XXDANWEI";

            XXWENJIANMING.Value = JingBaomI.XXWENJIANMING;
            XXQUYU.Value = JingBaomI.XXQUYU;
            XXNEIRONG.Value = JingBaomI.XXNEIRONG;
            XXBIANHAO.Value = JingBaomI.XXBIANHAO;
            XXSHIJIAN.Value = JingBaomI.XXSHIJIAN;
            XXDANWEI.Value = JingBaomI.XXDANWEI;

            DbParameter[] parameters = { XXWENJIANMING, XXQUYU, XXNEIRONG, XXBIANHAO,XXSHIJIAN, XXDANWEI };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增消息文件属性出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 消息文件属性保存
        /// </summary>
        /// <returns></returns>
        public int UpdateXiaoXiMe(CG_XIAOXI_ME JingBaomI)
        {
            string sql = "UPDATE CG_XIAOXI_ME SET XXQUYU=@XXQUYU,XXNEIRONG=@XXNEIRONG,XXBIANHAO=@XXBIANHAO,XXSHIJIAN=@XXSHIJIAN,XXDANWEI=@XXDANWEI where XXWENJIANMING=@XXWENJIANMING";
            var XXWENJIANMING = DataExe.GetDbParameter();
            var XXQUYU = DataExe.GetDbParameter();
            var XXNEIRONG = DataExe.GetDbParameter();
            var XXBIANHAO = DataExe.GetDbParameter();
            var XXSHIJIAN = DataExe.GetDbParameter();
            var XXDANWEI = DataExe.GetDbParameter();

            XXWENJIANMING.ParameterName = "@XXWENJIANMING";
            XXQUYU.ParameterName = "@XXQUYU";
            XXNEIRONG.ParameterName = "@XXNEIRONG";
            XXBIANHAO.ParameterName = "@XXBIANHAO";
            XXSHIJIAN.ParameterName = "@XXSHIJIAN";
            XXDANWEI.ParameterName = "@XXDANWEI";

            XXWENJIANMING.Value = JingBaomI.XXWENJIANMING;
            XXQUYU.Value = JingBaomI.XXQUYU;
            XXNEIRONG.Value = JingBaomI.XXNEIRONG;
            XXBIANHAO.Value = JingBaomI.XXBIANHAO;
            XXSHIJIAN.Value = JingBaomI.XXSHIJIAN;
            XXDANWEI.Value = JingBaomI.XXDANWEI;

            DbParameter[] parameters = { XXWENJIANMING, XXQUYU, XXNEIRONG, XXBIANHAO, XXSHIJIAN, XXDANWEI};

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("编辑消息文件属性出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        

        /// <summary>
        /// 消息文件保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_XiaoXiFILE_AllData(CG_XIAOXI_FILE JingBaomFile)
        {
            try
            {
                string sql = "select XXWENJIANMING from CG_XIAOXI_FILE where XXWENJIANMING='" + JingBaomFile.XXWENJIANMING + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 消息文件属性保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_XiaoXiME_AllData(CG_XIAOXI_ME JingBaomFile)
        {
            try
            {
                string sql = "select XXWENJIANMING from CG_XIAOXI_ME where XXWENJIANMING='" + JingBaomFile.XXWENJIANMING + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 消息文件内容保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_XiaoXiCON_AllData(CG_HT_XIAOXI_CONTENT JingBaomFile)
        {
            try
            {
                string sql = "select XXWENJIANMING from CG_HT_XIAOXI_CONTENT where XXWENJIANMING='" + JingBaomFile.XXWENJIANMING + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 消息文件新增保存
        /// </summary>
        /// <returns></returns>
        public int AddXiaoXiFile(CG_XIAOXI_FILE JingBaomFile)
        {
            string sql = "INSERT INTO  CG_XIAOXI_FILE (XXWENJIANMING,XXNEIRONG,PICFILE) VALUES (@XXWENJIANMING,@XXNEIRONG,@PICFILE)";
            var XXWENJIANMING = DataExe.GetDbParameter();
            var XXNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            XXWENJIANMING.ParameterName = "@XXWENJIANMING";
            XXNEIRONG.ParameterName = "@XXNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            XXWENJIANMING.Value = JingBaomFile.XXWENJIANMING;
            XXNEIRONG.Value = JingBaomFile.XXNEIRONG;
            PICFILE.Value = JingBaomFile.PICFILE;

            DbParameter[] parameters = { XXWENJIANMING, XXNEIRONG, PICFILE };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增消息文件出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 消息文件修改保存
        /// </summary>
        /// <returns></returns>
        public int UpdateXiaoXiFile(CG_XIAOXI_FILE JingBaomFile)
        {
            string sql = "UPDATE  CG_XIAOXI_FILE SET  XXNEIRONG = @XXNEIRONG, PICFILE = @PICFILE WHERE XXWENJIANMING = @XXWENJIANMING";

            var XXWENJIANMING = DataExe.GetDbParameter();
            var XXNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            XXWENJIANMING.ParameterName = "@XXWENJIANMING";
            XXNEIRONG.ParameterName = "@XXNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            XXWENJIANMING.Value = JingBaomFile.XXWENJIANMING;
            XXNEIRONG.Value = JingBaomFile.XXNEIRONG;
            PICFILE.Value = JingBaomFile.PICFILE;

            DbParameter[] parameters = { XXWENJIANMING, XXNEIRONG, PICFILE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改联系人信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// 消息数据查询绑定
        /// </summary>
        /// <returns></returns>
        public DataTable getXinXi(CG_XIAOXI_ME XXME)
        {
            try
            {
                string sql = "select* from(select C.CONTENT,C.LINKMAN,C.SENTTO from CG_XIAOXI_ME M left join cg_ht_xiaoxi_content C on M.XXWENJIANMING=C.XXWENJIANMING where M.XXDANWEI='" + XXME.XXDANWEI + "'and M.XXNEIRONG='" + XXME.XXNEIRONG+ "' order by C.DATETIME desc ) where rownum<2";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        

        #endregion

        #region 解除警报
        /// <summary>
        /// 解除警报文件内容编辑保存
        /// </summary>
        /// <returns></returns>
        public int AddJieChuJingBaoContent(CG_HT_JIECHUJINGBAO_CONTENT Contentvalue)
        {
            string sql = "INSERT INTO  CG_HT_JIECHUJINGBAO_CONTENT (JCJBWENJIANMING,CONTENT,SENTTO,ISSUEPICTURE,LINKMAN,JCTITLE,DATETIME,JCREMARKS) VALUES (@JCJBWENJIANMING,@CONTENT,@SENTTO,@ISSUEPICTURE,@LINKMAN,@JCTITLE,@DATETIME,@JCREMARKS)";
            var JCJBWENJIANMING = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();
            var SENTTO = DataExe.GetDbParameter();
            var ISSUEPICTURE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var JCTITLE = DataExe.GetDbParameter();
            var DATETIME = DataExe.GetDbParameter();
            var JCREMARKS = DataExe.GetDbParameter();

            JCJBWENJIANMING.ParameterName = "@JCJBWENJIANMING";
            CONTENT.ParameterName = "@CONTENT";
            SENTTO.ParameterName = "@SENTTO";
            ISSUEPICTURE.ParameterName = "@ISSUEPICTURE";
            LINKMAN.ParameterName = "@LINKMAN";
            JCTITLE.ParameterName = "@JCTITLE";
            DATETIME.ParameterName = "@DATETIME";
            JCREMARKS.ParameterName = "@JCREMARKS";

            JCJBWENJIANMING.Value = Contentvalue.JCJBWENJIANMING;
            CONTENT.Value = Contentvalue.CONTENT;
            SENTTO.Value = Contentvalue.SENTTO;
            ISSUEPICTURE.Value = Contentvalue.ISSUEPICTURE;
            LINKMAN.Value = Contentvalue.LINKMAN;
            JCTITLE.Value = Contentvalue.JCTITLE;
            DATETIME.Value = Contentvalue.DATETIME;
            JCREMARKS.Value = Contentvalue.JCREMARKS;

            DbParameter[] parameters = { JCJBWENJIANMING, CONTENT, SENTTO, ISSUEPICTURE, LINKMAN, JCTITLE, DATETIME, JCREMARKS };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增解除警报文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 解除警报文件内容编辑保存
        /// </summary>
        /// <returns></returns>
        public int UpdateJieChuJingBaoContent(CG_HT_JIECHUJINGBAO_CONTENT Contentvalue)
        {
            string sql = "UPDATE  CG_HT_JIECHUJINGBAO_CONTENT  set CONTENT = @CONTENT,SENTTO = @SENTTO,ISSUEPICTURE = @ISSUEPICTURE,LINKMAN = @LINKMAN,JCTITLE=@JCTITLE,DATETIME=@DATETIME,JCREMARKS=@JCREMARKS where JCJBWENJIANMING=@JCJBWENJIANMING";
            var JCJBWENJIANMING = DataExe.GetDbParameter();
            var CONTENT = DataExe.GetDbParameter();
            var SENTTO = DataExe.GetDbParameter();
            var ISSUEPICTURE = DataExe.GetDbParameter();
            var LINKMAN = DataExe.GetDbParameter();
            var JCTITLE= DataExe.GetDbParameter();
            var DATETIME = DataExe.GetDbParameter();
            var JCREMARKS = DataExe.GetDbParameter();

            JCJBWENJIANMING.ParameterName = "@JCJBWENJIANMING";
            CONTENT.ParameterName = "@CONTENT";
            SENTTO.ParameterName = "@SENTTO";
            ISSUEPICTURE.ParameterName = "@ISSUEPICTURE";
            LINKMAN.ParameterName = "@LINKMAN";
            JCTITLE.ParameterName = "@JCTITLE";
            DATETIME.ParameterName = "@DATETIME";
            JCREMARKS.ParameterName = "@JCREMARKS";

            JCJBWENJIANMING.Value = Contentvalue.JCJBWENJIANMING;
            CONTENT.Value = Contentvalue.CONTENT;
            SENTTO.Value = Contentvalue.SENTTO;
            ISSUEPICTURE.Value = Contentvalue.ISSUEPICTURE;
            LINKMAN.Value = Contentvalue.LINKMAN;
            JCTITLE.Value= Contentvalue.JCTITLE;
            DATETIME.Value = Contentvalue.DATETIME;
            JCREMARKS.Value = Contentvalue.JCREMARKS;

            DbParameter[] parameters = { JCJBWENJIANMING, CONTENT, SENTTO, ISSUEPICTURE, LINKMAN, JCTITLE, DATETIME, JCREMARKS };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("编辑解除警报文件内容出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        
        /// <summary>
        /// 解除警报文件属性新增保存
        /// </summary>
        /// <returns></returns>
        public int AddJieChuJingBaoMe(CG_JIECHUJINGBAO_ME JingBaomI)
        {
            string sql = "INSERT INTO  CG_JIECHUJINGBAO_ME (JCWENJIANMING,JCQUYU,JCNEIRONG,JCBIANHAO,JCJIBIE,JCSHIJIAN,JCDANWEI) VALUES (@JCWENJIANMING,@JCQUYU,@JCNEIRONG,@JCBIANHAO,@JCJIBIE,@JCSHIJIAN,@JCDANWEI)";
            var JCWENJIANMING = DataExe.GetDbParameter();
            var JCQUYU = DataExe.GetDbParameter();
            var JCNEIRONG = DataExe.GetDbParameter();
            var JCBIANHAO = DataExe.GetDbParameter();
            var JCJIBIE = DataExe.GetDbParameter();
            var JCSHIJIAN = DataExe.GetDbParameter();
            var JCDANWEI = DataExe.GetDbParameter();

            JCWENJIANMING.ParameterName = "@JCWENJIANMING";
            JCQUYU.ParameterName = "@JCQUYU";
            JCNEIRONG.ParameterName = "@JCNEIRONG";
            JCBIANHAO.ParameterName = "@JCBIANHAO";
            JCJIBIE.ParameterName = "@JCJIBIE";
            JCSHIJIAN.ParameterName = "@JCSHIJIAN";
            JCDANWEI.ParameterName = "@JCDANWEI";

            JCWENJIANMING.Value = JingBaomI.JCWENJIANMING;
            JCQUYU.Value = JingBaomI.JCQUYU;
            JCNEIRONG.Value = JingBaomI.JCNEIRONG;
            JCBIANHAO.Value = JingBaomI.JCBIANHAO;
            JCJIBIE.Value = JingBaomI.JCJIBIE;
            JCSHIJIAN.Value = JingBaomI.JCSHIJIAN;
            JCDANWEI.Value = JingBaomI.JCDANWEI;

            DbParameter[] parameters = { JCWENJIANMING, JCQUYU, JCNEIRONG, JCBIANHAO, JCJIBIE, JCSHIJIAN, JCDANWEI };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增解除警报文件属性出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        /// <summary>
        /// 解除警报文件属性修改保存
        /// </summary>
        /// <returns></returns>
        public int UpdateJieChuJingBaoMe(CG_JIECHUJINGBAO_ME JingBaomI)
        {
            string sql = "Update  CG_JIECHUJINGBAO_ME set JCQUYU=@JCQUYU,JCNEIRONG=@JCNEIRONG,JCBIANHAO=@JCBIANHAO,JCJIBIE=@JCJIBIE,JCSHIJIAN=@JCSHIJIAN,JCDANWEI=@JCDANWEI where   JCWENJIANMING=@JCWENJIANMING";
            var JCWENJIANMING = DataExe.GetDbParameter();
            var JCQUYU = DataExe.GetDbParameter();
            var JCNEIRONG = DataExe.GetDbParameter();
            var JCBIANHAO = DataExe.GetDbParameter();
            var JCJIBIE = DataExe.GetDbParameter();
            var JCSHIJIAN = DataExe.GetDbParameter();
            var JCDANWEI = DataExe.GetDbParameter();

            JCWENJIANMING.ParameterName = "@JCWENJIANMING";
            JCQUYU.ParameterName = "@JCQUYU";
            JCNEIRONG.ParameterName = "@JCNEIRONG";
            JCBIANHAO.ParameterName = "@JCBIANHAO";
            JCJIBIE.ParameterName = "@JCJIBIE";
            JCSHIJIAN.ParameterName = "@JCSHIJIAN";
            JCDANWEI.ParameterName = "@JCDANWEI";

            JCWENJIANMING.Value = JingBaomI.JCWENJIANMING;
            JCQUYU.Value = JingBaomI.JCQUYU;
            JCNEIRONG.Value = JingBaomI.JCNEIRONG;
            JCBIANHAO.Value = JingBaomI.JCBIANHAO;
            JCJIBIE.Value = JingBaomI.JCJIBIE;
            JCSHIJIAN.Value = JingBaomI.JCSHIJIAN;
            JCDANWEI.Value = JingBaomI.JCDANWEI;

            DbParameter[] parameters = { JCWENJIANMING, JCQUYU, JCNEIRONG, JCBIANHAO, JCJIBIE, JCSHIJIAN, JCDANWEI };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增解除警报文件属性出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        
        /// <summary>
        /// 解除警报文件保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_JieChuJingBaoFILE_AllData(CG_JIECHUJINGBAO_FILE JingBaomFile)
        {
            try
            {
                string sql = "select JCWENJIANMING from CG_JIECHUJINGBAO_FILE where JCWENJIANMING='" + JingBaomFile.JCWENJIANMING + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 解除警报属性保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_JieChuJingBaoME_AllData(CG_JIECHUJINGBAO_ME JingBaomFile)
        {
            try
            {
                string sql = "select JCWENJIANMING from CG_JIECHUJINGBAO_FILE where JCWENJIANMING='" + JingBaomFile.JCWENJIANMING + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 解除警报内容保存查询去重复
        /// </summary>
        /// <returns></returns>
        public DataTable get_JieChuJingBaoCON_AllData(CG_HT_JIECHUJINGBAO_CONTENT JingBaomFile)
        {
            try
            {
                string sql = "select JCJBWENJIANMING from CG_HT_JIECHUJINGBAO_CONTENT where JCJBWENJIANMING='" + JingBaomFile.JCJBWENJIANMING + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 警报文件新增保存
        /// </summary>
        /// <returns></returns>
        public int AddJCJingBaoFile(CG_JIECHUJINGBAO_FILE JingBaomFile)
        {
            string sql = "INSERT INTO  CG_JIECHUJINGBAO_FILE (JCWENJIANMING,JCNEIRONG,PICFILE) VALUES (@JCWENJIANMING,@JCNEIRONG,@PICFILE)";
            var JCWENJIANMING = DataExe.GetDbParameter();
            var JCNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            JCWENJIANMING.ParameterName = "@JCWENJIANMING";
            JCNEIRONG.ParameterName = "@JCNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            JCWENJIANMING.Value = JingBaomFile.JCWENJIANMING;
            JCNEIRONG.Value = JingBaomFile.JCNEIRONG;
            PICFILE.Value = JingBaomFile.PICFILE;

            DbParameter[] parameters = { JCWENJIANMING, JCNEIRONG, PICFILE };

            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增解除警报文件出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 警报文件修改保存
        /// </summary>
        /// <returns></returns>
        public int UpdateJCJingBaoFile(CG_JIECHUJINGBAO_FILE JingBaomFile)
        {
            string sql = "UPDATE  CG_JIECHUJINGBAO_FILE SET  JCNEIRONG = @JCNEIRONG, PICFILE = @PICFILE WHERE JCWENJIANMING = @JCWENJIANMING";

            var JCWENJIANMING = DataExe.GetDbParameter();
            var JCNEIRONG = DataExe.GetDbParameter();
            var PICFILE = DataExe.GetDbParameter();

            JCWENJIANMING.ParameterName = "@JCWENJIANMING";
            JCNEIRONG.ParameterName = "@JCNEIRONG";
            PICFILE.ParameterName = "@PICFILE";

            JCWENJIANMING.Value = JingBaomFile.JCWENJIANMING;
            JCNEIRONG.Value = JingBaomFile.JCNEIRONG;
            PICFILE.Value = JingBaomFile.PICFILE;

            DbParameter[] parameters = { JCWENJIANMING, JCNEIRONG, PICFILE };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改联系人信息异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// 解除警报数据查询绑定
        /// </summary>
        /// <returns></returns>
        public DataTable getJCJingBao(CG_JIECHUJINGBAO_ME XXME)
        {
            try
            {
                string sql = "select* from (select C.CONTENT,M.JCNEIRONG JBNEIRONG,M.JCBIANHAO JBBIANHAO,C.LINKMAN,C.SENTTO from CG_JIECHUJINGBAO_ME M left join CG_HT_JIECHUJINGBAO_CONTENT C on M.JCWENJIANMING=C.JCJBWENJIANMING where M.JCDANWEI='" + XXME.JCDANWEI + "'and M.JCNEIRONG='" + XXME.JCNEIRONG + "'order by C.DATETIME desc  ) where rownum< 2";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 解除警报判断警报是否解除查询绑定
        /// </summary>
        /// <returns></returns>
        public DataTable getCouldJCJingBao(CG_JIECHUJINGBAO_ME XXME)
        {
            try
            {
                string sql = " select * from CG_JIECHUJINGBAO_ME where JCBIANHAO like '%"+XXME.JCBIANHAO+"%'" + "and JCNEIRONG='"+ XXME.JCNEIRONG + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
