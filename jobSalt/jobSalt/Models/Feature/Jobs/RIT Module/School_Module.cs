using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs.RIT_Module
{
    public class School_Module : IJobModule
    {
        //private SqlConnection connection;
        private ocecsEntities dbContext = new ocecsEntities();
        public School_Module ()
        {            
            InitializeSQL();
        }

        private void InitializeSQL()
        {
            var SiteConfig = Config.ConfigLoader.SiteConfig;
            dbContext.ChangeDatabase(SiteConfig.JobsDBConnection);
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

        public List<JobPost> GetJobs(FilterBag filters, int page, int resultsperpage)
        {
            //The SQL query needs to include JOINs across different databases based on filters

            //Ignoring the Filters for now
            List<JobPost> jobs = new List<JobPost>();
            var jobsSearchQuery = dbContext.Jobs.Join(dbContext.Employers, j => j.employerId, e => e.id, (j, e) => new { Job = j, Employer = e });
            
            //Use a WHERE clause to match filters perhaps?
            if (!String.IsNullOrWhiteSpace(filters.CompanyName))
            {
                jobsSearchQuery = jobsSearchQuery.Where(item => item.Job.Employer.name.Contains(filters.CompanyName));
            }

            if (!String.IsNullOrWhiteSpace(filters.JobTitle))
            {
                jobsSearchQuery = jobsSearchQuery.Where(item => item.Job.title.Contains(filters.JobTitle));
            }

            if (!String.IsNullOrWhiteSpace(filters.Keyword))
            {
                jobsSearchQuery = jobsSearchQuery.Where(item => item.Job.description.Contains(filters.Keyword));
            }

            // Only get jobs that were modified within the last year
            DateTime recent = DateTime.Now.AddYears(-1);
            jobsSearchQuery = jobsSearchQuery.Where(item => item.Job.modifiedDate > recent);

            jobsSearchQuery = jobsSearchQuery.OrderByDescending(item => item.Job.modifiedDate);
            var jobsSearch = jobsSearchQuery.Skip(page * resultsperpage);
            jobsSearch = jobsSearch.Take(resultsperpage);
            foreach(var job in jobsSearch.ToList())
            {
                JobPost j = new JobPost()
                {
                    Company = job.Employer.name,
                    DatePosted = (DateTime)job.Job.modifiedDate,
                    Description = job.Job.description,
                    JobTitle = job.Job.title,
                    Location = new Location("", job.Employer.state, job.Employer.city),
                    SourceModule = Source,
                    URL = @"https://rit-csm.symplicity.com/students/index.php?mode=form&s=jobs&ss=jobs&id=" + job.Job.id
                };

                jobs.Add(j);
            }

            return jobs;
        }

        private Source source = new Source() { Icon = @"\Content\images\RIT_icon.png", Name = "RIT" };
        public Source Source
        {
            get { return source; }
        }
    }
}