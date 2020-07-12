using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class Tasks
    {
        public int Idtask { get; set; }
        public string NameTask { get; set; }
        public short IdSupervisor { get; set; }
        public short IdExecutor { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }
    }
}
