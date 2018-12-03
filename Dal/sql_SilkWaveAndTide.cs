using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 上午预报单七、八
    /// 丝绸之路风浪气象和潮汐预报
    /// sl
    /// </summary>
    public class sql_SilkWaveAndTide
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_SilkWaveAndTide()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        #region 丝绸之路风浪数据

        /// <summary>
        /// 获取丝绸之路风浪数据
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <returns></returns>
        public object GetSilkWave(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
        {
            try
            {
                string sql = "SELECT * FROM HT_SILKWINDWAVE "
                        + " WHERE PUBLISHDATE=to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND "
                        + " FORECASTDATE BETWEEN to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " AND  to_date('" + Tblyrbhwindwave72hforecasttwo.FORECASTDATE.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取丝绸之路风浪前一天数据
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <returns></returns>
        public object GetSilkWaveLast(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
        {
            try
            {
                string sql = "SELECT * FROM HT_SILKWINDWAVE "
                        + " WHERE PUBLISHDATE=to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') ";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// 查询丝绸之路风浪数据
        /// 用于Word文件生成时调用
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <returns></returns>
        public object GetSilkWaveWord(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
        {
            try
            {
                string sql = "SELECT * FROM HT_SILKWINDWAVE "
                        + " WHERE PUBLISHDATE=to_date('" + Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by reportarea,forecastdate asc";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// 添加丝绸之路风浪数据
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <returns></returns>
        public int AddSilkWave(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
        {

            string sql = "INSERT INTO  HT_SILKWINDWAVE (PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@REPORTAREA,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@YRBHWWFWAVEHEIGHT,@YRBHWWFWAVEDIR,@YRBHWWFFLOWDIR,@YRBHWWFFLOWLEVEL)";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var REPORTAREA = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var YRBHWWFWAVEHEIGHT = DataExe.GetDbParameter();
            var YRBHWWFWAVEDIR = DataExe.GetDbParameter();
            var YRBHWWFFLOWDIR = DataExe.GetDbParameter();
            var YRBHWWFFLOWLEVEL = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            REPORTAREA.ParameterName = "@REPORTAREA";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            YRBHWWFWAVEHEIGHT.ParameterName = "@YRBHWWFWAVEHEIGHT";
            YRBHWWFWAVEDIR.ParameterName = "@YRBHWWFWAVEDIR";
            YRBHWWFFLOWDIR.ParameterName = "@YRBHWWFFLOWDIR";
            YRBHWWFFLOWLEVEL.ParameterName = "@YRBHWWFFLOWLEVEL";

            PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
            REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
            FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
            YRBHWWFWAVEHEIGHT.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEHEIGHT;
            YRBHWWFWAVEDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEDIR.ToString();
            YRBHWWFFLOWDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWDIR.ToString();
            YRBHWWFFLOWLEVEL.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWLEVEL;

            DbParameter[] parameters = { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL };
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
        /// 修改丝绸之路风浪数据
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <returns></returns>
        public int EditSilkWave(TBLYRBHWINDWAVE72HFORECASTTWO Tblyrbhwindwave72hforecasttwo)
        {
            string sql = "UPDATE   HT_SILKWINDWAVE set  "
                + "YRBHWWFWAVEHEIGHT=@YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR=@YRBHWWFWAVEDIR, YRBHWWFFLOWDIR=@YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL=@YRBHWWFFLOWLEVEL "
                + "where  PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and REPORTAREA=@REPORTAREA and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";

            var PUBLISHDATE = DataExe.GetDbParameter();
            var REPORTAREA = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var YRBHWWFWAVEHEIGHT = DataExe.GetDbParameter();
            var YRBHWWFWAVEDIR = DataExe.GetDbParameter();
            var YRBHWWFFLOWDIR = DataExe.GetDbParameter();
            var YRBHWWFFLOWLEVEL = DataExe.GetDbParameter();

            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            REPORTAREA.ParameterName = "@REPORTAREA";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            YRBHWWFWAVEHEIGHT.ParameterName = "@YRBHWWFWAVEHEIGHT";
            YRBHWWFWAVEDIR.ParameterName = "@YRBHWWFWAVEDIR";
            YRBHWWFFLOWDIR.ParameterName = "@YRBHWWFFLOWDIR";
            YRBHWWFFLOWLEVEL.ParameterName = "@YRBHWWFFLOWLEVEL";

            PUBLISHDATE.Value = Tblyrbhwindwave72hforecasttwo.PUBLISHDATE.ToString();
            REPORTAREA.Value = Tblyrbhwindwave72hforecasttwo.REPORTAREA;
            FORECASTDATE.Value = Tblyrbhwindwave72hforecasttwo.FORECASTDATE.ToString();
            YRBHWWFWAVEHEIGHT.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEHEIGHT;
            YRBHWWFWAVEDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFWAVEDIR.ToString();
            YRBHWWFFLOWDIR.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWDIR.ToString();
            YRBHWWFFLOWLEVEL.Value = Tblyrbhwindwave72hforecasttwo.YRBHWWFFLOWLEVEL;

            DbParameter[] parameters = { PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion


        #region 丝绸之路潮汐数据
        /// <summary>
        /// 获取丝绸之路潮汐数据
        /// </summary>
        /// <param name="TBLHARBOURTIDELEVEL"></param>
        /// <returns></returns>
        public object GetSilkTide(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {

            try
            {
                string sql = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by HTLHARBOUR,forecastdate asc";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取丝绸之路潮汐数据 modify by xp 2018-9-12
        /// </summary>
        /// <param name="TBLHARBOURTIDELEVEL"></param>
        /// <returns></returns>
        public object GetSilkTide_Week(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {

            try
            {
                string sql = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') "+
                    "and  forecastdate>to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by HTLHARBOUR,forecastdate asc";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
       
        /// <summary>
        /// 获取丝绸之路潮汐前一天数据
        /// </summary>
        /// <param name="Tblyrbhwindwave72hforecasttwo"></param>
        /// <returns></returns>
        public object GetSilkTideLast(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {
            try
            {
                string sql = "SELECT * FROM HT_SILKTIDE "
                        + " WHERE PUBLISHDATE=to_date('" + TBLHARBOURTIDELEVEL.PUBLISHDATE.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') ";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加丝绸之路潮汐数据
        /// </summary>
        /// <param name="TBLHARBOURTIDELEVEL"></param>
        /// <returns></returns>
        public int AddSilkTide(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {

            string sql = "INSERT INTO  HT_SILKTIDE (PUBLISHDATE,HTLSECONDTIMELOWTIDE,HTLLOWTIDELEVELFORTHESECONDTIM,HTLHARBOUR,FORECASTDATE,HTLFIRSTWAVEOFTIME,HTLFIRSTWAVETIDELEVEL,HTLFIRSTTIMELOWTIDE,HTLLOWTIDELEVELFORTHEFIRSTTIME,HTLSECONDWAVEOFTIME,HTLSECONDWAVETIDELEVEL) VALUES (to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),@HTLSECONDTIMELOWTIDE,@HTLLOWTIDELEVELFORTHESECONDTIM,@HTLHARBOUR,to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@HTLFIRSTWAVEOFTIME,@HTLFIRSTWAVETIDELEVEL,@HTLFIRSTTIMELOWTIDE,@HTLLOWTIDELEVELFORTHEFIRSTTIME,@HTLSECONDWAVEOFTIME,@HTLSECONDWAVETIDELEVEL)";



            var PUBLISHDATE = DataExe.GetDbParameter();
            var HTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();
            var HTLLOWTIDELEVELFORTHESECONDTIM = DataExe.GetDbParameter();
            var HTLHARBOUR = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var HTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
            var HTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
            var HTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
            var HTLLOWTIDELEVELFORTHEFIRSTTIME = DataExe.GetDbParameter();
            var HTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
            var HTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            HTLSECONDTIMELOWTIDE.ParameterName = "@HTLSECONDTIMELOWTIDE";
            HTLLOWTIDELEVELFORTHESECONDTIM.ParameterName = "@HTLLOWTIDELEVELFORTHESECONDTIM";
            HTLHARBOUR.ParameterName = "@HTLHARBOUR";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            HTLFIRSTWAVEOFTIME.ParameterName = "@HTLFIRSTWAVEOFTIME";
            HTLFIRSTWAVETIDELEVEL.ParameterName = "@HTLFIRSTWAVETIDELEVEL";
            HTLFIRSTTIMELOWTIDE.ParameterName = "@HTLFIRSTTIMELOWTIDE";
            HTLLOWTIDELEVELFORTHEFIRSTTIME.ParameterName = "@HTLLOWTIDELEVELFORTHEFIRSTTIME";
            HTLSECONDWAVEOFTIME.ParameterName = "@HTLSECONDWAVEOFTIME";
            HTLSECONDWAVETIDELEVEL.ParameterName = "@HTLSECONDWAVETIDELEVEL";




            PUBLISHDATE.Value = TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString();
            HTLSECONDTIMELOWTIDE.Value = TBLHARBOURTIDELEVEL.HTLSECONDTIMELOWTIDE;
            HTLLOWTIDELEVELFORTHESECONDTIM.Value = TBLHARBOURTIDELEVEL.HTLLOWTIDELEVELFORTHESECONDTIM;
            HTLHARBOUR.Value = TBLHARBOURTIDELEVEL.HTLHARBOUR;
            FORECASTDATE.Value = TBLHARBOURTIDELEVEL.FORECASTDATE.ToString();
            HTLFIRSTWAVEOFTIME.Value = TBLHARBOURTIDELEVEL.HTLFIRSTWAVEOFTIME;
            HTLFIRSTWAVETIDELEVEL.Value = TBLHARBOURTIDELEVEL.HTLFIRSTWAVETIDELEVEL;
            HTLFIRSTTIMELOWTIDE.Value = TBLHARBOURTIDELEVEL.HTLFIRSTTIMELOWTIDE;
            HTLLOWTIDELEVELFORTHEFIRSTTIME.Value = TBLHARBOURTIDELEVEL.HTLLOWTIDELEVELFORTHEFIRSTTIME;
            HTLSECONDWAVEOFTIME.Value = TBLHARBOURTIDELEVEL.HTLSECONDWAVEOFTIME;
            HTLSECONDWAVETIDELEVEL.Value = TBLHARBOURTIDELEVEL.HTLSECONDWAVETIDELEVEL;


            DbParameter[] parameters = { PUBLISHDATE, HTLSECONDTIMELOWTIDE, HTLLOWTIDELEVELFORTHESECONDTIM, HTLHARBOUR, FORECASTDATE, HTLFIRSTWAVEOFTIME, HTLFIRSTWAVETIDELEVEL, HTLFIRSTTIMELOWTIDE, HTLLOWTIDELEVELFORTHEFIRSTTIME, HTLSECONDWAVEOFTIME, HTLSECONDWAVETIDELEVEL };
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
        /// 修改丝绸之路潮汐数据
        /// </summary>
        public int EditSilkTide(TBLHARBOURTIDELEVEL TBLHARBOURTIDELEVEL)
        {
            string sql = "UPDATE   HT_SILKTIDE set	HTLSECONDTIMELOWTIDE=@HTLSECONDTIMELOWTIDE,HTLLOWTIDELEVELFORTHESECONDTIM=@HTLLOWTIDELEVELFORTHESECONDTIM,HTLFIRSTWAVEOFTIME=@HTLFIRSTWAVEOFTIME,HTLFIRSTWAVETIDELEVEL=@HTLFIRSTWAVETIDELEVEL,HTLFIRSTTIMELOWTIDE=@HTLFIRSTTIMELOWTIDE,HTLLOWTIDELEVELFORTHEFIRSTTIME=@HTLLOWTIDELEVELFORTHEFIRSTTIME,HTLSECONDWAVEOFTIME=@HTLSECONDWAVEOFTIME,HTLSECONDWAVETIDELEVEL=@HTLSECONDWAVETIDELEVEL where PUBLISHDATE=to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss') and HTLHARBOUR=@HTLHARBOUR and FORECASTDATE=to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHDATE = DataExe.GetDbParameter();
            var HTLSECONDTIMELOWTIDE = DataExe.GetDbParameter();
            var HTLLOWTIDELEVELFORTHESECONDTIM = DataExe.GetDbParameter();
            var HTLHARBOUR = DataExe.GetDbParameter();
            var FORECASTDATE = DataExe.GetDbParameter();
            var HTLFIRSTWAVEOFTIME = DataExe.GetDbParameter();
            var HTLFIRSTWAVETIDELEVEL = DataExe.GetDbParameter();
            var HTLFIRSTTIMELOWTIDE = DataExe.GetDbParameter();
            var HTLLOWTIDELEVELFORTHEFIRSTTIME = DataExe.GetDbParameter();
            var HTLSECONDWAVEOFTIME = DataExe.GetDbParameter();
            var HTLSECONDWAVETIDELEVEL = DataExe.GetDbParameter();




            PUBLISHDATE.ParameterName = "@PUBLISHDATE";
            HTLSECONDTIMELOWTIDE.ParameterName = "@HTLSECONDTIMELOWTIDE";
            HTLLOWTIDELEVELFORTHESECONDTIM.ParameterName = "@HTLLOWTIDELEVELFORTHESECONDTIM";
            HTLHARBOUR.ParameterName = "@HTLHARBOUR";
            FORECASTDATE.ParameterName = "@FORECASTDATE";
            HTLFIRSTWAVEOFTIME.ParameterName = "@HTLFIRSTWAVEOFTIME";
            HTLFIRSTWAVETIDELEVEL.ParameterName = "@HTLFIRSTWAVETIDELEVEL";
            HTLFIRSTTIMELOWTIDE.ParameterName = "@HTLFIRSTTIMELOWTIDE";
            HTLLOWTIDELEVELFORTHEFIRSTTIME.ParameterName = "@HTLLOWTIDELEVELFORTHEFIRSTTIME";
            HTLSECONDWAVEOFTIME.ParameterName = "@HTLSECONDWAVEOFTIME";
            HTLSECONDWAVETIDELEVEL.ParameterName = "@HTLSECONDWAVETIDELEVEL";




            PUBLISHDATE.Value = TBLHARBOURTIDELEVEL.PUBLISHDATE.ToString();
            HTLSECONDTIMELOWTIDE.Value = TBLHARBOURTIDELEVEL.HTLSECONDTIMELOWTIDE;
            HTLLOWTIDELEVELFORTHESECONDTIM.Value = TBLHARBOURTIDELEVEL.HTLLOWTIDELEVELFORTHESECONDTIM;
            HTLHARBOUR.Value = TBLHARBOURTIDELEVEL.HTLHARBOUR;
            FORECASTDATE.Value = TBLHARBOURTIDELEVEL.FORECASTDATE.ToString();
            HTLFIRSTWAVEOFTIME.Value = TBLHARBOURTIDELEVEL.HTLFIRSTWAVEOFTIME;
            HTLFIRSTWAVETIDELEVEL.Value = TBLHARBOURTIDELEVEL.HTLFIRSTWAVETIDELEVEL;
            HTLFIRSTTIMELOWTIDE.Value = TBLHARBOURTIDELEVEL.HTLFIRSTTIMELOWTIDE;
            HTLLOWTIDELEVELFORTHEFIRSTTIME.Value = TBLHARBOURTIDELEVEL.HTLLOWTIDELEVELFORTHEFIRSTTIME;
            HTLSECONDWAVEOFTIME.Value = TBLHARBOURTIDELEVEL.HTLSECONDWAVEOFTIME;
            HTLSECONDWAVETIDELEVEL.Value = TBLHARBOURTIDELEVEL.HTLSECONDWAVETIDELEVEL;


            DbParameter[] parameters = { PUBLISHDATE, HTLSECONDTIMELOWTIDE, HTLLOWTIDELEVELFORTHESECONDTIM, HTLHARBOUR, FORECASTDATE, HTLFIRSTWAVEOFTIME, HTLFIRSTWAVETIDELEVEL, HTLFIRSTTIMELOWTIDE, HTLLOWTIDELEVELFORTHEFIRSTTIME, HTLSECONDWAVEOFTIME, HTLSECONDWAVETIDELEVEL };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                return 0;
            }


        }
        #endregion 
    }
}