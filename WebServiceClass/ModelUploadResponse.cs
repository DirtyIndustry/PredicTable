using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.WebServiceClass
{
    public class ModelUploadResponse
    {
        public string StationName { get; set; } = "";
        public string Station { get; set; } = "";
        public int TotalCount { get; set; } = 0;
        public int InsertCount { get; set; } = 0;
        public int UpdateCount { get; set; } = 0;
        public DateTime FirstDate { get; set; } = DateTime.Now;
        public DateTime LastDate { get; set; } = DateTime.Now;
        public string Message { get; set; } = "";
    }
}