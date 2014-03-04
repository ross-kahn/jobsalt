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

        Data_Types.Source IAlumniModule.Source
        {
            get { throw new NotImplementedException(); }
        }

        List<AlumniPost> IAlumniModule.GetAlumni(Dictionary<Field, string> filters)
        {
            List<AlumniPost> posts = new List<AlumniPost>();

            var AlumSearchQuery = db.GradPlacements.Where(item => !item.employerName.Equals(null)); // Prune people without jobs? This could be more extensive probably

            foreach (var alum in AlumSearchQuery.ToList())
            {
                AlumniPost a = new AlumniPost()
                {
                    Company = alum.employerName,
                    Location = new Location("", alum.employerStateId, alum.employerCity),
                    FieldOfStudy = alum.studentPrimaryDegreeId + "",
                    Name = alum.studentEmail + " is temp variable",
                    PhoneNumber = "None found yet",
                    Email = alum.studentEmail
                };

                posts.Add(a);
            }

            return posts;
        }

        List<string> IAlumniModule.GetCompanies()
        {
            throw new NotImplementedException();
        }
    }
}