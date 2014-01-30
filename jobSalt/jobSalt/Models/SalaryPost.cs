using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public class SalaryPost
    {
        public String FieldOfStudy { get; set; }
        public SalaryRange UniversitySalaryRange { get; set; }
        public SalaryRange GeneralSalaryRange { get; set; }
    }
}