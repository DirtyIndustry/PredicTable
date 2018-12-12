using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PredicTable.WebServiceClass
{
    public class ModelAmShortReport
    {
        public string reportStatus { get; set; } = "";
        public string reportTitle { get; set; } = "";
        public string reportStatusDesc { get; set; } = "";
        public bool reportButtonDisable { get; set; } = false;
        public List<int> datasource { get; set; } = new List<int>();
    }
}