using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Nullable<int> IsActive { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string tempData { get; set; }
        public string KeyData { get; set; }
        public Nullable<int> Activated { get; set; }
        public Nullable<int> Invaildated { get; set; }
        public string Text_Pass { get; set; }
        public Nullable<int> Imported_data { get; set; }
        public Nullable<int> SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string PhoneNo { get; set; }
    }
}
