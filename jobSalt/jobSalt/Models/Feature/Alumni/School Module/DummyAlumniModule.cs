using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Alumni.School_Module
{
    public class DummyAlumniModule : IAlumniModule
    {
        public List<AlumniPost> GetAlumni(Dictionary<Field, string> filters, int page, int resultsPerPage)
        {
            List<AlumniPost> alumni = new List<AlumniPost>();
            for (int i = 0; i < resultsPerPage; ++i)
            {
                alumni.Add(new AlumniPost()
                {
                    Company = "blah",
                    Location = new Location("14623","NY","Rochester"),
                    FieldOfStudy = "Fishing",
                    Name = "Jim Bondi",
                    PhoneNumber = "5558675309",
                    Email = "jtboce@rit.edu"
                });
            }
            return alumni;
        }

        public Source Source
        {
            get { throw new NotImplementedException(); }
        }

    }
}