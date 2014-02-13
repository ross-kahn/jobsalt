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
            this.DisplayName = "JobZone";

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

        public List<JobPost> GetJobs(List<Filter> filters, int page, int resultsperpage)
        {
            //The SQL query needs to include JOINs across different databases based on filters

            //Ignoring the Filters for now
            List<JobPost> jobs = new List<JobPost>();
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
                post.Location = new Location("14623", States.NY, "Rochester");
                post.Salary = (r.NextDouble() * 1000).ToString();
                post.Source = new Source();
                jobs.Add(post);
            }
             */
            return jobs;
        }
    }
}