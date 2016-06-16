using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class Module
    {
        public int RecID { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string ModuleImage { get; set; }
        public string ModuleDescription { get; set; }
        public byte active { get; set; }
        public byte Data_Dependent { get; set; }
        public byte M_Order { get; set; }
        public byte Is_Admin { get; set; }
        public string Group_Name { get; set; }
    }
}
