using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class FAQ
    {
        public int Id { get; set; }
        public string FaqQuestion { get; set; }
        public string FaqAnswer { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
