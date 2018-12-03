using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    /// <summary>
    /// 上合峰会专项预报海风实体类
    /// </summary>
    public class SCOFLModel
    {

        public DateTime _publishdate;
        /// <summary>
        /// 填报日期
        /// </summary>
        public DateTime PUBLISHDATE
        {
            get { return _publishdate; }
            set { _publishdate = value; }
        }

        public string _forecastarea;
        /// <summary>
        /// 预报海区
        /// </summary>
        public string FORECASTAREA
        {
            get { return _forecastarea; }
            set { _forecastarea = value; }
        }

        #region  天气
        public string _weather00d00h;
        /// <summary>
        /// 第一天一时段天气
        /// </summary>
        public string WEATHER00D00H
        {
            get { return _weather00d00h; }
            set { _weather00d00h = value; }
        }
        public string _weather00d01h;
        /// <summary>
        /// 第一天二时段天气
        /// </summary>
        public string WEATHER00D01H
        {
            get { return _weather00d01h; }
            set { _weather00d01h = value; }
        }

        public string _weather01d00h;
        /// <summary>
        /// 第2天1时段天气
        /// </summary>
        public string WEATHER01D00H
        {
            get { return _weather01d00h; }
            set { _weather01d00h = value; }
        }
        public string _weather01d01h;
        /// <summary>
        /// 第2天2时段天气
        /// </summary>
        public string WEATHER01D01H
        {
            get { return _weather01d01h; }
            set { _weather01d01h = value; }
        }
        public string _weather02d00h;
        /// <summary>
        /// 第3天一时段天气
        /// </summary>
        public string WEATHER02D00H
        {
            get { return _weather02d00h; }
            set { _weather02d00h = value; }
        }
        public string _weather02d01h;
        /// <summary>
        /// 第3天一时段天气
        /// </summary>
        public string WEATHER02D01H
        {
            get { return _weather02d01h; }
            set { _weather02d01h = value; }
        }
        #endregion

        #region 风力 
        #region 第一天风力
        public string _windforce00h;
        /// <summary>
        /// 1天00时风力
        /// </summary>
        public string WINDFORCE00H
        {
            get { return _windforce00h; }
            set { _windforce00h = value; }
        }

        public string _windforce01h;
        /// <summary>
        /// 1天01时风力
        /// </summary>
        public string WINDFORCE01H
        {
            get { return _windforce01h; }
            set { _windforce01h = value; }
        }

        public string _windforce02h;
        /// <summary>
        /// 1天02时风力
        /// </summary>
        public string WINDFORCE02H
        {
            get { return _windforce02h; }
            set { _windforce02h = value; }
        }

        public string _windforce03h;
        /// <summary>
        /// 1天03时风力
        /// </summary>
        public string WINDFORCE03H
        {
            get { return _windforce03h; }
            set { _windforce03h = value; }
        }

        public string _windforce04h;
        /// <summary>
        /// 1天04时风力
        /// </summary>
        public string WINDFORCE04H
        {
            get { return _windforce04h; }
            set { _windforce04h = value; }
        }

        public string _windforce05h;
        /// <summary>
        /// 1天05时风力
        /// </summary>
        public string WINDFORCE05H
        {
            get { return _windforce05h; }
            set { _windforce05h = value; }
        }

        public string _windforce06h;
        /// <summary>
        /// 1天06时风力
        /// </summary>
        public string WINDFORCE06H
        {
            get { return _windforce06h; }
            set { _windforce06h = value; }
        }
        public string _windforce07h;
        /// <summary>
        /// 1天00时风力
        /// </summary>
        public string WINDFORCE07H
        {
            get { return _windforce07h; }
            set { _windforce07h = value; }
        }
        public string _windforce08h;
        /// <summary>
        /// 1天08时风力
        /// </summary>
        public string WINDFORCE08H
        {
            get { return _windforce08h; }
            set { _windforce08h = value; }
        }
        public string _windforce09h;
        /// <summary>
        /// 1天09时风力
        /// </summary>
        public string WINDFORCE09H
        {
            get { return _windforce09h; }
            set { _windforce09h = value; }
        }

        public string _windforce10h;
        /// <summary>
        /// 1天10时风力
        /// </summary>
        public string WINDFORCE10H
        {
            get { return _windforce10h; }
            set { _windforce10h = value; }
        }

        public string _windforce11h;
        /// <summary>
        /// 1天11时风力
        /// </summary>
        public string WINDFORCE11H
        {
            get { return _windforce11h; }
            set { _windforce11h = value; }
        }
        public string _windforce12h;
        /// <summary>
        /// 1天12时风力
        /// </summary>
        public string WINDFORCE12H
        {
            get { return _windforce12h; }
            set { _windforce12h = value; }
        }
        public string _windforce13h;
        /// <summary>
        /// 1天13时风力
        /// </summary>
        public string WINDFORCE13H
        {
            get { return _windforce13h; }
            set { _windforce13h = value; }
        }
        public string _windforce14h;
        /// <summary>
        /// 1天14时风力
        /// </summary>
        public string WINDFORCE14H
        {
            get { return _windforce14h; }
            set { _windforce14h = value; }
        }
        public string _windforce15h;
        /// <summary>
        /// 1天15时风力
        /// </summary>
        public string WINDFORCE15H
        {
            get { return _windforce15h; }
            set { _windforce15h = value; }
        }
        public string _windforce16h;
        /// <summary>
        /// 1天16时风力
        /// </summary>
        public string WINDFORCE16H
        {
            get { return _windforce16h; }
            set { _windforce16h = value; }
        }
        public string _windforce17h;
        /// <summary>
        /// 1天17时风力
        /// </summary>
        public string WINDFORCE17H
        {
            get { return _windforce17h; }
            set { _windforce17h = value; }
        }
        public string _windforce18h;
        /// <summary>
        /// 1天18时风力
        /// </summary>
        public string WINDFORCE18H
        {
            get { return _windforce18h; }
            set { _windforce18h = value; }
        }
        public string _windforce19h;
        /// <summary>
        /// 1天19时风力
        /// </summary>
        public string WINDFORCE19H
        {
            get { return _windforce19h; }
            set { _windforce19h = value; }
        }
        public string _windforce20h;
        /// <summary>
        /// 1天20时风力
        /// </summary>
        public string WINDFORCE20H
        {
            get { return _windforce20h; }
            set { _windforce20h = value; }
        }
        public string _windforce21h;
        /// <summary>
        /// 1天21时风力
        /// </summary>
        public string WINDFORCE21H
        {
            get { return _windforce21h; }
            set { _windforce21h = value; }
        }
        public string _windforce22h;
        /// <summary>
        /// 1天22时风力
        /// </summary>
        public string WINDFORCE22H
        {
            get { return _windforce22h; }
            set { _windforce22h = value; }
        }
        public string _windforce23h;
        /// <summary>
        /// 1天23时风力
        /// </summary>
        public string WINDFORCE23H
        {
            get { return _windforce23h; }
            set { _windforce23h = value; }
        }
        #endregion
        #region 第二 三天 风力
        public string _windforce24h;
        /// <summary>
        /// 2天1时段风力
        /// </summary>
        public string WINDFORCE24H
        {
            get { return _windforce24h; }
            set { _windforce24h = value; }
        }
        public string _windforce25h;
        /// <summary>
        /// 2天2时段风力
        /// </summary>
        public string WINDFORCE25H
        {
            get { return _windforce25h; }
            set { _windforce25h = value; }
        }
        public string _windforce26h;
        /// <summary>
        /// 2天3时段风力
        /// </summary>
        public string WINDFORCE26H
        {
            get { return _windforce26h; }
            set { _windforce26h = value; }
        }
        public string _windforce27h;
        /// <summary>
        /// 2天4时段风力
        /// </summary>
        public string WINDFORCE27H
        {
            get { return _windforce27h; }
            set { _windforce27h = value; }
        }
        public string _windforce28h;
        /// <summary>
        /// 3天1时段风力
        /// </summary>
        public string WINDFORCE28H
        {
            get { return _windforce28h; }
            set { _windforce28h = value; }
        }
        public string _windforce29h;
        /// <summary>
        /// 3天2时段风力
        /// </summary>
        public string WINDFORCE29H
        {
            get { return _windforce29h; }
            set { _windforce29h = value; }
        }
        public string _windforce30h;
        /// <summary>
        /// 3天3时段风力
        /// </summary>
        public string WINDFORCE30H
        {
            get { return _windforce30h; }
            set { _windforce30h = value; }
        }
        public string _windforce31h;
        /// <summary>
        /// 3天4时段风力
        /// </summary>
        public string WINDFORCE31H
        {
            get { return _windforce31h; }
            set { _windforce31h = value; }
        }
        #endregion
        #endregion

        #region 风向  
        #region 第一天风向
        public string _winddirection00h;
        /// <summary>
        /// 1天00时风向
        /// </summary>
        public string WINDDIRECTION00H
        {
            get { return _winddirection00h; }
            set { _winddirection00h = value; }
        }
        public string _winddirection01h;
        /// <summary>
        /// 1天01时风向
        /// </summary>
        public string WINDDIRECTION01H
        {
            get { return _winddirection01h; }
            set { _winddirection01h = value; }
        }
        public string _winddirection02h;
        /// <summary>
        /// 1天02时风向
        /// </summary>
        public string WINDDIRECTION02H
        {
            get { return _winddirection02h; }
            set { _winddirection02h = value; }
        }
        public string _winddirection03h;
        /// <summary>
        /// 1天03时风向
        /// </summary>
        public string WINDDIRECTION03H
        {
            get { return _winddirection03h; }
            set { _winddirection03h = value; }
        }
        public string _winddirection04h;
        /// <summary>
        /// 1天04时风向
        /// </summary>
        public string WINDDIRECTION04H
        {
            get { return _winddirection04h; }
            set { _winddirection04h = value; }
        }
        public string _winddirection05h;
        /// <summary>
        /// 1天05时风向
        /// </summary>
        public string WINDDIRECTION05H
        {
            get { return _winddirection05h; }
            set { _winddirection05h = value; }
        }
        public string _winddirection06h;
        /// <summary>
        /// 1天06时风向
        /// </summary>
        public string WINDDIRECTION06H
        {
            get { return _winddirection06h; }
            set { _winddirection06h = value; }
        }
        public string _winddirection07h;
        /// <summary>
        /// 1天07时风向
        /// </summary>
        public string WINDDIRECTION07H
        {
            get { return _winddirection07h; }
            set { _winddirection07h = value; }
        }
        public string _winddirection08h;
        /// <summary>
        /// 1天08时风向
        /// </summary>
        public string WINDDIRECTION08H
        {
            get { return _winddirection08h; }
            set { _winddirection08h = value; }
        }
        public string _winddirection09h;
        /// <summary>
        /// 1天09时风向
        /// </summary>
        public string WINDDIRECTION09H
        {
            get { return _winddirection09h; }
            set { _winddirection09h = value; }
        }
        public string _winddirection10h;
        /// <summary>
        /// 1天10时风向
        /// </summary>
        public string WINDDIRECTION10H
        {
            get { return _winddirection10h; }
            set { _winddirection10h = value; }
        }
        public string _winddirection11h;
        /// <summary>
        /// 1天1时风向
        /// </summary>
        public string WINDDIRECTION11H
        {
            get { return _winddirection11h; }
            set { _winddirection11h = value; }
        }
        public string _winddirection12h;
        /// <summary>
        /// 1天12时风向
        /// </summary>
        public string WINDDIRECTION12H
        {
            get { return _winddirection12h; }
            set { _winddirection12h = value; }
        }
        public string _winddirection13h;
        /// <summary>
        /// 1天13时风向
        /// </summary>
        public string WINDDIRECTION13H
        {
            get { return _winddirection13h; }
            set { _winddirection13h = value; }
        }
        public string _winddirection14h;
        /// <summary>
        /// 1天14时风向
        /// </summary>
        public string WINDDIRECTION14H
        {
            get { return _winddirection14h; }
            set { _winddirection14h = value; }
        }
        public string _winddirection15h;
        /// <summary>
        /// 1天15时风向
        /// </summary>
        public string WINDDIRECTION15H
        {
            get { return _winddirection15h; }
            set { _winddirection15h = value; }
        }
        public string _winddirection16h;
        /// <summary>
        /// 1天16时风向
        /// </summary>
        public string WINDDIRECTION16H
        {
            get { return _winddirection16h; }
            set { _winddirection16h = value; }
        }
        public string _winddirection17h;
        /// <summary>
        /// 1天17时风向
        /// </summary>
        public string WINDDIRECTION17H
        {
            get { return _winddirection17h; }
            set { _winddirection17h = value; }

        }
        public string _winddirection18h;
        /// <summary>
        /// 1天18时风向
        /// </summary>
        public string WINDDIRECTION18H
        {
            get { return _winddirection18h; }
            set { _winddirection18h = value; }
        }
        public string _winddirection19h;
        /// <summary>
        /// 1天19时风向
        /// </summary>
        public string WINDDIRECTION19H
        {
            get { return _winddirection19h; }
            set { _winddirection19h = value; }
        }
        public string _winddirection20h;
        /// <summary>
        /// 1天02时风向
        /// </summary>
        public string WINDDIRECTION20H
        {
            get { return _winddirection20h; }
            set { _winddirection20h = value; }
        }
        public string _winddirection21h;
        /// <summary>
        /// 1天21时风向
        /// </summary>
        public string WINDDIRECTION21H
        {
            get { return _winddirection21h; }
            set { _winddirection21h = value; }
        }
        public string _winddirection22h;
        /// <summary>
        /// 1天22时风向
        /// </summary>
        public string WINDDIRECTION22H
        {
            get { return _winddirection22h; }
            set { _winddirection22h = value; }
        }
        public string _winddirection23h;
        /// <summary>
        /// 1天23时风向
        /// </summary>
        public string WINDDIRECTION23H
        {
            get { return _winddirection23h; }
            set { _winddirection23h = value; }
        }
        #endregion
        #region 第二天 三天风向
        public string _winddirection24h;
        /// <summary>
        /// 2天1时段风向
        /// </summary>
        public string WINDDIRECTION24H
        {
            get { return _winddirection24h; }
            set { _winddirection24h = value; }
        }
        public string _winddirection25h;
        /// <summary>
        /// 2天2时段风向
        /// </summary>
        public string WINDDIRECTION25H
        {
            get { return _winddirection25h; }
            set { _winddirection25h = value; }
        }
        public string _winddirection26h;
        /// <summary>
        /// 2天3时段风向
        /// </summary>
        public string WINDDIRECTION26H
        {
            get { return _winddirection26h; }
            set { _winddirection26h = value; }
        }
        public string _winddirection27h;
        /// <summary>
        /// 2天4时段风向
        /// </summary>
        public string WINDDIRECTION27H
        {
            get { return _winddirection27h; }
            set { _winddirection27h = value; }
        }
        public string _winddirection28h;
        /// <summary>
        /// 3天1时段风向
        /// </summary>
        public string WINDDIRECTION28H
        {
            get { return _winddirection28h; }
            set { _winddirection28h = value; }
        }
        public string _winddirection29h;
        /// <summary>
        /// 3天2时段风向
        /// </summary>
        public string WINDDIRECTION29H
        {
            get { return _winddirection29h; }
            set { _winddirection29h = value; }
        }
        public string _winddirection30h;
        /// <summary>
        /// 3天3时段风向
        /// </summary>
        public string WINDDIRECTION30H
        {
            get { return _winddirection30h; }
            set { _winddirection30h = value; }
        }
        public string _winddirection31h;
        /// <summary>
        /// 3天4时段风向
        /// </summary>
        public string WINDDIRECTION31H
        {
            get { return _winddirection31h; }
            set { _winddirection31h = value; }
        }
        #endregion
        #endregion

    }

    /// <summary>
    /// 上合峰会专项预报海浪实体类
    /// </summary>
    public class SCOWaveModel {
        public DateTime _publishdate;
        /// <summary>
        /// 填报日期
        /// </summary>
        public DateTime PUBLISHDATE
        {
            get { return _publishdate; }
            set { _publishdate = value; }
        }

        public string _forecastarea;
        /// <summary>
        /// 预报海区
        /// </summary>
        public string FORECASTAREA
        {
            get { return _forecastarea; }
            set { _forecastarea = value; }
        }
        #region 波高
        #region 第一天波高
        public string _waveforce00h;
        /// <summary>
        /// 1天00时浪高
        /// </summary>
        public string WAVEFORCE00H
        {
            get { return _waveforce00h; }
            set { _waveforce00h = value; }
        }

        public string _waveforce01h;
        /// <summary>
        /// 1天01时浪高
        /// </summary>
        public string WAVEFORCE01H
        {
            get { return _waveforce01h; }
            set { _waveforce01h = value; }
        }

        public string _waveforce02h;
        /// <summary>
        /// 1天02时浪高
        /// </summary>
        public string WAVEFORCE02H
        {
            get { return _waveforce02h; }
            set { _waveforce02h = value; }
        }

        public string _waveforce03h;
        /// <summary>
        /// 1天03时浪高
        /// </summary>
        public string WAVEFORCE03H
        {
            get { return _waveforce03h; }
            set { _waveforce03h = value; }
        }

        public string _waveforce04h;
        /// <summary>
        /// 1天04时浪高
        /// </summary>
        public string WAVEFORCE04H
        {
            get { return _waveforce04h; }
            set { _waveforce04h = value; }
        }

        public string _waveforce05h;
        /// <summary>
        /// 1天05时浪高
        /// </summary>
        public string WAVEFORCE05H
        {
            get { return _waveforce05h; }
            set { _waveforce05h = value; }
        }

        public string _waveforce06h;
        /// <summary>
        /// 1天06时浪高
        /// </summary>
        public string WAVEFORCE06H
        {
            get { return _waveforce06h; }
            set { _waveforce06h = value; }
        }
        public string _waveforce07h;
        /// <summary>
        /// 1天00时浪高
        /// </summary>
        public string WAVEFORCE07H
        {
            get { return _waveforce07h; }
            set { _waveforce07h = value; }
        }
        public string _waveforce08h;
        /// <summary>
        /// 1天08时浪高
        /// </summary>
        public string WAVEFORCE08H
        {
            get { return _waveforce08h; }
            set { _waveforce08h = value; }
        }
        public string _waveforce09h;
        /// <summary>
        /// 1天09时浪高
        /// </summary>
        public string WAVEFORCE09H
        {
            get { return _waveforce09h; }
            set { _waveforce09h = value; }
        }

        public string _waveforce10h;
        /// <summary>
        /// 1天10时浪高
        /// </summary>
        public string WAVEFORCE10H
        {
            get { return _waveforce10h; }
            set { _waveforce10h = value; }
        }

        public string _waveforce11h;
        /// <summary>
        /// 1天11时浪高
        /// </summary>
        public string WAVEFORCE11H
        {
            get { return _waveforce11h; }
            set { _waveforce11h = value; }
        }
        public string _waveforce12h;
        /// <summary>
        /// 1天12时浪高
        /// </summary>
        public string WAVEFORCE12H
        {
            get { return _waveforce12h; }
            set { _waveforce12h = value; }
        }
        public string _waveforce13h;
        /// <summary>
        /// 1天13时浪高
        /// </summary>
        public string WAVEFORCE13H
        {
            get { return _waveforce13h; }
            set { _waveforce13h = value; }
        }
        public string _waveforce14h;
        /// <summary>
        /// 1天14时浪高
        /// </summary>
        public string WAVEFORCE14H
        {
            get { return _waveforce14h; }
            set { _waveforce14h = value; }
        }
        public string _waveforce15h;
        /// <summary>
        /// 1天15时浪高
        /// </summary>
        public string WAVEFORCE15H
        {
            get { return _waveforce15h; }
            set { _waveforce15h = value; }
        }
        public string _waveforce16h;
        /// <summary>
        /// 1天16时浪高
        /// </summary>
        public string WAVEFORCE16H
        {
            get { return _waveforce16h; }
            set { _waveforce16h = value; }
        }
        public string _waveforce17h;
        /// <summary>
        /// 1天17时浪高
        /// </summary>
        public string WAVEFORCE17H
        {
            get { return _waveforce17h; }
            set { _waveforce17h = value; }
        }
        public string _waveforce18h;
        /// <summary>
        /// 1天18时浪高
        /// </summary>
        public string WAVEFORCE18H
        {
            get { return _waveforce18h; }
            set { _waveforce18h = value; }
        }
        public string _waveforce19h;
        /// <summary>
        /// 1天19时浪高
        /// </summary>
        public string WAVEFORCE19H
        {
            get { return _waveforce19h; }
            set { _waveforce19h = value; }
        }
        public string _waveforce20h;
        /// <summary>
        /// 1天20时浪高
        /// </summary>
        public string WAVEFORCE20H
        {
            get { return _waveforce20h; }
            set { _waveforce20h = value; }
        }
        public string _waveforce21h;
        /// <summary>
        /// 1天21时浪高
        /// </summary>
        public string WAVEFORCE21H
        {
            get { return _waveforce21h; }
            set { _waveforce21h = value; }
        }
        public string _waveforce22h;
        /// <summary>
        /// 1天22时浪高
        /// </summary>
        public string WAVEFORCE22H
        {
            get { return _waveforce22h; }
            set { _waveforce22h = value; }
        }
        public string _waveforce23h;
        /// <summary>
        /// 1天23时浪高
        /// </summary>
        public string WAVEFORCE23H
        {
            get { return _waveforce23h; }
            set { _waveforce23h = value; }
        }
        #endregion
        #region 第二 三天 浪高
        public string _waveforce24h;
        /// <summary>
        /// 2天1时段浪高
        /// </summary>
        public string WAVEFORCE24H
        {
            get { return _waveforce24h; }
            set { _waveforce24h = value; }
        }
        public string _waveforce25h;
        /// <summary>
        /// 2天2时段浪高
        /// </summary>
        public string WAVEFORCE25H
        {
            get { return _waveforce25h; }
            set { _waveforce25h = value; }
        }
        public string _waveforce26h;
        /// <summary>
        /// 2天3时段浪高
        /// </summary>
        public string WAVEFORCE26H
        {
            get { return _waveforce26h; }
            set { _waveforce26h = value; }
        }
        public string _waveforce27h;
        /// <summary>
        /// 2天4时段浪高
        /// </summary>
        public string WAVEFORCE27H
        {
            get { return _waveforce27h; }
            set { _waveforce27h = value; }
        }
        public string _waveforce28h;
        /// <summary>
        /// 3天1时段浪高
        /// </summary>
        public string WAVEFORCE28H
        {
            get { return _waveforce28h; }
            set { _waveforce28h = value; }
        }
        public string _waveforce29h;
        /// <summary>
        /// 3天2时段浪高
        /// </summary>
        public string WAVEFORCE29H
        {
            get { return _waveforce29h; }
            set { _waveforce29h = value; }
        }
        public string _waveforce30h;
        /// <summary>
        /// 3天3时段浪高
        /// </summary>
        public string WAVEFORCE30H
        {
            get { return _waveforce30h; }
            set { _waveforce30h = value; }
        }
        public string _waveforce31h;
        /// <summary>
        /// 3天4时段浪高
        /// </summary>
        public string WAVEFORCE31H
        {
            get { return _waveforce31h; }
            set { _waveforce31h = value; }
        }
        #endregion
        #endregion


        #region 浪向
        #region 第一天浪向
        public string _wavedirection00h;
        /// <summary>
        /// 1天00时浪向
        /// </summary>
        public string WAVEDIRECTION00H
        {
            get { return _wavedirection00h; }
            set { _wavedirection00h = value; }
        }
        public string _wavedirection01h;
        /// <summary>
        /// 1天01时浪向
        /// </summary>
        public string WAVEDIRECTION01H
        {
            get { return _wavedirection01h; }
            set { _wavedirection01h = value; }
        }
        public string _wavedirection02h;
        /// <summary>
        /// 1天02时浪向
        /// </summary>
        public string WAVEDIRECTION02H
        {
            get { return _wavedirection02h; }
            set { _wavedirection02h = value; }
        }
        public string _wavedirection03h;
        /// <summary>
        /// 1天03时浪向
        /// </summary>
        public string WAVEDIRECTION03H
        {
            get { return _wavedirection03h; }
            set { _wavedirection03h = value; }
        }
        public string _wavedirection04h;
        /// <summary>
        /// 1天04时浪向
        /// </summary>
        public string WAVEDIRECTION04H
        {
            get { return _wavedirection04h; }
            set { _wavedirection04h = value; }
        }
        public string _wavedirection05h;
        /// <summary>
        /// 1天05时浪向
        /// </summary>
        public string WAVEDIRECTION05H
        {
            get { return _wavedirection05h; }
            set { _wavedirection05h = value; }
        }
        public string _wavedirection06h;
        /// <summary>
        /// 1天06时浪向
        /// </summary>
        public string WAVEDIRECTION06H
        {
            get { return _wavedirection06h; }
            set { _wavedirection06h = value; }
        }
        public string _wavedirection07h;
        /// <summary>
        /// 1天07时浪向
        /// </summary>
        public string WAVEDIRECTION07H
        {
            get { return _wavedirection07h; }
            set { _wavedirection07h = value; }
        }
        public string _wavedirection08h;
        /// <summary>
        /// 1天08时浪向
        /// </summary>
        public string WAVEDIRECTION08H
        {
            get { return _wavedirection08h; }
            set { _wavedirection08h = value; }
        }
        public string _wavedirection09h;
        /// <summary>
        /// 1天09时浪向
        /// </summary>
        public string WAVEDIRECTION09H
        {
            get { return _wavedirection09h; }
            set { _wavedirection09h = value; }
        }
        public string _wavedirection10h;
        /// <summary>
        /// 1天10时浪向
        /// </summary>
        public string WAVEDIRECTION10H
        {
            get { return _wavedirection10h; }
            set { _wavedirection10h = value; }
        }
        public string _wavedirection11h;
        /// <summary>
        /// 1天1时浪向
        /// </summary>
        public string WAVEDIRECTION11H
        {
            get { return _wavedirection11h; }
            set { _wavedirection11h = value; }
        }
        public string _wavedirection12h;
        /// <summary>
        /// 1天12时浪向
        /// </summary>
        public string WAVEDIRECTION12H
        {
            get { return _wavedirection12h; }
            set { _wavedirection12h = value; }
        }
        public string _wavedirection13h;
        /// <summary>
        /// 1天13时浪向
        /// </summary>
        public string WAVEDIRECTION13H
        {
            get { return _wavedirection13h; }
            set { _wavedirection13h = value; }
        }
        public string _wavedirection14h;
        /// <summary>
        /// 1天14时浪向
        /// </summary>
        public string WAVEDIRECTION14H
        {
            get { return _wavedirection14h; }
            set { _wavedirection14h = value; }
        }
        public string _wavedirection15h;
        /// <summary>
        /// 1天15时浪向
        /// </summary>
        public string WAVEDIRECTION15H
        {
            get { return _wavedirection15h; }
            set { _wavedirection15h = value; }
        }
        public string _wavedirection16h;
        /// <summary>
        /// 1天16时浪向
        /// </summary>
        public string WAVEDIRECTION16H
        {
            get { return _wavedirection16h; }
            set { _wavedirection16h = value; }
        }
        public string _wavedirection17h;
        /// <summary>
        /// 1天17时浪向
        /// </summary>
        public string WAVEDIRECTION17H
        {
            get { return _wavedirection17h; }
            set { _wavedirection17h = value; }

        }
        public string _wavedirection18h;
        /// <summary>
        /// 1天18时浪向
        /// </summary>
        public string WAVEDIRECTION18H
        {
            get { return _wavedirection18h; }
            set { _wavedirection18h = value; }
        }
        public string _wavedirection19h;
        /// <summary>
        /// 1天19时浪向
        /// </summary>
        public string WAVEDIRECTION19H
        {
            get { return _wavedirection19h; }
            set { _wavedirection19h = value; }
        }
        public string _wavedirection20h;
        /// <summary>
        /// 1天02时浪向
        /// </summary>
        public string WAVEDIRECTION20H
        {
            get { return _wavedirection20h; }
            set { _wavedirection20h = value; }
        }
        public string _wavedirection21h;
        /// <summary>
        /// 1天21时浪向
        /// </summary>
        public string WAVEDIRECTION21H
        {
            get { return _wavedirection21h; }
            set { _wavedirection21h = value; }
        }
        public string _wavedirection22h;
        /// <summary>
        /// 1天22时浪向
        /// </summary>
        public string WAVEDIRECTION22H
        {
            get { return _wavedirection22h; }
            set { _wavedirection22h = value; }
        }
        public string _wavedirection23h;
        /// <summary>
        /// 1天23时浪向
        /// </summary>
        public string WAVEDIRECTION23H
        {
            get { return _wavedirection23h; }
            set { _wavedirection23h = value; }
        }
        #endregion
        #region 第二天 三天浪向
        public string _wavedirection24h;
        /// <summary>
        /// 2天1时段浪向
        /// </summary>
        public string WAVEDIRECTION24H
        {
            get { return _wavedirection24h; }
            set { _wavedirection24h = value; }
        }
        public string _wavedirection25h;
        /// <summary>
        /// 2天2时段浪向
        /// </summary>
        public string WAVEDIRECTION25H
        {
            get { return _wavedirection25h; }
            set { _wavedirection25h = value; }
        }
        public string _wavedirection26h;
        /// <summary>
        /// 2天3时段浪向
        /// </summary>
        public string WAVEDIRECTION26H
        {
            get { return _wavedirection26h; }
            set { _wavedirection26h = value; }
        }
        public string _wavedirection27h;
        /// <summary>
        /// 2天4时段浪向
        /// </summary>
        public string WAVEDIRECTION27H
        {
            get { return _wavedirection27h; }
            set { _wavedirection27h = value; }
        }
        public string _wavedirection28h;
        /// <summary>
        /// 3天1时段浪向
        /// </summary>
        public string WAVEDIRECTION28H
        {
            get { return _wavedirection28h; }
            set { _wavedirection28h = value; }
        }
        public string _wavedirection29h;
        /// <summary>
        /// 3天2时段浪向
        /// </summary>
        public string WAVEDIRECTION29H
        {
            get { return _wavedirection29h; }
            set { _wavedirection29h = value; }
        }
        public string _wavedirection30h;
        /// <summary>
        /// 3天3时段浪向
        /// </summary>
        public string WAVEDIRECTION30H
        {
            get { return _wavedirection30h; }
            set { _wavedirection30h = value; }
        }
        public string _wavedirection31h;
        /// <summary>
        /// 3天4时段浪向
        /// </summary>
        public string WAVEDIRECTION31H
        {
            get { return _wavedirection31h; }
            set { _wavedirection31h = value; }
        }
        #endregion
        #endregion
    }
    /// <summary>
    /// 水温
    /// </summary>
    public class SCOTemperatureMode{
        public DateTime _publishdate;
        /// <summary>
        /// 填报日期
        /// </summary>
        public DateTime PUBLISHDATE
        {
            get { return _publishdate; }
            set { _publishdate = value; }
        }
        public string _type;
        /// <summary>
        /// 预报区域
        /// </summary>
        public string TYPE
        {
            get { return _type; }
            set { _type = value; }
        }
        public string _max1;
        public string MAX1
        {
            get { return _max1; }
            set { _max1 = value; }
        }
        public string _max2;
        public string MAX2
        {
            get { return _max2; }
            set { _max2 = value; }
        }
        public string _max3;
        public string MAX3
        {
            get { return _max3; }
            set { _max3 = value; }
        }
        public string _min1;
        public string MIN1
        {
            get { return _min1; }
            set { _min1 = value; }
        }
        public string _min2;
        public string MIN2
        {
            get { return _min2; }
            set { _min2 = value; }
        }
        public string _min3;
        public string MIN3
        {
            get { return _min3; }
            set { _min3 = value; }
        }
        public string _avg1;
        public string AVG1
        {
            get { return _avg1; }
            set { _avg1 = value; }
        }
        public string _avg2;
        public string AVG2
        {
            get { return _avg2; }
            set { _avg2 = value; }
        }
        public string _avg3;
        public string AVG3
        {
            get { return _avg3; }
            set { _avg3 = value; }
        }
    }

    public class LvChaoWindAndWaveModel
    {
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PUBLISHDATE { get; set; }
        /// <summary>
        /// 预报时间
        /// </summary>
        public DateTime FORECASTDATE { get; set; }
        /// <summary>
        /// 预报海区
        /// </summary>
        public string FORECASTAREA { get; set; }
        /// <summary>
        /// 天气
        /// </summary>
        public string WEATHER { get; set; }
        /// <summary>
        /// 风力
        /// </summary>
        public string WINDFORCE { get; set; }
        /// <summary>
        /// 风向
        /// </summary>
        public string WINDDIRECTION { get; set; }
        /// <summary>
        /// 浪高
        /// </summary>
        public string WAVEHIGHT { get; set; }
        /// <summary>
        /// 浪向
        /// </summary>
        public string WAVEDIRECTION { get; set; }
       
        
    }
}