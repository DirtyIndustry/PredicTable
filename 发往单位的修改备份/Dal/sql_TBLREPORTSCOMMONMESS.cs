using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 

    public class sql_TBLREPORTSCOMMONMESS
    {
        DataExecution DataExe;//声明一个数据执行类
        public sql_TBLREPORTSCOMMONMESS()
        {
            DataExe = new DataExecution();//使用默认链接字符初始数据库操作类
        }
        //获取填报基本信息
        public object GetTBLREPORTSCOMMONMESS()
        {
            try
            {
                return DataExe.GetTableExeData("select * from TBLREPORTSCOMMONMESS");
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取填报基本信息出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
                return 0;
            }
        }
     
}