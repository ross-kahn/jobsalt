using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace jobSalt.Models.Feature.Salary
{
    public class SalaryShepard
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
        private List<ISalaryModule> modules;
        #endregion // Private Member Variables

        #region Constructors
        public SalaryShepard()
        {
            modules = new List<ISalaryModule>();
            modules.Add(new RIT_Module.RITSalaryModule());
        }
        #endregion // Constructors

        #region Public Methods
        public List<SalaryPost> GetAlumni(FilterBag filters)
        {
            List<SalaryPost> salaries = new List<SalaryPost>();

            // Use a dictionary of module to bool so each module can mark when it's complete,
            // this is used incase of a timeout so it can be determined which module did not complete.
            Dictionary<ISalaryModule, bool> moduleCompleted = new Dictionary<ISalaryModule, bool>();
            foreach (ISalaryModule module in modules)
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
                            SalaryPost salaryPartial = module.GetSalaries(filters);
                            lock (lockObject)
                            {
                                moduleCompleted[module] = true;
                                salaries.Add(salaryPartial);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.ToString());
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
            return PostProcessSalaries(salaries);
        }
        #endregion // Public Methods

        #region Private Methods
        /// <summary>
        /// Perform data checks on the list of salary such as re-ordering and data validation
        /// </summary>
        /// <param name="salary">Unprocessed list of salaries</param>
        /// <returns>Processed list of salaries</returns>
         List<SalaryPost> PostProcessSalaries(List<SalaryPost> salaries) 
        {
            return salaries;
        }
        #endregion // Private Methods

    }
}