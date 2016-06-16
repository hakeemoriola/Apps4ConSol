using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class MatchMetaColumn
    {
        public int Id { get; set; }
        public Nullable<int> DbId { get; set; }
        public Nullable<int> TableId { get; set; }
        public string DBColumn { get; set; }
        public string BaseDbColumn { get; set; }
        public bool IsDColumn { get; set; }
    }
}
