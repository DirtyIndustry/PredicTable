using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace PredicTable.ExportWord.NineteenWord
{
    public class SaveNineteenTablesJD
    {
        string filepath;
        ArrayList doctext;//word文档文本内容
        /// <summary>
        /// 预报时效
        /// </summary>
        public string predict_aging = ""; //预报时效
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime post_time;//文档发布时间
        /// <summary>
        /// 发送单位
        /// </summary>
        public string senddanwei = "";//发送单位
        /// <summary>
        /// 冰情概况
        /// </summary>
        public string ice_situation = "";//冰情概况
        /// <summary>
        /// 预计
        /// </summary>
        public string predict = "";//预计
        /// <summary>
        /// 说明
        /// </summary>
        public string description = "";//说明
        /// <summary>
        /// 发往
        /// </summary>
        public string fawang = "";//发往
        /// <summary>
        /// 联系人
        /// </summary>
        public string link = "";//联系人
        /// <summary>
        /// 电话
        /// </summary>
        public string tel = "";//电话
        /// <summary>
        /// 传真
        /// </summary>
        public string chuanzhen = "";//传真
        /// <summary>
        /// 表格，先判断null
        /// </summary>
        public string[,] table_str;//word中的表格
        private string[,] table_xml;


        public SaveNineteenTablesJD(string filepaths)
        {
            this.filepath = filepaths;
            doctext = getDocText(filepath);
        }

        //预报时效
        public void assignmentPredict_aging()
        {
            string str = Path.GetFileNameWithoutExtension(filepath);

             if (str.ToLower().Contains("_10day_") || str.Contains("旬"))
            {
                predict_aging = "010d";
            }
            else if (str.ToLower().Contains("_1mon_") || str.Contains("月"))
            {
                predict_aging = "001m";
            }
        }


        public void assignment()
        {
            //int i_tel = GetIndex(0, "发布单位");
            int i_tel = GetIndex(0, "值班员", "联系人");//电话与值班员在同一行
            int i_time = assignmentPosttime(0);//文档发布日、传真
            if (i_time == -1)
            {
                i_time = 0;
            }
            int i_Ice_situation = GetIndex(i_time, "冰情概况");
            int i_Predict = GetIndex(i_Ice_situation, "预计");
            int i_Table = GetIndex(i_Ice_situation, "表");
            int i_Img = GetIndex(i_Table, "图");
            int i_description = GetIndex(i_Img, "提示");
            int i_fawang = GetIndex(i_Img, "发往");
            int i_link = GetIndex(i_fawang, "联系人", "值班员");
            {
                //                                      没有预计                        没有表                       没有图                                没有说明
                int end = i_Ice_situation == i_Predict ? (i_Predict < i_Table ? i_Table : (i_Table < i_Img ? i_Img : (i_Img < i_description ? i_description : (i_fawang < i_link ? i_fawang : i_link)))) : i_Predict;
                ice_situation = SetValue(i_Ice_situation, end);
            }
            if (i_Predict != i_time)
            {
                //                                         没有表                     没有图                                没有说明
                int end = i_Predict < i_Table ? i_Table : (i_Table < i_Img ? i_Img : (i_Img < i_description ? i_description : (i_fawang < i_link ? i_fawang : i_link)));
                predict = SetValue(i_Predict, end);
            }
            if (i_fawang != i_Img)
            {
                int end = i_fawang < i_link ? i_fawang : i_link;
                description = SetValue(i_description, end);
            }
            if (i_fawang != i_Img)
            {
                fawang = SetValue(i_fawang, i_link);
                if (fawang.Length > 0)
                {
                    fawang = fawang.Trim().Trim('。').Substring(3).Replace('、', ';');
                }
            }



            link = SetLinks(i_link);
            //tel = SetTelAndDanwei(i_tel);
            tel = GetTel(i_tel, "电话");
            assignmentPredict_aging();
            //assignmentTable();
        }
        private string SetLinks(int index)
        {
            string str = doctext[index].ToString();
            string strRemoveSpace = str.Replace(" ", "");
            string links = strRemoveSpace.Substring(4, strRemoveSpace.IndexOf("电话") - 4);
            return links;
        }
        private string GetTel(int index, string splitStr)
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
                telOrchuanzhen = strRemoveSpace.Substring(5, a - 5);
            }
            else if (splitStr == "No.")
            {
                telOrchuanzhen = strRemoveSpace.Substring(3, a - 3);
            }
            return telOrchuanzhen;
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
            string retStr = "";
            string str = doctext[index].ToString();
            string strRemoveSpace = str.Replace(" ", "");
            string links = strRemoveSpace.Substring(4, strRemoveSpace.IndexOf("电话")-1);
            string[] strs = str.Split(new char[] { ' '},StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>();
            for (int i = 0; i < strs.Length; i++)
            {
                if (links.Contains(strs[i]))
                {
                    list.Add(strs[i]);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Length == 1)
                {
                    retStr += list[i];
                }
                else
                {
                    retStr += list[i] + ";";
                }
            }
            return retStr;
        }
        private string SetTelAndDanwei(int index)
        {
            string str = doctext[index].ToString().Replace(" ", "");
            senddanwei = str.Substring(5, str.IndexOf("电话")-1);
            tel = str.Substring(str.IndexOf("电话") + 3);
            return "";
        }
        /// <summary>
        /// 表格解析
        /// </summary>
        private void assignmentTable()
        {
            if (table_str != null)
            {
                table_xml = new string[4, 7];
                for (int h = 0; h < table_str.GetLength(0); h++)
                {
                    string name = table_str[h, 0].ToString();
                    if (name.Contains("辽东湾"))
                    {
                        table_xml[0, 0] = "070101";
                        table_xml[0, 1] = "辽东湾";
                        string terminal = table_str[h, 1] == null ? "-" : table_str[h, 1].ToString().Replace(" ", "");
                        table_xml[0, 2] = dataNum(terminal, 1);
                        table_xml[0, 3] = dataNum(terminal, 2);
                        string general = table_str[h, 2] == null ? "-" : table_str[h, 2].ToString().Replace(" ", "");
                        table_xml[0, 4] = dataNum(general, 1);
                        table_xml[0, 5] = dataNum(general, 2);
                        string max = table_str[h, 3] == null ? "-" : table_str[h, 3].ToString().Replace(" ", "");
                        table_xml[0, 6] = max;
                    }
                    else if (name.Contains("渤海湾"))
                    {
                        table_xml[1, 0] = "070102";
                        table_xml[1, 1] = "渤海湾";
                        string terminal = table_str[h, 1] == null ? "-" : table_str[h, 1].ToString().Replace(" ", "");
                        table_xml[1, 2] = dataNum(terminal, 1);
                        table_xml[1, 3] = dataNum(terminal, 2);
                        string general = table_str[h, 2] == null ? "-" : table_str[h, 2].ToString().Replace(" ", "");
                        table_xml[1, 4] = dataNum(general, 1);
                        table_xml[1, 5] = dataNum(general, 2);
                        string max = table_str[h, 3] == null ? "-" : table_str[h, 3].ToString().Replace(" ", "");
                        table_xml[1, 6] = max;
                    }
                    else if (name.Contains("莱州湾"))
                    {
                        table_xml[2, 0] = "070103";
                        table_xml[2, 1] = "莱州湾";
                        string terminal = table_str[h, 1] == null ? "-" : table_str[h, 1].ToString().Replace(" ", "");
                        table_xml[2, 2] = dataNum(terminal, 1);
                        table_xml[2, 3] = dataNum(terminal, 2);
                        string general = table_str[h, 2] == null ? "-" : table_str[h, 2].ToString().Replace(" ", "");
                        table_xml[2, 4] = dataNum(general, 1);
                        table_xml[2, 5] = dataNum(general, 2);
                        string max = table_str[h, 3] == null ? "-" : table_str[h, 3].ToString().Replace(" ", "");
                        table_xml[2, 6] = max;
                    }
                    else if (name.Contains("黄海北部"))
                    {
                        table_xml[3, 0] = "070201";
                        table_xml[3, 1] = "黄海北部";
                        string terminal = table_str[h, 1] == null ? "-" : table_str[h, 1].ToString().Replace(" ", "");
                        table_xml[3, 2] = dataNum(terminal, 1);
                        table_xml[3, 3] = dataNum(terminal, 2);
                        string general = table_str[h, 2] == null ? "-" : table_str[h, 2].ToString().Replace(" ", "");
                        table_xml[3, 4] = dataNum(general, 1);
                        table_xml[3, 5] = dataNum(general, 2);
                        string max = table_str[h, 3] == null ? "-" : table_str[h, 3].ToString().Replace(" ", "");
                        table_xml[3, 6] = max;
                    }
                }
            }
        }

        //post_time 文档发布时间 赋值 返回改行下标
        private int assignmentPosttime(int i)
        {
            string str;
            int j = -1;
            for (j = i; j < doctext.Count; j++)
            {
                str = doctext[j].ToString();
                str = str.Replace(" ", "");
                if (str.IndexOf("发布时间") == 0)
                {

                    string ye = str.Substring(str.IndexOf("年") - 4, 4);
                    string mo = str.Substring(str.IndexOf("年") + 1, str.IndexOf("月") - str.IndexOf("年") - 1);
                    string day = str.Substring(str.IndexOf("月") + 1, str.IndexOf("日") - str.IndexOf("月") - 1);
                    if (mo.Length == 1)
                        mo = "0" + mo;
                    if (day.Replace(" ", "").Length == 1)
                        day = "0" + day;
                    //post_time = ye + mo + day + "00";
                    post_time =Convert.ToDateTime(ye+ "-" + mo+"-" + day);
                    int index = str.IndexOf("传真");
                    chuanzhen = str.Substring(index+3);
                    break;
                }
            }
            return j;
        }


        private string tableNum(string[,] str)
        {
            int i = str.GetLength(0) - 1;
            int j = str.GetLength(1) - 1;
            for (int ii = i; ii >= 0; ii--)
            {
                for (int jj = j; jj >= 0; jj--)
                {
                    if (str[ii, jj] != null)
                    {
                        if (str[ii, jj].ToString().Replace(" ", "").Replace("\r", "").Replace("\a", "").Replace("\f", "").Length > 0)
                        {
                            return str[ii, jj].ToString().Replace(" ", "").Replace("\r", "").Replace("\a", "").Replace("\f", "");
                        }
                    }
                }
            }
            return "";
        }

        //获取word转换为arraylist数组,获取图片Bitmap，获取表格table_str
        public ArrayList getDocText(string filepath)
        {
            Word.Application app = new Word.Application();
            Word.Document doc = null;
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
                Word.Paragraph item = doc.Paragraphs[j];
                if (item.Range.InlineShapes.Count == 0)
                {
                    str.Add(item.Range.Text.Replace("\r", "").Replace("\a", "").Replace("\f", "").Trim());
                }

            }
            if (doc.Tables.Count == 1)
            {
                Word.Table nowTable = doc.Tables[1];
                table_str = new string[nowTable.Rows.Count, nowTable.Columns.Count];
                for (int rowPos = 1; rowPos <= nowTable.Rows.Count; rowPos++)
                {
                    for (int columPos = 1; columPos <= nowTable.Columns.Count; columPos++)
                    {
                        try
                        {
                            table_str[rowPos - 1, columPos - 1] = nowTable.Cell(rowPos, columPos).Range.Text.Replace("\r", "").Replace("\a", "");
                        }
                        catch (System.Runtime.InteropServices.COMException comex)
                        {
                        }
                    }
                }
            }

            doc.Close();
            app.Quit();
            return str;
        }
        //
        private string dataNum(string str, int i)
        {
            string num1 = "";
            string num2 = "";
            if (str.Contains("~"))
            {
                num1 = str.Substring(0, str.IndexOf("~"));
                if (num1.Length == 0)
                    num1 = str;
                num2 = str.Substring(str.IndexOf("~") + 1, str.Length - str.IndexOf("~") - 1);
            }
            else if (str.Contains("-"))
            {
                num1 = str.Substring(0, str.IndexOf("-"));
                if (num1.Length == 0)
                    num1 = str;
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
    }
}