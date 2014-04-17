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

            /*JobConfig config = ConfigLoader.JobConfig;
            foreach (var module in config.Modules)
            {

            }*/

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
		//Begin: Duplication removal logic
		if ( page==0 )
			{	//if on the first page, clear the hashes from session.
			HttpContext.Current.Session["Job_Fuzzy_Hashes"] = null;
			}
		//retrieve the job hash dictionary from session
		Dictionary<JobPost , string> jobHashDict = new Dictionary<JobPost , string>( );
		if ( HttpContext.Current.Session["Job_Fuzzy_Hashes"] != null && ( HttpContext.Current.Session["Job_Fuzzy_Hashes"] is Dictionary<JobPost , string> ) )
			jobHashDict = HttpContext.Current.Session["Job_Fuzzy_Hashes"] as Dictionary<JobPost , string>;
		//END: Duplication removal logic

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
            var t = new Timer(_ => cts.Cancel(), null, timeout, Timeout.Infinite);

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
				//Begin: Duplication removal logic
				//get a fuzzy hash for each jobPost
				int i =0;//unique number for each hash
				foreach ( var job in jobs )
					{



					//string jobHash = CalculateMD5Hash( job.Company+job.JobTitle );
					string jobHash = job.Company+" "+job.JobTitle+" "+i;
					//add hash to dictionary
					jobHashDict.Add( job , jobHash );
					i++;
					}
				//only remove duplicates if we have a reasonable number of jobs.
				if ( jobHashDict.Count( )>=10 )
					RemoveDuplicateJobs( jobHashDict , jobs );
			//End: Duplication removal logic 
            }
            catch(OperationCanceledException)
            {
                // This is where we should notify the user that a source timed out
                // The source can be determined by looking at the dictionary moduleCompleted
            }
            return PostProcessJobs(jobs);
        }
		/// <summary>
		/// Removes duplicate jobs from both the fuzzy hash dictionary and the jobs list.
		/// </summary>
		/// <param name="jobHashDict">The dictionary containing the jobPosts and their fuzzy hashes.</param>
		/// <param name="jobs">Current list of jobs that are to be shown to the user.</param>
		private void RemoveDuplicateJobs ( Dictionary<JobPost , string> jobHashDict , List<JobPost> jobs )
			{
			//keep track of duplicates
			List<JobPost> jobsToRemove = new List<JobPost>( );
			List<KeyValuePair<JobPost , string>> visited = new List<KeyValuePair<JobPost , string>>( );

			foreach ( KeyValuePair<JobPost , string> jobHashDictKV_a in jobHashDict )
				{
				visited.Add( jobHashDictKV_a );
				var compareList = from c in jobHashDict
								  where !visited.Any( a => a.Equals( c ) )
								  select c;
				foreach ( KeyValuePair<JobPost , string> jobHashDictKV_b in compareList )
					{
					Double threashold = 0.6;
					Double simScore = jobHashDictKV_a.Value.DiceCoefficient( jobHashDictKV_b.Value );
					//System.Diagnostics.Debug.WriteLine( "Fuzzy match score: "+ simScore +" similar." +"("+jobHashDictKV_a.Value+" , "+ jobHashDictKV_b.Value+ ")" );

					//compare a to b's hashes. remove if too similar
					if ( !jobHashDictKV_a.Key.Equals( jobHashDictKV_b.Key ) && ( Double.IsNaN( simScore ) || simScore>=threashold ) )
						{
						System.Diagnostics.Debug.WriteLine( "JobShepard found a duplicate, fuzzy match score: "+ simScore +" similar. Threashold = "+threashold+"\n"
														   +"\t[Source: "+jobHashDictKV_a.Key.SourceModule.Name+"\t\t\t  hash: " + jobHashDictKV_a.Value+"]...removing.\n" 
														   +"\t[Source: "+jobHashDictKV_b.Key.SourceModule.Name+"\t\t\t  hash: " + jobHashDictKV_b.Value+ "]\n" );
						//mark duplicate
						jobsToRemove.Add( jobHashDictKV_a.Key );
						}
					}
				}

			//remove duplicates from both jobHasDict and jobs
			Parallel.ForEach( jobsToRemove , ( duplicateJob ) =>
			{
				if ( jobs.Contains( duplicateJob ) )
					jobs.Remove( duplicateJob );
				if ( jobHashDict.ContainsKey( duplicateJob ) )
					jobHashDict.Remove( duplicateJob );
			} );
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
        List<JobPost> PostProcessJobs(List<JobPost> jobs) 
        {
            jobs = jobs.OrderByDescending(job => job.DatePosted).ToList();

            return jobs;
        }
        #endregion // Private Methods

    }
}