/*
 * 
 * 
   变更记录1
变更时间：10180710    
变更内容：下午四取消，取数从下午三青岛24小时预报取
         下午五取消，取数从下午三青岛48小时预报取
         下午十二潮汐预报取消，从下午三潍坊24小时预报取数
         下午十四的金沙滩预报取消，从下午三三天预报中取数
         下午十八威海取消，从下午三威海24小时预报取数
变更人员：Yuy     
变更记录2：
变更时间：2018.8.30
变更内容：上午、下午指挥处渔政局添加青岛近海3d预报数据 -- 连
         短期预报 下午六南堡油田数据获取 -- 于   
         下午十三数据获取 -- 连
变更记录：
变更内容：精细化风的数据更换数据源

更改记录：潮汐下午13的数据是照搬的下午5的数据，同时提交到下午22中



*/
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
    /// gettablelist 的摘要说明
    /// </summary>
    public class gettablelist : IHttpHandler, IRequiresSessionState
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
                    case "getbydata": getbydata(context); break;//上午
                    case "getbydataPM": getbydataPM(context); break;//下午
                    case "submit": submit(context); break;//上传
                    case "getbaseinfo": getbaseinfo(context); break;
                    case "setsession": setsession(context); break;
                    case "getweekdata": getbydataWeek(context); break;//周报
                    case "getAMdataNew": getAMdataNew(context); break;  //上午指挥处、渔政局预报
                    case "getPMdataNew": getPMdataNew(context); break; //下午指挥处、渔政局预报
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
        /// 根据时间与表单编号添加或修改数据
        /// </summary>
        /// <param name="context"></param>
      

        public void submit(HttpContext context)
        {

            //if (context.Session["userid"] != null)
            //{
            //userid = context.Session["userid"].ToString();
            DateTime date = DateTime.Parse(context.Request["date"].ToString());
            string type = context.Request["type"].ToString();
            string data = context.Request.Form["datas"].ToString();
            string quanxian = context.Session["type"].ToString();
            string srtmsg = "";
            string logmsg = "";
            string logdaima = "";
            //1127贾---------添加quanxian参数
            switch (type)
            {
                case "1": srtmsg = settabe01(date, data, quanxian); break;//一、72小时渤海海区及黄河海港风、浪预报
                case "2": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe02(date, data); break;//二、72小时港口潮位预报
                case "3": srtmsg = settabe03(date, data, quanxian); break;//四、预计未来24小时海浪、水温预报
                case "4": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe04(date, data); break;//五、24小时潮位预报
                case "5": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe05(date, data); break;//下午一、各海区24小时海浪预报
                case "6": srtmsg = settabe06(date, data, quanxian); break;//下午二、山东省近海七市3天海浪、水温预报
                case "7": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe07(date, data); break;//下午三、山东省近海七市24小时潮汐预报proc7City24TideData
                case "8": srtmsg = settabe08(date, data, quanxian); break;//下午四、青岛24小时潮位预报 预报取消从下午三中取数，海浪保存 edit by Yuy 180712
                case "9": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe09(date, data); break;//十三、黄河海港附近海域风、浪预报
                                                                                                               //下午五、黄河南海堤附近海域72小时风、浪预报
                case "10": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe10(date, data); break;//十三、72小时东营神仙沟挡潮闸专项预报
                                                                                                                //下午六、明泽闸潮位预报
                case "11": srtmsg = settabe11(date, data, quanxian); break;//下午七、南堡油田海域波浪、风、水温预报
                case "12": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe12(date, data); break;//下午八、南堡油田海域潮汐预报
                case "13": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe13(date, data); break;//下午九、海区24小时海浪、水温预报
                case "14": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe14(date, data); break;//下午十、海区48小时海浪预报
                case "15": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe15(date, data); break;//下午十一、海区72小时海浪预报
                                                                                                                // case "16": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe16(date, data); break;//下午十二、潍坊港24小时潮汐预报 从下午三中取数  edit by Yuy 180712 
                case "17": srtmsg = settabe17(date, data, quanxian); break;//下午十三、青岛市各海水浴场海浪、水温预报
                case "18": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe18(date, data); break;//下午十四、小麦岛24小时潮汐预报 金沙滩预报取消 取消从下午三中取数  dit by Yuy 180712
                case "19": srtmsg = settabe19(date, data, quanxian); break;//下午十五、青岛周边海域24小时海浪、水温预报
                //case "20": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe20(date, data); break;//下午十六、青岛沿岸48小时潮汐预报 取消从下午三中取数 edit by Yuy 180712
                case "21": srtmsg = settabe21(date, data, quanxian); break;//下午十七、威海电视台未来24小时预报
                case "22": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe22(date, data); break;//下午十八、威海24小时潮汐预报 其中威海预报取消 取消从下午三中取数 edit by Yuy 180712
                case "23": srtmsg = settabe23(date, data, quanxian); break;
                case "24": srtmsg = (quanxian.ToLower() != "hb") ? "editsuccess" : settabe24(date, data); break; //十一、东营胜利油田专项海冰周报
                case "25": srtmsg = settabe25(date, data, quanxian); break;//十、指挥处上午预报
                case "26": srtmsg = settabe26(date, data, quanxian); break;//下午十九、指挥处下午预报
                case "27": srtmsg = settabe27(date, data, quanxian); break;//三、3天海洋水文气象预报综述
                case "28": srtmsg = settabe28(date, data, quanxian); break;//六、24小时水文气象预报综述
                case "29": srtmsg = settabe29(date, data, quanxian); break;//七、7天渤海海区及黄河海港风、浪预报
                case "30": srtmsg = settabe30(date, data, quanxian); break;//八、7天海洋水文气象预报综述
                case "31": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe31(date, data); break;//九、7天港口潮位预报
                case "32": srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : settabe32(date, data); break;//十二、东营胜利油田专项海温周报
                //case "33": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe33(date, data); break;//十二、东营胜利油田专项海温周报
                //case "34": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe34(date, data); break;//十二、东营胜利油田专项海温周报
                case "35": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe35(date, data, quanxian); break;//下午渔政局
                //case "36": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe36(date, data); break;//上午潍坊24小时潮汐 
                // 修改 删除上午九edit by yuy 180710
                case "39": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe39(date, data); break;//上午七、海上丝绸之路三天海浪、气象预报
                case "38": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe38(date, data); break;//上午八、海上丝绸之路三天潮汐预报
                case "37": srtmsg = settabe37(date, data, quanxian); break;//下午四、青岛24小时潮位预报
                case "41": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe41(date, data); break;//下午二十一、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
                case "42": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settabe42(date, data); break;//下午二十二、 东营埕岛-未来三天高/低潮预报
                case "43": srtmsg = (quanxian.ToLower() != "cx" && quanxian.ToLower() != "fl") ? "editsuccess" : settabe43(date, data); break;//下午二十二、 东营埕岛-未来三天高/低潮预报
                case "44": srtmsg = settabe44(date, data, quanxian); break;//上午指挥处
                case "45": srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : settabe45(date, data, quanxian); break;//上午渔政局
                case "46": srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : settable46(date, data); break;//下午三潮汐潮高数据
                case "47":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable47(date, data);//海浪预报
                    break;
                case "48"://潮汐预报
                    srtmsg += (quanxian.ToLower() != "cx") ? "editsuccess" : setTable48(date, data);
                    //if (quanxian.ToLower() == "cx")
                    //{
                    //    TideCurve(date);//生成潮汐数据图片并上传到ftp服务器
                    //}
                    break;
                case "49":
                    srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable49(date, data);//海温预报
                    break;
                case "50":
                    srtmsg = setTable50(date, data,quanxian);//海温预报
                    break;
                case "51":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable51(date, data);//海温预报
                    break;
                case "52":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable52(date, data);//海温预报
                    break;
                case "53":
                    srtmsg = setTable53(date, data);//海温预报
                    break;
                case "54":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable54(date, data);//下午二十四、东营广利渔港-未来三天高/低潮预报
                    break;
                case "55":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable55(date, data);//下午二十五、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）
                    break;
                case "56":
                    srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable56(date, data);//下午二十六、东营广利渔港-未来三天的海面水温预报
                    break;
                case "57":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable57(date, data);//下午二十七、日照桃花岛-未来三天高/低潮预报
                    break;
                case "58":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable58(date, data);//下午二十八、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
                    break;
                case "59":
                    srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable59(date, data);//下午二十九、日照桃花岛-未来三天的海面水温预报
                    break;
                case "60":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable60(date, data);//下午三十、潍坊度假区-未来三天高/低潮预报
                    break;
                case "61":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable61(date, data);//下午三十一、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）
                    break;
                case "62":
                    srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable62(date, data);//下午三十二、潍坊度假区-未来三天的海面水温预报
                    break;
                case "63":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable63(date, data);//下午三十三、威海新区-未来三天高/低潮预报
                    break;
                case "64":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable64(date, data);//下午三十四、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）
                    break;
                case "65":
                    srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable65(date, data);//下午三十五、威海新区-未来三天的海面水温预报
                    break;
                case "66":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable66(date, data);//下午三十六、烟台清泉-未来三天高/低潮预报
                    break;
                case "67":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable67(date, data);//下午三十七、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
                    break;
                case "68":
                    srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable68(date, data);//下午三十八、烟台清泉-未来三天的海面水温预报
                    break;
                case "69":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable69(date, data);//下午三十九、董家口-未来三天高/低潮预报
                    break;
                case "70":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable70(date, data);//下午四十、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
                    break;
                case "71":
                    srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable71(date, data);//下午四十一、董家口-未来三天的海面水温预报
                    break;
                case "72":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable72(date, data);//下午四十二、东营渔港-未来三天高/低潮预报
                    break;
                case "73":
                    srtmsg = (quanxian.ToLower() != "fl") ? "editsuccess" : setTable73(date, data);//下午四十三、东营渔港 - 未来三天的海面风及海浪有效波高预报（20时起报）
                    break;
                case "74":
                    srtmsg = (quanxian.ToLower() != "sw") ? "editsuccess" : setTable74(date, data);//下午四十四、东营渔港-未来三天的海面水温预报
                    break;
                //下午潮汐预报拆分方法
                case "75":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable75(date, data, context);//将下午三的地区拆分到多个方法中添加,只是添加潮时
                    break;
                case "76":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable76(date, data, context);//将下午三的地区拆分到多个方法里面，此处是添加潮高数据
                    break;
                case "77":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable77(date, data, context);//下午三的数据同时添加到另一个TBLREFINETIDE表中，主要针对新的下午1,4,12,11
                    break;
                case "82":
                    srtmsg = (quanxian.ToLower() != "cx") ? "editsuccess" : setTable82(date, data, context);//下午16的多个地区数据拆分到多个方法里面
                    break;
                default:
                    break;
            }

            switch (srtmsg)
            {
                case "addsuccess": logdaima = "add_table"; logmsg = "新增表单" + type + "数据成功！"; break;
                case "adderror": logdaima = "add_table"; logmsg = "新增表单" + type + "数据失败！"; break;
                case "editsuccess": logdaima = "edit_table"; logmsg = "修改表单" + type + "数据成功！"; break;
                case "editerror": logdaima = "edit_table"; logmsg = "修改表单" + type + "数据失败！"; break;
                default:
                    break;
            }
            Sql_Caozuorizhi.WriteRizhi(userid, logdaima, logmsg);
            context.Response.Write(srtmsg);
            //}
            //else {
            ////未登录
            //context.Response.Write("-1");
            //}
        }

        #region 增加或修改表单数据

        /// <summary>
        /// 表单01数据
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public string settabe01(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            int dayCount = 3;
            sql_TBLYRBHWINDWAVE72HFORECASTTWO sql = new sql_TBLYRBHWINDWAVE72HFORECASTTWO();
            TBLYRBHWINDWAVE72HFORECASTTWO model = new TBLYRBHWINDWAVE72HFORECASTTWO();
            model.PUBLISHDATE = date;
            model.FORECASTDATE = date;          //预报日期
            //DataTable dt = (DataTable)sql.get_TBLYRBHWINDWAVE72HFORECASTTWO_AllData(model);
            DataTable dt = (DataTable)sql.get_TBLYRBHWINDWAVE72HFORECASTTWO_3Daysata(model);

            if (dt.Rows.Count > 0)//有数据 修改
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }

            //if(date.DayOfWeek ==DayOfWeek.Monday)
            //{
            //    dayCount = 7;
            //}
            for (int i = 0; i < dayCount; i++)
            {
                model.REPORTAREA = "渤海";                           //区域
                model.FORECASTDATE = date.AddDays((i + 1));          //预报日期
                model.YRBHWWFWAVEHEIGHT = tbdata[(i * 5)];           //波高
                model.YRBHWWFWAVEDIR = tbdata[(i * 5) + 1];          //波向
                model.YRBHWWFFLOWDIR = tbdata[(i * 5) + 2];          //风向
                model.YRBHWWFFLOWLEVEL = tbdata[(i * 5) + 3];        //风力
                model.YRBHWWFWATERTEMPERATURE = tbdata[(i * 5) + 4]; //水温
                if (quanxian == "fl" || quanxian == "sw")
                {
                    if (type == "add")
                    {
                        addnum += sql.Add_TBLYRBHWINDWAVE72HFORECASTTWO(model);
                    }
                    else if (type == "edit")
                    {
                        editnum += sql.EditTBLYRBHWINDWAVE72HFORECASTTWO(model, quanxian);
                    }
                }
                else if (quanxian == "cx" || quanxian == "hb")
                {
                    return "addsuccess";
                }
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.REPORTAREA = "黄河海港";                        //区域
                model.FORECASTDATE = date.AddDays((i + 1));           //预报日期
                model.YRBHWWFWAVEHEIGHT = tbdata[(i * 5) + 5 * dayCount];       //波高
                model.YRBHWWFWAVEDIR = tbdata[(i * 5) + 5 * dayCount + 1];          //波向
                model.YRBHWWFFLOWDIR = tbdata[(i * 5) + 5 * dayCount + 2];          //风向
                model.YRBHWWFFLOWLEVEL = tbdata[(i * 5) + 5 * dayCount + 3];        //风力
                model.YRBHWWFWATERTEMPERATURE = tbdata[(i * 5) + 5 * dayCount + 4]; //水温
                if (quanxian == "fl" || quanxian == "sw")
                {
                    if (type == "add")
                    {
                        addnum += sql.Add_TBLYRBHWINDWAVE72HFORECASTTWO(model);
                    }
                    else if (type == "edit")
                    {
                        editnum += sql.EditTBLYRBHWINDWAVE72HFORECASTTWO(model, quanxian);
                    }
                }
                else if (quanxian == "cx" || quanxian == "hb")
                {
                    return "addsuccess";
                }
            }
            if (type == "add")
            {
                if (addnum == 2 * dayCount)
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
                if (editnum == 2 * dayCount)
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
        /// 表单02数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe02(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            int dayCount = 3;
            sql_TBLHARBOURTIDELEVEL sql = new sql_TBLHARBOURTIDELEVEL();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLHARBOURTIDELEVEL_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            //if (date.DayOfWeek == DayOfWeek.Monday)
            //{
            //    dayCount = 7;
            //}
            for (int i = 0; i < dayCount; i++)
            {
                // model.PUBLISHDATE //填报日期
                model.HTLHARBOUR = "龙口港"; //港口
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.HTLFIRSTWAVEOFTIME = tbdata[(i * 8)];  //第一次高潮时间
                model.HTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 1];  //第一次高潮潮位
                model.HTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 2]; //第一次低潮时间
                model.HTLLOWTIDELEVELFORTHEFIRSTTIME = tbdata[(i * 8) + 3]; //第一次低潮潮位
                model.HTLSECONDWAVEOFTIME = tbdata[(i * 8) + 4]; //第二次高潮时间
                model.HTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 5]; //第二次高潮潮位
                model.HTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 6]; //第二次低潮时间
                model.HTLLOWTIDELEVELFORTHESECONDTIM = tbdata[(i * 8) + 7]; //第二次低潮潮位

                
                if (type == "add")
                {
                    addnum += sql.Add_TBLHARBOURTIDELEVEL(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLHARBOURTIDELEVEL(model);
                }
            }

            for (int i = 0; i < dayCount; i++)
            {
                model.HTLHARBOUR = "黄河海港"; //港口
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.HTLFIRSTWAVEOFTIME = tbdata[(i * 8) + 8 * dayCount];  //第一次高潮时间
                model.HTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 8 * dayCount + 1];  //第一次高潮潮位
                model.HTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 8 * dayCount + 2]; //第一次低潮时间
                model.HTLLOWTIDELEVELFORTHEFIRSTTIME = tbdata[(i * 8) + 8 * dayCount + 3]; //第一次低潮潮位
                model.HTLSECONDWAVEOFTIME = tbdata[(i * 8) + 8 * dayCount + 4]; //第二次高潮时间
                model.HTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 8 * dayCount + 5]; //第二次高潮潮位
                model.HTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 8 * dayCount + 6]; //第二次低潮时间
                model.HTLLOWTIDELEVELFORTHESECONDTIM = tbdata[(i * 8) + 8 * dayCount + 7]; //第二次低潮潮位
                if (type == "add")
                {
                    addnum += sql.Add_TBLHARBOURTIDELEVEL(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLHARBOURTIDELEVEL(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 2 * dayCount)
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
                if (editnum == 2 * dayCount)
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
        /// 表单03数据
        /// 1127  贾 ------ 添加quanxian参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe03(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLEXPECTEDFUTURE24HWAVEWATER sql = new sql_TBLEXPECTEDFUTURE24HWAVEWATER();
            TBLEXPECTEDFUTURE24HWAVEWATER model = new TBLEXPECTEDFUTURE24HWAVEWATER();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLEXPECTEDFUTURE24HWAVEWATER_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }

            //填报日期
            model.EFWWBHLOWESTWAVE = tbdata[0]; //渤海最低浪高
            model.EFWWBHHIGHESTWAVE = tbdata[1]; //渤海最高浪高
            model.EFWWBHWAVETYPE = tbdata[2]; //渤海浪高类型
            model.EFWWBHNORTHLOWESTWAVE = tbdata[3]; //黄海北部最低浪高
            model.EFWWBHNORTHHIGHESTWAVE = tbdata[4]; //黄海北部最高浪高
            model.EFWWBHNORTHWAVETYPE = tbdata[5]; //黄海北部浪高类型
            model.EFWWDKSEAAREAWAVEHEIGHT = tbdata[6]; //刁口海域浪高
            model.EFWWDKSEAAREAWATERTEMPE = tbdata[7]; //刁口海域水温
            model.EFWWHHKSEAAREAWAVEHEIGHT = tbdata[8]; //黄河口海域浪高
            model.EFWWHHKSEAAREAWATERTEMP = tbdata[9]; //黄河口水温
            model.EFWWGLGSEAAREAWAVEHEIGHT = tbdata[10]; //广利港海域浪高
            model.EFWWGLGSEAAREAWATERTEMP = tbdata[11]; //广利港水温
            model.EFWWDYGWAVEHEIGHT = tbdata[12]; //东营港浪高
            model.EFWWDYGWATERTEMPERATURE = tbdata[13]; //东营港水温
            model.EFWWXHWAVEHEIGHT = tbdata[14]; //新户海域浪高
            model.EFWWXHWATERTEMPERATURE = tbdata[15]; //新户海域水温
            model.EFWWCKWAVEHEIGHT = tbdata[16]; //埕口海域浪高
            model.EFWWCKWATERTEMPERATURE = tbdata[17]; //埕口海域水温
            if (quanxian == "fl" || quanxian == "sw")
            {
                if (type == "add")
                {
                    addnum += sql.Add_TBLEXPECTEDFUTURE24HWAVEWATER(model, quanxian);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLEXPECTEDFUTURE24HWAVEWATER(model, quanxian);
                }
            }
            else if (quanxian == "cx" || quanxian == "hb")
            {
                return "addsuccess";
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
        /// 表单04数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe04(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBL24HTIDELEVEL sql = new sql_TBL24HTIDELEVEL();
            TBL24HTIDELEVEL model = new TBL24HTIDELEVEL();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBL24HTIDELEVEL_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }

            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0: model.TLFORECASTSTANCE = "飞雁滩"; break;
                    case 1: model.TLFORECASTSTANCE = "孤东"; break;
                    case 2: model.TLFORECASTSTANCE = "小岛河"; break;
                    case 3: model.TLFORECASTSTANCE = "东营港"; break;
                    case 4: model.TLFORECASTSTANCE = "桩西"; break;
                    case 5: model.TLFORECASTSTANCE = "新户"; break;
                    default:
                        break;
                }
                //预报站位
                model.FORECASTDATE = date.AddDays(1); //预报日期
                model.TLFIRSTWAVEOFTIME = tbdata[(i * 8)];//第一次高潮时间
                model.TLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 1]; //第一次高潮潮位
                model.TLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 2]; //第一次低潮时间
                model.TLLOWTIDELEVELFORTHEFIRSTTIME = tbdata[(i * 8) + 3]; //第一次低潮潮位
                model.TLSECONDWAVEOFTIME = tbdata[(i * 8) + 4]; //第二次高潮时间
                model.TLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 5]; //第二次高潮潮位
                model.TLSECONDTIMELOWTIDE = tbdata[(i * 8) + 6]; //第二次低潮时间
                model.TLLOWTIDELEVELFORTHESECONDTIME = tbdata[(i * 8) + 7]; //第二次低潮潮位

                if (type == "add")
                {
                    addnum += sql.Add_TBL24HTIDELEVEL(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBL24HTIDELEVEL(model);
                }
            }


            if (type == "add")
            {
                if (addnum == 6)
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
                if (editnum == 6)
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
        /// 表单05数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>山东省近海七市3天海浪、水温预报
        public string settabe05(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLEACHSEAAREA24HSEAWAVE sql = new sql_TBLEACHSEAAREA24HSEAWAVE();
            TBLEACHSEAAREA24HSEAWAVE model = new TBLEACHSEAAREA24HSEAWAVE();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLEACHSEAAREA24HSEAWAVE_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0: model.ESASWAREA = "渤海"; break;
                    case 1: model.ESASWAREA = "黄海北部"; break;
                    case 2: model.ESASWAREA = "黄海中部"; break;
                    case 3: model.ESASWAREA = "黄海南部"; break;
                    default:
                        break;
                }
                model.ESASWLOWESTWAVEHEIGHT = tbdata[(i * 3)];//最低浪高
                model.ESASWHIGHTESTWAVEHEIGHT = tbdata[(i * 3) + 1]; //最高浪高
                model.ESASWWAVETYPE = tbdata[(i * 3) + 2];//浪高类型
                //预报站位
                if (type == "add")
                {
                    addnum += sql.Add_TBLEACHSEAAREA24HSEAWAVE(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLEACHSEAAREA24HSEAWAVE(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 下午二、山东省近海七市3天海浪、水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe06(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSDOFFSHORESEVENCITY24HWAVE sql = new sql_TBLSDOFFSHORESEVENCITY24HWAVE();
            TBLSDOFFSHORESEVENCITY24HWAVE model = new TBLSDOFFSHORESEVENCITY24HWAVE();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(model);
            // TBLSDOFFSHORESEVENCITY3DWAVE model = new TBLSDOFFSHORESEVENCITY3DWAVE();
            //DataTable dt = new DataTable();
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }

            for (int i = 0; i < 7; i++)
            {
                switch (i)
                {
                    case 0: model.SDOSCWAREA = "日照近海"; break;
                    case 1: model.SDOSCWAREA = "青岛近海"; break;
                    case 2: model.SDOSCWAREA = "威海近海"; break;
                    case 3: model.SDOSCWAREA = "烟台近海"; break;
                    case 4: model.SDOSCWAREA = "潍坊近海"; break;
                    case 5: model.SDOSCWAREA = "东营近海"; break;
                    case 6: model.SDOSCWAREA = "滨州近海"; break;
                    default:
                        break;
                }
                //model.SDOSCWAREA = (i < 3) ? "日照近海" : (i > 2 && i < 6) ? "青岛近海" : (i > 5 && i < 9) ? "威海近海" : (i > 8 && i < 12) ? "烟台近海" : (i > 11 && i < 15) ? "潍坊近海" : (i > 14 && i < 18) ? "东营近海" : "滨州近海";
                model.SDOSCWLOWESTWAVEHEIGHT = tbdata[(i * 6)]; //24小时浪高
                model.SDOSCWESTWAVEHEIGHT48H = tbdata[(i * 6) + 1]; //48小时浪高
                model.SDOSCWESTWAVEHEIGHT72H = tbdata[(i * 6) + 2]; //72小时浪高
                model.SDOSCWSURFACETEMPERATURE = tbdata[(i * 6) + 3]; //24小时水温
                model.SDOSCWSURFACETEMPERATURE48H = tbdata[(i * 6) + 4]; //48小时水温
                model.SDOSCWSURFACETEMPERATURE72H = tbdata[(i * 6) + 5]; //72小时水温
                model.SDOSCWHIGHTESTWAVEHEIGHT = "";

                if (quanxian == "fl" || quanxian == "sw")
                {
                    if (type == "add")
                    {
                        addnum += sql.Add_TBLSDOFFSHORESEVENCITY24HWAVE(model, quanxian);
                    }
                    else if (type == "edit")
                    {
                        editnum += sql.Edit_TBLSDOFFSHORESEVENCITY24HWAVE(model, quanxian);
                    }
                }
                else if (quanxian == "cx")
                {
                    return "addsuccess";
                }
            }


            if (type == "add")
            {
                if (addnum == 7)
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
                if (editnum == 7)
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
        /// 下午三、潮汐数据潮时提交
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe07(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSDOFFSHORESEVENCITY24HTIDE sql = new sql_TBLSDOFFSHORESEVENCITY24HTIDE();
            TBLSDOFFSHORESEVENCITY24HTIDE model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 21; i++)
            {
                switch (i)
                {
                    case 0:
                        model.SDOSCTCITY = "日照";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 1:
                        model.SDOSCTCITY = "日照";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 2:
                        model.SDOSCTCITY = "日照";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 3:
                        model.SDOSCTCITY = "青岛";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 4:
                        model.SDOSCTCITY = "青岛";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 5:
                        model.SDOSCTCITY = "青岛";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 6:
                        model.SDOSCTCITY = "威海";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 7:
                        model.SDOSCTCITY = "威海";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 8:
                        model.SDOSCTCITY = "威海";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 9:
                        model.SDOSCTCITY = "烟台";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 10:
                        model.SDOSCTCITY = "烟台";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 11:
                        model.SDOSCTCITY = "烟台";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 12:
                        model.SDOSCTCITY = "潍坊";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 13:
                        model.SDOSCTCITY = "潍坊";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 14:
                        model.SDOSCTCITY = "潍坊";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 15:
                        model.SDOSCTCITY = "东营";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 16:
                        model.SDOSCTCITY = "东营";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 17:
                        model.SDOSCTCITY = "东营";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 18:
                        model.SDOSCTCITY = "滨州";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 19:
                        model.SDOSCTCITY = "滨州";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 20:
                        model.SDOSCTCITY = "滨州";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    default:
                        break;
                }
                for (int j = 0; j < 4; j++)
                {
                    var str = tbdata[(i * 4) + j];
                    var h = "";
                    var min = "";

                    if (str!= "" && str.IndexOf("-") == -1)
                    {
                        h = str.Substring(0, 2);
                        min = str.Substring(2);
                    }
                    else
                    {
                        if (str.Length >= 2)
                        {
                            h = str.Substring(0, 1);
                            min = str.Substring(1);
                        }
                        else
                        {
                            h = "-";
                            min = "-";
                        }
                    }
                    switch (j)
                    {
                        case 0:
                            model.SDOSCTFIRSTHIGHWAVEHOUR = h; //第一次高潮时
                            model.SDOSCTFIRSTHIGHWAVEMINUTE = min;//第一次高潮分
                            break;
                        case 1:
                            model.SDOSCTSECONDHIGHWAVEHOUR = h; //第二次高潮时
                            model.SDOSCTSECONDHIGHWAVEMINUTE = min; //第二次高潮分
                            break;
                        case 2:
                            model.SDOSCTFIRSTLOWWAVEHOUR = h; //第一次低潮时
                            model.SDOSCTFIRSTLOWWAVEMINUTE = min; //第一次低潮分
                            break;
                        case 3:
                            model.SDOSCTSECONDLOWWAVEHOUR = h; //第二次低潮时
                            model.SDOSCTSECONDLOWWAVEMINUTE = min; //第二次低潮分
                            break;
                        default: break;
                    }
                }
                //model.SDOSCTFIRSTHIGHWAVEHOUR = tbdata[(i * 8)]; //第一次高潮时
                //model.SDOSCTFIRSTHIGHWAVEMINUTE = tbdata[(i * 8) + 1];//第一次高潮分
                //model.SDOSCTSECONDHIGHWAVEHOUR = tbdata[(i * 8) + 2]; //第二次高潮时
                //model.SDOSCTSECONDHIGHWAVEMINUTE = tbdata[(i * 8) + 3]; //第二次高潮分
                //model.SDOSCTFIRSTLOWWAVEHOUR = tbdata[(i * 8) + 4]; //第一次低潮时
                //model.SDOSCTFIRSTLOWWAVEMINUTE = tbdata[(i * 8) + 5]; //第一次低潮分
                //model.SDOSCTSECONDLOWWAVEHOUR = tbdata[(i * 8) + 6]; //第二次低潮时
                //model.SDOSCTSECONDLOWWAVEMINUTE = tbdata[(i * 8) + 7]; //第二次低潮分
                if (type == "add")
                {
                    addnum += sql.Add_TBLSDOFFSHORESEVENCITY24HTIDE(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLSDOFFSHORESEVENCITY24HTIDE(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 21)
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
                if (editnum == 21)
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
        /// 下午三、潮汐数据潮高提交
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settable46(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TideData sql = new sql_TideData();
            HT_TideData model = new HT_TideData();
            model.PUBLISHDATE = date;//填报日期
            DataTable dt = (DataTable)sql.getTideData(model);
            if (dt != null && dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 21; i++)
            {
                switch (i)
                {
                    case 0:
                        model.SDOSCTCITY = "日照";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 1:
                        model.SDOSCTCITY = "日照";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 2:
                        model.SDOSCTCITY = "日照";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 3:
                        model.SDOSCTCITY = "青岛";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 4:
                        model.SDOSCTCITY = "青岛";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 5:
                        model.SDOSCTCITY = "青岛";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 6:
                        model.SDOSCTCITY = "威海";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 7:
                        model.SDOSCTCITY = "威海";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 8:
                        model.SDOSCTCITY = "威海";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 9:
                        model.SDOSCTCITY = "烟台";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 10:
                        model.SDOSCTCITY = "烟台";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 11:
                        model.SDOSCTCITY = "烟台";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 12:
                        model.SDOSCTCITY = "潍坊";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 13:
                        model.SDOSCTCITY = "潍坊";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 14:
                        model.SDOSCTCITY = "潍坊";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 15:
                        model.SDOSCTCITY = "东营";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 16:
                        model.SDOSCTCITY = "东营";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 17:
                        model.SDOSCTCITY = "东营";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    case 18:
                        model.SDOSCTCITY = "滨州";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 19:
                        model.SDOSCTCITY = "滨州";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 20:
                        model.SDOSCTCITY = "滨州";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    default:
                        break;
                }
                model.FIRSTHIGHWAVETIDEDATA = tbdata[(i * 4)]; //第一次高潮潮高
                model.SECONDHIGHWAVETIDEDATA = tbdata[(i * 4) + 1];//第二次高潮潮高
                model.FIRSTLOWWAVETIDEDATA = tbdata[(i * 4) + 2]; //第一次低潮潮高
                model.SECONDLOWWAVETIDEDATA = tbdata[(i * 4) + 3]; //第二次低潮潮高
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideDate(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 21)
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
                if (editnum == 21)
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
        /// 表单08数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe08(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLQD24HTIDELEVEL sql = new sql_TBLQD24HTIDELEVEL();
            TBLQD24HTIDELEVEL model = new TBLQD24HTIDELEVEL();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLQD24HTIDELEVEL_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.QDTLFIRSTHIGHWAVEHOUR = "-"; //第一次高潮时
            model.QDTLFIRSTHIGHWAVEMINUTE = "-";//第一次高潮分
            model.QDTLFIRSTHIGHWAVEHEIGHT = "-"; //第一次高潮高度
            model.QDTLFIRSTLOWWAVEHOUR = "-"; //第一次低潮时
            model.QDTLFIRSTLOWWAVEMINUTE = "-";//第一次低潮分
            model.QDTLFIRSTLOWWAVEHEIGHT = "-"; //第一次低潮高度
            model.QDTLSECONDHIGHWAVEHOUR = "-"; //第二次高潮时
            model.QDTLSECONDHIGHWAVEMINUTE = "-"; //第二次高潮分
            model.QDTLSECONDHIGHWAVEHEIGHT = "-"; //第二次高潮高度
            model.QDTLSECONDLOWWAVEHOUR = "-";//第二次低潮时
            model.QDTLSECONDLOWWAVEMINUTE = "-"; //第二次低潮分
            model.QDTLSECONDLOWWAVEHEIGHT = "-"; //第二次低潮高度
            model.QDTLTOMORROWWAVEHEIGHT = tbdata[0]; //明日滨海浪高
            model.QDTLTOMORROWWAVEDIR = tbdata[1]; //浪向
            if (quanxian == "fl" || quanxian == "cx")
            {
                if (type == "add")
                {
                    addnum += sql.Add_TBLQD24HTIDELEVEL(model, quanxian);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLQD24HTIDELEVEL(model, quanxian);
                }
            }
            else
            {
                return "addsuccess";
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
        /// 表单09数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe09(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLYRSOUTHSEAWALL24WINDWAVE sql = new sql_TBLYRSOUTHSEAWALL24WINDWAVE();
            TBLYRSOUTHSEAWALL24WINDWAVE model = new TBLYRSOUTHSEAWALL24WINDWAVE();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLYRSOUTHSEAWALL24WINDWAVE_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 3; i++)
            {
                model.FORECASTDATE = date.AddDays((i + 1)); //预报日期
                model.YRSSWWWAVEHEIGHT = tbdata[(i * 4)]; //波高
                model.YRSSWWWAVEDIRECTION = tbdata[(i * 4) + 1]; //波向
                model.YRSSWWWINDDIRECTION = tbdata[(i * 4) + 2];  //风向
                model.YRSSWWWINDFORCE = tbdata[(i * 4) + 3];  //风力
                if (type == "add")
                {
                    addnum += sql.Add_TBLYRSOUTHSEAWALL24WINDWAVE(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLYRSOUTHSEAWALL24WINDWAVE(model);
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
            else
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

        }

        /// <summary>
        /// 表单10数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe10(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLMZZTIDELEVEL sql = new sql_TBLMZZTIDELEVEL();
            TBLMZZTIDELEVEL model = new TBLMZZTIDELEVEL();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLMZZTIDELEVEL_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 3; i++)
            {
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.MZZTLFIRSTWAVEOFTIME = tbdata[(i * 8)]; //第一次高潮时间
                model.MZZTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 1]; //第一次高潮潮位
                model.MZZTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 2];//第一次低潮时间
                model.MZZTLLOWTIDELEVELFORTHEFIRSTTI = tbdata[(i * 8) + 3]; //第一次低潮潮位
                model.MZZTLSECONDWAVEOFTIME = tbdata[(i * 8) + 4]; //第二次高潮时间
                model.MZZTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 5]; //第二次高潮潮位
                model.MZZTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 6]; //第二次低潮时间
                model.MZZTLLOWTIDELEVELFORTHESECONDT = tbdata[(i * 8) + 7]; //第二次低潮潮位

                if (type == "add")
                {
                    addnum += sql.Add_TBLMZZTIDELEVEL(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLMZZTIDELEVEL(model);
                }
            }
            //同时将数据存入到下午22中
            //setTable54(date,data);
            SetTableDYGL(date,data);

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
            else
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
        }

        /// <summary>
        /// 提交表单10同时将数据存入到老的下午22的表中
        /// 前台数据位置发生变化
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        private string SetTableDYGL(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTideData("DYGLFP", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 1];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 2];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 3];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 4];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 5];
                model.SECONDLOWTIME = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = "DYGLFP";
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
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
        /// 表单11数据
        /// 1127  贾 ------ 添加quanxian参数
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public string settabe11(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLNANPUWAVEFLOWWATERTFORECAST sql = new sql_TBLNANPUWAVEFLOWWATERTFORECAST();
            TBLNANPUWAVEFLOWWATERTFORECAST model = new TBLNANPUWAVEFLOWWATERTFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLNANPUWAVEFLOWWATERTFORECAST_AllData(model);
            if (dt.Rows.Count > 0)//有数据 修改
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            if (quanxian == "fl" || quanxian == "sw")
            {
                for (int i = 0; i < 3; i++)
                {
                    model.FORECASTDATE = date.AddDays((i + 1)); //预报日期
                    model.NWFWTFWAVEHEIGHT = tbdata[(i * 6)];   //波高
                    model.NWFWTFWAVEDIR = tbdata[(i * 6) + 1];     //波向
                    model.NWFWTFFLOWDIR = tbdata[(i * 6) + 2];     //风向
                    model.NWFWTFFLOWLEVEL = tbdata[(i * 6) + 3];     //风力
                    model.NWFWTFWATERTEMPERATURE = tbdata[(i * 6) + 4];     //水温
                    model.NWFWTFWEATHER = tbdata[(i * 6) + 5];    //天气
                    if (type == "add")
                    {
                        addnum += sql.Add_TBLNANPUWAVEFLOWWATERTFORECAST(model, quanxian);
                    }
                    else if (type == "edit")
                    {
                        editnum += sql.Edit_TBLNANPUWAVEFLOWWATERTFORECAST(model, quanxian);
                    }
                }

            }
            else if (quanxian == "cx")
            {

                return "addsuccess";
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
            else
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
        }

        /// <summary>
        /// 表单12数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe12(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLNANPUOILFIELDTIDALFORECAST sql = new sql_TBLNANPUOILFIELDTIDALFORECAST();
            TBLNANPUOILFIELDTIDALFORECAST model = new TBLNANPUOILFIELDTIDALFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLNANPUOILFIELDTIDALFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 3; i++)
            {
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.NOTFFIRSTHIGHWAVETIME = tbdata[(i * 8)]; //第一次高潮时间
                model.NOTFFIRSTHIGHWAVEHEIGHT = tbdata[(i * 8) + 1]; //第一次高潮潮位
                model.NOTFFIRSTLOWWAVETIME = tbdata[(i * 8) + 2];//第一次低潮时间
                model.NOTFFIRSTLOWWAVEHEIGHT = tbdata[(i * 8) + 3]; //第一次低潮潮位
                model.NOTFSECONDHIGHWAVETIME = tbdata[(i * 8) + 4]; //第二次高潮时间
                model.NOTFSECONDHIGHWAVEHEIGHT = tbdata[(i * 8) + 5]; //第二次高潮潮位
                model.NOTFSECONDLOWWAVETIME = tbdata[(i * 8) + 6]; //第二次低潮时间
                model.NOTFSECONDLOWWAVEHEIGHT = tbdata[(i * 8) + 7]; //第二次低潮潮位

                if (type == "add")
                {
                    addnum += sql.Add_TBLNANPUOILFIELDTIDALFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLNANPUOILFIELDTIDALFORECAST(model);
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
            else
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
        }

        /// <summary>
        /// 表单13数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe13(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSEAAREA24HWAVEFORECAST sql = new sql_TBLSEAAREA24HWAVEFORECAST();
            TBLSEAAREA24HWAVEFORECAST model = new TBLSEAAREA24HWAVEFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLSEAAREA24HWAVEFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.SA24HWFBOHAIWAVEHEIGHT = tbdata[0];  //渤海波高
            model.SA24HWFBOHAIWAVEDIR = tbdata[1];  //渤海波向
            model.SA24HWFBOHAISURGEDIR = tbdata[2]; //渤海涌向
            model.SA24HWFNORTHOFYSWAVEHEIGHT = tbdata[3];   //黄海北部波高
            model.SA24HWFNORTHOFYSWAVEDIR = tbdata[4];  //黄海北部波向
            model.SA24HWFNORTHOFYSSURGEDIR = tbdata[5]; //黄海北部涌向
            model.SA24HWFMIDDLEOFYSWAVEHEIGHT = tbdata[6];  //黄海中部波高
            model.SA24HWFMIDDLEOFYSWAVEDIR = tbdata[7]; //黄海中部波向
            model.SA24HWFMIDDLEOFYSSURGEDIR = tbdata[8];    //黄海中部涌向
            model.SA24HWFSOUTHOFYSWAVEHEIGHT = tbdata[9];   //黄海南部波高
            model.SA24HWFSOUTHOFYSWAVEDIR = tbdata[10];  //黄海南部波向
            model.SA24HWFSOUTHOFYSSURGEDIR = tbdata[11]; //黄海南部涌向
            model.SA24HWFQDOFFSHOREWAVEHEIGHT = tbdata[12];  //青岛近岸波高
            model.SA24HWFQDOFFSHOREWAVEDIR = tbdata[13]; //青岛近岸波向
            model.SA24HWFQDOFFSHORESURGESTATE = "";	//青岛近岸有无涌状况 //前台无值、规则未知
            model.SA24HWFQDOFFSHORESURGEDIR = tbdata[14];    //青岛近岸涌向
            model.SA24HWFBOHAIWAVENOTES = tbdata[15];    //渤海海浪备注
            model.SA24HWFNORTHOFYSWAVENOTES = tbdata[16];    //黄海北部海浪备注
            model.SA24HWFMIDDLEOFYSWAVENOTES = tbdata[17];   //黄海中部海浪备注
            model.SA24HWFSOUTHOFYSWAVENOTES = tbdata[18];    //黄海南部海浪备注
            model.SA24HWFQDOFFSHOREWAVENOTES = tbdata[19];   //青岛近岸海浪备注
            model.SA24HWFBOHAIWAVETYPE = "";  //渤海波级
            model.SA24HWFNORTHOFYSWAVETYPE = "";  //黄海北部波级
            model.SA24HWFMIDDLEOFYSWAVETYPE = "";  //黄海中部波级

            if (type == "add")
            {
                addnum += sql.Add_TBLSEAAREA24HWAVEFORECAST(model);
            }
            else if (type == "edit")
            {
                editnum += sql.Edit_TBLSEAAREA24HWAVEFORECAST(model);
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
        /// 表单14数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe14(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSEAAREA48HWAVEFORECAST sql = new sql_TBLSEAAREA48HWAVEFORECAST();
            TBLSEAAREA48HWAVEFORECAST model = new TBLSEAAREA48HWAVEFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLSEAAREA48HWAVEFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.SA48HWFBOHAIWAVEHEIGHT = tbdata[0];  //渤海波高
            model.SA48HWFBOHAIWAVEDIR = tbdata[1];  //渤海波向
            model.SA48HWFBOHAISURGEDIR = tbdata[2]; //渤海涌向
            model.SA48HWFNORTHOFYSWAVEHEIGHT = tbdata[3];   //黄海北部波高
            model.SA48HWFNORTHOFYSWAVEDIR = tbdata[4];  //黄海北部波向
            model.SA48HWFNORTHOFYSSURGEDIR = tbdata[5]; //黄海北部涌向
            model.SA48HWFMIDDLEOFYSWAVEHEIGHT = tbdata[6];  //黄海中部波高
            model.SA48HWFMIDDLEOFYSWAVEDIR = tbdata[7]; //黄海中部波向
            model.SA48HWFMIDDLEOFYSSURGEDIR = tbdata[8];    //黄海中部涌向
            model.SA48HWFSOUTHOFYSWAVEHEIGHT = tbdata[9];   //黄海南部波高
            model.SA48HWFSOUTHOFYSWAVEDIR = tbdata[10];  //黄海南部波向
            model.SA48HWFSOUTHOFYSSURGEDIR = tbdata[11]; //黄海南部涌向
            model.SA48HWFBOHAIWAVENOTES = tbdata[12];    //渤海海浪备注
            model.SA48HWFNORTHOFYSWAVENOTES = tbdata[13];    //黄海北部海浪备注
            model.SA48HWFMIDDLEOFYSWAVENOTES = tbdata[14];   //黄海中部海浪备注
            model.SA48HWFSOUTHOFYSWAVENOTES = tbdata[15];    //黄海南部海浪备注

            if (type == "add")
            {
                addnum += sql.Add_TBLSEAAREA48HWAVEFORECAST(model);
            }
            else if (type == "edit")
            {
                editnum += sql.Edit_TBLSEAAREA48HWAVEFORECAST(model);
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
        /// 表单15数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe15(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSEAAREA72HWAVEFORECAST sql = new sql_TBLSEAAREA72HWAVEFORECAST();
            TBLSEAAREA72HWAVEFORECAST model = new TBLSEAAREA72HWAVEFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLSEAAREA72HWAVEFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.SA72HWFBOHAIWAVEHEIGHT = tbdata[0];  //渤海波高
            model.SA72HWFBOHAIWAVEDIR = tbdata[1];  //渤海波向
            model.SA72HWFBOHAISURGEDIR = tbdata[2]; //渤海涌向
            model.SA72HWFNORTHOFYSWAVEHEIGHT = tbdata[3];   //黄海北部波高
            model.SA72HWFNORTHOFYSWAVEDIR = tbdata[4];  //黄海北部波向
            model.SA72HWFNORTHOFYSSURGEDIR = tbdata[5]; //黄海北部涌向
            model.SA72HWFMIDDLEOFYSWAVEHEIGHT = tbdata[6];  //黄海中部波高
            model.SA72HWFMIDDLEOFYSWAVEDIR = tbdata[7]; //黄海中部波向
            model.SA72HWFMIDDLEOFYSSURGEDIR = tbdata[8];    //黄海中部涌向
            model.SA72HWFSOUTHOFYSWAVEHEIGHT = tbdata[9];   //黄海南部波高
            model.SA72HWFSOUTHOFYSWAVEDIR = tbdata[10];  //黄海南部波向
            model.SA72HWFSOUTHOFYSSURGEDIR = tbdata[11]; //黄海南部涌向
            model.SA72HWFBOHAIWAVENOTES = tbdata[12];    //渤海海浪备注
            model.SA72HWFNORTHOFYSWAVENOTES = tbdata[13];    //黄海北部海浪备注
            model.SA72HWFMIDDLEOFYSWAVENOTES = tbdata[14];   //黄海中部海浪备注
            model.SA72HWFSOUTHOFYSWAVENOTES = tbdata[15];    //黄海南部海浪备注

            if (type == "add")
            {
                addnum += sql.Add_TBLSEAAREA72HWAVEFORECAST(model);
            }
            else if (type == "edit")
            {
                editnum += sql.Edit_TBLSEAAREA72HWAVEFORECAST(model);
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
        /// 表单16数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe16(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLWF24HTIDALFORECAST sql = new sql_TBLWF24HTIDALFORECAST();
            TBLWF24HTIDALFORECAST model = new TBLWF24HTIDALFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLWF24HTIDALFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.WF24HTFFIRSTHIGHWAVETIME = tbdata[0];//第一次高潮潮时
            model.WF24HTFFIRSTHIGHWAVEHEIGHT = tbdata[1]; //第一次高潮潮高
            model.WF24HTFSECONDHIGHWAVETIME = tbdata[2]; //第二次高潮潮时
            model.WF24HTFSECONDHIGHWAVEHEIGHT = tbdata[3];//第二次高潮潮高
            model.WF24HTFFIRSTLOWWAVETIME = tbdata[4]; //第一次低潮潮时
            model.WF24HTFFIRSTLOWWAVEHEIGHT = tbdata[5];//第一次低潮潮高
            model.WF24HTFSECONDLOWWAVETIME = tbdata[6];//第二次低潮潮时
            model.WF24HTFSECONDLOWWAVEHEIGHT = tbdata[7]; //第二次低潮潮高

            if (type == "add")
            {
                addnum += sql.Add_TBLWF24HTIDALFORECAST(model);
            }
            else if (type == "edit")
            {
                editnum += sql.Edit_TBLWF24HTIDALFORECAST(model);
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
        /// 表单17数据
        /// 1127  贾 ------ 添加quanxian参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe17(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSEABEACH24HWAVEFORECAST sql = new sql_TBLSEABEACH24HWAVEFORECAST();
            TBLSEABEACH24HWAVEFORECAST model = new TBLSEABEACH24HWAVEFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLSEABEACH24HWAVEFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.SB24HWFFIRSTBATHINGWAVEHEIGHT = tbdata[0]; //第一海水浴场浪高
            model.SB24HWFFIRSTBATHINGWATERTEMP = tbdata[1]; //第一海水浴场水温
            model.SB24HWFFIRSTBATHINGSWIMWARN = tbdata[2]; //第一海水浴场游泳预警
            model.SB24HWFSIXTHBATHINGWAVEHEIGHT = tbdata[3]; //第六海水浴场浪高
            model.SB24HWFSIXTHBATHINGWATERTEMP = tbdata[4]; //第六海水浴场水温
            model.SB24HWFSIXTHBATHINGSWIMWARN = tbdata[5];//第六海水浴场游泳预警
            model.SB24HWFSLRBATHINGWAVEHEIGHT = tbdata[6]; //石老人海水浴场浪高
            model.SB24HWFSLRBATHINGWATERTEMP = tbdata[7]; //石老人海水浴场水温
            model.SB24HWFSLRBATHINGSWIMWARN = tbdata[8]; //石老人海水浴场游泳预警
            model.SB24HWFGOLDBEACHWAVEHEIGHT = tbdata[9]; //金沙滩海水浴场浪高
            model.SB24HWFGOLDBEACHWATERTEMP = tbdata[10]; //金沙滩海水浴场水温
            model.SB24HWFGOLDBEACHSWIMWAIN = tbdata[11]; //金沙滩海水浴场游泳预警
            if (quanxian == "fl" || quanxian == "sw")
            {
                if (type == "add")
                {
                    addnum += sql.Add_TBLSEABEACH24HWAVEFORECAST(model, quanxian);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLSEABEACH24HWAVEFORECAST(model, quanxian);
                }
            }
            else if (quanxian == "cx")
            {

                return "addsuccess";
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
        /// 表单18数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe18(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLGOLDBEACH24HTIDALFORECAST sql = new sql_TBLGOLDBEACH24HTIDALFORECAST();
            TBLGOLDBEACH72HTIDALFORECAST model = new TBLGOLDBEACH72HTIDALFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLGOLDBEACH24HTIDALFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 3; i++)
            {
                switch (i) {
                    case 0:
                        model.SEABEACH = "青岛市区";
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 1:
                        model.SEABEACH = "青岛市区";
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 2:
                        model.SEABEACH = "青岛市区";
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    //case 3:
                    //    model.SEABEACH = "金沙滩";
                    //    model.FORECASTDATE = date.AddDays(1);
                    //    break;
                    //case 4:
                    //    model.SEABEACH = "金沙滩";
                    //    model.FORECASTDATE = date.AddDays(2);
                    //    break;
                    //case 5:
                    //    model.SEABEACH = "金沙滩";
                    //    model.FORECASTDATE = date.AddDays(3);
                    //    break;
                }
                model.FIRSTHIGHTIME = tbdata[8 * i]; //第一次高潮时
                model.FIRSTHIGHLEVEL = tbdata[8 * i + 1]; //第一次高潮分
                model.SECONDHIGHTIME = tbdata[8 * i + 2]; //第二次高潮时
                model.SECONDHEIGHTLEVEL = tbdata[8 * i + 3]; //第二次高潮分
                model.FIRSTLOWTIME = tbdata[8 * i + 4];// 第一次低潮时
                model.FIRSTLOWLEVEL = tbdata[8 * i + 5]; //第一次低潮分
               
                model.SECONDLOWTIME = tbdata[8 * i + 6]; //第二次低潮时
                model.SECONDLOWLEVEL = tbdata[8 * i + 7]; //第二次低潮分
                if (type == "add")
                {
                    addnum += sql.Add_TBLGOLDBEACH24HTIDALFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLGOLDBEACH24HTIDALFORECAST(model);
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
            else
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
        }

        /// <summary>
        /// 表单19数据
        /// 1127  贾 ------ 添加quanxian参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe19(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLQDCIRCUM24HWATERFORECAST sql = new sql_TBLQDCIRCUM24HWATERFORECAST();
            TBLQDCIRCUM24HWATERFORECAST model = new TBLQDCIRCUM24HWATERFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLQDCIRCUM24HWATERFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.QDC24HWFQDOFFSHOREWAVEHEIGHT = tbdata[0]; //青岛近海浪高
            model.QDC24HWFQDOFFSHOREWATERTEMP = tbdata[1]; //青岛近海水温
            model.QDC24HWFJMOFFSHOREWAVEHEIGHT = tbdata[2]; // 即墨近海浪高
            model.QDC24HWFJMOFFSHOREWATERTEMP = tbdata[3]; // 即墨近海水温
            model.QDC24HWFJZWOFFSHOREWAVEHEIGHT = tbdata[4]; // 胶州湾浪高
            model.QDC24HWFJZWOFFSHOREWATERTEMP = tbdata[5]; // 胶州湾水温
            model.QDC24HWFJNOFFSHOREWAVEHEIGHT = tbdata[6]; // 胶南近海浪高
            model.QDC24HWFJNOFFSHOREWATERTEMP = tbdata[7]; // 胶南近海水温
            if (quanxian == "fl" || quanxian == "sw")
            {
                if (type == "add")
                {
                    addnum += sql.Add_TBLQDCIRCUM24HWATERFORECAST(model, quanxian);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLQDCIRCUM24HWATERFORECAST(model, quanxian);
                }

            }
            else if (quanxian == "cx")
            {

                return "addsuccess";
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
        /// 表单20数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe20(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLQDCOAST48HTIDALFORECAST sql = new sql_TBLQDCOAST48HTIDALFORECAST();
            TBLQDCOAST48HTIDALFORECAST model = new TBLQDCOAST48HTIDALFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLQDCOAST48HTIDALFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.QDC48HTFFIRSTHIGHWAVEHOUR = tbdata[0]; //第一次高潮时
            model.QDC48HTFFIRSTHGHWAVEMINUTE = tbdata[1]; // 第一次高潮分
            model.QDC48HTFFIRSTHIGHWAVEHEIGHT = tbdata[2]; //第一次高潮潮高
            model.QDC48HTFSECONDHIGHWAVEHOUR = tbdata[3]; // 第二次高潮时
            model.QDC48HTFSECONDHIGHWAVEMINUTE = tbdata[4]; // 第二次高潮分
            model.QDC48HTFSECONDHIGHWAVEHEIGHT = tbdata[5]; // 第二次高潮潮高
            model.QDC48HTFFIRSTLOWWAVEHOUR = tbdata[6]; // 第一次低潮时
            model.QDC48HTFFIRSTLOWWAVEMINUTE = tbdata[7]; //第一次低潮分
            model.QDC48HTFFIRSTLOWWAVEHEIGHT = tbdata[8]; // 第一次低潮潮高
            model.QDC48HTFSECONDLOWWAVEHOUR = tbdata[9]; // 第二次低潮时
            model.QDC48HTFSECONDLOWWAVEMINUTE = tbdata[10]; // 第二次低潮分
            model.QDC48HTFSECONDLOWWAVEHEIGHT = tbdata[11]; // 第二次低潮潮高
            if (type == "add")
            {
                addnum += sql.Add_TBLQDCOAST48HTIDALFORECAST(model);
            }
            else if (type == "edit")
            {
                editnum += sql.Edit_TBLQDCOAST48HTIDALFORECAST(model);
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
        /// 表单21数据
        /// 1127  贾 ------ 添加quanxian参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe21(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLWEIHAITV24HFORECAST sql = new sql_TBLWEIHAITV24HFORECAST();
            TBLWEIHAITV24HFORECAST model = new TBLWEIHAITV24HFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLWEIHAITV24HFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.WTV24HSD24HFWAVEHEIGHT = tbdata[0]; // 石岛近海24h浪高
            model.WTV24HSD24HFWATERTEMP = tbdata[1]; // 石岛近海24h水温
            model.WTV24HWH48HFWAVEHEIGHT = tbdata[2]; //威海近海48h浪高
            model.WTV24HWH48HFWATERTEMP = tbdata[3]; // 威海近海48h水温
            model.WTV24HSD48HFWAVEHEIGHT = tbdata[4]; //石岛近海48h浪高
            model.WTV24HSD48HFWATERTEMP = tbdata[5]; // 石岛近海48水温
            model.WTV24HWD24HFWAVEHEIGHT = tbdata[6]; // 文登区24h浪高
            model.WTV24HWD24HFWATERTEMP = tbdata[7]; // 文登区24h水温
            model.WTV24HCST24HFWAVEHEIGHT = tbdata[8]; // 成山头24h浪高
            model.WTV24HCST24HFWATERTEMP = tbdata[9]; // 成山头24h水温
            model.WTV24HRS24HFWAVEHEIGHT = tbdata[10]; // 乳山市24h浪高
            model.WTV24HRS24HFWATERTEMP = tbdata[11]; // 乳山市24h水温
            model.WTV24HWD48HFWAVEHEIGHT = tbdata[12]; // 文登区48h浪高
            model.WTV24HWD48HFWATERTEMP = tbdata[13]; // 文登区48h水温
            model.WTV24HCST48HFWAVEHEIGHT = tbdata[14]; // 成山头48h浪高
            model.WTV24HCST48HFWATERTEMP = tbdata[15]; // 成山头48h水温
            model.WTV24HRS48HFWAVEHEIGHT = tbdata[16]; // 乳山市48h浪高
            model.WTV24HRS48HFWATERTEMP = tbdata[17]; // 乳山市48h水温
            if (quanxian == "fl" || quanxian == "sw")
            {
                if (type == "add")
                {
                    addnum += sql.Add_TBLWEIHAITV24HFORECAST(model, quanxian);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLWEIHAITV24HFORECAST(model, quanxian);
                }
            }
            else if (quanxian == "cx")
            {

                return "addsuccess";
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
        /// 表单22数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe22(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            int i1 = 0;
            string type = "edit";
            sql_TBLWEIHAISHIDAOTIDALFORECAST sql = new sql_TBLWEIHAISHIDAOTIDALFORECAST();
            TBLWEIHAISHIDAOTIDALFORECAST model = new TBLWEIHAISHIDAOTIDALFORECAST();
            model.PUBLISHDATE = date; 
            DataTable dt = (DataTable)sql.get_TBLWEIHAISHIDAOTIDALFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }

            for (int i = 0; i < 4; i++)
            {

                switch (i)
                {
                    case 0: model.REPORTAREA = "乳山"; break;
                    case 1: model.REPORTAREA = "文登"; break;
                    case 2: model.REPORTAREA = "石岛"; break;
                    case 3: model.REPORTAREA = "成山头"; break;
                    //case 4: model.REPORTAREA = "威海"; break;
                    default:
                        break;
                }
                i1 = (i == 0) ? 0 : (i * 2);
                model.FORECASTDATE = date.AddDays(1);// 预报日期
                model.FIRSTHIGHWAVETIME = tbdata[(i1 * 8)];//第一次高潮潮时
                model.FIRSTHIGHWAVEHEIGHT = tbdata[(i1 * 8) + 1];//第一次高潮潮高
                model.FIRSTLOWWAVETIME = tbdata[(i1 * 8) + 2];//第一次低潮潮时
                model.FIRSTLOWWAVEHEIGHT = tbdata[(i1 * 8) + 3];// 第一次低潮潮高
                model.SECONDHIGHWAVETIME = tbdata[(i1 * 8) + 4];// 第二次高潮潮时
                model.SECONDHIGHWAVEHEIGHT = tbdata[(i1 * 8) + 5];// 第二次高潮潮高
                model.SECONDLOWWAVETIME = tbdata[(i1 * 8) + 6];// 第二次低潮潮时
                model.SECONDLOWWAVEHEIGHT = tbdata[(i1 * 8) + 7];//第二次低潮潮高

                if (type == "add")
                {
                    addnum += sql.Add_TBLWEIHAISHIDAOTIDALFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLWEIHAISHIDAOTIDALFORECAST(model);
                }

                model.FORECASTDATE = date.AddDays(2);// 预报日期
                model.FIRSTHIGHWAVETIME = tbdata[(i1 * 8) + 8];//第一次高潮潮时
                model.FIRSTHIGHWAVEHEIGHT = tbdata[(i1 * 8) + 9];//第一次高潮潮高
                model.FIRSTLOWWAVETIME = tbdata[(i1 * 8) + 10];//第一次低潮潮时
                model.FIRSTLOWWAVEHEIGHT = tbdata[(i1 * 8) + 11];// 第一次低潮潮高
                model.SECONDHIGHWAVETIME = tbdata[(i1 * 8) + 12];// 第二次高潮潮时
                model.SECONDHIGHWAVEHEIGHT = tbdata[(i1 * 8) + 13];// 第二次高潮潮高
                model.SECONDLOWWAVETIME = tbdata[(i1 * 8) + 14];// 第二次低潮潮时
                model.SECONDLOWWAVEHEIGHT = tbdata[(i1 * 8) + 15];//第二次低潮潮高

                if (type == "add")
                {
                    addnum += sql.Add_TBLWEIHAISHIDAOTIDALFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLWEIHAISHIDAOTIDALFORECAST(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 8)
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
                if (editnum == 8)
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
        /// 表单23数据
        /// 底部数据的提交，新增对应的电话
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe23(DateTime date, string data,string quanxian)
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

            model.FWAVEFORECASTERTEL = tbdata[7]; //海浪预报员电话
            model.FTIDALFORECASTERTEL = tbdata[8]; // 潮汐预报员电话
            model.FWATERTEMPERATUREFORECASTERTEL = tbdata[9]; // 水温预报员电话
            model.ZHIBANTEL = tbdata[10]; // 预报值班
            model.SENDTEL = tbdata[11]; // 预报发送



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
        /// 表单24数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe24(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSEAAREASEAICEFORECAST sql = new sql_TBLSEAAREASEAICEFORECAST();
            TBLSEAAREASEAICEFORECAST model = new TBLSEAAREASEAICEFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLSEAAREASEAICEFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0: model.SEAAREA = "辽东湾"; break;
                    case 1: model.SEAAREA = "渤海湾"; break;
                    case 2: model.SEAAREA = "莱州湾"; break;
                    case 3: model.SEAAREA = "黄海北部"; break;
                    default:
                        break;
                }
                model.MAXICEAREA = tbdata[i * 3]; //最大结冰范围
                model.COMMONTHICKNESS = tbdata[i * 3 + 1]; //一般冰厚
                model.MAXTHICKNESS = tbdata[i * 3 + 2]; //最大冰厚

                if (type == "add")
                {
                    addnum += sql.Add_TBLSEAAREASEAICEFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLSEAAREASEAICEFORECAST(model);
                }
            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 表单25数据
        /// 1127  贾 ------ 添加quanxian参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe25(DateTime date, string data, string quanxian)
        {
            if (quanxian.ToLower() != "fl")
                return "editsuccess";
            DateTime datep = date.AddHours(7);
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLZHCFORECAST sql = new sql_TBLZHCFORECAST();
            TBLZHCFORECAST model = new TBLZHCFORECAST();
            model.PUBLISHDATE = datep;
            DateTime dateTime = datep;
            DataTable dt = (DataTable)sql.GETTBLZHCFORECASTBYFORCASEDATEBYPUBLISH(model);
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 15; i++)
            {
                var seaarea = "";
                switch (i)
                {
                    case 0: seaarea = "青岛市"; break;
                    case 1: seaarea = "青岛近海"; break;
                    case 3: seaarea = "渤海海峡"; break;
                    case 2:
                    case 7:
                    case 11:
                        seaarea = "渤海"; break;
                    case 4:
                    case 8:
                    case 12:
                        seaarea = "黄海北部"; break;
                    case 5:
                    case 9:
                    case 13:
                        seaarea = "黄海中部"; break;
                    case 6:
                    case 10:
                    case 14:
                        seaarea = "黄海南部"; break;
                }
                TBLZHCFORECAST modeli = new TBLZHCFORECAST();
                modeli.PUBLISHDATE = datep;
                if (i < 7)
                    modeli.FORECASTDATE = date.AddDays(1).AddHours(7);
                else if (i < 11)
                    modeli.FORECASTDATE = date.AddDays(2).AddHours(7);
                else if (i < 15)
                    modeli.FORECASTDATE = date.AddDays(3).AddHours(7);
                modeli.SEAAREA = seaarea;
                modeli.WEATHERAPPEARANCE = tbdata[5 * i];
                modeli.WINDDIRECTION = tbdata[5 * i + 1];
                modeli.WINDFORCE = tbdata[5 * i + 2];
                modeli.WAVEHEIGHT = tbdata[5 * i + 3];
                modeli.WAVEDIRECTION = tbdata[5 * i + 4];


                if (type == "add")
                {
                    addnum += sql.Add_TBLZHCFORECAST(modeli);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTBLZHCFORECAST(modeli, quanxian);
                }
            }
            if (type == "add")
            {
                if (addnum == 15)
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
                if (editnum == 15)
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
        /// 表单26数据
        /// 1127  贾 ------ 添加quanxian参数
        /// 青岛近海的数据由原来的1d改为3d Durriya,现在是17条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe26(DateTime date, string data, string quanxian)
        {
            if (quanxian.ToLower() != "fl")
                return "editsuccess";
            DateTime datep = date.AddHours(16);
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLZHCFORECAST sql = new sql_TBLZHCFORECAST();
            TBLZHCFORECAST model = new TBLZHCFORECAST();
            model.PUBLISHDATE = datep;
            DataTable dt = (DataTable)sql.get_TBLZHCFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
                sql.del_TBLZHCFORECAST_AllData(model);//先删除再添加
                type = "add";
            }
            else //无数据 新增
            {
                type = "add";
            }
         
            for (int i = 0; i < 17; i++)
            {
                var seaarea = "";
                switch (i)
                {
                    case 0: seaarea = "青岛市"; break;
                    case 3: seaarea = "渤海海峡"; break;
                    case 1:
                    case 7:
                    case 12:
                        seaarea = "青岛近海"; break;
                    case 2:
                    case 8:
                    case 13:
                        seaarea = "渤海"; break;
                    case 4:
                    case 9:
                    case 14:
                        seaarea = "黄海北部"; break;
                    case 5:
                    case 10:
                    case 15:
                        seaarea = "黄海中部"; break;
                    case 6:
                    case 11:
                    case 16:
                        seaarea = "黄海南部"; break;
                }
                TBLZHCFORECAST modeli = new TBLZHCFORECAST();
                modeli.PUBLISHDATE = datep;
                if (i < 7)
                    modeli.FORECASTDATE = date.AddDays(1).AddHours(16);
                else if (i < 12)
                    modeli.FORECASTDATE = date.AddDays(2).AddHours(16);
                else if (i < 17)
                    modeli.FORECASTDATE = date.AddDays(3).AddHours(16);
                modeli.SEAAREA = seaarea;
                modeli.WEATHERAPPEARANCE = tbdata[5 * i];
                modeli.WINDDIRECTION = tbdata[5 * i + 1];
                modeli.WINDFORCE = tbdata[5 * i + 2];
                modeli.WAVEHEIGHT = tbdata[5 * i + 3];
                modeli.WAVEDIRECTION = tbdata[5 * i + 4];

                if (type == "add")
                {
                    addnum += sql.Add_TBLZHCFORECAST(modeli);
                }
                else if (type == "edit")
                {
                    //editnum += sql.Edit_TBLZHCFORECAST(modeli, quanxian);
                  
                    addnum += sql.Add_TBLZHCFORECAST(modeli);
                }
            }
            //以下数据75改为85
            var seaAreasCount = (tbdata.Length - 85) / 5; //不固定预报站点海区的数目
            try
            {
                for (int j = 0; j < seaAreasCount; j++)
                {
                    TBLZHCFORECAST modelj1 = new TBLZHCFORECAST();
                    modelj1.PUBLISHDATE = datep;
                    modelj1.FORECASTDATE = DateTime.Parse(date.Year + "/" + (tbdata[85 + j *5 +1].Replace("月", "/")).Replace("日", "")).AddHours(16);
                    modelj1.SEAAREA = tbdata[85 + j * 5];
                    modelj1.WEATHERAPPEARANCE = "bgdhq";
                    modelj1.WINDDIRECTION = tbdata[85 + j * 5 + 2];
                    modelj1.WINDFORCE = tbdata[85 + j * 5 + 3];
                    modelj1.WAVEHEIGHT = tbdata[85 + j * 5 + 4];
                    modelj1.WAVEDIRECTION = "";



                    if (type == "add")
                    {
                        addnum += sql.Add_TBLZHCFORECAST(modelj1);
                        // addnum += sql.Add_TBLZHCFORECAST(modelj2, quanxian);
                    }
                    else if (type == "edit")
                    {
                       // editnum += sql.Edit_TBLZHCFORECAST(modelj1, quanxian);
                       //editnum += sql.Edit_TBLZHCFORECAST(modelj2, quanxian);
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            string errorsuccess = "";
            if (type == "add")
            {
                if (addnum >= 17)
                {
                    errorsuccess = "addsuccess"; 
                }
                else
                {
                    errorsuccess="adderror";
                }
            }
            else
            {
                if (quanxian == "sw")
                {
                    if (editnum >= 17)
                    {
                        errorsuccess= "editsuccess";
                    }
                    else
                    {
                        errorsuccess = "editerror";
                    }
                }

                else if (quanxian == "fl")
                {
                    {
                        if (editnum >= 17)
                        {
                            errorsuccess = "editsuccess";
                        }
                        else
                        {
                            errorsuccess = "editerror";
                        }
                    }
                }
            }
            return errorsuccess;
        }

        /// <summary>
        /// 上午指挥处
        /// Durriya修改新的模板，改青岛近海1天为3d
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe44(DateTime date, string data, string quanxian)
        {
            if (quanxian.ToLower() != "fl")
                return "editsuccess";
            DateTime datep = date.AddHours(7);
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLZHCFORECAST sql = new sql_TBLZHCFORECAST();
            TBLZHCFORECAST model = new TBLZHCFORECAST();
            model.PUBLISHDATE = datep;
            DataTable dt = (DataTable)sql.get_TBLZHCFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
                sql.del_TBLZHCFORECAST_AllData(model);//先删除再添加
                type = "add";
            }
            else //无数据 新增
            {
                type = "add";
            }

            for (int i = 0; i < 17; i++)//15改成17 Durriya
            {
                var seaarea = "";
                switch (i)
                {
                    case 0: seaarea = "青岛市"; break;
                    case 3: seaarea = "渤海海峡"; break;
                    case 1:
                    case 7:
                    case 12:
                        seaarea = "青岛近海"; break;
                    case 2:
                    case 8:
                    case 13:
                        seaarea = "渤海"; break;
                    case 4:
                    case 9:
                    case 14:
                        seaarea = "黄海北部"; break;
                    case 5:
                    case 10:
                    case 15:
                        seaarea = "黄海中部"; break;
                    case 6:
                    case 11:
                    case 16:
                        seaarea = "黄海南部"; break;


                        //case 0: seaarea = "青岛市"; break;
                        //case 1: seaarea = "青岛近海"; break;
                        //case 3: seaarea = "渤海海峡"; break;
                        //case 2:
                        //case 7:
                        //case 11:
                        //    seaarea = "渤海"; break;
                        //case 4:
                        //case 8:
                        //case 12:
                        //    seaarea = "黄海北部"; break;
                        //case 5:
                        //case 9:
                        //case 13:
                        //    seaarea = "黄海中部"; break;
                        //case 6:
                        //case 10:
                        //case 14:
                        //    seaarea = "黄海南部"; break;
                }
                TBLZHCFORECAST modeli = new TBLZHCFORECAST();
                modeli.PUBLISHDATE = datep;
                if (i < 7)
                    modeli.FORECASTDATE = date.AddDays(1).AddHours(7);
                else if (i < 12)
                    modeli.FORECASTDATE = date.AddDays(2).AddHours(7);
                else if (i < 17)
                    modeli.FORECASTDATE = date.AddDays(3).AddHours(7);
                modeli.SEAAREA = seaarea;
                modeli.WEATHERAPPEARANCE = tbdata[5 * i];
                modeli.WINDDIRECTION = tbdata[5 * i + 1];
                modeli.WINDFORCE = tbdata[5 * i + 2];
                modeli.WAVEHEIGHT = tbdata[5 * i + 3];
                modeli.WAVEDIRECTION = tbdata[5 * i + 4];

                if (type == "add")
                {
                    addnum += sql.Add_TBLZHCFORECAST(modeli);
                }
                else if (type == "edit")
                {
                    //editnum += sql.Edit_TBLZHCFORECAST(modeli, quanxian);

                    addnum += sql.Add_TBLZHCFORECAST(modeli);
                }
            }
           
            string errorsuccess = "";
            if (type == "add")
            {
                if (addnum == 17)
                {
                    errorsuccess = "addsuccess";
                }
                else
                {
                    errorsuccess = "adderror";
                }
            }
            
            return errorsuccess;
        }

        /// <summary>
        /// 上午渔政局
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <param name="quanxian"></param>
        /// <returns></returns>
        public string settabe45(DateTime date, string data, string quanxian)
        {
            if (quanxian.ToLower() != "fl")
                return "editerror";

            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            int dayCount = 13;
            sql_TBLYZJFORECAST sql = new sql_TBLYZJFORECAST();
            TBLYZJFORECAST model = new TBLYZJFORECAST();
            //DateTime weekPublishTime = GetMondayDate(date);
            //model.PUBLISHDATE = date;0
            model.PUBLISHDATE = date.AddHours(7);
            DataTable dt = (DataTable)sql.get_TBLYZJFORECAST(model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sql.delete_TBLYZJFORECAST(model);
                type = "add";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.PUBLISHDATE = date.AddHours(7);
                model.SEAAREA = tbdata[i * 4].ToString();
                if (i % 2 == 0 && model.SEAAREA != "青岛")
                {
                    model.FORECASTDATE = date.AddDays(1).AddHours(7);

                }

                else
                {
                    model.FORECASTDATE = date.AddDays(2).AddHours(7);
                }

                model.WINDDIRECTION = tbdata[i * 4 + 1].ToString();
                model.WINDFORCE = tbdata[i * 4 + 2].ToString(); 
                model.WAVEHEIGHT = tbdata[i * 4 + 3].ToString();

                if (type == "add")
                {
                    addnum += sql.Add_TBLYZJFORECAST(model, quanxian);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLYZJFORECAST(model, quanxian);
                }

            }
            var seaAreasCount = (tbdata.Length - 52) / 4; //不固定预报站点海区的数目
            try
            {
                for (int j = 0; j < seaAreasCount; j++)
                {
                    TBLYZJFORECAST modelj1 = new TBLYZJFORECAST();
                    modelj1.PUBLISHDATE = date.AddHours(7);
                    modelj1.FORECASTDATE = DateTime.Parse(date.Year + "/" + (tbdata[52 + j * 5 + 1].Replace("月", "/")).Replace("日", "")).AddHours(7);
                    modelj1.SEAAREA = tbdata[52 + j * 5];
                    modelj1.WINDDIRECTION = tbdata[52 + j * 5 + 2];
                    modelj1.WINDFORCE = tbdata[52 + j * 5 + 3];
                    modelj1.WAVEHEIGHT = tbdata[52 + j * 5 + 4];



                    if (type == "add")
                    {
                        addnum += sql.Add_TBLYZJFORECAST(modelj1, quanxian);
                        // addnum += sql.Add_TBLZHCFORECAST(modelj2, quanxian);
                    }
                    else if (type == "edit")
                    {
                        // editnum += sql.Edit_TBLZHCFORECAST(modelj1, quanxian);
                        //editnum += sql.Edit_TBLZHCFORECAST(modelj2, quanxian);
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            if (type == "add")
            {
                if (addnum >= dayCount)
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
                if (editnum == dayCount)
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
        /// 表单3天海洋水文气象预报棕述
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe27(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            string METEOROLOGICALREVIEW = tbdata[0];
            string METEOROLOGICALREVIEWCX = tbdata[1];
            sql_TBLZS sql = new sql_TBLZS();
            DataTable dt = (DataTable)sql.get_TBLSWQX_ZS_3DayS_OR_24HourS(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (quanxian == "fl")
                {

                    int rultEdit = sql.Edit_TBLSWQX_ZS_3DayS(date, METEOROLOGICALREVIEW);
                    if (rultEdit == 1)
                    {
                        return "editsuccess";
                    }
                }
                else if (quanxian == "cx")
                {
                    int rultEdit = sql.Edit_TBLSWQX_ZS_3DaySCX(date, METEOROLOGICALREVIEWCX);
                    if (rultEdit == 1)
                    {
                        return "editsuccess";
                    }
                }
                else if (quanxian == "sw")
                {
                    return "addsuccess";
                }
                return "editerror";
                
            }
            int rultAdd = sql.Add_TBLSWQX_ZS_3Days(date, METEOROLOGICALREVIEW, METEOROLOGICALREVIEWCX);
            if (rultAdd == 1)
            {
                return "addsuccess";
            }
            return "adderror";
        }

        /// <summary>
        /// 表单24小时水文气象预报综述
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe28(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            string METEOROLOGICALREVIEW24HOUR = tbdata[0];
            string METEOROLOGICALREVIEW24HOURCX = tbdata[1];
            sql_TBLZS sql = new sql_TBLZS();
            DataTable dt = (DataTable)sql.get_TBLSWQX_ZS_3DayS_OR_24HourS(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (quanxian == "fl")
                {

                    int rultEdit = sql.Edit_TBLSWQX_ZS_24HOURS(date, METEOROLOGICALREVIEW24HOUR);
                    if (rultEdit == 1)
                    {
                        return "editsuccess";
                    }
                }
              else  if (quanxian == "cx")
                {

                    int rultEdit = sql.Edit_TBLSWQX_ZS_24HOURSCX(date, METEOROLOGICALREVIEW24HOURCX);
                    if (rultEdit == 1)
                    {
                        return "editsuccess";
                    }
                }
                else if (quanxian == "sw")
                {
                    return "addsuccess";
                }
                return "editerror";
            }
            int rultAdd = sql.Add_TBLSWQX_ZS_24HOURS(date, METEOROLOGICALREVIEW24HOUR,METEOROLOGICALREVIEW24HOURCX);
            if (rultAdd == 1)
            {
                return "addsuccess";
            }
            return "adderror";
        }

        /// <summary>
        /// 7天渤海海区及黄河海港风、浪预报
        /// weekPublishTime为周一日期
        /// 1127  贾 ------ 添加quanxian参数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe29(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            int dayCount = 7;
            sql_TBLYRBHWINDWAVE7DAYFORECASTTWO sql = new sql_TBLYRBHWINDWAVE7DAYFORECASTTWO();
            TBLYRBHWINDWAVE72HFORECASTTWO model = new TBLYRBHWINDWAVE72HFORECASTTWO();
            //判断当前时间所在周的周一日期即为周报发布时间
            //int t = 1 - Convert.ToInt32(date.DayOfWeek.ToString("d"));
            //DateTime weekPublishTime = new DateTime();
            //if (t == 1)
            //{
            //    weekPublishTime = date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")) - 7);
            //}
            //else
            //{
            //    weekPublishTime = date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")));
            //}
            DateTime weekPublishTime = GetMondayDate(date);
            model.PUBLISHDATE = weekPublishTime;
            DataTable dt = (DataTable)sql.GETTBLYRBHWINDWAVE7DAYFORECASTTWO(model);
            if (dt.Rows.Count > 0)//有数据 修改
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.REPORTAREA = "渤海";                           //区域
                model.FORECASTDATE = weekPublishTime.AddDays((i + 1));          //预报日期
                model.YRBHWWFWAVEHEIGHT = tbdata[(i * 5)];           //波高
                model.YRBHWWFWAVEDIR = tbdata[(i * 5) + 1];          //波向
                model.YRBHWWFFLOWDIR = tbdata[(i * 5) + 2];          //风向
                model.YRBHWWFFLOWLEVEL = tbdata[(i * 5) + 3];        //风力
                model.YRBHWWFWATERTEMPERATURE = tbdata[(i * 5) + 4]; //水温
                if (quanxian == "fl" || quanxian == "sw")
                {

                    if (type == "add")
                    {
                        addnum += sql.AddTBLYRBHWINDWAVE7DAYFORECASTTWO(model, quanxian);
                    }
                    else if (type == "edit")
                    {
                        editnum += sql.UPDATETBLYRBHWINDWAVE7DAYFORECASTTWO(model, quanxian);
                    }
                }
                else if (quanxian == "cx" || quanxian == "hb")
                {
                    return "addsuccess";
                }
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.REPORTAREA = "黄河海港";                        //区域
                model.FORECASTDATE = weekPublishTime.AddDays((i + 1));           //预报日期
                model.YRBHWWFWAVEHEIGHT = tbdata[(i * 5) + 5 * dayCount];       //波高
                model.YRBHWWFWAVEDIR = tbdata[(i * 5) + 5 * dayCount + 1];          //波向
                model.YRBHWWFFLOWDIR = tbdata[(i * 5) + 5 * dayCount + 2];          //风向
                model.YRBHWWFFLOWLEVEL = tbdata[(i * 5) + 5 * dayCount + 3];        //风力
                model.YRBHWWFWATERTEMPERATURE = tbdata[(i * 5) + 5 * dayCount + 4]; //水温
                if (quanxian == "fl" || quanxian == "sw")
                {
                    if (type == "add")
                    {
                        addnum += sql.AddTBLYRBHWINDWAVE7DAYFORECASTTWO(model, quanxian);
                    }
                    else if (type == "edit")
                    {
                        editnum += sql.UPDATETBLYRBHWINDWAVE7DAYFORECASTTWO(model, quanxian);
                    }
                }
                else if (quanxian == "cx" || quanxian == "hb")
                {
                    return "addsuccess";
                }
            }
            if (type == "add")
            {
                if (addnum == 2 * dayCount)
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
                if (editnum == 2 * dayCount)
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
        /// 表单7天海洋水文气象预报综述
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe30(DateTime date, string data, string quanxian)
        {
            var tbdata = data.Split(',');
            string METEOROLOGICALREVIEW7DAYS = tbdata[0];
            string METEOROLOGICALREVIEW7DAYSCX = tbdata[1];
            sql_TBLZS sql = new sql_TBLZS();
            DataTable dt = (DataTable)sql.get_TBLSWQX_ZS_3DayS_OR_24HourS(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (quanxian == "fl")
                {
                    int rultEdit = sql.Edit_TBLSWQX_ZS_7Days(date, METEOROLOGICALREVIEW7DAYS);
                    if (rultEdit == 1)
                    {
                        return "editsuccess";
                    }
                }
                else if (quanxian == "cx")
                {

                    int rultEdit = sql.Edit_TBLSWQX_ZS_7DaysCX(date, METEOROLOGICALREVIEW7DAYSCX);
                    if (rultEdit == 1)
                    {
                        return "editsuccess";
                    }
                }
                else if (quanxian == "sw")
                {
                    return "addsuccess";
                }
                return "editerror";
            }
            int rultAdd = sql.Add_TBLSWQX_ZS_7Days(date, METEOROLOGICALREVIEW7DAYS, METEOROLOGICALREVIEW7DAYSCX);
            if (rultAdd == 1)
            {
                return "addsuccess";
            }
            return "adderror";
        }
        /// <summary>
        /// 7天港口潮位预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe31(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            int dayCount = 7;
            sql_TBLHARBOURTIDELEVEL7DAY sql = new sql_TBLHARBOURTIDELEVEL7DAY();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            DateTime weekPublishTime = GetMondayDate(date);
            //model.PUBLISHDATE = date;
            model.PUBLISHDATE = weekPublishTime;
            DataTable dt = (DataTable)sql.GETTBLHARBOURTIDELEVEL7DAY(model);
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < dayCount; i++)
            {
                // model.PUBLISHDATE //填报日期
                model.HTLHARBOUR = "龙口港"; //港口
                model.FORECASTDATE = weekPublishTime.AddDays((i + 1));   //预报日期
                model.HTLFIRSTWAVEOFTIME = tbdata[(i * 8)];  //第一次高潮时间
                model.HTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 1];  //第一次高潮潮位
                model.HTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 2]; //第一次低潮时间
                model.HTLLOWTIDELEVELFORTHEFIRSTTIME = tbdata[(i * 8) + 3]; //第一次低潮潮位
                model.HTLSECONDWAVEOFTIME = tbdata[(i * 8) + 4]; //第二次高潮时间
                model.HTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 5]; //第二次高潮潮位
                model.HTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 6]; //第二次低潮时间
                model.HTLLOWTIDELEVELFORTHESECONDTIM = tbdata[(i * 8) + 7]; //第二次低潮潮位


                if (type == "add")
                {
                    addnum += sql.AddTBLHARBOURTIDELEVEL7DAY(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.UPDATETBLHARBOURTIDELEVEL7DAY(model);
                }
            }

            for (int i = 0; i < dayCount; i++)
            {
                model.HTLHARBOUR = "黄河海港"; //港口
                model.FORECASTDATE = weekPublishTime.AddDays((i + 1));   //预报日期
                model.HTLFIRSTWAVEOFTIME = tbdata[(i * 8) + 8 * dayCount];  //第一次高潮时间
                model.HTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 8 * dayCount + 1];  //第一次高潮潮位
                model.HTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 8 * dayCount + 2]; //第一次低潮时间
                model.HTLLOWTIDELEVELFORTHEFIRSTTIME = tbdata[(i * 8) + 8 * dayCount + 3]; //第一次低潮潮位
                model.HTLSECONDWAVEOFTIME = tbdata[(i * 8) + 8 * dayCount + 4]; //第二次高潮时间
                model.HTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 8 * dayCount + 5]; //第二次高潮潮位
                model.HTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 8 * dayCount + 6]; //第二次低潮时间
                model.HTLLOWTIDELEVELFORTHESECONDTIM = tbdata[(i * 8) + 8 * dayCount + 7]; //第二次低潮潮位
                if (type == "add")
                {
                    addnum += sql.AddTBLHARBOURTIDELEVEL7DAY(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.UPDATETBLHARBOURTIDELEVEL7DAY(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 2 * dayCount)
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
                if (editnum == 2 * dayCount)
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
        /// 东营胜利油田专项海温周报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe32(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSLYTWEEKFORECAST sql = new sql_TBLSLYTWEEKFORECAST();
            TBLSDOFFSHORESEVENCITY24HWAVE model = new TBLSDOFFSHORESEVENCITY24HWAVE();
            //model.PUBLISHDATE = date;
            DateTime weekPublishTime = GetMondayDate(date);
            model.PUBLISHDATE = weekPublishTime;
            DataTable dt = (DataTable)sql.GETTBLSLYTWEEKFORECAST(model);
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 2; i < 7; i++)
            {
                switch (i)
                {
                    case 2: model.SDOSCWAREA = "威海近海"; break;
                    case 3: model.SDOSCWAREA = "烟台近海"; break;
                    case 4: model.SDOSCWAREA = "潍坊近海"; break;
                    case 5: model.SDOSCWAREA = "东营近海"; break;
                    case 6: model.SDOSCWAREA = "滨州近海"; break;
                    default:
                        break;
                }
                model.SDOSCWLOWESTWAVEHEIGHT = ""; //最低浪高
                model.SDOSCWHIGHTESTWAVEHEIGHT = ""; //最高浪高 //前台没有
                model.SDOSCWSURFACETEMPERATURE = tbdata[(i * 2) + 1]; //表层水温

                if (type == "add")
                {
                    addnum += sql.AddTBLSLYTWEEKFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTBLSLYTWEEKFORECAST(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 5)
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
                if (editnum == 5)
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
        /// 十三、黄河海港附近海域风、浪预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe33(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLHH3DAYSFORECAST sql = new sql_TBLHH3DAYSFORECAST();
            TBLYRSOUTHSEAWALL24WINDWAVE model = new TBLYRSOUTHSEAWALL24WINDWAVE();
            model.PUBLISHDATE = date;
            model.FORECASTDATE = date;
            DataTable dt = (DataTable)sql.GETTBLHH3DAYSFORECAST(model, "f");
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 3; i++)
            {
                model.FORECASTDATE = date.AddDays((i + 1)); //预报日期
                model.YRSSWWWAVEHEIGHT = tbdata[(i * 4)]; //波高
                model.YRSSWWWAVEDIRECTION = tbdata[(i * 4) + 1]; //波向
                model.YRSSWWWINDDIRECTION = tbdata[(i * 4) + 2];  //风向
                model.YRSSWWWINDFORCE = tbdata[(i * 4) + 3];  //风力
                if (type == "add")
                {
                    addnum += sql.AddTBLHH3DAYSFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTBLHH3DAYSFORECAST(model);
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
            else
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

        }
        /// <summary>
        /// 十三、72小时东营神仙沟挡潮闸专项预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe34(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSXGTIDELEVEL sql = new sql_TBLSXGTIDELEVEL();
            TBLMZZTIDELEVEL model = new TBLMZZTIDELEVEL();
            model.PUBLISHDATE = date;
            model.FORECASTDATE = date;
            DataTable dt = (DataTable)sql.GETTBLSXGTIDELEVEL(model, "f");
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 3; i++)
            {
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.MZZTLFIRSTWAVEOFTIME = tbdata[(i * 8)]; //第一次高潮时间
                model.MZZTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 1]; //第一次高潮潮位
                model.MZZTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 2];//第一次低潮时间
                model.MZZTLLOWTIDELEVELFORTHEFIRSTTI = tbdata[(i * 8) + 3]; //第一次低潮潮位
                model.MZZTLSECONDWAVEOFTIME = tbdata[(i * 8) + 4]; //第二次高潮时间
                model.MZZTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 5]; //第二次高潮潮位
                model.MZZTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 6]; //第二次低潮时间
                model.MZZTLLOWTIDELEVELFORTHESECONDT = tbdata[(i * 8) + 7]; //第二次低潮潮位

                if (type == "add")
                {
                    addnum += sql.AddTBLSXGTIDELEVEL(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTBLSXGTIDELEVEL(model);
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
            else
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
        }

        /// <summary>
        /// 下午渔政局
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <param name="quanxian"></param>
        /// <returns></returns>
        public string settabe35(DateTime date, string data, string quanxian)
        {
            if (quanxian.ToLower() != "fl")
                return "editerror";

            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            int dayCount = 13;
            sql_TBLYZJFORECAST sql = new sql_TBLYZJFORECAST();
            TBLYZJFORECAST model = new TBLYZJFORECAST();
            //DateTime weekPublishTime = GetMondayDate(date);
            //model.PUBLISHDATE = date;
            model.PUBLISHDATE = date.AddHours(16);
            DataTable dt = (DataTable)sql.get_TBLYZJFORECAST(model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sql.delete_TBLYZJFORECAST(model);
                type = "add";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.PUBLISHDATE = date.AddHours(16);
                model.SEAAREA = tbdata[i * 4].ToString();
                if (i % 2 == 0 && model.SEAAREA != "青岛")
                {
                    model.FORECASTDATE = date.AddDays(1);

                }

                else
                {
                    model.FORECASTDATE = date.AddDays(2);
                }

                model.WINDDIRECTION = tbdata[i * 4 + 1].ToString();
                model.WINDFORCE = tbdata[i * 4 + 2].ToString();
                model.WAVEHEIGHT = tbdata[i * 4 + 3].ToString();

                if (type == "add")
                {
                    addnum += sql.Add_TBLYZJFORECAST(model, quanxian);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLYZJFORECAST(model, quanxian);
                }
            }
            var seaAreasCount = (tbdata.Length - 52) / 4; //不固定预报站点海区的数目
            try
            {
                for (int j = 0; j < seaAreasCount; j++)
                {
                    TBLYZJFORECAST modelj1 = new TBLYZJFORECAST();
                    modelj1.PUBLISHDATE = date.AddHours(16);
                    modelj1.FORECASTDATE = DateTime.Parse(date.Year + "/" + (tbdata[52 + j * 5 + 1].Replace("月", "/")).Replace("日", "")).AddHours(16);
                    modelj1.SEAAREA = tbdata[52 + j * 5];
                    modelj1.WINDDIRECTION = tbdata[52 + j * 5 + 2];
                    modelj1.WINDFORCE = tbdata[52 + j * 5 + 3];
                    modelj1.WAVEHEIGHT = tbdata[52 + j * 5 + 4];



                    if (type == "add")
                    {
                        addnum += sql.Add_TBLYZJFORECAST(modelj1, quanxian);
                        // addnum += sql.Add_TBLZHCFORECAST(modelj2, quanxian);
                    }
                    else if (type == "edit")
                    {
                        // editnum += sql.Edit_TBLZHCFORECAST(modelj1, quanxian);
                        //editnum += sql.Edit_TBLZHCFORECAST(modelj2, quanxian);
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            if (type == "add")
            {
                if (addnum >= dayCount)
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
                if (editnum == dayCount)
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
        /// 表单16数据
        /// *180710 修改 删除上午九 潍坊港24小时潮汐预报
        /// * 生成预报单从上午八 潍坊港24小时获取
        ///  edit by yuy
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe36(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLWF24HTIDALFORECASTAM sql = new sql_TBLWF24HTIDALFORECASTAM();
            TBLWF24HTIDALFORECASTAM model = new TBLWF24HTIDALFORECASTAM();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLWF24HTIDALFORECASTAM_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.WF24HTFFIRSTHIGHWAVETIME = tbdata[0];//第一次高潮潮时
            model.WF24HTFFIRSTHIGHWAVEHEIGHT = tbdata[1]; //第一次高潮潮高
            model.WF24HTFSECONDHIGHWAVETIME = tbdata[2]; //第二次高潮潮时
            model.WF24HTFSECONDHIGHWAVEHEIGHT = tbdata[3];//第二次高潮潮高
            model.WF24HTFFIRSTLOWWAVETIME = tbdata[4]; //第一次低潮潮时
            model.WF24HTFFIRSTLOWWAVEHEIGHT = tbdata[5];//第一次低潮潮高
            model.WF24HTFSECONDLOWWAVETIME = tbdata[6];//第二次低潮潮时
            model.WF24HTFSECONDLOWWAVEHEIGHT = tbdata[7]; //第二次低潮潮高

            if (type == "add")
            {
                addnum += sql.Add_TBLWF24HTIDALFORECASTAM(model);
            }
            else if (type == "edit")
            {
                editnum += sql.Edit_TBLWF24HTIDALFORECASTAM(model);
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
        /// 上午七、海上丝绸之路三天海浪、气象预报
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public string settabe39(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            int dayCount = 3;
            sql_SilkWaveAndTide sql = new sql_SilkWaveAndTide();
            TBLYRBHWINDWAVE72HFORECASTTWO model = new TBLYRBHWINDWAVE72HFORECASTTWO();
            model.PUBLISHDATE = date;
            model.FORECASTDATE = date;          //预报日期
            //DataTable dt = (DataTable)sql.get_TBLYRBHWINDWAVE72HFORECASTTWO_AllData(model);
            DataTable dt = (DataTable)sql.GetSilkWave(model);

            if (dt.Rows.Count > 0)//有数据 修改
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.REPORTAREA = "青岛港";                           //区域
                model.FORECASTDATE = date.AddDays((i + 1));          //预报日期
                model.YRBHWWFWAVEHEIGHT = tbdata[(i * 4)];           //波高
                model.YRBHWWFWAVEDIR = tbdata[(i * 4) + 1];          //波向
                model.YRBHWWFFLOWDIR = tbdata[(i * 4) + 2];          //风向
                model.YRBHWWFFLOWLEVEL = tbdata[(i * 4) + 3];        //风力
                //model.YRBHWWFWATERTEMPERATURE = tbdata[(i * 5) + 4]; //水温
                if (type == "add")
                {
                    addnum += sql.AddSilkWave(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditSilkWave(model);
                }
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.REPORTAREA = "潍坊港";                        //区域
                model.FORECASTDATE = date.AddDays((i + 1));           //预报日期
                model.YRBHWWFWAVEHEIGHT = tbdata[(i * 4) + 4 * dayCount];       //波高
                model.YRBHWWFWAVEDIR = tbdata[(i * 4) + 4 * dayCount + 1];          //波向
                model.YRBHWWFFLOWDIR = tbdata[(i * 4) + 4 * dayCount + 2];          //风向
                model.YRBHWWFFLOWLEVEL = tbdata[(i * 4) + 4 * dayCount + 3];        //风力
                //model.YRBHWWFWATERTEMPERATURE = tbdata[(i * 5) + 5 * dayCount + 4]; //水温
                if (type == "add")
                {
                    addnum += sql.AddSilkWave(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditSilkWave(model);
                }
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.REPORTAREA = "营口港";                        //区域
                model.FORECASTDATE = date.AddDays((i + 1));           //预报日期
                model.YRBHWWFWAVEHEIGHT = tbdata[(i * 4) + 8 * dayCount];       //波高
                model.YRBHWWFWAVEDIR = tbdata[(i * 4) + 8 * dayCount + 1];          //波向
                model.YRBHWWFFLOWDIR = tbdata[(i * 4) + 8 * dayCount + 2];          //风向
                model.YRBHWWFFLOWLEVEL = tbdata[(i * 4) + 8 * dayCount + 3];        //风力
                //model.YRBHWWFWATERTEMPERATURE = tbdata[(i * 5) + 5 * dayCount + 4]; //水温
                if (type == "add")
                {
                    addnum += sql.AddSilkWave(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditSilkWave(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 3 * dayCount)
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
                if (editnum == 3 * dayCount)
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
        /// 上午八、海上丝绸之路三天潮汐预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe38(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            int dayCount = 3;
            sql_SilkWaveAndTide sql = new sql_SilkWaveAndTide();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.GetSilkTide(model);
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < dayCount; i++)
            {
                // model.PUBLISHDATE //填报日期
                model.HTLHARBOUR = "青岛港"; //港口
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.HTLFIRSTWAVEOFTIME = tbdata[(i * 8)];  //第一次高潮时间
                model.HTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 1];  //第一次高潮潮位
                model.HTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 2]; //第一次低潮时间
                model.HTLLOWTIDELEVELFORTHEFIRSTTIME = tbdata[(i * 8) + 3]; //第一次低潮潮位
                model.HTLSECONDWAVEOFTIME = tbdata[(i * 8) + 4]; //第二次高潮时间
                model.HTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 5]; //第二次高潮潮位
                model.HTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 6]; //第二次低潮时间
                model.HTLLOWTIDELEVELFORTHESECONDTIM = tbdata[(i * 8) + 7]; //第二次低潮潮位


                if (type == "add")
                {
                    addnum += sql.AddSilkTide(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditSilkTide(model);
                }
            }

            for (int i = 0; i < dayCount; i++)
            {
                model.HTLHARBOUR = "潍坊港"; //港口
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.HTLFIRSTWAVEOFTIME = tbdata[(i * 8) + 8 * dayCount];  //第一次高潮时间
                model.HTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 8 * dayCount + 1];  //第一次高潮潮位
                model.HTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 8 * dayCount + 2]; //第一次低潮时间
                model.HTLLOWTIDELEVELFORTHEFIRSTTIME = tbdata[(i * 8) + 8 * dayCount + 3]; //第一次低潮潮位
                model.HTLSECONDWAVEOFTIME = tbdata[(i * 8) + 8 * dayCount + 4]; //第二次高潮时间
                model.HTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 8 * dayCount + 5]; //第二次高潮潮位
                model.HTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 8 * dayCount + 6]; //第二次低潮时间
                model.HTLLOWTIDELEVELFORTHESECONDTIM = tbdata[(i * 8) + 8 * dayCount + 7]; //第二次低潮潮位
                if (type == "add")
                {
                    addnum += sql.AddSilkTide(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditSilkTide(model);
                }
            }
            for (int i = 0; i < dayCount; i++)
            {
                model.HTLHARBOUR = "营口港"; //港口
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.HTLFIRSTWAVEOFTIME = tbdata[(i * 8) + 16 * dayCount];  //第一次高潮时间
                model.HTLFIRSTWAVETIDELEVEL = tbdata[(i * 8) + 16 * dayCount + 1];  //第一次高潮潮位
                model.HTLFIRSTTIMELOWTIDE = tbdata[(i * 8) + 16 * dayCount + 2]; //第一次低潮时间
                model.HTLLOWTIDELEVELFORTHEFIRSTTIME = tbdata[(i * 8) + 16 * dayCount + 3]; //第一次低潮潮位
                model.HTLSECONDWAVEOFTIME = tbdata[(i * 8) + 16 * dayCount + 4]; //第二次高潮时间
                model.HTLSECONDWAVETIDELEVEL = tbdata[(i * 8) + 16 * dayCount + 5]; //第二次高潮潮位
                model.HTLSECONDTIMELOWTIDE = tbdata[(i * 8) + 16 * dayCount + 6]; //第二次低潮时间
                model.HTLLOWTIDELEVELFORTHESECONDTIM = tbdata[(i * 8) + 16 * dayCount + 7]; //第二次低潮潮位
                if (type == "add")
                {
                    addnum += sql.AddSilkTide(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditSilkTide(model);
                }
            }
            if (type == "add")
            {
                if (addnum == 3 * dayCount)
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
                if (editnum == 3 * dayCount)
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
        /// 表单37数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe37(DateTime date, string data ,string quanxian)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            Sql_HT_TBLWF24HWAVEFORECAST sql = new Sql_HT_TBLWF24HWAVEFORECAST();
            HT_TBLWF24HWAVEFORECAST model = new HT_TBLWF24HWAVEFORECAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.get_TBLWF24HWAVEFORECAST_AllData(model);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            model.SA24HWFBOHAIWAVEHEIGHT = tbdata[0];//渤海浪高
            model.SA24HWFNORTHOFYSWAVEHEIGHT = tbdata[1]; //黄海北部浪高
            model.SA24HWFMIDDLEOFYSWAVEHEIGHT = tbdata[2]; //黄海中部浪高
            model.SA24HWFSOUTHOFYSWAVEHEIGHT = tbdata[3];//黄海南部
            model.SA24HWFOFFSHOREWAVEHEIGHT = tbdata[4]; // 潍坊近岸浪高
            model.SA24HWFOFFSHORESW = tbdata[5];//潍坊近岸水温


            if (type == "add")
            {
                if (quanxian == "fl")
                {
                    addnum += sql.Add_TBLWF24HWAVEFORECAST_hl(model);
                }
                else if (quanxian == "sw")
                {
                    addnum += sql.Add_TBLWF24HWAVEFORECAST_sw(model);
                }
                else
                {
                    return "addsuccess";
                }
            }
            else if (type == "edit")
            {
                

                if (quanxian == "fl")
                {
                    editnum += sql.Edit_TBLWF24HWAVEFORECAST_hl(model);
                }
                else if (quanxian == "sw")
                {
                    editnum += sql.Edit_TBLWF24HWAVEFORECAST_sw(model);
                }
                else
                {
                    return "addsuccess";
                }
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
        /// 下午二十一、东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe41(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            HT_DYWAVEFORECAST model = new HT_DYWAVEFORECAST();
            model.PUBLISHDATE = date;
            Sql_DYWAVEFOREAST sql = new Sql_DYWAVEFOREAST();
            DataTable dt = (DataTable)sql.GetDyWaveForecastData(model);
            if (dt.Rows.Count > 0)//有数据 修改
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        model.TIMEEFFECTIVE = "12";
                        break;
                    case 1:
                        model.TIMEEFFECTIVE = "24";
                        break;
                    case 2:
                        model.TIMEEFFECTIVE = "48";
                        break;
                    case 3:
                        model.TIMEEFFECTIVE = "72";
                        break;
                    default:
                        break;
                }
                 
                model.WINDDIRECTION= tbdata[(i * 3)];
                model.WINDFORCE= tbdata[(i * 3)+1];
                model.WAVEHEIGHT = tbdata[(i * 3)+2];

                if (type == "add")
                {
                    addnum += sql.AddDyWaveForecastData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditDyWaveForecastData(model);
                }
            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 下午二十二、 东营埕岛-未来三天高/低潮预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe42(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            HT_DYTIDEFORECAST model = new HT_DYTIDEFORECAST();
            Sql_DYTIDEFOREAST sql = new Sql_DYTIDEFOREAST();
            model.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.GetDyTideForecastData(model);
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 3; i++)
            {
                model.FORECASTDATE = date.AddDays((i + 1));   //预报日期
                model.NOTFFIRSTHIGHWAVETIME = tbdata[(i * 8)]; //第一次高潮时间
                model.NOTFFIRSTHIGHWAVEHEIGHT = tbdata[(i * 8) + 1]; //第一次高潮潮位
                model.NOTFFIRSTLOWWAVETIME = tbdata[(i * 8) + 2];//第一次低潮时间
                model.NOTFFIRSTLOWWAVEHEIGHT = tbdata[(i * 8) + 3]; //第一次低潮潮位
                model.NOTFSECONDHIGHWAVETIME = tbdata[(i * 8) + 4]; //第二次高潮时间
                model.NOTFSECONDHIGHWAVEHEIGHT = tbdata[(i * 8) + 5]; //第二次高潮潮位
                model.NOTFSECONDLOWWAVETIME = tbdata[(i * 8) + 6]; //第二次低潮时间
                model.NOTFSECONDLOWWAVEHEIGHT = tbdata[(i * 8) + 7]; //第二次低潮潮位

                if (type == "add")
                {
                    addnum += sql.AddDyTideForecastData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditDyTideForecastData(model);
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
            else
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
        }

        /// <summary>
        /// 东营埕岛预报单编号
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string settabe43(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string rtnStr = "";
            string type = "edit";
            Sql_DYNO sql = new Sql_DYNO();
            var proYear = tbdata[0];
            var proNo= tbdata[1];
            DataTable dt = (DataTable)sql.GetDYNo(date);
            if (dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            if (type == "add")
            {
                addnum = sql.AddDYNo(date, proYear, proNo);
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
                editnum = sql.EditDYNo(date, proYear, proNo);
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
        /// 表单01.一、海洋牧场-海浪预报
        /// 海洋牧场下午19 新增加7个预报区域
        /// update by Lian
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        private string setTable47(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            OceanRanchWave oceanRanchWave = new OceanRanchWave();
            oceanRanchWave.PUBLISHDATE = date;
            sql_OceanRanchWave sql = new sql_OceanRanchWave();
            DataTable dt = (DataTable)sql.GetOceanRanchWaveList(oceanRanchWave);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }

            for (int i = 0; i < 10; i++)//修改3为10
            {
                switch (i)
                {
                    case 0:
                        oceanRanchWave.OCEANRANCHNAME = "寻山海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "寻山"; //海洋牧场短名称
                        oceanRanchWave.SN = "xsh"; //缩写
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
                    case 3:
                        oceanRanchWave.OCEANRANCHNAME = "滨州正海底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "滨州正海"; //海洋牧场短名称
                        oceanRanchWave.SN = "zhh"; //缩写
                        break;
                    case 4:
                        oceanRanchWave.OCEANRANCHNAME = "山东通和底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "山东通和"; //海洋牧场短名称
                        oceanRanchWave.SN = "the"; //缩写
                        break;
                    case 5:
                        oceanRanchWave.OCEANRANCHNAME = "山东莱州太平湾明波国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "太平湾明波"; //海洋牧场短名称
                        oceanRanchWave.SN = "mbo"; //缩写
                        break;
                    case 6:
                        oceanRanchWave.OCEANRANCHNAME = "山东琵琶口富瀚国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "琵琶口富瀚"; //海洋牧场短名称
                        oceanRanchWave.SN = "fuh"; //缩写
                        break;
                    case 7:
                        oceanRanchWave.OCEANRANCHNAME = "山东庙岛群岛东部佳益国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "庙岛群岛佳益"; //海洋牧场短名称
                        oceanRanchWave.SN = "jyi"; //缩写
                        break;
                    case 8:
                        oceanRanchWave.OCEANRANCHNAME = "山东海州湾顺风国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "海州湾顺风"; //海洋牧场短名称
                        oceanRanchWave.SN = "shf"; //缩写
                        break;
                    case 9:
                        oceanRanchWave.OCEANRANCHNAME = "山东岚山东部万泽丰国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchWave.OCEANRANCHSHORTNAME = "岚山万泽丰"; //海洋牧场短名称
                        oceanRanchWave.SN = "wzf"; //缩写
                        break;
                }

                oceanRanchWave.PUBLISHDATE = date;//填报日期
                oceanRanchWave.FORECASTDATE = date;//预报日期

                oceanRanchWave.WAVE24HDAY = tbdata[(i * 6)];  //24小时最大有效波高白天
                oceanRanchWave.WAVE24HNEIGHT = tbdata[(i * 6) + 1];  //24小时最大有效波高夜晚
                oceanRanchWave.WAVE48HDAY = tbdata[(i * 6) + 2]; //48小时最大有效波高白天
                oceanRanchWave.WAVE48HNEIGHT = tbdata[(i * 6) + 3]; //48小时最大有效波高夜晚
                oceanRanchWave.WAVE72HDAY = tbdata[(i * 6) + 4]; //72小时最大有效波高白天
                oceanRanchWave.WAVE72HNEIGHT = tbdata[(i * 6) + 5]; //72小时最大有效波高夜晚

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
                if (addnum == 10)
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
                if (editnum == 10)
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
        /// update by Lian 新增加7个预报地区3天的
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        private string setTable48(DateTime date, string data)
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

            for (int i = 0; i < 30; i++)//9改为30
            {
                switch (i)
                {
                    case 0:
                        oceanRanchTide.OCEANRANCHNAME = "寻山海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "寻山"; //海洋牧场短名称
                        oceanRanchTide.SN = "xsh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 1:
                        oceanRanchTide.OCEANRANCHNAME = "寻山海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "寻山"; //海洋牧场短名称
                        oceanRanchTide.SN = "xsh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 2:
                        oceanRanchTide.OCEANRANCHNAME = "寻山海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "寻山"; //海洋牧场短名称
                        oceanRanchTide.SN = "xsh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;
                    case 3:
                        oceanRanchTide.OCEANRANCHNAME = "荣成烟墩角游钓型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "烟墩角"; //海洋牧场短名称
                        oceanRanchTide.SN = "ydj"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 4:
                        oceanRanchTide.OCEANRANCHNAME = "荣成烟墩角游钓型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "烟墩角"; //海洋牧场短名称
                        oceanRanchTide.SN = "ydj"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 5:
                        oceanRanchTide.OCEANRANCHNAME = "荣成烟墩角游钓型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "烟墩角"; //海洋牧场短名称
                        oceanRanchTide.SN = "ydj"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;
                    case 6:
                        oceanRanchTide.OCEANRANCHNAME = "西霞口集团国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "西霞口"; //海洋牧场短名称
                        oceanRanchTide.SN = "xxk"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 7:
                        oceanRanchTide.OCEANRANCHNAME = "西霞口集团国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "西霞口"; //海洋牧场短名称
                        oceanRanchTide.SN = "xxk"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 8:
                        oceanRanchTide.OCEANRANCHNAME = "西霞口集团国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "西霞口"; //海洋牧场短名称
                        oceanRanchTide.SN = "xxk"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;

                    // add by Lian start
                    case 9:
                        oceanRanchTide.OCEANRANCHNAME = "滨州正海底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "滨州正海"; //海洋牧场短名称
                        oceanRanchTide.SN = "zhh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 10:
                        oceanRanchTide.OCEANRANCHNAME = "滨州正海底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "滨州正海"; //海洋牧场短名称
                        oceanRanchTide.SN = "zhh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 11:
                        oceanRanchTide.OCEANRANCHNAME = "滨州正海底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "滨州正海"; //海洋牧场短名称
                        oceanRanchTide.SN = "zhh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;

                    case 12:
                        oceanRanchTide.OCEANRANCHNAME = "山东通和底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "山东通和"; //海洋牧场短名称
                        oceanRanchTide.SN = "the"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 13:
                        oceanRanchTide.OCEANRANCHNAME = "山东通和底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "山东通和"; //海洋牧场短名称
                        oceanRanchTide.SN = "the"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 14:
                        oceanRanchTide.OCEANRANCHNAME = "山东通和底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "山东通和"; //海洋牧场短名称
                        oceanRanchTide.SN = "the"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;

                    case 15:
                        oceanRanchTide.OCEANRANCHNAME = "山东莱州太平湾明波国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "太平湾明波"; //海洋牧场短名称
                        oceanRanchTide.SN = "mbo"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 16:
                        oceanRanchTide.OCEANRANCHNAME = "山东莱州太平湾明波国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "太平湾明波"; //海洋牧场短名称
                        oceanRanchTide.SN = "mbo"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 17:
                        oceanRanchTide.OCEANRANCHNAME = "山东莱州太平湾明波国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "太平湾明波"; //海洋牧场短名称
                        oceanRanchTide.SN = "mbo"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;

                    case 18:
                        oceanRanchTide.OCEANRANCHNAME = "山东琵琶口富瀚国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "琵琶口富瀚"; //海洋牧场短名称
                        oceanRanchTide.SN = "fuh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 19:
                        oceanRanchTide.OCEANRANCHNAME = "山东琵琶口富瀚国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "琵琶口富瀚"; //海洋牧场短名称
                        oceanRanchTide.SN = "fuh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 20:
                        oceanRanchTide.OCEANRANCHNAME = "山东琵琶口富瀚国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "琵琶口富瀚"; //海洋牧场短名称
                        oceanRanchTide.SN = "fuh"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;

                    case 21:
                        oceanRanchTide.OCEANRANCHNAME = "山东庙岛群岛东部佳益国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "庙岛群岛佳益"; //海洋牧场短名称
                        oceanRanchTide.SN = "jyi"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 22:
                        oceanRanchTide.OCEANRANCHNAME = "山东庙岛群岛东部佳益国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "庙岛群岛佳益"; //海洋牧场短名称
                        oceanRanchTide.SN = "jyi"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 23:
                        oceanRanchTide.OCEANRANCHNAME = "山东庙岛群岛东部佳益国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "庙岛群岛佳益"; //海洋牧场短名称
                        oceanRanchTide.SN = "jyi"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;

                    case 24:
                        oceanRanchTide.OCEANRANCHNAME = "山东海州湾顺风国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "海州湾顺风"; //海洋牧场短名称
                        oceanRanchTide.SN = "shf"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 25:
                        oceanRanchTide.OCEANRANCHNAME = "山东海州湾顺风国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "海州湾顺风"; //海洋牧场短名称
                        oceanRanchTide.SN = "shf"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 26:
                        oceanRanchTide.OCEANRANCHNAME = "山东海州湾顺风国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "海州湾顺风"; //海洋牧场短名称
                        oceanRanchTide.SN = "shf"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;

                    case 27:
                        oceanRanchTide.OCEANRANCHNAME = "山东岚山东部万泽丰国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "岚山万泽丰"; //海洋牧场短名称
                        oceanRanchTide.SN = "wzf"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(1);
                        break;
                    case 28:
                        oceanRanchTide.OCEANRANCHNAME = "山东岚山东部万泽丰国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "岚山万泽丰"; //海洋牧场短名称
                        oceanRanchTide.SN = "wzf"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(2);
                        break;
                    case 29:
                        oceanRanchTide.OCEANRANCHNAME = "山东岚山东部万泽丰国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTide.OCEANRANCHSHORTNAME = "岚山万泽丰"; //海洋牧场短名称
                        oceanRanchTide.SN = "wzf"; //缩写
                        oceanRanchTide.FORECASTDATE = date.AddDays(3);
                        break;
                    //add by Lian end

                    default:
                        break;
                }
                oceanRanchTide.PUBLISHDATE = date;
                //oceanRanchTide.FORECASTDATE = date;
                //oceanRanchTide.OCEANRANCHNAME = "西霞口集团国家级海洋牧场";
                //oceanRanchTide.OCEANRANCHSHORTNAME = "西霞口";
                //oceanRanchTide.SN = "xxk";tbdata[(i * 7) + 1]
                oceanRanchTide.TIDE24H00 = tbdata[(i * 32)];
                oceanRanchTide.TIDE24H01 = tbdata[(i * 32) + 1];
                oceanRanchTide.TIDE24H02 = tbdata[(i * 32) + 2];
                oceanRanchTide.TIDE24H03 = tbdata[(i * 32) + 3];
                oceanRanchTide.TIDE24H04 = tbdata[(i * 32) + 4];
                oceanRanchTide.TIDE24H05 = tbdata[(i * 32) + 5];
                oceanRanchTide.TIDE24H06 = tbdata[(i * 32) + 6];
                oceanRanchTide.TIDE24H07 = tbdata[(i * 32) + 7];
                oceanRanchTide.TIDE24H08 = tbdata[(i * 32) + 8];
                oceanRanchTide.TIDE24H09 = tbdata[(i * 32) + 9];
                oceanRanchTide.TIDE24H10 = tbdata[(i * 32) + 10];
                oceanRanchTide.TIDE24H11 = tbdata[(i * 32) + 11];
                oceanRanchTide.TIDE24H12 = tbdata[(i * 32) + 12];
                oceanRanchTide.TIDE24H13 = tbdata[(i * 32) + 13];
                oceanRanchTide.TIDE24H14 = tbdata[(i * 32) + 14];
                oceanRanchTide.TIDE24H15 = tbdata[(i * 32) + 15];
                oceanRanchTide.TIDE24H16 = tbdata[(i * 32) + 16];
                oceanRanchTide.TIDE24H17 = tbdata[(i * 32) + 17];
                oceanRanchTide.TIDE24H18 = tbdata[(i * 32) + 18];
                oceanRanchTide.TIDE24H19 = tbdata[(i * 32) + 19];
                oceanRanchTide.TIDE24H20 = tbdata[(i * 32) + 20];
                oceanRanchTide.TIDE24H21 = tbdata[(i * 32) + 21];
                oceanRanchTide.TIDE24H22 = tbdata[(i * 32) + 22];
                oceanRanchTide.TIDE24H23 = tbdata[(i * 32) + 23];

                oceanRanchTide.TIDEFIRSTHTIME = tbdata[(i * 32) + 24];
                oceanRanchTide.TIDEFIRSTHHEIGHT = tbdata[(i * 32) + 25];
                oceanRanchTide.TIDESECONDHTIME = tbdata[(i * 32) + 26];
                oceanRanchTide.TIDESECONDHHEIGHT = tbdata[(i * 32) + 27];
                oceanRanchTide.TIDEFIRSTLTIME = tbdata[(i * 32) + 28];
                oceanRanchTide.TIDEFIRSTLHEIGHT = tbdata[(i * 32) + 29];
                oceanRanchTide.TIDESECONDLTIME = tbdata[(i * 32) + 30];
                oceanRanchTide.TIDESECONDLHEIGHT = tbdata[(i * 32) + 31];
                if (type == "add")
                {
                    addnum += sql.InsertOceanRanchTideList(oceanRanchTide);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditOceanRanchTideList   (oceanRanchTide);
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
        /// 新增7个海洋牧场预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        private string setTable49(DateTime date, string data)
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

            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        oceanRanchTemp.OCEANRANCHNAME = "寻山海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "寻山"; //海洋牧场短名称
                        oceanRanchTemp.SN = "xsh"; //缩写
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

                    case 3:
                        oceanRanchTemp.OCEANRANCHNAME = "滨州正海底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "滨州正海"; //海洋牧场短名称
                        oceanRanchTemp.SN = "zhh"; //缩写
                        break;
                    case 4:
                        oceanRanchTemp.OCEANRANCHNAME = "山东通和底播型海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "山东通和"; //海洋牧场短名称
                        oceanRanchTemp.SN = "the"; //缩写
                        break;
                    case 5:
                        oceanRanchTemp.OCEANRANCHNAME = "山东莱州太平湾明波国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "太平湾明波"; //海洋牧场短名称
                        oceanRanchTemp.SN = "mbo"; //缩写
                        break;
                    case 6:
                        oceanRanchTemp.OCEANRANCHNAME = "山东琵琶口富瀚国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "琵琶口富瀚"; //海洋牧场短名称
                        oceanRanchTemp.SN = "fuh"; //缩写
                        break;
                    case 7:
                        oceanRanchTemp.OCEANRANCHNAME = "山东庙岛群岛东部佳益国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "庙岛群岛佳益"; //海洋牧场短名称
                        oceanRanchTemp.SN = "jyi"; //缩写
                        break;
                    case 8:
                        oceanRanchTemp.OCEANRANCHNAME = "山东海州湾顺风国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "海州湾顺风"; //海洋牧场短名称
                        oceanRanchTemp.SN = "shf"; //缩写
                        break;
                    case 9:
                        oceanRanchTemp.OCEANRANCHNAME = "山东岚山东部万泽丰国家级海洋牧场"; //海洋牧场长名称
                        oceanRanchTemp.OCEANRANCHSHORTNAME = "岚山万泽丰"; //海洋牧场短名称
                        oceanRanchTemp.SN = "wzf"; //缩写
                        break;
                }

                oceanRanchTemp.PUBLISHDATE = date;//填报日期
                oceanRanchTemp.FORECASTDATE = date;   //预报日期

                oceanRanchTemp.TEMP24H = tbdata[(i * 3)];  //24小时海温平均值
                oceanRanchTemp.TEMP48H = tbdata[(i * 3) + 1];  //48小时海温平均值
                oceanRanchTemp.TEMP72H = tbdata[(i * 3) + 2]; //72小时海温平均值

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
                if (addnum == 10)
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
                if (editnum == 10)
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
        /// 上午十一、烟台南部海浪、水温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable50(DateTime date, string data,string quanxian)
        {
            if(quanxian == "cx")
            {
                return "editsuccess";
            }
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            YT_WaveForecast model = new YT_WaveForecast();
            model.PUBLISHDATE = date;
            Sql_YT_WaveForecast sql = new Sql_YT_WaveForecast();
            DataTable dt = (DataTable)sql.GetWaveDataBy_T(model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }

            model.FORECASTDATE = date.AddDays(1); //预报日期
            model.WAVELEVELONE = tbdata[0];
            model.WAVELEVELTYPE = tbdata[1];
            model.WAVEDIRECTION = tbdata[2];
            model.WATERTEMPERATURE = tbdata[3];

            if (type == "add")
            {
                addnum += sql.AddWaveData(model, quanxian);
            }
            else if (type == "edit")
            {
                editnum += sql.EditWaveData(model, quanxian);
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
        /// 上午十二、海阳近岸海域潮汐预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable51(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";            
            YT_TideForecast model = new YT_TideForecast();
            model.PUBLISHDATE = date;
            Sql_YT_TideForecast sql = new Sql_YT_TideForecast();
            DataTable dt = (DataTable)sql.GetTideDataBy_T(model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }

            model.FORECASTDATE = date.AddDays(1); //预报日期
            model.FIRSTHIGHTIME = tbdata[0];
            model.FIRSTHIGHLEVEL = tbdata[1];
            model.SECONDHIGHTIME = tbdata[2];
            model.SECONDHIGHLEVEL = tbdata[3];
            model.FIRSTLOWTIME = tbdata[4];
            model.FIRSTLOWLEVEL = tbdata[5];
            model.SECONDLOWTIME = tbdata[6];
            model.SECONDLOWLEVEL = tbdata[7];

            if (type == "add")
            {
                addnum += sql.AddTideData(model);
            }
            else if (type == "edit")
            {
                editnum += sql.EditTideData(model);
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
        /// 上午十三、海阳万米海滩海水浴场风、浪预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable52(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            YT_YC model = new YT_YC();
            model.PUBLISHDATE = date;
            Sql_YT_YC sql = new Sql_YT_YC();
            DataTable dt = (DataTable)sql.GetYcDataBy_T(model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }

            model.FORECASTDATE = date.AddDays(1); //预报日期
            model.WEATERSTATE = tbdata[0];
            model.TEMPERATURE = tbdata[1];
            model.WINDSPEED = tbdata[2];
            model.WINDDIRECTION = tbdata[3];
            model.WAVEHEIGHT = tbdata[4];

            if (type == "add")
            {
                addnum += sql.AddYcData(model);
            }
            else if (type == "edit")
            {
                editnum += sql.EditYcData(model);
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
        /// 表单43.东营埕岛预报编号
        /// </summary>
        /// <returns></returns>
        private string setTable53(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string rtnStr = "";
            string type = "edit";
            Sql_SDSEVENNO sql = new Sql_SDSEVENNO();
            var proYear = tbdata[0];
            var proNo = tbdata[1];
            DataTable dt = (DataTable)sql.GetSDSevenNO(date);
            if (dt!= null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else //无数据 新增
            {
                type = "add";
            }
            if (type == "add")
            {
                addnum = sql.AddSDSevenNo(date, proYear, proNo);
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
                editnum = sql.EditSDSevenNo(date, proYear, proNo);
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
        /// 表单54.下午二十四、东营广利渔港-未来三天高/低潮预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable54(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTideData("DYGLFP", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 1];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 2];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 3];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 4];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 5];
                model.SECONDLOWTIME = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = "DYGLFP";
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
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
        /// 表单55.下午二十五、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable55(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetWaveData("DYGLFP", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.WINDDIRECTION = tbdata[(i * 3) + 0];
                model.WINDFORCE = tbdata[(i * 3) + 1];
                model.WAVEHEIGHT = tbdata[(i * 3) + 2];
                model.FORECASTAREA = "DYGLFP";
                if (type == "add")
                {
                    addnum += sql.AddWaveData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditWaveData(model);
                }

            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 表单56.下午二十六、东营广利渔港-未来三天的海面水温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable56(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTemperatureData("DYGLFP", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.TEMPERATURE = tbdata[i];
                model.FORECASTAREA = "DYGLFP";
                if (type == "add")
                {
                    addnum += sql.AddTemperatureData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTemperatureData(model);
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
        /// 表单57.下午二十七、日照桃花岛-未来三天高/低潮预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable57(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTideData("RZTHD", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 1];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 2];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 3];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 4];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 5];
                model.SECONDLOWTIME = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = "RZTHD";
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
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
        /// 表单58.下午二十八、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable58(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetWaveData("RZTHD", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.WINDDIRECTION = tbdata[(i * 3) + 0];
                model.WINDFORCE = tbdata[(i * 3) + 1];
                model.WAVEHEIGHT = tbdata[(i * 3) + 2];
                model.FORECASTAREA = "RZTHD";
                if (type == "add")
                {
                    addnum += sql.AddWaveData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditWaveData(model);
                }

            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 表单59.下午二十九、日照桃花岛-未来三天的海面水温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable59(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTemperatureData("RZTHD", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.TEMPERATURE = tbdata[i];
                model.FORECASTAREA = "RZTHD";
                if (type == "add")
                {
                    addnum += sql.AddTemperatureData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTemperatureData(model);
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
        /// 表单60.下午三十、潍坊度假区-未来三天高/低潮预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable60(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTideData("WFDJQ", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 1];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 2];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 3];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 4];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 5];
                model.SECONDLOWTIME = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = "WFDJQ";
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
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
        /// 表单61.下午三十一、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable61(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetWaveData("WFDJQ", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.WINDDIRECTION = tbdata[(i * 3) + 0];
                model.WINDFORCE = tbdata[(i * 3) + 1];
                model.WAVEHEIGHT = tbdata[(i * 3) + 2];
                model.FORECASTAREA = "WFDJQ";
                if (type == "add")
                {
                    addnum += sql.AddWaveData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditWaveData(model);
                }

            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 表单62.下午三十二、潍坊度假区-未来三天的海面水温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable62(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTemperatureData("WFDJQ", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.TEMPERATURE = tbdata[i];
                model.FORECASTAREA = "WFDJQ";
                if (type == "add")
                {
                    addnum += sql.AddTemperatureData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTemperatureData(model);
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
        /// 表单63.下午三十三、威海新区-未来三天高/低潮预报
        /// 同时将表单63的前两条数据存入到下午16的数据库中以文登入库
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable63(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTideData("WHXQ", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 1];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 2];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 3];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 4];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 5];
                model.SECONDLOWTIME = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = "WHXQ";
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
                }
            }
            //将数据同时提交到TBLWEIHAISHIDAOTIDALFORECAST，但是只去前两天的数据
            SetTableWD(date, data);

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
        /// 表单63提交时同时将数据提交到另一个表中
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string SetTableWD(DateTime date, string data)
        {
            //潮汐拆分传来的地区
            string areaname = "文登";
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";

            sql_TBLWEIHAISHIDAOTIDALFORECAST sql = new sql_TBLWEIHAISHIDAOTIDALFORECAST();
            TBLWEIHAISHIDAOTIDALFORECAST model = new TBLWEIHAISHIDAOTIDALFORECAST();
            model.PUBLISHDATE = date;
           
            DataTable dt = (DataTable)sql.get_TBLWEIHAISHIDAOTIDALFORECAST_AllDataByArea(model, areaname);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 2; i++)
            {
                switch (i)
                {
                    case 0:
                        model.REPORTAREA = areaname;
                        model.FORECASTDATE = date.AddDays(1);// 预报日期
                        break;
                    case 1:
                        model.REPORTAREA = areaname;
                        model.FORECASTDATE = date.AddDays(2);// 预报日期
                        break;
                    default:
                        break;
                }
                model.FIRSTHIGHWAVETIME = tbdata[(i * 8)];//第一次高潮潮时
                model.FIRSTHIGHWAVEHEIGHT = tbdata[(i * 8) + 1];//第一次高潮潮高
                model.SECONDHIGHWAVETIME = tbdata[(i * 8) + 2];// 第二次高潮潮时
                model.SECONDHIGHWAVEHEIGHT = tbdata[(i * 8) + 3];// 第二次高潮潮高
                model.FIRSTLOWWAVETIME = tbdata[(i * 8) + 4];//第一次低潮潮时
                model.FIRSTLOWWAVEHEIGHT = tbdata[(i * 8) + 5];// 第一次低潮潮高
                model.SECONDLOWWAVETIME = tbdata[(i * 8) + 6];// 第二次低潮潮时
                model.SECONDLOWWAVEHEIGHT = tbdata[(i * 8) + 7];//第二次低潮潮高

                //model.FIRSTHIGHWAVETIME = tbdata[(i * 8)];//第一次高潮潮时
                //model.FIRSTHIGHWAVEHEIGHT = tbdata[(i * 8) + 1];//第一次高潮潮高
                //model.FIRSTLOWWAVETIME = tbdata[(i * 8) + 2];//第一次低潮潮时
                //model.FIRSTLOWWAVEHEIGHT = tbdata[(i * 8) + 3];// 第一次低潮潮高
                //model.SECONDHIGHWAVETIME = tbdata[(i * 8) + 4];// 第二次高潮潮时
                //model.SECONDHIGHWAVEHEIGHT = tbdata[(i * 8) + 5];// 第二次高潮潮高
                //model.SECONDLOWWAVETIME = tbdata[(i * 8) + 6];// 第二次低潮潮时
                //model.SECONDLOWWAVEHEIGHT = tbdata[(i * 8) + 7];//第二次低潮潮高
                if (type == "add")
                {
                    addnum += sql.Add_TBLWEIHAISHIDAOTIDALFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLWEIHAISHIDAOTIDALFORECAST(model);
                }
            }

            if (type == "add")
            {
                if (addnum == 2)
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
                if (editnum == 2)
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
        /// 表单64.下午三十四、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable64(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetWaveData("WHXQ", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.WINDDIRECTION = tbdata[(i * 3) + 0];
                model.WINDFORCE = tbdata[(i * 3) + 1];
                model.WAVEHEIGHT = tbdata[(i * 3) + 2];
                model.FORECASTAREA = "WHXQ";
                if (type == "add")
                {
                    addnum += sql.AddWaveData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditWaveData(model);
                }

            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 表单65.下午三十五、威海新区-未来三天的海面水温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable65(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTemperatureData("WHXQ", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.TEMPERATURE = tbdata[i];
                model.FORECASTAREA = "WHXQ";
                if (type == "add")
                {
                    addnum += sql.AddTemperatureData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTemperatureData(model);
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
        /// 表单66.下午三十六、烟台清泉-未来三天高/低潮预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable66(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTideData("YTQQ", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 1];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 2];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 3];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 4];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 5];
                model.SECONDLOWTIME = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = "YTQQ";
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
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
        /// 表单67.下午三十七、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable67(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetWaveData("YTQQ", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.WINDDIRECTION = tbdata[(i * 3) + 0];
                model.WINDFORCE = tbdata[(i * 3) + 1];
                model.WAVEHEIGHT = tbdata[(i * 3) + 2];
                model.FORECASTAREA = "YTQQ";
                if (type == "add")
                {
                    addnum += sql.AddWaveData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditWaveData(model);
                }

            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 表单68.下午三十八、烟台清泉-未来三天的海面水温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable68(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTemperatureData("YTQQ", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.TEMPERATURE = tbdata[i];
                model.FORECASTAREA = "YTQQ";
                if (type == "add")
                {
                    addnum += sql.AddTemperatureData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTemperatureData(model);
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
        /// 表单69.下午三十九、董家口-未来三天高/低潮预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable69(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTideData("DJKP", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 1];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 2];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 3];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 4];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 5];
                model.SECONDLOWTIME = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = "DJKP";
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
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
        /// 表单70.下午四十、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable70(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetWaveData("DJKP", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.WINDDIRECTION = tbdata[(i * 3) + 0];
                model.WINDFORCE = tbdata[(i * 3) + 1];
                model.WAVEHEIGHT = tbdata[(i * 3) + 2];
                model.FORECASTAREA = "DJKP";
                if (type == "add")
                {
                    addnum += sql.AddWaveData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditWaveData(model);
                }

            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 表单71.下午四十一、董家口-未来三天的海面水温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable71(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTemperatureData("DJKP", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.TEMPERATURE = tbdata[i];
                model.FORECASTAREA = "DJKP";
                if (type == "add")
                {
                    addnum += sql.AddTemperatureData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTemperatureData(model);
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
        /// 表单72.下午四十二、东营渔港-未来三天高/低潮预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable72(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTideData("DYFP", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 1];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 2];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 3];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 4];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 5];
                model.SECONDLOWTIME = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = "DYFP";
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
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
        /// 表单73.下午四十三、东营渔港-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable73(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetWaveData("DYFP", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                type = "edit";
            }
            else
            {
                type = "add";
            }
            for (int i = 0; i < 4; i++)
            {
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.WINDDIRECTION = tbdata[(i * 3) + 0];
                model.WINDFORCE = tbdata[(i * 3) + 1];
                model.WAVEHEIGHT = tbdata[(i * 3) + 2];
                model.FORECASTAREA = "DYFP";
                if (type == "add")
                {
                    addnum += sql.AddWaveData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditWaveData(model);
                }

            }

            if (type == "add")
            {
                if (addnum == 4)
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
                if (editnum == 4)
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
        /// 表单74.下午四十四、东营渔港-未来三天的海面水温预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable74(DateTime date, string data)
        {
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = date;
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            DataTable dt = (DataTable)sql.GetTemperatureData("DYFP", model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.TEMPERATURE = tbdata[i];
                model.FORECASTAREA = "DYFP";
                if (type == "add")
                {
                    addnum += sql.AddTemperatureData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTemperatureData(model);
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
        /// 表单75,76,77,78,79,80,81,拆分的潮汐数据提交到一个方法里面
        /// 下午1,4,9,11,12,14,16
        /// 潮时的数据提交
        /// </summary>
        /// private string setTable75(DateTime date, string data,HttpContext context)
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable75(DateTime date, string data, HttpContext context)
        {
            //潮汐拆分传来的地区
            string areaname = context.Request.Form["areaname"].ToString();
            var tbdata = data.Split(',');
            var areas = "";
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            sql_TBLSDOFFSHORESEVENCITY24HTIDE sql = new sql_TBLSDOFFSHORESEVENCITY24HTIDE();
            TBLSDOFFSHORESEVENCITY24HTIDE model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            model.PUBLISHDATE = date;
            switch (areaname)
            {
                case "QD":
                    areas = "青岛";
                    break;
                case "RZ":
                    areas = "日照";
                    break;
                case "WH":
                    areas = "威海";
                    break;
                case "YT":
                    areas = "烟台";
                    break;
                case "WF":
                    areas = "潍坊";
                    break;
                case "DY":
                    areas = "东营";
                    break;
                case "BZ":
                    areas = "滨州";
                    break;
                default:
                    break;
            }
            DataTable dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HTIDE_AllDataByArea(model,areas);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for(int i = 0; i < 3; i++)
            {
                switch(i)
                {
                    case 0:
                        model.SDOSCTCITY = areas;
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 1:
                        model.SDOSCTCITY = areas;
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 2:
                        model.SDOSCTCITY = areas;
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    default:
                        break;
                }
                for(int j = 0; j < 4; j++)
                {
                    var str = tbdata[(i * 4) + j];
                    var h = "";
                    var min = "";

                    if (str != "" && str.IndexOf("-") == -1)
                    {
                        h = str.Substring(0, 2);
                        min = str.Substring(2);
                    }
                    else
                    {
                        if (str.Length >= 2)
                        {
                            h = str.Substring(0, 1);
                            min = str.Substring(1);
                        }
                        else
                        {
                            h = "-";
                            min = "-";
                        }
                    }
                    switch (j)
                    {
                        case 0:
                            model.SDOSCTFIRSTHIGHWAVEHOUR = h; //第一次高潮时
                            model.SDOSCTFIRSTHIGHWAVEMINUTE = min;//第一次高潮分
                            break;
                        case 1:
                            model.SDOSCTSECONDHIGHWAVEHOUR = h; //第二次高潮时
                            model.SDOSCTSECONDHIGHWAVEMINUTE = min; //第二次高潮分
                            break;
                        case 2:
                            model.SDOSCTFIRSTLOWWAVEHOUR = h; //第一次低潮时
                            model.SDOSCTFIRSTLOWWAVEMINUTE = min; //第一次低潮分
                            break;
                        case 3:
                            model.SDOSCTSECONDLOWWAVEHOUR = h; //第二次低潮时
                            model.SDOSCTSECONDLOWWAVEMINUTE = min; //第二次低潮分
                            break;
                        default: break;
                    }
                }
                if(type == "add")
                {
                    addnum += sql.Add_TBLSDOFFSHORESEVENCITY24HTIDE(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLSDOFFSHORESEVENCITY24HTIDE(model);
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
            else
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
        }

        /// <summary>
        /// 表单75,76,77,78,79,80,81拆分的潮汐数据提交到一个方法里面
        /// 下午1,4,9,11,12,14,16
        /// 潮高的数据提交
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable76(DateTime date, string data, HttpContext context)
        {
            //潮汐拆分传来的地区
            string areaname = context.Request.Form["areaname"].ToString();
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            var areas = "";
            sql_TideData sql = new sql_TideData();
            HT_TideData model = new HT_TideData();
            model.PUBLISHDATE = date;//填报日期
            switch (areaname)
            {
                case "QD":
                    areas = "青岛";
                    break;
                case "RZ":
                    areas = "日照";
                    break;
                case "WH":
                    areas = "威海";
                    break;
                case "YT":
                    areas = "烟台";
                    break;
                case "WF":
                    areas = "潍坊";
                    break;
                case "DY":
                    areas = "东营";
                    break;
                case "BZ":
                    areas = "滨州";
                    break;
                default:
                    break;
            }
            DataTable dt = (DataTable)sql.getTideDataByArea(model, areas);
            if (dt != null && dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        model.SDOSCTCITY = areas;
                        model.FORECASTDATE = date.AddDays(1);
                        break;
                    case 1:
                        model.SDOSCTCITY = areas;
                        model.FORECASTDATE = date.AddDays(2);
                        break;
                    case 2:
                        model.SDOSCTCITY = areas;
                        model.FORECASTDATE = date.AddDays(3);
                        break;
                    default:
                        break;
                }
                model.FIRSTHIGHWAVETIDEDATA = tbdata[(i * 4)]; //第一次高潮潮高
                model.SECONDHIGHWAVETIDEDATA = tbdata[(i * 4) + 1];//第二次高潮潮高
                model.FIRSTLOWWAVETIDEDATA = tbdata[(i * 4) + 2]; //第一次低潮潮高
                model.SECONDLOWWAVETIDEDATA = tbdata[(i * 4) + 3]; //第二次低潮潮高
                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideDate(model);
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
            else
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
        }
        /// <summary>
        /// 新潮汐下午1,4,11,12将数据同时提交到TBLREFINETIDE表中
        /// 综合数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable77(DateTime date, string data, HttpContext context)
        {
            //潮汐拆分传来的地区
            string areaname = context.Request.Form["areaname"].ToString();
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            string type = "edit";
            var areas = "";
            TBLSEVENTIDE model = new TBLSEVENTIDE();
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = date;
            switch (areaname)
            {
                case "QD":
                    areas = "DYFP";
                    break;
                case "RZ":
                    areas = "RZTHD";
                    break;
                case "YT":
                    areas = "YTQQ";
                    break;
                case "WF":
                    areas = "WFDJQ";
                    break;
                default:
                    break;
            }
            DataTable dt = (DataTable)sql.GetTideData(areas, model);
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
                model.FORECASTDATE = date.AddDays(i + 1); //预报日期
                model.FIRSTHIGHTIME = tbdata[(i * 8) + 0];
                model.SECONDHIGHTIME = tbdata[(i * 8) + 1];
                model.FIRSTLOWTIME = tbdata[(i * 8) + 2];
                model.SECONDLOWTIME = tbdata[(i * 8) + 3];               
                model.FIRSTHIGHLEVEL = tbdata[(i * 8) + 4];
                model.SECONDHIGHLEVEL = tbdata[(i * 8) + 5];
                model.FIRSTLOWLEVEL = tbdata[(i * 8) + 6];
                model.SECONDLOWLEVEL = tbdata[(i * 8) + 7];
                model.FORECASTAREA = areas;

                if (type == "add")
                {
                    addnum += sql.AddTideData(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.EditTideData(model);
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
        /// 表单82,83,84拆分的潮汐数据提交到一个方法里面
        /// 下午5,7,8
        /// 潮时的数据提交
        /// </summary>
        /// <param name="date"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string setTable82(DateTime date, string data, HttpContext context)
        {
            //潮汐拆分传来的地区
            string areaname = context.Request.Form["areaname"].ToString();
            var tbdata = data.Split(',');
            int addnum = 0;
            int editnum = 0;
            int i1 = 0;
            var areas = "";
            string type = "edit";

            sql_TBLWEIHAISHIDAOTIDALFORECAST sql = new sql_TBLWEIHAISHIDAOTIDALFORECAST();
            TBLWEIHAISHIDAOTIDALFORECAST model = new TBLWEIHAISHIDAOTIDALFORECAST();
            model.PUBLISHDATE = date;
            switch (areaname)
            {
                case "RS":
                    areas = "乳山";
                    break;
                case "SD":
                    areas = "石岛";
                    break;
                case "CST":
                    areas = "成山头";
                    break;
                default:
                    break;
            }
            DataTable dt = (DataTable)sql.get_TBLWEIHAISHIDAOTIDALFORECAST_AllDataByArea(model,areas);
            if (dt.Rows.Count > 0)
            {
            }
            else //无数据 新增
            {
                type = "add";
            }
            for(int i = 0; i < 2; i++)
            {
                switch (i)
                {
                    case 0:
                        model.REPORTAREA = areas;
                        model.FORECASTDATE = date.AddDays(1);// 预报日期
                        break;
                    case 1:
                        model.REPORTAREA = areas;
                        model.FORECASTDATE = date.AddDays(2);// 预报日期
                        break;
                    default:
                        break;
                }
                model.FIRSTHIGHWAVETIME = tbdata[(i * 8)];//第一次高潮潮时
                model.FIRSTHIGHWAVEHEIGHT = tbdata[(i * 8) + 1];//第一次高潮潮高
                model.FIRSTLOWWAVETIME = tbdata[(i * 8) + 2];//第一次低潮潮时
                model.FIRSTLOWWAVEHEIGHT = tbdata[(i * 8) + 3];// 第一次低潮潮高
                model.SECONDHIGHWAVETIME = tbdata[(i * 8) + 4];// 第二次高潮潮时
                model.SECONDHIGHWAVEHEIGHT = tbdata[(i * 8) + 5];// 第二次高潮潮高
                model.SECONDLOWWAVETIME = tbdata[(i * 8) + 6];// 第二次低潮潮时
                model.SECONDLOWWAVEHEIGHT = tbdata[(i * 8) + 7];//第二次低潮潮高
                if (type == "add")
                {
                    addnum += sql.Add_TBLWEIHAISHIDAOTIDALFORECAST(model);
                }
                else if (type == "edit")
                {
                    editnum += sql.Edit_TBLWEIHAISHIDAOTIDALFORECAST(model);
                }
            }

            if (type == "add")
            {
                if (addnum == 2)
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
                if (editnum == 2)
                {
                    return "editsuccess";
                }
                else
                {
                    return "editerror";
                }
            }
        }

        #endregion

        #region 根据时间查询表单数据
        /// <summary>
        /// 根据时间查询数据上午短期预报
        /// </summary>
        /// <param name="context"></param>
        public void getbydata(HttpContext context)
        {

            var date = DateTime.Parse(context.Request["date"].ToString());
            var searchType = context.Request["searchtype"].ToString();//searchtype 按填报日期还是预报日期查询 p:填报日期 f:预报日期
            StringBuilder sb_str = new StringBuilder();
            sb_str.Append("[");
            sb_str.Append(gettabe01(date, searchType)); //上午一、72小时渤海海区及黄河海港风、浪预报
           
            sb_str.Append(gettabe02(date, searchType)); //上午二、72小时港口潮位预报
            sb_str.Append(gettabe03(date)); //上午五、预计未来24小时海浪、水温预报
            sb_str.Append(gettabe04(date)); //上午六、24小时潮位预报
            sb_str.Append(gettabe23(date));
            //sb_str.Append(gettabe24(date));
            //sb_str.Append(gettable25(date, searchType));
            sb_str.Append(gettable27(date)); //上午三、3天海洋水文气象预报综述
            sb_str.Append(gettable28(date)); //上午四、24小时水文气象预报综述
            //sb_str.Append(gettable29(date, searchType));
            //sb_str.Append(gettable30(date));
            //sb_str.Append(gettable31(date));
            //sb_str.Append(gettable32(date));
            //sb_str.Append(gettable36(date)); //上午九、潍坊港24小时潮汐预报 取消不用合并到上午八潍坊港24小时 100710 edit by yuy
            //sb_str.Append(gettabe34(date, searchType));
            //sb_str.Append(gettabe13(date, searchType));
            sb_str.Append(gettable37(date));//上午十、海区24小时海浪、水温预报
            sb_str.Append(gettabe39(date, searchType));//上午七、海上丝绸之路三天海浪、气象预报
            
            sb_str.Append(gettabe38(date, searchType,"AM"));//上午八、海上丝绸之路三天潮汐预报
            sb_str.Append(gettable50(date));//上午十一、烟台南部海浪、水温预报
            sb_str.Append(gettable51(date));//上午十二、海阳近岸海域潮汐预报
            sb_str.Append(gettable52(date));//上午十三、海阳万米海滩海水浴场风、浪预报
            context.Response.Write(sb_str.Replace("[,{", "[{").ToString() + "]");
        }

        /// <summary>
        /// 上午指挥处、渔政局
        /// </summary>
        /// <param name="context"></param>
        private void getAMdataNew(HttpContext context)
        {
            var date = DateTime.Parse(context.Request["date"].ToString());
            var searchType = context.Request["searchtype"].ToString();//searchtype 按填报日期还是预报日期查询 p:填报日期 f:预报日期
            StringBuilder sb_str = new StringBuilder();
            sb_str.Append("[");
            sb_str.Append(gettabe23(date));
            sb_str.Append(gettable44(date, searchType));//指挥处
            //sb_str.Append(gettable45(date, searchType));//渔政局
            context.Response.Write(sb_str.Replace("[,{", "[{").ToString() + "]");
        }

        /// <summary>
        /// 查询周报信息
        /// </summary>
        /// <param name="context"></param>
        public void getbydataWeek(HttpContext context)
        {

            var date = DateTime.Parse(context.Request["date"].ToString());
            
            var searchType = context.Request["searchtype"].ToString();//searchtype 按填报日期还是预报日期查询 p:填报日期 f:预报日期
            StringBuilder sb_str = new StringBuilder();
            sb_str.Append("[");
           
            sb_str.Append(gettabe03(date));
            sb_str.Append(gettabe04(date));
            sb_str.Append(gettabe24(date));
            sb_str.Append(gettabe23(date));
            sb_str.Append(gettable28(date));
            sb_str.Append(gettable29(date, searchType));
            sb_str.Append(gettable30(date));
            
            sb_str.Append(gettable31(date));//周一的“周报八”，将周日“上午二”潮汐预报里的龙口港和黄河海港的48小时和72小时预报数据放到这里的第一天和第二天位置modify by xp 2018-9-7
            sb_str.Append(gettable32(date));
            //sb_str.Append(gettable36(date)); //上午九、潍坊港24小时潮汐预报
            sb_str.Append(gettable37(date));
            sb_str.Append(gettabe39(date, searchType));//丝绸之路风浪、气象
            //sb_str.Append(gettabe38(date, searchType));//丝绸之路潮汐
            
            sb_str.Append(gettabe38(date, searchType,"Week"));//丝绸之路潮汐  周报五
            sb_str.Append(gettable50(date));
            sb_str.Append(gettable51(date));
            sb_str.Append(gettable52(date));
            context.Response.Write(sb_str.Replace("[,{", "[{").ToString() + "]");
        }

        /// <summary>
        /// 查询下午指挥处、渔政局信息
        /// </summary>
        /// <param name="context"></param>
        public void getPMdataNew(HttpContext context)
        {
            var date = DateTime.Parse(context.Request["date"].ToString());
            var searchType = context.Request["searchtype"].ToString();//searchtype 按填报日期还是预报日期查询 p:填报日期 f:预报日期
            StringBuilder sb_str = new StringBuilder();
            sb_str.Append("[");
            sb_str.Append(gettabe23(date));//填报信息
            sb_str.Append(gettable26(date, searchType));
            sb_str.Append(gettable35(date, searchType));
            context.Response.Write(sb_str.Replace("[,{", "[{").ToString() + "]");
        }

        /// <summary>
        /// 根据时间查询数据(下午)
        /// </summary>
        /// <param name="context"></param>
        public void getbydataPM(HttpContext context)
        {

            var date = DateTime.Parse(context.Request["date"].ToString());
            var searchType = context.Request["searchtype"].ToString();//searchtype 按填报日期还是预报日期查询 p:填报日期 f:预报日期
            StringBuilder sb_str = new StringBuilder();
            sb_str.Append("[");
            sb_str.Append(gettabe05(date));//下午一、各海区24小时海浪预报
            //RiZhiManage 05
            WriteLog.WriteDebug("PM-05");
            sb_str.Append(gettabe06(date)); //下午二、山东省近海七市3天海浪、水温预报
            //RiZhiManage 06
            WriteLog.WriteDebug("PM-06");
            sb_str.Append(gettabe07(date));//下午三、山东省近海七市72小时潮汐预报
            WriteLog.WriteDebug("PM-07");
            //edit by Yuy in 180712
            sb_str.Append(gettabe08(date)); //下午、青岛24小时潮位预报 潮汐取消不用去下午三青岛48小时数据
            //sb_str.Append(gettabe20(date)); //下午、青岛沿岸48小时潮汐预报 取消不用去下午三青岛48小时数据
            sb_str.Append(gettabe10(date, searchType));//下午四、明泽闸潮位预报
            WriteLog.WriteDebug("PM-10");
            sb_str.Append(gettabe11(date, searchType)); //下午五、南堡油田海域波浪、风、水温预报
            WriteLog.WriteDebug("PM-11");
            sb_str.Append(gettabe12(date, searchType));//下午六、南堡油田海域潮汐预报
            WriteLog.WriteDebug("PM-12");
            sb_str.Append(gettabe13(date, searchType));//下午七、海区24小时海浪、水温预报
            WriteLog.WriteDebug("PM-13");
            sb_str.Append(gettabe14(date, searchType));//下午八、海区48小时海浪预报
            WriteLog.WriteDebug("PM-14");
            sb_str.Append(gettabe15(date));//下午九、海区72小时海浪预报
            WriteLog.WriteDebug("PM-15");
            //edit by Yuy in 180712
            //sb_str.Append(gettabe16(date));//下午、潍坊港24小时潮汐预报  取消不用去下午三潍坊48小时数据
            sb_str.Append(gettabe17(date)); //下午十、青岛市各海水浴场海浪、水温预报
            WriteLog.WriteDebug("PM-17");
            //edit by Yuy in 180712
            sb_str.Append(gettabe18(date));//下午十一、小麦岛72小时潮汐预报 其中金沙滩取消不用，从下午三青岛三天预报取数
            WriteLog.WriteDebug("PM-18");
            sb_str.Append(gettabe19(date)); //下午十二 青岛周边海域24小时海浪、水温预报
            WriteLog.WriteDebug("PM-19");
            sb_str.Append(gettabe09(date, searchType));  //下午十三、黄河南海堤附近海域72小时风、浪预报
            WriteLog.WriteDebug("PM-09");
            sb_str.Append(gettabe21(date, searchType)); //下午十四威海电视台未来24小时预报
            WriteLog.WriteDebug("PM-21");
            //edit by Yuy in 180712
            sb_str.Append(gettabe22(date, searchType));//下午十五、威海48小时潮汐预报   其中威海两天预报取消不用，从下午三威海中取数
            WriteLog.WriteDebug("PM-22");
            sb_str.Append(gettabe23(date));
            WriteLog.WriteDebug("PM-23");
            //sb_str.Append(gettable26(date, searchType));
            //sb_str.Append(gettable26_2(date, searchType));
            //sb_str.Append(gettable35(date, searchType));
            sb_str.Append(gettable41(date, searchType)); //下午十六东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报
            WriteLog.WriteDebug("PM-41");
            sb_str.Append(gettable42(date, searchType));//下午十七、 东营埕岛-未来三天高/低潮预报
            WriteLog.WriteDebug("PM-42");
            sb_str.Append(gettable43(date, searchType));
            WriteLog.WriteDebug("PM-43");
            sb_str.Append(gettabe46(date));//下午三、山东省近海七市72小时潮汐预报
            WriteLog.WriteDebug("PM-46");

            sb_str.Append(gettabe47(date));//下午十八、海洋牧场-海浪预报
            WriteLog.WriteDebug("PM-47");
            sb_str.Append(gettabe48(date));//下午十九、海洋牧场-潮汐预报
            WriteLog.WriteDebug("PM-48");
            //sb_str.Append(gettabe48(date,1));//下午十九、海洋牧场-潮汐预报
            //WriteLog.WriteDebug("PM-48");
            sb_str.Append(gettabe49(date));//下午二十、海洋牧场-海温预报
            WriteLog.WriteDebug("PM-49");

            sb_str.Append(gettable53(date, searchType));
            WriteLog.WriteDebug("PM-53");

            sb_str.Append(gettable54(date, searchType));//下午二十一、东营广利渔港-未来三天高/低潮预报
            WriteLog.WriteDebug("PM-54");
            sb_str.Append(gettable55(date, searchType)); //下午二十二、东营广利渔港 - 未来三天的海面风及海浪有效波高预报（20时起报）
            WriteLog.WriteDebug("PM-55");
            sb_str.Append(gettable56(date));//下午二十三、东营广利渔港-未来三天的海面水温预报
            WriteLog.WriteDebug("PM-56");
            sb_str.Append(gettable57(date, searchType));//下午二十四、日照桃花岛-未来三天高/低潮预报
            WriteLog.WriteDebug("PM-57");
            sb_str.Append(gettable58(date, searchType));//下午二十五、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
            WriteLog.WriteDebug("PM-58");
            sb_str.Append(gettable59(date));//下午二十六、日照桃花岛-未来三天的海面水温预报
            WriteLog.WriteDebug("PM-59");
            sb_str.Append(gettable60(date, searchType));//下午二十七、潍坊度假区-未来三天高/低潮预报
            WriteLog.WriteDebug("PM-60");
            sb_str.Append(gettable61(date, searchType));// 下午二十八、潍坊度假区 - 未来三天的海面风及海浪有效波高预报（20时起报）
            WriteLog.WriteDebug("PM-61");
            sb_str.Append(gettable62(date));//下午二十九、潍坊度假区-未来三天的海面水温预报
            WriteLog.WriteDebug("PM-62");
            sb_str.Append(gettable63(date, searchType));//下午三十、威海新区-未来三天高/低潮预报
            WriteLog.WriteDebug("PM-63");
            sb_str.Append(gettable64(date, searchType)); //下午三十一、威海新区 - 未来三天的海面风及海浪有效波高预报（20时起报）
            WriteLog.WriteDebug("PM-64");

            sb_str.Append(gettable65(date));//下午三十二、威海新区-未来三天的海面水温预报
            WriteLog.WriteDebug("PM-65");

            sb_str.Append(gettable66(date, searchType));//下午三十三、烟台清泉-未来三天高/低潮预报
            WriteLog.WriteDebug("PM-66");
            sb_str.Append(gettable67(date, searchType));//下午三十四、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
            WriteLog.WriteDebug("PM-67");
            sb_str.Append(gettable68(date));//下午三十五、烟台清泉-未来三天的海面水温预报
            WriteLog.WriteDebug("PM-68");
            sb_str.Append(gettable69(date, searchType));//下午三十六、董家口-未来三天高/低潮预报
            WriteLog.WriteDebug("PM-69");
            sb_str.Append(gettable70(date, searchType));//下午三十七、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
            WriteLog.WriteDebug("PM-70");
            sb_str.Append(gettable71(date));//下午三十八、董家口-未来三天的海面水温预报
            WriteLog.WriteDebug("PM-71");
            sb_str.Append(gettable72(date, searchType));//下午四三十九、东营渔港-未来三天高/低潮预报
            WriteLog.WriteDebug("PM-72");
            sb_str.Append(gettable73(date, searchType));//下午四十、东营渔港-未来三天的海面风及海浪有效波高预报（20时起报）
            WriteLog.WriteDebug("PM-73");
            sb_str.Append(gettable74(date));//下午四十一、东营渔港-未来三天的海面水温预报
            WriteLog.WriteDebug("PM-74");
            //潮汐大换位
            sb_str.Append(gettable75(date));//潮汐换位从下午三取数据的多个地区潮时显示
            WriteLog.WriteDebug("PM-75");
            sb_str.Append(gettable76(date));//潮汐换位从下午三取数据的多个地区潮高显示
            WriteLog.WriteDebug("PM-76");
            sb_str.Append(gettable82(date, searchType));//潮汐换位从下午16取数据
            WriteLog.WriteDebug("PM-82");


            context.Response.Write(sb_str.Replace("[,{", "[{").ToString() + "]");
        }

        /// <summary>
        /// 根据时间查询数据(下午)
        /// </summary>
        /// <param name="context"></param>
        public void getbydataPMbak(HttpContext context)
        {

            var date = DateTime.Parse(context.Request["date"].ToString());
            var searchType = context.Request["searchtype"].ToString();//searchtype 按填报日期还是预报日期查询 p:填报日期 f:预报日期
            StringBuilder sb_str = new StringBuilder();
            sb_str.Append("[");
            sb_str.Append(gettabe05(date));//下午一、各海区24小时海浪预报
            sb_str.Append(gettabe06(date)); //下午二、山东省近海七市3天海浪、水温预报
            sb_str.Append(gettabe07(date));//下午三、山东省近海七市72小时潮汐预报
            //edit by Yuy in 180712
            //sb_str.Append(gettabe08(date)); //下午、青岛24小时潮位预报 潮汐取消不用去下午三青岛48小时数据
            //sb_str.Append(gettabe20(date)); //下午、青岛沿岸48小时潮汐预报 取消不用去下午三青岛48小时数据
            sb_str.Append(gettabe10(date, searchType));//下午四、明泽闸潮位预报
            sb_str.Append(gettabe11(date, searchType)); //下午五、南堡油田海域波浪、风、水温预报
            sb_str.Append(gettabe12(date, searchType));//下午六、南堡油田海域潮汐预报
            sb_str.Append(gettabe13(date, searchType));//下午七、海区24小时海浪、水温预报
            sb_str.Append(gettabe14(date, searchType));//下午八、海区48小时海浪预报
            sb_str.Append(gettabe15(date));//下午九、海区72小时海浪预报
            //edit by Yuy in 180712
            //sb_str.Append(gettabe16(date));//下午、潍坊港24小时潮汐预报  取消不用去下午三潍坊48小时数据
            sb_str.Append(gettabe17(date)); //下午十、青岛市各海水浴场海浪、水温预报
            //edit by Yuy in 180712
            sb_str.Append(gettabe18(date));//下午十一、小麦岛72小时潮汐预报 其中金沙滩取消不用，从下午三青岛三天预报取数
            sb_str.Append(gettabe19(date)); //下午十二 青岛周边海域24小时海浪、水温预报
            sb_str.Append(gettabe09(date, searchType));  //下午十三、黄河南海堤附近海域72小时风、浪预报
            sb_str.Append(gettabe21(date, searchType)); //下午十四威海电视台未来24小时预报
            //edit by Yuy in 180712
            sb_str.Append(gettabe22(date, searchType));//下午十五、威海48小时潮汐预报   其中威海两天预报取消不用，从下午三威海中取数
            sb_str.Append(gettabe23(date));
            //sb_str.Append(gettable26(date, searchType));
            //sb_str.Append(gettable26_2(date, searchType));
            //sb_str.Append(gettable35(date, searchType));
            sb_str.Append(gettable41(date, searchType)); //下午十六东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报
            sb_str.Append(gettable42(date, searchType));//下午十七、 东营埕岛-未来三天高/低潮预报
            sb_str.Append(gettable43(date, searchType));
            sb_str.Append(gettabe46(date));//下午三、山东省近海七市72小时潮汐预报

            sb_str.Append(gettabe47(date));//下午十八、海洋牧场-海浪预报
            sb_str.Append(gettabe48(date));//下午十九、海洋牧场-潮汐预报
            //sb_str.Append(gettabe48(date,1));//下午十九、海洋牧场-潮汐预报
            sb_str.Append(gettabe49(date));//下午二十、海洋牧场-海温预报

            sb_str.Append(gettable53(date, searchType));

            sb_str.Append(gettable54(date, searchType));//下午二十一、东营广利渔港-未来三天高/低潮预报
            sb_str.Append(gettable55(date, searchType)); //下午二十二、东营广利渔港 - 未来三天的海面风及海浪有效波高预报（20时起报）
            sb_str.Append(gettable56(date));//下午二十三、东营广利渔港-未来三天的海面水温预报
            sb_str.Append(gettable57(date, searchType));//下午二十四、日照桃花岛-未来三天高/低潮预报
            sb_str.Append(gettable58(date, searchType));//下午二十五、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
            sb_str.Append(gettable59(date));//下午二十六、日照桃花岛-未来三天的海面水温预报
            sb_str.Append(gettable60(date, searchType));//下午二十七、潍坊度假区-未来三天高/低潮预报
            sb_str.Append(gettable61(date, searchType));// 下午二十八、潍坊度假区 - 未来三天的海面风及海浪有效波高预报（20时起报）
            sb_str.Append(gettable62(date));//下午二十九、潍坊度假区-未来三天的海面水温预报
            sb_str.Append(gettable63(date, searchType));//下午三十、威海新区-未来三天高/低潮预报
            sb_str.Append(gettable64(date, searchType)); //下午三十一、威海新区 - 未来三天的海面风及海浪有效波高预报（20时起报）

            sb_str.Append(gettable65(date));//下午三十二、威海新区-未来三天的海面水温预报

            sb_str.Append(gettable66(date, searchType));//下午三十三、烟台清泉-未来三天高/低潮预报
            sb_str.Append(gettable67(date, searchType));//下午三十四、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
            sb_str.Append(gettable68(date));//下午三十五、烟台清泉-未来三天的海面水温预报
            sb_str.Append(gettable69(date, searchType));//下午三十六、董家口-未来三天高/低潮预报
            sb_str.Append(gettable70(date, searchType));//下午三十七、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
            sb_str.Append(gettable71(date));//下午三十八、董家口-未来三天的海面水温预报
            sb_str.Append(gettable72(date, searchType));//下午四三十九、东营渔港-未来三天高/低潮预报
            sb_str.Append(gettable73(date, searchType));//下午四十、东营渔港-未来三天的海面风及海浪有效波高预报（20时起报）
            sb_str.Append(gettable74(date));//下午四十一、东营渔港-未来三天的海面水温预报
            context.Response.Write(sb_str.Replace("[,{", "[{").ToString() + "]");
        }

        /// <summary>
        /// 表单01数据 /上午一、72小时渤海海区，黄河海港风、浪预报以及黄河海港水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
		public string gettabe01(DateTime data, string searchType)
        {
            
            sql_TBLYRBHWINDWAVE72HFORECASTTWO sql_01 = new sql_TBLYRBHWINDWAVE72HFORECASTTWO();
            TBLYRBHWINDWAVE72HFORECASTTWO model_01 = new TBLYRBHWINDWAVE72HFORECASTTWO();
            searchType = "f";
            model_01.PUBLISHDATE = data;
            model_01.FORECASTDATE = data;

            DataTable dt_01 = (DataTable)sql_01.TBLYRBHWINDWAVE72HFORECASTTWOAllData(model_01);  //获取填报后数据
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            DataTable dt = null;
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "渤海", "黄河海港" };
            //*********************判断当天数据是否存在**************************
            //存在
           
            if (dt_01 != null && dt_01.Rows.Count > 0)
            {
                int dbSWRowsNULLCount = 0;
                int dbFLRowsNullCout = 0;

                sb_str.Append("{ \"type\":\"t1\",\"pbtype\":\"bydb\",\"children\":[");
                //判断风浪、水温
                   for (int sw = 0; sw < dt_01.Rows.Count; sw++)
                {
                    //判断水温是否填报
                    if (Convert.ToString(dt_01.Rows[sw]["YRBHWWFWATERTEMPERATURE"]).Trim() == "" || DBNull.Value == dt_01.Rows[sw]["YRBHWWFWATERTEMPERATURE"])
                    {
                        dbSWRowsNULLCount++;
                    }
                    //判断风浪是否填报
                    if (((dt_01.Rows[sw]["YRBHWWFWAVEHEIGHT"]).ToString().Trim() == "" || DBNull.Value == dt_01.Rows[sw]["YRBHWWFWAVEHEIGHT"]) && ((dt_01.Rows[sw]["YRBHWWFWAVEDIR"]).ToString().Trim() == "" || DBNull.Value == dt_01.Rows[sw]["YRBHWWFWAVEDIR"]) && ((dt_01.Rows[sw]["YRBHWWFFLOWDIR"]).ToString().Trim() == "" || DBNull.Value == dt_01.Rows[sw]["YRBHWWFFLOWDIR"]) && ((dt_01.Rows[sw]["YRBHWWFFLOWLEVEL"]).ToString().Trim() == "" || DBNull.Value == dt_01.Rows[sw]["YRBHWWFFLOWLEVEL"]))
                    {
                        dbFLRowsNullCout++;
                    }
                }
 
                //填报后没有水温
                if (dbSWRowsNULLCount == 6&&dbFLRowsNullCout!=6)  
                {
                    string[] swarea = { "黄河海港" };
                    dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, swarea);
                    if(dt != null && dt.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt_01.Rows.Count; k++)
                        {
                            sb_str.Append("{ \"qy\":\"" + dt_01.Rows[k]["REPORTAREA"]
                               + "\",\"yb\":\"" + dt_01.Rows[k]["FORECASTDATE"]
                               + "\",\"bg\":\"" + dt_01.Rows[k]["YRBHWWFWAVEHEIGHT"]
                               + "\",\"bx\":\"" + dt_01.Rows[k]["YRBHWWFWAVEDIR"]
                               + "\",\"fx\":\"" + dt_01.Rows[k]["YRBHWWFFLOWDIR"]
                               + "\",\"fl\":\"" + dt_01.Rows[k]["YRBHWWFFLOWLEVEL"]);

                            string swStr = "";
                            if (dt_01.Rows[k]["REPORTAREA"].ToString() == "黄河海港")
                            {
                                DateTime forecastDate = Convert.ToDateTime(dt_01.Rows[k]["FORECASTDATE"]);
                                swStr = (forecastDate == data.AddDays(1)) ? dt.Rows[0]["MEAN_24H"].ToString() : (forecastDate == data.AddDays(2)) ? dt.Rows[0]["MEAN_48H"].ToString() : (forecastDate == data.AddDays(3)) ? dt.Rows[0]["MEAN_72H"].ToString() : "";
                                sb_str.Append("\",\"sw\":\"" + swStr);
                            }
                            else
                            {
                                sb_str.Append("\",\"sw\":\"" + dt_01.Rows[k]["YRBHWWFWATERTEMPERATURE"]);
                            }

                            sb_str.Append("\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }
                //填报后没有风浪数据，只有水温  Edit by yy in 20180809
                 if (dbSWRowsNULLCount != 6 && dbFLRowsNullCout == 6)
                {
                    DataTable dtWaveWind_s = new DataTable();
                    dtWaveWind_s = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");//获取源数据
                    if (dtWaveWind_s != null && dtWaveWind_s.Rows.Count > 0)
                    {

                        for (int k = 0; k < dt_01.Rows.Count; k++)
                        {
                            // string swStr = "";
                            if (dt_01.Rows[k]["REPORTAREA"].ToString() == "黄河海港")
                            {
                                sb_str.Append(
                                    "{ \"qy\":\"" + dt_01.Rows[k]["REPORTAREA"] //预报区域
                                    + "\",\"yb\":\"" + dt_01.Rows[k]["FORECASTDATE"] //预报日期
                                    + "\",\"sw\":\"" + dt_01.Rows[k]["YRBHWWFWATERTEMPERATURE"]//水温
                                    );
                                for (int i = 0; i < dtWaveWind_s.Rows.Count; i++)
                                {
                                    if (dtWaveWind_s.Rows[i]["FORECASTAREA"].ToString() == "黄河海港")
                                    {
                                        //string FLStr = string.Empty;
                                        int TimeSpan = ((DateTime)dt_01.Rows[k]["FORECASTDATE"] - (DateTime)dtWaveWind_s.Rows[i]["PUBLISHDATE"]).Days;
                                        int j = TimeSpan;
                                        sb_str.Append(
                                        //"{ \"qy\":\"" + dtWaveWind_s.Rows[i]["FORECASTAREA"]
                                        // "\",\"yb\":\"" + (((DateTime)dtWaveWind_s.Rows[i]["PUBLISHDATE"]).AddDays(j)).ToString()
                                        "\",\"bg\":\"" + dtWaveWind_s.Rows[i]["WAVE" + 24 * j + "FORECAST"]
                                       + "\",\"bx\":\"" + dtWaveWind_s.Rows[i]["WINDDIRECTION" + 24 * j + "FORECAST"] //波向取风向
                                       + "\",\"fx\":\"" + dtWaveWind_s.Rows[i]["WINDDIRECTION" + 24 * j + "FORECAST"]
                                       + "\",\"fl\":\"" + dtWaveWind_s.Rows[i]["WINDFORCE" + 24 * j + "FORECAST"]
                                       );

                                    }
                                }
                            }
                            else if (dt_01.Rows[k]["REPORTAREA"].ToString() == "渤海")
                            {
                                sb_str.Append(
                                    "{ \"qy\":\"" + dt_01.Rows[k]["REPORTAREA"] //预报区域
                                    + "\",\"yb\":\"" + dt_01.Rows[k]["FORECASTDATE"] //预报日期
                                    //+ "\",\"sw\":\"" + dt_01.Rows[k]["YRBHWWFWATERTEMPERATURE"]//水温
                                    );
                                for (int i = 0; i < dtWaveWind_s.Rows.Count; i++)
                                {
                                    if (dtWaveWind_s.Rows[i]["FORECASTAREA"].ToString() == "渤海")
                                    {
                                        TimeSpan s = (DateTime)dt_01.Rows[k]["FORECASTDATE"] - (DateTime)dtWaveWind_s.Rows[i]["PUBLISHDATE"];
                                        int j = s.Days;
                                        sb_str.Append(
                                           //"{ \"qy\":\"" + dtWaveWind_s.Rows[i]["FORECASTAREA"]
                                           // "\",\"yb\":\"" + (((DateTime)dtWaveWind_s.Rows[i]["PUBLISHDATE"]).AddDays(j)).ToString()
                                            "\",\"bg\":\"" + dtWaveWind_s.Rows[i]["WAVE" + 24 * j + "FORECAST"]
                                           + "\",\"bx\":\"" + dtWaveWind_s.Rows[i]["WINDDIRECTION" + 24 * j + "FORECAST"] //波向取风向
                                           + "\",\"fx\":\"" + dtWaveWind_s.Rows[i]["WINDDIRECTION" + 24 * j+ "FORECAST"]
                                           + "\",\"fl\":\"" + dtWaveWind_s.Rows[i]["WINDFORCE" + 24 * j+ "FORECAST"]
                                           );
                                        
                                    }
                                }
                            }
                            sb_str.Append("\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }
                //若都不为null
                for (int i = 0; i < dt_01.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt_01.Rows[i]["REPORTAREA"]
                        + "\",\"yb\":\"" + dt_01.Rows[i]["FORECASTDATE"]
                        + "\",\"bg\":\"" + dt_01.Rows[i]["YRBHWWFWAVEHEIGHT"]
                        + "\",\"bx\":\"" + dt_01.Rows[i]["YRBHWWFWAVEDIR"]
                        + "\",\"fx\":\"" + dt_01.Rows[i]["YRBHWWFFLOWDIR"]
                        + "\",\"fl\":\"" + dt_01.Rows[i]["YRBHWWFFLOWLEVEL"]
                        + "\",\"sw\":\"" + dt_01.Rows[i]["YRBHWWFWATERTEMPERATURE"] + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }

            //不存在
             //获取海浪、气象数据
            DataTable dtWaveWind = new DataTable();
            
            dtWaveWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA,"AM");

            //获取水温数据
            string[] area = { "黄河海港" };
            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);

            #region  取消拼接
            //获取气象数据
            //DataTableBuilder db = new DataTableBuilder();
            //DataTable dtNew = db.GetRtnDataTable();

            //dtNew.Columns.Add("YRBHWWFWATERTEMPERATURE24", typeof(string));
            //dtNew.Columns.Add("YRBHWWFWATERTEMPERATURE48", typeof(string));

            //if (dtWaveWind != null && dtWaveWind.Rows.Count > 0)
            //{
            //    for(int i =0;i< dtWaveWind.Rows.Count; i++)
            //    {
            //        DataRow rowNew = dtNew.NewRow();
            //        rowNew["PUBLISHDATE"] = dtWaveWind.Rows[i]["PUBLISHDATE"];
            //        rowNew["FORECASTAREA"] = dtWaveWind.Rows[i]["FORECASTAREA"].ToString();
            //        rowNew["WINDFORCE24FORECAST"] = dtWaveWind.Rows[i]["WINDFORCE24FORECAST"].ToString();
            //        rowNew["WINDFORCE48FORECAST"] = dtWaveWind.Rows[i]["WINDFORCE48FORECAST"].ToString();
            //        rowNew["WINDFORCE72FORECAST"] = dtWaveWind.Rows[i]["WINDFORCE72FORECAST"].ToString();
            //        rowNew["WINDDIRECTION24FORECAST"] = dtWaveWind.Rows[i]["WINDDIRECTION24FORECAST"].ToString();
            //        rowNew["WINDDIRECTION48FORECAST"] = dtWaveWind.Rows[i]["WINDDIRECTION48FORECAST"].ToString();
            //        rowNew["WINDDIRECTION72FORECAST"] = dtWaveWind.Rows[i]["WINDDIRECTION72FORECAST"].ToString();
            //        rowNew["WAVE24FORECAST"] = dtWaveWind.Rows[i]["WAVE24FORECAST"].ToString();
            //        rowNew["WAVE48FORECAST"] = dtWaveWind.Rows[i]["WAVE48FORECAST"].ToString();
            //        rowNew["WAVE72FORECAST"] = dtWaveWind.Rows[i]["WAVE72FORECAST"].ToString();
            //        rowNew["YRBHWWFWATERTEMPERATURE24"] = "";
            //        rowNew["YRBHWWFWATERTEMPERATURE48"] = "";
            //        dtNew.Rows.Add(rowNew);
            //    }
            //}

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    var YRBHWWFWATERTEMPERATURE24HH = "";
            //    var YRBHWWFWATERTEMPERATURE48HH = "";
            //    var YRBHWWFWATERTEMPERATURE24BH = "";
            //    var YRBHWWFWATERTEMPERATURE48BH = "";

            //    for (int j = 0; j < dt.Rows.Count; j++)
            //    {
            //        var area = dt.Rows[j]["REPORTAREA"].ToString();
            //        DateTime forecastDate = Convert.ToDateTime(dt.Rows[j]["FORECASTDATE"]);
            //        if (area == "黄河海港")
            //        {
            //            if(forecastDate == data)
            //            {
            //                YRBHWWFWATERTEMPERATURE24HH = dt.Rows[j]["YRBHWWFWATERTEMPERATURE"].ToString();
            //            }
            //            else if(forecastDate == data.AddDays(1))
            //            {
            //                YRBHWWFWATERTEMPERATURE48HH = dt.Rows[j]["YRBHWWFWATERTEMPERATURE"].ToString();
            //            }
            //        }
            //        else if (area == "渤海")
            //        {
            //            if (forecastDate == data)
            //            {
            //                YRBHWWFWATERTEMPERATURE24BH = dt.Rows[j]["YRBHWWFWATERTEMPERATURE"].ToString();
            //            }
            //            else if (forecastDate == data.AddDays(1))
            //            {
            //                YRBHWWFWATERTEMPERATURE48BH = dt.Rows[j]["YRBHWWFWATERTEMPERATURE"].ToString();
            //            }
            //        }

            //        if (dtNew != null && dtNew.Rows.Count > 0)
            //        {
            //            for (int m = 0; m < dtNew.Rows.Count; m++)
            //            {
            //                string area2 = dtNew.Rows[m]["FORECASTAREA"].ToString(); ;
            //                if(area2 == "黄河海港")
            //                {
            //                    dtNew.Rows[m]["YRBHWWFWATERTEMPERATURE24"] = YRBHWWFWATERTEMPERATURE24HH;
            //                    dtNew.Rows[m]["YRBHWWFWATERTEMPERATURE48"] = YRBHWWFWATERTEMPERATURE48HH;
            //                }
            //                else if (area2 == "渤海")
            //                {
            //                    dtNew.Rows[m]["YRBHWWFWATERTEMPERATURE24"] = YRBHWWFWATERTEMPERATURE24BH;
            //                    dtNew.Rows[m]["YRBHWWFWATERTEMPERATURE48"] = YRBHWWFWATERTEMPERATURE48BH;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            DataRow rowNew = dtNew.NewRow();
            //            rowNew["PUBLISHDATE"] = DateTime.Now;
            //            rowNew["FORECASTAREA"] = "黄河海港";
            //            rowNew["WINDFORCE24FORECAST"] = "";
            //            rowNew["WINDFORCE48FORECAST"] = "";
            //            rowNew["WINDFORCE72FORECAST"] = "";
            //            rowNew["WINDDIRECTION24FORECAST"] = "";
            //            rowNew["WINDDIRECTION48FORECAST"] = "";
            //            rowNew["WINDDIRECTION72FORECAST"] = "";
            //            rowNew["WAVE24FORECAST"] = "";
            //            rowNew["WAVE48FORECAST"] = "";
            //            rowNew["WAVE72FORECAST"] = "";
            //            rowNew["YRBHWWFWATERTEMPERATURE24"] = YRBHWWFWATERTEMPERATURE24HH;
            //            rowNew["YRBHWWFWATERTEMPERATURE48"] = YRBHWWFWATERTEMPERATURE48HH;
            //            dtNew.Rows.Add(rowNew);

            //            DataRow rowNew2 = dtNew.NewRow();
            //            rowNew2["PUBLISHDATE"] = DateTime.Now;
            //            rowNew2["FORECASTAREA"] = "渤海";
            //            rowNew2["WINDFORCE24FORECAST"] = "";
            //            rowNew2["WINDFORCE48FORECAST"] = "";
            //            rowNew2["WINDFORCE72FORECAST"] = "";
            //            rowNew2["WINDDIRECTION24FORECAST"] = "";
            //            rowNew2["WINDDIRECTION48FORECAST"] = "";
            //            rowNew2["WINDDIRECTION72FORECAST"] = "";
            //            rowNew2["WAVE24FORECAST"] = "";
            //            rowNew2["WAVE48FORECAST"] = "";
            //            rowNew2["WAVE72FORECAST"] = "";
            //            rowNew2["YRBHWWFWATERTEMPERATURE24"] = YRBHWWFWATERTEMPERATURE24BH;
            //            rowNew2["YRBHWWFWATERTEMPERATURE48"] = YRBHWWFWATERTEMPERATURE48BH;
            //            dtNew.Rows.Add(rowNew2);
            //        }
            //    }

            //}

            #endregion

            var result = "\"windwave\":"; //海浪、气象
            if(dtWaveWind != null && dtWaveWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWaveWind);
            }
            else
            {
                result += "[{}]";
            }
            result += ",\"sw\":";//水温
            if(dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }

            sb_str.Append(",{ \"type\":\"t1\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");

            return sb_str.ToString();
        }

        /// <summary>
        /// 表单02数据 / 上午二、72小时港口潮位预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe02(DateTime data, string searchType)
        {
            int week = (int)data.DayOfWeek;
            if (week == 2)
            {
                return gettabe02_week(data, searchType);//add by xp 2018-9-17周二的上午二
            }
            sql_TBLHARBOURTIDELEVEL sql = new sql_TBLHARBOURTIDELEVEL();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = (DataTable)sql.get_TBLHARBOURTIDELEVEL_AllData(model);
            if (dt.Rows.Count < 1)
            {
                dt = (DataTable)sql.TBLHARBOURTIDELEVELAllData(model);
            }
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t2\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["HTLHARBOUR"]
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"g1c\":\"" + dt.Rows[i]["HTLFIRSTWAVETIDELEVEL"]
                        + "\",\"g1s\":\"" + dt.Rows[i]["HTLFIRSTWAVEOFTIME"]
                        + "\",\"d1c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"]
                        + "\",\"d1s\":\"" + dt.Rows[i]["HTLFIRSTTIMELOWTIDE"]
                        + "\",\"g2c\":\"" + dt.Rows[i]["HTLSECONDWAVETIDELEVEL"]
                        + "\",\"g2s\":\"" + dt.Rows[i]["HTLSECONDWAVEOFTIME"]
                        + "\",\"d2c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"]
                        + "\",\"d2s\":\"" + dt.Rows[i]["HTLSECONDTIMELOWTIDE"] + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 表单02数据 / 上午二、72小时港口潮位预报 modify by xp 周二的上午二继承周一“周报八”的数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe02_week(DateTime data, string searchType)
        {
            sql_TBLHARBOURTIDELEVEL7DAY sql = new sql_TBLHARBOURTIDELEVEL7DAY();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            
            DateTime weekPublishTime = data.AddDays(-1);
            model.PUBLISHDATE = weekPublishTime;//周一
            DataTable dt = (DataTable)sql.GETTBLHARBOURTIDELEVEL7DAY_Week(model);
            StringBuilder sb_str = new StringBuilder();
            
            if (dt!=null &&dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t2\",\"children\":[");
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["HTLHARBOUR"]
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"g1c\":\"" + dt.Rows[i]["HTLFIRSTWAVETIDELEVEL"]
                        + "\",\"g1s\":\"" + dt.Rows[i]["HTLFIRSTWAVEOFTIME"]
                        + "\",\"d1c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"]
                        + "\",\"d1s\":\"" + dt.Rows[i]["HTLFIRSTTIMELOWTIDE"]
                        + "\",\"g2c\":\"" + dt.Rows[i]["HTLSECONDWAVETIDELEVEL"]
                        + "\",\"g2s\":\"" + dt.Rows[i]["HTLSECONDWAVEOFTIME"]
                        + "\",\"d2c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"]
                        + "\",\"d2s\":\"" + dt.Rows[i]["HTLSECONDTIMELOWTIDE"] + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 表单03数据 上午五
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe03(DateTime data)
        {
            sql_TBLEXPECTEDFUTURE24HWAVEWATER sql = new sql_TBLEXPECTEDFUTURE24HWAVEWATER();
            TBLEXPECTEDFUTURE24HWAVEWATER model = new TBLEXPECTEDFUTURE24HWAVEWATER();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLEXPECTEDFUTURE24HWAVEWATER_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();

            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "渤海", "黄海北部", "刁口近海", "黄河口海域", "广利港海域", "东营港海域", "新户近海", "埕岛近海" };

            #region 有填报数据
            if (dt != null && dt.Rows.Count > 0)
            {
                int dbSWRowsNULLCount = 0;//水温
                int dbFLRowsNULLCount = 0;//风浪
                sb_str.Append(",{ \"type\":\"t3\",\"pbtype\":\"bydb\",\"children\":[");
                
                //判断风浪水温是否填写数据
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    //判断海温数据是否为null
                    if (DBNull.Value == dt.Rows[sw]["EFWWDKSEAAREAWATERTEMPE"] && DBNull.Value == dt.Rows[sw]["EFWWHHKSEAAREAWATERTEMP"] && DBNull.Value == dt.Rows[sw]["EFWWGLGSEAAREAWATERTEMP"] && DBNull.Value == dt.Rows[sw]["EFWWDYGWATERTEMPERATURE"] && DBNull.Value == dt.Rows[sw]["EFWWXHWATERTEMPERATURE"] && DBNull.Value == dt.Rows[sw]["EFWWCKWATERTEMPERATURE"])
                    {
                        dbSWRowsNULLCount++;
                    }
                    //判断风浪数据视为为null
                    if (DBNull.Value ==dt.Rows[sw]["EFWWBHLOWESTWAVE"] && DBNull.Value == dt.Rows[sw]["EFWWBHHIGHESTWAVE"] && DBNull.Value == dt.Rows[sw]["EFWWBHWAVETYPE"] && DBNull.Value == dt.Rows[sw]["EFWWBHNORTHLOWESTWAVE"] && DBNull.Value == dt.Rows[sw]["EFWWBHNORTHHIGHESTWAVE"] && DBNull.Value == dt.Rows[sw]["EFWWBHNORTHWAVETYPE"] && DBNull.Value == dt.Rows[sw]["EFWWDKSEAAREAWAVEHEIGHT"] && DBNull.Value == dt.Rows[sw]["EFWWHHKSEAAREAWAVEHEIGHT"] && DBNull.Value == dt.Rows[sw]["EFWWGLGSEAAREAWAVEHEIGHT"] && DBNull.Value == dt.Rows[sw]["EFWWDYGWAVEHEIGHT"] && DBNull.Value == dt.Rows[sw]["EFWWXHWAVEHEIGHT"] && DBNull.Value == dt.Rows[sw]["EFWWCKWAVEHEIGHT"] )
                    {
                        dbFLRowsNULLCount++;
                    }
                }
                //无水温数据但是有风浪数据
                if (dbSWRowsNULLCount > 0&&dbFLRowsNULLCount==0)
                {
                    string[] areasw = { "刁口", "黄河口", "广利（羊口）", "东营", "新户", "埕口海洋站" };

                    DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areasw);

                    if (dtsw != null && dtsw.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            sb_str.Append("{\"bdl\":\"" + dt.Rows[j]["EFWWBHLOWESTWAVE"]
                                + "\",\"bgl\":\"" + dt.Rows[j]["EFWWBHHIGHESTWAVE"]
                                + "\",\"blx\":\"" + dt.Rows[j]["EFWWBHWAVETYPE"]
                                + "\",\"hdl\":\"" + dt.Rows[j]["EFWWBHNORTHLOWESTWAVE"]
                                + "\",\"hgl\":\"" + dt.Rows[j]["EFWWBHNORTHHIGHESTWAVE"]
                                + "\",\"hlx\":\"" + dt.Rows[j]["EFWWBHNORTHWAVETYPE"]

                                + "\",\"dl\":\"" + dt.Rows[j]["EFWWDKSEAAREAWAVEHEIGHT"]
                                + "\",\"hl\":\"" + dt.Rows[j]["EFWWHHKSEAAREAWAVEHEIGHT"]
                                + "\",\"gl\":\"" + dt.Rows[j]["EFWWGLGSEAAREAWAVEHEIGHT"]
                                + "\",\"dyl\":\"" + dt.Rows[j]["EFWWDYGWAVEHEIGHT"]
                                + "\",\"xl\":\"" + dt.Rows[j]["EFWWXHWAVEHEIGHT"]
                                + "\",\"cl\":\"" + dt.Rows[j]["EFWWCKWAVEHEIGHT"]);
                            for (int k = 0; k < dtsw.Rows.Count; k++)
                            {
                                string forecastArea = dtsw.Rows[k]["NAME"].ToString();
                                switch (forecastArea)
                                {
                                    case "刁口":
                                        sb_str.Append("\",\"ds\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "黄河口":
                                        sb_str.Append("\",\"hs\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "广利（羊口）":
                                        sb_str.Append("\",\"gs\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "东营":
                                        sb_str.Append("\",\"dys\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "新户":
                                        sb_str.Append("\",\"xs\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "埕口海洋站":
                                        sb_str.Append("\",\"cs\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            sb_str.Append("\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }
                //有水温数据但是没有风浪数据
                if (dbFLRowsNULLCount > 0 && dbSWRowsNULLCount == 0)
                {
                    DataTable flData_s = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");
                    sb_str.Append("{\"zhanwei\":\"占位");
                    if (flData_s != null && flData_s.Rows.Count > 0)
                    {
                        for (int i = 0; i < flData_s.Rows.Count; i++)
                        {
                            string forecastArea = flData_s.Rows[i]["FORECASTAREA"].ToString();
                            switch (forecastArea)
                            {
                                case "渤海":
                                    sb_str.Append("\",\"bgl\":\"" + flData_s.Rows[i]["WAVE24FORECAST"]);
                                    break;
                                case "黄海北部":
                                    sb_str.Append("\",\"hgl\":\"" + flData_s.Rows[i]["WAVE24FORECAST"]);
                                    break;
                                case "刁口近海":
                                    sb_str.Append("\",\"dl\":\"" + flData_s.Rows[i]["WAVE24FORECAST"]
                                        + "\",\"ds\":\"" + dt.Rows[0]["EFWWDKSEAAREAWATERTEMPE"]);
                                    break;
                                case "黄河口海域":
                                    sb_str.Append("\",\"hl\":\"" + flData_s.Rows[i]["WAVE24FORECAST"]
                                        + "\",\"hs\":\"" + dt.Rows[0]["EFWWHHKSEAAREAWATERTEMP"]);
                                    break;
                                case "广利港海域":
                                    sb_str.Append("\",\"gl\":\"" + flData_s.Rows[i]["WAVE24FORECAST"]
                                        + "\",\"gs\":\"" + dt.Rows[0]["EFWWGLGSEAAREAWATERTEMP"]);
                                    break;
                                case "东营港海域":
                                    sb_str.Append("\",\"dyl\":\"" + flData_s.Rows[i]["WAVE24FORECAST"]
                                        + "\",\"dys\":\"" + dt.Rows[0]["EFWWDYGWATERTEMPERATURE"]);
                                    break;
                                case "新户近海":
                                    sb_str.Append("\",\"xl\":\"" + flData_s.Rows[i]["WAVE24FORECAST"]
                                        + "\",\"xs\":\"" + dt.Rows[0]["EFWWXHWATERTEMPERATURE"]);
                                    break;
                                case "埕岛近海":
                                    sb_str.Append("\",\"cl\":\"" + flData_s.Rows[i]["WAVE24FORECAST"]
                                        + "\",\"cs\":\"" + dt.Rows[0]["EFWWCKWATERTEMPERATURE"]);
                                    break;
                                default:
                                    break;
                            }
                        }
                        sb_str.Append("\"},");
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }

                }
                //有风浪、水温数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"bdl\":\"" + dt.Rows[i]["EFWWBHLOWESTWAVE"]
                        + "\",\"bgl\":\"" + dt.Rows[i]["EFWWBHHIGHESTWAVE"]
                        + "\",\"blx\":\"" + dt.Rows[i]["EFWWBHWAVETYPE"]
                        + "\",\"hdl\":\"" + dt.Rows[i]["EFWWBHNORTHLOWESTWAVE"]
                        + "\",\"hgl\":\"" + dt.Rows[i]["EFWWBHNORTHHIGHESTWAVE"]
                        + "\",\"hlx\":\"" + dt.Rows[i]["EFWWBHNORTHWAVETYPE"]
                        + "\",\"dl\":\"" + dt.Rows[i]["EFWWDKSEAAREAWAVEHEIGHT"]
                        + "\",\"ds\":\"" + dt.Rows[i]["EFWWDKSEAAREAWATERTEMPE"]
                        + "\",\"hl\":\"" + dt.Rows[i]["EFWWHHKSEAAREAWAVEHEIGHT"]
                        + "\",\"hs\":\"" + dt.Rows[i]["EFWWHHKSEAAREAWATERTEMP"]
                        + "\",\"gl\":\"" + dt.Rows[i]["EFWWGLGSEAAREAWAVEHEIGHT"]
                        + "\",\"gs\":\"" + dt.Rows[i]["EFWWGLGSEAAREAWATERTEMP"]
                        + "\",\"dyl\":\"" + dt.Rows[i]["EFWWDYGWAVEHEIGHT"]
                        + "\",\"dys\":\"" + dt.Rows[i]["EFWWDYGWATERTEMPERATURE"]
                        + "\",\"xl\":\"" + dt.Rows[i]["EFWWXHWAVEHEIGHT"]
                        + "\",\"xs\":\"" + dt.Rows[i]["EFWWXHWATERTEMPERATURE"]
                        + "\",\"cl\":\"" + dt.Rows[i]["EFWWCKWAVEHEIGHT"]
                        + "\",\"cs\":\"" + dt.Rows[i]["EFWWCKWATERTEMPERATURE"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            #endregion

            #region 没有填报数据 
            //获取海浪、气象数据
            DataTable dtWaveWind = new DataTable();
            dtWaveWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            //获取水温数据
            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLEXPECTEDFUTURE24HWAVEWATER_AllData(model);
            string[] area = { "刁口", "黄河口", "广利（羊口）", "东营", "新户", "埕口海洋站" };
            
            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);

            var result = "\"windwave\":"; //海浪、气象
            if (dtWaveWind != null && dtWaveWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWaveWind);
            }
            else
            {
                result += "[{}]";
            }
            result += ",\"sw\":";//水温
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
           
            sb_str.Append(",{ \"type\":\"t3\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
            #endregion
        }

        /// <summary>
        /// 表单04数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe04(DateTime data)
        {
            sql_TBL24HTIDELEVEL sql = new sql_TBL24HTIDELEVEL();
            TBL24HTIDELEVEL model = new TBL24HTIDELEVEL();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBL24HTIDELEVEL_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t4\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["TLFORECASTSTANCE"]
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"g1s\":\"" + dt.Rows[i]["TLFIRSTWAVEOFTIME"]
                        + "\",\"g1c\":\"" + dt.Rows[i]["TLFIRSTWAVETIDELEVEL"]
                        + "\",\"d1s\":\"" + dt.Rows[i]["TLFIRSTTIMELOWTIDE"]
                        + "\",\"d1c\":\"" + dt.Rows[i]["TLLOWTIDELEVELFORTHEFIRSTTIME"]
                        + "\",\"g2s\":\"" + dt.Rows[i]["TLSECONDWAVEOFTIME"]
                        + "\",\"g2c\":\"" + dt.Rows[i]["TLSECONDWAVETIDELEVEL"]
                        + "\",\"d2s\":\"" + dt.Rows[i]["TLSECONDTIMELOWTIDE"]
                        + "\",\"d2c\":\"" + dt.Rows[i]["TLLOWTIDELEVELFORTHESECONDTIME"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 表单05数据  下午一
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe05(DateTime data)
        {
            sql_TBLEACHSEAAREA24HSEAWAVE sql = new sql_TBLEACHSEAAREA24HSEAWAVE();
            TBLEACHSEAAREA24HSEAWAVE model = new TBLEACHSEAAREA24HSEAWAVE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLEACHSEAAREA24HSEAWAVE_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt!=null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t5\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "渤海", "黄海北部", "黄海中部", "黄海南部" };

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            result = "\"wave\":"; 
            if (dtWave!=null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            //获取上午填写数据
            //上午五
            sql_TBLEXPECTEDFUTURE24HWAVEWATER sqlM = new sql_TBLEXPECTEDFUTURE24HWAVEWATER();
            TBLEXPECTEDFUTURE24HWAVEWATER modelM = new TBLEXPECTEDFUTURE24HWAVEWATER();
            modelM.PUBLISHDATE = data;
            DataTable dt5 = (DataTable)sqlM.get_TBLEXPECTEDFUTURE24HWAVEWATER_AllData(modelM);
            result += ",\"dt5\":";//水温
            if (dt5 != null && dt5.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt5);
            }
            else
            {
                result += "[{}]";
            }
            //上午十
            Sql_HT_TBLWF24HWAVEFORECAST sql10 = new Sql_HT_TBLWF24HWAVEFORECAST();
            HT_TBLWF24HWAVEFORECAST model10 = new HT_TBLWF24HWAVEFORECAST();
            model10.PUBLISHDATE = data;
            DataTable dt10 = (DataTable)sql10.get_TBLWF24HWAVEFORECAST_AllData(model10);
            result += ",\"dt10\":";//水温
            if (dt10 != null && dt10.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt10);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t5\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单06数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe06(DateTime data)
        {
            sql_TBLSDOFFSHORESEVENCITY24HWAVE sql = new sql_TBLSDOFFSHORESEVENCITY24HWAVE();
            TBLSDOFFSHORESEVENCITY24HWAVE model = new TBLSDOFFSHORESEVENCITY24HWAVE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t6\",\"pbtype\":\"bydb\",\"children\":[");
                int dbSWRowsNULLCount = 0;
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if(DBNull.Value == dt.Rows[sw]["SDOSCWSURFACETEMPERATURE"] && DBNull.Value == dt.Rows[sw]["SDOSCWSURFACETEMPERATURE48H"] && DBNull.Value == dt.Rows[sw]["SDOSCWSURFACETEMPERATURE72H"]){
                        dbSWRowsNULLCount++;
                    }
                }
                if (dbSWRowsNULLCount > 0)
                {
                    string[] areas = { "日照", "青岛", "威海", "烟台", "潍坊", "东营", "滨州" };

                    DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areas);
                    if (dtsw != null && dtsw.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string names = dt.Rows[j]["SDOSCWAREA"].ToString();
                            for (int k = 0; k < dtsw.Rows.Count; k++)
                            {
                                string forecastArea = dtsw.Rows[k]["NAME"].ToString() + "近海";
                                if(names == forecastArea)
                                {
                                    sb_str.Append("{\"lg24\":\"" + dt.Rows[j]["SDOSCWLOWESTWAVEHEIGHT"]
                           + "\",\"qy\":\"" + dt.Rows[j]["SDOSCWAREA"]
                           + "\",\"gl\":\"" + dt.Rows[j]["SDOSCWHIGHTESTWAVEHEIGHT"]
                           + "\",\"sw24\":\"" + dtsw.Rows[k]["MEAN_24H"]
                           + "\",\"lg48\":\"" + dt.Rows[j]["SDOSCWESTWAVEHEIGHT48H"]
                           + "\",\"sw48\":\"" + dtsw.Rows[k]["MEAN_48H"]
                           + "\",\"lg72\":\"" + dt.Rows[j]["SDOSCWESTWAVEHEIGHT72H"]
                           + "\",\"sw72\":\"" + dtsw.Rows[k]["MEAN_72H"]
                           + "\"},");
                                }
                            }
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                    
                }
                int dbHLRowsNULLCount = 0;
                for (int hl = 0; hl < dt.Rows.Count; hl++)
                {
                    if (DBNull.Value == dt.Rows[hl]["SDOSCWLOWESTWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["SDOSCWESTWAVEHEIGHT48H"] && DBNull.Value == dt.Rows[hl]["SDOSCWESTWAVEHEIGHT72H"])
                    {
                        dbHLRowsNULLCount++;
                    }
                }

                if (dbHLRowsNULLCount > 0)
                {
                    string[] areas = { "日照近海", "青岛近海", "威海近海", "烟台近海", "潍坊近海", "东营近海", "滨州近海" };
                    DataTable dthl = (DataTable)getBy_s.GetWaveAndWindData(data, areas, "AM");
                    if (dthl != null && dthl.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            var forecast1 = dt.Rows[j]["SDOSCWAREA"].ToString();
                            for (int k = 0; k < dthl.Rows.Count; k++)
                            {
                                var forecast2 = dthl.Rows[k]["FORECASTAREA"].ToString();
                                if (forecast1 == forecast2) {
                                    sb_str.Append("{\"lg24\":\"" + dthl.Rows[k]["WAVE24FORECAST"]
                             + "\",\"qy\":\"" + dt.Rows[j]["SDOSCWAREA"]
                             + "\",\"gl\":\"" + dt.Rows[j]["SDOSCWHIGHTESTWAVEHEIGHT"]
                             + "\",\"sw24\":\"" + dt.Rows[j]["SDOSCWSURFACETEMPERATURE"]
                             + "\",\"lg48\":\"" + dthl.Rows[k]["WAVE48FORECAST"]
                             + "\",\"sw48\":\"" + dt.Rows[j]["SDOSCWSURFACETEMPERATURE48H"]
                             + "\",\"lg72\":\"" + dthl.Rows[k]["WAVE72FORECAST"]
                             + "\",\"sw72\":\"" + dt.Rows[j]["SDOSCWSURFACETEMPERATURE72H"] + "\"},");
                                }
                               
                            }
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"lg24\":\"" + dt.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"]
                        + "\",\"qy\":\"" + dt.Rows[i]["SDOSCWAREA"]
                        + "\",\"gl\":\"" + dt.Rows[i]["SDOSCWHIGHTESTWAVEHEIGHT"]
                        + "\",\"sw24\":\"" + dt.Rows[i]["SDOSCWSURFACETEMPERATURE"]
                        + "\",\"lg48\":\"" + dt.Rows[i]["SDOSCWESTWAVEHEIGHT48H"]
                        + "\",\"sw48\":\"" + dt.Rows[i]["SDOSCWSURFACETEMPERATURE48H"]
                        + "\",\"lg72\":\"" + dt.Rows[i]["SDOSCWESTWAVEHEIGHT72H"]
                        + "\",\"sw72\":\"" + dt.Rows[i]["SDOSCWSURFACETEMPERATURE72H"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }


            //拼接
            
            string[] FORECASTAREA = { "日照近海", "青岛近海", "威海近海", "烟台近海", "潍坊近海", "东营近海", "滨州近海" };

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(model);

            string[] area = { "日照", "青岛", "威海", "烟台", "潍坊", "东营" ,"滨州"};
            
            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);

            result += ",\"sw\":";
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
            //上午五
            sql_TBLEXPECTEDFUTURE24HWAVEWATER sqlM = new sql_TBLEXPECTEDFUTURE24HWAVEWATER();
            TBLEXPECTEDFUTURE24HWAVEWATER modelM = new TBLEXPECTEDFUTURE24HWAVEWATER();
            modelM.PUBLISHDATE = data;
            DataTable dt5 = (DataTable)sqlM.get_TBLEXPECTEDFUTURE24HWAVEWATER_AllData(modelM);
            result += ",\"dt5\":";//水温
            if (dt5 != null && dt5.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt5);
            }
            else
            {
                result += "[{}]";
            }
            //上午十
            Sql_HT_TBLWF24HWAVEFORECAST sql10 = new Sql_HT_TBLWF24HWAVEFORECAST();
            HT_TBLWF24HWAVEFORECAST model10 = new HT_TBLWF24HWAVEFORECAST();
            model10.PUBLISHDATE = data;
            DataTable dt10 = (DataTable)sql10.get_TBLWF24HWAVEFORECAST_AllData(model10);
            result += ",\"dt10\":";//水温
            if (dt10 != null && dt10.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt10);
            }
            else
            {
                result += "[{}]";
            }

            sb_str.Append(",{ \"type\":\"t6\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单07数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe07(DateTime data)
        {
            sql_TBLSDOFFSHORESEVENCITY24HTIDE sql = new sql_TBLSDOFFSHORESEVENCITY24HTIDE();
            TBLSDOFFSHORESEVENCITY24HTIDE model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(model);
            }
            
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t7\",\"children\":");
                var result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }

        /// <summary>
        /// 下午三潮汐潮高数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe46(DateTime data)
        {
            sql_TideData sql = new sql_TideData();
            HT_TideData model = new HT_TideData();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.getTideData(model);
            StringBuilder sb_str = new StringBuilder();
           
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.getTideData(model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t46\",\"children\":");
                var result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }

        /// <summary>
        /// 表单08数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe08(DateTime data)
        {
            sql_TBLQD24HTIDELEVEL sql = new sql_TBLQD24HTIDELEVEL();
            TBLQD24HTIDELEVEL model = new TBLQD24HTIDELEVEL();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLQD24HTIDELEVEL_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t8\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
            }
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单09数据下午九  黄河南海堤附近海域72小时风、浪预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe09(DateTime data, string searchType = "p")
        {
            sql_TBLYRSOUTHSEAWALL24WINDWAVE sql = new sql_TBLYRSOUTHSEAWALL24WINDWAVE();
            TBLYRSOUTHSEAWALL24WINDWAVE model = new TBLYRSOUTHSEAWALL24WINDWAVE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLYRSOUTHSEAWALL24WINDWAVE_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)     
            {
                
                sb_str.Append(",{ \"type\":\"t9\",\"pbtype\":\"bydb\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"bg\":\"" + dt.Rows[i]["YRSSWWWAVEHEIGHT"]
                        + "\",\"bx\":\"" + dt.Rows[i]["YRSSWWWAVEDIRECTION"]
                        + "\",\"fx\":\"" + dt.Rows[i]["YRSSWWWINDDIRECTION"]
                        + "\",\"fl\":\"" + dt.Rows[i]["YRSSWWWINDFORCE"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }

            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, "黄河南海堤", "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "黄河南海堤", "PM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t9\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();

        }

        /// <summary>
        /// 表单10数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe10(DateTime data, string searchType = "p")
        {
            sql_TBLMZZTIDELEVEL sql = new sql_TBLMZZTIDELEVEL();
            TBLMZZTIDELEVEL model = new TBLMZZTIDELEVEL();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.get_TBLMZZTIDELEVEL_AllData(model);
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.get_TBLMZZTIDELEVEL_AllData(model);
            }
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t10\",\"children\":[");
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
        /// 表单11数据 下午六
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe11(DateTime data, string searchType = "p")
        {
            sql_TBLNANPUWAVEFLOWWATERTFORECAST sql = new sql_TBLNANPUWAVEFLOWWATERTFORECAST();
            TBLNANPUWAVEFLOWWATERTFORECAST model = new TBLNANPUWAVEFLOWWATERTFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLNANPUWAVEFLOWWATERTFORECAST_AllData(model);
            var result = "";
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();//add
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t11\",\"pbtype\":\"bydb\",\"children\":");

                #region 无水温有风浪
                int dbSWRowsNULLCount = 0;
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if (DBNull.Value == dt.Rows[sw]["NWFWTFWATERTEMPERATURE"])
                    {
                        dbSWRowsNULLCount++;
                    }
                    if (dbSWRowsNULLCount == 0) continue;
                    if (dbSWRowsNULLCount > 0)
                    {
                        string[] areas = { "南堡油田" };

                        DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areas);
                        if (dtsw != null && dtsw.Rows.Count > 0)
                        {
                            sb_str.Append("[");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[j]["PUBLISHDATE"]
                                + "\",\"FORECASTDATE\":\"" + dt.Rows[j]["FORECASTDATE"]
                                + "\",\"NWFWTFWAVEHEIGHT\":\"" + dt.Rows[j]["NWFWTFWAVEHEIGHT"]
                                + "\",\"NWFWTFWAVEDIR\":\"" + dt.Rows[j]["NWFWTFWAVEDIR"]
                                + "\",\"NWFWTFFLOWDIR\":\"" + dt.Rows[j]["NWFWTFFLOWDIR"]
                                + "\",\"NWFWTFFLOWLEVEL\":\"" + dt.Rows[j]["NWFWTFFLOWLEVEL"]
                                + "\",\"NWFWTFWEATHER\":\"" + dt.Rows[j]["NWFWTFWEATHER"]);
                                for (int k = 0; k < dtsw.Rows.Count; k++)
                                {
                                    DateTime PUBLISHDATE = (DateTime)dt.Rows[j]["PUBLISHDATE"];
                                    DateTime FORECASTDATE = (DateTime)dt.Rows[j]["FORECASTDATE"];
                                    if (FORECASTDATE == PUBLISHDATE.AddDays(1))
                                    {
                                        sb_str.Append("\",\"NWFWTFWATERTEMPERATURE\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                    }
                                    else if (FORECASTDATE == PUBLISHDATE.AddDays(2))
                                    {
                                        sb_str.Append("\",\"NWFWTFWATERTEMPERATURE\":\"" + dtsw.Rows[k]["MEAN_48H"]);
                                    }
                                    else if (FORECASTDATE == PUBLISHDATE.AddDays(3))
                                    {
                                        sb_str.Append("\",\"NWFWTFWATERTEMPERATURE\":\"" + dtsw.Rows[k]["MEAN_72H"]);
                                    }
                                }

                                sb_str.Append("\"},");
                            }
                            sb_str.Remove(sb_str.Length - 1, 1).ToString();
                            sb_str.Append("]}");
                            return sb_str.ToString();
                        }
                        
                    }
                    //result = JsonMore.Serialize(dt);
                    //sb_str.Append(result);
                    //sb_str.Append("}");
                    //return sb_str.ToString();
                }
                #endregion

                #region 有水温无风浪
                
                int dbHLCount = 0;
                for (int hl = 0; hl < dt.Rows.Count; hl++)
                {
                    if (DBNull.Value == dt.Rows[hl]["NWFWTFWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["NWFWTFWAVEDIR"] && DBNull.Value == dt.Rows[hl]["NWFWTFFLOWDIR"] && DBNull.Value == dt.Rows[hl]["NWFWTFFLOWLEVEL"])
                    {
                        dbHLCount++;
                    }
                }
                if (dbHLCount == 3)//风浪无数据
                {
                    DataTable dthl = (DataTable)getBy_s.GetWaveAndWindData(data, "南堡油田", "AM");//浪
                    DataTable dthf = (DataTable)getBy_s.GetWaveAndWindData(data, "南堡油田", "PM");//风

                    sb_str.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime s = (DateTime)dt.Rows[i]["FORECASTDATE"];
                        //DateTime d = (DateTime)dt.Rows[i]["PUBLISHDATE"];

                        sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[0]["PUBLISHDATE"]
                            + "\",\"FORECASTDATE\":\"" + dt.Rows[i]["FORECASTDATE"]
                            + "\",\"NWFWTFWATERTEMPERATURE\":\"" + dt.Rows[i]["NWFWTFWATERTEMPERATURE"]
                            + "\",\"NWFWTFWEATHER\":\"" + dt.Rows[i]["NWFWTFWEATHER"]);

                        if (dthl != null && dthl.Rows.Count > 0 && dthf != null && dthf.Rows.Count > 0)
                        {
                            DateTime d = (DateTime)dthl.Rows[0]["PUBLISHDATE"];
                            //天气
                            if (s == d.AddDays(1))
                            {
                                sb_str.Append("\",\"NWFWTFWAVEHEIGHT\":\"" + dthl.Rows[0]["WAVE24FORECAST"]
                                    + "\",\"NWFWTFFLOWLEVEL\":\"" + dthf.Rows[0]["WINDFORCE24FORECAST"]
                                    + "\",\"NWFWTFFLOWDIR\":\"" + dthf.Rows[0]["WINDDIRECTION24FORECAST"]
                                     + "\",\"NWFWTFWAVEDIR\":\"" + dthl.Rows[0]["WINDDIRECTION24FORECAST"]);
                            }
                            if (s == d.AddDays(2))
                            {
                                sb_str.Append("\",\"NWFWTFWAVEHEIGHT\":\"" + dthl.Rows[0]["WAVE48FORECAST"]
                                   + "\",\"NWFWTFFLOWLEVEL\":\"" + dthf.Rows[0]["WINDFORCE48FORECAST"]
                                   + "\",\"NWFWTFFLOWDIR\":\"" + dthf.Rows[0]["WINDDIRECTION48FORECAST"]
                                     + "\",\"NWFWTFWAVEDIR\":\"" + dthl.Rows[0]["WINDDIRECTION48FORECAST"]);
                            }
                            if (s == d.AddDays(3))
                            {
                                sb_str.Append("\",\"NWFWTFWAVEHEIGHT\":\"" + dthl.Rows[0]["WAVE72FORECAST"]
                                    + "\",\"NWFWTFFLOWLEVEL\":\"" + dthf.Rows[0]["WINDFORCE72FORECAST"]
                                    + "\",\"NWFWTFFLOWDIR\":\"" + dthf.Rows[0]["WINDDIRECTION72FORECAST"]
                                     + "\",\"NWFWTFWAVEDIR\":\"" + dthl.Rows[0]["WINDDIRECTION72FORECAST"]);
                            }

                        }
                        sb_str.Append("\"},");
                    }
                    if (sb_str.ToString().EndsWith(","))
                    {
                        sb_str.Remove(sb_str.Length - 1, 1).ToString();
                    }

                    //sb_str.Remove(sb_str.Length - 1, 1).ToString();
                    sb_str.Append("]}");
                    return sb_str.ToString();
                }
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();

            }
            #endregion
                //Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            #region 原始数据
            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, "南堡油田", "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "南堡油田", "PM");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLNANPUWAVEFLOWWATERTFORECAST_AllData(model);

            string[] area = { "南堡油田" };

            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);


            result += ",\"sw\":";
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t11\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
            #endregion
        }

        //原来的
        /*public string gettabe11(DateTime data, string searchType = "p")
        {
            sql_TBLNANPUWAVEFLOWWATERTFORECAST sql = new sql_TBLNANPUWAVEFLOWWATERTFORECAST();
            TBLNANPUWAVEFLOWWATERTFORECAST model = new TBLNANPUWAVEFLOWWATERTFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLNANPUWAVEFLOWWATERTFORECAST_AllData(model);
            var result = "";
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t11\",\"pbtype\":\"bydb\",\"children\":");
                int dbSWRowsNULLCount = 0;
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if (DBNull.Value == dt.Rows[sw]["NWFWTFWATERTEMPERATURE"])
                    {
                        dbSWRowsNULLCount++;
                    }
                    if(dbSWRowsNULLCount > 0)
                    {
                        string[] areas = { "南堡油田" };

                        DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areas);
                        if (dtsw != null && dtsw.Rows.Count > 0)
                        {
                            sb_str.Append("[");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[j]["PUBLISHDATE"]
                                + "\",\"FORECASTDATE\":\"" + dt.Rows[j]["FORECASTDATE"]
                                + "\",\"NWFWTFWAVEHEIGHT\":\"" + dt.Rows[j]["NWFWTFWAVEHEIGHT"]
                                + "\",\"NWFWTFWAVEDIR\":\"" + dt.Rows[j]["NWFWTFWAVEDIR"]
                                + "\",\"NWFWTFFLOWDIR\":\"" + dt.Rows[j]["NWFWTFFLOWDIR"]
                                + "\",\"NWFWTFFLOWLEVEL\":\"" + dt.Rows[j]["NWFWTFFLOWLEVEL"]
                                + "\",\"NWFWTFWEATHER\":\"" + dt.Rows[j]["NWFWTFWEATHER"]);
                                for (int k = 0; k < dtsw.Rows.Count; k++)
                                {
                                    DateTime PUBLISHDATE = (DateTime)dt.Rows[j]["PUBLISHDATE"];
                                    DateTime FORECASTDATE = (DateTime)dt.Rows[j]["FORECASTDATE"];
                                    if (FORECASTDATE == PUBLISHDATE.AddDays(1))
                                    {
                                        sb_str.Append("\",\"NWFWTFWATERTEMPERATURE\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                    }
                                    else if (FORECASTDATE == PUBLISHDATE.AddDays(2))
                                    {
                                        sb_str.Append("\",\"NWFWTFWATERTEMPERATURE\":\"" + dtsw.Rows[k]["MEAN_48H"]);
                                    }
                                    else if (FORECASTDATE == PUBLISHDATE.AddDays(3))
                                    {
                                        sb_str.Append("\",\"NWFWTFWATERTEMPERATURE\":\"" + dtsw.Rows[k]["MEAN_72H"]);
                                    }
                                }
                                
                                sb_str.Append("\"},");
                            }
                            sb_str.Remove(sb_str.Length - 1, 1).ToString();
                            sb_str.Append("]}");
                            return sb_str.ToString();
                        }
                    }
                    result = JsonMore.Serialize(dt);
                    sb_str.Append(result);
                    sb_str.Append("}");
                    return sb_str.ToString();
                }
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, "南堡油田", "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "南堡油田", "PM");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            { 
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLNANPUWAVEFLOWWATERTFORECAST_AllData(model);

            string[] area = { "南堡油田" };
            
            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);


            result += ",\"sw\":";
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t11\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }*/

        /// <summary>
        /// 表单12数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe12(DateTime data, string searchType = "p")
        {
            sql_TBLNANPUOILFIELDTIDALFORECAST sql = new sql_TBLNANPUOILFIELDTIDALFORECAST();
            TBLNANPUOILFIELDTIDALFORECAST model = new TBLNANPUOILFIELDTIDALFORECAST();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.get_TBLNANPUOILFIELDTIDALFORECAST_AllData(model);
            if(dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.get_TBLNANPUOILFIELDTIDALFORECAST_AllData(model);
            }
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t12\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
            }
            return sb_str.ToString();

        }

        /// <summary>
        /// 表单13数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe13(DateTime data, string searchType = "p")
        {
            sql_TBLSEAAREA24HWAVEFORECAST sql = new sql_TBLSEAAREA24HWAVEFORECAST();
            TBLSEAAREA24HWAVEFORECAST model = new TBLSEAAREA24HWAVEFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSEAAREA24HWAVEFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t13\",\"pbtype\":\"bydb\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"bg\":\"" + dt.Rows[i]["SA24HWFBOHAIWAVEHEIGHT"]
                        + "\",\"bb\":\"" + dt.Rows[i]["SA24HWFBOHAIWAVEDIR"]
                        + "\",\"bj\":\"" + dt.Rows[i]["SA24HWFBOHAIWAVETYPE"]
                        + "\",\"by\":\"" + dt.Rows[i]["SA24HWFBOHAISURGEDIR"]
                        + "\",\"hbg\":\"" + dt.Rows[i]["SA24HWFNORTHOFYSWAVEHEIGHT"]
                        + "\",\"hbbx\":\"" + dt.Rows[i]["SA24HWFNORTHOFYSWAVEDIR"]
                        + "\",\"hbbj\":\"" + dt.Rows[i]["SA24HWFNORTHOFYSWAVETYPE"]
                        + "\",\"hby\":\"" + dt.Rows[i]["SA24HWFNORTHOFYSSURGEDIR"]
                        + "\",\"hzg\":\"" + dt.Rows[i]["SA24HWFMIDDLEOFYSWAVEHEIGHT"]
                        + "\",\"hzbx\":\"" + dt.Rows[i]["SA24HWFMIDDLEOFYSWAVEDIR"]
                        + "\",\"hzbj\":\"" + dt.Rows[i]["SA24HWFMIDDLEOFYSWAVETYPE"]
                        + "\",\"hzy\":\"" + dt.Rows[i]["SA24HWFMIDDLEOFYSSURGEDIR"]
                        + "\",\"hng\":\"" + dt.Rows[i]["SA24HWFSOUTHOFYSWAVEHEIGHT"]
                        + "\",\"hnb\":\"" + dt.Rows[i]["SA24HWFSOUTHOFYSWAVEDIR"]
                        + "\",\"hny\":\"" + dt.Rows[i]["SA24HWFSOUTHOFYSSURGEDIR"]
                        + "\",\"qg\":\"" + dt.Rows[i]["SA24HWFQDOFFSHOREWAVEHEIGHT"]
                        + "\",\"qb\":\"" + dt.Rows[i]["SA24HWFQDOFFSHOREWAVEDIR"]
                        + "\",\"qy\":\"" + dt.Rows[i]["SA24HWFQDOFFSHORESURGEDIR"]
                        + "\",\"bbz\":\"" + dt.Rows[i]["SA24HWFBOHAIWAVENOTES"]
                        + "\",\"hbb\":\"" + dt.Rows[i]["SA24HWFNORTHOFYSWAVENOTES"]
                        + "\",\"hzb\":\"" + dt.Rows[i]["SA24HWFMIDDLEOFYSWAVENOTES"]
                        + "\",\"hnbz\":\"" + dt.Rows[i]["SA24HWFSOUTHOFYSWAVENOTES"]
                        + "\",\"hjb\":\"" + dt.Rows[i]["SA24HWFQDOFFSHOREWAVENOTES"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "渤海", "黄海北部", "黄海中部", "黄海南部", "青岛近海" };
            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "PM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            //上午十
            Sql_HT_TBLWF24HWAVEFORECAST sql10 = new Sql_HT_TBLWF24HWAVEFORECAST();
            HT_TBLWF24HWAVEFORECAST model10 = new HT_TBLWF24HWAVEFORECAST();
            model10.PUBLISHDATE = data;
            DataTable dt10 = (DataTable)sql10.get_TBLWF24HWAVEFORECAST_AllData(model10);
            result += ",\"dt10\":";//水温
            if (dt10 != null && dt10.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt10);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t13\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单14数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe14(DateTime data, string searchType = "p")
        {
            sql_TBLSEAAREA48HWAVEFORECAST sql = new sql_TBLSEAAREA48HWAVEFORECAST();
            TBLSEAAREA48HWAVEFORECAST model = new TBLSEAAREA48HWAVEFORECAST();

            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSEAAREA48HWAVEFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
          
            if (dt != null && dt.Rows.Count > 0)
            {
                
                sb_str.Append(",{ \"type\":\"t14\",\"pbtype\":\"bydb\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"bg\":\"" + dt.Rows[i]["SA48HWFBOHAIWAVEHEIGHT"]
                        + "\",\"bb\":\"" + dt.Rows[i]["SA48HWFBOHAIWAVEDIR"]
                        + "\",\"by\":\"" + dt.Rows[i]["SA48HWFBOHAISURGEDIR"]
                        + "\",\"hbg\":\"" + dt.Rows[i]["SA48HWFNORTHOFYSWAVEHEIGHT"]
                        + "\",\"hbbx\":\"" + dt.Rows[i]["SA48HWFNORTHOFYSWAVEDIR"]
                        + "\",\"hby\":\"" + dt.Rows[i]["SA48HWFNORTHOFYSSURGEDIR"]
                        + "\",\"hzg\":\"" + dt.Rows[i]["SA48HWFMIDDLEOFYSWAVEHEIGHT"]
                        + "\",\"hzbx\":\"" + dt.Rows[i]["SA48HWFMIDDLEOFYSWAVEDIR"]
                        + "\",\"hzy\":\"" + dt.Rows[i]["SA48HWFMIDDLEOFYSSURGEDIR"]
                        + "\",\"hng\":\"" + dt.Rows[i]["SA48HWFSOUTHOFYSWAVEHEIGHT"]
                        + "\",\"hnb\":\"" + dt.Rows[i]["SA48HWFSOUTHOFYSWAVEDIR"]
                        + "\",\"hny\":\"" + dt.Rows[i]["SA48HWFSOUTHOFYSSURGEDIR"]
                        //+ "\",\"qg\":\"" + dt.Rows[i]["SA48HWFQDOFFSHOREWAVEHEIGHT"]
                        //+ "\",\"qb\":\"" + dt.Rows[i]["SA48HWFQDOFFSHOREWAVEDIR"]
                        //+ "\",\"qy\":\"" + dt.Rows[i]["SA48HWFQDOFFSHORESURGEDIR"]
                        + "\",\"bbz\":\"" + dt.Rows[i]["SA48HWFBOHAIWAVENOTES"]
                        + "\",\"hbb\":\"" + dt.Rows[i]["SA48HWFNORTHOFYSWAVENOTES"]
                        + "\",\"hzb\":\"" + dt.Rows[i]["SA48HWFMIDDLEOFYSWAVENOTES"]
                        + "\",\"hnbz\":\"" + dt.Rows[i]["SA48HWFSOUTHOFYSWAVENOTES"]
                        //  + "\",\"hjb\":\"" + dt.Rows[i]["SA48HWFQDOFFSHOREWAVENOTES"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "渤海", "黄海北部", "黄海中部", "黄海南部" };
            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "PM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }

            sb_str.Append(",{ \"type\":\"t14\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单15数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe15(DateTime data)
        {
            sql_TBLSEAAREA72HWAVEFORECAST sql = new sql_TBLSEAAREA72HWAVEFORECAST();
            TBLSEAAREA72HWAVEFORECAST model = new TBLSEAAREA72HWAVEFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSEAAREA72HWAVEFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t15\",\"pbtype\":\"bydb\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"bg\":\"" + dt.Rows[i]["SA72HWFBOHAIWAVEHEIGHT"]
                        + "\",\"bb\":\"" + dt.Rows[i]["SA72HWFBOHAIWAVEDIR"]
                        + "\",\"by\":\"" + dt.Rows[i]["SA72HWFBOHAISURGEDIR"]
                        + "\",\"hbg\":\"" + dt.Rows[i]["SA72HWFNORTHOFYSWAVEHEIGHT"]
                        + "\",\"hbbx\":\"" + dt.Rows[i]["SA72HWFNORTHOFYSWAVEDIR"]
                        + "\",\"hby\":\"" + dt.Rows[i]["SA72HWFNORTHOFYSSURGEDIR"]
                        + "\",\"hzg\":\"" + dt.Rows[i]["SA72HWFMIDDLEOFYSWAVEHEIGHT"]
                        + "\",\"hzbx\":\"" + dt.Rows[i]["SA72HWFMIDDLEOFYSWAVEDIR"]
                        + "\",\"hzy\":\"" + dt.Rows[i]["SA72HWFMIDDLEOFYSSURGEDIR"]
                        + "\",\"hng\":\"" + dt.Rows[i]["SA72HWFSOUTHOFYSWAVEHEIGHT"]
                        + "\",\"hnb\":\"" + dt.Rows[i]["SA72HWFSOUTHOFYSWAVEDIR"]
                        + "\",\"hny\":\"" + dt.Rows[i]["SA72HWFSOUTHOFYSSURGEDIR"]
                        //+ "\",\"qg\":\"" + dt.Rows[i]["SA72HWFQDOFFSHOREWAVEHEIGHT"]
                        //+ "\",\"qb\":\"" + dt.Rows[i]["SA72HWFQDOFFSHOREWAVEDIR"]
                        //+ "\",\"qy\":\"" + dt.Rows[i]["SA72HWFQDOFFSHORESURGEDIR"]
                        + "\",\"bbz\":\"" + dt.Rows[i]["SA72HWFBOHAIWAVENOTES"]
                        + "\",\"hbb\":\"" + dt.Rows[i]["SA72HWFNORTHOFYSWAVENOTES"]
                        + "\",\"hzb\":\"" + dt.Rows[i]["SA72HWFMIDDLEOFYSWAVENOTES"]
                        + "\",\"hnbz\":\"" + dt.Rows[i]["SA72HWFSOUTHOFYSWAVENOTES"]
                        // + "\",\"hjb\":\"" + dt.Rows[i]["SA72HWFQDOFFSHOREWAVENOTES"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";

            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "渤海", "黄海北部", "黄海中部", "黄海南部" };
            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "PM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }

            sb_str.Append(",{ \"type\":\"t15\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单16数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe16(DateTime data)
        {
            sql_TBLWF24HTIDALFORECAST sql = new sql_TBLWF24HTIDALFORECAST();
            TBLWF24HTIDALFORECAST model = new TBLWF24HTIDALFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLWF24HTIDALFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t16\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"s1\":\"" + dt.Rows[i]["WF24HTFFIRSTHIGHWAVETIME"]
                    + "\",\"g1\":\"" + dt.Rows[i]["WF24HTFFIRSTHIGHWAVEHEIGHT"]
                        + "\",\"s2\":\"" + dt.Rows[i]["WF24HTFSECONDHIGHWAVETIME"]
                    + "\",\"g2\":\"" + dt.Rows[i]["WF24HTFSECONDHIGHWAVEHEIGHT"]
                        + "\",\"ds1\":\"" + dt.Rows[i]["WF24HTFFIRSTLOWWAVETIME"]
                        + "\",\"dg1\":\"" + dt.Rows[i]["WF24HTFFIRSTLOWWAVEHEIGHT"]
                        + "\",\"ds2\":\"" + dt.Rows[i]["WF24HTFSECONDLOWWAVETIME"]
                        + "\",\"dg2\":\"" + dt.Rows[i]["WF24HTFSECONDLOWWAVEHEIGHT"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 表单17数据 下午十七 青岛市各海水浴场海浪、水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe17(DateTime data)
        {
            sql_TBLSEABEACH24HWAVEFORECAST sql = new sql_TBLSEABEACH24HWAVEFORECAST();
            TBLSEABEACH24HWAVEFORECAST model = new TBLSEABEACH24HWAVEFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSEABEACH24HWAVEFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t17\",\"pbtype\":\"bydb\",\"children\":[");
                int dbSWRowsNULLCount = 0;
                //判断海温数据是否为null
                //若为null
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if (DBNull.Value == dt.Rows[sw]["SB24HWFFIRSTBATHINGWATERTEMP"] && DBNull.Value == dt.Rows[sw]["SB24HWFSIXTHBATHINGWATERTEMP"] && DBNull.Value == dt.Rows[sw]["SB24HWFSLRBATHINGWATERTEMP"] && DBNull.Value == dt.Rows[sw]["SB24HWFGOLDBEACHWATERTEMP"])
                    {
                        dbSWRowsNULLCount++;
                    }
                    if (dbSWRowsNULLCount > 0)
                    {
                        string[] areas = { "第一海水浴场", "第六海水浴场", "石老人海水浴场", "金沙滩海水浴场" };

                        DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areas);
                        if (dtsw != null && dtsw.Rows.Count > 0)
                        {

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                sb_str.Append("{\"yl\":\"" + dt.Rows[j]["SB24HWFFIRSTBATHINGWAVEHEIGHT"]
                       + "\",\"yy\":\"" + dt.Rows[j]["SB24HWFFIRSTBATHINGSWIMWARN"]
                       + "\",\"ll\":\"" + dt.Rows[j]["SB24HWFSIXTHBATHINGWAVEHEIGHT"]
                       + "\",\"ly\":\"" + dt.Rows[j]["SB24HWFSIXTHBATHINGSWIMWARN"]
                       + "\",\"sl\":\"" + dt.Rows[j]["SB24HWFSLRBATHINGWAVEHEIGHT"]
                       + "\",\"sy\":\"" + dt.Rows[j]["SB24HWFSLRBATHINGSWIMWARN"]
                       + "\",\"jl\":\"" + dt.Rows[j]["SB24HWFGOLDBEACHWAVEHEIGHT"]
                       + "\",\"jy\":\"" + dt.Rows[j]["SB24HWFGOLDBEACHSWIMWAIN"]);
                                for (int k = 0; k < dtsw.Rows.Count; k++)
                                {
                                    string forecastArea = dtsw.Rows[k]["NAME"].ToString();
                                    switch (forecastArea)
                                    {
                                        case "第一海水浴场":
                                            sb_str.Append("\",\"ys\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                            break;
                                        case "第六海水浴场":
                                            sb_str.Append("\",\"ls\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                            break;
                                        case "石老人海水浴场":
                                            sb_str.Append("\",\"ss\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                            break;
                                        case "金沙滩海水浴场":
                                            sb_str.Append("\",\"js\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                sb_str.Append("\"},");
                            }
                            return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"yl\":\"" + dt.Rows[i]["SB24HWFFIRSTBATHINGWAVEHEIGHT"]
                       + "\",\"ys\":\"" + dt.Rows[i]["SB24HWFFIRSTBATHINGWATERTEMP"]
                       + "\",\"yy\":\"" + dt.Rows[i]["SB24HWFFIRSTBATHINGSWIMWARN"]
                       + "\",\"ll\":\"" + dt.Rows[i]["SB24HWFSIXTHBATHINGWAVEHEIGHT"]
                       + "\",\"ls\":\"" + dt.Rows[i]["SB24HWFSIXTHBATHINGWATERTEMP"]
                       + "\",\"ly\":\"" + dt.Rows[i]["SB24HWFSIXTHBATHINGSWIMWARN"]
                       + "\",\"sl\":\"" + dt.Rows[i]["SB24HWFSLRBATHINGWAVEHEIGHT"]
                       + "\",\"ss\":\"" + dt.Rows[i]["SB24HWFSLRBATHINGWATERTEMP"]
                       + "\",\"sy\":\"" + dt.Rows[i]["SB24HWFSLRBATHINGSWIMWARN"]
                       + "\",\"jl\":\"" + dt.Rows[i]["SB24HWFGOLDBEACHWAVEHEIGHT"]
                       + "\",\"js\":\"" + dt.Rows[i]["SB24HWFGOLDBEACHWATERTEMP"]
                       + "\",\"jy\":\"" + dt.Rows[i]["SB24HWFGOLDBEACHSWIMWAIN"]
                       + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            string[] FORECASTAREA = { "青岛近海" };
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLSEABEACH24HWAVEFORECAST_AllData(model);
            string[] area = { "第一海水浴场", "第六海水浴场", "石老人海水浴场", "金沙滩海水浴场" };
            
            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);
            result += ",\"sw\":";
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t17\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单18数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe18(DateTime data)
        {
            sql_TBLGOLDBEACH24HTIDALFORECAST sql = new sql_TBLGOLDBEACH24HTIDALFORECAST();
            TBLGOLDBEACH72HTIDALFORECAST model = new TBLGOLDBEACH72HTIDALFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLGOLDBEACH24HTIDALFORECAST_AllData(model);
            //StringBuilder sb_str = new StringBuilder();
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.get_TBLGOLDBEACH24HTIDALFORECAST_AllData(model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();

                sb_str.Append(",{ \"type\":\"t18\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }

        /// <summary>
        /// 表单19数据//下午十五 青岛周边海域24小时海浪、水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe19(DateTime data)
        {
            sql_TBLQDCIRCUM24HWATERFORECAST sql = new sql_TBLQDCIRCUM24HWATERFORECAST();
            TBLQDCIRCUM24HWATERFORECAST model = new TBLQDCIRCUM24HWATERFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLQDCIRCUM24HWATERFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t19\",\"pbtype\":\"bydb\",\"children\":[");

                #region 无水温有风浪
                int dbSWRowsNULLCount = 0;
                //判断海温数据是否为null
                //若为null
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if (DBNull.Value == dt.Rows[sw]["QDC24HWFQDOFFSHOREWATERTEMP"] && DBNull.Value == dt.Rows[sw]["QDC24HWFJMOFFSHOREWATERTEMP"] && DBNull.Value == dt.Rows[sw]["QDC24HWFJZWOFFSHOREWATERTEMP"] && DBNull.Value == dt.Rows[sw]["QDC24HWFJNOFFSHOREWATERTEMP"])
                    {
                        dbSWRowsNULLCount++;
                    }
                }
                if (dbSWRowsNULLCount > 0)
                {
                    string[] areas = { "青岛近海", "即墨近海", "胶州近海", "黄岛近海" };

                    DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areas);

                    if (dtsw != null && dtsw.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            sb_str.Append("{\"ql\":\"" + dt.Rows[j]["QDC24HWFQDOFFSHOREWAVEHEIGHT"]
                       + "\",\"jl\":\"" + dt.Rows[j]["QDC24HWFJMOFFSHOREWAVEHEIGHT"]
                       + "\",\"jzl\":\"" + dt.Rows[j]["QDC24HWFJZWOFFSHOREWAVEHEIGHT"]
                       + "\",\"jnl\":\"" + dt.Rows[j]["QDC24HWFJNOFFSHOREWAVEHEIGHT"]);
                            for (int k = 0; k < dtsw.Rows.Count; k++)
                            {
                                string forecastArea = dtsw.Rows[k]["NAME"].ToString();
                                switch (forecastArea)
                                {
                                    case "青岛近海":
                                        sb_str.Append("\",\"qs\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "即墨近海":
                                        sb_str.Append("\",\"js\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "胶州近海":
                                        sb_str.Append("\",\"jzs\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "黄岛近海":
                                        sb_str.Append("\",\"jns\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            sb_str.Append("\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }
                #endregion

                #region 有水温无海浪和风
                int dbHLRowsNULLCount = 0;
                for (int hl = 0; hl < dt.Rows.Count; hl++)
                {
                    if (DBNull.Value == dt.Rows[hl]["QDC24HWFQDOFFSHOREWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["QDC24HWFJMOFFSHOREWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["QDC24HWFJZWOFFSHOREWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["QDC24HWFJNOFFSHOREWAVEHEIGHT"])
                    {
                        dbHLRowsNULLCount++;
                    }

                }//FORECASTAREA
                if (dbHLRowsNULLCount > 0)
                {
                    string[] areas = { "青岛近海", "即墨近海", "胶州湾", "黄岛近海" };
                    DataTable dthl = (DataTable)getBy_s.GetWaveAndWindData(data, areas, "AM");
                    if (dthl != null && dthl.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {                                            
                            sb_str.Append("{\"qs\":\"" + dt.Rows[j]["QDC24HWFQDOFFSHOREWATERTEMP"]
                                + "\",\"js\":\"" + dt.Rows[j]["QDC24HWFJMOFFSHOREWATERTEMP"]
                                + "\",\"jzs\":\"" + dt.Rows[j]["QDC24HWFJZWOFFSHOREWATERTEMP"]
                                + "\",\"jns\":\"" + dt.Rows[j]["QDC24HWFJNOFFSHOREWATERTEMP"]);
                            for (int k = 0; k < dthl.Rows.Count; k++)
                            {
                                string forecastArea = dthl.Rows[k]["FORECASTAREA"].ToString();
                                switch (forecastArea)
                                {
                                    case "青岛近海":
                                        sb_str.Append("\",\"ql\":\"" + dthl.Rows[k]["WAVE24FORECAST"]);
                                        break;
                                    case "即墨近海":
                                        sb_str.Append("\",\"jl\":\"" + dthl.Rows[k]["WAVE24FORECAST"]);
                                        break;
                                    case "胶州湾":
                                        sb_str.Append("\",\"jzl\":\"" + dthl.Rows[k]["WAVE24FORECAST"]);
                                        break;
                                    case "黄岛近海":
                                        sb_str.Append("\",\"jnl\":\"" + dthl.Rows[k]["WAVE24FORECAST"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            sb_str.Append("\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";

                    }
                }

                #endregion



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"ql\":\"" + dt.Rows[i]["QDC24HWFQDOFFSHOREWAVEHEIGHT"]
                       + "\",\"qs\":\"" + dt.Rows[i]["QDC24HWFQDOFFSHOREWATERTEMP"]
                       + "\",\"jl\":\"" + dt.Rows[i]["QDC24HWFJMOFFSHOREWAVEHEIGHT"]
                       + "\",\"js\":\"" + dt.Rows[i]["QDC24HWFJMOFFSHOREWATERTEMP"]
                       + "\",\"jzl\":\"" + dt.Rows[i]["QDC24HWFJZWOFFSHOREWAVEHEIGHT"]
                       + "\",\"jzs\":\"" + dt.Rows[i]["QDC24HWFJZWOFFSHOREWATERTEMP"]
                       + "\",\"jnl\":\"" + dt.Rows[i]["QDC24HWFJNOFFSHOREWAVEHEIGHT"]
                       + "\",\"jns\":\"" + dt.Rows[i]["QDC24HWFJNOFFSHOREWATERTEMP"]
                       + "\"},");
                }

                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            

            #region 原始数据
            DataTable dtWave = new DataTable();
            string[] FORECASTAREA = { "青岛近海", "即墨近海", "胶州湾", "黄岛近海" };
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLQDCIRCUM24HWATERFORECAST_AllData(model);

            string[] area = { "青岛近海", "即墨近海", "胶州近海", "黄岛近海" };

            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);

            result += ",\"sw\":";
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t19\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
            #endregion
        }

        //原来的
        /*public string gettabe19(DateTime data)
        {
            sql_TBLQDCIRCUM24HWATERFORECAST sql = new sql_TBLQDCIRCUM24HWATERFORECAST();
            TBLQDCIRCUM24HWATERFORECAST model = new TBLQDCIRCUM24HWATERFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLQDCIRCUM24HWATERFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t19\",\"pbtype\":\"bydb\",\"children\":[");
                int dbSWRowsNULLCount = 0;
                //判断海温数据是否为null
                //若为null
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if (DBNull.Value == dt.Rows[sw]["QDC24HWFQDOFFSHOREWATERTEMP"] && DBNull.Value == dt.Rows[sw]["QDC24HWFJMOFFSHOREWATERTEMP"] && DBNull.Value == dt.Rows[sw]["QDC24HWFJZWOFFSHOREWATERTEMP"] && DBNull.Value == dt.Rows[sw]["QDC24HWFJNOFFSHOREWATERTEMP"])
                    {
                        dbSWRowsNULLCount++;
                    }
                }
                if (dbSWRowsNULLCount > 0)
                {
                    string[] areas = { "青岛近海", "即墨近海", "胶州近海", "黄岛近海" };

                    DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areas);

                    if (dtsw != null && dtsw.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            sb_str.Append("{\"ql\":\"" + dt.Rows[j]["QDC24HWFQDOFFSHOREWAVEHEIGHT"]
                       + "\",\"jl\":\"" + dt.Rows[j]["QDC24HWFJMOFFSHOREWAVEHEIGHT"]
                       + "\",\"jzl\":\"" + dt.Rows[j]["QDC24HWFJZWOFFSHOREWAVEHEIGHT"]
                       + "\",\"jnl\":\"" + dt.Rows[j]["QDC24HWFJNOFFSHOREWAVEHEIGHT"]);
                            for (int k = 0; k < dtsw.Rows.Count; k++)
                            {
                                string forecastArea = dtsw.Rows[k]["NAME"].ToString();
                                switch (forecastArea)
                                {
                                    case "青岛近海":
                                        sb_str.Append("\",\"qs\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "即墨近海":
                                        sb_str.Append("\",\"js\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "胶州近海":
                                        sb_str.Append("\",\"jzs\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    case "黄岛近海":
                                        sb_str.Append("\",\"jns\":\"" + dtsw.Rows[k]["MEAN_24H"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            sb_str.Append("\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"ql\":\"" + dt.Rows[i]["QDC24HWFQDOFFSHOREWAVEHEIGHT"]
                       + "\",\"qs\":\"" + dt.Rows[i]["QDC24HWFQDOFFSHOREWATERTEMP"]
                       + "\",\"jl\":\"" + dt.Rows[i]["QDC24HWFJMOFFSHOREWAVEHEIGHT"]
                       + "\",\"js\":\"" + dt.Rows[i]["QDC24HWFJMOFFSHOREWATERTEMP"]
                       + "\",\"jzl\":\"" + dt.Rows[i]["QDC24HWFJZWOFFSHOREWAVEHEIGHT"]
                       + "\",\"jzs\":\"" + dt.Rows[i]["QDC24HWFJZWOFFSHOREWATERTEMP"]
                       + "\",\"jnl\":\"" + dt.Rows[i]["QDC24HWFJNOFFSHOREWAVEHEIGHT"]
                       + "\",\"jns\":\"" + dt.Rows[i]["QDC24HWFJNOFFSHOREWATERTEMP"]
                       + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            string[] FORECASTAREA = { "青岛近海", "即墨近海", "胶州湾", "黄岛近海" };
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLQDCIRCUM24HWATERFORECAST_AllData(model);

            string[] area = { "青岛近海", "即墨近海", "胶州近海", "黄岛近海" };
           
            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);

            result += ",\"sw\":";
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t19\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }*/

        /// <summary>
        /// 表单20数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe20(DateTime data)
        {
            sql_TBLQDCOAST48HTIDALFORECAST sql = new sql_TBLQDCOAST48HTIDALFORECAST();
            TBLQDCOAST48HTIDALFORECAST model = new TBLQDCOAST48HTIDALFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLQDCOAST48HTIDALFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t20\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"g1s\":\"" + dt.Rows[i]["QDC48HTFFIRSTHIGHWAVEHOUR"]
                       + "\",\"g1m\":\"" + dt.Rows[i]["QDC48HTFFIRSTHGHWAVEMINUTE"]
                       + "\",\"g1g\":\"" + dt.Rows[i]["QDC48HTFFIRSTHIGHWAVEHEIGHT"]
                       + "\",\"g2s\":\"" + dt.Rows[i]["QDC48HTFSECONDHIGHWAVEHOUR"]
                       + "\",\"g2m\":\"" + dt.Rows[i]["QDC48HTFSECONDHIGHWAVEMINUTE"]
                       + "\",\"g2g\":\"" + dt.Rows[i]["QDC48HTFSECONDHIGHWAVEHEIGHT"]
                       + "\",\"d1s\":\"" + dt.Rows[i]["QDC48HTFFIRSTLOWWAVEHOUR"]
                       + "\",\"d1m\":\"" + dt.Rows[i]["QDC48HTFFIRSTLOWWAVEMINUTE"]
                       + "\",\"d1g\":\"" + dt.Rows[i]["QDC48HTFFIRSTLOWWAVEHEIGHT"]
                       + "\",\"d2s\":\"" + dt.Rows[i]["QDC48HTFSECONDLOWWAVEHOUR"]
                       + "\",\"d2m\":\"" + dt.Rows[i]["QDC48HTFSECONDLOWWAVEMINUTE"]
                       + "\",\"d2g\":\"" + dt.Rows[i]["QDC48HTFSECONDLOWWAVEHEIGHT"]
                       + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 表单21数据//下午十五威海电视台未来24小时预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe21(DateTime data, string searchType = "p")
        {

            sql_TBLWEIHAITV24HFORECAST sql = new sql_TBLWEIHAITV24HFORECAST();
            TBLWEIHAITV24HFORECAST model = new TBLWEIHAITV24HFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLWEIHAITV24HFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t21\",\"pbtype\":\"bydb\",\"children\":[");
                int dbSWRowsNULLCount = 0;
                
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if (DBNull.Value == dt.Rows[sw]["WTV24HSD24HFWATERTEMP"] && DBNull.Value == dt.Rows[sw]["WTV24HSD48HFWATERTEMP"] && DBNull.Value == dt.Rows[sw]["WTV24HWD24HFWATERTEMP"] && DBNull.Value == dt.Rows[sw]["WTV24HWD48HFWATERTEMP"] && DBNull.Value == dt.Rows[sw]["WTV24HCST24HFWATERTEMP"] && DBNull.Value == dt.Rows[sw]["WTV24HCST48HFWATERTEMP"] && DBNull.Value == dt.Rows[sw]["WTV24HRS24HFWATERTEMP"] && DBNull.Value == dt.Rows[sw]["WTV24HRS48HFWATERTEMP"])
                    {
                        dbSWRowsNULLCount++;
                    }
                }
                
                if (dbSWRowsNULLCount > 0)
                {
                    string[] areas = { "成山头", "南黄岛", "石岛常规预报点", "文登常规预报点" };
                    DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areas);
                    if (dtsw != null && dtsw.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            sb_str.Append("{\"s2l\":\"" + dt.Rows[j]["WTV24HSD24HFWAVEHEIGHT"]
                       + "\",\"wh4l\":\"" + dt.Rows[j]["WTV24HWH48HFWAVEHEIGHT"]
                       + "\",\"wh4s\":\"" + dt.Rows[j]["WTV24HWH48HFWATERTEMP"]
                       + "\",\"s4l\":\"" + dt.Rows[j]["WTV24HSD48HFWAVEHEIGHT"]
                       + "\",\"w2l\":\"" + dt.Rows[j]["WTV24HWD24HFWAVEHEIGHT"]
                       + "\",\"c2l\":\"" + dt.Rows[j]["WTV24HCST24HFWAVEHEIGHT"]
                       + "\",\"r2l\":\"" + dt.Rows[j]["WTV24HRS24HFWAVEHEIGHT"]
                       + "\",\"w4l\":\"" + dt.Rows[j]["WTV24HWD48HFWAVEHEIGHT"]
                       + "\",\"c4l\":\"" + dt.Rows[j]["WTV24HCST48HFWAVEHEIGHT"]
                       + "\",\"r4l\":\"" + dt.Rows[j]["WTV24HRS48HFWAVEHEIGHT"]);
                            for (int k = 0; k < dtsw.Rows.Count; k++)
                            {
                                string forecastArea = dtsw.Rows[k]["NAME"].ToString();
                                switch (forecastArea)
                                {
                                    case "文登常规预报点":
                                        sb_str.Append("\",\"w2s\":\"" + dtsw.Rows[k]["MEAN_24H"] + "\",\"w4s\":\"" + dtsw.Rows[k]["MEAN_48H"]);
                                        break;
                                    case "石岛常规预报点":
                                        sb_str.Append("\",\"s2s\":\"" + dtsw.Rows[k]["MEAN_24H"] + "\",\"s4s\":\"" + dtsw.Rows[k]["MEAN_48H"]);
                                        break;
                                    case "成山头":
                                        sb_str.Append("\",\"c2s\":\"" + dtsw.Rows[k]["MEAN_24H"] + "\",\"c4s\":\"" + dtsw.Rows[k]["MEAN_48H"]);
                                        break;
                                    case "南黄岛":
                                        sb_str.Append("\",\"r2s\":\"" + dtsw.Rows[k]["MEAN_24H"] + "\",\"r4s\":\"" + dtsw.Rows[k]["MEAN_48H"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            sb_str.Append("\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }
                
                int dbHLRowsNULLCount = 0;
                for (int hl = 0; hl < dt.Rows.Count; hl++)
                {
                    if (DBNull.Value == dt.Rows[hl]["WTV24HSD24HFWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["WTV24HWH48HFWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["WTV24HSD48HFWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["WTV24HWD24HFWAVEHEIGHT"] 
                        && DBNull.Value == dt.Rows[hl]["WTV24HCST24HFWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["WTV24HRS24HFWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["WTV24HWD48HFWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["WTV24HCST48HFWAVEHEIGHT"] && DBNull.Value == dt.Rows[hl]["WTV24HRS48HFWAVEHEIGHT"])
                    {
                        dbHLRowsNULLCount++;
                    }
                }

                if (dbHLRowsNULLCount > 0)
                {
                    string[] areas = { "乳山近海", "文登近海", "石岛近海", "成山头", "威海近海" };
                    DataTable dthl = (DataTable)getBy_s.GetWaveAndWindData(data, areas, "AM");
                    if (dthl != null && dthl.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            sb_str.Append("{\"s2s\":\"" + dt.Rows[j]["WTV24HSD24HFWATERTEMP"]
                      + "\",\"wh4s\":\"" + dt.Rows[j]["WTV24HWH48HFWATERTEMP"]
                      + "\",\"s4s\":\"" + dt.Rows[j]["WTV24HSD48HFWATERTEMP"]
                      + "\",\"w2s\":\"" + dt.Rows[j]["WTV24HWD24HFWATERTEMP"]
                      + "\",\"c2s\":\"" + dt.Rows[j]["WTV24HCST24HFWATERTEMP"]
                      + "\",\"r2s\":\"" + dt.Rows[j]["WTV24HRS24HFWATERTEMP"]
                      + "\",\"w4s\":\"" + dt.Rows[j]["WTV24HWD48HFWATERTEMP"]
                      + "\",\"c4s\":\"" + dt.Rows[j]["WTV24HCST48HFWATERTEMP"]
                      + "\",\"r4s\":\"" + dt.Rows[j]["WTV24HRS48HFWATERTEMP"]);
                            for (int k = 0; k < dthl.Rows.Count; k++)
                            {
                                string forecastArea = dthl.Rows[k]["FORECASTAREA"].ToString();
                                switch (forecastArea)
                                {
                                    case "文登近海":
                                        sb_str.Append("\",\"w2l\":\"" + dthl.Rows[k]["WAVE24FORECAST"] + "\",\"w4l\":\"" + dthl.Rows[k]["WAVE48FORECAST"]);
                                        break;
                                    case "石岛近海":
                                        sb_str.Append("\",\"s2l\":\"" + dthl.Rows[k]["WAVE24FORECAST"] + "\",\"s4l\":\"" + dthl.Rows[k]["WAVE48FORECAST"]);
                                        break;
                                    case "成山头":
                                        sb_str.Append("\",\"c2l\":\"" + dthl.Rows[k]["WAVE24FORECAST"] + "\",\"c4l\":\"" + dthl.Rows[k]["WAVE48FORECAST"]);
                                        break;
                                    case "乳山近海":
                                        sb_str.Append("\",\"r2l\":\"" + dthl.Rows[k]["WAVE24FORECAST"] + "\",\"r4l\":\"" + dthl.Rows[k]["WAVE48FORECAST"]);
                                        break;
                                    case "威海近海":
                                        sb_str.Append("\",\"wh4l\":\"" + dthl.Rows[k]["WAVE48FORECAST"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            sb_str.Append("\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"s2l\":\"" + dt.Rows[i]["WTV24HSD24HFWAVEHEIGHT"]
                       + "\",\"s2s\":\"" + dt.Rows[i]["WTV24HSD24HFWATERTEMP"]
                       + "\",\"wh4l\":\"" + dt.Rows[i]["WTV24HWH48HFWAVEHEIGHT"]
                       + "\",\"wh4s\":\"" + dt.Rows[i]["WTV24HWH48HFWATERTEMP"]
                       + "\",\"s4l\":\"" + dt.Rows[i]["WTV24HSD48HFWAVEHEIGHT"]
                       + "\",\"s4s\":\"" + dt.Rows[i]["WTV24HSD48HFWATERTEMP"]
                       + "\",\"w2l\":\"" + dt.Rows[i]["WTV24HWD24HFWAVEHEIGHT"]
                       + "\",\"w2s\":\"" + dt.Rows[i]["WTV24HWD24HFWATERTEMP"]
                       + "\",\"c2l\":\"" + dt.Rows[i]["WTV24HCST24HFWAVEHEIGHT"]
                       + "\",\"c2s\":\"" + dt.Rows[i]["WTV24HCST24HFWATERTEMP"]
                       + "\",\"r2l\":\"" + dt.Rows[i]["WTV24HRS24HFWAVEHEIGHT"]
                       + "\",\"r2s\":\"" + dt.Rows[i]["WTV24HRS24HFWATERTEMP"]
                       + "\",\"w4l\":\"" + dt.Rows[i]["WTV24HWD48HFWAVEHEIGHT"]
                       + "\",\"w4s\":\"" + dt.Rows[i]["WTV24HWD48HFWATERTEMP"]
                       + "\",\"c4l\":\"" + dt.Rows[i]["WTV24HCST48HFWAVEHEIGHT"]
                       + "\",\"c4s\":\"" + dt.Rows[i]["WTV24HCST48HFWATERTEMP"]
                       + "\",\"r4l\":\"" + dt.Rows[i]["WTV24HRS48HFWAVEHEIGHT"]
                       + "\",\"r4s\":\"" + dt.Rows[i]["WTV24HRS48HFWATERTEMP"]
                       + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            
            
            string[] FORECASTAREA = { "乳山近海", "文登近海", "石岛近海", "成山头", "威海近海" };
            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            var result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLWEIHAITV24HFORECAST_AllData(model);

            string[] area = { "成山头", "南黄岛", "石岛常规预报点", "文登常规预报点" };
            //string[] ids = { "C16_SDO", "C18_NHD" };
            
            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);

            //DataTable dts = new DataTable();
            //string[]  areas = { "成山头", "南黄岛", "石岛常规预报点", "文登常规预报点" };
            //string[] ids = { "C16_SDO", "C18_NHD" };
            //dts = (DataTable)waterTemperature.GetWaterTemperatureData(data, areas,ids);
            result += ",\"sw\":";
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t21\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();

        }

        /// <summary>
        /// 表单22数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe22(DateTime data,string searchType)
        {
            sql_TBLWEIHAISHIDAOTIDALFORECAST sql = new sql_TBLWEIHAISHIDAOTIDALFORECAST();
            TBLWEIHAISHIDAOTIDALFORECAST model = new TBLWEIHAISHIDAOTIDALFORECAST();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.get_TBLWEIHAISHIDAOTIDALFORECAST_AllData(model);
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.get_TBLWEIHAISHIDAOTIDALFORECAST_AllData(model);
            }
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t22\",\"children\":[");
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
            if(dt == null || dt.Rows.Count < 1)
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
                        + "\",\"hltel\":\"" + dt.Rows[i]["FWAVEFORECASTERTEL"]
                        + "\",\"cxtel\":\"" + dt.Rows[i]["FTIDALFORECASTERTEL"]
                        + "\",\"swtel\":\"" + dt.Rows[i]["FWATERTEMPERATUREFORECASTERTEL"]
                        + "\",\"zhibantel\":\"" + dt.Rows[i]["ZHIBANTEL"]
                        + "\",\"sendtel\":\"" + dt.Rows[i]["SENDTEL"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 表单24数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe24(DateTime data)
        {

            sql_TBLSEAAREASEAICEFORECAST sql = new sql_TBLSEAAREASEAICEFORECAST();
            TBLSEAAREASEAICEFORECAST model = new TBLSEAAREASEAICEFORECAST();

            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSEAAREASEAICEFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {

                sb_str.Append(",{ \"type\":\"t24\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"sa\":\"" + dt.Rows[i]["SEAAREA"]
                        + "\",\"ma\":\"" + dt.Rows[i]["MAXICEAREA"]
                        + "\",\"ct\":\"" + dt.Rows[i]["COMMONTHICKNESS"]
                        + "\",\"mt\":\"" + dt.Rows[i]["MAXTHICKNESS"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 表单指挥处上午数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable25(DateTime data, string searchType = "p")
        {

            TBLZHCFORECAST TBLZHCFORECAST_Model = new TBLZHCFORECAST();
            System.Data.DataTable dttbTBLZHCFORECAST = new DataTable();
        
         
            TBLZHCFORECAST_Model.PUBLISHDATE = data.AddHours(7);
            if (searchType == "f")
            {
                string date1 = data.AddDays(1).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");
                string date2 = data.AddDays(2).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");
                string date3 = data.AddDays(3).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");

                dttbTBLZHCFORECAST = (System.Data.DataTable)new sql_TBLZHCFORECAST().GETTBLZHCFORECASTBYFORCASEDATEALL(TBLZHCFORECAST_Model, date1, date2, date3);
                if (dttbTBLZHCFORECAST.Rows.Count < 1)
                {
                    TBLZHCFORECAST_Model.PUBLISHDATE = data.AddDays(-1).AddHours(16);
                    dttbTBLZHCFORECAST = (System.Data.DataTable)new sql_TBLZHCFORECAST().GETTBLZHCFORECASTBYFORCASEDATEBYPUBLISH(TBLZHCFORECAST_Model);
                }
            }
            else
            {
                TBLZHCFORECAST_Model.PUBLISHDATE = data.AddHours(7);
                dttbTBLZHCFORECAST = (System.Data.DataTable)new sql_TBLZHCFORECAST().GETTBLZHCFORECASTBYFORCASEDATEBYPUBLISH(TBLZHCFORECAST_Model);
            }

            if (dttbTBLZHCFORECAST.Rows.Count == 0)
            {
                //if (searchType == "f")
                //{
                //    TBLZHCFORECAST_Model.PUBLISHDATE = data.AddDays(-1).AddHours(16);
                //}
                //else
                //{
                //    TBLZHCFORECAST_Model.PUBLISHDATE = data.AddHours(7);
                //}
                //dttbTBLZHCFORECAST = (System.Data.DataTable)new sql_TBLZHCFORECAST().get_TBLZHCFORECAST_AllData(TBLZHCFORECAST_Model);
            }
            else
            {
                var result = JsonMore.Serialize(dttbTBLZHCFORECAST);
                StringBuilder sb_str = new StringBuilder();

                sb_str.Append(",{ \"type\":\"t25\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }



        /// <summary>
        /// 表单指挥处下午数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable26(DateTime data, string searchType = "p")
        {
            //if (searchType == "p")
            //{
            TBLZHCFORECAST TBLZHCFORECAST_Model = new TBLZHCFORECAST();
            TBLZHCFORECAST_Model.PUBLISHDATE = data.AddHours(16);

            System.Data.DataTable dttbTBLZHCFORECAST = new DataTable();


            string date1 = data.AddDays(1).AddHours(16).ToString("yyyy-MM-dd HH:mm:ss");
            string date2 = data.AddDays(2).AddHours(16).ToString("yyyy-MM-dd HH:mm:ss");
            string date3 = data.AddDays(3).AddHours(16).ToString("yyyy-MM-dd HH:mm:ss");

            dttbTBLZHCFORECAST = (System.Data.DataTable)new sql_TBLZHCFORECAST().GETTBLZHCFORECASTBYFORCASEDATEALL(TBLZHCFORECAST_Model, date1, date2, date3);

            StringBuilder sb_str = new StringBuilder();
            var result = "";
            //当天下午存在
            if (dttbTBLZHCFORECAST != null && dttbTBLZHCFORECAST.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dttbTBLZHCFORECAST);

                sb_str.Append(",{ \"type\":\"t26\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }

            //若当天数据不存在
            //1:获取海浪、气象数据
            //2:获取昨天责任海区
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "青岛近海", "渤海", "渤海海峡", "黄海北部", "黄海中部", "黄海南部" };

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "PM");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            //获取青岛近海48h,72h的波高，数据来源为下午二的填报数据
            //add by Lian start
            sql_TBLSDOFFSHORESEVENCITY24HWAVE qdjhfor2 = new sql_TBLSDOFFSHORESEVENCITY24HWAVE();
            TBLSDOFFSHORESEVENCITY24HWAVE modelfor2 = new TBLSDOFFSHORESEVENCITY24HWAVE();
            modelfor2.PUBLISHDATE = data;
            DataTable dtfor2 = (DataTable)qdjhfor2.get_TBLSDOFFSHORESEVENCITY24HWAVE_AllData(modelfor2);
            result += ",\"qdjhfor2\":";
            if(dtfor2 !=null && dtfor2.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtfor2);
            }
            else
            {
                result += "[{}]";
            }
            //add by Lian end

            sql_TBLSEAAREA24HWAVEFORECAST sql24 = new sql_TBLSEAAREA24HWAVEFORECAST();
            TBLSEAAREA24HWAVEFORECAST model24 = new TBLSEAAREA24HWAVEFORECAST();
            model24.PUBLISHDATE = data;
            DataTable dt24 = (DataTable)sql24.get_TBLSEAAREA24HWAVEFORECAST_AllData(model24);

            result += ",\"dt24\":";
            if (dt24 != null && dt24.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt24);
            }
            else
            {
                result += "[{}]";
            }

            sql_TBLSEAAREA48HWAVEFORECAST sql48 = new sql_TBLSEAAREA48HWAVEFORECAST();
            TBLSEAAREA48HWAVEFORECAST model48 = new TBLSEAAREA48HWAVEFORECAST();
            model48.PUBLISHDATE = data;
            DataTable dt48 = (DataTable)sql48.get_TBLSEAAREA48HWAVEFORECAST_AllData(model48);

            result += ",\"dt48\":";
            if (dt48 != null && dt48.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt48);
            }
            else
            {
                result += "[{}]";
            }

            sql_TBLSEAAREA72HWAVEFORECAST sql72 = new sql_TBLSEAAREA72HWAVEFORECAST();
            TBLSEAAREA72HWAVEFORECAST model72 = new TBLSEAAREA72HWAVEFORECAST();
            model72.PUBLISHDATE = data;
            DataTable dt72 = (DataTable)sql72.get_TBLSEAAREA72HWAVEFORECAST_AllData(model72);

            result += ",\"dt72\":";
            if (dt72 != null && dt72.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt72);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"dtZRArea\":";
            TBLZHCFORECAST_Model.PUBLISHDATE = data.AddDays(-1).AddHours(16);
            DataTable dtZRArea = new DataTable();
            dtZRArea = (System.Data.DataTable)new sql_TBLZHCFORECAST().GetZRArea(TBLZHCFORECAST_Model);
            if (dtZRArea != null && dtZRArea.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtZRArea);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t26\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单指挥处上午数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable44(DateTime data, string searchType = "p")
        {
            TBLZHCFORECAST TBLZHCFORECAST_Model = new TBLZHCFORECAST();
            TBLZHCFORECAST_Model.PUBLISHDATE = data.AddHours(7);

            System.Data.DataTable dttbTBLZHCFORECAST = new DataTable();

            System.Data.DataTable dttbTBLZHCFORECAST_bgd = new DataTable();//如果没有当天的数据，不固定海区取昨天的数据
            string date1 = data.AddDays(1).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");
            string date2 = data.AddDays(2).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");
            string date3 = data.AddDays(3).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");

            dttbTBLZHCFORECAST = (System.Data.DataTable)new sql_TBLZHCFORECAST().GETTBLZHCFORECASTBYFORCASEDATEALL(TBLZHCFORECAST_Model, date1, date2, date3);
            System.Data.DataTable dttbTBLZHCFORECAST_bgdhq = new DataTable();

            
            if (dttbTBLZHCFORECAST != null && dttbTBLZHCFORECAST.Rows.Count < 1)
            {
                TBLZHCFORECAST_Model.PUBLISHDATE = data.AddDays(-1).AddHours(16);
                dttbTBLZHCFORECAST_bgd = (System.Data.DataTable)new sql_TBLZHCFORECAST().GETTBLZHCFORECASTBYFORCASEDATEBYPUBLISH_bgd(TBLZHCFORECAST_Model);
            }
            else
            {
                var result = JsonMore.Serialize(dttbTBLZHCFORECAST);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t44\",\"pbtime\":\"today\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            if (dttbTBLZHCFORECAST_bgd != null && dttbTBLZHCFORECAST_bgd.Rows.Count > 0)
            {

                var result = JsonMore.Serialize(dttbTBLZHCFORECAST_bgd);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t44\", \"pbtime\":\"yesterday\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取3天海洋水文气象预报综述
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string gettable27(DateTime date)
        {
            sql_TBLZS sql = new sql_TBLZS();
            DataTable dt = (DataTable)sql.get_TBLSWQX_ZS_3DayS_OR_24HourS(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t27\",\"children\":[");
                sb_str.Append("{\"meteorologicalreview\":\"" + dt.Rows[0]["METEOROLOGICALREVIEW"].ToString()
                      + "\",\"meteorologicalreviewcx\":\"" + dt.Rows[0]["METEOROLOGICALREVIEWCX"].ToString()
                    + "\"},");
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";


               
            }
            return "";
        }

        /// <summary>
        /// 获取24小时水文气象预报综述
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable28(DateTime date)
        {
            sql_TBLZS sql = new sql_TBLZS();
            DataTable dt = (DataTable)sql.get_TBLSWQX_ZS_3DayS_OR_24HourS(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t28\",\"children\":[");
                sb_str.Append("{\"meteorologicalreview24hour\":\"" + dt.Rows[0]["METEOROLOGICALREVIEW24HOUR"].ToString()
                      + "\",\"meteorologicalreview24hourcx\":\"" + dt.Rows[0]["METEOROLOGICALREVIEW24HOURCX"].ToString()
                    + "\"},");
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }

        /// <summary>
        /// 7天渤海海区及黄河海港风、浪预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="searchType">p:填报日期 f:预报日期</param>
        /// <returns></returns>
        public string gettable29(DateTime date, string searchType)
        {
            TBLYRBHWINDWAVE72HFORECASTTWO model_01 = new TBLYRBHWINDWAVE72HFORECASTTWO();
            sql_TBLYRBHWINDWAVE7DAYFORECASTTWO sql_01 = new sql_TBLYRBHWINDWAVE7DAYFORECASTTWO();
            //判断当前时间所在周的周一日期即为周报发布时间
            //int t = 1 - Convert.ToInt32(date.DayOfWeek.ToString("d"));
            //DateTime weekPublishTime = new DateTime();
            //if (t == 1)
            //{
            //    weekPublishTime = date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d"))-7);
            //}
            //else
            //{
            //    //weekPublishTime = date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")));
            //}
            DateTime weekPublishTime = GetMondayDate(date);
            model_01.PUBLISHDATE = weekPublishTime;
            DataTable dt_01 = (DataTable)sql_01.GETTBLYRBHWINDWAVE7DAYFORECASTTWO(model_01, searchType);
            StringBuilder sb_str = new StringBuilder();
            if (dt_01!= null && dt_01.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t29\",\"pbtype\":\"bydb\",\"children\":[");
                for (int i = 0; i < dt_01.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt_01.Rows[i]["REPORTAREA"]
                        + "\",\"yb\":\"" + dt_01.Rows[i]["FORECASTDATE"]
                        + "\",\"bg\":\"" + dt_01.Rows[i]["YRBHWWFWAVEHEIGHT"]
                        + "\",\"bx\":\"" + dt_01.Rows[i]["YRBHWWFWAVEDIR"]
                        + "\",\"fx\":\"" + dt_01.Rows[i]["YRBHWWFFLOWDIR"]
                        + "\",\"fl\":\"" + dt_01.Rows[i]["YRBHWWFFLOWLEVEL"] 
                      + "\",\"sw\":\"" + dt_01.Rows[i]["YRBHWWFWATERTEMPERATURE"] + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "渤海", "黄河海港" };
            DataTable dtWindWave = new DataTable();
            dtWindWave = (DataTable)getBy_s.GetWeekData(weekPublishTime, FORECASTAREA);
            
            var result = "\"dtWindWave\":";
            if (dtWindWave != null && dtWindWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWindWave);
            }
            else
            {
                result += "[{}]";
            }
            
            sb_str.Append(",{ \"type\":\"t29\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
        public static DateTime GetMondayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。   
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }

        /// <summary>
        /// 获取7天海洋水文气象预报综述
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string gettable30(DateTime date)
        {
            sql_TBLZS sql = new sql_TBLZS();
            DataTable dt = (DataTable)sql.get_TBLSWQX_ZS_3DayS_OR_24HourS(date);
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t30\",\"children\":[");
                sb_str.Append("{\"meteorologicalreview7Days\":\"" + dt.Rows[0]["METEOROLOGICALREVIEW7DAYS"].ToString()
                     + "\",\"meteorologicalreview7Dayscx\":\"" + dt.Rows[0]["METEOROLOGICALREVIEW7DAYSCX"].ToString()
                    + "\"},");
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }

        /// <summary>
        /// 7天港口潮位预报
        /// </summary>
        /// <param name="date"></param>
        /// <param name="searchType">p:填报日期 f:预报日期</param>
        /// <returns></returns>
        public string gettable31(DateTime date)
        {
            int week = (int)date.DayOfWeek;//add by xp 2018-9-14 
            if (week == 1)//周一的“周报八”取将周日“上午二”的数据
            {
                return gettable31_week(date);
            }
            sql_TBLHARBOURTIDELEVEL7DAY sql = new sql_TBLHARBOURTIDELEVEL7DAY();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            //判断当前时间所在周的周一日期即为周报发布时间
            //DateTime weekPublishTime = date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")));
            DateTime weekPublishTime = GetMondayDate(date);
            model.PUBLISHDATE = weekPublishTime;
            DataTable dt = (DataTable)sql.GETTBLHARBOURTIDELEVEL7DAY(model);
            StringBuilder sb_str = new StringBuilder();
            int num = 0;
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t31\",\"children\":[");
                if (dt.Rows.Count == 7)
                {
                    num = 7;
                }
                else
                {
                    num = dt.Rows.Count;
                }
                for (int i = 0; i < num; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["HTLHARBOUR"]
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"g1c\":\"" + dt.Rows[i]["HTLFIRSTWAVETIDELEVEL"]
                        + "\",\"g1s\":\"" + dt.Rows[i]["HTLFIRSTWAVEOFTIME"]
                        + "\",\"d1c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"]
                        + "\",\"d1s\":\"" + dt.Rows[i]["HTLFIRSTTIMELOWTIDE"]
                        + "\",\"g2c\":\"" + dt.Rows[i]["HTLSECONDWAVETIDELEVEL"]
                        + "\",\"g2s\":\"" + dt.Rows[i]["HTLSECONDWAVEOFTIME"]
                        + "\",\"d2c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"]
                        + "\",\"d2s\":\"" + dt.Rows[i]["HTLSECONDTIMELOWTIDE"] + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 7天港口潮位预报 modify by xp 2018-9-3
        /// </summary>
        /// <param name="date"></param>
        /// <param name="searchType">p:填报日期 f:预报日期</param>
        /// <returns></returns>
        public string gettable31_week(DateTime date)
        {
            sql_TBLHARBOURTIDELEVEL7DAY sql = new sql_TBLHARBOURTIDELEVEL7DAY();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
           
            //DateTime weekPublishTime = GetMondayDate(date);
            //model.PUBLISHDATE = weekPublishTime;//周一
           
            model.PUBLISHDATE = date.AddDays(-1);//周日
            DataTable dt = (DataTable)sql.get_TBLHARBOURTIDELEVEL_AllData(model);//取周日上午二的 数据
              
            StringBuilder sb_str = new StringBuilder();
            
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t31\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["HTLHARBOUR"]
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"g1c\":\"" + dt.Rows[i]["HTLFIRSTWAVETIDELEVEL"]
                        + "\",\"g1s\":\"" + dt.Rows[i]["HTLFIRSTWAVEOFTIME"]
                        + "\",\"d1c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"]
                        + "\",\"d1s\":\"" + dt.Rows[i]["HTLFIRSTTIMELOWTIDE"]
                        + "\",\"g2c\":\"" + dt.Rows[i]["HTLSECONDWAVETIDELEVEL"]
                        + "\",\"g2s\":\"" + dt.Rows[i]["HTLSECONDWAVEOFTIME"]
                        + "\",\"d2c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"]
                        + "\",\"d2s\":\"" + dt.Rows[i]["HTLSECONDTIMELOWTIDE"] + "\"},");                
                }  
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }
              
        /// <summary>
        /// 获取东营胜利油田海温周报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable32(DateTime data)
        {
            sql_TBLSLYTWEEKFORECAST sql = new sql_TBLSLYTWEEKFORECAST();
            TBLSDOFFSHORESEVENCITY24HWAVE model = new TBLSDOFFSHORESEVENCITY24HWAVE();
            DateTime weekPublishTime = GetMondayDate(data);
            model.PUBLISHDATE = weekPublishTime;
            //model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GETTBLSLYTWEEKFORECAST(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t32\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"dl\":\"" + dt.Rows[i]["SDOSCWLOWESTWAVEHEIGHT"]
                        + "\",\"qy\":\"" + dt.Rows[i]["SDOSCWAREA"]
                        + "\",\"gl\":\"" + dt.Rows[i]["SDOSCWHIGHTESTWAVEHEIGHT"]
                        + "\",\"sw\":\"" + dt.Rows[i]["SDOSCWSURFACETEMPERATURE"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 十三、黄河海港附近海域风、浪预报
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettabe33(DateTime data, string searchType = "p")
        {
            sql_TBLHH3DAYSFORECAST sql = new sql_TBLHH3DAYSFORECAST();
            TBLYRSOUTHSEAWALL24WINDWAVE model = new TBLYRSOUTHSEAWALL24WINDWAVE();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            if (searchType == "f")
            {
                dt = (DataTable)sql.GETTBLHH3DAYSFORECAST(model, searchType);
                if (dt.Rows.Count < 1)
                {
                    //如果不存在数据，检索前一天对后两天的预报
                    dt = (DataTable)sql.GETTBLHH3DAYSFORECAST3DAYS(model);
                }
            }
            else
            {
                dt = (DataTable)sql.GETTBLHH3DAYSFORECAST(model);
            }
            if (dt.Rows.Count > 0)
            {
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t33\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"bg\":\"" + dt.Rows[i]["YRSSWWWAVEHEIGHT"]
                        + "\",\"bx\":\"" + dt.Rows[i]["YRSSWWWAVEDIRECTION"]
                        + "\",\"fx\":\"" + dt.Rows[i]["YRSSWWWINDDIRECTION"]
                        + "\",\"fl\":\"" + dt.Rows[i]["YRSSWWWINDFORCE"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 十三、72小时东营神仙沟挡潮闸专项预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe34(DateTime data, string searchType = "p")
        {
            sql_TBLSXGTIDELEVEL sql = new sql_TBLSXGTIDELEVEL();
            TBLMZZTIDELEVEL model = new TBLMZZTIDELEVEL();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            if (searchType == "f")
            {
                dt = (DataTable)sql.GETTBLSXGTIDELEVEL(model, searchType);
                if (dt.Rows.Count < 1)
                {
                    dt = (DataTable)sql.GETTBLSXGDATA(model);
                }
            }
            else
            {
                dt = (DataTable)sql.GETTBLSXGTIDELEVEL(model, searchType);
            }

            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t34\",\"children\":[");
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
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 表单35 渔政局表2
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType">p是查询，f是刷新</param>
        /// <returns></returns>
        public string gettable35(DateTime data, string searchType)
        {
            sql_TBLYZJFORECAST sql = new sql_TBLYZJFORECAST();
            TBLYZJFORECAST model = new TBLYZJFORECAST();
            model.PUBLISHDATE = data.AddHours(16);
            DataTable dt = (DataTable)sql.get_TBLYZJFORECAST(model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt !=null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                

                sb_str.Append(",{ \"type\":\"t35\", \"pbtime\":\"today\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            else
            {
                model.PUBLISHDATE = data.AddDays(-1).AddHours(16);
                dt = (DataTable)sql.get_TBLYZJFORECAST(model);
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            string[] FORECASTAREA = { "旅顺", "烟台近海", "威海近海", "石岛近海", "黄海北部", "黄海北部", "青岛近海" };

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "PM");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            result += ",\"dtZRArea\":";
            model.PUBLISHDATE = data.AddDays(-1).AddHours(16);
            DataTable dtZRArea = new DataTable();
            dtZRArea = (System.Data.DataTable)new sql_TBLYZJFORECAST().get_TBLYZJFORECAST(model);
            if (dtZRArea != null && dtZRArea.Rows.Count > 0)
            {
                //新建表格
                DataTable dtYZJ = new DataTable("dtYZJ");
                dtYZJ.Columns.Add("PUBLISHDATE", typeof(string));
                dtYZJ.Columns.Add("FORECASTDATE", typeof(string));
                dtYZJ.Columns.Add("SEAAREA", typeof(string));
                dtYZJ.Columns.Add("WINDDIRECTION", typeof(string));
                dtYZJ.Columns.Add("WINDFORCE", typeof(string));
                dtYZJ.Columns.Add("WAVEHEIGHT", typeof(string));
                

                foreach (DataRow rowArea in dtZRArea.Rows)
                {
                    var area = rowArea["SEAAREA"].ToString();
                    if (area != "旅顺" && area != "烟台" && area != "威海" && area != "石岛" && area != "青岛" && area != "责任海区1" && area != "责任海区2")
                    {
                        DataRow rowAreaDt = dtYZJ.NewRow();
                        rowAreaDt["PUBLISHDATE"] = rowArea["PUBLISHDATE"].ToString();
                        rowAreaDt["FORECASTDATE"] = rowArea["FORECASTDATE"].ToString();
                        rowAreaDt["SEAAREA"] = rowArea["SEAAREA"].ToString();
                        rowAreaDt["WINDDIRECTION"] = rowArea["WINDDIRECTION"].ToString();
                        rowAreaDt["WINDFORCE"] = rowArea["WINDFORCE"].ToString();
                        rowAreaDt["WAVEHEIGHT"] = rowArea["WAVEHEIGHT"].ToString();
                        dtYZJ.Rows.Add(rowAreaDt);
                    }
                }

                if (dtYZJ!=null && dtYZJ.Rows.Count > 0)
                {
                    result += JsonMore.Serialize(dtYZJ);
                }
                else
                {
                    result += "[{}]";
                }
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t35\",\"pbtime\":\"yesterday\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();

        }

        /// <summary>
        /// 上午渔政局表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType">p是查询，f是刷新</param>
        /// <returns></returns>
        public string gettable45(DateTime data, string searchType)
        {
            sql_TBLYZJFORECAST sql = new sql_TBLYZJFORECAST();
            TBLYZJFORECAST model = new TBLYZJFORECAST();
            model.PUBLISHDATE = data.AddHours(7);
            DataTable dt = (DataTable)sql.get_TBLYZJFORECAST(model);
            if (dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);

                StringBuilder sb_str = new StringBuilder();

                sb_str.Append(",{ \"type\":\"t45\", \"pbtime\":\"today\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            else { 
                model.PUBLISHDATE = data.AddDays(-1).AddHours(16);
                dt = (DataTable)sql.get_TBLYZJFORECAST(model);
            }
            if (dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t45\", \"pbtime\":\"yesterday\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 上午九、潍坊港24小时潮汐预报 取消不用合并到上午八潍坊港24小时 100710 edit by yuy
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable36(DateTime data)
        {
            sql_TBLWF24HTIDALFORECASTAM sql = new sql_TBLWF24HTIDALFORECASTAM();
            TBLWF24HTIDALFORECASTAM model = new TBLWF24HTIDALFORECASTAM();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLWF24HTIDALFORECASTAM_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t16\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"s1\":\"" + dt.Rows[i]["WF24HTFFIRSTHIGHWAVETIME"]
                    + "\",\"g1\":\"" + dt.Rows[i]["WF24HTFFIRSTHIGHWAVEHEIGHT"]
                        + "\",\"s2\":\"" + dt.Rows[i]["WF24HTFSECONDHIGHWAVETIME"]
                    + "\",\"g2\":\"" + dt.Rows[i]["WF24HTFSECONDHIGHWAVEHEIGHT"]
                        + "\",\"ds1\":\"" + dt.Rows[i]["WF24HTFFIRSTLOWWAVETIME"]
                        + "\",\"dg1\":\"" + dt.Rows[i]["WF24HTFFIRSTLOWWAVEHEIGHT"]
                        + "\",\"ds2\":\"" + dt.Rows[i]["WF24HTFSECONDLOWWAVETIME"]
                        + "\",\"dg2\":\"" + dt.Rows[i]["WF24HTFSECONDLOWWAVEHEIGHT"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 上午七、海上丝绸之路三天海浪、气象预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe39(DateTime data, string searchType)
        {
            sql_SilkWaveAndTide sql_01 = new sql_SilkWaveAndTide();
            TBLYRBHWINDWAVE72HFORECASTTWO model_01 = new TBLYRBHWINDWAVE72HFORECASTTWO();
            searchType = "f";
            model_01.PUBLISHDATE = data;
            model_01.FORECASTDATE = data;
            DataTable dt = (DataTable)sql_01.GetSilkWave(model_01);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count < 1)
            {
                dt = (DataTable)sql_01.GetSilkWaveLast(model_01);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t39\",\"pbtype\":\"bydb\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["REPORTAREA"]
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]
                        + "\",\"bg\":\"" + dt.Rows[i]["YRBHWWFWAVEHEIGHT"]
                        + "\",\"bx\":\"" + dt.Rows[i]["YRBHWWFWAVEDIR"]
                        + "\",\"fx\":\"" + dt.Rows[i]["YRBHWWFFLOWDIR"]
                        + "\",\"fl\":\"" + dt.Rows[i]["YRBHWWFFLOWLEVEL"] 
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }

            //获取海浪、气象数据
            DataTable dtWaveWind = new DataTable();
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "青岛近海", "潍坊近海", "营口港" };
            //string[] FORECASTAREA = { "青岛近海", "潍坊近海", "东营近海" };
            dtWaveWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            var result = "\"windwave\":"; //海浪、气象
            if (dtWaveWind != null && dtWaveWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWaveWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t39\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 上午八、海上丝绸之路三天潮汐预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe38(DateTime data, string searchType,string Type)
        {
            int week = (int)data.DayOfWeek;
            if (Type == "Week")
            {
                if (week == 1)
                {
                    return gettabe38_week(data, searchType);//周一的周报五
                }
            }
            if (Type == "AM")
            {
                if (week == 2)
                {
                    return gettabe38_week2(data, searchType);//周二的上午八
                }
            }

            sql_SilkWaveAndTide sql = new sql_SilkWaveAndTide();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = (DataTable)sql.GetSilkTide(model);
            if (dt.Rows.Count < 1)
            {
                dt = (DataTable)sql.GetSilkTideLast(model);
            }
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t38\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["HTLHARBOUR"]//港口
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]//预报日期
                        + "\",\"g1c\":\"" + dt.Rows[i]["HTLFIRSTWAVETIDELEVEL"]//第一次高潮潮位
                        + "\",\"g1s\":\"" + dt.Rows[i]["HTLFIRSTWAVEOFTIME"]//第一次高潮时间
                        + "\",\"d1c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"]//第一次低潮潮位
                        + "\",\"d1s\":\"" + dt.Rows[i]["HTLFIRSTTIMELOWTIDE"]//第一次低潮时间
                        + "\",\"g2c\":\"" + dt.Rows[i]["HTLSECONDWAVETIDELEVEL"]//第二次高潮潮位
                        + "\",\"g2s\":\"" + dt.Rows[i]["HTLSECONDWAVEOFTIME"]//第二次高潮时间
                        + "\",\"d2c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"]//第二次低潮潮位
                        + "\",\"d2s\":\"" + dt.Rows[i]["HTLSECONDTIMELOWTIDE"] + "\"},");//第二次低潮时间
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 上午八、海上丝绸之路三天潮汐预报  周报 modify by xp 2018-9-5
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe38_week(DateTime data, string searchType)
        {
            sql_SilkWaveAndTide sql = new sql_SilkWaveAndTide();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            model.PUBLISHDATE = data.AddDays(-1);
            model.FORECASTDATE = data;
            DataTable dt = (DataTable)sql.GetSilkTide_Week(model);//取周日的上午八
                      
            StringBuilder sb_str = new StringBuilder();          
            if (dt.Rows.Count >0)
            {
                sb_str.Append(",{ \"type\":\"t38\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["HTLHARBOUR"]//港口
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]//预报日期
                        + "\",\"g1c\":\"" + dt.Rows[i]["HTLFIRSTWAVETIDELEVEL"]//第一次高潮潮位
                        + "\",\"g1s\":\"" + dt.Rows[i]["HTLFIRSTWAVEOFTIME"]//第一次高潮时间
                        + "\",\"d1c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"]//第一次低潮潮位
                        + "\",\"d1s\":\"" + dt.Rows[i]["HTLFIRSTTIMELOWTIDE"]//第一次低潮时间
                        + "\",\"g2c\":\"" + dt.Rows[i]["HTLSECONDWAVETIDELEVEL"]//第二次高潮潮位
                        + "\",\"g2s\":\"" + dt.Rows[i]["HTLSECONDWAVEOFTIME"]//第二次高潮时间
                        + "\",\"d2c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"]//第二次低潮潮位
                        + "\",\"d2s\":\"" + dt.Rows[i]["HTLSECONDTIMELOWTIDE"] + "\"},");//第二次低潮时间                  
                }                            
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }
        }
        /// <summary>周二的上午八
        /// 上午八、海上丝绸之路三天潮汐预报  周报 modify by xp 2018-9-10
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettabe38_week2(DateTime data, string searchType)
        {
            sql_SilkWaveAndTide sql = new sql_SilkWaveAndTide();
            TBLHARBOURTIDELEVEL model = new TBLHARBOURTIDELEVEL();
            model.PUBLISHDATE = data.AddDays(-1);//转到周一
            //model.FORECASTDATE = data;
            DataTable dt = (DataTable)sql.GetSilkTide_Week(model);//取后两天
            
            StringBuilder sb_str = new StringBuilder();         
            if (dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t38\",\"children\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{ \"qy\":\"" + dt.Rows[i]["HTLHARBOUR"]//港口
                        + "\",\"yb\":\"" + dt.Rows[i]["FORECASTDATE"]//预报日期
                        + "\",\"g1c\":\"" + dt.Rows[i]["HTLFIRSTWAVETIDELEVEL"]//第一次高潮潮位
                        + "\",\"g1s\":\"" + dt.Rows[i]["HTLFIRSTWAVEOFTIME"]//第一次高潮时间
                        + "\",\"d1c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHEFIRSTTIME"]//第一次低潮潮位
                        + "\",\"d1s\":\"" + dt.Rows[i]["HTLFIRSTTIMELOWTIDE"]//第一次低潮时间
                        + "\",\"g2c\":\"" + dt.Rows[i]["HTLSECONDWAVETIDELEVEL"]//第二次高潮潮位
                        + "\",\"g2s\":\"" + dt.Rows[i]["HTLSECONDWAVEOFTIME"]//第二次高潮时间
                        + "\",\"d2c\":\"" + dt.Rows[i]["HTLLOWTIDELEVELFORTHESECONDTIM"]//第二次低潮潮位
                        + "\",\"d2s\":\"" + dt.Rows[i]["HTLSECONDTIMELOWTIDE"] + "\"},");//第二次低潮时间
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            else
            {
                return "";
            }          
        }



        /// <summary>
        /// 上午潍坊24小时海浪、水温
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable37(DateTime data)
        {
            Sql_HT_TBLWF24HWAVEFORECAST sql = new Sql_HT_TBLWF24HWAVEFORECAST();
            HT_TBLWF24HWAVEFORECAST model = new HT_TBLWF24HWAVEFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLWF24HWAVEFORECAST_AllData(model);
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t37\",\"pbtype\":\"bydb\",\"children\":[");
                int dbSWRowsNULLCount = 0;
                //判断海温数据是否为null
                //若为null
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if (Convert.ToString(dt.Rows[sw]["SA24HWFOFFSHORESW"]).Trim() == "" || DBNull.Value == dt.Rows[sw]["SA24HWFOFFSHORESW"])
                    {
                        dbSWRowsNULLCount++;
                    }
                }
                if(dbSWRowsNULLCount > 0)
                {
                    string[] areasw = { "潍坊" };

                    DataTable dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, areasw);

                    if (dtsw != null && dtsw.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            sb_str.Append("{\"hl1\":\"" + dt.Rows[j]["SA24HWFBOHAIWAVEHEIGHT"]
                   + "\",\"hl2\":\"" + dt.Rows[j]["SA24HWFNORTHOFYSWAVEHEIGHT"]
                       + "\",\"hl3\":\"" + dt.Rows[j]["SA24HWFMIDDLEOFYSWAVEHEIGHT"]
                   + "\",\"hl4\":\"" + dt.Rows[j]["SA24HWFSOUTHOFYSWAVEHEIGHT"]
                       + "\",\"hl5\":\"" + dt.Rows[j]["SA24HWFOFFSHOREWAVEHEIGHT"]
                       + "\",\"sw\":\"" + dtsw.Rows[j]["MEAN_24H"]
                       + "\"},");
                        }
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                    
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb_str.Append("{\"hl1\":\"" + dt.Rows[i]["SA24HWFBOHAIWAVEHEIGHT"]
                    + "\",\"hl2\":\"" + dt.Rows[i]["SA24HWFNORTHOFYSWAVEHEIGHT"]
                        + "\",\"hl3\":\"" + dt.Rows[i]["SA24HWFMIDDLEOFYSWAVEHEIGHT"]
                    + "\",\"hl4\":\"" + dt.Rows[i]["SA24HWFSOUTHOFYSWAVEHEIGHT"]
                        + "\",\"hl5\":\"" + dt.Rows[i]["SA24HWFOFFSHOREWAVEHEIGHT"]
                        + "\",\"sw\":\"" + dt.Rows[i]["SA24HWFOFFSHORESW"]
                        + "\"},");
                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            //获取海浪、气象数据
            DataTable dtWaveWind = new DataTable();
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();
            string[] FORECASTAREA = { "渤海", "黄海北部", "黄海中部", "黄海南部", "潍坊近海" };
            dtWaveWind = (DataTable)getBy_s.GetWaveAndWindData(data, FORECASTAREA, "AM");

            //获取水温数据
            //model.PUBLISHDATE = data.AddDays(-1);
            //dt = (DataTable)sql.get_TBLWF24HWAVEFORECAST_AllData(model);

            string[] area = { "潍坊" };
            
            dt = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);

            var result = "\"windwave\":"; //海浪、气象
            if (dtWaveWind != null && dtWaveWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWaveWind);
            }
            else
            {
                result += "[{}]";
            }
            result += ",\"sw\":";//水温
            if (dt != null && dt.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dt);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t37\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }
        /// <summary>
        /// 东营埕岛-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable41(DateTime data, string searchType)
        {
            HT_DYWAVEFORECAST model = new HT_DYWAVEFORECAST();
            Sql_DYWAVEFOREAST sql = new Sql_DYWAVEFOREAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetDyWaveForecastData(model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                sb_str.Append(",{ \"type\":\"t41\",\"pbtime\":\"today\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, "埕岛近海", "AM");

            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "埕岛近海", "PM");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t41\",\"pbtime\":\"yesterday\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();

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
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.GetDyTideForecastData(model);
            }

            if (dt !=null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t42\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 东营埕岛预报单编号
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable43(DateTime data, string searchType)
        {
            Sql_DYNO sql = new Sql_DYNO();
            DataTable dt = (DataTable)sql.GetDYNo(data);
            StringBuilder sb_str = new StringBuilder();
            if (dt.Rows.Count < 1)
            {
                data = data.AddDays(-1);
                dt = (DataTable)sql.GetDYNo(data);
                if (dt.Rows.Count > 0)
                {
                    sb_str.Append(",{ \"type\":\"t43\", \"pbtime\":\"yesterday\",\"children\":");
                }
            }
            else
            {
                sb_str.Append(",{ \"type\":\"t43\", \"pbtime\":\"today\",\"children\":");
            }
            if (dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }

            else
            {
                return "";
            }
        }

        /// <summary>
        /// 表单01.一、海洋牧场-海浪预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string gettabe47(DateTime date)
        {
            OceanRanchWave oceanRanchWave = new OceanRanchWave();
            oceanRanchWave.PUBLISHDATE = date;
            sql_OceanRanchWave sql = new sql_OceanRanchWave();
            DataTable dt = (DataTable)sql.GetOceanRanchWaveList(oceanRanchWave);//获取OCEANRANCH72HWAVE_T当天保存的数据
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t47\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            dt = (DataTable)sql.GetOceanRanchWaveListBy_S(oceanRanchWave);//获取OCEANRANCH72HWAVE_S表中解析数据
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t47\",\"pbtype\":\"bys\",\"children\":");
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
        private string gettabe48(DateTime date)
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

        //private string gettabe48(DateTime date,int param)
        //{
        //    OceanRanchTide oceanRanchTide = new OceanRanchTide();
        //    sql_OceanRanchTide sql = new sql_OceanRanchTide();
        //    oceanRanchTide.PUBLISHDATE = date;
        //    DataTable dt = (DataTable)sql.GetOceanRanchTideList(oceanRanchTide);//获取OCEANRANCH72HWAVE_T当天保存的数据
        //    StringBuilder sb_str = new StringBuilder();
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        var result = JsonMore.Serialize(dt);
        //        sb_str.Append(",{ \"type\":\"t83\",\"children\":");
        //        sb_str.Append(result);
        //        sb_str.Append("}");
        //        return sb_str.ToString();
        //    }else
        //    {
        //        return "";
        //    }
            
        //}


        /// <summary>
        /// 表单03.三、海洋牧场-海温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string gettabe49(DateTime date)
        {
            OceanRanchTemp oceanRanchTemp = new OceanRanchTemp();
            sql_OceanRanchTemp sql = new sql_OceanRanchTemp();
            oceanRanchTemp.PUBLISHDATE = date;
            DataTable dt = (DataTable)sql.GetOceanRanchTempList(oceanRanchTemp);//获取OCEANRANCH72HTEMPERATURE_T当天保存的数据
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t49\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            dt = (DataTable)sql.GetOceanRanchTempListBy_S(oceanRanchTemp);//获取OCEANRANCH72HTEMPERATURE_S表中解析数据
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t49\",\"pbtype\":\"bys\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }

        /// <summary>
        /// 表单50.烟台南部海浪、水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable50(DateTime data)
        {
            Sql_YT_WaveForecast sql = new Sql_YT_WaveForecast();
            YT_WaveForecast model = new YT_WaveForecast();
            model.PUBLISHDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.GetWaveDataBy_T(model);
            StringBuilder sb_str = new StringBuilder();
            GetWaterTemperature waterTemperature = new GetWaterTemperature();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                int dbSWRowsNULLCount = 0;
                //判断海温数据是否为null
                //若为null
               
                for (int sw = 0; sw < dt.Rows.Count; sw++)
                {
                    if (Convert.ToString(dt.Rows[sw]["WATERTEMPERATURE"]).Trim() == "" || DBNull.Value == dt.Rows[sw]["WATERTEMPERATURE"])
                    {
                        dbSWRowsNULLCount++;
                    }
                }
                if(dbSWRowsNULLCount > 0)
                {
                    string[] swarea = { "南黄岛" };
                    DataTable swdt = (DataTable)waterTemperature.GetWaterTemperatureData(data, swarea);
                    if(swdt!=null && swdt.Rows.Count > 0)
                    {
                        sb_str.Append(",{ \"type\":\"t50\",\"pbtype\":\"bydb\",\"children\":[");
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[j]["PUBLISHDATE"]
                                + "\",\"FORECASTDATE\":\"" + dt.Rows[j]["FORECASTDATE"]
                                + "\",\"WAVELEVELONE\":\"" + dt.Rows[j]["WAVELEVELONE"]
                                + "\",\"WAVELEVELTYPE\":\"" + dt.Rows[j]["WAVELEVELTYPE"]
                                + "\",\"WAVEDIRECTION\":\"" + dt.Rows[j]["WAVEDIRECTION"]
                                + "\",\"WATERTEMPERATURE\":\"" + swdt.Rows[j]["MEAN_24H"]);
                        }
                        sb_str.Append("\"},");
                        return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
                    }
                }
                sb_str.Append(",{ \"type\":\"t50\",\"pbtype\":\"bydb\",\"children\":");
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            dt = (DataTable)sql.GetWaveDataBy_S(model);
            DataTable dtsw = new DataTable();
            DataTable dtWind = new DataTable();
            dtWind = (DataTable)sql.GetWindDataBy_S(model);
            //model.PUBLISHDATE = data.AddDays(-1);
            //dtsw = (DataTable)sql.GetWaveDataBy_T(model);
            string[] area = { "南黄岛" };
            dtsw = (DataTable)waterTemperature.GetWaterTemperatureData(data, area);
            //拼接
            DataTable dtNew = new DataTable();
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
            dtNew.Columns.Add("WATERTEMPERATURE", typeof(string));
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
            rowNew["WATERTEMPERATURE"] = "";
            dtNew.Rows.Add(rowNew);
            //添加海浪数据
            if (dt != null && dt.Rows.Count > 0)
            {
                //DataRow rowNew = dtNew.NewRow();
                rowNew["PUBLISHDATE"] = dt.Rows[0]["PUBLISHDATE"];
                rowNew["FORECASTAREA"] = dt.Rows[0]["FORECASTAREA"].ToString();
                rowNew["WAVE24FORECAST"] = dt.Rows[0]["WAVE24FORECAST"].ToString();
                
            }
            if (dtsw != null && dtsw.Rows.Count > 0)
            {
                rowNew["WATERTEMPERATURE"] = dtsw.Rows[0]["MEAN_24H"].ToString();
            }
            if(dtWind != null && dtWind.Rows.Count > 0)
            {
                rowNew["PUBLISHDATE"] = dtWind.Rows[0]["PUBLISHDATE"];
                rowNew["FORECASTAREA"] = dtWind.Rows[0]["FORECASTAREA"].ToString();
                rowNew["WINDDIRECTION24FORECAST"] = dtWind.Rows[0]["WINDDIRECTION24FORECAST"].ToString();
            }

            result = JsonMore.Serialize(dtNew);
            sb_str.Append(",{ \"type\":\"t50\",\"pbtype\":\"bys\",\"children\":");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 表单51.海阳近岸海域潮汐预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable51(DateTime data)
        {
            Sql_YT_TideForecast sql = new Sql_YT_TideForecast();
            YT_TideForecast model = new YT_TideForecast();
            model.PUBLISHDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.GetTideDataBy_T(model);
            StringBuilder sb_str = new StringBuilder();
            if (dt != null &&  dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t51\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }

        /// <summary>
        /// 表单52.海阳万米海滩海水浴场风、浪预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable52(DateTime data)
        {
            Sql_YT_YC sql = new Sql_YT_YC();
            YT_YC model = new YT_YC();
            model.PUBLISHDATE = data;
            DataTable dt = new DataTable();
            DataTable dtWave = new DataTable();
            DataTable dtLast = new DataTable();
            dt = (DataTable)sql.GetYcDataBy_T(model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);
                sb_str.Append(",{ \"type\":\"t52\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }

            dt = (DataTable)sql.GetYcDataBy_S(model);
            dtWave = (DataTable)sql.GetYcWaveDataBy_S(model);
            model.PUBLISHDATE = data.AddDays(-1);
            dtLast = (DataTable)sql.GetYcDataBy_T(model);
            //拼接
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("PUBLISHDATE", typeof(DateTime));
            //dtNew.Columns.Add("FORECASTAREA", typeof(string));
            dtNew.Columns.Add("WINDFORCE24FORECAST", typeof(string));
            dtNew.Columns.Add("WINDFORCE48FORECAST", typeof(string));
            dtNew.Columns.Add("WINDFORCE72FORECAST", typeof(string));
            dtNew.Columns.Add("WINDDIRECTION24FORECAST", typeof(string));
            dtNew.Columns.Add("WINDDIRECTION48FORECAST", typeof(string));
            dtNew.Columns.Add("WINDDIRECTION72FORECAST", typeof(string));
            dtNew.Columns.Add("WAVE24FORECAST", typeof(string)); 

            dtNew.Columns.Add("WAVE48FORECAST", typeof(string));
            dtNew.Columns.Add("WAVE72FORECAST", typeof(string));
            dtNew.Columns.Add("WEATERSTATE", typeof(string));
            dtNew.Columns.Add("TEMPERATURE", typeof(string));
            DataRow rowNew = dtNew.NewRow();
            rowNew["PUBLISHDATE"] = DateTime.Now;
            //rowNew["FORECASTAREA"] = "";
            rowNew["WINDFORCE24FORECAST"] = "";
            rowNew["WINDFORCE48FORECAST"] = "";
            rowNew["WINDFORCE72FORECAST"] = "";
            rowNew["WINDDIRECTION24FORECAST"] = "";
            rowNew["WINDDIRECTION48FORECAST"] = "";
            rowNew["WINDDIRECTION72FORECAST"] = "";
            rowNew["WAVE24FORECAST"] = "";
            rowNew["WAVE48FORECAST"] = "";
            rowNew["WAVE72FORECAST"] = "";
            rowNew["WEATERSTATE"] = "";
            rowNew["TEMPERATURE"] = "";
            dtNew.Rows.Add(rowNew);
            //添加海浪数据
            if (dt != null && dt.Rows.Count > 0)
            {
                rowNew["PUBLISHDATE"] = dt.Rows[0]["PUBLISHDATE"];
                //rowNew["FORECASTAREA"] = dt.Rows[0]["FORECASTAREA"].ToString();
                rowNew["WINDFORCE24FORECAST"] = dt.Rows[0]["WINDFORCE24FORECAST"].ToString();
                rowNew["WINDDIRECTION24FORECAST"] = dt.Rows[0]["WINDDIRECTION24FORECAST"].ToString();
            }
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                rowNew["PUBLISHDATE"] = dtWave.Rows[0]["PUBLISHDATE"];
                //rowNew["FORECASTAREA"] = dtWave.Rows[0]["FORECASTAREA"].ToString();
                rowNew["WAVE24FORECAST"] = dtWave.Rows[0]["WAVE24FORECAST"].ToString();
            }
            if (dtLast != null && dtLast.Rows.Count > 0)
            {
                rowNew["TEMPERATURE"] = dtLast.Rows[0]["TEMPERATURE"].ToString();
            }
            result = JsonMore.Serialize(dtNew);
            sb_str.Append(",{ \"type\":\"t52\",\"pbtype\":\"bys\",\"children\":");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 山东省7地市预报编号：
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable53(DateTime data, string searchType)
        {
            Sql_SDSEVENNO sql = new Sql_SDSEVENNO();
            DataTable dt = (DataTable)sql.GetSDSevenNO(data);
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count < 1)
            {
                data = data.AddDays(-1);
                dt = (DataTable)sql.GetSDSevenNO(data);
                if (dt.Rows.Count > 0)
                {
                    sb_str.Append(",{ \"type\":\"t53\", \"pbtime\":\"yesterday\",\"children\":");
                }
            }
            else
            {
                sb_str.Append(",{ \"type\":\"t53\", \"pbtime\":\"today\",\"children\":");
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }

            else
            {
                return "";
            }
        }

        #region  七地市潮汐预报
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
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.GetTideData("DYGLFP", model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t54\",\"children\":");
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
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.GetTideData("RZTHD", model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t57\",\"children\":");
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
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.GetTideData("WFDJQ", model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t60\",\"children\":");
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
                sb_str.Append(",{ \"type\":\"t63\",\"children\":");
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
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.GetTideData("YTQQ", model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t66\",\"children\":");
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
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.GetTideData("DJKP", model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t69\",\"children\":");
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
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.GetTideData("DYFP", model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var result = JsonMore.Serialize(dt);
                StringBuilder sb_str = new StringBuilder();
                sb_str.Append(",{ \"type\":\"t72\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            return "";
        }
        #endregion

        #region 七地市海温预报

        /// <summary>
        /// 下午二十六、东营广利渔港-未来三天的海面水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable56(DateTime data)
        {
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTemperatureData("DYGLFP", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t56\",\"pbtime\":\"today\",\"children\":");
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }

            //若当天未保存数据，从海洋牧场中获取数据
            dt = (DataTable)sql.GetTemperatureDataFormOCEAN("东营广利一级渔港", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t56\",\"pbtime\":\"yesterday\",\"children\":[");
                DateTime forecastDate = data;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var WATERTEMPERATURE = "";
                        switch (j)
                        {
                            case 0:
                                WATERTEMPERATURE = dt.Rows[i]["Min48"].ToString() + "~" + dt.Rows[i]["Max48"].ToString();
                                break;
                            case 1:
                                WATERTEMPERATURE = dt.Rows[i]["Min72"].ToString() + "~" + dt.Rows[i]["Max72"].ToString();
                                break;
                            case 2:
                                WATERTEMPERATURE = dt.Rows[i]["Min96"].ToString() + "~" + dt.Rows[i]["Max96"].ToString();
                                break;
                        }
                        forecastDate = forecastDate.AddDays(1);
                        sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                            + "\",\"FORECASTDATE\":\"" + forecastDate
                            + "\",\"WATERTEMPERATURE\":\"" + WATERTEMPERATURE
                            + "\"},");
                    }

                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }


        /// <summary>
        /// 下午二十九、日照桃花岛-未来三天的海面水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable59(DateTime data)
        {
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTemperatureData("RZTHD", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t59\",\"pbtime\":\"today\",\"children\":");
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            
            //若当天未保存数据，从海洋牧场中获取数据
            dt = (DataTable)sql.GetTemperatureDataFormOCEAN("日照桃花岛", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t59\",\"pbtime\":\"yesterday\",\"children\":[");
                DateTime forecastDate = data;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var WATERTEMPERATURE = "";
                        switch (j)
                        {
                            case 0:
                                WATERTEMPERATURE = dt.Rows[i]["Min48"].ToString() + "~" + dt.Rows[i]["Max48"].ToString();
                                break;
                            case 1:
                                WATERTEMPERATURE = dt.Rows[i]["Min72"].ToString() + "~" + dt.Rows[i]["Max72"].ToString();
                                break;
                            case 2:
                                WATERTEMPERATURE = dt.Rows[i]["Min96"].ToString() + "~" + dt.Rows[i]["Max96"].ToString();
                                break;
                        }
                        forecastDate = forecastDate.AddDays(1);
                        sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                            + "\",\"FORECASTDATE\":\"" + forecastDate
                            + "\",\"WATERTEMPERATURE\":\"" + WATERTEMPERATURE
                            + "\"},");
                    }

                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }

        /// <summary>
        /// 下午三十二、潍坊度假区-未来三天的海面水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable62(DateTime data)
        {
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTemperatureData("WFDJQ", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t62\",\"pbtime\":\"today\",\"children\":");
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            //若当天未保存数据，从海洋牧场中获取数据
            dt = (DataTable)sql.GetTemperatureDataFormOCEAN("潍坊旅游度假区", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t62\",\"pbtime\":\"yesterday\",\"children\":[");
                DateTime forecastDate = data;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var WATERTEMPERATURE = "";
                        switch (j)
                        {
                            case 0:
                                WATERTEMPERATURE = dt.Rows[i]["Min48"].ToString() + "~" + dt.Rows[i]["Max48"].ToString();
                                break;
                            case 1:
                                WATERTEMPERATURE = dt.Rows[i]["Min72"].ToString() + "~" + dt.Rows[i]["Max72"].ToString();
                                break;
                            case 2:
                                WATERTEMPERATURE = dt.Rows[i]["Min96"].ToString() + "~" + dt.Rows[i]["Max96"].ToString();
                                break;
                        }
                        forecastDate = forecastDate.AddDays(1);
                        sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                            + "\",\"FORECASTDATE\":\"" + forecastDate
                            + "\",\"WATERTEMPERATURE\":\"" + WATERTEMPERATURE
                            + "\"},");
                    }

                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }


        /// <summary>
        /// 下午三十五、威海新区-未来三天的海面水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable65(DateTime data)
        {
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTemperatureData("WHXQ", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t65\",\"pbtime\":\"today\",\"children\":");
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }

            //若当天未保存数据，从海洋牧场中获取数据
            dt = (DataTable)sql.GetTemperatureDataFormOCEAN("威海南海新区", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t65\",\"pbtime\":\"yesterday\",\"children\":[");
                DateTime forecastDate = data;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var WATERTEMPERATURE = "";
                        switch (j)
                        {
                            case 0:
                                WATERTEMPERATURE = dt.Rows[i]["Min48"].ToString() + "~" + dt.Rows[i]["Max48"].ToString();
                                break;
                            case 1:
                                WATERTEMPERATURE = dt.Rows[i]["Min72"].ToString() + "~" + dt.Rows[i]["Max72"].ToString();
                                break;
                            case 2:
                                WATERTEMPERATURE = dt.Rows[i]["Min96"].ToString() + "~" + dt.Rows[i]["Max96"].ToString();
                                break;
                        }
                        forecastDate = forecastDate.AddDays(1);
                        sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                            + "\",\"FORECASTDATE\":\"" + forecastDate
                            + "\",\"WATERTEMPERATURE\":\"" + WATERTEMPERATURE
                            + "\"},");
                    }

                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }

        /// <summary>
        /// 下午三十八、烟台清泉-未来三天的海面水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable68(DateTime data)
        {
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTemperatureData("YTQQ", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t68\",\"pbtime\":\"today\",\"children\":");
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            
            //若当天未保存数据，从海洋牧场中获取数据
            dt = (DataTable)sql.GetTemperatureDataFormOCEAN("烟台清泉码头", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t68\",\"pbtime\":\"yesterday\",\"children\":[");
                DateTime forecastDate = data;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var WATERTEMPERATURE = "";
                        switch (j)
                        {
                            case 0:
                                WATERTEMPERATURE = dt.Rows[i]["Min48"].ToString() + "~" + dt.Rows[i]["Max48"].ToString();
                                break;
                            case 1:
                                WATERTEMPERATURE = dt.Rows[i]["Min72"].ToString() + "~" + dt.Rows[i]["Max72"].ToString();
                                break;
                            case 2:
                                WATERTEMPERATURE = dt.Rows[i]["Min96"].ToString() + "~" + dt.Rows[i]["Max96"].ToString();
                                break;
                        }
                        forecastDate = forecastDate.AddDays(1);
                        sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                            + "\",\"FORECASTDATE\":\"" + forecastDate
                            + "\",\"WATERTEMPERATURE\":\"" + WATERTEMPERATURE
                            + "\"},");
                    }

                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }

        /// <summary>
        /// 下午四十一、董家口-未来三天的海面水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable71(DateTime data)
        {
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTemperatureData("DJKP", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t71\",\"pbtime\":\"today\",\"children\":");
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }

            //若当天未保存数据，从海洋牧场中获取数据
            dt = (DataTable)sql.GetTemperatureDataFormOCEAN("董家口", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t71\",\"pbtime\":\"yesterday\",\"children\":[");
                DateTime forecastDate = data;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var WATERTEMPERATURE = "";
                        switch (j)
                        {
                            case 0:
                                WATERTEMPERATURE = dt.Rows[i]["Min48"].ToString() + "~" + dt.Rows[i]["Max48"].ToString();
                                break;
                            case 1:
                                WATERTEMPERATURE = dt.Rows[i]["Min72"].ToString() + "~" + dt.Rows[i]["Max72"].ToString();
                                break;
                            case 2:
                                WATERTEMPERATURE = dt.Rows[i]["Min96"].ToString() + "~" + dt.Rows[i]["Max96"].ToString();
                                break;
                        }
                        forecastDate = forecastDate.AddDays(1);
                        sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                            + "\",\"FORECASTDATE\":\"" + forecastDate
                            + "\",\"WATERTEMPERATURE\":\"" + WATERTEMPERATURE
                            + "\"},");
                    }

                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }

        /// <summary>
        /// 下午四十四、东营渔港-未来三天的海面水温预报
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable74(DateTime data)
        {
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            TBLSEVENTEMPERATURE model = new TBLSEVENTEMPERATURE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetTemperatureData("DYFP", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t74\",\"pbtime\":\"today\",\"children\":");
                result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }

            //若当天未保存数据，从海洋牧场中获取数据
            dt = (DataTable)sql.GetTemperatureDataFormOCEAN("东营渔港", model);
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t74\",\"pbtime\":\"yesterday\",\"children\":[");
                DateTime forecastDate = data;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var WATERTEMPERATURE = "";
                        switch (j)
                        {
                            case 0:
                                WATERTEMPERATURE = dt.Rows[i]["Min48"].ToString() + "~" + dt.Rows[i]["Max48"].ToString();
                                break;
                            case 1:
                                WATERTEMPERATURE = dt.Rows[i]["Min72"].ToString() + "~" + dt.Rows[i]["Max72"].ToString();
                                break;
                            case 2:
                                WATERTEMPERATURE = dt.Rows[i]["Min96"].ToString() + "~" + dt.Rows[i]["Max96"].ToString();
                                break;
                        }
                        forecastDate = forecastDate.AddDays(1);
                        sb_str.Append("{\"PUBLISHDATE\":\"" + dt.Rows[i]["PUBLISHDATE"]
                            + "\",\"FORECASTDATE\":\"" + forecastDate
                            + "\",\"WATERTEMPERATURE\":\"" + WATERTEMPERATURE
                            + "\"},");
                    }

                }
                return sb_str.Remove(sb_str.Length - 1, 1).ToString() + "]}";
            }
            return "";
        }
        /// <summary>
        /// 表单75,76,77,78,79,80,81,拆分的潮汐数据提交到一个方法里面
        /// 下午1,4,9,11,12,14,16
        /// 潮时的数据显示，方法与07相同。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable75(DateTime data)
        {
            sql_TBLSDOFFSHORESEVENCITY24HTIDE sql = new sql_TBLSDOFFSHORESEVENCITY24HTIDE();
            TBLSDOFFSHORESEVENCITY24HTIDE model = new TBLSDOFFSHORESEVENCITY24HTIDE();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(model);
            StringBuilder sb_str = new StringBuilder();

            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.get_TBLSDOFFSHORESEVENCITY24HTIDE_AllData(model);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t75\",\"children\":");
                var result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }
        /// <summary>
        /// 表单75,76,77,78,79,80,81,拆分的潮汐数据提交到一个方法里面
        /// 下午1,4,9,11,12,14,16
        /// 潮高的数据显示，方法与46相同。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable76(DateTime data)
        {
            sql_TideData sql = new sql_TideData();
            HT_TideData model = new HT_TideData();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.getTideData(model);
            StringBuilder sb_str = new StringBuilder();

            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.getTideData(model);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t76\",\"children\":");
                var result = JsonMore.Serialize(dt);
                sb_str.Append(result);
                sb_str.Append("}");
                return sb_str.ToString();
            }
            return "";
        }

        /// <summary>
        /// 表单22数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string gettable82(DateTime data, string searchType)
        {
            sql_TBLWEIHAISHIDAOTIDALFORECAST sql = new sql_TBLWEIHAISHIDAOTIDALFORECAST();
            TBLWEIHAISHIDAOTIDALFORECAST model = new TBLWEIHAISHIDAOTIDALFORECAST();
            model.PUBLISHDATE = data;
            model.FORECASTDATE = data;
            DataTable dt = new DataTable();
            dt = (DataTable)sql.get_TBLWEIHAISHIDAOTIDALFORECAST_AllData(model);
            if (dt == null || dt.Rows.Count < 1)
            {
                model.PUBLISHDATE = data.AddDays(-1);
                dt = (DataTable)sql.get_TBLWEIHAISHIDAOTIDALFORECAST_AllData(model);
            }
            StringBuilder sb_str = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb_str.Append(",{ \"type\":\"t82\",\"children\":[");
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



        #endregion

        #region 七地市风浪气象

        /// <summary>
        /// 下午二十五、东营广利渔港-未来三天的海面风及海浪有效波高预报（20时起报）
        /// 原始数据风向和风速的要从WIND_POINT_JXH中获取
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable55(DateTime data, string searchType)
        {
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetWaveData("DYGLFP", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                sb_str.Append(",{ \"type\":\"t55\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveHeight(data, "东营广利一级渔港");

            //原来的
            //DataTable dtWind = new DataTable();
            //dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "广利港海域", "PM");

            //精细化新加的 暂时不确定
            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindDataJXH(data, "东营广利一级渔港");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t55\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 下午二十八、日照桃花岛-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable58(DateTime data, string searchType)
        {
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetWaveData("RZTHD", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                sb_str.Append(",{ \"type\":\"t58\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveHeight(data, "日照桃花岛");

            //DataTable dtWind = new DataTable();
            //dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "日照近海", "PM");

            //精细化新加的 暂时不确定
            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindDataJXH(data, "日照桃花岛");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t58\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 下午三十一、潍坊度假区-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable61(DateTime data, string searchType)
        {
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetWaveData("WFDJQ", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                sb_str.Append(",{ \"type\":\"t61\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveHeight(data, "潍坊旅游度假区");

            //DataTable dtWind = new DataTable();
            //dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "潍坊近海", "PM");

            //精细化新加的 暂时不确定
            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindDataJXH(data, "潍坊旅游度假区");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
               {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t61\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 下午三十四、威海新区-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable64(DateTime data, string searchType)
        {
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetWaveData("WHXQ", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                sb_str.Append(",{ \"type\":\"t64\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveHeight(data, "威海南海新区");

            //DataTable dtWind = new DataTable();
            //dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "乳山近海", "PM");

            //精细化新加的 暂时不确定
            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindDataJXH(data, "威海南海新区");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t64\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 下午三十七、烟台清泉-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable67(DateTime data, string searchType)
        {
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetWaveData("YTQQ", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                sb_str.Append(",{ \"type\":\"t67\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveHeight(data, "烟台清泉码头");

            //DataTable dtWind = new DataTable();
            //dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "烟台近海", "PM");

            //精细化新加的 暂时不确定
            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindDataJXH(data, "烟台清泉码头");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t67\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 下午四十、董家口-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable70(DateTime data, string searchType)
        {
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetWaveData("DJKP", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                sb_str.Append(",{ \"type\":\"t70\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveHeight(data, "董家口");

            //DataTable dtWave = new DataTable();
            //dtWave = (DataTable)getBy_s.GetWaveAndWindData(data, "黄岛近海", "AM");

            //DataTable dtWind = new DataTable();
            //dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "黄岛近海", "PM");

            //精细化新加的 暂时不确定
            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindDataJXH(data, "董家口");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t70\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        /// <summary>
        /// 下午四十三、东营渔港-未来三天的海面风及海浪有效波高预报（20时起报）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public string gettable73(DateTime data, string searchType)
        {
            TBLSEVENWAVE model = new TBLSEVENWAVE();
            sql_TBLSEVENFORECAST sql = new sql_TBLSEVENFORECAST();
            model.PUBLISHDATE = data;
            DataTable dt = (DataTable)sql.GetWaveData("DYFP", model);
            StringBuilder sb_str = new StringBuilder();
            var result = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                result = JsonMore.Serialize(dt);

                sb_str.Append(",{ \"type\":\"t73\",\"pbtype\":\"bydb\",\"children\":");
                sb_str.Append(result);
                return sb_str.ToString() + "}";
            }
            Sql_GetDataBy_S getBy_s = new Sql_GetDataBy_S();

            DataTable dtWave = new DataTable();
            dtWave = (DataTable)getBy_s.GetWaveHeight(data, "东营渔港");

            //DataTable dtWind = new DataTable();
            //dtWind = (DataTable)getBy_s.GetWaveAndWindData(data, "黄岛近海", "PM");

            //精细化新加的 暂时不确定
            DataTable dtWind = new DataTable();
            dtWind = (DataTable)getBy_s.GetWaveAndWindDataJXH(data, "东营渔港");

            result = "\"wave\":";
            if (dtWave != null && dtWave.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWave);
            }
            else
            {
                result += "[{}]";
            }

            result += ",\"wind\":";
            if (dtWind != null && dtWind.Rows.Count > 0)
            {
                result += JsonMore.Serialize(dtWind);
            }
            else
            {
                result += "[{}]";
            }
            sb_str.Append(",{ \"type\":\"t73\",\"pbtype\":\"bys\",");
            sb_str.Append(result);
            sb_str.Append("}");
            return sb_str.ToString();
        }

        #endregion

        #endregion

        #region
        /// <summary>
        /// 生成潮汐数据曲线图
        /// </summary>
        /// <returns></returns>
        private void TideCurve(DateTime date)
        {
            DateTime dtime = DateTime.Now;
            string OCEANRANCHNAME = "";
            string SN = "";
            string SX = "";
            for (int j = 0; j < 3; j++)
            {
                switch (j)
                {
                    case 0:
                        OCEANRANCHNAME = "寻山海洋牧场"; //海洋牧场长名称
                        SN = "xsh"; //缩写
                        SX = "寻山";
                        break;
                    case 1:
                        OCEANRANCHNAME = "荣成烟墩角游钓型海洋牧场"; //海洋牧场长名称
                        SN = "ydj"; //缩写
                        SX = "烟墩角";
                        break;
                    case 2:
                        OCEANRANCHNAME = "西霞口集团国家级海洋牧场"; //海洋牧场长名称
                        SN = "xxk"; //缩写
                        SX = "西霞口";
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
                curve.xKeys = new string[] {
                                         "00:00\n" + dtime.AddDays(1).ToString("MM-dd"), "06:00\n" + dtime.AddDays(1).ToString("MM-dd"), "12:00\n" + dtime.AddDays(1).ToString("MM-dd"), "18:00\n" + dtime.AddDays(1).ToString("MM-dd"),
                                         "00:00\n" + dtime.AddDays(2).ToString("MM-dd"), "06:00\n" + dtime.AddDays(2).ToString("MM-dd"), "12:00\n" + dtime.AddDays(2).ToString("MM-dd"), "18:00\n" + dtime.AddDays(2).ToString("MM-dd"),
                                         "00:00\n" + dtime.AddDays(3).ToString("MM-dd"), "06:00\n" + dtime.AddDays(3).ToString("MM-dd"), "12:00\n" + dtime.AddDays(3).ToString("MM-dd"), "18:00\n" + dtime.AddDays(3).ToString("MM-dd"),
                                         "00:00\n" + dtime.AddDays(4).ToString("MM-dd")
                                         };
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

                curve.yKeys = yvalues.ToArray();
                curve.xybgColor = Color.White;
                curve.bgColor = Color.FromArgb(204, 204, 204);
                curve.grid = false;
                curve.title = "【" + SX + "】" + "海洋牧场 72小时潮汐预报";
                Bitmap objBitmap = curve.CreateCurve();
                string fileName = "EL_" + SN + "_" + DateTime.Now.ToString("yyyyMMdd") + "_00_72hr.jpg";
                string docPath = System.AppDomain.CurrentDomain.BaseDirectory + "72小时潮汐预报图" + "\\" + date.ToString("yyyyMMdd");
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
        private List<float> GetTide(DateTime date, string OCEANRANCHNAME)
        {
            sql_OceanRanchTide sql = new sql_OceanRanchTide();
            List<float> tideHeight = new List<float>();
            DataTable dt = (DataTable)sql.Get24TideDataList(date, OCEANRANCHNAME);
            if (dt != null && dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        tideHeight.Add(float.Parse(dt.Rows[i][j].ToString()));
                    }
                }
            }
            date = date.AddDays(4);
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
        private string UpToServer(string path, string fileName)
        {
            TideFtpClient.FTPUploadFile(ftpIp + "/" + DateTime.Now.ToString("yyyyMMdd") + "/Tide1", ftpUserName, ftpPwd, new System.IO.FileInfo(path));
            //TideFtpClient.FTPUploadFile(ftpIp+ "/hyqx", ftpUserName, ftpPwd, new System.IO.FileInfo(path));
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