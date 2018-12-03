using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

	 	//金沙滩浴场潮汐
public class TBLGOLDBEACH72HTIDALFORECAST
{
    /// <summary>
    /// 填报日期
    /// </summary>		
    private DateTime _publishdate;
    public DateTime PUBLISHDATE
    {
        get { return _publishdate; }
        set { _publishdate = value; }
    }
    /// <summary>
    /// 预报日期
    /// </summary>		
    private DateTime _forecastdate;
    public DateTime FORECASTDATE
    {
        get { return _forecastdate; }
        set { _forecastdate = value; }
    }

    public string FIRSTHIGHTIME
    {
        get; set;
    }
    /// <summary>
    /// 第一次高潮潮位
    /// </summary>		
    public string FIRSTHIGHLEVEL
    {
        get; set;
    }
    /// <summary>
    /// 第二次高潮时间
    /// </summary>
    public string SECONDHIGHTIME
    {
        get; set;
    }
    /// <summary>
    /// 第二次高潮潮位
    /// </summary>
    public string SECONDHEIGHTLEVEL
    {
        get; set;
    }
    /// <summary>
    /// 第一次低潮时间
    /// </summary>
    public string FIRSTLOWTIME
    {
        get; set;
    }
    /// <summary>
    /// 第一次低潮潮位
    /// </summary>
    public string FIRSTLOWLEVEL
    {
        get; set;
    }

    /// <summary>
    /// 第二次低潮时间
    /// </summary>
    public string SECONDLOWTIME
    {
        get; set;
    }
    /// <summary>
    /// 第二次低潮潮位
    /// </summary>
    public string SECONDLOWLEVEL
    {
        get; set;
    }
    private string _seabeach;
    public string SEABEACH
    {
        get { return _seabeach; }
        set { _seabeach = value; }
    }
}


