using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public class SalaryRange
    {
        public int min { get; set; }
        public int max { get; set; }
        public double average { get; set; }
        public int median { get; set; }
    }
}