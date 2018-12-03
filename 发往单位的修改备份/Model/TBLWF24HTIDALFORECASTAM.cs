using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//潍坊项24小时潮汐预报(上午)
public class TBLWF24HTIDALFORECASTAM
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
    /// 第一次高潮潮时
    /// </summary>		
    private string _wf24htffirsthighwavetime;
    public string WF24HTFFIRSTHIGHWAVETIME
    {
        get { return _wf24htffirsthighwavetime; }
        set { _wf24htffirsthighwavetime = value; }
    }
    /// <summary>
    /// 第一次高潮潮高
    /// </summary>		
    private string _wf24htffirsthighwaveheight;
    public string WF24HTFFIRSTHIGHWAVEHEIGHT
    {
        get { return _wf24htffirsthighwaveheight; }
        set { _wf24htffirsthighwaveheight = value; }
    }
    /// <summary>
    /// 第二次高潮潮时
    /// </summary>		
    private string _wf24htfsecondhighwavetime;
    public string WF24HTFSECONDHIGHWAVETIME
    {
        get { return _wf24htfsecondhighwavetime; }
        set { _wf24htfsecondhighwavetime = value; }
    }
    /// <summary>
    /// 第二次高潮潮高
    /// </summary>		
    private string _wf24htfsecondhighwaveheight;
    public string WF24HTFSECONDHIGHWAVEHEIGHT
    {
        get { return _wf24htfsecondhighwaveheight; }
        set { _wf24htfsecondhighwaveheight = value; }
    }
    /// <summary>
    /// 第一次低潮潮时
    /// </summary>		
    private string _wf24htffirstlowwavetime;
    public string WF24HTFFIRSTLOWWAVETIME
    {
        get { return _wf24htffirstlowwavetime; }
        set { _wf24htffirstlowwavetime = value; }
    }
    /// <summary>
    /// 第一次低潮潮高
    /// </summary>		
    private string _wf24htffirstlowwaveheight;
    public string WF24HTFFIRSTLOWWAVEHEIGHT
    {
        get { return _wf24htffirstlowwaveheight; }
        set { _wf24htffirstlowwaveheight = value; }
    }
    /// <summary>
    /// 第二次低潮潮时
    /// </summary>		
    private string _wf24htfsecondlowwavetime;
    public string WF24HTFSECONDLOWWAVETIME
    {
        get { return _wf24htfsecondlowwavetime; }
        set { _wf24htfsecondlowwavetime = value; }
    }
    /// <summary>
    /// 第二次低潮潮高
    /// </summary>		
    private string _wf24htfsecondlowwaveheight;
    public string WF24HTFSECONDLOWWAVEHEIGHT
    {
        get { return _wf24htfsecondlowwaveheight; }
        set { _wf24htfsecondlowwaveheight = value; }
    }

}



