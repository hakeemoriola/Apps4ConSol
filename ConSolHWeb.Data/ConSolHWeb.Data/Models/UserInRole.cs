using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class UserInRole
    {
        public int Id { get; set; }
        public string UserRoleName { get; set; }
        public string UserName { get; set; }
    }
}
