 
using System;


/// <summary>
/// 精细化预报数据展示
/// </summary>

public class Sql_JXHZS
{
    DataExecution_YB DataExe;//声明一个数据执行类
    public Sql_JXHZS()
    {
        //
        DataExe = new DataExecution_YB();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public object Get_JXHYB_DataByDatetime(Model1 Model)
    {

        try
        {

            return DataExe.GetTableExeData("select * from HT_JXHYB where PUBLISHDATE=to_date('" + Model.PUBLISHDATE.ToString() + "', 'yyyy-mm-dd hh24@mi@ss')");
        }
        catch (Exception ex)
        {
            // WriteLog.Write("获取24小时潮位出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            return 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public object Get_JXHYB_AllData()
    {

        try
        {

            return DataExe.GetTableExeData("select * from HT_JXHYB ");
        }
        catch (Exception ex)
        {
            
            return 0;
        }
    }
}
