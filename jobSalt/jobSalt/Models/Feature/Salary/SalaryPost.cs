using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Salary
{
    public class SalaryPost
    {
        public String FieldOfStudy { get; set; }
        public SalaryRange UniversitySalaryRange { get; set; }
        public SalaryRange GeneralSalaryRange { get; set; }
    }
}