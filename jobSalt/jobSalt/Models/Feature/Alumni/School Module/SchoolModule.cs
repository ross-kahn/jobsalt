using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Alumni.School_Module
{
    public class SchoolModule : IAlumniModule
    {
        private SchoolAlumniDBContext db = new SchoolAlumniDBContext();

        public Data_Types.Source Source
        {
            get { throw new NotImplementedException(); }
        }

        public List<AlumniPost> GetAlumni(Dictionary<Field, string> filters)
        {
            // This will probably always be called with a company filter specified (but don't require it)
            throw new NotImplementedException();
        }

        public List<string> GetCompanies()
        {
            throw new NotImplementedException();
        }
    }
}