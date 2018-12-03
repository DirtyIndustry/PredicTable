using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// 海浪
/// </summary>
public class FHLXMLEntity
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
    public string element = "FHL";
    /// <summary>
    /// 潮汐信息
    /// </summary>
    public List<FHLEntity> entitys = new List<FHLEntity>();

    /// <summary>
    /// 联系人
    /// </summary>
    public string linkman { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    public string tel { get; set; }
}
public class FHLEntity
{
    /// <summary>
    ///  地点代码
    /// </summary>
    public string site { get; set; }
    /// <summary>
    ///  地点名称
    /// </summary>
    public string name { get; set; }
    /// <summary>
    ///  浪高初值起始值
    /// </summary>
    public string wave_height_initial_value_from { get; set; }
    /// <summary>
    ///  浪高初值修订值
    /// </summary>
    public string wave_height_initial_value_to { get; set; }
    /// <summary>
    ///  浪高变化转折词：曾至
    /// </summary>
    public string wave_height_word { get; set; }
    /// <summary>
    ///  浪高变化值起始值
    /// </summary>
    public string wave_height_change_value_from { get; set; }
    /// <summary>
    ///  浪高变化值修订值
    /// </summary>
    public string wave_height_change_value_to { get; set; }
    /// <summary>
    /// 浪级
    /// </summary>
    public string wave_scale { get; set; }

    /// <summary>
    /// 浪向初值
    /// </summary>
    public string wave_dir_from { get; set; }
    /// <summary>
    /// 浪向变化值
    /// </summary>
    public string wave_dir_to { get; set; }
    /// <summary>
    /// 涌高初值起始值
    /// </summary>
    public string surge_height_initial_value_from { get; set; }
    /// <summary>
    /// 涌高初值修订值
    /// </summary>
    public string surge_height_initial_value_to { get; set; }
    /// <summary>
    /// 涌高转折词
    /// </summary>
    public string surge_height_word { get; set; }
    /// <summary>
    /// 涌高变化初起始值
    /// </summary>
    public string surge_height_change_value_from { get; set; }
    /// <summary>
    /// 涌高变化修订值
    /// </summary>
    public string surge_height_change_value_to { get; set; }
    /// <summary>
    /// 涌向初值
    /// </summary>
    public string surge_dir_from { get; set; }
    /// <summary>
    /// 涌向变化值 
    /// </summary>
    public string surge_dir_to { get; set; }
    public FHLEntity() { }
    public FHLEntity(string name)
    {
        this.name = name;
        StaticCitySite city_site = new StaticCitySite();
        this.site = city_site.GetSite(name);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="wave">海浪变化值，length=5</param>
    /// <param name="surge">涌向值，length=5</param>
    public FHLEntity(string name, string[] wave)
    {
        this.name = name;
        StaticCitySite city_site = new StaticCitySite();
        this.site = city_site.GetSite(name);
        if (wave.Length == 5)
        {
            this.wave_height_initial_value_from = wave[0].ToString();
            this.wave_height_initial_value_to = wave[1].ToString();
            this.wave_height_word = wave[2].ToString();
            this.wave_height_change_value_from = wave[3].ToString();
            this.wave_height_change_value_to = wave[4].ToString();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="wave">海浪变化值，length=5</param>
    /// <param name="surge">涌向值，length=5</param>
    public FHLEntity(string name, string[] wave, string[] surge)
    {
        this.name = name;
        StaticCitySite city_site = new StaticCitySite();
        this.site = city_site.GetSite(name);
        if (wave.Length == 5)
        {
            this.wave_height_initial_value_from = wave[0].ToString();
            this.wave_height_initial_value_to = wave[1].ToString();
            this.wave_height_word = wave[2].ToString();
            this.wave_height_change_value_from = wave[3].ToString();
            this.wave_height_change_value_to = wave[4].ToString();
        }
        if (surge.Length == 5)
        {

            this.wave_height_initial_value_from = wave[0].ToString();
            this.wave_height_initial_value_to = wave[1].ToString();
            this.wave_height_word = wave[2].ToString();
            this.wave_height_change_value_from = wave[3].ToString();
            this.wave_height_change_value_to = wave[4].ToString();
        }
    }
    public void SetWave_height(string[] wave_height)
    {
        if (wave_height.Length == 5)
        {
            this.wave_height_initial_value_from = wave_height[0].ToString();
            this.wave_height_initial_value_to = wave_height[1].ToString();
            this.wave_height_word = wave_height[2].ToString();
            this.wave_height_change_value_from = wave_height[3].ToString();
            this.wave_height_change_value_to = wave_height[4].ToString();
        }
    }
    public void SetWave_dir(string[] Wave_dir)
    {
        if (Wave_dir.Length == 2)
        {
            this.wave_dir_from = Wave_dir[0];
            this.wave_dir_to = Wave_dir[1];
        }
    }
    public void SetSurge(string[] surge)
    {
        if (surge.Length == 5)
        {

            this.wave_height_initial_value_from = surge[0].ToString();
            this.wave_height_initial_value_to = surge[1].ToString();
            this.wave_height_word = surge[2].ToString();
            this.wave_height_change_value_from = surge[3].ToString();
            this.wave_height_change_value_to = surge[4].ToString();
        }
    }
}
