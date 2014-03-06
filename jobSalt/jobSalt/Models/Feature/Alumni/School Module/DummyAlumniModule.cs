using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Alumni.School_Module
{
    public class DummyAlumniModule : IAlumniModule
    {
        public List<AlumniPost> GetAlumni(Dictionary<Field, string> filters)
        {
            List<AlumniPost> alumni = new List<AlumniPost>();
            for (int i = 0; i < 4; ++i)
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

        public List<string> GetCompanies()
        {
            return new List<string>() { "OCE RIT", "Indeed", "Microsoft", "Google" };
        }

        public Source Source
        {
            get { throw new NotImplementedException(); }
        }
       
    }
}