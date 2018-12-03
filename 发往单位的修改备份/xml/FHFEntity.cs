using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class FHFXMLEntity
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
    public string element = "FHF";
    /// <summary>
    /// 潮汐信息
    /// </summary>
    public List<FHFEntity> entitys = new List<FHFEntity>();
    /// <summary>
    /// 联系人
    /// </summary>
    public string linkman { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    public string tel { get; set; }

}
public class FHFEntity
{ /// <summary>
  ///  地点代码
  /// </summary>
    public string site { get; set; }
    /// <summary>
    ///  地点名称
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 风速初值起始值
    /// </summary>
    public string wind_speed_initial_value_from { set; get; }
    /// <summary>
    /// 风速初值修订值
    /// </summary>
    public string wind_speed_initial_value_to { get; set; }
    /// <summary>
    /// 风速变化转折词
    /// </summary>
    public string wind_speed_word { get; set; }
    /// <summary>
    /// 风速变化值起始值
    /// </summary>
    public string wind_speed_change_value_from { get; set; }
    /// <summary>
    /// 风速变化值修订值
    /// </summary>
    public string wind_speed_change_value_to { get; set; }
    /// <summary>
    /// 风向起始方向
    /// </summary>
    public string wind_dir_from { get; set; }
    /// <summary>
    /// 风向修订方向
    /// </summary>
    public string wind_dir_to { get; set; }
    /// <summary>
    /// 风力
    /// </summary>
    public string wind_power { get; set; }
    public FHFEntity() { }
    public FHFEntity(string name)
    {
        this.name = name;
        StaticCitySite city_site = new StaticCitySite();
        this.site = city_site.GetSite(name);
    }
    public FHFEntity(string name,  string[] wind_dir, string wind_power)
    {
        this.name = name;
        StaticCitySite city_site = new StaticCitySite();
        this.site = city_site.GetSite(name);

        if (wind_dir.Length == 2)
        {
            this.wind_dir_from = wind_dir[0];
            this.wind_dir_to = wind_dir[1];
        }
        this.wind_power = wind_power;
    }
    public void SetWind_speed(string[] wind_speed)
    {
        if (wind_speed.Length == 5)
        {
            this.wind_speed_initial_value_from = wind_speed[0];
            this.wind_speed_initial_value_to = wind_speed[1];
            this.wind_speed_word = wind_speed[2];
            this.wind_speed_change_value_from = wind_speed[3];
            this.wind_speed_change_value_to = wind_speed[4];
        }
    }
    public void SetWind_dir(string[] wind_dir)
    {
        if (wind_dir.Length == 2)
        {
            this.wind_dir_from = wind_dir[0];
            this.wind_dir_to = wind_dir[1];
        }
    }

}
