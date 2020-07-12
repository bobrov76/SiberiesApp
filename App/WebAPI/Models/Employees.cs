using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class Employees
    {
        public short IdEmployee { get; set; }
        public string NameEmployee { get; set; }
        public string SurnameEmployee { get; set; }
        public string PatronimicEmployee { get; set; }
        public string Email { get; set; }
        public string ComandRol { get; set; }
        public string AuthRol { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
