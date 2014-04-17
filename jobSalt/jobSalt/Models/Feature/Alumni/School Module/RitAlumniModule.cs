using jobSalt.Models.Data_Types;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var AlumSearchQuery = db.LinkedInData_.Join(db.Students, grad => grad.UID, 
                stud => stud.UID, 
                (grad, stud) => new AlumniPost
                {
                    Company = grad.Employer,
                    Location = new Location
                        {
                            State = grad.State, 
                            City = grad.City, 
                            ZipCode = grad.Zip_Code
                        },
                    FieldOfStudy = stud.Program.name,
                    Name = stud.FirstName + " " + stud.LastName,
                    PhoneNumber = "None found yet",
                    Email = grad.E_Mail_1
                });


            AlumSearchQuery = AlumSearchQuery.Where(alum => alum.Company != null);

            if( !String.IsNullOrEmpty(filters.CompanyName) )
            {
                AlumSearchQuery = AlumSearchQuery.Where(alum => alum.Company.Contains(filters.CompanyName));
            }

            if (!String.IsNullOrEmpty(filters.Keyword))
            {
                AlumSearchQuery = AlumSearchQuery.Where(alum => alum.Name.Contains(filters.Keyword));
            }

            if (!String.IsNullOrEmpty(filters.FieldOfStudy))
            {
                AlumSearchQuery = AlumSearchQuery.Where(alum => alum.FieldOfStudy.Contains(filters.FieldOfStudy));
            }

           
            AlumSearchQuery = AlumSearchQuery.OrderBy(item => item.Company);


            foreach (var alum in AlumSearchQuery.ToList())
            {
                if (posts.ContainsKey(alum.Company))
                {
                    posts[alum.Company].Add(alum);
                }
                else
                {
                    posts.Add(alum.Company, new List<AlumniPost>() { alum });
                }
                
            }

            return posts;
        }
    }
}