using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Config
{
    public class Module
    {
        public string Name { get; set; }
        public string Enabled { get; set; }
    }

    public class JobConfig
    {

        public int NumResults { get; set; }

        public List<Module> Modules { get; set; }

        public JobConfig()
        {
            NumResults = 10;
        }
    }
}