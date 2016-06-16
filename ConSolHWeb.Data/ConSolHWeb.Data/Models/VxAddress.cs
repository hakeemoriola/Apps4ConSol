using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxAddress
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string FullAddress { get; set; }
        public Nullable<int> AddrSet { get; set; }
        public string State { get; set; }
        public string State1 { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string LGA { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }
    }
}
