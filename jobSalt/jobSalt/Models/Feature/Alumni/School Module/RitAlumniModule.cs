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

        Dictionary<string, List<AlumniPost>> IAlumniModule.GetAlumni(Dictionary<Field, string> filters)
        {
            Dictionary<string, List<AlumniPost>> posts = new Dictionary<string, List<AlumniPost>>();

            var AlumSearchQuery = db.GradPlacements.Join(db.Students, grad => grad.studentUid, stud => stud.UID, (grad, stud) => new { Grad = grad, Stud = stud })
                .Join(db.Programs, inner => inner.Grad.studentPrimaryProgramId, outer => outer.id, (inner, outer) => new AlumniPost
                {
                    Company = inner.Grad.employerName,
                    Location = new Location()
                    {
                        ZipCode = "",
                        City = inner.Grad.employerCity,
                        State = inner.Grad.employerStateId
                    },
                    FieldOfStudy = outer.name,
                    Name = inner.Stud.FirstName + " " + inner.Stud.LastName,
                    PhoneNumber = "None found yet",
                    Email = inner.Grad.studentEmail
                });


            AlumSearchQuery = AlumSearchQuery.Where(alum => alum.Company != null);
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