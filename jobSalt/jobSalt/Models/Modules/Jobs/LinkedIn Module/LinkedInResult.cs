using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Jobs.LinkedIn_Module
{
    public class LinkedInResult
    {
        public int Count { get; set; }
        public int Start { get; set; }
        public int Total { get; set; }

        public LinkedInJobPost[] Values { get; set; }
    }
}