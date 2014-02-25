using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Jobs.LinkedIn_Module.LinkedInJobPost
{
    public class LinkedInResult
    {
        public Jobs jobs { get; set; }
    }

    public class Jobs
    {
        public int _count { get; set; }
        public int _start { get; set; }
        public int _total { get; set; }
        public List<LinkedInJobPost> values { get; set; }
    }


}