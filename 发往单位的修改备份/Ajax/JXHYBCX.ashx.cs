using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data;

namespace PredicTable.Ajax
{
    /// <summary>
    /// JXHYBCX 的摘要说明
    /// </summary>
    public class JXHYBCX : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string method = context.Request["method"].ToString();

                switch (method)
                {
                    case "getbyDate": getbydata(context); break;
                    case "getbyDateTime": getbyDataTime(context); break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                context.Response.Write(ee.ToString());
            }
        }

        public void getbydata(HttpContext context)
        {
            //string datetime = context.Request["datetime"].ToString();

            StringBuilder sb = new StringBuilder();
            Sql_JXHZS sql_jxhzs = new Sql_JXHZS();
            // Model model = new Model();
            // model.PUBLISHDATE = datetime;
            //  DataTable dt = (DataTable)sql_jxhzs.Get_JXHYB_AllData(model);
            DataTable dt = (DataTable)sql_jxhzs.Get_JXHYB_AllData();
            if (dt.Rows.Count > 0)
            {

                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"FILENAME\":\"" + dt.Rows[i]["FILENAME"]
                        + "\",\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                        + "\",\"TARGETTYPE\":\"" + dt.Rows[i]["TARGETTYPE"]
                        + "\",\"TARGETNAME\":\"" + dt.Rows[i]["TARGETNAME"]
                        + "\",\"FILENUMBER\":\"" + dt.Rows[i]["FILENUMBER"]
                        + "\",\"FD_GC1_CS\":\"" + dt.Rows[i]["FD_GC1_CS"]
                        + "\",\"FD_GC1_CG\":\"" + dt.Rows[i]["FD_GC1_CG"]
                        + "\",\"FD_GC2_CS\":\"" + dt.Rows[i]["FD_GC2_CS"]
                        + "\",\"FD_GC2_CG\":\"" + dt.Rows[i]["FD_GC2_CG"]
                        + "\",\"FD_GC3_CS\":\"" + dt.Rows[i]["FD_GC3_CS"]
                        + "\",\"FD_GC3_CG\":\"" + dt.Rows[i]["FD_GC3_CG"]
                        + "\",\"FD_DC1_CS\":\"" + dt.Rows[i]["FD_DC1_CS"]
                        + "\",\"FD_DC1_CG\":\"" + dt.Rows[i]["FD_DC1_CG"]
                        + "\",\"FD_DC2_CS\":\"" + dt.Rows[i]["FD_DC2_CS"]
                        + "\",\"FD_DC2_CG\":\"" + dt.Rows[i]["FD_DC2_CG"]
                        + "\",\"FD_DC3_CS\":\"" + dt.Rows[i]["FD_DC3_CS"]
                        + "\",\"FD_DC3_CG\":\"" + dt.Rows[i]["FD_DC3_CG"]
                        + "\",\"SD_GC1_CS\":\"" + dt.Rows[i]["SD_GC1_CS"]
                        + "\",\"SD_GC1_CG\":\"" + dt.Rows[i]["SD_GC1_CG"]
                        + "\",\"SD_GC2_CS\":\"" + dt.Rows[i]["SD_GC2_CS"]
                        + "\",\"SD_GC2_CG\":\"" + dt.Rows[i]["SD_GC2_CG"]
                        + "\",\"SD_GC3_CS\":\"" + dt.Rows[i]["SD_GC3_CS"]
                        + "\",\"SD_GC3_CG\":\"" + dt.Rows[i]["SD_GC3_CG"]
                        + "\",\"SD_DC1_CS\":\"" + dt.Rows[i]["SD_DC1_CS"]
                        + "\",\"SD_DC1_CG\":\"" + dt.Rows[i]["SD_DC1_CG"]
                        + "\",\"SD_DC2_CS\":\"" + dt.Rows[i]["SD_DC2_CS"]
                        + "\",\"SD_DC2_CG\":\"" + dt.Rows[i]["SD_DC2_CG"]
                        + "\",\"SD_DC3_CS\":\"" + dt.Rows[i]["SD_DC3_CS"]
                        + "\",\"SD_DC3_CG\":\"" + dt.Rows[i]["SD_DC3_CG"]
                        + "\",\"TD_GC1_CS\":\"" + dt.Rows[i]["TD_GC1_CS"]
                        + "\",\"TD_GC1_CG\":\"" + dt.Rows[i]["TD_GC1_CG"]
                        + "\",\"TD_GC2_CS\":\"" + dt.Rows[i]["TD_GC2_CS"]
                        + "\",\"TD_GC2_CG\":\"" + dt.Rows[i]["TD_GC2_CG"]
                        + "\",\"TD_GC3_CS\":\"" + dt.Rows[i]["TD_GC3_CS"]
                        + "\",\"TD_GC3_CG\":\"" + dt.Rows[i]["TD_GC3_CG"]
                        + "\",\"TD_DC1_CS\":\"" + dt.Rows[i]["TD_DC1_CS"]
                        + "\",\"TD_DC1_CG\":\"" + dt.Rows[i]["TD_DC1_CG"]
                        + "\",\"TD_DC2_CS\":\"" + dt.Rows[i]["TD_DC2_CS"]
                        + "\",\"TD_DC2_CG\":\"" + dt.Rows[i]["TD_DC2_CG"]
                        + "\",\"TD_DC3_CS\":\"" + dt.Rows[i]["TD_DC3_CS"]
                        + "\",\"TD_DC3_CG\":\"" + dt.Rows[i]["TD_DC3_CG"]
                        + "\",\"LG_DATA1\":\"" + dt.Rows[i]["LG_DATA1"]
                        + "\",\"LG_DATA2\":\"" + dt.Rows[i]["LG_DATA2"]
                        + "\",\"LG_DATA3\":\"" + dt.Rows[i]["LG_DATA3"]
                        + "\",\"LG_DATA4\":\"" + dt.Rows[i]["LG_DATA4"]
                        + "\",\"LG_DATA5\":\"" + dt.Rows[i]["LG_DATA5"]
                        + "\",\"LG_DATA6\":\"" + dt.Rows[i]["LG_DATA6"]
                        + "\",\"DT1\":\"" + dt.Rows[i]["DT1"]
                        + "\",\"SW_DATA1\":\"" + dt.Rows[i]["SW_DATA1"]
                        + "\",\"DT2\":\"" + dt.Rows[i]["DT2"]
                        + "\",\"SW_DATA2\":\"" + dt.Rows[i]["SW_DATA2"]
                        + "\",\"DT3\":\"" + dt.Rows[i]["DT3"]
                        + "\",\"SW_DATA3\":\"" + dt.Rows[i]["SW_DATA3"]
                        + "\",\"LINKMAN\":\"" + dt.Rows[i]["LINKMAN"]
                        + "\",\"LINKPHONE\":\"" + dt.Rows[i]["LINKPHONE"] + "\"},");
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

        public void getbyDataTime(HttpContext context)
        {
            string datetime = context.Request["datetime"].ToString();
            datetime = Convert.ToDateTime(datetime).ToString();
            StringBuilder sb = new StringBuilder();
            Sql_JXHZS sql_jxhzs = new Sql_JXHZS();
            Model1 model = new Model1();
            model.PUBLISHDATE = datetime;
            DataTable dt = (DataTable)sql_jxhzs.Get_JXHYB_DataByDatetime(model);
            if (dt.Rows.Count > 0)
            {

                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{ \"FILENAME\":\"" + dt.Rows[i]["FILENAME"]
                        + "\",\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                        + "\",\"TARGETTYPE\":\"" + dt.Rows[i]["TARGETTYPE"]
                        + "\",\"TARGETNAME\":\"" + dt.Rows[i]["TARGETNAME"]
                        + "\",\"FILENUMBER\":\"" + dt.Rows[i]["FILENUMBER"]
                        + "\",\"FD_GC1_CS\":\"" + dt.Rows[i]["FD_GC1_CS"]
                        + "\",\"FD_GC1_CG\":\"" + dt.Rows[i]["FD_GC1_CG"]
                        + "\",\"FD_GC2_CS\":\"" + dt.Rows[i]["FD_GC2_CS"]
                        + "\",\"FD_GC2_CG\":\"" + dt.Rows[i]["FD_GC2_CG"]
                        + "\",\"FD_GC3_CS\":\"" + dt.Rows[i]["FD_GC3_CS"]
                        + "\",\"FD_GC3_CG\":\"" + dt.Rows[i]["FD_GC3_CG"]
                        + "\",\"FD_DC1_CS\":\"" + dt.Rows[i]["FD_DC1_CS"]
                        + "\",\"FD_DC1_CG\":\"" + dt.Rows[i]["FD_DC1_CG"]
                        + "\",\"FD_DC2_CS\":\"" + dt.Rows[i]["FD_DC2_CS"]
                        + "\",\"FD_DC2_CG\":\"" + dt.Rows[i]["FD_DC2_CG"]
                        + "\",\"FD_DC3_CS\":\"" + dt.Rows[i]["FD_DC3_CS"]
                        + "\",\"FD_DC3_CG\":\"" + dt.Rows[i]["FD_DC3_CG"]
                        + "\",\"SD_GC1_CS\":\"" + dt.Rows[i]["SD_GC1_CS"]
                        + "\",\"SD_GC1_CG\":\"" + dt.Rows[i]["SD_GC1_CG"]
                        + "\",\"SD_GC2_CS\":\"" + dt.Rows[i]["SD_GC2_CS"]
                        + "\",\"SD_GC2_CG\":\"" + dt.Rows[i]["SD_GC2_CG"]
                        + "\",\"SD_GC3_CS\":\"" + dt.Rows[i]["SD_GC3_CS"]
                        + "\",\"SD_GC3_CG\":\"" + dt.Rows[i]["SD_GC3_CG"]
                        + "\",\"SD_DC1_CS\":\"" + dt.Rows[i]["SD_DC1_CS"]
                        + "\",\"SD_DC1_CG\":\"" + dt.Rows[i]["SD_DC1_CG"]
                        + "\",\"SD_DC2_CS\":\"" + dt.Rows[i]["SD_DC2_CS"]
                        + "\",\"SD_DC2_CG\":\"" + dt.Rows[i]["SD_DC2_CG"]
                        + "\",\"SD_DC3_CS\":\"" + dt.Rows[i]["SD_DC3_CS"]
                        + "\",\"SD_DC3_CG\":\"" + dt.Rows[i]["SD_DC3_CG"]
                        + "\",\"TD_GC1_CS\":\"" + dt.Rows[i]["TD_GC1_CS"]
                        + "\",\"TD_GC1_CG\":\"" + dt.Rows[i]["TD_GC1_CG"]
                        + "\",\"TD_GC2_CS\":\"" + dt.Rows[i]["TD_GC2_CS"]
                        + "\",\"TD_GC2_CG\":\"" + dt.Rows[i]["TD_GC2_CG"]
                        + "\",\"TD_GC3_CS\":\"" + dt.Rows[i]["TD_GC3_CS"]
                        + "\",\"TD_GC3_CG\":\"" + dt.Rows[i]["TD_GC3_CG"]
                        + "\",\"TD_DC1_CS\":\"" + dt.Rows[i]["TD_DC1_CS"]
                        + "\",\"TD_DC1_CG\":\"" + dt.Rows[i]["TD_DC1_CG"]
                        + "\",\"TD_DC2_CS\":\"" + dt.Rows[i]["TD_DC2_CS"]
                        + "\",\"TD_DC2_CG\":\"" + dt.Rows[i]["TD_DC2_CG"]
                        + "\",\"TD_DC3_CS\":\"" + dt.Rows[i]["TD_DC3_CS"]
                        + "\",\"TD_DC3_CG\":\"" + dt.Rows[i]["TD_DC3_CG"]
                        + "\",\"LG_DATA1\":\"" + dt.Rows[i]["LG_DATA1"]
                        + "\",\"LG_DATA2\":\"" + dt.Rows[i]["LG_DATA2"]
                        + "\",\"LG_DATA3\":\"" + dt.Rows[i]["LG_DATA3"]
                        + "\",\"LG_DATA4\":\"" + dt.Rows[i]["LG_DATA4"]
                        + "\",\"LG_DATA5\":\"" + dt.Rows[i]["LG_DATA5"]
                        + "\",\"LG_DATA6\":\"" + dt.Rows[i]["LG_DATA6"]
                        + "\",\"DT1\":\"" + dt.Rows[i]["DT1"]
                        + "\",\"SW_DATA1\":\"" + dt.Rows[i]["SW_DATA1"]
                        + "\",\"DT2\":\"" + dt.Rows[i]["DT2"]
                        + "\",\"SW_DATA2\":\"" + dt.Rows[i]["SW_DATA2"]
                        + "\",\"DT3\":\"" + dt.Rows[i]["DT3"]
                        + "\",\"SW_DATA3\":\"" + dt.Rows[i]["SW_DATA3"]
                        + "\",\"LINKMAN\":\"" + dt.Rows[i]["LINKMAN"]
                        + "\",\"LINKPHONE\":\"" + dt.Rows[i]["LINKPHONE"] + "\"},");
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