
using System.Security.Cryptography;
using jobSalt.Models.Feature.Jobs.RIT_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using DuoVia.FuzzyStrings;
using jobSalt.Models.Config;

namespace jobSalt.Models.Feature.Jobs
{
    public class JobShepard
    {
        #region Properties
        public int MaxWaitTime { get; set; }
        #endregion // Properties

        #region Private Member Variables
        private List<IJobModule> modules;
        #endregion // Private Member Variables

        #region Constructors
        public JobShepard()
        {
            modules = new List<IJobModule>();

            // Set the maxium amount of time to wait for a module to complete in seconds.
            MaxWaitTime = 5;

            foreach (Module module in ConfigLoader.JobConfig.Modules)
            {
                if (module.Enabled)
                {
                    //Logging.JobSaltLogger.Instance.log("Launching Job Module: " + module.Name);
                    modules.Add((IJobModule)Activator.CreateInstance(Type.GetType(module.Name)));
                }
            }
        }
        #endregion // Constructors

        #region Public Methods
        public Task<List<JobPost>> GetJobsAsync(FilterBag filters, int page, int resultsPerModule)
        {
            HttpContext context = HttpContext.Current;
            Task<List<JobPost>> task = new Task<List<JobPost>>(() => 
                {
                    HttpContext.Current = context;
                    return GetJobs(filters, page, resultsPerModule);
                });
            task.Start();
            return task;
			}

        public List<JobPost> GetJobs(FilterBag filters, int page, int resultsPerModule)
        {
            if (filters.isEmpty())
            {
                return new List<JobPost>();
            }

            List<Task> tasks = new List<Task>();
            List<List<JobPost>> jobs = new List<List<JobPost>>();
            object lockObject = new object();

            foreach(IJobModule module in modules)
            {
                Task task = new Task(() => 
            {
                        try
                        {
                            List<JobPost> moduleJobs = module.GetJobs(filters, page, resultsPerModule);
                            lock (lockObject)
                            {
                                jobs.Add(moduleJobs);
                            }
                        }
                        catch (Exception)
                        {
                            // The module failed. Not a system failure but the user should be notified
                            // we need to create a mechanism to actually notify them and call it here
                        }
                        
                    });
                tasks.Add(task);
                task.Start();
                    }
            Task.WaitAll(tasks.ToArray(), MaxWaitTime * 1000);

            // Create a copy of jobs incase a module finishes late and tries to modifiy the
            // collection while we're still using it.
            List<List<JobPost>> duplicatedJobs = new List<List<JobPost>>(jobs);

            if (ConfigLoader.JobConfig.RemoveDuplicatePosts)
					{
                RemoveDuplicateJobs(duplicatedJobs, page);
            }

            return PostProcessJobs(duplicatedJobs); ;
        }
        
