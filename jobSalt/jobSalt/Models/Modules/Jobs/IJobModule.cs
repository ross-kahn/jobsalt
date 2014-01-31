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
        /// This property is used to set how many results should be given per page.
        /// If a call to GetJobs returns less results than ResultsPerPage then it is assumed
        /// that the module has no more results.
        /// </summary>
        int ResultsPerPage { get; set; }

        /// <summary>
        /// The name to display to the user for this data source
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// Asyncronously grap a set of job postings based off of the given filters and page number.
        /// </summary>
        /// <param name="filters">The filters that the module should use to query</param>
        /// <param name="page">What page number of results to grab</param>
        /// <returns>The job postings. If less results are returned than ResultsPerPage then it is assumed
        /// that this module has no more results to provide.</returns>
        async List<JobPost> GetJobs(List<Filter> filters, int page);
    }
}
