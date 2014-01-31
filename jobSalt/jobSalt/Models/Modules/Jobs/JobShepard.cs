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
        /// <summary>
        /// This property is used to set how many results should be given per page of each
        /// module. If there are 3 modules and 20 ResultsPerPage then 60 results will be returned.
        /// </summary>
        public int ResultsPerPage {
            get 
            {
                return resultsPerPage;
            }
            set
            {
                foreach (IJobModule module in modules)
                {
                    module.ResultsPerPage = value;
                }
                resultsPerPage = value;
            }
        }
        private int resultsPerPage;

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
        public JobShepard(int resultsPerPage)
        {
            modules = new List<IJobModule>();
            this.resultsPerPage = resultsPerPage;

            modules.Add(new DummyJobModule());

            foreach (IJobModule module in modules)
            {
                module.ResultsPerPage = resultsPerPage;
            }
        }

        public JobShepard() : this(20){}
        #endregion // Constructors

        #region Public Methods
        public List<JobPost> GetJobs(List<Filter> filters, int page)
        {
            List<JobPost> jobs = new List<JobPost>();

            object lockObject = new Object();

            Parallel.ForEach(modules, 
                () => new List<JobPost>(), 
            (module, loopState, partialResult) =>
            {
                return module.GetJobs(filters, page);
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