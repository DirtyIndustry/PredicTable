using PredicTable.Commen;
using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace PredicTable.Ajax
{
    /// <summary>
    /// Summary description for GetTideData
    /// </summary>
    public class GetTideData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var stations = context.Request.Params["stations"];
            var dayCount = int.Parse(context.Request.Params["dayCount"]);
            var Dtime1 = context.Request.Params["date"];
            var Dtime = DateTime.Parse(Dtime1);
            var dataTable = new DataTable();
            
            //if (preDate == "")
            //    preDate = DateTime.Now;
            if (dayCount == 7)
            {
                var date = DateTime.Now;
                //按控件的时间查询天文潮数据
                //var date = DateTime.Parse(context.Request.Form["date"]);
                DateTime weekPublishTime = date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")));
                var preStartDate = weekPublishTime.AddDays(1).ToShortDateString();
                var preEndDate = weekPublishTime.AddDays(dayCount).ToShortDateString();

                var sqlTide = new Sql_HT_YB_Tide();
                dataTable = sqlTide.GetTideDataForStationAndPredRange(stations, preStartDate, preEndDate);
            }
            else
            {
                //var preStartDate = DateTime.Now.AddDays(1).ToShortDateString();
                //var preEndDate = DateTime.Now.AddDays(dayCount).ToShortDateString();
                var preStartDate = Dtime.AddDays(1).ToShortDateString();
                var preEndDate = Dtime.AddDays(dayCount).ToShortDateString();
                var sqlTide = new Sql_HT_YB_Tide();
                dataTable = sqlTide.GetTideDataForStationAndPredRange(stations, preStartDate, preEndDate);
                //var dataTable = sqlTide.GetTideDataForStationAndPredictionDate("test2", "2016-01-01");
            }
            var dataJson = JsonMore.Serialize(dataTable);

            //var dataJson = "{\"HelloWorld\":10}";  //JosnMore.Serialize(dataTable);

            context.Response.ContentType = "application/json"; //"text/plain"; 
            context.Response.Write(dataJson);
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