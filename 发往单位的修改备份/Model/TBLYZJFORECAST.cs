using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class TBLYZJFORECAST
{
    /// <summary>
    /// 填写日期
    /// </summary>
    public DateTime PUBLISHDATE { get; set; }
    /// <summary>
    /// 预报日期
    /// </summary>
    public DateTime FORECASTDATE { get; set; }
    /// <summary>
    /// 海区
    /// </summary>
    public string SEAAREA { get; set; }
    /// <summary>
    /// 风向
    /// </summary>
    public string WINDDIRECTION { get; set; }
    /// <summary>
    /// 风力
    /// </summary>
    public string WINDFORCE { get; set; }
    /// <summary>
    /// 浪高
    /// </summary>
    public string WAVEHEIGHT { get; set; }
}
