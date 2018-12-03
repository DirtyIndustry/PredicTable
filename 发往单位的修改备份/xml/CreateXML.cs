using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class CreateXML
{
    public static void CreateSilkXML(DateTime dtime)
    {
        string[] fhl = "增至;减至".Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        string[] fhf = "转".Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "xml");
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }

        #region 潮汐
        FWLXMLEntity fwl24 = new FWLXMLEntity();
        FWLXMLEntity fwl48 = new FWLXMLEntity();
        FWLXMLEntity fwl72 = new FWLXMLEntity();
        fwl24.post_time = dtime.ToString("yyyyMMdd") + "10";

        fwl24.predict_aging = "024h";

        fwl48.post_time = dtime.ToString("yyyyMMdd") + "10";

        fwl48.predict_aging = "048h";

        fwl72.post_time = dtime.ToString("yyyyMMdd") + "10";

        fwl72.predict_aging = "072h";

        DataTable HT_SILKTIDE24 = GetOracleData.GetHT_SILKTIDE(dtime, dtime.AddDays(1));
        if (HT_SILKTIDE24.Rows.Count == 0)
        {
            fwl24 = null;
        }
        else
        {
            foreach (DataRow dr in HT_SILKTIDE24.Rows)
            {
                FWLEntity entity = new FWLEntity(dr["HTLHARBOUR"].ToString(), dr["HTLFIRSTWAVEOFTIME"].ToString(), dr["HTLFIRSTWAVETIDELEVEL"].ToString(), dr["HTLFIRSTTIMELOWTIDE"].ToString(), dr["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString(), dr["HTLSECONDWAVEOFTIME"].ToString(), dr["HTLSECONDWAVETIDELEVEL"].ToString(), dr["HTLSECONDTIMELOWTIDE"].ToString(), dr["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString());
                fwl24.entitys.Add(entity);
            }
        }
        DataTable HT_SILKTIDE48 = GetOracleData.GetHT_SILKTIDE(dtime, dtime.AddDays(2));
        if (HT_SILKTIDE48.Rows.Count == 0)
        {
            fwl48 = null;
        }
        else
        {
            foreach (DataRow dr in HT_SILKTIDE48.Rows)
            {
                FWLEntity entity = new FWLEntity(dr["HTLHARBOUR"].ToString(), dr["HTLFIRSTWAVEOFTIME"].ToString(), dr["HTLFIRSTWAVETIDELEVEL"].ToString(), dr["HTLFIRSTTIMELOWTIDE"].ToString(), dr["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString(), dr["HTLSECONDWAVEOFTIME"].ToString(), dr["HTLSECONDWAVETIDELEVEL"].ToString(), dr["HTLSECONDTIMELOWTIDE"].ToString(), dr["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString());
                fwl48.entitys.Add(entity);
            }
        }
        DataTable HT_SILKTIDE72 = GetOracleData.GetHT_SILKTIDE(dtime, dtime.AddDays(3));
        if (HT_SILKTIDE72.Rows.Count == 0)
        {
            fwl72 = null;
        }
        else
        {
            foreach (DataRow dr in HT_SILKTIDE72.Rows)
            {
                FWLEntity entity = new FWLEntity(dr["HTLHARBOUR"].ToString(), dr["HTLFIRSTWAVEOFTIME"].ToString(), dr["HTLFIRSTWAVETIDELEVEL"].ToString(), dr["HTLFIRSTTIMELOWTIDE"].ToString(), dr["HTLLOWTIDELEVELFORTHEFIRSTTIME"].ToString(), dr["HTLSECONDWAVEOFTIME"].ToString(), dr["HTLSECONDWAVETIDELEVEL"].ToString(), dr["HTLSECONDTIMELOWTIDE"].ToString(), dr["HTLLOWTIDELEVELFORTHESECONDTIM"].ToString());
                fwl72.entitys.Add(entity);
            }
        }
        #endregion

        #region 海浪、海风
        FHLXMLEntity fhl24 = new FHLXMLEntity();
        FHLXMLEntity fhl48 = new FHLXMLEntity();
        FHLXMLEntity fhl72 = new FHLXMLEntity();

        fhl24.post_time = dtime.ToString("yyyyMMdd") + "10";

        fhl24.predict_aging = "024h";
        fhl24.linkman = "海浪预报员HBY2006001";
        fhl24.tel = "13612345678";

        fhl48.post_time = dtime.ToString("yyyyMMdd") + "10";

        fhl48.predict_aging = "048h";
        fhl48.linkman = "海浪预报员HBY2006001";
        fhl48.tel = "13612345678";

        fhl72.post_time = dtime.ToString("yyyyMMdd") + "10";

        fhl72.predict_aging = "072h";
        fhl72.linkman = "海浪预报员HBY2006001";
        fhl72.tel = "13612345678";

        FHFXMLEntity fhf24 = new FHFXMLEntity();
        FHFXMLEntity fhf48 = new FHFXMLEntity();
        FHFXMLEntity fhf72 = new FHFXMLEntity();

        fhf24.post_time = dtime.ToString("yyyyMMdd") + "10";

        fhf24.predict_aging = "024h";
        fhf24.linkman = "海浪预报员HBY2006001";
        fhf24.tel = "13612345678";


        fhf48.post_time = dtime.ToString("yyyyMMdd") + "10";

        fhf48.predict_aging = "048h";
        fhf48.linkman = "海浪预报员HBY2006001";
        fhf48.tel = "13612345678";

        fhf72.post_time = dtime.ToString("yyyyMMdd") + "10";

        fhf72.predict_aging = "072h";
        fhf72.linkman = "海浪预报员HBY2006001";
        fhf72.tel = "13612345678";

        DataTable dt24 = GetOracleData.GetHT_SILKWINDWAVE(dtime, dtime.AddDays(1));
        if (dt24.Rows.Count == 0)
        {
            fhl24 = null;
            fhf24 = null;
        }
        else
        {
            foreach (DataRow dr in dt24.Rows)
            {
                FHLEntity fhlEntity = new FHLEntity(dr["REPORTAREA"].ToString(), StaticClass.StrTo5String(dr["YRBHWWFWAVEHEIGHT"].ToString(), fhl));
                fhlEntity.SetWave_dir(StaticClass.StrTo2String(dr["YRBHWWFWAVEDIR"].ToString(), fhl));
                FHFEntity fhfEntity = new FHFEntity(dr["REPORTAREA"].ToString(), StaticClass.StrTo2String(dr["YRBHWWFFLOWDIR"].ToString(), fhf), dr["YRBHWWFFLOWLEVEL"].ToString());
                fhl24.entitys.Add(fhlEntity);
                fhf24.entitys.Add(fhfEntity);
            }
        }

        DataTable dt48 = GetOracleData.GetHT_SILKWINDWAVE(dtime, dtime.AddDays(2));
        if (dt48.Rows.Count == 0)
        {
            fhl48 = null;
            fhf48 = null;
        }
        else
        {
            foreach (DataRow dr in dt48.Rows)
            {
                FHLEntity fhlEntity = new FHLEntity(dr["REPORTAREA"].ToString(), StaticClass.StrTo5String(dr["YRBHWWFWAVEHEIGHT"].ToString(), fhl));
                fhlEntity.SetWave_dir(StaticClass.StrTo2String(dr["YRBHWWFWAVEDIR"].ToString(), fhl));
                FHFEntity fhfEntity = new FHFEntity(dr["REPORTAREA"].ToString(), StaticClass.StrTo2String(dr["YRBHWWFFLOWDIR"].ToString(), fhf), dr["YRBHWWFFLOWLEVEL"].ToString());
                fhl48.entitys.Add(fhlEntity);
                fhf48.entitys.Add(fhfEntity);
            }
        }

        DataTable dt72 = GetOracleData.GetHT_SILKWINDWAVE(dtime, dtime.AddDays(3));
        if (dt72.Rows.Count == 0)
        {
            fhl72 = null;
            fhf72 = null;
        }
        else
        {
            foreach (DataRow dr in dt72.Rows)
            {
                FHLEntity fhlEntity = new FHLEntity(dr["REPORTAREA"].ToString(), StaticClass.StrTo5String(dr["YRBHWWFWAVEHEIGHT"].ToString(), fhl));
                fhlEntity.SetWave_dir(StaticClass.StrTo2String(dr["YRBHWWFWAVEDIR"].ToString(), fhl));
                FHFEntity fhfEntity = new FHFEntity(dr["REPORTAREA"].ToString(), StaticClass.StrTo2String(dr["YRBHWWFFLOWDIR"].ToString(), fhf), dr["YRBHWWFFLOWLEVEL"].ToString());
                fhl72.entitys.Add(fhlEntity);
                fhf72.entitys.Add(fhfEntity);
            }
        }
        List<string> list = new List<string>();
        try
        {
            CreateXmlFHF.CreateFile(fhf24, path + "\\BH_F_370200_GK_" + fhf24.post_time + "_024h_FHF.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhf48.post_time + "_024h_FHF.xml");
        }
        catch { }
        try
        {
            CreateXmlFHF.CreateFile(fhf48, path + "\\BH_F_370200_GK_" + fhf48.post_time + "_048h_FHF.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhf48.post_time + "_048h_FHF.xml");
        }
        catch { }
        try
        {
            CreateXmlFHF.CreateFile(fhf72, path + "\\BH_F_370200_GK_" + fhf72.post_time + "_072h_FHF.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhf72.post_time + "_072h_FHF.xml");
        }
        catch { }

        try
        {
            CreateXmlFHL.CreateFile(fhl24, path + "\\BH_F_370200_GK_" + fhl24.post_time + "_024h_FHL.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhl24.post_time + "_024h_FHL.xml");
        }
        catch { }
        try
        {
            CreateXmlFHL.CreateFile(fhl48, path + "\\BH_F_370200_GK_" + fhl48.post_time + "_048h_FHL.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhl48.post_time + "_048h_FHL.xml");
        }
        catch { }
        try
        {
            CreateXmlFHL.CreateFile(fhl72, path + "\\BH_F_370200_GK_" + fhl72.post_time + "_072h_FHL.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhl72.post_time + "_072h_FHL.xml");
        }
        catch { }
        try
        {
            CreateXmlFWL.CreateFile(fwl24, path + "\\BH_F_370200_GK_" + fhl24.post_time + "_024h_FWL.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhl24.post_time + "_024h_FWL.xml");
        }
        catch { }
        try
        {
            CreateXmlFWL.CreateFile(fwl48, path + "\\BH_F_370200_GK_" + fhl48.post_time + "_048h_FWL.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhl48.post_time + "_048h_FWL.xml");
        }
        catch { }
        try
        {
            CreateXmlFWL.CreateFile(fwl72, path + "\\BH_F_370200_GK_" + fhl72.post_time + "_072h_FWL.xml");
            list.Add(path + "\\BH_F_370200_GK_" + fhl72.post_time + "_072h_FWL.xml");
        }
        catch { }


        if (!System.IO.Directory.Exists(path + "\\upfile\\"))
            System.IO.Directory.CreateDirectory(path + "\\upfile\\");
        foreach (string str in list)
        {
            int i = 0;
            Exception exception = null;
            while (i != 3)
            {
                try
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(str);
                    FtpClient.DirectlyFTPUploadFile(fileInfo);
                    i = 3;
                    exception = null;
                    fileInfo.CopyTo(System.IO.Path.Combine(path, "upfile\\" + fileInfo.Name), true);
                    fileInfo.Delete();

                }
                catch (Exception ex)
                {
                    exception = ex;
                    i++;
                }
            }
            if (exception != null)
            {
                WriteLog.Write(exception.ToString());
            }
        }
        #endregion



    }
}
