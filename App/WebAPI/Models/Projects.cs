using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class Projects
    {
        public int Id { get; set; }
        public string NameProject { get; set; }
        public string NameСustomerCompany { get; set; }
        public string NameExecutingCompany { get; set; }
        public DateTime StartData { get; set; }
        public DateTime EndData { get; set; }
        public int Priority { get; set; }
        public string FilePath { get; set; }
    }
}
