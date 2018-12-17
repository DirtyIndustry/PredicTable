using Newtonsoft.Json;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PredicTable.WebServiceClass
{
    public class FuncAmShort
    {
        private delegate object GetTableMethod(DateTime date, List<int> fakedatalist, int fakedataindex);

        #region 上午短期预报 查询方法
        // 上午一、72小时渤海海区及黄河海港风、浪预报
        public static List<ModelAmShort1> getAmShort1(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort1> result = new List<ModelAmShort1>();
            int fake = 0;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(1), "渤海"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(2), "渤海"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(3), "渤海"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(1), "黄河海港"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(2), "黄河海港"));
            result.Add(new ModelAmShort1(pubdate, pubdate.AddDays(3), "黄河海港"));
            string sql = "select * from Tblyrbhwindwave72hforecasttwo where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count > 0)
            {
                List<ModelAmShort1> sqldatalist = OperatorSQL.TableToList<ModelAmShort1>(dt);
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
                fake = 0;
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
                DataTable dttemperature = OperatorSQL.queryData(sqltemperature);

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
                    fake = fake + 1;
                }
            }
            if (wavewindEmpty)
            {
                string sqlwavewind = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND  FORECASTAREA in ('渤海','黄河海港')";
                DataTable dtwavewind = OperatorSQL.queryData(sqlwavewind);

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
                    fake = fake + 100;
                }
            }
            result.Sort(new ModelAmShort1Comparer());
            foreach (ModelAmShort1 data in result)
            {
                if (data.REPORTAREA == "渤海")
                {
                    data.YRBHWWFWATERTEMPERATURE = "-";
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午二、72小时港口潮位预报
        public static List<ModelAmShort2> getAmShort2(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort2> result = new List<ModelAmShort2>();
            int fake = 0;
            DateTime publishdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(1), "龙口港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(2), "龙口港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(3), "龙口港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(1), "黄河海港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(2), "黄河海港"));
            result.Add(new ModelAmShort2(publishdate, publishdate.AddDays(3), "黄河海港"));

            // 不是周二
            string sql = "select * from TBLHARBOURTIDELEVEL where PUBLISHDATE=to_date('" + publishdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count < 1)
            {
                DayOfWeek week = date.DayOfWeek;
                if (week == DayOfWeek.Tuesday)
                {
                    // 周二
                    DateTime pubdate = date.AddDays(-1);
                    sql = "select * from TBLHARBOURTIDELEVEL7DAY where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') "
                        + "and forecastdate >to_date('" + pubdate.AddDays(1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + "and forecastdate< to_date('" + pubdate.AddDays(5).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by htlharbour desc, forecastdate asc";
                    dt = OperatorSQL.queryData(sql);
                }
                else
                {
                    // 不是周二
                    sql = "select * from TBLHARBOURTIDELEVEL "
                        + " where FORECASTDATE > to_date('" + publishdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and FORECASTDATE < to_date('" + publishdate.AddDays(3).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and PUBLISHDATE=to_date('" + publishdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    dt = OperatorSQL.queryData(sql);
                }
                fake = fake + 10;
            }
            if (dt.Rows.Count > 0)
            {
                // result = TableToList<ModelAmShort2>(dt);
                List<ModelAmShort2> sqldatalist = OperatorSQL.TableToList<ModelAmShort2>(dt);
                foreach (ModelAmShort2 sqldata in sqldatalist)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (sqldata.HTLHARBOUR == result[i].HTLHARBOUR & sqldata.FORECASTDATE.ToString("yyyy/MM/dd") == result[i].FORECASTDATE.ToString("yyyy/MM/dd"))
                        {
                            result[i].HTLFIRSTTIMELOWTIDE = sqldata.HTLFIRSTTIMELOWTIDE;
                            result[i].HTLFIRSTWAVEOFTIME = sqldata.HTLFIRSTWAVEOFTIME;
                            result[i].HTLFIRSTWAVETIDELEVEL = sqldata.HTLFIRSTWAVETIDELEVEL;
                            result[i].HTLLOWTIDELEVELFORTHEFIRSTTIME = sqldata.HTLLOWTIDELEVELFORTHEFIRSTTIME;
                            result[i].HTLLOWTIDELEVELFORTHESECONDTIM = sqldata.HTLLOWTIDELEVELFORTHESECONDTIM;
                            result[i].HTLSECONDTIMELOWTIDE = sqldata.HTLSECONDTIMELOWTIDE;
                            result[i].HTLSECONDWAVEOFTIME = sqldata.HTLSECONDWAVEOFTIME;
                            result[i].HTLSECONDWAVETIDELEVEL = sqldata.HTLSECONDWAVETIDELEVEL;
                        }
                    }
                }
            }

            // 用天文潮数据补齐天数
            if (fake > 0)
            {
                string sqlastro = "select * from HT_YB_TIDE where STATION in ('113lko', '119hhg') and PREDICTIONDATE between to_date('"
                    + publishdate.AddDays(1).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') and to_date('"
                    + publishdate.AddDays(3).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') ORDER BY STATION,PREDICTIONDATE";
                DataTable dtastro = OperatorSQL.queryData(sqlastro);
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
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午三、3天海洋水文气象预报综述
        // 上午四、24小时水文气象预报综述
        public static List<ModelAmShort3and4> getAmShort3and4(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort3and4> result = new List<ModelAmShort3and4>();
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort3and4(pubdate));
            string sql = "SELECT * FROM HT_ZSVIEW WHERE PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = OperatorSQL.TableToList<ModelAmShort3and4>(dt);
            }
            return result;
        }

        // 上午五、预计未来24小时海浪、水温预报
        public static List<ModelAmShort5> getAmShort5(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort5> result = new List<ModelAmShort5>();
            int fake = 0;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort5(pubdate));
            string sql = "select * from TBLEXPECTEDFUTURE24HWAVEWATER where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = OperatorSQL.TableToList<ModelAmShort5>(dt);
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
                DataTable dttemperature = OperatorSQL.queryData(sqltemperature);
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
                fake = fake + 1;
            }
            if (waveEmpty)
            {
                string sqlwave = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND  FORECASTAREA in ('渤海', '黄海北部', '刁口近海', '黄河口海域', '广利港海域', '东营港海域', '新户近海', '埕岛近海')";
                DataTable dtwave = OperatorSQL.queryData(sqlwave);
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
                    fake = fake + 100;
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午六、24小时潮位预报
        public static List<ModelAmShort6> getAmShort6(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort6> result = new List<ModelAmShort6>();
            int fake = 0;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "小岛河"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "孤东"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "东营港"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "桩西"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "飞雁滩"));
            result.Add(new ModelAmShort6(pubdate, pubdate.AddDays(1), "新户"));
            string sql = "select * from TBL24HTIDELEVEL where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count > 0)
            {
                List<ModelAmShort6> sqldatalist = OperatorSQL.TableToList<ModelAmShort6>(dt);
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
                DataTable dtastro = OperatorSQL.queryData(sqlastro);
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
                    fake = fake + 10;
                }
            }
            result.Sort(new ModelAmShort6Comparer());
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午七、海上丝绸之路三天海浪、气象预报
        public static List<ModelAmShort7> getAmShort7(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort7> result = new List<ModelAmShort7>();
            int fake = 0;
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
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count == 0)
            {
                sql = "SELECT * FROM HT_SILKWINDWAVE "
                        + " WHERE PUBLISHDATE=to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') ";
                dt = OperatorSQL.queryData(sql);
                fake = fake + 100;
            }
            if (dt.Rows.Count > 0)
            {
                List<ModelAmShort7> sqldatalist = OperatorSQL.TableToList<ModelAmShort7>(dt);
                for (int i = 0; i < result.Count; i++)
                {
                    foreach (ModelAmShort7 sqldata in sqldatalist)
                    {
                        if (result[i].REPORTAREA == sqldata.REPORTAREA & result[i].FORECASTDATE.ToString("yyyy/MM/dd") == sqldata.FORECASTDATE.ToString("yyyy/MM/dd"))
                        {
                            result[i].YRBHWWFFLOWDIR = sqldata.YRBHWWFFLOWDIR;
                            result[i].YRBHWWFFLOWLEVEL = sqldata.YRBHWWFFLOWLEVEL;
                            result[i].YRBHWWFWAVEDIR = sqldata.YRBHWWFWAVEDIR;
                            result[i].YRBHWWFWAVEHEIGHT = sqldata.YRBHWWFWAVEHEIGHT;
                        }
                    }
                }
            }
            else
            {
                sql = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND  FORECASTAREA in ('青岛近海', '潍坊近海', '营口港')";
                dt = OperatorSQL.queryData(sql);
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
                    fake = fake + 100;
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午八、海上丝绸之路三天潮汐预报
        public static List<ModelAmShort8> getAmShort8(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort8> result = new List<ModelAmShort8>();
            int fake = 0;
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

            string sql = "";
            DataTable dt = new DataTable();
            // 不是周二
            sql = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by HTLHARBOUR,forecastdate asc";
            dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count == 0)
            {
                DayOfWeek week = date.DayOfWeek;
                if (week == DayOfWeek.Tuesday)
                {
                    // 周二
                    sql = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') " +
                        "and  forecastdate>to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by HTLHARBOUR,forecastdate asc";
                    dt = OperatorSQL.queryData(sql);
                }
                else
                {
                    // 不是周二
                    sql = "SELECT * FROM HT_SILKTIDE "
                        + " WHERE PUBLISHDATE=to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') ";
                    dt = OperatorSQL.queryData(sql);
                }
                fake = fake + 10;
            }

            if (dt.Rows.Count > 0)
            {
                List<ModelAmShort8> sqldatalist = OperatorSQL.TableToList<ModelAmShort8>(dt);
                for (int i = 0; i < result.Count; i++)
                {
                    foreach (ModelAmShort8 sqldata in sqldatalist)
                    {
                        if (result[i].HTLHARBOUR == sqldata.HTLHARBOUR & result[i].FORECASTDATE.ToString("yyyy/MM/dd") == sqldata.FORECASTDATE.ToString("yyyy/MM/dd"))
                        {
                            result[i].HTLFIRSTTIMELOWTIDE = sqldata.HTLFIRSTTIMELOWTIDE;
                            result[i].HTLFIRSTWAVEOFTIME = sqldata.HTLFIRSTWAVEOFTIME;
                            result[i].HTLFIRSTWAVETIDELEVEL = sqldata.HTLFIRSTWAVETIDELEVEL;
                            result[i].HTLLOWTIDELEVELFORTHEFIRSTTIME = sqldata.HTLLOWTIDELEVELFORTHEFIRSTTIME;
                            result[i].HTLLOWTIDELEVELFORTHESECONDTIM = sqldata.HTLLOWTIDELEVELFORTHESECONDTIM;
                            result[i].HTLSECONDTIMELOWTIDE = sqldata.HTLSECONDTIMELOWTIDE;
                            result[i].HTLSECONDWAVEOFTIME = sqldata.HTLSECONDWAVEOFTIME;
                            result[i].HTLSECONDWAVETIDELEVEL = sqldata.HTLSECONDWAVETIDELEVEL;
                        }
                    }
                }
            }
            else if (dt.Rows.Count == 0 | fake > 0)
            {
                // 用天文潮数据补齐
                string sqlastro = "select * from HT_YB_TIDE where STATION in ("
                    + "'101wmt','114wfg','133byq'" + ") and PREDICTIONDATE between to_date('"
                    + pubdate.AddDays(1).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') and to_date('"
                    + pubdate.AddDays(3).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ORDER BY STATION,PREDICTIONDATE";
                DataTable dtastro = OperatorSQL.queryData(sqlastro);
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
                    fake = fake + 10;
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午九、海区24小时海浪、水温预报
        public static List<ModelAmShort9> getAmShort9(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort9> result = new List<ModelAmShort9>();
            int fake = 0;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort9(pubdate));
            string sql = "select * from HT_TBLWF24HWAVEFORECAST where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = OperatorSQL.TableToList<ModelAmShort9>(dt);
            }
            if (result[0].SA24HWFOFFSHORESW.Trim() == "")
            {
                string sqltemperature = "select * from  TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND  NAME IN ('潍坊')";
                DataTable dttemperature = OperatorSQL.queryData(sqltemperature);
                if (dttemperature.Rows.Count > 0)
                {
                    result[0].SA24HWFOFFSHORESW = dttemperature.Rows[0]["MEAN_24H"].ToString();
                    fake = fake + 1;
                }
            }
            if (result[0].SA24HWFBOHAIWAVEHEIGHT == ""
                & result[0].SA24HWFMIDDLEOFYSWAVEHEIGHT == ""
                & result[0].SA24HWFNORTHOFYSWAVEHEIGHT == ""
                & result[0].SA24HWFOFFSHOREWAVEHEIGHT == ""
                & result[0].SA24HWFSOUTHOFYSWAVEHEIGHT == "")
            {
                string sqlwave = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND  FORECASTAREA in ('渤海', '黄海北部', '黄海中部', '黄海南部', '潍坊近海')";
                DataTable dtwave = OperatorSQL.queryData(sqlwave);
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
                    fake = fake + 100;
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午十、海阳海浪、水温预报
        public static List<ModelAmShort10> getAmShort10(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort10> result = new List<ModelAmShort10>();
            int fake = 0;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort10(pubdate, pubdate.AddDays(1)));
            string sql = "SELECT * FROM TBLYTWAVE WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = OperatorSQL.TableToList<ModelAmShort10>(dt);
            }
            if (result[0].WATERTEMPERATURE.Trim() == "")
            {
                string sqltemperature = "select * from  TBLWATERTEMPERATURE where  PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND  NAME IN ('南黄岛')";
                DataTable dttemperature = OperatorSQL.queryData(sqltemperature);
                if (dttemperature.Rows.Count > 0)
                {
                    result[0].WATERTEMPERATURE = dttemperature.Rows[0]["MEAN_24H"].ToString();
                    fake = fake + 1;
                }
            }
            if (result[0].WAVELEVELONE.Trim() == "" & result[0].WAVEDIRECTION.Trim() == "")
            {
                string sqlwavewind = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND FORECASTAREA = '乳山近海'";
                DataTable dtwavewind = OperatorSQL.queryData(sqlwavewind);
                if (dtwavewind.Rows.Count > 0)
                {
                    result[0].WAVELEVELONE = dtwavewind.Rows[0]["WAVE24FORECAST"].ToString();
                    result[0].WAVEDIRECTION = dtwavewind.Rows[0]["WINDDIRECTION24FORECAST"].ToString();
                    fake = fake + 100;
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午十一、海阳近岸海域潮汐预报
        public static List<ModelAmShort11> getAmShort11(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort11> result = new List<ModelAmShort11>();
            int fake = 0;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort11(pubdate, pubdate.AddDays(1)));
            string sql = "SELECT * FROM TBLYTTIDE WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = OperatorSQL.TableToList<ModelAmShort11>(dt);
            }
            else
            {
                // 用天文潮数据补齐
                string sqlastro = "select * from HT_YB_TIDE where STATION in ("
                    + "'105nhd'" + ") and PREDICTIONDATE = to_date('"
                    + pubdate.AddDays(1).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ORDER BY STATION,PREDICTIONDATE";
                DataTable dtastro = OperatorSQL.queryData(sqlastro);
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
                    fake = fake + 10;
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 上午十二、海阳万米海滩海水浴场风、浪预报
        public static List<ModelAmShort12> getAmShort12(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelAmShort12> result = new List<ModelAmShort12>();
            int fake = 0;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelAmShort12(pubdate, pubdate.AddDays(1)));
            string sql = "SELECT * FROM TBLYTYC WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            if (dt.Rows.Count > 0)
            {
                result = OperatorSQL.TableToList<ModelAmShort12>(dt);
            }
            if (result[0].WAVEHEIGHT.Trim() == ""
                & result[0].WINDDIRECTION.Trim() == ""
                & result[0].WINDSPEED.Trim() == "")
            {
                string sqlwindwave = "SELECT * FROM TBLWINDANDWAVEFORECAST WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND FORECASTEFFECT = 'AM' AND FORECASTAREA = '乳山近海'";
                DataTable dtwindwave = OperatorSQL.queryData(sqlwindwave);
                if (dtwindwave.Rows.Count > 0)
                {
                    result[0].WAVEHEIGHT = dtwindwave.Rows[0]["WAVE24FORECAST"].ToString();
                    result[0].WINDDIRECTION = dtwindwave.Rows[0]["WINDDIRECTION24FORECAST"].ToString();
                    result[0].WINDSPEED = dtwindwave.Rows[0]["WINDFORCE24FORECAST"].ToString();
                    fake = fake + 100;
                }
            }
            if (result[0].TEMPERATURE.Trim() == "")
            {
                string sqltemperature = "SELECT * FROM TBLYTYC WHERE PUBLISHDATE = to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                DataTable dttemperature = OperatorSQL.queryData(sqltemperature);
                if (dttemperature.Rows.Count > 0)
                {
                    result[0].TEMPERATURE = dttemperature.Rows[0]["TEMPERATURE"].ToString();
                    fake = fake + 100;
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        // 页脚 填报信息
        public static List<ModelPublishMetaInfo> getPublishMetaInfo(DateTime date, List<int> fakedatalist, int fakedataindex)
        {
            List<ModelPublishMetaInfo> result = new List<ModelPublishMetaInfo>();
            int fake = 0;
            DateTime pubdate = Convert.ToDateTime(date.ToString("yyyy/MM/dd"));
            result.Add(new ModelPublishMetaInfo(pubdate));
            string sql = "select * from TBLFOOTER where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dt = OperatorSQL.queryData(sql);
            string sqlyesterday = "select * from TBLFOOTER where PUBLISHDATE=to_date('" + pubdate.AddDays(-1).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
            DataTable dtyesterday = OperatorSQL.queryData(sqlyesterday);
            if (dt.Rows.Count == 0)
            {
                dt = dtyesterday;
                fake = fake + 111;
            }
            if (dt.Rows.Count > 0)
            {
                List<ModelPublishMetaInfo> sqldatalist = OperatorSQL.TableToList<ModelPublishMetaInfo>(dt);
                result[0].FFAX = sqldatalist[0].FFAX;
                result[0].FRELEASEUNIT = sqldatalist[0].FRELEASEUNIT;
                result[0].FTELEPHONE = sqldatalist[0].FTELEPHONE;
                result[0].FTIDALFORECASTER = sqldatalist[0].FTIDALFORECASTER;
                result[0].FTIDALFORECASTERTEL = sqldatalist[0].FTIDALFORECASTERTEL;
                result[0].FWATERTEMPERATUREFORECASTER = sqldatalist[0].FWATERTEMPERATUREFORECASTER;
                result[0].FWATERTEMPERATUREFORECASTERTEL = sqldatalist[0].FWATERTEMPERATUREFORECASTERTEL;
                result[0].FWAVEFORECASTER = sqldatalist[0].FWAVEFORECASTER;
                result[0].FWAVEFORECASTERTEL = sqldatalist[0].FWAVEFORECASTERTEL;
                result[0].PUBLISHHOUR = sqldatalist[0].PUBLISHHOUR;
                result[0].SENDTEL = sqldatalist[0].SENDTEL;
                result[0].ZHIBANTEL = sqldatalist[0].ZHIBANTEL;
            }
            if (dtyesterday.Rows.Count > 0)
            {
                List<ModelPublishMetaInfo> yesterdaydatalist = OperatorSQL.TableToList<ModelPublishMetaInfo>(dtyesterday);
                if (result[0].FTIDALFORECASTER == "" & result[0].FTIDALFORECASTERTEL == "")
                {
                    result[0].FTIDALFORECASTER = yesterdaydatalist[0].FTIDALFORECASTER;
                    result[0].FTIDALFORECASTERTEL = yesterdaydatalist[0].FTIDALFORECASTERTEL;
                    fake = fake + 10;
                }
                if (result[0].FWATERTEMPERATUREFORECASTER == "" & result[0].FWATERTEMPERATUREFORECASTERTEL == "")
                {
                    result[0].FWATERTEMPERATUREFORECASTER = yesterdaydatalist[0].FWATERTEMPERATUREFORECASTER;
                    result[0].FWATERTEMPERATUREFORECASTERTEL = yesterdaydatalist[0].FWATERTEMPERATUREFORECASTERTEL;
                    fake = fake + 1;
                }
                if (result[0].FWAVEFORECASTER == "" & result[0].FWAVEFORECASTERTEL == "")
                {
                    result[0].FWAVEFORECASTER = yesterdaydatalist[0].FWAVEFORECASTER;
                    result[0].FWAVEFORECASTERTEL = yesterdaydatalist[0].FWAVEFORECASTERTEL;
                    fake = fake + 100;
                }
            }
            fakedatalist[fakedataindex] = fake;
            return result;
        }

        public static bool hasEmpty<T>(List<T> list) where T : class
        {
            foreach (T data in list)
            {
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (prop.GetValue(data, null) == null | prop.GetValue(data, null).ToString() == "")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 上午短期预报 存储方法
        // 上午一、72小时渤海海区及黄河海港风、浪预报
        public static string setAmShort1(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            // 判断输入参数
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort1> datalist = JsonConvert.DeserializeObject<List<ModelAmShort1>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "select * from Tblyrbhwindwave72hforecasttwo "
                        + " where FORECASTDATE > to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and FORECASTDATE < to_date('" + pubdate.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " and PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE TBLYRBHWINDWAVE72HFORECASTTWO set ";
                    string sqlinsert = "INSERT INTO TBLYRBHWINDWAVE72HFORECASTTWO ";
                    switch (type)
                    {
                        case "fl":
                            sqlupdate += "YRBHWWFWAVEHEIGHT='@YRBHWWFWAVEHEIGHT', YRBHWWFWAVEDIR='@YRBHWWFWAVEDIR', YRBHWWFFLOWDIR='@YRBHWWFFLOWDIR', YRBHWWFFLOWLEVEL='@YRBHWWFFLOWLEVEL' ";
                            sqlinsert += "(PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@REPORTAREA', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@YRBHWWFWAVEHEIGHT', '@YRBHWWFWAVEDIR', '@YRBHWWFFLOWDIR', '@YRBHWWFFLOWLEVEL')";
                            break;
                        case "sw":
                            sqlupdate += " YRBHWWFWATERTEMPERATURE='@YRBHWWFWATERTEMPERATURE' ";
                            sqlinsert += "(PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWATERTEMPERATURE) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@REPORTAREA', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@YRBHWWFWATERTEMPERATURE')";
                            break;
                        default: break;
                    }
                    sqlupdate += "where  PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and REPORTAREA='@REPORTAREA' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    List<string> typelist = new List<string>() { "fl", "sw" };
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort1, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午二、72小时港口潮位预报
        public static string setAmShort2(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            if (datajson != "")
            {
                List<ModelAmShort2> datalist = JsonConvert.DeserializeObject<List<ModelAmShort2>>(datajson);
                if (datalist.Count > 0)
                {
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "select * from TBLHARBOURTIDELEVEL where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE TBLHARBOURTIDELEVEL set HTLSECONDTIMELOWTIDE='@HTLSECONDTIMELOWTIDE', HTLLOWTIDELEVELFORTHESECONDTIM='@HTLLOWTIDELEVELFORTHESECONDTIM', HTLFIRSTWAVEOFTIME='@HTLFIRSTWAVEOFTIME', HTLFIRSTWAVETIDELEVEL='@HTLFIRSTWAVETIDELEVEL', HTLFIRSTTIMELOWTIDE='@HTLFIRSTTIMELOWTIDE', HTLLOWTIDELEVELFORTHEFIRSTTIME='@HTLLOWTIDELEVELFORTHEFIRSTTIME', HTLSECONDWAVEOFTIME='@HTLSECONDWAVEOFTIME', HTLSECONDWAVETIDELEVEL='@HTLSECONDWAVETIDELEVEL' where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and HTLHARBOUR='@HTLHARBOUR' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    string sqlinsert = "INSERT INTO TBLHARBOURTIDELEVEL (PUBLISHDATE, HTLSECONDTIMELOWTIDE, HTLLOWTIDELEVELFORTHESECONDTIM, HTLHARBOUR, FORECASTDATE, HTLFIRSTWAVEOFTIME, HTLFIRSTWAVETIDELEVEL, HTLFIRSTTIMELOWTIDE, HTLLOWTIDELEVELFORTHEFIRSTTIME, HTLSECONDWAVEOFTIME, HTLSECONDWAVETIDELEVEL) VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@HTLSECONDTIMELOWTIDE', '@HTLLOWTIDELEVELFORTHESECONDTIM', '@HTLHARBOUR', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@HTLFIRSTWAVEOFTIME', '@HTLFIRSTWAVETIDELEVEL', '@HTLFIRSTTIMELOWTIDE', '@HTLLOWTIDELEVELFORTHEFIRSTTIME', '@HTLSECONDWAVEOFTIME', '@HTLSECONDWAVETIDELEVEL')";
                    List<string> typelist = new List<string>() { "cx" };
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort2, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午三、3天海洋水文气象预报综述
        // 上午四、24小时水文气象预报综述
        public static string setAmShort3and4(int tablenumber, string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            if (datajson != "")
            {
                List<ModelAmShort3and4> datalist = JsonConvert.DeserializeObject<List<ModelAmShort3and4>>(datajson);
                if (datalist.Count > 0)
                {
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "SELECT * FROM HT_ZSVIEW WHERE PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd hh24@mi@ss')";
                    string sqlinsert = "";
                    string sqlupdate = "";
                    switch (tablenumber)
                    {
                        case 3:
                            switch (type)
                            {
                                case "fl":
                                    sqlinsert = "INSERT INTO HT_ZSVIEW (PUBLISHDATE, METEOROLOGICALREVIEW, METEOROLOGICALREVIEWCX, METEOROLOGICALREVIEW24HOUR,METEOROLOGICALREVIEW7DAYS,METEOROLOGICALREVIEW24HOURCX,METEOROLOGICALREVIEW7DAYSCX) "
                                        + " VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@METEOROLOGICALREVIEW', '', '', '', '', '')";
                                    sqlupdate = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEW='@METEOROLOGICALREVIEW' WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                                    break;
                                case "cx":
                                    sqlinsert = "INSERT INTO HT_ZSVIEW (PUBLISHDATE, METEOROLOGICALREVIEW, METEOROLOGICALREVIEWCX, METEOROLOGICALREVIEW24HOUR,METEOROLOGICALREVIEW7DAYS,METEOROLOGICALREVIEW24HOURCX,METEOROLOGICALREVIEW7DAYSCX) "
                                        + " VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '', '@METEOROLOGICALREVIEWCX', '', '', '', '')";
                                    sqlupdate = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEWCX='@METEOROLOGICALREVIEWCX' WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                                    break;
                                default: break;
                            }
                            break;
                        case 4:
                            switch (type)
                            {
                                case "fl":
                                    sqlinsert = "INSERT INTO HT_ZSVIEW (PUBLISHDATE, METEOROLOGICALREVIEW24HOUR, METEOROLOGICALREVIEW24HOURCX, METEOROLOGICALREVIEW, METEOROLOGICALREVIEW7DAYS, METEOROLOGICALREVIEWCX, METEOROLOGICALREVIEW7DAYSCX) "
                                        + " VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@METEOROLOGICALREVIEW24HOUR', '', '', '', '', '')";
                                    sqlupdate = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEW24HOUR='@METEOROLOGICALREVIEW24HOUR' WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                                    break;
                                case "cx":
                                    sqlinsert = "INSERT INTO HT_ZSVIEW (PUBLISHDATE, METEOROLOGICALREVIEW24HOUR, METEOROLOGICALREVIEW24HOURCX, METEOROLOGICALREVIEW, METEOROLOGICALREVIEW7DAYS, METEOROLOGICALREVIEWCX, METEOROLOGICALREVIEW7DAYSCX) "
                                        + " VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '', '@METEOROLOGICALREVIEW24HOURCX', '', '', '', '')";
                                    sqlupdate = "UPDATE HT_ZSVIEW SET METEOROLOGICALREVIEW24HOURCX='@METEOROLOGICALREVIEW24HOURCX' WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                                    break;
                                default: break;
                            }
                            break;
                        default: break;
                    }
                    List<string> typelist = new List<string>() { "fl", "cx" };
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort3and4, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }
            return JsonConvert.SerializeObject(result);
        }

        // 上午五、预计未来24小时海浪、水温预报
        public static string setAmShort5(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "fl", "sw" };
            // 判断输入参数
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort5> datalist = JsonConvert.DeserializeObject<List<ModelAmShort5>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "select * from TBLEXPECTEDFUTURE24HWAVEWATER where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE TBLEXPECTEDFUTURE24HWAVEWATER set ";
                    string sqlinsert = "INSERT INTO  TBLEXPECTEDFUTURE24HWAVEWATER ";
                    switch (type)
                    {
                        case "fl":
                            sqlupdate += " EFWWHHKSEAAREAWAVEHEIGHT='@EFWWHHKSEAAREAWAVEHEIGHT', EFWWGLGSEAAREAWAVEHEIGHT='@EFWWGLGSEAAREAWAVEHEIGHT', EFWWDYGWAVEHEIGHT='@EFWWDYGWAVEHEIGHT', EFWWXHWAVEHEIGHT='@EFWWXHWAVEHEIGHT', EFWWCKWAVEHEIGHT='@EFWWCKWAVEHEIGHT', EFWWBHLOWESTWAVE='@EFWWBHLOWESTWAVE', EFWWBHHIGHESTWAVE='@EFWWBHHIGHESTWAVE', EFWWBHWAVETYPE='@EFWWBHWAVETYPE', EFWWBHNORTHLOWESTWAVE='@EFWWBHNORTHLOWESTWAVE', EFWWBHNORTHHIGHESTWAVE='@EFWWBHNORTHHIGHESTWAVE', EFWWBHNORTHWAVETYPE='@EFWWBHNORTHWAVETYPE', EFWWDKSEAAREAWAVEHEIGHT='@EFWWDKSEAAREAWAVEHEIGHT' ";
                            sqlinsert += "(PUBLISHDATE,EFWWHHKSEAAREAWAVEHEIGHT,EFWWGLGSEAAREAWAVEHEIGHT,EFWWDYGWAVEHEIGHT,EFWWXHWAVEHEIGHT,EFWWCKWAVEHEIGHT,EFWWBHLOWESTWAVE,EFWWBHHIGHESTWAVE,EFWWBHWAVETYPE,EFWWBHNORTHLOWESTWAVE,EFWWBHNORTHHIGHESTWAVE,EFWWBHNORTHWAVETYPE,EFWWDKSEAAREAWAVEHEIGHT) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),'@EFWWHHKSEAAREAWAVEHEIGHT','@EFWWGLGSEAAREAWAVEHEIGHT','@EFWWDYGWAVEHEIGHT','@EFWWXHWAVEHEIGHT','@EFWWCKWAVEHEIGHT','@EFWWBHLOWESTWAVE','@EFWWBHHIGHESTWAVE','@EFWWBHWAVETYPE','@EFWWBHNORTHLOWESTWAVE','@EFWWBHNORTHHIGHESTWAVE','@EFWWBHNORTHWAVETYPE','@EFWWDKSEAAREAWAVEHEIGHT')";
                            break;
                        case "sw":
                            sqlupdate += " EFWWHHKSEAAREAWATERTEMP='@EFWWHHKSEAAREAWATERTEMP', EFWWGLGSEAAREAWATERTEMP='@EFWWGLGSEAAREAWATERTEMP', EFWWDYGWATERTEMPERATURE='@EFWWDYGWATERTEMPERATURE', EFWWXHWATERTEMPERATURE='@EFWWXHWATERTEMPERATURE', EFWWCKWATERTEMPERATURE='@EFWWCKWATERTEMPERATURE', EFWWDKSEAAREAWATERTEMPE='@EFWWDKSEAAREAWATERTEMPE' ";
                            sqlinsert += "(PUBLISHDATE, EFWWHHKSEAAREAWATERTEMP, EFWWGLGSEAAREAWATERTEMP, EFWWDYGWATERTEMPERATURE, EFWWXHWATERTEMPERATURE, EFWWCKWATERTEMPERATURE, EFWWDKSEAAREAWATERTEMPE) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE', 'yyyy-mm-dd hh24@mi@ss'), '@EFWWHHKSEAAREAWATERTEMP', '@EFWWGLGSEAAREAWATERTEMP', '@EFWWDYGWATERTEMPERATURE', '@EFWWXHWATERTEMPERATURE', '@EFWWCKWATERTEMPERATURE', '@EFWWDKSEAAREAWATERTEMPE')";
                            break;
                        default: break;
                    }
                    sqlupdate += "where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort5, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午六、24小时潮位预报
        public static string setAmShort6(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "cx" };
            // 判断输入参数 用户类型
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort6> datalist = JsonConvert.DeserializeObject<List<ModelAmShort6>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "select * from TBL24HTIDELEVEL where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE TBL24HTIDELEVEL set TLSECONDTIMELOWTIDE='@TLSECONDTIMELOWTIDE', TLLOWTIDELEVELFORTHESECONDTIME='@TLLOWTIDELEVELFORTHESECONDTIME', TLFIRSTWAVEOFTIME='@TLFIRSTWAVEOFTIME', TLFIRSTWAVETIDELEVEL='@TLFIRSTWAVETIDELEVEL', TLFIRSTTIMELOWTIDE='@TLFIRSTTIMELOWTIDE', TLLOWTIDELEVELFORTHEFIRSTTIME='@TLLOWTIDELEVELFORTHEFIRSTTIME', TLSECONDWAVEOFTIME='@TLSECONDWAVEOFTIME', TLSECONDWAVETIDELEVEL='@TLSECONDWAVETIDELEVEL' where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and TLFORECASTSTANCE='@TLFORECASTSTANCE' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    string sqlinsert = "INSERT INTO TBL24HTIDELEVEL (PUBLISHDATE, TLSECONDTIMELOWTIDE, TLLOWTIDELEVELFORTHESECONDTIME, TLFORECASTSTANCE, FORECASTDATE, TLFIRSTWAVEOFTIME, TLFIRSTWAVETIDELEVEL, TLFIRSTTIMELOWTIDE, TLLOWTIDELEVELFORTHEFIRSTTIME, TLSECONDWAVEOFTIME, TLSECONDWAVETIDELEVEL) VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@TLSECONDTIMELOWTIDE', '@TLLOWTIDELEVELFORTHESECONDTIME', '@TLFORECASTSTANCE', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@TLFIRSTWAVEOFTIME', '@TLFIRSTWAVETIDELEVEL', '@TLFIRSTTIMELOWTIDE', '@TLLOWTIDELEVELFORTHEFIRSTTIME', '@TLSECONDWAVEOFTIME', '@TLSECONDWAVETIDELEVEL')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort6, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午七、海上丝绸之路三天海浪、气象预报
        public static string setAmShort7(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "fl" };
            // 判断输入参数 用户类型
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort7> datalist = JsonConvert.DeserializeObject<List<ModelAmShort7>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "SELECT * FROM HT_SILKWINDWAVE "
                        + " WHERE PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') AND "
                        + " FORECASTDATE BETWEEN to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')"
                        + " AND  to_date('" + pubdate.AddDays(4).ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE HT_SILKWINDWAVE set "
                        + "YRBHWWFWAVEHEIGHT='@YRBHWWFWAVEHEIGHT', YRBHWWFWAVEDIR='@YRBHWWFWAVEDIR', YRBHWWFFLOWDIR='@YRBHWWFFLOWDIR', YRBHWWFFLOWLEVEL='@YRBHWWFFLOWLEVEL' "
                        + "where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and REPORTAREA='@REPORTAREA' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    string sqlinsert = "INSERT INTO HT_SILKWINDWAVE (PUBLISHDATE, REPORTAREA, FORECASTDATE, YRBHWWFWAVEHEIGHT, YRBHWWFWAVEDIR, YRBHWWFFLOWDIR, YRBHWWFFLOWLEVEL) VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@REPORTAREA', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@YRBHWWFWAVEHEIGHT', '@YRBHWWFWAVEDIR', '@YRBHWWFFLOWDIR', '@YRBHWWFFLOWLEVEL')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort7, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午八、海上丝绸之路三天潮汐预报
        public static string setAmShort8(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "cx" };
            // 判断输入参数 用户类型
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort8> datalist = JsonConvert.DeserializeObject<List<ModelAmShort8>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss') order by HTLHARBOUR,forecastdate asc";
                    string sqlupdate = "UPDATE HT_SILKTIDE set HTLSECONDTIMELOWTIDE='@HTLSECONDTIMELOWTIDE', HTLLOWTIDELEVELFORTHESECONDTIM='@HTLLOWTIDELEVELFORTHESECONDTIM', HTLFIRSTWAVEOFTIME='@HTLFIRSTWAVEOFTIME', HTLFIRSTWAVETIDELEVEL='@HTLFIRSTWAVETIDELEVEL', HTLFIRSTTIMELOWTIDE='@HTLFIRSTTIMELOWTIDE', HTLLOWTIDELEVELFORTHEFIRSTTIME='@HTLLOWTIDELEVELFORTHEFIRSTTIME', HTLSECONDWAVEOFTIME='@HTLSECONDWAVEOFTIME', HTLSECONDWAVETIDELEVEL='@HTLSECONDWAVETIDELEVEL' where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss') and HTLHARBOUR='@HTLHARBOUR' and FORECASTDATE=to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss')";
                    string sqlinsert = "INSERT INTO HT_SILKTIDE (PUBLISHDATE, HTLSECONDTIMELOWTIDE, HTLLOWTIDELEVELFORTHESECONDTIM, HTLHARBOUR, FORECASTDATE, HTLFIRSTWAVEOFTIME, HTLFIRSTWAVETIDELEVEL, HTLFIRSTTIMELOWTIDE, HTLLOWTIDELEVELFORTHEFIRSTTIME, HTLSECONDWAVEOFTIME, HTLSECONDWAVETIDELEVEL) VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@HTLSECONDTIMELOWTIDE', '@HTLLOWTIDELEVELFORTHESECONDTIM', '@HTLHARBOUR', to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@HTLFIRSTWAVEOFTIME', '@HTLFIRSTWAVETIDELEVEL', '@HTLFIRSTTIMELOWTIDE', '@HTLLOWTIDELEVELFORTHEFIRSTTIME', '@HTLSECONDWAVEOFTIME', '@HTLSECONDWAVETIDELEVEL')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort8, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午九、海区24小时海浪、水温预报
        public static string setAmShort9(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "fl", "sw" };
            // 判断输入参数 用户类型
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort9> datalist = JsonConvert.DeserializeObject<List<ModelAmShort9>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "select * from HT_TBLWF24HWAVEFORECAST where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE HT_TBLWF24HWAVEFORECAST set ";
                    string sqlinsert = "INSERT INTO HT_TBLWF24HWAVEFORECAST ";
                    switch (type)
                    {
                        case "fl":
                            sqlupdate += "SA24HWFBOHAIWAVEHEIGHT='@SA24HWFBOHAIWAVEHEIGHT', SA24HWFNORTHOFYSWAVEHEIGHT='@SA24HWFNORTHOFYSWAVEHEIGHT', SA24HWFMIDDLEOFYSWAVEHEIGHT='@SA24HWFMIDDLEOFYSWAVEHEIGHT', SA24HWFSOUTHOFYSWAVEHEIGHT='@SA24HWFSOUTHOFYSWAVEHEIGHT', SA24HWFOFFSHOREWAVEHEIGHT='@SA24HWFOFFSHOREWAVEHEIGHT' ";
                            sqlinsert += "(PUBLISHDATE, SA24HWFBOHAIWAVEHEIGHT, SA24HWFNORTHOFYSWAVEHEIGHT, SA24HWFMIDDLEOFYSWAVEHEIGHT, SA24HWFSOUTHOFYSWAVEHEIGHT, SA24HWFOFFSHOREWAVEHEIGHT) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@SA24HWFBOHAIWAVEHEIGHT', '@SA24HWFNORTHOFYSWAVEHEIGHT', '@SA24HWFMIDDLEOFYSWAVEHEIGHT', '@SA24HWFSOUTHOFYSWAVEHEIGHT', '@SA24HWFOFFSHOREWAVEHEIGHT')";
                            break;
                        case "sw":
                            sqlupdate += "SA24HWFOFFSHORESW='@SA24HWFOFFSHORESW' ";
                            sqlinsert += "(PUBLISHDATE, SA24HWFOFFSHORESW) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), '@SA24HWFOFFSHORESW')";
                            break;
                        default: break;
                    }
                    sqlupdate += "where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort9, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午十、海阳海浪、水温预报
        public static string setAmShort10(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "fl", "sw" };
            // 判断输入参数 用户类型
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort10> datalist = JsonConvert.DeserializeObject<List<ModelAmShort10>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "SELECT * FROM TBLYTWAVE WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE TBLYTWAVE set ";
                    string sqlinsert = "INSERT INTO TBLYTWAVE ";
                    switch (type)
                    {
                        case "fl":
                            sqlupdate += "WAVELEVELONE='@WAVELEVELONE', WAVELEVELTYPE='@WAVELEVELTYPE', WAVEDIRECTION='@WAVEDIRECTION' ";
                            sqlinsert += "(PUBLISHDATE, FORECASTDATE, WAVELEVELONE, WAVELEVELTYPE, WAVEDIRECTION) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@WAVELEVELONE', '@WAVELEVELTYPE', '@WAVEDIRECTION')";
                            break;
                        case "sw":
                            sqlupdate += "WATERTEMPERATURE='@WATERTEMPERATURE' ";
                            sqlinsert += "(PUBLISHDATE, FORECASTDATE, WATERTEMPERATURE) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@WATERTEMPERATURE')";
                            break;
                        default: break;
                    }
                    sqlupdate += "WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort10, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午十一、海阳近岸海域潮汐预报
        public static string setAmShort11(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "cx" };
            // 判断输入参数 用户类型
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort11> datalist = JsonConvert.DeserializeObject<List<ModelAmShort11>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "SELECT * FROM TBLYTTIDE WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE TBLYTTIDE SET FIRSTHIGHTIME='@FIRSTHIGHTIME', FIRSTHIGHLEVEL='@FIRSTHIGHLEVEL', FIRSTLOWTIME='@FIRSTLOWTIME', FIRSTLOWLEVEL='@FIRSTLOWLEVEL', SECONDHIGHTIME='@SECONDHIGHTIME', SECONDHIGHLEVEL='@SECONDHIGHLEVEL', SECONDLOWTIME='@SECONDLOWTIME', SECONDLOWLEVEL='@SECONDLOWLEVEL' WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    string sqlinsert = "INSERT INTO TBLYTTIDE"
                       + " (PUBLISHDATE,FORECASTDATE,FIRSTHIGHTIME,FIRSTHIGHLEVEL,FIRSTLOWTIME,FIRSTLOWLEVEL,SECONDHIGHTIME,SECONDHIGHLEVEL,SECONDLOWTIME,SECONDLOWLEVEL)"
                       + " VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@FIRSTHIGHTIME', '@FIRSTHIGHLEVEL', '@FIRSTLOWTIME', '@FIRSTLOWLEVEL', '@SECONDHIGHTIME', '@SECONDHIGHLEVEL', '@SECONDLOWTIME', '@SECONDLOWLEVEL')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort11, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 上午十二、海阳万米海滩海水浴场风、浪预报
        public static string setAmShort12(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "fl" };
            // 判断输入参数 用户类型
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelAmShort12> datalist = JsonConvert.DeserializeObject<List<ModelAmShort12>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "SELECT * FROM TBLYTYC WHERE PUBLISHDATE = to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE TBLYTYC SET WEATERSTATE='@WEATERSTATE', TEMPERATURE='@TEMPERATURE', WINDSPEED='@WINDSPEED', WINDDIRECTION='@WINDDIRECTION', WAVEHEIGHT='@WAVEHEIGHT' WHERE PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    string sqlinsert = "INSERT INTO TBLYTYC"
                       + " (PUBLISHDATE, FORECASTDATE, WEATERSTATE, TEMPERATURE, WINDSPEED, WINDDIRECTION, WAVEHEIGHT)"
                       + " VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'), to_date('@FORECASTDATE','yyyy-mm-dd hh24@mi@ss'), '@WEATERSTATE', '@TEMPERATURE', '@WINDSPEED', '@WINDDIRECTION', '@WAVEHEIGHT')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getAmShort12, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 页脚 填报信息
        public static string setPublishMetaInfo(string type, string datajson)
        {
            ModelEditResponse result = new ModelEditResponse();
            List<string> typelist = new List<string>() { "fl", "sw", "cx" };
            // 判断输入参数 用户类型
            if (datajson != "")
            {
                // 判断传入的数据有效性
                List<ModelPublishMetaInfo> datalist = JsonConvert.DeserializeObject<List<ModelPublishMetaInfo>>(datajson);
                if (datalist.Count > 0)
                {
                    // 判断数据库中是否已有该日期数据
                    DateTime pubdate = Convert.ToDateTime(datalist[0].PUBLISHDATE.ToLocalTime().ToString("yyyy/MM/dd"));
                    string sqlselect = "select * from TBLFOOTER where PUBLISHDATE=to_date('" + pubdate.ToString("yyyy-MM-dd") + "', 'yyyy-mm-dd hh24@mi@ss')";
                    string sqlupdate = "UPDATE TBLFOOTER set ";
                    string sqlinsert = "INSERT INTO TBLFOOTER ";
                    switch (type)
                    {
                        case "fl":
                            sqlupdate += "PUBLISHHOUR='@PUBLISHHOUR',FRELEASEUNIT='@FRELEASEUNIT',FTELEPHONE='@FTELEPHONE',FFAX='@FFAX',FWAVEFORECASTER='@FWAVEFORECASTER',FWAVEFORECASTERTEL='@FWAVEFORECASTERTEL',ZHIBANTEL='@ZHIBANTEL',SENDTEL='@SENDTEL' ";
                            sqlinsert += "(PUBLISHDATE,PUBLISHHOUR,FRELEASEUNIT,FTELEPHONE,FFAX,FWAVEFORECASTER,FWAVEFORECASTERTEL,ZHIBANTEL,SENDTEL) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),'@PUBLISHHOUR','@FRELEASEUNIT','@FTELEPHONE','@FFAX','@FWAVEFORECASTER','@FWAVEFORECASTERTEL','@ZHIBANTEL','@SENDTEL')";
                            break;
                        case "sw":
                            sqlupdate += "PUBLISHHOUR='@PUBLISHHOUR',FRELEASEUNIT='@FRELEASEUNIT',FTELEPHONE='@FTELEPHONE',FFAX='@FFAX',FWATERTEMPERATUREFORECASTER='@FWATERTEMPERATUREFORECASTER',FWATERTEMPERATUREFORECASTERTEL='@FWATERTEMPERATUREFORECASTERTEL',ZHIBANTEL='@ZHIBANTEL',SENDTEL='@SENDTEL' ";
                            sqlinsert += "(PUBLISHDATE,PUBLISHHOUR,FRELEASEUNIT,FTELEPHONE,FFAX,FWATERTEMPERATUREFORECASTER,FWATERTEMPERATUREFORECASTERTEL,ZHIBANTEL,SENDTEL) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),'@PUBLISHHOUR','@FRELEASEUNIT','@FTELEPHONE','@FFAX','@FWATERTEMPERATUREFORECASTER','@FWATERTEMPERATUREFORECASTERTEL','@ZHIBANTEL','@SENDTEL')";
                            break;
                        case "cx":
                            sqlupdate += "PUBLISHHOUR='@PUBLISHHOUR',FRELEASEUNIT='@FRELEASEUNIT',FTELEPHONE='@FTELEPHONE',FFAX='@FFAX',FTIDALFORECASTER='@FTIDALFORECASTER',FTIDALFORECASTERTEL='@FTIDALFORECASTERTEL',ZHIBANTEL='@ZHIBANTEL',SENDTEL='@SENDTEL' ";
                            sqlinsert += "(PUBLISHDATE,PUBLISHHOUR,FRELEASEUNIT,FTELEPHONE,FFAX,FTIDALFORECASTER,FTIDALFORECASTERTEL,ZHIBANTEL,SENDTEL) ";
                            sqlinsert += "VALUES (to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss'),'@PUBLISHHOUR','@FRELEASEUNIT','@FTELEPHONE','@FFAX','@FTIDALFORECASTER','@FTIDALFORECASTERTEL','@ZHIBANTEL','@SENDTEL')";
                            break;
                        default: break;
                    }
                    sqlupdate += "where PUBLISHDATE=to_date('@PUBLISHDATE','yyyy-mm-dd hh24@mi@ss')";
                    submitTable(type, typelist, datalist, pubdate, sqlselect, sqlupdate, sqlinsert, getPublishMetaInfo, result);
                }
                else
                {
                    result.Description = "提交数据为空";
                }
            }
            else
            {
                result.Description = "提交数据为空";
            }

            return JsonConvert.SerializeObject(result);
        }

        // 提交表单
        private static void submitTable<T>(string usertype, List<string> validusertypes, List<T> datalist, DateTime pubdate, string sqlselect, string sqlupdate, string sqlinsert, GetTableMethod getTable, ModelEditResponse result) where T : class
        {
            int executioncount = 0;
            // 判断输入参数 用户类型
            if (validusertypes.Contains(usertype))
            {
                // 判断数据库中是否已有该日期数据
                DataTable dtselect = OperatorSQL.queryData(sqlselect);
                string sql = "";
                if (dtselect.Rows.Count > 0)
                {
                    // Update
                    result.Description = "修改记录";
                    sql = sqlupdate;
                }
                else
                {
                    // Insert
                    result.Description = "新增记录";
                    sql = sqlinsert;
                }
                foreach (T data in datalist)
                {
                    List<DbParameter> dbParameters = OperatorSQL.buildParameters(data);
                    executioncount += OperatorSQL.executeSql(sql, dbParameters);
                }
                if (executioncount > 0)
                {
                    result.Success = true;
                    result.AffectedRowCount = executioncount;
                    List<int> newfakelist = new List<int>() { 0 };
                    List<T> newdata = (List<T>)getTable(pubdate, newfakelist, 0);
                    result.NewFakeData = newfakelist[0];
                    foreach (T model in newdata)
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
                result.Description = "预报员类型不符";
            }
        }

        #endregion

        #region 上午短期预报 生成Word方法
        public static string createWordDoc(DateTime publishdate, int publishhour, ModelAmShortReport report)
        {
            string templatepath = getAMShortTemplatePath(report.reportTitle);
            string folderpath = Path.GetDirectoryName(templatepath);
            if (!Directory.Exists(folderpath))
            {
                System.Diagnostics.Debug.WriteLine("模板文件夹不存在");
                Directory.CreateDirectory(folderpath);
            }
            if (!File.Exists(templatepath))
            {
                System.Diagnostics.Debug.WriteLine("模板文件不存在");
                return "找不到模板文件";
            }
            Document document = new Document();
            try
            {
                document.LoadFromFile(templatepath);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "模板文件被占用";
            }

            List<int> boollist = new List<int>() { 0 };
            foreach (int number in report.datasource)
            {
                switch (number)
                {
                    case 0:
                        List<ModelPublishMetaInfo> table0 = getPublishMetaInfo(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table0, document);
                        OperatorDOC.replaceDocComments(table0, document);
                        break;
                    case 1:
                        List<ModelAmShort1> table1 = getAmShort1(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table1, document);
                        OperatorDOC.replaceDocComments(table1, document);
                        break;
                    case 2:
                        List<ModelAmShort2> table2 = getAmShort2(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table2, document);
                        OperatorDOC.replaceDocComments(table2, document);
                        break;
                    case 3:
                        List<ModelAmShort3and4> table3 = getAmShort3and4(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table3, document);
                        OperatorDOC.replaceDocComments(table3, document);
                        break;
                    case 4:
                        List<ModelAmShort3and4> table4 = getAmShort3and4(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table4, document);
                        OperatorDOC.replaceDocComments(table4, document);
                        break;
                    case 5:
                        List<ModelAmShort5> table5 = getAmShort5(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table5, document);
                        OperatorDOC.replaceDocComments(table5, document);
                        break;
                    case 6:
                        List<ModelAmShort6> table6 = getAmShort6(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table6, document);
                        OperatorDOC.replaceDocComments(table6, document);
                        break;
                    case 7:
                        List<ModelAmShort7> table7 = getAmShort7(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table7, document);
                        OperatorDOC.replaceDocComments(table7, document);
                        break;
                    case 8:
                        List<ModelAmShort8> table8 = getAmShort8(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table8, document);
                        OperatorDOC.replaceDocComments(table8, document);
                        break;
                    case 9:
                        List<ModelAmShort9> table9 = getAmShort9(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table9, document);
                        OperatorDOC.replaceDocComments(table9, document);
                        break;
                    case 10:
                        List<ModelAmShort10> table10 = getAmShort10(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table10, document);
                        OperatorDOC.replaceDocComments(table10, document);
                        break;
                    case 11:
                        List<ModelAmShort11> table11 = getAmShort11(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table11, document);
                        OperatorDOC.replaceDocComments(table11, document);
                        break;
                    case 12:
                        List<ModelAmShort12> table12 = getAmShort12(publishdate, boollist, 0);
                        OperatorDOC.replaceDocStringMarkers(table12, document);
                        OperatorDOC.replaceDocComments(table12, document);
                        break;
                    default: break;
                }
            }

            string outputfilepath = getAmShortOutputPath(report.reportTitle, publishdate, publishhour);
            string outputfolderpath = Path.GetDirectoryName(outputfilepath);
            if (!Directory.Exists(outputfolderpath))
            {
                System.Diagnostics.Debug.WriteLine("输出文件夹不存在");
                Directory.CreateDirectory(outputfolderpath);
            }
            try
            {
                document.SaveToFile(outputfilepath, FileFormat.Docx);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "输出文件时出错";
            }

            //System.Drawing.Image[] images = document.SaveToImages(global::Spire.Doc.Documents.ImageType.Metafile);
            //for (int i = 0; i < images.Length; i++)
            //{
            //    images[i].Save(Path.Combine(outputfolderpath, "生成的预报单4_" + i + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            //}
            return "完成";
        }

        public static string getAMShortTemplatePath(string reporttitle)
        {
            string result = "";
            string folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebServiceClass", "WordTemplates", "AMShort");

            switch (reporttitle)
            {
                case "4号预报单(a4)-2014":
                    result = Path.Combine(folderpath, "4号预报单.docx");
                    break;
                case "5号预报单（a4）":
                    result = Path.Combine(folderpath, "5号预报单.doc");
                    break;
                case "6号预报单（a4）":
                    result = Path.Combine(folderpath, "6号预报单.doc");
                    break;
                case "7号海洋水温海冰预报":
                    result = Path.Combine(folderpath, "7号海洋水温海冰预报.doc");
                    break;
                case "20号潍坊市海洋预报台专项预报(10时)":
                    result = Path.Combine(folderpath, "20号潍坊市海洋预报台专项预报.docx");
                    break;
                case "24号东营专项预报":
                    result = Path.Combine(folderpath, "24号东营近海.docx");
                    break;
                case "26号预报单":
                    result = Path.Combine(folderpath, "26号预报单.docx");
                    break;
                case "海上丝绸之路预报":
                    result = Path.Combine(folderpath, "海上丝绸之路.docx");
                    break;
                case "上午的指挥处预报":
                    result = Path.Combine(folderpath, "指挥处预报单.doc");
                    break;
                case "海阳近岸专项预报单":
                    result = Path.Combine(folderpath, "海阳近岸专项预报单.docx");
                    break;
                default: break;
            }
            return result;
        }

        public static string getAmShortOutputPath(string reporttitle, DateTime publishdate, int publishhour)
        {
            string result = "";
            string folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "预报单共享", "duanqi", publishdate.Year.ToString() + publishdate.Month.ToString() + publishdate.Day.ToString());
            string zhihuichufolderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "预报单共享", "指挥处预报", "上午");
            string sichouzhilufolderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "预报单共享", "海上丝绸之路", publishdate.Year.ToString() + publishdate.Month.ToString() + publishdate.Day.ToString());

            switch (reporttitle)
            {
                case "4号预报单(a4)-2014":
                    result = Path.Combine(folderpath, "YB_SLOF_ZX_72hr_" + publishdate.ToString("yyyyMMdd") + publishhour + "_NMFC.doc");
                    break;
                case "5号预报单（a4）":
                    result = Path.Combine(folderpath, "YB_SLOF_ZX_7day_" + publishdate.ToString("yyyyMMdd") + "_NMFC.doc");
                    break;
                case "6号预报单（a4）":
                    result = Path.Combine(folderpath, "YB_SLOF_CXZX_7day_" + publishdate.ToString("yyyyMMdd") + "_NMFC.doc");
                    break;
                case "7号海洋水温海冰预报":
                    result = Path.Combine(folderpath, "YB_SLOF_HBHWZX_7day_" + publishdate.ToString("yyyyMMdd") + "_NMFC.doc");
                    break;
                case "20号潍坊市海洋预报台专项预报(10时)":
                    result = Path.Combine(folderpath, "YB_WF_YBTZX_24hr_" + publishdate.ToString("yyyyMMdd") + publishhour + "_NMFC.doc");
                    break;
                case "24号东营专项预报":
                    result = Path.Combine(folderpath, "YB_DY_ZX_24hr_" + publishdate.ToString("yyyyMMdd") + publishhour + "_NMFC.doc");
                    break;
                case "26号预报单":
                    result = Path.Combine(folderpath, "YB_DY_SXGZX_72hr_" + publishdate.ToString("yyyyMMdd") + publishhour + "_NMFC.doc");
                    break;
                case "海上丝绸之路预报":
                    result = Path.Combine(sichouzhilufolderpath, "YB_SS_ZX_72hr_" + publishdate.ToString("yyyyMMdd") + "_NMFC.doc");
                    break;
                case "上午的指挥处预报":
                    result = Path.Combine(zhihuichufolderpath, "YB_ZHC_ZX_72hr_" + publishdate.ToString("yyyyMMdd") + publishhour + "_NMFC.doc");
                    break;
                case "海阳近岸专项预报单":
                    result = Path.Combine(folderpath, "YB_HY_ZX_24hr_" + publishdate.ToString("yyyyMMdd") + publishhour + "_NMFC.doc");
                    break;
                default: break;
            }
            return result;
        }

        #endregion

        #region Helper Methods

        private static void fillModelAmShort2List(List<ModelAmShort2> list, int index, DataRow datarow)
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
        private static void fillModelAmShort6List(List<ModelAmShort6> list, int index, DataRow datarow)
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
        private static void fillModelAmShort7List(List<ModelAmShort7> list, int startindex, DataRow datarow)
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
        private static void fillModelAmShort8List(List<ModelAmShort8> list, int index, DataRow datarow)
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

    }
}