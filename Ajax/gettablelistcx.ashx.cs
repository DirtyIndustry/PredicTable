using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace PredicTable.Ajax
{
    /// <summary>
    /// gettablelistcx 的摘要说明
    /// </summary>
    public class gettablelistcx : IHttpHandler, IRequiresSessionState
    {
        string userid;
        string ftpIp = System.Configuration.ConfigurationManager.AppSettings["ftpIp"].ToString();
        string ftpUserName = System.Configuration.ConfigurationManager.AppSettings["ftpUserName"].ToString();
        string ftpPwd = System.Configuration.ConfigurationManager.AppSettings["ftpPwd"].ToString();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string method = context.Request["method"].ToString();
                switch (method)
                {
                    case "getbydataPMCX": getbydataPMCX(context); break;//下午
                    case "getbaseinfo": getbaseinfo(context); break;
                    case "setsession": setsession(context); break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("操作表单数据出错" + ex.ToString());
            }
        }
        /// <summary>
        /// 设置跨域session
        /// </summary>
        /// <param name="context"></param>
        void setsession(HttpContext context)
        {
            try
            {
                context.Session["userid"] = context.Request.Form["userids"];
                context.Session["type"] = context.Request.Form["types"];
                context.Response.Write("success");

                //HttpCookie cookie = new HttpCookie("MyCook");//初使化并设置Cookie的名称
                //cookie.Values.Add("userid", context.Request.Form["userids"]);
                //cookie.Values.Add("type", context.Request.Form["types"]);
                //cookie.Expires = DateTime.MaxValue;
                //context.Response.AppendCookie(cookie);
            }
            catch (Exception ex)
            {
                WriteLog.Write("跨域设置session失败。" + ex.ToString());
                context.Response.Write("error");
            }

        }
        /// <summary>
        /// 返回填报基本信息
        /// </summary>
        /// <param name="context"></param>
        void getbaseinfo(HttpContext context)
        {
            sql_TBLREPORTSCOMMONMESS baseinfo = new sql_TBLREPORTSCOMMONMESS();
            DataTable dtinfo = (DataTable)baseinfo.GetTBLREPORTSCOMMONMESS();
            if (dtinfo.Rows.Count > 0)
            {
                context.Response.Write(dtinfo.Rows[0]["RCMPUBLISHSECTOR"] + "," + dtinfo.Rows[0]["RCMTELLPHONE"] + "," + dtinfo.Rows[0]["RCMFAX"]);
            }
        }
        /// <summary>
        /// 根据时间查询数据(下午潮汐)
        /// </summary>
        /// <param name="context"></param>
        public void getbydataPMCX(HttpContext context)
        {
            var date = DateTime.Parse(context.Request["date"].ToString());
            var searchType = context.Request["searchtype"].ToString();//searchtype 按填报日期还是预报日期查询 p:填报日期 f:预报日期
            StringBuilder sb_str = new StringBuilder();
            sb_str.Append("[");
            
            sb_str.Append(gettable07(date));//下午3
            WriteLog.WriteDebug("PM-07");
            
            sb_str.Append(gettable10(date, searchType));//下午5
            WriteLog.WriteDebug("PM-10");
            
            sb_str.Append(gettable12(date, searchType));//下午7
            WriteLog.WriteDebug("PM-12");
            
            sb_str.Append(gettable18(date));//下午12
            WriteLog.WriteDebug("PM-18");
            
            
            sb_str.Append(gettable22(date, searchType));//下午16
            WriteLog.WriteDebug("PM-22");
            
            sb_str.Append(gettable42(date, searchType));//下午十18
            WriteLog.WriteDebug("PM-42");
            
            sb_str.Append(gettable46(date));//下午3
            WriteLog.WriteDebug("PM-46");

            sb_str.Append(gettable48(date));//下午20
            WriteLog.WriteDebug("PM-48");

            sb_str.Append(gettable54(date, searchType));//下午22
            WriteLog.WriteDebug("PM-54");
            
            
            sb_str.Append(gettable57(date, searchType));//下午25
            WriteLog.WriteDebug("PM-57");
            
            
            sb_str.Append(gettable60(date, searchType));//下午28
            WriteLog.WriteDebug("PM-60");
            
            
            sb_str.Append(gettable63(date, searchType));//下午31
            WriteLog.WriteDebug("PM-63");
           

            sb_str.Append(gettable66(date, searchType));//下午34
            WriteLog.WriteDebug("PM-66");
            
            
            sb_str.Append(gettable69(date, searchType));//下午37
            WriteLog.WriteDebug("PM-69");
            
            sb_str.Append(gettable72(date, searchType));//下午40
            WriteLog.WriteDebug("PM-72");
            


            context.Response.Write(sb_str.Replace("[,{", "[{").ToString() + "]");


        }
        /// <summary>
        /// 表单07数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable07(DateTime data)
        {
            sql_TBLSDOFFSHORESEVENCITY24HTIDE sql = new sql_TBLSDOFFSHORESEVENCITY24HTIDE();
            TBLSDOFFSHORESEVENCITY24HTIDE model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(model);
            StringBuilder sb_str = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"cx7\",\"children\":");
                var result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }
        /// <summary>
        /// 表单10数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable10(DateTime data, string searchType = "p")
        {
            sql_TBLMZZTIDELEVEL sql = new sql_TBLMZZTIDELEVEL();
            TBLMZZTIDELEVEL model = new TBLMZZTIDELEVEL();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.get_TBLMZZTIDELEVEL_AllData(model); 
            StringBuilder sb_str = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"cx10\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"g1s\":\"" + dt.Rows[i]["MZZTLFIRSTWAVEOFTIME"]
                        + "\",\"g1c\":\"" + dt.Rows[i]["MZZTLFIRSTWAVETIDELEVEL"]
                        + "\",\"d1s\":\"" + dt.Rows[i]["MZZTLFIRSTTIMELOWTIDE"]
                        + "\",\"d1c\":\"" + dt.Rows[i]["MZZTLLOWTIDELEVELFORTHEFIRSTTI"]
                        + "\",\"g2s\":\"" + dt.Rows[i]["MZZTLSECONDWAVEOFTIME"]
                        + "\",\"g2c\":\"" + dt.Rows[i]["MZZTLSECONDWAVETIDELEVEL"]
                        + "\",\"d2s\":\"" + dt.Rows[i]["MZZTLSECONDTIMELOWTIDE"]
                        + "\",\"d2c\":\"" + dt.Rows[i]["MZZTLLOWTIDELEVELFORTHESECONDT"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }
        /// <summary>
        /// 表单12数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable12(DateTime data, string searchType = "p")
        {
            sql_TBLNANPUOILFIELDTIDALFORECAST sql = new sql_TBLNANPUOILFIELDTIDALFORECAST();
            TBLNANPUOILFIELDTIDALFORECAST model = new TBLNANPUOILFIELDTIDALFORECAST();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.get_TBLNANPUOILFIELDTIDALFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"cx12\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
            }
            return sb_str.ToString();
        }
        /// <summary>
        /// 表单18数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable18(DateTime data)
        {
            sql_TBLGOLDBEACH24HTIDALFORECAST sql = new sql_TBLGOLDBEACH24HTIDALFORECAST();
            TBLGOLDBEACH72HTIDALFORECAST model = new TBLGOLDBEACH72HTIDALFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLGOLDBEACH24HTIDALFORECAST_AllData(model);

            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();

                sb_str.Append(",{ \"type\":\"cx18\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }
        /// <summary>
        /// 表单22数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable22(DateTime data, string searchType)
        {
            sql_TBLWEIHAISHIDAOTIDALFORECAST sql = new sql_TBLWEIHAISHIDAOTIDALFORECAST();
            TBLWEIHAISHIDAOTIDALFORECAST model = new TBLWEIHAISHIDAOTIDALFORECAST();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.get_TBLWEIHAISHIDAOTIDALFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"cx22\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["REPORTAREA"]
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"g1c\":\"" + dt.Rows[i]["FIRSTHIGHWAVEHEIGHT"]
                        + "\",\"g1s\":\"" + dt.Rows[i]["FIRSTHIGHWAVETIME"]
                        + "\",\"d1c\":\"" + dt.Rows[i]["FIRSTLOWWAVEHEIGHT"]
                        + "\",\"d1s\":\"" + dt.Rows[i]["FIRSTLOWWAVETIME"]
                        + "\",\"g2c\":\"" + dt.Rows[i]["SECONDHIGHWAVEHEIGHT"]
                        + "\",\"g2s\":\"" + dt.Rows[i]["SECONDHIGHWAVETIME"]
                        + "\",\"d2c\":\"" + dt.Rows[i]["SECONDLOWWAVEHEIGHT"]
                        + "\",\"d2s\":\"" + dt.Rows[i]["SECONDLOWWAVETIME"] + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }
        /// <summary>
        /// 东营埕岛-未来三天高/低潮预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable42(DateTime data, string searchType)
        {
            HT_DYTIDEFORECAST model = new HT_DYTIDEFORECAST();

            Sql_DYTIDEFOREAST sql = new Sql_DYTIDEFOREAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetDyTideForecastData(model);

            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"cx42\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 下午三潮汐潮高数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable46(DateTime data)
        {
            sql_TideData sql = new sql_TideData();
            HT_TideData model = new HT_TideData();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.getTideData(model);
            StringBuilder sb_str = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"cx46\",\"children\":");
                var result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }
        /// <summary>
        /// 表单02.二、海洋牧场-潮汐预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string gettable48(DateTime date)
        {
            OceanRanchTide oceanRanchTide = new OceanRanchTide();
            sql_OceanRanchTide sql = new sql_OceanRanchTide();
            oceanRanchTide.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.GetOceanRanchTideList(oceanRanchTide);//获取OCEANRANCH72HWAVE_T当天保存的数据
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"cx48\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            else
            {
                return "";
            }

        }
        /// <summary>
        /// 下午二十四、东营广利渔港-未来三天高/低潮预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable54(DateTime data, string searchType)
        {
            TBLSEVENTIDE model = new TBLSEVENTIDE();

            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTideData("DYGLFP", model);
    
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"cx54\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }
        /// <summary>
        /// 下午二十七、日照桃花岛-未来三天高/低潮预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable57(DateTime data, string searchType)
        {
            TBLSEVENTIDE model = new TBLSEVENTIDE();

            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTideData("RZTHD", model);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"cx57\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }
        /// <summary>
        /// 下午三十、潍坊度假区-未来三天高/低潮预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable60(DateTime data, string searchType)
        {
            TBLSEVENTIDE model = new TBLSEVENTIDE();

            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTideData("WFDJQ", model);
          
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"cx60\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }
        /// <summary>
        /// 下午三十三、威海新区-未来三天高/低潮预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable63(DateTime data, string searchType)
        {
            TBLSEVENTIDE model = new TBLSEVENTIDE();

            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTideData("WHXQ", model);
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.GetTideData("WHXQ", model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"cx63\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }
        /// <summary>
        /// 下午三十六、烟台清泉-未来三天高/低潮预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable66(DateTime data, string searchType)
        {
            TBLSEVENTIDE model = new TBLSEVENTIDE();

            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTideData("YTQQ", model);
           
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"cx66\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }
        /// <summary>
        /// 下午三十九、董家口-未来三天高/低潮预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable69(DateTime data, string searchType)
        {
            TBLSEVENTIDE model = new TBLSEVENTIDE();

            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTideData("DJKP", model);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"cx69\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }
        /// <summary>
        /// 下午四十二、东营渔港-未来三天高/低潮预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable72(DateTime data, string searchType)
        {
            TBLSEVENTIDE model = new TBLSEVENTIDE();

            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTideData("DYFP", model);
          
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"cx72\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}