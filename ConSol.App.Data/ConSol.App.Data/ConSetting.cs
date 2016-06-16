using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSol.App.Data
{
    public class ConSetting
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ServerIP { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Port { get; set; }
        public bool IsActive { get; set; }
    }
}