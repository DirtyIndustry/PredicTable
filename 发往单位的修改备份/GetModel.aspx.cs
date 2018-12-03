using PredicTable.ExportWord;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class GetModel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetWordModel();
            }
        }

        /// <summary>
        /// 获取word模板
        /// </summary>
        private void GetWordModel()
        {
            Sql_UPLOADWORD sql_uploadword = new Sql_UPLOADWORD();
            DataTable dt = (DataTable)sql_uploadword.get_UPLOADWORDdata("CN");
            if (dt != null && dt.Rows.Count > 0)
            {
                this.list.DataSource = dt;
                this.list.DataTextField = "NEWNAME";
                this.list.DataValueField = "OLDNAME";
                this.list.DataBind();
                this.hidd_model.Value = dt.Rows[0]["NEWNAME"].ToString();
            }
        }

        protected void startoperation_Click(object sender, EventArgs e)
        {
            string BackImage = "../Ascx/TempImage/";
            string fileName = this.hidd_model.Value; //list.Items[list.SelectedIndex].Text; //获取模板名称
            string type = rtnDocType(fileName);//判断文件类型（EN、CN），根据分类找模板
            string time = this.hid_field_time.Value != "" ? this.hid_field_time.Value : DateTime.Now.ToString("yyyyMMdd");
            string newName = rtnNewName(type,fileName,time);
            string templateFile = "";//模板文件位置
            Session["BackImage"] = BackImage + fileName + ".png";
            Session["ENPublishTime"] = time;  //获取发布时间
            if (type == "EN")
            {
                string templateType = rtnENType(fileName); //获取模板时效类型，选择不同ASCX
                templateFile = Server.MapPath("pageoffice/doc/EN-MediumAndLong/") + fileName;
                Session["ENEffectTime"] = templateType;  //获取当前时效类型
                Session["ENFBDW"] = this.rtnFBDW(fileName);  //获取发布单位
                if (templateType == "1yr") {
                    Control uc = Page.LoadControl("~/Ascx/ENModelYear.ascx");
                    this.Div1.Controls.Add(uc);
                }
                else if (templateType == "10day" || templateType=="1mon") {
                    Session["ENDayType"] = templateType;
                    Control uc = Page.LoadControl("~/Ascx/ENModelDay.ascx");
                    this.Div1.Controls.Add(uc);
                }
            }
            else if (type == "CN")
            {
                //根据中文模板文件名，获取预报单单位
                string companyName = this.rtnCNFBDW(fileName);
                Session["companyName"] = companyName;
                //获取模板文件
                templateFile = Server.MapPath("pageoffice/doc/CN-MediumAndLong/") + fileName;
                Control uc = Page.LoadControl("~/Ascx/CNModel.ascx");
                this.Div1.Controls.Add(uc);
            }
            Session["fileName"] = Server.MapPath("pageoffice/doc/oplonger/") + newName;//复制word时，生成word的名称
            Session["templateFile"] = templateFile;//模板名称
            Session["Type"] = type;
            Session["docName"] = newName;
        }
        /// <summary>
        /// 判断模板类型
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string rtnDocType(string fileName)
        {
            string typeJudge = fileName.Substring(0, 2);
            if (typeJudge == "国家" || typeJudge == "山东")
                return "EN";
            else
                return "CN";
        }

        #region  生 成 新 文 件 名

        /// <summary>
        /// 生成新文件名
        /// </summary>
        /// <returns></returns>
        private string rtnNewName(string type,string fileName,string time)
        {
            string newName = "";
            if (type == "EN")
            {
                newName = rtnNewCountryFileName(fileName, time);
            }
            else{
                newName = rtnNewCNFileName(fileName, time);
            }
            return newName;
        }
        /// <summary>
        /// 生成英文预报单名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private string rtnNewCountryFileName(string fileName, string time)
        {
            string ENDocName = "";
            string country = fileName.Substring(0, 2);
            string sxType = fileName.Substring(6, 1);
            if (country == "国家")
            {
                ENDocName = "YB_NCS_";
                if (sxType == "年")
                {
                    ENDocName += "ZH_1yr_" + time + "_NMFC.doc";
                }
                else if (sxType == "月")
                {
                    ENDocName += "HJ_1mon_" + time + "_NMFC.doc";
                }
                else
                {
                    ENDocName += "HJ_10day_" + time + "_NMFC.doc";

                }
            }
            else
            {
                ENDocName = "YB_SD_";
                if (sxType == "年")
                {
                    ENDocName += "ZH_1yr_" + time + "_SDMF.doc";
                }
                else if (sxType == "月")
                {
                    ENDocName += "HJ_1mon_" + time + "_SDMF.doc";
                }
                else
                {
                    ENDocName += "HJ_10day_" + time + "_SDMF.doc";
                }
            }
            return ENDocName;
        }

        /// <summary>
        /// 生成中文预报单
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="time"></param>
        private string rtnNewCNFileName(string fileName, string time)
        {
            string CNDocName = "";
            string[] fileNameSplit = fileName.Split('-');
            string pbArea = fileNameSplit[1].ToString();
            string year = time.Substring(0, 4);
            string month = Convert.ToInt32(time.Substring(4, 2)).ToString();
            DateTime t = Convert.ToDateTime(year + "-" + month + "-" + time.Substring(6, 2));
            int day = Convert.ToInt32(time.Substring(6, 2));
            if (day == 9)
            {
                CNDocName = year + "年" + month + "月中旬预报" + "-" + pbArea;
            }
            else if (day == 19)
            {
                CNDocName = year + "年" + month + "月下旬预报" + "-" + pbArea;
            }
            else if (day == 29 || (day == 28 && month == "2"))
            {
                DateTime cnt = t.AddMonths(1);
                CNDocName = cnt.Year + "年" + cnt.Month + "月上旬预报" + "-" + pbArea;
            }
            else if (day == 25 || day == 26)
            {
                DateTime cnt1 = t.AddMonths(1);
                CNDocName = cnt1.Year + "年" + cnt1.Month + "月预报" + "-" + pbArea;
            }
            return CNDocName;
        }
        /*private string rtnNewCNFileName(string fileName ,string time)
        {
            string CNDocName = "";
            string[] fileNameSplit = fileName.Split('-');
            string pbArea = fileNameSplit[1].ToString();
            string year = time.Substring(0,4);
            string month = Convert.ToInt32(time.Substring(4, 2)).ToString();
            DateTime t = Convert.ToDateTime(year+"-"+ month+"-"+time.Substring(6, 2));
            int day = Convert.ToInt32(time.Substring(6, 2));
            if (day < 11)
            {
                CNDocName = year + "年" + month + "月中旬预报" + "-" + pbArea;
            }
            else if(day > 10 && day < 21)
            {
                CNDocName = year + "年" + month + "月下旬预报" + "-" + pbArea;
            }
            else
            {
                //CNDocName = year + "年" + (month+1) + "月及上旬预报" + "-" + pbArea;
                DateTime cnt = t.AddMonths(1);
                CNDocName = cnt.Year + "年" + cnt.Month + "月及上旬预报" + "-" + pbArea;
            }
            return CNDocName;
        }*/

        #endregion


        /// <summary>
        /// 判断英文文件类型
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        private string rtnENType(string fileName)
        {
            string ENTempLateType = "";
            string sxType = fileName.Substring(6, 1);
            if (sxType == "年")
            {
                ENTempLateType = "1yr";
            }
            else if(sxType == "月")
            {
                ENTempLateType = "1mon";
            }
            else
            {
                ENTempLateType = "10day";
            }
            return ENTempLateType;
        }

        /// <summary>
        /// 获取英文发布单位
        /// </summary>
        /// <returns></returns>
        private string rtnFBDW(string fileName)
        {
            string fbdw = "";
            string typeJudge = fileName.Substring(0, 2);
            if (typeJudge == "国家")
            {
                fbdw = "NMFC";
            }
            else if(typeJudge == "山东")
            {
                fbdw = "SDMF";
            }
            else
            {
                fbdw="";
            }
            return fbdw;
        }

        /// <summary>
        /// 获取中文发布单位名称
        /// </summary>
        /// <returns></returns>
        private string rtnCNFBDW(string fileName) {
            string str = fileName.Split('-')[1];
            string rtnCompanyName = str.Split('.')[0];
            return rtnCompanyName;
        }

        /// <summary>
        /// 解析英文模板类型
        /// </summary>
        /// <returns></returns>
        private string GetENTemplateType(string fileName)
        {
            string[] str = fileName.Split('_');
            if(str.Length > 4)
            {
                return str[3].ToString();
            }
            return "";
        }
    }
}