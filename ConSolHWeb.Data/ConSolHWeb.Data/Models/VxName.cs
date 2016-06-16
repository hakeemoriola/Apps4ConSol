using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxName
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Surname { get; set; }
        public string fullName { get; set; }
        public string Gender { get; set; }
    }
}
