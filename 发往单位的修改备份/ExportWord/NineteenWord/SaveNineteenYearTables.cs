using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace PredicTable.ExportWord.NineteenWord
{
    public class SaveNineteenYearTables
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
        public string fawang = "";
        /// <summary>
        /// 传真
        /// </summary>
        public string chuanzhen = "";
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


        public SaveNineteenYearTables(string filepaths)
        {
            this.filepath = filepaths;
            doctext = getDocText(filepath);
        }
        public void assignment()
        {
            int i_tel = GetIndex(0, "电话");
            int i_chuanzhen = GetIndex(0, "传真");
            int i_time = assignmentPosttime(0);//文档发布日期
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


            //link = SetLink(i_link);
            //tel = SetTelOrChuanzhen(i_tel);

            //link = SetLink(i_link);
            //tel = SetTelOrChuanzhen(i_tel);

            link = SetLinks(i_link);
            //tel = SetTelOrChuanzhen(i_tel);
            tel = GetTel(i_tel, "电话");

            //chuanzhen = SetTelOrChuanzhen(i_chuanzhen);
            chuanzhen = isExistChuanZhen(i_chuanzhen, "No.") == true ? GetTel(i_chuanzhen, "No.") : SetTelOrChuanzhen(i_chuanzhen);
            //assignmentTable();//对表格解析
        }
        private bool isExistChuanZhen(int i_chuanzhen, string strString)
        {
            string str = doctext[i_chuanzhen].ToString();
            if (str.Contains(strString))
            {

                return true;
            }
            return false;
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
        /// <summary>
        /// 获取types在哪一行
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="types"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 获取startIndex到endIndex的字符串
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
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
            string links = strRemoveSpace.Substring(4, strRemoveSpace.IndexOf("电话") - 1);
            string[] strs = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
        private string SetTelOrChuanzhen(int index)
        {
            string str = doctext[index].ToString();
            if (str.Contains(":"))
            {
                return str.Substring(str.IndexOf(":") + 1);
            }
            if (str.Contains("："))
            {
                return str.Substring(str.IndexOf(":") + 1);
            }
            return "";
        }
        /// <summary>
        /// 3个表格分别解析到相应的table_xml_X中
        /// </summary>
        private void assignmentTable()
        {
            //对表1的解析
            assignmentTable_1();//entities
            //对表2的解析
            assignmentTable_2();//attached
            //对表3的解析
            assignmentTable_3();//attached
        }
        private void assignmentTable_1()
        {
            if (table_str_1 != null)
            {
                table_xml_1 = new string[4, 6];
                for (int h = 0; h < 5; h++)
                {
                    string name = table_str_1[h, 0].ToString();
                    if (name.Contains("辽东湾"))
                    {
                        table_xml_1[0, 0] = "070101";
                        table_xml_1[0, 1] = "辽东湾";
                        table_xml_1[0, 2] = table_str_1[h, 1].ToString().Replace(" ", "");
                        table_xml_1[0, 3] = table_str_1[h, 2].ToString().Replace(" ", "") + "-" + table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_1[0, 4] = table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_1[0, 5] = table_str_1[h, 4].ToString().Replace(" ", "");
                    }
                    else if (name.Contains("渤海湾"))
                    {
                        table_xml_1[1, 0] = "070102";
                        table_xml_1[1, 1] = "渤海湾";
                        table_xml_1[1, 2] = table_str_1[h, 1].ToString().Replace(" ", "");
                        table_xml_1[1, 3] = table_str_1[h, 2].ToString().Replace(" ", "") + "-" + table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_1[1, 4] = table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_1[1, 5] = table_str_1[h, 4].ToString().Replace(" ", "");
                    }
                    else if (name.Contains("莱州湾"))
                    {
                        table_xml_1[2, 0] = "070103";
                        table_xml_1[2, 1] = "莱州湾";
                        table_xml_1[2, 2] = table_str_1[h, 1].ToString().Replace(" ", "");
                        table_xml_1[2, 3] = table_str_1[h, 2].ToString().Replace(" ", "") + "-" + table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_1[2, 4] = table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_1[2, 5] = table_str_1[h, 4].ToString().Replace(" ", "");
                    }
                    else if (name.Contains("黄海北部"))
                    {
                        table_xml_1[3, 0] = "070201";
                        table_xml_1[3, 1] = "黄海北部";
                        table_xml_1[3, 2] = table_str_1[h, 1].ToString().Replace(" ", "");
                        table_xml_1[3, 3] = table_str_1[h, 2].ToString().Replace(" ", "") + "-" + table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_1[3, 4] = table_str_1[h, 3].ToString().Replace(" ", "");
                        table_xml_1[3, 5] = table_str_1[h, 4].ToString().Replace(" ", "");
                    }
                }
            }
        }
        private void assignmentTable_2()
        {
            if (table_str_2 != null)
            {
                table_xml_2 = new string[4, 7];
                for (int h = 0; h < 5; h++)
                {
                    string name = table_str_2[h, 0].ToString();
                    if (name.Contains("辽东湾"))
                    {
                        table_xml_2[0, 0] = "070101";
                        table_xml_2[0, 1] = "辽东湾";
                        string terminal = table_str_2[h, 1].ToString().Replace(" ", "");
                        table_xml_2[0, 2] = dataNum(terminal, 1);
                        table_xml_2[0, 3] = dataNum(terminal, 2);
                        string general = table_str_2[h, 2].ToString().Replace(" ", "");
                        table_xml_2[0, 4] = dataNum(general, 1);
                        table_xml_2[0, 5] = dataNum(general, 2);
                        string max = table_str_2[h, 3].ToString().Replace(" ", "");
                        table_xml_2[0, 6] = max;
                    }
                    else if (name.Contains("渤海湾"))
                    {
                        table_xml_2[1, 0] = "070102";
                        table_xml_2[1, 1] = "渤海湾";
                        string terminal = table_str_2[h, 1].ToString().Replace(" ", "");
                        table_xml_2[1, 2] = dataNum(terminal, 1);
                        table_xml_2[1, 3] = dataNum(terminal, 2);
                        string general = table_str_2[h, 2].ToString().Replace(" ", "");
                        table_xml_2[1, 4] = dataNum(general, 1);
                        table_xml_2[1, 5] = dataNum(general, 2);
                        string max = table_str_2[h, 3].ToString().Replace(" ", "");
                        table_xml_2[1, 6] = max;
                    }
                    else if (name.Contains("莱州湾"))
                    {
                        table_xml_2[2, 0] = "070103";
                        table_xml_2[2, 1] = "莱州湾";
                        string terminal = table_str_2[h, 1].ToString().Replace(" ", "");
                        table_xml_2[2, 2] = dataNum(terminal, 1);
                        table_xml_2[2, 3] = dataNum(terminal, 2);
                        string general = table_str_2[h, 2].ToString().Replace(" ", "");
                        table_xml_2[2, 4] = dataNum(general, 1);
                        table_xml_2[2, 5] = dataNum(general, 2);
                        string max = table_str_2[h, 3].ToString().Replace(" ", "");
                        table_xml_2[2, 6] = max;
                    }
                    else if (name.Contains("黄海北部"))
                    {
                        table_xml_2[3, 0] = "070201";
                        table_xml_2[3, 1] = "黄海北部";
                        string terminal = table_str_2[h, 1].ToString().Replace(" ", "");
                        table_xml_2[3, 2] = dataNum(terminal, 1);
                        table_xml_2[3, 3] = dataNum(terminal, 2);
                        string general = table_str_2[h, 2].ToString().Replace(" ", "");
                        table_xml_2[3, 4] = dataNum(general, 1);
                        table_xml_2[3, 5] = dataNum(general, 2);
                        string max = table_str_2[h, 3].ToString().Replace(" ", "");
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

        //post_time 文档发布时间 赋值 返回改行下标
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
                    post_time =Convert.ToDateTime(ye+"-" + mo+"-" + day);
                    seddanwei = str.Substring(0, str.IndexOf("年") - 4);
                    break;
                }
            }
            return j;
        }

        //获取word转换为arraylist数组,获取图片Bitmap，获取表格table_str
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
            //3个表格保存
            if (doc.Tables.Count == 3)
            {
                Table nowTable_1 = doc.Tables[1];
                Table nowTable_2 = doc.Tables[2];
                if (nowTable_1.Columns.Count == 5)
                {
                    table_str_1 = new string[nowTable_1.Rows.Count, nowTable_1.Columns.Count];
                    for (int rowPos = 1; rowPos <= nowTable_1.Rows.Count; rowPos++)
                    {
                        for (int columPos = 1; columPos <= nowTable_1.Columns.Count; columPos++)
                        {
                            try
                            {
                                table_str_1[rowPos - 1, columPos - 1] = nowTable_1.Cell(rowPos, columPos).Range.Text.Replace("\r", "").Replace("\a", "");
                            }
                            catch (System.Runtime.InteropServices.COMException comex)
                            {
                            }
                        }
                    }
                    table_str_2 = new string[nowTable_2.Rows.Count, nowTable_2.Columns.Count];
                    for (int rowPos = 1; rowPos <= nowTable_2.Rows.Count; rowPos++)
                    {
                        for (int columPos = 1; columPos <= nowTable_2.Columns.Count; columPos++)
                        {
                            try
                            {
                                table_str_2[rowPos - 1, columPos - 1] = nowTable_2.Cell(rowPos, columPos).Range.Text.Replace("\r", "").Replace("\a", "");
                            }
                            catch (System.Runtime.InteropServices.COMException comex)
                            {
                            }
                        }
                    }
                }
                else if (nowTable_1.Columns.Count == 4)
                {
                    table_str_1 = new string[nowTable_2.Rows.Count, nowTable_2.Columns.Count];
                    for (int rowPos = 1; rowPos <= nowTable_1.Rows.Count; rowPos++)
                    {
                        for (int columPos = 1; columPos <= nowTable_2.Columns.Count; columPos++)
                        {
                            try
                            {
                                table_str_1[rowPos - 1, columPos - 1] = nowTable_2.Cell(rowPos, columPos).Range.Text.Replace("\r", "").Replace("\a", "");
                            }
                            catch (System.Runtime.InteropServices.COMException comex)
                            {
                            }
                        }
                    }

                    table_str_2 = new string[nowTable_1.Rows.Count, nowTable_1.Columns.Count];
                    for (int rowPos = 1; rowPos <= nowTable_1.Rows.Count; rowPos++)
                    {
                        for (int columPos = 1; columPos <= nowTable_1.Columns.Count; columPos++)
                        {
                            try
                            {
                                table_str_2[rowPos - 1, columPos - 1] = nowTable_1.Cell(rowPos, columPos).Range.Text.Replace("\r", "").Replace("\a", "");
                            }
                            catch (System.Runtime.InteropServices.COMException comex)
                            {
                            }
                        }
                    }
                }
                Table nowTable_3 = doc.Tables[3];

                if (nowTable_3.Cell(2, 1).Range.Text.Replace("\r", "").Replace("\a", "").Contains("港"))
                {
                    //山东
                    table_str_3 = new string[nowTable_3.Columns.Count, nowTable_3.Rows.Count];
                    for (int rowPos = 1; rowPos <= nowTable_3.Rows.Count; rowPos++)
                    {
                        for (int columPos = 1; columPos <= nowTable_3.Columns.Count; columPos++)
                        {
                            try
                            {
                                table_str_3[columPos - 1, rowPos - 1] = nowTable_3.Cell(columPos, rowPos).Range.Text.Replace("\r", "").Replace("\a", "");
                            }
                            catch (System.Runtime.InteropServices.COMException comex)
                            {
                            }
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
                            try
                            {
                                table_str_3[rowPos - 1, columPos - 1] = nowTable_3.Cell(rowPos, columPos).Range.Text.Replace("\r", "").Replace("\a", "");
                            }
                            catch (System.Runtime.InteropServices.COMException comex)
                            {
                            }
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
        private string tableNumfist(string[,] str)
        {
            int i = str.GetLength(0) - 1;
            int j = str.GetLength(1) - 1;
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    if (str[ii, jj].ToString().Replace(" ", "").Replace("\r", "").Replace("\a", "").Replace("\f", "").Length > 0)
                    {
                        return str[ii, jj].ToString().Replace(" ", "").Replace("\r", "").Replace("\a", "").Replace("\f", "");
                    }
                }
            }
            return "";
        }
    }
}