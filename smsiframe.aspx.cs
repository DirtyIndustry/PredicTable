using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PredicTable
{
    public partial class smsiframe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DXButton2.Attributes.Add("onclick", "return  GetGroup();");
        }
        
        protected void DXButton2_Click(object sender, EventArgs e)
        {
            string userid = "";
            KJ_GongZhongPingTai pt = new KJ_GongZhongPingTai();
            Sql_GONGZHONGPINGTAI sql_gzpt = new Sql_GONGZHONGPINGTAI();
            pt.TIME = DateTime.Now;
            //Session["userid"] = "cx";//测试完毕删除
            if (Session["userid"] != null)
            {
                userid = Session["userid"].ToString();
            }
            else
            {

                userid = "";
            }
            pt.MESTYPE = "短信";
            pt.USERID = userid;
            pt.STATE = "已入库";
            pt.DOCTYPE = DOCTYPEdx.Value;
            // pt.DOCUMENTCONTENT = duanxin1.Value;
            pt.DOCUMENTCONTENT = duanxin.Value;

            pt.DXGROUP = group.Value;

            int a = sql_gzpt.Add_GongZhongPingTai(pt);

            string jbType = userid == "admin" ? "HL" : userid == "hl" ? "HL" : userid == "cx" ? "FBC" : "";
            string publishArea = parentDiv.Value == "divgroupBH" ? "NCS" : parentDiv.Value == "divgroupSD" ? "SD" : "";
            var morning2 = Convert.ToInt32(pt.TIME.Hour) < 12 ? "08" : "16";
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "预报单共享\\duanqi\\" + pt.TIME.ToString("yyyyMMdd") + "\\";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory(filepath);//不存在就创建目录 
            }
            string fileName = "DXJB_" + publishArea + "_" + jbType + "_" + pt.TIME.ToString("yyyyMMdd") + morning2 + ".txt";
            int b = this.CreateTxt(filepath, fileName, userid, duanxin.Value);
            if (a > 0 && b > 0)
            {
                ruku(filepath + "\\" + fileName, fileName, pt.TIME, userid);//入库  
                // Response.Write("<script>location.href='GongZhongPingTaiGL.aspx';</script>");
                Response.Write("<script>parent.location.href='SMSEditor.aspx';</script>");
                //Response.Redirect("<script>window.location.reload(true);</script>");
            }
        }

        /// <summary>
        /// 创建txt文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="userid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private int CreateTxt(string filepath,string fileName,string userid,string content)
        {
            StreamWriter sw = null;
            if (File.Exists(filepath + "\\" + fileName))
            {
                File.Delete(filepath + "\\" + fileName);
            }
            if (!File.Exists(filepath +"\\"+ fileName))
            {
                File.Create(filepath + "\\" + fileName).Close();
            }
            
            try
            {
                sw = new StreamWriter(filepath + "\\" + fileName, true, Encoding.UTF8);
                sw.WriteLine(content);
                sw.Flush();
                return 1;
            }
            catch (Exception ex)
            {
                Console.Write("日志写入失败：" + ex.Message);
                sw.Close();
                return 0;
            }
            finally
            {
                if (sw != null)
                    sw.Flush();
                sw.Close();
            }
            
        }

        /// <summary>
        /// 生成表单入库
        /// </summary>
        /// <param name="fileName">入库的文件名</param>
        /// <param name="strone">对应表单</param>
        /// <param name="dt">当前时间</param>
        private int ruku(string fileName, string strone, DateTime dt, string userid)
        {
            int a1 = 0;
            TBLYBDDOCUMENT tbl = new TBLYBDDOCUMENT();
            tbl.UPLOADDATE = dt;
            tbl.YBDNAME = strone;

            List<int> a = new List<int>();
            int i1 = 0;
            System.Data.DataTable tblybddocument = (System.Data.DataTable)new sql_TBLYBDDOCUMENT().get_TBLYBDDOCUMENT_AllData(tbl);
            if (tblybddocument.Rows.Count > 0)
            {
                //edit
                UpdateWord updateword = new UpdateWord();
                a1 = updateword.updateword(fileName, dt, userid);
            }
            else
            {
                //存入数据库
                InsertWord insertword = new InsertWord();
                a1 = insertword.saveword(fileName, dt, userid);
            }
            return a1;
        }
    }
}