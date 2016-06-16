using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class DBMetaColumn
    {
        public int Id { get; set; }
        public Nullable<int> DbId { get; set; }
        public Nullable<int> TableId { get; set; }
        public string ColumnName { get; set; }
    }
}
