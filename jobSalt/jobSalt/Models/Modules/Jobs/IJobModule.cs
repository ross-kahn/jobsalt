using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jobSalt.Models;

namespace jobSalt.Models.Modules.Jobs
{
    interface IJobModule
    {

        /// <summary>
        /// The name to display to the user for this data source
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Asyncronously grap a set of job postings based off of the given filters and page number.
        /// </summary>
        /// <param name="filters">The filters that the module should use to query</param>
        /// <param name="page">What page number of results to grab</param>
        /// <returns>The job postings</returns>
        List<JobPost> GetJobs(List<Filter> filters, int page, int resultsPerPage);
    }
}
