using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxSearchParam
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string pName { get; set; }
        public Nullable<int> pSingOrMutiple { get; set; }
        public string pValue { get; set; }
        public string pControl { get; set; }
        public Nullable<int> pOptions { get; set; }
        public string pDecision { get; set; }
    }
}
