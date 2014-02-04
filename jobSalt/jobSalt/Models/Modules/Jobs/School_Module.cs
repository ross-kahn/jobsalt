using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Jobs
{
    public class School_Module : IJobModule
    {        
        public School_Module ()
        {
            // May need to initialize DisplayName and ResultsPerPage here
        }
        public int ResultsPerPage
        {
            get
            {
                return ResultsPerPage;
            }
            set
            {
                ResultsPerPage = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return DisplayName;
            }
            set
            {
                DisplayName = value;
            }
        }

        public async List<JobPost> GetJobs(List<Filter> filters, int page)
        {
            //Ignoring the Filters for now
            List<JobPost> jobs = new List<JobPost>();
            Random r = new Random();
            for (int i = 0; i < page; i++)
            {
                JobPost post = new JobPost();
                post.Company = "Microsoft";
                post.DatePosted = DateTime.Now;
                post.Description = "A real job you want to apply for " + r.Next(50);
                post.FieldOfStudy = "Software Engineering";
                post.JobTitle = "Taco Technician";
                post.Location = new Location("14623", States.NY, "Rochester");
                post.Salary = (r.NextDouble() * 1000).ToString();
                post.Source = "School Database";
                jobs.Add(post);
            }
            return jobs;
        }
    }
}