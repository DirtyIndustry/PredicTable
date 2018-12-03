using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.WebServiceClass
{
    public class ModelEditResponse
    {
        public bool Success { get; set; } = false;
        public int AffectedRowCount { get; set; } = 0;
        public List<object> NewData { get; set; } = new List<object>();
        public bool NewFakeData { get; set; } = false;
    }
}