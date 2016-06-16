using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class Config
    {
        public int RecID { get; set; }
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
    }
}
