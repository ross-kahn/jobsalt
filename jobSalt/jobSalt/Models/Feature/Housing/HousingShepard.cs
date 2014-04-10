﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

namespace jobSalt.Models.Feature.Housing
{
    public class HousingShepard : IHousingModule
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
        private List<IHousingModule> modules;
        #endregion // Private Member Variables

        #region Constructors
        public HousingShepard()
        {
            modules = new List<IHousingModule>();
            modules.Add(null);
        }
        #endregion // Constructors

        #region Public Methods
        public Dictionary<string, List<HousingPost>> GetHousing(FilterBag filters)
        {
            Dictionary<string, List<HousingPost>> houses = new Dictionary<string, List<HousingPost>>();

            if (filters.isEmpty())
            {
                return houses;
            }

            // Use a dictionary of module to bool so each module can mark when it's complete,
            // this is used incase of a timeout so it can be determined which module did not complete.
            Dictionary<IHousingModule, bool> moduleCompleted = new Dictionary<IHousingModule, bool>();
            foreach (IHousingModule module in modules)
            {
                moduleCompleted.Add(module, false);
            }

            object lockObject = new Object();

            var timeout = 5000; // 5 seconds
            var cts = new CancellationTokenSource();
            var t = new System.Threading.Timer(_ => cts.Cancel(), null, timeout, -1);

            try
            {
                Parallel.ForEach(modules,
                    new ParallelOptions { CancellationToken = cts.Token },
                    (module) =>
                    {
                        try
                        {
                            Dictionary<string, List<HousingPost>> partialJobs = module.GetHousing(filters);
                            lock (lockObject)
                            {
                                moduleCompleted[module] = true;
                                houses = houses.Concat(partialJobs).ToDictionary(e => e.Key, e => e.Value);
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
            return PostProcessAlumni(houses);
        }
        #endregion // Public Methods

        #region Private Methods
        /// <summary>
        /// Perform data checks on the list of alumni such as re-ordering and data validation
        /// </summary>
        /// <param name="alumni">Unprocessed list of jobs</param>
        /// <returns>Processed list of alumni</returns>
        Dictionary<string, List<HousingPost>> PostProcessAlumni(Dictionary<string, List<HousingPost>> houses) 
        {
            return houses;
        }
        #endregion // Private Methods

    }
}