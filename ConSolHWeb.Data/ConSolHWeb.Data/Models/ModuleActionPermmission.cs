using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class ModuleActionPermmission
    {
        public int Id { get; set; }
        public string ModuleId { get; set; }
        public Nullable<int> ModuleActionId { get; set; }
        public Nullable<int> MAPValue { get; set; }
        public string RoleName { get; set; }
    }
}
