using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using System.IO;
using System.Data;
using PredicTable.Commen;
using PredicTable.Dal;
using PredicTable.Model;

namespace PredicTable.Ajax
{
    /// <summary>
    /// GetSCOSpecialTableList 的摘要说明
    /// </summary>
    public class GetSCOSpecialTableList : IHttpHandler, IRequiresSessionState
    {
        string UserName = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string method = context.Request["method"].ToString();

            switch (method)
            {
                case "getSCOTableList": getSCOTableList(context); break;
                case "submit": submit(context); break;//上传
                //case "PriviewTbl":PriviewTbl(context);break;
                default:
                    break;
            }

        } 

        #region 获取数据
        /// <summary>
        /// 获取上合峰会预报单数据
        /// </summary>
        /// <param name="context"></param>
        private void getSCOTableList(HttpContext context)
        {
            try
            {
                DateTime date = DateTime.Parse(context.Request["date"].ToString());
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                //sb.Append(GetTableOffShoreWind(date));   //获取近海风
                //sb.Append(GetTableOpenShoreWind(date));  //获取外海风
                //sb.Append(GetOffShoreWave(date));        //获取近海浪
                //sb.Append(GetOpenShoreWave(date));       // 获取外海浪
                sb.Append(GetOffShoreWaveAndWind(date)); //获取绿潮近海风浪
                sb.Append(GetOpenShoreWaveAndWind(date));//获取绿潮外海风浪
                sb.Append(GetTableOnShoreSW(date));    //获取外海 水温
                sb.Append(GetTableOffShoreSW(date));    //获取近海 水温
                sb.Append(GetTableSummarizeAndPeriod(date));    //期数和综述 
                context.Response.Write(sb.Replace("[,{", "[{").ToString() + "]");
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取黄海绿潮出现异常！" + ex.Message + "\r\n" + ex.StackTrace);
            }
            }

        #region 上合峰会预报单取消不用
        /// <summary>
        /// 获取近海风数据    t1
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns> 
        private string GetTableOffShoreWind(DateTime date)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            DataTable dtOffShoreFL_S = new DataTable();
            DataTable dtOffShoreFL = new DataTable();
            StringBuilder sb_str = new StringBuilder();
            string result = "\"offShoreWindWave\":";//近海风浪

            dtOffShoreFL = (DataTable)sql_SH.GetTableOffShoreWind(date);
            if (dtOffShoreFL != null && dtOffShoreFL.Rows.Count > 0)
            {   //有预报员修改的数据
                result += JsonMore.Serialize(dtOffShoreFL);
            }
            else
            {
                //获取元数据
                dtOffShoreFL_S = (DataTable)sql_SH.GetTableOffShoreWind_S(date);

                if (dtOffShoreFL_S != null && dtOffShoreFL_S.Rows.Count > 0)
                {
                    result += JsonMore.Serialize(dtOffShoreFL_S);
                }
                else
                {
                    result += "[{}]";
                }
            }

            sb_str.Append(",{\"type\":\"t1\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
        /// <summary>
        /// 获取外海风数据     t2
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetTableOpenShoreWind(DateTime date)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            DataTable dtOnShoreF_S = new DataTable();
            DataTable dtOnShoreF = new DataTable();
            StringBuilder sb_str = new StringBuilder();
            string result = "\"onShoreWind\":";//外海风

            dtOnShoreF = (DataTable)sql_SH.GetTableOffOnShoreWind(date);
            if (dtOnShoreF != null && dtOnShoreF.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtOnShoreF);
            }
            else
            {
                dtOnShoreF_S = (DataTable)sql_SH.GetTableOffOnShoreWind_S(date);

                if (dtOnShoreF_S != null && dtOnShoreF_S.Rows.Count > 0)
                {
                    result += JsonMore.Serialize(dtOnShoreF_S);
                }
                else
                {
                    result += "[{}]";
                }
            }

