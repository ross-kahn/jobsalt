using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using jobSalt.Models.Data_Types;
using System.Data.Common;

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
            List<JobPost> jobs = new List<JobPost>();

            string query = "select * from Jobs left join Employers on Jobs.employerId = Employers.id Where";
            List<SqlParameter> parameters = new List<SqlParameter>();

            List<string> whereClauses = new List<string>();

            // Only get jobs that were modified within the last year
            whereClauses.Add("[Jobs].modifiedDate > @RecentOrders");
            parameters.Add(new SqlParameter("RecentOrders", DateTime.Now.AddMonths(-6)));
            
            //Use a WHERE clause to match filters perhaps?
            if (!String.IsNullOrWhiteSpace(filters.CompanyName))
            {
                whereClauses.Add("[Employers].name like '%' + @CompanyName+ '%'");
                parameters.Add(new SqlParameter("CompanyName", filters.CompanyName));
            }

            if (filters.Location != null)
            {
                whereClauses.Add("([Employers].state = @StateLong OR [Employers].state = @StateShort) And [Employers].city = @City");
                parameters.Add(new SqlParameter("StateLong", filters.Location.StateLong));
                parameters.Add(new SqlParameter("StateShort", filters.Location.State));
                parameters.Add(new SqlParameter("City", filters.Location.City));
            }

            if (!String.IsNullOrWhiteSpace(filters.JobTitle))
            {
                whereClauses.Add("[Jobs].title like '%' + @JobTitle + '%'");
                parameters.Add(new SqlParameter("JobTitle", filters.JobTitle));
            }

            if (!String.IsNullOrWhiteSpace(filters.Keyword))
            {
                whereClauses.Add("[Jobs].description like '%' + @Keyword + '%' OR [Jobs].title like '%' + @Keyword + '%' OR [Employers].name like '%' + @Keyword + '%' OR [Jobs].qualifications like '%' + @Keyword + '%'");
                parameters.Add(new SqlParameter("Keyword", filters.Keyword));
            }

            if (!String.IsNullOrWhiteSpace(filters.FieldOfStudy))
            {
                whereClauses.Add("[Jobs].description like '%' + @FieldOfStudy + '%'");
                parameters.Add(new SqlParameter("FieldOfStudy", filters.FieldOfStudy));
            }

            if (filters.JobType != JobType.All)
            {
                if (filters.JobType == JobType.FullTime)
                {
                    whereClauses.Add("[Jobs].title like '%' + @JobType + '%'");
                    parameters.Add(new SqlParameter("JobType", "Fulltime"));
                }
                else if (filters.JobType == JobType.Internship)
                {
                    whereClauses.Add("[Jobs].title like '%' + @JobType + '%'");
                    parameters.Add(new SqlParameter("JobType", "Co-op"));
                }                
            }

            for(int i=0; i<whereClauses.Count; ++i)
                whereClauses[i] = "(" + whereClauses[i] + ")";

            query += String.Join(" And ", whereClauses);

            query += " Order By [Jobs].modifiedDate Desc ";

            var jobsQuery = dbContext.Jobs.SqlQuery(query, parameters.ToArray()).AsNoTracking();


            var jobsSearch = jobsQuery.Skip(page * resultsperpage);
            jobsSearch = jobsSearch.Take(resultsperpage);
            jobs = jobsSearch.Select( job =>
                new JobPost()
                {
                    Company = job.Employer.name,
                    DatePosted = (DateTime)job.modifiedDate,
                    Description = job.description,
                    JobTitle = job.title,
                    Location = new Location()
                    {
                     State = job.Employer.state,
                     City = job.Employer.city,
                     ZipCode = ""
                    },
                    URL = @"https://rit-csm.symplicity.com/students/index.php?mode=form&s=jobs&ss=jobs&id=" + job.id
                }).ToList();

            foreach (var job in jobs)
                job.SourceModule = source;
           

            return jobs;
        }

        private Source source = new Source() { Icon = @"\Content\images\RIT_icon.png", Name = "RIT" };
        public Source Source
        {
            get { return source; }
        }
    }
}