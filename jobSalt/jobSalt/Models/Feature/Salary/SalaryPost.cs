using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Salary
{
    public class SalaryPost
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Median { get; set; }
    }
}