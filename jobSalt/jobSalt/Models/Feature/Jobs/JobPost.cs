using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs
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

        // This doesn't need to be in a job post, right?
        // Field of study is only for searching...
        public string FieldOfStudy { get; set; }
    }
}