using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace jobSalt.Models.Modules.Jobs
{
    public class School_Module : IJobModule
    {
        private SqlConnection connection;
        public School_Module ()
        {
            this.ResultsPerPage = 10;
            this.DisplayName = "School Job Server";

            // Initialize SQL connection probably            
            InitializeSQL();
        }

        private void InitializeSQL()
        {
            connection = new SqlConnection("database=ocecs;server=ocecs-seniorproject.rit.edu\\mssql,1211");
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

        public List<JobPost> GetJobs(List<Filter> filters, int page, int resultsperpage)
        {
            //The SQL query needs to include JOINs across different databases based on filters

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
                post.Source = new Source();
                jobs.Add(post);
            }
            return jobs;
        }
    }
}