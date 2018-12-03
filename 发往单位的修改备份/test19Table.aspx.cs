using PredicTable.Dal;
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class test19Table : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["userid"] != null)
                {
                    string userid = Session["userid"].ToString();
                    if (Session["op19table"] != null)
                    {
                        string docname = Session["op19table"].ToString();
                        var filePath = System.Web.HttpContext.Current.Server.MapPath("/scword/" + docname);
                        if (File.Exists(filePath))
                            System.IO.File.Delete(docname);//如果文件存在，先删除再保存

                        PageOffice.FileSaver fs = new PageOffice.FileSaver();
                        fs.SaveToFile(filePath);
                        fs.Close();

                        var CG_YUBAO_ME_Model = new CG_YUBAO_ME();
                        CG_YUBAO_ME_Model.YBWENJIANMING = docname;
                        CG_YUBAO_ME_Model.YBQUYU = "北海区";
                        CG_YUBAO_ME_Model.YBNEIRONG = "海冰";
                        var docInfoArr = docname.Split('_');
                        if (docInfoArr.Length == 6)
                        {
                            switch (docInfoArr[3])
                            {
                                case "7day":
                                    CG_YUBAO_ME_Model.YBSHIXIAO = "周";
                                    break;
                                case "10day":
                                    CG_YUBAO_ME_Model.YBSHIXIAO = "旬";
                                    break;
                                case "1mon":
                                    CG_YUBAO_ME_Model.YBSHIXIAO = "月";
                                    break;
                                case "1yr":
                                    CG_YUBAO_ME_Model.YBSHIXIAO = "年";
                                    break;
                                default: break;
                            }

                            CG_YUBAO_ME_Model.YBSHIJIAN = DateTime.ParseExact(docInfoArr[4], "yyyyMMdd", CultureInfo.InvariantCulture);
                        }
                        CG_YUBAO_ME_Model.YBDANWEI = "北海预报中心";
                        ////////
                        var result2 = new sql_CG_YUBAO_ME().Add_CG_YUBAO_FILE(CG_YUBAO_ME_Model);
                        if (result2 == 0)
                        {
                            Response.Write("保存失败！");
                            return;
                        }

                        var CG_YUBAO_FILE_Model = new CG_YUBAO_FILE();
                        CG_YUBAO_FILE_Model.YBWENJIANMING = docname;
                        CG_YUBAO_FILE_Model.YBNEIRONG = File.ReadAllBytes(filePath);
                        CG_YUBAO_FILE_Model.PICFILE = new byte[0];
                        var result = new sql_CG_YUBAO_FILE().Add_CG_YUBAO_FILE(CG_YUBAO_FILE_Model);
                        if(result ==0)
                        {
                            Response.Write("保存失败！");
                            return;
                        }

                        Sql_Caozuorizhi.WriteRizhi(userid, "19_table", "生成19号预报单：" + docname + " 成功");
                        //Response.Write("<script>alert('生成19号预报单：" + docname + " 成功！');</script>");
                        Response.Write("保存成功！");
                    }
                    else
                    {
                        Sql_Caozuorizhi.WriteRizhi(userid, "19_table", "生成19号预报单失败");
                        Response.Write("保存失败");
                        //Response.Write("<script>alert('生成19号预报单失败！');</script>");
                    }
                }
                else
                {
                    //未登录或超时
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("保存word失败" + ex.ToString());
                //Response.Write("<script>alert('保存19号预报单失败！');</script>");
            }
        }

        /// <summary>
        /// 保存发送单位信息
        /// </summary>
        private void SaveUnitInfo()
        {

        }
    }
}