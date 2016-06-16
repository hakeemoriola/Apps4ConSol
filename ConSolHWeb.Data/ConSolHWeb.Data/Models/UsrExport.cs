using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class UsrExport
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> FileTypeId { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}
