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
    /// OceanRanchList 的摘要说明
    /// </summary>
    public class OceanRanchList : IHttpHandler, IRequiresSessionState
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
                    case "GetOceanListData": GetOceanListData(context); break;
                    case "SubmitOceanListData": SubmitOceanListData(context); break;
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
        #region  获取海洋牧场数据

        /// <summary>
        /// 获取海洋牧场数据
        /// </summary>
        private void GetOceanListData(HttpContext context)
        {
            var date = DateTime.Parse(context.Request["date"].ToString());
            StringBuilder sb_str = new StringBuilder();
            sb_str.Append("[");
            sb_str.Append(gettabe01(date));
            sb_str.Append(gettabe02(date));
            sb_str.Append(gettabe03(date));
            context.Response.Write(sb_str.Replace("[,{", "[{").ToString() + "]");
        }

        /// <summary>
        /// 表单01.一、海洋牧场-海浪预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string gettabe01(DateTime date)
        {
            OceanRanchWave oceanRanchWave = new OceanRanchWave();
            oceanRanchWave.PUBLISHDATE = date;
            sql_OceanRanchWave sql = new sql_OceanRanchWave();
            DataTable dt = (DataTable)sql.GetOceanRanchWaveList(oceanRanchWave);//获取OCEANRANCH72HWAVE_T当天保存的数据
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t47\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            dt = (DataTable)sql.GetOceanRanchWaveListBy_S(oceanRanchWave);//获取OCEANRANCH72HWAVE_S表中解析数据
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t47\",\"children\":");
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
        private string gettabe02(DateTime date)
         {
            OceanRanchTide oceanRanchTide = new OceanRanchTide();
            sql_OceanRanchTide sql = new sql_OceanRanchTide();
            oceanRanchTide.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.GetOceanRanchTideList(oceanRanchTide);//获取OCEANRANCH72HWAVE_T当天保存的数据
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t48\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            oceanRanchTide.PUBLISHDATE = date.AddDays(1);
            dt = (DataTable)sql.GetOceanRanchTideListBy_S(oceanRanchTide);//获取OCEANRANCH72HWAVE_S表中解析数据
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t48\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }


        /// <summary>
        /// 表单03.三、海洋牧场-海温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string gettabe03(DateTime date)
        {
            OceanRanchTemp oceanRanchTemp = new OceanRanchTemp();
            sql_OceanRanchTemp sql = new sql_OceanRanchTemp();
            oceanRanchTemp.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.GetOceanRanchTempList(oceanRanchTemp);//获取OCEANRANCH72HWAVE_T当天保存的数据
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t49\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            dt = (DataTable)sql.GetOceanRanchTempListBy_S(oceanRanchTemp);//获取OCEANRANCH72HWAVE_S表中解析数据
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t49\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }

        /// <summary>
        /// 填报信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe23(DateTime data)
        {
            sql_TBLFOOTER sql = new sql_TBLFOOTER();
            TBLFOOTER model = new TBLFOOTER();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLFOOTER_AllData(model);
            if (dt == null || dt.Rows.Count < 1)
            {
                dt = (DataTable)sql.GetTblFooterLastDay(model);
            }
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t23\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"rq\":\"" + dt.Rows[i]["PUBLISHDATE"]
                        + "\",\"xs\":\"" + dt.Rows[i]["PUBLISHHOUR"]
                        + "\",\"fb\":\"" + dt.Rows[i]["FRELEASEUNIT"]
                        + "\",\"dh\":\"" + dt.Rows[i]["FTELEPHONE"]
                        + "\",\"cz\":\"" + dt.Rows[i]["FFAX"]
                        + "\",\"hl\":\"" + dt.Rows[i]["FWAVEFORECASTER"]
                        + "\",\"cx\":\"" + dt.Rows[i]["FTIDALFORECASTER"]
                        + "\",\"sw\":\"" + dt.Rows[i]["FWATERTEMPERATUREFORECASTER"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region  提交海洋牧场数据

        /// <summary>
        /// 提交海洋牧场数据
        /// </summary>
        private void SubmitOceanListData(HttpContext context)
        {
            DateTime date = DateTime.Parse(context.Request["date"].ToString());
            string type = context.Request["type"].ToString();
            string data = context.Request.Form["datas"].ToString();
            string quanxian = context.Session["type"].ToString();
            string strMsg = "";//返回信息
            string logmsg = "";
            string logdaima = "";
            switch (type)
            {
                case "47":
                    strMsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable01(date, data);//海浪预报
                    break;
                case "48"://潮汐预报
                    strMsg += (quanxian.ToLower() != "cx") ? "editsuccess" : setTable02(date, data);
                    if (quanxian.ToLower() == "cx")
                    {
                        TideCurve(date);//生成潮汐数据图片并上传到ftp服务器
                    }
                    break;
                case "49":
                    strMsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable03(date, data);//海温预报
                    break;
                case "23":
                    strMsg = setTable23(date, data, quanxian); //填报信息
                    break;
                default:
                    break;
            }
            switch (strMsg)
            {
                case "addsuccess": logdaima = "add_table"; logmsg = "新增表单" + type + "数据成功！"; break;
                case "adderror": logdaima = "add_table"; logmsg = "新增表单" + type + "数据失败！"; break;
                case "editsuccess": logdaima = "edit_table"; logmsg = "修改表单" + type + "数据成功！"; break;
                case "editerror": logdaima = "edit_table"; logmsg = "修改表单" + type + "数据失败！"; break;
                default:
                    break;
            }
            Sql_Caozuorizhi.WriteRizhi(userid, logdaima, logmsg);
            context.Response.Write(strMsg);
        }

        /// <summary>
        /// 表单01.一、海洋牧场-海浪预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        private string setTable01(DateTime date,string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            OceanRanchWave oceanRanchWave = new OceanRanchWave();
            oceanRanchWave.PUBLISHDATE = date;
            sql_OceanRanchWave sql = new sql_OceanRanchWave();
            DataTable dt = (DataTable)sql.GetOceanRanchWaveList(oceanRanchWave);
            if(dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            
            for(int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        oceanRanchWave.OCEANRANCHNAME = "长青海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "长青"; //海洋牧场短名称
                        oceanRanchWave.SN = "cqi"; //缩写
                        break;
                    case 1:
                        oceanRanchWave.OCEANRANCHNAME = "荣成烟墩角游钓型海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "烟墩角"; //海洋牧场短名称
                        oceanRanchWave.SN = "ydj"; //缩写
                        break;
                    case 2:
                        oceanRanchWave.OCEANRANCHNAME = "西霞口集团国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "西霞口"; //海洋牧场短名称
                        oceanRanchWave.SN = "xxk"; //缩写
                        break;
                }
                
                oceanRanchWave.PUBLISHDATE = date;//填报日期
                oceanRanchWave.FORECASTDATE = Convert.ToDateTime(tbdata[(i * 7)]);   //预报日期

                oceanRanchWave.WAVE24HDAY = tbdata[(i * 7) + 1];  //24小时最大有效波高白天
                oceanRanchWave.WAVE24HNEIGHT = tbdata[(i * 7) + 2];  //24小时最大有效波高夜晚
                oceanRanchWave.WAVE48HDAY = tbdata[(i * 7) + 3]; //48小时最大有效波高白天
                oceanRanchWave.WAVE48HNEIGHT = tbdata[(i * 7) + 4]; //48小时最大有效波高夜晚
                oceanRanchWave.WAVE72HDAY = tbdata[(i * 7) + 5]; //72小时最大有效波高白天
                oceanRanchWave.WAVE72HNEIGHT = tbdata[(i * 7) + 6]; //72小时最大有效波高夜晚

                if (type == "add")
                {
                    addnum += sql.InsertOceanRanchWaveList(oceanRanchWave);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditOceanRanchWaveList(oceanRanchWave);
                }
            }

            if (type == "add")
            {
                if(addnum == 3)
                {
                    return "addsuccess";
                }
                else
                {
                    return "adderror";
                }
            }
            else if (type == "edit")
            {
                if (editnum == 3)
                {
                    return "editsuccess";
                }
                else
                {
                    return "editerror";
                }
            }

            return "";
        }

        /// <summary>
        /// 表单02.二、海洋牧场-潮汐预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        private string setTable02(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            OceanRanchTide oceanRanchTide = new OceanRanchTide();
            sql_OceanRanchTide sql = new sql_OceanRanchTide();
            oceanRanchTide.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.GetOceanRanchTideList(oceanRanchTide);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        oceanRanchTide.OCEANRANCHNAME = "威海长青国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "长青"; //海洋牧场短名称
                        oceanRanchTide.SN = "cqi"; //缩写
                        break;
                    case 1:
                        oceanRanchTide.OCEANRANCHNAME = "荣成烟墩角游钓型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "烟墩角"; //海洋牧场短名称
                        oceanRanchTide.SN = "ydj"; //缩写
                        break;
                    case 2:
                        oceanRanchTide.OCEANRANCHNAME = "西霞口集团国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "西霞口"; //海洋牧场短名称
                        oceanRanchTide.SN = "xxk"; //缩写
                        break;
                }
                oceanRanchTide.PUBLISHDATE = date;
                oceanRanchTide.FORECASTDATE = Convert.ToDateTime(tbdata[(i * 33) + 32]);
                //oceanRanchTide.OCEANRANCHNAME = "西霞口集团国家级海洋牧场";
                //oceanRanchTide.OCEANRANCHSHORTNAME = "西霞口";
                //oceanRanchTide.SN = "xxk";tbdata[(i * 7) + 1]
                oceanRanchTide.TIDE24H00 = tbdata[(i * 33)];
                oceanRanchTide.TIDE24H01 = tbdata[(i * 33) + 1];
                oceanRanchTide.TIDE24H02 = tbdata[(i * 33) + 2];
                oceanRanchTide.TIDE24H03 = tbdata[(i * 33) + 3];
                oceanRanchTide.TIDE24H04 = tbdata[(i * 33) + 4];
                oceanRanchTide.TIDE24H05 = tbdata[(i * 33) + 5];
                oceanRanchTide.TIDE24H06 = tbdata[(i * 33) + 6];
                oceanRanchTide.TIDE24H07 = tbdata[(i * 33) + 7];
                oceanRanchTide.TIDE24H08 = tbdata[(i * 33) + 8];
                oceanRanchTide.TIDE24H09 = tbdata[(i * 33) + 9];
                oceanRanchTide.TIDE24H10 = tbdata[(i * 33) + 10];
                oceanRanchTide.TIDE24H11 = tbdata[(i * 33) + 11];
                oceanRanchTide.TIDE24H12 = tbdata[(i * 33) + 12];
                oceanRanchTide.TIDE24H13 = tbdata[(i * 33) + 13];
                oceanRanchTide.TIDE24H14 = tbdata[(i * 33) + 14];
                oceanRanchTide.TIDE24H15 = tbdata[(i * 33) + 15];
                oceanRanchTide.TIDE24H16 = tbdata[(i * 33) + 16];
                oceanRanchTide.TIDE24H17 = tbdata[(i * 33) + 17];
                oceanRanchTide.TIDE24H18 = tbdata[(i * 33) + 18];
                oceanRanchTide.TIDE24H19 = tbdata[(i * 33) + 19];
                oceanRanchTide.TIDE24H20 = tbdata[(i * 33) + 20];
                oceanRanchTide.TIDE24H21 = tbdata[(i * 33) + 21];
                oceanRanchTide.TIDE24H22 = tbdata[(i * 33) + 22];
                oceanRanchTide.TIDE24H23 = tbdata[(i * 33) + 23];

                oceanRanchTide.TIDEFIRSTHTIME = tbdata[(i * 33) + 24];
                oceanRanchTide.TIDEFIRSTHHEIGHT = tbdata[(i * 33) + 25];
                oceanRanchTide.TIDESECONDHTIME = tbdata[(i * 33) + 26];
                oceanRanchTide.TIDESECONDHHEIGHT = tbdata[(i * 33) + 27];
                oceanRanchTide.TIDEFIRSTLTIME = tbdata[(i * 33) + 28];
                oceanRanchTide.TIDEFIRSTLHEIGHT = tbdata[(i * 33) + 29];
                oceanRanchTide.TIDESECONDLTIME = tbdata[(i * 33) + 30];
                oceanRanchTide.TIDESECONDLHEIGHT = tbdata[(i * 33) + 31];
                if (type == "add")
                {
                    addnum += sql.InsertOceanRanchTideList(oceanRanchTide);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditOceanRanchTideList(oceanRanchTide);
                }
            }
            
            if (type == "add")
            {
                if (addnum > 0)
                {
                    return "addsuccess";
                }
                else
                {
                    return "adderror";
                }
            }
            else if (type == "edit")
            {
                if (editnum > 0)
                {
                    return "editsuccess";
                }
                else
                {
                    return "editerror";
                }
            }
            return "";
        }

        /// <summary>
        /// 表单03.三、海洋牧场-海温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        private string setTable03(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            OceanRanchTemp oceanRanchTemp = new OceanRanchTemp();
            oceanRanchTemp.PUBLISHDATE = date;
            sql_OceanRanchTemp sql = new sql_OceanRanchTemp();
            DataTable dt = (DataTable)sql.GetOceanRanchTempList(oceanRanchTemp);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        oceanRanchTemp.OCEANRANCHNAME = "长青海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "长青"; //海洋牧场短名称
                        oceanRanchTemp.SN = "cqi"; //缩写
                        break;
                    case 1:
                        oceanRanchTemp.OCEANRANCHNAME = "荣成烟墩角游钓型海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "烟墩角"; //海洋牧场短名称
                        oceanRanchTemp.SN = "ydj"; //缩写
                        break;
                    case 2:
                        oceanRanchTemp.OCEANRANCHNAME = "西霞口集团国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "西霞口"; //海洋牧场短名称
                        oceanRanchTemp.SN = "xxk"; //缩写
                        break;
                }

                oceanRanchTemp.PUBLISHDATE = date;//填报日期
                oceanRanchTemp.FORECASTDATE = Convert.ToDateTime(tbdata[(i * 4)]);   //预报日期

                oceanRanchTemp.TEMP24H = tbdata[(i * 4) + 1];  //24小时海温平均值
                oceanRanchTemp.TEMP48H = tbdata[(i * 4) + 2];  //48小时海温平均值
                oceanRanchTemp.TEMP72H = tbdata[(i * 4) + 3]; //72小时海温平均值

                if (type == "add")
                {
                    addnum += sql.InsertOceanRanchTempList(oceanRanchTemp);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditOceanRanchTempList(oceanRanchTemp);
                }
            }

            if (type == "add")
            {
                if (addnum == 3)
                {
                    return "addsuccess";
                }
                else
                {
                    return "adderror";
                }
            }
            else if (type == "edit")
            {
                if (editnum == 3)
                {
                    return "editsuccess";
                }
                else
                {
                    return "editerror";
                }
            }

            return "";
        }

        /// <summary>
        /// 填报信息
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        private string setTable23(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLFOOTER sql = new sql_TBLFOOTER();
            TBLFOOTER model = new TBLFOOTER();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLFOOTER_AllData(model);
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.PUBLISHHOUR = Convert.ToInt32(tbdata[0]); // 填报小时
            model.FRELEASEUNIT = tbdata[1]; // 发布单位
            model.FTELEPHONE = tbdata[2]; // 电话
            model.FFAX = tbdata[3]; //传真
            model.FWAVEFORECASTER = tbdata[4]; //海浪预报员
            model.FTIDALFORECASTER = tbdata[5]; // 潮汐预报员
            model.FWATERTEMPERATUREFORECASTER = tbdata[6]; // 水温预报员
            if (type == "add")
            {
                addnum += sql.Add_TBLFOOTER(model);
            }
            else if (type == "edit")
            {
                editnum += sql.Edit_TBLFOOTER(model, quanxian);
            }

            if (type == "add")
            {
                if (addnum == 1)
                {
                    return "addsuccess";
                }
                else
                {
                    return "adderror";
                }
            }
            else
            {
                if (editnum == 1)
                {
                    return "editsuccess";
                }
                else
                {
                    return "editerror";
                }
            }
        }

        /// <summary>
        /// 生成潮汐数据曲线图
        /// </summary>
        /// <returns></returns>
        private void TideCurve(DateTime date)
        {
            DateTime dtime = DateTime.Now;
            string OCEANRANCHNAME = "";
            string SN = "";
            for (int j = 0; j < 3; j++)
            {
                switch (j)
                {
                    case 0:
                        OCEANRANCHNAME = "威海长青国家级海洋牧场"; //海洋牧场长名称
                        SN = "cqi"; //缩写
                        break;
                    case 1:
                        OCEANRANCHNAME = "荣成烟墩角游钓型海洋牧场"; //海洋牧场长名称
                        SN = "ydj"; //缩写
                        break;
                    case 2:
                        OCEANRANCHNAME = "西霞口集团国家级海洋牧场"; //海洋牧场长名称
                        SN = "xxk"; //缩写
                        break;
                }
                List<float> list = this.GetTide(date, OCEANRANCHNAME);
                MyCurve curve = new MyCurve();
                curve.width = 1260;
                curve.height = 690;
                curve.top = 45;
                curve.bottom = 50;
                curve.left = 30;
                curve.right = 30;
                curve.xKeys = new string[] { "00:00\n" + dtime.ToString("MM-dd"),  "06:00\n" + dtime.ToString("MM-dd"),
                  "12:00\n" + dtime.ToString("MM-dd"),  "18:00\n" + dtime.ToString("MM-dd"),
                                         "00:00\n" + dtime.AddDays(1).ToString("MM-dd") };
                curve.values = list.ToArray();
                float max = list.Max();
                float min = list.Min();
                int ymax = ((int)(max / 10)) + 2;
                int ymin = ((int)(min / 10)) - 1;
                List<double> yvalues = new List<double>();
                for (int i = ymax; i >= ymin; i -= 2)
                {
                    yvalues.Add(i * 10);
                }
                curve.yKeys = new double[] { 500, 400, 300, 200, 100, 0 };
                //new double[] { 0.3, 0.2, 0.1, 0, -0.1 };
                curve.xybgColor = Color.White;
                curve.bgColor = Color.FromArgb(204, 204, 204);
                curve.grid = true;
                curve.title = "海洋牧场 24小时潮汐预报";
                Bitmap objBitmap = curve.CreateCurve();
                string fileName = "EL_" + SN + "_" + DateTime.Now.ToString("yyyyMMdd") + "_00_24hr.jpg";
                string docPath = System.AppDomain.CurrentDomain.BaseDirectory + "24小时潮汐预报图" + "\\" + date.ToString("yyyyMMdd");
                if (!Directory.Exists(docPath))
                {
                    Directory.CreateDirectory(docPath);
                }
                objBitmap.Save(System.IO.Path.Combine(docPath, fileName), ImageFormat.Jpeg);
                string path = System.IO.Path.Combine(docPath, fileName);
                this.UpToServer(path, fileName);
                //MSG += fileName + "生成成功\n";
            }
            //return MSG;
        }

       

        /// <summary>
        /// 获取潮汐数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private List<float> GetTide(DateTime date,string OCEANRANCHNAME)
        {
            sql_OceanRanchTide sql = new sql_OceanRanchTide();
            List<float> tideHeight = new List<float>();
            DataTable dt = (DataTable)sql.Get24TideDataList(date, OCEANRANCHNAME);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int j = 0; j < 24; j++)
                {
                    tideHeight.Add(float.Parse(dt.Rows[0][j].ToString()));
                }
            }
            date = date.AddDays(1);
            DataTable dt2 = (DataTable)sql.Get48TideDataListBy_S(date, OCEANRANCHNAME);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                for (int k = 0; k < 1; k++)
                {
                    tideHeight.Add(float.Parse(dt2.Rows[0][k].ToString()));
                }
            }
            return tideHeight;
        }

        /// <summary>
        /// 上传到ftp
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string UpToServer(string path,string fileName)
        {
            TideFtpClient.FTPUploadFile(ftpIp + "/" + DateTime.Now.ToString("yyyyMMdd") + "/Tide1", ftpUserName, ftpPwd, new System.IO.FileInfo(path));
            return fileName + "上传成功\n";
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}