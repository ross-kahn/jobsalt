using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Jobs
{
    public class School_Module : IJobModule
    {        
        public School_Module ()
        {
            // May need to initialize DisplayName and ResultsPerPage here
        }
        public int ResultsPerPage
        {
            get
            {
                return ResultsPerPage;
            }
            set
            {
                ResultsPerPage = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return DisplayName;
            }
            set
            {
                DisplayName = value;
            }
        }

        public async List<JobPost> GetJobs(List<Filter> filters, int page)
        {
            throw new NotImplementedException();
        }
    }
}