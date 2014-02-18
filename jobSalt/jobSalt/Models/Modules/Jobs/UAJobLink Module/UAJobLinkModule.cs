using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Jobs.UAJobLink_Module
{
	public class UAJobLinkModule : IJobModule
	{

		//Still mostly an empty module for now.
		// Simplicity data from wildcat joblink needs to be cached first in order to be queried.

		private	Source source = new Source( )
			{
				Name = "UAJobLink" ,
				Icon = @"\Content\images\UA_Block_A.png"
			};
		public Source Source
		{
			get
			{
			return Source;
			}
		}
		/// <summary>
		/// Asyncronously grap a set of job postings based off of the given filters and page number.
		/// </summary>
		/// <param name="filters">The filters that the module should use to query</param>
		/// <param name="page">What page number of results to grab</param>
		/// <returns>The job postings</returns>
        public List<JobPost> GetJobs(Dictionary<Field, string> filters, int page, int resultsPerPage)
		{

		List<JobPost> jobs = new List<JobPost>( );
		for ( int i = 0 ; i < resultsPerPage ; ++i )
			{
			jobs.Add( new JobPost( )
			{
				Company = "None" ,
				DatePosted = DateTime.Now ,
				Description = "Default Posting from University of Arizona's Wildcat Joblink" ,
				FieldOfStudy = "Computer Science" ,
				JobTitle = "Default ",
				Location = new Location(),
				 Salary =null,
				  SourceModule = source,
				URL = "www.career.arizona.edu/joblink"
			} );
			}
		return jobs;
		}
    }
}