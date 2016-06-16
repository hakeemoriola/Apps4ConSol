using System;
using System.Collections.Generic;

namespace ConSolHWeb.Data.Models
{
    public partial class VxCustomer
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string CustomerType { get; set; }
        public string CustomerNo { get; set; }
        public string CallerProfile { get; set; }
        public string DateOfBirth { get; set; }
        public string Age { get; set; }
        public string Occupation { get; set; }
        public string Industry { get; set; }
        public string EmploymentStatus { get; set; }
        public virtual VxTelephone VxTelephone { get; set; }
    }
}
