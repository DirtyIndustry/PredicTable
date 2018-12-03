using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.ExportWord.JingBao
{
    /// <summary>
    /// 警报、解除警报Word解析
    /// </summary>
    public class WarningTranslateWord
    {
        string filepath;
        ArrayList doctext;//word文档文本内容
        /// <summary>
        /// 文档发布时间
        /// </summary>
        public DateTime post_time;//文档发布时间
        /// <summary>
        /// 冰情概况
        /// </summary>
        public string ice_situation = "";//冰情概况
        /// <summary>
        /// 预计
        /// </summary>
        public string predict = "";//预计
        public string predict_aging = "001y"; //预报时效
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
        /// <summary>
        /// 2015/2016年冬季渤海及黄海北部冰日预测
        /// </summary>
        public string[,] table_str_1;//word中的表格
        private string[,] table_xml_1;
        /// <summary>
        /// 浮冰外缘线离岸最大距离及平整冰厚度预测
        /// </summary>
        public string[,] table_str_2;//word中的表格
        private string[,] table_xml_2;
        /// <summary>
        /// 严重冰期沿岸主要港口平整冰厚度预测
        /// </summary>
        public string[,] table_str_3;//word中的表格
        private string[,] table_xml_3;
        public string filename_time = "";
        public string title = "";//标题
        public DateTime publish_time;//文档发布时间
        public string JBREMARKS = "";//警报备注
        //public string level = "";
        public WarningTranslateWord(string filepaths)
        {
            this.filepath = filepaths;
            doctext = getDocText(filepath);
        }
        public void assignmentTable() {
            assignment();
            assignmentTable_2();
            assignmentTable_3();
        }
        public void assignment()
        {
            int i_tel = GetIndex(0, "值班员","联系人");//电话与值班员在同一行
            int i_chuanzhen = GetIndex(0, "传真");
            //int i_time = assignmentPosttime(0);//文档发布日期
            int i_time = GetIndex(0,"时间");
            int i_bianhao = GetIndex(0, "编号");
            //int i_title = GetIndex(i_bianhao,"");
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
            //if (i_Ice_situation != i_time)
            //{
            //    ice_situation = SetValue(i_Ice_situation, i_Predict);
            //}
            //if (i_Predict != i_Ice_situation)
            //{
            //    predict = SetValue(i_Predict, i_Table);
            //}
            {
                //                                      没有预计                        没有表                  没有图                                没有说明
                int end = i_Ice_situation == i_Predict ? (i_Predict < i_Table ? i_Table : (i_Table < i_Img ? i_Img : (i_Img < i_description ? i_description : i_fawang))) : i_Predict;
                ice_situation = SetValue(i_Ice_situation, end);
            }
            if (i_Predict != i_time)
            {
                //                                         没有表                     没有图                                没有说明
                int end = i_Predict < i_Table ? i_Table : (i_Table < i_Img ? i_Img : (i_Img < i_description ? i_description : i_fawang));
                predict = SetValue(i_Predict, end);
            }
            if (i_Img != i_description)
            {
                description = SetValue(i_description, i_fawang);
            }
            if (i_fawang != i_description)
            {
                fawang = SetValue(i_fawang, i_link);
            }
            link = SetLink(i_link);
            tel = SetTelOrChuanzhen(i_tel,"电话");
            chuanzhen = SetTelOrChuanzhen(i_chuanzhen,"网址");
            if(i_bianhao != 0)
            {
                biaohao = SetTelOrChuanzhen(i_bianhao, "签发");
                title = getTitle(i_bianhao);
            }
            else
            {
                title = getTitle(i_time);
            }
            publish_time = SetTime(i_time, "签发");
            JBREMARKS = SetValue(i_Img, i_fawang);
        }
        private string getTitle(int i_bianhao)
        {
            for(int i = 1;i< doctext.Count; i++)
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
        private void assignmentTable_2()
        {
            if (table_str_1 != null)
            {
                table_xml_2 = new string[4, 7];
                for (int h = 0; h < 5; h++)
                {
                    string name = table_str_1[h, 0].ToString();
                    if (name.Contains("辽东湾"))
                    {
                        table_xml_2[0, 0] = "070101";
                        table_xml_2[0, 1] = "辽东湾";
                        string terminal = table_str_1[h, 1].ToString().Replace(" ", "");
                        table_xml_2[0, 2] = dataNum(terminal, 1);
                        table_xml_2[0, 3] = dataNum(terminal, 2);
                        string general = table_str_1[h, 2].ToString().Replace(" ", "");
                        table_xml_2[0, 4] = dataNum(general, 1);
                        table_xml_2[0, 5] = dataNum(general, 2);
                        string max = table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_2[0, 6] = max;
                    }
                    else if (name.Contains("渤海湾"))
                    {
                        table_xml_2[1, 0] = "070102";
                        table_xml_2[1, 1] = "渤海湾";
                        string terminal = table_str_1[h, 1].ToString().Replace(" ", "");
                        table_xml_2[1, 2] = dataNum(terminal, 1);
                        table_xml_2[1, 3] = dataNum(terminal, 2);
                        string general = table_str_1[h, 2].ToString().Replace(" ", "");
                        table_xml_2[1, 4] = dataNum(general, 1);
                        table_xml_2[1, 5] = dataNum(general, 2);
                        string max = table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_2[1, 6] = max;
                    }
                    else if (name.Contains("莱州湾"))
                    {
                        table_xml_2[2, 0] = "070103";
                        table_xml_2[2, 1] = "莱州湾";
                        string terminal = table_str_1[h, 1].ToString().Replace(" ", "");
                        table_xml_2[2, 2] = dataNum(terminal, 1);
                        table_xml_2[2, 3] = dataNum(terminal, 2);
                        string general = table_str_1[h, 2].ToString().Replace(" ", "");
                        table_xml_2[2, 4] = dataNum(general, 1);
                        table_xml_2[2, 5] = dataNum(general, 2);
                        string max = table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_2[2, 6] = max;
                    }
                    else if (name.Contains("黄海北部"))
                    {
                        table_xml_2[3, 0] = "070201";
                        table_xml_2[3, 1] = "黄海北部";
                        string terminal = table_str_1[h, 1].ToString().Replace(" ", "");
                        table_xml_2[3, 2] = dataNum(terminal, 1);
                        table_xml_2[3, 3] = dataNum(terminal, 2);
                        string general = table_str_1[h, 2].ToString().Replace(" ", "");
                        table_xml_2[3, 4] = dataNum(general, 1);
                        table_xml_2[3, 5] = dataNum(general, 2);
                        string max = table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_2[3, 6] = max;
                    }
                }
            }
        }

        private void assignmentTable_3()
        {
            if (table_str_3 != null)
            {
                table_xml_3 = new string[8, 5];
                for (int lie = 1, h = 0; lie < 9; lie++, h++)
                {
                    string name = table_str_3[0, lie].ToString();
                    if (name.Contains("营口港"))
                    {
                        table_xml_3[h, 0] = "210800";
                        table_xml_3[h, 1] = "营口港";
                        string general = table_str_3[1, lie].ToString().Replace(" ", "");

                        table_xml_3[h, 2] = dataNum(general, 1);
                        table_xml_3[h, 3] = dataNum(general, 2);

                        string max = table_str_3[2, lie].ToString().Replace(" ", "");
                        table_xml_3[h, 4] = max;
                    }
                    else if (name.Contains("锦州港"))
                    {
                        table_xml_3[h, 0] = "210700";
                        table_xml_3[h, 1] = "锦州港";
                        string general = table_str_3[1, lie].ToString().Replace(" ", "");

                        table_xml_3[h, 2] = dataNum(general, 1);
                        table_xml_3[h, 3] = dataNum(general, 2);

                        string max = table_str_3[2, lie].ToString().Replace(" ", "");
                        table_xml_3[h, 4] = max;
                    }
                    else if (name.Contains("秦皇岛港"))
                    {
                        table_xml_3[h, 0] = "130300";
                        table_xml_3[h, 1] = "秦皇岛港";
                        string general = table_str_3[1, lie].ToString().Replace(" ", "");

                        table_xml_3[h, 2] = dataNum(general, 1);
                        table_xml_3[h, 3] = dataNum(general, 2);

                        string max = table_str_3[2, lie].ToString().Replace(" ", "");
                        table_xml_3[h, 4] = max;
                    }
                    else if (name.Contains("天津港"))
                    {
                        table_xml_3[h, 0] = "120000";
                        table_xml_3[h, 1] = "天津港";
                        string general = table_str_3[1, lie].ToString().Replace(" ", "");

                        table_xml_3[h, 2] = dataNum(general, 1);
                        table_xml_3[h, 3] = dataNum(general, 2);

                        string max = table_str_3[2, lie].ToString().Replace(" ", "");
                        table_xml_3[h, 4] = max;
                    }
                    else if (name.Contains("黄骅港"))
                    {
                        table_xml_3[h, 0] = "070103";
                        table_xml_3[h, 1] = "黄骅港";
                        string general = table_str_3[1, lie].ToString().Replace(" ", "");

                        table_xml_3[h, 2] = dataNum(general, 1);
                        table_xml_3[h, 3] = dataNum(general, 2);

                        string max = table_str_3[2, lie].ToString().Replace(" ", "");
                        table_xml_3[h, 4] = max;
                    }
                    else if (name.Contains("东营港"))
                    {
                        table_xml_3[h, 0] = "370500";
                        table_xml_3[h, 1] = "东营港";
                        string general = table_str_3[1, lie].ToString().Replace(" ", "");

                        table_xml_3[h, 2] = dataNum(general, 1);
                        table_xml_3[h, 3] = dataNum(general, 2);

                        string max = table_str_3[2, lie].ToString().Replace(" ", "");
                        table_xml_3[h, 4] = max;
                    }
                    else if (name.Contains("潍坊港"))
                    {
                        table_xml_3[h, 0] = "370700";
                        table_xml_3[h, 1] = "潍坊港";
                        string general = table_str_3[1, lie].ToString().Replace(" ", "");

                        table_xml_3[h, 2] = dataNum(general, 1);
                        table_xml_3[h, 3] = dataNum(general, 2);

                        string max = table_str_3[2, lie].ToString().Replace(" ", "");
                        table_xml_3[h, 4] = max;
                    }
                    else if (name.Contains("觉华岛"))
                    {
                        table_xml_3[h, 0] = "211481";
                        table_xml_3[h, 1] = "觉华岛";
                        string general = table_str_3[1, lie].ToString().Replace(" ", "");

                        table_xml_3[h, 2] = dataNum(general, 1);
                        table_xml_3[h, 3] = dataNum(general, 2);

                        string max = table_str_3[2, lie].ToString().Replace(" ", "");
                        table_xml_3[h, 4] = max;
                    }
                }
            }
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
            if (doc.Tables.Count == 2)
            {
                Table nowTable_1 = doc.Tables[1];
                //Table nowTable_2 = doc.Tables[2];
                if (nowTable_1.Columns.Count == 4)
                {
                    table_str_1 = new string[nowTable_1.Rows.Count, nowTable_1.Columns.Count];
                    for (int rowPos = 1; rowPos <= nowTable_1.Rows.Count; rowPos++)
                    {
                        for (int columPos = 1; columPos <= nowTable_1.Columns.Count; columPos++)
                        {
                            table_str_1[rowPos - 1, columPos - 1] = nowTable_1.Cell(rowPos, columPos).Range.Text.Replace("\r", "").Replace("\a", "");
                        }
                    }
                }

                Table nowTable_3 = doc.Tables[2];

                if (nowTable_3.Cell(2, 1).Range.Text.Replace("\r", "").Replace("\a", "").Contains("港"))
                {
                    //山东
                    table_str_3 = new string[nowTable_3.Columns.Count, nowTable_3.Rows.Count];
                    for (int rowPos = 1; rowPos <= nowTable_3.Rows.Count; rowPos++)
                    {
                        for (int columPos = 1; columPos <= nowTable_3.Columns.Count; columPos++)
                        {
                            table_str_3[columPos - 1, rowPos - 1] = nowTable_3.Cell(columPos, rowPos).Range.Text.Replace("\r", "").Replace("\a", "");
                        }
                    }
                }
                else
                {
                    //北海区
                    table_str_3 = new string[nowTable_3.Rows.Count, nowTable_3.Columns.Count];
                    for (int rowPos = 1; rowPos <= nowTable_3.Rows.Count; rowPos++)
                    {
                        for (int columPos = 1; columPos <= nowTable_3.Columns.Count; columPos++)
                        {
                            table_str_3[rowPos - 1, columPos - 1] = nowTable_3.Cell(rowPos, columPos).Range.Text.Replace("\r", "").Replace("\a", "");
                        }
                    }
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
        private string SetTelOrChuanzhen(int index,string splitStr)
        {
            string str = doctext[index].ToString();
            string strRemoveSpace = str.Replace(" ", "");
            int a = strRemoveSpace.IndexOf(splitStr);
            string telOrchuanzhen = "";
            if (splitStr == "电话")
            {
                telOrchuanzhen = strRemoveSpace.Substring(a + 3);
            }
            else if(splitStr == "网址")
            {
                telOrchuanzhen = strRemoveSpace.Substring(3,a-3);
            }
            else if (splitStr == "签发")
            {
                telOrchuanzhen = strRemoveSpace.Substring(5, a - 5);
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