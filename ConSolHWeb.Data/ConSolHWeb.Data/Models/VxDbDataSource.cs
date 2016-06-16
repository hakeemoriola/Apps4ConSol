using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxDbDataSource
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<int> DbSource { get; set; }
        public virtual VxTelephone VxTelephone { get; set; }
    }
}
