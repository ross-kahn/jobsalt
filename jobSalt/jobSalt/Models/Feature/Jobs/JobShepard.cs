using jobSalt.Models.Feature.Jobs.RIT_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace jobSalt.Models.Feature.Jobs
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
            modules.Add(new Indeed_Module.IndeedModule());
            //modules.Add(new LinkedIn_Module.LinkedInModule());
            modules.Add(new School_Module());
			modules.Add( new CareerBuilder_Module.CareerBuilderModule( ) );
			//modules.Add( new UAJobLink_Module.UAJobLinkModule( ) );
			//modules.Add( new Dice_Module.DiceModule( ) );
        }
        #endregion // Constructors

        #region Public Methods
        public List<JobPost> GetJobs(FilterBag filters, int page, int resultsPerModule)
        {
            List<JobPost> jobs = new List<JobPost>();

            if (filters.isEmpty())
            {
                return jobs;
            }

            // Use a dictionary of module to bool so each module can mark when it's complete,
            // this is used incase of a timeout so it can be determined which module did not complete.
            Dictionary<IJobModule, bool> moduleCompleted = new Dictionary<IJobModule, bool>();
            foreach(IJobModule module in modules)
            {
                moduleCompleted.Add(module, false);
            }

            object lockObject = new Object();

            var timeout = 5000; // 5 seconds
            var cts = new CancellationTokenSource();
            var t = new Timer(_ => cts.Cancel(), null, timeout, -1);

            try
            {
                Parallel.ForEach(modules,
                    new ParallelOptions { CancellationToken = cts.Token },
                    (module) =>
                    {
                        try
                        {
                            List<JobPost> partialJobs = module.GetJobs(filters, page, resultsPerModule);
                            lock (lockObject)
                            {
                                moduleCompleted[module] = true;
                                jobs.AddRange(partialJobs);
                            }
                        }
                        catch (Exception)
                        {
                            // The module failed. Not a system failure but the user should be notified
                            // we need to create a mechanism to actually notify them and call it here
                        }
                        
                    }
                );
            }
            catch(OperationCanceledException)
            {
                // This is where we should notify the user that a source timed out
                // The source can be determined by looking at the dictionary moduleCompleted
            }
            return PostProcessJobs(jobs);
        }
        #endregion // Public Methods

        #region Private Methods
        /// <summary>
        /// Perform data checks on the list of jobs such as re-ordering and data validation
        /// </summary>
        /// <param name="jobs">Unprocessed list of jobs</param>
        /// <returns>Processed list of jobs</returns>
        List<JobPost> PostProcessJobs(List<JobPost> jobs) 
        {
            jobs = jobs.OrderByDescending(job => job.DatePosted).ToList();

            return jobs;
        }
        #endregion // Private Methods

    }
}