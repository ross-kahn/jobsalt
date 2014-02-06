using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Jobs
{
    public class DummyJobModule : IJobModule
    {
        public string DisplayName { get { return "Dummy Data"; } }

        public List<JobPost> GetJobs(List<Filter> filters, int page, int resultsPerPage)
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
                    Source = new Source() { Name = "DummyData" }
                });
            }
            return jobs;
        }
    }
}