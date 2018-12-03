using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.ExportWord
{
    public class WordName
    {
       List<KJ_Wordname> list = new List<KJ_Wordname>();
        public List<KJ_Wordname> wordname(string num ,DateTime dt,string hour ="")
        {
            list.Clear();
            string DT=dt.ToString("yyyyMMdd")+ hour;
            KJ_Wordname kj_wordname = new KJ_Wordname();
            if (num=="1")
            {
             
                kj_wordname.a = "YB";
                kj_wordname.b = "SD";
                kj_wordname.c = "DSTZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "SDMF";
                list.Add(kj_wordname);
            }
            if (num == "Radio")
            {

                kj_wordname.a = "YB";
                kj_wordname.b = "NCS";
                kj_wordname.c = "BHSP";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "172h")
            {

                kj_wordname.a = "YB";
                kj_wordname.b = "SD";
                kj_wordname.c = "DSTZX";
                kj_wordname.d = "72hr";
                kj_wordname.e = DT;
                kj_wordname.f = "SDMF";
                list.Add(kj_wordname);
            }
            if (num == "2")
            {
                kj_wordname.a = "";
                kj_wordname.b = "";
                kj_wordname.c = "";
                kj_wordname.d = "";
                kj_wordname.e = DT;
                kj_wordname.f = "";
                list.Add(kj_wordname);
            }
            if (num == "3")
            {
               // 03.YB_SSWHH_ZX_72hr_2015052814_NMFC
                kj_wordname.a = "YB";
                kj_wordname.b = "SSWHH";
                kj_wordname.c = "ZX";
                kj_wordname.d = "72hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "4")
            {
           
                kj_wordname.a = "YB";
                kj_wordname.b = "SLOF";
                kj_wordname.c = "ZX";
                kj_wordname.d = "72hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "5")
            {
                kj_wordname.a = "YB";
                kj_wordname.b = "SLOF";
                kj_wordname.c = "ZX";
                kj_wordname.d = "7day";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "6")
            {
                kj_wordname.a = "YB";
                kj_wordname.b = "SLOF";
                kj_wordname.c = "CXZX";
                kj_wordname.d = "7day";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "7")
            {//7#YB_SLOF_HBHWZX_7day_YYYYMMDD_NMFC(冬季)
                kj_wordname.a = "YB";
                kj_wordname.b = "SLOF";
                kj_wordname.c = "HBHWZX";
                kj_wordname.d = "7day";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "8")
            {
                kj_wordname.a = "";
                kj_wordname.b = "";
                kj_wordname.c = "";
                kj_wordname.d = "";
                kj_wordname.e = DT;
                kj_wordname.f = "";
                list.Add(kj_wordname);
            }
            if (num == "9")
            {
              
                kj_wordname.a = "YB";
                kj_wordname.b = "QD";
                kj_wordname.c = "ZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "10")
            {
               
                kj_wordname.a = "YB";
                kj_wordname.b = "NPOF";
                kj_wordname.c = "ZX";
                kj_wordname.d = "72hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "11")
            {
              
                kj_wordname.a = "YB";
                kj_wordname.b = "QD";
                kj_wordname.c = "HJ";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "QDMF";
                list.Add(kj_wordname);
            }
            if (num == "12")
            {
              
                kj_wordname.a = "YB";
                kj_wordname.b = "QD";
                kj_wordname.c = "HJ";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "SDMF";
                list.Add(kj_wordname);
            }
            if (num == "13")
            {
               
                kj_wordname.a = "YB";
                kj_wordname.b = "NCS";
                kj_wordname.c = "SWZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "14")
            {
              
                kj_wordname.a = "YB";
                kj_wordname.b = "SD";
                kj_wordname.c = "WZZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "SDMF";
                list.Add(kj_wordname);
            }
            if (num == "1472h")
            {

                kj_wordname.a = "YB";
                kj_wordname.b = "SD";
                kj_wordname.c = "WZZX";
                kj_wordname.d = "72hr";
                kj_wordname.e = DT;
                kj_wordname.f = "SDMF";
                list.Add(kj_wordname);
            }
            if (num == "15")
            {
              
                kj_wordname.a = "YB";
                kj_wordname.b = "NCS";
                kj_wordname.c = "HL";
                kj_wordname.d = "72hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "16")
            {
                kj_wordname.a = "";
                kj_wordname.b = "";
                kj_wordname.c = "";
                kj_wordname.d = "";
                kj_wordname.e = DT;
                kj_wordname.f = "";
                list.Add(kj_wordname);
            }
            if (num == "17")
            {
                kj_wordname.a = "";
                kj_wordname.b = "";
                kj_wordname.c = "";
                kj_wordname.d = "";
                kj_wordname.e = DT;
                kj_wordname.f = "";
                list.Add(kj_wordname);

            }
            if (num == "18")
            {
                kj_wordname.a = "";
                kj_wordname.b = "";
                kj_wordname.c = "";
                kj_wordname.d = "";
                kj_wordname.e = DT;
                kj_wordname.f = "";
                list.Add(kj_wordname);
            }
            if (num == "19")
            {
                kj_wordname.a = "";
                kj_wordname.b = "";
                kj_wordname.c = "";
                kj_wordname.d = "";
                kj_wordname.e = DT;
                kj_wordname.f = "";
                list.Add(kj_wordname);
            }
            if (num == "20")
            {
               
                kj_wordname.a = "YB";
                kj_wordname.b = "WF";
                kj_wordname.c = "YBTZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "21")
            {
                //YB_QD_DSTZX_24hr_YYYYMMDDHH_NMFC(7、8、9月）
                kj_wordname.a = "YB";
                kj_wordname.b = "QD";
                kj_wordname.c = "DSTZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "22")
            {
             
                kj_wordname.a = "YB";
                kj_wordname.b = "QD";
                kj_wordname.c = "DSTZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);

            }
            if (num == "23")
            {
                kj_wordname.a = "";
                kj_wordname.b = "";
                kj_wordname.c = "";
                kj_wordname.d = "";
                kj_wordname.e = DT;
                kj_wordname.f = "";
                list.Add(kj_wordname);
            }
            if (num == "24")
            {
             
                kj_wordname.a = "YB";
                kj_wordname.b = "DY";
                kj_wordname.c = "ZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "25")
            {
              
                kj_wordname.a = "YB";
                kj_wordname.b = "WH";
                kj_wordname.c = "DSTZX";
                kj_wordname.d = "48hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }
            if (num == "26")
            {
              
                kj_wordname.a = "YB";
                kj_wordname.b = "DY";
                kj_wordname.c = "SXGZX";
                kj_wordname.d = "72hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC";
                list.Add(kj_wordname);
            }

            if (num == "zhihuichu07")
            {
                //YB_ZHC_ZX_24hr_YYYYMMDDHH_NMFC(07时发布)_无单号
                kj_wordname.a = "YB";
                kj_wordname.b = "ZHC";
                kj_wordname.c = "ZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC(07时发布)";
                list.Add(kj_wordname);
            }

            if (num == "zhihuichu16")
            {
                //YB_ZHC_ZX_24hr_YYYYMMDDHH_NMFC(16时发布)_无单号
                kj_wordname.a = "YB";
                kj_wordname.b = "ZHC";
                kj_wordname.c = "ZX";
                kj_wordname.d = "24hr";
                kj_wordname.e = DT;
                kj_wordname.f = "NMFC(16时发布)";
                list.Add(kj_wordname);
            }
            //if (num == "东营广利一级渔港")
            //{
            //    kj_wordname.a = "YB";
            //    kj_wordname.b = "DYGLFP";
            //    kj_wordname.c = "JX";
            //    kj_wordname.d = "";
            //    kj_wordname.e = DT;
            //    kj_wordname.f = "SDMF";
            //    list.Add(kj_wordname);
            //}
            //if (num == "日照桃花岛")
            //{
            //    kj_wordname.a = "YB";
            //    kj_wordname.b = "RZTHD";
            //    kj_wordname.c = "JX";
            //    kj_wordname.d = "";
            //    kj_wordname.e = DT;
            //    kj_wordname.f = "SDMF";
            //    list.Add(kj_wordname);
            //}
            //if (num == "潍坊旅游度假区")
            //{
            //    kj_wordname.a = "YB";
            //    kj_wordname.b = "WFDJQ";
            //    kj_wordname.c = "JX";
            //    kj_wordname.d = "";
            //    kj_wordname.e = DT;
            //    kj_wordname.f = "SDMF";
            //    list.Add(kj_wordname);
            //}
            //if (num == "威海南海新区")
            //{
            //    kj_wordname.a = "YB";
            //    kj_wordname.b = "WHXQ";
            //    kj_wordname.c = "JX";
            //    kj_wordname.d = "";
            //    kj_wordname.e = DT;
            //    kj_wordname.f = "SDMF";
            //    list.Add(kj_wordname);
            //}
            //if (num == "烟台清泉码头")
            //{
            //    kj_wordname.a = "YB";
            //    kj_wordname.b = "YTQQ";
            //    kj_wordname.c = "JX";
            //    kj_wordname.d = "";
            //    kj_wordname.e = DT;
            //    kj_wordname.f = "SDMF";
            //    list.Add(kj_wordname);
            //}
            //if (num == "董家口港")
            //{
            //    kj_wordname.a = "YB";
            //    kj_wordname.b = "DJKP";
            //    kj_wordname.c = "JX";
            //    kj_wordname.d = "";
            //    kj_wordname.e = DT;
            //    kj_wordname.f = "SDMF";
            //    list.Add(kj_wordname);
            //}
            //if (num == "东营渔港")
            //{
            //    kj_wordname.a = "YB";
            //    kj_wordname.b = "DYFP";
            //    kj_wordname.c = "JX";
            //    kj_wordname.d = "";
            //    kj_wordname.e = DT;
            //    kj_wordname.f = "SDMF";
            //    list.Add(kj_wordname);
            //}
            //if (num == "yuzhengju27")
            //{

            //    kj_wordname.a = "渔政局"+dt.ToString("yyyyMMdd").Remove(0, 2);


            //    list.Add(kj_wordname);
            //}
            return list;
        }

    }
}