using Newtonsoft.Json;
using PredicTable.WebServiceClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Web.Services;

namespace PredicTable
{
    /// <summary>
    /// WebServices 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://123.234.129.234:10001/WebService/WebServices.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebServices : WebService
    {

        [WebMethod]
        public string GetTableData(DateTime date)
        {
            ModelAmShortResponse result = new ModelAmShortResponse();
            ModelAmShortFakeData amshortfakedata = new ModelAmShortFakeData();
            result.AmShort1Data = getAmShort1(date, out amshortfakedata.AmShort1FakeData);
            result.AmShort2Data = getAmShort2(date, out amshortfakedata.AmShort2FakeData);
            result.AmShort3and4Data = getAmShort3and4(date);
            result.AmShort5Data = getAmShort5(date, out amshortfakedata.AmShort5FakeData);
            result.AmShort6Data = getAmShort6(date, out amshortfakedata.AmShort6FakeData);
            result.AmShort7Data = getAmShort7(date, out amshortfakedata.AmShort7FakeData);
            result.AmShort8Data = getAmShort8(date, out amshortfakedata.AmShort8FakeData);
            result.AmShort9Data = getAmShort9(date, out amshortfakedata.AmShort9FakeData);
            result.AmShort10Data = getAmShort10(date, out amshortfakedata.AmShort10FakeData);
            result.AmShort11Data = getAmShort11(date, out amshortfakedata.AmShort11FakeData);
            result.AmShort12Data = getAmShort12(date, out amshortfakedata.AmShort12FakeData);
            result.PublishMetaInfo = getPublishMetaInfo(date, out amshortfakedata.PublishMetaInfoFakeData);
            result.AmShortFakeData = amshortfakedata;

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public string SetAmShortTableData(int tablenumber, string usertype, string datajson)
        {
            System.Diagnostics.Debug.WriteLine("SetAmShortTableData()");
            System.Diagnostics.Debug.WriteLine("tablenumber: " + tablenumber);
            System.Diagnostics.Debug.WriteLine("usertype: " + usertype);
            System.Diagnostics.Debug.WriteLine("datajson: " + datajson);
            string result = "";
            switch (tablenumber)
            {
                case 0:
                    result = setPublishMetaInfo(usertype, datajson);
                    break;
                case 1:
                    result = setAmShort1(usertype, datajson);
                    break;
                case 2:
                    result = setAmShort2(usertype, datajson);
                    break;
                case 3:
                case 4:
                    break;
                case 5:
                    result = setAmShort5(usertype, datajson);
                    break;
                case 6:
                    result = setAmShort6(usertype, datajson);
                    break;
                case 7:
                    result = setAmShort7(usertype, datajson);
                    break;
                case 8:
                    result = setAmShort8(usertype, datajson);
                    break;
                case 9:
                    result = setAmShort9(usertype, datajson);
                    break;
                case 10:
                    result = setAmShort10(usertype, datajson);
                    break;
                case 11:
                    result = setAmShort11(usertype, datajson);
                    break;
                case 12:
                    result = setAmShort12(usertype, datajson);
                    break;
                default: break;
            }
            return result;
        }

        [WebMethod]
        public string DevTest()
        {
            string sqlselect = "select * from Tblyrbhwindwave72hforecasttwo "
                        + " where FORECASTDATE > to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and FORECASTDATE < to_date('" + DateTime.Now.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and PUBLISHDATE=to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dtselect = queryData(sqlselect);

            return DataTableToJson(dtselect);
        }

        [WebMethod]
        public string CloseWord()
        {
            int count = 0;
            try
            {
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process p in processes)
                {
                    if (p.ProcessName == "WINWORD")
                    {
                        count++;
                        p.Kill();
                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return count.ToString();
        }

        #region 上午短期预报 查询方法
        // 上午一、72小时渤海海区及黄河海港风、浪预报
        private List<ModelAmShort1> getAmShort1(DateTime date, out bool fakedata)
        {
            List<ModelAmShort1> result = new List<ModelAmShort1>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(1), "渤海"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(2), "渤海"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(3), "渤海"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(1), "黄河海港"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(2), "黄河海港"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(3), "黄河海港"));
            string sql = "select * from Tblyrbhwindwave72hforecasttwo where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count > 0)
            {
                List<ModelAmShort1> sqldatalist = TableToList<ModelAmShort1>(dt);
                for (int i = 0; i < result.Count; i++)
                {
                    foreach (ModelAmShort1 sqldata in sqldatalist)
                    {
                        if (sqldata.REPORTAREA == result[i].REPORTAREA & sqldata.FORECASTDATE.ToString("yyyy/MM/dd") == result[i].FORECASTDATE.ToString("yyyy/MM/dd"))
                        {
                            result[i] = sqldata;
                        }
                    }
                }
                fake = false;
            }
            //List<ModelAmShort1> tablelist = TableToList<ModelAmShort1>(dt);
            //if (tablelist.Count == 6)
            //{
            //    result = tablelist;
            //}
            bool temperatureEmpty = true;
            bool wavewindEmpty = true;
            foreach (ModelAmShort1 info in result)
            {
                if (info.YRBHWWFWATERTEMPERATURE != "")
                {
                    temperatureEmpty = false;
                }
                if (info.YRBHWWFFLOWDIR != "" & info.YRBHWWFFLOWLEVEL != "" & info.YRBHWWFWAVEDIR != "" & info.YRBHWWFWAVEHEIGHT != "")
                {
                    wavewindEmpty = false;
                }
            }
            if (temperatureEmpty)
            {
                string sqltemperature = "select * from TBLWATERTEMPERATURE where PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND NAME IN('黄河海港')";
                DataTable dttemperature = queryData(sqltemperature);

                if (dttemperature.Rows.Count > 0)
                {
                    foreach (ModelAmShort1 info in result)
                    {
                        if (info.REPORTAREA == "黄河海港")
                        {
                            DateTime forecastDate = Convert.ToDateTime(info.FORECASTDATE);
                            if (forecastDate == pubdate.AddDays(1))
                            {
                                info.YRBHWWFWATERTEMPERATURE = dttemperature.Rows[0]["MEAN_24H"].ToString();
                            }
                            else if (forecastDate == pubdate.AddDays(2))
                            {
                                info.YRBHWWFWATERTEMPERATURE = dttemperature.Rows[0]["MEAN_48H"].ToString();
                            }
                            else if (forecastDate == pubdate.AddDays(3))
                            {
                                info.YRBHWWFWATERTEMPERATURE = dttemperature.Rows[0]["MEAN_72H"].ToString();
                            }
                        }
                    }
                    fake = true;
                }
            }
            if (wavewindEmpty)
            {
                string sqlwavewind = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND  FORECASTAREA in ('渤海','黄河海港')";
                DataTable dtwavewind = queryData(sqlwavewind);

                if (dtwavewind.Rows.Count > 0)
                {
                    foreach (ModelAmShort1 info in result)
                    {
                        for (int i = 0; i < dtwavewind.Rows.Count; i++)
                        {
                            if (info.REPORTAREA == dtwavewind.Rows[i]["FORECASTAREA"].ToString())
                            {
                                DateTime forecastDate = Convert.ToDateTime(info.FORECASTDATE);
                                if (forecastDate == pubdate.AddDays(1))
                                {
                                    info.YRBHWWFWAVEHEIGHT = dtwavewind.Rows[i]["WAVE24FORECAST"].ToString();
                                    info.YRBHWWFWAVEDIR = dtwavewind.Rows[i]["WINDDIRECTION24FORECAST"].ToString();
                                    info.YRBHWWFFLOWLEVEL = dtwavewind.Rows[i]["WINDFORCE24FORECAST"].ToString();
                                    info.YRBHWWFFLOWDIR = dtwavewind.Rows[i]["WINDDIRECTION24FORECAST"].ToString();
                                }
                                else if (forecastDate == pubdate.AddDays(2))
                                {
                                    info.YRBHWWFWAVEHEIGHT = dtwavewind.Rows[i]["WAVE48FORECAST"].ToString();
                                    info.YRBHWWFWAVEDIR = dtwavewind.Rows[i]["WINDDIRECTION48FORECAST"].ToString();
                                    info.YRBHWWFFLOWLEVEL = dtwavewind.Rows[i]["WINDFORCE48FORECAST"].ToString();
                                    info.YRBHWWFFLOWDIR = dtwavewind.Rows[i]["WINDDIRECTION48FORECAST"].ToString();
                                }
                                else if (forecastDate == pubdate.AddDays(3))
                                {
                                    info.YRBHWWFWAVEHEIGHT = dtwavewind.Rows[i]["WAVE72FORECAST"].ToString();
                                    info.YRBHWWFWAVEDIR = dtwavewind.Rows[i]["WINDDIRECTION72FORECAST"].ToString();
                                    info.YRBHWWFFLOWLEVEL = dtwavewind.Rows[i]["WINDFORCE72FORECAST"].ToString();
                                    info.YRBHWWFFLOWDIR = dtwavewind.Rows[i]["WINDDIRECTION72FORECAST"].ToString();
                                }
                            }
                        }
                    }
                    fake = true;
                }
            }
            result.Sort(new ModelAmShort1Comparer());
            fakedata = fake;
            return result;
        }

        // 上午二、72小时港口潮位预报
        private List<ModelAmShort2> getAmShort2(DateTime date, out bool fakedata)
        {
            List<ModelAmShort2> result = new List<ModelAmShort2>();
            bool fake = false;
            DateTime publishdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(1), "龙口港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(2), "龙口港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(3), "龙口港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(1), "黄河海港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(2), "黄河海港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(3), "黄河海港"));
            int week = (int)date.DayOfWeek;
            if (week == 2)
            {
                // 周二
                DateTime pubdate = date.AddDays(-1);
                string sql = "select * from TBLHARBOURTIDELEVEL7DAY where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') "
                    + "and forecastdate >to_date('" + pubdate.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                    + "and forecastdate< to_date('" + pubdate.AddDays(5).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by htlharbour desc, forecastdate asc";
                DataTable dt = queryData(sql);
                if (dt.Rows.Count > 0)
                {
                    // result = TableToList<ModelAmShort2>(dt);
                    List<ModelAmShort2> sqldatalist = TableToList<ModelAmShort2>(dt);
                    foreach (ModelAmShort2 sqldata in sqldatalist)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            if (sqldata.HTLHARBOUR == result[i].HTLHARBOUR & sqldata.FORECASTDATE.ToString("yyyy/MM/dd") == result[i].FORECASTDATE.ToString("yyyy/MM/dd"))
                            {
                                result[i] = sqldata;
                            }
                        }
                    }
                }
            }
            else
            {
                // 不是周二
                string sql = "select * from TBLHARBOURTIDELEVEL where PUBLISHDATE=to_date('" + publishdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                DataTable dt = queryData(sql);
                if (dt.Rows.Count < 1)
                {
                    sql = "select * from TBLHARBOURTIDELEVEL "
                        + " where FORECASTDATE > to_date('" + publishdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and FORECASTDATE < to_date('" + publishdate.AddDays(3).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and PUBLISHDATE=to_date('" + publishdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    dt = queryData(sql);
                    fake = true;
                }
                if (dt.Rows.Count > 0)
                {
                    // result = TableToList<ModelAmShort2>(dt);
                    List<ModelAmShort2> sqldatalist = TableToList<ModelAmShort2>(dt);
                    foreach (ModelAmShort2 sqldata in sqldatalist)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            if (sqldata.HTLHARBOUR == result[i].HTLHARBOUR & sqldata.FORECASTDATE.ToString("yyyy/MM/dd") == result[i].FORECASTDATE.ToString("yyyy/MM/dd"))
                            {
                                result[i] = sqldata;
                            }
                        }
                    }
                }
            }
            // 用天文潮数据补齐天数
            if (fake)
            {
                string sqlastro = "select * from HT_YB_TIDE where STATION in ('113lko', '119hhg') and PREDICTIONDATE between to_date('"
                    + publishdate.AddDays(1).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') and to_date('"
                    + publishdate.AddDays(3).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') ORDER BY STATION,PREDICTIONDATE";
                DataTable dtastro = queryData(sqlastro);
                if (dtastro.Rows.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (result[i].HTLFIRSTTIMELOWTIDE == ""
                            & result[i].HTLFIRSTWAVEOFTIME == ""
                            & result[i].HTLFIRSTWAVETIDELEVEL == ""
                            & result[i].HTLLOWTIDELEVELFORTHEFIRSTTIME == ""
                            & result[i].HTLLOWTIDELEVELFORTHESECONDTIM == ""
                            & result[i].HTLSECONDTIMELOWTIDE == ""
                            & result[i].HTLSECONDWAVEOFTIME == ""
                            & result[i].HTLSECONDWAVETIDELEVEL == "")
                        {
                            foreach (DataRow astrodata in dtastro.Rows)
                            {
                                if (result[i].FORECASTDATE.ToString("yyyy/MM/dd") == (Convert.ToDateTime(astrodata["PREDICTIONDATE"].ToString())).ToString("yyyy/MM/dd"))
                                {
                                    switch (astrodata["STATION"].ToString())
                                    {
                                        case "113lko":
                                            if (result[i].HTLHARBOUR == "龙口港")
                                            {
                                                fillModelAmShort2List(result, i, astrodata);
                                            }
                                            break;
                                        case "119hhg":
                                            if (result[i].HTLHARBOUR == "黄河海港")
                                            {
                                                fillModelAmShort2List(result, i, astrodata);
                                            }
                                            break;
                                        default: break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            result.Sort(new ModelAmShort2Comparer());
            fakedata = fake;
            return result;
        }

        // 上午三、3天海洋水文气象预报综述
        // 上午四、24小时水文气象预报综述
        private List<ModelAmShort3and4> getAmShort3and4(DateTime date)
        {
            List<ModelAmShort3and4> result = new List<ModelAmShort3and4>();
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort3and4(pubdate));
            string sql = "SELECT * FROM HT_ZSVIEW WHERE PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = TableToList<ModelAmShort3and4>(dt);
            }
            return result;
        }

        // 上午五、预计未来24小时海浪、水温预报
        private List<ModelAmShort5> getAmShort5(DateTime date, out bool fakedata)
        {
            List<ModelAmShort5> result = new List<ModelAmShort5>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort5(pubdate));
            string sql = "select * from TBLEXPECTEDFUTURE24HWAVEWATER where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = TableToList<ModelAmShort5>(dt);
            }
            bool temperatureEmpty = false;
            bool waveEmpty = false;
            if (result[0].EFWWDKSEAAREAWATERTEMPE == ""
                & result[0].EFWWHHKSEAAREAWATERTEMP == ""
                & result[0].EFWWGLGSEAAREAWATERTEMP == ""
                & result[0].EFWWDYGWATERTEMPERATURE == ""
                & result[0].EFWWXHWATERTEMPERATURE == ""
                & result[0].EFWWCKWATERTEMPERATURE == "")
            {
                temperatureEmpty = true;
            }
            if (result[0].EFWWBHLOWESTWAVE == ""
                & result[0].EFWWBHHIGHESTWAVE == ""
                & result[0].EFWWBHWAVETYPE == ""
                & result[0].EFWWBHNORTHLOWESTWAVE == ""
                & result[0].EFWWBHNORTHHIGHESTWAVE == ""
                & result[0].EFWWBHNORTHWAVETYPE == ""
                & result[0].EFWWDKSEAAREAWAVEHEIGHT == ""
                & result[0].EFWWHHKSEAAREAWAVEHEIGHT == ""
                & result[0].EFWWGLGSEAAREAWAVEHEIGHT == ""
                & result[0].EFWWDYGWAVEHEIGHT == ""
                & result[0].EFWWXHWAVEHEIGHT == ""
                & result[0].EFWWCKWAVEHEIGHT == "")
            {
                waveEmpty = true;
            }
            if (temperatureEmpty)
            {
                string sqltemperature = "select * from  TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND  NAME IN ('刁口', '黄河口', '东营广利一级渔港', '东营', '新户', '埕口海洋站')";
                DataTable dttemperature = queryData(sqltemperature);
                if (dttemperature.Rows.Count > 0)
                {
                    for (int i = 0; i < dttemperature.Rows.Count; i++)
                    {
                        string location = dttemperature.Rows[i]["NAME"].ToString();
                        switch (location)
                        {
                            case "刁口":
                                result[0].EFWWDKSEAAREAWATERTEMPE = dttemperature.Rows[i]["MEAN_24H"].ToString();
                                break;
                            case "黄河口":
                                result[0].EFWWHHKSEAAREAWATERTEMP = dttemperature.Rows[i]["MEAN_24H"].ToString();
                                break;
                            case "东营广利一级渔港":
                                result[0].EFWWGLGSEAAREAWATERTEMP = dttemperature.Rows[i]["MEAN_24H"].ToString();
                                break;
                            case "东营":
                                result[0].EFWWDYGWATERTEMPERATURE = dttemperature.Rows[i]["MEAN_24H"].ToString();
                                break;
                            case "新户":
                                result[0].EFWWXHWATERTEMPERATURE = dttemperature.Rows[i]["MEAN_24H"].ToString();
                                break;
                            case "埕口海洋站":
                                result[0].EFWWCKWATERTEMPERATURE = dttemperature.Rows[i]["MEAN_24H"].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                }
                fake = true;
            }
            if (waveEmpty)
            {
                string sqlwave = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND  FORECASTAREA in ('渤海', '黄海北部', '刁口近海', '黄河口海域', '广利港海域', '东营港海域', '新户近海', '埕岛近海')";
                DataTable dtwave = queryData(sqlwave);
                if (dtwave.Rows.Count > 0)
                {
                    for (int i = 0; i < dtwave.Rows.Count; i++)
                    {
                        string location = dtwave.Rows[i]["FORECASTAREA"].ToString();
                        switch (location)
                        {
                            case "渤海":
                                result[0].EFWWBHLOWESTWAVE = dtwave.Rows[i]["WAVE24FORECAST"].ToString();
                                break;
                            case "黄海北部":
                                result[0].EFWWBHNORTHLOWESTWAVE = dtwave.Rows[i]["WAVE24FORECAST"].ToString();
                                break;
                            case "刁口近海":
                                result[0].EFWWDKSEAAREAWAVEHEIGHT = dtwave.Rows[i]["WAVE24FORECAST"].ToString();
                                break;
                            case "黄河口海域":
                                result[0].EFWWHHKSEAAREAWAVEHEIGHT = dtwave.Rows[i]["WAVE24FORECAST"].ToString();
                                break;
                            case "广利港海域":
                                result[0].EFWWGLGSEAAREAWAVEHEIGHT = dtwave.Rows[i]["WAVE24FORECAST"].ToString();
                                break;
                            case "东营港海域":
                                result[0].EFWWDYGWAVEHEIGHT = dtwave.Rows[i]["WAVE24FORECAST"].ToString();
                                break;
                            case "新户近海":
                                result[0].EFWWXHWAVEHEIGHT = dtwave.Rows[i]["WAVE24FORECAST"].ToString();
                                break;
                            case "埕岛近海":
                                result[0].EFWWCKWAVEHEIGHT = dtwave.Rows[i]["WAVE24FORECAST"].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    fake = true;
                }
            }
            fakedata = fake;
            return result;
        }

        // 上午六、24小时潮位预报
        private List<ModelAmShort6> getAmShort6(DateTime date, out bool fakedata)
        {
            List<ModelAmShort6> result = new List<ModelAmShort6>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "小岛河"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "孤东"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "东营港"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "桩西"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "飞雁滩"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "新户"));
            string sql = "select * from TBL24HTIDELEVEL where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count > 0)
            {
                List<ModelAmShort6> sqldatalist = TableToList<ModelAmShort6>(dt);
                for (int i = 0; i < result.Count; i++)
                {
                    foreach (ModelAmShort6 sqldata in sqldatalist)
                    {
                        if (result[i].TLFORECASTSTANCE == sqldata.TLFORECASTSTANCE & result[i].FORECASTDATE.ToString("yyyy/MM/dd") == sqldata.FORECASTDATE.ToString("yyyy/MM/dd"))
                        {
                            result[i] = sqldata;
                        }
                    }
                }
                // result = TableToList<ModelAmShort6>(dt);
            }
            else
            {
                // 用天文潮数据补齐
                string sqlastro = "select * from HT_YB_TIDE where STATION in ("
                    + "'117xdh','118gud','119hhg','122fyt','121zx','123xhu'" + ") and PREDICTIONDATE = to_date('"
                    + pubdate.AddDays(1).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') ORDER BY STATION,PREDICTIONDATE";
                DataTable dtastro = queryData(sqlastro);
                if (dtastro.Rows.Count > 0)
                {
                    foreach (DataRow astrodata in dtastro.Rows)
                    {
                        switch (astrodata["STATION"].ToString())
                        {
                            case "117xdh":
                                fillModelAmShort6List(result, 0, astrodata);
                                break;
                            case "118gud":
                                fillModelAmShort6List(result, 1, astrodata);
                                break;
                            case "119hhg":
                                fillModelAmShort6List(result, 2, astrodata);
                                break;
                            case "121zx":
                                fillModelAmShort6List(result, 3, astrodata);
                                break;
                            case "122fyt":
                                fillModelAmShort6List(result, 4, astrodata);
                                break;
                            case "123xhu":
                                fillModelAmShort6List(result, 5, astrodata);
                                break;
                            default: break;
                        }
                    }
                    fake = true;
                }
            }
            result.Sort(new ModelAmShort6Comparer());
            fakedata = fake;
            return result;
        }

        // 上午七、海上丝绸之路三天海浪、气象预报
        private List<ModelAmShort7> getAmShort7(DateTime date, out bool fakedata)
        {
            List<ModelAmShort7> result = new List<ModelAmShort7>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(1), "青岛港"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(2), "青岛港"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(3), "青岛港"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(1), "潍坊港"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(2), "潍坊港"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(3), "潍坊港"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(1), "营口港"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(2), "营口港"));
            result.Add(new ModelAmShort7(pubdate, pubdate.AddDays(3), "营口港"));

            string sql = "SELECT * FROM HT_SILKWINDWAVE "
                        + " WHERE PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND "
                        + " FORECASTDATE BETWEEN to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " AND  to_date('" + pubdate.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count == 0)
            {
                sql = "SELECT * FROM HT_SILKWINDWAVE "
                        + " WHERE PUBLISHDATE=to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') ";
                dt = queryData(sql);
                fake = true;
            }
            if (dt.Rows.Count > 0)
            {
                List<ModelAmShort7> sqldatalist = TableToList<ModelAmShort7>(dt);
                for (int i = 0; i < result.Count; i++)
                {
                    foreach (ModelAmShort7 sqldata in sqldatalist)
                    {
                        if (result[i].REPORTAREA == sqldata.REPORTAREA & result[i].FORECASTDATE.ToString("yyyy/MM/dd") == sqldata.FORECASTDATE.ToString("yyyy/MM/dd"))
                        {
                            result[i] = sqldata;
                        }
                    }
                }
            }
            else
            {
                sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND  FORECASTAREA in ('青岛近海', '潍坊近海', '营口港')";
                dt = queryData(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (dt.Rows[i]["FORECASTAREA"].ToString())
                        {
                            case "青岛近海":
                                fillModelAmShort7List(result, 0, dt.Rows[i]);
                                break;
                            case "潍坊近海":
                                fillModelAmShort7List(result, 3, dt.Rows[i]);
                                break;
                            case "营口港":
                                fillModelAmShort7List(result, 6, dt.Rows[i]);
                                break;
                            default: break;
                        }
                    }
                    fake = true;
                }
            }
            fakedata = fake;
            return result;
        }

        // 上午八、海上丝绸之路三天潮汐预报
        private List<ModelAmShort8> getAmShort8(DateTime date, out bool fakedata)
        {
            List<ModelAmShort8> result = new List<ModelAmShort8>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort8(date, date.AddDays(1), "青岛港"));
            result.Add(new ModelAmShort8(date, date.AddDays(2), "青岛港"));
            result.Add(new ModelAmShort8(date, date.AddDays(3), "青岛港"));
            result.Add(new ModelAmShort8(date, date.AddDays(1), "潍坊港"));
            result.Add(new ModelAmShort8(date, date.AddDays(2), "潍坊港"));
            result.Add(new ModelAmShort8(date, date.AddDays(3), "潍坊港"));
            result.Add(new ModelAmShort8(date, date.AddDays(1), "营口港"));
            result.Add(new ModelAmShort8(date, date.AddDays(2), "营口港"));
            result.Add(new ModelAmShort8(date, date.AddDays(3), "营口港"));
            int week = (int)date.DayOfWeek;
            string sql = "";
            DataTable dt = new DataTable();
            if (week == 2)
            {
                // 周二
                sql = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') " +
                    "and  forecastdate>to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by HTLHARBOUR,forecastdate asc";
                dt = queryData(sql);
            }
            else
            {
                // 不是周二
                sql = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by HTLHARBOUR,forecastdate asc";
                dt = queryData(sql);
                if (dt.Rows.Count == 0)
                {
                    sql = "SELECT * FROM HT_SILKTIDE "
                        + " WHERE PUBLISHDATE=to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') ";
                    dt = queryData(sql);
                    fake = true;
                }
            }
            if (dt.Rows.Count > 0)
            {
                List<ModelAmShort8> sqldatalist = TableToList<ModelAmShort8>(dt);
                for (int i = 0; i < result.Count; i++)
                {
                    foreach (ModelAmShort8 sqldata in sqldatalist)
                    {
                        if (result[i].HTLHARBOUR == sqldata.HTLHARBOUR & result[i].FORECASTDATE.ToString("yyyy/MM/dd") == sqldata.FORECASTDATE.ToString("yyyy/MM/dd"))
                        {
                            result[i] = sqldata;
                        }
                    }
                }
            }
            else if (dt.Rows.Count == 0 | fake)
            {
                // 用天文潮数据补齐
                string sqlastro = "select * from HT_YB_TIDE where STATION in ("
                    + "'101wmt','114wfg','133byq'" + ") and PREDICTIONDATE between to_date('"
                    + pubdate.AddDays(1).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') and to_date('"
                    + pubdate.AddDays(3).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ORDER BY STATION,PREDICTIONDATE";
                DataTable dtastro = queryData(sqlastro);
                if (dtastro.Rows.Count > 0)
                {
                    foreach (DataRow astrodata in dtastro.Rows)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            switch (astrodata["STATION"].ToString())
                            {
                                case "101wmt":
                                    if (result[i].HTLHARBOUR == "青岛港" & result[i].FORECASTDATE.ToString("yyyy/MM/dd") == Convert.ToDateTime(astrodata["PREDICTIONDATE"].ToString()).ToString("yyyy/MM/dd"))
                                    {
                                        fillModelAmShort8List(result, i, astrodata);
                                    }
                                    break;
                                case "114wfg":
                                    if (result[i].HTLHARBOUR == "潍坊港" & result[i].FORECASTDATE.ToString("yyyy/MM/dd") == Convert.ToDateTime(astrodata["PREDICTIONDATE"].ToString()).ToString("yyyy/MM/dd"))
                                    {
                                        fillModelAmShort8List(result, i, astrodata);
                                    }
                                    break;
                                case "133byq":
                                    if (result[i].HTLHARBOUR == "营口港" & result[i].FORECASTDATE.ToString("yyyy/MM/dd") == Convert.ToDateTime(astrodata["PREDICTIONDATE"].ToString()).ToString("yyyy/MM/dd"))
                                    {
                                        fillModelAmShort8List(result, i, astrodata);
                                    }
                                    break;
                                default: break;
                            }
                        }
                    }
                    fake = true;
                }
            }
            fakedata = fake;
            return result;
        }

        // 上午九、海区24小时海浪、水温预报
        private List<ModelAmShort9> getAmShort9(DateTime date, out bool fakedata)
        {
            List<ModelAmShort9> result = new List<ModelAmShort9>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort9(pubdate));
            string sql = "select * from HT_TBLWF24HWAVEFORECAST where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = TableToList<ModelAmShort9>(dt);
            }
            if (result[0].SA24HWFOFFSHORESW.Trim() == "")
            {
                string sqltemperature = "select * from  TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND  NAME IN ('潍坊')";
                DataTable dttemperature = queryData(sqltemperature);
                if (dttemperature.Rows.Count > 0)
                {
                    result[0].SA24HWFOFFSHORESW = dttemperature.Rows[0]["MEAN_24H"].ToString();
                    fake = true;
                }
            }
            if (result[0].SA24HWFBOHAIWAVEHEIGHT == ""
                & result[0].SA24HWFMIDDLEOFYSWAVEHEIGHT == ""
                & result[0].SA24HWFNORTHOFYSWAVEHEIGHT == ""
                & result[0].SA24HWFOFFSHOREWAVEHEIGHT == ""
                & result[0].SA24HWFSOUTHOFYSWAVEHEIGHT == "")
            {
                string sqlwave = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND  FORECASTAREA in ('渤海', '黄海北部', '黄海中部', '黄海南部', '潍坊近海')";
                DataTable dtwave = queryData(sqlwave);
                if (dtwave.Rows.Count > 0)
                {
                    foreach (DataRow row in dtwave.Rows)
                    {
                        switch (row["FORECASTAREA"].ToString())
                        {
                            case "渤海":
                                result[0].SA24HWFBOHAIWAVEHEIGHT = row["WAVE24FORECAST"].ToString();
                                break;
                            case "黄海北部":
                                result[0].SA24HWFNORTHOFYSWAVEHEIGHT = row["WAVE24FORECAST"].ToString();
                                break;
                            case "黄海中部":
                                result[0].SA24HWFMIDDLEOFYSWAVEHEIGHT = row["WAVE24FORECAST"].ToString();
                                break;
                            case "黄海南部":
                                result[0].SA24HWFSOUTHOFYSWAVEHEIGHT = row["WAVE24FORECAST"].ToString();
                                break;
                            case "潍坊近海":
                                result[0].SA24HWFOFFSHOREWAVEHEIGHT = row["WAVE24FORECAST"].ToString();
                                break;
                            default: break;
                        }
                    }
                    fake = true;
                }
            }
            fakedata = fake;
            return result;
        }

        // 上午十、海阳海浪、水温预报
        private List<ModelAmShort10> getAmShort10(DateTime date, out bool fakedata)
        {
            List<ModelAmShort10> result = new List<ModelAmShort10>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort10(pubdate, pubdate.AddDays(1)));
            string sql = "SELECT * FROM TBLYTWAVE WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = TableToList<ModelAmShort10>(dt);
            }
            if (result[0].WATERTEMPERATURE.Trim() == "")
            {
                string sqltemperature = "select * from  TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND  NAME IN ('南黄岛')";
                DataTable dttemperature = queryData(sqltemperature);
                if (dttemperature.Rows.Count > 0)
                {
                    result[0].WATERTEMPERATURE = dttemperature.Rows[0]["MEAN_24H"].ToString();
                    fake = true;
                }
            }
            if (result[0].WAVELEVELONE.Trim() == "" & result[0].WAVEDIRECTION.Trim() == "")
            {
                string sqlwavewind = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND FORECASTAREA = '乳山近海'";
                DataTable dtwavewind = queryData(sqlwavewind);
                if (dtwavewind.Rows.Count > 0)
                {
                    result[0].WAVELEVELONE = dtwavewind.Rows[0]["WAVE24FORECAST"].ToString();
                    result[0].WAVEDIRECTION = dtwavewind.Rows[0]["WINDDIRECTION24FORECAST"].ToString();
                    fake = true;
                }
            }
            fakedata = fake;
            return result;
        }

        // 上午十一、海阳近岸海域潮汐预报
        private List<ModelAmShort11> getAmShort11(DateTime date, out bool fakedata)
        {
            List<ModelAmShort11> result = new List<ModelAmShort11>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort11(pubdate, pubdate.AddDays(1)));
            string sql = "SELECT * FROM TBLYTTIDE WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = TableToList<ModelAmShort11>(dt);
            }
            else
            {
                // 用天文潮数据补齐
                string sqlastro = "select * from HT_YB_TIDE where STATION in ("
                    + "'105nhd'" + ") and PREDICTIONDATE = to_date('"
                    + pubdate.AddDays(1).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ORDER BY STATION,PREDICTIONDATE";
                DataTable dtastro = queryData(sqlastro);
                if (dtastro.Rows.Count > 0)
                {
                    result[0].FIRSTHIGHTIME = dtastro.Rows[0]["FSTHIGHWIDETIME"].ToString().Replace(":", "");
                    result[0].FIRSTHIGHLEVEL = dtastro.Rows[0]["FSTHIGHWIDEHEIGHT"].ToString().Replace(":", "");
                    result[0].FIRSTLOWTIME = dtastro.Rows[0]["FSTLOWWIDETIME"].ToString().Replace(":", "");
                    result[0].FIRSTLOWLEVEL = dtastro.Rows[0]["FSTLOWWIDEHEIGHT"].ToString().Replace(":", "");
                    result[0].SECONDHIGHTIME = dtastro.Rows[0]["SCDHIGHWIDETIME"].ToString().Replace(":", "");
                    result[0].SECONDHIGHLEVEL = dtastro.Rows[0]["SCDHIGHWIDEHEIGHT"].ToString().Replace(":", "");
                    result[0].SECONDLOWTIME = dtastro.Rows[0]["SCDLOWWIDETIME"].ToString().Replace(":", "");
                    result[0].SECONDLOWLEVEL = dtastro.Rows[0]["SCDLOWWIDEHEIGHT"].ToString().Replace(":", "");
                    fake = true;
                }
            }
            fakedata = fake;
            return result;
        }

        // 上午十二、海阳万米海滩海水浴场风、浪预报
        private List<ModelAmShort12> getAmShort12(DateTime date, out bool fakedata)
        {
            List<ModelAmShort12> result = new List<ModelAmShort12>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort12(pubdate, pubdate.AddDays(1)));
            string sql = "SELECT * FROM TBLYTYC WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = TableToList<ModelAmShort12>(dt);
            }
            if (result[0].WAVEHEIGHT.Trim() == ""
                & result[0].WINDDIRECTION.Trim() == ""
                & result[0].WINDSPEED.Trim() == "")
            {
                string sqlwindwave = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND FORECASTAREA = '乳山近海'";
                DataTable dtwindwave = queryData(sqlwindwave);
                if (dtwindwave.Rows.Count > 0)
                {
                    result[0].WAVEHEIGHT = dtwindwave.Rows[0]["WAVE24FORECAST"].ToString();
                    result[0].WINDDIRECTION = dtwindwave.Rows[0]["WINDDIRECTION24FORECAST"].ToString();
                    result[0].WINDSPEED = dtwindwave.Rows[0]["WINDFORCE24FORECAST"].ToString();
                    fake = true;
                }
            }
            if (result[0].TEMPERATURE.Trim() == "")
            {
                string sqltemperature = "SELECT * FROM TBLYTYC WHERE PUBLISHDATE = to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                DataTable dttemperature = queryData(sqltemperature);
                if (dttemperature.Rows.Count > 0)
                {
                    result[0].TEMPERATURE = dttemperature.Rows[0]["TEMPERATURE"].ToString();
                    fake = true;
                }
            }
            fakedata = fake;
            return result;
        }

        // 页脚 填报信息
        private List<ModelPublishMetaInfo> getPublishMetaInfo(DateTime date, out bool fakedata)
        {
            List<ModelPublishMetaInfo> result = new List<ModelPublishMetaInfo>();
            bool fake = false;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelPublishMetaInfo(pubdate));
            string sql = "select * from TBLFOOTER where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = queryData(sql);
            if (dt.Rows.Count == 0)
            {
                sql = "select * from TBLFOOTER where PUBLISHDATE=to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                dt = queryData(sql);
                fake = true;
            }
            if (dt.Rows.Count > 0)
            {
                result = TableToList<ModelPublishMetaInfo>(dt);
            }
            fakedata = fake;
            return result;
        }

        #endregion

        #region 上午短期预报 存储方法
        // 上午一、72小时渤海海区及黄河海港风、浪预报
        private string setAmShort1(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "fl" | type == "sw") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort1> datalist = JsonConvert.DeserializeObject<List<ModelAmShort1>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "select * from Tblyrbhwindwave72hforecasttwo "
                        + " where FORECASTDATE > to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and FORECASTDATE < to_date('" + pubdate.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE TBLYRBHWINDWAVE72HFORECASTTWO set ";
                        switch (type)
                        {
                            case "fl":
                                sql += "YRBHWWFWAVEHEIGHT='@YRBHWWFWAVEHEIGHT', YRBHWWFWAVEDIR='@YRBHWWFWAVEDIR', YRBHWWFFLOWDIR='@YRBHWWFFLOWDIR', YRBHWWFFLOWLEVEL='@YRBHWWFFLOWLEVEL' ";
                                break;
                            case "sw":
                                sql += " YRBHWWFWATERTEMPERATURE='@YRBHWWFWATERTEMPERATURE' ";
                                break;
                            default: break;
                        }
                        sql += "where  PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and REPORTAREA='@REPORTAREA' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "INSERT INTO TBLYRBHWINDWAVE72HFORECASTTWO ";
                        switch (type)
                        {
                            case "fl":
                                sql += "(PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@REPORTAREA', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@YRBHWWFWAVEHEIGHT', '@YRBHWWFWAVEDIR', '@YRBHWWFFLOWDIR', '@YRBHWWFFLOWLEVEL')";
                                break;
                            case "sw":
                                sql += "(PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWATERTEMPERATURE) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@REPORTAREA', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@YRBHWWFWATERTEMPERATURE')";
                                break;
                            default: break;
                        }
                    }
                    foreach (ModelAmShort1 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort1> newdata = getAmShort1(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort1 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午二、72小时港口潮位预报
        private string setAmShort2(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executionCount = 0;
            if (type == "cx" & datajson != "")
            {
                List<ModelAmShort2> datalist = JsonConvert.DeserializeObject<List<ModelAmShort2>>(datajson);
                if (datalist.Count > 0)
                {
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "select * from TBLHARBOURTIDELEVEL where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE TBLHARBOURTIDELEVEL set HTLSECONDTIMELOWTIDE='@HTLSECONDTIMELOWTIDE', HTLLOWTIDELEVELFORTHESECONDTIM='@HTLLOWTIDELEVELFORTHESECONDTIM', HTLFIRSTWAVEOFTIME='@HTLFIRSTWAVEOFTIME', HTLFIRSTWAVETIDELEVEL='@HTLFIRSTWAVETIDELEVEL', HTLFIRSTTIMELOWTIDE='@HTLFIRSTTIMELOWTIDE', HTLLOWTIDELEVELFORTHEFIRSTTIME='@HTLLOWTIDELEVELFORTHEFIRSTTIME', HTLSECONDWAVEOFTIME='@HTLSECONDWAVEOFTIME', HTLSECONDWAVETIDELEVEL='@HTLSECONDWAVETIDELEVEL' where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and HTLHARBOUR='@HTLHARBOUR' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "添加纪录";
                        sql = "INSERT INTO TBLHARBOURTIDELEVEL (PUBLISHDATE, HTLSECONDTIMELOWTIDE, HTLLOWTIDELEVELFORTHESECONDTIM, HTLHARBOUR, FORECASTDATE, HTLFIRSTWAVEOFTIME, HTLFIRSTWAVETIDELEVEL, HTLFIRSTTIMELOWTIDE, HTLLOWTIDELEVELFORTHEFIRSTTIME, HTLSECONDWAVEOFTIME, HTLSECONDWAVETIDELEVEL) VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@HTLSECONDTIMELOWTIDE', '@HTLLOWTIDELEVELFORTHESECONDTIM', '@HTLHARBOUR', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@HTLFIRSTWAVEOFTIME', '@HTLFIRSTWAVETIDELEVEL', '@HTLFIRSTTIMELOWTIDE', '@HTLLOWTIDELEVELFORTHEFIRSTTIME', '@HTLSECONDWAVEOFTIME', '@HTLSECONDWAVETIDELEVEL')";
                    }
                    foreach (ModelAmShort2 model in datalist)
                    {
                        List<DbParameter> parameters = buildParameters(model);
                        executionCount += executeSql(sql, parameters);
                    }
                    if (executionCount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executionCount;
                        bool newfake = false;
                        List<ModelAmShort2> newdata = getAmShort2(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort2 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午三、3天海洋水文气象预报综述
        // 上午四、24小时水文气象预报综述

        // 上午五、预计未来24小时海浪、水温预报
        private string setAmShort5(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "fl" | type == "sw") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort5> datalist = JsonConvert.DeserializeObject<List<ModelAmShort5>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "select * from TBLEXPECTEDFUTURE24HWAVEWATER where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE TBLEXPECTEDFUTURE24HWAVEWATER set	";
                        switch (type)
                        {
                            case "fl":
                                sql += " EFWWHHKSEAAREAWAVEHEIGHT='@EFWWHHKSEAAREAWAVEHEIGHT', EFWWGLGSEAAREAWAVEHEIGHT='@EFWWGLGSEAAREAWAVEHEIGHT', EFWWDYGWAVEHEIGHT='@EFWWDYGWAVEHEIGHT', EFWWXHWAVEHEIGHT='@EFWWXHWAVEHEIGHT', EFWWCKWAVEHEIGHT='@EFWWCKWAVEHEIGHT', EFWWBHLOWESTWAVE='@EFWWBHLOWESTWAVE', EFWWBHHIGHESTWAVE='@EFWWBHHIGHESTWAVE', EFWWBHWAVETYPE='@EFWWBHWAVETYPE', EFWWBHNORTHLOWESTWAVE='@EFWWBHNORTHLOWESTWAVE', EFWWBHNORTHHIGHESTWAVE='@EFWWBHNORTHHIGHESTWAVE', EFWWBHNORTHWAVETYPE='@EFWWBHNORTHWAVETYPE', EFWWDKSEAAREAWAVEHEIGHT='@EFWWDKSEAAREAWAVEHEIGHT' ";
                                break;
                            case "sw":
                                sql += " EFWWHHKSEAAREAWATERTEMP='@EFWWHHKSEAAREAWATERTEMP', EFWWGLGSEAAREAWATERTEMP='@EFWWGLGSEAAREAWATERTEMP', EFWWDYGWATERTEMPERATURE='@EFWWDYGWATERTEMPERATURE', EFWWXHWATERTEMPERATURE='@EFWWXHWATERTEMPERATURE', EFWWCKWATERTEMPERATURE='@EFWWCKWATERTEMPERATURE', EFWWDKSEAAREAWATERTEMPE='@EFWWDKSEAAREAWATERTEMPE' ";
                                break;
                            default: break;
                        }
                        sql += "where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "INSERT INTO  TBLEXPECTEDFUTURE24HWAVEWATER ";
                        switch (type)
                        {
                            case "fl":
                                sql += "(PUBLISHDATE,EFWWHHKSEAAREAWAVEHEIGHT,EFWWGLGSEAAREAWAVEHEIGHT,EFWWDYGWAVEHEIGHT,EFWWXHWAVEHEIGHT,EFWWCKWAVEHEIGHT,EFWWBHLOWESTWAVE,EFWWBHHIGHESTWAVE,EFWWBHWAVETYPE,EFWWBHNORTHLOWESTWAVE,EFWWBHNORTHHIGHESTWAVE,EFWWBHNORTHWAVETYPE,EFWWDKSEAAREAWAVEHEIGHT) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),'@EFWWHHKSEAAREAWAVEHEIGHT','@EFWWGLGSEAAREAWAVEHEIGHT','@EFWWDYGWAVEHEIGHT','@EFWWXHWAVEHEIGHT','@EFWWCKWAVEHEIGHT','@EFWWBHLOWESTWAVE','@EFWWBHHIGHESTWAVE','@EFWWBHWAVETYPE','@EFWWBHNORTHLOWESTWAVE','@EFWWBHNORTHHIGHESTWAVE','@EFWWBHNORTHWAVETYPE','@EFWWDKSEAAREAWAVEHEIGHT')";
                                break;
                            case "sw":
                                sql += "(PUBLISHDATE, EFWWHHKSEAAREAWATERTEMP, EFWWGLGSEAAREAWATERTEMP, EFWWDYGWATERTEMPERATURE, EFWWXHWATERTEMPERATURE, EFWWCKWATERTEMPERATURE, EFWWDKSEAAREAWATERTEMPE) ";
                                sql += "VALUES (to_date('@PUBLISHDATE', 'yyyy-mm-dd hh24@mi@ss'), '@EFWWHHKSEAAREAWATERTEMP', '@EFWWGLGSEAAREAWATERTEMP', '@EFWWDYGWATERTEMPERATURE', '@EFWWXHWATERTEMPERATURE', '@EFWWCKWATERTEMPERATURE', '@EFWWDKSEAAREAWATERTEMPE')";
                                break;
                            default: break;
                        }
                    }
                    foreach (ModelAmShort5 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort5> newdata = getAmShort5(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort5 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午六、24小时潮位预报
        private string setAmShort6(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "cx") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort6> datalist = JsonConvert.DeserializeObject<List<ModelAmShort6>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "select * from TBL24HTIDELEVEL where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE TBL24HTIDELEVEL set TLSECONDTIMELOWTIDE='@TLSECONDTIMELOWTIDE', TLLOWTIDELEVELFORTHESECONDTIME='@TLLOWTIDELEVELFORTHESECONDTIME', TLFIRSTWAVEOFTIME='@TLFIRSTWAVEOFTIME', TLFIRSTWAVETIDELEVEL='@TLFIRSTWAVETIDELEVEL', TLFIRSTTIMELOWTIDE='@TLFIRSTTIMELOWTIDE', TLLOWTIDELEVELFORTHEFIRSTTIME='@TLLOWTIDELEVELFORTHEFIRSTTIME', TLSECONDWAVEOFTIME='@TLSECONDWAVEOFTIME', TLSECONDWAVETIDELEVEL='@TLSECONDWAVETIDELEVEL' where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and TLFORECASTSTANCE='@TLFORECASTSTANCE' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "INSERT INTO TBL24HTIDELEVEL (PUBLISHDATE, TLSECONDTIMELOWTIDE, TLLOWTIDELEVELFORTHESECONDTIME, TLFORECASTSTANCE, FORECASTDATE, TLFIRSTWAVEOFTIME, TLFIRSTWAVETIDELEVEL, TLFIRSTTIMELOWTIDE, TLLOWTIDELEVELFORTHEFIRSTTIME, TLSECONDWAVEOFTIME, TLSECONDWAVETIDELEVEL) VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@TLSECONDTIMELOWTIDE', '@TLLOWTIDELEVELFORTHESECONDTIME', '@TLFORECASTSTANCE', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@TLFIRSTWAVEOFTIME', '@TLFIRSTWAVETIDELEVEL', '@TLFIRSTTIMELOWTIDE', '@TLLOWTIDELEVELFORTHEFIRSTTIME', '@TLSECONDWAVEOFTIME', '@TLSECONDWAVETIDELEVEL')";
                    }
                    foreach (ModelAmShort6 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort6> newdata = getAmShort6(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort6 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午七、海上丝绸之路三天海浪、气象预报
        private string setAmShort7(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "fl") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort7> datalist = JsonConvert.DeserializeObject<List<ModelAmShort7>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "SELECT * FROM HT_SILKWINDWAVE "
                        + " WHERE PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND "
                        + " FORECASTDATE BETWEEN to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " AND  to_date('" + pubdate.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE HT_SILKWINDWAVE set "
                            + "YRBHWWFWAVEHEIGHT='@YRBHWWFWAVEHEIGHT', YRBHWWFWAVEDIR='@YRBHWWFWAVEDIR', YRBHWWFFLOWDIR='@YRBHWWFFLOWDIR', YRBHWWFFLOWLEVEL='@YRBHWWFFLOWLEVEL' "
                            + "where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and REPORTAREA='@REPORTAREA' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "INSERT INTO HT_SILKWINDWAVE (PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL) VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@REPORTAREA', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@YRBHWWFWAVEHEIGHT', '@YRBHWWFWAVEDIR', '@YRBHWWFFLOWDIR', '@YRBHWWFFLOWLEVEL')";
                    }
                    foreach (ModelAmShort7 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort7> newdata = getAmShort7(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort7 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午八、海上丝绸之路三天潮汐预报
        private string setAmShort8(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "cx") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort8> datalist = JsonConvert.DeserializeObject<List<ModelAmShort8>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by HTLHARBOUR,forecastdate asc";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE HT_SILKTIDE set HTLSECONDTIMELOWTIDE='@HTLSECONDTIMELOWTIDE', HTLLOWTIDELEVELFORTHESECONDTIM='@HTLLOWTIDELEVELFORTHESECONDTIM', HTLFIRSTWAVEOFTIME='@HTLFIRSTWAVEOFTIME', HTLFIRSTWAVETIDELEVEL='@HTLFIRSTWAVETIDELEVEL', HTLFIRSTTIMELOWTIDE='@HTLFIRSTTIMELOWTIDE', HTLLOWTIDELEVELFORTHEFIRSTTIME='@HTLLOWTIDELEVELFORTHEFIRSTTIME', HTLSECONDWAVEOFTIME='@HTLSECONDWAVEOFTIME', HTLSECONDWAVETIDELEVEL='@HTLSECONDWAVETIDELEVEL' where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and HTLHARBOUR='@HTLHARBOUR' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "INSERT INTO HT_SILKTIDE (PUBLISHDATE, HTLSECONDTIMELOWTIDE, HTLLOWTIDELEVELFORTHESECONDTIM, HTLHARBOUR, FORECASTDATE, HTLFIRSTWAVEOFTIME, HTLFIRSTWAVETIDELEVEL, HTLFIRSTTIMELOWTIDE, HTLLOWTIDELEVELFORTHEFIRSTTIME, HTLSECONDWAVEOFTIME, HTLSECONDWAVETIDELEVEL) VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@HTLSECONDTIMELOWTIDE', '@HTLLOWTIDELEVELFORTHESECONDTIM', '@HTLHARBOUR', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@HTLFIRSTWAVEOFTIME', '@HTLFIRSTWAVETIDELEVEL', '@HTLFIRSTTIMELOWTIDE', '@HTLLOWTIDELEVELFORTHEFIRSTTIME', '@HTLSECONDWAVEOFTIME', '@HTLSECONDWAVETIDELEVEL')";
                    }
                    foreach (ModelAmShort8 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort8> newdata = getAmShort8(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort8 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午九、海区24小时海浪、水温预报
        private string setAmShort9(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "fl" | type == "sw") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort9> datalist = JsonConvert.DeserializeObject<List<ModelAmShort9>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "select * from HT_TBLWF24HWAVEFORECAST where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "";
                        sql = "UPDATE HT_TBLWF24HWAVEFORECAST set ";
                        switch (type)
                        {
                            case "fl":
                                sql += "SA24HWFBOHAIWAVEHEIGHT='@SA24HWFBOHAIWAVEHEIGHT', SA24HWFNORTHOFYSWAVEHEIGHT='@SA24HWFNORTHOFYSWAVEHEIGHT', SA24HWFMIDDLEOFYSWAVEHEIGHT='@SA24HWFMIDDLEOFYSWAVEHEIGHT', SA24HWFSOUTHOFYSWAVEHEIGHT='@SA24HWFSOUTHOFYSWAVEHEIGHT', SA24HWFOFFSHOREWAVEHEIGHT='@SA24HWFOFFSHOREWAVEHEIGHT' ";
                                break;
                            case "sw":
                                sql += "SA24HWFOFFSHORESW='@SA24HWFOFFSHORESW' ";
                                break;
                            default: break;
                        }
                        sql += "where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "";
                        sql = "INSERT INTO HT_TBLWF24HWAVEFORECAST ";
                        switch (type)
                        {
                            case "fl":
                                sql += "(PUBLISHDATE, SA24HWFBOHAIWAVEHEIGHT, SA24HWFNORTHOFYSWAVEHEIGHT, SA24HWFMIDDLEOFYSWAVEHEIGHT, SA24HWFSOUTHOFYSWAVEHEIGHT, SA24HWFOFFSHOREWAVEHEIGHT) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@SA24HWFBOHAIWAVEHEIGHT', '@SA24HWFNORTHOFYSWAVEHEIGHT', '@SA24HWFMIDDLEOFYSWAVEHEIGHT', '@SA24HWFSOUTHOFYSWAVEHEIGHT', '@SA24HWFOFFSHOREWAVEHEIGHT')";
                                break;
                            case "sw":
                                sql += "(PUBLISHDATE, SA24HWFOFFSHORESW) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@SA24HWFOFFSHORESW')";
                                break;
                            default: break;
                        }
                    }
                    foreach (ModelAmShort9 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort9> newdata = getAmShort9(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort9 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午十、海阳海浪、水温预报
        private string setAmShort10(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "fl" | type == "sw") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort10> datalist = JsonConvert.DeserializeObject<List<ModelAmShort10>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "SELECT * FROM TBLYTWAVE WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE TBLYTWAVE set ";
                        switch (type)
                        {
                            case "fl":
                                sql += "WAVELEVELONE='@WAVELEVELONE', WAVELEVELTYPE='@WAVELEVELTYPE', WAVEDIRECTION='@WAVEDIRECTION' ";
                                break;
                            case "sw":
                                sql += "WATERTEMPERATURE='@WATERTEMPERATURE' ";
                                break;
                            default: break;
                        }
                        sql += "WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = " (PUBLISHDATE,FORECASTDATE,WAVELEVELONE,WAVELEVELTYPE,WAVEDIRECTION,WATERTEMPERATURE)"
                           + " VALUES"
                           + " (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'),'@WAVELEVELONE','@WAVELEVELTYPE','@WAVEDIRECTION','@WATERTEMPERATURE')";
                        sql = "INSERT INTO TBLYTWAVE ";
                        switch (type)
                        {
                            case "fl":
                                sql += "(PUBLISHDATE, FORECASTDATE, WAVELEVELONE, WAVELEVELTYPE, WAVEDIRECTION) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@WAVELEVELONE', '@WAVELEVELTYPE', '@WAVEDIRECTION')";
                                break;
                            case "sw":
                                sql += "(PUBLISHDATE, FORECASTDATE, WATERTEMPERATURE) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@WATERTEMPERATURE')";
                                break;
                            default: break;
                        }
                    }
                    foreach (ModelAmShort10 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort10> newdata = getAmShort10(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort10 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午十一、海阳近岸海域潮汐预报
        private string setAmShort11(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "cx") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort11> datalist = JsonConvert.DeserializeObject<List<ModelAmShort11>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "SELECT * FROM TBLYTTIDE WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE TBLYTTIDE SET FIRSTHIGHTIME='@FIRSTHIGHTIME', FIRSTHIGHLEVEL='@FIRSTHIGHLEVEL', FIRSTLOWTIME='@FIRSTLOWTIME', FIRSTLOWLEVEL='@FIRSTLOWLEVEL', SECONDHIGHTIME='@SECONDHIGHTIME', SECONDHIGHLEVEL='@SECONDHIGHLEVEL', SECONDLOWTIME='@SECONDLOWTIME', SECONDLOWLEVEL='@SECONDLOWLEVEL' WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "INSERT INTO TBLYTTIDE"
                           + " (PUBLISHDATE,FORECASTDATE,FIRSTHIGHTIME,FIRSTHIGHLEVEL,FIRSTLOWTIME,FIRSTLOWLEVEL,SECONDHIGHTIME,SECONDHIGHLEVEL,SECONDLOWTIME,SECONDLOWLEVEL)"
                           + " VALUES"
                           + " (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@FIRSTHIGHTIME', '@FIRSTHIGHLEVEL', '@FIRSTLOWTIME', '@FIRSTLOWLEVEL', '@SECONDHIGHTIME', '@SECONDHIGHLEVEL', '@SECONDLOWTIME', '@SECONDLOWLEVEL')";
                    }
                    foreach (ModelAmShort11 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort11> newdata = getAmShort11(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort11 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午十二、海阳万米海滩海水浴场风、浪预报
        private string setAmShort12(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "fl") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort12> datalist = JsonConvert.DeserializeObject<List<ModelAmShort12>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "SELECT * FROM TBLYTYC WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE TBLYTYC SET WEATERSTATE='@WEATERSTATE', TEMPERATURE='@TEMPERATURE', WINDSPEED='@WINDSPEED', WINDDIRECTION='@WINDDIRECTION', WAVEHEIGHT='@WAVEHEIGHT' WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "INSERT INTO TBLYTYC"
                           + " (PUBLISHDATE, FORECASTDATE, WEATERSTATE, TEMPERATURE, WINDSPEED, WINDDIRECTION, WAVEHEIGHT)"
                           + " VALUES"
                           + " (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@WEATERSTATE', '@TEMPERATURE', '@WINDSPEED', '@WINDDIRECTION', '@WAVEHEIGHT')";
                    }
                    foreach (ModelAmShort12 data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelAmShort12> newdata = getAmShort12(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelAmShort12 model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 页脚 填报信息
        private string setPublishMetaInfo(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            int executioncount = 0;
            // 判断输入参数 用户类型
            if ((type == "fl" | type == "sw" | type == "cx") & datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelPublishMetaInfo> datalist = JsonConvert.DeserializeObject<List<ModelPublishMetaInfo>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = datalist[0].PUBLISHDATE;
                    string sqlselect = "select * from TBLFOOTER where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    DataTable dtselect = queryData(sqlselect);
                    string sql = "";
                    if (dtselect.Rows.Count > 0)
                    {
                        // Update
                        result.Description = "修改记录";
                        sql = "UPDATE TBLFOOTER set ";
                        switch (type)
                        {
                            case "fl":
                                sql += "PUBLISHHOUR='@PUBLISHHOUR',FRELEASEUNIT='@FRELEASEUNIT',FTELEPHONE='@FTELEPHONE',FFAX='@FFAX',FWAVEFORECASTER='@FWAVEFORECASTER',FWAVEFORECASTERTEL='@FWAVEFORECASTERTEL',ZHIBANTEL='@ZHIBANTEL',SENDTEL='@SENDTEL' ";
                                break;
                            case "sw":
                                sql += "PUBLISHHOUR='@PUBLISHHOUR',FRELEASEUNIT='@FRELEASEUNIT',FTELEPHONE='@FTELEPHONE',FFAX='@FFAX',FWATERTEMPERATUREFORECASTER='@FWATERTEMPERATUREFORECASTER',FWATERTEMPERATUREFORECASTERTEL='@FWATERTEMPERATUREFORECASTERTEL',ZHIBANTEL='@ZHIBANTEL',SENDTEL='@SENDTEL' ";
                                break;
                            case "cx":
                                sql += "PUBLISHHOUR='@PUBLISHHOUR',FRELEASEUNIT='@FRELEASEUNIT',FTELEPHONE='@FTELEPHONE',FFAX='@FFAX',FTIDALFORECASTER='@FTIDALFORECASTER',FTIDALFORECASTERTEL='@FTIDALFORECASTERTEL',ZHIBANTEL='@ZHIBANTEL',SENDTEL='@SENDTEL' ";
                                break;
                            default: break;
                        }
                        sql += "where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    }
                    else
                    {
                        // Insert
                        result.Description = "新增记录";
                        sql = "INSERT INTO TBLFOOTER ";
                        switch (type)
                        {
                            case "fl":
                                sql += "(PUBLISHDATE,PUBLISHHOUR,FRELEASEUNIT,FTELEPHONE,FFAX,FWAVEFORECASTER,FWAVEFORECASTERTEL,ZHIBANTEL,SENDTEL) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),'@PUBLISHHOUR','@FRELEASEUNIT','@FTELEPHONE','@FFAX','@FWAVEFORECASTER','@FWAVEFORECASTERTEL','@ZHIBANTEL','@SENDTEL')";
                                break;
                            case "sw":
                                sql += "(PUBLISHDATE,PUBLISHHOUR,FRELEASEUNIT,FTELEPHONE,FFAX,FWATERTEMPERATUREFORECASTER,FWATERTEMPERATUREFORECASTERTEL,ZHIBANTEL,SENDTEL) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),'@PUBLISHHOUR','@FRELEASEUNIT','@FTELEPHONE','@FFAX','@FWATERTEMPERATUREFORECASTER','@FWATERTEMPERATUREFORECASTERTEL','@ZHIBANTEL','@SENDTEL')";
                                break;
                            case "cx":
                                sql += "(PUBLISHDATE,PUBLISHHOUR,FRELEASEUNIT,FTELEPHONE,FFAX,FTIDALFORECASTER,FTIDALFORECASTERTEL,ZHIBANTEL,SENDTEL) ";
                                sql += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),'@PUBLISHHOUR','@FRELEASEUNIT','@FTELEPHONE','@FFAX','@FTIDALFORECASTER','@FTIDALFORECASTERTEL','@ZHIBANTEL','@SENDTEL')";
                                break;
                            default: break;
                        }
                    }
                    foreach (ModelPublishMetaInfo data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(sql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                        bool newfake = false;
                        List<ModelPublishMetaInfo> newdata = getPublishMetaInfo(pubdate, out newfake);
                        result.NewFakeData = newfake;
                        foreach (ModelPublishMetaInfo model in newdata)
                        {
                            result.NewData.Add(model);
                        }
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return JsonConvert.SerializeObject(result);
        }

        private ModelEditResponse submitTable<T>(string type, List<string> validtypes, string datajson, string selectsql, string executesql) where T : class
        {
            ModelEditResponse result = new ModelEditResponse();

            int executioncount = 0;
            // 判断输入参数 用户类型
            if (validtypes.Contains(type) & datajson != "")
            {
                // 判断传入的数据有效性
                List<T> datalist = JsonConvert.DeserializeObject<List<T>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DataTable dtselect = queryData(selectsql);
                    foreach (T data in datalist)
                    {
                        List<DbParameter> dbParameters = buildParameters(data);
                        executioncount += executeSql(executesql, dbParameters);
                    }
                    if (executioncount > 0)
                    {
                        result.Success = true;
                        result.AffectedRowCount = executioncount;
                    }
                    else
                    {
                        result.Description = "未能写入数据库";
                    }
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "预报员类型不符";
            }

            return result;
        }

        #endregion

        #region Helper Methods
        private void fillModelAmShort2List(List<ModelAmShort2> list, int index, DataRow datarow)
        {
            list[index].HTLFIRSTWAVEOFTIME = datarow["FSTHIGHWIDETIME"].ToString().Replace(":", "");
            list[index].HTLFIRSTWAVETIDELEVEL = datarow["FSTHIGHWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].HTLFIRSTTIMELOWTIDE = datarow["FSTLOWWIDETIME"].ToString().Replace(":", "");
            list[index].HTLLOWTIDELEVELFORTHEFIRSTTIME = datarow["FSTLOWWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].HTLSECONDWAVEOFTIME = datarow["SCDHIGHWIDETIME"].ToString().Replace(":", "");
            list[index].HTLSECONDWAVETIDELEVEL = datarow["SCDHIGHWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].HTLSECONDTIMELOWTIDE = datarow["SCDLOWWIDETIME"].ToString().Replace(":", "");
            list[index].HTLLOWTIDELEVELFORTHESECONDTIM = datarow["SCDLOWWIDEHEIGHT"].ToString().Replace(":", "");
        }
        private void fillModelAmShort6List(List<ModelAmShort6> list, int index, DataRow datarow)
        {
            list[index].TLFIRSTWAVEOFTIME = datarow["FSTHIGHWIDETIME"].ToString().Replace(":", "");
            list[index].TLFIRSTWAVETIDELEVEL = datarow["FSTHIGHWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].TLFIRSTTIMELOWTIDE = datarow["FSTLOWWIDETIME"].ToString().Replace(":", "");
            list[index].TLLOWTIDELEVELFORTHEFIRSTTIME = datarow["FSTLOWWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].TLSECONDWAVEOFTIME = datarow["SCDHIGHWIDETIME"].ToString().Replace(":", "");
            list[index].TLSECONDWAVETIDELEVEL = datarow["SCDHIGHWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].TLSECONDTIMELOWTIDE = datarow["SCDLOWWIDETIME"].ToString().Replace(":", "");
            list[index].TLLOWTIDELEVELFORTHESECONDTIME = datarow["SCDLOWWIDEHEIGHT"].ToString().Replace(":", "");
        }
        private void fillModelAmShort7List(List<ModelAmShort7> list, int startindex, DataRow datarow)
        {
            list[startindex].YRBHWWFWAVEHEIGHT = datarow["WAVE24FORECAST"].ToString();
            list[startindex].YRBHWWFWAVEDIR = datarow["WINDDIRECTION24FORECAST"].ToString();
            list[startindex].YRBHWWFFLOWDIR = datarow["WINDDIRECTION24FORECAST"].ToString();
            list[startindex].YRBHWWFFLOWLEVEL = datarow["WINDFORCE24FORECAST"].ToString();
            list[startindex + 1].YRBHWWFWAVEHEIGHT = datarow["WAVE48FORECAST"].ToString();
            list[startindex + 1].YRBHWWFWAVEDIR = datarow["WINDDIRECTION48FORECAST"].ToString();
            list[startindex + 1].YRBHWWFFLOWDIR = datarow["WINDDIRECTION48FORECAST"].ToString();
            list[startindex + 1].YRBHWWFFLOWLEVEL = datarow["WINDFORCE48FORECAST"].ToString();
            list[startindex + 2].YRBHWWFWAVEHEIGHT = datarow["WAVE72FORECAST"].ToString();
            list[startindex + 2].YRBHWWFWAVEDIR = datarow["WINDDIRECTION72FORECAST"].ToString();
            list[startindex + 2].YRBHWWFFLOWDIR = datarow["WINDDIRECTION72FORECAST"].ToString();
            list[startindex + 2].YRBHWWFFLOWLEVEL = datarow["WINDFORCE72FORECAST"].ToString();
        }
        private void fillModelAmShort8List(List<ModelAmShort8> list, int index, DataRow datarow)
        {
            list[index].HTLFIRSTWAVEOFTIME = datarow["FSTHIGHWIDETIME"].ToString().Replace(":", "");
            list[index].HTLFIRSTWAVETIDELEVEL = datarow["FSTHIGHWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].HTLFIRSTTIMELOWTIDE = datarow["FSTLOWWIDETIME"].ToString().Replace(":", "");
            list[index].HTLLOWTIDELEVELFORTHEFIRSTTIME = datarow["FSTLOWWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].HTLSECONDWAVEOFTIME = datarow["SCDHIGHWIDETIME"].ToString().Replace(":", "");
            list[index].HTLSECONDWAVETIDELEVEL = datarow["SCDHIGHWIDEHEIGHT"].ToString().Replace(":", "");
            list[index].HTLSECONDTIMELOWTIDE = datarow["SCDLOWWIDETIME"].ToString().Replace(":", "");
            list[index].HTLLOWTIDELEVELFORTHESECONDTIM = datarow["SCDLOWWIDEHEIGHT"].ToString().Replace(":", "");
        }

        #endregion

        private DataTable queryData(string sql)
        {
            DataTable result = new DataTable();

            string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory Provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = ConnectionStr;
            DbCommand command = Provider.CreateCommand();
            command.Connection = conn;
            command.CommandText = sql;
            DbDataAdapter ada = Provider.CreateDataAdapter();
            try
            {
                conn.Open();
                ada.SelectCommand = command;
                DataSet ds = new DataSet();
                ada.Fill(ds, "tb");
                result = ds.Tables["tb"];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        private DataTable queryData(string sql, DbConnection connection)
        {
            DataTable result = new DataTable();
            string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
            DbCommand command = provider.CreateCommand();
            command.Connection = connection;
            command.CommandText = sql;
            DbDataAdapter adapter = provider.CreateDataAdapter();
            try
            {
                adapter.SelectCommand = command;
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "table");
                result = dataset.Tables["table"];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return result;
        }

        private DbConnection getDBConnection()
        {
            string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory Provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = ConnectionStr;
            return conn;
        }

        private int executeSql(string sql, List<DbParameter> parameters)
        {
            int result = 0;

            string ConnectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory Provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = ConnectionStr;
            DbCommand command = Provider.CreateCommand();
            command.Connection = conn;
            command.CommandType = CommandType.Text;
            command.Parameters.Clear();
            for (int i = 0; i < parameters.Count; i++)
            {
                sql = sql.Replace(parameters[i].ParameterName, parameters[i].Value.ToString());
            }
            command.CommandText = sql;
            try
            {
                conn.Open();
                System.Diagnostics.Debug.WriteLine(command.CommandText);
                result = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                command.Dispose();
                conn.Close();
            }
            return result;
        }

        private List<DbParameter> buildParameters<T>(T source) where T : class
        {
            List<DbParameter> result = new List<DbParameter>();

            string connectionStr = ConfigurationManager.ConnectionStrings["DataBaseCon"].ConnectionString;
            DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DataBaseCon"].ProviderName);

            foreach (var prop in typeof(T).GetProperties())
            {
                // System.Diagnostics.Debug.WriteLine(prop.PropertyType + " " + prop.Name + " : " + prop.GetValue(source, null));
                DbParameter parameter = provider.CreateParameter();
                parameter.ParameterName = "@" + prop.Name;
                if (prop.PropertyType == typeof(DateTime))
                {
                    parameter.Value = ((DateTime)prop.GetValue(source, null)).ToString("yyyy-MM-dd");
                }
                else
                {
                    parameter.Value = prop.GetValue(source, null);
                }

                result.Add(parameter);
            }
            return result;
        }

        /// <summary>
        /// DataTable转化为List集合
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="dt">datatable表</param>
        /// <param name="isStoreDB">是否存入数据库datetime字段，date字段没事，取出不用判断</param>
        /// <returns>返回list集合</returns>
        public static List<T> TableToList<T>(DataTable dt, bool isStoreDB = true)
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            //List<string> listColums = new List<string>();
            PropertyInfo[] pArray = type.GetProperties(); //集合属性数组
            foreach (DataRow row in dt.Rows)
            {
                T entity = Activator.CreateInstance<T>(); //新建对象实例 
                foreach (PropertyInfo p in pArray)
                {
                    if (!dt.Columns.Contains(p.Name) || row[p.Name] == null || row[p.Name] == DBNull.Value)
                    {
                        continue;  //DataTable列中不存在集合属性或者字段内容为空则，跳出循环，进行下个循环   
                    }
                    if (isStoreDB && p.PropertyType == typeof(DateTime) && Convert.ToDateTime(row[p.Name]) < Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    try
                    {
                        var obj = Convert.ChangeType(row[p.Name], p.PropertyType);//类型强转，将table字段类型转为集合字段类型  
                        p.SetValue(entity, obj, null);
                    }
                    catch (Exception)
                    {
                        // throw;
                    }
                    //if (row[p.Name].GetType() == p.PropertyType)
                    //{
                    //    p.SetValue(entity, row[p.Name], null); //如果不考虑类型异常，foreach下面只要这一句就行
                    //}                    
                    //object obj = null;
                    //if (ConvertType(row[p.Name], p.PropertyType,isStoreDB, out obj))
                    //{                                        
                    //    p.SetValue(entity, obj, null);
                    //}                
                }
                list.Add(entity);
            }
            return list;
        }

        private string DataTableToJson(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return "";
            }

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }
    }
}
