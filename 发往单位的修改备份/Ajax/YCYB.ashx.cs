using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace PredicTable.Ajax
{
    /// <summary>
    /// YCYB 的摘要说明
    /// </summary>
    public class YCYB : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"].ToString();

            switch (method)
            {
                case "getbyALLDate_YCYB_WAVE": getbydata_YCYB_WAVE(context); break;
                case "getbyDate_YCYB_WAVE": getbyDataTime_YCYB_WAVE(context); break;
                case "getbyALLDate_YCYB_WIND": getbydata_YCYB_WIND(context); break;
                case "getbyDate_YCYB_WIND": getbyDataTime_YCYB_WIND(context); break;
                default:
                    break;
            }
        }

        public void getbydata_YCYB_WAVE(HttpContext context)
        {
            //string datetime = context.Request["datetime"].ToString();

            StringBuilder sb = new StringBuilder();
            Sql_YCYB sql_ycybwavezs = new Sql_YCYB();
            // Model model = new Model();
            // model.PUBLISHDATE = datetime;
            //  DataTable dt = (DataTable)sql_jxhzs.Get_JXHYB_AllData(model);
            DataTable dt = (DataTable)sql_ycybwavezs.Get_YCYB_DataByWAVE();
            if (dt.Rows.Count > 0)
            {

                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"FILENAME\":\"" + dt.Rows[i]["FILENAME"]
                        + "\",\"REGION\":\"" + dt.Rows[i]["REGION"]
                        + "\",\"FILETYPE\":\"" + dt.Rows[i]["FILETYPE"]
                        + "\",\"PUBLISHDATA\":\"" + dt.Rows[i]["PUBLISHDATA"]
                        + "\",\"EFFECTIVETIME\":\"" + dt.Rows[i]["EFFECTIVETIME"]
                        + "\",\"FLAG\":\"" + dt.Rows[i]["FLAG"]
                        + "\",\"FISHINGGROUNDID\":\"" + dt.Rows[i]["FISHINGGROUNDID"]
                        + "\",\"FISHINGGROUNDNAME\":\"" + dt.Rows[i]["FISHINGGROUNDNAME"]
                        + "\",\"EFFECTIVETIME\":\"" + dt.Rows[i]["EFFECTIVETIME"]
                        + "\",\"EFFECTIVEWAVEHEIGHT1\":\"" + dt.Rows[i]["EFFECTIVEWAVEHEIGHT1"]
                        + "\",\"TREND\":\"" + dt.Rows[i]["TREND"]
                        + "\",\"EFFECTIVEWAVEHEIGHT2\":\"" + dt.Rows[i]["EFFECTIVEWAVEHEIGHT2"] + "\"},");
                }
                if (dt.Rows.Count <= 0)
                {
                    context.Response.Write(sb.ToString() + "]");
                }
                else
                {
                    context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]");
                }
            }
            else
            {

            }

        }

        public void getbyDataTime_YCYB_WAVE(HttpContext context)
        {
            string datetime = context.Request["datetime"].ToString();
            StringBuilder sb = new StringBuilder();
            Sql_YCYB sql_ycybtimezs = new Sql_YCYB();
            YC_FILE yc_file = new YC_FILE();
            yc_file.PublishData = Convert.ToDateTime(datetime);
            DataTable dt = (DataTable)sql_ycybtimezs.Get_YCYB_DataTimeByWAVE(yc_file);
            if (dt.Rows.Count > 0)
            {

                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"FILENAME\":\"" + dt.Rows[i]["FILENAME"]
                         + "\",\"REGION\":\"" + dt.Rows[i]["REGION"]
                         + "\",\"FILETYPE\":\"" + dt.Rows[i]["FILETYPE"]
                         + "\",\"PUBLISHDATA\":\"" + dt.Rows[i]["PUBLISHDATA"]
                         + "\",\"EFFECTIVETIME\":\"" + dt.Rows[i]["EFFECTIVETIME"]
                         + "\",\"FLAG\":\"" + dt.Rows[i]["FLAG"]
                         + "\",\"FISHINGGROUNDID\":\"" + dt.Rows[i]["FISHINGGROUNDID"]
                         + "\",\"FISHINGGROUNDNAME\":\"" + dt.Rows[i]["FISHINGGROUNDNAME"]
                         + "\",\"EFFECTIVETIME\":\"" + dt.Rows[i]["EFFECTIVETIME"]
                         + "\",\"EFFECTIVEWAVEHEIGHT1\":\"" + dt.Rows[i]["EFFECTIVEWAVEHEIGHT1"]
                         + "\",\"TREND\":\"" + dt.Rows[i]["TREND"]
                         + "\",\"EFFECTIVEWAVEHEIGHT2\":\"" + dt.Rows[i]["EFFECTIVEWAVEHEIGHT2"] + "\"},");
                }
                if (dt.Rows.Count <= 0)
                {
                    context.Response.Write(sb.ToString() + "]");
                }
                else
                {
                    context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]");
                }
            }
            else
            {

            }

        }



        public void getbydata_YCYB_WIND(HttpContext context)
        {
            //string datetime = context.Request["datetime"].ToString();

            StringBuilder sb = new StringBuilder();
            Sql_YCYB sql_ycybwindzs = new Sql_YCYB();
            // Model model = new Model();
            // model.PUBLISHDATE = datetime;
            //  DataTable dt = (DataTable)sql_jxhzs.Get_JXHYB_AllData(model);
            DataTable dt = (DataTable)sql_ycybwindzs.Get_YCYB_DataByWind();
            if (dt.Rows.Count > 0)
            {

                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"FILENAME\":\"" + dt.Rows[i]["FILENAME"]
                        + "\",\"REGION\":\"" + dt.Rows[i]["REGION"]
                        + "\",\"FILETYPE\":\"" + dt.Rows[i]["FILETYPE"]
                        + "\",\"PUBLISHDATA\":\"" + dt.Rows[i]["PUBLISHDATA"]
                        + "\",\"EFFECTIVETIME\":\"" + dt.Rows[i]["EFFECTIVETIME"]
                        + "\",\"FLAG\":\"" + dt.Rows[i]["FLAG"]
                        + "\",\"FISHINGGROUNDID\":\"" + dt.Rows[i]["FISHINGGROUNDID"]
                        + "\",\"FISHINGGROUNDNAME\":\"" + dt.Rows[i]["FISHINGGROUNDNAME"]
                        + "\",\"EFFECTIVETIME\":\"" + dt.Rows[i]["EFFECTIVETIME"]
                        + "\",\"WINDDIRECTION\":\"" + dt.Rows[i]["WINDDIRECTION"]
                        + "\",\"WINDFORCE\":\"" + dt.Rows[i]["WINDFORCE"]
                        + "\",\"TREND\":\"" + dt.Rows[i]["TREND"] + "\"},");
                }
                if (dt.Rows.Count <= 0)
                {
                    context.Response.Write(sb.ToString() + "]");
                }
                else
                {
                    context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]");
                }
            }
            else
            {

            }

        }

        public void getbyDataTime_YCYB_WIND(HttpContext context)
        {
            string datetime = context.Request["datetime"].ToString();
            StringBuilder sb = new StringBuilder();
            Sql_YCYB sql_ycybtimezs = new Sql_YCYB();
            YC_FILE yc_file = new YC_FILE();
            yc_file.PublishData = Convert.ToDateTime(datetime);
            DataTable dt = (DataTable)sql_ycybtimezs.Get_YCYB_DataTimeByWind(yc_file);
            if (dt.Rows.Count > 0)
            {

                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"FILENAME\":\"" + dt.Rows[i]["FILENAME"]
                       + "\",\"REGION\":\"" + dt.Rows[i]["REGION"]
                       + "\",\"FILETYPE\":\"" + dt.Rows[i]["FILETYPE"]
                       + "\",\"PUBLISHDATA\":\"" + dt.Rows[i]["PUBLISHDATA"]
                       + "\",\"EFFECTIVETIME\":\"" + dt.Rows[i]["EFFECTIVETIME"]
                       + "\",\"FLAG\":\"" + dt.Rows[i]["FLAG"]
                       + "\",\"FISHINGGROUNDID\":\"" + dt.Rows[i]["FISHINGGROUNDID"]
                       + "\",\"FISHINGGROUNDNAME\":\"" + dt.Rows[i]["FISHINGGROUNDNAME"]
                       + "\",\"EFFECTIVETIME\":\"" + dt.Rows[i]["EFFECTIVETIME"]
                       + "\",\"WINDDIRECTION\":\"" + dt.Rows[i]["WINDDIRECTION"]
                       + "\",\"WINDFORCE\":\"" + dt.Rows[i]["WINDFORCE"]
                       + "\",\"TREND\":\"" + dt.Rows[i]["TREND"] + "\"},");
                }
                if (dt.Rows.Count <= 0)
                {
                    context.Response.Write(sb.ToString() + "]");
                }
                else
                {
                    context.Response.Write(sb.Remove(sb.Length - 1, 1).ToString() + "]");
                }
            }
            else
            {

            }

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