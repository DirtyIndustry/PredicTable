//**********************************************************************************************

//文件名(File Name)：                   JieChuJingBao

//作者(Author)：                        sl

//日期(Create Date)：                   2017-02-06

//修改记录(Revision History)：
//        R1：
//         修改作者：                   sl  
//         修改日期：                   2017-02-06  
//         修改理由：                   添加
//                                      海浪解除警报Word文件解析
//                                      
//
//**********************************************************************************************
using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.ExportWord.WaveWordAnalysis
{
    public class JieChuJingBao
    {
        string filepath;
        ArrayList doctext;//word文档文本内容
        /// <summary>
        /// 文档发布时间
        /// </summary>
        public DateTime post_time;//文档发布时间
        /// <summary>
        /// 说明
        /// </summary>
        public string description = "";//说明
        /// <summary>
        /// 联系人
        /// </summary>
        public string link = "";//联系人
        /// <summary>
        /// 电话
        /// </summary>
        public string tel = "";//电话
        /// <summary>
        /// 发送单位
        /// </summary>
        public string seddanwei = "";
        /// <summary>
        /// 发往
        /// </summary>
        public string fawang = "";//发往
        /// <summary>
        /// 传真
        /// </summary>
        public string chuanzhen = "";//传真
        public string biaohao = ""; //编号
        public string title = "";//标题
        public string content = "";//内容
        public DateTime publish_time;//文档发布时间
        //public string level = "";
        public JieChuJingBao(string filepaths)
        {
            this.filepath = filepaths;
            doctext = getDocText(filepath);
        }
        public void assignmentTable()
        {
            assignment();
        }
        public void assignment()
        {
            int i_tel = GetIndex(0, "值班员", "联系人");//电话与值班员在同一行
            int i_chuanzhen = GetIndex(0, "传真");
            //int i_time = assignmentPosttime(0);//文档发布日期
            int i_time = GetIndex(0, "时间");
            int i_bianhao = GetIndex(0, "编号");
            //int i_title = GetIndex(i_bianhao,"");
            if (i_time == -1)
            {
                i_time = 0;
            }
            int i_fawang = GetIndex(0, "发往");
            int i_link = GetIndex(i_fawang, "联系人", "值班员");
          
            if (i_fawang != 0)
            {
                fawang = SetValue(i_fawang, i_link);
                if (fawang.Length > 0)
                {
                    fawang = fawang.Trim().Trim('。').Substring(3).Replace('、', ';');
                }
            }
            link = SetLink(i_link);
            tel = SetTelOrChuanzhen(i_tel, "电话");
            chuanzhen = SetTelOrChuanzhen(i_chuanzhen, "网址");
            if (i_bianhao != 0)
            {
                biaohao = SetTelOrChuanzhen(i_bianhao, "签发");
                title = getTitle(i_bianhao);
            }
            else
            {
                title = getTitle(i_time);
            }
            content = SetValue(i_bianhao+1, i_fawang);
            publish_time = SetTime(i_time, "签发");
        }
        private DateTime SetTime(int index, string splitStr)
        {
            string str = doctext[index].ToString();
            string strRemoveSpace = str.Replace(" ", "");
            int a = strRemoveSpace.IndexOf("时间");
            DateTime dtimt = DateTime.Now;

            string text = strRemoveSpace.Split('签')[0];
            string timeStr = text.Substring(a + 3);
            string year = timeStr.Substring(0, 4);
            int yIndex = timeStr.IndexOf('年');
            int mIndex = timeStr.IndexOf('月');
            int dIndex = timeStr.IndexOf('日');
            int hyIndex = timeStr.IndexOf('时');

            string month = timeStr.Substring(yIndex + 1, mIndex - yIndex - 1);
            string day = timeStr.Substring(mIndex + 1, dIndex - mIndex - 1);
            string hour = timeStr.Substring(dIndex + 1, hyIndex - dIndex - 1);

            dtimt = Convert.ToDateTime(year + "-" + month + "-" + day + "  " + hour + ":00:00");
            return dtimt;
        }
        private string getTitle(int i_bianhao)
        {
            for (int i = 1; i < doctext.Count; i++)
            {
                string str = doctext[i_bianhao + i].ToString();
                string strRemoveSpace = str.Replace(" ", "");
                strRemoveSpace = strRemoveSpace.Replace("\r", "");
                if (strRemoveSpace != null && strRemoveSpace != "")
                {
                    return strRemoveSpace;
                }
            }
            return "";
        }

        public ArrayList getDocText(string filepath)
        {
            Application app = new Application();
            Document doc = null;
            object unknow = Type.Missing;
            app.Visible = false;
            object file = filepath;
            doc = app.Documents.Open(ref file, ref unknow, true, ref unknow,
                ref unknow, ref unknow, ref unknow, ref unknow,
                ref unknow, ref unknow, ref unknow, ref unknow,
                ref unknow, ref unknow, ref unknow, ref unknow);
            int i = doc.Paragraphs.Count;
            ArrayList str = new ArrayList();
            for (int j = 1; j <= i; j++)
            {
                Paragraph item = doc.Paragraphs[j];
                if (item.Range.InlineShapes.Count == 0)
                {
                    str.Add(item.Range.Text);
                }

            }
            doc.Close();
            app.Quit();
            return str;
        }

        private string dataNum(string str, int i)
        {
            string num1 = "";
            string num2 = "";
            if (str.Contains("~"))
            {
                num1 = str.Substring(0, str.IndexOf("~"));
                num2 = str.Substring(str.IndexOf("~") + 1, str.Length - str.IndexOf("~") - 1);
            }
            else if (str.Contains("-"))
            {
                num1 = str.Substring(0, str.IndexOf("-"));
                num2 = str.Substring(str.IndexOf("-") + 1, str.Length - str.IndexOf("-") - 1);
            }
            else
            {
                num1 = str;
                num2 = "";
            }
            if (i == 1)
            {
                return num1;
            }
            else if (i == 2)
            {
                return num2;
            }
            return null;
        }

        private string tableNum(string[,] str)
        {
            int i = str.GetLength(0) - 1;
            int j = str.GetLength(1) - 1;
            for (int ii = i; ii >= 0; ii--)
            {
                for (int jj = j; jj >= 0; jj--)
                {
                    if (str[ii, jj].ToString().Replace(" ", "").Replace("\r", "").Replace("\a", "").Replace("\f", "").Length > 0)
                    {
                        return str[ii, jj].ToString().Replace(" ", "").Replace("\r", "").Replace("\a", "").Replace("\f", "");
                    }
                }
            }
            return "";
        }

        private int GetIndex(int startIndex, params string[] types)
        {
            string str;
            for (int j = startIndex; j < doctext.Count; j++)
            {
                str = doctext[j].ToString().Replace(" ", "");
                foreach (string type in types)
                {
                    if (str.IndexOf(type) == 0)
                    {
                        return j;
                    }
                }
            }
            return startIndex;
        }

        private string SetValue(int startIndex, int endIndex)
        {
            string para = "";
            if (startIndex == endIndex)
            {
                para = doctext[startIndex].ToString();
                return para;
            }
            for (int j = startIndex; j < endIndex; j++)
            {
                para += doctext[j].ToString();
            }
            return para;
        }
        private string SetLink(int index)
        {
            string str = doctext[index].ToString();
            string strRemoveSpace = str.Replace(" ", "");
            string links = strRemoveSpace.Substring(4, strRemoveSpace.IndexOf("电话") - 4);
            //string[] strs = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //List<string> list = new List<string>();
            //for (int i = 0; i < strs.Length; i++)
            //{
            //    if (links.Contains(strs[i]))
            //    {
            //        list.Add(strs[i]);
            //    }
            //}
            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (list[i].Length == 1)
            //    {
            //        retStr += list[i];
            //    }
            //    else
            //    {
            //        retStr += list[i] + ";";
            //    }
            //}
            return links;
        }
        private string SetTelOrChuanzhen(int index, string splitStr)
        {
            string str = doctext[index].ToString();
            string strRemoveSpace = str.Replace(" ", "");
            int a = strRemoveSpace.IndexOf(splitStr);
            string telOrchuanzhen = "";
            if (splitStr == "电话")
            {
                telOrchuanzhen = strRemoveSpace.Substring(a + 3);
            }
            else if (splitStr == "网址")
            {
                telOrchuanzhen = strRemoveSpace.Substring(3, a - 3);
            }
            else if (splitStr == "签发")
            {
                telOrchuanzhen = strRemoveSpace.Substring(3, a - 3);
            }
            return telOrchuanzhen;
        }

        private int assignmentPosttime(int i)
        {
            string str;
            int j = -1;
            for (j = i; j < doctext.Count; j++)
            {
                str = doctext[j].ToString();
                str = str.Replace(" ", "");
                str = str.Replace("\r", "");
                if (str.EndsWith("日发布"))
                {

                    string ye = str.Substring(str.IndexOf("年") - 4, 4);
                    string mo = str.Substring(str.IndexOf("年") + 1, str.IndexOf("月") - str.IndexOf("年") - 1);
                    string day = str.Substring(str.IndexOf("月") + 1, str.IndexOf("日") - str.IndexOf("月") - 1);
                    if (mo.Length == 1)
                        mo = "0" + mo;
                    if (day.Replace(" ", "").Length == 1)
                        day = "0" + day;
                    //post_time = ye + mo + day + "00";
                    post_time = Convert.ToDateTime(ye + "-" + mo + "-" + day);
                    seddanwei = str.Substring(0, str.IndexOf("年") - 4);
                    break;
                }
            }
            return j;
        }
    }
}