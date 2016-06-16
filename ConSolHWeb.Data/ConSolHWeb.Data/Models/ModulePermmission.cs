using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class ModulePermmission
    {
        public string RoleName { get; set; }
        public string ModuleID { get; set; }
        public int hasView { get; set; }
        public int hasEdit { get; set; }
        public int hasAdd { get; set; }
        public int hasDelete { get; set; }
    }
}
