using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 山东七地市预报编号
    /// </summary>
    public class Sql_SDSEVENNO
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_SDSEVENNO()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }

        public object GetSDSevenNO(DateTime PUBLISHDATE)
        {

            try
            {
                string sql = "select * from TBLSDSEVENNNO where PUBLISHTIME=to_date('" + PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取山东七地市预报编号出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        public int AddSDSevenNo(DateTime PUBLISHDATE, string proYear, string proNo)
        {

            string sql = "INSERT INTO  TBLSDSEVENNNO (PUBLISHTIME,PRONO,PROYEAR) VALUES (to_date(@PUBLISHTIME,'yyyy-mm-dd hh24@mi@ss'),@PRONO,@PROYEAR)";



            var PUBLISHTIME = DataExe.GetDbParameter();
            var PRONO = DataExe.GetDbParameter();
            var PROYEAR = DataExe.GetDbParameter();

            PUBLISHTIME.ParameterName = "@PUBLISHTIME";
            PRONO.ParameterName = "@PRONO";
            PROYEAR.ParameterName = "@PROYEAR";

            PUBLISHTIME.Value = PUBLISHDATE.ToString();
            PRONO.Value = proNo;
            PROYEAR.Value = proYear.ToString();


            DbParameter[] parameters = { PUBLISHTIME, PRONO, PROYEAR };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("新增山东七地市预报编号出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        public int EditSDSevenNo(DateTime PUBLISHDATE, string proYear, string proNo)
        {
            string sql = "UPDATE   TBLSDSEVENNNO set  PRONO=@PRONO,PROYEAR=@PROYEAR where PUBLISHTIME=to_date(@PUBLISHTIME,'yyyy-mm-dd hh24@mi@ss')";


            var PUBLISHTIME = DataExe.GetDbParameter();
            var PRONO = DataExe.GetDbParameter();
            var PROYEAR = DataExe.GetDbParameter();

            PUBLISHTIME.ParameterName = "@PUBLISHTIME";
            PRONO.ParameterName = "@PRONO";
            PROYEAR.ParameterName = "@PROYEAR";


            PUBLISHTIME.Value = PUBLISHDATE.ToString();
            PRONO.Value = proNo.ToString();
            PROYEAR.Value = proYear.ToString();

            DbParameter[] parameters = { PUBLISHTIME, PRONO, PROYEAR };
            try
            {
                return DataExe.GetIntExeData(sql, parameters);
            }
            catch (Exception ex)
            {
                WriteLog.Write("修改山东七地市预报编号出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }
    }
}