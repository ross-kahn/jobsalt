using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public class JobPost
    {
        public Source Source { get; set; }
        public DateTime DatePosted { get; set; }
        public String Company { get; set; }
        public String JobTitle { get; set; }
        public Location Location { get; set; }
        public String Salary { get; set; }
        public String Description { get; set; }
        public String FieldOfStudy { get; set; }
    }
}