        private void RemoveDuplicateJobs(List<List<JobPost>> jobs, int page)
        {
            Dictionary<JobPost, string> jobHashDict = new Dictionary<JobPost, string>();

            if (page == 0)
            {	//if on the first page, clear the hashes from session.
                HttpContext.Current.Session["Job_Fuzzy_Hashes"] = null;
            }
            //retrieve the job hash dictionary from session

            if (HttpContext.Current.Session["Job_Fuzzy_Hashes"] != null && (HttpContext.Current.Session["Job_Fuzzy_Hashes"] is Dictionary<JobPost, string>))
                jobHashDict = HttpContext.Current.Session["Job_Fuzzy_Hashes"] as Dictionary<JobPost, string>;

            //Begin: Duplication removal logic
            //get a fuzzy hash for each jobPost
            foreach (var moduleJobs in jobs)
            {
                foreach (var job in moduleJobs)
                {
					//string jobHash = CalculateMD5Hash( job.Company+job.JobTitle );
                    string jobHash = job.Company + " " + job.JobTitle + " " + job.Location.City + " , " + job
                        .Location.State + " " + job.Location.ZipCode + " " + job.Description;
					//add hash to dictionary
                    jobHashDict.Add(job, jobHash);
                }
					}
				//only remove duplicates if we have a reasonable number of jobs.
            if (jobHashDict.Count() < 10)
                return;

			//keep track of duplicates
            List<JobPost> jobsToRemove = new List<JobPost>();
            List<KeyValuePair<JobPost, string>> visited = new List<KeyValuePair<JobPost, string>>();

            foreach (KeyValuePair<JobPost, string> jobHashDictKV_a in jobHashDict)
				{
                visited.Add(jobHashDictKV_a);
                var compareList = from c in jobHashDict
                                  where !visited.Any(a => a.Equals(c))
								  select c;
				foreach (KeyValuePair<JobPost, string> jobHashDictKV_b in compareList)
					{
                    Double threashold = 0.98;
                    Double simScore = jobHashDictKV_a.Value.DiceCoefficient(jobHashDictKV_b.Value);
					//System.Diagnostics.Debug.WriteLine( "Fuzzy match score: "+ simScore +" similar." +"("+jobHashDictKV_a.Value+" , "+ jobHashDictKV_b.Value+ ")" );

					//compare a to b's hashes. remove if too similar
                    if (!jobHashDictKV_a.Key.Equals(jobHashDictKV_b.Key) && (Double.IsNaN(simScore) || simScore >= threashold))
						{
                        System.Diagnostics.Debug.WriteLine("JobShepard found a duplicate, fuzzy match score: " + simScore + " similar. Threashold = " + threashold + "\n"
                                                           + "\t[Source: " + jobHashDictKV_a.Key.SourceModule.Name + "\t\t\t  hash: " + jobHashDictKV_a.Value + "]...removing.\n"
                                                           + "\t[Source: " + jobHashDictKV_b.Key.SourceModule.Name + "\t\t\t  hash: " + jobHashDictKV_b.Value + "]\n");
						//mark duplicate
                        jobsToRemove.Add(jobHashDictKV_a.Key);
						}
					}
				}

			//remove duplicates from both jobHasDict and jobs
            Parallel.ForEach(jobsToRemove, (duplicateJob) =>
            {
                foreach (var moduleJobs in jobs)
			{
                    if (moduleJobs.Contains(duplicateJob))
                        moduleJobs.Remove(duplicateJob);
                }
                if (jobHashDict.ContainsKey(duplicateJob))
                    jobHashDict.Remove(duplicateJob);
            });
			}
		private string CalculateMD5Hash ( string input )
			{
			// Calculate MD5 hash from input
			MD5 md5 = System.Security.Cryptography.MD5.Create( );
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes( input );
			byte[] hash = md5.ComputeHash( inputBytes );

			// Convert byte array to hex string
			StringBuilder sb = new StringBuilder( );
			for ( int i = 0 ; i < hash.Length ; i++ )
				{
				sb.Append( hash[i].ToString( "X2" ) );
				}
			return sb.ToString( );
			}
        #endregion // Public Methods

        #region Private Methods
        /// <summary>
        /// Perform data checks on the list of jobs such as re-ordering and data validation
        /// </summary>
        /// <param name="jobs">Unprocessed list of jobs</param>
        /// <returns>Processed list of jobs</returns>
        List<JobPost> PostProcessJobs(List<List<JobPost>> jobs) 
        {
            List<JobPost> interleavedJobs = new List<JobPost>();
            int totalJobs = jobs.Sum(list => list.Count);
            int listIndex = 0;
            int numLists = jobs.Count;
            // Yes this looks rather complicated, but it's not really.
            // For however many jobs are in all of the lists append them
            // to the new list one at a time from each module.
            for (int i = 0; i < totalJobs; i++)
            {
                for (int j = 0; j < numLists; j++)
                {
                    if (jobs[listIndex].Count > 0)
                    {
                        interleavedJobs.Add(jobs[listIndex][0]);
                        jobs[listIndex].RemoveAt(0);
                        listIndex = (listIndex + 1) % numLists;
                        break;
                    }
                    else
        {
                        listIndex = (listIndex + 1) % numLists;
                    }
                }
            }

            return interleavedJobs;
        }
        #endregion // Private Methods

    }
}