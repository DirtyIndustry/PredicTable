using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.WebServiceClass
{
    public class ModelEditResponse
    {
        public bool Success { get; set; } = false;
        public string Description { get; set; } = "";
        public int AffectedRowCount { get; set; } = 0;
        public List<object> NewData { get; set; } = new List<object>();
        public int NewFakeData { get; set; } = 0;
    }
}