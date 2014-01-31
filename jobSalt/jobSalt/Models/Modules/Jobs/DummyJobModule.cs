﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Jobs
{
    public class DummyJobModule : IJobModule
    {
        public int ResultsPerPage { get; set; }

        public string DisplayName { get { return "Dummy Data"; } }

        public List<JobPost> GetJobs(List<Filter> filters, int page)
        {
            List<JobPost> jobs = new List<JobPost>();
            for (int i = 0; i < ResultsPerPage; ++i )
            {
                jobs.Add(new JobPost()
                {
                    Company = "blah",
                    DatePosted = DateTime.Now,
                    Description = "Fake Posting",
                    FieldOfStudy = "Fishing",
                    JobTitle = "Job title",
                    Source = new Source() { Name = "DummyData" }
                });
            }
            return jobs;
        }
    }
}