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

            

            return posts;
        }

        List<string> IAlumniModule.GetCompanies()
        {
            throw new NotImplementedException();
        }
    }
}