            sb_str.Append(",{\"type\":\"t2\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
       
        /// <summary>
        /// 获取近海浪      t3
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetOffShoreWave(DateTime date)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            StringBuilder sb_str = new StringBuilder();
            DataTable dtOffWave_S = new DataTable();
            DataTable dtOffWave = new DataTable();
            string result = "\"onShoreWave\":";//外海浪
            dtOffWave = (DataTable)sql_SH.GetTableOffShoreWave(date);
            if (dtOffWave != null && dtOffWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtOffWave);
            }
            else
            {
                dtOffWave_S = (DataTable)sql_SH.GetTableOffShoreWave_S(date);
                if (dtOffWave_S != null && dtOffWave_S.Rows.Count > 0)
                {
                    result += JsonMore.Serialize(dtOffWave_S);
                }
                else
                {
                    result += "[{}]";
                }
            }
            sb_str.Append(",{\"type\":\"t3\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();

        }
        /// <summary>
        /// 获取外海浪           t4
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetOpenShoreWave(DateTime date)
        {

            sql_SCOTableList sql_SH = new sql_SCOTableList();
            StringBuilder sb_str = new StringBuilder();
            DataTable dtOpenWave_S = new DataTable();
            DataTable dtOpenWave = new DataTable();
            string result = "\"onOpenWave\":";//外海浪
            dtOpenWave = (DataTable)sql_SH.GetTableOpenShoreWave(date);
            if (dtOpenWave != null && dtOpenWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtOpenWave);
            }
            else
            {
                dtOpenWave_S = (DataTable)sql_SH.GetTableOpenShoreWave_S(date);
                if (dtOpenWave_S != null && dtOpenWave_S.Rows.Count > 0)
                {
                    result += JsonMore.Serialize(dtOpenWave_S);
                }
                else
                {
                    result += "[{}]";
                }
            }
            sb_str.Append(",{\"type\":\"t4\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
        #endregion


        /// <summary>
        /// 获取当前日期的期数和综述        t0
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetTableSummarizeAndPeriod(DateTime date)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            StringBuilder sb_str = new StringBuilder();
            DataTable dtSumAndPeriod = new DataTable();
            dtSumAndPeriod = (DataTable)sql_SH.GetTableSumAndPeriod(date);
            string result = "\"SummarizeAndPeriod\":"; //期数和综述
            if (dtSumAndPeriod != null && dtSumAndPeriod.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtSumAndPeriod);
                sb_str.Append(",{\"type\":\"t0\",\"pbtype\":\"today\",");
            }
            else
            {
                //无当天期数，取最近一次日期的期数
                DataTable dtFirstPeriod = (DataTable)sql_SH.GetFirstPeriod();
                if (dtFirstPeriod != null && dtFirstPeriod.Rows.Count > 0)
                {
                    result += JsonMore.Serialize(dtFirstPeriod);
                }
                else
                {
                    result += "[{}]";
                }
                sb_str.Append(",{\"type\":\"t0\",\"pbtype\":\"yestoday\",");
            }
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
        /// <summary>
        /// 获取近海水温    t6
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetTableOffShoreSW(DateTime date)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            StringBuilder sb_str = new StringBuilder();
            DataTable dt_OffShoreSW_S = new DataTable();
            DataTable dt_OfShoreSW = new DataTable();

