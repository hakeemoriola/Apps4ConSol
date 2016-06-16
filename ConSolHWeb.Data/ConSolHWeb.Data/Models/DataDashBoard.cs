using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class DataDashBoard
    {
        public int Id { get; set; }
        public Nullable<int> DbId { get; set; }
        public Nullable<int> TableId { get; set; }
        public int TotalRecordCount { get; set; }
        public int TotalUniqueCount { get; set; }
    }
}
