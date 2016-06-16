using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class PageCTRL
    {
        public int RecId { get; set; }
        public string ControlName { get; set; }
        public string ControlPath { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
        public Nullable<int> SFCount { get; set; }
        public string SelectionFormula1 { get; set; }
        public string SelectionFormula2 { get; set; }
        public string SelectionFormula3 { get; set; }
        public Nullable<int> IsRange { get; set; }
        public string FromValueSelectionFormula { get; set; }
        public string ToValueSelectionFormula { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }
}
