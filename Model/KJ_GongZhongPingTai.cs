using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class KJ_GongZhongPingTai
{
    /// <summary>
    /// 数据ID
    /// </summary>
    public string ID
    {
        get;
        set;
    }
    /// <summary>
    /// 数据插入时间
    /// </summary>
    public DateTime TIME
    {
        get;
        set;
    }
    /// <summary>
    /// 用户标示
    /// </summary>
    public string USERID
    {
        get;
        set;
    }
    /// <summary>
    /// 文档类型  要发布的文档类型 预报，警报等
    /// </summary>
    public string DOCTYPE
    {
        get;
        set;
    }
    /// <summary>
    /// 文档内容
    /// </summary>
    public string DOCUMENTCONTENT
    {
        get;
        set;
    }
    /// <summary>
    /// 消息类型
    /// </summary>
    public string MESTYPE
    {
        get;
        set;

    }
    /// <summary>
    /// 状态
    /// </summary>
    public string STATE
    {
        get;
        set;

    }
    /// <summary>
    /// 附件
    /// </summary>
    public List<KJ_GONGZHONGPINGTAIFUJIAN> fujian
    {
        get;
        set;
    }
    /// <summary>
    /// 短信组
    /// </summary>
    public string DXGROUP
    {
        get;
        set;

    }
    /// <summary>
    /// 标题（微信使用）
    /// </summary>
    public string SUBJECT
    {
        get;
        set;

    }
    /// <summary>
    /// 摘要（微信使用）
    /// </summary>
    public string ABSTRACT
    {
        get;
        set;

    }
    /// <summary>
    /// 类型: 文字 图文 视频 音频（微信使用）
    /// </summary>
    public string TYPE
    {
        get;
        set;

    }
   
}
