using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class USERINFO
    {
        public int Id { get; set; }
        public int USERID { get; set; }
        public string Badgenumber { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int DEFAULTDEPTID { get; set; }
        public Nullable<short> InheritDeptSch { get; set; }
        public Nullable<short> InheritDeptSchClass { get; set; }
        public Nullable<short> AutoSchPlan { get; set; }
        public Nullable<int> Processed { get; set; }
    }
}
