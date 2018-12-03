using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// Sql_HT_YB_Tide 的摘要说明
    /// </summary>

    public class Sql_HT_YB_Tide
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_HT_YB_Tide()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        /// <summary>
        ///获取所有潮汐预报数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTideData()
        {
            try
            {
                return DataExe.GetTableExeData("select * from HT_YB_TIDE");
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        ///获取潮汐预报数据
        /// </summary>
        /// <param name="station"></param>
        /// <param name="predictionDate"></param>
        /// <returns></returns>
        public DataTable GetTideDataForStationAndPredictionDate(string station,string predictionDate)
        {
            try
            {
                var sql = "select * from HT_YB_TIDE where STATION ='"+ station + "' and PREDICTIONDATE = to_date('"+predictionDate+ "','yyyy/mm/dd')";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
                //WriteLog.Write("获取潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                //return null;
            }
        }

        /// <summary>
        ///获取潮汐预报数据
        /// </summary>
        /// <param name="station"></param>
        /// <param name="preStartDate"></param>
        /// <param name="preEndDate"></param>
        /// <returns></returns>
        public DataTable GetTideDataForStationAndPredRange(string station, string preStartDate,string preEndDate)
        {
            try
            {
                //var sql = "select * from HT_YB_TIDE where STATION ='" + station + "' and PREDICTIONDATE = to_date('" + preStartDate + "','yyyy/mm/dd')";
                var sql = "select * from HT_YB_TIDE where STATION in (" 
                    + station + ") and PREDICTIONDATE between to_date('" 
                    + preStartDate + "','yyyy/mm/dd') and to_date('"
                    + preEndDate + "','yyyy/mm/dd') ORDER BY STATION,PREDICTIONDATE";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
                //WriteLog.Write("获取潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                //return null;
            }
        }

        /// <summary>
        ///导入潮汐预报数据到数据库
        /// </summary>
        /// <param name="tide"></param>
        /// <returns></returns>
        public int AddTidesData(HT_YB_Tide tide)
        {
            // string sql = "INSERT INTO  HT_KJ_GONGZHONGPINGTAI (ID,TIME,USERID,DOCTYPE,DOCUMENTCONTENT,MESTYPE,STATE) VALUES (HT_KJ_GONGZHONGPINGTAI.Nextval,(to_date(@TIME,'yyyy-mm-dd hh24@mi@ss'),@USERID,@DOCTYPE,@DOCUMENTCONTENT,@MESTYPE,@STATE)";
            string sql = "INSERT INTO  HT_YB_TIDE VALUES (@STATION,to_date(@PREDICTIONDATE,'yyyymmdd'),@H0,@H1,@H2,@H3,@H4,@H5,@H6,@H7,@H8,@H9,"
                + "@H10,@H11,@H12,@H13,@H14,@H15,@H16,@H17,@H18,@H19,@H20,@H21,@H22,@H23,@FSTHIGHWIDETIME,@FSTHIGHWIDEHEIGHT,"
                + "@FSTLOWWIDETIME,@FSTLOWWIDEHEIGHT,@SCDHIGHWIDETIME,@SCDHIGHWIDEHEIGHT,@SCDLOWWIDETIME,@SCDLOWWIDEHEIGHT)";

            var STATION = DataExe.GetDbParameter();
            var PREDICTIONDATE = DataExe.GetDbParameter();
            var H0 = DataExe.GetDbParameter();
            var H1 = DataExe.GetDbParameter();
            var H2 = DataExe.GetDbParameter();
            var H3 = DataExe.GetDbParameter();
            var H4 = DataExe.GetDbParameter();
            var H5 = DataExe.GetDbParameter();
            var H6 = DataExe.GetDbParameter();
            var H7 = DataExe.GetDbParameter();
            var H8 = DataExe.GetDbParameter();
            var H9 = DataExe.GetDbParameter();
            var H10 = DataExe.GetDbParameter();
            var H11 = DataExe.GetDbParameter();
            var H12 = DataExe.GetDbParameter();
            var H13 = DataExe.GetDbParameter();
            var H14 = DataExe.GetDbParameter();
            var H15 = DataExe.GetDbParameter();
            var H16 = DataExe.GetDbParameter();
            var H17 = DataExe.GetDbParameter();
            var H18 = DataExe.GetDbParameter();
            var H19 = DataExe.GetDbParameter();
            var H20 = DataExe.GetDbParameter();
            var H21 = DataExe.GetDbParameter();
            var H22 = DataExe.GetDbParameter();
            var H23 = DataExe.GetDbParameter();
            var FSTHIGHWIDETIME = DataExe.GetDbParameter();
            var FSTHIGHWIDEHEIGHT = DataExe.GetDbParameter();
            var FSTLOWWIDETIME = DataExe.GetDbParameter();
            var FSTLOWWIDEHEIGHT = DataExe.GetDbParameter();
            var SCDHIGHWIDETIME = DataExe.GetDbParameter();
            var SCDHIGHWIDEHEIGHT = DataExe.GetDbParameter();
            var SCDLOWWIDETIME = DataExe.GetDbParameter();
            var SCDLOWWIDEHEIGHT = DataExe.GetDbParameter();

            STATION.ParameterName = "@STATION";
            PREDICTIONDATE.ParameterName = "@PREDICTIONDATE";
            H0.ParameterName = "@H0";
            H1.ParameterName = "@H1";
            H2.ParameterName = "@H2";
            H3.ParameterName = "@H3";
            H4.ParameterName = "@H4";
            H5.ParameterName = "@H5";
            H6.ParameterName = "@H6";
            H7.ParameterName = "@H7";
            H8.ParameterName = "@H8";
            H9.ParameterName = "@H9";
            H10.ParameterName = "@H10";
            H11.ParameterName = "@H11";
            H12.ParameterName = "@H12";
            H13.ParameterName = "@H13";
            H14.ParameterName = "@H14";
            H15.ParameterName = "@H15";
            H16.ParameterName = "@H16";
            H17.ParameterName = "@H17";
            H18.ParameterName = "@H18";
            H19.ParameterName = "@H19";
            H20.ParameterName = "@H20";
            H21.ParameterName = "@H21";
            H22.ParameterName = "@H22";
            H23.ParameterName = "@H23";
            FSTHIGHWIDETIME.ParameterName = "@FSTHIGHWIDETIME";
            FSTHIGHWIDEHEIGHT.ParameterName = "@FSTHIGHWIDEHEIGHT";
            FSTLOWWIDETIME.ParameterName = "@FSTLOWWIDETIME";
            FSTLOWWIDEHEIGHT.ParameterName = "@FSTLOWWIDEHEIGHT";
            SCDHIGHWIDETIME.ParameterName = "@SCDHIGHWIDETIME";
            SCDHIGHWIDEHEIGHT.ParameterName = "@SCDHIGHWIDEHEIGHT";
            SCDLOWWIDETIME.ParameterName = "@SCDLOWWIDETIME";
            SCDLOWWIDEHEIGHT.ParameterName = "@SCDLOWWIDEHEIGHT";

            STATION.Value = tide.STATION;
            PREDICTIONDATE.Value = tide.PREDICTIONDATE;
            H0.Value = tide.H0;
            H1.Value = tide.H1;
            H2.Value = tide.H2;
            H3.Value = tide.H3;
            H4.Value = tide.H4;
            H5.Value = tide.H5;
            H6.Value = tide.H6;
            H7.Value = tide.H7;
            H8.Value = tide.H8;
            H9.Value = tide.H9;
            H10.Value = tide.H10;
            H11.Value = tide.H11;
            H12.Value = tide.H12;
            H13.Value = tide.H13;
            H14.Value = tide.H14;
            H15.Value = tide.H15;
            H16.Value = tide.H16;
            H17.Value = tide.H17;
            H18.Value = tide.H18;
            H19.Value = tide.H19;
            H20.Value = tide.H20;
            H21.Value = tide.H21;
            H22.Value = tide.H22;
            H23.Value = tide.H23;
            FSTHIGHWIDETIME.Value = tide.FSTHIGHWIDETIME;
            FSTHIGHWIDEHEIGHT.Value = tide.FSTHIGHWIDEHEIGHT;
            FSTLOWWIDETIME.Value = tide.FSTLOWWIDETIME;
            FSTLOWWIDEHEIGHT.Value = tide.FSTLOWWIDEHEIGHT;
            SCDHIGHWIDETIME.Value = tide.SCDHIGHWIDETIME;
            SCDHIGHWIDEHEIGHT.Value = tide.SCDHIGHWIDEHEIGHT;
            SCDLOWWIDETIME.Value = tide.SCDLOWWIDETIME;
            SCDLOWWIDEHEIGHT.Value = tide.SCDLOWWIDEHEIGHT;


            DbParameter[] parameters = { STATION, PREDICTIONDATE, H0, H1, H2, H3, H4,H5,H6, H7, H8, H9, H10, H11, H12,
                H13, H14, H15, H16,H17,H18,H19,H20, H21, H22, H23,FSTHIGHWIDETIME,FSTHIGHWIDEHEIGHT,FSTLOWWIDETIME,FSTLOWWIDEHEIGHT,
                SCDHIGHWIDETIME,SCDHIGHWIDEHEIGHT,SCDLOWWIDETIME,SCDLOWWIDEHEIGHT};
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                //WriteLog.Write("获取潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
                //return 0;
            }
        }

        /// <summary>
        ///批量导入潮汐预报数据到数据库
        /// </summary>
        /// <param name="tides"></param>
        /// <returns></returns>
        public int AddTidesDataBatch(List<HT_YB_Tide> tides)
        {
            if (tides.Count == 0)
                return 0;

            string sqlItem = "INTO HT_YB_TIDE VALUES('@STATION',to_date('@PREDICTIONDATE','yyyymmdd'),@H0,@H1,@H2,@H3,@H4,@H5,"
                +"@H6,@H7,@H8,@H9,@H10,@H11,@H12,@H13,@H14,@H15,@H16,@H17,@H18,@H19,@H20,@H21,@H22,@H23,'@FSTHIGHWIDETIME',"
                +"'@FSTHIGHWIDEHEIGHT','@FSTLOWWIDETIME','@FSTLOWWIDEHEIGHT','@SCDHIGHWIDETIME','@SCDHIGHWIDEHEIGHT',"
                +"'@SCDLOWWIDETIME','@SCDLOWWIDEHEIGHT') ";



            var sqlIns = "";
            foreach (var tide in tides)
            {


                var sqlItemNew = sqlItem.Replace("@STATION", tide.STATION)
                        .Replace("@PREDICTIONDATE", tide.PREDICTIONDATE)
                        .Replace("@H10", tide.H10.ToString())
                        .Replace("@H11", tide.H11.ToString())
                        .Replace("@H12", tide.H12.ToString())
                        .Replace("@H13", tide.H13.ToString())
                        .Replace("@H14", tide.H14.ToString())
                        .Replace("@H15", tide.H15.ToString())
                        .Replace("@H16", tide.H16.ToString())
                        .Replace("@H17", tide.H17.ToString())
                        .Replace("@H18", tide.H18.ToString())
                        .Replace("@H19", tide.H19.ToString())
                        .Replace("@H20", tide.H20.ToString())
                        .Replace("@H21", tide.H21.ToString())
                        .Replace("@H22", tide.H22.ToString())
                        .Replace("@H23", tide.H23.ToString())
                        .Replace("@H0", tide.H0.ToString())
                        .Replace("@H1", tide.H1.ToString())
                        .Replace("@H2", tide.H2.ToString())
                        .Replace("@H3", tide.H3.ToString())
                        .Replace("@H4", tide.H4.ToString())
                        .Replace("@H5", tide.H5.ToString())
                        .Replace("@H6", tide.H6.ToString())
                        .Replace("@H7", tide.H7.ToString())
                        .Replace("@H8", tide.H8.ToString())
                        .Replace("@H9", tide.H9.ToString())
                        .Replace("@FSTHIGHWIDETIME", tide.FSTHIGHWIDETIME)
                        .Replace("@FSTHIGHWIDEHEIGHT", tide.FSTHIGHWIDEHEIGHT)
                        .Replace("@FSTLOWWIDETIME", tide.FSTLOWWIDETIME)
                        .Replace("@FSTLOWWIDEHEIGHT", tide.FSTLOWWIDEHEIGHT)
                        .Replace("@SCDHIGHWIDETIME", tide.SCDHIGHWIDETIME)
                        .Replace("@SCDHIGHWIDEHEIGHT", tide.SCDHIGHWIDEHEIGHT)
                        .Replace("@SCDLOWWIDETIME", tide.SCDLOWWIDETIME)
                        .Replace("@SCDLOWWIDEHEIGHT", tide.SCDLOWWIDEHEIGHT);

                sqlIns = sqlIns + sqlItemNew;

                //var STATION = DataExe.GetDbParameter();
                //var PREDICTIONDATE = DataExe.GetDbParameter();
                //var H0 = DataExe.GetDbParameter();
                //var H1 = DataExe.GetDbParameter();
                //var H2 = DataExe.GetDbParameter();
                //var H3 = DataExe.GetDbParameter();
                //var H4 = DataExe.GetDbParameter();
                //var H5 = DataExe.GetDbParameter();
                //var H6 = DataExe.GetDbParameter();
                //var H7 = DataExe.GetDbParameter();
                //var H8 = DataExe.GetDbParameter();
                //var H9 = DataExe.GetDbParameter();
                //var H10 = DataExe.GetDbParameter();
                //var H11 = DataExe.GetDbParameter();
                //var H12 = DataExe.GetDbParameter();
                //var H13 = DataExe.GetDbParameter();
                //var H14 = DataExe.GetDbParameter();
                //var H15 = DataExe.GetDbParameter();
                //var H16 = DataExe.GetDbParameter();
                //var H17 = DataExe.GetDbParameter();
                //var H18 = DataExe.GetDbParameter();
                //var H19 = DataExe.GetDbParameter();
                //var H20 = DataExe.GetDbParameter();
                //var H21 = DataExe.GetDbParameter();
                //var H22 = DataExe.GetDbParameter();
                //var H23 = DataExe.GetDbParameter();
                //var FSTHIGHWIDETIME = DataExe.GetDbParameter();
                //var FSTHIGHWIDEHEIGHT = DataExe.GetDbParameter();
                //var FSTLOWWIDETIME = DataExe.GetDbParameter();
                //var FSTLOWWIDEHEIGHT = DataExe.GetDbParameter();
                //var SCDHIGHWIDETIME = DataExe.GetDbParameter();
                //var SCDHIGHWIDEHEIGHT = DataExe.GetDbParameter();
                //var SCDLOWWIDETIME = DataExe.GetDbParameter();
                //var SCDLOWWIDEHEIGHT = DataExe.GetDbParameter();

                //STATION.ParameterName = "@STATION";
                //PREDICTIONDATE.ParameterName = "@PREDICTIONDATE";
                //H0.ParameterName = "@H0";
                //H1.ParameterName = "@H1";
                //H2.ParameterName = "@H2";
                //H3.ParameterName = "@H3";
                //H4.ParameterName = "@H4";
                //H5.ParameterName = "@H5";
                //H6.ParameterName = "@H6";
                //H7.ParameterName = "@H7";
                //H8.ParameterName = "@H8";
                //H9.ParameterName = "@H9";
                //H10.ParameterName = "@H10";
                //H11.ParameterName = "@H11";
                //H12.ParameterName = "@H12";
                //H13.ParameterName = "@H13";
                //H14.ParameterName = "@H14";
                //H15.ParameterName = "@H15";
                //H16.ParameterName = "@H16";
                //H17.ParameterName = "@H17";
                //H18.ParameterName = "@H18";
                //H19.ParameterName = "@H19";
                //H20.ParameterName = "@H20";
                //H21.ParameterName = "@H21";
                //H22.ParameterName = "@H22";
                //H23.ParameterName = "@H23";
                //FSTHIGHWIDETIME.ParameterName = "@FSTHIGHWIDETIME";
                //FSTHIGHWIDEHEIGHT.ParameterName = "@FSTHIGHWIDEHEIGHT";
                //FSTLOWWIDETIME.ParameterName = "@FSTLOWWIDETIME";
                //FSTLOWWIDEHEIGHT.ParameterName = "@FSTLOWWIDEHEIGHT";
                //SCDHIGHWIDETIME.ParameterName = "@SCDHIGHWIDETIME";
                //SCDHIGHWIDEHEIGHT.ParameterName = "@SCDHIGHWIDEHEIGHT";
                //SCDLOWWIDETIME.ParameterName = "@SCDLOWWIDETIME";
                //SCDLOWWIDEHEIGHT.ParameterName = "@SCDLOWWIDEHEIGHT";

                //STATION.Value = tide.STATION;
                //PREDICTIONDATE.Value = tide.PREDICTIONDATE.ToShortDateString();
                //H0.Value = tide.H0;
                //H1.Value = tide.H1;
                //H2.Value = tide.H2;
                //H3.Value = tide.H3;
                //H4.Value = tide.H4;
                //H5.Value = tide.H5;
                //H6.Value = tide.H6;
                //H7.Value = tide.H7;
                //H8.Value = tide.H8;
                //H9.Value = tide.H9;
                //H10.Value = tide.H10;
                //H11.Value = tide.H11;
                //H12.Value = tide.H12;
                //H13.Value = tide.H13;
                //H14.Value = tide.H14;
                //H15.Value = tide.H15;
                //H16.Value = tide.H16;
                //H17.Value = tide.H17;
                //H18.Value = tide.H18;
                //H19.Value = tide.H19;
                //H20.Value = tide.H20;
                //H21.Value = tide.H21;
                //H22.Value = tide.H22;
                //H23.Value = tide.H23;
                //FSTHIGHWIDETIME.Value = tide.FSTHIGHWIDETIME;
                //FSTHIGHWIDEHEIGHT.Value = tide.FSTHIGHWIDEHEIGHT;
                //FSTLOWWIDETIME.Value = tide.FSTLOWWIDETIME;
                //FSTLOWWIDEHEIGHT.Value = tide.FSTLOWWIDEHEIGHT;
                //SCDHIGHWIDETIME.Value = tide.SCDHIGHWIDETIME;
                //SCDHIGHWIDEHEIGHT.Value = tide.SCDHIGHWIDEHEIGHT;
                //SCDLOWWIDETIME.Value = tide.SCDLOWWIDETIME;
                //SCDLOWWIDEHEIGHT.Value = tide.SCDLOWWIDEHEIGHT;


                //DbParameter[] parameters = { STATION, PREDICTIONDATE, H0, H1, H2, H3, H4,H5,H6, H7, H8, H9, H10, H11, H12,
                //H13, H14, H15, H16,H17,H18,H19,H20, H21, H22, H23,FSTHIGHWIDETIME,FSTHIGHWIDEHEIGHT,FSTLOWWIDETIME,FSTLOWWIDEHEIGHT,
                //SCDHIGHWIDETIME,SCDHIGHWIDEHEIGHT,SCDLOWWIDETIME,SCDLOWWIDEHEIGHT};
            }
            var sql = "INSERT ALL " + sqlIns + " SELECT 1 FROM DUAL";
            try
            {
                return DataExe.GetIntExeData(sql);  //.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                //WriteLog.Write("添加潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
                //return 0;
            }
        }

        /// <summary>
        ///导入潮汐预报数据到数据库
        /// </summary>
        /// <param name="tides"></param>
        /// <returns></returns>
        public int AddTidesData3(List<HT_YB_Tide> tides)
        {
            if (tides.Count == 0)
                return 0;

            string sqlItem = "INTO HT_YB_TIDE VALUES('@STATION',to_date('@PREDICTIONDATE','yyyymmdd'),@H0,@H1,@H2,@H3,@H4,@H5,"
                + "@H6,@H7,@H8,@H9,@H10,@H11,@H12,@H13,@H14,@H15,@H16,@H17,@H18,@H19,@H20,@H21,@H22,@H23,'@FSTHIGHWIDETIME',"
                + "'@FSTHIGHWIDEHEIGHT','@FSTLOWWIDETIME','@FSTLOWWIDEHEIGHT','@SCDHIGHWIDETIME','@SCDHIGHWIDEHEIGHT',"
                + "'@SCDLOWWIDETIME','@SCDLOWWIDEHEIGHT') ";



            var sqlIns = "";
            foreach (var tide in tides)
            {


                var sqlItemNew = sqlItem.Replace("@STATION", tide.STATION)
                        .Replace("@PREDICTIONDATE", tide.PREDICTIONDATE)
                        .Replace("@H10", tide.H10.ToString())
                        .Replace("@H11", tide.H11.ToString())
                        .Replace("@H12", tide.H12.ToString())
                        .Replace("@H13", tide.H13.ToString())
                        .Replace("@H14", tide.H14.ToString())
                        .Replace("@H15", tide.H15.ToString())
                        .Replace("@H16", tide.H16.ToString())
                        .Replace("@H17", tide.H17.ToString())
                        .Replace("@H18", tide.H18.ToString())
                        .Replace("@H19", tide.H19.ToString())
                        .Replace("@H20", tide.H20.ToString())
                        .Replace("@H21", tide.H21.ToString())
                        .Replace("@H22", tide.H22.ToString())
                        .Replace("@H23", tide.H23.ToString())
                        .Replace("@H0", tide.H0.ToString())
                        .Replace("@H1", tide.H1.ToString())
                        .Replace("@H2", tide.H2.ToString())
                        .Replace("@H3", tide.H3.ToString())
                        .Replace("@H4", tide.H4.ToString())
                        .Replace("@H5", tide.H5.ToString())
                        .Replace("@H6", tide.H6.ToString())
                        .Replace("@H7", tide.H7.ToString())
                        .Replace("@H8", tide.H8.ToString())
                        .Replace("@H9", tide.H9.ToString())
                        .Replace("@FSTHIGHWIDETIME", tide.FSTHIGHWIDETIME)
                        .Replace("@FSTHIGHWIDEHEIGHT", tide.FSTHIGHWIDEHEIGHT)
                        .Replace("@FSTLOWWIDETIME", tide.FSTLOWWIDETIME)
                        .Replace("@FSTLOWWIDEHEIGHT", tide.FSTLOWWIDEHEIGHT)
                        .Replace("@SCDHIGHWIDETIME", tide.SCDHIGHWIDETIME)
                        .Replace("@SCDHIGHWIDEHEIGHT", tide.SCDHIGHWIDEHEIGHT)
                        .Replace("@SCDLOWWIDETIME", tide.SCDLOWWIDETIME)
                        .Replace("@SCDLOWWIDEHEIGHT", tide.SCDLOWWIDEHEIGHT);

                sqlIns = sqlIns + sqlItemNew;
            }
            var sql = "INSERT ALL " + sqlIns + " SELECT 1 FROM DUAL";
            try
            {
                return DataExe.GetIntExeData(sql);  //.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                //WriteLog.Write("添加潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
                //return 0;
            }
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="station">站点名</param>
        /// <param name="predictiondate">预测日期</param>
        /// <returns></returns>
        public int DelTideDataForStationAndPredictionDate(string station, string predictiondate)
        {
            try
            {
                var STATIONP = DataExe.GetDbParameter();
                STATIONP.ParameterName = "@STATION";
                STATIONP.Value = station;
                var PREDICTIONDATEP = DataExe.GetDbParameter();
                PREDICTIONDATEP.ParameterName = "@PREDICTIONDATE";
                PREDICTIONDATEP.Value = predictiondate;
                DbParameter[] parameters = { STATIONP, PREDICTIONDATEP };

                int a = DataExe.GetIntExeData("delete from HT_YB_TIDE where STATION = @STATION and PREDICTIONDATE = to_date(@PREDICTIONDATE,'yyyymmdd')", parameters);
                return a;

            }
            catch (Exception ex)
            {
                //WriteLog.Write("删除潮汐预报数据出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
                //return 0;
            }

        }
        /// <summary>
        /// 获取所有的导入数据
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="pagerow"></param>
        /// <returns></returns>
        public DataTable GetAllTide(int pagenum, int pagerow)
        {
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;
            
            string sql = "select * from(select t.*,rownum rn from(" +
                    "select* from HT_YB_TIDE   order by  STATION,PREDICTIONDATE desc" +
                    " ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
            try
            {
                DataTable dt = DataExe.GetTableExeData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                return null;

            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            try
            {
                return Convert.ToInt32(DataExe.GetObjectExeData("select COUNT(*) from HT_YB_TIDE"));
            }
            catch (Exception error)
            {
                WriteLog.Write("获取总数出现异常！" + error.Message + "\r\n" + error.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public int GetCount(HT_YB_Tide query)
        {

            string wherestr = "";
            DbParameter[] parameters = { };
            List<DbParameter> parameter = parameters.ToList();
            if (query.PREDICTIONDATE != "")//时间
            {
                string startdata = query.PREDICTIONDATE.Split(',')[0];
                string enddata = query.PREDICTIONDATE.Split(',')[1];
                wherestr += wherestr == "" ? " where " : " and ";
                wherestr += " PREDICTIONDATE>=to_date(@startdata,'yyyy-MM-dd') and PREDICTIONDATE <= to_date(@enddata,'yyyy-MM-dd') ";
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
                return Convert.ToInt32(DataExe.GetObjectExeData("select count(*) from HT_YB_Tide " + wherestr + "", parameters));
              }
            catch (Exception ex)
            {
                WriteLog.Write("获取总数出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return -1;
            }
        }
        /// 分页查询
        /// </summary>
        /// <param name="pagenum">当前页数</param>
        /// <param name="pagerow">当页行数</param>
        /// <returns></returns>
        public object GetTableQuerypage(int pagenum, int pagerow, HT_YB_Tide query)
        {
            //1-10 11-20 21-30
            int pagefist = pagerow * (pagenum - 1) + 1;
            int pagelast = pagerow * (pagenum - 1) + pagerow;
            string wherestr = "";
            DbParameter[] parameters = { };
            List<DbParameter> parameter = parameters.ToList();
            if (query.PREDICTIONDATE != "")//时间
            {
                string startdata = query.PREDICTIONDATE.Split(',')[0];
                string enddata = query.PREDICTIONDATE.Split(',')[1];
                wherestr += wherestr == "" ? " where " : " and ";
                wherestr += " PREDICTIONDATE>=to_date(@startdata,'yyyy-MM-dd') and PREDICTIONDATE <= to_date(@enddata,'yyyy-MM-dd') ";
                var pstartdata = DataExe.GetDbParameter();
                var penddata = DataExe.GetDbParameter();
                pstartdata.ParameterName = "@startdata";
                penddata.ParameterName = "@enddata";
                pstartdata.Value = startdata;
                penddata.Value = enddata;
                parameter.Add(pstartdata);
                parameter.Add(penddata);
            }
            string sql2 = "select * from(select t.*,rownum rn from(select * from HT_YB_TIDE " + wherestr + " order by STATION,PREDICTIONDATE desc ) t where rownum<=" + pagelast + ") where rn>=" + pagefist + "";
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


    }
}