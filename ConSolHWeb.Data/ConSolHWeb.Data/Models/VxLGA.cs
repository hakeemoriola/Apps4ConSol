using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxLGA
    {
        public int ID { get; set; }
        public string LGA_Name { get; set; }
        public Nullable<int> StateId { get; set; }
    }
}
