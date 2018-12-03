using PredicTable.Model.NineteenWord;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_CoastTable
    {
        DataExecution DataExe = new DataExecution();//声明一个数据执行类
        public DataTable GetTableData(NineteenYearCknessModel cknessModel, string WENJIANMING)
        {
            try
            {
                string sql = "SELECT * FROM CG_HT_COAST_TABLE WHERE WENJIANMING = '" + WENJIANMING + "' AND NAME = '" + cknessModel.NAME + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int InsertTableData(NineteenYearCknessModel cknessModel, string WENJIANMING)
        {
            string sql = "INSERT INTO CG_HT_COAST_TABLE (WENJIANMING,NAME ,GENERALICETHICKNESS,MAXICETHICKNESS) VALUES ('" + WENJIANMING + "','" + cknessModel.NAME + "','" + cknessModel.GENERALICETHICKNESS + "','" + cknessModel.MAXICETHICKNESS + "')";
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateTableData(NineteenYearCknessModel cknessModel, string WENJIANMING)
        {
            string sql = "UPDATE CG_HT_COAST_TABLE SET  GENERALICETHICKNESS = '" + cknessModel.GENERALICETHICKNESS + "',MAXICETHICKNESS = '" + cknessModel.MAXICETHICKNESS + "' WHERE WENJIANMING='" + WENJIANMING + "' AND NAME = '" + cknessModel.NAME + "'";
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}