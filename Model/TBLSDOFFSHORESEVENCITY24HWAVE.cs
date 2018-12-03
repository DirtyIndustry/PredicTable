using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;

//山东省近海七市24小时海浪
public class TBLSDOFFSHORESEVENCITY24HWAVE
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
    /// 地区
    /// </summary>		
    private string _sdoscwarea;
    public string SDOSCWAREA
    {
        get { return _sdoscwarea; }
        set { _sdoscwarea = value; }
    }
    /// <summary>
    /// 最低浪高
    /// </summary>		
    private string _sdoscwlowestwaveheight;
    public string SDOSCWLOWESTWAVEHEIGHT
    {
        get { return _sdoscwlowestwaveheight; }
        set { _sdoscwlowestwaveheight = value; }
    }
    /// <summary>
    /// 最高浪高
    /// </summary>		
    private string _sdoscwhightestwaveheight;
    public string SDOSCWHIGHTESTWAVEHEIGHT
    {
        get { return _sdoscwhightestwaveheight; }
        set { _sdoscwhightestwaveheight = value; }
    }
    /// <summary>
    /// 表层水温
    /// </summary>		
    private string _sdoscwsurfacetemperature;
    public string SDOSCWSURFACETEMPERATURE
    {
        get { return _sdoscwsurfacetemperature; }
        set { _sdoscwsurfacetemperature = value; }
    }

    /// <summary>
    /// 浪高48小时
    /// </summary>		
    public string SDOSCWESTWAVEHEIGHT48H
    {
        get; set;
    }
    /// <summary>
    /// 浪高72小时
    /// </summary>		
    public string SDOSCWESTWAVEHEIGHT72H
    {
        get; set;
    }
    /// <summary>
    /// 表层水温48小时
    /// </summary>
    public string SDOSCWSURFACETEMPERATURE48H
    {
        get; set;
    }
    /// <summary>
    /// 表层水温72小时
    /// </summary>
    public string SDOSCWSURFACETEMPERATURE72H
    {
        get; set;
    }

}


