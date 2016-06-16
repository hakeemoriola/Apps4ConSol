using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxDataPoint
    {
        public string Title { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string NAME { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<int> AddrSet { get; set; }
        public string Town { get; set; }
        public string MOBILENUMBER1 { get; set; }
        public string MOBILENUMBER2 { get; set; }
        public string Occupation { get; set; }
        public string JOBSTATUS { get; set; }
        public string Email { get; set; }
        public string Industry { get; set; }
        public string STATE { get; set; }
        public string LGA { get; set; }
        public Nullable<int> Source { get; set; }
    }
}
