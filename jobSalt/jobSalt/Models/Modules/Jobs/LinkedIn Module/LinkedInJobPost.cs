using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Jobs.LinkedIn_Module
{
    public class LinkedInJobPost
    {
        public string Company { get; set; }
        public string DescriptionSnippet { get; set; }
        public string LocationDescription { get; set; }
        public LinkedInPosition Position { get; set; }
        public LinkedInPostingDate PostingDate { get; set; }
        public string SiteJobURL { get; set; }
    }

    public class LinkedInPosition
    {
        public LinkedInExperienceLevel ExperienceLevel { get; set; }
        public LinkedInJobType JobType { get; set; }
        public LinkedInLocation Location { get; set; }
        public string Title { get; set; }
    }

    public class LinkedInExperienceLevel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class LinkedInJobType
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class LinkedInLocation
    {
        public LinkedInCountry Country { get; set; }
        public string Name { get; set; }
    }

    public class LinkedInCountry
    {
        public string Code { get; set; }
    }

    public class LinkedInPostingDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

    }

}