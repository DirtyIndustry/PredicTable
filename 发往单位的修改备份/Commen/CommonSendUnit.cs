
using PredicTable.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PredicTable.Commen
{
    /// <summary>
    /// 发送单位公共方法
    /// </summary>
    public class CommonSendUnit
    {
        private string docName;//文档名称
        private string sendUnits;//发送单位名称
        public CommonSendUnit()
        {

        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="docName">文档名称</param>
        /// <param name="sendUnits">发送单位名称</param>
        public CommonSendUnit(string _docName,string _sendUnits)
        {
            this.docName = _docName;
            this.sendUnits = _sendUnits;
        }
       
        /// <summary>
        /// 操作发送单位
        /// </summary>
        /// <returns></returns>
        public string resultSendUnit()
        {
            sql_CommonSendUnit commonSendUnits = new sql_CommonSendUnit();
            DataTable dtchkExists = commonSendUnits.GetSendUnit(docName);
            int rultInsert = 0;//返回值
            int rultUpdate = 0;
            if (dtchkExists != null && dtchkExists.Rows.Count > 0)
            {
                rultUpdate = commonSendUnits.UpdateSendUnit(docName, sendUnits);
                if (rultUpdate == 0) {
                    return "updateFailed";
                }else
                {
                    return "updateSuccess";
                }
            }
            else
            {
                rultInsert = commonSendUnits.InsertSendUnit(docName, sendUnits);
                if (rultInsert == 0)
                {
                    return "insertFailed";
                }
                else
                {
                    return "insertSuccess";
                }
            }
        }
      
        /// <summary>
        /// 操作发送单位
        /// </summary>
        /// <returns></returns>
        public string resultSendUnitbz(string docName1, string sendUnits1,string BZ1)
        {
            sql_CommonSendUnit commonSendUnits = new sql_CommonSendUnit();
            DataTable dtchkExists = commonSendUnits.GetSendUnit(docName1);
            int rultInsert = 0;//返回值
            int rultUpdate = 0;
            if (dtchkExists != null && dtchkExists.Rows.Count > 0)
            {
                rultUpdate = commonSendUnits.UpdateSendUnit1(docName1, sendUnits1, BZ1);
                if (rultUpdate == 0)
                {
                    return "updateFailed";
                }
                else
                {
                    return "updateSuccess";
                }
            }
            else
            {
                rultInsert = commonSendUnits.InsertSendUnit1(docName1, sendUnits1, BZ1);
                if (rultInsert == 0)
                {
                    return "insertFailed";
                }
                else
                {
                    return "insertSuccess";
                }
            }
        }
    }
}