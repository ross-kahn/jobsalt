using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Config
{
    public class AlumniConfig
    {
        public int NumResults { get; set; }

        public AlumniConfig()
        {
            NumResults = 10;
        }
    }
}