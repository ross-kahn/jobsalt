﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jobSalt.Models;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs
{
    interface IJobModule
    {

        /// <summary>
        /// The name to display to the user for this data source
        /// </summary>
        Source Source { get; }

        /// <summary>
        /// Asyncronously grap a set of job postings based off of the given filters and page number.
        /// </summary>
        /// <param name="filters">The filters that the module should use to query</param>
        /// <param name="page">What page number of results to grab</param>
        /// <returns>The job postings</returns>
        List<JobPost> GetJobs(Dictionary<Models.Field, string> filters, int page, int resultsPerPage);
    }
}
