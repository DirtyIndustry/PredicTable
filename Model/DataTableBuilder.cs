using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PredicTable.Model
{
    public class DataTableBuilder
    {
        DataTable dtNew;

        public DataTableBuilder()
        {
            
        }

        /// <summary>
        /// 创建新DataTable
        /// 进行数据拼接
        /// </summary>
        /// <returns></returns>
        public DataTable GetRtnDataTable()
        {
            dtNew = new DataTable();
            dtNew.Columns.Add("PUBLISHDATE", typeof(DateTime));
            dtNew.Columns.Add("FORECASTAREA", typeof(string));
            dtNew.Columns.Add("WINDFORCE24FORECAST", typeof(string));
            dtNew.Columns.Add("WINDFORCE48FORECAST", typeof(string));
            dtNew.Columns.Add("WINDFORCE72FORECAST", typeof(string));
            dtNew.Columns.Add("WINDDIRECTION24FORECAST", typeof(string));
            dtNew.Columns.Add("WINDDIRECTION48FORECAST", typeof(string));
            dtNew.Columns.Add("WINDDIRECTION72FORECAST", typeof(string));
            dtNew.Columns.Add("WAVE24FORECAST", typeof(string));
            dtNew.Columns.Add("WAVE48FORECAST", typeof(string));
            dtNew.Columns.Add("WAVE72FORECAST", typeof(string));
            return dtNew;
        }

        /// <summary>
        /// 初始化新DataTable
        /// </summary>
        /// <param name="dtNew"></param>
        /// <returns></returns>
        public DataTable SetValue(DataTable dtNew)
        {
            DataRow rowNew = dtNew.NewRow();
            rowNew["PUBLISHDATE"] = DateTime.Now;
            rowNew["FORECASTAREA"] = "";
            rowNew["WINDFORCE24FORECAST"] = "";
            rowNew["WINDFORCE48FORECAST"] = "";
            rowNew["WINDFORCE72FORECAST"] = "";
            rowNew["WINDDIRECTION24FORECAST"] = "";
            rowNew["WINDDIRECTION48FORECAST"] = "";
            rowNew["WINDDIRECTION72FORECAST"] = "";
            rowNew["WAVE24FORECAST"] = "";
            rowNew["WAVE48FORECAST"] = "";
            rowNew["WAVE72FORECAST"] = "";
            dtNew.Rows.Add(rowNew);
            return dtNew;
        }
    }
}