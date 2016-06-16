using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxEmail
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public virtual VxTelephone VxTelephone { get; set; }
    }
}
