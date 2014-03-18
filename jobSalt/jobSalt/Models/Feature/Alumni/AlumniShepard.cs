using jobSalt.Models.Feature.Alumni.School_Module;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace jobSalt.Models.Feature.Alumni
{
    public class AlumniShepard
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
        private List<IAlumniModule> modules;
        #endregion // Private Member Variables

        #region Constructors
        public AlumniShepard()
        {
            modules = new List<IAlumniModule>();
            modules.Add(new RitAlumniModule());
        }
        #endregion // Constructors

        #region Public Methods
        public Dictionary<string, List<AlumniPost>> GetAlumni(FilterBag filters)
        {
            Dictionary<string, List<AlumniPost>> alumni = new Dictionary<string, List<AlumniPost>>();

            // Use a dictionary of module to bool so each module can mark when it's complete,
            // this is used incase of a timeout so it can be determined which module did not complete.
            Dictionary<IAlumniModule, bool> moduleCompleted = new Dictionary<IAlumniModule, bool>();
            foreach(IAlumniModule module in modules)
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
                            Dictionary<string, List<AlumniPost>> partialJobs = module.GetAlumni(filters);
                            lock (lockObject)
                            {
                                moduleCompleted[module] = true;
                                alumni = alumni.Concat(partialJobs).ToDictionary(e => e.Key, e => e.Value);
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
            return PostProcessAlumni(alumni);
        }
        #endregion // Public Methods

        #region Private Methods
        /// <summary>
        /// Perform data checks on the list of alumni such as re-ordering and data validation
        /// </summary>
        /// <param name="alumni">Unprocessed list of jobs</param>
        /// <returns>Processed list of alumni</returns>
        Dictionary<string, List<AlumniPost>> PostProcessAlumni(Dictionary<string, List<AlumniPost>> alumni) 
        {
            return alumni;
        }
        #endregion // Private Methods

    }
}