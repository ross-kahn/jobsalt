using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Config
{
    public class JobConfig
    {
        public int NumResults { get; set; }

        public JobConfig()
        {
            NumResults = 10;
        }
    }
}