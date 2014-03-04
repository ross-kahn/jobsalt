using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Alumni
{
    interface IAlumniModule
    {
        /// <summary>
        /// The name to display to the user for this data source
        /// </summary>
        Source Source { get; }

        /// <summary>
        /// Asyncronously grab a set of alumni postings based off of the given filters and page number.
        /// </summary>
        /// <param name="filters">The filters that the module should use to query</param>
        /// <param name="page">What page number of results to grab</param>
        /// <returns>The alumni postings</returns>
        List<AlumniPost> GetAlumni(Dictionary<Models.Field, string> filters);

        /// <summary>
        /// Return a list of companies that alumni have worked at.
        /// </summary>
        /// <returns>List of company names</returns>
        List<String> GetCompanies();

    }
}