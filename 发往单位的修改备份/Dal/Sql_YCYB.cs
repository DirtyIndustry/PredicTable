 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Sql_YCYB
{
    DataExecution_YB DataExe;//声明一个数据执行类
    public Sql_YCYB()
    {
        //
        DataExe = new DataExecution_YB();//使用默认链接字符初始数据库操作类
        //
    }
    /// <summary>
    /// wave
    /// </summary>
    /// <returns></returns>
    public object Get_YCYB_DataByWAVE()
    {

        try
        {
            
            return DataExe.GetTableExeData("select* from yc_file a INNER join yc_wave c on a.id = c.fileid");
        }
        catch (Exception ex)
        {           
            return 0;
        }
    }

    /// <summary>
    /// wave
    /// </summary>
    /// <returns></returns>
    public object Get_YCYB_DataTimeByWAVE(YC_FILE yc_file)
    {

        try
        {

            return DataExe.GetTableExeData("select *from yc_file a INNER join yc_wave c on a.id=c.fileid  where a.publishdata = to_date('" + yc_file.PublishData.ToString() + "', 'yyyy-mm-dd hh24@mi@ss')");
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    //where PUBLISHDATE = to_date('" + Model.PUBLISHDATE.ToString() + "', 'yyyy-mm-dd hh24@mi@ss')
    /// <summary>
    /// wind
    /// </summary>
    /// <returns></returns>
    public object Get_YCYB_DataByWind()
    {

        try
        {         
            return DataExe.GetTableExeData("select* FROM yc_file a INNER join yc_wind b ON a.id = b.fileid");
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    /// <summary>
    /// wind
    /// </summary>
    /// <returns></returns>
    public object Get_YCYB_DataTimeByWind(YC_FILE yc_file)
    {

        try
        {
            return DataExe.GetTableExeData("select * FROM yc_file a INNER join yc_wind b ON a.id=b.fileid  where a.publishdata = to_date('" + yc_file.PublishData.ToString() + "', 'yyyy-mm-dd hh24@mi@ss')");
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}
