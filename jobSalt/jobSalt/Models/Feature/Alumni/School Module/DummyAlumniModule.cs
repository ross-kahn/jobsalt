using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Alumni.School_Module
{
    public class DummyAlumniModule : IAlumniModule
    {
        public Dictionary<string, List<AlumniPost>> GetAlumni(Dictionary<Field, string> filters)
        {
            Dictionary<string, List<AlumniPost>> alumni = new Dictionary<string, List<AlumniPost>>();
            alumni.Add("Microsoft", new List<AlumniPost>());
            alumni.Add("Google", new List<AlumniPost>());
            alumni.Add("Indeed", new List<AlumniPost>());
            alumni.Add("Fisher Price", new List<AlumniPost>());
            alumni.Add("Engadget", new List<AlumniPost>());

            foreach(var key in alumni.Keys)
            {
                alumni[key].Add(new AlumniPost()
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