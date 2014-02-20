using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace jobSalt.Models.Modules.Jobs.RIT_Module
{
    public class School_Module : IJobModule
    {
        private SqlConnection connection;
        private ocecsEntities dbContext = new ocecsEntities();
        public School_Module ()
        {            
            // Initialize SQL connection probably
            InitializeSQL();
        }

        private void InitializeSQL()
        {
            connection = new SqlConnection("database=ocecs;server=localhost");
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

        public List<JobPost> GetJobs(Dictionary<Field, string> filters, int page, int resultsperpage)
        {
            //The SQL query needs to include JOINs across different databases based on filters

            //Ignoring the Filters for now
            List<JobPost> jobs = new List<JobPost>();
            var jobsSearchQuery = dbContext.Jobs.Join(dbContext.Employers, j => j.employerId, e => e.id, (j, e) => new { Job = j, Employer = e });
            
            //Use a WHERE clause to match filters perhaps?
            foreach (Field key in filters.Keys)
            {
                switch (key)
                {
                    case Field.CompanyName:
                        jobsSearchQuery = jobsSearchQuery.Where(item => item.Job.Employer.name.Contains(filters[key]));
                        break;
                    default:
                        break;
                }
                //jobsSearchQuery = jobsSearchQuery.Where(item => it == f.TargetField);
            }

            jobsSearchQuery = jobsSearchQuery.OrderBy(item => item.Job.modifiedDate);
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

            /*
            connection.Open();

            //Two options, either Filter in the SQL or filter after the statement
            SqlCommand command = new SqlCommand("SELECT title,description,salary,postedDate,employerId from dbo.Jobs", connection);

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                JobPost j = new JobPost();
                j.JobTitle = reader.GetString(0);
                j.Description = reader.GetString(1);
                j.Salary = reader.GetString(2);
                j.DatePosted = reader.GetDateTime(3);
                j.Company = reader.GetString(4);
                jobs.Add(j);
            }

            /*
            Random r = new Random();
            for (int i = 0; i < page; i++)
            {
                JobPost post = new JobPost();
                post.Company = "Microsoft";
                post.DatePosted = DateTime.Now;
                post.Description = "A real job you want to apply for " + r.Next(50);
                post.FieldOfStudy = "Software Engineering";
                post.JobTitle = "Taco Technician";
                post.Location = new Location("14623", "NY", "Rochester");
                post.Salary = (r.NextDouble() * 1000).ToString();
                post.SourceModule = new Source();
                jobs.Add(post);
            }
             */
            return jobs;
        }

        private Source source = new Source() { Icon = @"\Content\images\RIT_icon.png", Name = "RIT" };
        public Source Source
        {
            get { return source; }
        }
    }
}