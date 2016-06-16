using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSolHWeb.Data
{
    public class RawDataDashBoard
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public int TotalRecordCount { get; set; }
        public int TotalUniqueCount { get; set; }
    }
}