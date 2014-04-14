using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Alumni.School_Module
{
    public class DummyAlumniModule : IAlumniModule
    {
        public Dictionary<string, List<AlumniPost>> GetAlumni(FilterBag filters)
        {
            Dictionary<string, List<AlumniPost>> alumni = new Dictionary<string, List<AlumniPost>>();
            alumni.Add("Microsoft", new List<AlumniPost>()
            {
                new AlumniPost(){
                    Email="ValidEmail@UA.edu",
                    FieldOfStudy="Software Engineering",
                    Location=new Location(){State="WA", City="Bellevue"},
                    Name="Colton Presler"
                },
                new AlumniPost(){
                    Email="ValidEmail@UA.edu",
                    FieldOfStudy="Software Engineering",
                    Location=new Location(){State="WA", City="Bellevue"},
                    Name="Chris Rosen"
                },
                new AlumniPost(){
                    Email="ValidEmail@UA.edu",
                    FieldOfStudy="Software Engineering",
                    Location=new Location(){State="WA", City="Bellevue"},
                    Name="David Lamont"
                },
                new AlumniPost(){
                    Email="ValidEmail@UA.edu",
                    FieldOfStudy="Computer Science",
                    Location=new Location(){State="WA", City="Bellevue"},
                    Name="Dan Corrigan"
                }
            });
            alumni.Add("Microsoft Game Studios", new List<AlumniPost>());

            return alumni;
        }

        public Source Source
        {
            get { throw new NotImplementedException(); }
        }
       
    }
}