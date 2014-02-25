using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs
{
    public class DummyJobModule : IJobModule
    {
        public List<JobPost> GetJobs(Dictionary<Field, string> filters, int page, int resultsPerPage)
        {
            List<JobPost> jobs = new List<JobPost>();
            for (int i = 0; i < resultsPerPage; ++i )
            {
                jobs.Add(new JobPost()
                {
                    Company = "blah",
                    DatePosted = DateTime.Now,
                    Description = "Fake Posting",
                    FieldOfStudy = "Fishing",
                    JobTitle = "Job title " + ((page-1)*resultsPerPage+i),
                });
            }
            return jobs;
        }

        public Source Source
        {
            get { throw new NotImplementedException(); }
        }

    }
}