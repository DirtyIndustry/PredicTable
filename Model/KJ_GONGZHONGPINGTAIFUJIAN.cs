using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class KJ_GONGZHONGPINGTAIFUJIAN
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
    /// 二进制文件存储
    /// </summary>
    public byte[] ANNEX
    {
        get;
        set;
    }
    /// <summary>
    /// 输出多张图片时可根据此项进行排序
    /// </summary>
    public string SORTID
    {
        get;
        set;
    }
    /// <summary>
    /// 包含扩展名的文件名称
    /// </summary>
    public string FILENAME
    {
        get;
        set;
    }
    /// <summary>
    /// 文件类型：下载附件，展示文件
    /// </summary>
    public string TYPE
    {
        get;
        set;
    }
    /// <summary>
    /// 是HT_KJ_GONGZHONGPINGTAI中的ID的外键，一对多关系
    /// </summary>
    public string WAIID
    {
        get;
        set;
    }

}
