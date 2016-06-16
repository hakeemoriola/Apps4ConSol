using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class FAQResource
    {
        public int Id { get; set; }
        public Nullable<int> FaqId { get; set; }
        public string FileName { get; set; }
        public Nullable<int> FileTypeId { get; set; }
    }
}
