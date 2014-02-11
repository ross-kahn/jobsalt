using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public class JobPost
    {
        public string URL { get; set; }
        public Source SourceModule { get; set; }
        public DateTime DatePosted { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public Location Location { get; set; }
        public string Salary { get; set; }
        public string Description { get; set; }
        public string FieldOfStudy { get; set; }
    }
}