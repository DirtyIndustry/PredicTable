using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

class GetOracleData
{
    public static DataTable GetHT_SILKTIDE(DateTime dt1, DateTime dt2)
    {
        string sql = "select * from HT_SILKTIDE where PUBLISHDATE=to_date('" + dt1.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and FORECASTDATE=to_date('" + dt2.ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
        return new DataExecution().GetTableExeData(sql);
        //return OracleHelper.ExecuteDt(sql);
    }
    public static DataTable GetHT_SILKWINDWAVE(DateTime dt1, DateTime dt2)
    {
        string sql = "select * from HT_SILKWINDWAVE where PUBLISHDATE=to_date('" + dt1.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and FORECASTDATE=to_date('" + dt2.ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
        return new DataExecution().GetTableExeData(sql);
        //return OracleHelper.ExecuteDt(sql);

    }
}

