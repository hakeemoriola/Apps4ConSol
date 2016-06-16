using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxTelephone
    {
        public VxTelephone()
        {
            this.VxDbDataSources = new List<VxDbDataSource>();
        }

        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string TPhoneNumber { get; set; }
        public string PrefixNo { get; set; }
        public string DbSourceString { get; set; }
        public virtual VxCustomer VxCustomer { get; set; }
        public virtual ICollection<VxDbDataSource> VxDbDataSources { get; set; }
        public virtual VxEmail VxEmail { get; set; }
    }
}
