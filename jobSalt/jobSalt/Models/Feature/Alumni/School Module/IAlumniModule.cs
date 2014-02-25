using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Alumni.School_Module
{
    interface IAlumniModule
    {
        /// <summary>
        /// The name to display to the user for this data source
        /// </summary>
        Source Source { get; }

        /// <summary>
        /// Asyncronously grap a set of alumni postings based off of the given filters and page number.
        /// </summary>
        /// <param name="filters">The filters that the module should use to query</param>
        /// <param name="page">What page number of results to grab</param>
        /// <returns>The alumni postings</returns>
        List<AlumniPost> GetAlumni(Dictionary<Models.Field, string> filters, int page, int resultsPerPage);
    }
}