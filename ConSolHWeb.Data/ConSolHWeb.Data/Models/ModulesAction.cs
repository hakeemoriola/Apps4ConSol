using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class ModulesAction
    {
        public int Id { get; set; }
        public string ModuleId { get; set; }
        public string ModuleActionName { get; set; }
        public string Description { get; set; }
        public Nullable<int> CanAppearOnLink { get; set; }
        public string ModuleActionUrl { get; set; }
    }
}
