using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    /// <summary>
    /// 东营埕岛油田预报编号
    /// </summary>
    public class Sql_DYNO
    {
        DataExecution DataExe;//声明一个数据执行类
        public Sql_DYNO()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        /// <summary>
        /// 获取东营埕岛油田预报编号
        /// </summary>
        /// <param name="PUBLISHTIME"></param>
        /// <returns></returns>
        public object GetDYNo(DateTime PUBLISHDATE)
        {

            try
            {
                string sql = "select * from HT_DYNO where PUBLISHTIME=to_date('" + PUBLISHDATE.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                return DataExe.GetTableExeData(sql);

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取东营埕岛油田预报编号出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        
        /// <summary>
        /// 新增东营埕岛油田预报编号
        /// </summary>
        /// <returns></returns>
        public int AddDYNo(DateTime PUBLISHDATE, string proYear, string proNo)
        {

            string sql = "INSERT INTO  HT_DYNO (PUBLISHTIME,PRONO,PROYEAR) VALUES (to_date(@PUBLISHTIME,'yyyy-mm-dd hh24@mi@ss'),@PRONO,@PROYEAR)";



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
                WriteLog.Write("新增东营埕岛油田预报编号出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 修改东营埕岛油田预报编号
        /// </summary>
        public int EditDYNo(DateTime PUBLISHDATE, string proYear, string proNo)
        {
            string sql = "UPDATE   HT_DYNO set  PRONO=@PRONO,PROYEAR=@PROYEAR where PUBLISHTIME=to_date(@PUBLISHTIME,'yyyy-mm-dd hh24@mi@ss')";


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
                WriteLog.Write("修改东营埕岛油田预报编号出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }


        }
    }
}