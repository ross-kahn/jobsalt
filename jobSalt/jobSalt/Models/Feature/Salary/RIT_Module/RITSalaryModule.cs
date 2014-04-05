using jobSalt.Models.Feature.Alumni.School_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Salary.RIT_Module
{
    public class RITSalaryModule : ISalaryModule
    {
        private SchoolAlumniDBContext db = new SchoolAlumniDBContext();

        public Data_Types.Source Source
        {
            get { throw new NotImplementedException(); }
        }

        public SalaryPost GetSalaries(FilterBag filters)
        {
            List<int> programIDs;

            if(String.IsNullOrEmpty( filters.FieldOfStudy ))
            {
                programIDs = db.Programs.Select(program => program.id).ToList();
            }
            else
            {
                var query = db.Programs.Where(program => program.name.Contains(filters.FieldOfStudy));
                programIDs = query.Select(program => program.id).ToList();
            }

            List<int> salaries;
            var salaryQuery = db.GradPlacements.Where(grad => grad.placementSalary != null 
                                                      && grad.placementSalary > 1000
                                                      && programIDs.Contains(grad.Program.id));
            salaryQuery = salaryQuery.OrderBy(grad => grad.placementSalary);
            salaries = salaryQuery.Select(grad => (int)grad.placementSalary).ToList();

            int median = salaries[(int)(salaries.Count / 2)];
            int mean = (int)salaries.Average();

            return new SalaryPost() { Source = "RIT", Average = mean, Median = median };
        }
    }
}