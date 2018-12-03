//**********************************************************************************************

//文件名(File Name)：                   sql_StromTable

//作者(Author)：                        sl

//日期(Create Date)：                   2017-02-07

//修改记录(Revision History)：
//        R1：
//         修改作者：                   sl  
//         修改日期：                   2017-02-07
//         修改理由：                   添加
//                                      风暴潮属性解析、入库
//                                      
//
//**********************************************************************************************
using PredicTable.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PredicTable.Dal
{
    public class sql_StromTable
    {
        DataExecution DataExe = new DataExecution();//声明一个数据执行类
        /// <summary>
        /// 检索数据是否存在
        /// </summary>
        /// <param name="stromModel"></param>
        /// <param name="WENJIANMING"></param>
        /// <returns></returns>
        public DataTable GetTableData(StromTableModel stromModel, string WENJIANMING)
        {
            try
            {
                string sql = "SELECT * FROM CG_HT_STROM_TABLE WHERE WENJIANMING = '" + WENJIANMING + "' AND STATION = '" + stromModel.STATION + "'";
                return DataExe.GetTableExeData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="stromModel"></param>
        /// <param name="WENJIANMING"></param>
        /// <returns></returns>
        public int InsertTableData(StromTableModel stromModel, string WENJIANMING)
        {
            string sql = "INSERT INTO CG_HT_STROM_TABLE (WENJIANMING,STATION ,PUBLISHTIME,HIGHTIME,HIGHVALUE,WARNINGTIDEVALUE,WARNINGLEVEL) "
                + " VALUES ('" + WENJIANMING + "','" + stromModel.STATION + "','" + stromModel.PUBLISHTIME + "','" + stromModel.HIGHTIME + "','" + stromModel.HIGHVALUE + "','" + stromModel.WARNINGTIDEVALUE + "','" + stromModel.WARNINGLEVEL + "')";
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int DeleteTableData(string WENJIANMING)
        {
            string sql = "DELETE FROM CG_HT_STROM_TABLE WHERE WENJIANMING = '"+ WENJIANMING + "'";
            try
            {
                return DataExe.GetIntExeData(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新表格数据
        /// </summary>
        /// <param name="stromModel"></param>
        /// <param name="WENJIANMING"></param>
        /// <returns></returns>
        public int UpdateTableData(StromTableModel stromModel, string WENJIANMING)
        {
            string sql = "UPDATE CG_HT_STROM_TABLE SET "
                + " PUBLISHTIME = '" + stromModel.PUBLISHTIME + "' ,HIGHTIME = '" + stromModel.HIGHTIME + "',HIGHVALUE = '" + stromModel.HIGHVALUE + "',WARNINGTIDEVALUE = '" + stromModel.WARNINGTIDEVALUE + "',WARNINGLEVEL = '" + stromModel.WARNINGLEVEL + "'"
                + " WHERE WENJIANMING='" + WENJIANMING + "' AND STATION = '" + stromModel.STATION + "' ";
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