using jobSalt.Models.Data_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace jobSalt.Models.Feature.Alumni.School_Module
{
    public class RitAlumniModule : IAlumniModule
    {
        private SchoolAlumniDBContext db = new SchoolAlumniDBContext();

        public RitAlumniModule()
        {
            var siteConfig = Config.ConfigLoader.SiteConfig;
            db.ChangeDatabase(siteConfig.AlumniDBConnection);
        }

        Data_Types.Source IAlumniModule.Source
        {
            get { throw new NotImplementedException(); }
        }

        Dictionary<string, List<AlumniPost>> IAlumniModule.GetAlumni(FilterBag filters)
        {
            Dictionary<string, List<AlumniPost>> posts = new Dictionary<string, List<AlumniPost>>();

            var AlumSearchQuery = db.GradPlacements.Select( grad => new AlumniPost
                {
                    Company = grad.employerName,
                    Location = new Location
                        {
                            State = grad.employerStateId, 
                            City = grad.employerCity, 
                            ZipCode = ""
                        },
                    FieldOfStudy = grad.Program.name,
                    Name = grad.Student.FirstName + " " + "Smith",
                    PhoneNumber = "None found yet",
                    Email = "StudentEmail@University.com",
                    GraduatingYear = grad.Student.CurrentExpectedGradTerm ?? 0
                });


            AlumSearchQuery = AlumSearchQuery.Where(alum => alum.Company != null);

            if( !String.IsNullOrEmpty(filters.CompanyName) )
            {
                AlumSearchQuery = AlumSearchQuery.Where(alum => alum.Company.Contains(filters.CompanyName));
            }

            if (!String.IsNullOrEmpty(filters.Keyword))
            {
                AlumSearchQuery = AlumSearchQuery.Where(alum => alum.Name.Contains(filters.Keyword) || alum.Company.Contains(filters.Keyword) || alum.FieldOfStudy.Contains(filters.Keyword));
            }

            if (!String.IsNullOrEmpty(filters.FieldOfStudy))
            {
                AlumSearchQuery = AlumSearchQuery.Where(alum => alum.FieldOfStudy.Contains(filters.FieldOfStudy));
            }

           
            AlumSearchQuery = AlumSearchQuery.OrderBy(item => item.Company);


            foreach (var alum in AlumSearchQuery.ToList())
            {
                string company = alum.Company.Trim().ToLower();
                Regex rgx = new Regex("[^a-zA-Z0-9 &-]");
                company = rgx.Replace(company, "");
                company = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(company);


                if (posts.ContainsKey(company))
                {
                    posts[company].Add(alum);
                }
                else
                {
                    posts.Add(company, new List<AlumniPost>() { alum });
                }

                alum.GraduatingYear = ConvertTermToYear(alum.GraduatingYear);
                
            }

            return posts;
        }

        private int ConvertTermToYear(int term)
        {
            if(term == 0)
            {
                return 0;
            }

            int year = 0;

            if(term > 9999) // Dates in quarters
            {
                year = term / 10;
                int quarter = term % (year * 10);
                if (quarter > 2)
                {
                    year++;
                }
            }
            else // Dates in semesters
            {
                int melenium = term / 1000;
                int decade = (term / 10) % (100 * melenium);
                int semester = term % (((melenium * 100) + decade)*10);

                year = (melenium * 1000) + decade;
                if (semester > 3)
                {
                    year++;
                }
            }

            return year;
        }
    }
}