            string result = "\"offShoreSW\":";//近海水温
            dt_OfShoreSW = (DataTable)sql_SH.GetOffShareWaterTemperature(date);
            if (dt_OfShoreSW != null && dt_OfShoreSW.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt_OfShoreSW);
            }
            else
            {
                dt_OffShoreSW_S = (DataTable)sql_SH.GetOffShareWaterTemperature_S(date);
                if (dt_OffShoreSW_S != null && dt_OffShoreSW_S.Rows.Count > 0)
                {
                    result += JosnMore.Serialize(dt_OffShoreSW_S);
                }
                else
                {
                    result += "[{}]";
                }

            }
            sb_str.Append(",{\"type\":\"t6\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
        /// <summary>
        /// 获取远海水温数据      t5
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetTableOnShoreSW(DateTime date)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            StringBuilder sb_str = new StringBuilder();
            DataTable dt_OnShoreSW_S = new DataTable();
            DataTable dt_OnShoreSW = new DataTable();

            string result = "\"onShoreSW\":";//外海水温
            dt_OnShoreSW = (DataTable)sql_SH.GetOnShoreSW(date);
            if (dt_OnShoreSW != null && dt_OnShoreSW.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt_OnShoreSW);
            }
            else
            {
                dt_OnShoreSW_S = (DataTable)sql_SH.GetOnShoreSW_S(date);
                if (dt_OnShoreSW_S != null && dt_OnShoreSW_S.Rows.Count > 0)
                {
                    result += JosnMore.Serialize(dt_OnShoreSW_S);
                }
                else
                {
                    result += "[{}]";
                }

            }
            sb_str.Append(",{\"type\":\"t5\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 黄海绿潮专项海洋环境预报 获取近海风浪信息       t1
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetOffShoreWaveAndWind(DateTime date)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            LvChaoWindAndWaveModel lvchao = new LvChaoWindAndWaveModel();//绿潮实体类
            StringBuilder sb_str = new StringBuilder();
            DataTable dt_S = new DataTable();
            DataTable dt = new DataTable();
            string result = "\"OffWaveAndWind\":";//近海风浪
            dt = (DataTable)sql_SH.GetTableOffShoreWindAndWave(date);//获取近海风浪信息
            //判断当天是否有三天的预报信息
            if (dt != null && dt.Rows.Count >= 3)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                dt_S = (DataTable)sql_SH.GetTableOffShoreWindAndWave_S(date);
                if (dt_S != null && dt_S.Rows.Count > 0)
                {
                    result += JosnMore.Serialize(dt_S);
                }
                else
                {
                    result += "[{},{},{}]";
                }

            }
            sb_str.Append(",{\"type\":\"t1\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
        /// <summary>
        /// 黄海绿潮专项海洋环境预报 获取外海风浪信息       t2
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetOpenShoreWaveAndWind(DateTime date)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();//sql类
            LvChaoWindAndWaveModel lvchao = new LvChaoWindAndWaveModel();//绿潮实体类
            StringBuilder sb_str = new StringBuilder();
            DataTable dt_S = new DataTable();
            DataTable dt = new DataTable();
            string result = "\"OpenWaveAndWind\":";//近海风浪
            dt = (DataTable)sql_SH.GetTableOnShoreWindAndWave(date);
            //判断当天是否有三天的预报信息
            if (dt != null && dt.Rows.Count >= 3)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                dt_S = (DataTable)sql_SH.GetTableONShoreWindAndWave_S(date);
                if (dt_S != null && dt_S.Rows.Count > 0)
                {
                    result += JosnMore.Serialize(dt_S);
                }
                else
                {
                    result += "[{},{},{}]";
                }

            }
            sb_str.Append(",{\"type\":\"t2\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
        #endregion
        #region 提交数据
        /// <summary>
        /// 提交上合峰会预报单数据
        /// </summary>
        /// <param name="context"></param>
        private void submit(HttpContext context)
        {
            DateTime date = DateTime.Parse(context.Request["date"].ToString());
            string tblType = context.Request["type"].ToString();  //确定哪个表单的数据
            // string data = context.Request.Form["datas"].ToString();
            // string Authority = context.Session["type"].ToString();//用户权限
            string strMsg = "";
            //string logMsg = "";
            switch (tblType)
            {
                case "1": strMsg = setTable01(date, context); break;//期数
                case "2": strMsg = setTable02(date, context); break;//综述
                case "3": strMsg = setTable03(date, context, tblType); break;//近海风浪
                case "4": strMsg = setTable03(date, context, tblType); break;//外海风浪
                case "5": strMsg = setTable04(date, context); break;//外海水温
                case "6": strMsg = setTable06(date, context); break;//近海水温
            }
            context.Response.Write(strMsg);
        }
        /// <summary>
        /// 表单01添加修改  期数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable01(DateTime date, HttpContext context)
        {
            string data = context.Request.Form["datas"].ToString();
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            DataTable dt = (DataTable)sql_SH.GetTablePeriod(date);
            int addnum = 0;
            int editnum = 0;
            string rtnStr = "";
            string type = "edit";
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            if (type == "add")
            {
                addnum += sql_SH.AddTablePeriod(date, data);
                if (addnum > 0)
                {
                    rtnStr = "addsuccess";
                }
                else
                {
                    rtnStr = "adderror";
                }
            }
            else
            {
                editnum += sql_SH.EditTablePeriod(date, data);
                if (editnum > 0)
                {
                    rtnStr = "editsuccess";
                }
                else
                {
                    rtnStr = "editerror";
                }
            }
            return rtnStr;
        }
        /// <summary>
        /// 表单02添加修改    综述
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable02(DateTime date, HttpContext context)
        {
            string data = context.Request.Form["datas"].ToString();
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            int addnum = 0;
            int editnum = 0;
            string rtnStr = "";
            string type = "edit";
            DataTable dt = (DataTable)sql_SH.GetTableSum(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                //有今天的综述
                type = "edit";
            }
            else
            {
                type = "add";
            }
            if (type == "add")
            {
                addnum += sql_SH.AddTableSum(date, data);
                if (addnum > 0)
                {
                    rtnStr = "addsuccess";
                }
                else
                {
                    rtnStr = "adderror";
                }
            }
            else if (type == "edit")
            {
                editnum += sql_SH.EditTableSum(date, data);
                if (editnum > 0)
                {
                    rtnStr = "editsuccess";
                }
                else
                {
                    rtnStr = "editerror";
                }
            }
            return rtnStr;
        }
        #region  上合峰会风浪气象上传   注掉不用
        /// <summary>
        /// 表单03\04添加修改    近海/外海风浪数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="context"></param>
        /// <param name="TblType">区别近海、外海</param>
        /// <returns></returns>
        //private string setTable03(DateTime date, HttpContext context, string TblType)
        //{
        //    sql_SCOTableList sql_SH = new sql_SCOTableList();
        //    int AddWindNum = 0;
        //    int EditWindnum = 0;
        //    int AddWaveNum = 0;
        //    int EditWaveNum = 0;

        //    string TableType = "";
        //    string type = "edit";
        //    string rtnStr = "";

        //    string FL_strData = context.Request.Form["FL_strData"].ToString();
        //    string FX_strData = context.Request.Form["FX_strData"].ToString();
        //    string BG_strData = context.Request.Form["BG_strData"].ToString();
        //    string BX_strData = context.Request.Form["BX_strData"].ToString();
        //    string Weather_strData = context.Request.Form["Weather_strData"].ToString();
        //    var FL_Data_Arr = FL_strData.Split(',');
        //    var FX_Data_Arr = FX_strData.Split(',');
        //    var BG_Data_Arr = BG_strData.Split(',');
        //    var BX_Data_Arr = BX_strData.Split(',');
        //    var Weather_Data_Arr = Weather_strData.Split(',');
        //    DataTable dt = new DataTable();
        //    if (TblType == "3")
        //    {
        //        TableType = "近海";
        //        dt = (DataTable)sql_SH.GetTableOffShoreWind(date);
        //    }
        //    else if (TblType == "4")
        //    {
        //        TableType = "外海";
        //        dt = (DataTable)sql_SH.GetTableOffOnShoreWind(date);
        //    }

        //    SCOFLModel SCOHF = NewHFModel(date, TableType, Weather_Data_Arr, FL_Data_Arr, FX_Data_Arr);
        //    SCOWaveModel WaveModel = NewWaveModel(date, TableType, BG_Data_Arr, BX_Data_Arr);

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        type = "edit";
        //    }
        //    else
        //    {
        //        type = "add";
        //    }

        //    if (type == "add")
        //    {
        //        AddWindNum += sql_SH.AddTableOffShoreWind(date, SCOHF);
        //        AddWaveNum += sql_SH.AddTableOffAndOnWave(date, WaveModel);
        //        //添加 风 / 浪
        //        if (AddWindNum > 0 && AddWaveNum > 0)
        //        {
        //            rtnStr = "addsuccess";
        //        }
        //        else
        //        {
        //            rtnStr = "adderror";
        //        }
        //    }
        //    else if (type == "edit")
        //    {
        //        EditWindnum += sql_SH.EditTableOffShoreWind(date, SCOHF);
        //        EditWaveNum += sql_SH.EditTableOffAndOnWave(date, WaveModel);
        //        //修改 风 /浪
        //        if (EditWindnum > 0 && EditWaveNum > 0)
        //        {
        //            rtnStr = "editsuccess";
        //        }
        //        else
        //        {
        //            rtnStr = "editerror";
        //        }
        //    }

        //    return rtnStr;
        //}
        #endregion
        /// <summary>
        /// 表单03\04 添加修改    近海/外海风浪数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="context"></param>
        /// <param name="TblType">区别近海、外海</param>
        /// <returns></returns>
        private string setTable03(DateTime date, HttpContext context, string TblType)
        {
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            LvChaoWindAndWaveModel LvChao = new LvChaoWindAndWaveModel();
            DataTable dt = new DataTable();
            int AddNum = 0;
            int Editnum = 0;
           
            string TableType = "";
            string type = "edit";
            string rtnStr = "";
            string WindAndWave_strData = context.Request.Form["datas"].ToString();
            var WindAndWave_strDataArr= WindAndWave_strData.Split(',');
            if (TblType == "3")
            {
                TableType = "近海";
                dt = (DataTable)sql_SH.GetTableOffShoreWindAndWave(date);
            }
            else if (TblType == "4")
            {
                TableType = "外海";
                dt = (DataTable)sql_SH.GetTableOnShoreWindAndWave(date);
            }
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
                LvChao.FORECASTAREA = TableType;  //海区
                LvChao.PUBLISHDATE = date;//填报日期
                LvChao.FORECASTDATE = date.AddDays((i + 1));//预报日期
                LvChao.WINDFORCE = WindAndWave_strDataArr[(i * 5) + 1];//风力
                LvChao.WINDDIRECTION = WindAndWave_strDataArr[(i * 5) + 2];//风向
                LvChao.WAVEHIGHT = WindAndWave_strDataArr[(i * 5) + 3];//波高
                LvChao.WAVEDIRECTION = WindAndWave_strDataArr[(i * 5) + 4];//波向
                LvChao.WEATHER = WindAndWave_strDataArr[(i * 5)];//天气

                if (type == "add")
                {
                    AddNum += sql_SH.AddTableOffAndOnShoreWindAndWave(date, LvChao);
                }
                else if (type == "edit")
                {
                    Editnum += sql_SH.EditTableOffAndOnShoreWindAndWave(date, LvChao);
                }
            }
            if (type == "add")
            {
                if (AddNum == 3)
                {
                    rtnStr = "addsuccess";
                }
                else
                {
                    rtnStr = "adderror";
                }
            }
            else if (type == "edit")
            {
                if (Editnum == 3)
                {
                    rtnStr = "editsuccess";
                }
                else
                {
                    rtnStr = "editerror";
                }
            }

            return rtnStr;
        }
        /// <summary>
        /// 表单05添加修改   外海水温
        /// </summary>
        /// <param name="date"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private string setTable04(DateTime date, HttpContext context)
        {
            SCOTemperatureMode SCO_SH = new SCOTemperatureMode();
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            string SW_strData = context.Request.Form["datas"].ToString();
            var SW_arrData = SW_strData.Split(',');

            SCO_SH.PUBLISHDATE = date;
            SCO_SH.MAX1 = SW_arrData[0];
            SCO_SH.MIN1 = SW_arrData[1];
            SCO_SH.AVG1 = SW_arrData[2];
            SCO_SH.MAX2 = SW_arrData[3];
            SCO_SH.MIN2 = SW_arrData[4];
            SCO_SH.AVG2 = SW_arrData[5];
            SCO_SH.MAX3 = SW_arrData[6];
            SCO_SH.MIN3 = SW_arrData[7];
            SCO_SH.AVG3 = SW_arrData[8];

            int addnum = 0;
            int editnum = 0;
            string rtnStr = "";
            string type = "edit";
            DataTable dt = (DataTable)sql_SH.GetOnShoreSW(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            if (type == "add")
            {
                addnum += sql_SH.AddOnShoreSW(SCO_SH);
                if (addnum > 0)
                {
                    rtnStr = "addsuccess";
                }
                else
                {
                    rtnStr = "adderror";
                }

            }
            else if (type == "edit")
            {
                editnum += sql_SH.EditOnShoreSW(SCO_SH);
                if (editnum > 0)
                {
                    rtnStr = "editsuccess";
                }
                else
                {
                    rtnStr = "editerror";
                }
            }
            return rtnStr;
        }
        /// <summary>
        /// 表单06添加修改   近海水温
        /// </summary>
        /// <param name="date"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private string setTable06(DateTime date, HttpContext context)
        {
            SCOTemperatureMode SCO_SH = new SCOTemperatureMode();
            sql_SCOTableList sql_SH = new sql_SCOTableList();
            string SW_strData = context.Request.Form["datas"].ToString();
            var SW_arrData = SW_strData.Split(',');

            SCO_SH.PUBLISHDATE = date;
            SCO_SH.MAX1 = SW_arrData[0];
            SCO_SH.MIN1 = SW_arrData[1];
            SCO_SH.AVG1 = SW_arrData[2];
            SCO_SH.MAX2 = SW_arrData[3];
            SCO_SH.MIN2 = SW_arrData[4];
            SCO_SH.AVG2 = SW_arrData[5];
            SCO_SH.MAX3 = SW_arrData[6];
            SCO_SH.MIN3 = SW_arrData[7];
            SCO_SH.AVG3 = SW_arrData[8];

            int addnum = 0;
            int editnum = 0;
            string rtnStr = "";
            string type = "edit";
            DataTable dt = (DataTable)sql_SH.GetOffShareWaterTemperature(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            if (type == "add")
            {
                addnum += sql_SH.AddOffShoreSW(SCO_SH);
                if (addnum > 0)
                {
                    rtnStr = "addsuccess";
                }
                else
                {
                    rtnStr = "adderror";
                }

            }
            else if (type == "edit")
            {
                editnum += sql_SH.EditOffShoreSW(SCO_SH);
                if (editnum > 0)
                {
                    rtnStr = "editsuccess";
                }
                else
                {
                    rtnStr = "editerror";
                }
            }
            return rtnStr;
        }
        #endregion

        #region 生成模型实体类
        /// <summary>
        /// 生成海风模型实体类
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="TableType">表单（内海、外海））</param>
        /// <param name="Weather_Data_Arr">天气</param>
        /// <param name="FL_Data_Arr">风力</param>
        /// <param name="FX_Data_Arr">风向</param>
        /// <returns></returns>
        private SCOFLModel NewHFModel(DateTime date, string TableType, string[] Weather_Data_Arr, string[] FL_Data_Arr, string[] FX_Data_Arr)
        {
            SCOFLModel SCOHF = new SCOFLModel();
            #region 海风实体类赋值

            SCOHF.PUBLISHDATE = date;
            SCOHF.FORECASTAREA = TableType;
            SCOHF.WEATHER00D00H = Weather_Data_Arr[0];
            SCOHF.WEATHER00D01H = Weather_Data_Arr[1];
            SCOHF.WEATHER01D00H = Weather_Data_Arr[2];
            SCOHF.WEATHER01D01H = Weather_Data_Arr[3];
            SCOHF.WEATHER02D00H = Weather_Data_Arr[4];
            SCOHF.WEATHER02D01H = Weather_Data_Arr[5];

            SCOHF.WINDFORCE00H = FL_Data_Arr[0];
            SCOHF.WINDFORCE01H = FL_Data_Arr[1];
            SCOHF.WINDFORCE02H = FL_Data_Arr[2];
            SCOHF.WINDFORCE03H = FL_Data_Arr[3];
            SCOHF.WINDFORCE04H = FL_Data_Arr[4];
            SCOHF.WINDFORCE05H = FL_Data_Arr[5];
            SCOHF.WINDFORCE06H = FL_Data_Arr[6];
            SCOHF.WINDFORCE07H = FL_Data_Arr[7];
            SCOHF.WINDFORCE08H = FL_Data_Arr[8];
            SCOHF.WINDFORCE09H = FL_Data_Arr[9];
            SCOHF.WINDFORCE10H = FL_Data_Arr[10];
            SCOHF.WINDFORCE11H = FL_Data_Arr[11];
            SCOHF.WINDFORCE12H = FL_Data_Arr[12];
            SCOHF.WINDFORCE13H = FL_Data_Arr[13];
            SCOHF.WINDFORCE14H = FL_Data_Arr[14];
            SCOHF.WINDFORCE15H = FL_Data_Arr[15];
            SCOHF.WINDFORCE16H = FL_Data_Arr[16];
            SCOHF.WINDFORCE17H = FL_Data_Arr[17];
            SCOHF.WINDFORCE18H = FL_Data_Arr[18];
            SCOHF.WINDFORCE19H = FL_Data_Arr[19];
            SCOHF.WINDFORCE20H = FL_Data_Arr[20];
            SCOHF.WINDFORCE21H = FL_Data_Arr[21];
            SCOHF.WINDFORCE22H = FL_Data_Arr[22];
            SCOHF.WINDFORCE23H = FL_Data_Arr[23];

            SCOHF.WINDFORCE24H = FL_Data_Arr[24];
            SCOHF.WINDFORCE25H = FL_Data_Arr[25];
            SCOHF.WINDFORCE26H = FL_Data_Arr[26];
            SCOHF.WINDFORCE27H = FL_Data_Arr[27];

            SCOHF.WINDFORCE28H = FL_Data_Arr[28];
            SCOHF.WINDFORCE29H = FL_Data_Arr[28];
            SCOHF.WINDFORCE30H = FL_Data_Arr[30];
            SCOHF.WINDFORCE31H = FL_Data_Arr[31];


            SCOHF.WINDDIRECTION00H = FX_Data_Arr[0];
            SCOHF.WINDDIRECTION01H = FX_Data_Arr[1];
            SCOHF.WINDDIRECTION02H = FX_Data_Arr[2];
            SCOHF.WINDDIRECTION03H = FX_Data_Arr[3];
            SCOHF.WINDDIRECTION04H = FX_Data_Arr[4];
            SCOHF.WINDDIRECTION05H = FX_Data_Arr[5];
            SCOHF.WINDDIRECTION06H = FX_Data_Arr[6];
            SCOHF.WINDDIRECTION07H = FX_Data_Arr[7];
            SCOHF.WINDDIRECTION08H = FX_Data_Arr[8];
            SCOHF.WINDDIRECTION09H = FX_Data_Arr[9];
            SCOHF.WINDDIRECTION10H = FX_Data_Arr[10];
            SCOHF.WINDDIRECTION11H = FX_Data_Arr[11];
            SCOHF.WINDDIRECTION12H = FX_Data_Arr[12];
            SCOHF.WINDDIRECTION13H = FX_Data_Arr[13];
            SCOHF.WINDDIRECTION14H = FX_Data_Arr[14];
            SCOHF.WINDDIRECTION15H = FX_Data_Arr[15];
            SCOHF.WINDDIRECTION16H = FX_Data_Arr[16];
            SCOHF.WINDDIRECTION17H = FX_Data_Arr[17];
            SCOHF.WINDDIRECTION18H = FX_Data_Arr[18];
            SCOHF.WINDDIRECTION19H = FX_Data_Arr[19];
            SCOHF.WINDDIRECTION20H = FX_Data_Arr[20];
            SCOHF.WINDDIRECTION21H = FX_Data_Arr[21];
            SCOHF.WINDDIRECTION22H = FX_Data_Arr[22];
            SCOHF.WINDDIRECTION23H = FX_Data_Arr[23];

            SCOHF.WINDDIRECTION24H = FX_Data_Arr[24];
            SCOHF.WINDDIRECTION25H = FX_Data_Arr[25];
            SCOHF.WINDDIRECTION26H = FX_Data_Arr[26];
            SCOHF.WINDDIRECTION27H = FX_Data_Arr[27];
            SCOHF.WINDDIRECTION28H = FX_Data_Arr[28];
            SCOHF.WINDDIRECTION29H = FX_Data_Arr[29];
            SCOHF.WINDDIRECTION30H = FX_Data_Arr[30];
            SCOHF.WINDDIRECTION31H = FX_Data_Arr[31];
            #endregion
            return SCOHF;
        }
        /// <summary>
        /// 生成海浪模型实体类
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="TableType">表单（内海、外海））</param>
        /// <param name="BG_Data_Arr">浪高</param>
        /// <param name="BX_Data_Arr">浪向</param>
        /// <returns></returns>
        private SCOWaveModel NewWaveModel(DateTime date, string TableType, string[] BG_Data_Arr, string[] BX_Data_Arr)
        {
            SCOWaveModel WaveModel = new SCOWaveModel();
            #region 海浪实体类赋值

            WaveModel.PUBLISHDATE = date;
            WaveModel.FORECASTAREA = TableType;

            WaveModel.WAVEFORCE00H = BG_Data_Arr[0];
            WaveModel.WAVEFORCE01H = BG_Data_Arr[1];
            WaveModel.WAVEFORCE02H = BG_Data_Arr[2];
            WaveModel.WAVEFORCE03H = BG_Data_Arr[3];
            WaveModel.WAVEFORCE04H = BG_Data_Arr[4];
            WaveModel.WAVEFORCE05H = BG_Data_Arr[5];
            WaveModel.WAVEFORCE06H = BG_Data_Arr[6];
            WaveModel.WAVEFORCE07H = BG_Data_Arr[7];
            WaveModel.WAVEFORCE08H = BG_Data_Arr[8];
            WaveModel.WAVEFORCE09H = BG_Data_Arr[9];
            WaveModel.WAVEFORCE10H = BG_Data_Arr[10];
            WaveModel.WAVEFORCE11H = BG_Data_Arr[11];
            WaveModel.WAVEFORCE12H = BG_Data_Arr[12];
            WaveModel.WAVEFORCE13H = BG_Data_Arr[13];
            WaveModel.WAVEFORCE14H = BG_Data_Arr[14];
            WaveModel.WAVEFORCE15H = BG_Data_Arr[15];
            WaveModel.WAVEFORCE16H = BG_Data_Arr[16];
            WaveModel.WAVEFORCE17H = BG_Data_Arr[17];
            WaveModel.WAVEFORCE18H = BG_Data_Arr[18];
            WaveModel.WAVEFORCE19H = BG_Data_Arr[19];
            WaveModel.WAVEFORCE20H = BG_Data_Arr[20];
            WaveModel.WAVEFORCE21H = BG_Data_Arr[21];
            WaveModel.WAVEFORCE22H = BG_Data_Arr[22];
            WaveModel.WAVEFORCE23H = BG_Data_Arr[23];

            WaveModel.WAVEFORCE24H = BG_Data_Arr[24];
            WaveModel.WAVEFORCE25H = BG_Data_Arr[25];
            WaveModel.WAVEFORCE26H = BG_Data_Arr[26];
            WaveModel.WAVEFORCE27H = BG_Data_Arr[27];

            WaveModel.WAVEFORCE28H = BG_Data_Arr[28];
            WaveModel.WAVEFORCE29H = BG_Data_Arr[28];
            WaveModel.WAVEFORCE30H = BG_Data_Arr[30];
            WaveModel.WAVEFORCE31H = BG_Data_Arr[31];


            WaveModel.WAVEDIRECTION00H = BX_Data_Arr[0];
            WaveModel.WAVEDIRECTION01H = BX_Data_Arr[1];
            WaveModel.WAVEDIRECTION02H = BX_Data_Arr[2];
            WaveModel.WAVEDIRECTION03H = BX_Data_Arr[3];
            WaveModel.WAVEDIRECTION04H = BX_Data_Arr[4];
            WaveModel.WAVEDIRECTION05H = BX_Data_Arr[5];
            WaveModel.WAVEDIRECTION06H = BX_Data_Arr[6];
            WaveModel.WAVEDIRECTION07H = BX_Data_Arr[7];
            WaveModel.WAVEDIRECTION08H = BX_Data_Arr[8];
            WaveModel.WAVEDIRECTION09H = BX_Data_Arr[9];
            WaveModel.WAVEDIRECTION10H = BX_Data_Arr[10];
            WaveModel.WAVEDIRECTION11H = BX_Data_Arr[11];
            WaveModel.WAVEDIRECTION12H = BX_Data_Arr[12];
            WaveModel.WAVEDIRECTION13H = BX_Data_Arr[13];
            WaveModel.WAVEDIRECTION14H = BX_Data_Arr[14];
            WaveModel.WAVEDIRECTION15H = BX_Data_Arr[15];
            WaveModel.WAVEDIRECTION16H = BX_Data_Arr[16];
            WaveModel.WAVEDIRECTION17H = BX_Data_Arr[17];
            WaveModel.WAVEDIRECTION18H = BX_Data_Arr[18];
            WaveModel.WAVEDIRECTION19H = BX_Data_Arr[19];
            WaveModel.WAVEDIRECTION20H = BX_Data_Arr[20];
            WaveModel.WAVEDIRECTION21H = BX_Data_Arr[21];
            WaveModel.WAVEDIRECTION22H = BX_Data_Arr[22];
            WaveModel.WAVEDIRECTION23H = BX_Data_Arr[23];

            WaveModel.WAVEDIRECTION24H = BX_Data_Arr[24];
            WaveModel.WAVEDIRECTION25H = BX_Data_Arr[25];
            WaveModel.WAVEDIRECTION26H = BX_Data_Arr[26];
            WaveModel.WAVEDIRECTION27H = BX_Data_Arr[27];
            WaveModel.WAVEDIRECTION28H = BX_Data_Arr[28];
            WaveModel.WAVEDIRECTION29H = BX_Data_Arr[29];
            WaveModel.WAVEDIRECTION30H = BX_Data_Arr[30];
            WaveModel.WAVEDIRECTION31H = BX_Data_Arr[31];
            #endregion
            return WaveModel;

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