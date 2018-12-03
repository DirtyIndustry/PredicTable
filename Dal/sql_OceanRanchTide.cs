using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 海洋牧场-潮汐
    /// </summary>
    public class sql_OceanRanchTide
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_OceanRanchTide()
        {
            DataExe = new DataExecution();
        }

        /// <summary>
        /// 获取海洋牧场潮汐预报
        /// </summary>
        /// <param name="oceanRanchTide"></param>
        /// <returns></returns>
        public object GetOceanRanchTideList(OceanRanchTide oceanRanchTide)
        {
            try
            {
                string sql = "SELECT * FROM OCEANRANCH24HTIDE_T WHERE PUBLISHDATE = to_date('" + oceanRanchTide.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海洋牧场潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取海洋牧场潮汐预报解析数据
        /// </summary>
        /// <param name="oceanRanchTide"></param>
        /// <returns></returns>
        public object GetOceanRanchTideListBy_S(OceanRanchTide oceanRanchTide)
        {
            try
            {
                string sql = "SELECT * FROM OCEANRANCH24HTIDE_S WHERE FORECASTDATE BETWEEN to_date('" + oceanRanchTide.PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " AND to_date('" + oceanRanchTide.PUBLISHDATE.AddDays(2).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海洋牧场潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 添加海洋牧场潮汐预报
        /// </summary>
        /// <param name="oceanRanchTide"></param>
        /// <returns></returns>
        public int InsertOceanRanchTideList(OceanRanchTide oceanRanchTide)
        {
            try
            {
                string sql = "INSERT INTO OCEANRANCH24HTIDE_T ( "
                           + " PUBLISHDATE,FORECASTDATE,OCEANRANCHNAME,OCEANRANCHSHORTNAME,SN,"
                           + " TIDE24H00,TIDE24H01,TIDE24H02,TIDE24H03,TIDE24H04,TIDE24H05,TIDE24H06,TIDE24H07,TIDE24H08,TIDE24H09,TIDE24H10,TIDE24H11,TIDE24H12,TIDE24H13,TIDE24H14,TIDE24H15,TIDE24H16,TIDE24H17,TIDE24H18,TIDE24H19,TIDE24H20,TIDE24H21,TIDE24H22,TIDE24H23,TIDEFIRSTHTIME,TIDEFIRSTHHEIGHT,TIDESECONDHTIME,TIDESECONDHHEIGHT,TIDEFIRSTLTIME,TIDEFIRSTLHEIGHT,TIDESECONDLTIME,TIDESECONDLHEIGHT)"
                           + " VALUES "
                           + " ( to_date(@PUBLISHDATE,'yyyy-mm-dd hh24@mi@ss'),to_date(@FORECASTDATE,'yyyy-mm-dd hh24@mi@ss'),@OCEANRANCHNAME,@OCEANRANCHSHORTNAME,@SN,@TIDE24H00,@TIDE24H01,@TIDE24H02,@TIDE24H03,@TIDE24H04,@TIDE24H05,@TIDE24H06,@TIDE24H07,@TIDE24H08,@TIDE24H09,@TIDE24H10,@TIDE24H11,@TIDE24H12,@TIDE24H13,@TIDE24H14,@TIDE24H15,@TIDE24H16,@TIDE24H17,@TIDE24H18,@TIDE24H19,@TIDE24H20,@TIDE24H21,@TIDE24H22,@TIDE24H23,@TIDEFIRSTHTIME,@TIDEFIRSTHHEIGHT,@TIDESECONDHTIME,@TIDESECONDHHEIGHT,@TIDEFIRSTLTIME,@TIDEFIRSTLHEIGHT,@TIDESECONDLTIME,@TIDESECONDLHEIGHT)";

                var PUBLISHDATE = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();
                var OCEANRANCHNAME = DataExe.GetDbParameter();
                var OCEANRANCHSHORTNAME = DataExe.GetDbParameter();
                var SN = DataExe.GetDbParameter();

                var TIDE24H00 = DataExe.GetDbParameter();
                var TIDE24H01 = DataExe.GetDbParameter();
                var TIDE24H02 = DataExe.GetDbParameter();
                var TIDE24H03 = DataExe.GetDbParameter();
                var TIDE24H04 = DataExe.GetDbParameter();
                var TIDE24H05 = DataExe.GetDbParameter();
                var TIDE24H06 = DataExe.GetDbParameter();
                var TIDE24H07 = DataExe.GetDbParameter();
                var TIDE24H08 = DataExe.GetDbParameter();
                var TIDE24H09 = DataExe.GetDbParameter();
                var TIDE24H10 = DataExe.GetDbParameter();
                var TIDE24H11 = DataExe.GetDbParameter();
                var TIDE24H12 = DataExe.GetDbParameter();
                var TIDE24H13 = DataExe.GetDbParameter();
                var TIDE24H14 = DataExe.GetDbParameter();
                var TIDE24H15 = DataExe.GetDbParameter();
                var TIDE24H16 = DataExe.GetDbParameter();
                var TIDE24H17 = DataExe.GetDbParameter();
                var TIDE24H18 = DataExe.GetDbParameter();
                var TIDE24H19 = DataExe.GetDbParameter();
                var TIDE24H20 = DataExe.GetDbParameter();
                var TIDE24H21 = DataExe.GetDbParameter();
                var TIDE24H22 = DataExe.GetDbParameter();
                var TIDE24H23 = DataExe.GetDbParameter();

                var TIDEFIRSTHTIME = DataExe.GetDbParameter();
                var TIDEFIRSTHHEIGHT = DataExe.GetDbParameter();
                var TIDESECONDHTIME = DataExe.GetDbParameter();
                var TIDESECONDHHEIGHT = DataExe.GetDbParameter();
                var TIDEFIRSTLTIME = DataExe.GetDbParameter();
                var TIDEFIRSTLHEIGHT = DataExe.GetDbParameter();
                var TIDESECONDLTIME = DataExe.GetDbParameter();
                var TIDESECONDLHEIGHT = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                FORECASTDATE.ParameterName = "@FORECASTDATE";
                OCEANRANCHNAME.ParameterName = "@OCEANRANCHNAME";
                OCEANRANCHSHORTNAME.ParameterName = "@OCEANRANCHSHORTNAME";
                SN.ParameterName = "@SN";

                TIDE24H00.ParameterName = "@TIDE24H00";
                TIDE24H01.ParameterName = "@TIDE24H01";
                TIDE24H02.ParameterName = "@TIDE24H02";
                TIDE24H03.ParameterName = "@TIDE24H03";
                TIDE24H04.ParameterName = "@TIDE24H04";
                TIDE24H05.ParameterName = "@TIDE24H05";
                TIDE24H06.ParameterName = "@TIDE24H06";
                TIDE24H07.ParameterName = "@TIDE24H07";
                TIDE24H08.ParameterName = "@TIDE24H08";
                TIDE24H09.ParameterName = "@TIDE24H09";
                TIDE24H10.ParameterName = "@TIDE24H10";
                TIDE24H11.ParameterName = "@TIDE24H11";
                TIDE24H12.ParameterName = "@TIDE24H12";
                TIDE24H13.ParameterName = "@TIDE24H13";
                TIDE24H14.ParameterName = "@TIDE24H14";
                TIDE24H15.ParameterName = "@TIDE24H15";
                TIDE24H16.ParameterName = "@TIDE24H16";
                TIDE24H17.ParameterName = "@TIDE24H17";
                TIDE24H18.ParameterName = "@TIDE24H18";
                TIDE24H19.ParameterName = "@TIDE24H19";
                TIDE24H20.ParameterName = "@TIDE24H20";
                TIDE24H21.ParameterName = "@TIDE24H21";
                TIDE24H22.ParameterName = "@TIDE24H22";
                TIDE24H23.ParameterName = "@TIDE24H23";

                TIDEFIRSTHTIME.ParameterName = "@TIDEFIRSTHTIME";
                TIDEFIRSTHHEIGHT.ParameterName = "@TIDEFIRSTHHEIGHT";
                TIDESECONDHTIME.ParameterName = "@TIDESECONDHTIME";
                TIDESECONDHHEIGHT.ParameterName = "@TIDESECONDHHEIGHT";
                TIDEFIRSTLTIME.ParameterName = "@TIDEFIRSTLTIME";
                TIDEFIRSTLHEIGHT.ParameterName = "@TIDEFIRSTLHEIGHT";
                TIDESECONDLTIME.ParameterName = "@TIDESECONDLTIME";
                TIDESECONDLHEIGHT.ParameterName = "@TIDESECONDLHEIGHT";
                

                PUBLISHDATE.Value = oceanRanchTide.PUBLISHDATE.ToString();
                FORECASTDATE.Value = oceanRanchTide.FORECASTDATE.ToString();
                OCEANRANCHNAME.Value = oceanRanchTide.OCEANRANCHNAME;
                OCEANRANCHSHORTNAME.Value = oceanRanchTide.OCEANRANCHSHORTNAME;
                SN.Value = oceanRanchTide.SN;

                TIDE24H00.Value = oceanRanchTide.TIDE24H00;
                TIDE24H01.Value = oceanRanchTide.TIDE24H01;
                TIDE24H02.Value = oceanRanchTide.TIDE24H02;
                TIDE24H03.Value = oceanRanchTide.TIDE24H03;
                TIDE24H04.Value = oceanRanchTide.TIDE24H04;
                TIDE24H05.Value = oceanRanchTide.TIDE24H05;
                TIDE24H06.Value = oceanRanchTide.TIDE24H06;
                TIDE24H07.Value = oceanRanchTide.TIDE24H07;
                TIDE24H08.Value = oceanRanchTide.TIDE24H08;
                TIDE24H09.Value = oceanRanchTide.TIDE24H09;
                TIDE24H10.Value = oceanRanchTide.TIDE24H10;
                TIDE24H11.Value = oceanRanchTide.TIDE24H11;
                TIDE24H12.Value = oceanRanchTide.TIDE24H12;
                TIDE24H13.Value = oceanRanchTide.TIDE24H13;
                TIDE24H14.Value = oceanRanchTide.TIDE24H14;
                TIDE24H15.Value = oceanRanchTide.TIDE24H15;
                TIDE24H16.Value = oceanRanchTide.TIDE24H16;
                TIDE24H17.Value = oceanRanchTide.TIDE24H17;
                TIDE24H18.Value = oceanRanchTide.TIDE24H18;
                TIDE24H19.Value = oceanRanchTide.TIDE24H19;
                TIDE24H20.Value = oceanRanchTide.TIDE24H20;
                TIDE24H21.Value = oceanRanchTide.TIDE24H21;
                TIDE24H22.Value = oceanRanchTide.TIDE24H22;
                TIDE24H23.Value = oceanRanchTide.TIDE24H23;

                TIDEFIRSTHTIME.Value = oceanRanchTide.TIDEFIRSTHTIME;
                TIDEFIRSTHHEIGHT.Value = oceanRanchTide.TIDEFIRSTHHEIGHT;
                TIDESECONDHTIME.Value = oceanRanchTide.TIDESECONDHTIME;
                TIDESECONDHHEIGHT.Value = oceanRanchTide.TIDESECONDHHEIGHT;
                TIDEFIRSTLTIME.Value = oceanRanchTide.TIDEFIRSTLTIME;
                TIDEFIRSTLHEIGHT.Value = oceanRanchTide.TIDEFIRSTLHEIGHT;
                TIDESECONDLTIME.Value = oceanRanchTide.TIDESECONDLTIME;
                TIDESECONDLHEIGHT.Value = oceanRanchTide.TIDESECONDLHEIGHT;


                DbParameter[] parameters = { PUBLISHDATE, FORECASTDATE, OCEANRANCHNAME, OCEANRANCHSHORTNAME, SN,TIDE24H00,TIDE24H01,TIDE24H02,TIDE24H03,TIDE24H04,TIDE24H05,TIDE24H06,TIDE24H07,TIDE24H08,TIDE24H09,TIDE24H10,TIDE24H11,TIDE24H12,TIDE24H13,TIDE24H14,TIDE24H15,TIDE24H16,TIDE24H17,TIDE24H18,TIDE24H19,TIDE24H20,TIDE24H21,TIDE24H22,TIDE24H23,TIDEFIRSTHTIME,TIDEFIRSTHHEIGHT,TIDESECONDHTIME,TIDESECONDHHEIGHT,TIDEFIRSTLTIME,TIDEFIRSTLHEIGHT,TIDESECONDLTIME,TIDESECONDLHEIGHT};
                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("添加海洋牧场潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改海洋牧场潮汐预报
        /// </summary>
        /// <param name="oceanRanchTide"></param>
        /// <returns></returns>
        public int EditOceanRanchTideList(OceanRanchTide oceanRanchTide)
        {
            try
            {
                string sql = "UPDATE OCEANRANCH24HTIDE_T "
                           + " SET"
                           + " TIDE24H00=@TIDE24H00,TIDE24H01=@TIDE24H01,TIDE24H02=@TIDE24H02,TIDE24H03=@TIDE24H03,TIDE24H04=@TIDE24H04,TIDE24H05=@TIDE24H05,TIDE24H06=@TIDE24H06,TIDE24H07=@TIDE24H07,TIDE24H08=@TIDE24H08,TIDE24H09=@TIDE24H09,TIDE24H10=@TIDE24H10,TIDE24H11=@TIDE24H11,TIDE24H12=@TIDE24H12,TIDE24H13=@TIDE24H13,TIDE24H14=@TIDE24H14,TIDE24H15=@TIDE24H15,TIDE24H16=@TIDE24H16,TIDE24H17=@TIDE24H17,TIDE24H18=@TIDE24H18,TIDE24H19=@TIDE24H19,TIDE24H20=@TIDE24H20,TIDE24H21=@TIDE24H21,TIDE24H22=@TIDE24H22,TIDE24H23=@TIDE24H23,TIDEFIRSTHTIME=@TIDEFIRSTHTIME,TIDEFIRSTHHEIGHT=@TIDEFIRSTHHEIGHT,TIDESECONDHTIME=@TIDESECONDHTIME,TIDESECONDHHEIGHT=@TIDESECONDHHEIGHT,TIDEFIRSTLTIME=@TIDEFIRSTLTIME,TIDEFIRSTLHEIGHT=@TIDEFIRSTLHEIGHT,TIDESECONDLTIME=@TIDESECONDLTIME,TIDESECONDLHEIGHT=@TIDESECONDLHEIGHT"
                           + " WHERE PUBLISHDATE=to_date(@PUBLISHDATE, 'yyyy-mm-dd hh24@mi@ss') AND OCEANRANCHNAME=@OCEANRANCHNAME AND FORECASTDATE = to_date(@FORECASTDATE, 'yyyy-mm-dd hh24@mi@ss')";


                var PUBLISHDATE = DataExe.GetDbParameter();
                var OCEANRANCHNAME = DataExe.GetDbParameter();

                var TIDE24H00 = DataExe.GetDbParameter();
                var TIDE24H01 = DataExe.GetDbParameter();
                var TIDE24H02 = DataExe.GetDbParameter();
                var TIDE24H03 = DataExe.GetDbParameter();
                var TIDE24H04 = DataExe.GetDbParameter();
                var TIDE24H05 = DataExe.GetDbParameter();
                var TIDE24H06 = DataExe.GetDbParameter();
                var TIDE24H07 = DataExe.GetDbParameter();
                var TIDE24H08 = DataExe.GetDbParameter();
                var TIDE24H09 = DataExe.GetDbParameter();
                var TIDE24H10 = DataExe.GetDbParameter();
                var TIDE24H11 = DataExe.GetDbParameter();
                var TIDE24H12 = DataExe.GetDbParameter();
                var TIDE24H13 = DataExe.GetDbParameter();
                var TIDE24H14 = DataExe.GetDbParameter();
                var TIDE24H15 = DataExe.GetDbParameter();
                var TIDE24H16 = DataExe.GetDbParameter();
                var TIDE24H17 = DataExe.GetDbParameter();
                var TIDE24H18 = DataExe.GetDbParameter();
                var TIDE24H19 = DataExe.GetDbParameter();
                var TIDE24H20 = DataExe.GetDbParameter();
                var TIDE24H21 = DataExe.GetDbParameter();
                var TIDE24H22 = DataExe.GetDbParameter();
                var TIDE24H23 = DataExe.GetDbParameter();

                var TIDEFIRSTHTIME = DataExe.GetDbParameter();
                var TIDEFIRSTHHEIGHT = DataExe.GetDbParameter();
                var TIDESECONDHTIME = DataExe.GetDbParameter();
                var TIDESECONDHHEIGHT = DataExe.GetDbParameter();
                var TIDEFIRSTLTIME = DataExe.GetDbParameter();
                var TIDEFIRSTLHEIGHT = DataExe.GetDbParameter();
                var TIDESECONDLTIME = DataExe.GetDbParameter();
                var TIDESECONDLHEIGHT = DataExe.GetDbParameter();
                var FORECASTDATE = DataExe.GetDbParameter();

                PUBLISHDATE.ParameterName = "@PUBLISHDATE";
                OCEANRANCHNAME.ParameterName = "@OCEANRANCHNAME";

                TIDE24H00.ParameterName = "@TIDE24H00";
                TIDE24H01.ParameterName = "@TIDE24H01";
                TIDE24H02.ParameterName = "@TIDE24H02";
                TIDE24H03.ParameterName = "@TIDE24H03";
                TIDE24H04.ParameterName = "@TIDE24H04";
                TIDE24H05.ParameterName = "@TIDE24H05";
                TIDE24H06.ParameterName = "@TIDE24H06";
                TIDE24H07.ParameterName = "@TIDE24H07";
                TIDE24H08.ParameterName = "@TIDE24H08";
                TIDE24H09.ParameterName = "@TIDE24H09";
                TIDE24H10.ParameterName = "@TIDE24H10";
                TIDE24H11.ParameterName = "@TIDE24H11";
                TIDE24H12.ParameterName = "@TIDE24H12";
                TIDE24H13.ParameterName = "@TIDE24H13";
                TIDE24H14.ParameterName = "@TIDE24H14";
                TIDE24H15.ParameterName = "@TIDE24H15";
                TIDE24H16.ParameterName = "@TIDE24H16";
                TIDE24H17.ParameterName = "@TIDE24H17";
                TIDE24H18.ParameterName = "@TIDE24H18";
                TIDE24H19.ParameterName = "@TIDE24H19";
                TIDE24H20.ParameterName = "@TIDE24H20";
                TIDE24H21.ParameterName = "@TIDE24H21";
                TIDE24H22.ParameterName = "@TIDE24H22";
                TIDE24H23.ParameterName = "@TIDE24H23";

                TIDEFIRSTHTIME.ParameterName = "@TIDEFIRSTHTIME";
                TIDEFIRSTHHEIGHT.ParameterName = "@TIDEFIRSTHHEIGHT";
                TIDESECONDHTIME.ParameterName = "@TIDESECONDHTIME";
                TIDESECONDHHEIGHT.ParameterName = "@TIDESECONDHHEIGHT";
                TIDEFIRSTLTIME.ParameterName = "@TIDEFIRSTLTIME";
                TIDEFIRSTLHEIGHT.ParameterName = "@TIDEFIRSTLHEIGHT";
                TIDESECONDLTIME.ParameterName = "@TIDESECONDLTIME";
                TIDESECONDLHEIGHT.ParameterName = "@TIDESECONDLHEIGHT";
                FORECASTDATE.ParameterName = "@FORECASTDATE";

                PUBLISHDATE.Value = oceanRanchTide.PUBLISHDATE.ToString();
                OCEANRANCHNAME.Value = oceanRanchTide.OCEANRANCHNAME;

                TIDE24H00.Value = oceanRanchTide.TIDE24H00;
                TIDE24H01.Value = oceanRanchTide.TIDE24H01;
                TIDE24H02.Value = oceanRanchTide.TIDE24H02;
                TIDE24H03.Value = oceanRanchTide.TIDE24H03;
                TIDE24H04.Value = oceanRanchTide.TIDE24H04;
                TIDE24H05.Value = oceanRanchTide.TIDE24H05;
                TIDE24H06.Value = oceanRanchTide.TIDE24H06;
                TIDE24H07.Value = oceanRanchTide.TIDE24H07;
                TIDE24H08.Value = oceanRanchTide.TIDE24H08;
                TIDE24H09.Value = oceanRanchTide.TIDE24H09;
                TIDE24H10.Value = oceanRanchTide.TIDE24H10;
                TIDE24H11.Value = oceanRanchTide.TIDE24H11;
                TIDE24H12.Value = oceanRanchTide.TIDE24H12;
                TIDE24H13.Value = oceanRanchTide.TIDE24H13;
                TIDE24H14.Value = oceanRanchTide.TIDE24H14;
                TIDE24H15.Value = oceanRanchTide.TIDE24H15;
                TIDE24H16.Value = oceanRanchTide.TIDE24H16;
                TIDE24H17.Value = oceanRanchTide.TIDE24H17;
                TIDE24H18.Value = oceanRanchTide.TIDE24H18;
                TIDE24H19.Value = oceanRanchTide.TIDE24H19;
                TIDE24H20.Value = oceanRanchTide.TIDE24H20;
                TIDE24H21.Value = oceanRanchTide.TIDE24H21;
                TIDE24H22.Value = oceanRanchTide.TIDE24H22;
                TIDE24H23.Value = oceanRanchTide.TIDE24H23;

                TIDEFIRSTHTIME.Value = oceanRanchTide.TIDEFIRSTHTIME;
                TIDEFIRSTHHEIGHT.Value = oceanRanchTide.TIDEFIRSTHHEIGHT;
                TIDESECONDHTIME.Value = oceanRanchTide.TIDESECONDHTIME;
                TIDESECONDHHEIGHT.Value = oceanRanchTide.TIDESECONDHHEIGHT;
                TIDEFIRSTLTIME.Value = oceanRanchTide.TIDEFIRSTLTIME;
                TIDEFIRSTLHEIGHT.Value = oceanRanchTide.TIDEFIRSTLHEIGHT;
                TIDESECONDLTIME.Value = oceanRanchTide.TIDESECONDLTIME;
                TIDESECONDLHEIGHT.Value = oceanRanchTide.TIDESECONDLHEIGHT;
                FORECASTDATE.Value = oceanRanchTide.FORECASTDATE.ToString();


                DbParameter[] parameters = { PUBLISHDATE, OCEANRANCHNAME, TIDE24H00, TIDE24H01, TIDE24H02, TIDE24H03, TIDE24H04, TIDE24H05, TIDE24H06, TIDE24H07, TIDE24H08, TIDE24H09, TIDE24H10, TIDE24H11, TIDE24H12, TIDE24H13, TIDE24H14, TIDE24H15, TIDE24H16, TIDE24H17, TIDE24H18, TIDE24H19, TIDE24H20, TIDE24H21, TIDE24H22, TIDE24H23, TIDEFIRSTHTIME, TIDEFIRSTHHEIGHT, TIDESECONDHTIME, TIDESECONDHHEIGHT, TIDEFIRSTLTIME, TIDEFIRSTLHEIGHT, TIDESECONDLTIME, TIDESECONDLHEIGHT , FORECASTDATE };

                return DataExe.GetIntExeData(sql, parameters);

            }
            catch (Exception ex)
            {
                WriteLog.Write("修改海洋牧场潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }


        #region 画潮汐数据折线图

        /// <summary>
        /// 获取潮汐数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object Get24TideDataList(DateTime date,string OCEANRANCHNAME)
        {
            try
            {
                string sql = "SELECT "
                            + " TIDE24H00,TIDE24H01,TIDE24H02,TIDE24H03,TIDE24H04,TIDE24H05,TIDE24H06,TIDE24H07,TIDE24H08,TIDE24H09,TIDE24H10,TIDE24H11,TIDE24H12,TIDE24H13,TIDE24H14,TIDE24H15,TIDE24H16,TIDE24H17,TIDE24H18,TIDE24H19,TIDE24H20,TIDE24H21,TIDE24H22,TIDE24H23"
                            + " FROM OCEANRANCH24HTIDE_T WHERE  OCEANRANCHNAME = '"+ OCEANRANCHNAME + "'"
                             + "  AND  FORECASTDATE BETWEEN to_date('" + date.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND to_date('" + date.AddDays(3).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND PUBLISHDATE = to_date('" + date.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by FORECASTDATE";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海洋牧场潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 获取天文潮数据中第二天00时数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public object Get48TideDataListBy_S(DateTime date, string OCEANRANCHNAME)
        {
            try
            {
                string sql = "SELECT TIDE24H00"
                            + " FROM OCEANRANCH24HTIDE_S WHERE FORECASTDATE = to_date('" + date.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND OCEANRANCHNAME = '" + OCEANRANCHNAME + "'";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取海洋牧场潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
        #endregion
    }
}