using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class DBMetaTable
    {
        public int Id { get; set; }
        public Nullable<int> DBId { get; set; }
        public string TableName { get; set; }
    }
}
