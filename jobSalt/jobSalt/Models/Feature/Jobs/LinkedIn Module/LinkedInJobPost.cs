using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Jobs.LinkedIn_Module.LinkedInJobPost
{
    public class Company
    {
        public string name { get; set; }
    }

    public class ExperienceLevel
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class JobType
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Country
    {
        public string code { get; set; }
    }

    public class Location
    {
        public Country country { get; set; }
        public string name { get; set; }
    }

    public class Position
    {
        public ExperienceLevel experienceLevel { get; set; }
        public JobType jobType { get; set; }
        public Location location { get; set; }
        public string title { get; set; }
    }

    public class PostingDate
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    public class LinkedInJobPost
    {
        public Company company { get; set; }
        public string descriptionSnippet { get; set; }
        public string locationDescription { get; set; }
        public Position position { get; set; }
        public PostingDate postingDate { get; set; }
        public string siteJobUrl { get; set; }
    }

}