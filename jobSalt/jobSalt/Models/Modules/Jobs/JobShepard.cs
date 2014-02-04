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
            modules.Add(new DummyJobModule());
        }
        #endregion // Constructors

        #region Public Methods
        public List<JobPost> GetJobs(List<Filter> filters, int page, int resultsPerModule)
        {
            List<JobPost> jobs = new List<JobPost>();

            object lockObject = new Object();

            Parallel.ForEach(modules,
                () => new List<JobPost>(),
                (module, loopState, partialResult) =>
                {
                    return module.GetJobs(filters, page, resultsPerModule);
                },
                (partialResult) =>
                {
                    lock (lockObject)
                    {
                        jobs.AddRange(partialResult);
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