using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace jobSalt.Models.Modules.Jobs
{
    public class JobShepard
    {
        #region Properties
        public int NumberOfModules
        {
            get
            {
                return modules.Count;
            }
        }
        #endregion // Properties

        #region Private Member Variables
        private List<IJobModule> modules;
        #endregion // Private Member Variables

        #region Constructors
        public JobShepard()
        {
            modules = new List<IJobModule>();
            modules.Add(new IndeedModule());
            //modules.Add(new LinkedInModule());
        }
        #endregion // Constructors

        #region Public Methods
        public List<JobPost> GetJobs(Dictionary<Field, string> filters, int page, int resultsPerModule)
        {
            List<JobPost> jobs = new List<JobPost>();

            object lockObject = new Object();

            Parallel.ForEach(modules,
                (module) =>
                {
                    List<JobPost> partialJobs = module.GetJobs(filters, page, resultsPerModule);
                    lock(lockObject)
                    {
                        jobs.AddRange(partialJobs);
                    }
                }
            );

            return jobs;
        }
        #endregion // Public Methods

        #region Private Methods
        #endregion // Private Methods

    }
}