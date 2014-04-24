using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Salary
{
    public class SalaryPost
    {
        public string Source { get; set; }

        public int Median { get; set; }

        public int Average { get; set; }

        public double StandardDeviation { get; set; }
    }
}