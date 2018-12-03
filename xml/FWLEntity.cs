using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 潮汐
/// </summary>
public class FWLXMLEntity
{
    /// <summary>
    /// 版本号
    /// </summary>
    public string version_number = "1.0";
    /// <summary>
    /// 发布单位
    /// </summary>
    public string post_org = "BH";
    /// <summary>
    /// 预报类型
    /// </summary>
    public string forecast_type = "F";
    /// <summary>
    /// 产品类型
    /// </summary>
    public string product_type = "GK";
    /// <summary>
    /// 发布时间
    /// </summary>
    public string post_time { get; set; }
    /// <summary>
    /// 预报区域
    /// </summary>
    public string area { get; set; }
    /// <summary>
    /// 预报时效
    /// </summary>
    public string predict_aging { get; set; }
    /// <summary>
    /// 预报要素
    /// </summary>
    public string element = "FWL";
    /// <summary>
    /// 潮汐信息
    /// </summary>
    public List<FWLEntity> entitys = new List<FWLEntity>();

    /// <summary>
    /// 联系人
    /// </summary>
    public string linkman { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    public string tel { get; set; }
}
public class FWLEntity
{
    /// <summary>
    /// 城市代码
    /// </summary>
    public string site { get; set; }
    /// <summary>
    /// 城市名
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 起算基准面
    /// </summary>
    public string datum_plane { get; set; }
    public string hour0 { get; set; }
    public string hour1 { get; set; }
    public string hour2 { get; set; }
    public string hour3 { get; set; }
    public string hour4 { get; set; }
    public string hour5 { get; set; }
    public string hour6 { get; set; }
    public string hour7 { get; set; }
    public string hour8 { get; set; }
    public string hour9 { get; set; }
    public string hour10 { get; set; }
    public string hour11 { get; set; }
    public string hour12 { get; set; }
    public string hour13 { get; set; }
    public string hour14 { get; set; }
    public string hour15 { get; set; }
    public string hour16 { get; set; }
    public string hour17 { get; set; }
    public string hour18 { get; set; }
    public string hour19 { get; set; }
    public string hour20 { get; set; }
    public string hour21 { get; set; }
    public string hour22 { get; set; }
    public string hour23 { get; set; }
    /// <summary>
    /// 第一高潮时
    /// </summary>
    public string high_tide_time_1 { get; set; }
    /// <summary>
    /// 第一高潮高
    /// </summary>
    public string high_tide_value_1 { get; set; }
    /// <summary>
    /// 第一低潮时
    /// </summary>
    public string low_tide_time_1 { get; set; }
    /// <summary>
    /// 第一低潮高（cm）
    /// </summary>
    public string low_tide_value_1 { get; set; }
    /// <summary>
    /// 第二高潮时
    /// </summary>
    public string high_tide_time_2 { get; set; }
    /// <summary>
    /// 第二高潮高（cm）
    /// </summary>
    public string high_tide_value_2 { get; set; }
    /// <summary>
    /// 第二低潮时
    /// </summary>
    public string low_tide_time_2 { get; set; }
    /// <summary>
    /// 第二低潮高（cm）
    /// </summary>
    public string low_tide_value_2 { get; set; }

    public FWLEntity() { }
    /// <param name="site">城市代码</param>
    /// <param name="name">城市名</param>
    /// <param name="high_tide_time_1">第一高潮时</param>
    /// <param name="high_tide_value_1">第一高潮高</param>
    /// <param name="low_tide_time_1">第一低潮时</param>
    /// <param name="low_tide_value_1">第一低潮高</param>
    /// <param name="high_tide_time_2">第二高潮时</param>
    /// <param name="high_tide_value_2">第二高潮高</param>
    /// <param name="low_tide_time_2">第二低潮时</param>
    /// <param name="low_tide_value_2">第二低潮高</param>
    /// 
    public FWLEntity(string name, string high_tide_time_1, string high_tide_value_1, string low_tide_time_1, string low_tide_value_1,
                                  string high_tide_time_2, string high_tide_value_2, string low_tide_time_2, string low_tide_value_2)
    {
        this.name = name;
        StaticCitySite city_site = new StaticCitySite();
        this.site = city_site.GetSite(name);
        this.high_tide_time_1 = high_tide_time_1;
        this.high_tide_value_1 = high_tide_value_1;
        this.low_tide_time_1 = low_tide_time_1;
        this.low_tide_value_1 = low_tide_value_1;
        this.high_tide_time_2 = high_tide_time_2;
        this.high_tide_value_2 = high_tide_value_2;
        this.low_tide_time_2 = low_tide_time_2;
        this.low_tide_value_2 = low_tide_value_2;

    }

}


